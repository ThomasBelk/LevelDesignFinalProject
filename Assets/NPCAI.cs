using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    [SerializeField] private GameObject[] wanderPoints;
    [SerializeField] private Transform talkPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip sound;
    private Animator anim; 
    private NavMeshAgent agent;
    private bool hasTalked = false;
    private bool doneTalking = false;
    private AudioSource audioSource;

    // Start i called before the first frame update
    void Start()
    {
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

        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 1);
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (sound != null)
        {
            audioSource.clip = sound;
        }
        
        agent.SetDestination(talkPoint.position);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, talkPoint.position) <= agent.stoppingDistance + .1f && !doneTalking)
        {
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
            else
            {
                if (!audioSource.isPlaying)
                {
                    doneTalking = true;
                }
            }

        }
        else if (doneTalking)
        {
            Debug.Log("Done Talking");
        }
    }
    
    private void LookAt(Vector3 target) 
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    // public void ReturnToIdle()
    // {
    //     anim.SetInteger("animState", 0);
    // }
}