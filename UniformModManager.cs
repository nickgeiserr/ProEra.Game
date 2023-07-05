// Decompiled with JetBrains decompiler
// Type: UniformModManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using ProEra.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class UniformModManager : MonoBehaviour
{
  public static string uniformModPath;
  public static readonly string[] ValidExtensions = new string[4]
  {
    ".png",
    ".jpg",
    ".jpeg",
    ".tga"
  };
  public static readonly string[] UniformFileExtensions = new string[2]
  {
    ".txt",
    ".rtf"
  };

  public static void Init(string p) => UniformModManager.uniformModPath = p;

  internal static UniformSet LoadUniformSet(int teamIndex)
  {
    TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.INFO, "Loading Uniforms for Mods Team #" + teamIndex.ToString());
    List<string> stringList = new List<string>();
    string[] uniformFilenames = UniformModManager.GetUniformFilenames(teamIndex);
    Dictionary<string, Texture2D> allTextures = new Dictionary<string, Texture2D>();
    for (int index = 0; index < uniformFilenames.Length; ++index)
      stringList.Add(UniformModManager.LoadUniformFile(uniformFilenames[index]));
    string[] namesOfUniformPieces1 = UniformModManager.GetNamesOfUniformPieces(UniformPiece.Jerseys, teamIndex);
    for (int pieceIndex = 0; pieceIndex < namesOfUniformPieces1.Length; ++pieceIndex)
    {
      string key = UniformModManager.RenameUniformPieceFilename(namesOfUniformPieces1[pieceIndex], UniformPiece.Jerseys);
      allTextures.Add(key, UniformModManager.LoadUniformTexture(teamIndex, UniformPiece.Jerseys, pieceIndex));
    }
    string[] namesOfUniformPieces2 = UniformModManager.GetNamesOfUniformPieces(UniformPiece.Helmets, teamIndex);
    for (int pieceIndex = 0; pieceIndex < namesOfUniformPieces2.Length; ++pieceIndex)
    {
      string key = UniformModManager.RenameUniformPieceFilename(namesOfUniformPieces2[pieceIndex], UniformPiece.Helmets);
      allTextures.Add(key, UniformModManager.LoadUniformTexture(teamIndex, UniformPiece.Helmets, pieceIndex));
    }
    string[] namesOfUniformPieces3 = UniformModManager.GetNamesOfUniformPieces(UniformPiece.Pants, teamIndex);
    for (int pieceIndex = 0; pieceIndex < namesOfUniformPieces3.Length; ++pieceIndex)
    {
      string key = UniformModManager.RenameUniformPieceFilename(namesOfUniformPieces3[pieceIndex], UniformPiece.Pants);
      allTextures.Add(key, UniformModManager.LoadUniformTexture(teamIndex, UniformPiece.Pants, pieceIndex));
    }
    UniformSet data = new UniformSet(stringList.ToArray(), allTextures, teamIndex);
    data.LoadCustomBaseUniforms();
    return data;
  }

  private static List<Texture2D> ConvertTextureDictionaryToList(Dictionary<string, Texture2D> dict)
  {
    List<Texture2D> list = new List<Texture2D>();
    foreach (KeyValuePair<string, Texture2D> keyValuePair in dict)
      list.Add(keyValuePair.Value);
    return list;
  }

  private static string RenameUniformPieceFilename(string filename, UniformPiece type)
  {
    filename = filename.Replace(" ", "_");
    filename = filename.Replace("'", "");
    filename = filename.Replace(",", "");
    filename = filename.ToLower();
    return UniformResourcesManager.GetPieceNamePrefix(type) + "_" + filename;
  }

  public static string CleanUniformPieceName(string filename)
  {
    filename = filename.Replace(" ", "_");
    filename = filename.Replace("'", "");
    filename = filename.Replace(",", "");
    return Utils.TrimString(filename).ToUpper();
  }

  private static string LoadUniformFile(string fileName)
  {
    try
    {
      return File.ReadAllText(fileName);
    }
    catch (Exception ex)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Error loading Uniform File: '" + fileName + "'");
    }
    return "";
  }

  private static string[] GetUniformFilenames(int teamIndex) => ((IEnumerable<string>) Directory.GetFiles(UniformModManager.uniformModPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/Uniforms/")).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) UniformModManager.UniformFileExtensions).Contains<string>(Path.GetExtension(file).ToLower()))).ToArray<string>();

  public static Dictionary<string, string> GetUniformDetails(int teamIndex, string uniformName)
  {
    try
    {
      return Utils.ReadProperties(File.ReadAllText(uniformName));
    }
    catch (Exception ex)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Tried to load Uniform details for '" + uniformName + "' on Mods Team #" + teamIndex.ToString() + ", but failed");
      return (Dictionary<string, string>) null;
    }
  }

  public static string[] GetNamesOfUniformPieces(UniformPiece uniformPiece, int tIndex)
  {
    string str = UniformModManager.DirectoryForUniformPiece(uniformPiece, tIndex);
    string path1 = UniformModManager.uniformModPath + str;
    List<string> stringList = new List<string>();
    if (Directory.Exists(path1))
    {
      foreach (string path2 in ((IEnumerable<string>) Directory.GetFiles(path1)).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) UniformModManager.ValidExtensions).Contains<string>(Path.GetExtension(file).ToLower()))))
        stringList.Add(Path.GetFileNameWithoutExtension(path2));
    }
    return stringList.ToArray();
  }

  public static int GetNumberOfUniformPieces(UniformPiece uniformPiece, int tIndex)
  {
    string str = UniformModManager.DirectoryForUniformPiece(uniformPiece, tIndex);
    return ((IEnumerable<string>) Directory.GetFiles(UniformModManager.uniformModPath + str)).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) UniformModManager.ValidExtensions).Contains<string>(Path.GetExtension(file).ToLower()))).Count<string>();
  }

  public static Texture2D LoadUniformTexture(
    int teamIndex,
    UniformPiece uniformPiece,
    int pieceIndex)
  {
    try
    {
      return UniformModManager.CreateTexture(((IEnumerable<string>) Directory.GetFiles(UniformModManager.uniformModPath + UniformModManager.DirectoryForUniformPiece(uniformPiece, teamIndex))).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) UniformModManager.ValidExtensions).Contains<string>(Path.GetExtension(file).ToLower()))).ToArray<string>()[pieceIndex], uniformPiece);
    }
    catch (Exception ex)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Could not load " + uniformPiece.ToString() + " number " + pieceIndex.ToString() + " for team #" + teamIndex.ToString());
      return (Texture2D) null;
    }
  }

  public static Texture2D LoadUniformTexture(
    int teamIndex,
    UniformPiece uniformPiece,
    string pieceName)
  {
    string path = UniformModManager.uniformModPath + UniformModManager.DirectoryForUniformPiece(uniformPiece, teamIndex);
    string[] array;
    try
    {
      array = ((IEnumerable<string>) Directory.GetFiles(path)).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) UniformModManager.ValidExtensions).Contains<string>(Path.GetExtension(file).ToLower()))).ToArray<string>();
    }
    catch (DirectoryNotFoundException ex)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Could not find the directory: '" + path + "'");
      return (Texture2D) null;
    }
    for (int index = 0; index < array.Length; ++index)
    {
      int startIndex = array[index].LastIndexOf('/') + 1;
      string str1 = array[index].Substring(startIndex);
      int length = str1.LastIndexOf('.');
      string str2 = str1.Substring(0, length);
      if (str2.ToLower() == pieceName.ToLower() || str2.Replace(" ", "_").ToLower() == pieceName.ToLower())
        return UniformModManager.CreateTexture(array[index], uniformPiece);
    }
    TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Found Could not find the texture in the Mods folder named: " + uniformPiece.ToString() + " : " + pieceName);
    return (Texture2D) null;
  }

  private static string GetUniformFilenameByIndex(int teamIndex, int uniformIndex)
  {
    string[] uniformFilenames = UniformModManager.GetUniformFilenames(teamIndex);
    string str = uniformFilenames[uniformIndex];
    int startIndex = str.LastIndexOf('/') + 1;
    return uniformFilenames[uniformIndex].Substring(startIndex, str.Length - startIndex);
  }

  private static Texture2D CreateTexture(string file, UniformPiece uniformPiece)
  {
    try
    {
      Texture2D tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
      tex.LoadImage(File.ReadAllBytes(file));
      if (uniformPiece != UniformPiece.Jerseys)
        tex.Compress(true);
      tex.anisoLevel = 1;
      return tex;
    }
    catch (Exception ex)
    {
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, "Error loading Uniform texture: '" + file + "'");
    }
    return (Texture2D) null;
  }

  public static void DeleteUniform(int teamIndex, string uniformName)
  {
    string path = UniformModManager.uniformModPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/Uniforms/" + uniformName + ".txt";
    if (File.Exists(path))
      File.Delete(path);
    else
      TeamModManager.TeamModsLogger.Log(Utility.Logging.LogType.ERROR, path + " does not exist");
  }

  public static bool SaveUniformSet_IsFilenameAvailable(string newFilename, int teamIndex)
  {
    string[] uniformFilenames = UniformModManager.GetUniformFilenames(teamIndex);
    bool flag = true;
    for (int index = 0; index < uniformFilenames.Length; ++index)
    {
      string upper = Path.GetFileNameWithoutExtension(uniformFilenames[index]).ToUpper();
      if (newFilename == upper)
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public static bool SaveUniformSet_IsDirectoryAvailable(string newCityName) => !Directory.Exists(Path.Combine(UniformModManager.uniformModPath, newCityName.ToUpper()));

  public static string SaveUniformSet_WriteToFile(
    string uniformName,
    int teamIndex,
    UniformSet uniformSet)
  {
    string path = UniformModManager.uniformModPath + ModManager.self.GetTeamFolderAt(teamIndex) + "/Uniforms/" + uniformName + ".txt";
    string newLine = Environment.NewLine;
    string str1 = "";
    UniformConfig uniformConfig = uniformSet.GetUniformConfig(uniformSet.GetLockedInUniform());
    string str2 = str1 + "UNIFORM_NAME=" + uniformName;
    int lockedInUniformPiece1 = uniformSet.GetLockedInUniformPiece(UniformPiece.Helmets);
    string pieceNameByIndex1 = uniformSet.GetUniformPieceNameByIndex(UniformPiece.Helmets, lockedInUniformPiece1);
    string str3 = str2 + newLine + "HELMET_NAME=" + pieceNameByIndex1;
    int lockedInUniformPiece2 = uniformSet.GetLockedInUniformPiece(UniformPiece.Jerseys);
    string pieceNameByIndex2 = uniformSet.GetUniformPieceNameByIndex(UniformPiece.Jerseys, lockedInUniformPiece2);
    string str4 = str3 + newLine + "JERSEY_NAME=" + pieceNameByIndex2;
    int lockedInUniformPiece3 = uniformSet.GetLockedInUniformPiece(UniformPiece.Pants);
    string pieceNameByIndex3 = uniformSet.GetUniformPieceNameByIndex(UniformPiece.Pants, lockedInUniformPiece3);
    string contents = str4 + newLine + "PANT_NAME=" + pieceNameByIndex3 + newLine + "NUMBER_FONT_INDEX=" + uniformConfig.NumberFontIndex + newLine + "NUMBER_FILL_COLOR=" + uniformConfig.NumberFillColor + newLine + "NUMBER_OUTLINE1_COLOR=" + uniformConfig.NumberOutline1Color + newLine + "NUMBER_OUTLINE2_COLOR=" + uniformConfig.NumberOutline2Color + newLine + "LETTER_FONT_INDEX=" + uniformConfig.LetterFontIndex + newLine + "LETTER_FILL_COLOR=" + uniformConfig.LetterFillColor + newLine + "LETTER_OUTLINE_COLOR=" + uniformConfig.LetterOutlineColor + newLine + "VISOR_COLOR=" + uniformConfig.VisorColor + newLine + "ARM_SLEEVE_COLOR=" + uniformConfig.ArmSleeveColor + newLine + "ARM_BAND_COLOR=" + uniformConfig.ArmBandColor + newLine + "HAS_SLEEVE_NUMBERS=" + uniformConfig.HasSleeveNumber + newLine + "HAS_SHOULDER_NUMBERS=" + uniformConfig.HasShoulderNumber + newLine + "HELMET_TYPE=" + uniformConfig.HelmetType;
    File.WriteAllText(path, contents);
    return "SUCCESS!";
  }

  public static bool SaveNewTeam_IsFilenameAvailable(string newFilename, int teamIndex) => true;

  public static string SaveNew_WriteToFile(
    string copyFromTeamCity,
    string newTeamName,
    string newCityName,
    string newAbbrev,
    string newUniformName,
    UniformSet uniformSet)
  {
    string str = UniformModManager.uniformModPath + "/" + newCityName.ToUpper();
    UniformModManager.DirectoryCopy(Application.dataPath + "/Team Mods/" + copyFromTeamCity, str, true);
    UniformModManager.UpdateTextureNames(str, UniformPiece.Pants);
    UniformModManager.UpdateTextureNames(str, UniformPiece.Jerseys);
    UniformModManager.UpdateTextureNames(str, UniformPiece.Helmets);
    UniformModManager.SaveNew_SaveUniformConfig(str, newUniformName, uniformSet);
    return "SUCCESS!";
  }

  private static void SaveNew_SaveUniformConfig(
    string newPath,
    string newUniformName,
    UniformSet uniformSet)
  {
    string newLine = Environment.NewLine;
    string str1 = "";
    UniformConfig uniformConfig = uniformSet.GetUniformConfig(uniformSet.GetLockedInUniform());
    string str2 = str1 + newUniformName;
    int lockedInUniformPiece1 = uniformSet.GetLockedInUniformPiece(UniformPiece.Helmets);
    string pieceNameByIndex1 = uniformSet.GetUniformPieceNameByIndex(UniformPiece.Helmets, lockedInUniformPiece1);
    string str3 = str2 + newLine + pieceNameByIndex1;
    int lockedInUniformPiece2 = uniformSet.GetLockedInUniformPiece(UniformPiece.Jerseys);
    string pieceNameByIndex2 = uniformSet.GetUniformPieceNameByIndex(UniformPiece.Jerseys, lockedInUniformPiece2);
    string str4 = str3 + newLine + pieceNameByIndex2;
    int lockedInUniformPiece3 = uniformSet.GetLockedInUniformPiece(UniformPiece.Pants);
    string pieceNameByIndex3 = uniformSet.GetUniformPieceNameByIndex(UniformPiece.Pants, lockedInUniformPiece3);
    string contents = str4 + newLine + pieceNameByIndex3 + newLine + pieceNameByIndex3 + newLine + uniformConfig.NumberFontIndex + newLine + uniformConfig.NumberFillColor + newLine + uniformConfig.NumberOutline1Color + newLine + uniformConfig.NumberOutline2Color + newLine + uniformConfig.LetterFontIndex + newLine + uniformConfig.LetterFillColor + newLine + uniformConfig.LetterOutlineColor + newLine + uniformConfig.VisorColor + newLine + uniformConfig.ArmSleeveColor + newLine + uniformConfig.ArmBandColor + newLine + uniformConfig.HasSleeveNumber + newLine + uniformConfig.HasShoulderNumber + newLine + uniformConfig.HelmetType;
    newPath = newPath + "/Uniforms/" + newUniformName.ToUpper() + ".txt";
    File.WriteAllText(newPath, contents);
  }

  private static void UpdateTextureNames(string newPath, UniformPiece type)
  {
    string oldValue = "";
    string str = "";
    switch (type)
    {
      case UniformPiece.Pants:
        oldValue = "pants_";
        str = "Pants";
        break;
      case UniformPiece.Jerseys:
        oldValue = "jerseys_";
        str = "Jerseys";
        break;
      case UniformPiece.Helmets:
        oldValue = "helmets_";
        str = "Helmets";
        break;
    }
    newPath = newPath + "/Uniforms/" + str;
    foreach (FileInfo file in new DirectoryInfo(newPath).GetFiles())
      File.Move(file.FullName, file.FullName.Replace(oldValue, ""));
  }

  private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
  {
    DirectoryInfo directoryInfo1 = new DirectoryInfo(sourceDirName);
    DirectoryInfo[] directoryInfoArray = directoryInfo1.Exists ? directoryInfo1.GetDirectories() : throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
    if (!Directory.Exists(destDirName))
      Directory.CreateDirectory(destDirName);
    foreach (FileInfo file in directoryInfo1.GetFiles())
    {
      string destFileName1 = Path.Combine(destDirName, file.Name);
      if (file.Name.EndsWith(".CSV.TXT"))
      {
        string destFileName2 = Path.Combine(destDirName, file.Name.Substring(0, file.Name.Length - 4));
        file.CopyTo(destFileName2, false);
      }
      else if (!file.Name.EndsWith(".meta"))
        file.CopyTo(destFileName1, false);
    }
    if (!copySubDirs)
      return;
    foreach (DirectoryInfo directoryInfo2 in directoryInfoArray)
    {
      string destDirName1 = Path.Combine(destDirName, directoryInfo2.Name);
      UniformModManager.DirectoryCopy(directoryInfo2.FullName, destDirName1, copySubDirs);
    }
  }

  private static string DirectoryForUniformPiece(UniformPiece uniformPiece, int teamIndex)
  {
    switch (uniformPiece)
    {
      case UniformPiece.Pants:
        return ModManager.self.GetTeamFolderAt(teamIndex) + "/Uniforms/Pants/";
      case UniformPiece.Jerseys:
        return ModManager.self.GetTeamFolderAt(teamIndex) + "/Uniforms/Jerseys/";
      case UniformPiece.Helmets:
        return ModManager.self.GetTeamFolderAt(teamIndex) + "/Uniforms/Helmets/";
      default:
        return "";
    }
  }
}
