// Decompiled with JetBrains decompiler
// Type: PBC.SetAnimFollow_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class SetAnimFollow_PBC : StateMachineBehaviour
  {
    private AnimFollow_PBC animfollow;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float forceTune = 1f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float accBias = 1f;
    private float storForceTune;
    private float storeAccBias;

    public override void OnStateEnter(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.animfollow)
      {
        if (!(bool) (Object) (this.animfollow = animator.transform.GetComponent<AnimFollow_PBC>()))
          return;
        this.storForceTune = this.animfollow.poseStiffness;
        this.storeAccBias = this.animfollow.accStiffness;
        this.animfollow.poseStiffness = this.forceTune;
        this.animfollow.accStiffness = this.accBias;
      }
      else
      {
        this.storForceTune = this.animfollow.poseStiffness;
        this.storeAccBias = this.animfollow.accStiffness;
        this.animfollow.poseStiffness = this.forceTune;
        this.animfollow.accStiffness = this.accBias;
      }
    }

    public override void OnStateExit(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.animfollow)
        return;
      this.animfollow.poseStiffness = this.storForceTune;
      this.animfollow.accStiffness = this.storeAccBias;
    }
  }
}
