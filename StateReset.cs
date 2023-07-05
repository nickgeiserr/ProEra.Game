// Decompiled with JetBrains decompiler
// Type: StateReset
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class StateReset : BaseStateMachineBehaviour
{
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    if (!this.MxmAnimator.IsInited())
      return;
    this.MxmAnimator.ClearAllTags();
    this.MxmAnimator.ResetMotion();
    this.MxmAnimator.ResetEventData();
  }
}
