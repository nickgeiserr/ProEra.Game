// Decompiled with JetBrains decompiler
// Type: MiniCampEntry
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using UDB;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class MiniCampEntry
{
  [Key(0)]
  public int Level = 1;
  [Key(1)]
  [SerializeField]
  public string TeamName;
  [Key(2)]
  public int EarnedStars;
  [Key(3)]
  public int TotalStars = 3;
  [Key(4)]
  [SerializeField]
  public bool FlipOrientation;
  [Key(5)]
  public int PersonalBest;

  public void SetBestStars(int stars)
  {
    if (this.EarnedStars >= stars)
      return;
    this.EarnedStars = stars;
  }

  public bool SetPersonalBest(int score)
  {
    if (this.PersonalBest >= score)
      return false;
    this.PersonalBest = score;
    return true;
  }

  public TeamDataStore GetTeam() => SingletonBehaviour<PersistentData, MonoBehaviour>.instance.GetTeamDataStoreByName(this.TeamName);

  public Sprite GetLogo() => this.GetTeam().Logo;
}
