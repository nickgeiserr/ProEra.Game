// Decompiled with JetBrains decompiler
// Type: ContractCalculator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ContractCalculator
{
  public static int leagueMinimumSalary = 500000;
  private static int maxSalary_QB = 30000000;
  private static int maxSalary_RB = 15000000;
  private static int maxSalary_WR = 18000000;
  private static int maxSalary_OL = 12000000;
  private static int maxSalary_TE = 10000000;
  private static int maxSalary_DL = 20000000;
  private static int maxSalary_LB = 15000000;
  private static int maxSalary_DB = 17000000;
  private static int maxSalary_K = 5000000;
  private static int maxSalary_P = 5000000;
  private static int maxSalary_HeadCoach = 10000000;
  private static int maxSalary_Coordinator = 5000000;
  private static int maxSalary_PositionCoach = 2000000;
  private static int maxSalary_Scout = 2000000;
  private static float[] salaryAdjustmentByDraftRound = new float[8]
  {
    1f,
    0.65f,
    0.55f,
    0.5f,
    0.45f,
    0.4f,
    0.35f,
    0.35f
  };
  private static int[] salaryDistribution = new int[35]
  {
    100,
    99,
    97,
    95,
    93,
    90,
    87,
    84,
    80,
    75,
    70,
    64,
    58,
    52,
    46,
    40,
    35,
    30,
    26,
    22,
    18,
    15,
    12,
    10,
    9,
    8,
    7,
    6,
    5,
    4,
    4,
    3,
    3,
    2,
    1
  };

  private static int GetMaxSalaryForPosition(Position p)
  {
    switch (p)
    {
      case Position.QB:
        return ContractCalculator.maxSalary_QB;
      case Position.RB:
        return ContractCalculator.maxSalary_RB;
      case Position.WR:
        return ContractCalculator.maxSalary_WR;
      case Position.TE:
        return ContractCalculator.maxSalary_TE;
      case Position.OL:
        return ContractCalculator.maxSalary_OL;
      case Position.K:
        return ContractCalculator.maxSalary_K;
      case Position.P:
        return ContractCalculator.maxSalary_P;
      case Position.DL:
        return ContractCalculator.maxSalary_DL;
      case Position.LB:
        return ContractCalculator.maxSalary_LB;
      case Position.DB:
        return ContractCalculator.maxSalary_DB;
      default:
        Debug.Log((object) ("Unknown position specified for GetMaxSalaryForPosition: " + p.ToString()));
        return 0;
    }
  }

  private static int GetMaxSalaryForCoach(CoachPositions p)
  {
    switch (p)
    {
      case CoachPositions.Head_Coach:
        return ContractCalculator.maxSalary_HeadCoach;
      case CoachPositions.OFF_Coordinator:
      case CoachPositions.DEF_Coordinator:
      case CoachPositions.ST_Coordinator:
        return ContractCalculator.maxSalary_Coordinator;
      case CoachPositions.QB_Coach:
      case CoachPositions.RB_Coach:
      case CoachPositions.REC_Coach:
      case CoachPositions.OL_Coach:
      case CoachPositions.DL_Coach:
      case CoachPositions.LB_Coach:
      case CoachPositions.DB_Coach:
        return ContractCalculator.maxSalary_PositionCoach;
      case CoachPositions.West_Scout:
      case CoachPositions.Central_Scout:
      case CoachPositions.SouthEast_Scout:
      case CoachPositions.NorthEast_Scout:
        return ContractCalculator.maxSalary_Scout;
      default:
        Debug.Log((object) ("Unknown coach position specified for GetMaxSalaryForCoach: " + p.ToString()));
        return 0;
    }
  }

  public static int GetDesiredYearlySalary(PlayerData player, bool addVariance)
  {
    if (player == null)
    {
      Debug.Log((object) "Attempting to get desired salary from a player that has been removed from team.");
      return 0;
    }
    int overall = player.GetOverall();
    int yearsPro = player.YearsPro;
    int age = player.Age;
    int salaryForPosition = ContractCalculator.GetMaxSalaryForPosition(player.PlayerPosition);
    int index = Mathf.Clamp(99 - overall, 0, ContractCalculator.salaryDistribution.Length - 1);
    int leagueMinimumSalary = Mathf.RoundToInt((float) salaryForPosition * ((float) ContractCalculator.salaryDistribution[index] * 0.01f));
    if (leagueMinimumSalary < ContractCalculator.leagueMinimumSalary)
      leagueMinimumSalary = ContractCalculator.leagueMinimumSalary;
    int num1 = 50000 * yearsPro;
    int num2 = leagueMinimumSalary + num1;
    if (yearsPro == 0)
      num2 = Mathf.RoundToInt((float) num2 * ContractCalculator.salaryAdjustmentByDraftRound[player.RoundDrafted]);
    if (addVariance)
    {
      int maxExclusive = Mathf.RoundToInt((float) num2 * 0.1f);
      num2 += Random.Range(maxExclusive * -1, maxExclusive);
    }
    int desiredYearlySalary = Mathf.RoundToInt((float) num2 / 100000f) * 100000;
    if (desiredYearlySalary < ContractCalculator.leagueMinimumSalary)
      desiredYearlySalary = ContractCalculator.leagueMinimumSalary;
    if (yearsPro == 0)
      desiredYearlySalary = Mathf.RoundToInt((float) desiredYearlySalary * 0.6f);
    return desiredYearlySalary;
  }

  public static int GetDesiredCoachSalary(
    CoachData coach,
    CoachPositions position,
    bool addVariance = false)
  {
    int overall = CoachData.GetOverall(coach, position);
    int maxSalaryForCoach = ContractCalculator.GetMaxSalaryForCoach(position);
    int index = Mathf.Clamp(99 - overall, 0, ContractCalculator.salaryDistribution.Length - 1);
    int leagueMinimumSalary = Mathf.RoundToInt((float) maxSalaryForCoach * ((float) ContractCalculator.salaryDistribution[index] * 0.01f));
    if (leagueMinimumSalary < ContractCalculator.leagueMinimumSalary)
      leagueMinimumSalary = ContractCalculator.leagueMinimumSalary;
    int num1 = 20000 * coach.Experience;
    int num2 = leagueMinimumSalary + num1;
    if (addVariance)
    {
      int maxExclusive = Mathf.RoundToInt((float) num2 * 0.1f);
      num2 += Random.Range(maxExclusive * -1, maxExclusive);
    }
    int desiredCoachSalary = Mathf.RoundToInt((float) num2 / 100000f) * 100000;
    if (desiredCoachSalary < ContractCalculator.leagueMinimumSalary)
      desiredCoachSalary = ContractCalculator.leagueMinimumSalary;
    return desiredCoachSalary;
  }

  public static int GetDesiredYearsOnContract(int age, bool addVariance)
  {
    if (addVariance)
    {
      if (age < 25)
        return Random.Range(2, 5);
      if (age < 30)
        return Random.Range(3, 6);
      return age < 35 ? Random.Range(4, 7) : Random.Range(1, 8);
    }
    if (age < 25)
      return 3;
    if (age < 30)
      return 4;
    return 5;
  }

  public static string GetCurrencyDisplay_Long(int salary) => salary.ToString("C0");

  public static string GetCurrencyDisplay_Short(int salary)
  {
    if (Mathf.Abs(salary) <= 999999)
      return "$" + (Mathf.RoundToInt((float) salary / 1000f).ToString() + "K");
    string str1 = ((float) salary / 1000000f).ToString("F2");
    int length = str1.IndexOf('.');
    string str2 = str1.Substring(length + 1, 1);
    string str3 = str1.Substring(length + 1, 1);
    if (str2.Equals("0") && str3.Equals("0"))
      str1 = str1.Substring(0, length);
    else if (str3.Equals("0"))
      str1 = str1.Substring(0, str1.Length - 1);
    return "$" + (str1 + "M");
  }
}
