using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Vector2 DestroyPos;
    private EnemyFSM fsm;
    private Params param;
    private PlayerParams playerParam;

    public float IdleSec = 5;
    public float AttackSec = 2;
    public float MoveSec = 5;

    // Use this for initialization
    void Start()
    {
        DestroyPos = new Vector2(-30f, transform.position.y);
        fsm = GetComponent<EnemyFSM>();
        playerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParams>();
        param = GetComponent<Params>();
        AI();

    }

    void AI()
    {
        StartCoroutine(Idle());
        StartCoroutine(Attack());
        StartCoroutine(Move());

        //if (transform.position.x <= targetPos1.x + 1)
        //    flag = true;

        //if (!flag)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, targetPos1, 1.5f * Time.deltaTime);
        //}
        //else if(flag)
        //{
        //    fsm.CurrentState = EnemyFSM.State.Attack;
        //    StartCoroutine(Move());
        //}

    }

    IEnumerator Idle()
    {
        fsm.CurrentState = EnemyFSM.State.Idle;
        yield return new WaitForSeconds(IdleSec);

    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(IdleSec);
        fsm.CurrentState = EnemyFSM.State.Attack;
        yield return new WaitForSeconds(AttackSec);
    }

    IEnumerator Move()
    {

        yield return new WaitForSeconds(IdleSec + AttackSec);
        fsm.CurrentState = EnemyFSM.State.Go;


    }

    void CheckDestroy()
    {
        if (transform.position.x <= DestroyPos.x)
        {
            playerParam.minusKI(param.KI);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestroy();

    }
}
