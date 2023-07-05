// Decompiled with JetBrains decompiler
// Type: MB3_DisableHiddenAnimations
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class MB3_DisableHiddenAnimations : MonoBehaviour
{
  public List<Animation> animationsToCull = new List<Animation>();

  private void Start()
  {
    if (!((Object) this.GetComponent<SkinnedMeshRenderer>() == (Object) null))
      return;
    Debug.LogError((object) ("The MB3_CullHiddenAnimations script was placed on and object " + this.name + " which has no SkinnedMeshRenderer attached"));
  }

  private void OnBecameVisible()
  {
    for (int index = 0; index < this.animationsToCull.Count; ++index)
    {
      if ((Object) this.animationsToCull[index] != (Object) null)
        this.animationsToCull[index].enabled = true;
    }
  }

  private void OnBecameInvisible()
  {
    for (int index = 0; index < this.animationsToCull.Count; ++index)
    {
      if ((Object) this.animationsToCull[index] != (Object) null)
        this.animationsToCull[index].enabled = false;
    }
  }
}
