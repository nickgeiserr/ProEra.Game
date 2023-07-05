// Decompiled with JetBrains decompiler
// Type: PBC.SetWASD_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class SetWASD_PBC : StateMachineBehaviour
  {
    private GetWASD_PBC getWASD;
    [SerializeField]
    private bool useRootRotation = true;
    [SerializeField]
    private bool useMouse;
    private bool storeUseRootRotation;
    private bool storeUseMouse;

    public override void OnStateEnter(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.getWASD)
      {
        if (!(bool) (Object) (this.getWASD = animator.transform.GetComponent<GetWASD_PBC>()))
          return;
        this.storeUseRootRotation = this.getWASD.useRootRotation;
        this.storeUseMouse = this.getWASD.useMouse;
        this.getWASD.useRootRotation = this.useRootRotation;
        this.getWASD.useMouse = this.useMouse;
      }
      else
      {
        this.storeUseRootRotation = this.getWASD.useRootRotation;
        this.storeUseMouse = this.getWASD.useMouse;
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
      this.getWASD.useRootRotation = this.storeUseRootRotation;
      this.getWASD.useMouse = this.storeUseMouse;
    }
  }
}
