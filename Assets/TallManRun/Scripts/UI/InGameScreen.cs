using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;
using TMPro;

public class InGameScreen : UISystem.Screen
{
    [Header("DIAMOND TEXT")]
    [SerializeField] TextMeshProUGUI diamondCountText;

    [Space]
    [Header("LEVEL TEXT")]
    [SerializeField] TextMeshProUGUI levelText;

    #region UI SYSTEM
    public override void Awake()
    {
        base.Awake();
    }
    public override void Show()
    {
        base.Show();
        UpdateLevelText();
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


    private void UpdateDiamondText()
    {
        diamondCountText.text = GameData.diamondCount.ToString();
    }

    private void UpdateLevelText()
    {
        levelText.text = GameData.CurrentLevelsDeck > 0 ? $"LEVEL {GameData.CurrentLevelsDeck}{GameData.CurrentLevel}" : $"LEVEL {GameData.CurrentLevel}";
    }

}// CLASS
