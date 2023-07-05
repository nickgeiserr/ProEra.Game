// Decompiled with JetBrains decompiler
// Type: FootballVR.SpawnEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  [ExecuteAlways]
  public class SpawnEffect : MonoBehaviour
  {
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Material _spawnEffectSharedMaterial;
    [SerializeField]
    private Renderer[] _renderers;
    [SerializeField]
    private GameObject _targetObject;
    public float fade;
    public float height;
    public bool originalMaterial;
    private bool _originalMat;
    public bool effectMaterial;
    private bool _effectMat;
    private static readonly int FadeProp = Shader.PropertyToID("_Fade");
    private static readonly int HeightProp = Shader.PropertyToID("_Height");
    private static readonly int VisibleProp = Animator.StringToHash(nameof (Visible));
    private bool _materialsCached;
    private Material[] _originalMaterials;
    private bool _spawnMaterialInstanced;
    private Material _spawnEffectMaterial;

    public bool Visible
    {
      set
      {
        if (!this._materialsCached)
          this.CacheMaterials();
        if (value && (Object) this._targetObject != (Object) null)
          this._targetObject.SetActive(true);
        this._animator.SetBool(SpawnEffect.VisibleProp, value);
      }
    }

    private Material[] originalMaterials
    {
      get
      {
        if (this._materialsCached)
          return this._originalMaterials;
        if (this._renderers == null || this._renderers.Length == 0)
        {
          Debug.LogError((object) "SpawnEffect can not work without any renderers assigned. Call PlayEffect method with list of renderers, or assign them using inspector.");
          return (Material[]) null;
        }
        this.CacheMaterials();
        return this._originalMaterials;
      }
    }

    private Material spawnEffectMaterial
    {
      get
      {
        if (this._spawnMaterialInstanced)
          return this._spawnEffectMaterial;
        if ((Object) this._spawnEffectSharedMaterial == (Object) null)
          return (Material) null;
        this._spawnMaterialInstanced = true;
        return this._spawnEffectMaterial = new Material(this._spawnEffectSharedMaterial);
      }
    }

    private void Awake() => this.transform.position = this.transform.position.SetY(0.0f);

    private void Update()
    {
      Material spawnEffectMaterial = this.spawnEffectMaterial;
      if (!this._spawnMaterialInstanced)
        return;
      spawnEffectMaterial.SetFloat(SpawnEffect.FadeProp, this.fade);
      spawnEffectMaterial.SetFloat(SpawnEffect.HeightProp, this.height);
      if (this._originalMat == this.originalMaterial && this._effectMat == this.effectMaterial)
        return;
      this.UpdateMaterials(this.originalMaterial, this.effectMaterial);
    }

    public void Initialize(Renderer[] renderers = null)
    {
      if (renderers != null && renderers != this._renderers)
      {
        this._materialsCached = false;
        this._renderers = renderers;
        foreach (Renderer renderer in renderers)
        {
          if ((Object) renderer != (Object) null)
            renderer.enabled = true;
        }
      }
      if (!this._materialsCached)
        this.CacheMaterials();
      this.originalMaterial = false;
      this.effectMaterial = true;
      this.Update();
    }

    private void UpdateMaterials(bool defaultMat, bool spawnEffectMat)
    {
      this._originalMat = defaultMat;
      this._effectMat = spawnEffectMat;
      if (defaultMat & spawnEffectMat)
      {
        for (int index = 0; index < this._renderers.Length; ++index)
          this._renderers[index].sharedMaterials = new Material[2]
          {
            this.originalMaterials[index],
            this.spawnEffectMaterial
          };
      }
      else if (spawnEffectMat)
      {
        for (int index = 0; index < this._renderers.Length; ++index)
          this._renderers[index].sharedMaterials = new Material[1]
          {
            this.spawnEffectMaterial
          };
      }
      else if (defaultMat)
      {
        for (int index = 0; index < this._renderers.Length; ++index)
          this._renderers[index].sharedMaterials = new Material[1]
          {
            this.originalMaterials[index]
          };
      }
      else
      {
        Material[] materialArray = new Material[0];
        for (int index = 0; index < this._renderers.Length; ++index)
          this._renderers[index].sharedMaterials = materialArray;
      }
    }

    [ContextMenu("Auto-find renderers")]
    public void GrabRenderers()
    {
      Renderer[] componentsInChildren = ((Object) this._targetObject != (Object) null ? this._targetObject : this.gameObject).GetComponentsInChildren<Renderer>();
      List<Renderer> rendererList = new List<Renderer>(componentsInChildren.Length);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        Renderer renderer = componentsInChildren[index];
        if (!renderer.gameObject.name.Contains("Beam"))
        {
          switch (renderer)
          {
            case MeshRenderer _:
            case SkinnedMeshRenderer _:
              rendererList.Add(renderer);
              continue;
            default:
              continue;
          }
        }
      }
      this._renderers = rendererList.ToArray();
      this.CacheMaterials();
    }

    [ContextMenu("Prepare")]
    private void CacheMaterials()
    {
      this._originalMaterials = new Material[this._renderers.Length];
      for (int index = 0; index < this._renderers.Length; ++index)
        this._originalMaterials[index] = this._renderers[index].sharedMaterial;
      this._materialsCached = true;
    }

    public void ActivateTarget()
    {
      if (!((Object) this._targetObject != (Object) null))
        return;
      this._targetObject.SetActive(true);
    }

    public void HideTarget()
    {
      if (!((Object) this._targetObject != (Object) null))
        return;
      this._targetObject.SetActive(false);
    }

    public void PlayEffect() => this._animator.Play(0);
  }
}
