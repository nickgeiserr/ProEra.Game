// Decompiled with JetBrains decompiler
// Type: TB12.ProfileProgress
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace TB12
{
  [MessagePackObject(false)]
  [Serializable]
  public class ProfileProgress : ISaveSync
  {
    [IgnoreMember]
    public static string FileName = "Solo-Profile";
    [IgnoreMember]
    private bool isDirty;
    [Key(0)]
    public Dictionary<string, string> metaData = new Dictionary<string, string>();
    [Key(1)]
    public int Points;
    [Key(2)]
    public int Level;
    [Key(3)]
    public float Progress;
    [Key(4)]
    public int MaxLevelCompleted;
    [Key(5)]
    public List<ProfileProgress.Entry> Datas = new List<ProfileProgress.Entry>();

    [Key(6)]
    public Dictionary<int, ProfileProgress.Entry> DataEntries { get; private set; } = new Dictionary<int, ProfileProgress.Entry>();

    public void Apply(int levelId, int stars, int score)
    {
      ProfileProgress.Entry entry;
      if (!this.DataEntries.TryGetValue(levelId, out entry))
      {
        entry = new ProfileProgress.Entry(levelId);
        this.DataEntries.Add(levelId, entry);
        this.Datas.Add(entry);
      }
      if (score > entry.Score)
      {
        entry.Score = score;
        entry.Star = stars;
      }
      if (levelId <= this.MaxLevelCompleted || stars <= 0)
        return;
      this.MaxLevelCompleted = levelId;
    }

    public ProfileProgress.Entry GetData(int levelId)
    {
      ProfileProgress.Entry entry;
      return !this.DataEntries.TryGetValue(levelId, out entry) ? (ProfileProgress.Entry) null : entry;
    }

    public void OnLoad()
    {
      if (this.DataEntries == null)
        this.DataEntries = new Dictionary<int, ProfileProgress.Entry>();
      this.DataEntries.Clear();
      List<ProfileProgress.Entry> entryList = new List<ProfileProgress.Entry>(0);
      for (int index = 0; index < this.Datas.Count; ++index)
      {
        ProfileProgress.Entry data = this.Datas[index];
        if (data != null)
        {
          ProfileProgress.Entry entry;
          if (!this.DataEntries.TryGetValue(data.LevelId, out entry))
          {
            this.DataEntries.Add(data.LevelId, data);
          }
          else
          {
            Debug.LogError((object) string.Format("Duplicate level data entry found. Entries are:\r\nExisting entry: {0}[{1}]\r\nNew Entry: {2}[{3}]", (object) entry.Score, (object) entry.Star, (object) data.Score, (object) data.Star));
            if (data.Star >= entry.Star && data.Score > entry.Score)
            {
              this.DataEntries[data.LevelId] = data;
              entryList.Add(entry);
            }
            else
              entryList.Add(data);
          }
        }
      }
      foreach (ProfileProgress.Entry entry in entryList)
        this.Datas.Remove(entry);
    }

    public bool GetDirty() => this.isDirty;

    public void SetDirty(bool value)
    {
      this.isDirty = value;
      if (!this.isDirty)
        return;
      AppEvents.SaveProfileProgress.Trigger();
    }

    public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

    public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

    public async Task Load()
    {
      Task<ProfileProgress> deserializedData = SaveIO.LoadAsync<ProfileProgress>(Path.Combine(SaveIO.DefaultFolderPath, ProfileProgress.FileName));
      ProfileProgress profileProgress = await deserializedData;
      if (deserializedData.Result == null)
      {
        deserializedData = (Task<ProfileProgress>) null;
      }
      else
      {
        PersistentSingleton<SaveManager>.Instance.profileProgress = deserializedData.Result;
        deserializedData = (Task<ProfileProgress>) null;
      }
    }

    public async Task Save()
    {
      ProfileProgress objectTarget = this;
      SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
      SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
      SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
      await SaveIO.SaveAsync<ProfileProgress>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, ProfileProgress.FileName));
    }

    public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

    public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);

    [MessagePackObject(false)]
    [Serializable]
    public class Entry
    {
      [Key(0)]
      public int LevelId;
      [Key(1)]
      public int Star;
      [Key(2)]
      public int Score;

      public Entry()
      {
      }

      public Entry(int id) => this.LevelId = id;
    }
  }
}
