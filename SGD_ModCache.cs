﻿// Decompiled with JetBrains decompiler
// Type: SGD_ModCache
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;

[MessagePackObject(false)]
[Serializable]
public class SGD_ModCache
{
  [Key(0)]
  public string fileName;
  [Key(1)]
  public double timeStamp;
  [Key(2)]
  public string gameVersion;
  [Key(3)]
  public string userName;
  [Key(4)]
  public bool markAsDeleted;
  [Key(5)]
  public bool isCacheValid;
  [Key(6)]
  public string[] ModFolders;

  public void SetFileName(string f) => this.fileName = f;

  public string GetFileName() => this.fileName;

  public void UpdateTimeStamp() => this.timeStamp = SaveSyncUtils.Current();

  public DateTime GetTimeStamp() => SaveSyncUtils.ConvertToDate(this.timeStamp);

  public void UpdateGameVersion() => this.gameVersion = SaveIO.gameVersion;

  public string GetGameVersion() => this.gameVersion;

  public void UpdateUserName(string u) => this.userName = u;

  public string GetUserName() => this.userName;

  public SGD_ModCache(string f)
  {
    this.SetFileName(f);
    this.SetDefaultValues();
  }

  private void SetDefaultValues()
  {
    this.isCacheValid = false;
    this.ModFolders = new string[0];
  }

  public void InvalidateCache() => this.isCacheValid = false;
}
