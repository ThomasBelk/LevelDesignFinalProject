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
        anim.Container = cont1;
        anim.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (anim.ElapsedTime >= anim.MaxSpeed && !test) 
        {
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
    }
}
