// Decompiled with JetBrains decompiler
// Type: GetIntoStance
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using ProEra.Game;
using UnityEngine;

public class GetIntoStance : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedVector3 targetPosition;
  public SharedBool GetToFormPos;
  private PlayerAI ownerPlayerAI;
  private Vector3 stancePosition;
  private static float STOP_ANGLE_THRESHOLD = 180f;
  private static float STOP_DIST_THRESHOLD = 1f;
  private Quaternion targetRotation;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
    GetIntoStance.STOP_DIST_THRESHOLD = this.ownerPlayerAI.animatorCommunicator.GoalReachRange;
    this.targetRotation = !this.ownerPlayerAI.onOffense ? Field.DEFENSE_TOWARDS_LOS_QUATERNION : Field.OFFENSE_TOWARDS_LOS_QUATERNION;
    if (this.ownerPlayerAI.onOffense)
    {
      if (global::Game.IsFG)
      {
        if (this.ownerPlayerAI.indexInFormation == 5)
          this.targetRotation = Quaternion.Euler(new Vector3(0.0f, (float) (45.0 + -90.0 * (double) global::Game.OffensiveFieldDirection), 0.0f));
        else if (this.ownerPlayerAI.indexInFormation == 6)
          this.targetRotation = Quaternion.Euler(new Vector3(0.0f, (float) (90.0 * (double) global::Game.OffensiveFieldDirection - 45.0), 0.0f));
      }
      else if (PlayState.IsKickoff && this.ownerPlayerAI.indexInFormation == 6)
        this.targetRotation = Quaternion.Euler(new Vector3(0.0f, (float) (90.0 * (double) global::Game.OffensiveFieldDirection - 45.0), 0.0f));
    }
    this.targetPosition = (SharedVector3) this.ownerPlayerAI.GetPlayStartPosition();
    this.ownerPlayerAI.animatorCommunicator.SetGoal(this.targetPosition.Value, this.ownerPlayerAI.StanceTargetRotation());
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI)
      return TaskStatus.Failure;
    float num = Vector3.Distance(this.ownerPlayerAI.trans.position, this.ownerPlayerAI.animatorCommunicator.CurrentGoal.position);
    if ((double) Quaternion.Angle(this.ownerPlayerAI.trans.rotation, this.targetRotation) >= (double) GetIntoStance.STOP_ANGLE_THRESHOLD || (double) num >= (double) GetIntoStance.STOP_DIST_THRESHOLD)
      return TaskStatus.Running;
    this.GetToFormPos.Value = false;
    if (!this.ownerPlayerAI.hasShiftedStartingPosition)
      this.ownerPlayerAI.PIPutPlayerInPlayPosition();
    return TaskStatus.Success;
  }
}
