// Decompiled with JetBrains decompiler
// Type: Save_TwoMD
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[MessagePackObject(false)]
[Serializable]
public class Save_TwoMD : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "SaveTwoMD";
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public float BestScore;
  [IgnoreMember]
  private bool isDirty;

  public void CopyData(Save_TwoMD value)
  {
    if (value == null)
      return;
    this.BestScore = value.BestScore;
    this.metaData = value.metaData;
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value)
  {
    this.isDirty = value;
    if (!this.isDirty)
      return;
    AppEvents.SaveTwoMinuteDrill.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_TwoMD> t = SaveIO.LoadAsync<Save_TwoMD>(Path.Combine(SaveIO.DefaultFolderPath, Save_TwoMD.FileName));
    Save_TwoMD saveTwoMd = await t;
    this.CopyData(t.Result);
    t = (Task<Save_TwoMD>) null;
  }

  public async Task Save()
  {
    Save_TwoMD objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_TwoMD>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_TwoMD.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
