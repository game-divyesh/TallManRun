﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class NewViewController : MonoBehaviour
    {
        public static NewViewController Instance;

        Screen currentView;

        Screen previousView;

        [SerializeField]
        ScreenName initScreen;

        [SerializeField]
        List<ScreenView> screens = new List<ScreenView>();

        [SerializeField]
        List<PopupView> popups = new List<PopupView>();

        [SerializeField]
        NavBar navBar;

        [SerializeField]
        Popup toast;

        Stack<ScreenName> screenStack = new Stack<ScreenName>();

        [System.Serializable]
        public struct ScreenView
        {
            public Screen screen;

            public ScreenName screenName;

            public bool hasNavBar;
        }

        [System.Serializable]
        public struct PopupView
        {
            public Popup popup;

            public PopupName popupName;
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            UnityEngine.Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Init();
        }

        public void ShowPopup(PopupName popupName)
        {
            Debug.Log(popupName);
            popups[GetPopupIndex(popupName)].popup.Show();
        }

        public void HidePopup(PopupName popupName)
        {
            popups[GetPopupIndex(popupName)].popup.Hide();
        }

        public void ShowToast(string description, float delay = 3)
        {
            toast.Fill(description);
            toast.Show();
        }

        public void ChangeScreen(ScreenName screen)
        {
            if (currentView != null)
            {
                previousView = currentView;
                previousView.Hide();
                currentView = screens[GetScreenIndex(screen)].screen;
                currentView.Show();
            }
            else
            {
                currentView = screens[GetScreenIndex(screen)].screen;
                currentView.Show();
            }
        }

        public void HideScreen(ScreenName screen)
        {
            currentView.Hide();
        }

        public void HideSelectedScreen(ScreenName screen)
        {
            currentView = screens[GetScreenIndex(screen)].screen;
            currentView.Hide();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // for (int i = 0; i < popups.Count; i++)
                // {
                //     if (popups[i].popup.isActive)
                //     {
                //         popups[i].popup.Hide();
                //         return;
                //     }
                // }
                // CanvasUtility.ShowPopups(PopupName.ExitPopup);
            }
            // if (Application.internetReachability == NetworkReachability.NotReachable && !showingInternetPopup)
            // {
            //     // ShowInfoPopUp("Cannot connect to internet, please check.", () =>
            //     // {
            //     //     showingInternetPopup = false;
            //     // });
            // }
            // else if (Application.internetReachability != NetworkReachability.NotReachable && showingInternetPopup)
            // {
            //     showingInternetPopup = false;
            //     popup.Hide();
            // }
        }

        // private bool showingInternetPopup = false;
        // InformationPopup popup;
        // private void ShowInfoPopUp(string information, Action action)
        // {
        //     showingInternetPopup = true;
        //     popup = Instance.GetPopup<InformationPopup>(PopupName.InformationPopup);
        //     popup.Show(this.currentView, information, false, action);
        // }
        int GetScreenIndex(ScreenName screen)
        {
            return screens
                .FindIndex(delegate (ScreenView screenView)
                {
                    return screenView.screenName.Equals(screen);
                });
        }

        int GetPopupIndex(PopupName popup)
        {
            return popups
                .FindIndex(delegate (PopupView popupView)
                {
                    return popupView.popupName.Equals(popup);
                });
        }

        public void RedrawView() => currentView.Redraw();

        private void Init()
        {
            for (
                int indexOfScreen = 0;
                indexOfScreen < screens.Count;
                indexOfScreen++
            )
            {
                screens[indexOfScreen].screen.Disable();
            }
            for (
                int indexOfpopup = 0;
                indexOfpopup < popups.Count;
                indexOfpopup++
            )
            {
                popups[indexOfpopup].popup.Disable();
            }
            if (initScreen != ScreenName.None)
            {
                ChangeScreen(initScreen);
            }

            // else if (initScreen != ScreenName.None && !GameConstants.Login)
            // {
            //     ChangeScreen(initScreen);
            // }

            // popups[GetPopupIndex(PopupName.LoadingPopup)].popup.Show();
        }

        // public void ShowPopup(string title, string description)
        // {
        //     toast.Show(title, description);
        // }
        // public void HidePopup()
        // {
        //     toast.Hide();
        // }
        // ViewManager.Instance.GetViewComponent<ViewHunting>().ToggleChipsPopup(true);
        public T GetScreen<T>(ScreenName sName) =>
            (T)screens[GetScreenIndex(sName)].screen.GetComponent<T>();

        public T GetPopup<T>(PopupName sName) =>
            (T)popups[GetPopupIndex(sName)].popup.GetComponent<T>();

        [ContextMenu("SetMaxLayerOfScreen")]
        public void SetMaxLayerOfScreen()
        {
            foreach (ScreenView view in screens)
            {
                view.screen.maxVisibleLayer = 1;
            }
        }
    }
}
