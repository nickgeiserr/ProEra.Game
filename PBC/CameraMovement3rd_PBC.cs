// Decompiled with JetBrains decompiler
// Type: PBC.CameraMovement3rd_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace PBC
{
  public class CameraMovement3rd_PBC : MonoBehaviour
  {
    [SerializeField]
    private float movementSmooth = 0.15f;
    public Transform followTransform1;
    public Transform followTransform2;
    private Transform followTransform;
    private Vector3 relCameraPos;
    [SerializeField]
    private bool lockOnMiddleMouse = true;

    private void Start()
    {
      if ((bool) (Object) this.followTransform1 && this.followTransform1.gameObject.activeInHierarchy)
        this.followTransform = this.followTransform1;
      else if ((bool) (Object) this.followTransform2 && this.followTransform2.gameObject.activeInHierarchy)
        this.followTransform = this.followTransform2;
      if ((bool) (Object) this.followTransform)
        this.relCameraPos = this.transform.position - this.followTransform.position;
      else
        Debug.Log((object) ("FollowTransform in script CameraMovement is not assigned on " + this.name + ".\n"));
    }

    private void FixedUpdate()
    {
      if (!(bool) (Object) this.followTransform)
        return;
      this.StartCoroutine(this.MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
      CameraMovement3rd_PBC cameraMovement3rdPbc = this;
      yield return (object) new WaitForFixedUpdate();
      Vector3 b = cameraMovement3rdPbc.followTransform.position + cameraMovement3rdPbc.relCameraPos;
      if (!Input.GetMouseButton(2) || !cameraMovement3rdPbc.lockOnMiddleMouse)
        cameraMovement3rdPbc.transform.position = Vector3.Lerp(cameraMovement3rdPbc.transform.position, b, cameraMovement3rdPbc.movementSmooth);
    }
  }
}
