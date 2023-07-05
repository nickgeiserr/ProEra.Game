// Decompiled with JetBrains decompiler
// Type: StayInStance
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class StayInStance : BehaviorDesigner.Runtime.Tasks.Action
{
  public SharedGameObject ownerPlayer;
  public SharedVector3 targetPosition;
  private PlayerAI ownerPlayerAI;

  public override void OnStart()
  {
    if (!(bool) (Object) this.ownerPlayer.Value)
      return;
    this.ownerPlayerAI = this.ownerPlayer.Value.GetComponent<PlayerAI>();
  }

  public override TaskStatus OnUpdate()
  {
    if (!(bool) (Object) this.ownerPlayerAI)
      return TaskStatus.Failure;
    this.ownerPlayerAI.animatorCommunicator.SetGoal(this.ownerPlayerAI.animatorCommunicator.CurrentGoal.position, 0.0f, this.ownerPlayerAI.animatorCommunicator.CurrentGoal.rotation);
    this.ownerPlayerAI.animatorCommunicator.SetStance(this.ownerPlayerAI, this.ownerPlayerAI.savedStance);
    return TaskStatus.Running;
  }
}
