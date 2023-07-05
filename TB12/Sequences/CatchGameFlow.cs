// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.CatchGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.Sequences
{
  public class CatchGameFlow : MonoBehaviour
  {
    [SerializeField]
    private CatchGameState _catchingGameState;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private CatchGameScene _scene;
    [SerializeField]
    private HandsDataModel _handsData;
    private const float frenzyDuration = 3f;
    private const float frenzyBallsPerSecond = 24f;
    private const float throwIntervalDecreaseRate = 0.3f;
    private readonly Vector3 catchLocation = new Vector3(0.0f, -0.25f, 0.2f);
    private readonly Vector3 catchLocationRadius = new Vector3(0.35f, 0.3f, 0.3f);
    private CatchChallenge _level;
    private bool _frenzyMode;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private Transform _camTx;
    private readonly List<IBallThrower> _readyThrowers = new List<IBallThrower>();

    private void Awake()
    {
      this._scene.OnBallThrown += new Action<BallObject>(this.ProcessThrownBall);
      this._catchingGameState.OnGameplayStart += new Action<CatchChallenge>(this.StartTraining);
      this._catchingGameState.OnStateExit += new System.Action(this.StopFlow);
      this._camTx = PlayerCamera.Camera.transform;
    }

    private void OnDestroy()
    {
      this.StopFlow();
      this._scene.OnBallThrown -= new Action<BallObject>(this.ProcessThrownBall);
      this._catchingGameState.OnGameplayStart -= new Action<CatchChallenge>(this.StartTraining);
      this._catchingGameState.OnStateExit -= new System.Action(this.StopFlow);
    }

    private void StartTraining(CatchChallenge level)
    {
      this.StopFlow();
      this._level = level;
      this._store.ResetStore();
      this._scene.InitializeProfile(level);
      UIDispatch.FrontScreen.DisplayView(EScreens.kIntroduction);
      this._routineHandle.Run(this.CatchGameplayRoutine());
    }

    private void StopFlow()
    {
      this._routineHandle.Stop();
      FinishSequence.Stop();
      this._handsData.ResetHandsState();
      this._frenzyMode = false;
    }

    private void ProcessThrownBall(BallObject ballObject)
    {
      if (!this._frenzyMode)
      {
        ++this._store.BallsEmitted;
        --this._store.AttemptsRemaining;
      }
      this._routineHandle.RunAdditive(this.ProcessBallFlightRoutine(ballObject));
    }

    private IEnumerator CatchGameplayRoutine()
    {
      this._store.AttemptsRemaining.Value = this._level.ballCount;
      yield return (object) this._routineHandle.RunAdditive(ControllerInput.WaitDoubleTrigger());
      AppState.GameInfoUI.SetValue(true);
      UIDispatch.HideAll();
      yield return (object) new WaitForSeconds(1f);
      GameplayUI.ShowText("Ready?");
      yield return (object) new WaitForSeconds(1f);
      GameplayUI.ShowText("GO!");
      yield return (object) new WaitForSeconds(1f);
      float throwInterval = this._level.throwInterval;
      float flightTime = this._level.ballTravelTime;
      Vector3 radius = this.catchLocationRadius;
      while (this._store.BallsEmitted < this._level.ballCount)
      {
        if (this._scene.BallThrowers.Count == 0)
        {
          Debug.LogError((object) "Cannot throw balls: no throwers registered");
          yield return (object) new WaitForSeconds(throwInterval);
        }
        else
        {
          if (this._store.BallsEmitted >= 3)
          {
            radius = this.catchLocationRadius;
            if ((double) UnityEngine.Random.value > 0.75)
              radius *= this._level.radiusIncrease;
          }
          Vector3 zero = Vector3.zero;
          for (int index = 0; index < 3; ++index)
            zero[index] = Mathf.Pow(UnityEngine.Random.value, 0.3f) * radius[index] * Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
          Vector3 position = this._camTx.position;
          Vector3 targetPos = this._camTx.TransformPoint(this.catchLocation + zero);
          IBallThrower orderedBallThrower = this.GetOrderedBallThrower();
          if (orderedBallThrower == null)
          {
            yield return (object) new WaitForSeconds(throwInterval);
          }
          else
          {
            this._scene.HideAllHighlights();
            orderedBallThrower.SetHighlight(true);
            if ((double) Vector3.Dot(this._camTx.forward, (orderedBallThrower.position - position).normalized) < 0.44999998807907104)
              GameplayUI.PointTo(orderedBallThrower.transform, "Incoming ball");
            orderedBallThrower.ThrowToSpot(targetPos, flightTime, this._level.throwDelay);
            yield return (object) new WaitForSeconds(throwInterval);
            if (this._store.BallsEmitted % 3 == 0)
              throwInterval -= 0.3f;
            if (this._store.BallsEmitted == 3)
              flightTime = this._level.ballTravelTime2;
          }
        }
      }
      this._scene.HideAllHighlights();
      GameplayUI.Hide();
      yield return (object) new WaitForSeconds(1f);
      GameplayUI.ShowText("Frenzy Mode!");
      yield return (object) new WaitForSeconds(1.5f);
      this._frenzyMode = true;
      this._scene.ShowAllHighlights();
      float endTime = (float) ((double) Time.time + 3.0 + 0.05000000074505806);
      float ballCharge = 0.0f;
      IBallThrower prevThrower = (IBallThrower) null;
      while ((double) Time.time < (double) endTime)
      {
        ballCharge += 24f * Time.deltaTime;
        if ((double) ballCharge > 1.0)
        {
          --ballCharge;
          Vector3 zero = Vector3.zero;
          for (int index = 0; index < 3; ++index)
            zero[index] = Mathf.Pow(UnityEngine.Random.value, 0.3f) * radius[index] * Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
          Vector3 targetPos = this._camTx.TransformPoint(this.catchLocation + zero);
          IBallThrower randomBallThrower;
          do
          {
            randomBallThrower = this.GetRandomBallThrower();
          }
          while (prevThrower == randomBallThrower);
          prevThrower = randomBallThrower;
          randomBallThrower?.ThrowToSpot(targetPos, flightTime, 0.0f);
        }
        yield return (object) null;
      }
      yield return (object) new WaitForSeconds(1f);
      this._scene.HideAllHighlights();
      this._store.Locked = true;
      bool win = this._store.BallsCaught >= this._level.ballsToWin;
      if (win)
        AppEvents.ChallengeComplete.Trigger((int) this._store.Score);
      AppSounds.PlayStinger(EStingerType.kStinger2);
      this._routineHandle.RunAdditive(FinishSequence.Routine(win));
    }

    private IBallThrower GetRandomBallThrower()
    {
      this._readyThrowers.Clear();
      for (int index = 0; index < this._scene.BallThrowers.Count; ++index)
      {
        IBallThrower ballThrower = this._scene.BallThrowers[index];
        if (ballThrower.isReady)
          this._readyThrowers.Add(ballThrower);
      }
      return this._readyThrowers.Count == 0 ? (IBallThrower) null : this._readyThrowers[UnityEngine.Random.Range(0, this._readyThrowers.Count)];
    }

    private IBallThrower GetOrderedBallThrower()
    {
      int emittersCount = this._level.emittersCount;
      if (this._level.fromBehind)
        emittersCount *= 2;
      return this._scene.BallThrowers[this._store.BallsEmitted % emittersCount];
    }

    private IEnumerator ProcessBallFlightRoutine(BallObject ballObject)
    {
      ballObject.Graphics.SetHighlight(true);
      Transform ballTx = ballObject.transform;
      while (!ballObject.inHand && (double) ballTx.position.y > 0.20000000298023224)
        yield return (object) null;
      if (ballObject.inHand)
      {
        int num = ballObject.twoHandedGrab ? 100 : 200;
        Vector3 pos = ballObject.transform.position + this._camTx.forward * 0.25f;
        if ((double) pos.y < (double) this._camTx.position.y - 0.10000000149011612)
          pos.y += 0.15f;
        int score = (int) ((double) num * (double) AppState.Difficulty.CatchPointsMultiplier);
        this._scene.DisplayScore(pos, (float) score);
        ++this._store.ComboModifier;
        this._store.AccumulateScore(score * this._store.ComboModifier);
        if (this._store.ComboModifier > 1)
          this._scene.DisplayCombo(this._store.ComboModifier);
        if (!this._frenzyMode)
          ++this._store.BallsCaught;
      }
      else
        this._store.ComboModifier = 0;
      ballObject.Graphics.SetHighlight(false);
    }
  }
}
