using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsLabel : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        GameObject.Find("GameController").GetComponent<CoinsController>().OnCoinUpdate += OnCoinUpdate;
    }

   private void OnCoinUpdate(int amount)
    {
        text.text = "" + amount;
    }
}
