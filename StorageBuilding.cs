using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBuilding : TileObject
{
    private bool faceRight = true;
    private ItemColor.Color color;
    public Material[] materials = new Material[ItemColor.NumberOfColors()];

    public override void Init(bool isEdge)
    {
        base.Init(isEdge);
        if (isEdge)
        {
            if (transform.parent.position.x < 0)
                faceRight = true;
            else
                faceRight = false;
        }
        else
        {
            if (Random.Range(0, 2) == 0)
                faceRight = true;
            else
                faceRight = false;
        }

        if (faceRight)
        {
            transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        color = ItemColor.RandomColor();
        Material[] matArray = GetComponent<MeshRenderer>().materials;
        matArray[1] = materials[(int)color];
        GetComponent<MeshRenderer>().materials = matArray;
    }

    public override void Hide()
    {
        base.Hide();
        GameManager.instance.levelGenerator.storageBuildings.Add(this);
        gameObject.SetActive(false);
    }

    public override void Hit(Player player)
    {
        base.Hit(player);
        if(player.goRight && !faceRight || !player.goRight && faceRight)//IN STORAGE POINTS
        {
            if (player.truckColor == color)
                player.inStorageCorrect = true;
            else
                player.inStorage = true;
        }
        else//WALL GAMEOVER
        {
            player.HitStorageWall();
        }
    }
}
