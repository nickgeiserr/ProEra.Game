// Decompiled with JetBrains decompiler
// Type: UDB.TKTapRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKTapRecognizer : TKAbstractGestureRecognizer
  {
    public int numberOfTapsRequired = 1;
    public int numberOfTouchesRequired = 1;
    private float maxDurationForTapConsideration = 0.5f;
    private float maxDeltaMovementForTapConsideration = 1f;
    private float touchBeganTime;
    private int preformedTapsCount;
    private float xDiff;
    private float yDiff;
    private bool xDiffAboveMaxDelta;
    private bool yDiffAboveMaxDelta;

    public event Action<TKTapRecognizer> gestureRecognizedEvent;

    public TKTapRecognizer()
      : this(0.5f, 1f)
    {
    }

    public TKTapRecognizer(
      float maxDurationForTapConsideration,
      float maxDeltaMovementForTapConsiderationCm)
    {
      this.maxDurationForTapConsideration = maxDurationForTapConsideration;
      this.maxDeltaMovementForTapConsideration = maxDeltaMovementForTapConsiderationCm;
    }

    internal override void FireRecognizedEvent()
    {
      if (this.gestureRecognizedEvent == null)
        return;
      this.gestureRecognizedEvent(this);
    }

    internal override bool TouchesBegan(List<TKTouch> touches)
    {
      if ((double) Time.time > (double) this.touchBeganTime + (double) this.maxDurationForTapConsideration && this.preformedTapsCount != 0 && this.preformedTapsCount < this.numberOfTapsRequired)
        this.state = TKGestureRecognizerState.FailedOrEnded;
      if (this.state == TKGestureRecognizerState.Possible)
      {
        for (int index = 0; index < touches.Count; ++index)
        {
          if (touches[index].phase == TouchPhase.Began)
          {
            this.trackingTouches.Add(touches[index]);
            if (this.trackingTouches.Count == this.numberOfTouchesRequired)
              break;
          }
        }
        if (this.trackingTouches.Count == this.numberOfTouchesRequired)
        {
          this.touchBeganTime = Time.time;
          this.preformedTapsCount = 0;
          this.state = TKGestureRecognizerState.Began;
          return true;
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state != TKGestureRecognizerState.Began)
        return;
      for (int index = 0; index < touches.Count; ++index)
      {
        this.xDiff = Mathf.Abs(touches[index].position.x - touches[index].startPosition.x);
        this.yDiff = Mathf.Abs(touches[index].position.y - touches[index].startPosition.y);
        this.xDiffAboveMaxDelta = (double) this.xDiff / (double) TouchKit.screenPixelsPerCm > (double) this.maxDeltaMovementForTapConsideration;
        this.yDiffAboveMaxDelta = (double) this.yDiff / (double) TouchKit.screenPixelsPerCm > (double) this.maxDeltaMovementForTapConsideration;
        if (this.xDiffAboveMaxDelta || this.yDiffAboveMaxDelta)
        {
          this.state = TKGestureRecognizerState.FailedOrEnded;
          break;
        }
      }
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.Began && (double) Time.time <= (double) this.touchBeganTime + (double) this.maxDurationForTapConsideration)
      {
        ++this.preformedTapsCount;
        if (this.preformedTapsCount != this.numberOfTapsRequired)
          return;
        this.state = TKGestureRecognizerState.Recognized;
      }
      else
        this.state = TKGestureRecognizerState.FailedOrEnded;
    }
  }
}
