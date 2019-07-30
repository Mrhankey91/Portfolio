using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            InteractWithObject();
        }
    }

    public void InteractWithObject()
    {
        try
        {
            if (Camera.main == null)
                return;

            //Ray ray = Camera.current.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
                if (hit.transform.GetComponent<InteractComponent>() != null)
                {
                    hit.transform.GetComponent<InteractComponent>().onInteract();
                }
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Missing Camera!");
        }
    }
}
