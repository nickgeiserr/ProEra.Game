// Decompiled with JetBrains decompiler
// Type: Save_SettingsStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Events;

[MessagePackObject(false)]
[Serializable]
public class Save_SettingsStore : ISaveSync
{
  [IgnoreMember]
  public static string FileName = nameof (Save_SettingsStore);
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public float SfxVolume = 1f;
  [Key(2)]
  public float BgmVolume = 1f;
  [Key(3)]
  public float HostVoVolume = 1f;
  [Key(4)]
  public float StadiumVolume = 1f;
  [Key(5)]
  public bool InstrumentalMusic;

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value) => this.isDirty = value;

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_SettingsStore> deserializedData = SaveIO.LoadAsync<Save_SettingsStore>(Path.Combine(SaveIO.DefaultFolderPath, Save_SettingsStore.FileName));
    Save_SettingsStore saveSettingsStore = await deserializedData;
    if (deserializedData.Result != null)
      this.CopyData(deserializedData.Result);
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<Save_SettingsStore>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<Save_SettingsStore>) null;
    }
  }

  private void CopyData(Save_SettingsStore loadedData)
  {
    this.metaData = loadedData.metaData;
    this.SfxVolume = loadedData.SfxVolume;
    this.BgmVolume = loadedData.BgmVolume;
    this.HostVoVolume = loadedData.HostVoVolume;
    this.StadiumVolume = loadedData.StadiumVolume;
    this.InstrumentalMusic = loadedData.InstrumentalMusic;
  }

  public async Task Save()
  {
    Save_SettingsStore objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_SettingsStore>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_SettingsStore.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
