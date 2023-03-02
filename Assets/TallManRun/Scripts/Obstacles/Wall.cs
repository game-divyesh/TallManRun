using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SoundManager.Instance.PlaySound(SoundManager.Instance.obstacleHitSound);
        PlayerMovementControl.Instance.MakePlayerShort(8);
        PlayerMovementControl.Instance.DeBuffThePlayer(8);

    }

}// CLASS
