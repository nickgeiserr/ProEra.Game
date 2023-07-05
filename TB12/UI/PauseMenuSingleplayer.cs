// Decompiled with JetBrains decompiler
// Type: TB12.UI.PauseMenuSingleplayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI
{
  public class PauseMenuSingleplayer : UIPanel
  {
    [SerializeField]
    private TouchButton _resumeButton;
    [SerializeField]
    private TouchButton _immersiveTackleButton;
    [SerializeField]
    private TouchButton _autoDropbackButton;
    [SerializeField]
    private TouchButton _backButton;

    protected override void OnInitialize()
    {
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._resumeButton, (Action) (() => this.Hide())),
        (EventHandle) UIHandle.Link((IButton) this._immersiveTackleButton, new Action(this.ToggleTackleTypeHandle)),
        (EventHandle) UIHandle.Link((IButton) this._autoDropbackButton, new Action(this.ToggleDropbackTypeHandle)),
        (EventHandle) UIHandle.Link((IButton) this._backButton, new Action(this.BackHandle))
      });
      this.SetImmersiveTackleText(ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.Value);
      this.SetDropbackText();
    }

    private void ToggleTackleTypeHandle()
    {
      ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.SetValue(!ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.Value);
      this.SetImmersiveTackleText(ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.Value);
    }

    private void ToggleDropbackTypeHandle()
    {
      ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled.Toggle();
      this.SetDropbackText();
    }

    private void SetDropbackText() => this._autoDropbackButton.GetComponent<ButtonText>().text = ScriptableSingleton<VRSettings>.Instance.AutoDropbackEnabled.Value ? "Auto Dropback:\nOn" : "Auto Dropback:\nOff";

    private void SetImmersiveTackleText(bool isOn) => this._immersiveTackleButton.GetComponent<ButtonText>().text = ScriptableSingleton<VRSettings>.Instance.ImmersiveTackleEnabled.Value ? "Immersive Tackle: On" : "Immersive Tackle: Off";

    private void RetryHandle()
    {
      if (AppEvents.Retry.enabled)
        UIDispatch.HideAll(false);
      this.Hide();
      AppEvents.Retry.Trigger();
    }

    private void BackHandle()
    {
      UIDispatch.HideAll(false);
      this.Hide();
      AppEvents.LoadMainMenu.Trigger();
    }

    protected override void WillDisappear() => VRState.PauseMenu.SetValue(false);

    public void ShowImmersiveTackleUI() => this._immersiveTackleButton.gameObject.SetActive(true);

    public void HideImmersiveTackleUI()
    {
      this._immersiveTackleButton.gameObject.SetActive(false);
      this.SetImmersiveTackleText(false);
    }
  }
}
