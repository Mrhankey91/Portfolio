using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject dogPrefab;
    public GameObject catPrefab;
    public GameObject tortoisePrefab;
    public GameObject playerPrefab;

    private GameObject lastPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject Respawn()
    {
        if (lastPlayer)
        {
            GameObject.Destroy(lastPlayer.GetComponent<PlayerControls>().tubeBody.gameObject);
            GameObject.Destroy(lastPlayer);
        }

        lastPlayer = Instantiate(playerPrefab);
        return lastPlayer;
    }
}
