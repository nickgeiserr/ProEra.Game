// Decompiled with JetBrains decompiler
// Type: TB12.TrainingCampFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections;
using TB12.AppStates;
using TB12.GameplayData;
using TB12.Sequences;
using TB12.UI;
using UnityEngine;
using Vars;

namespace TB12
{
  public class TrainingCampFlow : MonoBehaviour
  {
    [SerializeField]
    private TrainingCampState _state;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private TrainingCampLevels _levelsStore;
    [SerializeField]
    private TrainingCampScene _scene;
    [SerializeField]
    private ThrowManager _throwManager;
    private const float _playerDistanceToLine = 2f;
    private readonly RoutineHandle _flowRoutine = new RoutineHandle();
    private TB12.GameplayData.TrainingCampLevel _level;
    private TrainingCampThrowingLevel _throwLevel;
    private GameObject _currentLevelPrefab;
    private bool _timerActive;
    private float _gameTimer;
    private TrainingCampLevelManager _levelManager;

    private void Awake()
    {
      BallsContainerManager.IsEnabled.SetValue(true);
      PersistentSingleton<BallsContainerManager>.Instance.IsNetworked = false;
      BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
      this._state.OnTrainingStarted += new Action<TB12.GameplayData.TrainingCampLevel>(this.HandleTrainingStarted);
      this._state.OnExitTraining += new System.Action(this.StopGameplay);
      this._throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleThrow);
    }

    private void Update()
    {
      if (!this._timerActive)
        return;
      this._gameTimer += Time.deltaTime;
      if (this._level == null || (double) this._gameTimer < (double) this._level.time || this._level.time <= 0)
        return;
      this.GameOver();
    }

    private void OnDestroy()
    {
      this.StopGameplay();
      BallsContainerManager.IsEnabled.SetValue(false);
      BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
      this._state.OnTrainingStarted -= new Action<TB12.GameplayData.TrainingCampLevel>(this.HandleTrainingStarted);
      this._state.OnExitTraining -= new System.Action(this.StopGameplay);
      this._throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleThrow);
    }

    private void HandleTrainingStarted(TB12.GameplayData.TrainingCampLevel level)
    {
      this._store = GameManager.Instance.CurrentGameplayStore;
      this.StopGameplay();
      this._store.ResetStore();
      this._throwManager.HandsDataModel.ResetHandsState();
      this._level = level;
      if ((UnityEngine.Object) this._currentLevelPrefab != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this._currentLevelPrefab);
      this._currentLevelPrefab = UnityEngine.Object.Instantiate<GameObject>(this._level.levelPrefab);
      this._levelManager = this._currentLevelPrefab.GetComponent<TrainingCampLevelManager>();
      if (TrainingCampLevels.IsThrowingTraining(level))
      {
        this._throwLevel = (TrainingCampThrowingLevel) this._level;
        this._store.AttemptsRemaining.SetValue(this._throwLevel.totalBalls);
        PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
        {
          this._levelManager.BallBucket
        }, this._throwLevel.totalBalls);
        BallsContainerManager.CanSpawnBall.SetValue(true);
      }
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._levelManager.PlayerStart.position, Quaternion.identity);
      this.TrainingFlow(level);
    }

    private void TrainingFlow(TB12.GameplayData.TrainingCampLevel level)
    {
      UIDispatch.FrontScreen.DisplayView((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Activation ? EScreens.kPassIntro : EScreens.kIntroduction);
      this._gameTimer = 0.0f;
      this._timerActive = true;
      this._levelManager.StartLevel();
    }

    private void HandleBallSpawned(BallObject obj) => UIDispatch.HideAll();

    private void StopGameplay()
    {
      this._timerActive = false;
      this._flowRoutine.Stop();
      FinishSequence.Stop();
    }

    private void HandleThrow(BallObject ballObject, Vector3 throwVector) => this._store.DoThrow(throwVector.magnitude, false);

    private void HandleThrowResult(bool hitTarget, float distance)
    {
      if (this._store.Locked)
        return;
      --this._store.AttemptsRemaining;
      ++this._store.BallsThrown;
      this._store.UpdateDistance(distance);
      if (!hitTarget)
        this._store.ComboModifier = 0;
      if (this._throwLevel != null && ((int) this._store.BallsThrown < this._throwLevel.totalBalls || this._throwLevel.totalBalls < 0) && !this._level.DidPassTraining(this._store))
        return;
      this.GameOver();
    }

    public void OnTargetTouched()
    {
      if (!this._level.DidPassTraining(this._store))
        return;
      this.GameOver();
    }

    private void GameOver()
    {
      Debug.Log((object) nameof (GameOver));
      this._timerActive = false;
      BallsContainerManager.CanSpawnBall.SetValue(false);
      this._store.Locked = true;
      this._levelManager.GameOver();
      int timeScore;
      int levelScore;
      this.CalculateBonusScores(out timeScore, out levelScore);
      this._store.AddBonusScores(timeScore, levelScore);
      this._flowRoutine.Run(this.TrainingCompleteRoutine());
    }

    private IEnumerator TrainingCompleteRoutine()
    {
      bool levelPassed = this._level.DidPassTraining(this._store);
      Debug.Log((object) ("levelPassed[" + levelPassed.ToString() + "]"));
      yield return (object) new WaitForSeconds(1.5f);
      AppSounds.PlayStinger(EStingerType.kStinger2);
      this._flowRoutine.RunAdditive(FinishSequence.Routine(levelPassed));
    }

    private void CalculateBonusScores(out int timeScore, out int levelScore)
    {
      timeScore = this._level.time > 0 ? Mathf.FloorToInt(((float) this._level.time - this._gameTimer) * (float) this._levelsStore.TimeBonusMultiplier) : 0;
      levelScore = Mathf.FloorToInt((float) (this._store.Score.Value + timeScore) * (this._levelsStore.LevelBonusMultipliers[this._state.CurrentLevel] - 1f));
    }
  }
}
