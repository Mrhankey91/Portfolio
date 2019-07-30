using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public virtual void Init(bool isEdge)
    {
        transform.localScale = Vector3.one;
    }
    
    public virtual void Hide()
    {

    }
    public virtual void Hit(Player player)
    {
    }

}
