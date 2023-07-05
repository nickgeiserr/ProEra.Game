// Decompiled with JetBrains decompiler
// Type: PBC.AdvancedMoveScript_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace PBC
{
  [RequireComponent(typeof (GetWASD_PBC))]
  [RequireComponent(typeof (HashIDs_PBC))]
  public class AdvancedMoveScript_PBC : MoveBaseClass_PBC
  {
    private Animator animator;
    private GetWASD_PBC getWASD;
    private AnimFollow_PBC animFollow;
    private RagdollControl_PBC ragdollControl;
    private FootIKBaseClass_PBC footIK;
    private HashIDs_PBC hashIDs;
    [SerializeField]
    private Transform currentPlanet;
    [SerializeField]
    private Vector3 gravitySet = Physics.gravity;
    private bool noGravity;
    private float reciDeltaTime;
    private float realCharacterScale = 1f;
    private float sqrCharacterScale = 1f;
    private float sqrtCharacterScale = 1f;
    private Vector3 gravityMin = Physics.gravity * 1.7E-06f;
    private Vector3 relativeVelocityG;
    private Vector3 relativeVelocityN;
    private float relativeVelocity_n;
    private float relativeVelocity_r;
    private Vector3 lastVelocity;
    private Vector3 actualAcc;
    private Vector3 actualAccN;
    private Vector3 lastGravity_Hat;
    private Quaternion gChange;
    private float cv;
    private bool userNeedsToFixStuff;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float accelerationInputSensitivity;
    public Transform fakeAccelerationInput;
    private Vector3 desiredAnimVelocityT;
    private Vector3 animVelocityT;
    private float animVelocityY;
    private float relAccTOverAccT = 1f;
    private float relAccNOverAccN = 1f;
    private Rigidbody iAmOnRigidbody;
    private Vector3 desiredAcc;
    private Vector3 desiredAccT;
    private Vector3 desiredAccN;
    private float characterMass;
    private float torqueLerp = 0.3f;
    private Vector3 accTotFStoredN;
    private Vector3 accTotFStoredT;
    private Vector3 accRotFStored;
    [Range(0.1f, 4f)]
    [SerializeField]
    private float frictionSet = 1f;
    [SerializeField]
    private bool useMaterialFriction;
    private Vector3 accT;
    private Vector3 accN;
    private Vector3 gravityN;
    private Vector3 gravityT;
    private float limitAcc;
    private float friction;
    private float clampAcc = 2.5f;
    [Range(0.6f, 1.3f)]
    [SerializeField]
    private float scaleStepLength = 1f;
    [Range(0.6f, 1.3f)]
    [SerializeField]
    private float animatorSpeed = 1f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float slipLerp;
    private float limitAccLerped;
    private float relativeRotationVelocity2;
    [Range(2f, 8f)]
    [SerializeField]
    private float jumpVelocity = 5f;
    [Range(0.0f, 0.3f)]
    [SerializeField]
    private float jumpResponseTime = 0.1f;
    [Range(10f, 300f)]
    private float P_Vertical = 100f;
    [Range(0.0f, 0.2f)]
    private float D_Vertical = 0.01f;
    [Range(0.0f, 0.4f)]
    private float verticalTune = 0.15f;
    private bool jump;
    private bool jumping;
    private Vector3 verticalAcc;
    private Vector3 verticalAccT;
    private Vector3 verticalAccN;
    private Vector3 accJump;
    private Vector3 accJumpT;
    private Vector3 accJumpN;
    private float jumpEagerness;
    private float lastError_Vertical2;
    private float jumpVelocity2;
    private bool underGround;
    private bool storeAutoSuspend;
    private Transform lastIAmStandingOn;
    private Transform lastLastIAmStandingOn;
    private Quaternion lastPlatformRotation;
    private Vector3 lastPlatformPosition;
    private Vector3 lastOnPlatformVelocity;
    private Vector3 onPlatformRadius;
    private Vector3 onPlatformVelocity;
    private Vector3 onPlatformAccM;
    private Vector3 onPlatformAccMN;
    private Vector3 onPlatformAccMT;
    [SerializeField]
    private bool turnTowardsVelocity;
    [Range(0.0f, 20000f)]
    private float maxRotationAcc = 10000f;
    private Vector3 rotationVelocity;
    private float relativeRotationVelocity;
    private Vector3 rotationAcc;
    private Vector3 lastDirection;
    [Range(1f, 3f)]
    [SerializeField]
    private float tameLeaning = 1.3f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float useTiltPivot = 1f;
    [Range(0.0f, 0.5f)]
    private float effectiveUpSmoothTime = 0.25f;
    [Range(0.0f, 0.5f)]
    private float effectiveUpSmoothTime2 = 0.1f;
    private Vector3 feetRelativeVelocity;
    private Vector3 effectiveUpLerped;
    private Vector3 lastEffectiveUpLerped2;
    private Vector3 effectiveUpVel;
    private Vector3 effectiveUpVel2;

    private void Awake()
    {
      this.gravity = this.gravityN = this.gravitySet;
      this.gravity_Hat = this.gravity.normalized;
      this.clampAcc *= this.gravity.magnitude;
      this.effectiveUpLerped2 = this.effectiveUpLerped = this.lastEffectiveUpLerped2 = this.transform.up;
      this.reciDeltaTime = 1f / Time.fixedDeltaTime;
      this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>();
      this.getWASD = this.GetComponent<GetWASD_PBC>();
      this.userNeedsToFixStuff = !this.WeHaveAllTheStuff();
      Main_PBC componentInChildren;
      if ((bool) (UnityEngine.Object) (componentInChildren = this.transform.root.GetComponentInChildren<Main_PBC>()))
        this.realCharacterScale = componentInChildren.realCharacterScale;
      this.sqrCharacterScale = this.realCharacterScale * this.realCharacterScale;
      this.sqrtCharacterScale = Mathf.Sqrt(this.realCharacterScale);
      this.cv = 0.005f * this.realCharacterScale;
    }

    private void Start()
    {
      if (this.userNeedsToFixStuff)
        return;
      this.lastIAmStandingOn = this.footIK.iAmStandingOn;
      this.lastPlatformPosition = this.footIK.iAmStandingOn.position;
      this.lastPlatformRotation = this.footIK.iAmStandingOn.rotation;
      this.characterMass = this.animFollow.BodyMass;
    }

    public override void DoMoveClassUpdate()
    {
      if (this.userNeedsToFixStuff)
        return;
      if (this.noGravity)
        this.gravity = this.gravityMin;
      else
        this.gravity = this.gravitySet + this.gravityMin;
      if ((bool) (UnityEngine.Object) this.currentPlanet)
        this.gravity = (this.currentPlanet.position - this.transform.position).normalized * this.gravitySet.magnitude;
      if (this.getWASD.jumpKeyDown)
        this.jump = true;
      if (this.getWASD.jumpKey)
      {
        if (!this.footIK.grounded)
          this.gravity = this.gravity * 0.8f;
        this.jumpEagerness = (double) this.jumpResponseTime <= 0.0 ? 1f : Mathf.Clamp01(this.jumpEagerness + Time.deltaTime / this.jumpResponseTime / this.sqrtCharacterScale);
      }
      else
        this.jumpEagerness = 0.0f;
      if (this.perpendicularGravity)
        this.gravity = Vector3.Lerp(this.lastGravity_Hat, -this.footIK.nRaw_Hat, 5f * Time.fixedDeltaTime) * this.gravity.magnitude;
      this.gravity_Hat = this.gravity.normalized;
    }

    public override void MoveCharacter()
    {
      if (this.userNeedsToFixStuff)
        return;
      this.gChange = Quaternion.FromToRotation(this.lastGravity_Hat, this.gravity_Hat);
      if (!this.animFollow.ragdollSuspended)
      {
        this.velocity = this.velocity + this.animFollow.VelocityCorrection;
        this.relativeVelocity = this.velocity - this.onPlatformVelocity;
        this.actualRelativeVelocityMagnitude = this.relativeVelocity.magnitude;
        this.relativeVelocity_n = Vector3.Dot(this.relativeVelocity, this.footIK.nRaw_Hat);
        this.relativeVelocityN = this.relativeVelocity_n * this.footIK.nRaw_Hat;
        this.relativeVelocityT = this.relativeVelocity - this.relativeVelocityN;
        this.relativeVelocityT_Hat = this.relativeVelocityT.normalized;
        this.relativeVelocity_r = Vector3.Dot(this.relativeVelocity, this.footIK.raycastDown_Hat);
        this.relativeVelocityG = Vector3.Dot(this.relativeVelocity, this.gravity_Hat) * this.gravity_Hat;
      }
      this.Platform();
      this.DesiredVelnAcc_PBC();
      this.Jump();
      this.Friction();
      this.Rotate();
      this.Tilt();
      this.DoGlideAndStride();
      this.Forces();
      this.actualAcc = (this.velocity - this.lastVelocity) * this.reciDeltaTime;
      this.actualAccN = Vector3.Dot(this.actualAcc, this.footIK.nRaw_Hat) * this.footIK.nRaw_Hat;
      this.lastVelocity = this.velocity;
      this.lastGravity_Hat = this.gravity_Hat;
      this.acc.x -= Mathf.Abs(this.velocity.x) * this.velocity.x * this.cv;
      this.acc.y -= Mathf.Abs(this.velocity.y) * this.velocity.y * this.cv;
      this.acc.z -= Mathf.Abs(this.velocity.z) * this.velocity.z * this.cv;
      this.velocity = this.lastVelocity + this.acc * Time.fixedDeltaTime;
      this.transform.Translate(this.velocity * Time.fixedDeltaTime, Space.World);
      this.relativeVelocity = this.velocity - this.onPlatformVelocity;
      this.relativeVelocity_n = Vector3.Dot(this.relativeVelocity, this.footIK.nRaw_Hat);
      this.relativeVelocityN = this.relativeVelocity_n * this.footIK.nRaw_Hat;
      this.relativeVelocityT = this.relativeVelocity - this.relativeVelocityN;
      this.relativeVelocityT_Hat = this.relativeVelocityT.normalized;
      this.relativeVelocity_r = Vector3.Dot(this.relativeVelocity, this.footIK.raycastDown_Hat);
      this.relativeVelocityG = Vector3.Dot(this.relativeVelocity, this.gravity_Hat) * this.gravity_Hat;
    }

    private bool WeHaveAllTheStuff()
    {
      if (!(bool) (UnityEngine.Object) (this.getWASD = this.GetComponent<GetWASD_PBC>()))
      {
        Debug.LogWarning((object) ("No script getWASD on " + this.name + "\n"));
        return false;
      }
      if (!(bool) (UnityEngine.Object) (this.hashIDs = this.GetComponent<HashIDs_PBC>()))
      {
        Debug.LogWarning((object) ("No script HashIDs on " + this.name + ".\n"));
        return false;
      }
      if (!(bool) (UnityEngine.Object) (this.animFollow = this.GetComponent<AnimFollow_PBC>()))
      {
        Debug.LogWarning((object) ("No script AnimFollow on " + this.name + ".\n"));
        return false;
      }
      if (!(bool) (UnityEngine.Object) (this.footIK = this.GetComponent<FootIKBaseClass_PBC>()))
        this.footIK = (FootIKBaseClass_PBC) this.gameObject.AddComponent<NoFootIK_PBC>();
      if (!(bool) (UnityEngine.Object) this.transform.root.GetComponentInChildren<Main_PBC>())
        Debug.LogWarning((object) ("Missing script Main on " + this.name + ".\n"));
      if (!(bool) (UnityEngine.Object) (this.animator = this.GetComponent<Animator>()))
      {
        Debug.LogWarning((object) ("No animator on " + this.name + ".\n"));
        return false;
      }
      if (!this.animator.updateMode.Equals((object) AnimatorUpdateMode.AnimatePhysics))
      {
        Debug.Log((object) ("Animator on " + this.name + " is not set to animate physics.\n"));
        this.animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
      }
      if (!this.animator.applyRootMotion.Equals(false))
      {
        Debug.Log((object) ("Animator on " + this.name + " should not apply root motion.\n"));
        this.animator.applyRootMotion = false;
      }
      if (this.animator.isHuman)
        return true;
      Debug.LogWarning((object) ("Animator rig on " + this.name + " is not human.\nAnimator will likely not work.\n"));
      return false;
    }

    private void DesiredVelnAcc_PBC()
    {
      Quaternion rotation = Quaternion.FromToRotation(this.transform.up, this.footIK.nRaw_Hat);
      this.forwardT_Hat = rotation * this.transform.forward;
      this.rightT_Hat = rotation * this.transform.right;
      if ((double) this.accelerationInputSensitivity > 0.0)
      {
        this.desiredRelativeVelocityT = Vector3.Cross(Vector3.Cross(this.gravity_Hat, this.fakeAccelerationInput.up), this.gravity_Hat) * 20f * this.accelerationInputSensitivity;
        if ((double) this.desiredRelativeVelocityT.sqrMagnitude < 0.5)
          this.desiredRelativeVelocityT = Vector3.zero;
      }
      else
        this.desiredRelativeVelocityT = this.getWASD.desiredRunSpeed * this.forwardT_Hat + this.getWASD.desiredStrafeSpeed * this.rightT_Hat;
      this.desiredAnimVelocityT = this.desiredRelativeVelocityT * this.scaleStepLength * this.animator.speed;
      this.animVelocityT = (Vector3.Dot(this.animator.deltaPosition, this.transform.forward) * this.forwardT_Hat * this.runScale + Vector3.Dot(this.animator.deltaPosition, this.transform.right) * this.rightT_Hat * this.strafeScale) * this.reciDeltaTime;
      this.animVelocityY = this.animator.deltaPosition.y * this.reciDeltaTime;
      this.desiredAccT = (this.animVelocityT - this.relativeVelocityT) * this.reciDeltaTime / this.sqrtCharacterScale;
      this.desiredAccN = Vector3.zero;
      if (this.useVerticalRootMotion)
        this.desiredAccN = (this.animVelocityY * -this.gravity_Hat - this.relativeVelocityG) * this.reciDeltaTime;
      this.desiredAcc = this.desiredAccT + this.desiredAccN;
      Vector3 normalized = this.desiredAcc.normalized;
      if (this.footIK.grounded)
      {
        this.relAccTOverAccT = this.relAccNOverAccN = 1f;
        if (!(bool) (UnityEngine.Object) this.footIK.iAmStandingOn || !(bool) (UnityEngine.Object) (this.iAmOnRigidbody = this.footIK.iAmStandingOn.GetComponent<Rigidbody>()) || this.iAmOnRigidbody.isKinematic)
          return;
        Vector3 zero = Vector3.zero;
        if ((double) this.iAmOnRigidbody.inertiaTensor.x > 0.0)
          zero.x = 1f / this.iAmOnRigidbody.inertiaTensor.x;
        if ((double) this.iAmOnRigidbody.inertiaTensor.y > 0.0)
          zero.y = 1f / this.iAmOnRigidbody.inertiaTensor.y;
        if ((double) this.iAmOnRigidbody.inertiaTensor.z > 0.0)
          zero.z = 1f / this.iAmOnRigidbody.inertiaTensor.z;
        Vector3 lhs = Vector3.Cross(this.footIK.iAmStandingOn.rotation * Vector3.Scale(Quaternion.Inverse(this.footIK.iAmStandingOn.rotation) * Vector3.Cross(this.onPlatformRadius, normalized * this.characterMass), zero), this.onPlatformRadius);
        this.relAccTOverAccT = (float) (1.0 + (double) this.characterMass / (double) this.iAmOnRigidbody.mass) + (lhs - Vector3.Dot(lhs, this.footIK.nRaw_Hat) * this.footIK.nRaw_Hat).magnitude;
        this.relAccNOverAccN = (float) (1.0 + (double) this.characterMass / (double) this.iAmOnRigidbody.mass) + Mathf.Abs(Vector3.Dot(Vector3.Cross(this.footIK.iAmStandingOn.rotation * Vector3.Scale(Quaternion.Inverse(this.footIK.iAmStandingOn.rotation) * Vector3.Cross(this.onPlatformRadius, -this.footIK.nRaw_Hat * this.characterMass), zero), this.onPlatformRadius), -this.footIK.nRaw_Hat));
      }
      else
      {
        if (this.alwaysGrounded)
          return;
        this.desiredAccT = this.desiredAccN = Vector3.zero;
      }
    }

    private void Forces()
    {
      if (!(bool) (UnityEngine.Object) this.iAmOnRigidbody)
        return;
      this.accTotFStoredN += this.accN - this.animFollow.forceStrength * this.gravityN;
      Vector3 vector3_1 = this.accTotFStoredN / this.relAccNOverAccN;
      this.accTotFStoredN -= vector3_1;
      this.accTotFStoredT += this.accT - this.animFollow.forceStrength * this.gravityT;
      Vector3 vector3_2 = this.accTotFStoredT / this.relAccTOverAccT;
      this.accTotFStoredT -= vector3_2;
      Vector3 vector3_3 = vector3_1 + vector3_2;
      this.accRotFStored += this.rotationAcc;
      Vector3 vector3_4 = this.accRotFStored * this.torqueLerp;
      this.accRotFStored -= vector3_4;
      Vector3 vector3_5 = vector3_4 * this.animFollow.forceStrength;
      Vector3 vector3_6 = vector3_3 * this.animFollow.forceStrength;
      this.iAmOnRigidbody.AddForce(this.characterMass * -vector3_6);
      this.iAmOnRigidbody.AddTorque(-vector3_5 * ((float) Math.PI / 180f) * (this.animFollow.Mr2.y + this.animFollow.InertiaIntrinsic.y) + Vector3.Cross(this.transform.position - this.footIK.iAmStandingOn.position, this.characterMass * -vector3_6));
    }

    private void Friction()
    {
      this.gravityN = Vector3.Dot(this.gravity, this.footIK.nRaw_Hat) * this.footIK.nRaw_Hat;
      this.gravityT = this.gravity - this.gravityN;
      if ((double) Vector3.Dot(this.onPlatformAccM + this.verticalAcc + this.accJump - this.gravity, this.footIK.nRaw_Hat) > 0.0)
      {
        this.friction = this.frictionSet;
        Collider component;
        if (this.useMaterialFriction && (bool) (UnityEngine.Object) (component = this.footIK.iAmStandingOn.GetComponent<Collider>()))
          this.friction = component.material.staticFriction;
        this.accT = this.desiredAccT + this.onPlatformAccMT + this.verticalAccT + this.accJumpT;
        this.accN = this.desiredAccN + this.onPlatformAccMN + this.verticalAccN + this.accJumpN;
        this.limitAcc = (float) ((double) (this.accN - this.gravityN).magnitude * (double) this.friction / ((double) (this.accT - this.gravityT).magnitude + 9.9999997473787516E-06));
        this.limitAcc = Mathf.Clamp01(this.limitAcc);
        this.accT = this.jumping ? (this.accT - this.gravityT) * this.limitAcc + this.gravityT : Vector3.ClampMagnitude((this.accT - this.gravityT) * this.limitAcc + this.gravityT, this.clampAcc * this.friction);
        this.acc = this.accT + this.accN;
      }
      else
      {
        this.accN = this.gravityN;
        this.accT = this.gravityT;
        this.acc = this.gravity;
      }
    }

    private void DoGlideAndStride()
    {
      this.animator.speed = this.animatorSpeed;
      float f1;
      float f2;
      if (false)
      {
        this.runScale = this.strafeScale = 1f;
        f1 = Vector3.Dot(this.feetRelativeVelocity, this.forwardT_Hat);
        f2 = Vector3.Dot(this.feetRelativeVelocity, this.rightT_Hat);
      }
      else
      {
        this.limitAccLerped = Mathf.Lerp(this.limitAccLerped, this.limitAcc, 0.25f);
        float num1 = Mathf.Lerp(Vector3.Dot(this.feetRelativeVelocity, this.forwardT_Hat), Vector3.Dot(this.desiredAnimVelocityT, this.forwardT_Hat), this.slipLerp + this.limitAccLerped);
        float num2 = Mathf.Lerp(Vector3.Dot(this.feetRelativeVelocity, this.rightT_Hat), Vector3.Dot(this.desiredAnimVelocityT, this.rightT_Hat), this.slipLerp + this.limitAccLerped);
        if (!this.ragdollControl.falling)
        {
          f1 = num1 / this.animator.speed * this.animFollow.forceStrength * this.animFollow.angularStrength;
          f2 = num2 / this.animator.speed * this.animFollow.forceStrength * this.animFollow.angularStrength;
        }
        else
        {
          f1 = num1 / this.animator.speed;
          f2 = num2 / this.animator.speed;
        }
        this.runScale = this.scaleStepLength;
        this.strafeScale = this.scaleStepLength;
      }
      if ((bool) (UnityEngine.Object) this.footIK.stickToTransform)
      {
        f1 = 0.0f;
        f2 = 0.0f;
        this.relativeRotationVelocity = 0.0f;
      }
      float num3 = 0.02f * this.sqrCharacterScale;
      float num4 = Mathf.Clamp01(num3 / ((float) ((double) f1 * (double) f1 + (double) f2 * (double) f2) + num3));
      float num5 = (float) (((double) Mathf.Abs(f2 * (1f - num4)) / ((double) Mathf.Abs(f2) + (double) Mathf.Abs(f1) + 9.9999997473787516E-05) - (double) num4) * 0.86599999666213989);
      float runWeight = (float) (1.0 - (double) Mathf.Abs(num5) * 1.7319999933242798);
      this.relativeRotationVelocity2 = (double) Mathf.Abs(this.relativeRotationVelocity) >= 100.0 ? Mathf.Lerp(this.relativeRotationVelocity2, this.relativeRotationVelocity, 8f * Time.fixedDeltaTime) : Mathf.Lerp(this.relativeRotationVelocity2, 0.0f, 8f * Time.fixedDeltaTime);
      this.SetAnimatorFloats(f1 / this.realCharacterScale / this.scaleStepLength, f2 / this.realCharacterScale / this.scaleStepLength, this.relativeRotationVelocity2 * this.animFollow.forceStrength * this.animFollow.angularStrength, runWeight, num5);
    }

    private void SetAnimatorFloats(
      float speedFloat,
      float strafeSpeedFloat,
      float angularSpeed,
      float runWeight,
      float strafeTurn)
    {
      float dampTime = 0.1f;
      float fixedDeltaTime = Time.fixedDeltaTime;
      this.animator.SetFloat(this.hashIDs.speedFloat, speedFloat - 0.05f, dampTime, fixedDeltaTime);
      this.animator.SetFloat(this.hashIDs.strafeSpeedFloat, strafeSpeedFloat, dampTime, fixedDeltaTime);
      this.animator.SetFloat(this.hashIDs.angularSpeedFloat, angularSpeed, dampTime, fixedDeltaTime);
      this.animator.SetFloat(this.hashIDs.strafeTurnFloat, strafeTurn, dampTime, fixedDeltaTime);
      this.animator.SetFloat(this.hashIDs.runWeightFloat, runWeight, dampTime, fixedDeltaTime);
    }

    private void Jump()
    {
      float num1 = Vector3.Dot(this.footIK.transformTarget - this.transform.position, -this.footIK.raycastDown_Hat);
      float error = (float) ((-(double) this.relativeVelocity_n * (double) this.verticalTune + (double) num1) / (1.0 + (double) this.verticalTune));
      if (this.footIK.grounded)
      {
        float signal;
        StaticStuff_PBC.PDControl(this.P_Vertical, this.D_Vertical, out signal, error, ref this.lastError_Vertical2, this.reciDeltaTime);
        this.verticalAcc = signal * -this.footIK.raycastDown_Hat / this.sqrtCharacterScale;
        if ((double) this.jumpEagerness > 0.0 && (double) this.jumpEagerness < 1.0)
        {
          if ((double) num1 < 0.20000000298023224 * (double) this.realCharacterScale)
            this.verticalAcc = this.gravity;
          this.jumpVelocity2 = this.jumpVelocity * this.jumpEagerness * this.sqrtCharacterScale;
        }
        else if (this.jump || this.jumping)
        {
          if ((double) this.jumpEagerness == 1.0)
            this.jumpVelocity2 = this.jumpVelocity * this.sqrtCharacterScale;
          Vector3 rhs = -this.gravity_Hat;
          float num2 = this.jumpVelocity2 - Vector3.Dot(this.relativeVelocity, rhs);
          this.accJump = num2 * rhs * this.reciDeltaTime;
          if ((double) num2 * (double) this.reciDeltaTime < (double) signal)
          {
            this.accJump = this.verticalAcc;
            this.jumping = false;
          }
          this.verticalAcc = this.desiredAccT = Vector3.zero;
          this.jump = false;
          this.jumping = true;
        }
        else
        {
          this.accJump = Vector3.zero;
          this.jumping = false;
        }
      }
      else
      {
        this.verticalAcc = this.alwaysGrounded ? Vector3.zero : this.gravity;
        this.lastError_Vertical2 = error;
        this.accJump = Vector3.zero;
        this.jumping = this.jump = false;
      }
      this.accJumpN = Vector3.Dot(this.accJump, this.footIK.nRaw_Hat) * this.footIK.nRaw_Hat;
      this.accJumpT = this.accJump - this.accJumpN;
      if ((double) this.relativeVelocity_r > (double) this.realCharacterScale * 10.0 && (double) this.footIK.elevation < -0.20000000298023224 * (double) this.realCharacterScale || this.underGround)
      {
        if (!this.underGround)
        {
          this.storeAutoSuspend = this.animFollow.autoSuspendRagdoll;
          this.animFollow.suspendRagdoll = true;
        }
        this.underGround = (double) Mathf.Abs(this.footIK.elevation) > 0.20000000298023224 * (double) this.realCharacterScale;
        this.animFollow.autoSuspendRagdoll = false;
        if (!this.underGround)
        {
          this.animFollow.autoSuspendRagdoll = this.storeAutoSuspend;
          this.animFollow.suspendRagdoll = false;
        }
        this.jumping = this.jump = false;
        this.verticalAcc = -(this.relativeVelocity_r * this.footIK.raycastDown_Hat + this.footIK.raycastDown_Hat * 3f) * this.reciDeltaTime;
        this.ragdollControl.shotByBullet = true;
      }
      this.verticalAccN = Vector3.Dot(this.verticalAcc, this.footIK.nRaw_Hat) * this.footIK.nRaw_Hat;
      this.verticalAccT = this.verticalAcc - this.verticalAccN;
      this.desiredAccT -= this.verticalAccT;
    }

    private void Platform()
    {
      if ((bool) (UnityEngine.Object) this.footIK.iAmStandingOn)
      {
        if ((UnityEngine.Object) this.lastIAmStandingOn == (UnityEngine.Object) this.footIK.iAmStandingOn)
        {
          this.platformDeltaRotation = this.footIK.iAmStandingOn.rotation * Quaternion.Inverse(this.lastPlatformRotation);
          this.platformDeltaRotation.ToAngleAxis(out this.platformDeltaAngle, out this.platformAxis);
          this.platformDeltaAngle *= Mathf.Sign(Vector3.Dot(this.platformAxis, -this.gravity_Hat));
          Vector3 vector3_1 = this.footIK.iAmStandingOn.position - this.lastPlatformPosition;
          this.onPlatformRadius = this.transform.position - this.footIK.iAmStandingOn.position;
          this.onPlatformDeltaPos = vector3_1 + this.platformDeltaRotation * this.onPlatformRadius - this.onPlatformRadius;
          this.onPlatformVelocity = this.onPlatformDeltaPos * this.reciDeltaTime;
          if (this.footIK.grounded)
          {
            if ((UnityEngine.Object) this.lastLastIAmStandingOn == (UnityEngine.Object) this.footIK.iAmStandingOn)
            {
              Vector3 lhs = (this.onPlatformVelocity - this.lastOnPlatformVelocity) * this.reciDeltaTime;
              Vector3 vector3_2 = Vector3.Dot(lhs, -this.footIK.raycastDown_Hat) * -this.footIK.raycastDown_Hat;
              this.onPlatformAccM = lhs - vector3_2;
              this.onPlatformAccMN = Vector3.Dot(this.onPlatformAccM, this.footIK.nRaw_Hat) * this.footIK.nRaw_Hat;
              this.onPlatformAccMT = this.onPlatformAccM - this.onPlatformAccMN;
            }
          }
          else
          {
            this.platformDeltaAngle = 0.0f;
            this.onPlatformAccMN = this.onPlatformAccMT = Vector3.zero;
          }
        }
        else
          this.accTotFStoredN = this.accTotFStoredT = this.accRotFStored = Vector3.zero;
        this.lastOnPlatformVelocity = this.onPlatformVelocity;
        this.lastPlatformPosition = this.footIK.iAmStandingOn.position;
        this.lastPlatformRotation = this.footIK.iAmStandingOn.rotation;
        this.lastLastIAmStandingOn = this.lastIAmStandingOn;
        this.lastIAmStandingOn = this.footIK.iAmStandingOn;
      }
      else
      {
        this.onPlatformVelocity = Vector3.zero;
        this.lastIAmStandingOn = this.footIK.iAmStandingOn;
        this.iAmOnRigidbody = (Rigidbody) null;
      }
    }

    private void Rotate()
    {
      float num1 = this.platformDeltaAngle;
      Vector3 lhs1 = Vector3.Cross(this.footIK.nRaw_Hat, this.platformAxis);
      Vector3 to = lhs1 - Vector3.Dot(lhs1, this.gravity_Hat) * this.gravity_Hat;
      if ((double) to.sqrMagnitude > 0.0099999997764825821 && (double) this.lastDirection.sqrMagnitude > 0.0099999997764825821)
        num1 = -Mathf.Min(Vector3.Angle(this.lastDirection, to), Mathf.Abs(this.platformDeltaAngle)) * Mathf.Sign(Vector3.Dot(this.gravity_Hat, this.platformAxis));
      if ((bool) (UnityEngine.Object) this.currentPlanet)
        num1 *= Mathf.Abs(Vector3.Dot(this.transform.up, this.platformAxis));
      this.lastDirection = to;
      if (this.turnTowardsVelocity)
        this.getWASD.deltaAngle = StaticStuff_PBC.FindAngle(this.transform.forward, this.relativeVelocity, this.transform.up) * 5f * Time.fixedDeltaTime * Mathf.Clamp01(this.relativeVelocity.sqrMagnitude);
      Vector3 lhs2 = this.getWASD.deltaAngle * this.animFollow.MasterToCOMVector_Hat - this.gravity_Hat * num1;
      Vector3 vector3 = lhs2 * this.reciDeltaTime;
      float magnitude = lhs2.magnitude;
      this.rotationVelocity = (double) magnitude <= 0.0 ? this.rotationVelocity.magnitude * this.animFollow.MasterToCOMVector_Hat * Mathf.Sign(Vector3.Dot(this.animFollow.MasterToCOMVector_Hat, this.rotationVelocity)) : this.rotationVelocity.magnitude * (lhs2 / magnitude) * Mathf.Sign(Vector3.Dot(lhs2, this.rotationVelocity));
      this.rotationAcc = (vector3 - this.rotationVelocity) * this.reciDeltaTime;
      float num2 = this.maxRotationAcc * this.friction * Mathf.Clamp01((this.actualAccN - this.gravityN).magnitude / this.gravity.magnitude);
      if ((double) this.rotationAcc.magnitude > (double) num2)
        this.rotationAcc = num2 * this.rotationAcc.normalized;
      this.rotationVelocity += this.rotationAcc * Time.fixedDeltaTime;
      this.deltaMouseRotation = Quaternion.AngleAxis(this.rotationVelocity.magnitude * Time.fixedDeltaTime, this.rotationVelocity);
      this.transform.rotation = this.deltaMouseRotation * this.transform.rotation;
      this.relativeRotationVelocity = Vector3.Dot(this.rotationVelocity, -this.gravity_Hat) - this.platformDeltaAngle * this.reciDeltaTime;
    }

    private void Tilt()
    {
      this.feetRelativeVelocity = this.relativeVelocity;
      float num = Mathf.Clamp01(this.velocity.magnitude);
      Quaternion quaternion1 = this.gChange * Quaternion.AngleAxis(Mathf.Clamp(Vector3.Angle(this.velocity, this.lastVelocity) * num, -5f, 5f) * Vector3.Dot(Vector3.Cross(this.velocity, this.lastVelocity).normalized, -this.gravity_Hat), this.gravity_Hat);
      this.effectiveUpLerped = quaternion1 * this.effectiveUpLerped;
      this.effectiveUpLerped2 = quaternion1 * this.effectiveUpLerped2;
      this.effectiveUpVel = quaternion1 * this.effectiveUpVel;
      this.effectiveUpVel2 = quaternion1 * this.effectiveUpVel2;
      this.lastEffectiveUpLerped2 = this.effectiveUpLerped2;
      Vector3 target;
      if (this.tiltAsGravity)
        target = -this.gravity_Hat;
      else if (this.tiltAsNormal)
      {
        target = this.footIK.nRaw_Hat;
      }
      else
      {
        Vector3 vector3 = this.onPlatformAccMT * this.limitAcc;
        Vector3 b = (this.actualAcc + this.acc) * 0.5f - vector3 - this.gravity * this.tameLeaning * Mathf.Max(1f, Mathf.Sqrt((float) ((double) this.friction * (double) this.gravity.magnitude / 9.8199996948242188)));
        target = !this.footIK.grounded ? -this.gravity : Vector3.Lerp(-this.gravity, b, (float) (((double) this.relativeVelocityT.magnitude + (double) (this.desiredRelativeVelocityT - this.relativeVelocityT).magnitude) * 0.079999998211860657) / this.realCharacterScale) + vector3 / this.tameLeaning;
      }
      Vector3 vector3_1 = !this.cGBalance ? this.transform.up : this.animFollow.MasterToCOMVector_Hat;
      target = target.normalized;
      if ((bool) (UnityEngine.Object) this.footIK.stickToTransform)
        target = this.footIK.stickToTransform.GetChild(0).up;
      this.effectiveUpLerped = Vector3.SmoothDamp(this.effectiveUpLerped, target, ref this.effectiveUpVel, this.effectiveUpSmoothTime * this.sqrtCharacterScale, float.PositiveInfinity, Time.fixedDeltaTime);
      if ((bool) (UnityEngine.Object) this.animFollow && !this.animFollow.ragdollSuspended)
        this.effectiveUpLerped2 = Vector3.SmoothDamp(vector3_1, this.effectiveUpLerped, ref this.effectiveUpVel2, this.effectiveUpSmoothTime2 * this.sqrtCharacterScale, float.PositiveInfinity, Time.fixedDeltaTime);
      else
        this.effectiveUpLerped2 = Vector3.SmoothDamp(this.effectiveUpLerped2, this.effectiveUpLerped, ref this.effectiveUpVel2, this.effectiveUpSmoothTime2 * this.sqrtCharacterScale, float.PositiveInfinity, Time.fixedDeltaTime);
      float t = 1f;
      if (this.footIK.grounded)
        t = Mathf.Lerp(0.0f, this.useTiltPivot, this.relativeVelocity.magnitude / this.realCharacterScale);
      if (this.ragdollControl.falling)
        return;
      Quaternion quaternion2 = Quaternion.Lerp(Quaternion.identity, Quaternion.FromToRotation(vector3_1, this.effectiveUpLerped2), this.animFollow.forceStrength * this.animFollow.angularStrength);
      Vector3 b1 = Quaternion.Inverse(this.gChange) * quaternion2 * this.animFollow.MasterToCOMVector - this.animFollow.MasterToCOMVector;
      Vector3 vector3_2 = Vector3.Lerp(-(this.gChange * Quaternion.FromToRotation(this.lastEffectiveUpLerped2, vector3_1) * this.animFollow.MasterToCOMVector - this.animFollow.MasterToCOMVector), b1, t) * this.animFollow.forceStrength * this.animFollow.angularStrength;
      Vector3 vector3_3 = vector3_2 * this.reciDeltaTime * Mathf.Clamp01(this.relativeVelocity.magnitude - 0.2f * this.realCharacterScale);
      this.desiredAnimVelocityT -= vector3_3;
      this.feetRelativeVelocity = this.relativeVelocity - vector3_3;
      this.transform.Translate(-vector3_2, Space.World);
      this.transform.rotation = quaternion2 * this.transform.rotation;
    }
  }
}
