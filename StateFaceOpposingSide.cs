// Decompiled with JetBrains decompiler
// Type: StateFaceOpposingSide
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class StateFaceOpposingSide : BaseStateMachineBehaviour
{
  public override void OnStateUpdate(
    Animator animator,
    AnimatorStateInfo stateInfo,
    int layerIndex)
  {
    base.OnStateUpdate(animator, stateInfo, layerIndex);
    float absoluteYardlineNumber = this.gameboard.context.absoluteYardlineNumber;
    Vector3 localPosition = this._player.transform.localPosition;
    this._telegraphy.BodyOrientation = Vector3.SignedAngle(Vector3.forward, new Vector3(absoluteYardlineNumber.YardsToMeters() - localPosition.x, 0.0f), Vector3.up);
  }
}
