// Decompiled with JetBrains decompiler
// Type: ParticleSystemManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
  public static ParticleSystemManager Instance;
  [SerializeField]
  private GameObject FootParticle;
  private Dictionary<ParticleSystemManager.Particles, Queue<GameObject>> _particlePool;

  private void Awake()
  {
    ParticleSystemManager.Instance = this;
    this._particlePool = new Dictionary<ParticleSystemManager.Particles, Queue<GameObject>>();
    for (int key = 0; key < 1; ++key)
      this._particlePool.Add((ParticleSystemManager.Particles) key, new Queue<GameObject>());
  }

  private GameObject SpawnParticle(ParticleSystemManager.Particles p)
  {
    GameObject original = (GameObject) null;
    if (p == ParticleSystemManager.Particles.Foot)
      original = this.FootParticle;
    return Object.Instantiate<GameObject>(original);
  }

  public GameObject GetParticle(ParticleSystemManager.Particles p)
  {
    if (this._particlePool[p].Count <= 0)
      return this.SpawnParticle(p);
    GameObject particle = this._particlePool[p].Dequeue();
    particle.SetActive(true);
    return particle;
  }

  public void PoolObject(ParticleSystemManager.Particles p, GameObject g)
  {
    this._particlePool[p].Enqueue(g);
    g.SetActive(false);
  }

  public enum Particles
  {
    Foot,
    COUNT,
  }
}
