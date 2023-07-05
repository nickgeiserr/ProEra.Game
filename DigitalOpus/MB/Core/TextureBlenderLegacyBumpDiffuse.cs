// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.TextureBlenderLegacyBumpDiffuse
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class TextureBlenderLegacyBumpDiffuse : TextureBlender
  {
    private bool doColor;
    private Color m_tintColor;
    private Color m_defaultTintColor = Color.white;

    public bool DoesShaderNameMatch(string shaderName)
    {
      switch (shaderName)
      {
        case "Legacy Shaders/Bumped Diffuse":
          return true;
        case "Bumped Diffuse":
          return true;
        default:
          return false;
      }
    }

    public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
    {
      if (shaderTexturePropertyName.EndsWith("_MainTex"))
      {
        this.doColor = true;
        this.m_tintColor = sourceMat.GetColor("_Color");
      }
      else
        this.doColor = false;
    }

    public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor) => this.doColor ? new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a) : pixelColor;

    public bool NonTexturePropertiesAreEqual(Material a, Material b) => TextureBlenderFallback._compareColor(a, b, this.m_defaultTintColor, "_Color");

    public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial) => resultMaterial.SetColor("_Color", Color.white);

    public Color GetColorIfNoTexture(Material m, ShaderTextureProperty texPropertyName)
    {
      if (texPropertyName.name.Equals("_BumpMap"))
        return new Color(0.5f, 0.5f, 1f);
      if (texPropertyName.name.Equals("_MainTex") && (UnityEngine.Object) m != (UnityEngine.Object) null)
      {
        if (m.HasProperty("_Color"))
        {
          try
          {
            return m.GetColor("_Color");
          }
          catch (Exception ex)
          {
          }
        }
      }
      return new Color(1f, 1f, 1f, 0.0f);
    }
  }
}
