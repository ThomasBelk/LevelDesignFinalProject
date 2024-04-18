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
    public AudioClip startEngine;
    public AudioClip engineSound;
    private AudioSource audioSource;
    private bool switched = false;
    public GameObject npc;
    public Transform spawnpoint;

    public Transform stopPos;
    
    void Start()
    {
        if (spawnpoint == null)
        {
            spawnpoint = GameObject.FindGameObjectWithTag("CarPoint").transform;
        }
        

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = engineSound;
        audioSource.loop = true;
        audioSource.Play();

        anim = GetComponent<SplineAnimate>();
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.ElapsedTime >= anim.MaxSpeed && !test) 
        {
            audioSource.Stop();
            audioSource.loop = false;
            GameObject newNPC = Instantiate(npc, transform);
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
        StartCoroutine(PlayTwoSounds(startEngine, engineSound, false, true));
    }
    
    IEnumerator PlayTwoSounds(AudioClip firstSound, AudioClip secondSound, bool loopFirst, bool loopSecond)
    {
        audioSource.clip = firstSound;
        audioSource.loop = loopFirst;
        audioSource.Play();

        while (audioSource.isPlaying && audioSource.clip == firstSound)
        {
            yield return null;
        }

        audioSource.clip = secondSound;
        audioSource.loop = loopSecond;
        audioSource.Play();
    }
}
