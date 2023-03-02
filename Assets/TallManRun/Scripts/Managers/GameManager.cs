using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UISystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{

    public bool isGameStart = false;
    public bool isGameEnd = false;

    public static event Action OnDiamondCountUpdated;

    [Space]
    [SerializeField] float winScreenDelayInSeconds = 1.5f;
    [SerializeField] float loseScreenDelayInSeconds = 1.5f;
    [Space]
    [SerializeField] LevelManager levelManagerScript;
    [SerializeField] CameraController cameraControllerScript;


    [Space]
    [Header("Default Positions")]
    [SerializeField] Transform playerDefaultPos;
    [SerializeField] Transform cameraDefaultPos;

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 120;
        SetGameData();

    }

    public void ShowLoseScreen()
    {
        Invoke(nameof(OnLevelFailed), loseScreenDelayInSeconds);
    }

    public void ShowWinScreen()
    {
        Invoke(nameof(OnLevelWin), winScreenDelayInSeconds);
    }

    private void OnLevelFailed()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.levelFailSound);
        ViewController.Instance.ChangeScreen(ScreenName.FailedLevelScreen);

        SetPlayerDataOnWinorLoss();
    }

    private void OnLevelWin()
    {
        isGameStart = false;
        GameData.CurrentLevel++;
        GameData.levelCompletedCount++;
        SoundManager.Instance.PlaySound(SoundManager.Instance.levelWinSound);

        ViewController.Instance.ChangeScreen(ScreenName.CompleteLevelScreen);

        levelManagerScript.ShowLevel(GameData.levelCompletedCount);

        SetPlayerDataOnWinorLoss();


    }

    //private void Start()
    //{
    //	levelNumberText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1);
    //}
    //public void ShowWinPanel()
    //{
    //	Invoke(nameof(WinPanel), winPanelDelayInSeconds);
    //}

    //public void ShowLosePanel()
    //{
    //	Invoke(nameof(LosePanel), losePanelDelayInSeconds);
    //}

    //private void WinPanel()
    //{
    //	SoundManager.Instance.PlaySound(SoundManager.Instance.levelWinSound);
    //	winPanel.SetActive(true);
    //}

    //private void LosePanel()
    //{
    //	SoundManager.Instance.PlaySound(SoundManager.Instance.levelFailSound);
    //	losePanel.SetActive(true);
    //}

    //public void NextButton()
    //{
    //	if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount)
    //	{
    //		SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCount));
    //	}
    //	else
    //		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    //public void ReloadButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void OnSpeedIncrease()
    {
        UpdateDiamondCount(-GameData.currentSpeedCost);
        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
        GameData.currentSpeed += GameData.speedIncrementFactor;
        PlayerMovementControl.Instance.movementSpeed = GameData.currentSpeed;
    }

    public void OnHeightIncrease()
    {
        UpdateDiamondCount(-GameData.currentHeightCost);
        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
        PlayerMovementControl.Instance.MakeThePlayerTall(GameData.bodyIncrementFactor);

    }

    public void OnWidthIncrease()
    {
        UpdateDiamondCount(-GameData.currentWidthCost);
        SoundManager.Instance.PlaySound(SoundManager.Instance.multiplierAdditionSound);
        PlayerMovementControl.Instance.BuffThePlayer(GameData.bodyIncrementFactor);
    }

    private void UpdateDiamondCount(int value)
    {
        GameData.diamondCount += value;
        OnDiamondCountUpdated?.Invoke();
    }

    public void PickUpTheDiamond()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.pickUpSound);
        UpdateDiamondCount(+1);
    }

    private void SetGameData()
    {
        GameData.CurrentLevel = 1;
        GameData.CurrentLevelsDeck = 0;

        GameData.PurchasedMaterialItems = new int[12];
        GameData.PurchasedHeadItems = new int[12] { 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
    }

    private void SetPlayerDataOnWinorLoss()
    {
        // Reset Player Pos and Scale
        PlayerMovementControl.Instance.ResetPlayer(playerDefaultPos);
        PlayerMovementControl.Instance.ResetPlayerBody();


        //Reset Camera Pos
        Camera.main.transform.position = cameraDefaultPos.position;
        Camera.main.transform.rotation = cameraDefaultPos.rotation;
        cameraControllerScript._playersLastPosition = PlayerMovementControl.Instance.transform.position;
    }

}// CLASS
