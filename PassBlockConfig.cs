// Decompiled with JetBrains decompiler
// Type: PassBlockConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PassBlockConfig : MonoBehaviour, IGameplayConfig
{
  [Header("Finding Best Block Target")]
  [Tooltip("The Max Angle To a Block Target")]
  public float maxPassBlockTargetAngle = 90f;
  [Tooltip("The Max Angle to a Block Target When Dropping Back")]
  public float maxPassDropBackBlockTargetAngle = 45f;
  [Tooltip("The Max Distance To a Block Target for Blockers at Line.")]
  public float maxDistToBlockTarget = 5f;
  [Tooltip("The Max Distance To a Block Target for Blocking Backs Behind Line.")]
  public float maxDistToBlockTargetBlockingBack = 10f;
  [Tooltip("The Max X Distance To a Block Target.")]
  public float maxXDistToBlockTarget = 3f;
  [Header("Scoring of Block Target")]
  [Tooltip("The Targeting Score Weight For Distance to Block Target")]
  public float distScoreWeight = 100f;
  [Tooltip("The Targeting Score Weight For X Distance to Block Target ")]
  public float xDistScoreWeight = 50f;
  [Tooltip("The Targeting Score Weight For X Distance to Predicted Block Target")]
  public float xDistPredictedScoreWeight = 25f;
  [Tooltip("The Targeting Score Weight For X Distance to Block Target ")]
  public float defPosCrossLosScore = 50f;
  [Tooltip("The Targeting Score Weight For X Distance to Predicted Block Target")]
  public float defPredPosCrossLosScore = 20f;
  [Tooltip("The Targeting Score Addtion For Edge Rushers")]
  public float defEdgeRushScore = 50f;
  [Tooltip("The Min Score Diff For Switching Block Target")]
  public float minScoreDifForTargetSwitch = 60f;
  [Tooltip(" Block Score White Line")]
  public float maxBlockScoreWhiteLine = 50f;
  [Tooltip(" Block Score Cyan Line")]
  public float maxBlockScoreCyanLine = 100f;
  [Tooltip(" Block Score Blue Line")]
  public float maxBlockScoreMagentaLine = 200f;
  [Header("Blocking Timers")]
  [Tooltip(" Min Play Time Before Pass BLockEnagement")]
  public float minPlayTimeForPassBlockEngagement = 0.75f;
}
