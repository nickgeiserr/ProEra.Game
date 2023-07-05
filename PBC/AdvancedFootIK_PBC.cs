// Decompiled with JetBrains decompiler
// Type: PBC.AdvancedFootIK_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class AdvancedFootIK_PBC : FootIKBaseClass_PBC
  {
    private Animator animator;
    private MoveBaseClass_PBC moveClass;
    private AnimFollow_PBC animFollow;
    private RagdollControl_PBC ragdollControl;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float leftFootIKWeightSet = 1f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float rightFootIKWeightSet = 1f;
    [SerializeField]
    private bool usePredict = true;
    [SerializeField]
    private string[] ignoreLayers = new string[1]{ "Water" };
    [SerializeField]
    private bool useMorePredict = true;
    [SerializeField]
    private bool showRaycasts;
    [SerializeField]
    private bool disableAiredLayer;
    private float footRootRayLerp;
    private bool tripped;
    private float layerControl = 1f;
    private bool numberOfCollisionsGTZero;
    private float maxStepHeight;
    private float raycastLength;
    private float raycastHeight;
    private float tempRayLength;
    private float disLerp;
    [Range(5f, 30f)]
    private float airedLerp = 15f;
    private Transform iAmStandingOnRight;
    private Transform iAmStandingOnLeft;
    private LayerMask layerMask;
    private float reciDeltaTime;
    private float leftFootIKWeight = 1f;
    private float rightFootIKWeight = 1f;
    private float realCharacterScale = 1f;
    private float sqrtCharacterScale = 1f;
    private float footLength;
    private int airedLayerIndex;
    private Vector3 leftFootStickLPos;
    private Vector3 rightFootStickLPos;
    private bool sticking;
    [HideInInspector]
    public Transform leftToe2;
    [HideInInspector]
    public Transform leftHeel;
    [HideInInspector]
    public Transform leftFoot;
    [HideInInspector]
    public Transform leftCalf;
    [HideInInspector]
    public Transform leftThigh;
    [HideInInspector]
    public Transform rightToe2;
    [HideInInspector]
    public Transform rightHeel;
    [HideInInspector]
    public Transform rightFoot;
    [HideInInspector]
    public Transform rightCalf;
    [HideInInspector]
    public Transform rightThigh;
    private bool userNeedsToFixStuff;
    [SerializeField]
    private bool showFootPredict;
    [HideInInspector]
    public Vector3 predictPlanePos;
    [HideInInspector]
    public Vector3 predictTargetPlane;
    private Transform lastPredictFiringTransform;
    private Vector3 lastLeftPredictFiringPos;
    private Vector3 lastRightPredictFiringPos;
    private Vector3 lastLeftToePos;
    private Vector3 lastLeftHeelPos;
    private Vector3 lastRightToePos;
    private Vector3 lastRightHeelPos;
    private Vector3 movingToeHeelPos;
    private bool movedByPredict;
    private bool willTrip;
    [Range(0.0f, 2f)]
    public float StepSoundVolume = 0.5f;
    private AudioSource footSoundAudioSource;
    private float leftFootElevation;
    private float rightFootElevation;
    private float leftStepSoundTime;
    private float rightStepSoundTime;
    private bool leftFootGrounded;
    private bool rightFootGrounded;
    private float footGroundedLimit = 0.06f;
    private bool leftFootWasAired;
    private bool rightFootWasAired;
    private float footWasAiredLimit = 0.07f;
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float limitFootSnapSet = 0.03f;
    public bool disableFootFix;
    private Vector3 leftFootTargetPos;
    private Vector3 leftFootTargetNormal;
    private Vector3 lastLeftFootTargetNormal;
    private Vector3 lastLeftFootTargetPos;
    private Vector3 rightFootTargetPos;
    private Vector3 rightFootTargetNormal;
    private Vector3 lastRightFootTargetNormal;
    private Vector3 lastRightFootTargetPos;
    private Vector3 leftFootIKMove;
    private Vector3 rightFootIKMove;
    private Quaternion leftFootIKRotation;
    private Quaternion rightFootIKRotation;
    private float thighLength;
    private float thighLengthSquared;
    private float calfLength;
    private float calfLengthSquared;
    private float reciDenominator;
    private float noButtkick;
    private float maxLength;
    private Vector3 IKMissLP = Vector3.zero;
    private Vector3 IKMissRP = Vector3.zero;
    private Quaternion IKMissLR = Quaternion.identity;
    private Quaternion IKMissRR = Quaternion.identity;
    private Quaternion lastLeftFootRotation;
    private Quaternion lastRightFootRotation;
    private float footFixLimit = 0.15f;
    private bool once;
    private float predictWeight;
    private Transform movingFoot;
    private Transform movingHeel;
    private Transform movingToe;
    private Transform notMovingFoot;
    private Transform notMovingHeel;
    private Transform notMovingToe;
    private bool leftFootMoving = true;
    private bool rightFootMoving;
    private Vector3 stepVectorChangeFoot;
    private Vector3 lastStepVectorChangeFoot;
    private Vector3 stepVectorChangeToe;
    private Vector3 lastStepVectorChangeToe;
    private Vector3 stepVectorChangeHeel;
    private Vector3 lastStepVectorChangeHeel;
    private float footChangeNormTime;
    private float lastFootChangeNormTime;
    private float normTime;
    private float normTimeLeft;
    private float normTimeCurveLeft;
    private float normTimeCurveRight;
    private Vector3 lastLeftFootAnimPos;
    private Vector3 lastRightFootAnimPos;
    private Vector3 rightFootAnimDeltaPos;
    private Vector3 leftFootAnimDeltaPos;
    private float groundedLimit = 0.02f;
    private Vector3 rightDH;
    private Vector3 leftDH;
    private Vector3 futureDH;
    [HideInInspector]
    public Vector3 destinationNormal_Hat = -Physics.gravity.normalized;
    private Vector3 leftFootPosition2;
    private Vector3 rightFootPosition2;
    private float hipRadius;
    public bool toeAndHeelRays = true;
    [Range(0.0f, 0.5f)]
    private float angleReject = 0.4f;
    [Range(1f, 50f)]
    private float footNormalLerp = 20f;
    public Transform ragdollLeftFoot;
    public Transform ragdollRightFoot;
    private UnityEngine.RaycastHit raycastHitLeftFoot;
    private UnityEngine.RaycastHit raycastHitRightFoot;
    private UnityEngine.RaycastHit raycastHitTransform;
    private UnityEngine.RaycastHit raycastHitFutureFoot;
    private UnityEngine.RaycastHit raycastHitFutureToe;
    private UnityEngine.RaycastHit raycastHitFutureHeel;
    private Vector3 leftFootToeVector;
    private Vector3 rightFootToeVector;
    private Vector3 leftFootHeelVector;
    private Vector3 rightFootHeelVector;
    private Vector3 leftFootFootVector;
    private Vector3 rightFootFootVector;
    private bool futureHit;
    private bool futureHeelHigh;
    private Vector3 leftFootIKMoved;
    private Vector3 rightFootIKMoved;
    private UnityEngine.RaycastHit raycastHitToe;
    private UnityEngine.RaycastHit raycastHitHeel;
    private Vector3 transforDeltaPosition;
    private Vector3 lastTransformPosition;

    private void Awake()
    {
      if ((bool) (Object) this.GetComponent<Main_PBC>())
        this.realCharacterScale = this.GetComponent<Main_PBC>().realCharacterScale;
      this.sqrtCharacterScale = Mathf.Sqrt(this.realCharacterScale);
      this.maxStepHeight = this.realCharacterScale * 0.5f;
      this.raycastHeight = this.maxStepHeight;
      this.raycastLength = this.maxStepHeight + this.realCharacterScale;
      this.groundedLimit *= this.realCharacterScale;
      this.footGroundedLimit *= this.realCharacterScale;
      this.footWasAiredLimit *= this.realCharacterScale;
      this.limitFootSnapSet *= this.realCharacterScale;
      this.AwakeInSetup();
      this.AwakeInPredict();
      this.AwakeInMorePredict();
      this.AwakeInPositionFeet();
      this.AwakeInShootIkRays();
      if (!(bool) (Object) this.moveClass)
        return;
      this.raycastDown_Hat = -this.transform.up;
      this.nRaw_Hat = -this.moveClass.gravity_Hat;
    }

    public override void DoFootIK()
    {
      if (this.userNeedsToFixStuff)
        return;
      if ((bool) (Object) this.stickToTransform)
      {
        this.moveClass.velocity = Vector3.zero;
        if (this.ragdollControl.notTripped)
          this.StickToTransform();
        else
          this.stickToTransform = (Transform) null;
      }
      else
      {
        this.sticking = false;
        if ((bool) (Object) this.ragdollControl)
        {
          this.footRootRayLerp = this.ragdollControl.footRootRayLerp;
          this.tripped = !this.ragdollControl.notTripped;
          this.layerControl = this.ragdollControl.layerControl;
          this.numberOfCollisionsGTZero = this.ragdollControl.numberOfCollisions > 0 || !this.ragdollControl.notTripped;
        }
        this.disLerp = Mathf.Clamp01((float) (1.0 - (double) this.elevation * 4.0 / (double) this.realCharacterScale)) * Mathf.Clamp01(1f - Mathf.Pow(1f - Vector3.Dot(this.transform.up, -this.moveClass.gravity_Hat), 3f)) * this.animFollow.forceStrength;
        this.SetAiredLayer();
        this.SetRaycastDir();
        this.DoPredict();
        this.ShootIKRays();
        this.PositionFeet();
        this.PlayFootstepSound();
        this.PrepareMoveCharacter();
      }
    }

    private void SetAiredLayer()
    {
      if (this.disableAiredLayer || this.moveClass.useVerticalRootMotion)
        this.animator.SetLayerWeight(this.airedLayerIndex, 0.0f);
      else
        this.animator.SetLayerWeight(this.airedLayerIndex, Mathf.Lerp(this.animator.GetLayerWeight(this.airedLayerIndex), Mathf.Clamp(this.elevation * 0.8f / this.realCharacterScale, 0.0f, 0.8f) * this.animFollow.forceStrength * this.animFollow.angularStrength, this.airedLerp * Time.fixedDeltaTime / this.sqrtCharacterScale) * this.layerControl);
    }

    private void SetRaycastDir() => this.raycastDown_Hat = Vector3.Lerp(this.moveClass.gravity_Hat, -(this.transform.up + this.nRaw_Hat).normalized, this.disLerp);

    private void StickToTransform()
    {
      if (!this.sticking)
      {
        this.leftFootStickLPos = Quaternion.Inverse(this.transform.rotation) * (this.leftFoot.position - this.transform.position);
        this.rightFootStickLPos = Quaternion.Inverse(this.transform.rotation) * (this.rightFoot.position - this.transform.position);
        this.grounded = true;
        this.sticking = true;
      }
      Quaternion rotation = this.stickToTransform.rotation;
      this.leftFootTargetPos = this.stickToTransform.position + rotation * this.leftFootStickLPos;
      this.rightFootTargetPos = this.stickToTransform.position + rotation * this.rightFootStickLPos;
      this.leftFootTargetNormal = this.stickToTransform.up;
      this.rightFootTargetNormal = this.stickToTransform.up;
      this.transform.position = this.stickToTransform.GetChild(0).position - Mathf.Abs(Vector3.Dot(this.leftFootTargetPos - this.rightFootTargetPos, this.transform.up)) * this.transform.up;
      this.transforDeltaPosition = this.transform.position - this.lastTransformPosition;
      this.lastTransformPosition = this.transform.position;
      Vector3 up = this.transform.up;
      this.transform.rotation = this.stickToTransform.rotation;
      this.transform.rotation = Quaternion.FromToRotation(this.transform.up, up) * this.transform.rotation;
      this.PositionFeet();
    }

    private void IKRaysFoot_PBC()
    {
      if (this.showRaycasts)
        Debug.DrawRay(this.leftFootIKMoved + this.leftFootFootVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.magenta);
      if (!Physics.Raycast(this.leftFootIKMoved + this.leftFootFootVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitLeftFoot, this.raycastLength, (int) this.layerMask))
      {
        this.raycastHitLeftFoot.normal = this.leftFootTargetNormal = this.transform.up;
        this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
        this.raycastHitLeftFoot.point = this.leftFootIKMoved + this.leftFootFootVector + this.raycastDown_Hat * (this.raycastLength - this.raycastHeight);
        this.leftFootTargetPos = this.raycastHitLeftFoot.point - this.leftFootFootVector + Vector3.Dot((this.leftToe2.position + this.leftHeel.position) * 0.5f - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
        this.iAmStandingOnLeft = (Transform) null;
      }
      else
      {
        this.leftFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitLeftFoot.normal, (1f - this.normTimeCurveLeft) * this.leftFootIKWeight);
        this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
        this.leftFootTargetPos = this.raycastHitLeftFoot.point - this.leftFootFootVector + Vector3.Dot((this.leftToe2.position + this.leftHeel.position) * 0.5f - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
        this.nRaw_Hat = this.raycastHitLeftFoot.normal;
        this.iAmStandingOnLeft = this.raycastHitLeftFoot.transform;
      }
      this.tempRayLength = 0.0f;
      if (!(bool) (Object) this.raycastHitLeftFoot.transform)
        this.tempRayLength = Vector3.Dot(this.moveClass.velocity, this.raycastDown_Hat) * Time.fixedDeltaTime + this.footLength;
      if (this.showRaycasts)
        Debug.DrawRay(this.rightFootIKMoved + this.rightFootFootVector - this.raycastDown_Hat * (this.raycastHeight + this.tempRayLength), this.raycastDown_Hat * (this.raycastLength + this.tempRayLength), Color.magenta);
      Vector3 vector3_1;
      if (!Physics.Raycast(this.rightFootIKMoved + this.rightFootFootVector - this.raycastDown_Hat * (this.raycastHeight + this.tempRayLength), this.raycastDown_Hat, out this.raycastHitRightFoot, this.raycastLength + this.tempRayLength, (int) this.layerMask))
      {
        this.raycastHitRightFoot.normal = this.rightFootTargetNormal = this.transform.up;
        this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
        this.raycastHitRightFoot.point = this.rightFootIKMoved + this.rightFootFootVector + this.raycastDown_Hat * (this.raycastLength - this.raycastHeight);
        this.rightFootTargetPos = this.raycastHitRightFoot.point - this.rightFootFootVector + Vector3.Dot((this.rightToe2.position + this.rightHeel.position) * 0.5f - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
        this.nRaw_Hat = (-this.moveClass.gravity_Hat + this.nRaw_Hat).normalized;
        this.iAmStandingOnRight = (Transform) null;
      }
      else
      {
        this.rightFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitRightFoot.normal, (1f - this.normTimeCurveRight) * this.rightFootIKWeight);
        this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
        this.rightFootTargetPos = this.raycastHitRightFoot.point - this.rightFootFootVector + Vector3.Dot((this.rightToe2.position + this.rightHeel.position) * 0.5f - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
        vector3_1 = this.raycastHitRightFoot.normal + this.nRaw_Hat;
        this.nRaw_Hat = vector3_1.normalized;
        this.iAmStandingOnRight = this.raycastHitRightFoot.transform;
      }
      if (this.leftFootMoving)
        this.iAmStandingOn = this.iAmStandingOnRight;
      else
        this.iAmStandingOn = this.iAmStandingOnLeft;
      this.futureHit = false;
      if (!this.usePredict || (double) this.moveClass.relativeVelocityT.sqrMagnitude <= 0.5 || this.numberOfCollisionsGTZero)
        return;
      Vector3 vector3_2 = this.moveClass.relativeVelocityT * (this.animator.GetCurrentAnimatorStateInfo(0).length * 0.5f * this.normTimeLeft) + this.transform.rotation * this.lastStepVectorChangeFoot;
      if (this.leftFootMoving && (double) this.leftFootIKWeight > 0.0)
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.transform.position + this.leftFootFootVector + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat * this.raycastLength * 2f, Color.cyan);
        this.futureHit = true;
        if (!Physics.Raycast(this.transform.position + this.leftFootFootVector + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureFoot, this.raycastLength * 2f, (int) this.layerMask))
          this.futureHit = false;
        else if (this.useMorePredict)
        {
          this.raycastHitFutureToe.normal = this.raycastHitFutureFoot.normal;
          this.raycastHitFutureHeel.normal = this.raycastHitFutureFoot.normal;
          vector3_1 = this.leftToe2.position - this.leftHeel.position;
          Vector3 normalized = vector3_1.normalized;
          Vector3 rhs = this.leftCalf.position - this.leftFoot.position;
          Vector3 fromDirection = Vector3.Cross(Vector3.Cross(normalized, rhs), normalized);
          Vector3 vector3_3 = this.leftFootToeVector - this.leftFootFootVector;
          Vector3 vector3_4 = this.leftFootHeelVector - this.leftFootFootVector;
          Quaternion rotation = Quaternion.FromToRotation(fromDirection, this.raycastHitFutureFoot.normal);
          this.raycastHitFutureToe.point = this.raycastHitFutureFoot.point + rotation * vector3_3;
          this.raycastHitFutureHeel.point = this.raycastHitFutureFoot.point + rotation * vector3_4;
        }
      }
      else if ((double) this.rightFootIKWeight > 0.0)
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.transform.position + this.rightFootFootVector + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat * this.raycastLength * 2f, Color.cyan);
        this.futureHit = true;
        if (!Physics.Raycast(this.transform.position + this.rightFootFootVector + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureFoot, this.raycastLength * 2f, (int) this.layerMask))
          this.futureHit = false;
        else if (this.useMorePredict)
        {
          this.raycastHitFutureToe.normal = this.raycastHitFutureFoot.normal;
          this.raycastHitFutureHeel.normal = this.raycastHitFutureFoot.normal;
          vector3_1 = this.rightToe2.position - this.rightHeel.position;
          Vector3 normalized = vector3_1.normalized;
          Vector3 rhs = this.rightCalf.position - this.rightFoot.position;
          Vector3 fromDirection = Vector3.Cross(Vector3.Cross(normalized, rhs), normalized);
          Vector3 vector3_5 = this.rightFootToeVector - this.rightFootFootVector;
          Vector3 vector3_6 = this.rightFootHeelVector - this.rightFootFootVector;
          Quaternion rotation = Quaternion.FromToRotation(fromDirection, this.raycastHitFutureFoot.normal);
          this.raycastHitFutureToe.point = this.raycastHitFutureFoot.point + rotation * vector3_5;
          this.raycastHitFutureHeel.point = this.raycastHitFutureFoot.point + rotation * vector3_6;
        }
      }
      this.futureDH = Quaternion.FromToRotation(this.transform.up, this.raycastHitFutureFoot.normal) * this.rightFootFootVector - this.rightFootFootVector;
    }

    private void IKRaysToeHeel_PBC()
    {
      if (this.showRaycasts)
        Debug.DrawRay(this.leftFootIKMoved + this.leftFootHeelVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
      if (!Physics.Raycast(this.leftFootIKMoved + this.leftFootHeelVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitHeel, this.raycastLength, (int) this.layerMask))
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.leftFootIKMoved + this.leftFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
        if (!Physics.Raycast(this.leftFootIKMoved + this.leftFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitToe, this.raycastLength, (int) this.layerMask))
        {
          this.raycastHitLeftFoot.normal = this.leftFootTargetNormal = this.transform.up;
          this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
          this.raycastHitLeftFoot.point = this.leftFootIKMoved + this.leftFootFootVector + this.raycastDown_Hat * (this.raycastLength - this.raycastHeight);
          this.leftFootTargetPos = this.raycastHitLeftFoot.point - this.leftFootFootVector + Vector3.Dot((this.leftToe2.position + this.leftHeel.position) * 0.5f - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          this.iAmStandingOnLeft = (Transform) null;
        }
        else
        {
          this.raycastHitLeftFoot.normal = this.raycastHitToe.normal;
          this.leftFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitLeftFoot.normal, (1f - this.normTimeCurveLeft) * this.leftFootIKWeight);
          this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
          this.leftFootTargetPos = this.raycastHitToe.point - this.leftFootToeVector + Vector3.Dot(this.leftToe2.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          this.raycastHitLeftFoot.point = this.raycastHitToe.point + StaticStuff_PBC.Project(-this.leftFootToeVector + this.leftFootFootVector, this.raycastHitLeftFoot.normal, -this.raycastDown_Hat);
          this.nRaw_Hat = this.raycastHitLeftFoot.normal;
          this.iAmStandingOnLeft = this.raycastHitToe.transform;
        }
      }
      else if (!Physics.Raycast(this.leftFootIKMoved + this.leftFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitToe, this.raycastLength, (int) this.layerMask))
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.leftFootIKMoved + this.leftFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
        this.raycastHitLeftFoot.normal = this.raycastHitHeel.normal;
        this.leftFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitLeftFoot.normal, (1f - this.normTimeCurveLeft) * this.leftFootIKWeight);
        this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
        this.leftFootTargetPos = this.raycastHitHeel.point - this.leftFootHeelVector + Vector3.Dot(this.leftHeel.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
        this.raycastHitLeftFoot.point = this.raycastHitHeel.point + StaticStuff_PBC.Project(-this.leftFootHeelVector + this.leftFootFootVector, this.raycastHitLeftFoot.normal, -this.raycastDown_Hat);
        this.nRaw_Hat = this.raycastHitLeftFoot.normal;
        this.iAmStandingOnLeft = this.raycastHitHeel.transform;
      }
      else
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.leftFootIKMoved + this.leftFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
        bool flag1 = (double) Vector3.Dot(this.raycastHitToe.normal, -this.moveClass.gravity_Hat) > (double) this.angleReject;
        bool flag2 = (double) Vector3.Dot(this.raycastHitHeel.normal, -this.moveClass.gravity_Hat) > (double) this.angleReject;
        bool flag3 = (double) Vector3.Dot(this.raycastHitToe.point - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight;
        bool flag4 = (double) Vector3.Dot(this.raycastHitHeel.point - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight;
        if (flag1 ^ flag2 || !flag3 || !flag4)
        {
          if (flag1 & flag3)
          {
            this.raycastHitLeftFoot.normal = this.raycastHitToe.normal;
            this.leftFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitLeftFoot.normal, (1f - this.normTimeCurveLeft) * this.leftFootIKWeight);
            this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
            this.leftFootTargetPos = this.raycastHitToe.point - this.leftFootToeVector + Vector3.Dot(this.leftToe2.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
            this.raycastHitLeftFoot.point = this.raycastHitToe.point + StaticStuff_PBC.Project(-this.leftFootToeVector + this.leftFootFootVector, this.raycastHitLeftFoot.normal, -this.raycastDown_Hat);
            this.nRaw_Hat = this.raycastHitLeftFoot.normal;
            this.iAmStandingOnLeft = this.raycastHitToe.transform;
          }
          else
          {
            this.raycastHitLeftFoot.normal = this.raycastHitHeel.normal;
            this.leftFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitLeftFoot.normal, (1f - this.normTimeCurveLeft) * this.leftFootIKWeight);
            this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
            this.leftFootTargetPos = this.raycastHitHeel.point - this.leftFootHeelVector + Vector3.Dot(this.leftHeel.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
            this.raycastHitLeftFoot.point = this.raycastHitHeel.point + StaticStuff_PBC.Project(-this.leftFootHeelVector + this.leftFootFootVector, this.raycastHitLeftFoot.normal, -this.raycastDown_Hat);
            this.nRaw_Hat = this.raycastHitLeftFoot.normal;
            this.iAmStandingOnLeft = this.raycastHitHeel.transform;
          }
        }
        else
        {
          Vector3 normalized = (this.raycastHitHeel.normal + this.raycastHitToe.normal).normalized;
          Vector3 vector3 = this.raycastHitToe.point - this.raycastHitHeel.point;
          this.raycastHitLeftFoot.normal = Vector3.Lerp(Vector3.Cross(vector3, Vector3.Cross(normalized, vector3)).normalized, normalized, Mathf.Pow(Vector3.Dot(this.raycastHitHeel.normal, -this.moveClass.gravity_Hat) * Vector3.Dot(this.raycastHitToe.normal, -this.moveClass.gravity_Hat), 8f));
          this.nRaw_Hat = this.raycastHitLeftFoot.normal;
          this.leftFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitLeftFoot.normal, (1f - this.normTimeCurveLeft) * this.leftFootIKWeight);
          this.leftFootTargetNormal = Vector3.Lerp(this.lastLeftFootTargetNormal, this.leftFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
          this.leftFootTargetPos = this.raycastHitHeel.point - this.leftFootHeelVector + Vector3.Dot(this.leftHeel.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          Vector3 lhs = this.raycastHitToe.point - this.leftFootToeVector + Vector3.Dot(this.leftToe2.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          if ((double) Vector3.Dot(this.leftFootTargetPos, -this.raycastDown_Hat) < (double) Vector3.Dot(lhs, -this.raycastDown_Hat))
          {
            this.leftFootTargetPos = lhs;
            this.raycastHitLeftFoot.point = this.raycastHitToe.point + StaticStuff_PBC.Project(-this.leftFootToeVector + this.leftFootFootVector, this.raycastHitLeftFoot.normal, -this.raycastDown_Hat);
            this.iAmStandingOnLeft = this.raycastHitToe.transform;
          }
          else
          {
            this.raycastHitLeftFoot.point = this.raycastHitHeel.point + StaticStuff_PBC.Project(-this.leftFootHeelVector + this.leftFootFootVector, this.raycastHitLeftFoot.normal, -this.raycastDown_Hat);
            this.iAmStandingOnLeft = this.raycastHitHeel.transform;
          }
        }
      }
      if (this.showRaycasts)
        Debug.DrawRay(this.rightFootIKMoved + this.rightFootHeelVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
      if (!Physics.Raycast(this.rightFootIKMoved + this.rightFootHeelVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitHeel, this.raycastLength, (int) this.layerMask))
      {
        this.tempRayLength = Vector3.Dot(this.moveClass.velocity, this.raycastDown_Hat) * Time.fixedDeltaTime + this.footLength;
        if (this.showRaycasts)
          Debug.DrawRay(this.rightFootIKMoved + this.rightFootToeVector - this.raycastDown_Hat * (this.raycastHeight + this.tempRayLength), this.raycastDown_Hat * (this.raycastLength + this.tempRayLength), Color.blue);
        if (!Physics.Raycast(this.rightFootIKMoved + this.rightFootToeVector - this.raycastDown_Hat * (this.raycastHeight + this.tempRayLength), this.raycastDown_Hat, out this.raycastHitToe, this.raycastLength + this.tempRayLength, (int) this.layerMask))
        {
          this.raycastHitRightFoot.normal = this.rightFootTargetNormal = this.transform.up;
          this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
          this.raycastHitRightFoot.point = this.rightFootIKMoved + this.rightFootFootVector + this.raycastDown_Hat * (this.raycastLength - this.raycastHeight);
          this.rightFootTargetPos = this.raycastHitRightFoot.point - this.rightFootFootVector + Vector3.Dot((this.rightToe2.position + this.rightHeel.position) * 0.5f - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          this.nRaw_Hat = (-this.moveClass.gravity_Hat + this.nRaw_Hat).normalized;
          this.iAmStandingOnRight = (Transform) null;
        }
        else
        {
          this.raycastHitRightFoot.normal = this.raycastHitToe.normal;
          this.rightFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitRightFoot.normal, (1f - this.normTimeCurveRight) * this.rightFootIKWeight);
          this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
          this.rightFootTargetPos = this.raycastHitToe.point - this.rightFootToeVector + Vector3.Dot(this.rightToe2.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          this.raycastHitRightFoot.point = this.raycastHitToe.point + StaticStuff_PBC.Project(-this.rightFootToeVector + this.rightFootFootVector, this.raycastHitRightFoot.normal, -this.raycastDown_Hat);
          this.nRaw_Hat = (this.raycastHitRightFoot.normal + this.nRaw_Hat).normalized;
          this.iAmStandingOnRight = this.raycastHitToe.transform;
        }
      }
      else if (!Physics.Raycast(this.rightFootIKMoved + this.rightFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitToe, this.raycastLength, (int) this.layerMask))
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.rightFootIKMoved + this.rightFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
        this.raycastHitRightFoot.normal = this.raycastHitHeel.normal;
        this.rightFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitRightFoot.normal, (1f - this.normTimeCurveRight) * this.rightFootIKWeight);
        this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
        this.rightFootTargetPos = this.raycastHitHeel.point - this.rightFootHeelVector + Vector3.Dot(this.rightHeel.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
        this.raycastHitRightFoot.point = this.raycastHitHeel.point + StaticStuff_PBC.Project(-this.rightFootHeelVector + this.rightFootFootVector, this.raycastHitRightFoot.normal, -this.raycastDown_Hat);
        this.nRaw_Hat = (this.raycastHitRightFoot.normal + this.nRaw_Hat).normalized;
        this.iAmStandingOnRight = this.raycastHitHeel.transform;
      }
      else
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.rightFootIKMoved + this.rightFootToeVector - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.blue);
        bool flag5 = (double) Vector3.Dot(this.raycastHitToe.normal, -this.moveClass.gravity_Hat) > (double) this.angleReject;
        bool flag6 = (double) Vector3.Dot(this.raycastHitHeel.normal, -this.moveClass.gravity_Hat) > (double) this.angleReject;
        bool flag7 = (double) Vector3.Dot(this.raycastHitToe.point - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight;
        bool flag8 = (double) Vector3.Dot(this.raycastHitHeel.point - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight;
        if (flag5 ^ flag6 || !flag7 || !flag8)
        {
          if (flag5 & flag7)
          {
            this.raycastHitRightFoot.normal = this.raycastHitToe.normal;
            this.rightFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitRightFoot.normal, (1f - this.normTimeCurveRight) * this.rightFootIKWeight);
            this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
            this.rightFootTargetPos = this.raycastHitToe.point - this.rightFootToeVector + Vector3.Dot(this.rightToe2.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
            this.raycastHitRightFoot.point = this.raycastHitToe.point + StaticStuff_PBC.Project(-this.rightFootToeVector + this.rightFootFootVector, this.raycastHitRightFoot.normal, -this.raycastDown_Hat);
            this.nRaw_Hat = (this.raycastHitRightFoot.normal + this.nRaw_Hat).normalized;
            this.iAmStandingOnRight = this.raycastHitToe.transform;
          }
          else
          {
            this.raycastHitRightFoot.normal = this.raycastHitHeel.normal;
            this.rightFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitRightFoot.normal, (1f - this.normTimeCurveRight) * this.rightFootIKWeight);
            this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
            this.rightFootTargetPos = this.raycastHitHeel.point - this.rightFootHeelVector + Vector3.Dot(this.rightHeel.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
            this.raycastHitRightFoot.point = this.raycastHitHeel.point + StaticStuff_PBC.Project(-this.rightFootHeelVector + this.rightFootFootVector, this.raycastHitRightFoot.normal, -this.raycastDown_Hat);
            this.nRaw_Hat = (this.raycastHitRightFoot.normal + this.nRaw_Hat).normalized;
            this.iAmStandingOnRight = this.raycastHitHeel.transform;
          }
        }
        else
        {
          Vector3 normalized = (this.raycastHitHeel.normal + this.raycastHitToe.normal).normalized;
          Vector3 vector3 = this.raycastHitToe.point - this.raycastHitHeel.point;
          this.raycastHitRightFoot.normal = Vector3.Lerp(Vector3.Cross(vector3, Vector3.Cross(normalized, vector3)).normalized, normalized, Mathf.Pow(Vector3.Dot(this.raycastHitHeel.normal, -this.moveClass.gravity_Hat) * Vector3.Dot(this.raycastHitToe.normal, -this.moveClass.gravity_Hat), 8f));
          this.nRaw_Hat = (this.raycastHitRightFoot.normal + this.nRaw_Hat).normalized;
          this.rightFootTargetNormal = Vector3.Lerp(this.transform.up, this.raycastHitRightFoot.normal, (1f - this.normTimeCurveRight) * this.rightFootIKWeight);
          this.rightFootTargetNormal = Vector3.Lerp(this.lastRightFootTargetNormal, this.rightFootTargetNormal, this.footNormalLerp * Time.fixedDeltaTime);
          this.rightFootTargetPos = this.raycastHitHeel.point - this.rightFootHeelVector + Vector3.Dot(this.rightHeel.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          Vector3 lhs = this.raycastHitToe.point - this.rightFootToeVector + Vector3.Dot(this.rightToe2.position - this.transform.position, this.transform.up) * -this.raycastDown_Hat;
          if ((double) Vector3.Dot(this.rightFootTargetPos, -this.raycastDown_Hat) < (double) Vector3.Dot(lhs, -this.raycastDown_Hat) && (double) Vector3.Dot(lhs - this.transform.position, this.transform.up) < (double) this.maxStepHeight)
          {
            this.rightFootTargetPos = lhs;
            this.raycastHitRightFoot.point = this.raycastHitToe.point + StaticStuff_PBC.Project(-this.rightFootToeVector + this.rightFootFootVector, this.raycastHitRightFoot.normal, -this.raycastDown_Hat);
            this.iAmStandingOnRight = this.raycastHitToe.transform;
          }
          else
          {
            this.raycastHitRightFoot.point = this.raycastHitHeel.point + StaticStuff_PBC.Project(-this.rightFootHeelVector + this.rightFootFootVector, this.raycastHitRightFoot.normal, -this.raycastDown_Hat);
            this.iAmStandingOnRight = this.raycastHitHeel.transform;
          }
        }
      }
      if (this.leftFootMoving)
        this.iAmStandingOn = this.iAmStandingOnRight;
      else
        this.iAmStandingOn = this.iAmStandingOnLeft;
      this.futureHit = false;
      if (!this.usePredict || (double) this.moveClass.relativeVelocityT.sqrMagnitude <= 0.5 || this.numberOfCollisionsGTZero)
        return;
      Vector3 vector3_1 = this.moveClass.relativeVelocityT * (this.animator.GetCurrentAnimatorStateInfo(0).length * 0.5f * this.normTimeLeft);
      Vector3 vector3_2 = vector3_1 + this.transform.rotation * this.lastStepVectorChangeToe;
      Vector3 vector3_3 = vector3_1 + this.transform.rotation * this.lastStepVectorChangeHeel;
      if (this.showRaycasts)
      {
        Debug.DrawRay(this.transform.position + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat * this.raycastLength * 2f, Color.cyan);
        Debug.DrawRay(this.transform.position + vector3_3 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat * this.raycastLength * 2f, Color.cyan);
      }
      if (this.leftFootMoving && (double) this.leftFootIKWeight > 0.0)
      {
        if (!Physics.Raycast(this.transform.position + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureToe, this.raycastLength * 2f, (int) this.layerMask))
        {
          if (!Physics.Raycast(this.transform.position + vector3_3 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureHeel, this.raycastLength * 2f, (int) this.layerMask))
            return;
          Vector3 vector3_4 = this.leftToe2.position - this.leftHeel.position;
          Vector3 rhs = this.leftCalf.position - this.leftFoot.position;
          this.raycastHitFutureToe.point = this.raycastHitFutureHeel.point + Quaternion.FromToRotation(Vector3.Cross(Vector3.Cross(vector3_4, rhs), vector3_4), this.raycastHitFutureHeel.normal) * vector3_4;
          this.raycastHitFutureToe.normal = this.raycastHitFutureHeel.normal;
          this.raycastHitFutureFoot = this.raycastHitFutureHeel;
          this.raycastHitFutureFoot.point = (this.raycastHitFutureHeel.point + this.raycastHitFutureToe.point) * 0.5f;
          this.futureHeelHigh = true;
          this.futureHit = true;
        }
        else if (!Physics.Raycast(this.transform.position + vector3_3 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureHeel, this.raycastLength * 2f, (int) this.layerMask))
        {
          Vector3 vector3_5 = this.leftToe2.position - this.leftHeel.position;
          Vector3 rhs = this.leftCalf.position - this.leftFoot.position;
          this.raycastHitFutureHeel.point = this.raycastHitFutureToe.point - Quaternion.FromToRotation(Vector3.Cross(Vector3.Cross(vector3_5, rhs), vector3_5), this.raycastHitFutureToe.normal) * vector3_5;
          this.raycastHitFutureHeel.normal = this.raycastHitFutureToe.normal;
          this.raycastHitFutureFoot = this.raycastHitFutureToe;
          this.raycastHitFutureFoot.point = (this.raycastHitFutureHeel.point + this.raycastHitFutureToe.point) * 0.5f;
          this.futureHeelHigh = false;
          this.futureHit = true;
        }
        else
        {
          if ((double) Vector3.Dot(this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point, this.raycastDown_Hat) > 0.0)
          {
            this.raycastHitFutureFoot = this.raycastHitFutureHeel;
            this.raycastHitFutureFoot.point = this.raycastHitFutureHeel.point + StaticStuff_PBC.Project(this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point, this.raycastHitFutureFoot.normal, this.raycastDown_Hat) * 0.5f;
            this.futureHeelHigh = true;
          }
          else
          {
            this.raycastHitFutureFoot = this.raycastHitFutureToe;
            this.raycastHitFutureFoot.point = this.raycastHitFutureToe.point - StaticStuff_PBC.Project(this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point, this.raycastHitFutureFoot.normal, this.raycastDown_Hat) * 0.5f;
            this.futureHeelHigh = false;
          }
          this.futureHit = true;
        }
        this.futureDH = Quaternion.FromToRotation(this.transform.up, this.raycastHitFutureFoot.normal) * this.leftFootFootVector - this.leftFootFootVector;
      }
      else
      {
        if ((double) this.rightFootIKWeight <= 0.0)
          return;
        if (!Physics.Raycast(this.transform.position + vector3_2 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureToe, this.raycastLength * 2f, (int) this.layerMask))
        {
          if (!Physics.Raycast(this.transform.position + vector3_3 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureHeel, this.raycastLength * 2f, (int) this.layerMask))
            return;
          Vector3 vector3_6 = this.rightToe2.position - this.rightHeel.position;
          Vector3 rhs = this.rightCalf.position - this.rightFoot.position;
          this.raycastHitFutureToe.point = this.raycastHitFutureHeel.point + Quaternion.FromToRotation(Vector3.Cross(Vector3.Cross(vector3_6, rhs), vector3_6), this.raycastHitFutureHeel.normal) * vector3_6;
          this.raycastHitFutureToe.normal = this.raycastHitFutureHeel.normal;
          this.raycastHitFutureFoot = this.raycastHitFutureHeel;
          this.raycastHitFutureFoot.point = (this.raycastHitFutureHeel.point + this.raycastHitFutureToe.point) * 0.5f;
          this.futureHeelHigh = true;
          this.futureHit = true;
        }
        else if (!Physics.Raycast(this.transform.position + vector3_3 - this.raycastDown_Hat * this.raycastLength, this.raycastDown_Hat, out this.raycastHitFutureHeel, this.raycastLength * 2f, (int) this.layerMask))
        {
          Vector3 vector3_7 = this.rightToe2.position - this.rightHeel.position;
          Vector3 rhs = this.rightCalf.position - this.rightFoot.position;
          this.raycastHitFutureHeel.point = this.raycastHitFutureToe.point - Quaternion.FromToRotation(Vector3.Cross(Vector3.Cross(vector3_7, rhs), vector3_7), this.raycastHitFutureToe.normal) * vector3_7;
          this.raycastHitFutureHeel.normal = this.raycastHitFutureToe.normal;
          this.raycastHitFutureFoot = this.raycastHitFutureToe;
          this.raycastHitFutureFoot.point = (this.raycastHitFutureHeel.point + this.raycastHitFutureToe.point) * 0.5f;
          this.futureHeelHigh = false;
          this.futureHit = true;
        }
        else
        {
          if ((double) Vector3.Dot(this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point, this.raycastDown_Hat) > 0.0)
          {
            this.raycastHitFutureFoot = this.raycastHitFutureHeel;
            this.raycastHitFutureFoot.point = this.raycastHitFutureHeel.point + StaticStuff_PBC.Project(this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point, this.raycastHitFutureFoot.normal, this.raycastDown_Hat) * 0.5f;
            this.futureHeelHigh = true;
          }
          else
          {
            this.raycastHitFutureFoot = this.raycastHitFutureToe;
            this.raycastHitFutureFoot.point = this.raycastHitFutureToe.point - StaticStuff_PBC.Project(this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point, this.raycastHitFutureFoot.normal, this.raycastDown_Hat) * 0.5f;
            this.futureHeelHigh = false;
          }
          this.futureHit = true;
        }
        this.futureDH = Quaternion.FromToRotation(this.transform.up, this.raycastHitFutureFoot.normal) * this.rightFootFootVector - this.rightFootFootVector;
      }
    }

    private void AwakeInSetup()
    {
      this.reciDeltaTime = 1f / Time.fixedDeltaTime;
      this.SetIgnoreLayers();
      if (this.userNeedsToFixStuff = !this.WeHaveAllTheStuff())
        return;
      Vector3 vector3;
      if ((double) this.hipRadius == 0.0)
      {
        vector3 = this.rightThigh.position - this.leftThigh.position;
        this.hipRadius = vector3.magnitude * 0.5f;
      }
      vector3 = this.leftToe2.position - this.leftHeel.position;
      this.footLength = vector3.magnitude;
    }

    private void SetIgnoreLayers()
    {
      foreach (string ignoreLayer in this.ignoreLayers)
        this.layerMask = (LayerMask) ((int) this.layerMask | 1 << LayerMask.NameToLayer(ignoreLayer));
      this.layerMask = (LayerMask) ~(int) this.layerMask;
    }

    private bool WeHaveAllTheStuff()
    {
      if (!Physics.Raycast(this.transform.position + this.transform.up, -this.transform.up, out this.raycastHitTransform, 500f, (int) this.layerMask))
      {
        Debug.LogWarning((object) ("Bad spawn of " + this.name + ".\n Spawn character closer to ground or platform.\n"));
        return false;
      }
      this.iAmStandingOn = this.iAmStandingOnRight = this.iAmStandingOnLeft = this.raycastHitTransform.transform;
      if (!(bool) (Object) this.leftThigh || !(bool) (Object) this.leftCalf || !(bool) (Object) this.leftFoot || !(bool) (Object) this.leftToe2 || !(bool) (Object) this.rightThigh || !(bool) (Object) this.rightCalf || !(bool) (Object) this.rightFoot || !(bool) (Object) this.rightToe2)
      {
        this.gameObject.AddComponent<AutoAssignIKLegs_PBC>();
        Debug.Log((object) ("Drag and drop script AutoAssignIKLegs_PBC onto " + this.name + "\n"));
        if (!(bool) (Object) this.leftThigh || !(bool) (Object) this.leftCalf || !(bool) (Object) this.leftFoot || !(bool) (Object) this.leftToe2 || !(bool) (Object) this.rightThigh || !(bool) (Object) this.rightCalf || !(bool) (Object) this.rightFoot || !(bool) (Object) this.rightToe2)
          Debug.Log((object) ("Script AutoAssignIKLegs_PBC failed on " + this.name + ".\nNo need to drop it there again\n"));
      }
      if (!(bool) (Object) (this.animFollow = this.GetComponent<AnimFollow_PBC>()))
      {
        Debug.LogWarning((object) ("No script AnimFollow on " + this.name + ".\n"));
        return false;
      }
      if (!(bool) (Object) this.ragdollLeftFoot || !(bool) (Object) this.ragdollRightFoot)
      {
        Rigidbody[] componentsInChildren = this.transform.root.GetComponentsInChildren<Rigidbody>();
        for (int index = 0; index < componentsInChildren.Length; ++index)
        {
          if (!(bool) (Object) this.ragdollLeftFoot && componentsInChildren[index].name == this.leftFoot.name)
          {
            this.ragdollLeftFoot = componentsInChildren[index].transform;
            if ((bool) (Object) this.ragdollRightFoot)
              break;
          }
          else if (!(bool) (Object) this.ragdollRightFoot && componentsInChildren[index].name == this.rightFoot.name)
          {
            this.ragdollRightFoot = componentsInChildren[index].transform;
            if ((bool) (Object) this.ragdollLeftFoot)
              break;
          }
        }
        if (!(bool) (Object) this.ragdollLeftFoot || !(bool) (Object) this.ragdollRightFoot)
          Debug.LogWarning((object) ("Auto assigning of fields ragdoll feet in script AdvancedFootIK failed on " + this.name + ".\n"));
      }
      if (!(bool) (Object) (this.animator = this.GetComponent<Animator>()))
      {
        Debug.LogWarning((object) ("Missing animator on " + this.name + "\n"));
        return false;
      }
      if (-1 == (this.airedLayerIndex = this.animator.GetLayerIndex("Aired")))
        Debug.LogWarning((object) ("No layer named \"Aired\" on animator of " + this.name + "\n"));
      if (!(bool) (Object) (this.moveClass = this.GetComponent<MoveBaseClass_PBC>()))
      {
        Debug.LogWarning((object) ("No script AdvancedMoveClass on " + this.name + "\n"));
        return false;
      }
      if (!(bool) (Object) (this.footSoundAudioSource = this.GetComponent<AudioSource>()))
      {
        this.footSoundAudioSource = this.gameObject.AddComponent<AudioSource>();
        this.footSoundAudioSource.playOnAwake = false;
        this.footSoundAudioSource.spatialBlend = 1f;
      }
      if (!(bool) (Object) this.footSoundAudioSource.clip && !(bool) (Object) (this.footSoundAudioSource.clip = (AudioClip) Resources.Load("defaultFootstepSound", typeof (AudioClip))))
        Debug.LogWarning((object) ("Could not load AudioClip named \"defaultFootstepSound\" on " + this.name + ".\nIt must be in a folder named \"Resources\".\n"));
      int num = (bool) (Object) (this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>()) ? 1 : 0;
      if (!(bool) (Object) this.transform.root.GetComponentInChildren<Main_PBC>())
        Debug.LogWarning((object) ("Missing script Main on " + this.name + "\n"));
      foreach (Collider componentsInChild in this.transform.root.GetComponentsInChildren<Collider>())
      {
        bool flag = false;
        foreach (string ignoreLayer in this.ignoreLayers)
        {
          if (componentsInChild.gameObject.layer.Equals(LayerMask.NameToLayer(ignoreLayer)))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          Debug.LogWarning((object) ("Layer for all colliders on " + componentsInChild.name + " must be set to an ignored layer\n"));
      }
      return true;
    }

    private void AwakeInMorePredict()
    {
      this.lastLeftPredictFiringPos = this.leftToe2.position;
      this.lastRightPredictFiringPos = this.rightToe2.position;
    }

    private void DoMorePredict()
    {
      Vector3 lhs1 = this.leftToe2.position - this.leftHeel.position;
      Vector3 lhs2 = this.rightToe2.position - this.rightHeel.position;
      Vector3 start;
      Vector3 vector3_1;
      Transform transform;
      if (this.leftFootMoving)
      {
        start = this.lastLeftPredictFiringPos;
        if ((double) Vector3.Dot(lhs1, this.predictTargetPlane) < 0.0)
        {
          vector3_1 = this.raycastHitFutureToe.point;
          if (this.futureHeelHigh)
            vector3_1 = this.raycastHitFutureHeel.point + Vector3.Cross(Vector3.Cross(this.raycastHitFutureHeel.normal, this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point), this.raycastHitFutureHeel.normal).normalized * this.footLength;
          transform = this.leftToe2;
          this.movingToeHeelPos = this.movingToe.position;
          if (!this.movedByPredict || (Object) this.lastPredictFiringTransform != (Object) transform)
            start = this.lastLeftToePos;
        }
        else
        {
          vector3_1 = this.raycastHitFutureHeel.point;
          if (!this.futureHeelHigh)
            vector3_1 = this.raycastHitFutureToe.point - Vector3.Cross(Vector3.Cross(this.raycastHitFutureToe.normal, this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point), this.raycastHitFutureToe.normal).normalized * this.footLength;
          transform = this.leftHeel;
          this.movingToeHeelPos = this.movingHeel.position;
          if (!this.movedByPredict || (Object) this.lastPredictFiringTransform != (Object) transform)
            start = this.lastLeftHeelPos;
        }
      }
      else
      {
        start = this.lastRightPredictFiringPos;
        if ((double) Vector3.Dot(lhs2, this.predictTargetPlane) < 0.0)
        {
          vector3_1 = this.raycastHitFutureToe.point;
          if (this.futureHeelHigh)
            vector3_1 = this.raycastHitFutureHeel.point + Vector3.Cross(Vector3.Cross(this.raycastHitFutureHeel.normal, this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point), this.raycastHitFutureHeel.normal).normalized * this.footLength;
          transform = this.rightToe2;
          this.movingToeHeelPos = this.movingToe.position;
          if (!this.movedByPredict || (Object) this.lastPredictFiringTransform != (Object) transform)
            start = this.lastRightToePos;
        }
        else
        {
          vector3_1 = this.raycastHitFutureHeel.point;
          if (!this.futureHeelHigh)
            vector3_1 = this.raycastHitFutureToe.point - Vector3.Cross(Vector3.Cross(this.raycastHitFutureToe.normal, this.raycastHitFutureToe.point - this.raycastHitFutureHeel.point), this.raycastHitFutureToe.normal).normalized * this.footLength;
          transform = this.rightHeel;
          this.movingToeHeelPos = this.movingHeel.position;
          if (!this.movedByPredict || (Object) this.lastPredictFiringTransform != (Object) transform)
            start = this.lastRightHeelPos;
        }
      }
      this.lastPredictFiringTransform = transform;
      this.willTrip = false;
      if (this.showRaycasts)
        Debug.DrawLine(start, vector3_1 - this.raycastDown_Hat * (1f / 1000f), Color.yellow);
      UnityEngine.RaycastHit hitInfo1;
      Vector3 vector3_2;
      if (Physics.Linecast(start, vector3_1 - this.raycastDown_Hat * (1f / 1000f), out hitInfo1, (int) this.layerMask))
      {
        Vector3 rhs = hitInfo1.point - start;
        float magnitude = rhs.magnitude;
        float maxDistance = (double) magnitude > (double) this.maxStepHeight ? magnitude : this.maxStepHeight;
        Vector3 normalized = Vector3.Cross(Vector3.Cross(-this.raycastDown_Hat, rhs), hitInfo1.normal).normalized;
        if (this.showRaycasts)
          Debug.DrawRay(hitInfo1.point + normalized * maxDistance - hitInfo1.normal * (1f / 1000f), -normalized * maxDistance, Color.yellow);
        UnityEngine.RaycastHit hitInfo2;
        if (!Physics.Raycast(hitInfo1.point + normalized * maxDistance - hitInfo1.normal * (1f / 1000f), -normalized, out hitInfo2, maxDistance, (int) this.layerMask))
        {
          if ((double) Vector3.Dot(vector3_1 - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight)
          {
            vector3_2 = vector3_1 - start;
            this.predictTargetPlane = Vector3.Cross(Vector3.Cross(vector3_2, -this.raycastDown_Hat), vector3_2).normalized;
            this.predictPlanePos = vector3_1;
          }
          else
          {
            this.willTrip = true;
            vector3_2 = this.moveClass.relativeVelocityT;
            this.predictTargetPlane = Vector3.Cross(Vector3.Cross(vector3_2, -this.raycastDown_Hat), vector3_2).normalized;
            this.predictPlanePos = start + this.moveClass.relativeVelocityT_Hat;
          }
        }
        else if ((double) Vector3.Dot(hitInfo2.point - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight)
        {
          vector3_2 = hitInfo2.point - start;
          this.predictTargetPlane = Vector3.Cross(Vector3.Cross(hitInfo2.normal, hitInfo1.normal), vector3_2).normalized;
          this.predictPlanePos = hitInfo2.point;
        }
        else
        {
          this.willTrip = true;
          vector3_2 = this.moveClass.relativeVelocityT;
          this.predictTargetPlane = Vector3.Cross(Vector3.Cross(vector3_2, -this.raycastDown_Hat), vector3_2).normalized;
          this.predictPlanePos = start + this.moveClass.relativeVelocityT_Hat;
        }
      }
      else if ((double) Vector3.Dot(vector3_1 - this.transform.position, -this.raycastDown_Hat) < (double) this.maxStepHeight)
      {
        vector3_2 = vector3_1 - start;
        this.predictTargetPlane = Vector3.Cross(Vector3.Cross(vector3_2, -this.raycastDown_Hat), vector3_2).normalized;
        this.predictPlanePos = vector3_1;
      }
      else
      {
        this.willTrip = true;
        vector3_2 = this.moveClass.relativeVelocityT;
        this.predictTargetPlane = Vector3.Cross(Vector3.Cross(vector3_2, -this.raycastDown_Hat), vector3_2).normalized;
        this.predictPlanePos = start + this.moveClass.relativeVelocityT_Hat;
      }
      if (this.showFootPredict)
        Debug.DrawRay(start, vector3_2, Color.green);
      float num = Vector3.Dot(this.predictPlanePos - this.movingToeHeelPos, this.predictTargetPlane);
      this.movedByPredict = false;
      if ((double) num <= 0.0)
        return;
      Vector3 vector3_3 = num / Vector3.Dot(-this.raycastDown_Hat, this.predictTargetPlane) * -this.raycastDown_Hat;
      Vector3 vector3_4 = vector3_3 * Mathf.Clamp01(Vector3.Dot((this.movingToeHeelPos + vector3_3 - start).normalized, vector3_2.normalized));
      if (this.leftFootMoving)
      {
        this.leftFootTargetPos += vector3_4;
        this.lastLeftPredictFiringPos = this.movingToeHeelPos + vector3_4;
      }
      else
      {
        this.rightFootTargetPos += vector3_4;
        this.lastRightPredictFiringPos = this.movingToeHeelPos + vector3_4;
      }
      this.movedByPredict = true;
    }

    private void PlayFootstepSound()
    {
      if (!(bool) (Object) this.footSoundAudioSource)
        return;
      this.leftFootElevation = Vector3.Dot(-this.raycastDown_Hat, this.leftFoot.position - this.IKMissLP + this.leftFootFootVector - this.raycastHitLeftFoot.point);
      this.rightFootElevation = Vector3.Dot(-this.raycastDown_Hat, this.rightFoot.position - this.IKMissRP + this.rightFootFootVector - this.raycastHitRightFoot.point);
      this.leftFootGrounded = (double) this.leftFootElevation < (double) this.footGroundedLimit;
      this.rightFootGrounded = (double) this.rightFootElevation < (double) this.footGroundedLimit;
      if (this.leftFootGrounded && !this.leftFootMoving && this.leftFootWasAired && (double) Time.fixedTime > (double) this.leftStepSoundTime)
      {
        this.footSoundAudioSource.volume = (float) ((double) this.moveClass.relativeVelocity.magnitude * 0.039999999105930328 + 0.20000000298023224) * this.StepSoundVolume;
        this.footSoundAudioSource.Play();
        this.leftStepSoundTime = Time.fixedTime + 0.3f;
        this.leftFootWasAired = false;
      }
      else if ((double) this.leftFootElevation > (double) this.footWasAiredLimit)
        this.leftFootWasAired = true;
      if (this.rightFootGrounded && !this.rightFootMoving && this.rightFootWasAired && (double) Time.fixedTime > (double) this.rightStepSoundTime)
      {
        this.footSoundAudioSource.volume = (float) ((double) this.moveClass.relativeVelocity.magnitude * 0.039999999105930328 + 0.20000000298023224) * this.StepSoundVolume;
        this.footSoundAudioSource.Play();
        this.rightStepSoundTime = Time.fixedTime + 0.3f;
        this.rightFootWasAired = false;
      }
      else
      {
        if ((double) this.rightFootElevation <= (double) this.footWasAiredLimit)
          return;
        this.rightFootWasAired = true;
      }
    }

    private void AwakeInPositionFeet()
    {
      this.thighLength = (this.rightThigh.position - this.rightCalf.position).magnitude;
      this.thighLengthSquared = (this.rightThigh.position - this.rightCalf.position).sqrMagnitude;
      this.calfLength = (this.rightCalf.position - this.rightFoot.position).magnitude;
      this.calfLengthSquared = (this.rightCalf.position - this.rightFoot.position).sqrMagnitude;
      this.reciDenominator = -0.5f / this.calfLength / this.thighLength;
      this.noButtkick = (float) (2.0 * (double) Mathf.Abs(this.thighLength - this.calfLength) + 0.10000000149011612);
      this.maxLength = (float) ((double) this.calfLength + (double) this.thighLength - 0.0099999997764825821);
      this.lastLeftFootTargetPos = this.leftFoot.position;
      this.lastLeftFootTargetNormal = this.transform.up;
      this.lastRightFootTargetPos = this.rightFoot.position;
      this.lastRightFootTargetNormal = this.transform.up;
      this.lastLeftFootRotation = this.leftFoot.rotation;
      this.lastRightFootRotation = this.rightFoot.rotation;
      this.footFixLimit *= this.realCharacterScale;
    }

    private void PositionFeet()
    {
      Vector3 position1 = this.leftFoot.position;
      Vector3 position2 = this.rightFoot.position;
      Quaternion rotation1 = this.leftFoot.rotation;
      Quaternion rotation2 = this.rightFoot.rotation;
      if (this.once && this.animFollow.ragdollSuspended)
      {
        this.once = false;
        this.IKMissLP = Vector3.zero;
        this.IKMissRP = Vector3.zero;
        this.IKMissLR = Quaternion.identity;
        this.IKMissRR = Quaternion.identity;
      }
      if (!this.sticking && this.futureHit && this.useMorePredict)
      {
        this.leftFoot.position = this.leftFootTargetPos;
        this.leftFoot.rotation = Quaternion.FromToRotation(this.transform.up, this.leftFootTargetNormal) * rotation1;
        this.rightFoot.position = this.rightFootTargetPos;
        this.rightFoot.rotation = Quaternion.FromToRotation(this.transform.up, this.rightFootTargetNormal) * rotation2;
        this.DoMorePredict();
        this.leftFoot.position = position1;
        this.leftFoot.rotation = rotation1;
        this.rightFoot.position = position2;
        this.rightFoot.rotation = rotation2;
      }
      float limitFootSnapSet = this.limitFootSnapSet;
      if ((bool) (Object) this.moveClass && !(bool) (Object) this.stickToTransform)
      {
        float num = limitFootSnapSet + this.moveClass.relativeVelocity.magnitude * Time.fixedDeltaTime;
        Vector3 vector3 = (double) this.transforDeltaPosition.sqrMagnitude > (double) this.moveClass.onPlatformDeltaPos.sqrMagnitude ? this.transforDeltaPosition : this.moveClass.onPlatformDeltaPos;
        this.leftFootTargetPos = Vector3.Lerp(this.leftFoot.position, this.leftFootTargetPos, this.leftFootIKWeight);
        float f1 = Vector3.Dot(this.leftFootTargetPos - this.lastLeftFootTargetPos - this.leftFootAnimDeltaPos - vector3, -this.raycastDown_Hat);
        if ((double) Mathf.Abs(f1) > (double) num)
          this.leftFootTargetPos += (num * Mathf.Sign(f1) - f1) * -this.raycastDown_Hat;
        this.rightFootTargetPos = Vector3.Lerp(this.rightFoot.position, this.rightFootTargetPos, this.rightFootIKWeight);
        float f2 = Vector3.Dot(this.rightFootTargetPos - this.lastRightFootTargetPos - this.rightFootAnimDeltaPos - vector3, -this.raycastDown_Hat);
        if ((double) Mathf.Abs(f2) > (double) num)
          this.rightFootTargetPos += (num * Mathf.Sign(f2) - f2) * -this.raycastDown_Hat;
      }
      if (!this.animFollow.ragdollSuspended)
      {
        if (!this.disableFootFix)
        {
          this.IKMissLP = Mathf.Clamp(Vector3.Dot(this.lastLeftFootTargetPos - this.ragdollLeftFoot.position + this.IKMissLP, this.leftFootTargetNormal), -this.footFixLimit, this.footFixLimit) * this.leftFootTargetNormal * this.animFollow.forceStrength;
          this.IKMissRP = Mathf.Clamp(Vector3.Dot(this.lastRightFootTargetPos - this.ragdollRightFoot.position + this.IKMissRP, this.rightFootTargetNormal), -this.footFixLimit, this.footFixLimit) * this.rightFootTargetNormal * this.animFollow.forceStrength;
          this.IKMissLR = this.lastLeftFootRotation * Quaternion.Inverse(this.ragdollLeftFoot.rotation);
          this.IKMissRR = this.lastRightFootRotation * Quaternion.Inverse(this.ragdollRightFoot.rotation);
          this.IKMissLR = Quaternion.RotateTowards(Quaternion.identity, this.IKMissLR, 30f * this.animFollow.forceStrength);
          this.IKMissRR = Quaternion.RotateTowards(Quaternion.identity, this.IKMissRR, 30f * this.animFollow.forceStrength);
        }
        else
        {
          this.IKMissLP = this.IKMissRP = Vector3.zero;
          this.IKMissLR = this.IKMissRR = Quaternion.identity;
        }
        this.once = true;
      }
      Vector3 vector3_1 = this.leftFootTargetPos + this.IKMissLP - this.leftThigh.position;
      Vector3 normalized1 = vector3_1.normalized;
      float num1 = vector3_1.magnitude;
      if ((double) num1 > (double) this.maxLength)
      {
        num1 = this.maxLength;
        if (!this.disableFootFix)
          this.IKMissLP -= vector3_1 - normalized1 * this.maxLength;
      }
      if ((double) num1 < (double) this.noButtkick)
      {
        num1 = this.noButtkick;
        this.IKMissLP -= vector3_1 - normalized1 * this.noButtkick;
      }
      float num2 = Mathf.Acos((num1 * num1 - this.calfLengthSquared - this.thighLengthSquared) * this.reciDenominator) * 57.29578f;
      Quaternion rotation3 = Quaternion.FromToRotation(this.leftCalf.position - this.leftThigh.position, this.leftFoot.position - this.leftCalf.position);
      float angle;
      Vector3 axis;
      rotation3.ToAngleAxis(out angle, out axis);
      if ((double) angle > 180.0)
      {
        angle = 360f - angle;
        axis *= -1f;
      }
      this.leftCalf.Rotate(axis, 180f - num2 - angle, Space.World);
      this.leftThigh.rotation = Quaternion.FromToRotation(this.leftFoot.position - this.leftThigh.position, this.leftFootTargetPos + this.IKMissLP - this.leftThigh.position) * this.leftThigh.rotation;
      Vector3 vector3_2 = this.rightFootTargetPos + this.IKMissRP - this.rightThigh.position;
      Vector3 normalized2 = vector3_2.normalized;
      float num3 = vector3_2.magnitude;
      if ((double) num3 > (double) this.maxLength)
      {
        num3 = this.maxLength;
        if (!this.disableFootFix)
          this.IKMissRP -= vector3_2 - vector3_2.normalized * this.maxLength;
      }
      if ((double) num3 < (double) this.noButtkick)
      {
        num3 = this.noButtkick;
        this.IKMissLP -= vector3_2 - normalized2 * this.noButtkick;
      }
      float num4 = Mathf.Acos((num3 * num3 - this.calfLengthSquared - this.thighLengthSquared) * this.reciDenominator) * 57.29578f;
      rotation3 = Quaternion.FromToRotation(this.rightCalf.position - this.rightThigh.position, this.rightFoot.position - this.rightCalf.position);
      rotation3.ToAngleAxis(out angle, out axis);
      if ((double) angle > 180.0)
      {
        angle = 360f - angle;
        axis *= -1f;
      }
      this.rightCalf.Rotate(axis, 180f - num4 - angle, Space.World);
      this.rightThigh.rotation = Quaternion.FromToRotation(this.rightFoot.position - this.rightThigh.position, this.rightFootTargetPos + this.IKMissRP - this.rightThigh.position) * this.rightThigh.rotation;
      if (!this.animFollow.ragdollSuspended)
      {
        this.leftFootIKMove = Vector3.Dot(this.ragdollLeftFoot.position - position1, -this.raycastDown_Hat) * -this.raycastDown_Hat;
        this.rightFootIKMove = Vector3.Dot(this.ragdollRightFoot.position - position2, -this.raycastDown_Hat) * -this.raycastDown_Hat;
      }
      else
      {
        this.IKMissLP = this.IKMissRP = Vector3.zero;
        this.leftFootIKMove = Vector3.Dot(this.leftFoot.position - position1, -this.raycastDown_Hat) * -this.raycastDown_Hat;
        this.rightFootIKMove = Vector3.Dot(this.leftFoot.position - position2, -this.raycastDown_Hat) * -this.raycastDown_Hat;
      }
      this.leftFootIKRotation = Quaternion.FromToRotation(this.transform.up, this.leftFootTargetNormal);
      this.leftFoot.rotation = this.IKMissLR * this.leftFootIKRotation * rotation1;
      this.rightFootIKRotation = Quaternion.FromToRotation(this.transform.up, this.rightFootTargetNormal);
      this.rightFoot.rotation = this.IKMissRR * this.rightFootIKRotation * rotation2;
      this.lastLeftToePos = this.leftToe2.position - this.IKMissLP;
      this.lastLeftHeelPos = this.leftHeel.position - this.IKMissLP;
      this.lastRightToePos = this.rightToe2.position - this.IKMissRP;
      this.lastRightHeelPos = this.rightHeel.position - this.IKMissRP;
      this.lastLeftFootTargetPos = this.leftFootTargetPos;
      this.lastLeftFootTargetNormal = this.leftFootTargetNormal;
      this.lastRightFootTargetPos = this.rightFootTargetPos;
      this.lastRightFootTargetNormal = this.rightFootTargetNormal;
      this.lastLeftFootRotation = this.leftFoot.rotation;
      this.lastRightFootRotation = this.rightFoot.rotation;
    }

    private void AwakeInPredict()
    {
      this.movingFoot = this.leftFoot;
      this.movingHeel = this.leftHeel;
      this.notMovingHeel = this.rightHeel;
      this.movingToe = this.leftToe2;
      this.notMovingToe = this.rightToe2;
    }

    private void DoPredict()
    {
      float num = 1f;
      this.predictWeight = Mathf.Clamp01((float) (1.0 - (double) (this.moveClass.desiredRelativeVelocityT - this.moveClass.relativeVelocityT).magnitude / ((double) this.moveClass.desiredRelativeVelocityT.magnitude + 9.9999997473787516E-06)));
      if (this.animator.IsInTransition(0))
        this.footChangeNormTime = 0.0f;
      Vector3 vector3_1 = this.leftToe2.position - this.transform.position;
      this.leftFootAnimDeltaPos = vector3_1 - this.lastLeftFootAnimPos;
      Vector3 lhs1 = (this.leftFootAnimDeltaPos + this.animator.deltaPosition) * this.reciDeltaTime;
      Vector3 vector3_2 = lhs1 - Vector3.Dot(lhs1, this.transform.up) * this.transform.up;
      this.lastLeftFootAnimPos = vector3_1;
      Vector3 vector3_3 = this.rightToe2.position - this.transform.position;
      this.rightFootAnimDeltaPos = vector3_3 - this.lastRightFootAnimPos;
      Vector3 lhs2 = (this.rightFootAnimDeltaPos + this.animator.deltaPosition) * this.reciDeltaTime;
      Vector3 vector3_4 = lhs2 - Vector3.Dot(lhs2, this.transform.up) * this.transform.up;
      this.lastRightFootAnimPos = vector3_3;
      Vector3 vector3_5;
      Vector3 vector3_6;
      if (this.leftFootMoving)
      {
        vector3_5 = vector3_2;
        vector3_6 = vector3_4;
      }
      else
      {
        vector3_5 = vector3_4;
        vector3_6 = vector3_2;
      }
      if ((double) vector3_6.magnitude / ((double) vector3_5.magnitude + 0.10000000149011612) > 1.0 && (double) this.normTime > 0.10000000149011612)
      {
        this.movingFoot = (double) vector3_2.sqrMagnitude > (double) vector3_4.sqrMagnitude ? this.leftFoot : this.rightFoot;
        this.lastFootChangeNormTime = this.footChangeNormTime;
        this.footChangeNormTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        num = (float) (0.5 / (1.0 - (double) this.footChangeNormTime + (double) this.lastFootChangeNormTime));
        if ((double) num < 0.800000011920929 || (double) num > 1.25)
          num = 1f;
        if ((Object) this.movingFoot == (Object) this.leftFoot)
        {
          this.leftFootMoving = true;
          this.rightFootMoving = false;
          this.movingToe = this.leftToe2;
          this.movingHeel = this.leftHeel;
          this.notMovingToe = this.rightToe2;
          this.notMovingHeel = this.rightHeel;
          this.movingFoot = this.leftFoot;
          this.notMovingFoot = this.rightFoot;
        }
        else
        {
          this.rightFootMoving = true;
          this.leftFootMoving = false;
          this.movingToe = this.rightToe2;
          this.movingHeel = this.rightHeel;
          this.notMovingToe = this.leftToe2;
          this.notMovingHeel = this.leftHeel;
          this.movingFoot = this.rightFoot;
          this.notMovingFoot = this.leftFoot;
        }
        if (this.grounded)
        {
          Quaternion quaternion = Quaternion.Inverse(this.transform.rotation);
          this.lastStepVectorChangeToe = this.stepVectorChangeToe;
          this.stepVectorChangeToe = quaternion * (this.notMovingToe.position - this.transform.position);
          this.lastStepVectorChangeHeel = this.stepVectorChangeHeel;
          this.stepVectorChangeHeel = quaternion * (this.notMovingHeel.position - this.transform.position);
          this.lastStepVectorChangeFoot = this.stepVectorChangeFoot;
          this.stepVectorChangeFoot = quaternion * (this.notMovingFoot.position - this.transform.position);
        }
        else
        {
          Vector3 stepVectorChangeToe = this.lastStepVectorChangeToe;
          this.lastStepVectorChangeToe = this.stepVectorChangeToe;
          this.stepVectorChangeToe = stepVectorChangeToe;
          Vector3 vectorChangeHeel = this.lastStepVectorChangeHeel;
          this.lastStepVectorChangeHeel = this.stepVectorChangeHeel;
          this.stepVectorChangeHeel = vectorChangeHeel;
          Vector3 vectorChangeFoot = this.lastStepVectorChangeFoot;
          this.lastStepVectorChangeFoot = this.stepVectorChangeFoot;
          this.stepVectorChangeFoot = vectorChangeFoot;
        }
      }
      this.normTime = (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime - this.footChangeNormTime) * num;
      this.normTimeLeft = Mathf.Clamp01((float) (1.0 - (double) this.normTime * 2.0));
      this.normTimeCurveRight = (float) ((double) this.normTimeLeft * (1.0 - (double) this.normTimeLeft) * 4.0) * this.predictWeight;
      if (this.leftFootMoving)
      {
        this.normTimeCurveLeft = this.normTimeCurveRight;
        this.normTimeCurveRight = 0.0f;
      }
      else
        this.normTimeCurveLeft = 0.0f;
    }

    private void PrepareMoveCharacter()
    {
      Vector3 zero = Vector3.zero;
      float num1 = Vector3.Dot(this.raycastHitRightFoot.point - this.rightDH - this.transform.position, -this.raycastDown_Hat);
      float num2 = Vector3.Dot(this.raycastHitLeftFoot.point - this.leftDH - this.transform.position, -this.raycastDown_Hat);
      Vector3 vector3_1;
      if ((double) num1 < (double) num2)
      {
        vector3_1 = this.raycastHitRightFoot.point - this.rightDH;
        this.elevation = -num2;
      }
      else
      {
        vector3_1 = this.raycastHitLeftFoot.point - this.leftDH;
        this.elevation = -num1;
      }
      float num3 = this.elevation;
      float num4 = this.moveClass.relativeVelocityT.magnitude / this.realCharacterScale;
      bool flag = this.willTrip && (double) num4 < 2.0;
      float num5 = 0.0f;
      if (this.futureHit && !flag)
      {
        num5 = num4;
        Vector3 vector3_2 = this.raycastHitFutureFoot.point - this.futureDH;
        num3 = Vector3.Dot(vector3_2 - this.transform.position, -this.raycastDown_Hat);
        this.transformTarget = this.transformTarget + this.moveClass.onPlatformDeltaPos;
        Vector3 vector3_3 = vector3_2 - this.transformTarget;
        this.destinationNormal_Hat = Vector3.Cross(Vector3.Cross(vector3_3, this.nRaw_Hat), vector3_3).normalized;
        if ((double) Vector3.Dot(vector3_3, this.moveClass.relativeVelocityT_Hat) < 0.20000000298023224)
          this.destinationNormal_Hat = this.nRaw_Hat;
        this.transformTarget = this.transformTarget + StaticStuff_PBC.Project(this.transform.position - this.transformTarget, this.destinationNormal_Hat, this.raycastDown_Hat);
        this.nRaw_Hat = (this.nRaw_Hat + this.destinationNormal_Hat * num4).normalized;
      }
      this.transformTarget = Vector3.Lerp(vector3_1 + StaticStuff_PBC.Project(this.transform.position - vector3_1, Vector3.Lerp(-this.moveClass.gravity_Hat, this.transform.up, this.disLerp), this.raycastDown_Hat), this.transformTarget, num5 * this.disLerp);
      this.transformTarget = Vector3.Lerp(this.transformTarget, this.raycastHitTransform.point, this.footRootRayLerp);
      if (this.futureHit && !flag && (double) this.elevation > -(double) num3)
        this.elevation = -num3;
      this.grounded = (double) this.elevation < (double) this.groundedLimit;
    }

    private void ResetAnimations()
    {
      this.leftFoot.position = this.leftFootPosition2;
      this.rightFoot.position = this.rightFootPosition2;
    }

    private void ScaleAnimations(float runScale, float strafeScale)
    {
      this.leftFootPosition2 = this.leftFoot.position;
      this.rightFootPosition2 = this.rightFoot.position;
      if ((double) runScale == 1.0 && (double) strafeScale == 1.0)
        return;
      Vector3 rhs1 = this.leftFoot.position - this.transform.position;
      Vector3 rhs2 = this.rightFoot.position - this.transform.position;
      Vector3 translation1 = Vector3.Dot(this.transform.forward, rhs1) * this.transform.forward * (runScale - 1f) + (Vector3.Dot(this.transform.right, rhs1) + this.hipRadius) * this.transform.right * (strafeScale - 1f);
      Vector3 translation2 = Vector3.Dot(this.transform.forward, rhs2) * this.transform.forward * (runScale - 1f) + (Vector3.Dot(this.transform.right, rhs2) - this.hipRadius) * this.transform.right * (strafeScale - 1f);
      this.leftFoot.transform.Translate(translation1, Space.World);
      this.rightFoot.transform.Translate(translation2, Space.World);
    }

    private void AwakeInShootIkRays() => this.lastTransformPosition = this.transform.position;

    private void ShootIKRays()
    {
      this.leftFootToeVector = this.leftToe2.position - this.leftFoot.position;
      this.rightFootToeVector = this.rightToe2.position - this.rightFoot.position;
      this.leftFootHeelVector = this.leftHeel.position - this.leftFoot.position;
      this.rightFootHeelVector = this.rightHeel.position - this.rightFoot.position;
      this.leftFootFootVector = (this.leftToe2.position + this.leftHeel.position) * 0.5f - this.leftFoot.position;
      this.rightFootFootVector = (this.rightToe2.position + this.rightHeel.position) * 0.5f - this.rightFoot.position;
      this.leftFootElevation = Vector3.Dot(this.leftFoot.position + this.leftFootFootVector - this.transform.position, this.transform.up);
      this.rightFootElevation = Vector3.Dot(this.rightFoot.position + this.rightFootFootVector - this.transform.position, this.transform.up);
      this.leftFootIKWeight = Mathf.Clamp01((this.realCharacterScale - this.leftFootElevation) / this.realCharacterScale) * this.disLerp * this.leftFootIKWeightSet;
      this.rightFootIKWeight = Mathf.Clamp01((this.realCharacterScale - this.rightFootElevation) / this.realCharacterScale) * this.disLerp * this.rightFootIKWeightSet;
      this.ScaleAnimations(this.moveClass.runScale, this.moveClass.strafeScale);
      this.leftFootIKMoved = this.leftFoot.position + this.leftFootIKMove;
      this.rightFootIKMoved = this.rightFoot.position + this.rightFootIKMove;
      this.transforDeltaPosition = this.transform.position - this.lastTransformPosition;
      this.leftFootHeelVector = this.leftFootIKRotation * this.leftFootHeelVector;
      this.rightFootHeelVector = this.rightFootIKRotation * this.rightFootHeelVector;
      this.leftFootToeVector = this.leftFootIKRotation * this.leftFootToeVector;
      this.rightFootToeVector = this.rightFootIKRotation * this.rightFootToeVector;
      this.leftDH = this.leftFootIKRotation * this.leftFootFootVector - this.leftFootFootVector;
      this.rightDH = this.rightFootIKRotation * this.rightFootFootVector - this.rightFootFootVector;
      this.leftFootFootVector += this.leftDH;
      this.rightFootFootVector += this.rightDH;
      if (this.toeAndHeelRays && !this.tripped && this.grounded && (double) this.leftFootIKWeightSet * (double) this.rightFootIKWeightSet > 0.25 && (double) this.moveClass.relativeVelocityT.magnitude < 4.0)
        this.IKRaysToeHeel_PBC();
      else
        this.IKRaysFoot_PBC();
      this.footRootRayLerp = this.grounded && !(bool) (Object) this.iAmStandingOn || this.moveClass.posYByTransform ? 1f : this.ragdollControl.footRootRayLerp;
      if ((double) this.footRootRayLerp > 0.0)
      {
        if (this.showRaycasts)
          Debug.DrawRay(this.animator.transform.position - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat * this.raycastLength, Color.red);
        if (Physics.Raycast(this.animator.transform.position - this.raycastDown_Hat * this.raycastHeight, this.raycastDown_Hat, out this.raycastHitTransform, this.raycastLength, (int) this.layerMask))
          this.nRaw_Hat = (this.nRaw_Hat + this.raycastHitTransform.normal).normalized;
        else
          this.raycastHitTransform = this.raycastHitRightFoot;
        this.iAmStandingOn = this.raycastHitTransform.transform;
      }
      this.ResetAnimations();
      this.lastTransformPosition = this.transform.position;
    }
  }
}
