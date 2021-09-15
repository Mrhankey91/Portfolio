using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinsController coinsController;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        coinsController = GameObject.Find("GameController").GetComponent<CoinsController>();
    }

    public void Init(Vector3 position)
    {
        transform.localPosition = position;
        hit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hit)
            return;

        if(coinsController == null)
            coinsController = GameObject.Find("GameController").GetComponent<CoinsController>();

        hit = true;
        coinsController.AddCoins();
        gameObject.SetActive(false);
    }
}
