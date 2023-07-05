// Decompiled with JetBrains decompiler
// Type: CoachData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class CoachData
{
  [Key(0)]
  private string _firstName;
  [Key(1)]
  private string _lastName;
  [Key(2)]
  private int _skin;
  [Key(3)]
  private int _portrait;
  [Key(4)]
  private int _age;
  [Key(5)]
  private int _experience;
  [Key(6)]
  private int _salary;
  [Key(7)]
  private int _yearsRemainingOnContract;
  [Key(8)]
  private int _careerWins;
  [Key(9)]
  private int _careerLosses;
  [Key(10)]
  private int _playoffWins;
  [Key(11)]
  private int _playoffLosses;
  [Key(12)]
  private int _championships;
  [Key(13)]
  private int _playoffAppearances;
  [Key(14)]
  private int _offense;
  [Key(15)]
  private int _defense;
  [Key(16)]
  private int _evaluation;
  [Key(17)]
  private int _motivation;
  [Key(18)]
  private int _development;
  [Key(19)]
  private int _discipline;
  [Key(20)]
  private int _potential;
  [Key(21)]
  private CoachPositions _position;
  [Key(22)]
  private int _badge1;
  [Key(23)]
  private int _badge2;
  [Key(24)]
  private int _badge3;
  [Key(25)]
  private int _badge4;

  [IgnoreMember]
  public string FirstName
  {
    get => this._firstName;
    set => this._firstName = value;
  }

  [IgnoreMember]
  public string LastName
  {
    get => this._lastName;
    set => this._lastName = value;
  }

  [IgnoreMember]
  public string FullName => this._firstName + " " + this._lastName;

  [IgnoreMember]
  public string FirstInitalAndLastName => this._firstName.Substring(0, 1) + ". " + this._lastName;

  [IgnoreMember]
  public int Skin
  {
    get => this._skin;
    set => this._skin = value;
  }

  [IgnoreMember]
  public int Portrait
  {
    get => this._portrait;
    set => this._portrait = value;
  }

  [IgnoreMember]
  public int Age
  {
    get => this._age;
    set => this._age = value;
  }

  [IgnoreMember]
  public int Experience
  {
    get => this._experience;
    set => this._experience = value;
  }

  [IgnoreMember]
  public int Salary
  {
    get => this._salary;
    set => this._salary = value;
  }

  [IgnoreMember]
  public int YearsRemainingOnContract
  {
    get => this._yearsRemainingOnContract;
    set => this._yearsRemainingOnContract = value;
  }

  [IgnoreMember]
  public int CareerWins
  {
    get => this._careerWins;
    set => this._careerWins = value;
  }

  [IgnoreMember]
  public int CareerLosses
  {
    get => this._careerLosses;
    set => this._careerLosses = value;
  }

  [IgnoreMember]
  public int PlayoffWins
  {
    get => this._playoffWins;
    set => this._playoffWins = value;
  }

  [IgnoreMember]
  public int PlayoffLosses
  {
    get => this._playoffLosses;
    set => this._playoffLosses = value;
  }

  [IgnoreMember]
  public int Championships
  {
    get => this._championships;
    set => this._championships = value;
  }

  [IgnoreMember]
  public int PlayoffAppearances
  {
    get => this._playoffAppearances;
    set => this._playoffAppearances = value;
  }

  [IgnoreMember]
  public int Offense
  {
    get => this._offense;
    set => this._offense = value;
  }

  [IgnoreMember]
  public int Defense
  {
    get => this._defense;
    set => this._defense = value;
  }

  [IgnoreMember]
  public int Evaluation
  {
    get => this._evaluation;
    set => this._evaluation = value;
  }

  [IgnoreMember]
  public int Motivation
  {
    get => this._motivation;
    set => this._motivation = value;
  }

  [IgnoreMember]
  public int Development
  {
    get => this._development;
    set => this._development = value;
  }

  [IgnoreMember]
  public int Discipline
  {
    get => this._discipline;
    set => this._discipline = value;
  }

  [IgnoreMember]
  public int Potential
  {
    get => this._potential;
    set => this._potential = value;
  }

  [IgnoreMember]
  public CoachPositions CoachPosition
  {
    get => this._position;
    set => this._position = value;
  }

  [IgnoreMember]
  public int Badge1
  {
    get => this._badge1;
    set => this._badge1 = value;
  }

  [IgnoreMember]
  public int Badge2
  {
    get => this._badge2;
    set => this._badge2 = value;
  }

  [IgnoreMember]
  public int Badge3
  {
    get => this._badge3;
    set => this._badge3 = value;
  }

  [IgnoreMember]
  public int Badge4
  {
    get => this._badge4;
    set => this._badge4 = value;
  }

  public void EarnBadge(CoachBadgeName badge)
  {
    if (this.Badge1 == -1)
      this.Badge1 = (int) badge;
    else if (this.Badge2 == -1)
      this.Badge2 = (int) badge;
    else if (this.Badge3 == -1)
    {
      this.Badge3 = (int) badge;
    }
    else
    {
      if (this.Badge4 != -1)
        return;
      this.Badge4 = (int) badge;
    }
  }

  public bool HasBadge(CoachBadgeName badge, bool validate = false)
  {
    int num = (int) badge;
    bool flag = false;
    if (this.Badge1 == num || this.Badge2 == num || this.Badge3 == num || this.Badge4 == num)
      flag = true;
    if (!validate)
      return flag;
    return flag && this.DoesCoachMeetBadgeRequiredRole(BadgeDatabase.GetCoachBadge((int) badge));
  }

  public bool DoesCoachMeetBadgeRequiredRole(CoachBadge badge)
  {
    CoachBadgeRequiredRole requiredRole = badge.requiredRole;
    bool higherRoleToActivate = badge.allowHigherRoleToActivate;
    if (requiredRole == CoachBadgeRequiredRole.Any)
      return true;
    return this.CoachPosition == CoachPositions.Head_Coach ? higherRoleToActivate || requiredRole == CoachBadgeRequiredRole.Head_Coach : (this.CoachPosition == CoachPositions.OFF_Coordinator ? requiredRole == CoachBadgeRequiredRole.Any_Coordinator || requiredRole == CoachBadgeRequiredRole.Off_Coordinator || higherRoleToActivate && (requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.QB_Coach || requiredRole == CoachBadgeRequiredRole.RB_Coach || requiredRole == CoachBadgeRequiredRole.Receivers_Coach || requiredRole == CoachBadgeRequiredRole.OL_Coach) : (this.CoachPosition == CoachPositions.DEF_Coordinator ? requiredRole == CoachBadgeRequiredRole.Any_Coordinator || requiredRole == CoachBadgeRequiredRole.Def_Coordinator || higherRoleToActivate && (requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.DL_Coach || requiredRole == CoachBadgeRequiredRole.LB_Coach || requiredRole == CoachBadgeRequiredRole.DB_Coach) : (this.CoachPosition == CoachPositions.ST_Coordinator ? requiredRole == CoachBadgeRequiredRole.Any_Coordinator || requiredRole == CoachBadgeRequiredRole.ST_Coordinator || higherRoleToActivate && (requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.QB_Coach || requiredRole == CoachBadgeRequiredRole.RB_Coach || requiredRole == CoachBadgeRequiredRole.Receivers_Coach || requiredRole == CoachBadgeRequiredRole.OL_Coach || requiredRole == CoachBadgeRequiredRole.DL_Coach || requiredRole == CoachBadgeRequiredRole.LB_Coach || requiredRole == CoachBadgeRequiredRole.DB_Coach) : (this.CoachPosition == CoachPositions.QB_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.QB_Coach : (this.CoachPosition == CoachPositions.RB_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.RB_Coach : (this.CoachPosition == CoachPositions.REC_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.Receivers_Coach : (this.CoachPosition == CoachPositions.OL_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.OL_Coach : (this.CoachPosition == CoachPositions.DL_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.DL_Coach : (this.CoachPosition == CoachPositions.LB_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.LB_Coach : (this.CoachPosition == CoachPositions.DB_Coach ? requiredRole == CoachBadgeRequiredRole.Any_Position_Coach || requiredRole == CoachBadgeRequiredRole.DB_Coach : (this.CoachPosition == CoachPositions.West_Scout ? requiredRole == CoachBadgeRequiredRole.Any_Scout || requiredRole == CoachBadgeRequiredRole.West_Scout : (this.CoachPosition == CoachPositions.Central_Scout ? requiredRole == CoachBadgeRequiredRole.Any_Scout || requiredRole == CoachBadgeRequiredRole.Central_Scout : (this.CoachPosition == CoachPositions.SouthEast_Scout ? requiredRole == CoachBadgeRequiredRole.Any_Scout || requiredRole == CoachBadgeRequiredRole.SouthEast_Scout : this.CoachPosition == CoachPositions.NorthEast_Scout && (requiredRole == CoachBadgeRequiredRole.Any_Scout || requiredRole == CoachBadgeRequiredRole.NorthEast_Scout))))))))))))));
  }

  public int GetNumberOfBadges()
  {
    int numberOfBadges = 0;
    if (this.Badge1 != -1)
      ++numberOfBadges;
    if (this.Badge2 != -1)
      ++numberOfBadges;
    if (this.Badge3 != -1)
      ++numberOfBadges;
    if (this.Badge4 != -1)
      ++numberOfBadges;
    return numberOfBadges;
  }

  public int GetOverall() => CoachData.GetOverall(this, this.CoachPosition);

  public static int GetOverall(CoachData coach, CoachPositions position)
  {
    float f = 0.0f;
    switch (position)
    {
      case CoachPositions.Head_Coach:
      case CoachPositions.Generic:
        f = f + (float) coach.Offense * 0.25f + (float) coach.Defense * 0.25f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.15f + (float) coach.Motivation * 0.15f + (float) coach.Discipline * 0.15f;
        if (coach.HasBadge(CoachBadgeName.Passing_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Backs_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Receiving_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.OLine_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.DLine_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Linebacker_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.DB_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Champion))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Players_Coach))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Offensive_Guru))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Defensive_Guru))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Special_Teams_Guru))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Mentor))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Locker_Room_Hero))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Inspirational_Leader))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Drill_Master))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Practice_Pro))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Play_Action_Pro))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Blitz_Pro))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.OFF_Coordinator:
        f = f + (float) coach.Offense * 0.6f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.1f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.1f;
        if (coach.HasBadge(CoachBadgeName.Passing_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Backs_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Receiving_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Offensive_Guru))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Drill_Master))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Practice_Pro))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Play_Action_Pro))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.DEF_Coordinator:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.6f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.1f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.1f;
        if (coach.HasBadge(CoachBadgeName.DLine_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Linebacker_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.DB_Expert))
          f += 2f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Defensive_Guru))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Drill_Master))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Practice_Pro))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Blitz_Pro))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.ST_Coordinator:
        f = f + (float) coach.Offense * 0.2f + (float) coach.Defense * 0.2f + (float) coach.Evaluation * 0.15f + (float) coach.Development * 0.15f + (float) coach.Motivation * 0.15f + (float) coach.Discipline * 0.15f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Special_Teams_Guru))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Drill_Master))
          f += 3f;
        if (coach.HasBadge(CoachBadgeName.Practice_Pro))
          f += 5f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.QB_Coach:
        f = f + (float) coach.Offense * 0.5f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Passing_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.RB_Coach:
        f = f + (float) coach.Offense * 0.5f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Backs_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.REC_Coach:
        f = f + (float) coach.Offense * 0.5f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Receiving_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.OL_Coach:
        f = f + (float) coach.Offense * 0.5f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.OLine_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.DL_Coach:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.5f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.DLine_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.LB_Coach:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.5f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Linebacker_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.DB_Coach:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.5f + (float) coach.Evaluation * 0.05f + (float) coach.Development * 0.25f + (float) coach.Motivation * 0.1f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.DB_Expert))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Cultivator))
          f += 4f;
        if (coach.HasBadge(CoachBadgeName.Film_Studies))
        {
          f += 4f;
          break;
        }
        break;
      case CoachPositions.West_Scout:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.6f + (float) coach.Development * 0.05f + (float) coach.Motivation * 0.2f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Western_Roots))
        {
          f += 5f;
          break;
        }
        break;
      case CoachPositions.Central_Scout:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.6f + (float) coach.Development * 0.05f + (float) coach.Motivation * 0.2f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Central_Roots))
        {
          f += 5f;
          break;
        }
        break;
      case CoachPositions.SouthEast_Scout:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.6f + (float) coach.Development * 0.05f + (float) coach.Motivation * 0.2f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Eastern_Roots))
        {
          f += 5f;
          break;
        }
        break;
      case CoachPositions.NorthEast_Scout:
        f = f + (float) coach.Offense * 0.05f + (float) coach.Defense * 0.05f + (float) coach.Evaluation * 0.6f + (float) coach.Development * 0.05f + (float) coach.Motivation * 0.2f + (float) coach.Discipline * 0.05f;
        if (coach.HasBadge(CoachBadgeName.Minor_League_Roots))
        {
          f += 5f;
          break;
        }
        break;
      default:
        Debug.Log((object) ("Unknown CoachPosition specified: " + position.ToString()));
        break;
    }
    if ((double) f > 99.0)
      f = 99f;
    return Mathf.RoundToInt(f);
  }

  public int GetAttributeByIndex(int attributeIndex)
  {
    switch (attributeIndex)
    {
      case 0:
        return this.GetOverall();
      case 1:
        return this.Offense;
      case 2:
        return this.Defense;
      case 3:
        return this.Evaluation;
      case 4:
        return this.Motivation;
      case 5:
        return this.Development;
      case 6:
        return this.Discipline;
      case 7:
        return this.GetNumberOfBadges();
      default:
        Debug.Log((object) ("Unknown attribute index: " + attributeIndex.ToString()));
        return 0;
    }
  }
}
