// Decompiled with JetBrains decompiler
// Type: ParticleAmountIncreaser
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using net.krej.FPSCounter;
using UnityEngine;

public class ParticleAmountIncreaser : MonoBehaviour
{
  private ParticleSystem _particles;

  private void Start()
  {
    this._particles = this.GetComponent<ParticleSystem>();
    this._particles.emission.rateOverTime = (ParticleSystem.MinMaxCurve) 0.0f;
  }

  private void Update()
  {
    if ((double) net.krej.Singleton.Singleton<FramerateCounter>.Instance.currentFrameRate < 0.9 * (double) net.krej.Singleton.Singleton<net.krej.AutoQualityChooser.AutoQualityChooser>.Instance.settings.minAcceptableFramerate)
      return;
    ParticleSystem.EmissionModule emission = this._particles.emission;
    emission.rateOverTime = (ParticleSystem.MinMaxCurve) (emission.rateOverTimeMultiplier + Time.deltaTime * 10f);
  }
}
