// Decompiled with JetBrains decompiler
// Type: FxProNS.BloomHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FxProNS
{
  public class BloomHelper : Singleton<BloomHelper>, IDisposable
  {
    private static Material _mat;
    private BloomHelperParams p;
    private int bloomSamples = 5;
    private float bloomBlurRadius = 5f;

    public static Material Mat
    {
      get
      {
        if ((UnityEngine.Object) null == (UnityEngine.Object) BloomHelper._mat)
        {
          Material material = new Material(Shader.Find("Hidden/BloomPro"));
          material.hideFlags = HideFlags.HideAndDontSave;
          BloomHelper._mat = material;
        }
        return BloomHelper._mat;
      }
    }

    public void SetParams(BloomHelperParams _p) => this.p = _p;

    public void Init()
    {
      float num = Mathf.Exp(this.p.BloomIntensity) - 1f;
      if (Application.platform == RuntimePlatform.IPhonePlayer)
        this.p.BloomThreshold *= 0.75f;
      BloomHelper.Mat.SetFloat("_BloomThreshold", this.p.BloomThreshold);
      BloomHelper.Mat.SetFloat("_BloomIntensity", num);
      BloomHelper.Mat.SetColor("_BloomTint", this.p.BloomTint);
      if (this.p.Quality == EffectsQuality.High || this.p.Quality == EffectsQuality.Normal)
      {
        this.bloomSamples = 5;
        BloomHelper.Mat.EnableKeyword("BLOOM_SAMPLES_5");
        BloomHelper.Mat.DisableKeyword("BLOOM_SAMPLES_3");
      }
      if (this.p.Quality == EffectsQuality.Fast || this.p.Quality == EffectsQuality.Fastest)
      {
        this.bloomSamples = 3;
        BloomHelper.Mat.EnableKeyword("BLOOM_SAMPLES_3");
        BloomHelper.Mat.DisableKeyword("BLOOM_SAMPLES_5");
      }
      if (this.p.Quality == EffectsQuality.High)
      {
        this.bloomBlurRadius = 10f;
        BloomHelper.Mat.EnableKeyword("BLUR_RADIUS_10");
        BloomHelper.Mat.DisableKeyword("BLUR_RADIUS_5");
      }
      else
      {
        this.bloomBlurRadius = 5f;
        BloomHelper.Mat.EnableKeyword("BLUR_RADIUS_5");
        BloomHelper.Mat.DisableKeyword("BLUR_RADIUS_10");
      }
      float[] bloomTexFactors = this.CalculateBloomTexFactors(Mathf.Exp(this.p.BloomSoftness) - 1f);
      if (bloomTexFactors.Length == 5)
      {
        BloomHelper.Mat.SetVector("_BloomTexFactors1", new Vector4(bloomTexFactors[0], bloomTexFactors[1], bloomTexFactors[2], bloomTexFactors[3]));
        BloomHelper.Mat.SetVector("_BloomTexFactors2", new Vector4(bloomTexFactors[4], 0.0f, 0.0f, 0.0f));
      }
      else if (bloomTexFactors.Length == 3)
        BloomHelper.Mat.SetVector("_BloomTexFactors1", new Vector4(bloomTexFactors[0], bloomTexFactors[1], bloomTexFactors[2], 0.0f));
      else
        Debug.LogError((object) ("Unsupported bloomTexFactors.Length: " + bloomTexFactors.Length.ToString()));
      RenderTextureManager.Instance.Dispose();
    }

    public void RenderBloomTexture(RenderTexture source, RenderTexture dest)
    {
      RenderTexture a = RenderTextureManager.Instance.RequestRenderTexture(source.width, source.height, source.depth, source.format);
      Graphics.Blit((Texture) source, a, BloomHelper.Mat, 0);
      for (int index = 1; index <= this.bloomSamples; ++index)
      {
        float _spread = Mathf.Lerp(1f, 2f, (float) (index - 1) / (float) this.bloomSamples);
        RenderTextureManager.Instance.SafeAssign(ref a, FxPro.DownsampleTex(a, 2f));
        RenderTextureManager.Instance.SafeAssign(ref a, this.BlurTex(a, _spread));
        BloomHelper.Mat.SetTexture("_DsTex" + index.ToString(), (Texture) a);
      }
      Graphics.Blit((Texture) null, dest, BloomHelper.Mat, 1);
      RenderTextureManager.Instance.ReleaseRenderTexture(a);
    }

    public RenderTexture BlurTex(RenderTexture _input, float _spread)
    {
      float num = _spread * 10f / this.bloomBlurRadius;
      RenderTexture renderTexture = RenderTextureManager.Instance.RequestRenderTexture(_input.width, _input.height, _input.depth, _input.format);
      RenderTexture dest = RenderTextureManager.Instance.RequestRenderTexture(_input.width, _input.height, _input.depth, _input.format);
      BloomHelper.Mat.SetVector("_SeparableBlurOffsets", new Vector4(1f, 0.0f, 0.0f, 0.0f) * num);
      Graphics.Blit((Texture) _input, renderTexture, BloomHelper.Mat, 2);
      BloomHelper.Mat.SetVector("_SeparableBlurOffsets", new Vector4(0.0f, 1f, 0.0f, 0.0f) * num);
      Graphics.Blit((Texture) renderTexture, dest, BloomHelper.Mat, 2);
      RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture);
      return dest;
    }

    private float[] CalculateBloomTexFactors(float softness)
    {
      float[] _in = new float[this.bloomSamples];
      for (int index = 0; index < _in.Length; ++index)
      {
        float t = (float) index / (float) (_in.Length - 1);
        _in[index] = Mathf.Lerp(1f, softness, t);
      }
      return this.MakeSumOne((IList<float>) _in);
    }

    private float[] MakeSumOne(IList<float> _in)
    {
      float num = _in.Sum();
      float[] numArray = new float[_in.Count];
      for (int index = 0; index < _in.Count; ++index)
        numArray[index] = _in[index] / num;
      return numArray;
    }

    public void Dispose()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) BloomHelper.Mat)
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) BloomHelper.Mat);
      RenderTextureManager.Instance.Dispose();
    }
  }
}
