// Decompiled with JetBrains decompiler
// Type: Save_MiniCamp
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TB12;

[MessagePackObject(false)]
[Serializable]
public class Save_MiniCamp : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "SaveMiniCamp";
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public MiniCampData SelectedMiniCamp;
  [Key(2)]
  public MiniCampEntry SelectedEntry;
  [Key(3)]
  public MiniCampData[] MiniCamps;
  [Key(4)]
  public bool LoadedTemplate;
  [IgnoreMember]
  private bool isDirty;

  public void Select(EMiniCampTourType miniCampTourType)
  {
    foreach (MiniCampData miniCamp in this.MiniCamps)
    {
      if (miniCamp.MiniCampTourType == miniCampTourType)
      {
        this.SelectedMiniCamp = miniCamp;
        break;
      }
    }
  }

  public MiniCampData GetMiniCampData(EMiniCampTourType miniCampTourType)
  {
    MiniCampData miniCampData = (MiniCampData) null;
    foreach (MiniCampData miniCamp in this.MiniCamps)
    {
      if (miniCamp.MiniCampTourType == miniCampTourType)
        miniCampData = miniCamp;
    }
    return miniCampData;
  }

  public bool AreLevelsMaxed()
  {
    bool flag = true;
    foreach (MiniCampData miniCamp in this.MiniCamps)
    {
      if (miniCamp.CurrentLevel < 4)
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public void MaximizeAllLevels()
  {
    foreach (MiniCampData miniCamp in this.MiniCamps)
      miniCamp.CurrentLevel = 4;
  }

  public void MinimizeAllLevels()
  {
    foreach (MiniCampData miniCamp in this.MiniCamps)
      miniCamp.CurrentLevel = 1;
  }

  public void CopyData(Save_MiniCamp value)
  {
    if (value == null)
      return;
    this.SelectedMiniCamp = value.SelectedMiniCamp;
    this.SelectedEntry = value.SelectedEntry;
    this.MiniCamps = value.MiniCamps;
    this.LoadedTemplate = value.LoadedTemplate;
    this.metaData = value.metaData;
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value)
  {
    this.isDirty = value;
    if (!this.isDirty)
      return;
    AppEvents.SaveMiniCamp.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_MiniCamp> t = SaveIO.LoadAsync<Save_MiniCamp>(Path.Combine(SaveIO.DefaultFolderPath, Save_MiniCamp.FileName));
    Save_MiniCamp saveMiniCamp = await t;
    this.CopyData(t.Result);
    t = (Task<Save_MiniCamp>) null;
  }

  public async Task Save()
  {
    Save_MiniCamp objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_MiniCamp>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_MiniCamp.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
