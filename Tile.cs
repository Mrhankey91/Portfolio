using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : TileObject
{
    public bool isEdge = false;
    public TileObject tileObject;
    private bool isEmpty = false;

    public override void Init(bool isEdge)
    {
        base.Init(isEdge);
        this.isEdge = isEdge;
    }

    public void Reset()
    {
        isEmpty = false;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void IsEmpty()
    {
        isEmpty = true;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public override void Hit(Player player)
    {
        base.Hit(player);
        if (isEmpty)
        {
            player.isFalling = true;
        }
    }
}
