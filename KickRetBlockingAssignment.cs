// Decompiled with JetBrains decompiler
// Type: KickRetBlockingAssignment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class KickRetBlockingAssignment : RunPathAssignment
{
  public EKickRetBlockerType blockerType { get; private set; }

  public KickRetBlockingAssignment(
    RouteGraphicData graphicData,
    float[] r,
    EKickRetBlockerType type)
    : base(EPlayAssignmentId.KickReturnBlocker, graphicData, r)
  {
    this.blockerType = type;
  }

  public KickRetBlockingAssignment(KickRetBlockingAssignment copyAssign)
    : base((RunPathAssignment) copyAssign)
  {
    if (copyAssign == null)
      return;
    this.blockerType = copyAssign.blockerType;
  }

  public bool ShouldWaitForCatchBeforeBlocking() => this.blockerType == EKickRetBlockerType.UpBlocker;

  public bool ShouldImmediatelyChaseBallAfterKick() => this.blockerType == EKickRetBlockerType.OnsideBlocker;
}
