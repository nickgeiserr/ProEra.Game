// Decompiled with JetBrains decompiler
// Type: PBC.Hoverboard_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class Hoverboard_PBC : MonoBehaviour
  {
    [SerializeField]
    private float mouseSensitivity = 0.5f;
    [SerializeField]
    private float boardWidth = 0.5f;
    [Range(0.0f, 2f)]
    [SerializeField]
    private float exaggerateSlopeAcc = 1.1f;
    [Range(0.0f, 3f)]
    [SerializeField]
    private float edgeFriction = 1.5f;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float tiltLift;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float jazzIt = 0.5f;
    private AnimFollow_PBC animFollow;
    public Transform rider;
    public Transform transformRef;
    private Quaternion transformRefRotation;
    private Vector3 gravity_Hat;
    private Vector3 lastTransformPos;
    private Vector3 lastDesiredVelocity;
    private Vector3 raycastDown = Physics.gravity.normalized;
    private Vector3 velocity;
    private Vector3 groundNormalLerped;
    private Vector3 boardUp;
    private Vector3 boardUpSmoothVel;
    private Vector3 groundNormalSmoothVel;
    private Vector3 boardUp_Hat;
    private Vector3 gravity = Physics.gravity;
    private float acc;
    private float tiltBoard;
    private float boardLift;
    private float boardSwing;
    private float hoverHeight = 0.1f;
    private float dMouseX;
    private float mouseRotationVelocity;
    private float smoothOn;
    private bool almostGrounded;
    public LayerMask layerMask;

    private void Awake()
    {
      if ((bool) (Object) this.rider)
        this.animFollow = this.rider.GetComponent<AnimFollow_PBC>();
      this.transformRef = this.transform.GetChild(0).GetChild(0);
      this.gravity_Hat = this.gravity.normalized;
      this.raycastDown = Vector3.down;
      this.lastTransformPos = this.transform.position;
      this.layerMask = (LayerMask) ((int) this.layerMask | 1 << LayerMask.NameToLayer("Water"));
      this.layerMask = (LayerMask) ((int) this.layerMask | 1 << LayerMask.NameToLayer("UI"));
      this.layerMask = (LayerMask) ~(int) this.layerMask;
    }

    private void Update()
    {
      this.acc = this.tiltBoard = 0.0f;
      if (!this.almostGrounded)
        return;
      this.acc = Input.GetAxis("Vertical") * 10f;
      this.tiltBoard = Input.GetAxis("Horizontal");
      this.dMouseX += Input.GetAxis("Mouse X") * this.mouseSensitivity;
      this.mouseRotationVelocity = Mathf.Clamp(Mathf.Lerp(this.mouseRotationVelocity, this.dMouseX / (Time.deltaTime + 1E-06f) * Time.timeScale, this.smoothOn), -1000f, 1000f);
    }

    private void FixedUpdate()
    {
      this.almostGrounded = true;
      RaycastHit hitInfo;
      this.almostGrounded = Physics.Raycast(this.lastTransformPos - this.raycastDown, this.raycastDown, out hitInfo, 2f, (int) this.layerMask) && (double) Vector3.Dot(this.lastTransformPos - hitInfo.point, this.transform.up) < 0.5;
      if (this.almostGrounded)
      {
        this.transform.position = this.lastTransformPos;
        this.smoothOn = Mathf.Clamp01(this.smoothOn + 0.4f * Time.fixedDeltaTime);
        this.transform.rotation = Quaternion.FromToRotation(this.transform.up, this.groundNormalLerped) * this.transform.rotation;
        this.transform.rotation = Quaternion.AngleAxis(this.mouseRotationVelocity * Time.fixedDeltaTime, this.groundNormalLerped) * this.transform.rotation;
        float min = Vector3.Dot(this.gravity, this.groundNormalLerped);
        Vector3 lhs1 = this.gravity - min * this.groundNormalLerped;
        float num1 = Vector3.Dot(lhs1, this.transform.right);
        Vector3 vector3_1 = num1 * this.transform.right;
        Vector3 vector3_2 = lhs1 - vector3_1;
        this.velocity += (this.acc * this.transform.forward + vector3_2 * this.exaggerateSlopeAcc) * Time.fixedDeltaTime;
        Vector3 vector3_3 = hitInfo.point + this.groundNormalLerped * this.hoverHeight - this.transform.position;
        Vector3 vector3_4 = (this.transform.forward + vector3_3) * this.velocity.magnitude;
        Vector3 lhs2 = (vector3_4 - this.velocity) / Time.fixedDeltaTime * 0.5f;
        float num2 = Mathf.Clamp(Vector3.Dot(lhs2, this.groundNormalLerped), min, 100f);
        Vector3 vector3_5 = num2 * this.groundNormalLerped;
        double num3 = (double) Vector3.Dot(lhs2 - vector3_5, this.transform.right);
        float num4 = Mathf.Clamp(num2 * 0.9f - min, 0.0f, 20f) * this.edgeFriction;
        double num5 = (double) num1;
        Vector3 vector3_6 = Mathf.Clamp((float) (num3 - num5), (float) (-(double) num4 * (1.0 - (double) Mathf.Clamp(this.tiltBoard, -0.5f, 0.95f))), num4 * (1f - Mathf.Clamp(-this.tiltBoard, -0.5f, 0.95f))) * this.transform.right + vector3_1;
        Debug.DrawRay(this.transform.position, vector3_6 * Time.fixedDeltaTime);
        this.velocity += (vector3_6 + vector3_5) * Time.fixedDeltaTime;
        this.velocity -= this.velocity.normalized * (float) (1.0 + (double) this.velocity.sqrMagnitude * 0.0099999997764825821) * Time.fixedDeltaTime;
        if ((bool) (Object) this.animFollow)
          this.velocity += this.animFollow.VelocityCorrection;
        this.transform.position += (this.velocity + Vector3.ClampMagnitude(vector3_3 * 8f, 0.5f)) * Time.fixedDeltaTime;
        this.lastTransformPos = this.transform.position;
        this.transformRefRotation = Quaternion.FromToRotation(this.transformRef.up, -this.gravity + (vector3_6 + vector3_5 * 0.6f + this.acc * 0.1f * this.transform.forward + vector3_2)) * this.transformRef.rotation;
        float num6 = Mathf.Clamp01(this.velocity.magnitude);
        this.transformRefRotation = Quaternion.AngleAxis(Mathf.Clamp(Vector3.Angle(vector3_4, this.lastDesiredVelocity) * 10f * num6, -30f, 30f) * Vector3.Dot(Vector3.Cross(vector3_4, this.lastDesiredVelocity).normalized, -this.gravity_Hat), this.gravity_Hat) * this.transformRefRotation;
        this.lastDesiredVelocity = vector3_4;
        this.boardUp = Vector3.SmoothDamp(this.boardUp, -this.gravity + (vector3_6 + vector3_5) * 0.8f, ref this.boardUpSmoothVel, 0.3f, float.PositiveInfinity, Time.fixedDeltaTime);
        this.boardUp_Hat = this.boardUp.normalized;
        this.boardLift = (float) ((double) Mathf.Abs(Vector3.Dot(this.transform.right, this.boardUp_Hat)) * (double) this.boardWidth * 0.5);
        this.transform.Translate(this.boardLift * this.tiltLift * this.boardUp_Hat, Space.World);
        Vector3 right = this.transform.right;
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward, this.boardUp_Hat);
        this.boardSwing = Vector3.Dot(this.transform.right, Vector3.up) * Mathf.Sqrt(this.velocity.magnitude * 0.1f) * this.smoothOn;
        this.transform.Translate(this.boardSwing * this.jazzIt * right, Space.World);
        this.transformRef.rotation = this.transformRefRotation;
        this.groundNormalLerped = Vector3.SmoothDamp(this.groundNormalLerped, hitInfo.normal, ref this.groundNormalSmoothVel, 0.1f, float.PositiveInfinity, Time.fixedDeltaTime);
        this.raycastDown = -this.groundNormalLerped;
      }
      else
      {
        this.smoothOn = 0.0f;
        this.groundNormalSmoothVel = this.boardUpSmoothVel = Vector3.zero;
        this.velocity += this.gravity * Time.fixedDeltaTime;
        this.transform.rotation = Quaternion.AngleAxis(this.mouseRotationVelocity * Time.fixedDeltaTime, this.boardUp) * this.transform.rotation;
        this.groundNormalLerped = this.transform.up;
        this.boardUp = this.groundNormalLerped;
        this.transformRef.rotation = this.transform.rotation;
        this.raycastDown = (-this.groundNormalLerped + Vector3.down).normalized;
        this.transform.position += this.velocity * Time.fixedDeltaTime;
        this.lastTransformPos = this.transform.position;
      }
      if ((double) this.velocity.magnitude > 0.5)
      {
        Debug.DrawRay(this.transform.position - this.transform.forward * 0.5f, this.transform.right * 0.3f, Color.blue, 2f);
        Debug.DrawRay(this.transform.position - this.transform.forward * 0.5f, this.transform.right * -0.3f, Color.blue, 2f);
      }
      this.dMouseX = 0.0f;
    }
  }
}
