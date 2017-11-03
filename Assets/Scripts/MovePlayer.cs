using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private const float moveSpeedConst = 0.1f;
    public float moveSpeed = 2f;
    public Vector2 moveVector { set; get; }

    public bool MoveFlag
    {
        get
        {
            return moveFlag;
        }

        set
        {
            moveFlag = value;
        }
    }

    public VirtualJoyStick joystick;
    private bool moveFlag = true;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    private void Move()
    {
        //thisRigidbody.AddForce((moveVector * moveSpeed));
        moveVector = PoolInput();
        PlayerFSM fsm = GetComponent<PlayerFSM>();

        if (moveFlag)
        {
            if (fsm.CurrentState == PlayerFSM.State.ATTACK)
            {

            }
            else if (moveVector.x < 0)
            {
                //fsm.CurrentState = PlayerFSM.State.BACK;
                fsm.ChangeState(PlayerFSM.State.BACK, PlayerAni.ANI_BACK);
            }
            else if (moveVector.x > 0)
            {
                //fsm.CurrentState = PlayerFSM.State.GO;
                fsm.ChangeState(PlayerFSM.State.GO, PlayerAni.ANI_GO);
            }
            else if (moveVector.y != 0)
            {
                //fsm.CurrentState = PlayerFSM.State.GO;
                fsm.ChangeState(PlayerFSM.State.Idle, PlayerAni.ANI_IDLE);
            }
            else
            {
                //fsm.CurrentState = PlayerFSM.State.Idle;
                fsm.ChangeState(PlayerFSM.State.Idle, PlayerAni.ANI_IDLE);
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
