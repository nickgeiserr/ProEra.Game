// Decompiled with JetBrains decompiler
// Type: TB12.AgilityGameFlow
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
  public class AgilityGameFlow : MonoBehaviour
  {
    [SerializeField]
    private AgilityGameState _state;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private AgilityGameScene _scene;
    [SerializeField]
    private PracticeTargetsManager _targetsManager;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    private UniformStore _uniformStore;
    [SerializeField]
    private AvatarGraphics _introAvatar;
    [SerializeField]
    private Transform[] _bucketLocations;
    [SerializeField]
    private int _ballCountPerBucket;
    private const float _playerDistanceToLine = 2f;
    private readonly RoutineHandle _flowRoutine = new RoutineHandle();
    private AgilityChallenge _level;
    private bool _ftuePlayed;

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      BallsContainerManager.IsEnabled.SetValue(true);
      BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
      this._state.OnTrainingStarted += new Action<AgilityChallenge>(this.HandleTrainingStarted);
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
      this._state.OnTrainingStarted -= new Action<AgilityChallenge>(this.HandleTrainingStarted);
      this._state.OnExitTraining -= new System.Action(this.StopGameplay);
      this._throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleThrow);
      this._uniformStore = (UniformStore) null;
    }

    private void HandleTrainingStarted(AgilityChallenge level)
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
      PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(this._bucketLocations, this._ballCountPerBucket);
      BallsContainerManager.CanSpawnBall.SetValue(true);
      this._flowRoutine.Run(this.TrainingFlow(level));
    }

    private IEnumerator TrainingFlow(AgilityChallenge level)
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
        if (this._levelsStore.CurrentLevel.Value == null)
          Debug.LogError((object) "Current level missing, cannot setup qb");
        else
          this._uniformStore.SetNamesAndNumbersVisibility(true);
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
      AgilityGameFlow agilityGameFlow = this;
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
      {
        // ISSUE: reference to a compiler-generated method
        yield return (object) new WaitUntil(new Func<bool>(agilityGameFlow.\u003CTrainingCompleteRoutine\u003Eb__23_0));
      }
      bool levelPassed = (int) agilityGameFlow._store.BallsHitTarget >= agilityGameFlow._level.throwsToWin;
      agilityGameFlow._targetsManager.HideTargets();
      if (agilityGameFlow._level != null & levelPassed)
        AppEvents.ChallengeComplete.Trigger((int) agilityGameFlow._store.Score);
      yield return (object) new WaitForSeconds(1.5f);
      AppSounds.PlayStinger(EStingerType.kStinger2);
      agilityGameFlow._flowRoutine.RunAdditive(FinishSequence.Routine(levelPassed));
      if (levelPassed && (EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
      {
        agilityGameFlow._introAvatar.Appear(1f, 0.0f);
        yield return (object) new WaitForSeconds(3.5f);
        agilityGameFlow._introAvatar.Disappear(1f, 0.0f);
      }
    }
  }
}
