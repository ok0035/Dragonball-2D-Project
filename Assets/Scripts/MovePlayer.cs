using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float moveSpeed = 15f;
    public float drag = 0.5f;
    public Vector2 moveVector { set; get; }
    public VirtualJoyStick joystick;

    private Rigidbody2D thisRigidbody;
    private Transform transform;

    // Use this for initialization
    void Start () {

        thisRigidbody = gameObject.AddComponent<Rigidbody2D>();
        thisRigidbody.gravityScale = 0;
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        moveVector = PoolInput();

        Move();
	}

    private void Move()
    {
        //thisRigidbody.AddForce((moveVector * moveSpeed));
        transform.position += new Vector3(moveVector.x, moveVector.y, 0);
    }

    private Vector2 PoolInput()
    {
        Vector2 dir = Vector2.zero;
        //dir.x = Input.GetAxis("Horizontal");
        //dir.z = Input.GetAxis("Vertical");

        dir.x = joystick.Horizontal() * 0.1f * moveSpeed;
        dir.y = joystick.Vertical() * 0.1f * moveSpeed;

        if (dir.magnitude > 1) dir.Normalize();

        return dir;
    }

}
