using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    public int speed = 1;
    public int health = 1;
    public int attack = 1;
    public int block = 1;

    public virtual bool ReceiveDamage(int amount)
    {
        if (!Block(amount))
            health = System.Math.Max(health - amount, 0);
        else
            Debug.Log("Block");

        return health <= 0;
    }

    public virtual bool Block(int damage)
    {
        return Random.Range(0, block+1) > damage;
    }

    public virtual void GiveHealth(int amount)
    {
        this.health += amount;
    }

    public virtual int DoDamage()
    {
        return attack;
    }
}
