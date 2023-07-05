// Decompiled with JetBrains decompiler
// Type: StateLookAtOpposingSide
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class StateLookAtOpposingSide : BaseStateMachineBehaviour
{
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    this.UpdateGazeTargetPosition();
    this.gazeController.SetTarget(this.GazeTarget);
  }

  public override void OnStateUpdate(
    Animator animator,
    AnimatorStateInfo stateInfo,
    int layerIndex)
  {
    base.OnStateUpdate(animator, stateInfo, layerIndex);
    this.UpdateGazeTargetPosition();
  }

  private void UpdateGazeTargetPosition()
  {
    float absoluteYardlineNumber = this.gameboard.context.absoluteYardlineNumber;
    Vector3 localPosition = this._player.transform.localPosition;
    Vector3 vector3 = new Vector3(absoluteYardlineNumber.YardsToMeters() - localPosition.x, 0.0f);
    this.GazeTarget.position = localPosition + vector3.normalized * 10f + new Vector3(0.0f, 1.7f, 0.0f);
  }
}
