﻿// Decompiled with JetBrains decompiler
// Type: MB3_AtlasPackerRenderTexture
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MB3_AtlasPackerRenderTexture : MonoBehaviour
{
  private MB_TextureCombinerRenderTexture fastRenderer;
  private bool _doRenderAtlas;
  public int width;
  public int height;
  public int padding;
  public bool isNormalMap;
  public bool fixOutOfBoundsUVs;
  public bool considerNonTextureProperties;
  public TextureBlender resultMaterialTextureBlender;
  public Rect[] rects;
  public Texture2D tex1;
  public List<MB3_TextureCombiner.MB_TexSet> textureSets;
  public int indexOfTexSetToRender;
  public ShaderTextureProperty texPropertyName;
  public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;
  public Texture2D testTex;
  public Material testMat;

  public Texture2D OnRenderAtlas(MB3_TextureCombiner combiner)
  {
    this.fastRenderer = new MB_TextureCombinerRenderTexture();
    this._doRenderAtlas = true;
    Texture2D texture2D = this.fastRenderer.DoRenderAtlas(this.gameObject, this.width, this.height, this.padding, this.rects, this.textureSets, this.indexOfTexSetToRender, this.texPropertyName, this.resultMaterialTextureBlender, this.isNormalMap, this.fixOutOfBoundsUVs, this.considerNonTextureProperties, combiner, this.LOG_LEVEL);
    this._doRenderAtlas = false;
    return texture2D;
  }

  private void OnRenderObject()
  {
    if (!this._doRenderAtlas)
      return;
    this.fastRenderer.OnRenderObject();
    this._doRenderAtlas = false;
  }
}
