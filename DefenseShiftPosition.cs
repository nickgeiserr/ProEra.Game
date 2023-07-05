// Decompiled with JetBrains decompiler
// Type: DefenseShiftPosition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class DefenseShiftPosition : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedVector3 initPosition;
  public SharedVector3 shiftPosition;
  public SharedBool startShift;
  public SharedBool hasShifted;
  public SharedInt playerTeamPosition;
  private PlayerAI ownerPlayerAI;
  private Vector3 currentMovementTarget;
  private bool reachedShiftTarget;

  private event System.Action OnTargetReached;

  public override void OnStart()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.OnTargetReached += new System.Action(this.OnReachedTargetPos);
    this.initPosition = (SharedVector3) this.ownerPlayerAI.trans.position;
    this.InitShift();
  }

  private void InitShift()
  {
    this.currentMovementTarget = (Vector3) this.shiftPosition.GetValue();
    this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.DefaultStrafe);
    this.ownerPlayerAI.hasShiftedStartingPosition = true;
    this.hasShifted.Value = true;
    this.ownerPlayerAI.animatorCommunicator.SetGoal(this.currentMovementTarget, 0.3f, Field.DEFENSE_TOWARDS_LOS_QUATERNION, this.OnTargetReached);
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (UnityEngine.Object) this.ownerPlayerAI || !this.startShift.Value || this.ownerPlayerAI.onOffense || Game.IsPlayActive)
      return TaskStatus.Failure;
    if (!this.reachedShiftTarget)
      return TaskStatus.Running;
    this.ownerPlayerAI.hasShiftedStartingPosition = true;
    this.hasShifted.Value = true;
    return TaskStatus.Success;
  }

  private void OnReachedTargetPos() => this.reachedShiftTarget = true;

  private void CleanupTaskState()
  {
    if ((UnityEngine.Object) this.ownerPlayerAI != (UnityEngine.Object) null && (UnityEngine.Object) this.ownerPlayerAI.animatorCommunicator != (UnityEngine.Object) null)
      this.ownerPlayerAI.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
    this.startShift.Value = false;
    this.OnTargetReached -= new System.Action(this.OnReachedTargetPos);
  }

  public override void OnEnd()
  {
    base.OnEnd();
    this.CleanupTaskState();
  }

  public override void OnBehaviorComplete()
  {
    base.OnBehaviorComplete();
    this.CleanupTaskState();
  }

  public override void OnDrawGizmos()
  {
  }
}
