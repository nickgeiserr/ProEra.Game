// Decompiled with JetBrains decompiler
// Type: SplineMesh.CubicBezierCurve
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SplineMesh
{
  [Serializable]
  public class CubicBezierCurve
  {
    private const int STEP_COUNT = 30;
    private const float T_STEP = 0.0333333351f;
    private readonly List<CurveSample> samples = new List<CurveSample>(30);
    public SplineNode n1;
    public SplineNode n2;
    public UnityEvent Changed = new UnityEvent();

    public float Length { get; private set; }

    public CubicBezierCurve(SplineNode n1, SplineNode n2)
    {
      this.n1 = n1;
      this.n2 = n2;
      n1.Changed += new EventHandler(this.ComputeSamples);
      n2.Changed += new EventHandler(this.ComputeSamples);
      this.ComputeSamples((object) null, (EventArgs) null);
    }

    public void ConnectStart(SplineNode n1)
    {
      this.n1.Changed -= new EventHandler(this.ComputeSamples);
      this.n1 = n1;
      n1.Changed += new EventHandler(this.ComputeSamples);
      this.ComputeSamples((object) null, (EventArgs) null);
    }

    public void ConnectEnd(SplineNode n2)
    {
      this.n2.Changed -= new EventHandler(this.ComputeSamples);
      this.n2 = n2;
      n2.Changed += new EventHandler(this.ComputeSamples);
      this.ComputeSamples((object) null, (EventArgs) null);
    }

    public Vector3 GetInverseDirection() => 2f * this.n2.Position - this.n2.Direction;

    private Vector3 GetLocation(float t)
    {
      float num1 = 1f - t;
      float num2 = num1 * num1;
      float num3 = t * t;
      return this.n1.Position * (num2 * num1) + this.n1.Direction * (3f * num2 * t) + this.GetInverseDirection() * (3f * num1 * num3) + this.n2.Position * (num3 * t);
    }

    private Vector3 GetTangent(float t)
    {
      float num1 = 1f - t;
      float num2 = num1 * num1;
      float num3 = t * t;
      return (this.n1.Position * -num2 + this.n1.Direction * (float) (3.0 * (double) num2 - 2.0 * (double) num1) + this.GetInverseDirection() * (float) (-3.0 * (double) num3 + 2.0 * (double) t) + this.n2.Position * num3).normalized;
    }

    private Vector3 GetUp(float t) => Vector3.Lerp(this.n1.Up, this.n2.Up, t);

    private Vector2 GetScale(float t) => Vector2.Lerp(this.n1.Scale, this.n2.Scale, t);

    private float GetRoll(float t) => Mathf.Lerp(this.n1.Roll, this.n2.Roll, t);

    private void ComputeSamples(object sender, EventArgs e)
    {
      this.samples.Clear();
      this.Length = 0.0f;
      Vector3 a = this.GetLocation(0.0f);
      for (float num = 0.0f; (double) num < 1.0; num += 0.0333333351f)
      {
        Vector3 location = this.GetLocation(num);
        this.Length += Vector3.Distance(a, location);
        a = location;
        this.samples.Add(this.CreateSample(this.Length, num));
      }
      this.Length += Vector3.Distance(a, this.GetLocation(1f));
      this.samples.Add(this.CreateSample(this.Length, 1f));
      if (this.Changed == null)
        return;
      this.Changed.Invoke();
    }

    private CurveSample CreateSample(float distance, float time) => new CurveSample(this.GetLocation(time), this.GetTangent(time), this.GetUp(time), this.GetScale(time), this.GetRoll(time), distance, time);

    public CurveSample GetSample(float time)
    {
      CubicBezierCurve.AssertTimeInBounds(time);
      CurveSample a = this.samples[0];
      CurveSample b = new CurveSample();
      bool flag = false;
      foreach (CurveSample sample in this.samples)
      {
        if ((double) sample.timeInCurve >= (double) time)
        {
          b = sample;
          flag = true;
          break;
        }
        a = sample;
      }
      if (!flag)
        throw new Exception("Can't find curve samples.");
      float t = b == a ? 0.0f : (float) (((double) time - (double) a.timeInCurve) / ((double) b.timeInCurve - (double) a.timeInCurve));
      return CurveSample.Lerp(a, b, t);
    }

    public CurveSample GetSampleAtDistance(float d)
    {
      if ((double) d < 0.0 || (double) d > (double) this.Length)
        throw new ArgumentException("Distance must be positive and less than curve length. Length = " + this.Length.ToString() + ", given distance was " + d.ToString());
      CurveSample a = this.samples[0];
      CurveSample b = new CurveSample();
      bool flag = false;
      foreach (CurveSample sample in this.samples)
      {
        if ((double) sample.distanceInCurve >= (double) d)
        {
          b = sample;
          flag = true;
          break;
        }
        a = sample;
      }
      if (!flag)
        throw new Exception("Can't find curve samples.");
      float t = b == a ? 0.0f : (float) (((double) d - (double) a.distanceInCurve) / ((double) b.distanceInCurve - (double) a.distanceInCurve));
      return CurveSample.Lerp(a, b, t);
    }

    private static void AssertTimeInBounds(float time)
    {
      if ((double) time < 0.0 || (double) time > 1.0)
        throw new ArgumentException("Time must be between 0 and 1 (was " + time.ToString() + ").");
    }

    public CurveSample GetProjectionSample(Vector3 pointToProject)
    {
      float num1 = float.PositiveInfinity;
      int index = -1;
      int num2 = 0;
      foreach (CurveSample sample in this.samples)
      {
        float sqrMagnitude = (sample.location - pointToProject).sqrMagnitude;
        if ((double) sqrMagnitude < (double) num1)
        {
          num1 = sqrMagnitude;
          index = num2;
        }
        ++num2;
      }
      CurveSample sample1;
      CurveSample sample2;
      Vector3 vector3;
      if (index == 0)
      {
        sample1 = this.samples[index];
        sample2 = this.samples[index + 1];
      }
      else if (index == this.samples.Count - 1)
      {
        sample1 = this.samples[index - 1];
        sample2 = this.samples[index];
      }
      else
      {
        vector3 = pointToProject - this.samples[index - 1].location;
        double sqrMagnitude1 = (double) vector3.sqrMagnitude;
        vector3 = pointToProject - this.samples[index + 1].location;
        double sqrMagnitude2 = (double) vector3.sqrMagnitude;
        if (sqrMagnitude1 < sqrMagnitude2)
        {
          sample1 = this.samples[index - 1];
          sample2 = this.samples[index];
        }
        else
        {
          sample1 = this.samples[index];
          sample2 = this.samples[index + 1];
        }
      }
      vector3 = Vector3.Project(pointToProject - sample1.location, sample2.location - sample1.location) + sample1.location - sample1.location;
      double sqrMagnitude3 = (double) vector3.sqrMagnitude;
      vector3 = sample2.location - sample1.location;
      double sqrMagnitude4 = (double) vector3.sqrMagnitude;
      float t = Mathf.Clamp((float) (sqrMagnitude3 / sqrMagnitude4), 0.0f, 1f);
      return CurveSample.Lerp(sample1, sample2, t);
    }
  }
}
