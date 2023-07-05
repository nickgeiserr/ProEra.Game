// Decompiled with JetBrains decompiler
// Type: FootballVR.CameraEffects
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class CameraEffects : MonoBehaviour
  {
    [SerializeField]
    private OVRVignette _vignette;
    private CameraEffectsSettings _settings;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    public static readonly VariableFloat VignetteIntensity = new VariableFloat(0.0f);
    public static readonly VariableBool VignetteState = new VariableBool();

    private void Awake()
    {
      this._settings = ScriptableSingleton<VRSettings>.Instance.cameraEffects;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        CameraEffects.VignetteIntensity.Link<float>(new Action<float>(this.HandleVignetteIntensity)),
        CameraEffects.VignetteState.Link<bool>(new Action<bool>(this.HandleVignetteStateChanged)),
        this._settings.VignetteEnabled.Link<bool>(new Action<bool>(this.HandleVignetteStateChanged)),
        this._settings.VignetteAspectRatio.Link<float>((Action<float>) (ratio => this._vignette.VignetteAspectRatio = ratio)),
        this._settings.VignetteFalloffDegrees.Link<float>((Action<float>) (degrees => this._vignette.VignetteFalloffDegrees = degrees))
      });
    }

    private void OnDestroy() => this._linksHandler.Clear();

    private void HandleVignetteIntensity(float intensity) => this._vignette.VignetteFieldOfView = Mathf.Lerp(this._settings.VignetteMaxFov, this._settings.VignetteMinFov, intensity);

    private void HandleVignetteStateChanged(bool state) => this._vignette.enabled = (bool) CameraEffects.VignetteState && (bool) this._settings.VignetteEnabled;
  }
}
