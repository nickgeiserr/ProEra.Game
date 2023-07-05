// Decompiled with JetBrains decompiler
// Type: BOnly.Limb_BOnly
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using PBC;
using UnityEngine;

namespace BOnly
{
  public class Limb_BOnly : MonoBehaviour
  {
    private RagdollControl_PBC ragdollControl;
    private Rigidbody thisRigidbody;

    private void Awake()
    {
      if (!(bool) (Object) (this.thisRigidbody = this.GetComponent<Rigidbody>()))
        Debug.Log((object) ("Limb script on " + this.name + " can't find a rigidbody component.\n"));
      if ((bool) (Object) (this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>()))
        return;
      Debug.Log((object) ("Limb script on " + this.name + " can't find a RagdollControl script on ragdoll.\n"));
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (!(bool) (Object) this.ragdollControl || !((Object) collision.transform.root != (Object) this.transform.root))
        return;
      ++this.ragdollControl.numberOfCollisions;
      this.ragdollControl.collisionSpeed = Vector3.Dot(collision.relativeVelocity, collision.contacts[0].normal) - Mathf.Clamp01(Vector3.Dot(this.thisRigidbody.velocity - this.ragdollControl.ragdollCOMVelocity, -collision.relativeVelocity.normalized));
    }

    private void OnCollisionExit(Collision collision)
    {
      if (!(bool) (Object) this.ragdollControl || !((Object) collision.transform.root != (Object) this.transform.root))
        return;
      --this.ragdollControl.numberOfCollisions;
    }

    private void ReceiveBulletHit(BulletInfo_PBC bulletInfo)
    {
      if (!(bool) (Object) this.ragdollControl)
        return;
      this.ragdollControl.shotByBullet = true;
    }
  }
}
