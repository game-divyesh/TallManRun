using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsMovements : MonoBehaviour
{
    public PickUpsMovement pickUpsMovement;

    public List<Transform> pickupTransfrom = new List<Transform>();

    public float lastYPos;
    public float lastXPos;

    [Space]
    public float animTime;
    public float delayTime;


    private void Start()
    {
        if (pickUpsMovement == PickUpsMovement.Vertical)
        {
            StartCoroutine(MoveVertical());
        }
        else if (pickUpsMovement == PickUpsMovement.Horizontal)
        {
            StartCoroutine(MoveHorizontal());
        }
    }


    private IEnumerator MoveVertical()
    {
        foreach (Transform obj in pickupTransfrom)
        {
            obj.DOMoveY(lastYPos, animTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(delayTime);
        }
    }

    private IEnumerator MoveHorizontal()
    {
        foreach (Transform obj in pickupTransfrom)
        {
            obj.DOMoveX(lastXPos, animTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(delayTime);
        }
    }


}// CLASS


public enum PickUpsMovement
{
    none,
    Vertical,
    Horizontal
}