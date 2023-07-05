// Decompiled with JetBrains decompiler
// Type: ModManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class ModManager : MonoBehaviour
{
  public static ModManager self;
  public TeamModManager teamManager;
  public static string appPath;
  public static string modPath = "/Mods/";
  private string[] userModFolders;
  private static readonly string[] ValidExtensions = new string[4]
  {
    ".png",
    ".jpg",
    ".jpeg",
    ".tga"
  };

  public static string AppModPath => Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/Mods/";

  private void Awake()
  {
    if ((UnityEngine.Object) ModManager.self == (UnityEngine.Object) null)
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      ModManager.self = this;
    }
    else
    {
      if (!((UnityEngine.Object) ModManager.self != (UnityEngine.Object) this))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
  }

  public void Init()
  {
    ModManager.appPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
    this.CreateUserModFolders();
    ModManager.self.teamManager.Path(ModManager.appPath + ModManager.modPath + "Team Mods/");
    ModManager.self.teamManager.Init();
  }

  public void CreateUserModFolders()
  {
    this.userModFolders = Directory.GetDirectories(ModManager.appPath + ModManager.modPath + "Team Mods/");
    for (int index = 0; index < this.userModFolders.Length; ++index)
    {
      int num = this.userModFolders[index].LastIndexOf("/");
      string str = this.userModFolders[index].Substring(num + 1);
      bool flag1 = false;
      Utility.Logging.Logger logger = new Utility.Logging.Logger(ModManager.appPath + ModManager.modPath + "/Logs/", "StartupLoadMods", "txt", true);
      try
      {
        bool flag2 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/TEAM.TXT");
        bool flag3 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/PLAYCALLING.TXT");
        bool flag4 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/ROSTER.CSV");
        bool flag5 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/COACHINGSTAFF.CSV");
        bool flag6 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/endzone.png");
        bool flag7 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/large_logo.png");
        bool flag8 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/small_logo.png");
        bool flag9 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/tiny_logo.png");
        bool flag10 = File.Exists(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/medium_logo.png");
        bool flag11 = ((IEnumerable<string>) Directory.GetFiles(ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/Uniforms/")).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) UniformModManager.UniformFileExtensions).Contains<string>(Path.GetExtension(file).ToLower()))).ToArray<string>().Length != 0;
        if (!flag2)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/TEAM.TXT, not loading this team");
          flag1 = true;
        }
        if (!flag3)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/PLAYCALLING.TXT, not loading this team");
          flag1 = true;
        }
        if (!flag4)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/ROSTER.CSV.TXT, not loading this team");
          flag1 = true;
        }
        if (!flag5)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/COACHINGSTAFF.CSV, not loading this team");
          flag1 = true;
        }
        if (!flag11)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find any uniform files in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/Uniforms/, not loading this team");
          flag1 = true;
        }
        if (!flag6)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find endzone.png in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + ", not loading this team");
          flag1 = true;
        }
        if (!flag7)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find large_logo.png in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + ", not loading this team");
          flag1 = true;
        }
        if (!flag8)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find any uniform files in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + "/Uniforms/, not loading this team");
          flag1 = true;
        }
        if (!flag9)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find tiny_logo.png in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + ", not loading this team");
          flag1 = true;
        }
        if (!flag10)
        {
          logger.Log(Utility.Logging.LogType.ERROR, "Could not find medium_logo.png in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + ", not loading this team");
          flag1 = true;
        }
        if (flag1)
          this.userModFolders[index] = "REMOVE";
        else
          this.userModFolders[index] = str;
      }
      catch
      {
        logger.Log(Utility.Logging.LogType.ERROR, "An unknown error occurred when trying to parse something in " + ModManager.appPath + ModManager.modPath + "Team Mods/" + str + ", not loading this team");
      }
    }
    this.userModFolders = this.RemoveExtraDirectories(this.userModFolders);
  }

  private string[] RemoveExtraDirectories(string[] folders)
  {
    List<string> stringList = new List<string>();
    stringList.AddRange((IEnumerable<string>) folders);
    for (int index = stringList.Count - 1; index >= 0; --index)
    {
      if (stringList[index] == "SAMPLE" || stringList[index] == "REMOVE")
        stringList.RemoveAt(index);
    }
    return stringList.ToArray();
  }

  public static int getCountOfDraftClasses() => Directory.GetFiles(ModManager.appPath + ModManager.modPath + "Drafts/", "*.txt", SearchOption.TopDirectoryOnly).Length;

  public static Texture2D LoadCustomField(string fieldName)
  {
    string[] array = ((IEnumerable<string>) Directory.GetFiles(Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + ModManager.modPath + "Fields/")).Where<string>((Func<string, bool>) (file => ((IEnumerable<string>) ModManager.ValidExtensions).Contains<string>(Path.GetExtension(file).ToLower()))).ToArray<string>();
    string regex = ".+?" + fieldName.ToLower() + "\\.[png|jpg|jpeg|tga]";
    Func<string, bool> predicate = (Func<string, bool>) (f => Regex.IsMatch(f.ToLower(), regex));
    return ModManager.CreateTexture(((IEnumerable<string>) array).Where<string>(predicate).SingleOrDefault<string>() ?? throw new FileNotFoundException("Could not find a field with name: '" + fieldName + "'"));
  }

  public static List<CustomPlaybook> LoadCustomPlaybooks()
  {
    List<CustomPlaybook> customPlaybookList = new List<CustomPlaybook>();
    string path = "";
    string[] array;
    try
    {
      path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + ModManager.modPath + "Playbooks/";
      array = ((IEnumerable<string>) Directory.GetFiles(path)).Where<string>((Func<string, bool>) (file => Path.GetExtension(file).ToLower() == ".txt")).ToArray<string>();
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Could not find Custom Playbooks directory at " + path + ", continuing"));
      Debug.Log((object) ex.Message);
      return customPlaybookList;
    }
    for (int index = 0; index < array.Length; ++index)
    {
      try
      {
        CustomPlaybook customPlaybook = new CustomPlaybook(ModManager.ConvertLinesToDictionary(File.ReadAllText(array[index]).Split("\n"[0], StringSplitOptions.None)));
        customPlaybookList.Add(customPlaybook);
      }
      catch (Exception ex)
      {
        Debug.Log((object) ("Could not load playbook at " + array[index] + ", continuing"));
        Debug.Log((object) ex.Message);
      }
    }
    return customPlaybookList;
  }

  public static void SaveLinesToTextFile(
    string folderUnderMods,
    string filenameWithExtension,
    string[] lines)
  {
    if (!Directory.Exists(ModManager.AppModPath + folderUnderMods))
    {
      Debug.Log((object) ("Could not find the folder '" + folderUnderMods + "' under the Mods folder!"));
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string line in lines)
        stringBuilder.Append(line).Append(Environment.NewLine);
      string path = ModManager.AppModPath + folderUnderMods + Path.DirectorySeparatorChar.ToString() + filenameWithExtension;
      if (File.Exists(path))
        File.Delete(path);
      File.WriteAllText(path, stringBuilder.ToString());
    }
  }

  public static Dictionary<string, string> LoadNameValuePairTextFile(string pathAndFilename)
  {
    string str = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/Mods/";
    return File.Exists(str + pathAndFilename) ? ModManager.ConvertLinesToDictionary(File.ReadAllText(str + pathAndFilename).Split("\n"[0], StringSplitOptions.None)) : new Dictionary<string, string>();
  }

  private static Dictionary<string, string> ConvertLinesToDictionary(string[] lines)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    for (int index = 0; index < lines.Length; ++index)
    {
      if (lines[index].Length <= 0 || lines[index][0] != '#')
      {
        string[] strArray = lines[index].Split('=', StringSplitOptions.None);
        if (strArray.Length == 2)
        {
          string key = Utils.TrimString(strArray[0]);
          string str = Utils.TrimString(strArray[1]);
          if (key.Length > 0 && str.Length > 0)
            dictionary.Add(key, str);
        }
      }
    }
    return dictionary;
  }

  public static float? ParseFloat(string value)
  {
    float result;
    return float.TryParse(value, out result) ? new float?(result) : new float?();
  }

  private static Texture2D CreateTexture(string file)
  {
    Texture2D tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
    tex.LoadImage(File.ReadAllBytes(file));
    tex.anisoLevel = 1;
    return tex;
  }

  public int GetCountOfTeamFolders() => this.userModFolders.Length;

  public string GetTeamFolderAt(int i) => this.userModFolders[i];

  public int GetIndexOfTeam(string teamCity)
  {
    for (int indexOfTeam = 0; indexOfTeam < this.userModFolders.Length; ++indexOfTeam)
    {
      if (this.userModFolders[indexOfTeam].ToUpper() == teamCity.ToUpper())
        return indexOfTeam;
    }
    return 0;
  }

  public static string GetAppPath() => ModManager.appPath;

  public static string GetModPath() => ModManager.modPath;
}
