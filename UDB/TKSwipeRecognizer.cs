// Decompiled with JetBrains decompiler
// Type: UDB.TKSwipeRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UDB
{
  public class TKSwipeRecognizer : TKAbstractGestureRecognizer
  {
    public float timeToSwipe = 0.5f;
    public int minimumNumberOfTouches = 1;
    public int maximumNumberOfTouches = 2;
    public bool triggerWhenCriteriaMet = true;
    private float minimumDistance = 2f;
    private List<Vector2> points = new List<Vector2>();
    private float startTime;
    private float idealDistance;
    private float idealDistanceCM;
    private float realDistance;
    private float swipeAngle;
    private Vector2 normalizedSwipe;

    public event Action<TKSwipeRecognizer> gestureRecognizedEvent;

    public float swipeVelocity { get; private set; }

    public TKSwipeDirection completedSwipeDirection { get; private set; }

    public Vector2 startPoint => this.points.FirstOrDefault<Vector2>();

    public Vector2 endPoint => this.points.LastOrDefault<Vector2>();

    public TKSwipeRecognizer()
      : this(2f)
    {
    }

    public TKSwipeRecognizer(float minimumDistanceCm) => this.minimumDistance = minimumDistanceCm;

    internal override void FireRecognizedEvent()
    {
      if (this.gestureRecognizedEvent == null)
        return;
      this.gestureRecognizedEvent(this);
    }

    internal override bool TouchesBegan(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.Possible)
      {
        for (int index = 0; index < touches.Count; ++index)
          this.trackingTouches.Add(touches[index]);
        if (this.trackingTouches.Count >= this.minimumNumberOfTouches && this.trackingTouches.Count <= this.maximumNumberOfTouches)
        {
          this.points.Clear();
          this.points.Add(touches[0].position);
          this.startTime = Time.time;
          this.state = TKGestureRecognizerState.Began;
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state != TKGestureRecognizerState.Began)
        return;
      this.points.Add(touches[0].position);
      if (!this.triggerWhenCriteriaMet || !this.CheckForSwipeCompletion(touches[0]))
        return;
      this.state = TKGestureRecognizerState.Recognized;
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      if (this.state != TKGestureRecognizerState.Began)
        return;
      this.points.Add(touches[0].position);
      if (this.CheckForSwipeCompletion(touches[0]))
        this.state = TKGestureRecognizerState.Recognized;
      else
        this.state = TKGestureRecognizerState.FailedOrEnded;
    }

    public override string ToString() => string.Format("{0}, swipe direction: {1}, swipe velocity: {2}, start point: {3}, end point: {4}", (object) base.ToString(), (object) this.completedSwipeDirection, (object) this.swipeVelocity, (object) this.startPoint, (object) this.endPoint);

    private bool CheckForSwipeCompletion(TKTouch touch)
    {
      if ((double) this.timeToSwipe > 0.0 && (double) Time.time - (double) this.startTime > (double) this.timeToSwipe || this.points.Count < 2)
        return false;
      this.idealDistance = Vector2.Distance(this.startPoint, this.endPoint);
      this.idealDistanceCM = this.idealDistance / TouchKit.screenPixelsPerCm;
      if ((double) this.idealDistanceCM < (double) this.minimumDistance)
        return false;
      this.realDistance = 0.0f;
      for (int index = 1; index < this.points.Count; ++index)
        this.realDistance += Vector2.Distance(this.points[index], this.points[index - 1]);
      if ((double) this.realDistance > (double) this.idealDistance * 1.1000000238418579)
        return false;
      this.swipeVelocity = this.idealDistanceCM / (Time.time - this.startTime);
      this.normalizedSwipe = (this.endPoint - this.startPoint).normalized;
      this.swipeAngle = Mathf.Atan2(this.normalizedSwipe.y, this.normalizedSwipe.x) * 57.29578f;
      if ((double) this.swipeAngle < 0.0)
        this.swipeAngle = 360f + this.swipeAngle;
      this.swipeAngle = 360f - this.swipeAngle;
      this.completedSwipeDirection = (double) this.swipeAngle < 292.5 || (double) this.swipeAngle > 337.5 ? ((double) this.swipeAngle < 247.5 || (double) this.swipeAngle > 292.5 ? ((double) this.swipeAngle < 202.5 || (double) this.swipeAngle > 247.5 ? ((double) this.swipeAngle < 157.5 || (double) this.swipeAngle > 202.5 ? ((double) this.swipeAngle < 112.5 || (double) this.swipeAngle > 157.5 ? ((double) this.swipeAngle < 67.5 || (double) this.swipeAngle > 112.5 ? ((double) this.swipeAngle < 22.5 || (double) this.swipeAngle > 67.5 ? TKSwipeDirection.Right : TKSwipeDirection.DownRight) : TKSwipeDirection.Down) : TKSwipeDirection.DownLeft) : TKSwipeDirection.Left) : TKSwipeDirection.UpLeft) : TKSwipeDirection.Up) : TKSwipeDirection.UpRight;
      return true;
    }
  }
}
