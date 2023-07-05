// Decompiled with JetBrains decompiler
// Type: PBC.LimbControl_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace PBC
{
  [ExecuteInEditMode]
  public class LimbControl_PBC : MonoBehaviour
  {
    private AnimFollow_PBC animFollow;
    public Transform[] limbTransforms = new Transform[4];
    [Range(0.0f, 1f)]
    public float jointStrength;
    [Range(0.0f, 1f)]
    public float forceStrength;
    public bool write;
    private int[] limbNumbers = new int[1]{ -1 };

    private void Awake()
    {
      this.animFollow = this.GetComponent<AnimFollow_PBC>();
      Array.Resize<int>(ref this.limbNumbers, this.animFollow.ragdollRigidTransforms.Length);
      int index1 = 0;
      int newSize = 0;
      for (; (bool) (UnityEngine.Object) this.limbTransforms[index1]; ++index1)
      {
        for (int index2 = 0; index2 < this.animFollow.ragdollRigidTransforms.Length; ++index2)
        {
          if ((UnityEngine.Object) this.animFollow.ragdollRigidTransforms[index2] == (UnityEngine.Object) this.limbTransforms[index1] || (UnityEngine.Object) this.animFollow.masterRigidTransforms[index2] == (UnityEngine.Object) this.limbTransforms[index1])
          {
            this.limbNumbers[newSize++] = index2;
            break;
          }
        }
      }
      if (newSize == 0)
        newSize = 1;
      Array.Resize<int>(ref this.limbNumbers, newSize);
    }

    private void Update()
    {
      if (!this.write)
        return;
      this.Write();
    }

    private void Write()
    {
      if (this.limbNumbers[0] == -1)
        this.Awake();
      if (this.limbNumbers[0] == -1)
        return;
      for (int index = 0; index < this.limbNumbers.Length; ++index)
      {
        this.animFollow.jointStrengthProfile[this.limbNumbers[index]] = this.jointStrength;
        this.animFollow.forceStrengthProfile[this.limbNumbers[index]] = this.forceStrength;
      }
      this.animFollow.updateInspectorChanges = true;
      this.write = false;
    }
  }
}
