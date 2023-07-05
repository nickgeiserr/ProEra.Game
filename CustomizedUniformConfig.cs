// Decompiled with JetBrains decompiler
// Type: CustomizedUniformConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomizedUniformConfig", menuName = "Data/Uniforms/Customized Uniform", order = 1)]
public class CustomizedUniformConfig : UniformConfig
{
  public Texture2D logoMap;
  public Texture2D decalMap;
  public Color decal1;
  public Color decal2;
  public Color decal3;
  public Color helmet;
  public Color shirt;
  public Color pants;
  private static readonly int Tex = Shader.PropertyToID("_MainTex");
  private static readonly int BaseColor = Shader.PropertyToID("_NumbersBaseColor");
  private static readonly int OutlineColor = Shader.PropertyToID("_NumbersOutlineColor");
  private static readonly int SecondOutlineColor = Shader.PropertyToID("_NumbersSecondOutlineColor");
  private static readonly int DecalsMap = Shader.PropertyToID("_DecalsMap");
  private static readonly int HelmetColor = Shader.PropertyToID("_HelmetColor");
  private static readonly int ShirtColor = Shader.PropertyToID("_ShirtColor");
  private static readonly int PantsColor = Shader.PropertyToID("_PantsColor");
  private static readonly int Decal1Color = Shader.PropertyToID("_Decal1Color");
  private static readonly int Decal2Color = Shader.PropertyToID("_Decal2Color");
  private static readonly int Decal3Color = Shader.PropertyToID("_Decal3Color");
  private static readonly int LogosMap = Shader.PropertyToID("_LogosMap");

  public override void ApplyConfig(Renderer renderer)
  {
    renderer.material = this.baseMaterial;
    MaterialPropertyBlock properties = new MaterialPropertyBlock();
    properties.SetTexture(CustomizedUniformConfig.Tex, (Texture) this.baseMap);
    properties.SetColor(CustomizedUniformConfig.BaseColor, this.numberFill);
    properties.SetColor(CustomizedUniformConfig.OutlineColor, this.numberOutline1);
    properties.SetColor(CustomizedUniformConfig.SecondOutlineColor, this.numberOutline2);
    if ((Object) this.logoMap != (Object) null)
      properties.SetTexture(CustomizedUniformConfig.LogosMap, (Texture) this.logoMap);
    properties.SetTexture(CustomizedUniformConfig.DecalsMap, (Texture) this.decalMap);
    properties.SetColor(CustomizedUniformConfig.HelmetColor, this.helmet);
    properties.SetColor(CustomizedUniformConfig.ShirtColor, this.shirt);
    properties.SetColor(CustomizedUniformConfig.PantsColor, this.pants);
    properties.SetColor(CustomizedUniformConfig.Decal1Color, this.decal1);
    properties.SetColor(CustomizedUniformConfig.Decal2Color, this.decal2);
    properties.SetColor(CustomizedUniformConfig.Decal3Color, this.decal3);
    renderer.SetPropertyBlock(properties);
  }
}
