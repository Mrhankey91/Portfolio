using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampTextureAnimation : MonoBehaviour
{
    public Material material;
    private Vector2 offset = new Vector2();
    private void Start()
    {
        material.SetTextureOffset("_MainTex", offset);
    }

    private void FixedUpdate()
    {
        offset.y -= 0.01f;// 0.4f * Time.deltaTime;

        if (offset.y <= -1)//keep number small
            offset.y += 1;

        material.SetTextureOffset("_MainTex", offset);
    }
}
