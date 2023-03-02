using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bot : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Collider capsuleCollider;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody _rb;
    
    public float forceToApply;
    public float angleOfForce;

    private float _xComponent;
    private float _yComponent;


    private bool isTriggered = false;

    private void Update()
    {
        if (!isTriggered)
        {
            float dis = Vector3.Distance(PlayerMovementControl.Instance.transform.position, transform.position);
            if (dis < 10)
            {
                animator.SetInteger("mode", 1);

                transform.position = Vector3.MoveTowards(transform.position, PlayerMovementControl.Instance.transform.position, speed * Time.deltaTime);
                transform.LookAt(PlayerMovementControl.Instance.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetInteger("mode", 2);
            isTriggered = true;
            AddForceAtAnAngle(forceToApply, angleOfForce);
            SoundManager.Instance.PlaySound(SoundManager.Instance.obstacleHitSound);
            PlayerMovementControl.Instance.MakePlayerShort(2);
            PlayerMovementControl.Instance.DeBuffThePlayer(2);

            capsuleCollider.enabled = false;
        }

    }


    private void AddForceAtAnAngle(float force, float angle)
    {
        angle *= Mathf.Deg2Rad;
        _xComponent = Mathf.Cos(angle) * force;
        _yComponent = Mathf.Sin(angle) * force;

        var forceApplied = new Vector3(_xComponent, _yComponent, 30);

        _rb.AddForce(forceApplied, ForceMode.Acceleration);

    }

}// CLASS
