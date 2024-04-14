using System.Collections;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private float leftDoorRotation = 100f;
    [SerializeField] private float rightDoorRotation = -100f;

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
        // Your doors opening logic goes here
        Debug.Log("Opening doors...");

        // Example: Move the doors to the open position
        leftDoor.transform.Rotate(0, leftDoorRotation, 0);
        rightDoor.transform.Rotate(0, rightDoorRotation, 0);

        // Wait for some time
        yield return new WaitForSeconds(1f);

        // Once done, you can clear the coroutine reference
        doorsCoroutine = null;
    }

    private IEnumerator DoorsClose()
    {
        // Your doors closing logic goes here
        Debug.Log("Closing doors...");

        // Example: Move the doors back to the closed position
        leftDoor.transform.Rotate(0, -leftDoorRotation, 0);
        rightDoor.transform.Rotate(0, -rightDoorRotation, 0);

        // Wait for some time
        yield return new WaitForSeconds(1f);

        // Once done, you can clear the coroutine reference
        doorsCoroutine = null;
    }
}