using UnityEngine;
using System.Collections;

public class PlayerParams : Params {

    //이 강좌에서 이 속성들을 다 사용하지는 않을 것이지만 나중에 게임을 개발할때 이런식으로 확장하면 된다는 것을 알려주기 위해 추가

    private void Update()
    {

    }

    public override void InitParams() {

        Score = 0;
        isDead = false;
    
    }

    public void AddScore(int score)
    {
        Score += score;
    }

}
