using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private float firstCar = 15f;
    private bool firstCarEnabled = false;
    private int currentIndex = 0;

    private void FixedUpdate()
    {
        if (!firstCarEnabled && Time.time >= firstCar)
        {
            cars[0].SetActive(true);
            firstCarEnabled = true;
        }
        //Debug.Log("Time" + Time.time);
    }
    
    public IEnumerator NewCar(float time)
    {
        float startTime = Time.time;
        float delayStart = 0;

        
        delayStart = Time.time;
        Debug.Log(startTime);
        while (Time.time < startTime + time)
        {
            yield return null;
        }
        Debug.Log("time waited" + time);
        
        if (currentIndex + 1 < cars.Length)
        {
            cars[currentIndex + 1].SetActive(true);
        }
        cars[currentIndex].SetActive(false);
        
        // Debug.Log("Delay time: " + newCarDelay);
        // while (Time.time <= delayStart + newCarDelay)
        // {
        //     Debug.Log("IsInLoop");
        //     yield return null;
        // }
        //
        // Debug.Log("Current Index: " + currentIndex);
        // if (currentIndex < cars.Length)
        // {
        //     cars[currentIndex].SetActive(true);
        // }
    }

}
