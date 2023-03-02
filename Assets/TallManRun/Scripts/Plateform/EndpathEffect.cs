using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftConfetti;
    [SerializeField] private ParticleSystem rightConfetti;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            leftConfetti.Play();
            rightConfetti.Play();
        }
    }


}// CLASS
