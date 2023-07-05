// Decompiled with JetBrains decompiler
// Type: PlayInitiationAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;

public class PlayInitiationAbility : MonoBehaviour
{
  public HandoffDictionary handoffDictionary;
  public HandoffDictionary leftHandedQbHandoffDictionary;
  public SnapDictionary snapDictionary;
  public SnapDictionary leftHandedQbSnapDictionary;
  public InteractionController kickoffInteraction;
  public InteractionController puntInteraction;
  public InteractionController leftLeggedPuntInteraction;
  public AnimationEventController kickoffEventController;
  public AnimationEventController onsideKickoffEventController;

  public bool LookForHandoffOpportunities(PlayersManager playersManager, PlayerAI qbPlayerAI)
  {
    PlayerAI handOffTarget = MatchManager.instance.playManager.handOffTarget;
    PlayerAI ballSnapper = MatchManager.instance.playManager.ballSnapper;
    if (qbPlayerAI.nteractAgent.IsInsideInteraction && !qbPlayerAI.nteractAgent.CanBeInterupted || ballSnapper.nteractAgent.IsInsideInteraction && !ballSnapper.nteractAgent.CanBeInterupted || handOffTarget.nteractAgent.IsInsideInteraction && !handOffTarget.nteractAgent.CanBeInterupted || qbPlayerAI.eventAgent.IsInsideEvent && !qbPlayerAI.eventAgent.CanBeInterupted || ballSnapper.eventAgent.IsInsideEvent && !ballSnapper.eventAgent.CanBeInterupted || handOffTarget.eventAgent.IsInsideEvent && !handOffTarget.eventAgent.CanBeInterupted)
      return false;
    int handoffType = (int) MatchManager.instance.playManager.savedOffPlay.GetHandoffType();
    InteractionController controller = (qbPlayerAI.LeftHanded ? this.leftHandedQbHandoffDictionary : this.handoffDictionary).GetController(handoffType);
    SlottingResult slotAssignment = new SlottingResult();
    slotAssignment.Add(qbPlayerAI.nteractAgent, controller.participants[0].slot.index);
    slotAssignment.Add(ballSnapper.nteractAgent, controller.participants[1].slot.index);
    slotAssignment.Add(handOffTarget.nteractAgent, controller.participants[2].slot.index);
    Debug.Log((object) (((object) controller)?.ToString() + " was selected for play initiation"));
    InteractionController.StartPlaybackAndAlignment(controller, 0, slotAssignment, (InteractionGroup) null);
    return false;
  }

  public bool LookForSnapOpportunities(PlayersManager playersManager, PlayerAI qbPlayerAI)
  {
    PlayerAI ballSnapper = MatchManager.instance.playManager.ballSnapper;
    if (qbPlayerAI.nteractAgent.IsInsideInteraction && !qbPlayerAI.nteractAgent.CanBeInterupted || ballSnapper.nteractAgent.IsInsideInteraction && !ballSnapper.nteractAgent.CanBeInterupted || qbPlayerAI.eventAgent.IsInsideEvent && !qbPlayerAI.eventAgent.CanBeInterupted || ballSnapper.eventAgent.IsInsideEvent && !ballSnapper.eventAgent.CanBeInterupted)
      return false;
    DropbackType dropbackType = MatchManager.instance.playManager.savedOffPlay.GetDropbackType();
    float centerDistance = Vector3.Distance(ballSnapper.transform.position, this.transform.position);
    InteractionController controller = (qbPlayerAI.LeftHanded ? this.leftHandedQbSnapDictionary : this.snapDictionary).GetController(dropbackType, centerDistance);
    SlottingResult slotAssignment = new SlottingResult();
    slotAssignment.Add(qbPlayerAI.nteractAgent, controller.participants[0].slot.index);
    slotAssignment.Add(ballSnapper.nteractAgent, controller.participants[1].slot.index);
    Debug.Log((object) (((object) controller)?.ToString() + " was selected for play initiation"));
    InteractionController.StartPlaybackAndAlignment(controller, 0, slotAssignment, (InteractionGroup) null);
    return false;
  }

  public bool LookForFieldGoalOpportunities(PlayersManager playersManager, PlayerAI kickerPlayerAI)
  {
    PlayerAI fieldGoalKicker = MatchManager.instance.playManager.fieldGoalKicker;
    PlayerAI ballSnapper = MatchManager.instance.playManager.ballSnapper;
    if (kickerPlayerAI.nteractAgent.IsInsideInteraction && !kickerPlayerAI.nteractAgent.CanBeInterupted || ballSnapper.nteractAgent.IsInsideInteraction && !ballSnapper.nteractAgent.CanBeInterupted || fieldGoalKicker.nteractAgent.IsInsideInteraction && !fieldGoalKicker.nteractAgent.CanBeInterupted || kickerPlayerAI.eventAgent.IsInsideEvent && !kickerPlayerAI.eventAgent.CanBeInterupted || ballSnapper.eventAgent.IsInsideEvent && !ballSnapper.eventAgent.CanBeInterupted || fieldGoalKicker.eventAgent.IsInsideEvent && !fieldGoalKicker.eventAgent.CanBeInterupted)
      return false;
    SlottingResult slotAssignment = new SlottingResult();
    slotAssignment.Add(kickerPlayerAI.nteractAgent, this.kickoffInteraction.participants[0].slot.index);
    slotAssignment.Add(ballSnapper.nteractAgent, this.kickoffInteraction.participants[1].slot.index);
    slotAssignment.Add(fieldGoalKicker.nteractAgent, this.kickoffInteraction.participants[2].slot.index);
    Debug.Log((object) (((object) this.kickoffInteraction)?.ToString() + " was selected for play initiation"));
    InteractionController.StartPlaybackAndAlignment(this.kickoffInteraction, 0, slotAssignment, (InteractionGroup) null);
    return false;
  }

  public bool LookForPuntOpportunities(PlayersManager playersManager, PlayerAI punterPlayerAI)
  {
    PlayerAI ballSnapper = MatchManager.instance.playManager.ballSnapper;
    if (punterPlayerAI.nteractAgent.IsInsideInteraction && !punterPlayerAI.nteractAgent.CanBeInterupted || ballSnapper.nteractAgent.IsInsideInteraction && !ballSnapper.nteractAgent.CanBeInterupted || punterPlayerAI.eventAgent.IsInsideEvent && !punterPlayerAI.eventAgent.CanBeInterupted || ballSnapper.eventAgent.IsInsideEvent && !ballSnapper.eventAgent.CanBeInterupted)
      return false;
    InteractionController controller = punterPlayerAI.LeftHanded ? this.leftLeggedPuntInteraction : this.puntInteraction;
    SlottingResult slotAssignment = new SlottingResult();
    slotAssignment.Add(punterPlayerAI.nteractAgent, controller.participants[0].slot.index);
    slotAssignment.Add(ballSnapper.nteractAgent, controller.participants[1].slot.index);
    Debug.Log((object) (((object) controller)?.ToString() + " was selected for play initiation"));
    InteractionController.StartPlaybackAndAlignment(controller, 0, slotAssignment, (InteractionGroup) null);
    return false;
  }

  private AnimationEventController GetKickoffController(bool isOnsideKick) => !isOnsideKick ? this.kickoffEventController : this.onsideKickoffEventController;

  public void StartKickoff(PlayerAI kickerPlayerAI, bool isOnsideKick)
  {
    if (kickerPlayerAI.eventAgent.IsInsideEvent && !kickerPlayerAI.eventAgent.CanBeInterupted || kickerPlayerAI.nteractAgent.IsInsideInteraction && !kickerPlayerAI.nteractAgent.CanBeInterupted)
      return;
    AffineTransform masterAT = new AffineTransform(SingletonBehaviour<BallManager, MonoBehaviour>.instance.trans.position, Field.OFFENSE_TOWARDS_LOS_QUATERNION);
    AnimationEventController kickoffController = this.GetKickoffController(isOnsideKick);
    kickerPlayerAI.eventAgent.EnterEvent(masterAT, kickoffController);
  }

  public AffineTransform GetIdealTransformFromBallToKicker(bool isOnsideKick)
  {
    AnimationEventController kickoffController = this.GetKickoffController(isOnsideKick);
    return new AffineTransform(kickoffController.keyEvent.agentRelativeLocation, Quaternion.Euler(0.0f, kickoffController.keyEvent.agentRelativeRotationY, 0.0f));
  }
}
