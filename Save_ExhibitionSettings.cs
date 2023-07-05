// Decompiled with JetBrains decompiler
// Type: Save_ExhibitionSettings
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
public class Save_ExhibitionSettings : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "SaveExhibitionSettings";
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public int WeatherTypeIndex;
  [Key(2)]
  public int TimeOfDayIndex;
  [Key(3)]
  public int WindIndex;
  [Key(4)]
  public int DefDifficulty = 1;
  [Key(5)]
  public int OffDifficulty = 1;
  [Key(6)]
  public int QuarterLengthIndex = 1;
  [Key(7)]
  public bool UseFatigue = true;
  [Key(8)]
  public bool UseInjuries = true;
  [IgnoreMember]
  private bool isDirty;

  public void CopyData(Save_ExhibitionSettings value)
  {
    if (value == null)
      return;
    this.WeatherTypeIndex = value.WeatherTypeIndex;
    this.TimeOfDayIndex = value.TimeOfDayIndex;
    this.WindIndex = value.WindIndex;
    this.DefDifficulty = value.DefDifficulty;
    this.OffDifficulty = value.OffDifficulty;
    this.QuarterLengthIndex = value.QuarterLengthIndex;
    this.UseFatigue = value.UseFatigue;
    this.UseInjuries = value.UseInjuries;
    this.metaData = value.metaData;
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value)
  {
    this.isDirty = value;
    if (!this.isDirty)
      return;
    AppEvents.SaveExhibitionSettings.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_ExhibitionSettings> t = SaveIO.LoadAsync<Save_ExhibitionSettings>(Path.Combine(SaveIO.DefaultFolderPath, Save_ExhibitionSettings.FileName));
    Save_ExhibitionSettings exhibitionSettings = await t;
    this.CopyData(t.Result);
    t = (Task<Save_ExhibitionSettings>) null;
  }

  public async Task Save()
  {
    Save_ExhibitionSettings objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_ExhibitionSettings>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_ExhibitionSettings.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
