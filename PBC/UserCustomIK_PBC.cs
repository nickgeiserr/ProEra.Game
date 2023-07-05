// Decompiled with JetBrains decompiler
// Type: PBC.UserCustomIK_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class UserCustomIK_PBC : MonoBehaviour
  {
    public Transform lookAtTransform;

    public void DoCustomIK()
    {
      if (!(bool) (Object) this.lookAtTransform)
        return;
      Transform masterRigidTransform = this.GetComponent<AnimFollow_PBC>().masterRigidTransforms[9];
      masterRigidTransform.rotation = Quaternion.LookRotation(this.lookAtTransform.position - masterRigidTransform.position, this.transform.up);
      masterRigidTransform.Rotate(180f, 90f, 90f);
    }
  }
}
