// Decompiled with JetBrains decompiler
// Type: TB12.UI.WelcomeScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class WelcomeScreen : UIView
  {
    [SerializeField]
    private TouchButton _singlePlayerButton;
    [SerializeField]
    private TouchButton _multiplayerButton;
    [SerializeField]
    private TouchButton _settingsButton;
    [SerializeField]
    private TouchButton _customizeButton;
    [SerializeField]
    private TouchButton _statsButton;
    [SerializeField]
    private TouchButton _switchGameType;
    [SerializeField]
    private TouchButton _switchGameTypeBehind;
    private System.Action _pendingAction;

    public override Enum ViewId { get; } = (Enum) EScreens.kWelcome;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._settingsButton, (System.Action) (() => UIDispatch.FrontScreen.DisplayView(EScreens.kSettings))),
      (EventHandle) UIHandle.Link((IButton) this._customizeButton, (System.Action) (() => AppEvents.LoadState(EAppState.kChangeGear))),
      (EventHandle) UIHandle.Link((IButton) this._multiplayerButton, new System.Action(this.MultiplayerHandler)),
      (EventHandle) UIHandle.Link((IButton) this._singlePlayerButton, (System.Action) (() =>
      {
        AppState.Mode.SetValue(EMode.kSolo);
        UIDispatch.FrontScreen.DisplayDialog(EScreens.kSinglePlayer);
      })),
      (EventHandle) UIHandle.Link((IButton) this._switchGameType, AppEvents.ChangeGameMode),
      (EventHandle) UIHandle.Link((IButton) this._switchGameTypeBehind, AppEvents.ChangeGameMode),
      EventHandle.Link<bool>((Variable<bool>) DeveloperMode.Activated, new Action<bool>(this.HandleDeveloperMode))
    });

    protected override void WillAppear() => AppState.Mode.SetValue(EMode.kUnknown);

    protected override void DidAppear() => this._pendingAction = (System.Action) null;

    protected override void DidDisappear()
    {
      System.Action pendingAction = this._pendingAction;
      if (pendingAction != null)
        pendingAction();
      this._pendingAction = (System.Action) null;
    }

    private void HandleDeveloperMode(bool obj) => this._switchGameType.gameObject.SetActive(obj);

    private void MultiplayerHandler()
    {
      AppState.Mode.SetValue(EMode.kMultiplayer);
      AppEvents.LoadState(EAppState.kChangeGear);
      this.Hide();
    }
  }
}
