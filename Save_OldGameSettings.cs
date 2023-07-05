// Decompiled with JetBrains decompiler
// Type: Save_OldGameSettings
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
public class Save_OldGameSettings : ISaveSync
{
  [IgnoreMember]
  public static string FileName = nameof (Save_OldGameSettings);
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public float TimeScale = 0.9f;
  [Key(2)]
  public bool OffenseGoingNorth = true;
  [Key(3)]
  public bool PlayerOnField;
  [Key(4)]
  public int DifficultyLevel = 1;
  [Key(5)]
  public float AutoDropBackBulletTimeSpeed = 0.5f;
  [Key(6)]
  public float HandoffBulletTimeSpeed = 0.5f;

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value) => this.isDirty = value;

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_OldGameSettings> deserializedData = SaveIO.LoadAsync<Save_OldGameSettings>(Path.Combine(SaveIO.DefaultFolderPath, Save_OldGameSettings.FileName));
    Save_OldGameSettings saveOldGameSettings = await deserializedData;
    if (deserializedData.Result != null)
      this.CopyData(deserializedData.Result);
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<Save_OldGameSettings>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<Save_OldGameSettings>) null;
    }
  }

  private void CopyData(Save_OldGameSettings loadedData)
  {
    this.metaData = loadedData.metaData;
    this.TimeScale = loadedData.TimeScale;
    this.OffenseGoingNorth = loadedData.OffenseGoingNorth;
    this.PlayerOnField = loadedData.PlayerOnField;
    this.DifficultyLevel = loadedData.DifficultyLevel;
    this.AutoDropBackBulletTimeSpeed = loadedData.AutoDropBackBulletTimeSpeed;
    this.HandoffBulletTimeSpeed = loadedData.HandoffBulletTimeSpeed;
  }

  public async Task Save()
  {
    Save_OldGameSettings objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_OldGameSettings>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_OldGameSettings.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
