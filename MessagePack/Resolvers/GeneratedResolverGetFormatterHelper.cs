// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.GeneratedResolverGetFormatterHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballWorld;
using MessagePack.Formatters;
using MessagePack.Formatters.DDL.UniformData;
using MessagePack.Formatters.FootballVR;
using MessagePack.Formatters.FootballWorld;
using MessagePack.Formatters.ProEra.Game;
using MessagePack.Formatters.ProEra.Game.Achievements;
using MessagePack.Formatters.ProEra.Game.Sources.TeamData;
using MessagePack.Formatters.TB12;
using MessagePack.Formatters.UnityEngine;
using ProEra.Game.Achievements;
using ProEra.Game.Sources.TeamData;
using System.Collections.Generic;
using TB12;
using UnityEngine;

namespace MessagePack.Resolvers
{
  internal static class GeneratedResolverGetFormatterHelper
  {
    private static readonly Dictionary<System.Type, int> lookup = new Dictionary<System.Type, int>(107)
    {
      {
        typeof (Award[]),
        0
      },
      {
        typeof (CoachData[]),
        1
      },
      {
        typeof (GameSummary[,]),
        2
      },
      {
        typeof (GameSummary[]),
        3
      },
      {
        typeof (MiniCampData[]),
        4
      },
      {
        typeof (MiniCampEntry[]),
        5
      },
      {
        typeof (PlayerData_Basic[]),
        6
      },
      {
        typeof (PlayerData[]),
        7
      },
      {
        typeof (PlayerStats[]),
        8
      },
      {
        typeof (DefaultPlayerData[]),
        9
      },
      {
        typeof (RosterFileData[]),
        10
      },
      {
        typeof (RosterPlayerData[]),
        11
      },
      {
        typeof (SGD_TeamData[]),
        12
      },
      {
        typeof (SGD_Uniforms[]),
        13
      },
      {
        typeof (Dictionary<int, ProfileProgress.Entry>),
        14
      },
      {
        typeof (Dictionary<string, float>),
        15
      },
      {
        typeof (Dictionary<string, Achievement>),
        16
      },
      {
        typeof (Dictionary<string, AcknowledgeableAward>),
        17
      },
      {
        typeof (Dictionary<string, global::TeamData>),
        18
      },
      {
        typeof (Dictionary<string, int[]>),
        19
      },
      {
        typeof (Dictionary<string, int>),
        20
      },
      {
        typeof (Dictionary<string, string>),
        21
      },
      {
        typeof (HashSet<int>),
        22
      },
      {
        typeof (List<Award>),
        23
      },
      {
        typeof (List<DriveEndType>),
        24
      },
      {
        typeof (List<Injury>),
        25
      },
      {
        typeof (List<PlayerStats>),
        26
      },
      {
        typeof (List<ProEra.Game.UniformConfig>),
        27
      },
      {
        typeof (List<ProfileProgress.Entry>),
        28
      },
      {
        typeof (List<TeamSeasonStats>),
        29
      },
      {
        typeof (List<int[]>),
        30
      },
      {
        typeof (List<int>),
        31
      },
      {
        typeof (int[,]),
        32
      },
      {
        typeof (AwardType),
        33
      },
      {
        typeof (Conference),
        34
      },
      {
        typeof (ETeamUniformId),
        35
      },
      {
        typeof (Division),
        36
      },
      {
        typeof (DriveEndType),
        37
      },
      {
        typeof (EBodyType),
        38
      },
      {
        typeof (EMoveType),
        39
      },
      {
        typeof (ETargetInterpolation),
        40
      },
      {
        typeof (EBallTrail),
        41
      },
      {
        typeof (EGlovesId),
        42
      },
      {
        typeof (InjuryType),
        43
      },
      {
        typeof (Position),
        44
      },
      {
        typeof (ProEraSeasonState),
        45
      },
      {
        typeof (RosterType),
        46
      },
      {
        typeof (ScoutingRegion),
        47
      },
      {
        typeof (StartingPosition),
        48
      },
      {
        typeof (EGameMode),
        49
      },
      {
        typeof (EMiniCampTourType),
        50
      },
      {
        typeof (RigidbodyInterpolation),
        51
      },
      {
        typeof (Award),
        52
      },
      {
        typeof (CoachData),
        53
      },
      {
        typeof (CustomLogoData),
        54
      },
      {
        typeof (DepthChart),
        55
      },
      {
        typeof (GameSummary),
        56
      },
      {
        typeof (Injury),
        57
      },
      {
        typeof (MiniCampData),
        58
      },
      {
        typeof (MiniCampEntry),
        59
      },
      {
        typeof (PlayerData),
        60
      },
      {
        typeof (PlayerData_Basic),
        61
      },
      {
        typeof (PlayerStats),
        62
      },
      {
        typeof (Achievement),
        63
      },
      {
        typeof (AchievementTier),
        64
      },
      {
        typeof (AcknowledgeableAward),
        65
      },
      {
        typeof (SaveAchievements),
        66
      },
      {
        typeof (DefaultPlayerData),
        67
      },
      {
        typeof (DefaultPlayerFileData),
        68
      },
      {
        typeof (RosterFileData),
        69
      },
      {
        typeof (RosterPlayerData),
        70
      },
      {
        typeof (ProEra.Game.UniformConfig),
        71
      },
      {
        typeof (RecordHolder),
        72
      },
      {
        typeof (RecordHolderGroup),
        73
      },
      {
        typeof (RosterData),
        74
      },
      {
        typeof (RosterSaveData),
        75
      },
      {
        typeof (Save_AvatarsSettings),
        76
      },
      {
        typeof (Save_ExhibitionSettings),
        77
      },
      {
        typeof (Save_GameSettings),
        78
      },
      {
        typeof (Save_MiniCamp),
        79
      },
      {
        typeof (Save_OldGameSettings),
        80
      },
      {
        typeof (Save_SettingsStore),
        81
      },
      {
        typeof (Save_ThrowSettings),
        82
      },
      {
        typeof (Save_TwoMD),
        83
      },
      {
        typeof (Save_VRSettings),
        84
      },
      {
        typeof (SavedGameData),
        85
      },
      {
        typeof (SaveKeycloakUserData),
        86
      },
      {
        typeof (SavePlayerCustomization),
        87
      },
      {
        typeof (SeasonModeTeamMap),
        88
      },
      {
        typeof (SGD_Achievements),
        89
      },
      {
        typeof (SGD_ModCache),
        90
      },
      {
        typeof (SGD_SeasonModeData),
        91
      },
      {
        typeof (SGD_TeamChanges),
        92
      },
      {
        typeof (SGD_TeamData),
        93
      },
      {
        typeof (SGD_Uniforms),
        94
      },
      {
        typeof (StatSet),
        95
      },
      {
        typeof (ProfileProgress),
        96
      },
      {
        typeof (ProfileProgress.Entry),
        97
      },
      {
        typeof (TeamConferenceData),
        98
      },
      {
        typeof (global::TeamData),
        99
      },
      {
        typeof (TeamFile),
        100
      },
      {
        typeof (TeamGameStats),
        101
      },
      {
        typeof (TeamPlayCalling),
        102
      },
      {
        typeof (TeamSeasonData),
        103
      },
      {
        typeof (TeamSeasonStats),
        104
      },
      {
        typeof (UserCareerStats),
        105
      },
      {
        typeof (UserCareerStats.SaveData),
        106
      }
    };

    internal static object GetFormatter(System.Type t)
    {
      int num;
      if (!GeneratedResolverGetFormatterHelper.lookup.TryGetValue(t, out num))
        return (object) null;
      switch (num)
      {
        case 0:
          return (object) new ArrayFormatter<Award>();
        case 1:
          return (object) new ArrayFormatter<CoachData>();
        case 2:
          return (object) new TwoDimensionalArrayFormatter<GameSummary>();
        case 3:
          return (object) new ArrayFormatter<GameSummary>();
        case 4:
          return (object) new ArrayFormatter<MiniCampData>();
        case 5:
          return (object) new ArrayFormatter<MiniCampEntry>();
        case 6:
          return (object) new ArrayFormatter<PlayerData_Basic>();
        case 7:
          return (object) new ArrayFormatter<PlayerData>();
        case 8:
          return (object) new ArrayFormatter<PlayerStats>();
        case 9:
          return (object) new ArrayFormatter<DefaultPlayerData>();
        case 10:
          return (object) new ArrayFormatter<RosterFileData>();
        case 11:
          return (object) new ArrayFormatter<RosterPlayerData>();
        case 12:
          return (object) new ArrayFormatter<SGD_TeamData>();
        case 13:
          return (object) new ArrayFormatter<SGD_Uniforms>();
        case 14:
          return (object) new DictionaryFormatter<int, ProfileProgress.Entry>();
        case 15:
          return (object) new DictionaryFormatter<string, float>();
        case 16:
          return (object) new DictionaryFormatter<string, Achievement>();
        case 17:
          return (object) new DictionaryFormatter<string, AcknowledgeableAward>();
        case 18:
          return (object) new DictionaryFormatter<string, global::TeamData>();
        case 19:
          return (object) new DictionaryFormatter<string, int[]>();
        case 20:
          return (object) new DictionaryFormatter<string, int>();
        case 21:
          return (object) new DictionaryFormatter<string, string>();
        case 22:
          return (object) new HashSetFormatter<int>();
        case 23:
          return (object) new ListFormatter<Award>();
        case 24:
          return (object) new ListFormatter<DriveEndType>();
        case 25:
          return (object) new ListFormatter<Injury>();
        case 26:
          return (object) new ListFormatter<PlayerStats>();
        case 27:
          return (object) new ListFormatter<ProEra.Game.UniformConfig>();
        case 28:
          return (object) new ListFormatter<ProfileProgress.Entry>();
        case 29:
          return (object) new ListFormatter<TeamSeasonStats>();
        case 30:
          return (object) new ListFormatter<int[]>();
        case 31:
          return (object) new ListFormatter<int>();
        case 32:
          return (object) new TwoDimensionalArrayFormatter<int>();
        case 33:
          return (object) new AwardTypeFormatter();
        case 34:
          return (object) new ConferenceFormatter();
        case 35:
          return (object) new ETeamUniformIdFormatter();
        case 36:
          return (object) new DivisionFormatter();
        case 37:
          return (object) new DriveEndTypeFormatter();
        case 38:
          return (object) new EBodyTypeFormatter();
        case 39:
          return (object) new EMoveTypeFormatter();
        case 40:
          return (object) new ETargetInterpolationFormatter();
        case 41:
          return (object) new EBallTrailFormatter();
        case 42:
          return (object) new EGlovesIdFormatter();
        case 43:
          return (object) new InjuryTypeFormatter();
        case 44:
          return (object) new PositionFormatter();
        case 45:
          return (object) new ProEraSeasonStateFormatter();
        case 46:
          return (object) new RosterTypeFormatter();
        case 47:
          return (object) new ScoutingRegionFormatter();
        case 48:
          return (object) new StartingPositionFormatter();
        case 49:
          return (object) new EGameModeFormatter();
        case 50:
          return (object) new EMiniCampTourTypeFormatter();
        case 51:
          return (object) new RigidbodyInterpolationFormatter();
        case 52:
          return (object) new AwardFormatter();
        case 53:
          return (object) new CoachDataFormatter();
        case 54:
          return (object) new CustomLogoDataFormatter();
        case 55:
          return (object) new DepthChartFormatter();
        case 56:
          return (object) new GameSummaryFormatter();
        case 57:
          return (object) new InjuryFormatter();
        case 58:
          return (object) new MiniCampDataFormatter();
        case 59:
          return (object) new MiniCampEntryFormatter();
        case 60:
          return (object) new PlayerDataFormatter();
        case 61:
          return (object) new PlayerData_BasicFormatter();
        case 62:
          return (object) new PlayerStatsFormatter();
        case 63:
          return (object) new AchievementFormatter();
        case 64:
          return (object) new AchievementTierFormatter();
        case 65:
          return (object) new AcknowledgeableAwardFormatter();
        case 66:
          return (object) new SaveAchievementsFormatter();
        case 67:
          return (object) new DefaultPlayerDataFormatter();
        case 68:
          return (object) new DefaultPlayerFileDataFormatter();
        case 69:
          return (object) new RosterFileDataFormatter();
        case 70:
          return (object) new RosterPlayerDataFormatter();
        case 71:
          return (object) new UniformConfigFormatter();
        case 72:
          return (object) new RecordHolderFormatter();
        case 73:
          return (object) new RecordHolderGroupFormatter();
        case 74:
          return (object) new RosterDataFormatter();
        case 75:
          return (object) new RosterSaveDataFormatter();
        case 76:
          return (object) new Save_AvatarsSettingsFormatter();
        case 77:
          return (object) new Save_ExhibitionSettingsFormatter();
        case 78:
          return (object) new Save_GameSettingsFormatter();
        case 79:
          return (object) new Save_MiniCampFormatter();
        case 80:
          return (object) new Save_OldGameSettingsFormatter();
        case 81:
          return (object) new Save_SettingsStoreFormatter();
        case 82:
          return (object) new Save_ThrowSettingsFormatter();
        case 83:
          return (object) new Save_TwoMDFormatter();
        case 84:
          return (object) new Save_VRSettingsFormatter();
        case 85:
          return (object) new SavedGameDataFormatter();
        case 86:
          return (object) new SaveKeycloakUserDataFormatter();
        case 87:
          return (object) new SavePlayerCustomizationFormatter();
        case 88:
          return (object) new SeasonModeTeamMapFormatter();
        case 89:
          return (object) new SGD_AchievementsFormatter();
        case 90:
          return (object) new SGD_ModCacheFormatter();
        case 91:
          return (object) new SGD_SeasonModeDataFormatter();
        case 92:
          return (object) new SGD_TeamChangesFormatter();
        case 93:
          return (object) new SGD_TeamDataFormatter();
        case 94:
          return (object) new SGD_UniformsFormatter();
        case 95:
          return (object) new StatSetFormatter();
        case 96:
          return (object) new ProfileProgressFormatter();
        case 97:
          return (object) new ProfileProgress_EntryFormatter();
        case 98:
          return (object) new TeamConferenceDataFormatter();
        case 99:
          return (object) new TeamDataFormatter();
        case 100:
          return (object) new TeamFileFormatter();
        case 101:
          return (object) new TeamGameStatsFormatter();
        case 102:
          return (object) new TeamPlayCallingFormatter();
        case 103:
          return (object) new TeamSeasonDataFormatter();
        case 104:
          return (object) new TeamSeasonStatsFormatter();
        case 105:
          return (object) new UserCareerStatsFormatter();
        case 106:
          return (object) new UserCareerStats_SaveDataFormatter();
        default:
          return (object) null;
      }
    }
  }
}
