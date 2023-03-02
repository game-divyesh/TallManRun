using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

public class SettingPopup : UISystem.Popup
{
    [SerializeField] Sprite onImage;
    [SerializeField] Sprite offImage;

    [SerializeField] 

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


    public void OnCloseButtonClick()
    {
        Hide();
    }

    public void OnSoundButtonClick()
    {

    }

    public void OnVibrateButtonClick()
    {

    }

    public void OnPrivacyPolicyButtonClick()
    {

    }

    public void OnRateUsButtonClick()
    {

    }

    public void OnRemoveAdsButtonClick()
    {

    }

}
