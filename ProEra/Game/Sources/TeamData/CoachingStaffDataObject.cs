// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.CoachingStaffDataObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [CreateAssetMenu(menuName = "Data/CoachingStaff")]
  public class CoachingStaffDataObject : ScriptableObject
  {
    private static readonly Dictionary<string, CoachPositions> PositionsMap = new Dictionary<string, CoachPositions>()
    {
      {
        "Head Coach",
        CoachPositions.Head_Coach
      },
      {
        "OC",
        CoachPositions.OFF_Coordinator
      },
      {
        "DC",
        CoachPositions.DEF_Coordinator
      },
      {
        "ST Coordinator",
        CoachPositions.ST_Coordinator
      },
      {
        "QB Coach",
        CoachPositions.QB_Coach
      },
      {
        "RB Coach",
        CoachPositions.RB_Coach
      },
      {
        "Receivers Coach",
        CoachPositions.REC_Coach
      },
      {
        "OL Coach",
        CoachPositions.OL_Coach
      },
      {
        "DL Coach",
        CoachPositions.DL_Coach
      },
      {
        "LB Coach",
        CoachPositions.LB_Coach
      },
      {
        "DB Coach",
        CoachPositions.DB_Coach
      },
      {
        "West Scout",
        CoachPositions.West_Scout
      },
      {
        "Central Scout",
        CoachPositions.Central_Scout
      },
      {
        "East Scout",
        CoachPositions.SouthEast_Scout
      },
      {
        "Minor League Scout",
        CoachPositions.NorthEast_Scout
      }
    };
    [SerializeField]
    private CoachingStaffData[] coaches;

    public void SetDataFromCsv(string csv)
    {
      string[] array1 = ((IEnumerable<string>) csv.Trim().Split('\n', StringSplitOptions.None)).Skip<string>(1).ToArray<string>();
      this.coaches = new CoachingStaffData[array1.Length];
      for (int index = 0; index < array1.Length; ++index)
      {
        this.coaches[index] = new CoachingStaffData();
        string[] array2 = ((IEnumerable<string>) array1[index].Split(',', StringSplitOptions.None)).ToArray<string>();
        this.coaches[index].role = array2[0].Trim();
        this.coaches[index].firstName = array2[1].Trim();
        this.coaches[index].lastName = array2[2].Trim();
        this.coaches[index].skin = int.Parse(array2[3]);
        this.coaches[index].portrait = int.Parse(array2[4]);
        this.coaches[index].age = int.Parse(array2[5]);
        this.coaches[index].experience = int.Parse(array2[6]);
        this.coaches[index].offense = int.Parse(array2[7]);
        this.coaches[index].defense = int.Parse(array2[8]);
        this.coaches[index].evaluation = int.Parse(array2[9]);
        this.coaches[index].motivation = int.Parse(array2[10]);
        this.coaches[index].development = int.Parse(array2[11]);
        this.coaches[index].discipline = int.Parse(array2[12]);
        this.coaches[index].badge1 = int.Parse(array2[13]);
        this.coaches[index].badge2 = int.Parse(array2[14]);
        this.coaches[index].badge3 = int.Parse(array2[15]);
      }
    }

    public CoachData[] ToCoachData() => ((IEnumerable<CoachingStaffData>) this.coaches).Select<CoachingStaffData, CoachData>((Func<CoachingStaffData, CoachData>) (x => new CoachData()
    {
      CoachPosition = CoachingStaffDataObject.PositionsMap[x.role],
      FirstName = x.firstName,
      LastName = x.lastName,
      Skin = x.skin,
      Portrait = x.portrait,
      Age = x.age,
      Experience = x.experience,
      Offense = x.offense,
      Defense = x.defense,
      Evaluation = x.evaluation,
      Motivation = x.motivation,
      Development = x.development,
      Discipline = x.discipline,
      Badge1 = x.badge1,
      Badge2 = x.badge2,
      Badge3 = x.badge3
    })).ToArray<CoachData>();
  }
}
