// Decompiled with JetBrains decompiler
// Type: VisibilityAreaConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[Serializable]
public class VisibilityAreaConfig
{
  [Header("Visibility Angles")]
  [Range(0.0f, 180f)]
  public float maxVisibilityAngle = 160f;
  [Range(0.0f, 180f)]
  public float minVisibilityAngle = 40f;
  public float minVisibilitySpeed = 7f;
  [Header("Proximity (close range)")]
  public float maxProximityRadius = 1f;
  public float minProximityRadius = 0.5f;
  public float minProximitySpeed = 7f;
  [Header("Visibility Radius (far range)")]
  public float maxVisibilityRadius = 4.5f;
  public float minVisibilityRadius = 2f;
  public float maxVisibilitySpeed = 7f;
  private static Vector3 _planeXZ = new Vector3(1f, 0.0f, 1f);

  public bool IsBallPositionCandidateInsideVisibilityArea(
    Vector3 playerPos,
    Vector3 playerFwd,
    Vector3 playerUnscaledVelocity,
    Vector3 evaluatedBallPos)
  {
    playerPos.Scale(VisibilityAreaConfig._planeXZ);
    playerFwd.Scale(VisibilityAreaConfig._planeXZ);
    playerFwd.Normalize();
    evaluatedBallPos.Scale(VisibilityAreaConfig._planeXZ);
    playerUnscaledVelocity.Scale(VisibilityAreaConfig._planeXZ);
    double num1 = (double) Vector3.Dot(playerFwd, playerUnscaledVelocity);
    float num2 = MathUtils.MapRange((float) num1, 0.0f, this.maxVisibilitySpeed, this.minVisibilityRadius, this.maxVisibilityRadius);
    float num3 = Vector3.Distance(playerPos, evaluatedBallPos);
    bool flag1 = (double) num3 < (double) num2;
    float num4 = MathUtils.MapRange((float) num1, 0.0f, this.minVisibilitySpeed, this.maxVisibilityAngle / 2f, this.minVisibilityAngle / 2f);
    Vector3 to = evaluatedBallPos - playerPos;
    bool flag2 = (double) Vector3.Angle(playerFwd, to) < (double) num4;
    float num5 = MathUtils.MapRange((float) num1, 0.0f, this.minProximitySpeed, this.maxProximityRadius, this.minProximityRadius);
    bool flag3 = (double) num3 < (double) num5;
    return flag1 & flag2 | flag3;
  }

  public bool IsBallPositionCandidateInsideForwardVisibilityRadius(
    Vector3 playerPos,
    Vector3 playerFwd,
    Vector3 playerUnscaledVelocity,
    Vector3 evaluatedBallPos)
  {
    playerPos.Scale(VisibilityAreaConfig._planeXZ);
    playerFwd.Scale(VisibilityAreaConfig._planeXZ);
    playerFwd.Normalize();
    evaluatedBallPos.Scale(VisibilityAreaConfig._planeXZ);
    playerUnscaledVelocity.Scale(VisibilityAreaConfig._planeXZ);
    double num1 = (double) Vector3.Dot(playerFwd, playerUnscaledVelocity);
    float num2 = MathUtils.MapRange((float) num1, 0.0f, this.maxVisibilitySpeed, this.minVisibilityRadius, this.maxVisibilityRadius);
    bool flag1 = (double) Vector3.Distance(playerPos, evaluatedBallPos) < (double) num2;
    float num3 = MathUtils.MapRange((float) num1, 0.0f, this.minVisibilitySpeed, this.maxVisibilityAngle / 2f, this.minVisibilityAngle / 2f);
    Vector3 to = evaluatedBallPos - playerPos;
    bool flag2 = (double) Vector3.Angle(playerFwd, to) < (double) num3;
    return flag1 & flag2;
  }
}
