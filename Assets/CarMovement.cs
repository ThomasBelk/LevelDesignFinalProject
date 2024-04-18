using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CarMovement : MonoBehaviour
{
    public SplineAnimate anim;
    public SplineContainer cont1;
    public SplineContainer cont2;
    public bool test = false;
    public AudioSource startEngine;
    public AudioSource engineSound;
    //private AudioSource audioSource;
    private bool switched = false;
    public GameObject npc;
    public Transform spawnpoint;
    public float engineTurnOffTime = .2f;

    public Transform stopPos;
    
    void Start()
    {
        if (spawnpoint == null)
        {
            spawnpoint = GameObject.FindGameObjectWithTag("CarPoint").transform;
        }

        anim = GetComponent<SplineAnimate>();
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.ElapsedTime >= anim.MaxSpeed && !test)
        {
            //engineSound.Stop();
            StartCoroutine(FadeOut(engineSound, engineTurnOffTime));
            GameObject newNPC = Instantiate(npc, spawnpoint.transform);
            newNPC.GetComponent<NPCAI>().GetCar(this);
            test = true;
        }
    }

    public void DriveAway()
    {
        if (!switched)
        {
            anim.ElapsedTime = 0;
            switched = true;
        }
        anim.Container = cont2;
        anim.Play();
        engineSound.Play();
        // I have no idea why this works but if you use startEngine.Play() with the gameobject
        // enabled there is a crazy scratching sound. ¯\_(ツ)_/¯
        startEngine.gameObject.SetActive(true);
        //StartCoroutine(PlayTwoSounds(startEngine, engineSound, false, true));
    }
    
    private IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    
    // IEnumerator PlayTwoSounds(AudioClip firstSound, AudioClip secondSound, bool loopFirst, bool loopSecond)
    // {
    //     audioSource.clip = firstSound;
    //     audioSource.loop = loopFirst;
    //     audioSource.Play();
    //
    //     while (audioSource.isPlaying && audioSource.clip == firstSound)
    //     {
    //         yield return null;
    //     }
    //
    //     audioSource.clip = secondSound;
    //     audioSource.loop = loopSecond;
    //     audioSource.Play();
    // }
}
