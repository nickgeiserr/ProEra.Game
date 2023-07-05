// Decompiled with JetBrains decompiler
// Type: UDB.TKCurveRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKCurveRecognizer : TKAbstractGestureRecognizer
  {
    public float reportRotationStep = 20f;
    public float squareDistance = 10f;
    public float maxSharpnes = 500f;
    public int minimumNumberOfTouches = 1;
    public int maximumNumberOfTouches = 2;
    public float deltaRotation;
    private Vector2 previousLocation;
    private Vector2 deltaTranslation;
    private Vector2 previousDeltaTranslation;

    public event Action<TKCurveRecognizer> gestureRecognizedEvent;

    public event Action<TKCurveRecognizer> gestureCompleteEvent;

    private bool touchCountBelowMax => this.trackingTouches.Count < this.maximumNumberOfTouches;

    internal override void FireRecognizedEvent()
    {
      if (this.gestureRecognizedEvent == null)
        return;
      this.gestureRecognizedEvent(this);
    }

    internal override bool TouchesBegan(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.Possible || this.touchBeganOrRecongized && this.touchCountBelowMax)
      {
        for (int index = 0; index < touches.Count; ++index)
        {
          if (touches[index].phase == TouchPhase.Began)
          {
            this.trackingTouches.Add(touches[index]);
            if (this.trackingTouches.Count == this.maximumNumberOfTouches)
              break;
          }
        }
        if (this.trackingTouches.Count >= this.minimumNumberOfTouches)
        {
          this.previousLocation = this.TouchLocation();
          if (this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing)
          {
            this.state = TKGestureRecognizerState.Possible;
            this.deltaRotation = 0.0f;
            this.deltaTranslation = Vector2.zero;
            this.previousDeltaTranslation = Vector2.zero;
          }
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.Possible)
      {
        Vector2 vector2 = this.TouchLocation();
        this.deltaTranslation = vector2 - this.previousLocation;
        this.previousLocation = vector2;
        this.previousDeltaTranslation = this.deltaTranslation;
        this.state = TKGestureRecognizerState.Began;
      }
      else
      {
        if (this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing && this.state != TKGestureRecognizerState.Began)
          return;
        Vector2 vector2_1 = this.TouchLocation();
        Vector2 vector2_2 = vector2_1 - this.previousLocation;
        if ((double) vector2_2.sqrMagnitude < 10.0)
          return;
        float num = Vector2.Angle(this.previousDeltaTranslation, vector2_2);
        if ((double) num > (double) this.maxSharpnes)
        {
          Debug.Log((object) ("Curve is to sharp: " + num.ToString() + "  max sharpnes set to:" + this.maxSharpnes.ToString()));
          this.state = TKGestureRecognizerState.FailedOrEnded;
        }
        else
        {
          this.deltaTranslation = vector2_2;
          if ((double) Vector3.Cross((Vector3) this.previousDeltaTranslation, (Vector3) vector2_2).z > 0.0)
            this.deltaRotation -= num;
          else
            this.deltaRotation += num;
          if ((double) Mathf.Abs(this.deltaRotation) >= (double) this.reportRotationStep)
          {
            this.state = TKGestureRecognizerState.RecognizedAndStillRecognizing;
            this.deltaRotation = 0.0f;
          }
          this.previousLocation = vector2_1;
          this.previousDeltaTranslation = this.deltaTranslation;
        }
      }
    }

    internal override void TouchesEnded(List<TKTouch> touches)
    {
      for (int index = 0; index < touches.Count; ++index)
      {
        if (touches[index].phase == TouchPhase.Ended)
          this.trackingTouches.Remove(touches[index]);
      }
      if (this.trackingTouches.Count >= this.minimumNumberOfTouches)
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

    public override string ToString() => string.Format("[{0}] state: {1}, trans: {2}, lastTrans: {3}, totalRot: {4}", (object) this.GetType(), (object) this.state, (object) this.deltaTranslation, (object) this.previousDeltaTranslation, (object) this.deltaRotation);
  }
}
