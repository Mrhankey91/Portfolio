using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Transform playerBody;
    public Transform tubeBody;
    private float speed = 5f;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Mouse();
#elif UNITY_ANDROID || UNITY_IOS
        Touch();
#endif
    }

    private void Mouse()
    {
        //Move(Input.GetAxis("Horizontal"));
        Move(Input.mousePosition);
    }

    private void Touch()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            gameController.ShowTouchInstructions(false);

        Move(Input.GetTouch(0).position);
    }

    private void Move(Vector3 input)
    {
        input.x = Mathf.Clamp(input.x, 0, Screen.width);
        MoveBody(transform, input);
        if (tubeBody.GetComponent<Joint>())
            ;// MoveBody(tubeBody, input);
    }

    private void MoveBody(Transform body, Vector3 input)
    {
        Vector3 worldPosition = body.position;
        worldPosition.x = Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, 15f)).x; //15f is camera distance with player
        body.position = Vector3.MoveTowards(body.position, worldPosition, speed * Time.deltaTime);
    }

    public void UnfreezeRigidBodies()
    {
        playerBody.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        tubeBody.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
