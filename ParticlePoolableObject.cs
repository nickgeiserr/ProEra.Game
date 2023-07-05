// Decompiled with JetBrains decompiler
// Type: ParticlePoolableObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ParticlePoolableObject : MonoBehaviour
{
  [SerializeField]
  private ParticleSystemManager.Particles Type;
  private ParticleSystem _particleSystem;
  private float _time;
  private float _timer;

  private void Start()
  {
    this._particleSystem = this.GetComponent<ParticleSystem>();
    ParticleSystem.MainModule main = this._particleSystem.main;
    double duration = (double) main.duration;
    main = this._particleSystem.main;
    double lifetimeMultiplier = (double) main.startLifetimeMultiplier;
    this._time = (float) (duration + lifetimeMultiplier);
    this._timer = 0.0f;
  }

  private void OnEnable() => this._timer = 0.0f;

  private void Update()
  {
    this._timer += Time.deltaTime;
    if ((double) this._timer < (double) this._time)
      return;
    ParticleSystemManager.Instance.PoolObject(this.Type, this.gameObject);
    this._timer = 0.0f;
  }
}
