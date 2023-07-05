// Decompiled with JetBrains decompiler
// Type: UDB.TKRotationRecognizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TKRotationRecognizer : TKAbstractGestureRecognizer
  {
    public float deltaRotation;
    public float minimumRotationToRecognize;
    protected float previousRotation;
    protected float firstRotation;
    protected float initialRotation;
    private float angleBetweenPoints;
    private float currentRotation;

    public event Action<TKRotationRecognizer> gestureRecognizedEvent;

    public event Action<TKRotationRecognizer> gestureCompleteEvent;

    public float accumulatedRotation
    {
      get
      {
        if (this.trackingTouches.Count != 2)
          return 0.0f;
        this.angleBetweenPoints = TKRotationRecognizer.AngleBetweenPoints(this.trackingTouches[0].position, this.trackingTouches[1].position);
        return Mathf.DeltaAngle(this.angleBetweenPoints, this.initialRotation);
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
        {
          if ((double) this.minimumRotationToRecognize == 0.0)
          {
            this.deltaRotation = 0.0f;
            this.previousRotation = TKRotationRecognizer.AngleBetweenPoints(this.trackingTouches[0].position, this.trackingTouches[1].position);
            this.state = TKGestureRecognizerState.Began;
          }
          else
            this.firstRotation = TKRotationRecognizer.AngleBetweenPoints(this.trackingTouches[0].position, this.trackingTouches[1].position);
        }
      }
      return false;
    }

    internal override void TouchesMoved(List<TKTouch> touches)
    {
      if (this.state == TKGestureRecognizerState.Possible && this.trackingTouches.Count == 2)
      {
        this.currentRotation = TKRotationRecognizer.AngleBetweenPoints(this.trackingTouches[0].position, this.trackingTouches[1].position);
        if ((double) Mathf.Abs(Mathf.DeltaAngle(this.currentRotation, this.firstRotation)) > (double) this.minimumRotationToRecognize)
        {
          this.initialRotation = this.currentRotation;
          this.deltaRotation = 0.0f;
          this.previousRotation = TKRotationRecognizer.AngleBetweenPoints(this.trackingTouches[0].position, this.trackingTouches[1].position);
          this.state = TKGestureRecognizerState.Began;
        }
      }
      if (this.state != TKGestureRecognizerState.RecognizedAndStillRecognizing && this.state != TKGestureRecognizerState.Began)
        return;
      this.currentRotation = TKRotationRecognizer.AngleBetweenPoints(this.trackingTouches[0].position, this.trackingTouches[1].position);
      this.deltaRotation = Mathf.DeltaAngle(this.currentRotation, this.previousRotation);
      this.previousRotation = this.currentRotation;
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
      if (this.trackingTouches.Count == 1)
      {
        this.state = TKGestureRecognizerState.Possible;
        this.deltaRotation = 0.0f;
      }
      else
      {
        this.state = TKGestureRecognizerState.FailedOrEnded;
        this.initialRotation = 0.0f;
      }
    }

    public override string ToString() => string.Format("[{0}] state: {1}, location: {2}, rotation: {3}", (object) this.GetType(), (object) this.state, (object) this.TouchLocation(), (object) this.deltaRotation);

    public static float AngleBetweenPoints(Vector2 position1, Vector2 position2)
    {
      Vector2 vector2_1 = position2 - position1;
      Vector2 vector2_2 = new Vector2(1f, 0.0f);
      float num = Vector2.Angle(vector2_1, vector2_2);
      if ((double) Vector3.Cross((Vector3) vector2_1, (Vector3) vector2_2).z > 0.0)
        num = 360f - num;
      return num;
    }
  }
}
