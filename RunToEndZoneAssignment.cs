﻿// Decompiled with JetBrains decompiler
// Type: RunToEndZoneAssignment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class RunToEndZoneAssignment : RunPathAssignment
{
  public RunToEndZoneAssignment(RouteGraphicData graphicData, float[] r)
    : base(EPlayAssignmentId.RunToEndZone, graphicData, r)
  {
  }
}