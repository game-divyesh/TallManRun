using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSliding : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.gravity = Vector3.up * -100f;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;

            PlayerMovementControl.Instance.PlaySlidingAnimation();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            Physics.gravity = Vector3.up * -9.8f;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            PlayerMovementControl.Instance.StopSlidingAnimation();
        }
    }




}// CLASS
