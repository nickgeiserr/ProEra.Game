// Decompiled with JetBrains decompiler
// Type: PBC.RagdollControl_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace PBC
{
  public class RagdollControl_PBC : MonoBehaviour
  {
    private AnimFollow_PBC animFollow;
    private Animator animator;
    private HashIDs_PBC hash;
    private MoveBaseClass_PBC moveScript;
    [HideInInspector]
    public Transform master;
    [HideInInspector]
    public ParticleSystem[] blood = new ParticleSystem[4];
    [HideInInspector]
    public PhysicMaterial iceMaterial;
    [Range(8f, 256f)]
    [SerializeField]
    private float externalForceLimit = 100f;
    [Range(2f, 32f)]
    [SerializeField]
    private float speedLimit = 12f;
    [Range(0.0f, 12f)]
    [SerializeField]
    private float fallLerp = 4f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float residualJointStrength = 1f;
    public bool stayDown;
    public bool inhibitFall;
    public bool startFall;
    public bool useFallAnimation = true;
    private float settledSpeed = 0.3f;
    private float riseTimeout = 5f;
    private float externalForceValue;
    [HideInInspector]
    public bool delayedGetupDone;
    [HideInInspector]
    public bool doubleBlood;
    [HideInInspector]
    public int bloodCounter;
    [HideInInspector]
    public int numberOfCollisions;
    [HideInInspector]
    public float collisionFreeTime;
    [HideInInspector]
    public float getUpLerp = 1f;
    [HideInInspector]
    public float layerControl = 1f;
    [HideInInspector]
    public float collisionSpeed;
    [HideInInspector]
    public float footRootRayLerp;
    [HideInInspector]
    public bool inhibitMove;
    [HideInInspector]
    public bool falling;
    [HideInInspector]
    public bool gettingUp;
    [HideInInspector]
    public bool headShot;
    [HideInInspector]
    public bool stayDown2;
    [HideInInspector]
    public bool orientate;
    [HideInInspector]
    public bool notTripped = true;
    [HideInInspector]
    public Vector3 ragdollCOMVelocity;
    private float realCharacterScale = 1f;
    private float hitTime;
    private float orientatedTime;
    private float storeForceStrength;
    private float storeJointStrength;
    private float[] storeJointStrengthProfile = new float[30];
    private float[] storeForceStrengthProfile = new float[30];
    private float storePushStiffness;
    private float storeSpinStiffness;
    private bool storeInhibitFall;
    private bool valuesStored;
    private bool orientated = true;
    private bool iced;
    private bool userNeedsToFixStuff;
    private bool isInTransitionToGetup;
    public bool shotByBullet;
    [SerializeField]
    private bool extLimiten;
    [SerializeField]
    private bool speedLimiten;

    private void Awake()
    {
      if (this.userNeedsToFixStuff = !this.WeHaveAllTheStuff())
        return;
      this.storeInhibitFall = this.inhibitFall;
      this.StartCoroutine(this.WaitForStartMessToCalm());
    }

    private IEnumerator WaitForStartMessToCalm()
    {
      this.storeJointStrength = this.animFollow.jointStrength;
      this.storeForceStrength = this.animFollow.poseStiffness;
      this.animFollow.jointStrength = 1f;
      this.animFollow.poseStiffness = 1f;
      this.inhibitFall = true;
      yield return (object) new WaitForSeconds(1f);
      this.collisionSpeed = 0.0f;
      this.shotByBullet = false;
      this.inhibitFall = this.storeInhibitFall;
      this.animFollow.jointStrength = this.storeJointStrength;
      this.animFollow.poseStiffness = this.storeForceStrength;
    }

    public void DoRagdollControl(Vector3 gravity_Hat)
    {
      if (this.inhibitFall)
      {
        this.shotByBullet = false;
        this.collisionFreeTime += Time.fixedDeltaTime;
      }
      else
      {
        this.ragdollCOMVelocity = this.animFollow.RagdollCOMVelocity;
        float num1 = !(bool) (Object) this.moveScript ? this.ragdollCOMVelocity.magnitude : this.moveScript.actualRelativeVelocityMagnitude;
        if (this.numberOfCollisions == 0)
          this.collisionFreeTime += Time.fixedDeltaTime;
        else
          this.collisionFreeTime = 0.0f;
        this.externalForceValue = this.animFollow.ExternalAccLerped.magnitude;
        bool flag1 = (double) this.externalForceValue > (double) this.externalForceLimit;
        bool flag2 = (double) this.collisionSpeed > (double) this.speedLimit;
        this.collisionSpeed = 0.0f;
        if (this.delayedGetupDone)
          this.delayedGetupDone = (double) this.externalForceValue > (double) this.externalForceLimit * 0.20000000298023224;
        if (this.iced && !this.delayedGetupDone)
        {
          this.animFollow.pushStiffness = this.storePushStiffness;
          this.animFollow.spinStiffness = this.storeSpinStiffness;
          this.notTripped = true;
          this.iced = false;
          foreach (Component ragdollRigidTransform in this.animFollow.ragdollRigidTransforms)
            ragdollRigidTransform.GetComponent<Collider>().material = (PhysicMaterial) null;
        }
        if (this.startFall)
        {
          this.hitTime = Time.time;
          if (!this.valuesStored)
          {
            this.storeForceStrength = this.animFollow.forceStrength;
            this.storeJointStrength = this.animFollow.jointStrength;
            this.storePushStiffness = this.animFollow.pushStiffness;
            this.storeSpinStiffness = this.animFollow.spinStiffness;
            this.valuesStored = true;
            for (int index = 0; index < this.animFollow.jointStrengthProfile.Length; ++index)
              this.storeJointStrengthProfile[index] = this.animFollow.jointStrengthProfile[index];
            for (int index = 0; index < this.animFollow.forceStrengthProfile.Length; ++index)
              this.storeForceStrengthProfile[index] = this.animFollow.forceStrengthProfile[index];
          }
          if (!this.falling)
          {
            if (this.useFallAnimation)
              this.animator.SetTrigger("FallingTrigger");
            this.footRootRayLerp = 1f;
            this.notTripped = false;
            this.animFollow.SetJointLimitSprings(1f);
            this.animFollow.EnableJointLimits(true);
            this.animFollow.pushStiffness = 0.0f;
            this.animFollow.spinStiffness = 0.0f;
            float num2 = 1f;
            if (!this.useFallAnimation)
              num2 = Mathf.Sqrt(num1 * 0.13f);
            if (this.headShot)
              num2 = 0.0f;
            this.animFollow.jointStrength = Mathf.Min(this.residualJointStrength * num2, this.animFollow.jointStrength);
            this.animFollow.forceStrength = 0.0f;
          }
          if (this.iced)
          {
            foreach (Component ragdollRigidTransform in this.animFollow.ragdollRigidTransforms)
              ragdollRigidTransform.GetComponent<Collider>().material = (PhysicMaterial) null;
          }
          this.startFall = false;
          this.shotByBullet = false;
          this.headShot = false;
          this.falling = true;
          this.gettingUp = false;
          this.orientate = false;
          this.orientated = false;
          this.delayedGetupDone = false;
          this.extLimiten = flag1;
          this.speedLimiten = flag2;
        }
        else if (this.gettingUp)
        {
          AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
          int num3;
          if (!animatorStateInfo.fullPathHash.Equals(this.hash.getupFront))
          {
            animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
            if (!animatorStateInfo.fullPathHash.Equals(this.hash.getupBack))
            {
              animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
              if (!animatorStateInfo.fullPathHash.Equals(this.hash.getupFrontMirror))
              {
                animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
                num3 = animatorStateInfo.fullPathHash.Equals(this.hash.getupBackMirror) ? 1 : 0;
                goto label_38;
              }
            }
          }
          num3 = 1;
label_38:
          bool flag3 = num3 != 0;
          bool transitionToGetup = this.isInTransitionToGetup;
          int num4;
          if (!this.animator.GetAnimatorTransitionInfo(0).fullPathHash.Equals(this.hash.anyStateToGetupFront))
          {
            AnimatorTransitionInfo animatorTransitionInfo = this.animator.GetAnimatorTransitionInfo(0);
            if (!animatorTransitionInfo.fullPathHash.Equals(this.hash.anyStateToGetupBack))
            {
              animatorTransitionInfo = this.animator.GetAnimatorTransitionInfo(0);
              if (!animatorTransitionInfo.fullPathHash.Equals(this.hash.anyStateToGetupFrontMirror))
              {
                animatorTransitionInfo = this.animator.GetAnimatorTransitionInfo(0);
                num4 = animatorTransitionInfo.fullPathHash.Equals(this.hash.anyStateToGetupBackMirror) ? 1 : 0;
                goto label_43;
              }
            }
          }
          num4 = 1;
label_43:
          this.isInTransitionToGetup = num4 != 0;
          if (this.orientated)
          {
            animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
            this.getUpLerp = (float) (((double) animatorStateInfo.normalizedTime - (double) this.orientatedTime) * (1.0 + (double) this.orientatedTime));
            this.footRootRayLerp = Mathf.Lerp(0.0f, 1f, (float) (4.0 - (double) this.getUpLerp * 4.0));
            this.layerControl = this.getUpLerp * this.getUpLerp;
            this.animFollow.forceStrength = this.storeForceStrength * this.getUpLerp;
            this.animFollow.jointStrength = this.storeJointStrength * this.getUpLerp;
            if (!flag3 && !this.isInTransitionToGetup)
            {
              this.gettingUp = false;
              this.layerControl = 1f;
              this.getUpLerp = 1f;
              this.inhibitMove = false;
              this.footRootRayLerp = 0.0f;
              for (int index = 0; index < this.animFollow.jointStrengthProfile.Length; ++index)
                this.animFollow.jointStrengthProfile[index] = this.storeJointStrengthProfile[index];
              for (int index = 0; index < this.animFollow.forceStrengthProfile.Length; ++index)
                this.animFollow.forceStrengthProfile[index] = this.storeForceStrengthProfile[index];
              this.animFollow.forceStrength = this.storeForceStrength;
              this.animFollow.jointStrength = this.storeJointStrength;
              this.valuesStored = false;
              this.delayedGetupDone = (double) this.externalForceValue > (double) this.externalForceLimit * 0.20000000298023224;
              if (!this.delayedGetupDone)
              {
                this.animFollow.pushStiffness = this.storePushStiffness;
                this.animFollow.spinStiffness = this.storeSpinStiffness;
                this.notTripped = true;
              }
              else
              {
                this.iced = true;
                foreach (Component ragdollRigidTransform in this.animFollow.ragdollRigidTransforms)
                  ragdollRigidTransform.GetComponent<Collider>().material = this.iceMaterial;
              }
            }
          }
          if (!(this.orientate & transitionToGetup) || this.isInTransitionToGetup)
            return;
          this.orientate = false;
          this.orientated = true;
          this.animFollow.EnableJointLimits(false);
          animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
          this.orientatedTime = animatorStateInfo.normalizedTime;
          if ((double) this.orientatedTime <= 0.20000000298023224)
            return;
          Debug.LogWarning((object) "Mecanim transitions to the get-up animations should be fixed duration.\n");
        }
        else
        {
          if (!this.falling)
            return;
          this.animFollow.jointStrength = Mathf.Lerp(this.animFollow.jointStrength, 0.0f, this.fallLerp * Time.fixedDeltaTime) * Mathf.Clamp01(num1);
          this.animFollow.SetJointLimitSprings(1f - this.animFollow.jointStrength);
          this.layerControl = Mathf.Lerp(this.layerControl, 0.0f, 2f * Time.fixedDeltaTime);
          if (((double) Time.time - (double) this.hitTime <= (double) this.settledSpeed || (double) num1 >= (double) this.settledSpeed) && (double) Time.time - (double) this.hitTime <= (double) this.riseTimeout)
            return;
          if (!this.animator.enabled)
          {
            this.animator.enabled = true;
            foreach (Component ragdollRigidTransform in this.animFollow.ragdollRigidTransforms)
              ragdollRigidTransform.GetComponent<Rigidbody>().useGravity = false;
          }
          if (this.stayDown)
          {
            this.stayDown2 = true;
            this.animator.enabled = false;
            foreach (Component ragdollRigidTransform in this.animFollow.ragdollRigidTransforms)
              ragdollRigidTransform.GetComponent<Rigidbody>().useGravity = true;
          }
          else
          {
            this.layerControl = 0.0f;
            this.getUpLerp = 0.0f;
            this.falling = false;
            this.gettingUp = true;
            this.orientate = true;
            this.inhibitMove = true;
            this.animFollow.forceStrength = 0.0f;
            this.animFollow.jointStrength = 0.0f;
            for (int index = 0; index < this.animFollow.jointStrengthProfile.Length; ++index)
              this.animFollow.jointStrengthProfile[index] = 1f;
            for (int index = 0; index < this.animFollow.forceStrengthProfile.Length; ++index)
              this.animFollow.forceStrengthProfile[index] = 1f;
            if ((double) Vector3.Dot(this.animFollow.ragdollTransform.bodyRotation * Vector3.forward, gravity_Hat) > 0.10000000149011612)
            {
              if (!this.animator.GetCurrentAnimatorStateInfo(0).fullPathHash.Equals(this.hash.getupFront))
                this.animator.SetBool(this.hash.frontTrigger, true);
              else
                this.animator.SetBool(this.hash.frontMirrorTrigger, true);
            }
            else if (!this.animator.GetCurrentAnimatorStateInfo(0).fullPathHash.Equals(this.hash.getupBack))
              this.animator.SetBool(this.hash.backTrigger, true);
            else
              this.animator.SetBool(this.hash.backMirrorTrigger, true);
          }
        }
      }
    }

    private bool WeHaveAllTheStuff()
    {
      if (!(bool) (Object) this.master && (!(bool) (Object) this.transform.root.GetComponentInChildren<AnimFollow_PBC>() || !(bool) (Object) (this.master = this.transform.root.GetComponentInChildren<AnimFollow_PBC>().transform)))
      {
        Debug.LogWarning((object) ("master not assigned in script RagdollControl on " + this.name + "\n"));
        return false;
      }
      this.moveScript = (MoveBaseClass_PBC) this.master.GetComponent<AdvancedMoveScript_PBC>();
      if (!(bool) (Object) (this.animFollow = this.master.GetComponent<AnimFollow_PBC>()))
      {
        Debug.LogWarning((object) ("Missing Script: AnimFollow on " + this.master.name + "\n"));
        return false;
      }
      if (!(bool) (Object) (this.hash = this.master.GetComponent<HashIDs_PBC>()))
      {
        Debug.LogWarning((object) ("Missing Script: HashIDs on " + this.master.name + "\n"));
        return false;
      }
      if (!(bool) (Object) (this.animator = this.master.GetComponent<Animator>()))
      {
        Debug.LogWarning((object) ("Missing Animator on " + this.master.name + "\n"));
        return false;
      }
      Main_PBC componentInChildren;
      if ((bool) (Object) (componentInChildren = this.transform.root.GetComponentInChildren<Main_PBC>()))
        this.realCharacterScale = componentInChildren.realCharacterScale;
      this.speedLimit *= Mathf.Sqrt(this.realCharacterScale);
      this.settledSpeed *= Mathf.Sqrt(this.realCharacterScale);
      if (this.speedLimiten && this.extLimiten)
        MonoBehaviour.print((object) "This will never show and is here just to avoid a compiler warning");
      return true;
    }
  }
}
