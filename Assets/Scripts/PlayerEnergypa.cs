using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergypa : MonoBehaviour {

    public GameObject Energypa;    // 복제할 미사일 오브젝트
    public Transform Location;   // 미사일이 발사될 위치
    public float Delay;             // 미사일 발사 속도(미사일이 날라가는 속도x)
    private bool State;             // 미사일 발사 속도를 제어할 변수

    void Start()
    {
        // 처음에 미사일을 발사할 수 있도록 제어변수를 true로 설정
        State = true;
    }

    void Update()
    {
        // 매 프레임마다 미사일발사 함수를 체크한다.
        playerFire();
    }

    // 미사일을 발사하는 함수
    private void playerFire()
    {
        // 제어변수가 true일때만 발동
        if (State)
        {
            // 키보드의 "A"를 누르면
            if (Input.GetKey(KeyCode.A))
            {
                // 코루틴 "FireCycleControl"이 실행되며
                StartCoroutine(FireCycleControl());
                // "PlayerMissile"을 "MissileLocation"의 위치에 "MissileLocation"의 방향으로 복제한다.
                Instantiate(Energypa, Location.position, Location.rotation);
            }
        }
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
    }
}