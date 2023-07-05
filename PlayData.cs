// Decompiled with JetBrains decompiler
// Type: PlayData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

[Serializable]
public class PlayData
{
  protected PlayAssignment[] routes;
  private string playName;
  private FormationPositions formation;
  private PlayConcept playConcept;

  public PlayData()
  {
  }

  public PlayData(FormationPositions f, string playN, PlayConcept concept)
  {
    this.formation = f;
    this.playName = playN;
    this.playConcept = concept;
  }

  public float[] GetRoute(int i) => this.routes[i].GetRoutePoints();

  public PlayAssignment GetRouteData(int i) => this.routes[i];

  public RouteGraphicData GetRouteGraphicData(int i) => this.routes[i].GetRouteGraphicData();

  public int GetAssignmentType(int i) => (int) this.routes[i].GetAssignmentType();

  public FormationPositions GetFormation() => this.formation;

  public string GetPlayName() => this.playName;

  public PlayConcept GetPlayConcept() => this.playConcept;

  public string GetPlayConceptString() => Common.EnumToString(this.playConcept.ToString());

  public int GetPlayType()
  {
    Debug.Log((object) "Calling Play Type On PlayData Object. Find the call and typecast to PlayDataOff");
    return 99;
  }
}
