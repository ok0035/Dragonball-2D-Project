using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SkillEnergypa : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private PlayerFSM fsm;
    private MovePlayer movePlayer;

    public GameObject Energypa;    // 복제할 미사일 오브젝트
    private Transform Location;   // 미사일이 발사될 위치
    public float Delay;             // 미사일 발사 속도(미사일이 날라가는 속도x)
    private bool State;             // 미사일 발사 속도를 제어할 변수

    // Use this for initialization
    void Start()
    {
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        fsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
        Location = GameObject.FindGameObjectWithTag("PlayerSkillLocation").transform;
        State = true;
        Delay = 0.35f;

    }

    // Update is called once per frame
    void Update()
    {

    }


    // 코루틴 함수
    IEnumerator FireCycleControl()
    {
        // 처음에 FireState를 false로 만들고
        State = false;
        // FireDelay초 후에
        yield return new WaitForSeconds(Delay);
        // FireState를 true로 만든다.
        State = true;

        // "PlayerMissile"을 "MissileLocation"의 위치에 "MissileLocation"의 방향으로 복제한다.
        Instantiate(Energypa, Location.position, Location.rotation);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        movePlayer.moveFlag = false;
        fsm.currentState = PlayerFSM.State.ATTACK;

        // 코루틴 "FireCycleControl"이 실행되며
        StartCoroutine(FireCycleControl());

        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        movePlayer.moveFlag = true;
        fsm.currentState = PlayerFSM.State.Idle;
    }
}
