using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    public CharacterJoint joint;
    private Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        joint = GetComponent<CharacterJoint>() as CharacterJoint;
        player = transform.parent.Find("Body/Rig/root/Spine");
    }

    private void FixedUpdate()
    {
        if(joint != null && player.transform.position.y < transform.position.y)
        {
            transform.parent = null;
            Destroy(joint);
        }
    }

    private void OnJointBreak(float breakForce)
    {
        transform.parent = null;
        Destroy(joint);
    }
}
