// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.QbHikeSequence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace TB12.Sequences
{
  public class QbHikeSequence
  {
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private BallObject _transitionBall;
    private const float flightTime = 0.4f;

    private bool ballHiked { get; set; }

    public Coroutine RunHikeSequence(FootballVR.Avatar center, FootballVR.Avatar qb, BallObject gameBall) => this._routineHandle.Run(this.HikeRoutine(center, qb, gameBall));

    public void Stop()
    {
      this._routineHandle.Stop();
      this.Reset();
    }

    public void Reset() => this.ballHiked = false;

    private IEnumerator HikeRoutine(FootballVR.Avatar center, FootballVR.Avatar qb, BallObject transitionBall)
    {
      QbHikeSequence qbHikeSequence = this;
      qbHikeSequence._transitionBall = transitionBall;
      center.behaviourController.HikeBall(new EventData()
      {
        time = 0.3f,
        OnEventKeyMoment = new Action<object>(qbHikeSequence.HandleBallHiked)
      });
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(qbHikeSequence.\u003CHikeRoutine\u003Eb__10_0));
      yield return (object) qbHikeSequence._routineHandle.RunAdditive(qbHikeSequence.BallHikeFlightRoutine(qb));
    }

    private void HandleBallHiked(object data)
    {
      if (data != null)
        this._transitionBall.transform.position = ((Transform) data).position;
      else
        Debug.LogError((object) "QbHikeSequence: data is null");
      if (this.ballHiked)
        Debug.LogError((object) "Ball was already hiked. Did something not reset properly?");
      this.ballHiked = true;
      this._transitionBall.gameObject.SetActive(true);
      this._transitionBall.ResetState();
    }

    private IEnumerator BallHikeFlightRoutine(FootballVR.Avatar qb)
    {
      this._transitionBall.ResetState();
      this._transitionBall.inFlight = true;
      Transform ballTx = this._transitionBall.transform;
      Vector3 startPos = ballTx.position;
      Vector3 velocity = AutoAim.GetImpulseToHitTarget(qb.transform.position.SetY(1.4f) - startPos, 0.4f);
      this._transitionBall.GetComponent<Collider>().isTrigger = true;
      float endTime = Time.time + 0.4f;
      float timeElapsed = 0.0f;
      ballTx.rotation = Quaternion.LookRotation(-velocity);
      while ((double) Time.time < (double) endTime)
      {
        timeElapsed += Time.deltaTime;
        ballTx.position = startPos + velocity * timeElapsed - (float) (9.8100004196167 * (double) timeElapsed * (double) timeElapsed / 2.0) * Vector3.up;
        yield return (object) null;
      }
      this._transitionBall.Pick();
      qb.behaviourController.GrabBall(ballTx);
    }
  }
}
