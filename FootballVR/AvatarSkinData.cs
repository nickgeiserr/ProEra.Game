// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarSkinData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "NewAvatarSkinData", menuName = "Data/DataModel/AvatarSkinData")]
  public class AvatarSkinData : ScriptableObject
  {
    [SerializeField]
    private Color[] _skinColors;

    public Color GetRandomSkinColor() => this._skinColors[Random.Range(0, this._skinColors.Length)];
  }
}
