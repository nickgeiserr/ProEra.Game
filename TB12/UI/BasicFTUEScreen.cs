// Decompiled with JetBrains decompiler
// Type: TB12.UI.BasicFTUEScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.UI;
using System;
using UnityEngine;

namespace TB12.UI
{
  public class BasicFTUEScreen : UIView
  {
    public GameObject[] oculusSprites;
    public GameObject[] psvrSprites;
    public GameObject[] psvr2Sprites;
    public GameObject[] indexSprites;
    public GameObject[] viveSprites;

    public override Enum ViewId { get; } = (Enum) EScreens.kBasicFTUE;

    private void OnEnable()
    {
      this.SetSpritesActive(this.oculusSprites, false);
      this.SetSpritesActive(this.psvrSprites, false);
      this.SetSpritesActive(this.psvr2Sprites, false);
      this.SetSpritesActive(this.indexSprites, false);
      this.SetSpritesActive(this.viveSprites, false);
      switch (VRUtils.GetDeviceType())
      {
        case VRUtils.DeviceType.Oculus:
          this.SetSpritesActive(this.oculusSprites, true);
          break;
        case VRUtils.DeviceType.PSVR2:
          this.SetSpritesActive(this.psvr2Sprites, true);
          break;
        case VRUtils.DeviceType.Vive:
          this.SetSpritesActive(this.viveSprites, true);
          break;
        case VRUtils.DeviceType.Index:
          this.SetSpritesActive(this.indexSprites, true);
          break;
        case VRUtils.DeviceType.PSVR:
          this.SetSpritesActive(this.psvrSprites, true);
          break;
      }
    }

    private void SetSpritesActive(GameObject[] Sprites, bool Active)
    {
      foreach (GameObject sprite in Sprites)
      {
        if ((UnityEngine.Object) sprite != (UnityEngine.Object) null)
          sprite.SetActive(Active);
      }
    }
  }
}
