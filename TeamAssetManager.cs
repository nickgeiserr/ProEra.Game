// Decompiled with JetBrains decompiler
// Type: TeamAssetManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using Framework;
using ProEra.Game.Sources.TeamData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamAssetManager : MonoBehaviour
{
  public static int NUMBER_OF_PLAYERS_ON_ROSTER = 53;
  public static int NUMBER_OF_PLAYERS_ON_PRACTICE_SQUAD = 15;
  public static int NUMBER_OF_PLAYER_ATTRIBUTES = 33;
  public static int NUMBER_OF_PLAYERS_IN_DRAFT = 555;
  public static int NUMBER_OF_PLAYERS_IN_FREE_AGENCY = 300;
  public static int NUMBER_OF_BASE_TEAMS = 32;
  public static int NUMBER_OF_COACHES_ON_TEAM = 15;
  public static Dictionary<Position, int> DRAFT_POSITION_DISTRIBUTION = new Dictionary<Position, int>()
  {
    {
      Position.QB,
      60
    },
    {
      Position.RB,
      60
    },
    {
      Position.WR,
      60
    },
    {
      Position.TE,
      50
    },
    {
      Position.OL,
      75
    },
    {
      Position.DL,
      75
    },
    {
      Position.LB,
      75
    },
    {
      Position.DB,
      60
    },
    {
      Position.P,
      20
    },
    {
      Position.K,
      20
    }
  };
  public static Dictionary<Position, int> FREE_AGENT_POSITION_DISTRIBUTION = new Dictionary<Position, int>()
  {
    {
      Position.QB,
      16
    },
    {
      Position.RB,
      30
    },
    {
      Position.WR,
      38
    },
    {
      Position.TE,
      24
    },
    {
      Position.OL,
      51
    },
    {
      Position.DL,
      42
    },
    {
      Position.LB,
      36
    },
    {
      Position.DB,
      45
    },
    {
      Position.P,
      9
    },
    {
      Position.K,
      9
    }
  };
  public static Dictionary<Position, int> TEAM_POSITION_DISTRIBUTION = new Dictionary<Position, int>()
  {
    {
      Position.QB,
      3
    },
    {
      Position.RB,
      4
    },
    {
      Position.WR,
      6
    },
    {
      Position.TE,
      3
    },
    {
      Position.OL,
      9
    },
    {
      Position.DL,
      9
    },
    {
      Position.LB,
      8
    },
    {
      Position.DB,
      9
    },
    {
      Position.P,
      1
    },
    {
      Position.K,
      1
    }
  };
  public static Dictionary<CoachPositions, int> COACH_POSITION_DISTRIBUTION = new Dictionary<CoachPositions, int>()
  {
    {
      CoachPositions.Head_Coach,
      25
    },
    {
      CoachPositions.OFF_Coordinator,
      20
    },
    {
      CoachPositions.DEF_Coordinator,
      20
    },
    {
      CoachPositions.ST_Coordinator,
      20
    },
    {
      CoachPositions.QB_Coach,
      15
    },
    {
      CoachPositions.RB_Coach,
      15
    },
    {
      CoachPositions.REC_Coach,
      15
    },
    {
      CoachPositions.OL_Coach,
      15
    },
    {
      CoachPositions.DL_Coach,
      15
    },
    {
      CoachPositions.LB_Coach,
      15
    },
    {
      CoachPositions.DB_Coach,
      15
    },
    {
      CoachPositions.West_Scout,
      15
    },
    {
      CoachPositions.Central_Scout,
      15
    },
    {
      CoachPositions.SouthEast_Scout,
      15
    },
    {
      CoachPositions.NorthEast_Scout,
      15
    }
  };
  public static Dictionary<CoachPositions, int> SINGLE_COACHING_STAFF = new Dictionary<CoachPositions, int>()
  {
    {
      CoachPositions.Head_Coach,
      1
    },
    {
      CoachPositions.OFF_Coordinator,
      1
    },
    {
      CoachPositions.DEF_Coordinator,
      1
    },
    {
      CoachPositions.ST_Coordinator,
      1
    },
    {
      CoachPositions.QB_Coach,
      1
    },
    {
      CoachPositions.RB_Coach,
      1
    },
    {
      CoachPositions.REC_Coach,
      1
    },
    {
      CoachPositions.OL_Coach,
      1
    },
    {
      CoachPositions.DL_Coach,
      1
    },
    {
      CoachPositions.LB_Coach,
      1
    },
    {
      CoachPositions.DB_Coach,
      1
    },
    {
      CoachPositions.West_Scout,
      1
    },
    {
      CoachPositions.Central_Scout,
      1
    },
    {
      CoachPositions.SouthEast_Scout,
      1
    },
    {
      CoachPositions.NorthEast_Scout,
      1
    }
  };

  private static TeamAssetType GetTeamAssetType(int teamIndex) => teamIndex >= TeamAssetManager.NUMBER_OF_BASE_TEAMS ? TeamAssetType.MODS : TeamAssetType.RESOURCES;

  public static Texture2D LoadTeamGraphic(int teamIndex, TeamGraphicType type)
  {
    teamIndex = AssetManager.GetModifiedTeamIndex(teamIndex);
    return teamIndex < TeamAssetManager.NUMBER_OF_BASE_TEAMS ? TeamResourcesManager.LoadTeamGraphic(teamIndex, type) : TeamModManager.LoadTeamGraphic(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS, type);
  }

  public static Sprite LoadLogoSprite(int teamIndex, TeamGraphicType type)
  {
    teamIndex = AssetManager.GetModifiedTeamIndex(teamIndex);
    return teamIndex < TeamAssetManager.NUMBER_OF_BASE_TEAMS ? TeamResourcesManager.LoadLogoSprite(teamIndex, type) : TeamModManager.LoadLogoSprite(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS, type);
  }

  public static int TotalCountOfTeams()
  {
    int num = 0;
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets)
      num += TeamResourcesManager.GetCountOfTeams();
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets)
      num += ModManager.self.GetCountOfTeamFolders();
    return num;
  }

  public static TeamFile LoadTeamFile(int teamIndex) => TeamAssetManager.GetTeamAssetType(teamIndex) == TeamAssetType.RESOURCES ? PersistentSingleton<TeamResourcesManager>.Instance.LoadTeamFile(teamIndex) : TeamModManager.LoadTeamFile(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS);

  public static RosterData LoadTeamRoster(int teamIndex) => TeamAssetManager.GetTeamAssetType(teamIndex) == TeamAssetType.RESOURCES ? TeamResourcesManager.LoadTeamRoster(teamIndex) : TeamModManager.LoadTeamRoster(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS);

  public static CoachData[] LoadCoachingStaff(int teamIndex) => TeamAssetManager.GetTeamAssetType(teamIndex) == TeamAssetType.RESOURCES ? TeamResourcesManager.LoadCoachingStaff(teamIndex) : TeamModManager.LoadCoachingStaff(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS);

  public static TeamPlayCalling LoadTeamPlayCalling(int teamIndex) => TeamAssetManager.GetTeamAssetType(teamIndex) == TeamAssetType.RESOURCES ? PersistentSingleton<TeamResourcesManager>.Instance.LoadTeamPlayCalling(teamIndex) : TeamModManager.LoadTeamPlayCalling(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS);

  public static RosterData ParseTeamRoster(
    string contents,
    Dictionary<string, int> defaultPlayers,
    bool isMods = false,
    RosterType rosterType = RosterType.MainTeamRoster)
  {
    List<PlayerData> playerDataList = new List<PlayerData>();
    string[] strArray1 = contents.Split("\n"[0], StringSplitOptions.None);
    string[] strArray2 = strArray1[0].Split(',', StringSplitOptions.None);
    for (int index = 0; index < strArray2.Length; ++index)
      strArray2[index] = AssetManager.TrimString(strArray2[index]);
    int num = 0;
    for (int index1 = 1; index1 < strArray1.Length; ++index1)
    {
      string[] strArray3 = strArray1[index1].Split(',', StringSplitOptions.None);
      if (strArray3.Length > 1)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
          strArray3[index2] = AssetManager.TrimString(strArray3[index2]);
        PlayerData playerData = new PlayerData();
        try
        {
          playerData.IndexOnTeam = num;
          ++num;
          playerData.FirstName = strArray3[1].ToUpper();
          playerData.LastName = strArray3[2].ToUpper();
          playerData.SkinColor = int.Parse(strArray3[3]);
          playerData.Number = int.Parse(strArray3[4]);
          playerData.Height = int.Parse(strArray3[5]);
          playerData.Weight = int.Parse(strArray3[6]);
          playerData.PlayerPosition = global::TeamData.GetPositionFromString(strArray3[7]);
          playerData.Speed = int.Parse(strArray3[8]);
          playerData.TackleBreaking = int.Parse(strArray3[9]);
          playerData.Fumbling = int.Parse(strArray3[10]);
          playerData.Catching = int.Parse(strArray3[11]);
          playerData.Blocking = int.Parse(strArray3[12]);
          playerData.ThrowAccuracy = int.Parse(strArray3[13]);
          playerData.KickPower = int.Parse(strArray3[14]);
          playerData.KickAccuracy = int.Parse(strArray3[15]);
          playerData.BlockBreaking = int.Parse(strArray3[16]);
          playerData.Tackling = int.Parse(strArray3[17]);
          playerData.ThrowPower = int.Parse(strArray3[18]);
          playerData.Fitness = int.Parse(strArray3[19]);
          playerData.Awareness = int.Parse(strArray3[20]);
          playerData.Agility = int.Parse(strArray3[21]);
          playerData.Coverage = int.Parse(strArray3[22]);
          playerData.HitPower = int.Parse(strArray3[23]);
          playerData.Endurance = int.Parse(strArray3[24]);
          playerData.Visor = int.Parse(strArray3[25]);
          playerData.Sleeves = int.Parse(strArray3[26]);
          playerData.Bands = int.Parse(strArray3[27]);
          playerData.Wraps = int.Parse(strArray3[28]);
          playerData.Age = int.Parse(strArray3[29]);
          playerData.Potential = int.Parse(strArray3[30]);
          playerData.PortraitID = int.Parse(strArray3[31]);
          playerData.Discipline = int.Parse(strArray3[32]);
          if (strArray3.Length > 33)
            playerData.AvatarID = strArray3[33];
          if (strArray3.Length > 34)
            playerData.IsLeftHanded = int.Parse(strArray3[34]);
          playerData.ContractLength = 0;
          playerData.YearsRemainingOnContract = 0;
          playerData.Salary = 0;
          playerDataList.Add(playerData);
        }
        catch (Exception ex)
        {
          if (isMods)
            TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Cound not process one of the players. Error on line " + index1.ToString() + " : '" + strArray1[index1].Trim() + "'");
        }
      }
    }
    if (isMods && num < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER)
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Not enough players in ROSTER.CSV.  Only found " + num.ToString() + " players!");
    else if (isMods && num > TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER)
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.WARNING, "Found too many players in ROSTER.CSV.  Found " + num.ToString() + " players, discarding the extras.");
    return new RosterData(playerDataList.ToArray(), defaultPlayers, rosterType);
  }

  public static CoachData[] ParseCoachingStaffCSVFile(string contents, bool isMods = false)
  {
    CoachData[] coachingStaffCsvFile = new CoachData[TeamAssetManager.NUMBER_OF_COACHES_ON_TEAM];
    string[] strArray1 = contents.Split("\n"[0], StringSplitOptions.None);
    string[] strArray2 = strArray1[0].Split(',', StringSplitOptions.None);
    if (isMods && strArray2.Length != 17)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "    INDEX        FIRST        LAST        SKIN           PORTRAIT        AGE        EXPERIENCE        OFFENSE        DEFENSE        EVALUATION        MOTIVATION        DEVELOPMENT        DISCIPLINE        BADGE 1        BADGE 2        BADGE 3        POTENTIAL");
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "    Using an auto-generated coaching staff instead");
      return TeamResourcesManager.GenerateCoachClass(TeamAssetManager.SINGLE_COACHING_STAFF);
    }
    for (int index = 0; index < strArray2.Length; ++index)
      strArray2[index] = AssetManager.TrimString(strArray2[index]);
    int index1 = 0;
    for (int index2 = 1; index2 < strArray1.Length; ++index2)
    {
      string[] strArray3 = strArray1[index2].Split(',', StringSplitOptions.None);
      if (strArray3.Length > 1)
      {
        for (int index3 = 0; index3 < strArray3.Length; ++index3)
          strArray3[index3] = AssetManager.TrimString(strArray3[index3]);
        try
        {
          coachingStaffCsvFile[index1] = new CoachData()
          {
            FirstName = strArray3[1].ToUpper(),
            LastName = strArray3[2].ToUpper(),
            Skin = int.Parse(strArray3[3]),
            Portrait = int.Parse(strArray3[4]),
            Age = int.Parse(strArray3[5]),
            Experience = int.Parse(strArray3[6]),
            Offense = int.Parse(strArray3[7]),
            Defense = int.Parse(strArray3[8]),
            Evaluation = int.Parse(strArray3[9]),
            Motivation = int.Parse(strArray3[10]),
            Development = int.Parse(strArray3[11]),
            Discipline = int.Parse(strArray3[12]),
            Badge1 = int.Parse(strArray3[13]),
            Badge2 = int.Parse(strArray3[14]),
            Badge3 = int.Parse(strArray3[15]),
            Badge4 = -1,
            Potential = int.Parse(strArray3[16]),
            CoachPosition = (CoachPositions) index1
          };
          ++index1;
        }
        catch (Exception ex)
        {
          if (isMods)
          {
            TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Cound not process one of the coaches. Error on line " + index2.ToString() + " : '" + strArray1[index2].Trim() + "'");
            TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "    Using an auto-generated coaching staff instead");
          }
          else
            Debug.Log((object) ex.ToString());
          return TeamResourcesManager.GenerateCoachClass(TeamAssetManager.SINGLE_COACHING_STAFF);
        }
      }
    }
    if (isMods && index1 < 15)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Not enough coaches in COACHINGSTAFF.CSV.  Only found " + index1.ToString() + " players! There should be 15.");
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "    Using an auto-generated coaching staff instead");
      return TeamResourcesManager.GenerateCoachClass(TeamAssetManager.SINGLE_COACHING_STAFF);
    }
    if (isMods && index1 > 15)
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.WARNING, "Found too many coaches in COACHINGSTAFF.CSV.  Found " + index1.ToString() + " players, discarding the extras.");
    return coachingStaffCsvFile;
  }

  public static int GetNumberOfBaseTeams() => !PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets ? 0 : TeamAssetManager.NUMBER_OF_BASE_TEAMS;

  public static void DestroyOrUnloadResource(int teamIndex, UnityEngine.Object resource)
  {
    if (teamIndex < TeamAssetManager.NUMBER_OF_BASE_TEAMS)
      Resources.UnloadAsset(resource);
    else
      UnityEngine.Object.Destroy(resource);
  }

  public static Dictionary<string, int> LoadDefaultPlayers(int teamIndex) => TeamResourcesManager.LoadDefaultPlayers(teamIndex);

  public static Dictionary<string, int> ParseDefaultPlayersCSVFile(string contents, bool isMods = false)
  {
    Dictionary<string, int> defaultPlayersCsvFile = new Dictionary<string, int>();
    string[] strArray1 = contents.Split("\n"[0], StringSplitOptions.None);
    string[] strArray2 = strArray1[0].Split(',', StringSplitOptions.None);
    for (int index = 0; index < strArray2.Length; ++index)
      strArray2[index] = Utils.TrimString(strArray2[index]);
    for (int index1 = 1; index1 < strArray1.Length; ++index1)
    {
      string[] strArray3 = strArray1[index1].Split(',', StringSplitOptions.None);
      if (strArray3.Length > 1)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
          strArray3[index2] = Utils.TrimString(strArray3[index2]);
        defaultPlayersCsvFile.Add(strArray3[0], int.Parse(strArray3[1]));
      }
    }
    return defaultPlayersCsvFile;
  }

  public static TeamConferenceData[] ParseConferencesCSVFile(string contents)
  {
    TeamConferenceData[] conferencesCsvFile = new TeamConferenceData[TeamAssetManager.NUMBER_OF_BASE_TEAMS];
    string[] strArray1 = contents.Split("\n"[0], StringSplitOptions.None);
    string[] strArray2 = strArray1[0].Split(',', StringSplitOptions.None);
    for (int index = 0; index < strArray2.Length; ++index)
      strArray2[index] = Utils.TrimString(strArray2[index]);
    for (int index1 = 1; index1 < strArray1.Length; ++index1)
    {
      string[] strArray3 = strArray1[index1].Split(',', StringSplitOptions.None);
      if (strArray3.Length == 6)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
          strArray3[index2] = Utils.TrimString(strArray3[index2]);
        try
        {
          TeamConferenceData teamConferenceData = new TeamConferenceData();
          teamConferenceData.conference = (Conference) Enum.Parse(typeof (Conference), strArray3[0], true);
          teamConferenceData.division = (Division) Enum.Parse(typeof (Division), strArray3[1], true);
          teamConferenceData.numInDivision = int.Parse(strArray3[2]);
          string str = strArray3[3];
          teamConferenceData.teamMap = new SeasonModeTeamMap(strArray3[4], int.Parse(strArray3[5]));
          conferencesCsvFile[index1 - 1] = teamConferenceData;
          Debug.Log((object) ("Parsed Team Conference Data == " + teamConferenceData.teamMap.name + " Index=" + teamConferenceData.teamMap.index.ToString() + " Conf=" + teamConferenceData.conference.ToString() + " Div=" + teamConferenceData.division.ToString() + "Num in Div" + teamConferenceData.numInDivision.ToString()));
        }
        catch (Exception ex)
        {
          Debug.LogError((object) "Error Parsing TeamConference CSV");
          Debug.Log((object) ex.ToString());
        }
      }
    }
    return conferencesCsvFile;
  }

  public static Dictionary<string, List<ScheduleMatchupData>> ParseBaseScheduleFile(string contents)
  {
    Dictionary<string, List<ScheduleMatchupData>> baseScheduleFile = new Dictionary<string, List<ScheduleMatchupData>>();
    string[] strArray1 = contents.Split("\n"[0], StringSplitOptions.None);
    string[] strArray2 = strArray1[0].Split(',', StringSplitOptions.None);
    for (int index = 0; index < strArray2.Length; ++index)
      strArray2[index] = Utils.TrimString(strArray2[index]);
    for (int index1 = 1; index1 < strArray1.Length; ++index1)
    {
      string[] strArray3 = strArray1[index1].Split(',', StringSplitOptions.None);
      if (strArray3.Length == 19)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
          strArray3[index2] = Utils.TrimString(strArray3[index2]);
        try
        {
          string key = strArray3[0];
          List<ScheduleMatchupData> scheduleMatchupDataList = new List<ScheduleMatchupData>();
          for (int index3 = 1; index3 < strArray3.Length; ++index3)
            scheduleMatchupDataList.Add(new ScheduleMatchupData()
            {
              matchup = strArray3[index3]
            });
          baseScheduleFile.Add(key, scheduleMatchupDataList);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) "Error Parsing Base Schedule CSV");
          Debug.Log((object) ex.ToString());
        }
      }
    }
    return baseScheduleFile;
  }
}
