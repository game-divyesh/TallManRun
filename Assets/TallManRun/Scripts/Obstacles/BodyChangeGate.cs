using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class BodyChangeGate : MonoBehaviour
{
	

	public ObjectType objectType = ObjectType.none;

	public GameObject tallSprite;
	public GameObject shortSprite;
	public GameObject fatSprite;
	public GameObject skinnySprite;

	public MeshRenderer meshRenderer;
	
	public ChangeType changeType = ChangeType.Tall;
	public TMP_Text text;
	public int factor;

	private bool _hasBeenUsed = false;

	public Color positiveColor;
	public Color negativeColor;

	[Space]
	public float animTime;


	private void Start()
	{
		
		if (changeType == ChangeType.Fat || changeType == ChangeType.Tall)
		{
			meshRenderer.material.color = positiveColor;
			transform.GetChild(0).GetComponent<MeshRenderer>().material.color = positiveColor;
		}
		else
		{
			meshRenderer.material.color = negativeColor;
			transform.GetChild(0).GetComponent<MeshRenderer>().material.color = negativeColor;
		}


		if (objectType == ObjectType.Moveable)
		{
			MoveGate();

        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (_hasBeenUsed) return;

        PlayerMovementControl.Instance.gatePassEffect.Play();

        if (other.CompareTag("Player"))
        {
            switch (changeType)
            {
                case ChangeType.Tall:
                    {
                        PlayerMovementControl.Instance.MakeThePlayerTall(factor);
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
                        break;
                    }
                case ChangeType.Shrink:
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierSubractionSound);
                        PlayerMovementControl.Instance.MakePlayerShort(factor);
                        break;
                    }
                case ChangeType.Fat:
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
                        PlayerMovementControl.Instance.BuffThePlayer(factor);
                        break;
                    }
                case ChangeType.Skinny:
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierSubractionSound);
                        PlayerMovementControl.Instance.DeBuffThePlayer(factor);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        _hasBeenUsed = true;
        Invoke(nameof(HideGate), 0.1f);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (_hasBeenUsed) return;

        PlayerMovementControl.Instance.gatePassEffect.Play();

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (changeType)
            {
                case ChangeType.Tall:
                    {
                        PlayerMovementControl.Instance.MakeThePlayerTall(factor);
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
                        break;
                    }
                case ChangeType.Shrink:
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierSubractionSound);
                        PlayerMovementControl.Instance.MakePlayerShort(factor);
                        break;
                    }
                case ChangeType.Fat:
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
                        PlayerMovementControl.Instance.BuffThePlayer(factor);
                        break;
                    }
                case ChangeType.Skinny:
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierSubractionSound);
                        PlayerMovementControl.Instance.DeBuffThePlayer(factor);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        _hasBeenUsed = true;
        Invoke(nameof(HideGate), 0.1f);
    }*/

	private void OnValidate()
	{
		if (changeType == ChangeType.Tall)
		{
			tallSprite.SetActive(true);
			shortSprite.SetActive(false);
			fatSprite.SetActive(false);
			skinnySprite.SetActive(false);
			name = text.text = " + " + factor;
			transform.name = "Gate" + name;
		}
		else if (changeType == ChangeType.Shrink)
		{
			tallSprite.SetActive(false);
			shortSprite.SetActive(true);
			fatSprite.SetActive(false);
			skinnySprite.SetActive(false);
			name = text.text = " - " + factor;
            transform.name = "Gate" + name;
        }
		else if (changeType == ChangeType.Fat)
		{
			tallSprite.SetActive(false);
			shortSprite.SetActive(false);
			fatSprite.SetActive(true);
			skinnySprite.SetActive(false);
			name = text.text = " + " + factor;
            transform.name = "Gate" + name;
        }
		else if (changeType == ChangeType.Skinny)
		{
			tallSprite.SetActive(false);
			shortSprite.SetActive(false);
			fatSprite.SetActive(false);
			skinnySprite.SetActive(true);
			name = text.text = " - " + factor;
            transform.name = "Gate" + name;
        }
		else
		{
			name = name;
		}
		
		if(objectType == ObjectType.none)
		{
			animTime= 0;
		}
	}

	private void HideGate()
    {
		transform.DOMoveY(-10, 0.4f).SetEase(Ease.InOutSine);
    }


	private void MoveGate()
	{
		transform.DOMoveX(1.5f, animTime).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
	}
}

public enum ObjectType
{
	none,
	Moveable
}


public enum ChangeType
{
    Tall,
    Shrink,
    Fat,
    Skinny
}