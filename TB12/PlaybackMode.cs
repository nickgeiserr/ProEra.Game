// Decompiled with JetBrains decompiler
// Type: TB12.PlaybackMode
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using UnityEngine;
using Vars;

namespace TB12
{
  public class PlaybackMode : MonoBehaviour
  {
    private readonly VariableBool aButtonPressed = new VariableBool();
    private bool _paused;

    private void Awake()
    {
      this.enabled = (bool) DevControls.PlaybackMode;
      if (!this.enabled)
        return;
      this.aButtonPressed.OnValueChanged += new Action<bool>(this.HandlePress);
      VRState.LocomotionEnabled.SetValue(true);
    }

    private void OnDestroy() => this.aButtonPressed.OnValueChanged -= new Action<bool>(this.HandlePress);

    private void HandlePress(bool pressed)
    {
      if (!pressed)
        return;
      this._paused = !this._paused;
      Time.timeScale = this._paused ? 0.0f : 1f;
    }

    private void Update()
    {
    }
  }
}
