using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SprintTrigger : MonoBehaviour
{
	private Transform cameraPosition;
	
	private Camera _camera;

	private void Start()
	{
		_camera = Camera.main;
		cameraPosition = _camera.transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;
		PlayerMovementControl.Instance.isAtSprintingPhase = true;
		print("Sprint");
		PlayerMovementControl.Instance.PlaySprintAnimation();
		if (PlayerMovementControl.ExistingSequence.IsActive())
		{
			
			PlayerMovementControl.ExistingSequence.Kill(true);
		}
		_camera.transform.DOMove(new Vector3(4, cameraPosition.position.y, cameraPosition.position.z) , 1f);
		_camera.transform.DORotate(new Vector3(10.657f, -16.413f, 0f), 1f);
		PlayerMovementControl.Instance.toSprint = true;
		PlayerMovementControl.Instance.xSpeed = 0f;
	}
}
