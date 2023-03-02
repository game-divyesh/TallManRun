using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UISystem;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevelScreen : UISystem.Screen
{
    [Header("LEVEL TEXT")]
    [SerializeField] TextMeshProUGUI levelText;
    [Space]
    [Header("DIAMOND TEXT")]
    [SerializeField] TextMeshProUGUI diamondCountText;

    [Space]
    [Header("Item-Sprite")]

    [SerializeField] private List<Sprite> itemSprites = new List<Sprite>();

    [Space]
    [SerializeField] private Image blackImg;
    [SerializeField] private Image itemImg;

    [Space]
    [SerializeField] TextMeshProUGUI itemPercentageText;

    [Space]
    [SerializeField] Slider diamondMuliplierSlider;



    private Tweener diamondMultiplierTween;
    private int multiplier;



    #region UI SYSTEM
    public override void Awake()
    {
        base.Awake();
    }
    public override void Show()
    {
        base.Show();
        UpdateDiamondText();
        UpdateLevelText();

        DiamondMultiplier();
        SetRandomHeadWearItem();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void Enable()
    {
        base.Enable();
    }
    public override void Disable()
    {
        base.Disable();
    }
    public override void Redraw()
    {
        base.Redraw();
    }


    #endregion

    public void OnAddExtraPercentageButtonClick()
    {

    }

    public void OnGemsMultiplierButtonClick()
    {
        DOTween.Kill(diamondMultiplierTween);
        //DOTween.KillAll();
        float slidervalue = (diamondMuliplierSlider.value);

        if (slidervalue >= 0 && slidervalue <= 0.72f)
        {
            multiplier = 2;
        }
        else if (slidervalue > 0.72f && slidervalue <= 1.48f)
        {
            multiplier = 3;
        }
        else if (slidervalue > 1.48f && slidervalue <= 2.42f)
        {
            multiplier = 4;
        }
        else if (slidervalue > 2.42f && slidervalue <= 3.58f)
        {
            multiplier = 5;
        }
        else if (slidervalue > 3.58f && slidervalue <= 4.52f)
        {
            multiplier = 4;
        }
        else if (slidervalue > 4.52f && slidervalue <= 5.27f)
        {
            multiplier = 3;
        }
        else if (slidervalue > 5.27f && slidervalue <= 6)
        {
            multiplier = 2;
        }

        Debug.Log(multiplier);

    }

    public void OnNoThankButtonClick()
    {
        ViewController.Instance.ChangeScreen(ScreenName.MainScreen);
    }

    public void OnCliamButtonClick()
    {

    }

    private void UpdateDiamondText()
    {
        diamondCountText.text = GameData.diamondCount.ToString();
    }

    private void UpdateLevelText()
    {
        levelText.text = GameData.CurrentLevelsDeck > 0 ? $"LEVEL {GameData.CurrentLevelsDeck}{GameData.CurrentLevel}" : $"LEVEL {GameData.CurrentLevel}";
    }



    private void SetRandomHeadWearItem()
    {
        if (itemImg.fillAmount == 0)
        {
            var unSold = (
                from data in GameData.PurchasedHeadItems
                where data.Equals(-1)
                select data
                ).ToList();

            if (unSold.Count>0)
            {
                int itemIndex = UnityEngine.Random.Range(0, unSold.Count);

                blackImg.sprite = itemImg.sprite = itemSprites[itemIndex];
                Debug.Log(itemIndex); 
            }
        }

        itemImg.fillAmount += 0.1f;
        itemPercentageText.text = $"{itemImg.fillAmount * 100}%";

    }

    private void DiamondMultiplier()
    {
        diamondMultiplierTween = diamondMuliplierSlider.DOValue(6, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

}// CLASS
