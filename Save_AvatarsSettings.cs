// Decompiled with JetBrains decompiler
// Type: Save_AvatarsSettings
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
public class Save_AvatarsSettings : ISaveSync
{
  [IgnoreMember]
  public static string FileName = nameof (Save_AvatarsSettings);
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public bool CustomStateBehavior = true;
  [Key(2)]
  public bool PlayModeCollision;
  [Key(3)]
  public bool PlayersCollisionPhysics;
  [Key(4)]
  public float CatchUpThreshold = 0.08f;
  [Key(5)]
  public float SoftCatchUpThreshold = 0.8f;
  [Key(6)]
  public float SoftCatchupSpeed = 6f;
  [Key(7)]
  public float CatchupLerpFactor = 0.25f;
  [Key(8)]
  public float CatchUpMaxSpeed = 40f;

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value) => this.isDirty = value;

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_AvatarsSettings> deserializedData = SaveIO.LoadAsync<Save_AvatarsSettings>(Path.Combine(SaveIO.DefaultFolderPath, Save_AvatarsSettings.FileName));
    Save_AvatarsSettings saveAvatarsSettings = await deserializedData;
    if (deserializedData.Result != null)
      this.CopyData(deserializedData.Result);
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<Save_AvatarsSettings>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<Save_AvatarsSettings>) null;
    }
  }

  private void CopyData(Save_AvatarsSettings loadedData)
  {
    this.metaData = loadedData.metaData;
    this.CustomStateBehavior = loadedData.CustomStateBehavior;
    this.PlayModeCollision = loadedData.PlayModeCollision;
    this.PlayersCollisionPhysics = loadedData.PlayersCollisionPhysics;
    this.CatchUpThreshold = loadedData.CatchUpThreshold;
    this.SoftCatchUpThreshold = loadedData.SoftCatchUpThreshold;
    this.SoftCatchupSpeed = loadedData.SoftCatchupSpeed;
    this.CatchupLerpFactor = loadedData.CatchupLerpFactor;
    this.CatchUpMaxSpeed = loadedData.CatchUpMaxSpeed;
  }

  public async Task Save()
  {
    Save_AvatarsSettings objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_AvatarsSettings>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_AvatarsSettings.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
