// Decompiled with JetBrains decompiler
// Type: UDB.TKButtonRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKButtonRecognizer : TKAbstractGestureRecognizer
  {
    private TKRect defaultFrame;
    private TKRect highlightedFrame;

    public event Action<TKButtonRecognizer> onSelectedEvent;

    public event Action<TKButtonRecognizer> onDeselectedEvent;

    public event Action<TKButtonRecognizer> onTouchUpInsideEvent;

    public TKButtonRecognizer(TKRect defaultFrame)
      : this(defaultFrame, 40f)
    {
    }

    public TKButtonRecognizer(TKRect defaultFrame, float highlightedExpansion)
      : this(defaultFrame, defaultFrame.CopyWithExpansion(highlightedExpansion))
    {
    }

    public TKButtonRecognizer(TKRect defaultFrame, TKRect highlightedFrame)
    {
      this.defaultFrame = defaultFrame;
      this.highlightedFrame = highlightedFrame;
      this.boundaryFrame = new TKRect?(this.defaultFrame);
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
            this.OnSelected();
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
        if (touches[index].phase == TouchPhase.Stationary)
        {
          bool flag = this.IsTouchWithinBoundaryFrame(touches[index]);
          if (this.state == TKGestureRecognizerState.Began & flag)
          {
            this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
            this.OnSelected();
          }
          else if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing && !flag)
          {
            this.state = TKGestureRecognizerState.FailedOrEnded;
            this.OnDeselected();
          }
        }
      }
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.RecognizedAndStillRecognizing)
        this.OnTouchUpInside();
      this.boundaryFrame = new TKRect?(this.defaultFrame);
      this.state = TKGestureRecognizerState.FailedOrEnded;
    }

    protected virtual void OnSelected()
    {
      this.boundaryFrame = new TKRect?(this.highlightedFrame);
      if (this.onSelectedEvent == null)
        return;
      this.onSelectedEvent(this);
    }

    protected virtual void OnDeselected()
    {
      if (this.onDeselectedEvent == null)
        return;
      this.onDeselectedEvent(this);
    }

    protected virtual void OnTouchUpInside()
    {
      if (this.onTouchUpInsideEvent == null)
        return;
      this.onTouchUpInsideEvent(this);
    }
  }
}
