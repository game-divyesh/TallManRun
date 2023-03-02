using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public enum BombType
    {
        None,
        Moveable,
        Rotation,
        Both
    }



    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem fuseEffect;
    [Space]

    [SerializeField] private float animTime;
    [SerializeField] private Collider bombCollider;
    [Space]

    public BombType bombType = BombType.None;



    private void Start()
    {
        BombMovement();
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DOTween.KillAll();
            fuseEffect.Stop();
            explosionEffect.Play();
            bombCollider.enabled = false;
            transform.localScale = Vector3.zero;

            SoundManager.Instance.PlaySound(SoundManager.Instance.obstacleHitSound);
            PlayerMovementControl.Instance.MakePlayerShort(5);
            PlayerMovementControl.Instance.DeBuffThePlayer(5);
        }
    }

    private void BombMovement()
    {
        switch (bombType)
        {
            case BombType.None:
                break;


            case BombType.Moveable:
                transform.DOMoveX(1.5f, animTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;


            case BombType.Rotation:
                transform.DOLocalRotate(new Vector3(0, 360, 0), animTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
                break;


            case BombType.Both:
                transform.DOMoveX(1.5f, animTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                transform.DOLocalRotate(new Vector3(0, 360, 0), animTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
                break;
        }
    }


}// CLASS
