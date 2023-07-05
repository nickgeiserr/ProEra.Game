// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.TextureBlenderFallback
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class TextureBlenderFallback : TextureBlender
  {
    private bool m_doTintColor;
    private Color m_tintColor;
    private Color m_defaultColor = Color.white;

    public bool DoesShaderNameMatch(string shaderName) => true;

    public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
    {
      if (shaderTexturePropertyName.Equals("_MainTex"))
      {
        this.m_doTintColor = true;
        this.m_tintColor = Color.white;
        if (sourceMat.HasProperty("_Color"))
        {
          this.m_tintColor = sourceMat.GetColor("_Color");
        }
        else
        {
          if (!sourceMat.HasProperty("_TintColor"))
            return;
          this.m_tintColor = sourceMat.GetColor("_TintColor");
        }
      }
      else
        this.m_doTintColor = false;
    }

    public Color OnBlendTexturePixel(string shaderPropertyName, Color pixelColor) => this.m_doTintColor ? new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a) : pixelColor;

    public bool NonTexturePropertiesAreEqual(Material a, Material b)
    {
      if (a.HasProperty("_Color"))
      {
        if (TextureBlenderFallback._compareColor(a, b, this.m_defaultColor, "_Color"))
          return true;
      }
      else if (a.HasProperty("_TintColor") && TextureBlenderFallback._compareColor(a, b, this.m_defaultColor, "_TintColor"))
        return true;
      return false;
    }

    public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
    {
      if (resultMaterial.HasProperty("_Color"))
      {
        resultMaterial.SetColor("_Color", this.m_defaultColor);
      }
      else
      {
        if (!resultMaterial.HasProperty("_TintColor"))
          return;
        resultMaterial.SetColor("_TintColor", this.m_defaultColor);
      }
    }

    public Color GetColorIfNoTexture(Material mat, ShaderTextureProperty texProperty)
    {
      if (texProperty.isNormalMap)
        return new Color(0.5f, 0.5f, 1f);
      if (texProperty.name.Equals("_MainTex"))
      {
        if ((UnityEngine.Object) mat != (UnityEngine.Object) null)
        {
          if (mat.HasProperty("_Color"))
          {
            try
            {
              return mat.GetColor("_Color");
            }
            catch (Exception ex)
            {
              goto label_44;
            }
          }
        }
        if ((UnityEngine.Object) mat != (UnityEngine.Object) null)
        {
          if (mat.HasProperty("_TintColor"))
          {
            try
            {
              return mat.GetColor("_TintColor");
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      else if (texProperty.name.Equals("_SpecGlossMap"))
      {
        if ((UnityEngine.Object) mat != (UnityEngine.Object) null)
        {
          if (mat.HasProperty("_SpecColor"))
          {
            try
            {
              Color color = mat.GetColor("_SpecColor");
              if (mat.HasProperty("_Glossiness"))
              {
                try
                {
                  color.a = mat.GetFloat("_Glossiness");
                }
                catch (Exception ex)
                {
                }
              }
              Debug.LogWarning((object) color);
              return color;
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      else if (texProperty.name.Equals("_MetallicGlossMap"))
      {
        if ((UnityEngine.Object) mat != (UnityEngine.Object) null)
        {
          if (mat.HasProperty("_Metallic"))
          {
            try
            {
              float num = mat.GetFloat("_Metallic");
              Color colorIfNoTexture = new Color(num, num, num);
              if (mat.HasProperty("_Glossiness"))
              {
                try
                {
                  colorIfNoTexture.a = mat.GetFloat("_Glossiness");
                }
                catch (Exception ex)
                {
                }
              }
              return colorIfNoTexture;
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      else
      {
        if (texProperty.name.Equals("_ParallaxMap"))
          return new Color(0.0f, 0.0f, 0.0f, 0.0f);
        if (texProperty.name.Equals("_OcclusionMap"))
          return new Color(1f, 1f, 1f, 1f);
        if (texProperty.name.Equals("_EmissionMap"))
        {
          if ((UnityEngine.Object) mat != (UnityEngine.Object) null && mat.HasProperty("_EmissionScaleUI"))
          {
            if (mat.HasProperty("_EmissionColor"))
            {
              if (mat.HasProperty("_EmissionColorUI"))
              {
                try
                {
                  Color color1 = mat.GetColor("_EmissionColor");
                  Color color2 = mat.GetColor("_EmissionColorUI");
                  float num = mat.GetFloat("_EmissionScaleUI");
                  Color color3 = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                  return color1 == color3 && color2 == new Color(1f, 1f, 1f, 1f) ? new Color(num, num, num, num) : color2;
                }
                catch (Exception ex)
                {
                  goto label_44;
                }
              }
            }
            try
            {
              double num = (double) mat.GetFloat("_EmissionScaleUI");
              return new Color((float) num, (float) num, (float) num, (float) num);
            }
            catch (Exception ex)
            {
            }
          }
        }
        else if (texProperty.name.Equals("_DetailMask"))
          return new Color(0.0f, 0.0f, 0.0f, 0.0f);
      }
label_44:
      return new Color(1f, 1f, 1f, 0.0f);
    }

    public static bool _compareColor(
      Material a,
      Material b,
      Color defaultVal,
      string propertyName)
    {
      Color color1 = defaultVal;
      Color color2 = defaultVal;
      if (a.HasProperty(propertyName))
        color1 = a.GetColor(propertyName);
      if (b.HasProperty(propertyName))
        color2 = b.GetColor(propertyName);
      return !(color1 != color2);
    }

    public static bool _compareFloat(
      Material a,
      Material b,
      float defaultVal,
      string propertyName)
    {
      float num1 = defaultVal;
      float num2 = defaultVal;
      if (a.HasProperty(propertyName))
        num1 = a.GetFloat(propertyName);
      if (b.HasProperty(propertyName))
        num2 = b.GetFloat(propertyName);
      return (double) num1 == (double) num2;
    }
  }
}
