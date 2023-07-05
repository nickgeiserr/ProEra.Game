// Decompiled with JetBrains decompiler
// Type: FxPro
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FxProNS;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/FxPro™")]
public class FxPro : MonoBehaviour
{
  public EffectsQuality Quality = EffectsQuality.Normal;
  private static Material _mat;
  private static Material _tapMat;
  private Camera _effectCamera;
  public bool BloomEnabled = true;
  public BloomHelperParams BloomParams = new BloomHelperParams();
  public bool VisualizeBloom;
  public Texture2D LensDirtTexture;
  [Range(0.0f, 2f)]
  public float LensDirtIntensity = 1f;
  public bool ChromaticAberration = true;
  public bool ChromaticAberrationPrecise;
  [Range(1f, 2.5f)]
  public float ChromaticAberrationOffset = 1f;
  [Range(0.0f, 1f)]
  public float SCurveIntensity = 0.5f;
  public bool LensCurvatureEnabled = true;
  [Range(1f, 2f)]
  public float LensCurvaturePower = 1.1f;
  public bool LensCurvaturePrecise;
  [Range(0.0f, 1f)]
  public float FilmGrainIntensity = 0.5f;
  [Range(1f, 10f)]
  public float FilmGrainTiling = 4f;
  [Range(0.0f, 1f)]
  public float VignettingIntensity = 0.5f;
  public bool DOFEnabled = true;
  public bool BlurCOCTexture = true;
  public DOFHelperParams DOFParams = new DOFHelperParams();
  public bool VisualizeCOC;
  private List<Texture2D> _filmGrainTextures;
  public bool ColorEffectsEnabled = true;
  public Color CloseTint = new Color(1f, 0.5f, 0.0f, 1f);
  public Color FarTint = new Color(0.0f, 0.0f, 1f, 1f);
  [Range(0.0f, 1f)]
  public float CloseTintStrength = 0.5f;
  [Range(0.0f, 1f)]
  public float FarTintStrength = 0.5f;
  [Range(0.0f, 2f)]
  public float DesaturateDarksStrength = 0.5f;
  [Range(0.0f, 1f)]
  public float DesaturateFarObjsStrength = 0.5f;
  public Color FogTint = Color.white;
  [Range(0.0f, 1f)]
  public float FogStrength = 0.5f;
  public bool HalfResolution;

  public static Material Mat
  {
    get
    {
      if ((Object) null == (Object) FxPro._mat)
      {
        Material material = new Material(Shader.Find("Hidden/FxPro"));
        material.hideFlags = HideFlags.HideAndDontSave;
        FxPro._mat = material;
      }
      return FxPro._mat;
    }
  }

  private static Material TapMat
  {
    get
    {
      if ((Object) null == (Object) FxPro._tapMat)
      {
        Material material = new Material(Shader.Find("Hidden/FxProTap"));
        material.hideFlags = HideFlags.HideAndDontSave;
        FxPro._tapMat = material;
      }
      return FxPro._tapMat;
    }
  }

  private Camera EffectCamera
  {
    get
    {
      if ((Object) null == (Object) this._effectCamera)
        this._effectCamera = this.GetComponent<Camera>();
      return this._effectCamera;
    }
  }

  public void Start()
  {
    this._filmGrainTextures = new List<Texture2D>();
    for (int index = 1; index <= 4; ++index)
    {
      string path = "filmgrain_0" + index.ToString();
      Texture2D texture2D = Resources.Load(path) as Texture2D;
      if ((Object) null == (Object) texture2D)
        Debug.LogError((object) ("Unable to load grain texture '" + path + "'"));
      else
        this._filmGrainTextures.Add(texture2D);
    }
  }

  public void Init(bool searchForNonDepthmapAlphaObjects = false)
  {
    if (this.HalfResolution)
    {
      Resolution currentResolution = Screen.currentResolution;
      int width = currentResolution.width / 2;
      currentResolution = Screen.currentResolution;
      int height = currentResolution.height / 2;
      int num = Screen.fullScreen ? 1 : 0;
      currentResolution = Screen.currentResolution;
      int refreshRate = currentResolution.refreshRate;
      Screen.SetResolution(width, height, num != 0, refreshRate);
    }
    FxPro.Mat.SetFloat("_DirtIntensity", Mathf.Exp(this.LensDirtIntensity) - 1f);
    if ((Object) null == (Object) this.LensDirtTexture || (double) this.LensDirtIntensity <= 0.0)
    {
      FxPro.Mat.DisableKeyword("LENS_DIRT_ON");
      FxPro.Mat.EnableKeyword("LENS_DIRT_OFF");
    }
    else
    {
      FxPro.Mat.SetTexture("_LensDirtTex", (Texture) this.LensDirtTexture);
      FxPro.Mat.EnableKeyword("LENS_DIRT_ON");
      FxPro.Mat.DisableKeyword("LENS_DIRT_OFF");
    }
    if (this.ChromaticAberration)
    {
      FxPro.Mat.EnableKeyword("CHROMATIC_ABERRATION_ON");
      FxPro.Mat.DisableKeyword("CHROMATIC_ABERRATION_OFF");
    }
    else
    {
      FxPro.Mat.EnableKeyword("CHROMATIC_ABERRATION_OFF");
      FxPro.Mat.DisableKeyword("CHROMATIC_ABERRATION_ON");
    }
    if (this.EffectCamera.allowHDR)
    {
      Shader.EnableKeyword("FXPRO_HDR_ON");
      Shader.DisableKeyword("FXPRO_HDR_OFF");
    }
    else
    {
      Shader.EnableKeyword("FXPRO_HDR_OFF");
      Shader.DisableKeyword("FXPRO_HDR_ON");
    }
    FxPro.Mat.SetFloat("_SCurveIntensity", this.SCurveIntensity);
    if ((this.DOFEnabled || this.ColorEffectsEnabled) && this.EffectCamera.depthTextureMode == DepthTextureMode.None)
      this.EffectCamera.depthTextureMode = DepthTextureMode.Depth;
    if (this.DOFEnabled)
    {
      if ((Object) null == (Object) this.DOFParams.EffectCamera)
        this.DOFParams.EffectCamera = this.EffectCamera;
      this.DOFParams.DepthCompression = Mathf.Clamp(this.DOFParams.DepthCompression, 2f, 8f);
      Singleton<DOFHelper>.Instance.SetParams(this.DOFParams);
      Singleton<DOFHelper>.Instance.Init(searchForNonDepthmapAlphaObjects);
      FxPro.Mat.DisableKeyword("DOF_DISABLED");
      FxPro.Mat.EnableKeyword("DOF_ENABLED");
      if (!this.DOFParams.DoubleIntensityBlur)
        Singleton<DOFHelper>.Instance.SetBlurRadius(this.Quality == EffectsQuality.Fastest || this.Quality == EffectsQuality.Fast ? 3 : 5);
      else
        Singleton<DOFHelper>.Instance.SetBlurRadius(this.Quality == EffectsQuality.Fastest || this.Quality == EffectsQuality.Fast ? 5 : 10);
    }
    else
    {
      FxPro.Mat.EnableKeyword("DOF_DISABLED");
      FxPro.Mat.DisableKeyword("DOF_ENABLED");
    }
    if (this.BloomEnabled)
    {
      this.BloomParams.Quality = this.Quality;
      Singleton<BloomHelper>.Instance.SetParams(this.BloomParams);
      Singleton<BloomHelper>.Instance.Init();
      FxPro.Mat.DisableKeyword("BLOOM_DISABLED");
      FxPro.Mat.EnableKeyword("BLOOM_ENABLED");
    }
    else
    {
      FxPro.Mat.EnableKeyword("BLOOM_DISABLED");
      FxPro.Mat.DisableKeyword("BLOOM_ENABLED");
    }
    if (this.LensCurvatureEnabled)
    {
      this.UpdateLensCurvatureZoom();
      FxPro.Mat.SetFloat("_LensCurvatureBarrelPower", this.LensCurvaturePower);
    }
    if ((double) this.FilmGrainIntensity >= 1.0 / 1000.0)
    {
      FxPro.Mat.SetFloat("_FilmGrainIntensity", this.FilmGrainIntensity);
      FxPro.Mat.SetFloat("_FilmGrainTiling", this.FilmGrainTiling);
      FxPro.Mat.EnableKeyword("FILM_GRAIN_ON");
      FxPro.Mat.DisableKeyword("FILM_GRAIN_OFF");
    }
    else
    {
      FxPro.Mat.EnableKeyword("FILM_GRAIN_OFF");
      FxPro.Mat.DisableKeyword("FILM_GRAIN_ON");
    }
    if ((double) this.VignettingIntensity <= 1.0)
    {
      FxPro.Mat.SetFloat("_VignettingIntensity", this.VignettingIntensity);
      FxPro.Mat.EnableKeyword("VIGNETTING_ON");
      FxPro.Mat.DisableKeyword("VIGNETTING_OFF");
    }
    else
    {
      FxPro.Mat.EnableKeyword("VIGNETTING_OFF");
      FxPro.Mat.DisableKeyword("VIGNETTING_ON");
    }
    FxPro.Mat.SetFloat("_ChromaticAberrationOffset", this.ChromaticAberrationOffset);
    if (this.ColorEffectsEnabled)
    {
      FxPro.Mat.EnableKeyword("COLOR_FX_ON");
      FxPro.Mat.DisableKeyword("COLOR_FX_OFF");
      FxPro.Mat.SetColor("_CloseTint", this.CloseTint);
      FxPro.Mat.SetColor("_FarTint", this.FarTint);
      FxPro.Mat.SetFloat("_CloseTintStrength", this.CloseTintStrength);
      FxPro.Mat.SetFloat("_FarTintStrength", this.FarTintStrength);
      FxPro.Mat.SetFloat("_DesaturateDarksStrength", this.DesaturateDarksStrength);
      FxPro.Mat.SetFloat("_DesaturateFarObjsStrength", this.DesaturateFarObjsStrength);
      FxPro.Mat.SetColor("_FogTint", this.FogTint);
      FxPro.Mat.SetFloat("_FogStrength", this.FogStrength);
    }
    else
    {
      FxPro.Mat.EnableKeyword("COLOR_FX_OFF");
      FxPro.Mat.DisableKeyword("COLOR_FX_ON");
    }
  }

  public void OnEnable() => this.Init(true);

  public void OnDisable()
  {
    if ((Object) null != (Object) FxPro.Mat)
      Object.DestroyImmediate((Object) FxPro.Mat);
    RenderTextureManager.Instance.Dispose();
    Singleton<DOFHelper>.Instance.Dispose();
    Singleton<BloomHelper>.Instance.Dispose();
  }

  public void OnValidate() => this.Init();

  public static RenderTexture DownsampleTex(RenderTexture input, float downsampleBy)
  {
    RenderTexture dest = RenderTextureManager.Instance.RequestRenderTexture(Mathf.RoundToInt((float) input.width / downsampleBy), Mathf.RoundToInt((float) input.height / downsampleBy), input.depth, input.format);
    dest.filterMode = FilterMode.Bilinear;
    Graphics.BlitMultiTap((Texture) input, dest, FxPro.TapMat, new Vector2(-1f, -1f), new Vector2(-1f, 1f), new Vector2(1f, 1f), new Vector2(1f, -1f));
    return dest;
  }

  private RenderTexture ApplyColorEffects(RenderTexture input)
  {
    if (!this.ColorEffectsEnabled)
      return input;
    RenderTexture dest = RenderTextureManager.Instance.RequestRenderTexture(input.width, input.height, input.depth, input.format);
    Graphics.Blit((Texture) input, dest, FxPro.Mat, 5);
    return dest;
  }

  private RenderTexture ApplyLensCurvature(RenderTexture input)
  {
    if (!this.LensCurvatureEnabled)
      return input;
    RenderTexture dest = RenderTextureManager.Instance.RequestRenderTexture(input.width, input.height, input.depth, input.format);
    Graphics.Blit((Texture) input, dest, FxPro.Mat, this.LensCurvaturePrecise ? 3 : 4);
    return dest;
  }

  private RenderTexture ApplyChromaticAberration(RenderTexture input)
  {
    if (!this.ChromaticAberration)
      return (RenderTexture) null;
    RenderTexture dest = RenderTextureManager.Instance.RequestRenderTexture(input.width, input.height, input.depth, input.format);
    dest.filterMode = FilterMode.Bilinear;
    Graphics.Blit((Texture) input, dest, FxPro.Mat, 2);
    FxPro.Mat.SetTexture("_ChromAberrTex", (Texture) dest);
    return dest;
  }

  private Vector2 ApplyLensCurvature(Vector2 uv, float barrelPower, bool precise)
  {
    uv = uv * 2f - Vector2.one;
    uv.x *= this.EffectCamera.aspect * 2f;
    float f = Mathf.Atan2(uv.y, uv.x);
    float magnitude = uv.magnitude;
    float num = !precise ? Mathf.Lerp(magnitude, magnitude * magnitude, Mathf.Clamp01(barrelPower - 1f)) : Mathf.Pow(magnitude, barrelPower);
    uv.x = num * Mathf.Cos(f);
    uv.y = num * Mathf.Sin(f);
    uv.x /= this.EffectCamera.aspect * 2f;
    return 0.5f * (uv + Vector2.one);
  }

  private void UpdateLensCurvatureZoom() => FxPro.Mat.SetFloat("_LensCurvatureZoom", 1f / this.ApplyLensCurvature(new Vector2(1f, 1f), this.LensCurvaturePower, this.LensCurvaturePrecise).x);

  private void UpdateFilmGrain()
  {
    if ((double) this.FilmGrainIntensity < 1.0 / 1000.0)
      return;
    FxPro.Mat.SetTexture("_FilmGrainTex", (Texture) this._filmGrainTextures[Random.Range(0, 3)]);
    switch (Random.Range(0, 3))
    {
      case 0:
        FxPro.Mat.SetVector("_FilmGrainChannel", new Vector4(1f, 0.0f, 0.0f, 0.0f));
        break;
      case 1:
        FxPro.Mat.SetVector("_FilmGrainChannel", new Vector4(0.0f, 1f, 0.0f, 0.0f));
        break;
      case 2:
        FxPro.Mat.SetVector("_FilmGrainChannel", new Vector4(0.0f, 0.0f, 1f, 0.0f));
        break;
      case 3:
        FxPro.Mat.SetVector("_FilmGrainChannel", new Vector4(0.0f, 0.0f, 0.0f, 1f));
        break;
    }
  }

  private void RenderEffects(RenderTexture source, RenderTexture destination)
  {
    source.filterMode = FilterMode.Bilinear;
    this.UpdateFilmGrain();
    RenderTexture _tex = source;
    RenderTexture a1 = source;
    RenderTexture a2 = this.ApplyColorEffects(source);
    RenderTextureManager.Instance.SafeAssign(ref a2, this.ApplyLensCurvature(a2));
    if (this.ChromaticAberrationPrecise)
      _tex = this.ApplyChromaticAberration(a2);
    RenderTextureManager.Instance.SafeAssign(ref a1, FxPro.DownsampleTex(a2, 2f));
    if (this.Quality == EffectsQuality.Fastest)
      RenderTextureManager.Instance.SafeAssign(ref a1, FxPro.DownsampleTex(a1, 2f));
    RenderTexture renderTexture1 = (RenderTexture) null;
    RenderTexture renderTexture2 = (RenderTexture) null;
    if (this.DOFEnabled)
    {
      if ((Object) null == (Object) this.DOFParams.EffectCamera)
      {
        Debug.LogError((object) "null == DOFParams.camera");
        return;
      }
      renderTexture1 = RenderTextureManager.Instance.RequestRenderTexture(a1.width, a1.height, a1.depth, a1.format);
      Singleton<DOFHelper>.Instance.RenderCOCTexture(a1, renderTexture1, this.BlurCOCTexture ? 1.5f : 0.0f);
      if (this.VisualizeCOC)
      {
        Graphics.Blit((Texture) renderTexture1, destination, DOFHelper.Mat, 3);
        RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture1);
        RenderTextureManager.Instance.ReleaseRenderTexture(a1);
        return;
      }
      renderTexture2 = RenderTextureManager.Instance.RequestRenderTexture(a1.width, a1.height, a1.depth, a1.format);
      Singleton<DOFHelper>.Instance.RenderDOFBlur(a1, renderTexture2, renderTexture1);
      FxPro.Mat.SetTexture("_DOFTex", (Texture) renderTexture2);
      FxPro.Mat.SetTexture("_COCTex", (Texture) renderTexture1);
    }
    if (!this.ChromaticAberrationPrecise)
      _tex = this.ApplyChromaticAberration(a1);
    if (this.BloomEnabled)
    {
      RenderTexture renderTexture3 = RenderTextureManager.Instance.RequestRenderTexture(a1.width, a1.height, a1.depth, a1.format);
      Singleton<BloomHelper>.Instance.RenderBloomTexture(a1, renderTexture3);
      FxPro.Mat.SetTexture("_BloomTex", (Texture) renderTexture3);
      if (this.VisualizeBloom)
      {
        Graphics.Blit((Texture) renderTexture3, destination);
        return;
      }
    }
    Graphics.Blit((Texture) a2, destination, FxPro.Mat, 0);
    RenderTextureManager.Instance.ReleaseRenderTexture(a2);
    RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture1);
    RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture2);
    RenderTextureManager.Instance.ReleaseRenderTexture(a1);
    RenderTextureManager.Instance.ReleaseRenderTexture(_tex);
  }

  [ImageEffectTransformsToLDR]
  public void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    this.RenderEffects(source, destination);
    RenderTextureManager.Instance.ReleaseAllRenderTextures();
  }
}
