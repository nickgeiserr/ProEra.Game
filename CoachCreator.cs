// Decompiled with JetBrains decompiler
// Type: CoachCreator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class CoachCreator
{
  private static string[] firstNames;
  private static string[] lastNames;
  private static int badgeChance = 15;
  private static int totalBadges = 25;
  private static int[] offensiveBadges = new int[11]
  {
    0,
    1,
    2,
    3,
    8,
    9,
    10,
    20,
    21,
    22,
    23
  };
  private static int[] defensiveBadges = new int[11]
  {
    4,
    5,
    6,
    8,
    9,
    11,
    18,
    20,
    21,
    22,
    24
  };

  public static CoachData CreateCoach(CoachPositions coachPosition = CoachPositions.Generic)
  {
    CoachData coach = new CoachData()
    {
      CoachPosition = coachPosition,
      FirstName = CoachCreator.GetRandomFirstName(),
      LastName = CoachCreator.GetRandomLastName(),
      Skin = CoachCreator.GetSkin(),
      Portrait = CoachCreator.GetPortrait(),
      Age = CoachCreator.GetAge()
    };
    coach.Experience = CoachCreator.GetExperience(coach.Age);
    coach.Offense = CoachCreator.GetOffense(coachPosition);
    coach.Defense = CoachCreator.GetDefense(coachPosition);
    coach.Evaluation = CoachCreator.GetEvaluation(coachPosition);
    coach.Motivation = CoachCreator.GetMotivation(coachPosition);
    coach.Development = CoachCreator.GetDevelopment(coachPosition);
    coach.Discipline = CoachCreator.GetDiscipline(coachPosition);
    CoachCreator.GenerateBadges(coach);
    CoachCreator.SimulateCareer(coach);
    return coach;
  }

  private static void LoadNames()
  {
    CoachCreator.firstNames = TeamResourcesManager.LoadPlayerFirstNamesBank();
    CoachCreator.lastNames = TeamResourcesManager.LoadPlayerLastNameBank();
  }

  private static string GetRandomFirstName()
  {
    if (CoachCreator.firstNames == null)
      CoachCreator.LoadNames();
    return CoachCreator.firstNames[Random.Range(0, CoachCreator.firstNames.Length)];
  }

  private static string GetRandomLastName()
  {
    if (CoachCreator.lastNames == null)
      CoachCreator.LoadNames();
    return CoachCreator.lastNames[Random.Range(0, CoachCreator.lastNames.Length)];
  }

  private static int GetPortrait() => Random.Range(0, PortraitManager.NUMBER_OF_COACH_PORTRAITS_PER_SKIN_TYPE);

  private static int GetSkin() => Random.Range(1, 7);

  private static int GetAge() => Random.Range(30, 53);

  private static int GetExperience(int age) => Mathf.Max(1, age - Random.Range(28, 35));

  private static int GetOffense(CoachPositions position)
  {
    switch (position)
    {
      case CoachPositions.Head_Coach:
      case CoachPositions.OFF_Coordinator:
        return Random.Range(60, 95);
      case CoachPositions.DEF_Coordinator:
        return Random.Range(50, 70);
      case CoachPositions.ST_Coordinator:
        return Random.Range(50, 80);
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
        return Random.Range(65, 90);
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return Random.Range(50, 70);
      default:
        return Random.Range(60, 80);
    }
  }

  private static int GetDefense(CoachPositions position)
  {
    switch (position)
    {
      case CoachPositions.Head_Coach:
      case CoachPositions.DEF_Coordinator:
        return Random.Range(60, 95);
      case CoachPositions.OFF_Coordinator:
        return Random.Range(50, 70);
      case CoachPositions.ST_Coordinator:
        return Random.Range(50, 80);
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
        return Random.Range(50, 70);
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
        return Random.Range(70, 90);
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return Random.Range(50, 70);
      default:
        return Random.Range(60, 80);
    }
  }

  private static int GetEvaluation(CoachPositions position)
  {
    switch (position)
    {
      case CoachPositions.Head_Coach:
        return Random.Range(70, 85);
      case CoachPositions.OFF_Coordinator:
      case CoachPositions.DEF_Coordinator:
      case CoachPositions.ST_Coordinator:
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
        return Random.Range(65, 90);
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return Random.Range(75, 99);
      default:
        return Random.Range(60, 80);
    }
  }

  private static int GetMotivation(CoachPositions position)
  {
    switch (position)
    {
      case CoachPositions.Head_Coach:
      case CoachPositions.OFF_Coordinator:
      case CoachPositions.DEF_Coordinator:
      case CoachPositions.ST_Coordinator:
        return Random.Range(70, 85);
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return Random.Range(60, 90);
      default:
        return Random.Range(60, 80);
    }
  }

  private static int GetDevelopment(CoachPositions position)
  {
    switch (position)
    {
      case CoachPositions.Head_Coach:
      case CoachPositions.OFF_Coordinator:
      case CoachPositions.DEF_Coordinator:
      case CoachPositions.ST_Coordinator:
        return Random.Range(70, 95);
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
        return Random.Range(60, 90);
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return Random.Range(65, 95);
      default:
        return Random.Range(60, 80);
    }
  }

  private static int GetDiscipline(CoachPositions position)
  {
    switch (position)
    {
      case CoachPositions.Head_Coach:
      case CoachPositions.OFF_Coordinator:
      case CoachPositions.DEF_Coordinator:
      case CoachPositions.ST_Coordinator:
        return Random.Range(75, 95);
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return Random.Range(60, 90);
      default:
        return Random.Range(60, 80);
    }
  }

  private static void GenerateBadges(CoachData coach)
  {
    coach.Badge1 = -1;
    coach.Badge2 = -1;
    coach.Badge3 = -1;
    coach.Badge4 = -1;
    if (Random.Range(0, 100) >= CoachCreator.badgeChance)
      return;
    int num;
    switch (coach.CoachPosition)
    {
      case CoachPositions.Head_Coach:
        num = Random.Range(0, CoachCreator.totalBadges);
        break;
      case CoachPositions.OFF_Coordinator:
        num = CoachCreator.offensiveBadges[Random.Range(0, CoachCreator.offensiveBadges.Length)];
        break;
      case CoachPositions.DEF_Coordinator:
        num = CoachCreator.defensiveBadges[Random.Range(0, CoachCreator.defensiveBadges.Length)];
        break;
      case CoachPositions.ST_Coordinator:
        num = Random.Range(0, CoachCreator.totalBadges);
        break;
      case CoachPositions.QB_Coach:
        num = 0;
        break;
      case CoachPositions.RB_Coach:
        num = 1;
        break;
      case CoachPositions.REC_Coach:
        num = 2;
        break;
      case CoachPositions.OL_Coach:
        num = 3;
        break;
      case CoachPositions.DL_Coach:
        num = 4;
        break;
      case CoachPositions.LB_Coach:
        num = 5;
        break;
      case CoachPositions.DB_Coach:
        num = 6;
        break;
      case CoachPositions.West_Scout:
        num = 14;
        break;
      case CoachPositions.Central_Scout:
        num = 15;
        break;
      case CoachPositions.SouthEast_Scout:
        num = 16;
        break;
      case CoachPositions.NorthEast_Scout:
        num = 17;
        break;
      default:
        num = -1;
        break;
    }
    coach.Badge1 = num;
  }

  private static void SimulateCareer(CoachData coach)
  {
    float num1 = (float) (CoachData.GetOverall(coach, coach.CoachPosition) - Random.Range(20, 30)) / 100f;
    int num2 = 16;
    for (int index = 0; index < coach.Experience; ++index)
    {
      int num3 = Mathf.RoundToInt(num1 * (float) num2);
      coach.CareerWins += num3;
      coach.CareerLosses += num2 - num3;
      if (num3 > 10 && coach.PlayoffAppearances < 10)
      {
        ++coach.PlayoffAppearances;
        int num4 = Random.Range(0, 5);
        if (num4 == 4 && coach.Championships < 4)
          ++coach.Championships;
        else
          ++coach.PlayoffLosses;
        coach.PlayoffWins += num4;
      }
    }
  }
}
