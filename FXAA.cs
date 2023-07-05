// Decompiled with JetBrains decompiler
// Type: FXAA
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/FXAA")]
public class FXAA : FXAAPostEffectsBase
{
  public Shader shader;
  private Material mat;

  private void CreateMaterials()
  {
    if (!((Object) this.mat == (Object) null))
      return;
    this.mat = this.CheckShaderAndCreateMaterial(this.shader, this.mat);
  }

  private void Start()
  {
    this.shader = Shader.Find("Hidden/FXAA3");
    this.CreateMaterials();
    this.CheckSupport(false);
  }

  public void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    this.CreateMaterials();
    float x = 1f / (float) Screen.width;
    float y = 1f / (float) Screen.height;
    this.mat.SetVector("_rcpFrame", new Vector4(x, y, 0.0f, 0.0f));
    this.mat.SetVector("_rcpFrameOpt", new Vector4(x * 2f, y * 2f, x * 0.5f, y * 0.5f));
    Graphics.Blit((Texture) source, destination, this.mat);
  }
}
