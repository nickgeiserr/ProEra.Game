// Decompiled with JetBrains decompiler
// Type: PBC.DragRigidbody_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace PBC
{
  public class DragRigidbody_PBC : MonoBehaviour
  {
    public float maxDistance = 100f;
    public float spring = 50f;
    public float damper = 5f;
    public float drag = 10f;
    public float angularDrag = 5f;
    public float distance = 0.2f;
    public bool attachToCenterOfMass;
    private SpringJoint springJoint;

    private void Update()
    {
      RaycastHit hitInfo;
      if (!Input.GetMouseButtonDown(2) || !Physics.Raycast(this.FindCamera().ScreenPointToRay(Input.mousePosition), out hitInfo, this.maxDistance) || !(bool) (Object) hitInfo.rigidbody || hitInfo.rigidbody.isKinematic)
        return;
      if (!(bool) (Object) this.springJoint)
      {
        GameObject gameObject = new GameObject("Rigidbody dragger");
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        this.springJoint = gameObject.AddComponent<SpringJoint>();
      }
      this.springJoint.transform.position = hitInfo.point;
      if (this.attachToCenterOfMass)
        this.springJoint.anchor = this.springJoint.transform.InverseTransformPoint(this.transform.TransformDirection(hitInfo.rigidbody.centerOfMass) + hitInfo.rigidbody.transform.position);
      else
        this.springJoint.anchor = Vector3.zero;
      this.springJoint.spring = this.spring;
      this.springJoint.damper = this.damper;
      this.springJoint.maxDistance = this.distance;
      this.springJoint.connectedBody = hitInfo.rigidbody;
      this.StartCoroutine(this.DragObject(hitInfo.distance));
    }

    private IEnumerator DragObject(float distance)
    {
      float oldDrag = this.springJoint.connectedBody.drag;
      float oldAngularDrag = this.springJoint.connectedBody.angularDrag;
      this.springJoint.connectedBody.drag = this.drag;
      this.springJoint.connectedBody.angularDrag = this.angularDrag;
      Camera cam = this.FindCamera();
      while (Input.GetMouseButton(2))
      {
        this.springJoint.transform.position = cam.ScreenPointToRay(Input.mousePosition).GetPoint(distance);
        yield return (object) null;
      }
      if ((bool) (Object) this.springJoint.connectedBody)
      {
        this.springJoint.connectedBody.drag = oldDrag;
        this.springJoint.connectedBody.angularDrag = oldAngularDrag;
        this.springJoint.connectedBody = (Rigidbody) null;
      }
    }

    private Camera FindCamera() => (bool) (Object) this.GetComponent<Camera>() ? this.GetComponent<Camera>() : Camera.main;
  }
}
