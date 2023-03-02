using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private ParticleSystem ripplePartical;

    private bool isPlayerInPool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInPool = true;

            StartCoroutine(RippleFollowPlayer());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInPool = false;
            StopCoroutine(RippleFollowPlayer());
        }
    }


    private IEnumerator RippleFollowPlayer() 
    {
        while (isPlayerInPool) 
        {
            ripplePartical.transform.position = new Vector3(PlayerMovementControl.Instance.transform.position.x, ripplePartical.transform.position.y, PlayerMovementControl.Instance.transform.position.z);
            yield return null;
        }

    }

}// CLASS
