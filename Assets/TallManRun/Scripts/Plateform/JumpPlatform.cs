using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class JumpPlatform : MonoBehaviour
{
	[SerializeField] private Transform jumpTarget;
	[SerializeField] private float jumpPower;
	[SerializeField] private float jumpDuration;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerMovementControl.Instance.playerRB.isKinematic = true;
            Physics.gravity = Vector3.up * -9.8f;
            PlayerMovementControl.Instance.playerRB.constraints = RigidbodyConstraints.FreezeAll;

            PlayerMovementControl.Instance.PlayJumpAnimation();
			PlayerMovementControl.Instance.RestrictPlayerInput();
			SoundManager.Instance.PlaySound(SoundManager.Instance.jumpSound);
			PlayerMovementControl.ExistingSequence = other.transform.DOJump(jumpTarget.position, jumpPower, 1, jumpDuration)
				.OnComplete(() =>
				{
					print("Jump");
					PlayerMovementControl.Instance.StopJumpAnimation();
					PlayerMovementControl.Instance.ResetPlayerInput();
				});
			/*
			var lEffector = other.GetComponent<PlayerMovementControl>().lengthEffector.transform;
			lEffector.DOLocalJump( jumpTarget.position + Vector3.up * lEffector.position.y, 5f, 1, 3f); //.SetEase(Ease.InCubic);
		*/
		/*other.GetComponent<PlayerMovementControl>().lengthEffector.transform
				.DOLocalJump(new Vector3(jumpTarget.position.x,other.GetComponent<PlayerMovementControl>().lengthEffector.transform.position.y,jumpTarget.position.z ), 5f, 1, 3f); //.SetEase(Ease.InCubic);
		*/
		}
	}
}
