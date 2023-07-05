// Decompiled with JetBrains decompiler
// Type: IAnimatorQuerries
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public interface IAnimatorQuerries
{
  bool IsQBInPocket();

  bool IsQBMovingInPocket();

  bool IsQBInPocketIdle();

  bool IsSprinting();

  bool IsStrafing();

  bool AllowPivot();

  bool IsInLocomotion();

  bool IsInInactivePlayLocomotion();

  bool IsInDefenderStrafeIdle();

  bool IsInIdle();

  bool IsInPivot();

  bool IsInPassBlockDropback();

  bool IsInQBUnderCenter();

  bool IsInQBShotgunIdle();

  bool IsQBReadyForSnap();

  bool IsInQBIdle();

  AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex);
}
