// Decompiled with JetBrains decompiler
// Type: UDB.TKAbstractGestureRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public abstract class TKAbstractGestureRecognizer : IComparable<TKAbstractGestureRecognizer>
  {
    public bool enabled = true;
    public TKRect? boundaryFrame;
    public uint zIndex;
    public float pinchSensitivity = 1f;
    private TKGestureRecognizerState _state;
    protected bool alwaysSendTouchesMoved;
    protected List<TKTouch> trackingTouches = new List<TKTouch>();
    private List<TKTouch> subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer = new List<TKTouch>();
    private bool sentTouchesBegan;
    private bool sentTouchesMoved;
    private bool sentTouchesEnded;

    public TKGestureRecognizerState state
    {
      get => this._state;
      set
      {
        this._state = value;
        if (this._state == TKGestureRecognizerState.Recognized || this._state == TKGestureRecognizerState.RecognizedAndStillRecognizing)
          this.FireRecognizedEvent();
        if (this._state != TKGestureRecognizerState.Recognized && this._state != TKGestureRecognizerState.FailedOrEnded)
          return;
        this.Reset();
      }
    }

    private bool shouldAttemptToRecognize => this.enabled && this.state != TKGestureRecognizerState.FailedOrEnded && this.state != TKGestureRecognizerState.Recognized;

    protected bool touchBeganOrRecongized => this.state == TKGestureRecognizerState.Began || this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing;

    public int CompareTo(TKAbstractGestureRecognizer other) => this.zIndex.CompareTo(other.zIndex);

    public override string ToString() => string.Format("[{0}] state: {1}, location: {2}, zIndex: {3}", (object) this.GetType(), (object) this.state, (object) this.TouchLocation(), (object) this.zIndex);

    private bool PopulateSubsetOfTouchesBeingTracked(List<TKTouch> touches)
    {
      this.subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer.Clear();
      for (int index = 0; index < touches.Count; ++index)
      {
        if (this.alwaysSendTouchesMoved || this.IsTrackingTouch(touches[index]))
          this.subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer.Add(touches[index]);
      }
      return this.subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer.Count > 0;
    }

    protected bool IsTrackingTouch(TKTouch touch) => this.trackingTouches.Contains(touch);

    protected bool IsTrackingAnyTouch(List<TKTouch> touches)
    {
      for (int index = 0; index < touches.Count; ++index)
      {
        if (this.trackingTouches.Contains(touches[index]))
          return true;
      }
      return false;
    }

    internal void RecognizeTouches(List<TKTouch> touches)
    {
      if (!this.shouldAttemptToRecognize)
        return;
      this.sentTouchesBegan = this.sentTouchesMoved = this.sentTouchesEnded = false;
      for (int index1 = touches.Count - 1; index1 >= 0; --index1)
      {
        TKTouch touch = touches[index1];
        bool flag1 = this.PopulateSubsetOfTouchesBeingTracked(touches);
        bool flag2 = this.subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer.Contains(touch);
        switch (touch.phase)
        {
          case TouchPhase.Began:
            if (!this.sentTouchesBegan && this.IsTouchWithinBoundaryFrame(touches[index1]))
            {
              if (this.TouchesBegan(touches) && this.zIndex > 0U)
              {
                int num = 0;
                for (int index2 = touches.Count - 1; index2 >= 0; --index2)
                {
                  if (touches[index2].phase == TouchPhase.Began)
                  {
                    touches.RemoveAt(index2);
                    ++num;
                  }
                }
                if (num > 0)
                  index1 -= num - 1;
              }
              this.sentTouchesBegan = true;
              break;
            }
            break;
          case TouchPhase.Moved:
            if (!this.sentTouchesMoved & flag1 & flag2)
            {
              this.TouchesMoved(this.subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer);
              this.sentTouchesMoved = true;
              break;
            }
            break;
          case TouchPhase.Ended:
          case TouchPhase.Canceled:
            if (!this.sentTouchesEnded & flag1 & flag2)
            {
              this.TouchesEnded(this.subsetOfTouchesBeingTrackedApplicableToCurrentRecognizer);
              this.sentTouchesEnded = true;
              break;
            }
            break;
        }
      }
    }

    internal void Reset()
    {
      this._state = TKGestureRecognizerState.Possible;
      this.trackingTouches.Clear();
    }

    internal bool IsTouchWithinBoundaryFrame(TKTouch touch) => !this.boundaryFrame.HasValue || this.boundaryFrame.Value.Contains(touch.position);

    public Vector2 TouchLocation()
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      for (int index = 0; index < this.trackingTouches.Count; ++index)
      {
        num1 += this.trackingTouches[index].position.x;
        num2 += this.trackingTouches[index].position.y;
        ++num3;
      }
      return (double) num3 > 0.0 ? new Vector2(num1 / num3, num2 / num3) : Vector2.zero;
    }

    public Vector2 StartTouchLocation()
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      for (int index = 0; index < this.trackingTouches.Count; ++index)
      {
        num1 += this.trackingTouches[index].startPosition.x;
        num2 += this.trackingTouches[index].startPosition.y;
        ++num3;
      }
      return (double) num3 > 0.0 ? new Vector2(num1 / num3, num2 / num3) : Vector2.zero;
    }

    internal virtual bool TouchesBegan(List<TKTouch> touches) => false;

    internal virtual void TouchesMoved(List<TKTouch> touches)
    {
    }

    internal virtual void TouchesEnded(List<TKTouch> touches)
    {
    }

    internal abstract void FireRecognizedEvent();
  }
}
