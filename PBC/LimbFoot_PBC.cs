// Decompiled with JetBrains decompiler
// Type: PBC.LimbFoot_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace PBC
{
  public class LimbFoot_PBC : MonoBehaviour
  {
    private MoveBaseClass_PBC moveClass;
    private RagdollControl_PBC ragdollControl;
    [HideInInspector]
    public int bloodCounter;
    private bool dontTangleLegs = true;
    private Collider ignoredCollider;
    private Collider thisCollider;

    private void Awake()
    {
      if (!(bool) (Object) (this.thisCollider = this.GetComponent<Collider>()))
        Debug.Log((object) ("Limb script on " + this.name + " can't find a Collider component.\n"));
      if (!(bool) (Object) (this.moveClass = this.transform.root.GetComponentInChildren<MoveBaseClass_PBC>()))
        Debug.Log((object) ("Limb script on " + this.name + " can't find a script inheriting from MoveBaseClass_PBC on the ragdoll.\n"));
      if ((bool) (Object) (this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>()))
        return;
      Debug.Log((object) ("Limb script on " + this.name + " can't find a RagdollControl script on the ragdoll.\n"));
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (this.dontTangleLegs && (Object) collision.transform.root == (Object) this.transform.root)
        Physics.IgnoreCollision(collision.collider, this.thisCollider);
      if ((bool) (Object) this.ignoredCollider)
      {
        Physics.IgnoreCollision(this.ignoredCollider, this.thisCollider, false);
        this.ignoredCollider = (Collider) null;
        this.StopCoroutine(this.ReenableFootCollision());
      }
      if (!(bool) (Object) this.moveClass || (double) Vector3.Dot(collision.contacts[0].normal, this.moveClass.gravity_Hat) <= 0.699999988079071)
        return;
      this.ignoredCollider = collision.collider;
      Physics.IgnoreCollision(this.ignoredCollider, this.thisCollider);
      this.StartCoroutine(this.ReenableFootCollision());
    }

    private IEnumerator ReenableFootCollision()
    {
      yield return (object) new WaitForSeconds(1f);
      if ((bool) (Object) this.ignoredCollider && this.thisCollider.enabled)
        Physics.IgnoreCollision(this.ignoredCollider, this.thisCollider, false);
      this.ignoredCollider = (Collider) null;
    }

    private void ReceiveBulletHit(BulletInfo_PBC bulletInfo)
    {
      if (!(bool) (Object) this.ragdollControl)
        return;
      this.ragdollControl.shotByBullet = true;
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
