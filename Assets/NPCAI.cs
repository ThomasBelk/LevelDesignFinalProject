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
    private Animator anim; 
    private NavMeshAgent agent;
    private bool hasTalked = false;

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

        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 1);
        agent = GetComponent<NavMeshAgent>();
        
        agent.SetDestination(talkPoint.position);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, talkPoint.position) <= agent.stoppingDistance + .1f)
        {
            agent.speed = 0f;
            LookAt(player.transform.position);
            agent.SetDestination(transform.position);
            anim.SetInteger("animState", 0);
            if (!hasTalked)
            {
                anim.SetTrigger("Talk");
                hasTalked = true;
            }
            // if (anim.GetInteger("animState") != 2 && anim.GetInteger("animState") != 0)
            // {
            //     anim.SetInteger("animState", 2);
            // }
            // else
            // {
            //     anim.SetInteger("animState", 0);
            // }
        }
    }
    
    private void LookAt(Vector3 target) 
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    public void ReturnToIdle()
    {
        anim.SetInteger("animState", 0);
    }
}