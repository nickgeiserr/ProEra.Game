// Decompiled with JetBrains decompiler
// Type: PBC.ResizeProfiles_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace PBC
{
  [ExecuteInEditMode]
  public class ResizeProfiles_PBC : MonoBehaviour
  {
    private AnimFollow_PBC animFollow;
    private Transform ragdoll;

    private void Start()
    {
      if ((bool) (UnityEngine.Object) (this.animFollow = this.GetComponent<AnimFollow_PBC>()))
      {
        int newSize = 0;
        if (!(bool) (UnityEngine.Object) (this.ragdoll = this.transform.root.GetComponentInChildren<RagdollControl_PBC>().transform))
        {
          Debug.LogWarning((object) ("ResizeProfiles script on " + this.name + " needs a RAgdollControl script on the ragdoll to locate the ragdoll\n"));
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this);
        }
        else
          newSize = this.ragdoll.GetComponentsInChildren<Rigidbody>().Length;
        int num1 = this.animFollow.jointStrengthProfile.Length;
        Array.Resize<float>(ref this.animFollow.jointStrengthProfile, newSize);
        if (num1 == 0)
        {
          this.animFollow.jointStrengthProfile[0] = 1f;
          num1 = 1;
        }
        for (int index = 1; index <= newSize - num1; ++index)
          this.animFollow.jointStrengthProfile[newSize - index] = this.animFollow.jointStrengthProfile[num1 - 1];
        int num2 = this.animFollow.jointDampingProfile.Length;
        Array.Resize<float>(ref this.animFollow.jointDampingProfile, newSize);
        if (num2 == 0)
        {
          this.animFollow.jointDampingProfile[0] = 1f;
          num2 = 1;
        }
        for (int index = 1; index <= newSize - num2; ++index)
          this.animFollow.jointDampingProfile[newSize - index] = this.animFollow.jointDampingProfile[num2 - 1];
        int num3 = this.animFollow.forceStrengthProfile.Length;
        Array.Resize<float>(ref this.animFollow.forceStrengthProfile, newSize);
        if (num3 == 0)
        {
          this.animFollow.forceStrengthProfile[0] = 1f;
          num3 = 1;
        }
        for (int index = 1; index <= newSize - num3; ++index)
          this.animFollow.forceStrengthProfile[newSize - index] = this.animFollow.forceStrengthProfile[num3 - 1];
      }
      else
        Debug.LogWarning((object) ("There is no AnimFollow script on game object " + this.name + ".\nUnable to resize profiles"));
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this);
    }
  }
}
