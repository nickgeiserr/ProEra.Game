// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.AgilityGameState
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
using Vars;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/AgilityGameState")]
  public class AgilityGameState : GameState
  {
    [SerializeField]
    private GameplayDataStore _gameplayData;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private ThrowManager _throwManager;
    private AgilityChallenge _currentLevel;
    private readonly RoutineHandle _retryRoutine = new RoutineHandle();

    public override EAppState Id => EAppState.kAgilityGame;

    public event Action<AgilityChallenge> OnTrainingStarted;

    public event System.Action OnExitTraining;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      WorldState.CrowdEnabled.SetValue(true);
      AppState.GameInfoUI.SetValue(true);
      VRState.HelmetEnabled.SetValue(true);
      VRState.LocomotionEnabled.SetValue(true);
      AppEvents.Retry.OnTrigger += new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger += new System.Action(this.HandleContinue);
      AppSounds.PlayStinger(EStingerType.kStinger4);
      this._currentLevel = this._gameplayData.GetAgilityChallenge(AppState.LevelId);
      if (this._currentLevel == null)
        return;
      Action<AgilityChallenge> onTrainingStarted = this.OnTrainingStarted;
      if (onTrainingStarted == null)
        return;
      onTrainingStarted(this._currentLevel);
    }

    protected override void OnExitState()
    {
      WorldState.CrowdEnabled.SetValue(false);
      AppState.GameInfoUI.SetValue(false);
      VRState.HelmetEnabled.SetValue(false);
      VRState.LocomotionEnabled.SetValue(false);
      AppEvents.Retry.OnTrigger -= new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger -= new System.Action(this.HandleContinue);
      PersistentSingleton<BallsContainerManager>.Instance.ResetPosition();
      if ((UnityEngine.Object) this._throwManager != (UnityEngine.Object) null)
        this._throwManager.Clear();
      System.Action onExitTraining = this.OnExitTraining;
      if (onExitTraining == null)
        return;
      onExitTraining();
    }

    private void HandleContinue()
    {
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
        this._levelsStore.NextLevel();
      AppEvents.LoadMainMenu.Trigger();
    }

    private void HandleRetry() => this._retryRoutine.Run(this.RetryRoutine());

    private IEnumerator RetryRoutine()
    {
      UIDispatch.HideAll();
      yield return (object) GamePlayerController.CameraFade.Fade();
      BallsContainerManager.ClearBalls.Trigger();
      Action<AgilityChallenge> onTrainingStarted = this.OnTrainingStarted;
      if (onTrainingStarted != null)
        onTrainingStarted(this._currentLevel);
      yield return (object) GamePlayerController.CameraFade.Clear();
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinigame_AgilityDrill);
  }
}
