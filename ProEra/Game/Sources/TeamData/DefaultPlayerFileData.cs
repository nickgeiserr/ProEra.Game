// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.DefaultPlayerFileData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [MessagePackObject(false)]
  [Serializable]
  public class DefaultPlayerFileData
  {
    [SerializeField]
    [Key(0)]
    public DefaultPlayerData[] DefaultPlayerCollection;

    public void SetDataFromCsv(string csv)
    {
      string[] array = ((IEnumerable<string>) csv.Trim().Split('\n', StringSplitOptions.None)).Skip<string>(1).ToArray<string>();
      this.DefaultPlayerCollection = new DefaultPlayerData[array.Length];
      for (int index = 0; index < array.Length; ++index)
      {
        string[] strArray = array[index].Split(',', StringSplitOptions.None);
        this.DefaultPlayerCollection[index] = new DefaultPlayerData()
        {
          position = strArray[0].Trim(),
          index = int.Parse(strArray[1])
        };
      }
    }

    public Dictionary<string, int> ToDictionary()
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (DefaultPlayerData defaultPlayer in this.DefaultPlayerCollection)
        dictionary[defaultPlayer.position] = defaultPlayer.index;
      return dictionary;
    }
  }
}
