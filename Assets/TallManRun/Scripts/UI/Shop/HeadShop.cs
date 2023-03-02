using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShop : MonoBehaviour
{
    [Header("All Body Items")]
    [SerializeField] List<ShopHeadItem> shopHeadItems = new List<ShopHeadItem>();

    [Space]
    [SerializeField] private ShopScreen shopScreenScript;

    int previousIndex = 0;

    private void Start()
    {
        SetBodyItems();
        previousIndex = GameData.PlayerHeadWear;
    }

    private void SetBodyItems()
    {
        for (int item = 0; item < shopHeadItems.Count; item++)
        {
            if (!shopHeadItems[item].isPurchased)
            {
                shopHeadItems[item].LockedItem();
            }
            else
            {
                shopHeadItems[item].DeselectItem();
            }

            shopHeadItems[item].OnPurchasedButtonClick(item, OnButtonClick);
        }
    }

    public void OnButtonClick(int itemIndex)
    {
        if (!shopHeadItems[itemIndex].isPurchased)
        {
            if (shopHeadItems[itemIndex].CheckIsPurchaseable())
            {
                GameData.diamondCount -= shopHeadItems[itemIndex].cost;
                shopScreenScript.UpdateDiamondText();

                shopHeadItems[itemIndex].isPurchased = true;
                shopHeadItems[itemIndex].OwnItem(itemIndex);

                shopHeadItems[itemIndex].SelectItem(itemIndex);

                shopHeadItems[previousIndex].DeselectItem();
                previousIndex = itemIndex;
            }
        }
        else
        {
            shopHeadItems[itemIndex].SelectItem(itemIndex);
            shopHeadItems[previousIndex].DeselectItem();
            previousIndex = itemIndex;
        }
    }
}// CLASS
