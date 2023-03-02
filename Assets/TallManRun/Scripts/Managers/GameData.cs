using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int diamondCount = 5000;

    public static int currentSpeedCost = 50;
    public static int currentHeightCost = 50;
    public static int currentWidthCost = 50;

    public static int incrementCost = 10;
    public static float currentSpeed = 4.25f;
    public static float speedIncrementFactor = 0.1f;
    public static int bodyIncrementFactor = 10;

    public static int currentSpeedLevel = 0;
    public static int currentHeightLevel = 0;
    public static int currentWidthLevel = 0;


    public static int levelCompletedCount = 0;

    public static bool isPurchasedAdBlock = false;


    private static int currentLevel;
    public static int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            if (value >= 1 && value <= 12)
            {
                currentLevel = value;
            }
            if (value == 12)
            {
                CurrentLevelsDeck++;
                CurrentLevel = 1;
            }
        }
    }


    private static int currentLevelsDeck;
    public static int CurrentLevelsDeck
    {
        get { return currentLevelsDeck; }
        set
        {
            if (value >= 0 && value < 5)
            {
                currentLevelsDeck = value;
            }
        }
    }


    private static int playerMaterial =0;
    public static int PlayerMaterialColor
    {
        get { return playerMaterial; }
        set
        {
            if (value >-1 && value < 12)
            {
                playerMaterial = value;
            }
        }
    }

    private static int playerHeadWear;

    public static int PlayerHeadWear
    {
        get { return playerHeadWear; }
        set 
        {
            if (value>-1 && value < 12)
            {
                playerHeadWear = value; 
            } 
        }
    }

    /*private static List<int> purchasedMaterialItems;

    public static List<int> PurchasedMaterialItems
    {
        get { return purchasedMaterialItems; }
        set { purchasedMaterialItems = value; }
    }


    private static List<int> purchasedHeadItems;

    public static List<int> PurchasedHeadItems
    {
        get { return purchasedHeadItems; }
        set { purchasedHeadItems = value; }
    }*/


    private static int[] purchasedMaterialItems;

    public static int[] PurchasedMaterialItems
    {
        get { return purchasedMaterialItems; }
        set { purchasedMaterialItems = value; }
    }


    private static int[] purchasedHeadItems;

    public static int[] PurchasedHeadItems
    {
        get { return purchasedHeadItems; }
        set { purchasedHeadItems = value; }
    }

}// CLASS
