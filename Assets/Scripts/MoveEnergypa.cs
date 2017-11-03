using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnergypa : MonoBehaviour {


    public float MoveSpeed;     // 미사일이 날라가는 속도
    public float DestroyXPos;   // 미사일이 사라지는 지점
    public GameObject Explosion;

    private Params param;
    private GameObject player;
    private PlayerParams playerParams;


    // Use this for initialization
    void Start () {
        param = GetComponent<Params>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerParams = player.GetComponent<PlayerParams>();
        param.KI = (int)(playerParams.KI * 0.7);
    }
	
	// Update is called once per frame
	void Update () {

        // 매 프레임마다 미사일이 MoveSpeed 만큼 right방향(X축 +방향)으로 날라갑니다.
        transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        // 만약에 미사일의 위치가 DestroyXPos를 넘어서면
        if (transform.position.x >= DestroyXPos)
        {
            // 미사일을 제거
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("충돌");
            Instantiate(Explosion, collision.transform.position, collision.transform.rotation);
            collision.GetComponent<Params>();
            SoundManager.instance.PlayExplosionSound();

            int myAttack = param.GetRandomAttack();
            int opAttack = collision.GetComponent<Params>().GetRandomAttack();
            
            if (opAttack < collision.GetComponent<Params>().KI)
            {
                playerParams.AddScore((int)(opAttack * 0.05));
                playerParams.AddKI((int)(opAttack * 0.01));
            }
            else
            {
                playerParams.AddScore((int)(collision.GetComponent<Params>().KI * 0.05));
                playerParams.AddKI((int)(collision.GetComponent<Params>().KI * 0.01));
            }

            collision.GetComponent<Params>().minusKI(myAttack);
            param.minusKI(opAttack);

            if (param.KI <= 0) Destroy(gameObject);
            if (collision.GetComponent<Params>().KI <= 0) Destroy(collision.gameObject);

        }
    }
}
