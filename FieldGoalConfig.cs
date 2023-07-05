// Decompiled with JetBrains decompiler
// Type: FieldGoalConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class FieldGoalConfig : MonoBehaviour, IGameplayConfig
{
  [Header("CPU")]
  [Tooltip("The lowest kick meter value the CPU can have.  The kick power used for this kick is the average of the kick meter value and the player's kickPower rating.")]
  [Range(0.0f, 99f)]
  public int kickMeterValueMin = 95;
  [Tooltip("The highest kick meter value the CPU can have.  The kick power used for this kick is the average of the kick meter value and the player's kickPower rating.")]
  [Range(0.0f, 99f)]
  public int kickMeterValueMax = 98;
  [Header("Distance")]
  [Tooltip("If the kicker's kickPower is this value or lower, the ball will travel 'Field Goal Yards Min' yards.")]
  public int kickPowerForMinYards = 47;
  [Tooltip("If the kicker's kickPower is this value or higher, the ball will travel 'Field Goal Yards Max' yards.")]
  public int kickPowerForMaxYards = 99;
  [Tooltip("If the kicker's kickPower is 'Kick Power For Min Yards' or lower, the ball will travel this many yards.")]
  public float fieldGoalYardsMin = 25f;
  [Tooltip("If the kicker's kickPower is 'Kick Power For Max Yards' or higher, the ball will travel this many yards.")]
  public float fieldGoalYardsMax = 61f;
  [Header("Accuracy")]
  [Tooltip("If the kicker's kickAccuracy is this value or higher, the kicker will miss the ideal angle by at most 'Field Goal Min Variance Degrees'.")]
  public int kickAccuracyForMinVariance = 99;
  [Tooltip("If the kicker's kickAccuracy is this value or lower, the kicker will miss the ideal angle by at most 'Field Goal Min Variance Degrees'.")]
  public int kickAccuracyForMaxVariance = 25;
  [Tooltip("If the kicker's kickAccuracy is 'Kick Accuracy For Min Variance' or higher, the kicker will miss the ideal angle by at most this many degrees.")]
  public float fieldGoalMinVarianceDegrees = 5f;
  [Tooltip("If the kicker's kickAccuracy is 'Kick Accuracy For Max Variance' or higher, the kicker will miss the ideal angle by at most this many degrees.")]
  public float fieldGoalMaxVarianceDegrees = 20f;
  [Header("Goal Line Power Reduction")]
  [Tooltip("If the ball is spotted this far from the goal line, the kicker's kickPower will be reduced by 'Kick Power Reduction At Min Dist'.")]
  public int kickPowerMinYardsFromGoalLine;
  [Tooltip("If the ball is spotted this far from the goal line, the kicker's kickPower will be reduced by 'Kick Power Reduction At Max Dist'.")]
  public int kickPowerMaxYardsFromGoalLine = 20;
  [Tooltip("If the ball is spotted at 'Kick Power Min Yards From Goal Line' from the scoring end zone, the kicker's kickPower will be reduced by this much.")]
  public float kickPowerReductionAtMinDist = 13.3f;
  [Tooltip("If the ball is spotted at 'Kick Power Max Yards From Goal Line' from the scoring end zone, the kicker's kickPower will be reduced by this much.")]
  public float kickPowerReductionAtMaxDist;
}
