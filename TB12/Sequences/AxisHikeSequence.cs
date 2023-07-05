// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.AxisHikeSequence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System.Collections;
using UnityEngine;

namespace TB12.Sequences
{
  public class AxisHikeSequence
  {
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    public event System.Action OnHikeComplete;

    public bool hikeSuccess { get; private set; }

    public Coroutine RunHikeSequence(BallObject gameBall, HandData hand) => this._routineHandle.Run(this.BallHikeFlightRoutine(gameBall, hand));

    public void Stop()
    {
      this._routineHandle.Stop();
      this.Reset();
    }

    public void Reset() => this.hikeSuccess = false;

    private IEnumerator BallHikeFlightRoutine(BallObject gameBall, HandData hand)
    {
      FlightSettings flightSettings = ScriptableSingleton<ThrowSettings>.Instance.FlightSettings;
      Transform ballTx = gameBall.transform;
      Vector3 startPos = ballTx.position;
      if ((bool) (UnityEngine.Object) gameBall.GetComponent<BallManager>())
        startPos = gameBall.GetComponent<BallManager>().LastCenterPosition;
      gameBall.ResetState();
      gameBall.Release();
      gameBall.inFlight = true;
      Transform transform = PlayerCamera.Camera.transform;
      Vector3 velocity = AutoAim.GetImpulseToHitTarget(hand.position - startPos, flightSettings.HikeFlightTime);
      gameBall.Collider.isTrigger = true;
      float startTime = Time.time;
      float endTime = (float) ((double) startTime + (double) flightSettings.HikeFlightTime + 0.05000000074505806);
      float timeElapsed = 0.0f;
      ballTx.rotation = Quaternion.LookRotation(-velocity);
      gameBall.Graphics.SetHighlight(true);
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      FootballVR.DifficultySetting difficulty = GameSettings.GetDifficulty();
      if ((savedOffPlay.IsUnderCenterPlay() && Game.IsPass && (difficulty.UnderCenterAutoDropBackPass || difficulty.UnderCenterBulletTimePass)) | (savedOffPlay.IsUnderCenterPlay() && Game.IsRun && (difficulty.UnderCenterAutoDropBackRun || difficulty.UnderCenterBulletTimeRun)) | (!savedOffPlay.IsUnderCenterPlay() && Game.IsPass && difficulty.ShotgunBulletTimePass) | (!savedOffPlay.IsUnderCenterPlay() && Game.IsRun && !Game.IsPitchPlay && !Game.IsQBKeeper && difficulty.ShotgunBulletTimeRun))
        GamePlayerController.BulletTime.RunCustomBulletTime(Game.IsRun ? ScriptableSingleton<GameSettings>.Instance.HandoffBulletTimeSpeed : ScriptableSingleton<GameSettings>.Instance.AutoDropBackBulletTimeSpeed);
      while ((double) Time.time < (double) endTime && !gameBall.inHand)
      {
        timeElapsed += Time.deltaTime;
        ballTx.position = startPos + velocity * timeElapsed - (float) (9.8100004196167 * (double) timeElapsed * (double) timeElapsed / 2.0) * Vector3.up;
        double num = (double) Mathf.Sqrt(Mathf.InverseLerp(startTime, endTime, Time.time));
        yield return (object) null;
      }
      System.Action onHikeComplete = this.OnHikeComplete;
      if (onHikeComplete != null)
        onHikeComplete();
      PEGameplayEventManager.RecordHikeCompleteEvent();
      gameBall.Graphics.SetHighlight(false);
      this.hikeSuccess = gameBall.inHand;
      if (!gameBall.inHand)
        Debug.Log((object) "Hike failed!");
    }
  }
}
