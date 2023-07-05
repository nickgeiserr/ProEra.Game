// Decompiled with JetBrains decompiler
// Type: AssetManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class AssetManager : SingletonBehaviour<AssetManager, SerializedMonoBehaviour>
{
  private static bool isFirstTime = true;
  public static Sprite[] largeTeamLogos;
  public static Sprite[] mediumTeamLogos;
  public static Sprite[] smallTeamLogos;
  public static Sprite[] tinyTeamLogos;

  public static int TotalTeams { get; set; } = 32;

  public static bool UseModsAssets { get; private set; }

  public static bool UseBaseAssets { get; private set; }

  private new void Awake()
  {
    if (AssetManager.isFirstTime)
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      AssetManager.isFirstTime = false;
    }
    else
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.gameObject);
    AssetManager.TotalTeams = 0;
    if (AssetManager.IsPlatformThatSupportsMods())
    {
      if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets)
      {
        AssetManager.UseModsAssets = true;
        ModManager.self.Init();
        UniformAssetManager.InitializeUniformModManager();
        AssetManager.TotalTeams += ModManager.self.GetCountOfTeamFolders();
      }
      if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets || ModManager.self.GetCountOfTeamFolders() == 0)
      {
        AssetManager.UseBaseAssets = true;
        AssetManager.TotalTeams += TeamResourcesManager.GetCountOfTeams();
      }
    }
    else
    {
      AssetManager.UseBaseAssets = true;
      AssetManager.TotalTeams = TeamResourcesManager.GetCountOfTeams();
    }
    Debug.Log((object) "Setting logos to not null");
    AssetManager.largeTeamLogos = new Sprite[AssetManager.TotalTeams];
    AssetManager.smallTeamLogos = new Sprite[AssetManager.TotalTeams];
    AssetManager.tinyTeamLogos = new Sprite[AssetManager.TotalTeams];
    AssetManager.mediumTeamLogos = new Sprite[AssetManager.TotalTeams];
  }

  private void OnDestroy()
  {
    Debug.Log((object) "AssetManager -> OnDestroy");
    SingletonBehaviour<AssetManager, SerializedMonoBehaviour>.instance = (AssetManager) null;
  }

  public static int GetModifiedTeamIndex(int teamIndex) => teamIndex;

  public static Sprite GetLargeTeamLogo(int teamIndex)
  {
    if ((UnityEngine.Object) AssetManager.largeTeamLogos[teamIndex] == (UnityEngine.Object) null)
      AssetManager.largeTeamLogos[teamIndex] = TeamAssetManager.LoadLogoSprite(teamIndex, TeamGraphicType.LARGE_LOGO);
    return AssetManager.largeTeamLogos[teamIndex];
  }

  public static Sprite GetMediumTeamLogo(int teamIndex)
  {
    if ((UnityEngine.Object) AssetManager.mediumTeamLogos[teamIndex] == (UnityEngine.Object) null)
      AssetManager.mediumTeamLogos[teamIndex] = TeamAssetManager.LoadLogoSprite(teamIndex, TeamGraphicType.MEDIUM_LOGO);
    return AssetManager.mediumTeamLogos[teamIndex];
  }

  public static Sprite GetSmallTeamLogo(int teamIndex)
  {
    if ((UnityEngine.Object) AssetManager.smallTeamLogos[teamIndex] == (UnityEngine.Object) null)
      AssetManager.smallTeamLogos[teamIndex] = TeamAssetManager.LoadLogoSprite(teamIndex, TeamGraphicType.SMALL_LOGO);
    return AssetManager.smallTeamLogos[teamIndex];
  }

  public static Sprite GetTinyTeamLogo(int teamIndex)
  {
    if ((UnityEngine.Object) AssetManager.tinyTeamLogos[teamIndex] == (UnityEngine.Object) null)
      AssetManager.tinyTeamLogos[teamIndex] = TeamAssetManager.LoadLogoSprite(teamIndex, TeamGraphicType.TINY_LOGO);
    return AssetManager.tinyTeamLogos[teamIndex];
  }

  public static Texture2D GetMidfieldLogo(int teamIndex) => TeamAssetManager.LoadTeamGraphic(teamIndex, TeamGraphicType.MIDFIELD);

  public static Texture2D GetEndzoneGraphic(int teamIndex) => TeamAssetManager.LoadTeamGraphic(teamIndex, TeamGraphicType.ENDZONE);

  public static void ClearUnusedLargeTeamLogos(int homeIndex, int awayIndex)
  {
    for (int index = 0; index < AssetManager.largeTeamLogos.Length; ++index)
    {
      if (index != homeIndex && index != awayIndex)
        AssetManager.largeTeamLogos[index] = (Sprite) null;
    }
  }

  public static void ClearUnusedMediumTeamLogos(int homeIndex, int awayIndex)
  {
    for (int index = 0; index < AssetManager.mediumTeamLogos.Length; ++index)
    {
      if (index != homeIndex && index != awayIndex)
        AssetManager.mediumTeamLogos[index] = (Sprite) null;
    }
  }

  public static void ClearUnusedSmallTeamLogos(int homeIndex, int awayIndex)
  {
    for (int index = 0; index < AssetManager.smallTeamLogos.Length; ++index)
    {
      if (index != homeIndex && index != awayIndex)
        AssetManager.smallTeamLogos[index] = (Sprite) null;
    }
  }

  public static void ClearUnusedTinyTeamLogos(int homeIndex, int awayIndex)
  {
    for (int index = 0; index < AssetManager.tinyTeamLogos.Length; ++index)
    {
      if (index != homeIndex && index != awayIndex)
        AssetManager.tinyTeamLogos[index] = (Sprite) null;
    }
  }

  public static bool IsPlatformThatSupportsMods() => false;

  public static string TrimString(string s) => s.Trim('\t', ' ', '\r', '\n', '"');

  public static string RemoveWhitespace(string s)
  {
    s = s.Replace("\t", "").Replace(" ", "").Replace("\r", "").Replace("\n", "");
    return s;
  }

  public static Dictionary<string, string> ReadProperties(string contents)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (string str in contents.Split('\n', StringSplitOptions.None))
    {
      string[] strArray = str.Split('=', StringSplitOptions.None);
      dictionary.Add(AssetManager.TrimString(strArray[0]), AssetManager.TrimString(strArray[1]));
    }
    return dictionary;
  }

  public static string[] SplitTextFile(string contents) => contents.Split("\n"[0], StringSplitOptions.None);

  public static string[] SplitAndTrimTextFile(string contents)
  {
    string[] strArray = contents.Split(new char[1]
    {
      "\n"[0]
    }, StringSplitOptions.RemoveEmptyEntries);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = AssetManager.TrimString(strArray[index]);
    return strArray;
  }

  public static Color ConvertToColor(string s)
  {
    s = s.Trim('\t', ' ');
    string[] strArray = s.Split(","[0], StringSplitOptions.None);
    return new Color((float) int.Parse(strArray[0]) / (float) byte.MaxValue, (float) int.Parse(strArray[1]) / (float) byte.MaxValue, (float) int.Parse(strArray[2]) / (float) byte.MaxValue);
  }

  public static string ConvertColorToRGB(Color c)
  {
    float num1 = (float) Mathf.RoundToInt(c.r * (float) byte.MaxValue);
    float num2 = (float) Mathf.RoundToInt(c.g * (float) byte.MaxValue);
    float num3 = (float) Mathf.RoundToInt(c.b * (float) byte.MaxValue);
    return num1.ToString() + ", " + num2.ToString() + ", " + num3.ToString();
  }
}
