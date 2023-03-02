using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using TMPro;

public class BonusLevelScreen : UISystem.Screen
{
    [Header("LEVEL TEXT")]
    [SerializeField] TextMeshProUGUI levelText;
    [Space]
    [Header("DIAMOND TEXT")]
    [SerializeField] TextMeshProUGUI diamondCountText;

    #region UI SYSTEM
    public override void Awake()
    {
        base.Awake();
    }
    public override void Show()
    {
        base.Show();
        UpdateDiamondText();
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


    public void OnSkipLevelButtonClick()
    {
         ViewController.Instance.ChangeScreen(ScreenName.MainScreen);
    }

    public void OnBonusLevelPlayButtonClick()
    {

    }

    private void UpdateDiamondText()
    {
        diamondCountText.text = GameData.diamondCount.ToString();
    }


}// CLASS
