// Decompiled with JetBrains decompiler
// Type: PBC.GetWASD_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class GetWASD_PBC : MonoBehaviour
  {
    private Animator animator;
    private MoveBaseClass_PBC moveScript;
    private AnimFollow_PBC animFollow;
    private RagdollControl_PBC ragdollControl;
    public Transform rabbitTransform;
    [SerializeField]
    private float sprintSpeedSet = 7f;
    [SerializeField]
    private float runSpeedSet = 5f;
    [SerializeField]
    private float walkSpeedSet = 2f;
    [SerializeField]
    private float runBwdSpeedSet = 4.2f;
    [SerializeField]
    private float walkBwdSpeedSet = 2f;
    [SerializeField]
    private float strafeRunSpeedSet = 3f;
    [SerializeField]
    private float strafeWalkSpeedSet = 2f;
    private float walkFactor;
    private float walkBwdFactor;
    private float sprintFactor;
    private float strafeWalkFactor;
    [HideInInspector]
    public float desiredRunSpeed;
    [HideInInspector]
    public float desiredStrafeSpeed;
    [HideInInspector]
    public bool jumpKeyDown;
    [HideInInspector]
    public bool jumpKey;
    [HideInInspector]
    public float deltaAngle;
    [SerializeField]
    private float mouseSensitivity = 0.5f;
    [SerializeField]
    private float masterMouseTune = 0.7f;
    [SerializeField]
    private float navRotationDist = 0.5f;
    [SerializeField]
    private float tuneFollowSpeed = 3f;
    private Vector3 lastRabbitPosition;
    private float mouseRotationVelocity;
    private float timen;
    private float navTolerance = 0.1f;
    private bool fixedDone;
    private bool followTransform;
    public bool mimicRabbitRotation;
    public bool useMouse = true;
    public bool useRootRotation;
    public bool disableButtons;

    private void Awake()
    {
      this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>();
      if ((bool) (Object) (this.animator = this.GetComponent<Animator>()))
      {
        float num = 1f;
        Main_PBC componentInChildren;
        if ((bool) (Object) (componentInChildren = this.transform.root.GetComponentInChildren<Main_PBC>()))
          num = componentInChildren.realCharacterScale;
        this.sprintSpeedSet *= num;
        this.runSpeedSet *= num;
        this.walkSpeedSet *= num;
        this.runBwdSpeedSet *= num;
        this.walkBwdSpeedSet *= num;
        this.strafeRunSpeedSet *= num;
        this.strafeWalkSpeedSet *= num;
        this.navTolerance *= num;
        this.navRotationDist *= num;
      }
      this.walkFactor = (float) (1.0 - (double) this.walkSpeedSet / (double) this.runSpeedSet);
      this.walkBwdFactor = (float) (1.0 - (double) this.walkBwdSpeedSet / (double) this.runBwdSpeedSet);
      this.sprintFactor = (float) (1.0 - (double) this.sprintSpeedSet / (double) this.runSpeedSet);
      this.strafeWalkFactor = (float) (1.0 - (double) this.strafeWalkSpeedSet / (double) this.strafeRunSpeedSet);
      if (!(bool) (Object) (this.moveScript = (MoveBaseClass_PBC) this.GetComponent<AdvancedMoveScript_PBC>()))
        Debug.LogWarning((object) ("No Script AdvancedMoveScript on " + this.name + "\n"));
      if ((bool) (Object) (this.animFollow = this.GetComponent<AnimFollow_PBC>()))
        return;
      Debug.LogWarning((object) ("No Script AnimFollow on " + this.name + "\n"));
    }

    public void DoGetWASD_Update(bool grounded)
    {
      this.mouseRotationVelocity = (!this.animFollow.ragdollSuspended ? Input.GetAxis("Mouse X") * this.mouseSensitivity : Input.GetAxis("Mouse X") * this.mouseSensitivity * this.masterMouseTune) / (Time.deltaTime + 1E-06f) * Time.timeScale;
      if (!(bool) (Object) this.rabbitTransform)
      {
        this.followTransform = false;
        this.desiredRunSpeed = 0.0f;
        try
        {
          if ((double) Input.GetAxis("Vertical") > 0.0099999997764825821)
            this.desiredRunSpeed = (float) ((double) Input.GetAxis("Vertical") * (1.0 - (double) Input.GetAxis("Walk") * (double) this.walkFactor) * (1.0 - (double) Input.GetAxis("Sprint") * (double) this.sprintFactor)) * this.runSpeedSet;
          else if ((double) Input.GetAxis("Vertical") < -0.0099999997764825821)
            this.desiredRunSpeed = Input.GetAxis("Vertical") * (float) (1.0 - (double) Input.GetAxis("Walk") * (double) this.walkBwdFactor) * this.runBwdSpeedSet;
          this.desiredStrafeSpeed = 0.0f;
          if ((double) Input.GetAxis("Sprint") > 0.0099999997764825821)
          {
            if ((double) Input.GetAxis("Vertical") > 0.0099999997764825821)
              goto label_18;
          }
          this.desiredStrafeSpeed = Input.GetAxis("Horizontal") * (float) (1.0 - (double) Input.GetAxis("Walk") * (double) this.strafeWalkFactor) * this.strafeRunSpeedSet;
        }
        catch
        {
          if ((double) Input.GetAxis("Vertical") > 0.0099999997764825821)
            this.desiredRunSpeed = Input.GetAxis("Vertical") * this.runSpeedSet;
          else if ((double) Input.GetAxis("Vertical") < -0.0099999997764825821)
            this.desiredRunSpeed = Input.GetAxis("Vertical") * this.runBwdSpeedSet;
          this.desiredStrafeSpeed = 0.0f;
          if ((double) Input.GetAxis("Sprint") > 0.0099999997764825821)
          {
            if ((double) Input.GetAxis("Vertical") > 0.0099999997764825821)
              goto label_18;
          }
          this.desiredStrafeSpeed = Input.GetAxis("Horizontal") * this.strafeRunSpeedSet;
        }
      }
      else
        this.DoFollowRabbit();
label_18:
      if (this.disableButtons)
        return;
      this.DoButtons(grounded);
    }

    public void DoGetWASD_Fixed()
    {
      this.fixedDone = true;
      if (this.followTransform)
        return;
      this.deltaAngle = 0.0f;
      if (this.useMouse && this.useRootRotation)
        this.deltaAngle = Mathf.Lerp(this.mouseRotationVelocity * Time.fixedDeltaTime, 0.0f, this.ragdollControl.footRootRayLerp) + Vector3.Dot(this.animator.angularVelocity, this.transform.up) * 57.29578f * Time.fixedDeltaTime;
      else if (this.useMouse)
      {
        this.deltaAngle = Mathf.Lerp(this.mouseRotationVelocity * Time.fixedDeltaTime, 0.0f, this.ragdollControl.footRootRayLerp);
      }
      else
      {
        if (!this.useRootRotation)
          return;
        this.deltaAngle = Vector3.Dot(this.animator.angularVelocity, this.transform.up) * 57.29578f * Time.fixedDeltaTime;
      }
    }

    private void DoButtons(bool grounded)
    {
      this.jumpKeyDown = Input.GetButtonDown("Jump") & grounded && (!(bool) (Object) this.ragdollControl || this.ragdollControl.notTripped);
      this.jumpKey = Input.GetButton("Jump");
      if (Input.GetKeyDown(KeyCode.P))
        Time.timeScale = (double) Time.timeScale != 1.0 ? 1f : 0.0001f;
      if (Input.GetKeyDown(KeyCode.R) && (bool) (Object) this.animator && !this.animator.GetBool("RoundKickBool"))
        this.animator.SetBool("RoundKickBool", true);
      if (Input.GetKeyDown(KeyCode.G))
        this.moveScript.perpendicularGravity = !this.moveScript.perpendicularGravity;
      if (Input.GetKey(KeyCode.T))
      {
        this.moveScript.useVerticalRootMotion = true;
        this.moveScript.alwaysGrounded = true;
        this.animator.SetBool("ClimbUp", true);
      }
      else if (this.animator.GetBool("ClimbUp"))
      {
        this.animator.SetBool("ClimbUp", false);
        if (!Input.GetKey(KeyCode.Y))
        {
          this.moveScript.useVerticalRootMotion = false;
          this.moveScript.alwaysGrounded = false;
        }
      }
      if (Input.GetKey(KeyCode.Y))
      {
        this.moveScript.useVerticalRootMotion = true;
        this.moveScript.alwaysGrounded = true;
        this.animator.SetBool("ClimbDown", true);
      }
      else if (this.animator.GetBool("ClimbDown"))
      {
        this.animator.SetBool("ClimbDown", false);
        if (!Input.GetKey(KeyCode.T))
        {
          this.moveScript.useVerticalRootMotion = false;
          this.moveScript.alwaysGrounded = false;
        }
      }
      if (!Input.GetKeyDown(KeyCode.O))
        return;
      if (!this.animator.GetBool("TestAnimations"))
      {
        this.animator.SetBool("TestAnimations", true);
        this.useMouse = false;
        this.useRootRotation = true;
      }
      else
      {
        this.animator.SetBool("TestAnimations", false);
        this.useMouse = true;
        this.useRootRotation = false;
      }
    }

    private void DoFollowRabbit()
    {
      if (!this.followTransform)
      {
        this.lastRabbitPosition = this.rabbitTransform.position;
        this.followTransform = true;
      }
      this.timen += Time.deltaTime;
      if (!this.fixedDone)
        return;
      this.fixedDone = false;
      Vector3 lhs1 = this.rabbitTransform.position - this.transform.position;
      Vector3 vector3 = lhs1 - Vector3.Dot(lhs1, this.moveScript.gravity_Hat) * this.moveScript.gravity_Hat;
      Vector3 normalized = vector3.normalized;
      float magnitude1 = vector3.magnitude;
      if ((double) magnitude1 > (double) this.navTolerance)
      {
        float num = Mathf.Sqrt(magnitude1 * this.tuneFollowSpeed);
        float magnitude2 = ((this.rabbitTransform.position - this.lastRabbitPosition) / this.timen).magnitude;
        this.lastRabbitPosition = this.rabbitTransform.position;
        this.desiredRunSpeed = Mathf.Clamp(Vector3.Dot(this.moveScript.forwardT_Hat, normalized) * (magnitude2 + num), -this.runBwdSpeedSet, this.sprintSpeedSet);
        float f = Vector3.Dot(this.moveScript.rightT_Hat, normalized);
        this.desiredStrafeSpeed = Mathf.Clamp((float) ((double) Mathf.Abs(f) * (double) f * ((double) magnitude2 + (double) num)), -this.strafeRunSpeedSet, this.strafeRunSpeedSet);
      }
      else
        this.desiredRunSpeed = this.desiredStrafeSpeed = 0.0f;
      this.timen = 0.0f;
      if (this.useRootRotation)
      {
        this.deltaAngle = Vector3.Dot(this.animator.angularVelocity, this.transform.up) * 57.29578f * this.timen;
      }
      else
      {
        Vector3 lhs2 = !this.mimicRabbitRotation ? Vector3.Lerp(this.transform.forward, normalized, (magnitude1 - this.navRotationDist) / this.navRotationDist) : Vector3.Lerp(this.rabbitTransform.forward, normalized, (magnitude1 - this.navRotationDist) / this.navRotationDist);
        this.deltaAngle = StaticStuff_PBC.FindAngle(this.transform.forward, lhs2 - Vector3.Dot(lhs2, this.transform.up) * this.transform.up, this.transform.up) * 5f * Time.fixedDeltaTime;
        this.deltaAngle = Mathf.Lerp(this.deltaAngle, 0.0f, this.ragdollControl.footRootRayLerp);
      }
    }
  }
}
