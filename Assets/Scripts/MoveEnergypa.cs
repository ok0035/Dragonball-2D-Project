using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnergypa : MonoBehaviour {


    public float MoveSpeed;     // 미사일이 날라가는 속도
    public float DestroyXPos;   // 미사일이 사라지는 지점
    public GameObject Explosion;


	// Use this for initialization
	void Start () {
		
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
            //Destroy(Explosion);
            Destroy(gameObject);
            
        }
    }
}
