// Decompiled with JetBrains decompiler
// Type: PBC.AnimFollow_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;

namespace PBC
{
  public class AnimFollow_PBC : MonoBehaviour
  {
    private Animator animator;
    private RagdollControl_PBC ragdollControl;
    public Transform[] masterRigidTransforms;
    public Transform[] ragdollRigidTransforms;
    [HideInInspector]
    public Rigidbody[] ragdollRigidbodies;
    [HideInInspector]
    public Transform[] masterConnectedTransforms;
    [HideInInspector]
    public ConfigurableJoint[] configurableJoints;
    [HideInInspector]
    public CapsuleCollider oneTrigger;
    public float[] jointStrengthProfile = new float[1]{ 1f };
    public float[] forceStrengthProfile = new float[1]{ 1f };
    [Range(0.0f, 1f)]
    public float jointStrength = 0.2f;
    [Range(0.0f, 1f)]
    public float forceStrength = 1f;
    [Range(0.0f, 1f)]
    public float angularStrength = 1f;
    [Range(0.1f, 1f)]
    [SerializeField]
    private float rotationStiffness = 0.4f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float rotationDamping = 0.1f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float tiltStiffness = 0.4f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float tiltDamping = 0.1f;
    [Range(0.05f, 1f)]
    [SerializeField]
    private float poseSoftness = 0.5f;
    [Range(0.05f, 1f)]
    public float poseStiffness = 0.2f;
    [Range(0.0f, 1f)]
    public float accStiffness = 0.7f;
    [Range(0.0f, 1f)]
    public float pushStiffness;
    [Range(0.0f, 1f)]
    public float spinStiffness;
    [Range(0.0f, 1f)]
    public float noLiftOff = 0.3f;
    [Range(0.0f, 1f)]
    public float noCentrifugal = 0.5f;
    public bool updateInspectorChanges;
    public bool autoSuspendRagdoll;
    public bool suspendRagdoll;
    [HideInInspector]
    public bool ragdollSuspended;
    [SerializeField]
    private bool seeMaster;
    [Range(0.0f, 1f)]
    private float jointLimitStrenght = 1f;
    [SerializeField]
    private bool showExternalForce;
    public bool useLagHack1;
    public bool useLagHack2;
    private float angularDrag = 12f;
    private float drag;
    private float jointDamping;
    [HideInInspector]
    public float[] jointDampingProfile = new float[1]{ 1f };
    private float fullJointStrength = 3000f;
    private bool interpolate;
    private bool extrapolate;
    [HideInInspector]
    public Quaternion[] masterRigidRotations;
    [HideInInspector]
    public Vector3 masterRigidPosition0;
    [HideInInspector]
    public bool mResetNeeded;
    private Quaternion[] startJointRotation;
    private Quaternion[] jointOrientation;
    private Vector3[] rigidbodiesPosToCOM;
    private float reciFixedDeltaTime;
    private Transform hips;
    private Transform head;
    private Transform leftBiceps;
    private Transform rightBiceps;
    private Vector3[] forceLastError;
    private Vector3 lastTotalForce;
    private float lastJointStrength = 1f;
    private Vector3 desiredAngAccLerped;
    private Quaternion ragdollRotationError = Quaternion.identity;
    private Vector3[] rigidCenterError;
    private Vector3[] masterRigidTransformsWCOMs;
    private Vector3[] masterRigidTransformsLCOMs;
    private Vector3[] lastMasterRigidTransformsLCOMs;
    private Vector3[] lastMasterRigidLCOM_Velocities;
    private Vector3[] torqueAcc;
    private Vector3[] ragdollLimbRadius;
    private Vector3[] lastRagdollLimbRadius;
    private Vector3[] lastMasterLimbRadius;
    private Vector3[] lastMasterLimbRadius2;
    private Vector3 ragdollAngularVelocity;
    private Vector3 lastRagdollAngularVelocity;
    private Vector3 masterCOMVelocity;
    private Vector3 masterCOMAcceleration;
    private Vector3 masterCOMAccelerationRel;
    private Vector3 masterAngularVelocity;
    private Vector3 masterAngularVelocity2;
    private Vector3 lastMasterAngularVelocity;
    private Vector3 lastMasterAngularVelocity2;
    private Vector3 lastMasterCenterOfMass;
    private Vector3 lastMasterCOMVelocity;
    private Vector3 totalMassCenterError;
    private Vector3 ragdollCOMAcceleration;
    private Vector3 ragdollCenterOfMass;
    private Vector3 externalTorqueAcc;
    private Vector3 gravity_Hat = Physics.gravity.normalized;
    private int numOfRB;
    private bool fixedDone;
    private bool userNeedsToFixStuff;
    private JointDrive[] jointDrive = new JointDrive[1];
    private Transform thisRoot;
    public AnimFollow_PBC.RagdollTransform ragdollTransform;
    private Vector3 velocityCorrection;
    private Vector3 masterCenterOfMass;
    private Vector3 masterToCOMVector;
    private Vector3 masterToCOMVector_Hat;
    private Vector3 ragdollToCOMVector;
    private Vector3 ragdollCOMVelocity;
    private Vector3 externalAccLerped;
    private Vector3 externalTorqueLerped;
    private Vector3 softRotateVector;
    private Vector3 mr2;
    private Vector3 inertiaIntrinsic;
    private float bodyMass;

    public float JointDamping
    {
      get => this.jointDamping;
      set
      {
        this.jointDamping = value;
        this.SetJointDamping(this.jointDamping);
      }
    }

    public float AngularDrag
    {
      get => this.angularDrag;
      set
      {
        this.angularDrag = value;
        this.SetAngularDrag(this.angularDrag);
      }
    }

    public float Drag
    {
      get => this.drag;
      set
      {
        this.drag = value;
        this.SetDrag(this.drag);
      }
    }

    public Vector3 VelocityCorrection
    {
      get => this.velocityCorrection;
      set => Debug.LogWarning((object) "Can't set VelocityCorrection");
    }

    public Vector3 MasterCenterOfMass
    {
      get => this.masterCenterOfMass;
      set => Debug.LogWarning((object) "Can't set MasterCenterOfMass");
    }

    public Vector3 MasterToCOMVector
    {
      get => this.masterToCOMVector;
      set => Debug.LogWarning((object) "Can't set MasterToCOMVector");
    }

    public Vector3 MasterToCOMVector_Hat
    {
      get => this.masterToCOMVector_Hat;
      set => Debug.LogWarning((object) "Can't set MasterToCOMVector_Hat");
    }

    public Vector3 RagdollToCOMVector
    {
      get => this.ragdollToCOMVector;
      set => Debug.LogWarning((object) "Can't set RagdollToCOMVector");
    }

    public Vector3 RagdollCOMVelocity
    {
      get => this.ragdollCOMVelocity;
      set => Debug.LogWarning((object) "Can't set RagdollCOMVelocity");
    }

    public Vector3 ExternalAccLerped
    {
      get => this.externalAccLerped;
      set => Debug.LogWarning((object) "Can't set ExternalAccLerped");
    }

    public Vector3 ExternalTorqueLerped
    {
      get => this.externalTorqueLerped;
      set => Debug.LogWarning((object) "Can't set ExternalTorqueLerped");
    }

    public Vector3 SoftRotateVector
    {
      get => this.softRotateVector;
      set => Debug.LogWarning((object) "Can't set SoftRotateVector");
    }

    public Vector3 Mr2
    {
      get => this.mr2;
      set => Debug.LogWarning((object) "Can't set Mr2");
    }

    public Vector3 InertiaIntrinsic
    {
      get => this.inertiaIntrinsic;
      set => Debug.LogWarning((object) "Can't set InertiaIntrinsic");
    }

    public float BodyMass
    {
      get => this.bodyMass;
      set => Debug.LogWarning((object) "Can't set BodyMass");
    }

    private void Awake()
    {
      this.ragdollRotationError = Quaternion.identity;
      if (this.userNeedsToFixStuff = !this.WeHaveAllTheStuff())
      {
        this.ragdollTransform.position = this.transform.position;
        this.ragdollTransform.rotation_m = this.transform.rotation;
        if (!(bool) (UnityEngine.Object) this.animator)
          return;
        this.ragdollTransform.bodyRotation = this.animator.bodyRotation;
      }
      else
      {
        this.thisRoot = this.transform.root;
        if ((bool) (UnityEngine.Object) this.GetComponent<Main_PBC>())
          this.fullJointStrength *= Mathf.Pow(this.GetComponent<Main_PBC>().realCharacterScale, 2f);
        this.SetJointsAndBodies();
        this.SetJointTorque(this.fullJointStrength);
        this.SetJointDamping(this.jointDamping);
        this.EnableJointLimits(false);
        this.reciFixedDeltaTime = 1f / Time.fixedDeltaTime;
        this.numOfRB = this.ragdollRigidTransforms.Length;
        this.ragdollTransform.position = this.transform.position;
        this.ragdollTransform.rotation_m = this.transform.rotation;
        if (!(bool) (UnityEngine.Object) (this.hips = this.animator.GetBoneTransform(HumanBodyBones.Hips)) || !(bool) (UnityEngine.Object) (this.leftBiceps = this.animator.GetBoneTransform(HumanBodyBones.LeftUpperArm)) || !(bool) (UnityEngine.Object) (this.rightBiceps = this.animator.GetBoneTransform(HumanBodyBones.RightUpperArm)) || !(bool) (UnityEngine.Object) (this.head = this.animator.GetBoneTransform(HumanBodyBones.Head)))
          Debug.LogWarning((object) ("Avatar on " + this.name + " is no good\n"));
        Vector3 normalized1 = Vector3.Cross(this.rightBiceps.position - this.leftBiceps.position, this.head.position - this.hips.position).normalized;
        Vector3 normalized2 = Vector3.Cross(normalized1, this.rightBiceps.position - this.leftBiceps.position).normalized;
        this.ragdollTransform.bodyRotation = Quaternion.LookRotation(normalized1, normalized2);
        this.UpdateRagdollCOMValues(true);
        this.UpdateMasterCOMvalues();
      }
    }

    private void Update()
    {
      if (!this.suspendRagdoll && Input.GetKeyDown(KeyCode.H) && this.ragdollControl.notTripped)
      {
        this.suspendRagdoll = true;
      }
      else
      {
        if (!Input.GetKeyDown(KeyCode.H))
          return;
        this.suspendRagdoll = false;
      }
    }

    public void AfterIK(Vector3 gravity)
    {
      if (this.userNeedsToFixStuff)
      {
        if (!(bool) (UnityEngine.Object) this.animator)
          return;
        this.ragdollTransform.position = this.transform.position;
        this.ragdollTransform.rotation_m = this.transform.rotation;
        this.ragdollTransform.bodyRotation = this.animator.bodyRotation;
      }
      else
      {
        this.gravity_Hat = gravity.normalized;
        this.UpdateMasterCOMvalues();
        this.GetMasterMovements();
        if (this.DoSuspend())
          return;
        if ((double) this.jointStrength != (double) this.lastJointStrength)
        {
          this.SetJointTorque(this.fullJointStrength * this.jointStrength);
          this.lastJointStrength = this.jointStrength;
        }
        if (this.updateInspectorChanges)
        {
          this.SetJointLimitSprings(this.jointLimitStrenght);
          this.JointDamping = this.jointDamping;
          this.Drag = this.drag;
          this.AngularDrag = this.angularDrag;
          this.SetJointTorque(this.fullJointStrength * this.jointStrength);
          this.updateInspectorChanges = false;
        }
        this.CalculateRagdollTransform();
        Quaternion ragdollRotationError = this.ragdollRotationError;
        this.transform.rotation = ragdollRotationError * this.transform.rotation;
        Vector3 vector3_1 = ragdollRotationError * this.masterToCOMVector;
        Vector3 vector3_2 = vector3_1 - this.masterToCOMVector;
        this.masterToCOMVector = vector3_1;
        this.masterToCOMVector_Hat = this.masterToCOMVector.normalized;
        Vector3 vector3_3 = Vector3.zero;
        if ((double) this.pushStiffness > 0.0)
          vector3_3 = -this.externalAccLerped * this.pushStiffness;
        Vector3 vector3_4 = Vector3.zero;
        if ((double) this.noLiftOff > 0.0)
          vector3_4 = Mathf.Clamp(Vector3.Dot(-this.externalAccLerped * this.noLiftOff, this.gravity_Hat), 0.0f, 1000f) * this.gravity_Hat;
        this.transform.Translate(-this.totalMassCenterError - vector3_2, Space.World);
        Vector3 vector3_5 = Vector3.Lerp(this.masterCOMAcceleration, this.masterCOMAccelerationRel, this.accStiffness);
        for (int index = 0; index < this.numOfRB; ++index)
        {
          Vector3 vector3_6 = this.masterRigidTransforms[index].position + this.masterRigidTransforms[index].rotation * this.rigidbodiesPosToCOM[index];
          this.rigidCenterError[index] = vector3_6 - this.ragdollRigidbodies[index].worldCenterOfMass;
        }
        float num1 = 0.0f;
        if (false)
        {
          for (int index = 0; index < this.numOfRB; ++index)
            num1 += this.rigidCenterError[index].magnitude * this.ragdollRigidbodies[index].mass;
          float num2 = num1 / this.bodyMass;
        }
        float P = this.reciFixedDeltaTime / this.poseSoftness;
        this.lastTotalForce = Vector3.zero;
        float num3 = this.reciFixedDeltaTime * (1f - this.poseStiffness);
        float D = this.poseSoftness * this.poseStiffness;
        Quaternion rotation = this.transform.rotation;
        for (int index = 0; index < this.numOfRB; ++index)
        {
          Vector3 vector3_7 = (this.masterRigidTransformsLCOMs[index] - this.lastMasterRigidTransformsLCOMs[index]) * this.reciFixedDeltaTime;
          Vector3 vector3_8 = rotation * (vector3_7 - this.lastMasterRigidLCOM_Velocities[index]) * num3;
          this.lastMasterRigidLCOM_Velocities[index] = vector3_7;
          Vector3 signal;
          StaticStuff_PBC.PDControl(P, D, out signal, this.rigidCenterError[index], ref this.forceLastError[index], this.reciFixedDeltaTime);
          float num4 = this.forceStrength * this.forceStrengthProfile[index];
          Vector3 force = (signal + vector3_5 + vector3_8 + vector3_3 + vector3_4 + this.torqueAcc[index]) * num4 + (1f - num4) * gravity;
          this.ragdollRigidbodies[index].AddForce(force, ForceMode.Acceleration);
          this.lastTotalForce += force * this.ragdollRigidbodies[index].mass;
          this.ragdollRigidbodies[index].AddTorque(this.desiredAngAccLerped * num4, ForceMode.Acceleration);
        }
        if ((double) this.jointStrength > 0.0)
        {
          for (int index = 1; index < this.numOfRB; ++index)
            this.configurableJoints[index].targetRotation = Quaternion.Inverse(this.masterRigidTransforms[index].rotation * this.jointOrientation[index]) * this.masterConnectedTransforms[index].rotation * this.startJointRotation[index];
        }
        this.fixedDone = true;
      }
    }

    private void LateUpdate()
    {
      if (this.userNeedsToFixStuff)
        return;
      if (this.fixedDone && !this.seeMaster)
      {
        this.masterRigidTransforms[0].rotation = this.ragdollRigidTransforms[0].rotation;
        this.masterRigidTransforms[0].position = !this.useLagHack2 ? this.ragdollRigidTransforms[0].position : this.ragdollRigidTransforms[0].position + this.ragdollCOMVelocity * Time.fixedDeltaTime;
        for (int index = 1; index < this.numOfRB; ++index)
          this.masterRigidTransforms[index].rotation = this.ragdollRigidTransforms[index].rotation;
        this.mResetNeeded = true;
      }
      if (this.ragdollControl.stayDown2)
        return;
      this.fixedDone = false;
    }

    public void ResetMaster()
    {
      if (!this.mResetNeeded)
        return;
      this.masterRigidTransforms[0].position = this.masterRigidPosition0;
      for (int index = 0; index < this.numOfRB; ++index)
        this.masterRigidTransforms[index].rotation = this.masterRigidRotations[index];
      this.mResetNeeded = false;
    }

    public void BeforeMove()
    {
      if (this.userNeedsToFixStuff || this.ragdollSuspended)
        return;
      this.UpdateRagdollCOMValues(false);
      this.GetRagdollDeltaRotation();
      this.externalAccLerped = Vector3.Lerp(this.externalAccLerped, this.ragdollCOMAcceleration - this.lastTotalForce / this.bodyMass, 0.25f);
      if (this.showExternalForce)
        this.ShowExternalForce();
      this.transform.rotation = this.ragdollRotationError * this.transform.rotation;
      this.transform.Translate(this.ragdollCOMVelocity * Time.fixedDeltaTime + this.ragdollRotationError * -this.masterToCOMVector + this.masterToCOMVector, Space.World);
      this.masterRigidPosition0 = this.masterRigidTransforms[0].position;
      for (int index = 0; index < this.numOfRB; ++index)
        this.masterRigidRotations[index] = this.masterRigidTransforms[index].rotation;
      this.velocityCorrection = this.RagdollCOMVelocity - this.masterCOMVelocity;
    }

    private void UpdateMasterCOMvalues()
    {
      this.totalMassCenterError = Vector3.zero;
      Quaternion quaternion = Quaternion.Inverse(this.transform.rotation);
      for (int index = 0; index < this.numOfRB; ++index)
      {
        this.masterRigidTransformsWCOMs[index] = this.masterRigidTransforms[index].position + this.masterRigidTransforms[index].rotation * this.rigidbodiesPosToCOM[index];
        this.rigidCenterError[index] = this.masterRigidTransformsWCOMs[index] - this.ragdollRigidbodies[index].worldCenterOfMass;
        this.lastMasterRigidTransformsLCOMs[index] = this.masterRigidTransformsLCOMs[index];
        this.totalMassCenterError += this.rigidCenterError[index] * this.ragdollRigidbodies[index].mass;
      }
      this.totalMassCenterError /= this.bodyMass;
      this.masterCenterOfMass = this.ragdollCenterOfMass + this.totalMassCenterError;
      this.masterToCOMVector = this.masterCenterOfMass - this.transform.position;
      this.masterToCOMVector_Hat = this.masterToCOMVector.normalized;
      for (int index = 0; index < this.numOfRB; ++index)
        this.masterRigidTransformsLCOMs[index] = quaternion * (this.masterRigidTransformsWCOMs[index] - this.masterCenterOfMass);
    }

    private void UpdateRagdollCOMValues(bool initial)
    {
      Vector3 ragdollCenterOfMass = this.ragdollCenterOfMass;
      Vector3 ragdollComVelocity = this.ragdollCOMVelocity;
      this.ragdollCenterOfMass = Vector3.zero;
      this.bodyMass = 0.0f;
      for (int index = 0; index < this.numOfRB; ++index)
      {
        this.ragdollCenterOfMass += this.ragdollRigidbodies[index].worldCenterOfMass * this.ragdollRigidbodies[index].mass;
        this.bodyMass += this.ragdollRigidbodies[index].mass;
      }
      this.ragdollCenterOfMass /= this.bodyMass;
      if (!initial)
      {
        this.ragdollCOMVelocity = (this.ragdollCenterOfMass - ragdollCenterOfMass) * this.reciFixedDeltaTime;
        this.ragdollCOMAcceleration = (this.ragdollCOMVelocity - ragdollComVelocity) * this.reciFixedDeltaTime;
      }
      else
      {
        this.ragdollCOMVelocity = Vector3.zero;
        this.ragdollCOMAcceleration = Vector3.zero;
      }
    }

    private void GetMasterMovements()
    {
      if (!this.ragdollSuspended)
      {
        this.masterCOMVelocity = (this.masterCenterOfMass - this.ragdollCenterOfMass) * this.reciFixedDeltaTime;
        this.masterCOMAcceleration = (this.masterCOMVelocity - this.lastMasterCOMVelocity) * this.reciFixedDeltaTime;
        this.masterCOMAccelerationRel = (this.masterCOMVelocity - this.ragdollCOMVelocity) * this.reciFixedDeltaTime;
        this.lastMasterCenterOfMass = this.ragdollCenterOfMass;
        this.lastMasterCOMVelocity = this.masterCOMVelocity;
      }
      else
      {
        this.masterCOMVelocity = (this.masterCenterOfMass - this.lastMasterCenterOfMass) * this.reciFixedDeltaTime;
        this.masterCOMAcceleration = (this.masterCOMVelocity - this.lastMasterCOMVelocity) * this.reciFixedDeltaTime;
        this.lastMasterCenterOfMass = this.masterCenterOfMass;
        this.lastMasterCOMVelocity = this.masterCOMVelocity;
      }
    }

    private void GetRagdollDeltaRotation()
    {
      Vector3 zero1 = Vector3.zero;
      Vector3 zero2 = Vector3.zero;
      Quaternion quaternion1 = Quaternion.Inverse(this.ragdollTransform.rotation);
      for (int index = 0; index < this.numOfRB; ++index)
      {
        this.lastRagdollLimbRadius[index] = this.ragdollLimbRadius[index];
        this.ragdollLimbRadius[index] = this.ragdollRigidbodies[index].worldCenterOfMass - this.ragdollCenterOfMass;
        Vector3 rhs = this.lastRagdollLimbRadius[index] - this.ragdollLimbRadius[index];
        Vector3 vector3_1 = Vector3.Cross(this.ragdollLimbRadius[index], rhs) * this.ragdollRigidbodies[index].mass;
        zero1 += vector3_1;
        Vector3 vector3_2 = quaternion1 * this.ragdollLimbRadius[index];
        Vector3 vector3_3;
        vector3_3.x = (float) ((double) vector3_2.y * (double) vector3_2.y + (double) vector3_2.z * (double) vector3_2.z);
        vector3_3.y = (float) ((double) vector3_2.x * (double) vector3_2.x + (double) vector3_2.z * (double) vector3_2.z);
        vector3_3.z = (float) ((double) vector3_2.x * (double) vector3_2.x + (double) vector3_2.y * (double) vector3_2.y);
        vector3_3 *= this.ragdollRigidbodies[index].mass;
        zero2 += vector3_3;
      }
      Vector3 vector3_4 = quaternion1 * zero1;
      Vector3 axis;
      axis.x = vector3_4.x / zero2.x;
      axis.y = vector3_4.y / zero2.y;
      axis.z = vector3_4.z / zero2.z;
      Quaternion ragdollRotationError = this.ragdollRotationError;
      this.ragdollRotationError = Quaternion.AngleAxis((float) (-(double) axis.magnitude * 57.295780181884766), axis);
      this.ragdollRotationError = this.ragdollTransform.rotation * this.ragdollRotationError * quaternion1;
      Quaternion quaternion2 = this.ragdollRotationError * ragdollRotationError;
      for (int index = 0; index < this.numOfRB; ++index)
        this.lastMasterLimbRadius[index] = quaternion2 * this.lastMasterLimbRadius[index];
      this.ragdollTransform.rotation_m = this.ragdollRotationError * this.ragdollTransform.rotation_m;
      this.ragdollTransform.rotation = this.ragdollRotationError * this.ragdollTransform.rotation;
      this.ragdollTransform.bodyRotation = this.ragdollRotationError * this.ragdollTransform.bodyRotation;
      this.ragdollToCOMVector = this.ragdollRotationError * this.ragdollToCOMVector;
      this.ragdollTransform.position = this.ragdollCenterOfMass - this.ragdollToCOMVector;
      this.masterToCOMVector = this.ragdollRotationError * this.masterToCOMVector;
      this.masterToCOMVector_Hat = this.masterToCOMVector.normalized;
      axis = this.ragdollTransform.rotation * axis;
      this.lastRagdollAngularVelocity = this.ragdollAngularVelocity;
      this.ragdollAngularVelocity = -axis * this.reciFixedDeltaTime;
      Vector3 vector3_5 = (this.ragdollAngularVelocity - this.lastRagdollAngularVelocity) * this.reciFixedDeltaTime;
      this.externalTorqueAcc = vector3_5 - this.desiredAngAccLerped * this.forceStrength;
      this.externalTorqueLerped = Vector3.Lerp(this.externalTorqueLerped, this.transform.rotation * Vector3.Scale(quaternion1 * (vector3_5 - this.desiredAngAccLerped * this.forceStrength), zero2 + this.inertiaIntrinsic), 0.25f);
    }

    private void CalculateRagdollTransform()
    {
      Vector3 zero1 = Vector3.zero;
      Vector3 zero2 = Vector3.zero;
      Vector3 zero3 = Vector3.zero;
      this.mr2 = Vector3.zero;
      Quaternion quaternion = Quaternion.Inverse(this.transform.rotation);
      for (int index = 0; index < this.numOfRB; ++index)
      {
        Vector3 lhs = this.masterRigidTransformsWCOMs[index] - this.masterCenterOfMass;
        Vector3 rhs1 = this.ragdollLimbRadius[index] - lhs;
        Vector3 vector3_1 = Vector3.Cross(lhs, rhs1) * this.ragdollRigidbodies[index].mass;
        zero1 += vector3_1;
        Vector3 rhs2 = this.lastMasterLimbRadius[index] - lhs;
        Vector3 vector3_2 = Vector3.Cross(lhs, rhs2) * this.ragdollRigidbodies[index].mass;
        zero2 += vector3_2;
        Vector3 rhs3 = this.lastMasterLimbRadius2[index] - lhs;
        Vector3 vector3_3 = Vector3.Cross(lhs, rhs3) * this.ragdollRigidbodies[index].mass;
        zero3 += vector3_3;
        Vector3 vector3_4 = quaternion * lhs;
        Vector3 vector3_5;
        vector3_5.x = (float) ((double) vector3_4.y * (double) vector3_4.y + (double) vector3_4.z * (double) vector3_4.z);
        vector3_5.y = (float) ((double) vector3_4.x * (double) vector3_4.x + (double) vector3_4.z * (double) vector3_4.z);
        vector3_5.z = (float) ((double) vector3_4.x * (double) vector3_4.x + (double) vector3_4.y * (double) vector3_4.y);
        vector3_5 *= this.ragdollRigidbodies[index].mass;
        this.mr2 += vector3_5;
        this.lastMasterLimbRadius[index] = lhs;
        this.lastMasterLimbRadius2[index] = lhs;
      }
      Vector3 vector3_6 = quaternion * zero1;
      Vector3 axis;
      axis.x = vector3_6.x / this.mr2.x;
      axis.y = vector3_6.y / this.mr2.y;
      axis.z = vector3_6.z / this.mr2.z;
      this.ragdollRotationError = Quaternion.AngleAxis(axis.magnitude * 57.29578f, axis);
      this.ragdollRotationError = this.transform.rotation * this.ragdollRotationError * quaternion;
      Vector3 vector3_7 = quaternion * zero2;
      Vector3 vector3_8;
      vector3_8.x = vector3_7.x / this.mr2.x;
      vector3_8.y = vector3_7.y / this.mr2.y;
      vector3_8.z = vector3_7.z / this.mr2.z;
      Vector3 vector3_9 = quaternion * zero3;
      Vector3 vector3_10;
      vector3_10.x = vector3_9.x / this.mr2.x;
      vector3_10.y = vector3_9.y / this.mr2.y;
      vector3_10.z = vector3_9.z / this.mr2.z;
      this.ragdollTransform.rotation_m = this.ragdollRotationError * this.transform.rotation;
      Vector3 normalized1 = Vector3.Cross(this.rightBiceps.position - this.leftBiceps.position, this.head.position - this.hips.position).normalized;
      Vector3 normalized2 = Vector3.Cross(normalized1, this.rightBiceps.position - this.leftBiceps.position).normalized;
      this.ragdollTransform.bodyRotation = this.ragdollRotationError * Quaternion.LookRotation(normalized1, normalized2);
      this.ragdollTransform.rotation = Quaternion.Lerp(this.ragdollTransform.bodyRotation, this.ragdollTransform.rotation_m, this.ragdollControl.layerControl);
      this.ragdollToCOMVector = this.ragdollRotationError * this.masterToCOMVector;
      this.ragdollTransform.position = this.ragdollCenterOfMass - this.ragdollToCOMVector;
      Vector3 vector3_11 = this.transform.rotation * axis;
      this.lastMasterAngularVelocity = this.masterAngularVelocity;
      this.masterAngularVelocity = this.transform.rotation * vector3_8 * -this.reciFixedDeltaTime;
      this.lastMasterAngularVelocity2 = this.masterAngularVelocity2;
      this.masterAngularVelocity2 = this.transform.rotation * vector3_10 * -this.reciFixedDeltaTime;
      Vector3 lhs1 = (this.masterAngularVelocity - this.ragdollAngularVelocity) * this.reciFixedDeltaTime;
      Vector3 b1 = Vector3.Dot(lhs1, this.transform.up) * this.transform.up;
      Vector3 b2 = lhs1 - b1;
      Vector3 lhs2 = (this.masterAngularVelocity - this.lastMasterAngularVelocity) * this.reciFixedDeltaTime;
      Vector3 a1 = Vector3.Dot(lhs2, this.transform.up) * this.transform.up;
      Vector3 a2 = lhs2 - a1;
      Vector3 lhs3 = (this.masterAngularVelocity2 - this.ragdollAngularVelocity) * this.reciFixedDeltaTime;
      Vector3 b3 = Vector3.Dot(lhs3, this.transform.up) * this.transform.up;
      Vector3 vector3_12 = lhs3 - b3;
      Vector3 lhs4 = (this.masterAngularVelocity2 - this.lastMasterAngularVelocity2) * this.reciFixedDeltaTime;
      Vector3 a3 = Vector3.Dot(lhs4, this.transform.up) * this.transform.up;
      Vector3 a4 = lhs4 - a3;
      Vector3 a5 = Vector3.Lerp(a2, b2, this.tiltStiffness);
      Vector3 b4 = vector3_12;
      double tiltStiffness = (double) this.tiltStiffness;
      Vector3 b5 = Vector3.Lerp(a4, b4, (float) tiltStiffness);
      this.desiredAngAccLerped = (Vector3.Lerp(a5, b5, this.tiltDamping) + Vector3.Lerp(Vector3.Lerp(a1, b1, this.rotationStiffness), Vector3.Lerp(a3, b3, this.rotationStiffness), this.rotationDamping) - this.externalTorqueAcc * this.spinStiffness) * this.angularStrength;
      for (int index = 0; index < this.numOfRB; ++index)
        this.torqueAcc[index] = Vector3.Cross(this.desiredAngAccLerped, this.ragdollLimbRadius[index]) + Vector3.Cross(this.ragdollAngularVelocity, Vector3.Cross(this.ragdollAngularVelocity, this.ragdollLimbRadius[index])) * this.noCentrifugal;
    }

    private void SetJointDamping(float positionDamper)
    {
      for (int index = 1; index < this.configurableJoints.Length; ++index)
      {
        this.jointDrive[index].positionDamper = positionDamper * this.jointDampingProfile[index];
        this.configurableJoints[index].slerpDrive = this.jointDrive[index];
      }
    }

    private void SetJointTorque(float positionSpring)
    {
      for (int index = 1; index < this.configurableJoints.Length; ++index)
      {
        this.jointDrive[index].positionSpring = positionSpring * this.jointStrengthProfile[index];
        this.configurableJoints[index].slerpDrive = this.jointDrive[index];
      }
    }

    private void SetjointProjection()
    {
      for (int index = 1; index < this.configurableJoints.Length; ++index)
      {
        this.configurableJoints[index].projectionMode = JointProjectionMode.PositionAndRotation;
        this.configurableJoints[index].projectionDistance = 0.0001f;
      }
    }

    public void EnableJointLimits(bool jointLimits)
    {
      for (int index = 1; index < this.configurableJoints.Length; ++index)
      {
        if (jointLimits)
        {
          this.configurableJoints[index].angularXMotion = ConfigurableJointMotion.Limited;
          this.configurableJoints[index].angularYMotion = ConfigurableJointMotion.Limited;
          this.configurableJoints[index].angularZMotion = ConfigurableJointMotion.Limited;
        }
        else
        {
          this.configurableJoints[index].angularXMotion = ConfigurableJointMotion.Free;
          this.configurableJoints[index].angularYMotion = ConfigurableJointMotion.Free;
          this.configurableJoints[index].angularZMotion = ConfigurableJointMotion.Free;
        }
      }
    }

    public void SetJointLimitSprings(float spring)
    {
      SoftJointLimitSpring angularXlimitSpring = this.configurableJoints[1].angularXLimitSpring;
      for (int index = 1; index < this.configurableJoints.Length; ++index)
      {
        angularXlimitSpring.spring = (float) ((double) spring * (double) this.fullJointStrength + 1.0);
        this.configurableJoints[index].angularXLimitSpring = angularXlimitSpring;
        this.configurableJoints[index].angularYZLimitSpring = angularXlimitSpring;
      }
    }

    private void SetJointsAndBodies()
    {
      this.inertiaIntrinsic = Vector3.zero;
      this.SetjointProjection();
      int index = 0;
      foreach (Transform ragdollRigidTransform in this.ragdollRigidTransforms)
      {
        this.ragdollRigidbodies[index].useGravity = false;
        this.ragdollRigidbodies[index].angularDrag = this.angularDrag;
        this.ragdollRigidbodies[index].drag = this.drag;
        if (this.interpolate)
          this.ragdollRigidbodies[index].interpolation = RigidbodyInterpolation.Interpolate;
        if (this.extrapolate)
          this.ragdollRigidbodies[index].interpolation = RigidbodyInterpolation.Extrapolate;
        this.ragdollRigidbodies[index].maxAngularVelocity = 100f;
        this.bodyMass += this.ragdollRigidbodies[index].mass;
        if ((bool) (UnityEngine.Object) this.configurableJoints[index])
        {
          Vector3 forward = Vector3.Cross(this.configurableJoints[index].axis, this.configurableJoints[index].secondaryAxis);
          Vector3 secondaryAxis = this.configurableJoints[index].secondaryAxis;
          if (forward == Vector3.zero || secondaryAxis == Vector3.zero)
            Debug.LogWarning((object) ("Setup failed.\nYou need to manually set the configurable joint's axis and/or secondary axis on " + this.ragdollRigidbodies[index].name + ".\n"));
          this.jointOrientation[index] = Quaternion.LookRotation(forward, secondaryAxis);
          this.startJointRotation[index] = ragdollRigidTransform.localRotation * this.jointOrientation[index];
          this.configurableJoints[index].rotationDriveMode = RotationDriveMode.Slerp;
          this.jointDrive[index] = this.configurableJoints[index].slerpDrive;
          this.configurableJoints[index].slerpDrive = this.jointDrive[index];
        }
        this.rigidbodiesPosToCOM[index] = Quaternion.Inverse(ragdollRigidTransform.rotation) * (this.ragdollRigidbodies[index].worldCenterOfMass - ragdollRigidTransform.position);
        Quaternion rotation = Quaternion.Inverse(this.ragdollRigidbodies[index].transform.rotation) * this.transform.rotation;
        this.inertiaIntrinsic += Quaternion.Inverse(rotation) * Vector3.Scale(rotation * Vector3.one, this.ragdollRigidbodies[index].inertiaTensor);
        ++index;
      }
    }

    private void SetAngularDrag(float angularDrag)
    {
      for (int index = 0; index < this.numOfRB; ++index)
        this.ragdollRigidbodies[index].angularDrag = angularDrag;
    }

    private void SetDrag(float drag)
    {
      for (int index = 0; index < this.numOfRB; ++index)
        this.ragdollRigidbodies[index].drag = drag;
    }

    private void ShowExternalForce() => Debug.DrawRay(this.MasterCenterOfMass + Vector3.Cross(this.ExternalAccLerped * this.BodyMass, this.ExternalTorqueLerped) / (this.ExternalAccLerped * this.BodyMass).sqrMagnitude, this.ExternalAccLerped * 0.2f, Color.yellow);

    private bool WeHaveAllTheStuff()
    {
      if (!(bool) (UnityEngine.Object) (this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>()))
      {
        Debug.LogWarning((object) ("Missing script RagdollControl on " + this.name + "\n"));
        return false;
      }
      if (!(bool) (UnityEngine.Object) (this.animator = this.GetComponent<Animator>()))
      {
        Debug.LogWarning((object) ("Missing animator on " + this.name + "\n"));
        return false;
      }
      if (!this.animator.updateMode.Equals((object) AnimatorUpdateMode.AnimatePhysics))
      {
        Debug.LogWarning((object) ("Animator on " + this.name + " is not set to animate physics\n"));
        this.animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
      }
      if (!this.animator.cullingMode.Equals((object) AnimatorCullingMode.AlwaysAnimate))
      {
        Debug.LogWarning((object) ("Animator on " + this.name + " is not set to always animate\n"));
        this.animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
      }
      int newSize = 0;
      foreach (Transform ragdollRigidTransform in this.ragdollRigidTransforms)
      {
        try
        {
          if (!((UnityEngine.Object) ragdollRigidTransform == (UnityEngine.Object) null) && !((UnityEngine.Object) this.masterRigidTransforms[newSize] == (UnityEngine.Object) null) && !((UnityEngine.Object) this.ragdollRigidbodies[newSize++] == (UnityEngine.Object) null))
          {
            if (newSize < this.configurableJoints.Length)
            {
              if (!((UnityEngine.Object) this.configurableJoints[newSize] == (UnityEngine.Object) null))
              {
                if (!((UnityEngine.Object) this.masterConnectedTransforms[newSize] == (UnityEngine.Object) null))
                  continue;
              }
            }
            else
              continue;
          }
          Debug.LogWarning((object) ("Ragdoll transform arrays not all assigned on " + this.name + ".\n"));
          return false;
        }
        catch
        {
          Debug.LogWarning((object) ("Ragdoll transform arrays not all assigned on " + this.name + ".\n"));
          return false;
        }
      }
      if (this.jointStrengthProfile.Length != newSize || this.forceStrengthProfile.Length != newSize)
      {
        Array.Resize<float>(ref this.jointStrengthProfile, newSize);
        Array.Resize<float>(ref this.forceStrengthProfile, newSize);
        if ((double) this.jointStrengthProfile[newSize - 1] == 0.0 || (double) this.forceStrengthProfile[newSize - 1] == 0.0)
          Debug.LogWarning((object) ("Strength profile is set to zero on " + this.name + ".\n"));
      }
      Array.Resize<JointDrive>(ref this.jointDrive, newSize);
      Array.Resize<Vector3>(ref this.rigidCenterError, newSize);
      Array.Resize<Vector3>(ref this.masterRigidTransformsWCOMs, newSize);
      Array.Resize<Vector3>(ref this.masterRigidTransformsLCOMs, newSize);
      Array.Resize<Vector3>(ref this.lastMasterRigidTransformsLCOMs, newSize);
      Array.Resize<Vector3>(ref this.lastMasterRigidLCOM_Velocities, newSize);
      Array.Resize<Vector3>(ref this.ragdollLimbRadius, newSize);
      Array.Resize<Vector3>(ref this.lastRagdollLimbRadius, newSize);
      Array.Resize<Vector3>(ref this.lastMasterLimbRadius, newSize);
      Array.Resize<Vector3>(ref this.lastMasterLimbRadius2, newSize);
      Array.Resize<Vector3>(ref this.torqueAcc, newSize);
      Array.Resize<Vector3>(ref this.forceLastError, newSize);
      Array.Resize<Vector3>(ref this.rigidbodiesPosToCOM, newSize);
      Array.Resize<Quaternion>(ref this.startJointRotation, newSize);
      Array.Resize<Quaternion>(ref this.jointOrientation, newSize);
      Array.Resize<Quaternion>(ref this.masterRigidRotations, newSize);
      return true;
    }

    private void OnTriggerStay(Collider collider)
    {
      if (!((UnityEngine.Object) collider.transform.root != (UnityEngine.Object) this.thisRoot))
        return;
      this.suspendRagdoll = false;
      this.oneTrigger.enabled = false;
      this.ragdollControl.collisionFreeTime = 0.0f;
    }

    private void ReceiveBulletHit(BulletInfo_PBC bulletInfo)
    {
      for (int index = 0; index < this.numOfRB; ++index)
      {
        this.ragdollRigidTransforms[index].position = this.masterRigidTransforms[index].position;
        this.ragdollRigidTransforms[index].rotation = this.masterRigidTransforms[index].rotation;
        this.ragdollRigidTransforms[index].GetComponent<Collider>().enabled = true;
      }
      this.UpdateRagdollCOMValues(true);
      if (Physics.Raycast(bulletInfo.raycastHit.point + bulletInfo.impulse.normalized * 0.01f, bulletInfo.impulse, out bulletInfo.raycastHit, 2f) && (UnityEngine.Object) bulletInfo.raycastHit.transform.root == (UnityEngine.Object) this.thisRoot)
      {
        this.suspendRagdoll = false;
        this.oneTrigger.enabled = false;
        this.ragdollControl.collisionFreeTime = 0.0f;
        bulletInfo.localHit = bulletInfo.raycastHit.point - bulletInfo.raycastHit.transform.position;
        this.StartCoroutine(this.AddForceToLimb(bulletInfo));
      }
      else
      {
        for (int index = 0; index < this.numOfRB; ++index)
          this.ragdollRigidTransforms[index].GetComponent<Collider>().enabled = false;
      }
    }

    private IEnumerator AddForceToLimb(BulletInfo_PBC bulletInfo)
    {
      while (this.ragdollSuspended)
        yield return (object) null;
      bulletInfo.raycastHit.transform.SendMessage("ReceiveBulletHit", (object) bulletInfo, SendMessageOptions.DontRequireReceiver);
      bulletInfo.raycastHit.rigidbody.AddForceAtPosition(bulletInfo.impulse, bulletInfo.raycastHit.transform.position + bulletInfo.localHit, ForceMode.Impulse);
    }

    private bool DoSuspend()
    {
      if (this.autoSuspendRagdoll && (double) this.ragdollControl.collisionFreeTime >= 0.5 && this.ragdollControl.notTripped)
        this.suspendRagdoll = true;
      if (this.ragdollSuspended || this.suspendRagdoll)
      {
        if (this.suspendRagdoll && this.ragdollSuspended)
        {
          this.oneTrigger.enabled = true;
          if (!this.autoSuspendRagdoll)
            this.oneTrigger.enabled = false;
          return true;
        }
        if (this.suspendRagdoll)
        {
          this.ragdollSuspended = true;
          this.softRotateVector = Vector3.zero;
          this.externalAccLerped = Vector3.zero;
          this.externalTorqueLerped = Vector3.zero;
          this.externalTorqueAcc = Vector3.zero;
          if (this.autoSuspendRagdoll)
            this.oneTrigger.enabled = true;
          for (int index = 0; index < this.numOfRB; ++index)
          {
            this.ragdollRigidbodies[index].isKinematic = true;
            this.ragdollRigidTransforms[index].GetComponent<Collider>().enabled = false;
            this.ragdollRigidbodies[index].velocity = Vector3.zero;
          }
          if (!this.useLagHack1 && !this.useLagHack2)
            this.transform.Translate(-this.totalMassCenterError, Space.World);
          return true;
        }
        if (this.ragdollSuspended && !this.suspendRagdoll)
        {
          this.ragdollSuspended = false;
          this.oneTrigger.enabled = false;
          this.ragdollControl.collisionFreeTime = 0.0f;
          if (this.useLagHack1 || this.useLagHack2)
            this.transform.Translate(-this.masterCOMVelocity * Time.fixedDeltaTime, Space.World);
          for (int index = 0; index < this.numOfRB; ++index)
          {
            this.ragdollRigidTransforms[index].position = this.masterRigidTransforms[index].position;
            this.ragdollRigidTransforms[index].rotation = this.masterRigidTransforms[index].rotation;
            this.ragdollRigidbodies[index].isKinematic = false;
            this.forceLastError[index] = Vector3.zero;
            this.ragdollRigidTransforms[index].GetComponent<Collider>().enabled = true;
            this.ragdollRigidbodies[index].velocity = this.masterCOMVelocity;
          }
          this.ragdollTransform.position = this.transform.position;
          this.ragdollTransform.rotation_m = this.transform.rotation;
          this.ragdollTransform.bodyRotation = this.animator.bodyRotation;
          this.UpdateRagdollCOMValues(true);
          this.ragdollCOMVelocity = this.masterCOMVelocity;
          this.UpdateMasterCOMvalues();
          this.masterCOMAccelerationRel = Vector3.zero;
          for (int index = 0; index < this.numOfRB; ++index)
          {
            this.ragdollLimbRadius[index] = this.ragdollRigidbodies[index].worldCenterOfMass - this.ragdollCenterOfMass;
            this.lastMasterLimbRadius[index] = this.ragdollLimbRadius[index];
            this.lastMasterRigidLCOM_Velocities[index] = Vector3.zero;
          }
          this.masterAngularVelocity = Vector3.zero;
          this.masterAngularVelocity2 = Vector3.zero;
          this.ragdollAngularVelocity = Vector3.zero;
          this.ragdollControl.numberOfCollisions = 0;
        }
      }
      return false;
    }

    public struct RagdollTransform
    {
      public Vector3 position;
      public Quaternion rotation;
      public Quaternion rotation_m;
      public Quaternion bodyRotation;
    }
  }
}
