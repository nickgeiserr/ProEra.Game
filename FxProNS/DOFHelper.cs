// Decompiled with JetBrains decompiler
// Type: FxProNS.DOFHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace FxProNS
{
  public class DOFHelper : Singleton<DOFHelper>, IDisposable
  {
    private static Material _mat;
    private DOFHelperParams _p;
    private float _curAutoFocusDist;

    public static Material Mat
    {
      get
      {
        if ((UnityEngine.Object) null == (UnityEngine.Object) DOFHelper._mat)
        {
          Material material = new Material(Shader.Find("Hidden/DOFPro"));
          material.hideFlags = HideFlags.HideAndDontSave;
          DOFHelper._mat = material;
        }
        return DOFHelper._mat;
      }
    }

    public void SetParams(DOFHelperParams p) => this._p = p;

    public void Init(bool searchForNonDepthmapAlphaObjects)
    {
      if (this._p == null)
        Debug.LogError((object) "Call SetParams first");
      else if ((UnityEngine.Object) null == (UnityEngine.Object) this._p.EffectCamera)
      {
        Debug.LogError((object) "null == p.camera");
      }
      else
      {
        if ((UnityEngine.Object) null == (UnityEngine.Object) DOFHelper.Mat)
          return;
        DOFHelper.Mat.EnableKeyword("USE_CAMERA_DEPTH_TEXTURE");
        DOFHelper.Mat.DisableKeyword("DONT_USE_CAMERA_DEPTH_TEXTURE");
        this._p.FocalLengthMultiplier = Mathf.Clamp(this._p.FocalLengthMultiplier, 0.01f, 0.99f);
        this._p.DepthCompression = Mathf.Clamp(this._p.DepthCompression, 1f, 10f);
        Shader.SetGlobalFloat("_OneOverDepthScale", this._p.DepthCompression);
        Shader.SetGlobalFloat("_OneOverDepthFar", 1f / this._p.EffectCamera.farClipPlane);
        if (!this._p.BokehEnabled)
          return;
        DOFHelper.Mat.SetFloat("_BokehThreshold", this._p.BokehThreshold);
        DOFHelper.Mat.SetFloat("_BokehGain", this._p.BokehGain);
        DOFHelper.Mat.SetFloat("_BokehBias", this._p.BokehBias);
      }
    }

    public void SetBlurRadius(int radius)
    {
      Shader.DisableKeyword("BLUR_RADIUS_10");
      Shader.DisableKeyword("BLUR_RADIUS_5");
      Shader.DisableKeyword("BLUR_RADIUS_3");
      Shader.DisableKeyword("BLUR_RADIUS_2");
      Shader.DisableKeyword("BLUR_RADIUS_1");
      if (radius != 10 && radius != 5 && radius != 3 && radius != 2 && radius != 1)
        radius = 5;
      if (radius < 3)
        radius = 3;
      Shader.EnableKeyword("BLUR_RADIUS_" + radius.ToString());
    }

    private void CalculateAndUpdateFocalDist()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this._p.EffectCamera)
      {
        Debug.LogError((object) "null == p.camera");
      }
      else
      {
        float num = (this._p.AutoFocus || !((UnityEngine.Object) null != (UnityEngine.Object) this._p.Target) ? (this._curAutoFocusDist = Mathf.Lerp(this._curAutoFocusDist, this.CalculateAutoFocusDist(), Time.deltaTime * this._p.AutoFocusSpeed)) : this._p.EffectCamera.WorldToViewportPoint(this._p.Target.position).z) / this._p.EffectCamera.farClipPlane * (this._p.FocalDistMultiplier * this._p.DepthCompression);
        DOFHelper.Mat.SetFloat("_FocalDist", num);
        DOFHelper.Mat.SetFloat("_FocalLength", num * this._p.FocalLengthMultiplier);
      }
    }

    private float CalculateAutoFocusDist()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this._p.EffectCamera)
        return 0.0f;
      RaycastHit hitInfo;
      return !Physics.Raycast(this._p.EffectCamera.transform.position, this._p.EffectCamera.transform.forward, out hitInfo, float.PositiveInfinity, this._p.AutoFocusLayerMask.value) ? this._p.EffectCamera.farClipPlane : hitInfo.distance;
    }

    public void RenderCOCTexture(RenderTexture src, RenderTexture dest, float blurScale)
    {
      this.CalculateAndUpdateFocalDist();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this._p.EffectCamera)
      {
        Debug.LogError((object) "null == p.camera");
      }
      else
      {
        if (this._p.EffectCamera.depthTextureMode == DepthTextureMode.None)
          this._p.EffectCamera.depthTextureMode = DepthTextureMode.Depth;
        if ((double) this._p.DOFBlurSize > 1.0 / 1000.0)
        {
          RenderTexture renderTexture1 = RenderTextureManager.Instance.RequestRenderTexture(src.width, src.height, src.depth, src.format);
          RenderTexture renderTexture2 = RenderTextureManager.Instance.RequestRenderTexture(src.width, src.height, src.depth, src.format);
          Graphics.Blit((Texture) src, renderTexture1, DOFHelper.Mat, 0);
          DOFHelper.Mat.SetVector("_SeparableBlurOffsets", new Vector4(blurScale, 0.0f, 0.0f, 0.0f));
          Graphics.Blit((Texture) renderTexture1, renderTexture2, DOFHelper.Mat, 2);
          DOFHelper.Mat.SetVector("_SeparableBlurOffsets", new Vector4(0.0f, blurScale, 0.0f, 0.0f));
          Graphics.Blit((Texture) renderTexture2, dest, DOFHelper.Mat, 2);
          RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture1);
          RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture2);
        }
        else
          Graphics.Blit((Texture) src, dest, DOFHelper.Mat, 0);
      }
    }

    public void RenderDOFBlur(RenderTexture src, RenderTexture dest, RenderTexture cocTexture)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) cocTexture)
      {
        Debug.LogError((object) "null == cocTexture");
      }
      else
      {
        DOFHelper.Mat.SetTexture("_COCTex", (Texture) cocTexture);
        if (this._p.BokehEnabled)
        {
          DOFHelper.Mat.SetFloat("_BlurIntensity", this._p.DOFBlurSize);
          Graphics.Blit((Texture) src, dest, DOFHelper.Mat, 4);
        }
        else
        {
          RenderTexture renderTexture = RenderTextureManager.Instance.RequestRenderTexture(src.width, src.height, src.depth, src.format);
          DOFHelper.Mat.SetVector("_SeparableBlurOffsets", new Vector4(this._p.DOFBlurSize, 0.0f, 0.0f, 0.0f));
          Graphics.Blit((Texture) src, renderTexture, DOFHelper.Mat, 1);
          DOFHelper.Mat.SetVector("_SeparableBlurOffsets", new Vector4(0.0f, this._p.DOFBlurSize, 0.0f, 0.0f));
          Graphics.Blit((Texture) renderTexture, dest, DOFHelper.Mat, 1);
          RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture);
        }
      }
    }

    public void RenderEffect(RenderTexture src, RenderTexture dest) => this.RenderEffect(src, dest, false);

    public void RenderEffect(RenderTexture src, RenderTexture dest, bool visualizeCOC)
    {
      RenderTexture renderTexture = RenderTextureManager.Instance.RequestRenderTexture(src.width, src.height, src.depth, src.format);
      this.RenderCOCTexture(src, renderTexture, 0.0f);
      if (visualizeCOC)
      {
        Graphics.Blit((Texture) renderTexture, dest);
        RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture);
        RenderTextureManager.Instance.ReleaseAllRenderTextures();
      }
      else
      {
        this.RenderDOFBlur(src, dest, renderTexture);
        RenderTextureManager.Instance.ReleaseRenderTexture(renderTexture);
      }
    }

    public static GameObject[] GetNonDepthmapAlphaObjects()
    {
      if (!Application.isPlaying)
        return new GameObject[0];
      Renderer[] objectsOfType = UnityEngine.Object.FindObjectsOfType<Renderer>();
      List<GameObject> gameObjectList = new List<GameObject>();
      List<Material> materialList = new List<Material>();
      foreach (Renderer renderer in objectsOfType)
      {
        if (renderer.sharedMaterials != null && !((UnityEngine.Object) null != (UnityEngine.Object) renderer.GetComponent<ParticleSystem>()))
        {
          foreach (Material sharedMaterial in renderer.sharedMaterials)
          {
            if (!((UnityEngine.Object) null == (UnityEngine.Object) sharedMaterial.shader))
            {
              bool flag = sharedMaterial.GetTag("RenderType", false) == null;
              if (flag || !(sharedMaterial.GetTag("RenderType", false).ToLower() == "transparent") && !(sharedMaterial.GetTag("Queue", false).ToLower() == "transparent"))
              {
                if (sharedMaterial.GetTag("OUTPUT_DEPTH_TO_ALPHA", false) == null || sharedMaterial.GetTag("OUTPUT_DEPTH_TO_ALPHA", false).ToLower() != "true")
                  flag = true;
                if (flag && !materialList.Contains(sharedMaterial))
                {
                  materialList.Add(sharedMaterial);
                  Debug.Log((object) ("Non-depthmapped: " + DOFHelper.GetFullPath(renderer.gameObject)));
                  gameObjectList.Add(renderer.gameObject);
                }
              }
            }
          }
        }
      }
      return gameObjectList.ToArray();
    }

    public static string GetFullPath(GameObject obj)
    {
      string str = "/" + obj.name;
      while ((UnityEngine.Object) obj.transform.parent != (UnityEngine.Object) null)
      {
        obj = obj.transform.parent.gameObject;
        str = "/" + obj.name + str;
      }
      return "'" + str + "'";
    }

    public void Dispose()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) DOFHelper.Mat)
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) DOFHelper.Mat);
      RenderTextureManager.Instance.Dispose();
    }
  }
}
