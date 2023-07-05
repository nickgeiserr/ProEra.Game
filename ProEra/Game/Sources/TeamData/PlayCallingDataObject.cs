// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.PlayCallingDataObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UDB;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [CreateAssetMenu(menuName = "Data/Play Calling")]
  public class PlayCallingDataObject : ScriptableObject
  {
    [Header("IF THIS CHANGES PLEASE GO TO")]
    [Header("P_TEAMRESOURCESMANAGER TO REBUILD")]
    [SerializeField]
    private PlayCallingDataMap _dataMap = new PlayCallingDataMap();
    [SerializeField]
    [HideInInspector]
    public List<string> DataKeys = new List<string>();
    [SerializeField]
    [HideInInspector]
    public List<int> DataValues = new List<int>();

    public PlayCallingDataMap DataMap => this._dataMap;

    public void SetDataFromConfigFile(string config)
    {
      foreach (string str in PlayCallingDataObject.ReadLinesFromConfigFile(config))
      {
        string[] strArray = str.Split('=', StringSplitOptions.None);
        this._dataMap.Add(strArray[0], int.Parse(strArray[1]));
      }
      this.BuildArrays();
    }

    public void BuildArrays()
    {
      List<string> keysList = this.DataMap.keysList;
      this.DataKeys.Clear();
      this.DataValues.Clear();
      for (int index = 0; index < keysList.Count; ++index)
      {
        this.DataKeys.Add(AssetManager.TrimString(keysList[index]));
        this.DataValues.Add(this.DataMap.GetValue(keysList[index]));
      }
    }

    public static IEnumerable<string> ReadLinesFromConfigFile(string config) => ((IEnumerable<string>) config.Split('\n', StringSplitOptions.None)).Where<string>((Func<string, bool>) (line => !line.StartsWith("#") && !line.IsEmptyOrWhiteSpaceOrNull())).Select<string, string>((Func<string, string>) (line => line.Trim('\r')));
  }
}
