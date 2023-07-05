// Decompiled with JetBrains decompiler
// Type: KickReturnConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class KickReturnConfig : MonoBehaviour, IGameplayConfig
{
  [Header("Blocking")]
  [Tooltip("At the start of the play, front line blockers will drop back to the a position specified by their assignment.  They will randomly vary how far they drop back by this many yards.")]
  public float maxYardLineVarianceForFrontLine = 5f;
  [Tooltip("At the start of the play, up blockers will drop back to the a position specified by their assignment.  They will randomly vary how far they drop back by this many yards.")]
  public float maxYardLineVarianceForUpBlockers = 2.5f;
  [Tooltip("When moving to their spot, the blockers will stay at least this many yards in front of the landing spot.")]
  public float minYardsInFrontOfLandingSpot = 10f;
  [Tooltip("Width of the cone used to find a blocker in front of the player")]
  public float minAngleDiffFromForwardToBlockTargetDegrees = 90f;
  [Tooltip("Max distance for a player to be considered a valid block target")]
  public float maxYardsFromBlockerToTarget = 20f;
  [Tooltip("Max distance for an up blocker to switch to blocking even if the ball hasn't been caught yet.")]
  public float maxYardsFromTargetForUpBlockerToSwitchEarly = 7f;
  [Header("Catching")]
  [Tooltip("Don't try to start the kick catch animation until the receiver is within this distance of where the ball will be at the catch time")]
  public float maxYardsFromKickCatchPos = 2f;
  [Tooltip("Don't try the kick catch animation if it would be early by this many seconds.")]
  public float maxSecondsBeforeIdealCatchStartTime;
  [Tooltip("Don't try the kick catch animation if it would be late by this many seconds.")]
  public float maxSecondsAfterIdealStartCatchTime = 1.5f;

  public float GetRandomDepthOffsetDistance(EKickRetBlockerType blockerType)
  {
    float distance;
    switch (blockerType)
    {
      case EKickRetBlockerType.UpBlocker:
        distance = Field.ConvertYardsToDistance(this.maxYardLineVarianceForUpBlockers);
        break;
      case EKickRetBlockerType.FrontLineBlocker:
        distance = Field.ConvertYardsToDistance(this.maxYardLineVarianceForFrontLine);
        break;
      default:
        return 0.0f;
    }
    return Random.Range(-distance, distance);
  }

  public float GetMaxDistanceFromBlockerToTarget() => Field.ConvertYardsToDistance(this.maxYardsFromBlockerToTarget);

  public float GetMinDistanceInFrontOfLandingSpot() => Field.ConvertYardsToDistance(this.minYardsInFrontOfLandingSpot);

  public float GetMaxDistanceFromTargetForUpBlockerToSwitchEarly() => Field.ConvertYardsToDistance(this.maxYardsFromTargetForUpBlockerToSwitchEarly);

  public float GetMaxDistanceFromKickCatchPos() => Field.ConvertYardsToDistance(this.maxYardsFromKickCatchPos);
}
