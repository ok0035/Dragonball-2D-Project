using UnityEngine;
using System.Collections;

public class PlayerParams : CharacterParams {

    //이 강좌에서 이 속성들을 다 사용하지는 않을 것이지만 나중에 게임을 개발할때 이런식으로 확장하면 된다는 것을 알려주기 위해 추가

    public string name { get; set; }
    public int curExp { get; set; }
    public int expToNextLevel { get; set; }
    public int money { get; set; }

    public override void InitParams() {

        name = "Jin";
        level = 1;
        maxHp = 100;
        curHp = maxHp;
        attackMin = 30;
        attackMax = 40;
        defense = 1;
        
        curExp = 0;
        expToNextLevel = 100 * level;
        money = 0;

        isDead = false;

        //UIManager.instance.UpdatePlayerUI(this);
    
    }

    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();

        //UIManager.instance.UpdatePlayerUI(this);
    }

    public void AddMoney( int money) {

        this.money += money;

        //UIManager.instance.UpdatePlayerUI(this);
    }

}
