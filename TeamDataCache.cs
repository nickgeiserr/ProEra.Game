// Decompiled with JetBrains decompiler
// Type: TeamDataCache
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using System.Collections.Generic;
using UnityEngine;

public class TeamDataCache : MonoBehaviour
{
  private static TeamData[] cachedTeamData;
  private static Dictionary<TeamData, Sprite> customTeamLogos;

  public static TeamData GetTeam(int teamIndex)
  {
    if (TeamDataCache.cachedTeamData == null)
      TeamDataCache.cachedTeamData = new TeamData[32];
    if (TeamDataCache.cachedTeamData[teamIndex] == null)
      TeamDataCache.cachedTeamData[teamIndex] = TeamDataCache.LoadTeamData(teamIndex);
    return TeamDataCache.cachedTeamData[teamIndex];
  }

  public static void SetTeamData(int teamIndex, TeamData newTeamData)
  {
    if (TeamDataCache.cachedTeamData == null)
      return;
    TeamDataCache.cachedTeamData[teamIndex] = newTeamData;
  }

  private static TeamData LoadTeamData(int teamIndex)
  {
    if (teamIndex < 0)
    {
      Debug.LogWarning((object) "We received a team number of less than 0. Setting it to 29... GO BUCCANEERS!");
      teamIndex = 29;
    }
    return TeamData.NewTeamData(teamIndex);
  }

  public static Sprite GetCustomTeamLogo(TeamData teamData)
  {
    if (TeamDataCache.customTeamLogos == null)
      TeamDataCache.customTeamLogos = new Dictionary<TeamData, Sprite>();
    if (!TeamDataCache.customTeamLogos.ContainsKey(teamData))
      TeamDataCache.customTeamLogos[teamData] = teamData.CustomLogo.GetSpriteFromTexture2D();
    return TeamDataCache.customTeamLogos[teamData];
  }

  public static void ClearTeamDataCache()
  {
    TeamDataCache.cachedTeamData = (TeamData[]) null;
    TeamDataCache.customTeamLogos = (Dictionary<TeamData, Sprite>) null;
  }

  public static void ClearLogoCacheForTeam(TeamData teamData)
  {
    if (TeamDataCache.customTeamLogos == null || !TeamDataCache.customTeamLogos.ContainsKey(teamData))
      return;
    TeamDataCache.customTeamLogos.Remove(teamData);
  }

  public static bool IsRival(int myTeam, int compTeam) => TeamDataCache.GetTeam(myTeam).GetRivals().Contains(compTeam);

  public static int ToTeamIndex(ETeamUniformId teamID)
  {
    int teamIndex;
    switch (teamID)
    {
      case ETeamUniformId.Bengals:
        teamIndex = 7;
        break;
      case ETeamUniformId.Colts:
        teamIndex = 14;
        break;
      case ETeamUniformId.Jaguars:
        teamIndex = 15;
        break;
      case ETeamUniformId.Packers:
        teamIndex = 12;
        break;
      case ETeamUniformId.Redskins:
        teamIndex = 31;
        break;
      case ETeamUniformId.Steelers:
        teamIndex = 26;
        break;
      case ETeamUniformId.Titans:
        teamIndex = 30;
        break;
      case ETeamUniformId.Vikings:
        teamIndex = 21;
        break;
      case ETeamUniformId.Bears:
        teamIndex = 6;
        break;
      case ETeamUniformId.Bills:
        teamIndex = 4;
        break;
      case ETeamUniformId.Dolphins:
        teamIndex = 20;
        break;
      case ETeamUniformId.Cowboys:
        teamIndex = 9;
        break;
      case ETeamUniformId.Falcons:
        teamIndex = 1;
        break;
      case ETeamUniformId.Eagles:
        teamIndex = 25;
        break;
      case ETeamUniformId.Lions:
        teamIndex = 11;
        break;
      case ETeamUniformId.Raiders:
        teamIndex = 17;
        break;
      case ETeamUniformId.Ravens:
        teamIndex = 2;
        break;
      case ETeamUniformId.Saints:
        teamIndex = 22;
        break;
      case ETeamUniformId.Texans:
        teamIndex = 13;
        break;
      case ETeamUniformId.Cardinals:
        teamIndex = 0;
        break;
      case ETeamUniformId.Browns:
        teamIndex = 8;
        break;
      case ETeamUniformId.SF49ers:
        teamIndex = 27;
        break;
      case ETeamUniformId.Patriots:
        teamIndex = 3;
        break;
      case ETeamUniformId.Buccaneers:
        teamIndex = 29;
        break;
      case ETeamUniformId.Panthers:
        teamIndex = 5;
        break;
      case ETeamUniformId.Broncos:
        teamIndex = 10;
        break;
      case ETeamUniformId.Chiefs:
        teamIndex = 16;
        break;
      case ETeamUniformId.Seahawks:
        teamIndex = 28;
        break;
      case ETeamUniformId.Chargers:
        teamIndex = 18;
        break;
      case ETeamUniformId.Rams:
        teamIndex = 19;
        break;
      case ETeamUniformId.Giants:
        teamIndex = 23;
        break;
      case ETeamUniformId.Jets:
        teamIndex = 24;
        break;
      default:
        teamIndex = -1;
        break;
    }
    return teamIndex;
  }

  public static ETeamUniformId ToTeamUniformId(int teamID)
  {
    ETeamUniformId teamUniformId;
    switch (teamID)
    {
      case 0:
        teamUniformId = ETeamUniformId.Cardinals;
        break;
      case 1:
        teamUniformId = ETeamUniformId.Falcons;
        break;
      case 2:
        teamUniformId = ETeamUniformId.Ravens;
        break;
      case 3:
        teamUniformId = ETeamUniformId.Patriots;
        break;
      case 4:
        teamUniformId = ETeamUniformId.Bills;
        break;
      case 5:
        teamUniformId = ETeamUniformId.Panthers;
        break;
      case 6:
        teamUniformId = ETeamUniformId.Bears;
        break;
      case 7:
        teamUniformId = ETeamUniformId.Bengals;
        break;
      case 8:
        teamUniformId = ETeamUniformId.Browns;
        break;
      case 9:
        teamUniformId = ETeamUniformId.Cowboys;
        break;
      case 10:
        teamUniformId = ETeamUniformId.Broncos;
        break;
      case 11:
        teamUniformId = ETeamUniformId.Lions;
        break;
      case 12:
        teamUniformId = ETeamUniformId.Packers;
        break;
      case 13:
        teamUniformId = ETeamUniformId.Texans;
        break;
      case 14:
        teamUniformId = ETeamUniformId.Colts;
        break;
      case 15:
        teamUniformId = ETeamUniformId.Jaguars;
        break;
      case 16:
        teamUniformId = ETeamUniformId.Chiefs;
        break;
      case 17:
        teamUniformId = ETeamUniformId.Raiders;
        break;
      case 18:
        teamUniformId = ETeamUniformId.Chargers;
        break;
      case 19:
        teamUniformId = ETeamUniformId.Rams;
        break;
      case 20:
        teamUniformId = ETeamUniformId.Dolphins;
        break;
      case 21:
        teamUniformId = ETeamUniformId.Vikings;
        break;
      case 22:
        teamUniformId = ETeamUniformId.Saints;
        break;
      case 23:
        teamUniformId = ETeamUniformId.Giants;
        break;
      case 24:
        teamUniformId = ETeamUniformId.Jets;
        break;
      case 25:
        teamUniformId = ETeamUniformId.Eagles;
        break;
      case 26:
        teamUniformId = ETeamUniformId.Steelers;
        break;
      case 27:
        teamUniformId = ETeamUniformId.SF49ers;
        break;
      case 28:
        teamUniformId = ETeamUniformId.Seahawks;
        break;
      case 29:
        teamUniformId = ETeamUniformId.Buccaneers;
        break;
      case 30:
        teamUniformId = ETeamUniformId.Titans;
        break;
      case 31:
        teamUniformId = ETeamUniformId.Redskins;
        break;
      default:
        teamUniformId = ETeamUniformId.Ravens;
        break;
    }
    return teamUniformId;
  }
}
