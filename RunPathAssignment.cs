// Decompiled with JetBrains decompiler
// Type: RunPathAssignment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunPathAssignment : PlayAssignment
{
  protected List<RouteLeg> behaviorRoute;

  public RunPathAssignment(
    EPlayAssignmentId playAssignId,
    RouteGraphicData graphicData,
    float[] routeArray)
    : base(playAssignId, graphicData, routeArray)
  {
    this.behaviorRoute = new List<RouteLeg>();
    if (routeArray != null)
    {
      for (int index = 1; index < routeArray.Length; index += 3)
        this.behaviorRoute.Add(new RouteLeg(routeArray[index], routeArray[index + 1], routeArray[index + 2]));
    }
    else
      Debug.LogError((object) "Routearray is null for RunPathAssignment -- no path created");
  }

  public RunPathAssignment(RunPathAssignment copyAssign)
    : base((PlayAssignment) copyAssign)
  {
    if (copyAssign == null)
      return;
    this.behaviorRoute = new List<RouteLeg>();
    this.behaviorRoute.AddRange((IEnumerable<RouteLeg>) copyAssign.behaviorRoute);
  }

  public List<RouteLeg> GetRoute() => this.behaviorRoute;
}
