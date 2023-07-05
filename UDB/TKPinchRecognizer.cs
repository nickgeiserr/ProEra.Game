// Decompiled with JetBrains decompiler
// Type: UDB.TKPinchRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKPinchRecognizer : TKAbstractGestureRecognizer
  {
    public float minimumScaleDistanceToRecognize;
    public float deltaScale;
    private float intialDistance;
    private float firstDistance;
    private float previousDistance;
    private float currentDistance;
    private float distance;

    public event Action<TKPinchRecognizer> gestureRecognizedEvent;

    public event Action<TKPinchRecognizer> gestureCompleteEvent;

    public float accumulatedScale => this.distanceBetweenTrackedTouches / this.intialDistance;

    private float distanceBetweenTrackedTouches
    {
      get
      {
        this.distance = Vector2.Distance(this.trackingTouches[0].position, this.trackingTouches[1].position);
        return Mathf.Max(0.0001f, this.distance) / TouchKit.screenPixelsPerCm * this.pinchSensitivity;
      }
    }

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
        {
          if (touches[index].phase == TouchPhase.Began)
          {
            this.trackingTouches.Add(touches[index]);
            if (this.trackingTouches.Count == 2)
              break;
          }
        }
        if (this.trackingTouches.Count == 2)
          this.firstDistance = this.distanceBetweenTrackedTouches;
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.trackingTouches.Count != 2)
        return;
      if (this.state == TKGestureRecognizerState.Possible)
      {
        if ((double) Mathf.Abs(this.distanceBetweenTrackedTouches - this.firstDistance) < (double) this.minimumScaleDistanceToRecognize)
          return;
        this.deltaScale = 0.0f;
        this.intialDistance = this.distanceBetweenTrackedTouches;
        this.previousDistance = this.intialDistance;
        this.state = TKGestureRecognizerState.Began;
      }
      else
      {
        if (this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing && this.state != TKGestureRecognizerState.Began)
          return;
        this.currentDistance = this.distanceBetweenTrackedTouches;
        this.deltaScale = (this.currentDistance - this.previousDistance) / this.intialDistance;
        this.previousDistance = this.currentDistance;
        this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
      }
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      for (int index = 0; index < touches.Count; ++index)
      {
        if (touches[index].phase == TouchPhase.Ended)
          this.trackingTouches.Remove(touches[index]);
      }
      if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing && this.gestureCompleteEvent != null)
        this.gestureCompleteEvent(this);
      if (this.trackingTouches.Count == 1)
      {
        this.state = TKGestureRecognizerState.Possible;
        this.deltaScale = 0.0f;
      }
      else
        this.state = TKGestureRecognizerState.FailedOrEnded;
    }

    public override string ToString() => string.Format("[{0}] state: {1}, deltaScale: {2}", (object) this.GetType(), (object) this.state, (object) this.deltaScale);
  }
}
