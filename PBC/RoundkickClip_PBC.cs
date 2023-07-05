// Decompiled with JetBrains decompiler
// Type: PBC.RoundkickClip_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class RoundkickClip_PBC : StateMachineBehaviour
  {
    private GetWASD_PBC getWASD;
    private AnimFollow_PBC animFollow;
    private HashIDs_PBC hashIDs;
    [SerializeField]
    private float spinStiffness = 0.85f;
    [SerializeField]
    private bool useRootRotation;
    [SerializeField]
    private bool useMouse;
    private float storeSpinStiffness;
    private bool storeUseRootRotation;
    private bool storeUseMouse;

    public override void OnStateEnter(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.getWASD)
      {
        this.hashIDs = animator.transform.GetComponent<HashIDs_PBC>();
        this.animFollow = animator.transform.GetComponent<AnimFollow_PBC>();
        if (!(bool) (Object) (this.getWASD = animator.transform.GetComponent<GetWASD_PBC>()))
          return;
        this.storeSpinStiffness = this.animFollow.spinStiffness;
        this.storeUseRootRotation = this.getWASD.useRootRotation;
        this.storeUseMouse = this.getWASD.useMouse;
        this.animFollow.spinStiffness = this.spinStiffness;
        this.getWASD.useRootRotation = this.useRootRotation;
        this.getWASD.useMouse = this.useMouse;
      }
      else
      {
        this.storeSpinStiffness = this.animFollow.spinStiffness;
        this.storeUseRootRotation = this.getWASD.useRootRotation;
        this.storeUseMouse = this.getWASD.useMouse;
        this.animFollow.spinStiffness = this.spinStiffness;
        this.getWASD.useRootRotation = this.useRootRotation;
        this.getWASD.useMouse = this.useMouse;
      }
    }

    public override void OnStateExit(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.getWASD)
        return;
      this.animFollow.spinStiffness = this.storeSpinStiffness;
      this.getWASD.useRootRotation = this.storeUseRootRotation;
      this.getWASD.useMouse = this.storeUseMouse;
      if (!(bool) (Object) this.hashIDs)
        return;
      animator.SetBool(this.hashIDs.roundKickBool, false);
    }
  }
}
