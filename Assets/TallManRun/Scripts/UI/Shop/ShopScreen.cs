using System;
using UnityEngine;
using UISystem;
using TMPro;
using UnityEngine.UI;

public class ShopScreen : UISystem.Screen
{
    [Header("DIAMOND TEXT")]
    [SerializeField] TextMeshProUGUI diamondCountText;

    [Space]
    [SerializeField] GameObject shopPlayerObj;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button bodyBtn;
    [SerializeField] private Button headBtn;
    [Space]
    [SerializeField] GameObject bodyContainerObj, headContainerObj;


    #region UI SYSTEM
    public override void Awake()
    {
        base.Awake();
    }
    public override void Show()
    {
        base.Show();
        shopPlayerObj.SetActive(true);
        UpdateDiamondText();
        OnBodyButtonClick();
    }
    public override void Hide()
    {
        base.Hide();
        shopPlayerObj.SetActive(false);
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


    public void OnBackButtonClick()
    {
        ViewController.Instance.ChangeScreen(ScreenName.MainScreen);
    }

    public void OnBodyButtonClick()
    {
        bodyContainerObj.SetActive(true);
        headContainerObj.SetActive(false);

        bodyBtn.interactable= false;
        headBtn.interactable= true;
    }

    public void OnHeadButtonClick()
    {
        bodyContainerObj.SetActive(false);
        headContainerObj.SetActive(true);

        bodyBtn.interactable = true;
        headBtn.interactable = false;
    }

    public void OnGetGemsButtonClick()
    {

    }

    public void UpdateDiamondText()
    {
        diamondCountText.text = GameData.diamondCount.ToString();
    }


}// CLASS
