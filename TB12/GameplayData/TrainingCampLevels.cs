// Decompiled with JetBrains decompiler
// Type: TB12.GameplayData.TrainingCampLevels
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12.GameplayData
{
  [CreateAssetMenu(fileName = "TrainingCampLevels", menuName = "TB12/Data/TrainingCampLevels", order = 1)]
  public class TrainingCampLevels : ScriptableObject
  {
    public StationaryTargetLevel[] StationaryTargetLevels;
    public ThreadTheNeedleLevel[] ThreadTheNeedleLevels;
    public MovingTargetLevel[] MovingTargetLevels;
    public ScrambleLevel[] ScrambleLevels;
    public PocketLevel[] PocketLevels;
    public int TimeBonusMultiplier;
    public float[] LevelBonusMultipliers;

    public TrainingCampLevel GetLevel(int gameMode, int level)
    {
      if (level < 0)
        return (TrainingCampLevel) null;
      switch (gameMode)
      {
        case 0:
          return (TrainingCampLevel) this.StationaryTargetLevels[level];
        case 1:
          return (TrainingCampLevel) this.ThreadTheNeedleLevels[level];
        case 2:
          return (TrainingCampLevel) this.MovingTargetLevels[level];
        case 3:
          return (TrainingCampLevel) this.ScrambleLevels[level];
        case 4:
          return (TrainingCampLevel) this.PocketLevels[level];
        default:
          return (TrainingCampLevel) null;
      }
    }

    public static bool IsThrowingTraining(TrainingCampLevel level) => !(level is PocketLevel);
  }
}
