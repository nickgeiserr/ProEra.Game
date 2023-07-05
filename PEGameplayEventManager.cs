// Decompiled with JetBrains decompiler
// Type: PEGameplayEventManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using Vars;

public static class PEGameplayEventManager
{
  private static List<PEGameplayEvent> _playEvents = new List<PEGameplayEvent>();
  private static List<List<PEGameplayEvent>> _driveEvents = new List<List<PEGameplayEvent>>();
  private static List<PEGameplayEvent> _gameEvents = new List<PEGameplayEvent>();
  public static VariableBool PlayerOnSideline = new VariableBool(false);

  public static event Action<PEGameplayEvent> OnEventOccurred;

  public static List<PEGameplayEvent> PlayEvents => PEGameplayEventManager._playEvents;

  public static void RecordBallThrownEvent(
    float time,
    Vector3 ballPos,
    PlayerAI thrower,
    PlayerAI receiver,
    bool user)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEBallThrownEvent(time, ballPos, thrower, receiver, user));
  }

  public static void RecordBallCaughtEvent(
    float time,
    Vector3 ballPos,
    PlayerAI receiver,
    bool interception)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEBallCaughtEvent(time, ballPos, receiver, interception));
  }

  public static void RecordTackleEvent(
    float time,
    Vector3 ballPos,
    PlayerAI tackler,
    PlayerAI ballHolder,
    bool userTackled = false)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PETackleEvent(time, ballPos, tackler, ballHolder, userTackled));
  }

  public static void RecordBallKickedEvent(
    float time,
    Vector3 ballPos,
    PlayerAI kicker,
    PlayerAI center = null,
    PlayerAI holder = null)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEBallKickedEvent(time, ballPos, kicker, center, holder));
  }

  public static void RecordPlayOverEvent(
    float time,
    Vector3 ballPos,
    PlayEndType type,
    PlayerAI ballHolder)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEPlayOverEvent(time, ballPos, type, ballHolder));
    PEGameplayEventManager._driveEvents.Add(PEGameplayEventManager._playEvents);
    PEGameplayEventManager._playEvents = new List<PEGameplayEvent>();
  }

  public static void RecordBallSnappedEvent(float time, Vector3 ballPos, PlayerAI qb) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEBallHikedEvent(time, ballPos, qb));

  public static void RecordBallHandoffEvent(
    float time,
    Vector3 ballPos,
    PlayerAI receiver,
    bool fake = false)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEBallHandoffEvent(time, ballPos, receiver, fake));
  }

  public static void RecordHandoffAbortedEvent(
    float time,
    Vector3 ballPos,
    PlayerAI giver,
    PlayerAI receiver)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEHandoffAbortedEvent(time, ballPos, giver, receiver));
  }

  public static void RecordHandoffTimeReachedEvent(float time, Vector3 ballPos) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEHandoffTimeReachedEvent(time, ballPos));

  public static void RecordQBRunningEvent(float time, Vector3 ballPos, PlayerAI qb) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEQBRunningEvent(time, ballPos, qb));

  public static void RecordPenaltyEvent(float time, Vector3 ballPos, PenaltyType type)
  {
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEPenaltyEvent(time, ballPos, type));
    Debug.Log((object) string.Format(" Recording Penalty Event -- type = {0}, time={1}, ballPos={2} ", (object) Enum.GetName(typeof (PenaltyType), (object) type), (object) time, (object) ballPos));
  }

  public static void RecordAboutToCallOffPlayEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEAboutToCallOffPlayEvent());

  public static void RecordPlaySelectedEvent(
    float time,
    Vector3 ballPos,
    int quarter,
    bool homeOnOffense,
    bool offenseGoingNorth,
    PlayDataOff offData,
    PlayDataDef defData,
    int down,
    float firstDownZ,
    float ballPosZ)
  {
    int distance = Mathf.CeilToInt((float) Field.ConvertDistanceToYards(Mathf.Abs(firstDownZ - ballPosZ)));
    PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEPlaySelectedEvent(time, ballPos, quarter, homeOnOffense, offenseGoingNorth, offData, defData, down, distance));
  }

  public static void RecordQuarterEndEvent(float time, Vector3 ballPos, int quarter) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEQuarterEndEvent(time, ballPos, quarter));

  public static void RecordShowPlaybookEvent(float time, Vector3 ballPos) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEShowPlaybookEvent(time, ballPos));

  public static void RecordTwoMinWarningEvent(float time, Vector3 ballPos) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PETwoMinWarningEvent(time, ballPos));

  public static void RecordKickReturnEvent(float time, Vector3 ballPos, PlayerAI receiver) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEKickReturnEvent(time, ballPos, receiver));

  public static void RecordPuntBlockedEvent(float time, Vector3 ballPos) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEPuntBlockedEvent(time, ballPos));

  public static void RecordAudibleEvent(float time, Vector3 ballPos, PlayDataOff newPlay) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEAudibleCalledEvent(time, ballPos, newPlay));

  public static void RecordPlayCallMadeEvent(float time, Vector3 ballPos) => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEPlayCallMadeEvent(time, ballPos));

  public static void RecordUserHitGroundEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEUserHitGroundEvent());

  public static void RecordTimeoutEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PETimeoutEvent());

  public static void RecordHikeCompleteEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEHikeCompleteEvent());

  public static void RecordGameOverEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEGameOverEvent());

  public static void RecordReadyToHikeEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEReadyToHikeEvent());

  public static void RecordHomeTeamTurnover() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEHomeTeamTurnover());

  public static void RecordMovePlayersToHuddleEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEMovePlayersToHuddleEvent());

  public static void RecordBreakHuddleEvent() => PEGameplayEventManager.RecordEvent((PEGameplayEvent) new PEBreakHuddleEvent());

  private static void RecordEvent(PEGameplayEvent ge)
  {
    PEGameplayEventManager._gameEvents.Add(ge);
    PEGameplayEventManager._playEvents.Add(ge);
    Action<PEGameplayEvent> onEventOccurred = PEGameplayEventManager.OnEventOccurred;
    if (onEventOccurred == null)
      return;
    onEventOccurred(ge);
  }
}
