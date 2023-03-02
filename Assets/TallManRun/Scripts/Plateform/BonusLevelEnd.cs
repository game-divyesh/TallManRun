using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementControl.Instance.BounceLevelComplete();
        }
    }

}// CLASS
