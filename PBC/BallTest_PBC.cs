// Decompiled with JetBrains decompiler
// Type: PBC.BallTest_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  [RequireComponent(typeof (Rigidbody))]
  public class BallTest_PBC : MonoBehaviour
  {
    public Transform hitTransform;
    public float ballVelocity = 12f;
    private Rigidbody thisRigidbody;

    private void Awake()
    {
      this.thisRigidbody = this.GetComponent<Rigidbody>();
      this.thisRigidbody.isKinematic = false;
      if ((bool) (Object) this.hitTransform)
        return;
      Debug.Log((object) ("hitTransform on " + this.name + " is not assigned.\n"));
      this.hitTransform = this.transform;
    }

    private void Update()
    {
      if (!Input.GetKeyDown(KeyCode.B))
        return;
      this.thisRigidbody.useGravity = false;
      this.thisRigidbody.velocity = (this.hitTransform.position - this.transform.position).normalized * this.ballVelocity;
    }

    private void OnCollisionEnter(Collision collision) => this.GetComponent<Rigidbody>().useGravity = true;
  }
}
