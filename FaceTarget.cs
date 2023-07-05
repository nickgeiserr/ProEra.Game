// Decompiled with JetBrains decompiler
// Type: FaceTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FaceTarget : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedVector3 targetPosition;
  public SharedBool atTargetPos;
  private PlayerAI ownerPlayerAI;
  private static float STOP_ANGLE_THRESHOLD = 2f;
  private static float MIN_TURN_SPEED = 0.5f;
  private static float MIN_FACETARGET_DIST = 1f;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    this.atTargetPos.Value = false;
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI)
      return TaskStatus.Failure;
    if (this.atTargetPos.Value)
      return TaskStatus.Success;
    this.targetPosition = (SharedVector3) this.ownerPlayerAI.animatorCommunicator.CurrentGoal.position;
    Vector3 position = new Vector3(this.targetPosition.Value.x, 0.0f, this.targetPosition.Value.z);
    Vector3 lookDir = (position - this.ownerPlayerAI.trans.position) with
    {
      y = 0.0f
    };
    if ((double) lookDir.magnitude < (double) FaceTarget.MIN_FACETARGET_DIST)
      return TaskStatus.Success;
    lookDir.Normalize();
    if (Game.IsPlayActive && !this.ownerPlayerAI.IsGoingForThrownBall)
      this.ownerPlayerAI.isLocalAvoiding = this.ownerPlayerAI.PULocalAvoidance(ref lookDir);
    Quaternion quaternion = Quaternion.LookRotation(lookDir);
    if ((double) Quaternion.Angle(quaternion, this.ownerPlayerAI.trans.rotation) < (double) FaceTarget.STOP_ANGLE_THRESHOLD)
      return TaskStatus.Success;
    this.ownerPlayerAI.animatorCommunicator.SetGoal(position, quaternion);
    return TaskStatus.Running;
  }
}
