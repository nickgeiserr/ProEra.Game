// Decompiled with JetBrains decompiler
// Type: UniformAssetManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using UnityEngine;

public class UniformAssetManager : MonoBehaviour
{
  public static UniformSet GetUniformSet(int teamIndex)
  {
    teamIndex = AssetManager.GetModifiedTeamIndex(teamIndex);
    return teamIndex < TeamAssetManager.NUMBER_OF_BASE_TEAMS ? UniformResourcesManager.LoadUniformSet(teamIndex) : UniformModManager.LoadUniformSet(teamIndex - TeamAssetManager.NUMBER_OF_BASE_TEAMS);
  }

  private static void UnloadUniformSet(UniformSet uni)
  {
    int modifiedTeamIndex = AssetManager.GetModifiedTeamIndex(uni.teamIndex);
    if (modifiedTeamIndex < TeamAssetManager.NUMBER_OF_BASE_TEAMS)
      UniformResourcesManager.UnloadUniform(modifiedTeamIndex);
    else
      uni.ClearAllTextures();
  }

  public static void InitializeUniformModManager() => UniformModManager.Init(ModManager.GetAppPath() + ModManager.GetModPath() + "Team Mods/");

  public static void DeleteUniform(int teamIndex, string uniformName)
  {
    if (teamIndex < TeamAssetManager.GetNumberOfBaseTeams())
      return;
    UniformModManager.DeleteUniform(teamIndex - TeamAssetManager.GetNumberOfBaseTeams(), uniformName);
  }

  public static bool SaveUniformSet_IsFilenameAvailable(int teamIndex, string newFilename) => UniformModManager.SaveUniformSet_IsFilenameAvailable(newFilename, teamIndex);

  public static bool SaveUniformSet_IsDirectoryAvailable(string newCityName) => UniformModManager.SaveUniformSet_IsDirectoryAvailable(newCityName);

  public static string SaveUniformSet_WriteToFile(
    int teamIndex,
    string newFileName,
    UniformSet uniformSet)
  {
    return UniformModManager.SaveUniformSet_WriteToFile(newFileName, teamIndex, uniformSet);
  }

  public static string SaveNew_WriteToFile(
    string copyFromTeamCity,
    string newTeamName,
    string newCityName,
    string newAbbrev,
    string newUniformName,
    UniformSet uniformSet)
  {
    return UniformModManager.SaveNew_WriteToFile(copyFromTeamCity, newTeamName, newCityName, newAbbrev, newUniformName, uniformSet);
  }
}
