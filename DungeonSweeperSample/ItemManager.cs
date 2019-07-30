using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public GameObject key;
    public GameObject health;
    public GameObject weapon;
    public GameObject coin;
    public GameObject life;

    private Player player;

    public int numberItems = 0;

    public int weaponChance = 5;
    public int healthChance = 5;
    public int coinsChance = 25;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>() as Player;
    }

    public void Init(int levelId)
    {
        numberItems = 3;
    }

    public void AddItems(List<Tile> tiles)
    {
        bool keyAdded = false;
        if (player == null)
            player = GameObject.Find("Player").GetComponent<Player>() as Player;

        //List<int> livesInLevel = player.LevelIsHoldingLife(GameInfo.Instance.levelId);
        //int numberLives = livesInLevel.Count;

        foreach (Tile t in tiles)
        {
            if (!keyAdded)
            {
                AddItem<Key>(key, t, 1);
                keyAdded = true;
            }
            /*else if (numberLives > 0)
            {
                AddItem<Life>(life, t, livesInLevel[numberLives-1]);
                numberLives--;
            }*/
            else
            {
                RandomItem(t);
            }
            //obj.transform.parent = t.transform;
            //obj.GetComponent<SpriteRenderer>().sortingOrder = t.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }

    private void AddItem<T>(GameObject go, Tile t, int value) where T : Item
    {
        GameObject obj = Instantiate(go, t.gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        t.SetItem(obj.GetComponent<T>()); 
        t.item.Init(value);
        obj.transform.parent = t.transform;
    }

    private void AddKeyItem(Tile t)
    {
        GameObject obj = Instantiate(key, t.gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        t.SetItem(obj.GetComponent<Key>() as Key);
        obj.transform.parent = t.transform;
    }

    private void AddHealthItem(Tile t)
    {
        GameObject obj = Instantiate(health, t.gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        t.SetItem(obj.GetComponent<Health>() as Health);
        t.item.Init(1);
        obj.transform.parent = t.transform;
    }

    public void EnemyKilled(Tile t)
    {
        t.type = 5;
        //AddHealthItem(t);

        RandomItem(t);
    }

    private void RandomItem(Tile t)
    {
        int rand = Random.Range(0, 100);

        if (player.activeCharacter.health <= 3)
        {
            if (rand <= 25)
                AddItem<Weapon>(weapon, t, 1);
            else
                AddItem<Health>(health, t, 1);
        }
        else if (rand <= weaponChance)
            AddItem<Weapon>(weapon, t, 1);
        else if (rand <= weaponChance + healthChance)
            AddItem<Health>(health, t, 5);
        else if (rand <= weaponChance + healthChance + coinsChance)
            AddItem<Coin>(coin, t, 5);
        else
            AddItem<Coin>(coin, t, 1);
    }
}
