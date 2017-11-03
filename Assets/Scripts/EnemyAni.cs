using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour {

    public enum State
    {
        ANI_IDLE,
        ANI_ATK,
        ANI_GO
    };
    
    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAni(int aniNumber)
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("aniName", aniNumber);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
