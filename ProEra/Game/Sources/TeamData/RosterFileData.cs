// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.RosterFileData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UDB;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [MessagePackObject(false)]
  [Serializable]
  public class RosterFileData
  {
    [IgnoreMember]
    private static readonly Dictionary<string, Position> PositionMap = new Dictionary<string, Position>()
    {
      {
        "QB",
        Position.QB
      },
      {
        "RB",
        Position.RB
      },
      {
        "FB",
        Position.FB
      },
      {
        "WR",
        Position.WR
      },
      {
        "SLT",
        Position.SLT
      },
      {
        "TE",
        Position.TE
      },
      {
        "OL",
        Position.OL
      },
      {
        "LT",
        Position.LT
      },
      {
        "LG",
        Position.LG
      },
      {
        "C",
        Position.C
      },
      {
        "RG",
        Position.RG
      },
      {
        "RT",
        Position.RT
      },
      {
        "K",
        Position.K
      },
      {
        "P",
        Position.P
      },
      {
        "KR",
        Position.KR
      },
      {
        "PR",
        Position.PR
      },
      {
        "GUN",
        Position.GUN
      },
      {
        "DL",
        Position.DL
      },
      {
        "DT",
        Position.DT
      },
      {
        "NT",
        Position.NT
      },
      {
        "DE",
        Position.DE
      },
      {
        "LB",
        Position.LB
      },
      {
        "OLB",
        Position.OLB
      },
      {
        "ILB",
        Position.ILB
      },
      {
        "MLB",
        Position.MLB
      },
      {
        "CB",
        Position.CB
      },
      {
        "FS",
        Position.FS
      },
      {
        "SS",
        Position.SS
      },
      {
        "DB",
        Position.DB
      },
      {
        "BLK",
        Position.BLK
      },
      {
        "None",
        Position.None
      }
    };
    [SerializeField]
    [Key(0)]
    public int Version = -1;
    [SerializeField]
    [Key(1)]
    public string CityAbbreviation;
    [SerializeField]
    [Key(2)]
    public RosterPlayerData[] Players;
    [SerializeField]
    [Key(3)]
    public DefaultPlayerFileData DefaultPlayerDataObject;

    public void SetDataFromCsv(string csv, DefaultPlayerFileData defaultPlayerDataObject)
    {
      string[] array1 = ((IEnumerable<string>) csv.Trim().Split('\n', StringSplitOptions.None)).Where<string>((Func<string, bool>) (line => !line.IsEmptyOrWhiteSpaceOrNull())).Skip<string>(1).ToArray<string>();
      this.Players = new RosterPlayerData[array1.Length];
      for (int index = 0; index < array1.Length; ++index)
      {
        this.Players[index] = new RosterPlayerData();
        string[] array2 = ((IEnumerable<string>) array1[index].Split(',', StringSplitOptions.None)).ToArray<string>();
        this.Players[index].index = int.Parse(array2[0]);
        this.Players[index].firstName = array2[1];
        this.Players[index].lastName = array2[2];
        this.Players[index].skin = int.Parse(array2[3]);
        this.Players[index].number = int.Parse(array2[4]);
        this.Players[index].height = int.Parse(array2[5]);
        this.Players[index].weight = int.Parse(array2[6]);
        this.Players[index].position = array2[7];
        this.Players[index].speed = int.Parse(array2[8]);
        this.Players[index].tackleBreak = int.Parse(array2[9]);
        this.Players[index].fumble = int.Parse(array2[10]);
        this.Players[index].catching = int.Parse(array2[11]);
        this.Players[index].blocking = int.Parse(array2[12]);
        this.Players[index].throwingAccuracy = int.Parse(array2[13]);
        this.Players[index].kickingPower = int.Parse(array2[14]);
        this.Players[index].kickingAccuracy = int.Parse(array2[15]);
        this.Players[index].blockBreak = int.Parse(array2[16]);
        this.Players[index].tackle = int.Parse(array2[17]);
        this.Players[index].throwingPower = int.Parse(array2[18]);
        this.Players[index].fitness = int.Parse(array2[19]);
        this.Players[index].awareness = int.Parse(array2[20]);
        this.Players[index].agility = int.Parse(array2[21]);
        this.Players[index].cover = int.Parse(array2[22]);
        this.Players[index].hitPower = int.Parse(array2[23]);
        this.Players[index].endurance = int.Parse(array2[24]);
        this.Players[index].visor = int.Parse(array2[25]);
        this.Players[index].sleeves = int.Parse(array2[26]);
        this.Players[index].bands = int.Parse(array2[27]);
        this.Players[index].wraps = int.Parse(array2[28]);
        this.Players[index].age = int.Parse(array2[29]);
        this.Players[index].potential = int.Parse(array2[30]);
        this.Players[index].portrait = int.Parse(array2[31]);
        this.Players[index].discipline = int.Parse(array2[32]);
        this.Players[index].avatarId = array2[33].Trim('\r');
        int result;
        this.Players[index].isLeftHanded = array2.Length <= 34 || !int.TryParse(array2[34], out result) ? 0 : result;
      }
      this.DefaultPlayerDataObject = defaultPlayerDataObject;
    }

    public RosterData ToRosterData()
    {
      PlayerData[] players = new PlayerData[this.Players.Length];
      for (int index = 0; index < this.Players.Length; ++index)
        players[index] = new PlayerData()
        {
          IndexOnTeam = this.Players[index].index,
          FirstName = this.Players[index].firstName,
          LastName = this.Players[index].lastName,
          SkinColor = this.Players[index].skin,
          Number = this.Players[index].number,
          Height = this.Players[index].height,
          Weight = this.Players[index].weight,
          PlayerPosition = RosterFileData.PositionMap[this.Players[index].position],
          Speed = this.Players[index].speed,
          TackleBreaking = this.Players[index].tackleBreak,
          Fumbling = this.Players[index].fumble,
          Catching = this.Players[index].catching,
          Blocking = this.Players[index].blocking,
          ThrowAccuracy = this.Players[index].throwingAccuracy,
          KickPower = this.Players[index].kickingPower,
          KickAccuracy = this.Players[index].kickingAccuracy,
          BlockBreaking = this.Players[index].blockBreak,
          Tackling = this.Players[index].tackle,
          ThrowPower = this.Players[index].throwingPower,
          Fitness = this.Players[index].fitness,
          Awareness = this.Players[index].awareness,
          Agility = this.Players[index].agility,
          Coverage = this.Players[index].cover,
          HitPower = this.Players[index].hitPower,
          Endurance = this.Players[index].endurance,
          Visor = this.Players[index].visor,
          Sleeves = this.Players[index].sleeves,
          Bands = this.Players[index].bands,
          Wraps = this.Players[index].wraps,
          Age = this.Players[index].age,
          Potential = this.Players[index].potential,
          PortraitID = this.Players[index].portrait,
          Discipline = this.Players[index].discipline,
          AvatarID = this.Players[index].avatarId,
          IsLeftHanded = this.Players[index].isLeftHanded
        };
      return new RosterData(players, this.DefaultPlayerDataObject.ToDictionary());
    }
  }
}
