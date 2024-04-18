using System.Collections;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private float leftDoorRotation = 100f;
    [SerializeField] private float rightDoorRotation = -100f;
    [SerializeField] private AudioSource bell;

    private Coroutine doorsCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            
            if (doorsCoroutine != null)
                StopCoroutine(doorsCoroutine);
            
            doorsCoroutine = StartCoroutine(DoorsOpen());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            if (doorsCoroutine != null)
                StopCoroutine(doorsCoroutine);
            
            doorsCoroutine = StartCoroutine(DoorsClose());
        }
    }

    private IEnumerator DoorsOpen()
    {
        bell.Play();
        leftDoor.transform.Rotate(0, leftDoorRotation, 0);
        rightDoor.transform.Rotate(0, rightDoorRotation, 0);

        yield return new WaitForSeconds(1f);

        doorsCoroutine = null;
    }

    private IEnumerator DoorsClose()
    {
        //bell.Play();
        leftDoor.transform.Rotate(0, -leftDoorRotation, 0);
        rightDoor.transform.Rotate(0, -rightDoorRotation, 0);
        
        yield return new WaitForSeconds(1f);
        doorsCoroutine = null;
    }
}