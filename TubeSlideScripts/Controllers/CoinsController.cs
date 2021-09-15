using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    public int coins = 0;

    public delegate void CoinUpdate(int amount);
    public CoinUpdate OnCoinUpdate;

    // Start is called before the first frame update
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        OnCoinUpdate?.Invoke(coins);
    }

    public void AddCoins(int value = 1)
    {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
        OnCoinUpdate?.Invoke(coins);
    }

    public void RemoveCoins(int value = 1)
    {
        coins = Mathf.Max(coins - value, 0);
        PlayerPrefs.SetInt("Coins", coins);
        OnCoinUpdate?.Invoke(coins);
    }

    public bool HasCoins(int amount)
    {
        return coins >= amount;
    }
}
