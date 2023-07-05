// Decompiled with JetBrains decompiler
// Type: TB12.SettingsStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using UnityEngine;
using UnityEngine.Events;
using Vars;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/Settings Store")]
  [AppStore]
  public class SettingsStore : SingletonScriptableSettings<SettingsStore>
  {
    [NonSerialized]
    public Save_SettingsStore saveSettingsStore = new Save_SettingsStore();
    [NonSerialized]
    public VariableFloat SfxVolume = new VariableFloat(0.5f);
    [NonSerialized]
    public VariableFloat BgmVolume = new VariableFloat(1f);
    [NonSerialized]
    public VariableFloat HostVoVolume = new VariableFloat(1f);
    [NonSerialized]
    public VariableFloat StadiumVolume = new VariableFloat(1f);
    [NonSerialized]
    public VariableBool InstrumentalMusic = new VariableBool(false);
    private bool _initialized;

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
    }

    public void ResetStore()
    {
      this.SfxVolume.Value = 1f;
      this.BgmVolume.Value = 1f;
      this.HostVoVolume.Value = 1f;
      this.StadiumVolume.Value = 1f;
      this.InstrumentalMusic.Value = false;
    }

    public void Deinitialize()
    {
      this.ResetStore();
      this._initialized = false;
    }

    public void PrepareToLoad()
    {
      this.saveSettingsStore = new Save_SettingsStore();
      this.saveSettingsStore.OnLoaded += new UnityAction(this.SettingsLoaded);
    }

    private void SettingsLoaded()
    {
      this.SfxVolume.SetValue(this.saveSettingsStore.SfxVolume);
      this.BgmVolume.SetValue(this.saveSettingsStore.BgmVolume);
      this.HostVoVolume.SetValue(this.saveSettingsStore.HostVoVolume);
      this.StadiumVolume.SetValue(this.saveSettingsStore.StadiumVolume);
      this.InstrumentalMusic.SetValue(this.saveSettingsStore.InstrumentalMusic);
      this.saveSettingsStore.OnLoaded -= new UnityAction(this.SettingsLoaded);
    }

    public void PrepareToSave()
    {
      this.saveSettingsStore.SfxVolume = this.SfxVolume.Value;
      this.saveSettingsStore.BgmVolume = this.BgmVolume.Value;
      this.saveSettingsStore.HostVoVolume = this.HostVoVolume.Value;
      this.saveSettingsStore.StadiumVolume = this.StadiumVolume.Value;
      this.saveSettingsStore.InstrumentalMusic = this.InstrumentalMusic.Value;
    }
  }
}
