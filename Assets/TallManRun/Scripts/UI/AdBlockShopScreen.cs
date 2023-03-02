using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

public class AdBlockShopScreen : UISystem.Screen
{


    #region UI SYSTEM
    public override void Awake()
    {
        base.Awake();
    }
    public override void Show()
    {
        base.Show();
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


    public void OnBackButtonClick()
    {
        ViewController.Instance.ChangeScreen(ScreenName.MainScreen);
    }

    public void OnAdBlockButtonClick()
    {

    }

    public void OnAdBlockWithGemButtonClick()
    {

    }

    public void OnAdBlockWithGemBoxButtonClick()
    {

    }

}// CLASS
