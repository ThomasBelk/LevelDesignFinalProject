using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class NPCAI : MonoBehaviour
{
    private enum NPCSTATE
    {
        WalkIn,
        Talk,
        Wander,
        WalkOut
    }

    private NPCSTATE state;

    [SerializeField] private GameObject[] wanderPoints;
    [SerializeField] private Transform talkPoint;
    [SerializeField] private Transform carPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip sound;

    [SerializeField] private AudioSource footSteps;

    [SerializeField] private AudioClip gasp;
    [SerializeField] private AudioClip heartbeat;


    private Animator anim; 
    private NavMeshAgent agent;
    private bool hasTalked = false;
    private bool doneTalking = false;
    private AudioSource audioSource;
    private float npcSpeed;

    private LinkedList<GameObject> chosenWanderPoints;
    private CarMovement carMovement;
    private bool firstTime = true;
    private bool firstTime2 = true;
    private AudioSource pa;
    public AudioMixerGroup playervoice;
    public AudioMixerGroup heartBeatGroup;
    
    // Start i called before the first frame update
    void Start()
    {
        state = NPCSTATE.WalkIn;
        if (wanderPoints.Length <= 0)
        {
            wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (talkPoint == null)
        {
            talkPoint = GameObject.FindGameObjectWithTag("TalkPoint").transform;
        }

        if (carPoint == null)
        {
            carPoint = GameObject.FindGameObjectWithTag("CarPoint").transform;
        }

        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 1);
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (sound != null)
        {
            audioSource.clip = sound;
        }

        chosenWanderPoints = new LinkedList<GameObject>();
        int i = Random.Range(0, wanderPoints.Length - 1);
        for (int j = 0; j <= i; j++)
        {
            chosenWanderPoints.AddLast(wanderPoints[j]);
        }
        Debug.Log(chosenWanderPoints.Count);
        
        npcSpeed = agent.speed;
        agent.SetDestination(talkPoint.position);
        pa = player.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (state)
        {
            case NPCSTATE.Talk:
                Talk();
                break;
            case NPCSTATE.WalkIn:
                WalkIn();
                break;
            case NPCSTATE.WalkOut:
                WalkOut();
                break;
            case NPCSTATE.Wander:
                Wander();
                break;
            default:
                WalkIn();
                break;
        }
    }
    
    private void LookAt(Vector3 target) 
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void WalkIn()
    {
        anim.SetInteger("animState", 1);
        agent.SetDestination(talkPoint.position);
        if (Vector3.Distance(transform.position, talkPoint.position) <= agent.stoppingDistance + .1f && !doneTalking)
        {
            state = NPCSTATE.Talk;
        }

        footSteps.loop = true;
        if (!footSteps.isPlaying)
        {
            footSteps.Play();
        }

        if (firstTime)
        {
            pa.outputAudioMixerGroup = playervoice;
            pa.clip = gasp;
            pa.Play();
            
            firstTime = false;
        }

        if (!pa.isPlaying && firstTime2 && !firstTime)
        {
            pa.outputAudioMixerGroup = heartBeatGroup;
            pa.clip = heartbeat;
            pa.Play();
            firstTime2 = false;
        }
    }

    private void Talk()
    {
        footSteps.loop = false;
        agent.speed = 0f;
        LookAt(player.transform.position);
        agent.SetDestination(transform.position);
        anim.SetInteger("animState", 0);
        if (!hasTalked)
        {
            anim.SetTrigger("Talk");
            audioSource.Play();
            hasTalked = true;
        }
        
        if (!audioSource.isPlaying)
        {
            doneTalking = true;
            state = NPCSTATE.Wander;
        }
    }

    private void Wander()
    {
        footSteps.loop = true;
        if (!footSteps.isPlaying)
        {
            footSteps.Play();
        }
        agent.speed = npcSpeed;
        anim.SetInteger("animState", 1);
        if (chosenWanderPoints.Count <= 0)
        {
            state = NPCSTATE.WalkOut;
        }
        else
        {
            agent.SetDestination(chosenWanderPoints.Last().transform.position);
            if (Vector3.Distance(transform.position, chosenWanderPoints.Last().transform.position)
                <= agent.stoppingDistance + .1f)
            {
                chosenWanderPoints.RemoveLast();
            }
        }
        
    }

    private void WalkOut()
    {
        footSteps.loop = true;
        if (!footSteps.isPlaying)
        {
            footSteps.Play();
        }
        if (Vector3.Distance(transform.position, carPoint.position) <= .5f)
        {
            carMovement.DriveAway();
            Destroy(gameObject, 1f);
            return;
        }
        agent.stoppingDistance = 0f;
        agent.autoBraking = false;
        agent.SetDestination(carPoint.position);
    }

    public void GetCar(CarMovement car)
    {
        carMovement = car;
    }
    
    


    // public void ReturnToIdle()
    // {
    //     anim.SetInteger("animState", 0);
    // }
}