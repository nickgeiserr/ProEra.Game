// Decompiled with JetBrains decompiler
// Type: UDB.TKAngleSwipeRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKAngleSwipeRecognizer : TKAbstractGestureRecognizer
  {
    private List<TKAngleSwipeRecognizer.AngleListener> angleRecognizedEvents = new List<TKAngleSwipeRecognizer.AngleListener>();
    public float timeToSwipe = 0.5f;
    public float minimumDistance = 2f;
    private float startTime;
    private Vector2 _startPoint;
    private Vector2 _endPoint;

    public event Action<TKAngleSwipeRecognizer> gestureRecognizedEvent;

    public float swipeVelocity { get; private set; }

    public float swipeAngle { get; private set; }

    public Vector2 swipeVelVector { get; private set; }

    public Vector2 startPoint
    {
      get => this._startPoint;
      private set => this._startPoint = value;
    }

    public Vector2 endPoint
    {
      get => this._endPoint;
      private set => this._endPoint = value;
    }

    public TKAngleSwipeRecognizer()
      : this(2f)
    {
    }

    public TKAngleSwipeRecognizer(float minimumDistanceCm) => this.minimumDistance = minimumDistanceCm;

    internal override void FireRecognizedEvent()
    {
      if (this.gestureRecognizedEvent != null)
        this.gestureRecognizedEvent(this);
      this.fireAngleRecognizedEvents();
    }

    internal override bool TouchesBegan(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.Possible)
      {
        this.startPoint = touches[0].position;
        this.startTime = Time.time;
        this.trackingTouches.Add(touches[0]);
        this.state = TKGestureRecognizerState.Began;
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state != TKGestureRecognizerState.Began || !this.checkForSwipeCompletion(touches[0]))
        return;
      this.state = TKGestureRecognizerState.Recognized;
    }

    internal override void TouchesEnded(List<TKTouch> touches) => this.state = TKGestureRecognizerState.FailedOrEnded;

    public override string ToString() => string.Format("{0}, velocity: {1}, angle: {2}, start point: {3}, end point: {4}", (object) base.ToString(), (object) this.swipeVelocity, (object) this.swipeAngle, (object) this.startPoint, (object) this.endPoint);

    private bool checkForSwipeCompletion(TKTouch touch)
    {
      if ((double) this.timeToSwipe > 0.0 && (double) Time.time - (double) this.startTime > (double) this.timeToSwipe)
      {
        this.state = TKGestureRecognizerState.FailedOrEnded;
        return false;
      }
      float num1 = Mathf.Abs(this.startPoint.x - touch.position.x) / TouchKit.screenPixelsPerCm;
      float num2 = Mathf.Abs(this.startPoint.y - touch.position.y) / TouchKit.screenPixelsPerCm;
      this.endPoint = touch.position;
      this.swipeVelocity = Mathf.Sqrt((float) ((double) num1 * (double) num1 + (double) num2 * (double) num2));
      Vector2 vector2 = this.endPoint - this.startPoint;
      this.swipeAngle = 57.29578f * Mathf.Atan2(vector2.y, vector2.x);
      if ((double) this.swipeAngle < 0.0)
        this.swipeAngle += 360f;
      this.swipeVelVector = this.endPoint - this.startPoint;
      return (double) this.swipeVelocity > (double) this.minimumDistance;
    }

    public void addAngleRecogizedEvents(
      Action<TKAngleSwipeRecognizer> action,
      Vector2 direction,
      float angleVarience)
    {
      this.angleRecognizedEvents.Add(new TKAngleSwipeRecognizer.AngleListener(direction, angleVarience, action));
    }

    public void removeAngleRecognizedEvents(Action<TKAngleSwipeRecognizer> action) => this.angleRecognizedEvents.RemoveAll((Predicate<TKAngleSwipeRecognizer.AngleListener>) (listener => listener.action == action));

    public void removeAllAngleRecongnizedEvents() => this.angleRecognizedEvents.Clear();

    public void fireAngleRecognizedEvents()
    {
      int index = 0;
      for (int count = this.angleRecognizedEvents.Count; index < count; ++index)
      {
        TKAngleSwipeRecognizer.AngleListener angleRecognizedEvent = this.angleRecognizedEvents[index];
        if ((double) angleRecognizedEvent.varience > (double) Vector2.Angle(angleRecognizedEvent.direction, this.swipeVelVector))
          angleRecognizedEvent.action(this);
      }
    }

    private struct AngleListener
    {
      public float varience;
      public Vector2 direction;
      public Action<TKAngleSwipeRecognizer> action;

      public AngleListener(
        Vector2 direction,
        float varience,
        Action<TKAngleSwipeRecognizer> action)
        : this()
      {
        this.varience = varience;
        this.direction = direction;
        this.action = action;
      }
    }
  }
}
