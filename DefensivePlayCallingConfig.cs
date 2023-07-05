// Decompiled with JetBrains decompiler
// Type: DefensivePlayCallingConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class DefensivePlayCallingConfig : MonoBehaviour, IGameplayConfig
{
  [Tooltip("Certain man coverage concepts will only get used if the line of scrimmage is within this distance from the goal line.")]
  public float maxYardsFromGoalLineForManCoverage = 3f;
  [Tooltip("If there are at least this many receivers in the offensive formation, the defense will come out in dime.")]
  public int minReceiversForDime = 5;
  [Tooltip("If there are at least this many receivers in the offensive formation (and less than 'Min Receivers For Dime', the defense will come out in nickel.")]
  public int minReceiversForNickel = 3;

  public float GetMaxDistanceFromGoalLineForManCoverage() => Field.ConvertYardsToDistance(this.maxYardsFromGoalLineForManCoverage);
}
