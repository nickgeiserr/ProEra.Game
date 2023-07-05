// Decompiled with JetBrains decompiler
// Type: FootballWorld.FakeShadow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballWorld
{
  public class FakeShadow : MonoBehaviour
  {
    [SerializeField]
    private Transform _objectTx;
    [SerializeField]
    private Renderer _shadowRenderer;
    [SerializeField]
    private bool _reactToHeight;
    private readonly Quaternion rot = Quaternion.identity;
    private MaterialPropertyBlock _mpb;
    private Transform _tx;
    private bool _initialized;
    private Color _shadowColor = Color.white;
    public static int BaseColor = Shader.PropertyToID("_BaseColor");

    private void Awake()
    {
      this._tx = this.transform;
      this.Initialize();
    }

    private void Update()
    {
      this._tx.SetPositionAndRotation(this._objectTx.position.SetY(0.005f), this.rot);
      if (!this._reactToHeight)
        return;
      float y = this.transform.position.y;
      this.SetSizeFactor(Mathf.Clamp(y / 2f, 0.5f, 1.5f));
      this.SetShadowAlpha(1f / Mathf.Clamp(y, 1f, 2f));
    }

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
      this._mpb = new MaterialPropertyBlock();
    }

    private void SetSizeFactor(float size) => this.transform.localScale = Vector3.one * size;

    private void SetShadowAlpha(float alpha)
    {
      this._shadowRenderer.GetPropertyBlock(this._mpb);
      this._shadowColor.a = alpha;
      this._mpb.SetColor(FakeShadow.BaseColor, this._shadowColor);
      this._shadowRenderer.SetPropertyBlock(this._mpb);
    }
  }
}
