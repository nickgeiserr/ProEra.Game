// Decompiled with JetBrains decompiler
// Type: PBC.LimbHead_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class LimbHead_PBC : MonoBehaviour
  {
    private RagdollControl_PBC ragdollControl;
    private Rigidbody thisRigidbody;
    [HideInInspector]
    public int bloodCounter;

    private void Awake()
    {
      if (!(bool) (Object) (this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>()))
        Debug.Log((object) ("Limb script on " + this.name + " can't find a RagdollControl script on the ragdoll.\n"));
      this.thisRigidbody = this.GetComponent<Rigidbody>();
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
      this.ragdollControl.headShot = true;
      if (!(bool) (Object) this.ragdollControl.blood[0])
        return;
      this.bloodCounter = this.ragdollControl.bloodCounter++;
      if (this.bloodCounter == 3)
        this.ragdollControl.bloodCounter = 0;
      this.ragdollControl.blood[this.bloodCounter].transform.position = bulletInfo.raycastHit.point;
      this.ragdollControl.blood[this.bloodCounter].transform.rotation = Quaternion.LookRotation(bulletInfo.raycastHit.normal);
      this.ragdollControl.blood[this.bloodCounter].transform.parent = bulletInfo.raycastHit.transform;
      this.ragdollControl.blood[this.bloodCounter].gameObject.SetActive(true);
      this.ragdollControl.blood[this.bloodCounter].Play();
      RaycastHit hitInfo;
      if (!this.ragdollControl.doubleBlood || !Physics.Linecast(bulletInfo.raycastHit.point + bulletInfo.impulse.normalized * 0.4f, bulletInfo.raycastHit.point, out hitInfo))
        return;
      this.bloodCounter = this.ragdollControl.bloodCounter++;
      if (this.bloodCounter == 3)
        this.ragdollControl.bloodCounter = 0;
      this.ragdollControl.blood[this.bloodCounter].transform.position = hitInfo.point;
      this.ragdollControl.blood[this.bloodCounter].transform.rotation = Quaternion.LookRotation(bulletInfo.impulse);
      this.ragdollControl.blood[this.bloodCounter].transform.parent = hitInfo.transform;
      this.ragdollControl.blood[this.bloodCounter].gameObject.SetActive(true);
      this.ragdollControl.blood[this.bloodCounter].Play();
    }
  }
}
