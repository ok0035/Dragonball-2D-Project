using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour {

    public enum State{
        Idle,
        Attack,
        Go,
        Dead

    };

    private EnemyAni ani;
    private Params param;
    private bool flag = false;

    public State CurrentState = State.Idle;
    public float delay;
    public float IdleSpeed, RunSpeed;

    public GameObject attackObj;
    public GameObject Explosion;
    public Transform attackLocation;

    

    // Use this for initialization
    void Start () {
        ani = GetComponent<EnemyAni>();
        param = GetComponent<Params>();
        //attackLocation = gameObject.GetComponentInChildren<Transform>().GetChild(0);
        
	}
	
	// Update is called once per frame
	void Update () {
        UpdateState();
	}

    void UpdateState()
    {

        switch(CurrentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Go:
                MoveState();
                break;
            case State.Dead:
                DeadState();
                break;
        }
    }

    void ChangeState(State currnetState, EnemyAni.State aniState)
    {
        CurrentState = currnetState;
        ani.ChangeAni((int)aniState);
    }
   
    void IdleState()
    {
        ChangeState(State.Idle, EnemyAni.State.ANI_IDLE);
        transform.Translate(new Vector2(-IdleSpeed * Time.deltaTime, 0));
    }

    void AttackState()
    {
        ChangeState(State.Attack, EnemyAni.State.ANI_ATK);
        if(!flag)
        {
            StartCoroutine(Attack());
            flag = true;
        }
            
    }

    void DeadState()
    {
        ChangeState(State.Dead, EnemyAni.State.ANI_IDLE);
        Destroy(gameObject);
    }

    void MoveState()
    {
        ChangeState(State.Go, EnemyAni.State.ANI_GO);
        StartCoroutine(Move());
    }

    IEnumerator Attack()
    {
        //attackLocation = gameObject.GetComponentInChildren<Transform>().GetChild(0);
        yield return new WaitForSeconds(delay);
        GameObject obj = Instantiate(attackObj, attackLocation);
        obj.transform.position = attackLocation.position;
        yield return new WaitForSeconds(2);
        ChangeState(State.Go, EnemyAni.State.ANI_GO);
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1);
        transform.Translate(new Vector2(-RunSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerSkill"))
        {

            Debug.Log("충돌");
            Instantiate(Explosion, collision.transform.position, collision.transform.rotation);
            SoundManager.instance.PlayExplosionSound();

            if(param.KI <= 0)
                Destroy(gameObject);

        }
    }

}
