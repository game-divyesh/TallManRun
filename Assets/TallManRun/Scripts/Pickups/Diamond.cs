using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public ParticleSystem diamondCollectEffect;
    public Collider diamondCollider;

    private Tween tween;

    private void Start()
    {
        tween = transform.DOLocalRotate(new Vector3(0, 360, 0), 1.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Collector"))
        {
            DOTween.Kill(tween);
            GameManager.Instance.PickUpTheDiamond();
            
            diamondCollectEffect.Play();
            diamondCollider.enabled = false;
            transform.localScale = Vector3.zero;

        }
    }
}// CLASS
