// Decompiled with JetBrains decompiler
// Type: MiniCampHighlightDecal
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MiniCampHighlightDecal : MonoBehaviour
{
  public MiniCampHighlightDecal.ELocation Location;
  [SerializeField]
  private GameObject TargetHighlightDecal;

  public void SetTargetHighlightActive(bool active) => this.TargetHighlightDecal.SetActive(active);

  public enum ELocation
  {
    kLeft,
    kRight,
    kMiddle,
  }
}
