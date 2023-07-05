// Decompiled with JetBrains decompiler
// Type: TeamTypeDescriptionStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TB12;
using UnityEngine;

[CreateAssetMenu(menuName = "ProEra/Stores/TeamTypeDescriptionStore", fileName = "TeamTypeDescriptionStore")]
[AppStore]
public class TeamTypeDescriptionStore : ScriptableObject
{
  public List<ETeamTypes> TeamTypeDescriptionKeys;
  public List<global::TeamTypeDescriptionValues> TeamTypeDescriptionValues;

  public string GetTeamTypeDescription(ETeamTypes type)
  {
    List<string> values = this.TeamTypeDescriptionValues[this.TeamTypeDescriptionKeys.IndexOf(type)].values;
    return values[Random.Range(0, values.Count)];
  }
}
