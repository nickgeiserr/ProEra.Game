// Decompiled with JetBrains decompiler
// Type: PBC.SphereRotation_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class SphereRotation_PBC : MonoBehaviour
  {
    private Rigidbody rigidboty;
    [SerializeField]
    private float torque = 5f;
    [SerializeField]
    private float angularDrag = 5f;
    [SerializeField]
    private float kinematicAngularSpeed = 1f;

    private void Awake()
    {
      this.rigidboty = this.GetComponent<Rigidbody>();
      this.rigidboty.angularDrag = this.angularDrag;
    }

    private void FixedUpdate()
    {
      if (this.rigidboty.isKinematic)
        this.transform.Rotate(Vector3.up * this.kinematicAngularSpeed);
      else
        this.rigidboty.AddTorque((Vector3.up + Vector3.forward * Mathf.Sin(Time.time * 0.2f) + Vector3.right * Mathf.Cos(Time.time * 0.2f)) * this.torque);
    }
  }
}
