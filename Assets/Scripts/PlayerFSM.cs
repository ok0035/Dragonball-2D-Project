using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerFSM : MonoBehaviour
{

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

    private State currentState;
    private PlayerAni myAni;
    private PlayerParams param;
    private MovePlayer movePlayer;
    public GameObject Explosion;

    public State CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    // Use this for initialization
    void Start()
    {

        myAni = GetComponent<PlayerAni>();
        param = GetComponent<PlayerParams>();
        movePlayer = GetComponent<MovePlayer>();
        ChangeState(State.Idle, PlayerAni.ANI_IDLE);

    }


    public void ChangeState(State newState, int aniNumber)
    {
        if (currentState == newState)
        {
            //print("현재 상태 : " + CurrentState);
            return;
        }

        myAni.ChangeAni(aniNumber);
        CurrentState = newState;
    }

    void UpdateState()
    {

        switch (CurrentState)
        {

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

    void IdleState()
    {

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

    void AttackState()
    {

        ChangeState(State.ATTACK, PlayerAni.ANI_ATTACK);

    }

    void AttackWaitState()
    {

        ChangeState(State.ATKIDLE, PlayerAni.ANI_ATKIDLE);

    }

    void DeadState()
    {

        ChangeState(State.Dead, PlayerAni.ANI_DIE);

    }

    public void moveGo()
    {
        if (CurrentState != State.ATTACK)
            ChangeState(State.GO, PlayerAni.ANI_GO);
    }

    public void moveBack()
    {

        if (CurrentState != State.ATTACK)
            ChangeState(State.BACK, PlayerAni.ANI_BACK);
    }

    public void CheckMoveBoundary()
    {
        Vector3 boundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.y > boundary.y - 0.5f) transform.position = new Vector2(transform.position.x, boundary.y - 0.5f);
        if (transform.position.y < -(boundary.y - 0.5)) transform.position = new Vector2(transform.position.x, -(boundary.y - 0.5f));
        if (transform.position.x > boundary.x) transform.position = new Vector2(boundary.x, transform.position.y);
        if (transform.position.x < -boundary.x) transform.position = new Vector2(-boundary.x, transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            Debug.Log("충돌!!!");
            Instantiate(Explosion, collision.transform.position, collision.transform.rotation);
            SoundManager.instance.PlayExplosionSound();

            int myAttack = param.GetRandomAttack();
            int opAttack = collision.GetComponent<Params>().GetRandomAttack();

            if(opAttack < collision.GetComponent<Params>().KI)
            {
                param.AddScore((int)(opAttack * 0.05));
                param.AddKI((int)(opAttack * 0.01));
            } else
            {
                param.AddScore((int)(collision.GetComponent<Params>().KI * 0.05));
                param.AddKI((int)(collision.GetComponent<Params>().KI * 0.01));
            }

            collision.GetComponent<Params>().minusKI(myAttack);
            param.minusKI(opAttack);

            print(collision.GetComponent<Params>().GetRandomAttack());

            if (collision.GetComponent<Params>().KI <= 0) Destroy(collision.gameObject);

            CheckDead();
            //Destroy(Explosion);

        }
    }

    public void CheckDead()
    {
        if (param.isDead || param.KI <= 0)
        {
            param.isDead = true;
            movePlayer.MoveFlag = false;
            GetComponent<BoxCollider2D>().enabled = false;
            ChangeState(State.Dead, PlayerAni.ANI_DIE);
            SoundManager.instance.PlayDieSound();
            StartCoroutine(DestroyDelay(3));

        }
    }

    IEnumerator DestroyDelay(int sec)
    {
        yield return new WaitForSeconds(sec);
        print("dead");
        Destroy(gameObject);
        SceneManager.LoadScene("Ending");
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveBoundary();
        UpdateState();

    }
}
