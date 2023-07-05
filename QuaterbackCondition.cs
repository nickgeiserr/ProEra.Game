// Decompiled with JetBrains decompiler
// Type: QuaterbackCondition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

[CreateAssetMenu(menuName = "NTeract/Slot Conditions/Quaterback Condition", fileName = "QuaterbackCondition")]
public class QuaterbackCondition : SlotCondition
{
  public override bool Evaluate(NteractAgent evaluatedAgent) => (Object) evaluatedAgent.gameObject == (Object) MatchManager.instance.playersManager.GetCurrentQB().gameObject;
}
