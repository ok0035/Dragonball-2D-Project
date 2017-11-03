using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private PlayerParams player;
    private GameObject UICanvas;

    public Text KIText;
    public Text ScoreText;
    public Text TimeText;

    private static UIManager manager;
    
    public static UIManager instance()
    {
        if (manager == null) manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        return manager;
    }

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParams>();
        StartCoroutine(PrintTime());
	}
	
	// Update is called once per frame
	void Update () {
        
        CheckUIStatus();
	}

    void CheckUIStatus()
    {

        KIText.text = player.KI + "";
        ScoreText.text = player.Score + "";

    }

    IEnumerator PrintTime()
    {
        while (true && !player.isDead)
        {
            yield return new WaitForSeconds(1);
            int time = int.Parse(TimeText.text) + 1;
            player.AddScore((int)(100 + (time * 4.8)));
            TimeText.text = time + "";
        }
    }
}
