// Decompiled with JetBrains decompiler
// Type: RosterSaveData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using ProEra.Game.Sources.TeamData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[MessagePackObject(false)]
[Serializable]
public class RosterSaveData : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "RosterData";
  [IgnoreMember]
  private bool _isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public RosterFileData[] rosters = new RosterFileData[32];

  public bool GetDirty() => this._isDirty;

  public void SetDirty(bool value)
  {
    this._isDirty = value;
    if (!this._isDirty)
      return;
    AppEvents.SaveKeycloak.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<RosterSaveData> deserializedData = SaveIO.LoadAsync<RosterSaveData>(SaveIO.GetPath(RosterSaveData.FileName));
    RosterSaveData rosterSaveData = await deserializedData;
    if (deserializedData.Result != null)
      PersistentSingleton<RosterApi>.Instance.UpdateRosters(((IEnumerable<RosterFileData>) deserializedData.Result.rosters).Select<RosterFileData, int>((Func<RosterFileData, int>) (x => x.Version)).ToArray<int>());
    else
      Debug.Log((object) "COULD NOT FIND ROSTER DATA");
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<RosterSaveData>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<RosterSaveData>) null;
    }
  }

  public async Task Save()
  {
    RosterSaveData objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<RosterSaveData>(objectTarget, SaveIO.GetPath(RosterSaveData.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
