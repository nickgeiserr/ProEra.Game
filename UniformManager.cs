// Decompiled with JetBrains decompiler
// Type: UniformManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using UDB;
using UnityEngine;

public class UniformManager : MonoBehaviour
{
  [SerializeField]
  private Renderer helmetRenderer;
  [SerializeField]
  private Renderer upperBodyRenderer;
  [SerializeField]
  private Renderer lowerBodyRenderer;
  [SerializeField]
  private Renderer armSleevesRenderer;
  [SerializeField]
  private Renderer armBandsRenderer;
  [SerializeField]
  private Renderer skinRenderer;
  private UniformSet uniformSet;
  private UniformConfig uniformConfig;
  private static Color[] skinTones = new Color[7]
  {
    Color.white,
    new Color(0.8392157f, 0.7607843f, 0.694117665f),
    new Color(0.6627451f, 0.572549045f, 0.4509804f),
    new Color(0.607843161f, 0.490196079f, 0.3529412f),
    new Color(0.2784314f, 0.239215687f, 0.164705887f),
    new Color(0.215686277f, 0.156862751f, 0.09411765f),
    new Color(0.149019614f, 0.0784313753f, 0.0235294122f)
  };
  private static int visorMaterialIndex = 0;
  private static int helmetMaterialIndex = 1;

  private void Awake()
  {
  }

  public void SetUniform(UniformSet u, UniformConfig config, int helmetIndex, int pantIndex)
  {
    this.uniformSet = u;
    this.uniformConfig = config;
    this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].mainTexture = (Texture) u.GetHelmetTexture(helmetIndex);
    this.SetHelmetType(config.HelmetIsChrome(), config.HelmetIsMatte());
    this.lowerBodyRenderer.material.mainTexture = (Texture) u.GetPantTexture(pantIndex);
  }

  public void SetHelmetType(bool isChrome, bool isMatte)
  {
    if (isChrome)
    {
      this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].SetFloat("_Metallic", 1f);
      this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].SetFloat("_GlossMapScale", 1f);
    }
    else if (isMatte)
    {
      this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].SetFloat("_Metallic", 0.0f);
      this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].SetFloat("_GlossMapScale", 0.0f);
    }
    else
    {
      this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].SetFloat("_Metallic", 0.5f);
      this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].SetFloat("_GlossMapScale", 0.5f);
    }
  }

  public void SetJersey(
    int jerseyIndex,
    string playerName,
    int jerseyNumber,
    UniformAssetType type)
  {
    Texture2D savedJersey = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.uniformFactory.GetSavedJersey(playerName + jerseyNumber.ToString(), type);
    if ((Object) savedJersey != (Object) null)
    {
      this.upperBodyRenderer.material.mainTexture = (Texture) savedJersey;
    }
    else
    {
      Texture2D jersey = UniformWriter.CreateJersey(this.uniformSet, this.uniformConfig, jerseyIndex, playerName, jerseyNumber);
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.uniformFactory.SaveJersey(playerName + jerseyNumber.ToString(), jersey, type);
      this.upperBodyRenderer.material.mainTexture = (Texture) jersey;
    }
  }

  public void SetSkinTone(int i)
  {
    i = Mathf.Clamp(i, 0, 6);
    this.skinRenderer.material.color = UniformManager.skinTones[i];
  }

  public void ShowArmSleeves(Color armSleeveColor) => this.armSleevesRenderer.material.color = armSleeveColor;

  public void HideArmSleeves() => this.armSleevesRenderer.enabled = false;

  public void ShowArmBands(Color armBandColor)
  {
    this.armBandsRenderer.material.color = armBandColor;
    this.armBandsRenderer.enabled = true;
  }

  public void HideArmBands() => this.armBandsRenderer.enabled = false;

  public void ShowHelmetVisor(Color visorColor) => this.helmetRenderer.materials[UniformManager.visorMaterialIndex].color = new Color(visorColor.r, visorColor.g, visorColor.b, 0.6f);

  public void HideHelmetVisor() => this.helmetRenderer.materials[UniformManager.visorMaterialIndex].color = Color.clear;

  public void HideUniformRenderers()
  {
    this.helmetRenderer.enabled = false;
    this.upperBodyRenderer.enabled = false;
    this.lowerBodyRenderer.enabled = false;
    this.armSleevesRenderer.enabled = false;
    this.armBandsRenderer.enabled = false;
    this.skinRenderer.enabled = false;
  }

  public void ShowUniformRenderers()
  {
    this.helmetRenderer.enabled = true;
    this.upperBodyRenderer.enabled = true;
    this.lowerBodyRenderer.enabled = true;
    this.armSleevesRenderer.enabled = true;
    this.armBandsRenderer.enabled = true;
    this.skinRenderer.enabled = true;
  }

  public void SetHelmetTexture(Texture2D tex) => this.helmetRenderer.materials[UniformManager.helmetMaterialIndex].mainTexture = (Texture) tex;

  public void SetPantTexture(Texture2D tex) => this.lowerBodyRenderer.material.mainTexture = (Texture) tex;

  public void SetJerseyTexture(Texture2D tex) => this.upperBodyRenderer.material.mainTexture = (Texture) tex;

  public Material GetVisorMaterial() => this.helmetRenderer.materials[UniformManager.visorMaterialIndex];

  public Material GetArmBandMaterial() => this.armBandsRenderer.material;

  public Material GetArmSleeveMaterial() => this.armSleevesRenderer.material;

  public Renderer GetArmSleevesRenderer() => this.armSleevesRenderer;

  public Renderer GetArmBandsRenderer() => this.armBandsRenderer;
}
