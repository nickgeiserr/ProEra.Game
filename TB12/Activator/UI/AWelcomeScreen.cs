// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.AWelcomeScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12.Activator.Data;
using TB12.UI;
using UnityEngine;
using Vars;

namespace TB12.Activator.UI
{
  public class AWelcomeScreen : UIView
  {
    [SerializeField]
    private TouchButton _passingButton;
    [SerializeField]
    private TouchButton _catchingButton;
    [SerializeField]
    private TouchButton _collisionModeButton;
    [SerializeField]
    private TouchButton _playButton;
    [SerializeField]
    private TouchButton _receiverButton;
    [SerializeField]
    private TouchButton _settingsButton;
    [SerializeField]
    private TouchButton _gearButton;
    [SerializeField]
    private TouchButton _switchGameType;
    [SerializeField]
    private TouchButton _multiplayerButton;
    [SerializeField]
    private TouchButton _switchToGameBehind;
    [SerializeField]
    private TouchButton _playbackMode;
    [SerializeField]
    private TouchButton _intelPlayButton;
    [SerializeField]
    private TouchButton _practiceModeButton;
    [SerializeField]
    private TouchButton _loadSeasonButton;
    [SerializeField]
    private TouchButton _minigamesButton;
    [SerializeField]
    private TouchButton _newSeasonButton;
    [SerializeField]
    private ALeaderboardData _leaderboardData;
    [SerializeField]
    private float _actionDelay = 0.4f;

    public override Enum ViewId { get; } = (Enum) EScreens.kWelcome;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._passingButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.ThrowMode))),
      (EventHandle) UIHandle.Link((IButton) this._collisionModeButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.AgilityMode))),
      (EventHandle) UIHandle.Link((IButton) this._catchingButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.CatchingMode))),
      (EventHandle) UIHandle.Link((IButton) this._playButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.PlayMode))),
      (EventHandle) UIHandle.Link((IButton) this._settingsButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.Settings))),
      (EventHandle) UIHandle.Link((IButton) this._gearButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.LockerRoom))),
      (EventHandle) UIHandle.Link((IButton) this._loadSeasonButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.LoadSeason))),
      (EventHandle) UIHandle.Link((IButton) this._newSeasonButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.NewSeason))),
      (EventHandle) UIHandle.Link((IButton) this._multiplayerButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.Multiplayer))),
      (EventHandle) this._practiceModeButton.Link((System.Action) (() => GameplayManager.LoadLevelActivation(EGameMode.kPracticeMode, ETimeOfDay.Clear))),
      (EventHandle) UIHandle.Link((IButton) this._minigamesButton, (System.Action) (() => this.HandleResult(AWelcomeScreen.EWelcomeScreenAction.Minigames))),
      (EventHandle) UIHandle.Link((IButton) this._switchGameType, AppEvents.ChangeGameMode),
      EventHandle.Link<bool>((Variable<bool>) DeveloperMode.Activated, new Action<bool>(this.HandleDeveloperMode))
    });

    protected override void WillAppear() => this._leaderboardData.ResetHighlight();

    private void HandleDeveloperMode(bool devModeEnabled)
    {
    }

    private async void HandleResult(AWelcomeScreen.EWelcomeScreenAction action)
    {
      AppState.SeasonMode.SetValue(ESeasonMode.kUnknown);
      await Task.Delay((int) ((double) this._actionDelay * 1000.0));
      switch (action)
      {
        case AWelcomeScreen.EWelcomeScreenAction.AgilityMode:
          GameplayManager.LoadLevelActivation(EGameMode.kAgility, ETimeOfDay.Dawn);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.CatchingMode:
          GameplayManager.LoadLevelActivation(EGameMode.kCatch, ETimeOfDay.Dusk);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.PlayMode:
          GameplayManager.LoadLevelActivation(EGameMode.kAxisGame, ETimeOfDay.Night);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.PlaybackMode:
          DevControls.PlaybackMode.SetValue(true);
          AppEvents.LoadState(EAppState.kPlay);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.ReceiverMode:
          AppEvents.LoadState(EAppState.kReceiverGame, ETimeOfDay.Overcast);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.Settings:
          UIDispatch.FrontScreen.DisplayView(EScreens.kSettings);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.LockerRoom:
          AppEvents.LoadState(EAppState.kChangeGear);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.Multiplayer:
          AppState.Mode.SetValue(TB12.EMode.kMultiplayer);
          AppEvents.LoadState(EAppState.kChangeGear);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.LoadSeason:
          AppState.SeasonMode.SetValue(ESeasonMode.kLoad);
          AppEvents.LoadState(EAppState.kSeasonLocker);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.NewSeason:
          AppState.SeasonMode.SetValue(ESeasonMode.kNew);
          AppEvents.LoadState(EAppState.kSeasonLocker);
          break;
        case AWelcomeScreen.EWelcomeScreenAction.Minigames:
          AppState.Mode.SetValue(TB12.EMode.kMultiplayer);
          AppEvents.LoadState(EAppState.kMinigameMode);
          break;
        default:
          GameplayManager.LoadLevelActivation(EGameMode.kThrow, ETimeOfDay.Clear);
          break;
      }
    }

    private enum EWelcomeScreenAction
    {
      ThrowMode,
      AgilityMode,
      CatchingMode,
      PlayMode,
      PlaybackMode,
      ReceiverMode,
      Settings,
      LockerRoom,
      Multiplayer,
      PracticeMode,
      LoadSeason,
      NewSeason,
      Minigames,
    }
  }
}
