// Decompiled with JetBrains decompiler
// Type: StadiumSet
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

public class StadiumSet : ScriptableObject
{
  public string homeTeamStadium;
  public string stadiumName;
  public string stadiumLocation;
  public string capacity;
  public bool allowsPrecipitation;
  public bool affectedByWind;
  public string fieldTexture;
  public Sprite previewImage;
  public string sceneName;
  public bool useInMobileBuilds;
  public bool useInStandaloneBuilds;
  public bool uses2DFans;
  public Vector3[] panPathPoints;
  public Vector3[] panPathAngles;

  public Texture2D GetFieldTexture() => AddressablesData.instance.LoadAssetSync<Texture2D>("fieldtextures", this.fieldTexture);
}
