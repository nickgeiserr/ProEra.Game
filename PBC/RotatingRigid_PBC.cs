// Decompiled with JetBrains decompiler
// Type: PBC.RotatingRigid_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class RotatingRigid_PBC : MonoBehaviour
  {
    public float forceY = 500000f;
    private Rigidbody thisRigid;
    private Collider thisCollider;
    private Collider terrainCollidern;
    [SerializeField]
    private float angularSpeed = 1f;
    private float posY;

    private void Awake()
    {
      this.thisRigid = this.GetComponent<Rigidbody>();
      this.thisCollider = this.GetComponent<Collider>();
      this.terrainCollidern = Terrain.activeTerrain.GetComponent<Collider>();
      this.posY = this.transform.position.y;
      this.thisRigid.mass = 200f;
      this.thisRigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
      Physics.IgnoreCollision(this.thisCollider, this.terrainCollidern);
      if (this.thisRigid.isKinematic)
        this.transform.Rotate(Vector3.up * this.angularSpeed);
      else
        this.thisRigid.AddRelativeTorque(this.transform.up * (this.angularSpeed - Vector3.Dot(this.thisRigid.angularVelocity, this.transform.up)), ForceMode.VelocityChange);
      this.thisRigid.AddForce((this.posY - this.transform.position.y) * Vector3.up * this.forceY);
      if ((double) this.transform.position.y + (double) this.transform.localScale.y + (double) this.thisRigid.velocity.y * (double) Time.fixedDeltaTime >= 0.019999999552965164)
        return;
      this.thisRigid.AddForce(-this.thisRigid.velocity + Vector3.up * 2f, ForceMode.VelocityChange);
    }
  }
}
