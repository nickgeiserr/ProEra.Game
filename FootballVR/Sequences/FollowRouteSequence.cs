// Decompiled with JetBrains decompiler
// Type: FootballVR.Sequences.FollowRouteSequence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using TB12;
using UnityEngine;

namespace FootballVR.Sequences
{
  public class FollowRouteSequence
  {
    private RouteObject _route;
    private int _currentPointIndex;
    private float _progress;
    private Transform _userTx;
    private Vector3[] routePoints;
    private readonly FloatCache _progressCache = new FloatCache(30);
    private readonly RoutineHandle _sequenceRoutine = new RoutineHandle();
    private const float errorTolerance = 0.25f;
    private float _completionOffset;

    public Vector3 EndPosition { get; private set; }

    public float progressPerSecond => this._progressCache.AverageValue();

    public bool done { get; private set; }

    public float progress => this._progress;

    public Coroutine RunSequence(RouteObject route, float completionOffset)
    {
      this._completionOffset = completionOffset;
      this._route = route;
      this._userTx = PersistentSingleton<PlayerCamera>.Instance.transform;
      this.Stop();
      return this._sequenceRoutine.Run(this.SequenceRoutine());
    }

    public void Stop()
    {
      if ((Object) this._route != (Object) null)
        this._route.SetProgress(0.0f);
      this._progressCache.Clear();
      this._progress = 0.0f;
      this._currentPointIndex = 0;
      this._sequenceRoutine.Stop();
      this.done = false;
    }

    private IEnumerator SequenceRoutine()
    {
      this.routePoints = this._route.WorldPoints;
      int lastPointIndex = this.routePoints.Length - 1;
      float segmentLength = (this.routePoints[1] - this.routePoints[0]).magnitude;
      float completedPathLength = 0.0f;
      this.EndPosition = this.routePoints[this.routePoints.Length - 1];
      while (this._currentPointIndex < lastPointIndex)
      {
        Vector3 vector3_1 = this._userTx.position.SetY(0.0f);
        if ((double) (this.EndPosition - vector3_1).magnitude >= (double) this._completionOffset)
        {
          Vector3 routePoint = this.routePoints[this._currentPointIndex + 1];
          float num1 = FollowRouteSequence.InverseLerpVector3(this.routePoints[this._currentPointIndex], routePoint, vector3_1);
          float num2 = (completedPathLength + num1 * segmentLength) / this._route.Length;
          this._progressCache.PushValue((num2 - this._progress) / Time.deltaTime);
          this._progress = num2;
          this._route.SetProgress(this._progress);
          Vector3 vector3_2 = vector3_1 - routePoint;
          if ((double) vector3_2.magnitude < 0.25 || (double) num1 > 0.98000001907348633)
          {
            ++this._currentPointIndex;
            completedPathLength += segmentLength;
            double num3;
            if (this._currentPointIndex >= lastPointIndex)
            {
              num3 = 0.0;
            }
            else
            {
              vector3_2 = this.routePoints[this._currentPointIndex + 1] - this.routePoints[this._currentPointIndex];
              num3 = (double) vector3_2.magnitude;
            }
            segmentLength = (float) num3;
          }
          else
            yield return (object) null;
        }
        else
          break;
      }
      this._route.MarkComplete();
      this._route.SetProgress(1f);
      this.done = true;
    }

    private static float InverseLerpVector3(Vector3 a, Vector3 b, Vector3 value)
    {
      Vector3 vector3 = b - a;
      return Mathf.Clamp01(Vector3.Dot(value - a, vector3) / Vector3.Dot(vector3, vector3));
    }

    public void SetEndPos(Vector3 pos) => this.EndPosition = pos;
  }
}
