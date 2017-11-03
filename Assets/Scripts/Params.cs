using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;


// 플레이어의 파라미터 클래스와 몬스터 파라미터 클래스의 부모클래스 역할을 하게 될 클래스
public class Params : MonoBehaviour {

    public int KI;
    public int Score { get; set; }
    public bool isDead { get; set; }

    [System.NonSerialized]
    public UnityEvent deadEvent = new UnityEvent();

	// Use this for initialization
	void Start () {

	    

    }

    private void OnEnable()
    {
        InitParams();
    }


    public virtual void InitParams() {

        StartCoroutine(DelayInit());
    }

    IEnumerator DelayInit()
    {
        yield return new WaitForSeconds(3);
        UIManager ui = UIManager.instance();
        print("KI = : " + KI);
        if(ui)
        {
            KI = (int)(KI + KI * int.Parse(ui.TimeText.text) * 0.016);
            print("적 체력 ~ ! : " + KI);
        }
            
    }

    public int GetRandomAttack()
    {
        int randAttack = Random.Range((int)(KI * 0.7), (int)(KI + 1));

        return randAttack;
    }

    public void AddKI(int ki)
    {

        this.KI += ki;

        //UIManager.instance.UpdatePlayerUI(this);
    }

    public void minusKI(int ki)
    {
        KI -= ki;

        if (KI <= 0)
        {
            KI = 0;
            isDead = true;
        }
    }
}
