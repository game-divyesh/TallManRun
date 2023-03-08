using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ShopHeadItem : MonoBehaviour
{

    public BodyItemPurchaseType bodyPurchaseType = BodyItemPurchaseType.Diamond;
    [Space]
    public GameObject iconImg;
    public GameObject costTextObj;
    public int cost;
    [Space]
    public Sprite selectBG;
    public Sprite lockBG;
    public Sprite openBG;
    [Space]
    public Image currenntBG;

    [Space]
    public Button purchaseBtn;
    public bool isPurchased = false;

    public static event Action OnNewHeadWearSelected;



    public void OnPurchasedButtonClick(int itemIndex, UnityAction<int> action)
    {
        purchaseBtn.onClick.RemoveAllListeners();

        purchaseBtn.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void SelectItem(int headWear)
    {
        currenntBG.sprite = selectBG;
        purchaseBtn.interactable = false;
        iconImg.gameObject.SetActive(false);
        costTextObj.gameObject.SetActive(false);

        GameData.PlayerHeadWear = headWear;
        OnNewHeadWearSelected?.Invoke();
    }

    public void DeselectItem()
    {
        currenntBG.sprite = openBG;
        purchaseBtn.interactable = true;
        iconImg.gameObject.SetActive(false);
        costTextObj.gameObject.SetActive(false);
    }

    public void LockedItem()
    {
        currenntBG.sprite = lockBG;
        iconImg.gameObject.SetActive(true);
        costTextObj.gameObject.SetActive(true);

        purchaseBtn.interactable = CheckIsPurchaseable();
    }

    public bool CheckIsPurchaseable()
    {
        bool isPurchaseable = false;
        if (GameData.diamondCount >= cost)
        {
            isPurchaseable = true;
        }
        return isPurchaseable;
    }


    public void OwnItem(int index)
    {
        GameData.PurchasedHeadItems[index] = index;
        PrefsManager.Instance.BuyHeadItems(index);
        //GameData.PurchasedHeadItems.Sort();
    }



}// CLASS
