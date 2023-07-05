// Decompiled with JetBrains decompiler
// Type: UDB.TKPanRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKPanRecognizer : TKAbstractGestureRecognizer
  {
    public Vector2 deltaTranslation;
    public float deltaTranslationCm;
    public int minimumNumberOfTouches = 1;
    public int maximumNumberOfTouches = 2;
    private float totalDeltaMovementInCm;
    private Vector2 previousLocation;
    private float minDistanceToPanCm;
    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 currentLocation;

    public event Action<TKPanRecognizer> gestureRecognizedEvent;

    public event Action<TKPanRecognizer> gestureCompleteEvent;

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

    private bool touchCountBelowMax => this.trackingTouches.Count < this.maximumNumberOfTouches;

    private bool touchCountBelowOrEqualToMax => this.trackingTouches.Count <= this.maximumNumberOfTouches;

    private bool touchCountAboverOrEqualToMin => this.trackingTouches.Count >= this.minimumNumberOfTouches;

    public TKPanRecognizer(float minPanDistanceCm = 0.5f) => this.minDistanceToPanCm = minPanDistanceCm;

    internal override void FireRecognizedEvent()
    {
      if (this.gestureRecognizedEvent == null)
        return;
      this.gestureRecognizedEvent(this);
    }

    internal override bool TouchesBegan(List<TKTouch> touches)
    {
      if (this.trackingTouches.Count + touches.Count > this.maximumNumberOfTouches)
      {
        this.state = TKGestureRecognizerState.FailedOrEnded;
        return false;
      }
      if (this.state == TKGestureRecognizerState.Possible || this.touchBeganOrRecongized && this.touchCountBelowMax)
      {
        for (int index = 0; index < touches.Count; ++index)
        {
          if (touches[index].phase == TouchPhase.Began)
          {
            this.trackingTouches.Add(touches[index]);
            this.startPoint = touches[0].position;
            if (this.trackingTouches.Count == this.maximumNumberOfTouches)
              break;
          }
        }
        if (this.touchCountAboverOrEqualToMin && this.touchCountBelowOrEqualToMax)
        {
          this.previousLocation = this.TouchLocation();
          if (this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing)
          {
            this.totalDeltaMovementInCm = 0.0f;
            this.state = TKGestureRecognizerState.Began;
          }
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (!this.touchCountAboverOrEqualToMin || !this.touchCountBelowOrEqualToMax)
        return;
      this.currentLocation = this.TouchLocation();
      this.deltaTranslation = this.currentLocation - this.previousLocation;
      this.deltaTranslationCm = this.deltaTranslation.magnitude / TouchKit.screenPixelsPerCm;
      this.previousLocation = this.currentLocation;
      if (this.state == TKGestureRecognizerState.Began)
      {
        this.totalDeltaMovementInCm += this.deltaTranslationCm;
        if ((double) Mathf.Abs(this.totalDeltaMovementInCm) < (double) this.minDistanceToPanCm)
          return;
        this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
      }
      else
        this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      this.endPoint = this.TouchLocation();
      for (int index = 0; index < touches.Count; ++index)
      {
        if (touches[index].phase == TouchPhase.Ended)
          this.trackingTouches.Remove(touches[index]);
      }
      if (this.touchCountAboverOrEqualToMin)
      {
        this.previousLocation = this.TouchLocation();
        this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
      }
      else
      {
        if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing && this.gestureCompleteEvent != null)
          this.gestureCompleteEvent(this);
        this.state = TKGestureRecognizerState.FailedOrEnded;
      }
    }

    public override string ToString() => string.Format("[{0}] state: {1}, location: {2}, deltaTranslation: {3}", (object) this.GetType(), (object) this.state, (object) this.TouchLocation(), (object) this.deltaTranslation);
  }
}
