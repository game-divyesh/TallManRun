using DG.Tweening;
using System;
using UnityEngine;

public class BodyPowerUps : MonoBehaviour
{
    public ChangeType changeType;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;


    private bool _hasBeenUsed;
    public int factor;

    [Space]
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;

    [Space]
    private Color upgradeColor= Color.green;
    private Color degradeColor= Color.red;

    private void OnTriggerEnter(Collider other)
    {
        if (_hasBeenUsed) return;

        PlayerMovementControl.Instance.gatePassEffect.Play();

        if (other.tag.Contains("Player"))
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
            }
        }
        _hasBeenUsed = true;
        Invoke(nameof(HidePowerup), 0.1f);

    }


    private void OnValidate()
    {
        if (changeType == ChangeType.Skinny)
        {
            upArrow.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);

            leftArrow.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            rightArrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            leftArrow.transform.GetChild(0).GetComponent<Renderer>().material = redMat;
            rightArrow.transform.GetChild(0).GetComponent<Renderer>().material = redMat;
            
        }
        else if (changeType == ChangeType.Fat)
        {
            upArrow.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);

            leftArrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rightArrow.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            leftArrow.transform.GetChild(0).GetComponent<Renderer>().material = greenMat;
            rightArrow.transform.GetChild(0).GetComponent<Renderer>().material = greenMat;
        }
        else if (changeType == ChangeType.Tall)
        {
            upArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);

            upArrow.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            upArrow.transform.GetComponent<Renderer>().material = greenMat;
        }
        else if (changeType == ChangeType.Shrink)
        {

            upArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);

            upArrow.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            upArrow.transform.GetComponent<Renderer>().material = redMat;
        }
        name = changeType.ToString() + " Arrow";
    }


    private void HidePowerup()
    {
        gameObject.SetActive(false);
    }

}// Class


