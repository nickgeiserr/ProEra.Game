// Decompiled with JetBrains decompiler
// Type: MoveToLinePosition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToLinePosition : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedVector3 targetPosition;
  public SharedBool atTargetPos;
  private PlayerAI ownerPlayerAI;
  private Color distColor = Color.white;
  private static float STOPPING_DIST = 0.5f;
  private static float SWITCH_TO_WALK_DIST = 3f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    MoveToLinePosition.STOPPING_DIST = this.ownerPlayerAI.animatorCommunicator.GoalReachRange;
    this.atTargetPos.Value = false;
    this.targetPosition = (SharedVector3) this.ownerPlayerAI.GetPlayStartPosition();
    this.ownerPlayerAI.animatorCommunicator.SetGoal(this.targetPosition.Value, 0.65f, this.ownerPlayerAI.StanceTargetRotation());
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI || this.ownerPlayerAI.hasShiftedStartingPosition)
      return TaskStatus.Failure;
    double num = (double) Vector3.Distance(this.ownerPlayerAI.trans.position, this.targetPosition.Value);
    if (num < (double) MoveToLinePosition.SWITCH_TO_WALK_DIST)
      this.ownerPlayerAI.animatorCommunicator.SetGoal(this.targetPosition.Value, 0.3f, this.ownerPlayerAI.StanceTargetRotation());
    if (num < (double) MoveToLinePosition.STOPPING_DIST)
    {
      this.atTargetPos.Value = true;
      return TaskStatus.Success;
    }
    Debug.DrawLine(this.ownerPlayerAI.trans.position, this.targetPosition.Value, this.distColor);
    return TaskStatus.Running;
  }

  public override void OnEnd() => base.OnEnd();
}
