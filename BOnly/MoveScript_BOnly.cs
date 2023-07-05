// Decompiled with JetBrains decompiler
// Type: BOnly.MoveScript_BOnly
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MovementEffects;
using PBC;
using System.Collections.Generic;
using UnityEngine;

namespace BOnly
{
  public class MoveScript_BOnly : MonoBehaviour
  {
    private Animator animator;
    private AnimFollow_PBC animFollow;
    private RagdollControl_PBC ragdoll;
    private HashIDs_PBC hash;
    private PlayerAI playerScript;
    [SerializeField]
    private float mouseSensitivity = 0.5f;
    [SerializeField]
    private string[] ignoreLayers = new string[1]{ "Water" };
    public Transform stickToTransform;
    public LayerMask layerMask;
    public Vector3 temp;
    public bool setPosition;
    private Vector3 velocity;
    private float mouseRotationVelocity;
    private bool userNeedsToFixStuff;
    private Rigidbody rb;
    private Vector3 animVelocity;
    public Vector3 animPosition;
    public Quaternion animRotation;
    private Transform thisParent;

    private void Awake()
    {
      this.thisParent = this.transform.parent;
      this.userNeedsToFixStuff = !this.WeHaveAllTheStuff();
    }

    private void OnAnimatorMove() => this.animRotation = this.animator.deltaRotation;

    private IEnumerator<float> FreezePlayer()
    {
      MoveScript_BOnly moveScriptBonly = this;
      moveScriptBonly.animFollow.suspendRagdoll = true;
      yield return Timing.WaitForOneFrame;
      moveScriptBonly.transform.position = Vector3.zero;
      for (int index = 0; index < moveScriptBonly.animFollow.ragdollRigidTransforms.Length; ++index)
      {
        moveScriptBonly.animFollow.ragdollRigidTransforms[index].position = moveScriptBonly.animFollow.masterRigidTransforms[index].position;
        moveScriptBonly.animFollow.ragdollRigidTransforms[index].rotation = moveScriptBonly.animFollow.masterRigidTransforms[index].rotation;
      }
      yield return Timing.WaitForOneFrame;
      moveScriptBonly.animFollow.suspendRagdoll = false;
    }

    public void DoMoveClassUpdate()
    {
      if (this.userNeedsToFixStuff)
        return;
      this.animator.SetFloat("speed", Input.GetAxis("Vertical"));
      this.mouseRotationVelocity = Input.GetAxis("Mouse X") * this.mouseSensitivity / (Time.deltaTime + 1E-06f) * Time.timeScale;
      if (!Input.GetKeyDown(KeyCode.R) || this.animator.GetBool("RoundKickTrigger"))
        return;
      this.animator.SetBool("RoundKickTrigger", true);
    }

    public void MoveCharacter()
    {
      if (this.userNeedsToFixStuff)
        return;
      if ((bool) (Object) this.stickToTransform)
      {
        this.transform.rotation = this.stickToTransform.rotation;
        this.transform.position = this.stickToTransform.position;
      }
      else
      {
        UnityEngine.RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position + Vector3.up, Vector3.down, out hitInfo, 1.2f, (int) this.layerMask))
        {
          if (!((Object) this.transform.parent != (Object) hitInfo.transform))
            ;
        }
        else
          hitInfo.distance = 1.2f;
        this.velocity = (hitInfo.distance - 1f) * Vector3.down / Time.fixedDeltaTime * 0.5f;
        if ((bool) (Object) this.animFollow && !this.animFollow.ragdollSuspended)
          this.velocity += this.animFollow.VelocityCorrection;
        Vector3 axis = Vector3.Cross(this.transform.up, Vector3.up);
        Quaternion quaternion = Quaternion.AngleAxis((float) ((double) axis.magnitude * 57.295780181884766 * 0.20000000298023224), axis);
        Vector3 vector3 = this.animFollow.MasterToCOMVector - quaternion * this.animFollow.MasterToCOMVector;
        this.transform.rotation = this.animRotation * quaternion * this.transform.rotation;
        this.transform.Translate(this.velocity * Time.fixedDeltaTime + vector3, Space.World);
      }
    }

    private void SetIgnoreLayers()
    {
      foreach (string ignoreLayer in this.ignoreLayers)
        this.layerMask = (LayerMask) ((int) this.layerMask | 1 << LayerMask.NameToLayer(ignoreLayer));
      this.layerMask = (LayerMask) ~(int) this.layerMask;
    }

    private bool WeHaveAllTheStuff()
    {
      if (!(bool) (Object) (this.animFollow = this.GetComponent<AnimFollow_PBC>()))
      {
        Debug.LogWarning((object) ("No script AnimFollow on " + this.name + ".\n"));
        return false;
      }
      if (!(bool) (Object) (this.animator = this.GetComponent<Animator>()))
      {
        Debug.LogWarning((object) ("No animator on " + this.name + ".\n"));
        return false;
      }
      this.hash = this.GetComponent<HashIDs_PBC>();
      this.playerScript = this.GetComponent<PlayerAI>();
      this.rb = this.GetComponent<Rigidbody>();
      this.ragdoll = this.transform.parent.GetComponentInChildren<RagdollControl_PBC>();
      return true;
    }
  }
}
