using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followObject;
    private Vector3 offset;
    private Vector3 pos;

    private void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!followObject)
            return;

        pos = followObject.position + offset;
        pos.x = 0f;
        transform.position = pos;// Vector3.Lerp(transform.position, pos, 10f * Time.deltaTime) ;
    }
}
