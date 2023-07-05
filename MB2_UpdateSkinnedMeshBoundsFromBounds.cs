// Decompiled with JetBrains decompiler
// Type: MB2_UpdateSkinnedMeshBoundsFromBounds
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DigitalOpus.MB.Core;
using System.Collections.Generic;
using UnityEngine;

public class MB2_UpdateSkinnedMeshBoundsFromBounds : MonoBehaviour
{
  public List<GameObject> objects;
  private SkinnedMeshRenderer smr;

  private void Start()
  {
    this.smr = this.GetComponent<SkinnedMeshRenderer>();
    if ((Object) this.smr == (Object) null)
      Debug.LogError((object) "Need to attach MB2_UpdateSkinnedMeshBoundsFromBounds script to an object with a SkinnedMeshRenderer component attached.");
    else if (this.objects == null || this.objects.Count == 0)
    {
      Debug.LogWarning((object) "The MB2_UpdateSkinnedMeshBoundsFromBounds had no Game Objects. It should have the same list of game objects that the MeshBaker does.");
      this.smr = (SkinnedMeshRenderer) null;
    }
    else
    {
      for (int index = 0; index < this.objects.Count; ++index)
      {
        if ((Object) this.objects[index] == (Object) null || (Object) this.objects[index].GetComponent<Renderer>() == (Object) null)
        {
          Debug.LogError((object) ("The list of objects had nulls or game objects without a renderer attached at position " + index.ToString()));
          this.smr = (SkinnedMeshRenderer) null;
          return;
        }
      }
      bool updateWhenOffscreen = this.smr.updateWhenOffscreen;
      this.smr.updateWhenOffscreen = true;
      this.smr.updateWhenOffscreen = updateWhenOffscreen;
    }
  }

  private void Update()
  {
    if (!((Object) this.smr != (Object) null) || this.objects == null)
      return;
    MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(this.objects, this.smr);
  }
}
