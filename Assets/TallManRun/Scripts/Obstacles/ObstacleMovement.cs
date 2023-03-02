using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
	public ObstacleType obstacleType = ObstacleType.none;
	public float lastY;
	public float animTime;

    private void Start()
    {
		if (obstacleType == ObstacleType.UpDown)
		{
			MoveObjectUpDown();
        }
		if (obstacleType == ObstacleType.Rotation)
		{
			RotateObject();
        }
		if (obstacleType == ObstacleType.Both)
		{
            MoveObjectUpDown();
            RotateObject();
        }
    }

	private void MoveObjectUpDown()
	{
		transform.DOMoveY(lastY, animTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}

	private void RotateObject()
	{
		transform.DOLocalRotate(new Vector3(0, 360, 0), animTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;
		SoundManager.Instance.PlaySound(SoundManager.Instance.movingObstacleHitSound);
		PlayerMovementControl.Instance.MakePlayerShort(1);
		PlayerMovementControl.Instance.DeBuffThePlayer(1);
	}
}


public enum ObstacleType
{
	none,
	UpDown,
	Rotation,
	Both
}