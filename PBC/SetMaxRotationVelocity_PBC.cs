// Decompiled with JetBrains decompiler
// Type: PBC.SetMaxRotationVelocity_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class SetMaxRotationVelocity_PBC : MonoBehaviour
  {
    private Rigidbody thisRigidbody;
    [SerializeField]
    private float maxAngularVelocity = 100f;
    [SerializeField]
    private bool test;

    private void Awake()
    {
      this.thisRigidbody = this.GetComponent<Rigidbody>();
      this.thisRigidbody.maxAngularVelocity = this.maxAngularVelocity;
    }

    private void FixedUpdate()
    {
      if (!this.test)
        return;
      this.thisRigidbody.AddTorque(this.transform.up * this.thisRigidbody.mass);
    }
  }
}
