using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    public delegate void OnInteract();
    public OnInteract onInteract;

}
