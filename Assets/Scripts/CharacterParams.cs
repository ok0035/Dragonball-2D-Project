using UnityEngine;
using UnityEngine.Events;
using System.Collections;


// 플레이어의 파라미터 클래스와 몬스터 파라미터 클래스의 부모클래스 역할을 하게 될 클래스
public class CharacterParams : MonoBehaviour {

    public int level { get; set; }
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int attackMin { get; set; }
    public int attackMax { get; set; }
    public int defense { get; set; }

    public bool isDead { get; set; }

    [System.NonSerialized]
    public UnityEvent deadEvent = new UnityEvent();

	// Use this for initialization
	void Start () {
	    InitParams();
	}

    public virtual void InitParams() {

    }

    public int GetRandomAttack() {

        int randAttack = Random.Range(attackMin, attackMax + 1);

        return randAttack;
    }

    public void SetEnemyAttack(int enemyAttackPower) {

        curHp -= enemyAttackPower;
        UpdateAfterReceiveAttack();
    }

    protected virtual void UpdateAfterReceiveAttack() {

        print(name + "'s HP: " + curHp);

        if(curHp <= 0) {

            curHp = 0;
            isDead = true;
            deadEvent.Invoke();
        }
    }
}
