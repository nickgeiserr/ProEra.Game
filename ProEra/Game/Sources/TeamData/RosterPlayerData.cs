// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.RosterPlayerData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [MessagePackObject(false)]
  [Serializable]
  public struct RosterPlayerData
  {
    [SerializeField]
    [Key(0)]
    public int index;
    [SerializeField]
    [Key(1)]
    public string firstName;
    [SerializeField]
    [Key(2)]
    public string lastName;
    [SerializeField]
    [Key(3)]
    public int skin;
    [SerializeField]
    [Key(4)]
    public int number;
    [SerializeField]
    [Key(5)]
    public int height;
    [SerializeField]
    [Key(6)]
    public int weight;
    [SerializeField]
    [Key(7)]
    public string position;
    [SerializeField]
    [Key(8)]
    public int speed;
    [SerializeField]
    [Key(9)]
    public int tackleBreak;
    [SerializeField]
    [Key(10)]
    public int fumble;
    [SerializeField]
    [Key(11)]
    public int catching;
    [SerializeField]
    [Key(12)]
    public int blocking;
    [SerializeField]
    [Key(13)]
    public int throwingAccuracy;
    [SerializeField]
    [Key(14)]
    public int kickingPower;
    [SerializeField]
    [Key(15)]
    public int kickingAccuracy;
    [SerializeField]
    [Key(16)]
    public int blockBreak;
    [SerializeField]
    [Key(17)]
    public int tackle;
    [SerializeField]
    [Key(18)]
    public int throwingPower;
    [SerializeField]
    [Key(19)]
    public int fitness;
    [SerializeField]
    [Key(20)]
    public int awareness;
    [SerializeField]
    [Key(21)]
    public int agility;
    [SerializeField]
    [Key(22)]
    public int cover;
    [SerializeField]
    [Key(23)]
    public int hitPower;
    [SerializeField]
    [Key(24)]
    public int endurance;
    [SerializeField]
    [Key(25)]
    public int visor;
    [SerializeField]
    [Key(26)]
    public int sleeves;
    [SerializeField]
    [Key(27)]
    public int bands;
    [SerializeField]
    [Key(28)]
    public int wraps;
    [SerializeField]
    [Key(29)]
    public int age;
    [SerializeField]
    [Key(30)]
    public int potential;
    [SerializeField]
    [Key(31)]
    public int portrait;
    [SerializeField]
    [Key(32)]
    public int discipline;
    [SerializeField]
    [Key(33)]
    public string avatarId;
    [SerializeField]
    [Key(34)]
    public int isLeftHanded;
  }
}
