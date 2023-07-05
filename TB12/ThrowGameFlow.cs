// Decompiled with JetBrains decompiler
// Type: TB12.ThrowGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
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
  public class ThrowGameFlow : MonoBehaviour
  {
    [SerializeField]
    private ThrowGameState _state;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private ThrowGameScene _scene;
    [SerializeField]
    private PracticeTargetsManager _targetsManager;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private AvatarGraphics _introAvatar;
    [SerializeField]
    private Transform _bucketTxR;
    [SerializeField]
    private Transform _bucketTxL;
    private const float _playerDistanceToLine = 2f;
    private readonly RoutineHandle _flowRoutine = new RoutineHandle();
    private ThrowLevel _level;
    private bool _ftuePlayed;
    private UniformStore _uniformStore;

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      BallsContainerManager.IsEnabled.SetValue(true);
      BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
      this._state.OnTrainingStarted += new Action<ThrowLevel>(this.HandleTrainingStarted);
      this._state.OnExitTraining += new System.Action(this.StopGameplay);
      this._throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleThrow);
      this._uniformStore.SetNamesAndNumbersVisibility(true);
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode != EAppMode.Activation)
        return;
      this._introAvatar.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
      this.StopGameplay();
      BallsContainerManager.IsEnabled.SetValue(false);
      BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
      this._state.OnTrainingStarted -= new Action<ThrowLevel>(this.HandleTrainingStarted);
      this._state.OnExitTraining -= new System.Action(this.StopGameplay);
      this._throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleThrow);
      this._uniformStore = (UniformStore) null;
    }

    private void HandleTrainingStarted(ThrowLevel level)
    {
      this.StopGameplay();
      this._store.ResetStore();
      this._throwManager.HandsDataModel.ResetHandsState();
      this._playbackInfo.Setup(0.0f, 3000f);
      this._playbackInfo.StartPlayback();
      this._level = level;
      this._store.AttemptsRemaining.SetValue(level.totalThrows);
      Vector3 gamePos = Utilities.YardsToGamePos(new Vector2(26.675f, (float) this._level.yardLine - 2f));
      this.transform.position = gamePos;
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(gamePos, Quaternion.identity);
      if (AppState.AppMode.Value == EAppMode.Game)
        PersistentSingleton<GamePlayerController>.Instance.SetMovementLimits(maxYardLine: this._level.yardLine);
      PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
      {
        level.bucketOnLeft ? this._bucketTxL : this._bucketTxR
      }, this._level.totalThrows);
      BallsContainerManager.CanSpawnBall.SetValue(true);
      this._flowRoutine.Run(this.TrainingFlow(level));
    }

    private IEnumerator TrainingFlow(ThrowLevel level)
    {
      this._targetsManager.LoadTargets(level.sceneId);
      UIDispatch.FrontScreen.DisplayView((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Activation ? EScreens.kPassIntro : EScreens.kIntroduction);
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game && !this._ftuePlayed)
      {
        this._introAvatar.Appear(1f, 0.0f);
        yield return (object) new WaitForSeconds(4f);
        this._introAvatar.Disappear(1f, 0.0f);
        this._ftuePlayed = true;
        yield return (object) new WaitForSeconds(1f);
      }
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
      {
        GameLevel gameLevel = this._levelsStore.CurrentLevel.Value;
        if (gameLevel == null)
        {
          Debug.LogError((object) "Current level missing, cannot setup qb");
        }
        else
        {
          this._scene.LoadOpponents(gameLevel.qb, this._level);
          this._uniformStore.SetNamesAndNumbersVisibility(true);
        }
      }
    }

    private void HandleBallSpawned(BallObject obj) => UIDispatch.HideAll();

    private void StopGameplay()
    {
      this._scene.CleanupScene();
      this._targetsManager.HideTargets();
      this._flowRoutine.Stop();
      FinishSequence.Stop();
      this._playbackInfo.StopPlayback();
      if (UnityState.quitting)
        return;
      PersistentSingleton<GamePlayerController>.Instance.SetMovementLimits();
      this._introAvatar.Disappear(1f, 0.0f);
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
      if ((int) this._store.BallsThrown < this._level.totalThrows)
        return;
      BallsContainerManager.CanSpawnBall.SetValue(false);
      this._store.Locked = true;
      this._flowRoutine.Run(this.TrainingCompleteRoutine());
    }

    private IEnumerator TrainingCompleteRoutine()
    {
      ThrowGameFlow throwGameFlow = this;
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
      {
        // ISSUE: reference to a compiler-generated method
        yield return (object) new WaitUntil(new Func<bool>(throwGameFlow.\u003CTrainingCompleteRoutine\u003Eb__23_0));
      }
      bool levelPassed = (int) throwGameFlow._store.BallsHitTarget >= throwGameFlow._level.throwsToWin;
      throwGameFlow._targetsManager.HideTargets();
      if (throwGameFlow._level != null & levelPassed)
        AppEvents.ChallengeComplete.Trigger((int) throwGameFlow._store.Score);
      yield return (object) new WaitForSeconds(1.5f);
      AppSounds.PlayStinger(EStingerType.kStinger2);
      throwGameFlow._flowRoutine.RunAdditive(FinishSequence.Routine(levelPassed));
      if (levelPassed && (EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
      {
        throwGameFlow._introAvatar.Appear(1f, 0.0f);
        yield return (object) new WaitForSeconds(3.5f);
        throwGameFlow._introAvatar.Disappear(1f, 0.0f);
      }
    }
  }
}
