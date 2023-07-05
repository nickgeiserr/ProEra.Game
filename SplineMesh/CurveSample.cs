// Decompiled with JetBrains decompiler
// Type: SplineMesh.CurveSample
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace SplineMesh
{
  [Serializable]
  public struct CurveSample
  {
    public Vector3 location;
    public Vector3 tangent;
    public Vector3 up;
    public Vector2 scale;
    public float roll;
    public float distanceInCurve;
    public float timeInCurve;
    public float timeInSpline;
    private Quaternion rotation;

    public Quaternion Rotation
    {
      get
      {
        if (this.rotation == Quaternion.identity)
          this.rotation = Quaternion.LookRotation(this.tangent, Vector3.Cross(this.tangent, Vector3.Cross(Quaternion.AngleAxis(this.roll, Vector3.forward) * this.up, this.tangent).normalized));
        return this.rotation;
      }
    }

    public CurveSample(
      Vector3 location,
      Vector3 tangent,
      Vector3 up,
      Vector2 scale,
      float roll,
      float distanceInCurve,
      float timeInCurve)
    {
      this.location = location;
      this.tangent = tangent;
      this.up = up;
      this.roll = roll;
      this.scale = scale;
      this.distanceInCurve = distanceInCurve;
      this.timeInCurve = timeInCurve;
      this.rotation = Quaternion.identity;
      this.timeInSpline = 0.0f;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
        return false;
      CurveSample curveSample = (CurveSample) obj;
      return this.location == curveSample.location && this.tangent == curveSample.tangent && this.up == curveSample.up && this.scale == curveSample.scale && (double) this.roll == (double) curveSample.roll && (double) this.distanceInCurve == (double) curveSample.distanceInCurve && (double) this.timeInCurve == (double) curveSample.timeInCurve;
    }

    public override int GetHashCode() => base.GetHashCode();

    public static bool operator ==(CurveSample cs1, CurveSample cs2) => cs1.Equals((object) cs2);

    public static bool operator !=(CurveSample cs1, CurveSample cs2) => !cs1.Equals((object) cs2);

    public static CurveSample Lerp(CurveSample a, CurveSample b, float t) => new CurveSample(Vector3.Lerp(a.location, b.location, t), Vector3.Lerp(a.tangent, b.tangent, t).normalized, Vector3.Lerp(a.up, b.up, t), Vector2.Lerp(a.scale, b.scale, t), Mathf.Lerp(a.roll, b.roll, t), Mathf.Lerp(a.distanceInCurve, b.distanceInCurve, t), Mathf.Lerp(a.timeInCurve, b.timeInCurve, t));
  }
}
