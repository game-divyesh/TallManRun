﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace UISystem
{
    public class Screen : BaseUI
    {
        private Module currentModule;
        [HideInInspector]
        public List<BaseUI> visibleBaseUI;

        public override void Awake()
        {
            base.Awake();
            visibleBaseUI = new List<BaseUI>();
            visibleBaseUI.Add(this);
        }

        public override void Show()
        {
            visibleBaseUI.Clear();
            visibleBaseUI.Add(this);
            base.Show();
        }

        public void ChangeModule(Module targetModule)
        {
            if (currentModule != null)
                currentModule.Hide();
            currentModule = targetModule;
            currentModule.Show();
        }

        public void HideModule(params Module[] modules)
        {
            for (int indexOfModule = 0; indexOfModule < modules.Length; indexOfModule++)
            {
                modules[indexOfModule].Disable();
            }
        }

        public virtual void Back()
        {

        }

        public void AddPopup(BaseUI popup)
        {
            visibleBaseUI.Add(popup);
        }

        public void HideUnncessoryBaseUI(BaseUI baseUI)
        {
            AddPopup(baseUI);
            int maxVisibleLayer = visibleBaseUI[visibleBaseUI.Count - 1].maxVisibleLayer;
            //for (int indexOfBaseUI = visibleBaseUI.Count - (maxVisibleLayer + 1); indexOfBaseUI >= 0; indexOfBaseUI--)
            //{
            //    visibleBaseUI[indexOfBaseUI].ToggleCanvas(false);
            //    //Debug.Log("visibleBaseUI :  " + visibleBaseUI[indexOfBaseUI].name);
            //}

            for (int indexOfBaseUI = 0; indexOfBaseUI < visibleBaseUI.Count - (maxVisibleLayer + 1); indexOfBaseUI++)
            {
                visibleBaseUI[indexOfBaseUI].ToggleCanvas(true);
                //Debug.Log("visibleBaseUI :  " + visibleBaseUI[indexOfBaseUI].name);
            }


            //Debug.Log(visibleBaseUI.Count - (maxVisibleLayer + 1));
        }

        public void ShowNcessoryBaseUI(BaseUI baseUI)
        {
            RemoveFromVisibleList(baseUI);
            //  
            int maxVisibleLayer = visibleBaseUI[visibleBaseUI.Count - 1].maxVisibleLayer;
            //            Debug.Log(visibleBaseUI[visibleBaseUI.Count - 1].name);
            //          Debug.Log(visibleBaseUI.Count);

            for (int indexOfBaseUI = 0; indexOfBaseUI < visibleBaseUI.Count - maxVisibleLayer; indexOfBaseUI++)
            {
                visibleBaseUI[indexOfBaseUI].ToggleCanvas(true);
                //Debug.Log("visibleBaseUI :  " + visibleBaseUI[indexOfBaseUI].name);
            }

            //for (int indexOfBaseUI = visibleBaseUI.Count - 1; indexOfBaseUI >= visibleBaseUI.Count - maxVisibleLayer; indexOfBaseUI--)
            //{
            //    visibleBaseUI[indexOfBaseUI].ToggleCanvas(true);
            //    //Debug.Log("visibleBaseUI :  " + visibleBaseUI[indexOfBaseUI].name);
            //}
            // }
            // else
            // {
            //     visibleBaseUI[0].ToggleCanvas(true);
            // }
        }

        public void RemoveFromVisibleList(BaseUI baseUI)
        {
            if (visibleBaseUI.Contains(baseUI))
            {
                visibleBaseUI.Remove(baseUI);
            }
        }

        public new int GetSortingOrder()
        {
            if (visibleBaseUI.Count > 0)
            {
                return visibleBaseUI[visibleBaseUI.Count - 1].GetSortingOrder();
            }
            return GetSortingOrder();
        }
    }
}