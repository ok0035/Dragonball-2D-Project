using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SkillKI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    PlayerFSM fsm;
    MovePlayer movePlayer;
    PlayerParams playerParams;
    public Text KIText;
    private bool KIFlag = false;
    private int incKI;

    public int IncKI
    {
        get
        {
            return incKI;
        }

        set
        {
            incKI = value;
        }
    }

    // Use this for initialization
    void Start()
    {

        fsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
        playerParams = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParams>();
        IncKI = (int)(playerParams.KI * 0.007);

    }

    // Update is called once per frame
    void Update()
    {
        //if (KIFlag) StartCoroutine(UpKI());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!playerParams.isDead)
        {
            movePlayer.MoveFlag = false;
            KIFlag = true;
            //fsm.CurrentState = PlayerFSM.State.KI;
            fsm.ChangeState(PlayerFSM.State.KI, PlayerAni.ANI_KI);
            SoundManager.instance.PlayKISound();
            StartCoroutine(UpKI());
            print("KI!!!");
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!playerParams.isDead)
        {
            //fsm.CurrentState = PlayerFSM.State.Idle;
            fsm.ChangeState(PlayerFSM.State.Idle, PlayerAni.ANI_IDLE);
            movePlayer.MoveFlag = true;
            KIFlag = false;
            //StopCoroutine(UpKI());
            StopAllCoroutines();
        }

    }

    IEnumerator UpKI()
    {
        while (true && !playerParams.isDead)
        {
            playerParams.AddKI(IncKI);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
