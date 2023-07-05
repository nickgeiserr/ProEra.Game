// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.SeasonMode.SeasonTablet.CanvasTabManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace ProEra.Game.Sources.SeasonMode.SeasonTablet
{
  public class CanvasTabManager : MonoBehaviour
  {
    [SerializeField]
    private CanvasTabManagerConfig _config;
    [SerializeField]
    private Color _textColorUnselected;
    [SerializeField]
    private Color _textColorSelected;
    [SerializeField]
    private Color _textColorDisabeled;
    [SerializeField]
    private SeasonTabletCanvasManager _canvasManager;
    public bool autoResetToStartTab;
    [Tooltip("Instead of enable/disable we move the object offscreen")]
    public bool disableOffsetsObject;
    public int startingTab;
    private int cTab;
    public UnityEvent onChangeAnyTab;
    public List<CanvasTabManager.TabContentPair> m_tabs;
    private bool m_tabsEnabled = true;
    private const int FARAWAY_Y = -100;

    public int tabCount => this.m_tabs.Count;

    public bool SetTabsEnabled(bool a) => this.m_tabsEnabled = a;

    private void Awake() => this.Initialize();

    private void ValidateInspectorBinding()
    {
    }

    private void Initialize()
    {
      if (this.m_tabs == null)
        this.m_tabs = new List<CanvasTabManager.TabContentPair>();
      this.cTab = this.startingTab;
      for (int index = 0; index < this.m_tabs.Count; ++index)
      {
        int tabIndex = index;
        UnityEngine.UI.Button tabButton = this.m_tabs[index].tabButton;
        tabButton.onClick.RemoveAllListeners();
        tabButton.onClick.AddListener((UnityAction) (() => this.SwitchToTabIndex(tabIndex)));
        IButton component = tabButton.gameObject.GetComponent<IButton>();
        (component == null ? (IButton) tabButton.gameObject.AddComponent<TouchUI2DButton>() : component).onClick += (System.Action) (() => this.SimulateTabPress(tabIndex));
      }
      this.SimulateTabPress(this.startingTab, true);
    }

    private void OnEnable()
    {
      if (this.autoResetToStartTab)
        this.SwitchToTabIndex(this.startingTab);
      else
        this.HandleTextColors(this.cTab);
    }

    public void SimulateTabPress(int a_tabIndex, bool force = false)
    {
      if (!this.m_tabsEnabled && !force)
        return;
      int count = this.m_tabs.Count;
      UnityEngine.UI.Button tabButton = a_tabIndex >= count ? (UnityEngine.UI.Button) null : this.m_tabs[a_tabIndex].tabButton;
      if ((UnityEngine.Object) tabButton == (UnityEngine.Object) null || !tabButton.interactable)
        return;
      this._config.CachedTabIndex = a_tabIndex;
      tabButton.onClick?.Invoke();
    }

    private void SwitchToTabIndex(int a_buttonIndex)
    {
      this.cTab = a_buttonIndex;
      this.SetAllTabButtonsInteractable(true);
      this.SetAllContentObjectsEnabled(false);
      this.SetTabButtonInteractable(a_buttonIndex, false);
      this.SetContentObjectEnabled(a_buttonIndex, true);
      this.HandleTextColors(a_buttonIndex);
      this.m_tabs[a_buttonIndex].onChangeToTab?.Invoke();
      this.onChangeAnyTab?.Invoke();
      if (!((UnityEngine.Object) this.m_tabs[a_buttonIndex].contentObject != (UnityEngine.Object) null))
        return;
      this.m_tabs[a_buttonIndex].contentObject.SetActive(true);
    }

    private void HandleTextColors(int a_currentTabIndex)
    {
      for (int index = 0; index < this.m_tabs.Count; ++index)
      {
        TMP_Text tabButtonText = this.m_tabs[index].tabButtonText;
        if ((UnityEngine.Object) tabButtonText != (UnityEngine.Object) null)
        {
          Color color = a_currentTabIndex == index ? this._textColorSelected : (!this.m_tabsEnabled ? this._textColorDisabeled : this._textColorUnselected);
          tabButtonText.color = color;
        }
      }
    }

    public void HandleTextColors() => this.HandleTextColors(this.cTab);

    private void SetTabButtonInteractable(int a_tabIndex, bool a_interactable) => this.m_tabs[a_tabIndex].tabButton.interactable = a_interactable;

    private void SetContentObjectEnabled(int a_contentIndex, bool a_enabled)
    {
      if (!this.disableOffsetsObject)
      {
        this.m_tabs[a_contentIndex].SetActive(true);
      }
      else
      {
        if (!((UnityEngine.Object) this._canvasManager != (UnityEngine.Object) null))
          return;
        this._canvasManager.ShowPage((SeasonTabletCanvasManager.ESeasonTabletCanvas) a_contentIndex);
      }
    }

    private void SetAllTabButtonsInteractable(bool a_interactable)
    {
      foreach (CanvasTabManager.TabContentPair tab in this.m_tabs)
        tab.tabButton.interactable = a_interactable;
    }

    private void SetAllContentObjectsEnabled(bool a_enabled)
    {
      if (this.disableOffsetsObject)
        return;
      this.m_tabs.ForEach((Action<CanvasTabManager.TabContentPair>) (t => t.SetActive(a_enabled).ConfigureAwait(false)));
    }

    [ContextMenu("SimulatePress0")]
    public void Press0() => this.SimulateTabPress(0);

    [ContextMenu("SimulatePress1")]
    public void Press1() => this.SimulateTabPress(1);

    [ContextMenu("SimulatePress2")]
    public void Press2() => this.SimulateTabPress(2);

    [ContextMenu("SimulatePress3")]
    public void Press3() => this.SimulateTabPress(3);

    [ContextMenu("SimulatePress4")]
    public void Press4() => this.SimulateTabPress(4);

    [Serializable]
    public class TabContentPair : IDisposable
    {
      [Space]
      public UnityEngine.UI.Button tabButton;
      public TMP_Text tabButtonText;
      public GameObject contentObject;
      [Space]
      public AssetReference contentRef;
      public Transform parent;
      private GameObject contentRefObject;
      [Space]
      public UnityEvent onChangeToTab;

      public async Task SetActive(bool enable)
      {
        if ((UnityEngine.Object) this.contentObject != (UnityEngine.Object) null && this.contentObject.activeSelf != enable)
          this.contentObject.SetActive(enable);
        if (!this.contentRef.RuntimeKeyIsValid())
          return;
        if ((UnityEngine.Object) this.contentRefObject == (UnityEngine.Object) null & enable)
          this.contentRefObject = await AddressablesData.instance.InstantiateAsync(this.contentRef, Vector3.zero, Quaternion.Euler(Vector3.zero), this.parent);
        if (!((UnityEngine.Object) this.contentRefObject != (UnityEngine.Object) null) || enable)
          return;
        AddressablesData.DestroyGameObject(this.contentRefObject);
      }

      public void Dispose()
      {
        if (!((UnityEngine.Object) this.contentRefObject != (UnityEngine.Object) null) || AddressablesData.instance == null)
          return;
        AddressablesData.DestroyGameObject(this.contentRefObject);
      }
    }
  }
}
