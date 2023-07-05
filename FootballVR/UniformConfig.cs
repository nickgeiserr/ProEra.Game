// Decompiled with JetBrains decompiler
// Type: FootballVR.UniformConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL;
using DDL.UniformData;
using TMPro;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "NewUniformConfig", menuName = "Data/Uniforms/Painted Uniform", order = 0)]
  public class UniformConfig : ScriptableObject
  {
    [Header("Team Information")]
    public string displayName;
    public ETeamUniformId team;
    public ECustomerType teamType;
    public ETeamUniformFlags uniFlags;
    public TeamUniformConfigs.EUniformGroup group;
    [Space]
    public Material baseMaterial;
    [Header("Numbers")]
    public TMP_FontAsset numberFont;
    public Color numberFill;
    public Color numberOutline1;
    public Color numberOutline2 = Color.white;
    public Color namesFill = Color.red;
    public Color namesOutline = Color.green;
    [Header("Uniform")]
    public Texture2D baseMap;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int NumbersBaseColor = Shader.PropertyToID("_NumbersBaseColor");
    private static readonly int NumbersOutlineColor = Shader.PropertyToID("_NumbersOutlineColor");
    private static readonly int NumbersSecondOutlineColor = Shader.PropertyToID("_NumbersSecondOutlineColor");

    public virtual void ApplyConfig(Renderer renderer)
    {
      renderer.material = this.baseMaterial;
      MaterialPropertyBlock properties = new MaterialPropertyBlock();
      properties.SetTexture(UniformConfig.MainTex, (Texture) this.baseMap);
      properties.SetColor(UniformConfig.NumbersBaseColor, this.numberFill);
      properties.SetColor(UniformConfig.NumbersOutlineColor, this.numberOutline1);
      properties.SetColor(UniformConfig.NumbersSecondOutlineColor, this.numberOutline2);
      renderer.SetPropertyBlock(properties);
    }
  }
}
