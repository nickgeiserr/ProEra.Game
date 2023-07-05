// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.SaveAchievements
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProEra.Game.Achievements
{
  [MessagePackObject(false)]
  [Serializable]
  public class SaveAchievements : ISaveSync
  {
    [IgnoreMember]
    public static string FileName = nameof (SaveAchievements);
    [IgnoreMember]
    private bool isDirty;
    [Key(0)]
    public Dictionary<string, string> metaData = new Dictionary<string, string>();
    [Key(1)]
    public Dictionary<string, Achievement> Achievements = new Dictionary<string, Achievement>();
    [Key(2)]
    public Dictionary<string, AcknowledgeableAward> TeamsDefeatedByIndex = new Dictionary<string, AcknowledgeableAward>();
    [Key(3)]
    public Dictionary<string, AcknowledgeableAward> SuperBowlAwardsByTeam = new Dictionary<string, AcknowledgeableAward>();

    public bool GetDirty() => this.isDirty;

    public void SetDirty(bool value)
    {
      this.isDirty = value;
      if (!this.isDirty)
        return;
      AppEvents.SaveAchievements.Trigger();
    }

    public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

    public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

    public async Task Load()
    {
      SaveAchievements saveAchievements = await SaveIO.LoadAsync<SaveAchievements>(SaveIO.GetPath(SaveAchievements.FileName));
      if (saveAchievements != null)
      {
        PersistentSingleton<SaveManager>.Instance.achievements = saveAchievements;
        SaveManager.GetAchievementData().Init();
      }
      else
      {
        Debug.Log((object) "COULD NOT FIND ACHIEVEMENTS DATA");
        SaveManager.GetAchievementData().Init();
      }
    }

    public async Task Save()
    {
      SaveAchievements objectTarget = this;
      SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
      SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
      SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
      await SaveIO.SaveAsync<SaveAchievements>(objectTarget, SaveIO.GetPath(SaveAchievements.FileName));
      SaveManager.GetAchievementData().Init();
    }

    public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

    public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
  }
}
