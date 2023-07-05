// Decompiled with JetBrains decompiler
// Type: PBC.RoundkickClip_AF2
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class RoundkickClip_AF2 : StateMachineBehaviour
  {
    private AnimFollow_PBC animFollow;
    [SerializeField]
    private float spinStiffness = 0.85f;
    private float storeSpinStiffness;

    public override void OnStateEnter(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.animFollow)
      {
        if (!(bool) (Object) (this.animFollow = animator.transform.GetComponent<AnimFollow_PBC>()))
          return;
        this.storeSpinStiffness = this.animFollow.spinStiffness;
        this.animFollow.spinStiffness = this.spinStiffness;
      }
      else
      {
        this.storeSpinStiffness = this.animFollow.spinStiffness;
        this.animFollow.spinStiffness = this.spinStiffness;
      }
    }

    public override void OnStateExit(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if ((bool) (Object) this.animFollow)
        this.animFollow.spinStiffness = this.storeSpinStiffness;
      animator.SetBool("RoundKickBool", false);
    }
  }
}
