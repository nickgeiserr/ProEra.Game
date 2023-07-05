// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Water.WaterBase
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UnityStandardAssets.Water
{
  [ExecuteInEditMode]
  public class WaterBase : MonoBehaviour
  {
    public Material sharedMaterial;
    public WaterQuality waterQuality = WaterQuality.High;
    public bool edgeBlend = true;

    public void UpdateShader()
    {
      this.sharedMaterial.shader.maximumLOD = this.waterQuality <= WaterQuality.Medium ? (this.waterQuality <= WaterQuality.Low ? 201 : 301) : 501;
      if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
        this.edgeBlend = false;
      if (this.edgeBlend)
      {
        Shader.EnableKeyword("WATER_EDGEBLEND_ON");
        Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
        if (!(bool) (Object) Camera.main)
          return;
        Camera.main.depthTextureMode |= DepthTextureMode.Depth;
      }
      else
      {
        Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
        Shader.DisableKeyword("WATER_EDGEBLEND_ON");
      }
    }

    public void WaterTileBeingRendered(Transform tr, Camera currentCam)
    {
      if (!(bool) (Object) currentCam || !this.edgeBlend)
        return;
      currentCam.depthTextureMode |= DepthTextureMode.Depth;
    }

    public void Update()
    {
      if (!(bool) (Object) this.sharedMaterial)
        return;
      this.UpdateShader();
    }
  }
}
