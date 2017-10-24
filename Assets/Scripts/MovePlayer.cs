using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    private const float moveSpeedConst = 0.1f;
    public float moveSpeed = 2f;
    public Vector2 moveVector { set; get; }
    public VirtualJoyStick joystick;
    public bool moveFlag;

    // Use this for initialization
    void Start () {

        moveFlag = true;

    }
	
	// Update is called once per frame
	void Update () {

        Move();
	}

    private void Move()
    {
        //thisRigidbody.AddForce((moveVector * moveSpeed));
        moveVector = PoolInput();
        PlayerFSM fsm = GetComponent<PlayerFSM>();

        if (moveFlag)
        {
            if (moveVector.x < 0)
            {
                fsm.currentState = PlayerFSM.State.BACK;
            }
            else if (moveVector.x > 0)
            {
                fsm.currentState = PlayerFSM.State.GO;
            }
            else
            {
                fsm.currentState = PlayerFSM.State.Idle;
            }
            transform.position += new Vector3(moveVector.x, moveVector.y, 0);
        }

        
    }

    private Vector2 PoolInput()
    {
        Vector2 dir = Vector2.zero;

        dir.x = joystick.Horizontal() * moveSpeedConst * moveSpeed;
        dir.y = joystick.Vertical() * moveSpeedConst * moveSpeed;

        if (dir.magnitude > 1) dir.Normalize();

        return dir;
    }

}
