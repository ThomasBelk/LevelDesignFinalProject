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

    public Transform stopPos;
    // Start is called before the first frame update
    void Start()
    {
        //anim.GetComponent<SplineAnimate>();
        //anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            if (!switched)
            {
                anim.ElapsedTime = 0;
                switched = true;
            }
            anim.Container = cont2;
        }
        else
        {
            anim.Container = cont1;
            if (anim.ElapsedTime >= anim.MaxSpeed)
            {
                test = true;
            }
        }

        //Debug.Log(anim.Container.Splines[anim.Container.Splines.Count - 1].Knots.);
    }
}
