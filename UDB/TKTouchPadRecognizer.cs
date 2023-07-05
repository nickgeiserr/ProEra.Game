// Decompiled with JetBrains decompiler
// Type: UDB.TKTouchPadRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKTouchPadRecognizer : TKAbstractGestureRecognizer
  {
    public AnimationCurve inputCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    public Vector2 tempValue;
    private Vector2 currentLocation;

    public event Action<TKTouchPadRecognizer> gestureRecognizedEvent;

    public event Action<TKTouchPadRecognizer> gestureCompleteEvent;

    public TKTouchPadRecognizer(TKRect frame) => this.boundaryFrame = new TKRect?(frame);

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
            this.trackingTouches.Add(touches[index]);
        }
        if (this.trackingTouches.Count > 0)
        {
          this.state = TKGestureRecognizerState.Began;
          this.TouchesMoved(touches);
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (!this.touchBeganOrRecongized)
        return;
      this.currentLocation = this.TouchLocation();
      this.tempValue = this.currentLocation - this.boundaryFrame.Value.center;
      this.tempValue.x = Mathf.Clamp(this.tempValue.x / (this.boundaryFrame.Value.width * 0.5f), -1f, 1f);
      this.tempValue.y = Mathf.Clamp(this.tempValue.y / (this.boundaryFrame.Value.height * 0.5f), -1f, 1f);
      this.tempValue.x = this.inputCurve.Evaluate(Mathf.Abs(this.tempValue.x)) * Mathf.Sign(this.tempValue.x);
      this.tempValue.y = this.inputCurve.Evaluate(Mathf.Abs(this.tempValue.y)) * Mathf.Sign(this.tempValue.y);
      this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
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
      this.tempValue = Vector2.zero;
      this.state = TKGestureRecognizerState.FailedOrEnded;
    }

    public override string ToString() => string.Format("[{0}] state: {1}, value: {2}", (object) this.GetType(), (object) this.state, (object) this.tempValue);
  }
}
