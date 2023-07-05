// Decompiled with JetBrains decompiler
// Type: UDB.TKLongPressRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKLongPressRecognizer : TKAbstractGestureRecognizer
  {
    public float allowableMovementCm = 1f;
    public float minimumPressDuration = 0.5f;
    public int requiredTouchesCount = -1;
    private Vector2 beginLocation;
    private bool waiting;

    public event Action<TKLongPressRecognizer> gestureRecognizedEvent;

    public event Action<TKLongPressRecognizer> gestureCompleteEvent;

    private bool notWaitingOrDoingLongPress => !this.waiting && this.state == TKGestureRecognizerState.Possible;

    public TKLongPressRecognizer()
    {
    }

    public TKLongPressRecognizer(
      float minimumPressDuration,
      float allowableMovement,
      int requiredTouchesCount)
    {
      this.minimumPressDuration = minimumPressDuration;
      this.allowableMovementCm = allowableMovement;
      this.requiredTouchesCount = requiredTouchesCount;
    }

    internal override void FireRecognizedEvent()
    {
      if (this.gestureRecognizedEvent == null)
        return;
      this.gestureRecognizedEvent(this);
    }

    internal override bool TouchesBegan(List<TKTouch> touches)
    {
      if (this.notWaitingOrDoingLongPress && (this.requiredTouchesCount == -1 || touches.Count == this.requiredTouchesCount))
      {
        this.beginLocation = touches[0].position;
        this.waiting = true;
        SingletonBehaviour<TouchKit, MonoBehaviour>.instance.StartCoroutine(this.BeginGesture());
        this.trackingTouches.Add(touches[0]);
        this.state = TKGestureRecognizerState.Began;
      }
      else if (this.requiredTouchesCount != -1)
        this.waiting = false;
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state != TKGestureRecognizerState.Began && this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing || (double) Vector2.Distance(touches[0].position, this.beginLocation) / (double) TouchKit.screenPixelsPerCm <= (double) this.allowableMovementCm)
        return;
      if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing && this.gestureCompleteEvent != null)
        this.gestureCompleteEvent(this);
      this.state = TKGestureRecognizerState.FailedOrEnded;
      this.waiting = false;
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing && this.gestureCompleteEvent != null)
        this.gestureCompleteEvent(this);
      this.state = TKGestureRecognizerState.FailedOrEnded;
      this.waiting = false;
    }

    private IEnumerator BeginGesture()
    {
      TKLongPressRecognizer longPressRecognizer = this;
      float endTime = Time.time + longPressRecognizer.minimumPressDuration;
      while (longPressRecognizer.waiting && (double) Time.time < (double) endTime)
        yield return (object) null;
      if ((double) Time.time >= (double) endTime && longPressRecognizer.state == TKGestureRecognizerState.Began)
        longPressRecognizer.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
      longPressRecognizer.waiting = false;
    }
  }
}
