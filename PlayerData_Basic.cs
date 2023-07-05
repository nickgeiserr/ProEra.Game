// Decompiled with JetBrains decompiler
// Type: PlayerData_Basic
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class PlayerData_Basic
{
  [Key(0)]
  public string FirstName { get; set; }

  [Key(1)]
  public string LastName { get; set; }

  [Key(2)]
  public Position PlayerPosition { get; set; }

  [IgnoreMember]
  public string FullName => this.FirstName + " " + this.LastName;

  [IgnoreMember]
  public string FirstInitalAndLastName => this.FirstName.Substring(0, 1) + ". " + this.LastName;

  [Key(3)]
  public int Number { get; set; }

  [Key(4)]
  public int SkinColor { get; set; }

  [Key(5)]
  public int Height { get; set; }

  [Key(6)]
  public int Weight { get; set; }

  [Key(7)]
  public int Age { get; set; }

  [Key(8)]
  public int PortraitID { get; set; }

  public PlayerData_Basic()
  {
  }

  public PlayerData_Basic(PlayerData source)
  {
    this.FirstName = source.FirstName;
    this.LastName = source.LastName;
    this.PlayerPosition = source.PlayerPosition;
    this.Number = source.Number;
    this.SkinColor = source.SkinColor;
    this.Height = source.Height;
    this.Weight = source.Weight;
    this.Age = source.Age;
    this.PortraitID = source.PortraitID;
  }
}
