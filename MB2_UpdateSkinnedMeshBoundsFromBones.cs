// Decompiled with JetBrains decompiler
// Type: MB2_UpdateSkinnedMeshBoundsFromBones
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using UnityEngine;

public class MB2_UpdateSkinnedMeshBoundsFromBones : MonoBehaviour
{
  private SkinnedMeshRenderer smr;
  private Transform[] bones;

  private void Start()
  {
    this.smr = this.GetComponent<SkinnedMeshRenderer>();
    if ((Object) this.smr == (Object) null)
    {
      Debug.LogError((object) "Need to attach MB2_UpdateSkinnedMeshBoundsFromBones script to an object with a SkinnedMeshRenderer component attached.");
    }
    else
    {
      this.bones = this.smr.bones;
      bool updateWhenOffscreen = this.smr.updateWhenOffscreen;
      this.smr.updateWhenOffscreen = true;
      this.smr.updateWhenOffscreen = updateWhenOffscreen;
    }
  }

  private void Update()
  {
    if (!((Object) this.smr != (Object) null))
      return;
    MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(this.bones, this.smr);
  }
}
