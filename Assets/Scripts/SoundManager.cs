using UnityEngine;
using System.Collections;

//사운드 매니저는 오디오소스(AudioSource)가 반드시 필요
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    AudioSource myAudio;

    //사운드를 저장할 변수
    public AudioClip sndHitEnemy;
    public AudioClip sndEnemyAttack;
    public AudioClip sndEnemyDie;
    public AudioClip sndKI;
    public AudioClip sndGameOver;
    public AudioClip sndOpening;
    public AudioClip sndEnergypa;
    public AudioClip sndExplosion;
    public AudioClip sndBackground;
    public AudioClip sndDieSound;
    public AudioClip sndFinalFresh;
    public AudioClip sndKidBooBlast;

    // Use this for initialization
    void Awake()
    {

        if (instance == null)
            instance = this;

    }


    void Start()
    {

        myAudio = GetComponent<AudioSource>();

    }

    //사운드를 플레이하는 함수
    public void PlayHitSound()
    {

        myAudio.PlayOneShot(sndHitEnemy);
    }

    public void PlayKidBooBlastSound()
    {

        myAudio.PlayOneShot(sndKidBooBlast);
    }


    public void PlayFinalFreshSound()
    {

        myAudio.PlayOneShot(sndFinalFresh);
    }

    public void PlayEnemyAttack()
    {

        myAudio.PlayOneShot(sndEnemyAttack);

    }

    public void PlayEnemyDie()
    {

        myAudio.PlayOneShot(sndEnemyDie);

    }

    public void PlayKISound()
    {
        myAudio.PlayOneShot(sndKI);
    }

    public void PlayEnergypaSound()
    {
        myAudio.PlayOneShot(sndEnergypa);
    }

    public void PlayExplosionSound()
    {
        myAudio.PlayOneShot(sndExplosion);
    }

    public void PlayBackgroundSound()
    {
        myAudio.PlayOneShot(sndBackground);
    }

    public void PlayDieSound()
    {
        myAudio.PlayOneShot(sndDieSound);
    }

    public void PlayOpeningSound()
    {
        myAudio.PlayOneShot(sndOpening);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
