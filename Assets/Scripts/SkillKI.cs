using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SkillKI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    PlayerFSM fsm;
    MovePlayer movePlayer;

    public void OnPointerDown(PointerEventData eventData)
    {

        movePlayer.moveFlag = false;
        fsm.currentState = PlayerFSM.State.KI;
        print("KI!!!");

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fsm.currentState = PlayerFSM.State.Idle;
        movePlayer.moveFlag = true;
    }

    // Use this for initialization
    void Start () {
        fsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFSM>();
        movePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
