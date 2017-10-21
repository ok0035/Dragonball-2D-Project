using UnityEngine;
using System.Collections;

public class PlayerAni : MonoBehaviour {

    public const int ANI_IDLE        = 0;
    public const int ANI_GO          = 1;
    public const int ANI_BACK        = 2;
    public const int ANI_ATTACK      = 3;
    public const int ANI_ATKIDLE     = 4;
    public const int ANI_DIE         = 5;
    public const int ANI_KI          = 6;

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}

    public void ChangeAni(int aniNumber)
    {
        anim.SetInteger("aniName", aniNumber);
        print("애니넘버 : " + aniNumber);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
