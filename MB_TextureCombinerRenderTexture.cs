// Decompiled with JetBrains decompiler
// Type: MB_TextureCombinerRenderTexture
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MB_TextureCombinerRenderTexture
{
  public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;
  private Material mat;
  private RenderTexture _destinationTexture;
  private Camera myCamera;
  private int _padding;
  private bool _isNormalMap;
  private bool _fixOutOfBoundsUVs;
  private bool _doRenderAtlas;
  private Rect[] rs;
  private List<MB3_TextureCombiner.MB_TexSet> textureSets;
  private int indexOfTexSetToRender;
  private ShaderTextureProperty _texPropertyName;
  private TextureBlender _resultMaterialTextureBlender;
  private Texture2D targTex;
  private MB3_TextureCombiner combiner;

  public Texture2D DoRenderAtlas(
    GameObject gameObject,
    int width,
    int height,
    int padding,
    Rect[] rss,
    List<MB3_TextureCombiner.MB_TexSet> textureSetss,
    int indexOfTexSetToRenders,
    ShaderTextureProperty texPropertyname,
    TextureBlender resultMaterialTextureBlender,
    bool isNormalMap,
    bool fixOutOfBoundsUVs,
    bool considerNonTextureProperties,
    MB3_TextureCombiner texCombiner,
    MB2_LogLevel LOG_LEV)
  {
    this.LOG_LEVEL = LOG_LEV;
    this.textureSets = textureSetss;
    this.indexOfTexSetToRender = indexOfTexSetToRenders;
    this._texPropertyName = texPropertyname;
    this._padding = padding;
    this._isNormalMap = isNormalMap;
    this._fixOutOfBoundsUVs = fixOutOfBoundsUVs;
    this._resultMaterialTextureBlender = resultMaterialTextureBlender;
    this.combiner = texCombiner;
    this.rs = rss;
    Shader shader = !this._isNormalMap ? Shader.Find("MeshBaker/AlbedoShader") : Shader.Find("MeshBaker/NormalMapShader");
    if ((UnityEngine.Object) shader == (UnityEngine.Object) null)
    {
      UnityEngine.Debug.LogError((object) "Could not find shader for RenderTexture. Try reimporting mesh baker");
      return (Texture2D) null;
    }
    this.mat = new Material(shader);
    this._destinationTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
    this._destinationTexture.filterMode = FilterMode.Point;
    this.myCamera = gameObject.GetComponent<Camera>();
    this.myCamera.orthographic = true;
    this.myCamera.orthographicSize = (float) (height >> 1);
    this.myCamera.aspect = (float) (width / height);
    this.myCamera.targetTexture = this._destinationTexture;
    this.myCamera.clearFlags = CameraClearFlags.Color;
    Transform component = this.myCamera.GetComponent<Transform>();
    component.localPosition = new Vector3((float) width / 2f, (float) height / 2f, 3f);
    component.localRotation = Quaternion.Euler(0.0f, 180f, 180f);
    this._doRenderAtlas = true;
    if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      UnityEngine.Debug.Log((object) string.Format("Begin Camera.Render destTex w={0} h={1} camPos={2}", (object) width, (object) height, (object) component.localPosition));
    this.myCamera.Render();
    this._doRenderAtlas = false;
    MB_Utility.Destroy((UnityEngine.Object) this.mat);
    MB_Utility.Destroy((UnityEngine.Object) this._destinationTexture);
    if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      UnityEngine.Debug.Log((object) "Finished Camera.Render ");
    Texture2D targTex = this.targTex;
    this.targTex = (Texture2D) null;
    return targTex;
  }

  public void OnRenderObject()
  {
    if (!this._doRenderAtlas)
      return;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    for (int index = 0; index < this.rs.Length; ++index)
    {
      MB3_TextureCombiner.MeshBakerMaterialTexture t = this.textureSets[index].ts[this.indexOfTexSetToRender];
      if (this.LOG_LEVEL >= MB2_LogLevel.trace && (UnityEngine.Object) t.t != (UnityEngine.Object) null)
      {
        UnityEngine.Debug.Log((object) ("Added " + ((object) t.t)?.ToString() + " to atlas w=" + t.t.width.ToString() + " h=" + t.t.height.ToString() + " offset=" + t.matTilingRect.min.ToString() + " scale=" + t.matTilingRect.size.ToString() + " rect=" + this.rs[index].ToString() + " padding=" + this._padding.ToString()));
        this._printTexture(t.t);
      }
      this.CopyScaledAndTiledToAtlas(this.textureSets[index], t, this.textureSets[index].obUVoffset, this.textureSets[index].obUVscale, this.rs[index], this._texPropertyName, this._resultMaterialTextureBlender);
    }
    stopwatch.Stop();
    stopwatch.Start();
    long elapsedMilliseconds;
    if (this.LOG_LEVEL >= MB2_LogLevel.debug)
    {
      elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
      UnityEngine.Debug.Log((object) ("Total time for Graphics.DrawTexture calls " + elapsedMilliseconds.ToString("f5")));
    }
    if (this.LOG_LEVEL >= MB2_LogLevel.debug)
      UnityEngine.Debug.Log((object) ("Copying RenderTexture to Texture2D. destW" + this._destinationTexture.width.ToString() + " destH" + this._destinationTexture.height.ToString()));
    Texture2D t1 = new Texture2D(this._destinationTexture.width, this._destinationTexture.height, TextureFormat.ARGB32, true);
    RenderTexture active = RenderTexture.active;
    RenderTexture.active = this._destinationTexture;
    int num1 = this._destinationTexture.width / 512;
    int num2 = this._destinationTexture.height / 512;
    if (num1 == 0 || num2 == 0)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.trace)
        UnityEngine.Debug.Log((object) "Copying all in one shot");
      t1.ReadPixels(new Rect(0.0f, 0.0f, (float) this._destinationTexture.width, (float) this._destinationTexture.height), 0, 0, true);
    }
    else if (this.IsOpenGL())
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.trace)
        UnityEngine.Debug.Log((object) "OpenGL copying blocks");
      for (int index1 = 0; index1 < num1; ++index1)
      {
        for (int index2 = 0; index2 < num2; ++index2)
          t1.ReadPixels(new Rect((float) (index1 * 512), (float) (index2 * 512), 512f, 512f), index1 * 512, index2 * 512, true);
      }
    }
    else
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.trace)
        UnityEngine.Debug.Log((object) "Not OpenGL copying blocks");
      for (int index3 = 0; index3 < num1; ++index3)
      {
        for (int index4 = 0; index4 < num2; ++index4)
          t1.ReadPixels(new Rect((float) (index3 * 512), (float) (this._destinationTexture.height - 512 - index4 * 512), 512f, 512f), index3 * 512, index4 * 512, true);
      }
    }
    RenderTexture.active = active;
    t1.Apply();
    if (this.LOG_LEVEL >= MB2_LogLevel.trace)
    {
      UnityEngine.Debug.Log((object) "TempTexture ");
      this._printTexture(t1);
    }
    this.myCamera.targetTexture = (RenderTexture) null;
    RenderTexture.active = (RenderTexture) null;
    this.targTex = t1;
    if (this.LOG_LEVEL < MB2_LogLevel.debug)
      return;
    elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
    UnityEngine.Debug.Log((object) ("Total time to copy RenderTexture to Texture2D " + elapsedMilliseconds.ToString("f5")));
  }

  private Color32 ConvertNormalFormatFromUnity_ToStandard(Color32 c)
  {
    Vector3 zero = Vector3.zero with
    {
      x = (float) ((double) c.a * 2.0 - 1.0),
      y = (float) ((double) c.g * 2.0 - 1.0)
    };
    zero.z = Mathf.Sqrt((float) (1.0 - (double) zero.x * (double) zero.x - (double) zero.y * (double) zero.y));
    return new Color32()
    {
      a = 1,
      r = (byte) (((double) zero.x + 1.0) * 0.5),
      g = (byte) (((double) zero.y + 1.0) * 0.5),
      b = (byte) (((double) zero.z + 1.0) * 0.5)
    };
  }

  private bool IsOpenGL() => SystemInfo.graphicsDeviceVersion.StartsWith("OpenGL");

  private void CopyScaledAndTiledToAtlas(
    MB3_TextureCombiner.MB_TexSet texSet,
    MB3_TextureCombiner.MeshBakerMaterialTexture source,
    Vector2 obUVoffset,
    Vector2 obUVscale,
    Rect rec,
    ShaderTextureProperty texturePropertyName,
    TextureBlender resultMatTexBlender)
  {
    Rect screenRect1 = rec;
    this.myCamera.backgroundColor = resultMatTexBlender == null ? MB3_TextureCombiner.GetColorIfNoTexture(texturePropertyName) : resultMatTexBlender.GetColorIfNoTexture(texSet.matsAndGOs.mats[0].mat, texturePropertyName);
    if ((UnityEngine.Object) source.t == (UnityEngine.Object) null)
      source.t = this.combiner._createTemporaryTexture(16, 16, TextureFormat.ARGB32, true);
    screenRect1.y = (float) (1.0 - ((double) screenRect1.y + (double) screenRect1.height));
    screenRect1.x *= (float) this._destinationTexture.width;
    screenRect1.y *= (float) this._destinationTexture.height;
    screenRect1.width *= (float) this._destinationTexture.width;
    screenRect1.height *= (float) this._destinationTexture.height;
    Rect rect = screenRect1;
    rect.x -= (float) this._padding;
    rect.y -= (float) this._padding;
    rect.width += (float) (this._padding * 2);
    rect.height += (float) (this._padding * 2);
    Rect r1 = source.matTilingRect.GetRect();
    Rect screenRect2 = new Rect();
    if (this._fixOutOfBoundsUVs)
    {
      Rect r2 = new Rect(obUVoffset.x, obUVoffset.y, obUVscale.x, obUVscale.y);
      r1 = MB3_UVTransformUtility.CombineTransforms(ref r1, ref r2);
      if (this.LOG_LEVEL >= MB2_LogLevel.trace)
        UnityEngine.Debug.Log((object) ("Fixing out of bounds UVs for tex " + ((object) source.t)?.ToString()));
    }
    Texture2D t = source.t;
    TextureWrapMode wrapMode = t.wrapMode;
    if ((double) r1.width == 1.0 && (double) r1.height == 1.0 && (double) r1.x == 0.0 && (double) r1.y == 0.0)
      t.wrapMode = TextureWrapMode.Clamp;
    else
      t.wrapMode = TextureWrapMode.Repeat;
    if (this.LOG_LEVEL >= MB2_LogLevel.trace)
      UnityEngine.Debug.Log((object) ("DrawTexture tex=" + t.name + " destRect=" + screenRect1.ToString() + " srcRect=" + r1.ToString() + " Mat=" + ((object) this.mat)?.ToString()));
    Rect sourceRect = new Rect();
    sourceRect.x = r1.x;
    sourceRect.y = (float) ((double) r1.y + 1.0 - 1.0 / (double) t.height);
    sourceRect.width = r1.width;
    sourceRect.height = 1f / (float) t.height;
    screenRect2.x = screenRect1.x;
    screenRect2.y = rect.y;
    screenRect2.width = screenRect1.width;
    screenRect2.height = (float) this._padding;
    RenderTexture active = RenderTexture.active;
    RenderTexture.active = this._destinationTexture;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = r1.x;
    sourceRect.y = r1.y;
    sourceRect.width = r1.width;
    sourceRect.height = 1f / (float) t.height;
    screenRect2.x = screenRect1.x;
    screenRect2.y = screenRect1.y + screenRect1.height;
    screenRect2.width = screenRect1.width;
    screenRect2.height = (float) this._padding;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = r1.x;
    sourceRect.y = r1.y;
    sourceRect.width = 1f / (float) t.width;
    sourceRect.height = r1.height;
    screenRect2.x = rect.x;
    screenRect2.y = screenRect1.y;
    screenRect2.width = (float) this._padding;
    screenRect2.height = screenRect1.height;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = (float) ((double) r1.x + 1.0 - 1.0 / (double) t.width);
    sourceRect.y = r1.y;
    sourceRect.width = 1f / (float) t.width;
    sourceRect.height = r1.height;
    screenRect2.x = screenRect1.x + screenRect1.width;
    screenRect2.y = screenRect1.y;
    screenRect2.width = (float) this._padding;
    screenRect2.height = screenRect1.height;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = r1.x;
    sourceRect.y = (float) ((double) r1.y + 1.0 - 1.0 / (double) t.height);
    sourceRect.width = 1f / (float) t.width;
    sourceRect.height = 1f / (float) t.height;
    screenRect2.x = rect.x;
    screenRect2.y = rect.y;
    screenRect2.width = (float) this._padding;
    screenRect2.height = (float) this._padding;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = (float) ((double) r1.x + 1.0 - 1.0 / (double) t.width);
    sourceRect.y = (float) ((double) r1.y + 1.0 - 1.0 / (double) t.height);
    sourceRect.width = 1f / (float) t.width;
    sourceRect.height = 1f / (float) t.height;
    screenRect2.x = screenRect1.x + screenRect1.width;
    screenRect2.y = rect.y;
    screenRect2.width = (float) this._padding;
    screenRect2.height = (float) this._padding;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = r1.x;
    sourceRect.y = r1.y;
    sourceRect.width = 1f / (float) t.width;
    sourceRect.height = 1f / (float) t.height;
    screenRect2.x = rect.x;
    screenRect2.y = screenRect1.y + screenRect1.height;
    screenRect2.width = (float) this._padding;
    screenRect2.height = (float) this._padding;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    sourceRect.x = (float) ((double) r1.x + 1.0 - 1.0 / (double) t.width);
    sourceRect.y = r1.y;
    sourceRect.width = 1f / (float) t.width;
    sourceRect.height = 1f / (float) t.height;
    screenRect2.x = screenRect1.x + screenRect1.width;
    screenRect2.y = screenRect1.y + screenRect1.height;
    screenRect2.width = (float) this._padding;
    screenRect2.height = (float) this._padding;
    Graphics.DrawTexture(screenRect2, (Texture) t, sourceRect, 0, 0, 0, 0, this.mat);
    Graphics.DrawTexture(screenRect1, (Texture) t, r1, 0, 0, 0, 0, this.mat);
    RenderTexture.active = active;
    t.wrapMode = wrapMode;
  }

  private void _printTexture(Texture2D t)
  {
    if (t.width * t.height > 100)
      UnityEngine.Debug.Log((object) "Not printing texture too large.");
    try
    {
      Color32[] pixels32 = t.GetPixels32();
      string message = "";
      for (int index1 = 0; index1 < t.height; ++index1)
      {
        for (int index2 = 0; index2 < t.width; ++index2)
          message = message + pixels32[index1 * t.width + index2].ToString() + ", ";
        message += "\n";
      }
      UnityEngine.Debug.Log((object) message);
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.Log((object) ("Could not print texture. texture may not be readable." + ex.ToString()));
    }
  }
}
