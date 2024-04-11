using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    [SerializeField] private GameObject[] wanderPoints;
    private NavMeshAgent agent;

    // Start i called before the first frame update
    void Start()
    {
        if (wanderPoints.Length <= 0)
        {
            wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        }

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wanderPoints[0].transform.position);
    }

    private void Update()
    {
        
    }
}