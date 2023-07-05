// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.CatchGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/CatchingGameState")]
  public class CatchGameState : GameState
  {
    [SerializeField]
    private GameplayDataStore _gameplayData;
    [SerializeField]
    private HandsDataModel _handsDataModel;
    [SerializeField]
    private ThrowManager _throwManager;
    private CatchChallenge _currentLevel;
    private readonly RoutineHandle _retryRoutine = new RoutineHandle();

    public override EAppState Id => EAppState.kCatchGame;

    public event Action<CatchChallenge> OnGameplayStart;

    public event System.Action OnStateExit;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      AppEvents.Retry.OnTrigger += new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger += new System.Action(this.HandleContinue);
      WorldState.CrowdEnabled.SetValue(true);
      VRState.HelmetEnabled.SetValue(true);
      this.LoadChallenge();
    }

    public override void WillExit()
    {
      System.Action onStateExit = this.OnStateExit;
      if (onStateExit == null)
        return;
      onStateExit();
    }

    protected override void OnExitState()
    {
      UIDispatch.HideAll();
      GameplayUI.Hide();
      VRState.HelmetEnabled.SetValue(false);
      WorldState.CrowdEnabled.SetValue(false);
      AppState.GameInfoUI.SetValue(false);
      AppEvents.Retry.OnTrigger -= new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger -= new System.Action(this.HandleContinue);
    }

    private void HandleContinue() => AppEvents.LoadMainMenu.Trigger();

    private void HandleRetry() => this._retryRoutine.Run(this.RetryRoutine());

    private IEnumerator RetryRoutine()
    {
      UIDispatch.HideAll();
      yield return (object) GamePlayerController.CameraFade.Fade();
      this.LoadChallenge();
      yield return (object) GamePlayerController.CameraFade.Clear();
    }

    private void LoadChallenge()
    {
      string levelId = AppState.LevelId;
      Debug.Log((object) ("Running catch training " + levelId));
      CatchChallenge catchingChallenge = this._gameplayData.GetCatchingChallenge(levelId);
      if (catchingChallenge == null)
      {
        Debug.LogError((object) ("Could not find profile for " + levelId));
      }
      else
      {
        this._currentLevel = catchingChallenge;
        Action<CatchChallenge> onGameplayStart = this.OnGameplayStart;
        if (onGameplayStart == null)
          return;
        onGameplayStart(this._currentLevel);
      }
    }
  }
}
