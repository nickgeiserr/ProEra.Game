// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.AchievementTier
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;

namespace ProEra.Game.Achievements
{
  [MessagePackObject(false)]
  public class AchievementTier
  {
    [Key(0)]
    public int[] _levels;
    [Key(1)]
    public int Count;

    [IgnoreMember]
    public int[] Levels => this._levels;

    public AchievementTier()
    {
    }

    public AchievementTier(int[] levels)
    {
      this._levels = levels;
      this.Count = levels.Length;
    }

    public int GetTierFromProgress(int progressValue)
    {
      int length = this._levels.Length;
      for (int tierFromProgress = 0; tierFromProgress < length; ++tierFromProgress)
      {
        if (progressValue >= this._levels[tierFromProgress])
          return tierFromProgress;
      }
      return -1;
    }
  }
}
