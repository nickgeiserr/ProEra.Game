// Decompiled with JetBrains decompiler
// Type: TeamModManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TeamModManager : MonoBehaviour
{
  private static string teamPath;
  private bool onComputer;
  public static Utility.Logging.Logger TeamModsLogger;

  private void Awake() => this.onComputer = Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;

  public void Path(string p) => TeamModManager.teamPath = p;

  public static string GetTeamPath() => TeamModManager.teamPath;

  public void Init() => TeamModManager.TeamModsLogger = new Utility.Logging.Logger(ModManager.appPath + ModManager.modPath + "/Logs/", "LoadTeam", "txt", true);

  private static Texture2D LoadGraphic(
    string fullPath,
    int imageWidth,
    int imageHeight,
    TextureFormat format,
    bool mipmap,
    FilterMode filter,
    int anisoLevel)
  {
    Texture2D tex = new Texture2D(imageWidth, imageHeight, format, mipmap);
    tex.LoadImage(File.ReadAllBytes(fullPath));
    tex.filterMode = filter;
    tex.anisoLevel = anisoLevel;
    return tex;
  }

  public static Texture2D LoadTeamGraphic(int teamIndex, TeamGraphicType type)
  {
    string str = "";
    int imageWidth = 0;
    int imageHeight = 0;
    int anisoLevel = 1;
    TextureFormat format = TextureFormat.RGBA32;
    FilterMode filter = FilterMode.Bilinear;
    bool mipmap = true;
    switch (type)
    {
      case TeamGraphicType.ENDZONE:
        str = "endzone.png";
        imageWidth = 1488;
        imageHeight = 272;
        format = TextureFormat.RGBA32;
        mipmap = true;
        filter = FilterMode.Bilinear;
        anisoLevel = 1;
        break;
      case TeamGraphicType.MIDFIELD:
        str = "midfield.png";
        imageWidth = 512;
        imageHeight = 512;
        format = TextureFormat.RGBA32;
        mipmap = false;
        filter = FilterMode.Bilinear;
        anisoLevel = 1;
        break;
      case TeamGraphicType.SMALL_LOGO:
        str = "small_logo.png";
        imageWidth = 512;
        imageHeight = 512;
        format = TextureFormat.DXT5;
        mipmap = false;
        filter = FilterMode.Bilinear;
        anisoLevel = 1;
        break;
      case TeamGraphicType.LARGE_LOGO:
        str = "large_logo.png";
        imageWidth = 1024;
        imageHeight = 1024;
        format = TextureFormat.DXT5;
        mipmap = false;
        filter = FilterMode.Bilinear;
        anisoLevel = 0;
        break;
      case TeamGraphicType.TINY_LOGO:
        str = "tiny_logo.png";
        imageWidth = 64;
        imageHeight = 64;
        format = TextureFormat.DXT5;
        mipmap = false;
        filter = FilterMode.Bilinear;
        anisoLevel = 0;
        break;
      case TeamGraphicType.MEDIUM_LOGO:
        str = "medium_logo.png";
        imageWidth = 256;
        imageHeight = 256;
        format = TextureFormat.DXT5;
        mipmap = false;
        filter = FilterMode.Bilinear;
        anisoLevel = 0;
        break;
    }
    return TeamModManager.LoadGraphic(TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/" + str, imageWidth, imageHeight, format, mipmap, filter, anisoLevel);
  }

  public static Sprite LoadLogoSprite(int teamIndex, TeamGraphicType type)
  {
    Texture2D texture = TeamModManager.LoadTeamGraphic(teamIndex, type);
    int width = 0;
    int height = 0;
    switch (type)
    {
      case TeamGraphicType.SMALL_LOGO:
        width = 128;
        height = 128;
        break;
      case TeamGraphicType.LARGE_LOGO:
        width = 1024;
        height = 1024;
        break;
      case TeamGraphicType.TINY_LOGO:
        width = 64;
        height = 64;
        break;
      case TeamGraphicType.MEDIUM_LOGO:
        width = 256;
        height = 256;
        break;
    }
    return Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) width, (float) height), new Vector2(0.0f, 1f), 1f);
  }

  internal static TeamFile LoadTeamFile(int teamIndex)
  {
    string[] strArray1 = TeamModManager.LoadTextFile(teamIndex, "TEAM.TXT").Split("\n"[0], StringSplitOptions.None);
    TeamFile teamFile = new TeamFile();
    for (int index = 0; index < strArray1.Length; ++index)
    {
      if (strArray1[index].Length <= 0 || strArray1[index][0] != '#')
      {
        string[] strArray2 = strArray1[index].Split('=', StringSplitOptions.None);
        if (strArray2.Length == 2)
        {
          string name = Utils.TrimString(strArray2[0]);
          string str = Utils.TrimString(strArray2[1]);
          if (name.Length > 0 && str.Length > 0)
            teamFile.AddNameValuePair(name, str);
        }
      }
    }
    return teamFile;
  }

  internal static TeamPlayCalling LoadTeamPlayCalling(int teamIndex)
  {
    string[] strArray1 = TeamModManager.LoadTextFile(teamIndex, "PLAYCALLING").Split("\n"[0], StringSplitOptions.None);
    TeamPlayCalling teamPlayCalling = new TeamPlayCalling();
    for (int index = 0; index < strArray1.Length; ++index)
    {
      if (strArray1[index].Length <= 0 || strArray1[index][0] != '#')
      {
        string[] strArray2 = strArray1[index].Split('=', StringSplitOptions.None);
        if (strArray2.Length == 2)
        {
          string name = AssetManager.TrimString(strArray2[0]);
          string s = AssetManager.TrimString(strArray2[1]);
          if (name.Length > 0 && s.Length > 0)
            teamPlayCalling.AddNameValuePair(name, int.Parse(s));
        }
      }
    }
    teamPlayCalling.ValidateDefaultValues();
    return teamPlayCalling;
  }

  internal static Dictionary<string, int> LoadDefaultPlayers(int teamIndex)
  {
    string path = TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/DEFAULTPLAYERS.CSV";
    TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.INFO, "Loading file - " + path);
    return TeamAssetManager.ParseDefaultPlayersCSVFile(File.ReadAllText(path), true);
  }

  internal static RosterData LoadTeamRoster(int teamIndex)
  {
    TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.INFO, "Loading file - " + TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + " / ROSTER.CSV");
    return TeamAssetManager.ParseTeamRoster(File.ReadAllText(TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/ROSTER.CSV"), TeamModManager.LoadDefaultPlayers(teamIndex), true);
  }

  internal static CoachData[] LoadCoachingStaff(int teamIndex)
  {
    TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.INFO, "Loading file - " + TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + " / COACHINGSTAFF.CSV");
    return TeamAssetManager.ParseCoachingStaffCSVFile(File.ReadAllText(TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/COACHINGSTAFF.CSV"), true);
  }

  private static string LoadTextFile(int teamIndex, string filename) => File.ReadAllText(TeamModManager.teamPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/" + filename);

  public static RosterData LoadDraftClass(int draftYear) => TeamAssetManager.ParseTeamRoster(TeamModManager.LoadDraftClassText(draftYear), (Dictionary<string, int>) null, true, RosterType.Draft);

  private static string LoadDraftClassText(int draftYear)
  {
    string str = "Draft Class " + draftYear.ToString() + ".txt";
    return File.ReadAllText(ModManager.appPath + ModManager.modPath + "/Drafts/" + str);
  }
}
