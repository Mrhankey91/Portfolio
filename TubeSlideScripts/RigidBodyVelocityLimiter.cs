using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyVelocityLimiter : MonoBehaviour
{
    public bool x = false;
    public bool y = false;
    public bool z = false;

    public Vector2 xLimit;
    public Vector2 yLimit;
    public Vector2 zLimit;

    public float rotXLimit;
    public float rotYLimit;
    public float rotZLimit;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LimitVelocity();
        LimitRotation();
    }

    private void LimitVelocity()
    {
        if (!x && !y && !z)
            return;

        Vector3 velocity = rigidBody.velocity;

        if (x)
            velocity.x = Mathf.Clamp(velocity.x, xLimit.x, xLimit.y);
        if (y)
            velocity.y = Mathf.Clamp(velocity.y, yLimit.x, yLimit.y);
        if (z)
            velocity.z = Mathf.Clamp(velocity.z, zLimit.x, zLimit.y);

        rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, velocity, 1f);
    }

    private void LimitRotation()
    {
        if (rotXLimit == 0 && rotYLimit == 0 && rotZLimit == 0)
            return;

        Vector3 angularVelocity = rigidBody.angularVelocity;

        if (rotXLimit != 0)
            angularVelocity.x = Mathf.Clamp(angularVelocity.x, -rotXLimit, rotXLimit);
        if (rotYLimit != 0)
            angularVelocity.y = Mathf.Clamp(angularVelocity.y, -rotYLimit, rotYLimit);
        if (rotZLimit != 0)
            angularVelocity.z = Mathf.Clamp(angularVelocity.z, -rotZLimit, rotZLimit);

        rigidBody.angularVelocity = angularVelocity;
    }
}
