// Decompiled with JetBrains decompiler
// Type: UDB.TKAnyTouchRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKAnyTouchRecognizer : TKAbstractGestureRecognizer
  {
    public event Action<TKAnyTouchRecognizer> onEnteredEvent;

    public event Action<TKAnyTouchRecognizer> onExitedEvent;

    public TKAnyTouchRecognizer(TKRect frame)
    {
      this.alwaysSendTouchesMoved = true;
      this.boundaryFrame = new TKRect?(frame);
    }

    internal override void FireRecognizedEvent()
    {
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
            this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
            this.OnTouchEntered();
            return true;
          }
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      for (int index = 0; index < touches.Count; ++index)
      {
        bool flag1 = this.IsTouchWithinBoundaryFrame(touches[index]);
        bool flag2 = this.trackingTouches.Contains(touches[index]);
        if (!(flag2 & flag1))
        {
          if (!flag2 & flag1)
          {
            this.trackingTouches.Add(touches[index]);
            this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
            this.OnTouchEntered();
          }
          else if (flag2 && !flag1)
          {
            this.trackingTouches.Remove(touches[index]);
            this.state = TKGestureRecognizerState.FailedOrEnded;
            this.OnTouchExited();
          }
        }
      }
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      for (int index = 0; index < touches.Count; ++index)
      {
        if (touches[index].phase == TouchPhase.Ended && this.trackingTouches.Contains(touches[index]))
        {
          this.trackingTouches.Remove(touches[index]);
          this.state = TKGestureRecognizerState.FailedOrEnded;
          this.OnTouchExited();
        }
      }
    }

    private void OnTouchEntered()
    {
      if (this.trackingTouches.Count != 1 || this.onEnteredEvent == null)
        return;
      this.onEnteredEvent(this);
    }

    private void OnTouchExited()
    {
      if (this.trackingTouches.Count != 0 || this.onExitedEvent == null)
        return;
      this.onExitedEvent(this);
    }
  }
}
