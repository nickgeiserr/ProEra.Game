// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.BallHikeSequence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections;
using TB12.UI;
using UnityEngine;

namespace TB12.Sequences
{
  public class BallHikeSequence
  {
    private readonly ThrowManager _throwManager;
    private readonly PlaybackInfo _playbackInfo;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private BallObject _gameBall;

    private bool ballHiked { get; set; }

    public bool hikeSuccess { get; private set; }

    public BallHikeSequence(PlaybackInfo playbackInfo, ThrowManager throwManager)
    {
      this._throwManager = throwManager;
      this._playbackInfo = playbackInfo;
    }

    public Coroutine RunHikeSequence(FootballVR.Avatar center, BallObject gameBall, bool hideCenterAfterHike = false) => this._routineHandle.Run(this.HikeRoutine(center, gameBall, hideCenterAfterHike));

    public void Stop()
    {
      this._routineHandle.Stop();
      this.Reset();
      Time.timeScale = (float) GameSettings.TimeScale;
    }

    public void Reset()
    {
      this.ballHiked = false;
      this.hikeSuccess = false;
      Time.timeScale = (float) GameSettings.TimeScale;
    }

    private IEnumerator HikeRoutine(FootballVR.Avatar center, BallObject gameBall, bool hideCenterAfterHike)
    {
      BallHikeSequence ballHikeSequence = this;
      ballHikeSequence._gameBall = gameBall;
      ballHikeSequence._gameBall.gameObject.SetActive(true);
      ballHikeSequence._gameBall.Pick();
      center.behaviourController.GrabBall(ballHikeSequence._gameBall.transform);
      UIDispatch.FrontScreen.DisplayView(EScreens.kHikeIntro);
      yield return (object) ballHikeSequence._routineHandle.RunAdditive(ControllerInput.WaitDoubleTrigger());
      UIDispatch.HideAll();
      center.behaviourController.HikeBall(new EventData()
      {
        time = ballHikeSequence._playbackInfo.PlayTime + 0.3f,
        OnEventKeyMoment = new Action<object>(ballHikeSequence.HandleBallHiked)
      });
      ballHikeSequence._playbackInfo.StartPlayback(reset: false);
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(ballHikeSequence.\u003CHikeRoutine\u003Eb__16_0));
      if (hideCenterAfterHike)
        center.Disappear(delay: 0.2f);
      yield return (object) ballHikeSequence._routineHandle.RunAdditive(ballHikeSequence.BallHikeFlightRoutine());
    }

    private void HandleBallHiked(object data)
    {
      if (this.ballHiked)
        Debug.LogError((object) "Ball was already hiked. Did something not reset properly?");
      this.ballHiked = true;
    }

    private IEnumerator BallHikeFlightRoutine()
    {
      FlightSettings flightSettings = this._throwManager.Settings.FlightSettings;
      this._gameBall.ResetState();
      this._gameBall.Release();
      this._gameBall.inFlight = true;
      Transform transform = PlayerCamera.Camera.transform;
      Transform ballTx = this._gameBall.transform;
      Vector3 startPos = ballTx.position;
      Vector3 velocity = AutoAim.GetImpulseToHitTarget(transform.position.SetY(1.4f) - startPos, flightSettings.HikeFlightTime);
      this._gameBall.GetComponent<Collider>().isTrigger = true;
      float startTime = Time.time;
      float endTime = (float) ((double) startTime + (double) flightSettings.HikeFlightTime + 0.05000000074505806);
      float timeElapsed = 0.0f;
      ballTx.rotation = Quaternion.LookRotation(-velocity);
      this._gameBall.Graphics.SetHighlight(true);
      while ((double) Time.time < (double) endTime && !this._gameBall.inHand)
      {
        timeElapsed += Time.deltaTime;
        ballTx.position = startPos + velocity * timeElapsed - (float) (9.8100004196167 * (double) timeElapsed * (double) timeElapsed / 2.0) * Vector3.up;
        Time.timeScale = (float) ((1.0 - (double) Mathf.Sqrt(Mathf.InverseLerp(startTime, endTime, Time.time))) * 0.60000002384185791 + 0.31999999284744263);
        yield return (object) null;
      }
      this._gameBall.Graphics.SetHighlight(false);
      Time.timeScale = (float) GameSettings.TimeScale;
      this.hikeSuccess = this._gameBall.inHand;
      if (!this._gameBall.inHand)
        Debug.Log((object) "Hike failed!");
    }
  }
}
