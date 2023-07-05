// Decompiled with JetBrains decompiler
// Type: StatTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stats/StatTracker")]
[Serializable]
public class StatTracker : ScriptableObject
{
  [SerializeField]
  private TrackedStat[] trackedStats;
  [SerializeField]
  private StatTracker[] subTrackers;

  public void SetStatByName<T>(string statAccess, T newStatValue, int subLimit = 0)
  {
    statAccess = string.Concat<char>(statAccess.Where<char>((Func<char, bool>) (c => !char.IsWhiteSpace(c)))).ToLower();
    foreach (TrackedStat trackedStat in this.trackedStats)
    {
      string lower = string.Concat<char>(trackedStat.StatAccessName.Where<char>((Func<char, bool>) (c => !char.IsWhiteSpace(c)))).ToLower();
      if (statAccess == lower)
        trackedStat.SetValue<T>(newStatValue);
    }
    if (subLimit <= 0)
      return;
    foreach (StatTracker subTracker in this.subTrackers)
      subTracker.SetStatByName<T>(statAccess, newStatValue, subLimit - 1);
  }

  public List<TrackedStat> GetStats(int subLayers = 0)
  {
    List<TrackedStat> stats = new List<TrackedStat>((IEnumerable<TrackedStat>) this.trackedStats);
    if (subLayers > 0)
    {
      for (int index = 0; index < this.subTrackers.Length; ++index)
      {
        StatTracker subTracker = this.subTrackers[index];
        stats.AddRange((IEnumerable<TrackedStat>) subTracker.GetStats(subLayers - 1));
      }
    }
    return stats;
  }
}
