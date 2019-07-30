using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : TileObject
{
    public override void Hide()
    {
        base.Hide();
        GameManager.instance.levelGenerator.coins.Add(this);
        gameObject.SetActive(false);
    }

    public override void Hit(Player player)
    {
        base.Hit(player);
        GetComponent<AudioSource>().Play();
        GameManager.instance.AddCoins(1);
        GetComponent<Animator>().SetTrigger("CoinCollected");
    }
}
