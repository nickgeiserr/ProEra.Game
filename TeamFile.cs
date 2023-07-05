// Decompiled with JetBrains decompiler
// Type: TeamFile
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class TeamFile
{
  [Key(0)]
  public Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
  [IgnoreMember]
  private const string NOT_FOUND = "NOT FOUND";
  private static readonly HashSet<string> KeysToRetainCase = new HashSet<string>()
  {
    "KeysToTheGame"
  };

  public void AddNameValuePair(string name, string value)
  {
    if (TeamFile.KeysToRetainCase.Contains(name))
      this.nameValuePairs.Add(name, value);
    else
      this.nameValuePairs.Add(name, value.ToUpper());
  }

  public Dictionary<string, string> GetAllNameValuePairs() => this.nameValuePairs;

  public string GetCity() => this.SafeLoadValue("TeamCity");

  public string GetName() => this.SafeLoadValue("TeamName");

  public string GetAbbreviation() => this.SafeLoadValue("TeamAbbrev");

  public string GetOffensivePlaybook()
  {
    string str = this.SafeLoadValue("OffPlaybook");
    return str == "NOT FOUND" ? "SINGLEBACK" : str;
  }

  public string GetDefensivePlaybook()
  {
    string str = this.SafeLoadValue("DefPlaybook");
    return str == "NOT FOUND" ? "FOUR THREE" : str;
  }

  public string GetCustomExternalFieldTextureName()
  {
    string str = this.SafeLoadValue("TeamField");
    return str == "NOT FOUND" ? "" : str;
  }

  public float GetCustomFieldHashLocation()
  {
    float result;
    return float.TryParse(this.SafeLoadValue("TeamFieldHashLocation"), out result) ? result : Field.DEFAULT_HASH_OFFSET;
  }

  public float GetCustomFieldPATLocation()
  {
    float result;
    return float.TryParse(this.SafeLoadValue("TeamFieldPATLocation"), out result) ? result : Field.DEFAULT_PAT_LOCATION;
  }

  public Color GetPrimaryColor() => this.SafeLoadColor("PrimaryColor");

  public Color GetSecondaryColor() => this.SafeLoadColor("SecondaryColor");

  public Color GetAlternateColor() => this.SafeLoadColor("AlternateColor");

  public string GetLeague()
  {
    string str = this.SafeLoadValue("League");
    return str.ToUpper() == "NOT FOUND" ? "AXIS LEAGUE" : str;
  }

  public List<int> GetRivals() => this.SafeLoadList<int>("Rivals");

  public List<string> GetKeysToTheGame() => this.SafeLoadList<string>("KeysToTheGame", ';');

  public float GetAvgBlitzPercent() => this.SafeLoadFloat("AvgBlitzPercent");

  public float GetAvgManPercent() => this.SafeLoadFloat("AvgManPercent");

  public float GetAvgPassPercent() => this.SafeLoadFloat("AvgPassPercent");

  public float GetAvgRunPercent() => this.SafeLoadFloat("AvgRunPercent");

  public void SetCity(string value) => this.nameValuePairs["TeamCity"] = value;

  public void SetName(string value) => this.nameValuePairs["TeamName"] = value;

  public void SetAbbreviation(string value) => this.nameValuePairs["TeamAbbrev"] = value;

  public void SetOffensivePlaybook(string value) => this.nameValuePairs["OffPlaybook"] = value;

  public void SetDefensivePlaybook(string value) => this.nameValuePairs["DefPlaybook"] = value;

  public void SetTeamHashLocation(string value) => this.nameValuePairs["TeamFieldHashLocation"] = value;

  public void SetTeamPATLocation(string value) => this.nameValuePairs["TeamFieldPATLocation"] = value;

  private string SafeLoadValue(string key)
  {
    string str;
    return this.nameValuePairs.TryGetValue(key, out str) ? str : "NOT FOUND";
  }

  private Color SafeLoadColor(string key)
  {
    string s = this.SafeLoadValue(key);
    try
    {
      return AssetManager.ConvertToColor(s);
    }
    catch
    {
      return new Color(1f, 0.0f, 1f);
    }
  }

  private int SafeLoadInt(string key, int backup)
  {
    string s = this.SafeLoadValue(key);
    try
    {
      return int.Parse(s);
    }
    catch
    {
      Debug.Log((object) ("Hit safe load int value for key: " + key));
      return backup;
    }
  }

  private float SafeLoadFloat(string key)
  {
    string s = this.SafeLoadValue(key);
    try
    {
      return float.Parse(s);
    }
    catch
    {
      Debug.Log((object) ("Hit safe load float value for key: " + key));
      return -1f;
    }
  }

  private List<T> SafeLoadList<T>(string key, char delimiter = ',')
  {
    string str = this.SafeLoadValue(key);
    if (str == "NOT FOUND")
    {
      Debug.LogError((object) ("SafeLoadList: Value Not Found For Key : " + key));
      return new List<T>();
    }
    try
    {
      List<T> objList = new List<T>();
      foreach (object obj in str.Split(delimiter, StringSplitOptions.None))
        objList.Add((T) Convert.ChangeType(obj, typeof (T)));
      return objList;
    }
    catch
    {
      Debug.Log((object) ("Hit safe load list value for key: " + key));
      return new List<T>();
    }
  }
}
