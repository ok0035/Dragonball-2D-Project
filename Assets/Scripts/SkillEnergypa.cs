using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SkillEnergypa : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private PlayerFSM fsm;
    private PlayerParams param;
    private bool atkIdleFlag;
    private int useKI;

    public GameObject Energypa;    // 복제할 미사일 오브젝트
    private Transform Location;   // 미사일이 발사될 위치
    public float Delay, PostDelay;  // 미사일 발사 속도(미사일이 날라가는 속도x)

    // Use this for initialization
    void Start()
    {
        fsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
        param = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParams>();
        Location = GameObject.FindGameObjectWithTag("PlayerSkillLocation").transform;
        atkIdleFlag = true;
        useKI = 300 + (int)(param.KI * 0.01);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (atkIdleFlag && !param.isDead)
        {

            //fsm.CurrentState = PlayerFSM.State.ATTACK;
            fsm.ChangeState(PlayerFSM.State.ATTACK, PlayerAni.ANI_ATTACK);
            SoundManager.instance.PlayEnergypaSound();

            // 코루틴 "FireCycleControl"이 실행되며
            StartCoroutine(FireCycleControl());
            print("에네르기파!!");

            atkIdleFlag = false;
            StartCoroutine(AttackIdleTime());
        }


    }

    // 코루틴 함수
    IEnumerator FireCycleControl()
    {
        // 처음에 FireState를 false로 만들고
        // FireDelay초 후에
        yield return new WaitForSeconds(Delay);

        // "PlayerMissile"을 "MissileLocation"의 위치에 "MissileLocation"의 방향으로 복제한다.
        Instantiate(Energypa, Location.position, Location.rotation);
        param.minusKI(useKI);
    }

    IEnumerator AttackIdleTime()
    {
        while (!atkIdleFlag)
        {
            yield return new WaitForSeconds(PostDelay);
            atkIdleFlag = true;
            
        }
        fsm.ChangeState(PlayerFSM.State.Idle, PlayerAni.ANI_IDLE);

    }

}
