// Decompiled with JetBrains decompiler
// Type: FootballVR.HelmetController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using System;
using UnityEngine;

namespace FootballVR
{
  public class HelmetController : MonoBehaviour
  {
    private UniformStore _uniformStore;
    [SerializeField]
    protected Renderer _renderer;
    [SerializeField]
    protected Renderer _networkedRenderer;
    private MaterialPropertyBlock _mpb;
    private bool _init;
    private bool _forceHideHelmet;
    private bool _forceVisible;

    public event Action<BallObject> OnBallCollision;

    public Renderer Renderer => this._renderer;

    public Renderer NetworkedRenderer => this._networkedRenderer;

    public bool ForceVisible
    {
      get => this._forceVisible;
      set
      {
        this._forceVisible = value;
        if (this._forceVisible)
        {
          this.SetRenderState(true);
        }
        else
        {
          if (!this._forceHideHelmet)
            return;
          this.SetRenderState(false);
        }
      }
    }

    private void Awake() => this.Initialize();

    public void Initialize()
    {
      if (this._init)
        return;
      this._uniformStore = SaveManager.GetUniformStore();
      this._init = true;
      this._mpb = new MaterialPropertyBlock();
    }

    public void SetRenderState(bool state, bool networked = false)
    {
      if (this._forceVisible)
        state = true;
      else if (this._forceHideHelmet)
        state = false;
      Debug.Log((object) ("SetRenderState: networked[" + networked.ToString() + "] state[" + state.ToString() + "]"));
      if (!networked)
      {
        if (!(bool) (UnityEngine.Object) this._renderer)
          return;
        this._renderer.enabled = state;
      }
      else
      {
        if ((bool) (UnityEngine.Object) this._networkedRenderer)
          this._networkedRenderer.enabled = state;
        if (!(bool) (UnityEngine.Object) this._renderer)
          return;
        this._renderer.enabled = false;
      }
    }

    public void ShowHelmet(bool show, bool networked = false)
    {
      this._forceHideHelmet = !show;
      if (!this._forceHideHelmet || this.ForceVisible)
        return;
      this.SetRenderState(false, networked);
    }

    public void ApplyCustomization(ETeamUniformId uniformId)
    {
      this.gameObject.SetActive(true);
      FootballWorld.UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(uniformId, ETeamUniformFlags.Home);
      this._renderer.GetPropertyBlock(this._mpb);
      this._mpb.SetTexture(WorldConstants.Player.Basemap, (Texture) uniformConfig.BasemapAlternative);
      this._renderer.SetPropertyBlock(this._mpb);
    }

    private void OnCollisionEnter(Collision other)
    {
      if (!WorldConstants.Layers.Interactables.ContainsLayer(other.gameObject.layer))
        return;
      BallObject component = other.gameObject.GetComponent<BallObject>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      Action<BallObject> onBallCollision = this.OnBallCollision;
      if (onBallCollision == null)
        return;
      onBallCollision(component);
    }
  }
}
