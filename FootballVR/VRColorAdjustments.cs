// Decompiled with JetBrains decompiler
// Type: FootballVR.VRColorAdjustments
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class VRColorAdjustments : MonoBehaviour
  {
    [SerializeField]
    private MeshRenderer _renderer;
    private int _shaderColorId;
    private MaterialPropertyBlock _mpb;
    private bool _initialized;

    public Color colorFilter
    {
      set
      {
        this._mpb.SetColor(this._shaderColorId, value);
        this._renderer.SetPropertyBlock(this._mpb);
      }
    }

    public bool active
    {
      set => this._renderer.enabled = value;
    }

    private void Awake() => this.Initialize();

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      if ((Object) this._renderer == (Object) null)
        this._renderer = this.GetComponent<MeshRenderer>();
      this._shaderColorId = Shader.PropertyToID("_Color");
      this._mpb = new MaterialPropertyBlock();
      this._renderer.enabled = true;
      this._renderer.sortingOrder = 4;
    }
  }
}
