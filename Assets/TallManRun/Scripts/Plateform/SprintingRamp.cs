using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintingRamp : MonoBehaviour
{

    public int bounceDiamondCount = 0;

    public void Enable()
    {
        GameManager.OnDiamondCountUpdated += GetBounceDiamondCount;
        Debug.Log("Enable-----");
    }
    public void Disable()
    {
        GameManager.OnDiamondCountUpdated -= GetBounceDiamondCount;
    }

    private void GetBounceDiamondCount()
    {
        Debug.Log("HEllo----------------------");
        bounceDiamondCount++;
    }

}// CLASS
