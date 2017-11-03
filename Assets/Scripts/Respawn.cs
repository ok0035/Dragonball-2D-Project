using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public List<GameObject> EnemyList;

	// Use this for initialization
	void Start () {
        StartCoroutine(RespawnEnemy());
	}

    IEnumerator RespawnEnemy()
    {
        while(true)
        {    
            int RandomTime = Random.Range(2, 5);
            yield return new WaitForSeconds(RandomTime);
            int RandomEnemy = Random.Range(0, 6);
            GameObject enemy = Instantiate(EnemyList[RandomEnemy], transform);
            enemy.transform.position = transform.position;
            print("리스폰중..");
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
