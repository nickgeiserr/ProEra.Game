// Decompiled with JetBrains decompiler
// Type: BallCarrierOrHandoffReceiverCondition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

[CreateAssetMenu(menuName = "NTeract/Slot Conditions/Ball Carrier Or Handoff Receiver Condition", fileName = "BallCarrierOrHandoffReceiverCondition")]
public class BallCarrierOrHandoffReceiverCondition : SlotCondition
{
  public override bool Evaluate(NteractAgent evaluatedAgent)
  {
    if ((Object) evaluatedAgent.gameObject == (Object) MatchManager.instance.playersManager.ballHolder)
      return true;
    GameObject gameObject = (Object) Game.HandoffTarget != (Object) null ? Game.HandoffTarget.gameObject : (GameObject) null;
    return (Object) evaluatedAgent.gameObject == (Object) gameObject;
  }
}
