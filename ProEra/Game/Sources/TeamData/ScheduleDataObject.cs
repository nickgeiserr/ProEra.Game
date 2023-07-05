// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.ScheduleDataObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [CreateAssetMenu(menuName = "Data/Schedule")]
  public class ScheduleDataObject : ScriptableObject
  {
    [SerializeField]
    private ScheduleData[] schedules;

    public ScheduleData[] Schedules => this.schedules;

    public bool SetDataFromCsv(string csv)
    {
      Dictionary<string, List<ScheduleMatchupData>> baseScheduleFile = TeamAssetManager.ParseBaseScheduleFile(csv);
      if (!this.ValidateSchedule(baseScheduleFile))
      {
        Debug.LogError((object) "Format validation failed. Aborting conversion...");
        return false;
      }
      this.schedules = new ScheduleData[baseScheduleFile.Keys.Count];
      for (int index = 0; index < baseScheduleFile.Keys.Count; ++index)
      {
        string key = baseScheduleFile.Keys.ElementAt<string>(index);
        this.schedules[index] = new ScheduleData()
        {
          team = key,
          matchups = baseScheduleFile[key].ToArray()
        };
      }
      return true;
    }

    private bool ValidateSchedule(
      Dictionary<string, List<ScheduleMatchupData>> schedule)
    {
      bool flag1 = true;
      foreach (string key in schedule.Keys)
      {
        for (int index = 0; index < schedule[key].Count; ++index)
        {
          string matchup = schedule[key][index].matchup;
          if (!matchup.ToUpperInvariant().Contains("BYE"))
          {
            string str1 = matchup.TrimStart('@');
            if (string.CompareOrdinal(key, str1) == 0)
            {
              Debug.LogError((object) ("Schedule conflict: Week " + index.ToString() + " - " + key + " vs " + matchup));
              flag1 = false;
            }
            else if (!schedule.ContainsKey(str1))
            {
              Debug.LogError((object) ("schedule does not contain entry for team: " + str1));
              flag1 = false;
            }
            else
            {
              ScheduleMatchupData scheduleMatchupData = schedule[str1][index];
              bool flag2 = matchup.StartsWith('@');
              bool flag3 = scheduleMatchupData.matchup.StartsWith('@');
              string str2 = "[FORMAT ERROR]:  week " + (index + 1).ToString() + " [" + key + " vs " + str1 + "]: ";
              if (flag2 & flag3)
              {
                Debug.LogError((object) (str2 + "both teams set to AWAY"));
                flag1 = false;
              }
              else if (!flag2 && !flag3)
              {
                Debug.LogError((object) (str2 + "both teams set to HOME"));
                flag1 = false;
              }
            }
          }
        }
      }
      return flag1;
    }
  }
}
