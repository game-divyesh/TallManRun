using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BodyShop : MonoBehaviour
{
    [Header("All Body Items")]
    [SerializeField] List<ShopBodyItem> shopBodyItems = new List<ShopBodyItem>();

    [Space]
    [SerializeField] private ShopScreen shopScreenScript;

    int previousIndex = 0;

    private void Start()
    {
        SetBodyItems();
        previousIndex = GameData.PlayerMaterialColor;
    }

    private void SetBodyItems()
    {
        for (int item = 0; item < shopBodyItems.Count; item++)
        {
            if (!shopBodyItems[item].isPurchased)
            {
                shopBodyItems[item].LockedItem();
            }
            else
            {
                shopBodyItems[item].DeselectItem();
            }

            shopBodyItems[item].OnPurchasedButtonClick(item, OnButtonClick);
        }
    }

    public void OnButtonClick(int itemIndex)
    {
        if (!shopBodyItems[itemIndex].isPurchased)
        {
            if (shopBodyItems[itemIndex].CheckIsPurchaseable())
            {
                GameData.diamondCount -= shopBodyItems[itemIndex].cost;
                shopScreenScript.UpdateDiamondText();

                shopBodyItems[itemIndex].isPurchased = true;
                shopBodyItems[itemIndex].OwnItem(itemIndex);

                shopBodyItems[itemIndex].SelectItem(itemIndex);

                shopBodyItems[previousIndex].DeselectItem();
                previousIndex = itemIndex;
            }
        }
        else
        {
            shopBodyItems[itemIndex].SelectItem(itemIndex);
            shopBodyItems[previousIndex].DeselectItem();
            previousIndex = itemIndex;
        }
    }

}// CLASS
