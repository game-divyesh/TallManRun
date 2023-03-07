using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    #region Variables

    public static PrefsManager Instance;

    internal int Current_Level_Number 
    {
        get
        {
            return PlayerPrefs.GetInt(Current_Level, 1);
        }
        set
        {
            PlayerPrefs.SetInt(Current_Level, value);
        }
    }
    [SerializeField] internal string Current_Level = "CurrentLevel";

    internal int Last_Level_Number
    {
        get
        {
            return PlayerPrefs.GetInt(Last_Level, 1);
        }
        set
        {
            PlayerPrefs.SetInt(Last_Level, value);
        }
    }
    [SerializeField] internal string Last_Level = "LastLevel";

    internal int Diamonds 
    {
        get 
        { 
            return PlayerPrefs.GetInt("Diamonds", 0); 
        }
        set
        {
            PlayerPrefs.SetInt(Diamond, value);
        }
    }
    [SerializeField] internal string Diamond = "Diamonds";

    internal int Player_Material_Color_Index
    {
        get
        {
            return PlayerPrefs.GetInt(Player_Material_Color, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Player_Material_Color, value);
        }
    }
    [SerializeField] internal string Player_Material_Color = "PlayerMaterialColor";

    #endregion

    #region head

    internal int CheckHeadItems(int index)
    {
        return PlayerPrefs.GetInt($"HeadItem-{index}", -1);
    }

    internal void BuyHeadItems(int index)
    {
        PlayerPrefs.SetInt($"HeadItem-{index}", 0);
    }

    #endregion

}
