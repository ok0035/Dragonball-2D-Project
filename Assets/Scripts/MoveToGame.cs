using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;

public class MoveToGame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public Sprite DownImage;
    public Sprite UpImage;

    private Image image;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        SoundManager.instance.PlayOpeningSound();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = DownImage;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = UpImage;
        SceneManager.LoadScene("Main");
    }
}
