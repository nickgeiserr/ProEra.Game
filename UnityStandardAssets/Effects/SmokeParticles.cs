// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Effects.SmokeParticles
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UnityStandardAssets.Effects
{
  public class SmokeParticles : MonoBehaviour
  {
    public AudioClip[] extinguishSounds;

    private void Start()
    {
      this.GetComponent<AudioSource>().clip = this.extinguishSounds[Random.Range(0, this.extinguishSounds.Length)];
      this.GetComponent<AudioSource>().Play();
    }
  }
}
