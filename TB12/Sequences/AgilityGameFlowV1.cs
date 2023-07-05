// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.AgilityGameFlowV1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.Sequences
{
  public class AgilityGameFlowV1 : MonoBehaviour
  {
    [SerializeField]
    private AgilityGameStateV1 _state;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private AgilityGameSceneV1 _scene;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    private BallHikeSequence _hikeSequence;
    private readonly RoutineHandle _gameFlowRoutine = new RoutineHandle();
    private readonly RoutineHandle _gameWaveRoutine = new RoutineHandle();
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private CollisionSettings _collisionSettings => ScriptableSingleton<CollisionSettings>.Instance;

    private void Awake()
    {
      this._scene.OnAttackerMissed += new System.Action(this.HandleDodge);
      this._state.OnTrainingStarted += new Action<AgilityChallengeV1>(this.HandleTrainingStarted);
      this._state.OnExitTraining += new System.Action(this.StopGameplay);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VREvents.UserCollision.Link<Collider>(new Action<Collider>(this.HandleCollision)),
        VREvents.ThrowResult.Link<bool, float>(new Action<bool, float>(this.HandleThrowResult))
      });
      this._hikeSequence = new BallHikeSequence(this._playbackInfo, this._throwManager);
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      this._scene.OnAttackerMissed -= new System.Action(this.HandleDodge);
      this._state.OnTrainingStarted -= new Action<AgilityChallengeV1>(this.HandleTrainingStarted);
      this._state.OnExitTraining -= new System.Action(this.StopGameplay);
    }

    private void HandleThrowResult(bool targetHit, float distance)
    {
      if (targetHit)
        return;
      this._store.ComboModifier = 0;
    }

    private void HandleTrainingStarted(AgilityChallengeV1 agilityChallenge)
    {
      this.StopGameplay();
      PlayerPositionTracker.TrackHeadPosition.SetValue(true);
      GamePlayerController.CameraFade.Clear(0.0f, fromBlack: false);
      this._gameFlowRoutine.Run(this.GameFlowRoutine(agilityChallenge));
    }

    private IEnumerator GameFlowRoutine(AgilityChallengeV1 agilityChallenge)
    {
      AgilityGameFlowV1 agilityGameFlowV1 = this;
      agilityGameFlowV1._store.ResetStore();
      agilityGameFlowV1._store.AttemptsRemaining.SetValue(agilityChallenge.failCountAllowed);
      agilityGameFlowV1._playbackInfo.Setup(0.0f, 300f);
      agilityGameFlowV1._playbackInfo.PlayTime = 0.0f;
      VRState.CollisionEnabled.SetValue((bool) agilityGameFlowV1._collisionSettings.CollisionEnabled);
      int waveCount = agilityChallenge.playersInWaves.Length;
      for (int waveId = 0; waveId < waveCount; ++waveId)
      {
        if (waveCount - waveId == 1)
          GameplayUI.ShowText("Final round");
        agilityGameFlowV1._hikeSequence.Reset();
        agilityGameFlowV1._playbackInfo.Reset();
        agilityGameFlowV1._scene.ResetScene();
        yield return (object) new WaitForSeconds(1.15f);
        BallObject ball = agilityGameFlowV1._scene.GetBall();
        yield return (object) agilityGameFlowV1._hikeSequence.RunHikeSequence(agilityGameFlowV1._scene.CenterPlayer, ball, true);
        if (!agilityGameFlowV1._hikeSequence.hikeSuccess)
        {
          agilityGameFlowV1._scene.ReturnBall(ball);
          yield return (object) agilityGameFlowV1._gameFlowRoutine.RunAdditive(agilityGameFlowV1.HikeFailedSequence());
          agilityGameFlowV1._store.ComboModifier = 0;
          --waveId;
        }
        else
        {
          yield return (object) new WaitForSeconds(0.4f);
          AppSounds.PlaySfx(ESfxTypes.kCatchSuccess);
          int playersInWave = agilityChallenge.playersInWaves[waveId];
          yield return (object) agilityGameFlowV1._gameWaveRoutine.Run(agilityGameFlowV1.GameplayWaveRoutine(agilityChallenge, playersInWave));
          agilityGameFlowV1._throwManager.HandsDataModel.ResetHandsState();
          if ((UnityEngine.Object) ball != (UnityEngine.Object) null && ball.inHand)
            agilityGameFlowV1._store.ComboModifier = 0;
          ball = (BallObject) null;
        }
      }
      GameplayUI.ShowText("Frenzy Mode!");
      yield return (object) new WaitForSeconds(1.5f);
      agilityGameFlowV1._playbackInfo.StartPlayback();
      yield return (object) agilityGameFlowV1._gameWaveRoutine.Run(agilityGameFlowV1.GameplayWaveRoutine(agilityChallenge, agilityChallenge.frenzyPlayerCount, true));
      agilityGameFlowV1.StartCoroutine(agilityGameFlowV1.WinSequence());
    }

    private IEnumerator GameplayWaveRoutine(
      AgilityChallengeV1 agilityChallenge,
      int attackerCount,
      bool frenzyMode = false)
    {
      float maxCollisionTime;
      List<AttackerData> attackerDatas = this._scene.GenerateAttackerDatas(agilityChallenge, attackerCount, frenzyMode, out maxCollisionTime);
      attackerDatas.Sort(new Comparison<AttackerData>(AgilityGameFlowV1.SortAttackersBySpawnTime));
      while (attackerDatas.Count > 0)
      {
        AttackerData attacker = attackerDatas[0];
        float seconds = attacker.spawnTime - this._playbackInfo.PlayTime;
        if ((double) seconds > 0.0)
          yield return (object) new WaitForSeconds(seconds);
        this._scene.InitializeAttacker(attacker.position, attacker.spawnTime, agilityChallenge.attackerSpeed, AppState.Difficulty.LockThreshold);
        attackerDatas.RemoveAt(0);
        attacker = (AttackerData) null;
      }
      float endTime = maxCollisionTime + 2f;
      if (!frenzyMode)
      {
        float seconds = (float) Mathf.CeilToInt((float) ((double) maxCollisionTime - (double) this._playbackInfo.PlayTime + 2.5));
        endTime = (float) ((double) this._playbackInfo.PlayTime + (double) seconds + 0.60000002384185791);
        this._scene.Target.ShowForSeconds(seconds, false);
      }
      yield return (object) new WaitUntil((Func<bool>) (() => (double) this._playbackInfo.PlayTime > (double) maxCollisionTime + 1.0));
      foreach (FootballVR.Avatar allocatedObject in this._scene.Avatars.AllocatedObjects)
        allocatedObject.Disappear(1f, 0.0f);
      yield return (object) new WaitUntil((Func<bool>) (() => (double) this._playbackInfo.PlayTime > (double) endTime));
      this._scene.Avatars.ReturnAllObjects();
      this._playbackInfo.StopPlayback();
    }

    private static int SortAttackersBySpawnTime(AttackerData x, AttackerData y) => (double) y.spawnTime >= (double) x.spawnTime ? -1 : 1;

    private void HandleDodge()
    {
      int score = (int) (100.0 * (double) AppState.Difficulty.DodgePointsMultiplier);
      this._scene.DisplayScore(this._collisionSettings.ScorePosition, (float) score);
      ++this._store.ComboModifier;
      this._store.AccumulateScore(score * this._store.ComboModifier);
      if (this._store.ComboModifier <= 1)
        return;
      this._scene.DisplayCombo(this._store.ComboModifier);
    }

    private void HandleCollision(Collider unused)
    {
      if ((int) --this._store.AttemptsRemaining <= 0)
      {
        this.StopGameplay();
        this._gameFlowRoutine.RunAdditive(this.WinSequence());
      }
      this._store.ComboModifier = 0;
      VREvents.PlayerCollision.Trigger(unused.gameObject);
    }

    private IEnumerator WinSequence()
    {
      this._store.Locked = true;
      this._throwManager.HandsDataModel.ResetHandsState();
      yield return (object) this._gameFlowRoutine.RunAdditive(AppSounds.WinSequence());
      yield return (object) this._gameFlowRoutine.RunAdditive(FinishSequence.Routine());
    }

    private IEnumerator HikeFailedSequence()
    {
      yield return (object) new WaitForSeconds(0.7f);
      yield return (object) this._gameFlowRoutine.RunAdditive(AppSounds.FailSequence());
      yield return (object) new WaitForSeconds(0.15f);
    }

    private void StopGameplay()
    {
      PlayerPositionTracker.TrackHeadPosition.SetValue(false);
      VRState.CollisionEnabled.SetValue(false);
      this._gameFlowRoutine.Stop();
      this._gameWaveRoutine.Stop();
      this._throwManager.HandsDataModel.ResetHandsState();
      this._playbackInfo.StopPlayback();
      FinishSequence.Stop();
      if (!((UnityEngine.Object) this._scene != (UnityEngine.Object) null))
        return;
      this._scene.CleanupScene();
    }
  }
}
