// Decompiled with JetBrains decompiler
// Type: TeamSeasonData
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
public class TeamSeasonData : ISaveSync
{
  [IgnoreMember]
  public string FileName = "-Season-Profile";
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoad;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  private string fullId;
  [Key(2)]
  public int win;
  [Key(3)]
  public int loss;
  [Key(4)]
  public int tie;

  [IgnoreMember]
  private static string id { get; } = "-Season-Profile";

  public TeamSeasonData()
  {
  }

  public TeamSeasonData(string teamName) => this.FileName = teamName + TeamSeasonData.id;

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value) => this.isDirty = value;

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<TeamSeasonData> deserializedData = SaveIO.LoadAsync<TeamSeasonData>(Path.Combine(SaveIO.DefaultFolderPath, this.FileName));
    TeamSeasonData teamSeasonData = await deserializedData;
    if (deserializedData.Result == null)
    {
      deserializedData = (Task<TeamSeasonData>) null;
    }
    else
    {
      this.CopyData(deserializedData.Result);
      deserializedData = (Task<TeamSeasonData>) null;
    }
  }

  private void CopyData(TeamSeasonData LoadedData)
  {
    this.metaData = LoadedData.metaData;
    this.fullId = LoadedData.fullId;
    this.win = LoadedData.win;
    this.loss = LoadedData.loss;
    this.tie = LoadedData.tie;
  }

  public async Task Save()
  {
    TeamSeasonData objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<TeamSeasonData>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, objectTarget.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
