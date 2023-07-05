// Decompiled with JetBrains decompiler
// Type: PlayAssignment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class PlayAssignment
{
  protected EPlayAssignmentId assignmentId;
  protected RouteGraphicData routeGraphicData;
  protected float[] playRoute;

  public PlayAssignment(
    EPlayAssignmentId playAssignId,
    RouteGraphicData graphicData,
    float[] routeArray)
  {
    this.assignmentId = playAssignId;
    this.routeGraphicData = graphicData;
    this.playRoute = routeArray;
  }

  public PlayAssignment(PlayAssignment copyPlayAssign)
  {
    if (copyPlayAssign == null)
      return;
    this.assignmentId = copyPlayAssign.assignmentId;
    this.routeGraphicData = copyPlayAssign.routeGraphicData;
    this.playRoute = copyPlayAssign.playRoute;
  }

  public EPlayAssignmentId GetAssignmentType() => this.assignmentId;

  public RouteGraphicData GetRouteGraphicData() => this.routeGraphicData;

  public float[] GetRoutePoints() => this.playRoute;
}
