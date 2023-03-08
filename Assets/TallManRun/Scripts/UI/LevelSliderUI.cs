using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliderUI : MonoBehaviour
{
    [SerializeField] private List<LevelUIData> levelUIDatas= new List<LevelUIData>();

    [Space]
    [SerializeField] private Slider slider;

    [Space]
    [Header("Env Sprite")]
    [SerializeField] List<Sprite> envSprites;

    public void UpdateLevelSliderData()
    {
        levelUIDatas[GameData.CurrentLevel-1].GetComponent<UISystem.Scale>().StartAnimate();
        slider.value = GameData.CurrentLevel - 1;

        for (int index = 0; index < levelUIDatas.Count; index++)
        {
            if (index != levelUIDatas.Count-1)
            {
                levelUIDatas[index].levelText.text = GameData.CurrentLevelsDeck > 0 ? $"{GameData.CurrentLevelsDeck}{index+1}" : $"{index+1}";
            }
            else
            {
                levelUIDatas[index].levelText.text = $"{GameData.CurrentLevelsDeck + 1}{index+1}";
            }


            if (index <= GameData.CurrentLevel-1)
            {
                levelUIDatas[index].fillImg.gameObject.SetActive(true);
            }
            else
            {
                levelUIDatas[index].fillImg.gameObject.SetActive(false);
            }

            //if (index == (GameData.CurrentLevel % 10)-1)
            if (index == (GameData.CurrentLevel - 1))
            {
                levelUIDatas[index].GetComponent<UISystem.Scale>().cycleType = UISystem.AnimationCycleType.Continuous;
                levelUIDatas[index].GetComponent<UISystem.Scale>().StartAnimate();
            }
            else
            {
                levelUIDatas[index].GetComponent<UISystem.Scale>().cycleType = UISystem.AnimationCycleType.None;
                levelUIDatas[index].GetComponent<UISystem.Scale>().StopAnimate();
            }
        }

        levelUIDatas[levelUIDatas.Count - 1].GetComponent<Image>().sprite = envSprites[GameData.CurrentLevelsDeck];
    }

}// CLASS
