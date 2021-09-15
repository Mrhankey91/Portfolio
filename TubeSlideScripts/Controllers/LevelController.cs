using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Transform levelParent;

    public GameObject coinPrefab;
    public GameObject rampPrefab;
    public GameObject pillarPrefab;

    private Coin[] coins = new Coin[3];
    private List<GameObject> rampsPool = new List<GameObject>();
    private List<GameObject> pillarsPool = new List<GameObject>();

    public LevelData levelData;

    // Start is called before the first frame update
    void Awake()
    {
        levelParent = GameObject.Find("Slide").transform;
        //SpawnCoins();
    }

    public void LoadLevel(int id)
    {
        if (id > -1)
        {
            string filePath = string.Format("Levels/Level{0}", id);
            string jsonString = Resources.Load<TextAsset>(filePath).text;
            levelData = JsonUtility.FromJson<LevelData>(jsonString);
        }
        //RespawnCoins();
        RespawnObstacles();
    }

    private void SpawnCoins()
    {
        for(int i = 0; i < coins.Length; ++i)
        {
            coins[i] = GameObject.Instantiate(coinPrefab, new Vector3(0f, -1000f, 0f), Quaternion.identity, levelParent).GetComponent<Coin>();
            //coins[i].transform.SetParent(levelParent, true);
        }
    }

    private void RespawnCoins()
    {
        for (int i = 0; i < coins.Length; ++i)
        {
            coins[i].gameObject.SetActive(true);
            coins[i].Init(levelData.coins[i].GetWorldPosition());
        }
    }

    private void RespawnObstacles()
    {
        LevelData.ObstacleData[] obstacles = levelData.obstacles;
        int numberRamps = 0;
        int numberPillars = 0;

        foreach(LevelData.ObstacleData obstacle in obstacles)
        {
            if (obstacle.type == "ramp")
            {
                SpawnObstacle(rampPrefab, rampsPool, numberRamps, obstacle.position, obstacle.z);
                numberRamps++;
            }
            else if (obstacle.type == "pillar")
            {
                SpawnObstacle(pillarPrefab, pillarsPool, numberPillars, obstacle.position, obstacle.z);
                numberPillars++;
            }
        }
    }

    private void SpawnObstacle(GameObject prefab, List<GameObject> pool, int numberObject, int position, float z)
    {
        GameObject obj;
        float x = position == 0 ? -4 : position == 1 ? 0 : 4;
        if (numberObject < pool.Count)
        {
            obj = pool[numberObject];
            obj.SetActive(true);
            obj.transform.localPosition = new Vector3(x, -2.5f, z);
        }
        else
        {
            obj = Instantiate(prefab, levelParent);
            obj.transform.localPosition = new Vector3(x, -2.5f, z);
            pool.Add(obj);
        }
    }

    public void RestartLevel()
    {
        LoadLevel(-1);
    }

    public void NextLevel()
    {
        LoadLevel(levelData.level + 1);
    }
    public bool HasNextLevel()
    {
        return Resources.Load(string.Format("Levels/Level{0}", levelData.level + 1)) != null;
    }

    public bool LevelExists(int id)
    {
        string filePath = string.Format("Levels/Level{0}", id);
        return Resources.Load(filePath) != null;
    }
}
