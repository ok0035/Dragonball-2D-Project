using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float MoveSpeed;     // 미사일이 날라가는 속도
    public float DestroyXPos;   // 미사일이 사라지는 지점
    public GameObject Explosion;
    private Params param;


    // Use this for initialization
    void Start()
    {
        param = GetComponent<Params>();
        print("적 공격 전투력 : " + param.KI);
    }

    // Update is called once per frame
    void Update()
    {

        // 매 프레임마다 미사일이 MoveSpeed 만큼 right방향(X축 +방향)으로 날라갑니다.
        transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        // 만약에 미사일의 위치가 DestroyXPos를 넘어서면
        if (transform.position.x <= DestroyXPos)
        {
            // 미사일을 제거
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerSkill"))
        {
            Debug.Log("플레이어와 충돌(스킬)");
            Instantiate(Explosion, collision.transform.position, collision.transform.rotation);
            SoundManager.instance.PlayExplosionSound();

            if (param.KI <= 0) Destroy(gameObject);

        }
    }
}
