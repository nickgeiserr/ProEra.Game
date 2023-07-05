// Decompiled with JetBrains decompiler
// Type: FootParticleSpawner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class FootParticleSpawner : MonoBehaviour
{
  [SerializeField]
  private Animator PlayerAnimator;
  private bool _ready;

  private void OnTriggerEnter(Collider other)
  {
    if (!this._ready)
      return;
    this._ready = false;
    if ((double) this.PlayerAnimator.GetFloat(LocomotionAgentController.HashIDs.currentEffortFloat) < 0.800000011920929 || !((Object) ParticleSystemManager.Instance != (Object) null))
      return;
    GameObject particle = ParticleSystemManager.Instance.GetParticle(ParticleSystemManager.Particles.Foot);
    particle.transform.position = other.ClosestPointOnBounds(this.transform.position);
    particle.GetComponent<ParticleSystem>().Play();
  }

  private void OnTriggerExit(Collider other) => this._ready = true;
}
