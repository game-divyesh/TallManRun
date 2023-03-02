using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCircle : MonoBehaviour
{
    public float animTime;

    private void Start()
    {
        RotateObject();
    }


    private void RotateObject()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), animTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

}// CLASS
