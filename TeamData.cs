// Decompiled with JetBrains decompiler
// Type: TeamData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UDB;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class TeamData
{
  [Key(0)]
  public static string[] attributeAbbreviations = new string[17]
  {
    "SPD",
    "TLB",
    "FUM",
    "CAT",
    "BLK",
    "THA",
    "THP",
    "KAC",
    "KPW",
    "BLB",
    "TKL",
    "FIT",
    "AWR",
    "COV",
    "HTP",
    "END",
    "AGI"
  };
  [Key(1)]
  public static List<Position> ALL_GENERIC_POSITIONS = new List<Position>((IEnumerable<Position>) new Position[10]
  {
    Position.QB,
    Position.RB,
    Position.WR,
    Position.TE,
    Position.OL,
    Position.DL,
    Position.LB,
    Position.DB,
    Position.K,
    Position.P
  });
  [Key(2)]
  public static List<Position> ALL_DEPTH_CHART_POSITIONS = new List<Position>((IEnumerable<Position>) new Position[12]
  {
    Position.QB,
    Position.RB,
    Position.WR,
    Position.TE,
    Position.OL,
    Position.DL,
    Position.LB,
    Position.DB,
    Position.K,
    Position.P,
    Position.KR,
    Position.PR
  });
  [Key(3)]
  public static List<Position> LIST_OF_OFFENSIVE_POSITIONS = new List<Position>((IEnumerable<Position>) new Position[6]
  {
    Position.QB,
    Position.WR,
    Position.OL,
    Position.TE,
    Position.RB,
    Position.FB
  });
  [Key(4)]
  public static List<Position> LIST_OF_DEFENSIVE_POSITIONS = new List<Position>((IEnumerable<Position>) new Position[3]
  {
    Position.DL,
    Position.LB,
    Position.DB
  });
  [Key(5)]
  public static List<Position> LIST_OF_ST_POSITIONS = new List<Position>((IEnumerable<Position>) new Position[2]
  {
    Position.P,
    Position.K
  });
  [Key(6)]
  [Obsolete]
  public DepthChart SavedTeamDepthChart;
  private DepthChart _TeamDepthChart;
  [Key(7)]
  public CoachData[] CoachingStaff;
  [Key(8)]
  public RosterData MainRoster;
  [Key(9)]
  public RosterData PracticeSquad;
  [Key(10)]
  public TeamSeasonStats CurrentSeasonStats;
  [Key(11)]
  [Obsolete]
  public TeamFile SavedTeamFile;
  private TeamFile _teamFile;
  [Key(12)]
  public TeamPlayCalling PlayCalling;
  [Key(13)]
  public TeamGameStats CurrentGameStats;
  [Key(14)]
  public List<TeamSeasonStats> AllSeasonStats;
  [Key(15)]
  public CustomLogoData CustomLogo;
  [Key(16)]
  private float[] primaryColor;
  [Key(17)]
  private float[] secondaryColor;
  [Key(18)]
  private float[] alternateColor;
  [Key(19)]
  private string customEndzone = "";
  [Key(20)]
  private string customField = "";
  [Key(21)]
  public int TeamIndex;

  [IgnoreMember]
  public DepthChart TeamDepthChart
  {
    get
    {
      if (this._TeamDepthChart == null)
      {
        if (this.MainRoster.defaultPlayers == null)
        {
          if (this.TeamIndex < 0)
            this.TeamIndex = 2;
          this.MainRoster.defaultPlayers = TeamAssetManager.LoadDefaultPlayers(this.TeamIndex);
        }
        this._TeamDepthChart = new DepthChart(this.MainRoster, this.PracticeSquad, this.GetDefensivePlaybook(), this.MainRoster.defaultPlayers);
      }
      return this._TeamDepthChart;
    }
  }

  [IgnoreMember]
  public TeamFile teamFile
  {
    get
    {
      if (this._teamFile == null)
        this._teamFile = TeamAssetManager.LoadTeamFile(this.TeamIndex);
      return this._teamFile;
    }
  }

  [IgnoreMember]
  public int CurrentSeasonPassCompletions
  {
    get
    {
      int seasonPassCompletions = 0;
      for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
      {
        PlayerData player = this.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null)
          seasonPassCompletions += player.CurrentSeasonStats.QBCompletions;
      }
      return seasonPassCompletions;
    }
  }

  [IgnoreMember]
  public float CurrentSeasonPassCompletionPercentage
  {
    get
    {
      int num1 = 0;
      int num2 = 0;
      for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
      {
        PlayerData player = this.GetPlayer(playerIndex);
        if (player?.CurrentSeasonStats != null)
        {
          num1 += player.CurrentSeasonStats.QBAttempts;
          num2 += player.CurrentSeasonStats.QBCompletions;
        }
      }
      return num1 == 0 ? 0.0f : (float) (num2 / num1);
    }
  }

  [IgnoreMember]
  public float CareerPassCompletionPercentage
  {
    get
    {
      int num1 = 0;
      int num2 = 0;
      for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
      {
        PlayerData player = this.GetPlayer(playerIndex);
        if (player?.CareerStats != null)
        {
          num1 += player.CareerStats.Sum<PlayerStats>((Func<PlayerStats, int>) (stat => stat.QBAttempts));
          num2 += player.CareerStats.Sum<PlayerStats>((Func<PlayerStats, int>) (stat => stat.QBCompletions));
        }
      }
      return num1 == 0 ? 0.0f : (float) (num2 / num1);
    }
  }

  [IgnoreMember]
  public int CurrentSeasonTouchdownPasses
  {
    get
    {
      int seasonTouchdownPasses = 0;
      for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
      {
        PlayerData player = this.GetPlayer(playerIndex);
        if (player?.CurrentSeasonStats != null)
          seasonTouchdownPasses += player.CurrentSeasonStats.QBPassTDs;
      }
      return seasonTouchdownPasses;
    }
  }

  [IgnoreMember]
  public float HighestSeasonQbRating
  {
    get
    {
      float highestSeasonQbRating = 0.0f;
      for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
      {
        float qbRating = (float) this.GetPlayer(playerIndex).CurrentSeasonStats.GetQBRating();
        if ((double) qbRating > (double) highestSeasonQbRating)
          highestSeasonQbRating = qbRating;
      }
      return highestSeasonQbRating;
    }
  }

  public TeamData() => this.TeamIndex = -1;

  public static TeamData NewTeamData(int teamIndex)
  {
    TeamData teamData = new TeamData();
    teamData.TeamIndex = teamIndex;
    teamData._teamFile = TeamAssetManager.LoadTeamFile(teamIndex);
    teamData.PlayCalling = TeamAssetManager.LoadTeamPlayCalling(teamIndex);
    teamData.SetMainRoster(TeamAssetManager.LoadTeamRoster(teamIndex));
    teamData.SetPracticeSquad(new RosterData(TeamAssetManager.NUMBER_OF_PLAYERS_ON_PRACTICE_SQUAD));
    teamData.SetCoachingStaff(TeamAssetManager.LoadCoachingStaff(teamIndex));
    teamData._TeamDepthChart = new DepthChart(teamData.MainRoster, teamData.PracticeSquad, teamData.GetDefensivePlaybook(), teamData.MainRoster.defaultPlayers);
    teamData.CurrentSeasonStats = new TeamSeasonStats();
    teamData.CurrentSeasonStats.TeamIndex = teamData.TeamIndex;
    teamData.AllSeasonStats = new List<TeamSeasonStats>();
    return teamData;
  }

  public string GetAbbreviation() => this.teamFile.GetAbbreviation();

  public void SetAbbreviation(string value) => this.teamFile.SetAbbreviation(value);

  public string GetCity() => this.teamFile.GetCity();

  public void SetCity(string value) => this.teamFile.SetCity(value);

  public string GetName() => this.teamFile.GetName().ToUpper();

  public void SetName(string value) => this.teamFile.SetName(value);

  public string GetFullDisplayName() => this.teamFile.GetCity() + " " + this.teamFile.GetName();

  public List<int> GetRivals() => this.teamFile.GetRivals();

  public List<string> GetKeysToTheGame() => this.teamFile.GetKeysToTheGame();

  public float GetAvgBlitzPercent() => this.teamFile.GetAvgBlitzPercent();

  public float GetAvgManPercent() => this.teamFile.GetAvgManPercent();

  public float GetAvgPassPercent() => this.teamFile.GetAvgPassPercent();

  public float GetAvgRunPercent() => this.teamFile.GetAvgRunPercent();

  public float GetTeamHashLocation() => this.teamFile.GetCustomFieldHashLocation();

  public void SetTeamHashLocation(string value) => this.teamFile.SetTeamHashLocation(value);

  public float GetTeamPATLocation() => this.teamFile.GetCustomFieldPATLocation();

  public void SetTeamPATLocation(string value) => this.teamFile.SetTeamPATLocation(value);

  public string GetOffensivePlaybook() => this.teamFile.GetOffensivePlaybook();

  public string GetDefensivePlaybook() => this.teamFile.GetDefensivePlaybook();

  public void SetOffensivePlaybook(string value) => this.teamFile.SetOffensivePlaybook(value);

  public void SetDefensivePlaybook(string value) => this.teamFile.SetDefensivePlaybook(value);

  public string GetLeague() => this.teamFile.GetLeague();

  public Color GetPrimaryColor() => this.DoesTeamUseCustomColors() ? new Color(this.primaryColor[0], this.primaryColor[1], this.primaryColor[2]) : this.teamFile.GetPrimaryColor();

  public Color GetSecondaryColor() => this.DoesTeamUseCustomColors() ? new Color(this.secondaryColor[0], this.secondaryColor[1], this.secondaryColor[2]) : this.teamFile.GetSecondaryColor();

  public Color GetAlternateColor() => this.DoesTeamUseCustomColors() ? new Color(this.alternateColor[0], this.alternateColor[1], this.alternateColor[2]) : this.teamFile.GetAlternateColor();

  public void SetCustomColorScheme(Color _primary, Color _secondary, Color _alternate)
  {
    if (!this.DoesTeamUseCustomColors())
    {
      this.primaryColor = new float[3];
      this.secondaryColor = new float[3];
      this.alternateColor = new float[3];
    }
    this.primaryColor[0] = _primary.r;
    this.primaryColor[1] = _primary.g;
    this.primaryColor[2] = _primary.b;
    this.secondaryColor[0] = _secondary.r;
    this.secondaryColor[1] = _secondary.g;
    this.secondaryColor[2] = _secondary.b;
    this.alternateColor[0] = _alternate.r;
    this.alternateColor[1] = _alternate.g;
    this.alternateColor[2] = _alternate.b;
  }

  public void RestoreColorsToDefault()
  {
    this.primaryColor = (float[]) null;
    this.secondaryColor = (float[]) null;
    this.alternateColor = (float[]) null;
  }

  public bool DoesTeamUseCustomColors() => this.primaryColor != null;

  public Sprite GetLargeLogo() => this.CustomLogo == null ? AssetManager.GetLargeTeamLogo(this.TeamIndex) : TeamDataCache.GetCustomTeamLogo(this);

  public Sprite GetMediumLogo() => this.CustomLogo == null ? AssetManager.GetMediumTeamLogo(this.TeamIndex) : TeamDataCache.GetCustomTeamLogo(this);

  public Sprite GetSmallLogo()
  {
    Debug.Log((object) ("GetSmallLogo: " + this.CustomLogo?.ToString()));
    return this.CustomLogo == null ? AssetManager.GetSmallTeamLogo(this.TeamIndex) : TeamDataCache.GetCustomTeamLogo(this);
  }

  public Sprite GetTinyLogo() => this.CustomLogo == null ? AssetManager.GetLargeTeamLogo(this.TeamIndex) : TeamDataCache.GetCustomTeamLogo(this);

  public Texture2D GetMidfieldLogo() => this.CustomLogo == null ? AssetManager.GetLargeTeamLogo(this.TeamIndex).texture : TeamDataCache.GetCustomTeamLogo(this).texture;

  public void SetCustomLogo(CustomLogoData logoData) => this.CustomLogo = logoData;

  public Texture2D GetEndzoneGraphic()
  {
    if (!this.DoesTeamUseCustomEndzone())
      return AssetManager.GetEndzoneGraphic(this.TeamIndex);
    Texture2D texture2D = AddressablesData.instance.LoadAssetSync<Texture2D>(AddressablesData.CorrectingAssetKey("custom_endzones"), this.customEndzone);
    Texture2D targetTexture = new Texture2D(texture2D.width, texture2D.height, TextureFormat.ARGB32, false);
    Color32[] pixels32 = texture2D.GetPixels32();
    Color32[] color32Array = new Color32[pixels32.Length];
    Color32[] sourceLogoMap = pixels32;
    Color32[] targetLogoMap = color32Array;
    Color primaryColor = this.GetPrimaryColor();
    Color secondaryColor = this.GetSecondaryColor();
    Color alternateColor = this.GetAlternateColor();
    Color white = Color.white;
    return TextureUtility.ColorTexture(targetTexture, sourceLogoMap, targetLogoMap, primaryColor, secondaryColor, alternateColor, white);
  }

  public void SetCustomEndzone(string type) => this.customEndzone = type;

  public string GetCustomEndzoneName() => this.customEndzone;

  public bool DoesTeamUseCustomEndzone() => this.customEndzone != "";

  public bool DoesTeamUseCustomBuildInFieldTexture() => this.customField != "";

  public void SetCustomField(string value) => this.customField = value;

  public string GetCustomBuiltInFieldTextureName() => this.customField;

  public Texture2D GetCustomBuiltInFieldTexture() => AddressablesData.instance.LoadAssetSync<Texture2D>("fieldtextures", this.customField);

  public bool DoesTeamUseCustomExternalFieldTexture() => this.teamFile.GetCustomExternalFieldTextureName() != "";

  public string GetCustomExternalFieldTextureName() => this.teamFile.GetCustomExternalFieldTextureName();

  public Texture2D GetCustomExternalFieldTexture()
  {
    try
    {
      return ModManager.LoadCustomField(this.GetCustomExternalFieldTextureName());
    }
    catch (Exception ex)
    {
      Debug.Log((object) ex.Message);
      return (Texture2D) null;
    }
  }

  public void SetMainRoster(RosterData roster) => this.MainRoster = roster;

  public void SetPracticeSquad(RosterData roster) => this.PracticeSquad = roster;

  public PlayerData GetPlayer(int playerIndex) => this.MainRoster.GetPlayer(playerIndex);

  public PlayerData GetPlayerOnPracticeSquad(int playerIndex) => this.PracticeSquad.GetPlayer(playerIndex);

  public int GetNumberOfPlayersOnRoster() => this.MainRoster.GetNumberOfPlayers();

  public int GetNumberOfPlayersOnPracticeSquad() => this.PracticeSquad.GetNumberOfPlayers();

  public string GetAvatarID(int playerID) => this.MainRoster.GetPlayer(playerID).AvatarID;

  public int GetNumberOfInjuredPlayers()
  {
    int ofInjuredPlayers = 0;
    for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
    {
      PlayerData player = this.MainRoster.GetPlayer(playerIndex);
      if (player != null && player.CurrentInjury != null)
        ++ofInjuredPlayers;
    }
    if (this.PracticeSquad != null)
    {
      for (int playerIndex = 0; playerIndex < this.PracticeSquad.GetNumberOfPlayers(); ++playerIndex)
      {
        PlayerData player = this.PracticeSquad.GetPlayer(playerIndex);
        if (player != null && player.CurrentInjury != null)
          ++ofInjuredPlayers;
      }
    }
    return ofInjuredPlayers;
  }

  public static int[] GetAttributeOrder(Position p)
  {
    switch (p)
    {
      case Position.QB:
        return new int[17]
        {
          5,
          6,
          12,
          0,
          11,
          16,
          1,
          2,
          3,
          4,
          7,
          8,
          9,
          10,
          13,
          14,
          15
        };
      case Position.RB:
        return new int[17]
        {
          0,
          1,
          12,
          4,
          3,
          2,
          11,
          16,
          15,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.FB:
        return new int[17]
        {
          4,
          12,
          1,
          0,
          3,
          2,
          11,
          16,
          15,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.WR:
      case Position.SLT:
        return new int[17]
        {
          0,
          3,
          12,
          4,
          15,
          11,
          16,
          2,
          1,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.TE:
        return new int[17]
        {
          4,
          3,
          12,
          0,
          15,
          11,
          16,
          1,
          2,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.OL:
      case Position.LT:
      case Position.LG:
      case Position.C:
      case Position.RG:
      case Position.RT:
        return new int[17]
        {
          4,
          12,
          16,
          11,
          0,
          15,
          1,
          2,
          3,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.K:
        return new int[17]
        {
          7,
          8,
          12,
          0,
          1,
          2,
          3,
          4,
          5,
          6,
          9,
          10,
          11,
          13,
          14,
          15,
          16
        };
      case Position.P:
        return new int[17]
        {
          8,
          7,
          12,
          0,
          1,
          2,
          3,
          4,
          5,
          6,
          9,
          10,
          11,
          13,
          14,
          15,
          16
        };
      case Position.KR:
        return new int[17]
        {
          0,
          12,
          3,
          1,
          2,
          11,
          16,
          15,
          4,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.PR:
        return new int[17]
        {
          0,
          3,
          12,
          1,
          2,
          11,
          16,
          15,
          4,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      case Position.GUN:
        return new int[17]
        {
          0,
          12,
          13,
          10,
          16,
          11,
          15,
          9,
          14,
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8
        };
      case Position.DL:
      case Position.DT:
      case Position.NT:
      case Position.DE:
        return new int[17]
        {
          12,
          10,
          9,
          14,
          0,
          16,
          11,
          15,
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8,
          13
        };
      case Position.LB:
      case Position.OLB:
      case Position.ILB:
      case Position.MLB:
        return new int[17]
        {
          12,
          10,
          13,
          0,
          9,
          14,
          16,
          11,
          15,
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8
        };
      case Position.CB:
      case Position.FS:
      case Position.SS:
      case Position.DB:
        return new int[17]
        {
          12,
          13,
          0,
          10,
          16,
          11,
          15,
          9,
          14,
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8
        };
      case Position.BLK:
        return new int[17]
        {
          4,
          12,
          0,
          15,
          11,
          16,
          1,
          2,
          3,
          5,
          6,
          7,
          8,
          9,
          10,
          13,
          14
        };
      default:
        Debug.Log((object) ("Position not found for position: " + p.ToString()));
        return (int[]) null;
    }
  }

  public static string GetAttributeGradeFromNumber(int value)
  {
    if (value > 95)
      return "A+";
    if (value > 90)
      return "A";
    if (value > 85)
      return "B+";
    if (value > 80)
      return "B";
    if (value > 75)
      return "C+";
    if (value > 65)
      return "C";
    if (value > 55)
      return "D+";
    return value > 40 ? "D" : "F";
  }

  public void CreateNewRoster() => this.TeamDepthChart.SetDepthChartMainRoster(this.MainRoster);

  public void RestoreRosterToDefault()
  {
    this.SetMainRoster(TeamAssetManager.LoadTeamRoster(this.TeamIndex));
    this.TeamDepthChart.SetDepthChartMainRoster(this.MainRoster);
  }

  public int GetTotalPlayerSalariesForTeam()
  {
    int playerSalariesForTeam = 0;
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnRoster(); ++playerIndex)
      playerSalariesForTeam += this.GetPlayer(playerIndex).Salary;
    return playerSalariesForTeam;
  }

  public void SetInitialContracts()
  {
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      this.GetPlayer(playerIndex).Salary = ContractCalculator.GetDesiredYearlySalary(this.GetPlayer(playerIndex), false);
      this.GetPlayer(playerIndex).ContractLength = ContractCalculator.GetDesiredYearsOnContract(this.GetPlayer(playerIndex).Age, true);
    }
    this.SetYearsRemainingOnContract();
  }

  private void SetYearsRemainingOnContract()
  {
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnRoster(); ++playerIndex)
      this.GetPlayer(playerIndex).YearsRemainingOnContract = UnityEngine.Random.Range(0, this.GetPlayer(playerIndex).ContractLength) + 1;
  }

  public void SetYearsProFromAge()
  {
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnRoster(); ++playerIndex)
      this.GetPlayer(playerIndex).YearsPro = Mathf.Max(0, this.GetPlayer(playerIndex).Age - 22);
  }

  public float GetAverageAgeOfStarters()
  {
    int num1 = 0 + this.TeamDepthChart.GetStartingLT().Age + this.TeamDepthChart.GetStartingLG().Age + this.TeamDepthChart.GetStartingC().Age + this.TeamDepthChart.GetStartingRG().Age + this.TeamDepthChart.GetStartingRT().Age + this.TeamDepthChart.GetStartingTE().Age + this.TeamDepthChart.GetStartingRB().Age + this.TeamDepthChart.GetStartingFB().Age + this.TeamDepthChart.GetStartingWRX().Age + this.TeamDepthChart.GetStartingWRY().Age + this.TeamDepthChart.GetStartingWRZ().Age + this.TeamDepthChart.GetStartingQB().Age;
    int num2 = this.TeamDepthChart.NumberOfDLUsed != 3 ? num1 + this.TeamDepthChart.GetStartingLDE_43().Age + this.TeamDepthChart.GetStartingLDT().Age + this.TeamDepthChart.GetStartingRDT().Age + this.TeamDepthChart.GetStartingRDE_43().Age : num1 + this.TeamDepthChart.GetStartingLDE_34().Age + this.TeamDepthChart.GetStartingNT().Age + this.TeamDepthChart.GetStartingRDE_34().Age;
    return (float) ((this.TeamDepthChart.NumberOfLBUsed != 3 ? num2 + this.TeamDepthChart.GetStartingLOLB().Age + this.TeamDepthChart.GetStartingLILB().Age + this.TeamDepthChart.GetStartingRILB().Age + this.TeamDepthChart.GetStartingROLB().Age : num2 + this.TeamDepthChart.GetStartingWLB().Age + this.TeamDepthChart.GetStartingMLB().Age + this.TeamDepthChart.GetStartingSLB().Age) + this.TeamDepthChart.GetStartingLCB().Age + this.TeamDepthChart.GetStartingSS().Age + this.TeamDepthChart.GetStartingFS().Age + this.TeamDepthChart.GetStartingRCB().Age) / 24f;
  }

  public void SetCoachingStaff(CoachData[] _coachingStaff) => this.CoachingStaff = _coachingStaff;

  public CoachData GetCoach(int i) => this.CoachingStaff[i];

  public void SetCoach(CoachData newCoach, int coachIndex) => this.CoachingStaff[coachIndex] = newCoach;

  public int GetNumberOfCoachingPositions() => this.CoachingStaff.Length;

  public void ReleaseCoach(int coachIndex) => this.CoachingStaff[coachIndex] = (CoachData) null;

  public void SetInitialCoachInformation()
  {
    for (int position = 0; position < this.CoachingStaff.Length; ++position)
    {
      CoachData coach = this.CoachingStaff[position];
      coach.YearsRemainingOnContract = UnityEngine.Random.Range(3, 7);
      coach.Salary = ContractCalculator.GetDesiredCoachSalary(coach, (CoachPositions) position);
      int overall = CoachData.GetOverall(coach, (CoachPositions) position);
      int experience = coach.Experience;
      float num1 = (float) (overall - UnityEngine.Random.Range(15, 25)) / 100f;
      int num2 = 16;
      for (int index = 0; index < experience; ++index)
      {
        int num3 = Mathf.RoundToInt(num1 * (float) num2);
        coach.CareerWins += num3;
        coach.CareerLosses += num2 - num3;
        if (num3 > 10 && coach.PlayoffAppearances < 10)
        {
          ++coach.PlayoffAppearances;
          int num4 = UnityEngine.Random.Range(0, 5);
          if (num4 == 4 && coach.Championships < 4)
            ++coach.Championships;
          else
            ++coach.PlayoffLosses;
          coach.PlayoffWins += num4;
        }
      }
    }
  }

  public int GetTotalCoachSalariesForTeam()
  {
    int coachSalariesForTeam = 0;
    for (int index = 0; index < this.CoachingStaff.Length; ++index)
    {
      if (this.CoachingStaff[index] != null)
        coachSalariesForTeam += this.CoachingStaff[index].Salary;
    }
    return coachSalariesForTeam;
  }

  public static Position GetPositionFromString(string p)
  {
    if (p.IsEqual("QB"))
      return Position.QB;
    if (p.IsEqual("RB"))
      return Position.RB;
    if (p.IsEqual("WR"))
      return Position.WR;
    if (p.IsEqual("TE"))
      return Position.TE;
    if (p.IsEqual("OL"))
      return Position.OL;
    if (p.IsEqual("DL"))
      return Position.DL;
    if (p.IsEqual("LB"))
      return Position.LB;
    if (p.IsEqual("DB"))
      return Position.DB;
    if (p.IsEqual("K"))
      return Position.K;
    if (p.IsEqual("P"))
      return Position.P;
    Debug.Log((object) ("Found unknown position: " + p));
    return Position.QB;
  }

  private int FindOpenNumberForPosition(Position p)
  {
    int[] numArray;
    switch (p)
    {
      case Position.QB:
      case Position.K:
      case Position.P:
        numArray = new int[19]
        {
          1,
          2,
          3,
          4,
          5,
          6,
          7,
          8,
          9,
          10,
          11,
          12,
          13,
          14,
          15,
          16,
          17,
          18,
          19
        };
        break;
      case Position.RB:
      case Position.FB:
      case Position.CB:
      case Position.FS:
      case Position.SS:
      case Position.DB:
        numArray = new int[30]
        {
          20,
          21,
          22,
          23,
          24,
          25,
          26,
          27,
          28,
          29,
          30,
          31,
          32,
          33,
          34,
          35,
          36,
          37,
          38,
          39,
          40,
          41,
          42,
          43,
          44,
          45,
          46,
          47,
          48,
          49
        };
        break;
      case Position.WR:
      case Position.SLT:
        numArray = new int[20]
        {
          10,
          11,
          12,
          13,
          14,
          15,
          16,
          17,
          18,
          19,
          80,
          81,
          82,
          83,
          84,
          85,
          86,
          87,
          88,
          89
        };
        break;
      case Position.TE:
        numArray = new int[20]
        {
          40,
          41,
          42,
          43,
          44,
          45,
          46,
          47,
          48,
          49,
          80,
          81,
          82,
          83,
          84,
          85,
          86,
          87,
          88,
          89
        };
        break;
      case Position.OL:
      case Position.LT:
      case Position.LG:
      case Position.C:
      case Position.RG:
      case Position.RT:
        numArray = new int[30]
        {
          50,
          51,
          52,
          53,
          54,
          55,
          56,
          57,
          58,
          59,
          60,
          61,
          62,
          63,
          64,
          65,
          66,
          67,
          68,
          69,
          70,
          71,
          72,
          73,
          74,
          75,
          76,
          77,
          78,
          79
        };
        break;
      case Position.DL:
      case Position.DT:
      case Position.NT:
      case Position.DE:
        numArray = new int[40]
        {
          50,
          51,
          52,
          53,
          54,
          55,
          56,
          57,
          58,
          59,
          60,
          61,
          62,
          63,
          64,
          65,
          66,
          67,
          68,
          69,
          70,
          71,
          72,
          73,
          74,
          75,
          76,
          77,
          78,
          79,
          90,
          91,
          92,
          93,
          94,
          95,
          96,
          97,
          98,
          99
        };
        break;
      case Position.LB:
      case Position.OLB:
      case Position.ILB:
      case Position.MLB:
        numArray = new int[30]
        {
          40,
          41,
          42,
          43,
          44,
          45,
          46,
          47,
          48,
          49,
          50,
          51,
          52,
          53,
          54,
          55,
          56,
          57,
          58,
          59,
          90,
          91,
          92,
          93,
          94,
          95,
          96,
          97,
          98,
          99
        };
        break;
      default:
        Debug.Log((object) ("Position not found for position: " + p.ToString()));
        return 1;
    }
    for (int index = 0; index < numArray.Length; ++index)
    {
      int number = numArray[index];
      if (!this.IsJerseyNumberUsedOnTeam(number))
        return number;
    }
    return 1;
  }

  private bool IsJerseyNumberUsedOnTeam(int number)
  {
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      if (this.GetPlayer(playerIndex) != null && this.GetPlayer(playerIndex).Number == number)
        return true;
    }
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnPracticeSquad(); ++playerIndex)
    {
      if (this.GetPlayerOnPracticeSquad(playerIndex) != null && this.GetPlayerOnPracticeSquad(playerIndex).Number == number)
        return true;
    }
    return false;
  }

  public void SetPlayer(int index, PlayerData player) => this.MainRoster.SetPlayer(index, player);

  public void AddPlayerToMainRoster(PlayerData newPlayer, int indexOnRoster)
  {
    newPlayer.IndexOnTeam = indexOnRoster;
    if (this.IsJerseyNumberUsedOnTeam(newPlayer.Number))
      newPlayer.Number = this.FindOpenNumberForPosition(newPlayer.PlayerPosition);
    this.MainRoster.SetPlayer(indexOnRoster, newPlayer);
  }

  public void AddPlayerToPracticeSquad(PlayerData newPlayer, int indexOnRoster)
  {
    newPlayer.IndexOnTeam = indexOnRoster;
    if (this.IsJerseyNumberUsedOnTeam(newPlayer.Number))
      newPlayer.Number = this.FindOpenNumberForPosition(newPlayer.PlayerPosition);
    this.PracticeSquad.SetPlayer(indexOnRoster, newPlayer);
  }

  public int GetOverall(int playerIndex) => this.MainRoster.GetPlayer(playerIndex) == null ? 0 : this.MainRoster.GetOverall(playerIndex);

  public int FindWorstStarterAtPosition(Position p)
  {
    switch (p)
    {
      case Position.QB:
        return this.TeamDepthChart.GetStartingQBIndex();
      case Position.RB:
        return this.FindWorstPlayerInList(new int[2]
        {
          this.TeamDepthChart.GetStartingRBIndex(),
          this.TeamDepthChart.GetStartingFBIndex()
        });
      case Position.WR:
        return this.FindWorstPlayerInList(new int[2]
        {
          this.TeamDepthChart.GetStartingWRXIndex(),
          this.TeamDepthChart.GetStartingWRZIndex()
        });
      case Position.TE:
        return this.TeamDepthChart.GetStartingTEIndex();
      case Position.OL:
        return this.FindWorstPlayerInList(new int[5]
        {
          this.TeamDepthChart.GetStartingLTIndex(),
          this.TeamDepthChart.GetStartingLGIndex(),
          this.TeamDepthChart.GetStartingCIndex(),
          this.TeamDepthChart.GetStartingRGIndex(),
          this.TeamDepthChart.GetStartingRTIndex()
        });
      case Position.K:
        return this.TeamDepthChart.GetStartingKickerIndex();
      case Position.P:
        return this.TeamDepthChart.GetStartingPunterIndex();
      case Position.DL:
        return this.TeamDepthChart.NumberOfDLUsed == 3 ? this.FindWorstPlayerInList(new int[3]
        {
          this.TeamDepthChart.GetStartingLDEIndex_34(),
          this.TeamDepthChart.GetStartingNTIndex(),
          this.TeamDepthChart.GetStartingRDEIndex_34()
        }) : this.FindWorstPlayerInList(new int[4]
        {
          this.TeamDepthChart.GetStartingLDEIndex_43(),
          this.TeamDepthChart.GetStartingLDTIndex(),
          this.TeamDepthChart.GetStartingRDTIndex(),
          this.TeamDepthChart.GetStartingRDEIndex_43()
        });
      case Position.LB:
        return this.TeamDepthChart.NumberOfLBUsed == 3 ? this.FindWorstPlayerInList(new int[3]
        {
          this.TeamDepthChart.GetStartingWLBIndex(),
          this.TeamDepthChart.GetStartingMLBIndex(),
          this.TeamDepthChart.GetStartingSLBIndex()
        }) : this.FindWorstPlayerInList(new int[4]
        {
          this.TeamDepthChart.GetStartingLOLBIndex(),
          this.TeamDepthChart.GetStartingLILBIndex(),
          this.TeamDepthChart.GetStartingRILBIndex(),
          this.TeamDepthChart.GetStartingROLBIndex()
        });
      case Position.DB:
        return this.FindWorstPlayerInList(new int[4]
        {
          this.TeamDepthChart.GetStartingLCBIndex(),
          this.TeamDepthChart.GetStartingFSIndex(),
          this.TeamDepthChart.GetStartingSSIndex(),
          this.TeamDepthChart.GetStartingRCBIndex()
        });
      default:
        Debug.Log((object) ("Unknown position specified for FindWorstStarterAtPosition () in TeamAttributes.cs. Position: " + p.ToString()));
        return 0;
    }
  }

  private int FindWorstPlayerInList(int[] playerList)
  {
    int overall = this.GetOverall(playerList[0]);
    int worstPlayerInList = 0;
    for (int index = 1; index < playerList.Length; ++index)
    {
      if (this.GetOverall(playerList[index]) < overall)
      {
        overall = this.GetOverall(playerList[index]);
        worstPlayerInList = playerList[index];
      }
    }
    return worstPlayerInList;
  }

  public int FindWorstStarter(int[] excludedIndexList = null)
  {
    int num = 100;
    int worstStarter = this.TeamDepthChart.GetStartingWRBIndex();
    if (this.MainRoster.FindAnyMissingPositionsOnRoster() != Position.None)
      return this.MainRoster.FindWorstPlayer();
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingQBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingQBIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingQBIndex());
      worstStarter = this.TeamDepthChart.GetStartingQBIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRBIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingRBIndex());
      worstStarter = this.TeamDepthChart.GetStartingRBIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingFBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingFBIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingFBIndex());
      worstStarter = this.TeamDepthChart.GetStartingFBIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingWRXIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingWRXIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingWRXIndex());
      worstStarter = this.TeamDepthChart.GetStartingWRXIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingWRZIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingWRZIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingWRZIndex());
      worstStarter = this.TeamDepthChart.GetStartingWRZIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingTEIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingTEIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingTEIndex());
      worstStarter = this.TeamDepthChart.GetStartingTEIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLTIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLTIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingLTIndex());
      worstStarter = this.TeamDepthChart.GetStartingLTIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLGIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLGIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingLGIndex());
      worstStarter = this.TeamDepthChart.GetStartingLGIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingCIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingCIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingCIndex());
      worstStarter = this.TeamDepthChart.GetStartingCIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRGIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRGIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingRGIndex());
      worstStarter = this.TeamDepthChart.GetStartingRGIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRTIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRTIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingRTIndex());
      worstStarter = this.TeamDepthChart.GetStartingRTIndex();
    }
    if (this.TeamDepthChart.NumberOfDLUsed == 3)
    {
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLDEIndex_34()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLDEIndex_34()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingLDEIndex_34());
        worstStarter = this.TeamDepthChart.GetStartingLDEIndex_34();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingNTIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingNTIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingNTIndex());
        worstStarter = this.TeamDepthChart.GetStartingNTIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRDEIndex_34()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRDEIndex_34()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingRDEIndex_34());
        worstStarter = this.TeamDepthChart.GetStartingRDEIndex_34();
      }
    }
    else
    {
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLDEIndex_43()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLDEIndex_43()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingLDEIndex_43());
        worstStarter = this.TeamDepthChart.GetStartingLDEIndex_43();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLDTIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLDTIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingLDTIndex());
        worstStarter = this.TeamDepthChart.GetStartingLDTIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRDTIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRDTIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingRDTIndex());
        worstStarter = this.TeamDepthChart.GetStartingRDTIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRDEIndex_43()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRDEIndex_43()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingRDEIndex_43());
        worstStarter = this.TeamDepthChart.GetStartingRDEIndex_43();
      }
    }
    if (this.TeamDepthChart.NumberOfLBUsed == 3)
    {
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingWLBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingWLBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingWLBIndex());
        worstStarter = this.TeamDepthChart.GetStartingWLBIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingMLBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingMLBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingMLBIndex());
        worstStarter = this.TeamDepthChart.GetStartingMLBIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingSLBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingSLBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingSLBIndex());
        worstStarter = this.TeamDepthChart.GetStartingSLBIndex();
      }
    }
    else
    {
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLOLBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLOLBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingLOLBIndex());
        worstStarter = this.TeamDepthChart.GetStartingLOLBIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLILBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLILBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingLILBIndex());
        worstStarter = this.TeamDepthChart.GetStartingLILBIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRILBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRILBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingRILBIndex());
        worstStarter = this.TeamDepthChart.GetStartingRILBIndex();
      }
      if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingROLBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingROLBIndex()))
      {
        num = this.GetOverall(this.TeamDepthChart.GetStartingROLBIndex());
        worstStarter = this.TeamDepthChart.GetStartingROLBIndex();
      }
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingLCBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingLCBIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingLCBIndex());
      worstStarter = this.TeamDepthChart.GetStartingLCBIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingSSIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingSSIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingSSIndex());
      worstStarter = this.TeamDepthChart.GetStartingSSIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingFSIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingFSIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingFSIndex());
      worstStarter = this.TeamDepthChart.GetStartingFSIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingRCBIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingRCBIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingRCBIndex());
      worstStarter = this.TeamDepthChart.GetStartingRCBIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingKickerIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingKickerIndex()))
    {
      num = this.GetOverall(this.TeamDepthChart.GetStartingKickerIndex());
      worstStarter = this.TeamDepthChart.GetStartingKickerIndex();
    }
    if (this.IsNotInWorstStarterExclusionList(excludedIndexList, this.TeamDepthChart.GetStartingPunterIndex()) && num > this.GetOverall(this.TeamDepthChart.GetStartingPunterIndex()))
    {
      this.GetOverall(this.TeamDepthChart.GetStartingPunterIndex());
      worstStarter = this.TeamDepthChart.GetStartingPunterIndex();
    }
    return worstStarter;
  }

  private bool IsNotInWorstStarterExclusionList(int[] exclusionList, int playerIndex)
  {
    if (exclusionList == null)
      return true;
    for (int index = 0; index < exclusionList.Length; ++index)
    {
      if (exclusionList[index] == playerIndex)
        return false;
    }
    return true;
  }

  public Position FindWorstStartingPositionGroup()
  {
    int num = 100;
    Position startingPositionGroup = Position.QB;
    for (int index = 0; index < TeamData.ALL_GENERIC_POSITIONS.Count; ++index)
    {
      Position p = TeamData.ALL_GENERIC_POSITIONS[index];
      int ratingOfStarters = this.FindAverageRatingOfStarters(p);
      if (ratingOfStarters < num)
      {
        num = ratingOfStarters;
        startingPositionGroup = p;
      }
    }
    return startingPositionGroup;
  }

  public string FindTeamPositionNeeds()
  {
    PlayerData player = this.GetPlayer(this.FindWorstStarter());
    Position positionsOnRoster = this.MainRoster.FindAnyMissingPositionsOnRoster();
    Position position = player != null ? player.PlayerPosition : Position.None;
    Position startingPositionGroup = this.FindWorstStartingPositionGroup();
    if (positionsOnRoster != Position.None)
      return positionsOnRoster.ToString();
    if (position == Position.None)
      return startingPositionGroup.ToString();
    return position == startingPositionGroup ? position.ToString() : position.ToString() + ", " + startingPositionGroup.ToString();
  }

  public Position FindTeamPositionNeed() => this.GetPlayer(this.FindWorstStarter()).PlayerPosition;

  public int FindBestNonStarter()
  {
    int num = 0;
    int bestNonStarter = -1;
    for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
    {
      if (this.MainRoster.GetPlayer(playerIndex) != null && !this.TeamDepthChart.IsPlayerAStarter(playerIndex) && this.MainRoster.GetPlayer(playerIndex).CurrentInjury == null && this.GetOverall(playerIndex) > num)
      {
        num = this.GetOverall(playerIndex);
        bestNonStarter = playerIndex;
      }
    }
    return bestNonStarter;
  }

  public bool IsPlayerBetterThanAnExistingStarter(PlayerData newPlayer) => newPlayer.GetOverall() > this.GetPlayer(this.FindWorstStarterAtPosition(newPlayer.PlayerPosition)).GetOverall();

  public void AddLastYearsStatsToTotals()
  {
    if (this.AllSeasonStats == null)
      this.AllSeasonStats = new List<TeamSeasonStats>();
    this.AllSeasonStats.Add(this.CurrentSeasonStats);
  }

  public void CreateNewTeamSeasonStats()
  {
    this.CurrentSeasonStats = new TeamSeasonStats();
    this.CurrentSeasonStats.TeamIndex = this.TeamIndex;
  }

  public void CreateNewTeamGameStats() => this.CurrentGameStats = new TeamGameStats();

  public void CreateNewSeasonStatsForAllPlayers(int seasonYear)
  {
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      if (this.GetPlayer(playerIndex) != null)
        this.GetPlayer(playerIndex).CreateNewPlayerStatsForSeason(seasonYear, this.GetAbbreviation());
    }
    if (this.PracticeSquad == null)
      return;
    for (int playerIndex = 0; playerIndex < this.GetNumberOfPlayersOnPracticeSquad(); ++playerIndex)
    {
      if (this.GetPlayerOnPracticeSquad(playerIndex) != null)
        this.GetPlayerOnPracticeSquad(playerIndex).CreateNewPlayerStatsForSeason(seasonYear, this.GetAbbreviation());
    }
  }

  public void AddPlayerStatsFromGame()
  {
    for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
    {
      if (this.GetPlayer(playerIndex) != null)
        this.GetPlayer(playerIndex).AddStatsFromGameToSeasonTotal();
    }
  }

  public PlayerData GetPlayerWithMost_PassYards()
  {
    PlayerData withMostPassYards = (PlayerData) null;
    int num = -1;
    for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
    {
      if (this.GetPlayer(playerIndex) != null && this.GetPlayer(playerIndex).CurrentSeasonStats != null && this.GetPlayer(playerIndex).CurrentSeasonStats.QBPassYards > num)
      {
        withMostPassYards = this.GetPlayer(playerIndex);
        num = withMostPassYards.CurrentSeasonStats.QBPassYards;
      }
    }
    return withMostPassYards;
  }

  public PlayerData GetPlayerWithMost_RushYards()
  {
    PlayerData withMostRushYards = (PlayerData) null;
    int num = -1;
    for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
    {
      if (this.GetPlayer(playerIndex) != null && this.GetPlayer(playerIndex).CurrentSeasonStats != null && this.GetPlayer(playerIndex).CurrentSeasonStats.RushYards > num)
      {
        withMostRushYards = this.GetPlayer(playerIndex);
        num = withMostRushYards.CurrentSeasonStats.RushYards;
      }
    }
    return withMostRushYards;
  }

  public PlayerData GetPlayerWithMost_ReceivingYards()
  {
    PlayerData mostReceivingYards = (PlayerData) null;
    int num = -1;
    for (int playerIndex = 0; playerIndex < this.MainRoster.GetNumberOfPlayers(); ++playerIndex)
    {
      if (this.GetPlayer(playerIndex) != null && this.GetPlayer(playerIndex).CurrentSeasonStats != null && this.GetPlayer(playerIndex).CurrentSeasonStats.ReceivingYards > num)
      {
        mostReceivingYards = this.GetPlayer(playerIndex);
        num = mostReceivingYards.CurrentSeasonStats.ReceivingYards;
      }
    }
    return mostReceivingYards;
  }

  public int[] GetTeamRatings() => new int[4]
  {
    0,
    this.GetTeamRating_OFF(),
    this.GetTeamRating_DEF(),
    this.GetTeamRating_SPC()
  };

  public int GetTeamRating_OVR() => Mathf.RoundToInt((float) ((double) this.GetTeamRating_OFF() * 0.40000000596046448 + (double) this.GetTeamRating_DEF() * 0.40000000596046448 + (double) this.GetTeamRating_SPC() * 0.20000000298023224));

  public int GetTeamRating_OFF() => Mathf.RoundToInt((float) (this.FindAverageRatingOfStarters(Position.QB) * 2 + this.FindAverageRatingOfStarters(Position.RB) * 2 + this.FindAverageRatingOfStarters(Position.WR) + this.FindAverageRatingOfStarters(Position.OL) + this.FindAverageRatingOfStarters(Position.TE)) / 7f);

  public int GetTeamRating_DEF() => Mathf.RoundToInt((float) (this.FindAverageRatingOfStarters(Position.DL) + this.FindAverageRatingOfStarters(Position.LB) + this.FindAverageRatingOfStarters(Position.DB)) / 3f);

  public int GetTeamRating_SPC() => Mathf.RoundToInt((float) (this.FindAverageRatingOfStarters(Position.K) * 2 + this.FindAverageRatingOfStarters(Position.P) * 2 + this.FindAverageRatingOfStarters(Position.OL) + this.FindAverageRatingOfStarters(Position.DB) + this.FindAverageRatingOfStarters(Position.WR)) / 7f);

  public int FindAverageRatingOfStarters(Position p, List<int> excludedPlayerIndexes = null)
  {
    int num1 = 0;
    int num2 = 1;
    List<int> intList = new List<int>();
    if (excludedPlayerIndexes != null)
    {
      for (int index = 0; index < excludedPlayerIndexes.Count; ++index)
        intList.Add(excludedPlayerIndexes[index]);
    }
    switch (p)
    {
      case Position.QB:
      case Position.RB:
      case Position.TE:
      case Position.K:
      case Position.P:
        num2 = 1;
        num1 += this.GetOverall(this.MainRoster.FindBestPlayerAtPosition(p, intList.ToArray()));
        break;
      case Position.WR:
        num2 = 3;
        for (int index = 0; index < num2; ++index)
        {
          int playerAtPosition = this.MainRoster.FindBestPlayerAtPosition(p, intList.ToArray());
          num1 += this.GetOverall(playerAtPosition);
          intList.Add(playerAtPosition);
        }
        break;
      case Position.OL:
        num2 = 5;
        for (int index = 0; index < num2; ++index)
        {
          int playerAtPosition = this.MainRoster.FindBestPlayerAtPosition(p, intList.ToArray());
          num1 += this.GetOverall(playerAtPosition);
          intList.Add(playerAtPosition);
        }
        break;
      case Position.DL:
      case Position.LB:
      case Position.DB:
        num2 = 4;
        for (int index = 0; index < num2; ++index)
        {
          int playerAtPosition = this.MainRoster.FindBestPlayerAtPosition(p, intList.ToArray());
          num1 += this.GetOverall(playerAtPosition);
          intList.Add(playerAtPosition);
        }
        break;
    }
    return Mathf.RoundToInt((float) num1 / (float) num2);
  }
}
