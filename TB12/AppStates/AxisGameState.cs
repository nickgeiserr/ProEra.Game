// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.AxisGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/AxisGameState")]
  public class AxisGameState : GameState
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private UniformLogoStore _store;
    protected float _tunnelTimer;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private bool _finishedOnEnter;
    private bool _finishedLoadingStadium;
    private readonly RoutineHandle _transRoutineHandle = new RoutineHandle();

    public override EAppState Id => EAppState.kAxisGame;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      if (this.Id == EAppState.kTunnel)
        return;
      this.OnEnterStateAsync();
    }

    private async System.Threading.Tasks.Task OnEnterStateAsync()
    {
      AxisGameState axisGameState = this;
      axisGameState._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        Globals.GameOver.Link<bool>(new Action<bool>(axisGameState.OnGameOver)),
        PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(axisGameState.HandleStateTransition))
      });
      AppState.GameInfoUI.SetValue(true);
      VRState.LocomotionEnabled.SetValue(false);
      ScriptableSingleton<VRSettings>.Instance.HelmetActive.OnValueChanged += new Action<bool>(axisGameState.HandleHelmetChanged);
      PersistentData.SetGameMode(GameMode.PlayerVsAI);
      PersistentData.CoachCallsPlays = true;
      MatchManager.instance.CallAwake();
      SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.CallStart();
      await MatchManager.instance.playersManager.CallAwake();
      ScoreClockState.TEMP_InitializeScoreClock.Trigger();
      if (AppState.GameMode != EGameMode.kOnboarding && AppState.GameMode != EGameMode.k2MD && AppState.GameMode != EGameMode.kHeroMoment)
        MatchManager.instance.CallStart();
      MatchManager.instance.playersManager.CallStart();
      UIDispatch.FrontScreen.Preload(EScreens.kBasicFTUE);
      switch (axisGameState)
      {
        case PracticeModeState _:
        case OnboardingState _:
          AppSounds.PlayerChatterSound.ForceValue(false);
          PlaybookState.CurrentFormation.SetValue(Plays.self.shotgunPlays_Normal);
          int[] playersInFormation1 = PersistentData.GetUserTeam().TeamDepthChart.GetPlayersInFormation(PlaybookState.CurrentFormation.Value.GetPlay(0).GetFormation());
          MatchManager.instance.playersManager.CreateTeamPlayers(1, playersInFormation1);
          MatchManager.instance.playersManager.CreateTeamPlayers(2);
          break;
        default:
          WorldState.CrowdEnabled.SetValue(true);
          AppSounds.CrowdSound.SetValue(true);
          AppSounds.AnnouncerSound.SetValue(true);
          AppSounds.PlayerChatterSound.SetValue(true);
          if (AppState.SeasonMode.Value == ESeasonMode.kUnknown)
          {
            PersistentData.gameType = GameType.QuickMatch;
            MatchManager.instance.EmptyField();
            MatchManager.instance.playersManager.CreateTeamPlayers(1);
            MatchManager.instance.playersManager.CreateTeamPlayers(2);
            if (axisGameState.Id != EAppState.k2MD)
              axisGameState.UpdateGroupPresence_ExhibitionMode();
          }
          else
          {
            PersistentData.gameType = GameType.SeasonMode;
            MatchManager.instance.EmptyField();
            MatchManager.instance.playersManager.CreateTeamPlayers(1);
            MatchManager.instance.playersManager.CreateTeamPlayers(2);
            axisGameState.UpdateGroupPresence_SeasonMode();
          }
          PlaybookState.CurrentFormation.SetValue(Plays.self.specialOffPlays);
          int[] playersInFormation2 = PersistentData.GetCompTeam().TeamDepthChart.GetPlayersInFormation(PlaybookState.CurrentFormation.Value.GetPlay(2).GetFormation());
          MatchManager.instance.playersManager.CreateTeamPlayers(2, playersInFormation2);
          PlaybookState.CurrentFormation.SetValue(Plays.self.kickReturnPlays);
          int[] playersInFormation3 = PersistentData.GetUserTeam().TeamDepthChart.GetPlayersInFormation((FormationPositions) ((PlayDataDef) PlaybookState.CurrentFormation.Value.GetPlay(1)).GetFormation());
          MatchManager.instance.playersManager.CreateTeamPlayers(1, playersInFormation3);
          PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 180f, 0.0f)));
          break;
      }
      axisGameState._throwManager.ForceAutoAim = true;
      axisGameState._throwManager.AutoAimRange = 10f;
      axisGameState._finishedOnEnter = true;
      axisGameState.CheckIfPrereqsAreLoaded();
    }

    private void OnGameOver(bool gameOver)
    {
      if (!gameOver)
        return;
      this._transRoutineHandle.Run(this.OnGameOverRoutine());
    }

    private IEnumerator OnGameOverRoutine()
    {
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.SeasonModeUpdate, true);
      yield return (object) new WaitForSeconds(3f);
      TransitionScreenController.SetTransitionActive(TransitionScreenController.ETransitionType.SeasonModeUpdate, false);
      AppEvents.LoadMainMenu.Trigger();
    }

    private void HandleHelmetChanged(bool enabled) => VRState.HelmetEnabled.SetValue(enabled);

    private void HandleStateTransition(bool inTransition)
    {
      if (inTransition)
        return;
      if (AppState.GameMode != EGameMode.kPracticeMode)
      {
        Debug.Log((object) ("HandleStateTransition: AppState.SeasonMode.Value: " + AppState.SeasonMode.Value.ToString()));
        this.OnStadiumLoadFinished();
      }
      else
        GamePlayerController.CameraFade.Clear();
    }

    protected virtual void OnStadiumLoadFinished()
    {
      Debug.Log((object) nameof (OnStadiumLoadFinished));
      AppState.GameInfoUI.ForceValue(true);
      AppSounds.StopSfx(ESfxTypes.kTunnel, 1);
      AppSounds.StopMusic.Trigger();
      AppSounds.AmbienceSound.ForceValue(true);
      AppSounds.AMBIENT_MOD = 0.0f;
      AppSounds.AdjustAmbientVolume(AppSounds.AMBIENT_START);
      if (AppState.GameMode == EGameMode.kAxisGame)
      {
        if (!GamePlayerController.CameraFade.IsFadeRunning)
          this.ShowCoinTossScreen();
        else
          PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnCameraFadeComplete += new System.Action(this.ShowCoinTossScreen);
        OnFieldCanvas.Instance.Init();
        PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 180f, 0.0f)));
      }
      this._finishedLoadingStadium = true;
      this.CheckIfPrereqsAreLoaded();
    }

    private void CheckIfPrereqsAreLoaded()
    {
      if (!this._finishedLoadingStadium || !this._finishedOnEnter || AppState.GameMode != EGameMode.k2MD && AppState.GameMode != EGameMode.kHeroMoment)
        return;
      MatchManager.instance.CallStart();
    }

    protected override void OnExitState()
    {
      UIDispatch.HideAll();
      GameplayUI.Hide();
      VRState.LocomotionEnabled.SetValue(false);
      VRState.HelmetEnabled.SetValue(false);
      WorldState.CrowdEnabled.SetValue(false);
      AppState.GameInfoUI.SetValue(false);
      this._linksHandler.Clear();
      AppSounds.StopMusic.Trigger();
      AppSounds.StopSfx(ESfxTypes.kTunnel);
      ScriptableSingleton<VRSettings>.Instance.HelmetActive.OnValueChanged -= new Action<bool>(this.HandleHelmetChanged);
      Globals.GameOver.Value = false;
      this._finishedOnEnter = false;
      this._finishedLoadingStadium = false;
      this._routineHandle.Stop();
      if (!((UnityEngine.Object) this._throwManager != (UnityEngine.Object) null))
        return;
      this._throwManager.Clear();
      this._throwManager.ForceAutoAim = false;
    }

    protected override void UpdateGroupPresence()
    {
    }

    private void UpdateGroupPresence_SeasonMode() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_SeasonMode);

    private void UpdateGroupPresence_ExhibitionMode() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_ExhibitionMode);

    private void UpdateGroupPresence_PracticeMode() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_PracticeMode);

    private void ShowCoinTossScreen()
    {
      UIDispatch.FrontScreen.DisplayView(EScreens.kCoinToss);
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnCameraFadeComplete -= new System.Action(this.ShowCoinTossScreen);
    }
  }
}
