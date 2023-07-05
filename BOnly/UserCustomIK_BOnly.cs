// Decompiled with JetBrains decompiler
// Type: BOnly.UserCustomIK_BOnly
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using RootMotion.FinalIK;
using UnityEngine;

namespace BOnly
{
  public class UserCustomIK_BOnly : MonoBehaviour
  {
    public Transform lookAtTransform;
    public BipedIK bipedIK;
    private RotationLimitSpline[] shoulderLimits;

    private void Start()
    {
      if (!(bool) (Object) this.bipedIK)
        return;
      this.bipedIK.Disable();
      this.shoulderLimits = this.GetComponentsInChildren<RotationLimitSpline>();
      foreach (RotationLimit shoulderLimit in this.shoulderLimits)
        shoulderLimit.Disable();
    }

    public void DoCustomIK()
    {
      if (!(bool) (Object) this.bipedIK)
        return;
      this.bipedIK.UpdateBipedIK();
      foreach (RotationLimit shoulderLimit in this.shoulderLimits)
        shoulderLimit.Apply();
    }
  }
}
