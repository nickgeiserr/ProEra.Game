// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.AISimGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using ProEra;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UDB;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/AISimGameState")]
  public class AISimGameState : GameState
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private UniformLogoStore _store;
    [EditorSetting(ESettingType.Debug)]
    private static bool superSimUser;
    [EditorSetting(ESettingType.Debug)]
    private static bool superSimComp;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private readonly RoutineHandle _transRoutineHandle = new RoutineHandle();

    public override EAppState Id => EAppState.kAISimGame;

    public virtual EScreens PlaybookToDisplay => EScreens.kSelectPlay;

    public override bool showLoadingScreen => false;

    public override bool showTransition => false;

    public override bool UnloadGameplayScene => true;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      this.OnEnterStateAsync();
    }

    private async System.Threading.Tasks.Task OnEnterStateAsync()
    {
      AISimGameState aiSimGameState = this;
      PersistentSingleton<GamePlayerController>.Instance.gameObject.SetActive(false);
      PlayerAvatar.Instance.gameObject.SetActive(false);
      AppState.GameMode = EGameMode.kAISimGameMode;
      AppState.GameInfoUI.SetValue(true);
      aiSimGameState._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        Globals.GameOver.Link<bool>(new Action<bool>(aiSimGameState.OnGameOver)),
        PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(aiSimGameState.HandleStateTransition))
      });
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.scenarioAISimGame = true;
      PersistentData.SetGameMode(GameMode.Spectate);
      await MatchManager.instance.playersManager.CallAwake();
      MatchManager.instance.CallAwake();
      ScoreClockState.TEMP_InitializeScoreClock.Trigger();
      MatchManager.instance.CallStart();
      SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.CallStart();
      MatchManager.instance.playersManager.CallStart();
      PersistentData.gameType = GameType.QuickMatch;
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.superSimComp = AISimGameState.superSimComp;
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.superSimUser = AISimGameState.superSimUser;
      bool offenseNorth = (double) UnityEngine.Random.value > 0.5;
      EMatchState state = (double) UnityEngine.Random.value > 0.5 ? EMatchState.UserOnDefense : EMatchState.UserOnOffense;
      MatchManager.instance.StartFromCoinFlip(offenseNorth, state);
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(new Vector3(0.0f, 180f, 0.0f)));
      MatchManager.instance.playersManager.CreateTeamPlayers(1);
      MatchManager.instance.playersManager.CreateTeamPlayers(2);
      GamePlayerController.CameraFade.Clear(1f);
      foreach (GameScoreboardUI gameScoreboardUi in UnityEngine.Object.FindObjectsOfType<GameScoreboardUI>())
      {
        gameScoreboardUi.SendMessage("OnDestroy");
        gameScoreboardUi.SendMessage("Awake");
      }
    }

    private void OnGameOver(bool gameOver)
    {
      if (!gameOver)
        return;
      this._transRoutineHandle.Run(this.OnGameOverRoutine());
    }

    private IEnumerator OnGameOverRoutine()
    {
      yield return (object) new WaitForSeconds(3f);
      AppEvents.LoadAISimGameHub.Trigger();
    }

    private void HandleStateTransition(bool inTransition)
    {
    }

    protected override void OnExitState()
    {
      this._linksHandler.Clear();
      Globals.GameOver.Value = false;
      this._routineHandle.Stop();
      this._throwManager.Clear();
      this._throwManager.ForceAutoAim = false;
    }

    private void ShowPlaybookHandler()
    {
    }

    private void StopShowPlayBook()
    {
    }
  }
}
