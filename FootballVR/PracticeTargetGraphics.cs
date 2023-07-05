// Decompiled with JetBrains decompiler
// Type: FootballVR.PracticeTargetGraphics
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace FootballVR
{
  public class PracticeTargetGraphics : MonoBehaviour
  {
    [SerializeField]
    private PracticeTarget _target;
    [SerializeField]
    private MeshRenderer _renderer;
    [SerializeField]
    private Color _defaultColor;
    [SerializeField]
    private Color _disabledColor;
    private MaterialPropertyBlock _mpb;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void OnValidate()
    {
      if (!((UnityEngine.Object) this._target == (UnityEngine.Object) null))
        return;
      this._target = this.GetComponent<PracticeTarget>();
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this._target == (UnityEngine.Object) null)
        this._target = this.GetComponent<PracticeTarget>();
      if ((UnityEngine.Object) this._target != (UnityEngine.Object) null)
        this._target.OnValidityChanged += new Action<bool>(this.HandleValidState);
      this._mpb = new MaterialPropertyBlock();
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this._target != (UnityEngine.Object) null))
        return;
      this._target.OnValidityChanged -= new Action<bool>(this.HandleValidState);
    }

    private void HandleValidState(bool state)
    {
      if (!((UnityEngine.Object) this._renderer != (UnityEngine.Object) null))
        return;
      this._mpb.SetColor(PracticeTargetGraphics.EmissionColor, state ? this._defaultColor : this._disabledColor);
      this._renderer.SetPropertyBlock(this._mpb);
    }
  }
}
