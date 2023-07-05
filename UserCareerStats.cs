// Decompiled with JetBrains decompiler
// Type: UserCareerStats
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public sealed class UserCareerStats
{
  [Key(0)]
  public StatSet Stats = new StatSet();

  public static UserCareerStats FromSaveData(UserCareerStats.SaveData saveData) => new UserCareerStats()
  {
    Stats = saveData.Stats
  };

  public void AddStatsFromGame(StatSet statDelta) => this.Stats += statDelta;

  [MessagePackObject(false)]
  [Serializable]
  public class SaveData : ISaveSync
  {
    [Key(0)]
    public Dictionary<string, string> metaData = new Dictionary<string, string>();
    [Key(1)]
    public StatSet Stats = new StatSet();
    private bool isDirty;
    [IgnoreMember]
    public static readonly string FileName = "SaveCareerStats";

    public bool GetDirty() => this.isDirty;

    public void SetDirty(bool value)
    {
      this.isDirty = value;
      if (!this.isDirty)
        return;
      AppEvents.SaveCareerStats.Trigger();
    }

    public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

    public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

    public async Task Load()
    {
      UserCareerStats.SaveData saveData = await SaveIO.LoadAsync<UserCareerStats.SaveData>(SaveIO.GetPath(UserCareerStats.SaveData.FileName));
      if (saveData != null)
      {
        PersistentSingleton<SaveManager>.Instance.careerStats = saveData;
      }
      else
      {
        Debug.LogError((object) "[UserCareerStats] Failed to load save data for careers stats. Creating default");
        PersistentSingleton<SaveManager>.Instance.careerStats = new UserCareerStats.SaveData();
      }
    }

    public async Task Save()
    {
      UserCareerStats.SaveData objectTarget = this;
      SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
      SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
      SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
      await SaveIO.SaveAsync<UserCareerStats.SaveData>(objectTarget, SaveIO.GetPath(UserCareerStats.SaveData.FileName));
    }

    public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

    public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
  }
}
