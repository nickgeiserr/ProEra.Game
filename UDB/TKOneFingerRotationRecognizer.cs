// Decompiled with JetBrains decompiler
// Type: UDB.TKOneFingerRotationRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKOneFingerRotationRecognizer : TKRotationRecognizer
  {
    public Vector2 targetPosition;
    private float currentRotation;

    public event Action<TKOneFingerRotationRecognizer> gestureRecognizedEvent;

    public event Action<TKOneFingerRotationRecognizer> gestureCompleteEvent;

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
        this.trackingTouches.Add(touches[0]);
        this.deltaRotation = 0.0f;
        this.previousRotation = TKRotationRecognizer.AngleBetweenPoints(this.targetPosition, this.trackingTouches[0].position);
        this.state = TKGestureRecognizerState.Began;
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing && this.state != TKGestureRecognizerState.Began)
        return;
      this.currentRotation = TKRotationRecognizer.AngleBetweenPoints(this.targetPosition, this.trackingTouches[0].position);
      this.deltaRotation = Mathf.DeltaAngle(this.currentRotation, this.previousRotation);
      this.previousRotation = this.currentRotation;
      this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing && this.gestureCompleteEvent != null)
        this.gestureCompleteEvent(this);
      this.state = TKGestureRecognizerState.FailedOrEnded;
    }
  }
}
