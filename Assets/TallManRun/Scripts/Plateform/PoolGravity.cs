using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGravity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.gravity = Vector3.up * -100f;
            PlayerMovementControl.Instance.playerRB.isKinematic = false;
            PlayerMovementControl.Instance.playerRB.constraints = ~RigidbodyConstraints.FreezePositionY;
        }
    }


}// CLASS
