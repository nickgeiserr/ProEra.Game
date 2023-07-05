// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CanvasTabManager3D
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FootballVR.UI
{
  public class CanvasTabManager3D : MonoBehaviour
  {
    [SerializeField]
    private int defaultTab;
    public List<CanvasTabManager3D.TouchButtonTabContentPair> m_tabs;
    private CanvasTabManager3D.TouchButtonTabContentPair m_currentTab;

    private void Awake()
    {
      foreach (CanvasTabManager3D.TouchButtonTabContentPair tab in this.m_tabs)
      {
        CanvasTabManager3D.TouchButtonTabContentPair _thisTab = tab;
        _thisTab.tabButton.onClick += (Action) (() => this.SetCurrentTab(_thisTab));
      }
      if (this.defaultTab >= this.m_tabs.Count)
        return;
      this.m_tabs[this.defaultTab].tabTargetScreen.SetActive(true);
      MonoBehaviour.print((object) "DefaultTab");
    }

    private void Update()
    {
    }

    private void SetAllTabButtonsInteractable(bool a_interactable) => this.m_tabs.ForEach((Action<CanvasTabManager3D.TouchButtonTabContentPair>) (t => t.tabButton.Interactable.SetValue(a_interactable)));

    private void SetAllTabContents(bool a_active) => this.m_tabs.ForEach((Action<CanvasTabManager3D.TouchButtonTabContentPair>) (t => t.tabTargetScreen.SetActive(a_active)));

    private void SetTabButtonInteractable(TouchButton a_button, bool a_interactable) => a_button.Interactable.SetValue(a_interactable);

    private void SetCurrentTab(CanvasTabManager3D.TouchButtonTabContentPair a_tab)
    {
      this.m_currentTab = a_tab;
      this.SetAllTabContents(false);
      this.m_currentTab.tabTargetScreen.SetActive(true);
      MonoBehaviour.print((object) ("TAB:" + this.m_currentTab.tabTargetScreen.name));
    }

    [Serializable]
    public class TouchButtonTabContentPair
    {
      [Space]
      public TouchButton tabButton;
      public GameObject tabTargetScreen;
      [Space]
      public UnityEvent onChangeToTab;
    }
  }
}
