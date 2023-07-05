// Decompiled with JetBrains decompiler
// Type: PBC.WeaponAnimFollow_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class WeaponAnimFollow_PBC : MonoBehaviour
  {
    public Transform connectedTransform;
    public Transform followTransform;
    private Rigidbody meleeWeaponRB;
    private ConfigurableJoint configurableJoint;
    private Quaternion jointOrientation;
    private Quaternion startJointLocalRotation;
    private Quaternion startLocalRotation;
    private Quaternion startFollowLocalRotation;
    private Quaternion severalStartRotations;
    private Vector3 rigidbodyPosToCOM;
    private JointDrive jointDrive;
    private Vector3 forceLastError;
    private float reciFixedDeltaTime;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float DForce = 0.3f;
    private float PForce;
    private bool userNeedsToFixStuff;

    private void Awake()
    {
      if (this.userNeedsToFixStuff = !this.WeHaveAllTheStuff())
        return;
      this.reciFixedDeltaTime = 1f / Time.fixedDeltaTime;
      this.meleeWeaponRB.useGravity = false;
      this.meleeWeaponRB.angularDrag = 12f;
      this.meleeWeaponRB.drag = 0.0f;
      this.meleeWeaponRB.maxAngularVelocity = 200f;
      this.jointOrientation = Quaternion.LookRotation(Vector3.Cross(this.configurableJoint.axis, this.configurableJoint.secondaryAxis), this.configurableJoint.secondaryAxis);
      this.startLocalRotation = Quaternion.Inverse(this.connectedTransform.rotation) * this.transform.rotation;
      this.startJointLocalRotation = this.startLocalRotation * this.jointOrientation;
      this.startFollowLocalRotation = Quaternion.Inverse(this.connectedTransform.rotation) * this.followTransform.rotation;
      this.severalStartRotations = Quaternion.Inverse(this.startLocalRotation * this.jointOrientation) * this.startFollowLocalRotation;
      this.rigidbodyPosToCOM = Quaternion.Inverse(this.followTransform.rotation) * (this.meleeWeaponRB.worldCenterOfMass - this.followTransform.position);
      this.configurableJoint.rotationDriveMode = RotationDriveMode.Slerp;
      this.jointDrive = this.configurableJoint.slerpDrive;
      this.configurableJoint.slerpDrive = this.jointDrive;
      this.jointDrive = this.configurableJoint.slerpDrive;
      this.jointDrive.positionDamper = 0.1f;
      this.configurableJoint.slerpDrive = this.jointDrive;
      this.jointDrive = this.configurableJoint.slerpDrive;
      this.jointDrive.positionSpring = 10000f;
      this.configurableJoint.slerpDrive = this.jointDrive;
      this.configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
      this.configurableJoint.projectionDistance = 0.0001f;
    }

    public void DoWeaponAnimFollow()
    {
      if (this.userNeedsToFixStuff)
        return;
      this.PForce = this.reciFixedDeltaTime / this.DForce;
      Vector3 error = this.followTransform.position + this.followTransform.rotation * this.rigidbodyPosToCOM - this.meleeWeaponRB.worldCenterOfMass;
      Vector3 signal;
      StaticStuff_PBC.PDControl(this.PForce, this.DForce, out signal, error, ref this.forceLastError, this.reciFixedDeltaTime);
      this.meleeWeaponRB.AddForce(signal, ForceMode.Acceleration);
      this.configurableJoint.targetRotation = this.severalStartRotations * Quaternion.Inverse(this.followTransform.localRotation) * this.startJointLocalRotation;
    }

    private bool WeHaveAllTheStuff()
    {
      if (!(bool) (Object) this.connectedTransform || !(bool) (Object) this.followTransform)
      {
        Debug.LogWarning((object) ("Not assigned: connectedBody or followTransform " + this.name + "\n"));
        return false;
      }
      this.connectedTransform.root.GetComponentInChildren<Main_PBC>().weaponAnimFollow = this;
      this.meleeWeaponRB = this.GetComponent<Rigidbody>();
      this.configurableJoint = this.GetComponent<ConfigurableJoint>();
      return true;
    }
  }
}
