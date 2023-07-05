// Decompiled with JetBrains decompiler
// Type: FootballVR.TargetHitEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class TargetHitEffect : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem _mainEffect;
    [SerializeField]
    private ParticleSystem[] _effects;

    public void PlayEffect(Color color)
    {
      foreach (ParticleSystem effect in this._effects)
      {
        ParticleSystem.MainModule main = effect.main;
        ParticleSystem.MinMaxGradient startColor = main.startColor with
        {
          color = color
        };
        main.startColor = startColor;
      }
      if (!((Object) this._mainEffect != (Object) null))
        return;
      this._mainEffect.Play();
    }

    public void SimplePlayEffect()
    {
      if (!((Object) this._mainEffect != (Object) null))
        return;
      this._mainEffect.Play();
    }
  }
}
