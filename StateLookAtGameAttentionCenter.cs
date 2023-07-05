// Decompiled with JetBrains decompiler
// Type: StateLookAtGameAttentionCenter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class StateLookAtGameAttentionCenter : BaseStateMachineBehaviour
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
    Vector3 gameAttentionCenter = this.gameboard.GameAttentionCenter;
    this.GazeTarget.position = new Vector3(gameAttentionCenter.x, Mathf.Clamp(gameAttentionCenter.y, 1.5f, 3f), gameAttentionCenter.z);
  }
}
