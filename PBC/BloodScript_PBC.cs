// Decompiled with JetBrains decompiler
// Type: PBC.BloodScript_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class BloodScript_PBC : MonoBehaviour
  {
    private ParticleSystem particleSystemet;

    private void Awake()
    {
      if ((bool) (Object) (this.particleSystemet = this.GetComponent<ParticleSystem>()))
        return;
      Debug.Log((object) ("No particle system on " + this.name + "\n"));
    }

    private void FixedUpdate()
    {
      if (this.particleSystemet.isPlaying)
        return;
      this.gameObject.SetActive(false);
    }
  }
}
