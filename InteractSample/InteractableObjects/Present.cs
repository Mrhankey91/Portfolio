using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractComponent))]
public class Present : MonoBehaviour
{
    void Start()
    {
        GetComponent<InteractComponent>().onInteract += Interact; 
    }

    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("Open");
    }
}
