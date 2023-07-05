// Decompiled with JetBrains decompiler
// Type: HashIDs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class HashIDs : MonoBehaviour
{
  [HideInInspector]
  public int locomotionState;
  [HideInInspector]
  public int qbHasBallIdleState;
  [HideInInspector]
  public int qbThrowState;
  [HideInInspector]
  public int idleState;
  [HideInInspector]
  public int pivotLeft180State;
  [HideInInspector]
  public int pivotRight180State;
  [HideInInspector]
  public int pivotLeft135State;
  [HideInInspector]
  public int pivotRight135State;
  [HideInInspector]
  public int pivotLeft90State;
  [HideInInspector]
  public int pivotRight90State;
  [HideInInspector]
  public int pivotLeft45State;
  [HideInInspector]
  public int pivotRight45State;
  [HideInInspector]
  public int defenderStrafeState;
  [HideInInspector]
  public int idleToRunLeftState;
  [HideInInspector]
  public int idleToRunRightState;
  [HideInInspector]
  public int idleToRunBackState;
  [HideInInspector]
  public int qbPivotLeftState;
  [HideInInspector]
  public int qbPivotRightState;
  [HideInInspector]
  public int qbUnderCenterState;
  [HideInInspector]
  public int qbShotgunIdleState;
  [HideInInspector]
  public int inactivePlayLocomotion;
  [HideInInspector]
  public int passBlockLoop_LT;
  [HideInInspector]
  public int passBlockLoop_LG;
  [HideInInspector]
  public int passBlockLoop_RG;
  [HideInInspector]
  public int passBlockLoop_RT;
  [HideInInspector]
  public int speedFloat;
  [HideInInspector]
  public int directionFloat;
  [HideInInspector]
  public int angleFloat;
  [HideInInspector]
  public int colliderHeightFloat;
  [HideInInspector]
  public int movementTypeFloat;
  [HideInInspector]
  public int rightMovementFloat;
  [HideInInspector]
  public int forwardMovementFloat;
  [HideInInspector]
  public int stanceInt;
  [HideInInspector]
  public int catchTypeInt;
  [HideInInspector]
  public int playTypeInt;
  [HideInInspector]
  public int playTypeSpecificInt;
  [HideInInspector]
  public int shiftInt;
  [HideInInspector]
  public int tackleTypeInt;
  [HideInInspector]
  public int defenseTypeInt;
  [HideInInspector]
  public int runnerActionInt;
  [HideInInspector]
  public int blockTypeInt;
  [HideInInspector]
  public int audibleInt;
  [HideInInspector]
  public int ballStateInt;
  [HideInInspector]
  public int cheerActionInt;
  [HideInInspector]
  public int indexInFormation;
  [HideInInspector]
  public int handoffDirection;
  [HideInInspector]
  public int dropbackTypeInt;
  [HideInInspector]
  public int randomNumberInt;
  [HideInInspector]
  public int playersInHuddleInt;
  [HideInInspector]
  public int celebrationTypeInt;
  [HideInInspector]
  public int playActiveBool;
  [HideInInspector]
  public int hasBallBool;
  [HideInInspector]
  public int isBallThrownBool;
  [HideInInspector]
  public int handBallBool;
  [HideInInspector]
  public int isBlockingBool;
  [HideInInspector]
  public int playOverBool;
  [HideInInspector]
  public int takeHandoffBool;
  [HideInInspector]
  public int isStrafingBool;
  [HideInInspector]
  public int allowPivotBool;
  [HideInInspector]
  public int isSprintingBool;
  [HideInInspector]
  public int onOffenseBool;
  [HideInInspector]
  public int leftSideOfBallBool;
  [HideInInspector]
  public int leftSideOfBlockerBool;
  [HideInInspector]
  public int controlledByUser;
  [HideInInspector]
  public int forceIdleBool;
  [HideInInspector]
  public int atHuddlePositionBool;
  [HideInInspector]
  public int atPlayPositionBool;
  [HideInInspector]
  public int blockShuffleBool;
  [HideInInspector]
  public int allowReactToAudibleBool;
  [HideInInspector]
  public int normal_Trigger;
  [HideInInspector]
  public int highlighted_Trigger;
  [HideInInspector]
  public int selected_Trigger;
  [HideInInspector]
  public int kickReturnTurnToBlockTrigger;
  [HideInInspector]
  public int reactToAudible_Trigger;
  public static HashIDs self;

  private void Awake()
  {
    if ((Object) HashIDs.self == (Object) null)
      HashIDs.self = this;
    this.locomotionState = Animator.StringToHash("Base Layer.Locomotion");
    this.inactivePlayLocomotion = Animator.StringToHash("Base Layer.Inactive Play Locomotion");
    this.qbHasBallIdleState = Animator.StringToHash("Base Layer.QBHasBallIdle");
    this.qbThrowState = Animator.StringToHash("Base Layer.QBThrow");
    this.idleState = Animator.StringToHash("Base Layer.Idle");
    this.defenderStrafeState = Animator.StringToHash("Base Layer.Defender Strafe");
    this.idleToRunLeftState = Animator.StringToHash("Base Layer.Idle To Run Left");
    this.idleToRunRightState = Animator.StringToHash("Base Layer.Idle To Run Right");
    this.pivotLeft180State = Animator.StringToHash("Base Layer.Pivots.Pivot Left 180");
    this.pivotRight180State = Animator.StringToHash("Base Layer.Pivots.Pivot Right 180");
    this.pivotLeft135State = Animator.StringToHash("Base Layer.Pivots.Pivot Left 135");
    this.pivotRight135State = Animator.StringToHash("Base Layer.Pivots.Pivot Right 135");
    this.pivotLeft90State = Animator.StringToHash("Base Layer.Pivots.Pivot Left 90");
    this.pivotRight90State = Animator.StringToHash("Base Layer.Pivots.Pivot Right 90");
    this.pivotLeft45State = Animator.StringToHash("Base Layer.Pivots.Pivot Left 45");
    this.pivotRight45State = Animator.StringToHash("Base Layer.Pivots.Pivot Right 45");
    this.qbUnderCenterState = Animator.StringToHash("Base Layer.Play Stances.QB Under Center Idle");
    this.qbShotgunIdleState = Animator.StringToHash("Base Layer.Play Stances.QB Shotgun Idle");
    this.passBlockLoop_LT = Animator.StringToHash("Base Layer.LT Pass Block Loop");
    this.passBlockLoop_LG = Animator.StringToHash("Base Layer.LG Pass Block Loop");
    this.passBlockLoop_RG = Animator.StringToHash("Base Layer.RG Pass Block Loop");
    this.passBlockLoop_RT = Animator.StringToHash("Base Layer.RT Pass Block Loop");
    this.speedFloat = Animator.StringToHash("Speed");
    this.directionFloat = Animator.StringToHash("Direction");
    this.angleFloat = Animator.StringToHash("Angle");
    this.playActiveBool = Animator.StringToHash("PlayActive");
    this.hasBallBool = Animator.StringToHash("HasBall");
    this.isBallThrownBool = Animator.StringToHash("IsBallThrown");
    this.handBallBool = Animator.StringToHash("HandBall");
    this.stanceInt = Animator.StringToHash("Stance");
    this.catchTypeInt = Animator.StringToHash("CatchType");
    this.playTypeInt = Animator.StringToHash("PlayType");
    this.playTypeSpecificInt = Animator.StringToHash("PlayTypeSpecific");
    this.isBlockingBool = Animator.StringToHash("IsBlocking");
    this.shiftInt = Animator.StringToHash("Shift");
    this.tackleTypeInt = Animator.StringToHash("TackleType");
    this.defenseTypeInt = Animator.StringToHash("DefenseType");
    this.playOverBool = Animator.StringToHash("PlayOver");
    this.ballStateInt = Animator.StringToHash("BallState");
    this.runnerActionInt = Animator.StringToHash("RunnerAction");
    this.handoffDirection = Animator.StringToHash("HandoffDirection");
    this.cheerActionInt = Animator.StringToHash("Action");
    this.takeHandoffBool = Animator.StringToHash("TakeHandoff");
    this.blockTypeInt = Animator.StringToHash("BlockType");
    this.indexInFormation = Animator.StringToHash("IndexInFormation");
    this.onOffenseBool = Animator.StringToHash("OnOffense");
    this.leftSideOfBallBool = Animator.StringToHash("LeftSideOfBall");
    this.leftSideOfBlockerBool = Animator.StringToHash("LeftSideOfBlocker");
    this.controlledByUser = Animator.StringToHash("ControlledByUser");
    this.audibleInt = Animator.StringToHash("Audible");
    this.colliderHeightFloat = Animator.StringToHash("ColliderHeight");
    this.rightMovementFloat = Animator.StringToHash("Right");
    this.forwardMovementFloat = Animator.StringToHash("Forward");
    this.movementTypeFloat = Animator.StringToHash("MovementType");
    this.isStrafingBool = Animator.StringToHash("IsStrafing");
    this.allowPivotBool = Animator.StringToHash("AllowPivot");
    this.isSprintingBool = Animator.StringToHash("IsSprinting");
    this.dropbackTypeInt = Animator.StringToHash("DropbackType");
    this.randomNumberInt = Animator.StringToHash("RandomNumber");
    this.forceIdleBool = Animator.StringToHash("ForceIdle");
    this.kickReturnTurnToBlockTrigger = Animator.StringToHash("KickReturnTurnToBlock");
    this.playersInHuddleInt = Animator.StringToHash("PlayersInHuddle");
    this.atHuddlePositionBool = Animator.StringToHash("AtHuddlePosition");
    this.atPlayPositionBool = Animator.StringToHash("AtPlayPosition");
    this.blockShuffleBool = Animator.StringToHash("BlockShuffle");
    this.celebrationTypeInt = Animator.StringToHash("CelebrationType");
    this.allowReactToAudibleBool = Animator.StringToHash("AllowReactToAudible");
    this.normal_Trigger = Animator.StringToHash("Normal");
    this.highlighted_Trigger = Animator.StringToHash("Highlighted");
    this.selected_Trigger = Animator.StringToHash("Selected");
    this.reactToAudible_Trigger = Animator.StringToHash("ReactToAudible");
  }
}
