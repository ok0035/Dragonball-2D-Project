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

    public State currentState = State.Idle;

    Vector3 curTargetPos;

    GameObject curEnemy;

    public float rotAnglePerSecond = 360f;
    public float moveSpeed = 2f;

    float attackDelay = 2f;
    float attackTimer = 0f;

    float attackDistance = 1.5f;

    // 전투 중 적이 도망가면 다시 추적을 시작하기 위한 거리(2.5미터 바깥으로 도망 가는 순간 다시 추적), 여기선 사용하지 않을 예정
    float chaseDistance = 2.5f; 


    PlayerAni myAni;
    PlayerParams myParams;
    //MonsterParams curEnemyParams;

	// Use this for initialization
	void Start () {

        myAni = GetComponent<PlayerAni>();
        myParams = GetComponent<PlayerParams>();
        myParams.InitParams();
        myParams.deadEvent.AddListener(ChangeToPlayerDead);

        ChangeState(State.Idle, PlayerAni.ANI_IDLE);

	}

    public void ChangeToPlayerDead() { // 캐릭터가 죽으면 자동실행되는 함수

        print("player was dead");
        ChangeState(State.Dead, PlayerAni.ANI_DIE);

        //UIManager.instance.ShowGameOver();

    }

    public void CurrentEnemyDead() {

        ChangeState(State.Idle, PlayerAni.ANI_IDLE);
        print("enemy was killed");

        curEnemy = null;
    }

    public void AttackCalculate()
    {
        //if (curEnemy == null) return;

        //curEnemy.GetComponent<MonsterFSM>().ShowHitEffect();

        int attackPower = myParams.GetRandomAttack();
        //curEnemyParams.SetEnemyAttack(attackPower);

        //SoundManager.instance.PlayHitSound();
    }

    public void AttackEnemy(GameObject enemy) {

        if (curEnemy != null && curEnemy == enemy) return;

        //curEnemyParams = enemy.GetComponent<MonsterParams>();

        //if(curEnemyParams.isDead == false) {

        //    curEnemy = enemy;
        //    curTargetPos = curEnemy.transform.position;
        //    GameManager.instance.ChangeCurrentTarget(curEnemy);

        //    ChangeState(State.Move, PlayerAni.ANI_WALK);
        //} else
        //    curEnemyParams = null;

    }

    void ChangeState(State newState, int aniNumber)
    {
        if (currentState == newState) return;
         
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

        }
    }

    void IdleState() {

        ChangeState(State.Idle, PlayerAni.ANI_DIE);
    }

    void MoveState()
    {

        MoveToDestination();

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

        attackTimer = 0f;

        //transform.LookAt(curTargetPos);
        ChangeState(State.ATTACK, PlayerAni.ANI_ATTACK);

    }

    void AttackWaitState() {

        //if (attackTimer > attackDelay)
        //{

        //    ChangeState(State.Attack, PlayerAni.ANI_ATTACK);

        //}

        //attackTimer += Time.deltaTime;

        ChangeState(State.ATKIDLE, PlayerAni.ANI_ATKIDLE);

    }

    void DeadState() {

        ChangeState(State.Dead, PlayerAni.ANI_DIE);

    }

    public void moveGo()
    {
        //if( currentState == State.Dead) return ;

        //curEnemy = null;
        //curTargetPos = tPos;

        ChangeState(State.GO, PlayerAni.ANI_GO);
    }

    public void moveBack()
    {
        //if( currentState == State.Dead) return ;

        //curEnemy = null;
        //curTargetPos = tPos;

        ChangeState(State.BACK, PlayerAni.ANI_BACK);
    }

    void turnToDestination()
    {
        //Quaternion lookRotation = Quaternion.LookRotation(curTargetPos - transform.position);

        ////RotateTowards(현재의 rotation 값, 최종 목표 rotation값, 최대 최전각);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotAnglePerSecond);

    }

    void MoveToDestination()
    {

        //transform.position =
        //    Vector3.MoveTowards(transform.position, curTargetPos, moveSpeed * Time.deltaTime);

        //if (curEnemy == null)
        //{

        //    if (transform.position == curTargetPos)
        //    {
        //        ChangeState(State.Idle, PlayerAni.ANI_IDLE);
        //    }

        //}

        //else {

        //    if (Vector3.Distance(transform.position, curTargetPos) < attackDistance)
        //    {
        //        ChangeState(State.Attack, PlayerAni.ANI_ATTACK);
        //    }
        //}

    }

	// Update is called once per frame
	void Update () {
        UpdateState();
	}
}
