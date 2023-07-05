// Decompiled with JetBrains decompiler
// Type: TouchUIButtonDropdown
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchUIButtonDropdown : MonoBehaviour
{
  [SerializeField]
  protected TouchUI2DButton m_dropdownButton;
  [SerializeField]
  protected TMP_Text m_dropdownText;
  [SerializeField]
  protected LayoutGroup m_optionsContainer;
  [SerializeField]
  protected TouchUIButtonDropdown.ButtonActionPair[] m_options;
  protected TouchUI2DButton m_lastSelected;
  private bool m_isOpen;
  private List<EventHandle> _eventHandlers;

  protected void Start() => this.Initialize();

  protected void Initialize()
  {
    this.m_dropdownButton.onClick += new System.Action(this.ToggleDropdownShow);
    this.InitializeOptionButtonLinks();
    this.HideDropdown();
    if (this.m_options.Length == 0 || !((UnityEngine.Object) this.m_options[0].touchButton != (UnityEngine.Object) null))
      return;
    this.m_lastSelected = this.m_options[0].touchButton;
    this.HandleSelection();
  }

  protected void Deinitialize() => this._eventHandlers.Clear();

  protected void ToggleDropdownShow()
  {
    if (this.m_isOpen)
      this.HideDropdown();
    else
      this.ShowDropdown();
    Debug.Log((object) this.m_isOpen);
  }

  protected void ShowDropdown()
  {
    this.m_isOpen = true;
    this.m_optionsContainer.gameObject.SetActive(this.m_isOpen);
  }

  protected void HideDropdown()
  {
    this.m_isOpen = false;
    this.m_optionsContainer.gameObject.SetActive(this.m_isOpen);
  }

  protected void HandleSelection()
  {
    this.m_dropdownText.text = this.m_lastSelected.GetButtonText();
    this.HideDropdown();
  }

  protected virtual void InitializeOptionButtonLinks()
  {
    List<UIHandle> uiHandleList = new List<UIHandle>();
    int num = -1;
    foreach (TouchUIButtonDropdown.ButtonActionPair option in this.m_options)
    {
      ++num;
      if ((UnityEngine.Object) option.touchButton != (UnityEngine.Object) null && option.touchEvent != null)
      {
        option.m_dropdownOwner = this;
        option.touchButton.transform.parent = this.m_optionsContainer.transform;
        option.touchButton.onClick += new System.Action(option.InvokeEvent);
        option.touchButton.onClick += new System.Action(option.SetAsLastSelected);
        option.touchButton.onClick += new System.Action(this.HandleSelection);
      }
      else
        option.touchButton.gameObject.SetActive(false);
    }
  }

  protected void DeinitializeOptionButtonLinks()
  {
    List<UIHandle> uiHandleList = new List<UIHandle>();
    foreach (TouchUIButtonDropdown.ButtonActionPair option in this.m_options)
    {
      option.touchButton.onClick -= new System.Action(option.InvokeEvent);
      option.touchButton.onClick -= new System.Action(option.SetAsLastSelected);
      option.touchButton.onClick -= new System.Action(this.HandleSelection);
    }
  }

  [Serializable]
  public sealed class ButtonActionPair
  {
    [HideInInspector]
    public TouchUIButtonDropdown m_dropdownOwner;
    public TouchUI2DButton touchButton;
    public UnityEvent touchEvent;

    public void InvokeEvent() => this.touchEvent?.Invoke();

    public void SetAsLastSelected()
    {
      if (!((UnityEngine.Object) this.m_dropdownOwner != (UnityEngine.Object) null))
        return;
      this.m_dropdownOwner.m_lastSelected = this.touchButton;
    }
  }
}
