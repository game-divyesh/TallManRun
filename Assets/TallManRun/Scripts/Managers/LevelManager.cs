using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels = new List<GameObject>();

    [Space]
    [Header("Skyboxes")]
    [SerializeField] Material morningSkybox;
    [SerializeField] Material eveningSkybox;

    private GameObject currentLevel;

    private void Start()
    {
        ShowLevel(PrefsManager.Instance.Last_Level_Number);
    }


    public void ShowLevel(int LevelCount)
    {
        if (currentLevel != null)
            Destroy(currentLevel);
        currentLevel = Instantiate(levels[LevelCount], transform.position, Quaternion.identity);
    }


}// CLASS
