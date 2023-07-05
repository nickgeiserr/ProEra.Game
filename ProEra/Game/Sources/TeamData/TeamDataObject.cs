// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.TeamDataObject
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
  [CreateAssetMenu(menuName = "Data/Team")]
  public class TeamDataObject : ScriptableObject
  {
    [Header("IF THIS CHANGES PLEASE GO TO")]
    [Header("P_TEAMRESOURCESMANAGER TO REBUILD")]
    [SerializeField]
    private TeamDataMap _dataMap = new TeamDataMap();
    [SerializeField]
    [HideInInspector]
    public List<string> DataKeys = new List<string>();
    [SerializeField]
    [HideInInspector]
    public List<string> DataValues = new List<string>();

    public TeamDataMap DataMap => this._dataMap;

    public void SetDataFromConfigFile(string config)
    {
      foreach (string str in ((IEnumerable<string>) config.Split('\n', StringSplitOptions.None)).Where<string>((Func<string, bool>) (line => !line.IsEmptyOrWhiteSpaceOrNull())).ToArray<string>())
      {
        string[] strArray = str.Split('=', StringSplitOptions.None);
        this._dataMap.Add(strArray[0].Trim('\r'), strArray[1].Trim('\r'));
      }
    }
  }
}
