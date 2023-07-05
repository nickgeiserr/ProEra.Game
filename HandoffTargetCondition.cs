// Decompiled with JetBrains decompiler
// Type: HandoffTargetCondition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

[CreateAssetMenu(menuName = "NTeract/Slot Conditions/Handoff Target Condition", fileName = "HandoffTargetCondition")]
public class HandoffTargetCondition : SlotCondition
{
  public override bool Evaluate(NteractAgent evaluatedAgent) => (Object) evaluatedAgent.GetComponent<PlayerAI>() == (Object) MatchManager.instance.playManager.handOffTarget;
}
