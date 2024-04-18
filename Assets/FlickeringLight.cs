using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light light;
    public float minTime;
    public float maxTime;
    private float timer;

    public Material offMat;
    public Material onMat;
    private bool changeMaterials = true;

    //public AudioSource audio;
    private MeshRenderer meshRend;
    public int materialsIndex = 1;

    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        meshRend = GetComponent<MeshRenderer>();
        if (offMat == null || onMat == null)
        {
            changeMaterials = false;
        }
    }   

    void Update()
    {
        Flicker();
    }

    private void Flicker()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            light.enabled = !light.enabled;
            if (changeMaterials)
            {
                if (light.enabled)
                {
                    var copy = meshRend.materials;
                    copy[materialsIndex] = onMat;
                    meshRend.materials = copy;
                }
                else
                {
                    var copy = meshRend.materials;
                    copy[materialsIndex] = offMat;
                    meshRend.materials = copy;
                }
            }

            timer = Random.Range(minTime, maxTime);
            
        }
    }
}
