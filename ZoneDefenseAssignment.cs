// Decompiled with JetBrains decompiler
// Type: ZoneDefenseAssignment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[Serializable]
public class ZoneDefenseAssignment : PlayAssignment
{
  public Vector3 homePos;
  public float top;
  public float left;
  public float bottom;
  public float right;
  private ZoneCoveragePrecedence primaryZoneCoveragePrecedence;
  private ZoneCoveragePrecedence secondaryZoneCoveragePrecedence;
  private bool shouldTurnAndChaseReceiver;
  private bool shouldStayDeeperThanDeepestReceiver;
  private bool shouldChargeQBIfOutsidePocket;
  private float maxDistanceFromBackOfEndZone;

  public ZoneDefenseAssignment(
    RouteGraphicData graphicData,
    float[] deftypeTopLeftBottomRight,
    Vector3 homePos,
    ZoneCoverageParameters zoneParams)
    : base(EPlayAssignmentId.ZoneCoverage, graphicData, deftypeTopLeftBottomRight)
  {
    this.homePos = homePos;
    this.top = deftypeTopLeftBottomRight[1];
    this.left = deftypeTopLeftBottomRight[2];
    this.bottom = deftypeTopLeftBottomRight[3];
    this.right = deftypeTopLeftBottomRight[4];
    this.primaryZoneCoveragePrecedence = zoneParams.primaryPrecedence;
    this.secondaryZoneCoveragePrecedence = zoneParams.secondaryPrecedence;
    this.shouldTurnAndChaseReceiver = zoneParams.shouldTurnAndChaseReceiver;
    this.shouldStayDeeperThanDeepestReceiver = zoneParams.shouldStayDeeperThanDeepestReceiver;
    this.shouldChargeQBIfOutsidePocket = zoneParams.shouldChargeQBIfOutsidePocket;
    this.maxDistanceFromBackOfEndZone = zoneParams.maxDistanceFromBackOfEndZone;
  }

  public ZoneDefenseAssignment(ZoneDefenseAssignment copyDefAssign)
    : base((PlayAssignment) copyDefAssign)
  {
    this.homePos = copyDefAssign.homePos;
    this.top = copyDefAssign.top;
    this.left = copyDefAssign.left;
    this.bottom = copyDefAssign.bottom;
    this.right = copyDefAssign.right;
    this.primaryZoneCoveragePrecedence = copyDefAssign.primaryZoneCoveragePrecedence;
    this.secondaryZoneCoveragePrecedence = copyDefAssign.secondaryZoneCoveragePrecedence;
    this.shouldTurnAndChaseReceiver = copyDefAssign.shouldTurnAndChaseReceiver;
    this.shouldStayDeeperThanDeepestReceiver = copyDefAssign.shouldStayDeeperThanDeepestReceiver;
    this.shouldChargeQBIfOutsidePocket = copyDefAssign.shouldChargeQBIfOutsidePocket;
    this.maxDistanceFromBackOfEndZone = copyDefAssign.maxDistanceFromBackOfEndZone;
  }

  public ZoneCoveragePrecedence GetPrimaryZoneCoveragePrecedence() => this.primaryZoneCoveragePrecedence;

  public ZoneCoveragePrecedence GetSecondaryZoneCoveragePrecedence() => this.secondaryZoneCoveragePrecedence;

  public bool ShouldTurnAndChaseReceiver() => this.shouldTurnAndChaseReceiver;

  public bool ShouldStayDeeperThanDeepestReceiver() => this.shouldStayDeeperThanDeepestReceiver;

  public bool ShouldChargeQBIfOutsidePocket() => this.shouldChargeQBIfOutsidePocket;

  public float GetMaxDistanceFromBackOfEndZone() => this.maxDistanceFromBackOfEndZone;

  public float GetMostDownfieldCoveragePosition() => Field.OFFENSIVE_BACK_OF_ENDZONE - this.GetMaxDistanceFromBackOfEndZone() * (float) Game.OffensiveFieldDirection;

  public void SetZoneDefense(float ballOn, float flipVal)
  {
    ZoneCoverageConfig zoneCoverageConfig = Game.ZoneCoverageConfig;
    this.left *= flipVal;
    this.right *= flipVal;
    this.homePos.x *= flipVal;
    if (Field.MoreLeftOf(this.right, this.left))
    {
      float left = this.left;
      this.left = this.right;
      this.right = left;
    }
    this.top = ballOn + this.top * (float) Game.OffensiveFieldDirection;
    this.bottom = ballOn + this.bottom * (float) Game.OffensiveFieldDirection;
    this.homePos.z = ballOn + this.homePos.z * (float) Game.OffensiveFieldDirection;
    this.homePos.z = Field.LeastDownfield(this.homePos.z, this.GetMostDownfieldCoveragePosition());
    this.bottom = Field.LeastDownfield(this.bottom, this.homePos.z);
    Field.FurtherDownfield(this.bottom, Field.OFFENSIVE_GOAL_LINE);
  }

  public bool IsReceiverInDefendersZone(Vector3 receiverPosition) => Field.MoreRightOf(receiverPosition.x, this.right) && Field.MoreLeftOf(receiverPosition.x, this.left) && Field.FurtherDownfield(receiverPosition.z, this.bottom) && Field.FurtherDownfield(this.top, receiverPosition.z);
}
