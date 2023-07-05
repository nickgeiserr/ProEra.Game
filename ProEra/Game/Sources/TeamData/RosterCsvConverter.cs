// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.RosterCsvConverter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [ExecuteInEditMode]
  public class RosterCsvConverter : MonoBehaviour, ICsvConverter
  {
    [SerializeField]
    private RosterTransferObject[] objectsToConvert;
    private static readonly string[] CityAbbreviations = new string[32]
    {
      "ARI",
      "ATL",
      "BAL",
      "BUF",
      "CAR",
      "CHI",
      "CIN",
      "CLE",
      "DAL",
      "DEN",
      "DET",
      "GB",
      "HOU",
      "IND",
      "JAC",
      "KC",
      "LV",
      "LAC",
      "LAR",
      "MIA",
      "MIN",
      "NE",
      "NO",
      "NYG",
      "NYJ",
      "PHI",
      "PIT",
      "SF",
      "SEA",
      "TB",
      "TEN",
      "WAS"
    };

    public void ConvertObjects()
    {
      Debug.Log((object) "Converting objects...");
      for (int index = 0; index < this.objectsToConvert.Length; ++index)
      {
        this.objectsToConvert[index].rosterDataObject.RosterFileData.CityAbbreviation = RosterCsvConverter.CityAbbreviations[index];
        this.objectsToConvert[index].defaultPlayerDataObject.DefaultPlayerData.SetDataFromCsv(this.objectsToConvert[index].defaultPlayerCsvFile.text);
        this.objectsToConvert[index].rosterDataObject.RosterFileData.SetDataFromCsv(this.objectsToConvert[index].rosterCsvFile.text, this.objectsToConvert[index].defaultPlayerDataObject.DefaultPlayerData);
        Debug.Log((object) ("Finished converting " + this.objectsToConvert[index].rosterDataObject.name));
        Debug.Log((object) JsonConvert.SerializeObject((object) this.objectsToConvert[index].rosterDataObject));
      }
      Debug.Log((object) ("Roster Collection: " + JsonConvert.SerializeObject((object) ((IEnumerable<RosterTransferObject>) this.objectsToConvert).Select<RosterTransferObject, RosterDataObject>((Func<RosterTransferObject, RosterDataObject>) (x => x.rosterDataObject)).ToArray<RosterDataObject>())));
    }
  }
}
