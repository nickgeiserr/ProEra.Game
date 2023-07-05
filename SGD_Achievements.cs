// Decompiled with JetBrains decompiler
// Type: SGD_Achievements
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class SGD_Achievements
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
  public int numberOfAchievements = 10;
  [Key(6)]
  public bool[] achievementEarnedStatus;

  public void SetFileName(string f) => this.fileName = f;

  public string GetFileName() => this.fileName;

  public void UpdateTimeStamp() => this.timeStamp = SaveSyncUtils.Current();

  public DateTime GetTimeStamp() => SaveSyncUtils.ConvertToDate(this.timeStamp);

  public void UpdateGameVersion() => this.gameVersion = SaveIO.gameVersion;

  public string GetGameVersion() => this.gameVersion;

  public void UpdateUserName(string u) => this.userName = u;

  public string GetUserName() => this.userName;

  public SGD_Achievements(string f)
  {
    this.SetFileName(f);
    this.SetDefaultValues();
  }

  public void EarnAchievement(int i)
  {
    if (i > this.achievementEarnedStatus.Length - 1)
      Debug.Log((object) ("Attempting to earn achievement index: " + i.ToString() + ", which is beyond the bounds of the array.Please change the numberOfAchievements variables in SGD_Achievements.cs"));
    else
      this.achievementEarnedStatus[i] = true;
  }

  private void SetDefaultValues()
  {
    this.achievementEarnedStatus = new bool[this.numberOfAchievements + 1];
    for (int index = 0; index < this.achievementEarnedStatus.Length; ++index)
      this.achievementEarnedStatus[index] = false;
  }
}
