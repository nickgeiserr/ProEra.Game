// Decompiled with JetBrains decompiler
// Type: SavePlayerCustomization
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class SavePlayerCustomization
{
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public ETeamUniformId Uniform = ETeamUniformId.Ravens;
  [Key(2)]
  public int UniformNumber = 5;
  [Key(3)]
  public bool HomeUniform = true;
  [Key(4)]
  public EGlovesId GloveId;
  [Key(5)]
  public int MultiplayerTeamBallId = -1;
  [Key(6)]
  public Color TrailColor = Color.yellow;
  [Key(7)]
  public EBallTrail TrailType;
  [Key(8)]
  public Color NameColor = Color.black;
  [Key(9)]
  public Color WristbandColor = new Color(0.28f, 0.28f, 0.28f, 1f);
  [Key(10)]
  public string FirstName = "Troy";
  [Key(11)]
  public string LastName = "Jones";
  [Key(12)]
  public float BodyHeight = 1.7f;
  [Key(13)]
  public bool AvatarCustomized;
  [Key(14)]
  public EBodyType BodyType;
  [Key(15)]
  public int BodyModelId;
  [Key(16)]
  public int HeadModelId;
  [Key(17)]
  public int FaceModelId;
  [Key(18)]
  public string AvatarPresetId = "default";
  [Key(19)]
  public float HeightScale = 1f;
  [Key(20)]
  public bool IsNewCustomization = true;

  public void ParseData(PlayerCustomization target)
  {
    this.Uniform = target.Uniform.Value;
    this.UniformNumber = target.UniformNumber.Value;
    this.HomeUniform = target.HomeUniform.Value;
    this.GloveId = target.GloveId.Value;
    this.MultiplayerTeamBallId = target.MultiplayerTeamBallId.Value;
    this.TrailColor = target.TrailColor.Value;
    this.TrailType = target.TrailType.Value;
    this.NameColor = target.NameColor.Value;
    this.WristbandColor = target.WristbandColor.Value;
    this.FirstName = target.FirstName.Value;
    this.LastName = target.LastName.Value;
    this.BodyHeight = target.BodyHeight.Value;
    this.AvatarCustomized = target.AvatarCustomized.Value;
    this.BodyType = target.BodyType.Value;
    this.BodyModelId = target.BodyModelId.Value;
    this.HeadModelId = target.HeadModelId.Value;
    this.FaceModelId = target.FaceModelId.Value;
    this.AvatarPresetId = target.AvatarPresetId.Value;
    this.HeightScale = target.HeightScale;
    this.IsNewCustomization = (bool) target.IsNewCustomization;
  }

  public void ParseTarget(PlayerCustomization target)
  {
    target.Uniform.SetValue(this.Uniform);
    target.UniformNumber.SetValue(this.UniformNumber);
    target.HomeUniform.SetValue(this.HomeUniform);
    target.GloveId.SetValue(this.GloveId);
    target.MultiplayerTeamBallId.SetValue(this.MultiplayerTeamBallId);
    target.TrailColor.SetValue(this.TrailColor);
    target.TrailType.SetValue(this.TrailType);
    target.NameColor.SetValue(this.NameColor);
    target.WristbandColor.SetValue(this.WristbandColor);
    target.FirstName.SetValue(this.FirstName);
    target.LastName.SetValue(this.LastName);
    target.BodyHeight.SetValue(this.BodyHeight);
    target.AvatarCustomized.SetValue(this.AvatarCustomized);
    target.BodyType.SetValue(this.BodyType);
    target.BodyModelId.SetValue(this.BodyModelId);
    target.HeadModelId.SetValue(this.HeadModelId);
    target.FaceModelId.SetValue(this.FaceModelId);
    target.AvatarPresetId.SetValue(this.AvatarPresetId);
    target.HeightScale = this.HeightScale;
    target.IsNewCustomization.SetValue(this.IsNewCustomization);
  }
}
