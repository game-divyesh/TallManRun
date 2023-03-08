using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using UnityEngine.UI;
using TMPro;
using System;

public class MainScreen : UISystem.Screen
{
    public List<PopupView> popupViews;

    [Space]
    [Header("BUTTONS")]
    [SerializeField] Button speedBtn;
    [SerializeField] Button heightBtn;
    [SerializeField] Button widthBtn;

    [Space]
    [Header("BUTTONS LEVEL TEXT")]
    [SerializeField] TextMeshProUGUI speedLevelText;
    [SerializeField] TextMeshProUGUI heightLevelText;
    [SerializeField] TextMeshProUGUI widthLevelText;

    [Space]
    [Header("BUTTONS DIAMOND COST TEXT")]
    [SerializeField] TextMeshProUGUI speedCostText;
    [SerializeField] TextMeshProUGUI heightCostText;
    [SerializeField] TextMeshProUGUI widthCostText;

    [Space]
    [Header("LEVEL TEXT")]
    [SerializeField] TextMeshProUGUI levelText;
    [Space]
    [Header("DIAMOND TEXT")]
    [SerializeField] TextMeshProUGUI diamondCountText;

    [Space]
    [Header("Level Slider Script")]
    [SerializeField] private LevelSliderUI levelSliderUIScript;

    


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
        CheckDiamondAmout();
        levelSliderUIScript.UpdateLevelSliderData();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void Enable()
    {
        base.Enable();
        GameManager.OnDiamondCountUpdated += UpdateDiamondText;
    }
    public override void Disable()
    {
        base.Disable();
        GameManager.OnDiamondCountUpdated -= UpdateDiamondText;
    }
    public override void Redraw()
    {
        base.Redraw();
    }


    #endregion



    #region BUTTON METHOD's

    public void OnSettingButtonClick()
    {
        CanvasUtility.ShowPopups(this, popupViews, PopupName.SettingPopUp);
    }

    public void OnShopButtonClick()
    {
        ViewController.Instance.ChangeScreen(ScreenName.ShopScreen);
    }

    public void OnAdBlockButtonClick()
    {
        ViewController.Instance.ChangeScreen(ScreenName.AdsShopScreen);
    }

    public void OnSpeedUpButtonClick()
    {
        if (GameData.diamondCount <= 0) return;
        GameManager.Instance.OnSpeedIncrease();
        GameData.currentSpeedCost += GameData.incrementCost;
        speedCostText.text = GameData.currentSpeedCost.ToString();
        GameData.currentSpeedLevel++;
        speedLevelText.text = $"LEVEL {GameData.currentSpeedLevel}";
        CheckDiamondAmout();
    }

    public void OnHeightIncreaseButtonClick()
    {
        if (GameData.diamondCount <= 0) return;
        GameManager.Instance.OnHeightIncrease();
        GameData.currentHeightCost += GameData.incrementCost;
        heightCostText.text = GameData.currentHeightCost.ToString();
        GameData.currentHeightLevel++;
        heightLevelText.text = $"LEVEL {GameData.currentHeightLevel}";
        CheckDiamondAmout();
    }

    public void OnWidthIncreaseButtonClick()
    {
        if (GameData.diamondCount <= 0) return;
        GameManager.Instance.OnWidthIncrease();
        GameData.currentWidthCost += GameData.incrementCost;
        widthCostText.text = GameData.currentWidthCost.ToString();
        GameData.currentWidthLevel++;
        widthLevelText.text = $"LEVEL {GameData.currentWidthLevel}";
        CheckDiamondAmout();
    }

    public void OnTapToStartButtonClick()
    {
        ViewController.Instance.ChangeScreen(ScreenName.InGameScreen);
        GameManager.Instance.isGameStart = true;
        GameManager.Instance.isGameEnd = false;

        PlayerMovementControl.Instance.OnGameStartSavePlayerBodyData();
    }

    #endregion

    private void UpdateDiamondText()
    {
        diamondCountText.text = GameData.diamondCount.ToString();
    }

    private void UpdateLevelText()
    {
        levelText.text = GameData.CurrentLevelsDeck > 0 ? $"LEVEL {GameData.CurrentLevelsDeck}{GameData.CurrentLevel}" : $"LEVEL {GameData.CurrentLevel}";
    }

    private void CheckDiamondAmout()
    {
        if (GameData.diamondCount < GameData.currentSpeedCost)
            speedBtn.interactable = false;
        else
            speedBtn.interactable = true;


        if (GameData.diamondCount < GameData.currentHeightCost)
            heightBtn.interactable = false;
        else
            heightBtn.interactable = true;


        if (GameData.diamondCount < GameData.currentWidthCost)
            widthBtn.interactable = false;
        else
            widthBtn.interactable = true;
    }

}// CLASS
