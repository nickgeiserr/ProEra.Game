// Decompiled with JetBrains decompiler
// Type: PBC.TranslateRigid_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class TranslateRigid_PBC : MonoBehaviour
  {
    private Rigidbody thisRigid;
    [SerializeField]
    private float frequency = 1f;
    [SerializeField]
    private float stroke = 2f;
    private Vector3 lastPos;

    private void Awake()
    {
      this.thisRigid = this.GetComponent<Rigidbody>();
      this.thisRigid.mass = 10000f;
      this.thisRigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    private void FixedUpdate()
    {
      Vector3 vector3_1 = this.stroke * Mathf.Sin(this.frequency * Time.time) * this.transform.right;
      Vector3 translation = vector3_1 - this.lastPos;
      Vector3 vector3_2 = translation / Time.fixedDeltaTime;
      if (this.thisRigid.isKinematic)
        this.transform.Translate(translation);
      else
        this.thisRigid.AddForce(vector3_2 - Vector3.Dot(this.thisRigid.velocity, this.transform.right) * this.transform.right, ForceMode.VelocityChange);
      this.lastPos = vector3_1;
    }
  }
}
