// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.TextureBlenderStandardSpecular
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class TextureBlenderStandardSpecular : TextureBlender
  {
    private Color m_tintColor;
    private Color m_emission;
    private TextureBlenderStandardSpecular.Prop propertyToDo = TextureBlenderStandardSpecular.Prop.doNone;
    private Color m_defaultColor = Color.white;
    private Color m_defaultSpecular = Color.black;
    private float m_defaultGlossiness = 0.5f;
    private Color m_defaultEmission = Color.black;

    public bool DoesShaderNameMatch(string shaderName) => shaderName.Equals("Standard (Specular setup)");

    public void OnBeforeTintTexture(Material sourceMat, string shaderTexturePropertyName)
    {
      switch (shaderTexturePropertyName)
      {
        case "_MainTex":
          this.propertyToDo = TextureBlenderStandardSpecular.Prop.doColor;
          if (sourceMat.HasProperty(shaderTexturePropertyName))
          {
            this.m_tintColor = sourceMat.GetColor("_Color");
            break;
          }
          this.m_tintColor = this.m_defaultColor;
          break;
        case "_MetallicGlossMap":
          this.propertyToDo = TextureBlenderStandardSpecular.Prop.doSpecular;
          break;
        case "_EmissionMap":
          this.propertyToDo = TextureBlenderStandardSpecular.Prop.doEmission;
          if (sourceMat.HasProperty(shaderTexturePropertyName))
          {
            this.m_emission = sourceMat.GetColor("_EmissionColor");
            break;
          }
          this.m_emission = this.m_defaultEmission;
          break;
        default:
          this.propertyToDo = TextureBlenderStandardSpecular.Prop.doNone;
          break;
      }
    }

    public Color OnBlendTexturePixel(string propertyToDoshaderPropertyName, Color pixelColor)
    {
      if (this.propertyToDo == TextureBlenderStandardSpecular.Prop.doColor)
        return new Color(pixelColor.r * this.m_tintColor.r, pixelColor.g * this.m_tintColor.g, pixelColor.b * this.m_tintColor.b, pixelColor.a * this.m_tintColor.a);
      return this.propertyToDo == TextureBlenderStandardSpecular.Prop.doSpecular || this.propertyToDo != TextureBlenderStandardSpecular.Prop.doEmission ? pixelColor : new Color(pixelColor.r * this.m_emission.r, pixelColor.g * this.m_emission.g, pixelColor.b * this.m_emission.b, pixelColor.a * this.m_emission.a);
    }

    public bool NonTexturePropertiesAreEqual(Material a, Material b) => TextureBlenderFallback._compareColor(a, b, this.m_defaultColor, "_Color") && TextureBlenderFallback._compareColor(a, b, this.m_defaultSpecular, "_SpecColor") && TextureBlenderFallback._compareFloat(a, b, this.m_defaultGlossiness, "_Glossiness") && TextureBlenderFallback._compareColor(a, b, this.m_defaultEmission, "_EmissionColor");

    public void SetNonTexturePropertyValuesOnResultMaterial(Material resultMaterial)
    {
      resultMaterial.SetColor("_Color", this.m_defaultColor);
      resultMaterial.SetColor("_SpecColor", this.m_defaultSpecular);
      resultMaterial.SetFloat("_Glossiness", this.m_defaultGlossiness);
      if ((UnityEngine.Object) resultMaterial.GetTexture("_EmissionMap") == (UnityEngine.Object) null)
        resultMaterial.SetColor("_EmissionColor", Color.black);
      else
        resultMaterial.SetColor("_EmissionColor", Color.white);
    }

    public Color GetColorIfNoTexture(Material mat, ShaderTextureProperty texPropertyName)
    {
      if (texPropertyName.name.Equals("_BumpMap"))
        return new Color(0.5f, 0.5f, 1f);
      if (texPropertyName.name.Equals("_MainTex"))
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
            }
          }
        }
      }
      else if (texPropertyName.name.Equals("_SpecGlossMap"))
      {
        bool flag = false;
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
                  flag = true;
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
        if (!flag)
          return this.m_defaultSpecular;
      }
      else
      {
        if (texPropertyName.name.Equals("_ParallaxMap"))
          return new Color(0.0f, 0.0f, 0.0f, 0.0f);
        if (texPropertyName.name.Equals("_OcclusionMap"))
          return new Color(1f, 1f, 1f, 1f);
        if (texPropertyName.name.Equals("_EmissionMap"))
        {
          if ((UnityEngine.Object) mat != (UnityEngine.Object) null)
          {
            if (!mat.HasProperty("_EmissionColor"))
              return Color.black;
            try
            {
              return mat.GetColor("_EmissionColor");
            }
            catch (Exception ex)
            {
            }
          }
        }
        else if (texPropertyName.name.Equals("_DetailMask"))
          return new Color(0.0f, 0.0f, 0.0f, 0.0f);
      }
      return new Color(1f, 1f, 1f, 0.0f);
    }

    private enum Prop
    {
      doColor,
      doSpecular,
      doEmission,
      doNone,
    }
  }
}
