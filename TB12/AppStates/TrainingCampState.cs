// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.TrainingCampState
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
  [CreateAssetMenu(menuName = "TB12/States/TrainingCampState")]
  public class TrainingCampState : GameState
  {
    [SerializeField]
    private GameplayDataStore _gameplayData;
    [SerializeField]
    private TrainingCampLevels _levelsStore;
    [SerializeField]
    private ThrowManager _throwManager;
    private int _gameMode = -1;
    private int _currentLevel = -1;
    private TB12.GameplayData.TrainingCampLevel _trainingCampLevel;
    private readonly RoutineHandle _retryRoutine = new RoutineHandle();

    public override EAppState Id => EAppState.kTrainingCamp;

    public event Action<TB12.GameplayData.TrainingCampLevel> OnTrainingStarted;

    public event System.Action OnExitTraining;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      WorldState.CrowdEnabled.SetValue(true);
      AppState.GameInfoUI.SetValue(true);
      VRState.HelmetEnabled.SetValue(true);
      VRState.LocomotionEnabled.SetValue(true);
      VRState.HandsVisible.SetValue(true);
      AppState.GameMode = EGameMode.kTrainingCamp;
      AppState.Mode.SetValue(EMode.kSolo);
      AppEvents.Retry.OnTrigger += new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger += new System.Action(this.HandleContinue);
      AppSounds.PlayStinger(EStingerType.kStinger4);
      this._gameMode = -1;
      this._currentLevel = -1;
      this._trainingCampLevel = this._levelsStore.GetLevel(this._gameMode, this._currentLevel);
      if (this._trainingCampLevel == null)
      {
        this._gameMode = PlayerPrefs.GetInt("TCLD_GameMode");
        this._currentLevel = PlayerPrefs.GetInt("TCLD_Level");
        this._trainingCampLevel = this._levelsStore.GetLevel(this._gameMode, this._currentLevel);
        if (this._trainingCampLevel == null)
          return;
      }
      Action<TB12.GameplayData.TrainingCampLevel> onTrainingStarted = this.OnTrainingStarted;
      if (onTrainingStarted == null)
        return;
      onTrainingStarted(this._trainingCampLevel);
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

    private void HandleContinue() => AppEvents.LoadMainMenu.Trigger();

    private void HandleRetry() => this._retryRoutine.Run(this.RetryRoutine());

    private IEnumerator RetryRoutine()
    {
      UIDispatch.HideAll();
      yield return (object) GamePlayerController.CameraFade.Fade();
      BallsContainerManager.ClearBalls.Trigger();
      Action<TB12.GameplayData.TrainingCampLevel> onTrainingStarted = this.OnTrainingStarted;
      if (onTrainingStarted != null)
        onTrainingStarted(this._trainingCampLevel);
      yield return (object) GamePlayerController.CameraFade.Clear();
    }

    public int CurrentLevel => this._currentLevel;
  }
}
