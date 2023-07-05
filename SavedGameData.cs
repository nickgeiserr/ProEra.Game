// Decompiled with JetBrains decompiler
// Type: SavedGameData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class SavedGameData
{
  [Key("fileName")]
  public string fileName;
  [Key("timeStamp")]
  public double timeStamp;
  [Key("gameVers")]
  public string gameVersion;
  [Key("userName")]
  public string userName;
  [Key("toDelete")]
  public bool markAsDeleted;

  public void SetFileName(string f) => this.fileName = f;

  public string GetFileName() => this.fileName;

  public void UpdateTimeStamp() => this.timeStamp = SaveSyncUtils.Current();

  public DateTime GetTimeStamp() => SaveSyncUtils.ConvertToDate(this.timeStamp);

  public void UpdateGameVersion() => this.gameVersion = SaveIO.gameVersion;

  public string GetGameVersion() => this.gameVersion;

  public void UpdateUserName(string u) => this.userName = u;

  public string GetUserName() => this.userName;
}
