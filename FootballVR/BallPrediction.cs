// Decompiled with JetBrains decompiler
// Type: FootballVR.BallPrediction
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  public class BallPrediction : MonoBehaviour
  {
    [SerializeField]
    private TrailRenderer _trail;
    [SerializeField]
    private float _timeStep = 0.05f;
    [SerializeField]
    private float _fadeDelay = 0.5f;
    [SerializeField]
    private float _fadeDuration = 0.25f;
    [SerializeField]
    private Color _trailColor;
    private readonly RoutineHandle _trailRoutine = new RoutineHandle();

    public event Action<BallPrediction> OnDone;

    public void ResetState()
    {
      this.OnDone = (Action<BallPrediction>) null;
      this._trail.enabled = false;
      this._trail.emitting = false;
    }

    private void OnDisable() => this._trailRoutine.Stop();

    private IEnumerator PredictionTrailSequence(
      Vector3 shootPointPosition,
      Vector3 impulse,
      float fadeDuration = -1f,
      bool postProcessPoints = false)
    {
      BallPrediction ballPrediction = this;
      float flightTime = AutoAim.GetFlightTime(shootPointPosition, impulse, 0.0f);
      Vector3[] trajectory = AutoAim.GetTrajectory(shootPointPosition, impulse, ballPrediction._timeStep, flightTime);
      if (trajectory != null && trajectory.Length != 0)
      {
        ballPrediction.transform.position = trajectory[trajectory.Length - 1];
        ballPrediction._trail.enabled = true;
        ballPrediction._trail.Clear();
        ballPrediction._trail.emitting = true;
        ballPrediction._trail.AddPositions(trajectory);
        if ((double) fadeDuration < 0.0)
          fadeDuration = ballPrediction._fadeDuration;
        float startTime = Time.time;
        float endTime = Time.time + fadeDuration;
        Color color = ballPrediction._trailColor;
        while ((double) Time.time < (double) endTime)
        {
          color.a = Mathf.InverseLerp(startTime, endTime, Time.time);
          ballPrediction.SetColor(color);
          yield return (object) null;
        }
        yield return (object) new WaitForSeconds(ballPrediction._fadeDelay);
        startTime = Time.time;
        endTime = Time.time + ballPrediction._fadeDuration;
        while ((double) Time.time < (double) endTime)
        {
          color.a = 1f - Mathf.InverseLerp(startTime, endTime, Time.time);
          ballPrediction.SetColor(color);
          yield return (object) null;
        }
        Action<BallPrediction> onDone = ballPrediction.OnDone;
        if (onDone != null)
          onDone(ballPrediction);
        ballPrediction.ResetState();
      }
    }

    private void SetColor(Color color)
    {
      this._trail.startColor = color;
      this._trail.endColor = color;
    }

    public void ShowTrail(Vector3 shootPointPosition, Vector3 impulse) => this._trailRoutine.Run(this.PredictionTrailSequence(shootPointPosition, impulse));

    public void SetWidth(float width) => this._trail.widthMultiplier = width;
  }
}
