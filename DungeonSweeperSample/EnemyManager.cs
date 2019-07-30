using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemy;

    private Player player;

    private int startNumberEnemies = 3;
    public int numberEnemies = 3;
    public int levelId = 1;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>() as Player;

        return;

        for (int i = 1; i < 20; i++)
        {
            int speed = (int)(((i / 3f) + (GameInfo.Instance.gameDifficulty / 3f)));
            int health = (int)(((i / 3f) + (GameInfo.Instance.gameDifficulty / 3f)));
            int attack = (int)(((i / 3f) + (GameInfo.Instance.gameDifficulty / 3f)));

            int test = (int)(Mathf.Pow(i, 1.1f * (GameInfo.Instance.gameDifficulty / 3f)));

            Debug.Log("Level " + i + " ---> " + test);
            //Debug.Log("Level " + i + " ---> Speed: " + speed + " Health: " + health + " Do damage: " + doDamage);
        }
    }

    public void Init(int levelId)
    {
        this.levelId = levelId;
        NumberEnemies();//numberEnemies = (int)( 3 + levelId * (GameInfo.Instance.gameDifficulty / 3f) );
    }

    private void NumberEnemies()
    {
        if (GameInfo.Instance.gameDifficulty == 1)
        {
            numberEnemies = startNumberEnemies + levelId / 5;
        }
        else if (GameInfo.Instance.gameDifficulty == 2)
        {
            numberEnemies = startNumberEnemies + levelId / 3;
        }
        else if (GameInfo.Instance.gameDifficulty == 3)
        {
            numberEnemies = startNumberEnemies + levelId - 1;
        }

        numberEnemies = System.Math.Min(numberEnemies, 10);
    }

    public void AddEnemies(List<Tile> tiles)
    {
        foreach (Tile t in tiles)
        {
            GameObject obj = Instantiate(enemy, t.gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            t.enemy = obj.GetComponent<Enemy>() as Enemy;
            SetEnemy(t.enemy);
            obj.transform.parent = t.transform;
            obj.GetComponent<SpriteRenderer>().sortingOrder = t.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    private void SetEnemy(Enemy enemy)
    {
        int speed = (int)(Mathf.Pow(levelId, 1.1f * (GameInfo.Instance.gameDifficulty / 3f))) * enemy.speed; //(int)((levelId / 2.0f) + (gameDifficulty / 2f)) * enemy.speed;
        int health = (int)(Mathf.Pow(levelId, 1.1f * (GameInfo.Instance.gameDifficulty / 3f))) * enemy.health; //(int)((levelId / 2.0f) + (gameDifficulty / 2f)) * enemy.health;
        int attack = (int)(Mathf.Pow(levelId, 1.1f * (GameInfo.Instance.gameDifficulty / 3f))) * enemy.attack; //(int)((levelId / 2.0f) + (gameDifficulty / 2f)) * enemy.doDamage;

        enemy.Init(speed, health, attack);
    }
}
