using UnityEngine;
using System.Collections;

public class PlayerFSM : MonoBehaviour {

    public enum State
    {
       Idle,   
       GO,     
       BACK,   
       ATTACK, 
       ATKIDLE,
       Dead,    
       KI     
    }

    public State currentState;
    private PlayerAni myAni;

	// Use this for initialization
	void Start () {

        myAni = GetComponent<PlayerAni>();
        ChangeState(State.Idle, PlayerAni.ANI_IDLE);

    }


    void ChangeState(State newState, int aniNumber)
    {
        //if (currentState == newState)
        //{
        //    print("현재 상태 : " +  currentState);
        //    return;
        //}

        myAni.ChangeAni(aniNumber);
        currentState = newState;
    }

    void UpdateState()
    {

        switch (currentState) {

            case State.Idle:
                IdleState();
                break;

            case State.GO:
                GoState();
                break;

            case State.BACK:
                BackState();
                break;

            case State.ATTACK:
                AttackState();
                break;

            case State.ATKIDLE:
                AttackWaitState();
                break;

            case State.Dead:
                DeadState();
                break;

            case State.KI:
                KIState();
                break;

        }
    }

    void KIState()
    {
        ChangeState(State.KI, PlayerAni.ANI_KI);
    }

    void IdleState() {

        ChangeState(State.Idle, PlayerAni.ANI_IDLE);    
    }


    void GoState()
    {
        moveGo();
    }

    void BackState()
    {
        moveBack();
    }

    void AttackState() {

        ChangeState(State.ATTACK, PlayerAni.ANI_ATTACK);

    }

    void AttackWaitState() {

        ChangeState(State.ATKIDLE, PlayerAni.ANI_ATKIDLE);

    }

    void DeadState() {

        ChangeState(State.Dead, PlayerAni.ANI_DIE);

    }

    public void moveGo()
    {

        ChangeState(State.GO, PlayerAni.ANI_GO);
    }

    public void moveBack()
    {


        ChangeState(State.BACK, PlayerAni.ANI_BACK);
    }

	// Update is called once per frame
	void Update () {

        UpdateState();

	}
}
