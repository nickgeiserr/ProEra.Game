// Decompiled with JetBrains decompiler
// Type: BOnly.CreateBOnlyGeneric_BOnly
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using PBC;
using System;
using System.Collections;
using UnityEngine;

namespace BOnly
{
  [ExecuteInEditMode]
  public class CreateBOnlyGeneric_BOnly : MonoBehaviour
  {
    private Animator animator;
    private AnimFollow_PBC animFollow;
    public volatile bool createNow;
    private Avatar avatar;
    private Vector3 thisPosition;
    private Quaternion thisRotation;
    private Transform[] ragdollBones;
    private Transform[] masterBones;
    private int numberOfTransforms;

    private void OnEnable()
    {
      this.thisPosition = this.transform.position;
      this.thisRotation = this.transform.rotation;
      if (!(bool) (UnityEngine.Object) (this.animator = this.GetComponent<Animator>()))
        this.animator = this.gameObject.AddComponent<Animator>();
      this.animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
      this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
      this.animator.applyRootMotion = false;
      this.StartCoroutine(this.SetItUp());
    }

    private IEnumerator SetItUp()
    {
      CreateBOnlyGeneric_BOnly bonlyGenericBonly = this;
      while (!bonlyGenericBonly.createNow)
      {
        while (!bonlyGenericBonly.createNow)
          yield return (object) null;
        if ((bool) (UnityEngine.Object) (bonlyGenericBonly.avatar = bonlyGenericBonly.animator.avatar) && bonlyGenericBonly.avatar.isHuman)
        {
          Debug.LogWarning((object) ("Avatar on " + bonlyGenericBonly.name + " is not generic.\nFor a humaniod character this is not the best choise of setupt script.\n"));
          bonlyGenericBonly.createNow = false;
        }
        else if (!(bool) (UnityEngine.Object) bonlyGenericBonly.transform.parent)
        {
          Debug.LogWarning((object) ("No parent to " + bonlyGenericBonly.name + ".\nRead the Setup generic section in the manual.\n"));
          bonlyGenericBonly.createNow = false;
        }
        else
        {
          bool flag = false;
          for (int index = 0; index < bonlyGenericBonly.transform.parent.childCount; ++index)
          {
            Transform child = bonlyGenericBonly.transform.parent.GetChild(index);
            if (child.name.ToLower().Contains("ragdoll"))
            {
              bonlyGenericBonly.numberOfTransforms = child.GetComponentsInChildren<Transform>().Length;
              Array.Resize<Transform>(ref bonlyGenericBonly.ragdollBones, bonlyGenericBonly.numberOfTransforms);
              Array.Resize<Transform>(ref bonlyGenericBonly.masterBones, bonlyGenericBonly.numberOfTransforms - 1);
              bonlyGenericBonly.ragdollBones = child.GetComponentsInChildren<Transform>();
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            Debug.LogWarning((object) ("No ragdoll found as sibling to " + bonlyGenericBonly.name + ".\nRead the Setup generic section in the manual.\n"));
            bonlyGenericBonly.createNow = false;
          }
          else
          {
            Transform[] componentsInChildren = bonlyGenericBonly.GetComponentsInChildren<Transform>();
            for (int index1 = 1; index1 < bonlyGenericBonly.numberOfTransforms; ++index1)
            {
              for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
              {
                if (componentsInChildren[index2].name == bonlyGenericBonly.ragdollBones[index1].name)
                {
                  bonlyGenericBonly.masterBones[index1 - 1] = componentsInChildren[index2];
                  break;
                }
              }
            }
            bonlyGenericBonly.animFollow = bonlyGenericBonly.gameObject.AddComponent<AnimFollow_PBC>();
            bonlyGenericBonly.animFollow.accStiffness = 0.15f;
            bonlyGenericBonly.gameObject.AddComponent<SeeAnimatedMaster_PBC>();
            bonlyGenericBonly.gameObject.AddComponent<UserCustomIK_BOnly>();
            bonlyGenericBonly.gameObject.AddComponent<MoveScript_BOnly>();
            bonlyGenericBonly.gameObject.AddComponent<Main_BOnly>();
            Array.Resize<Transform>(ref bonlyGenericBonly.animFollow.masterRigidTransforms, bonlyGenericBonly.numberOfTransforms - 1);
            Array.Resize<Transform>(ref bonlyGenericBonly.animFollow.ragdollRigidTransforms, bonlyGenericBonly.numberOfTransforms - 1);
            Array.Resize<Rigidbody>(ref bonlyGenericBonly.animFollow.ragdollRigidbodies, bonlyGenericBonly.numberOfTransforms - 1);
            Array.Resize<Transform>(ref bonlyGenericBonly.animFollow.masterConnectedTransforms, bonlyGenericBonly.numberOfTransforms - 1);
            Array.Resize<ConfigurableJoint>(ref bonlyGenericBonly.animFollow.configurableJoints, bonlyGenericBonly.numberOfTransforms - 1);
            CapsuleCollider capsuleCollider = bonlyGenericBonly.gameObject.AddComponent<CapsuleCollider>();
            bonlyGenericBonly.animFollow.oneTrigger = capsuleCollider;
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 1.6f;
            capsuleCollider.center = 1.1f * Vector3.up;
            capsuleCollider.isTrigger = true;
            capsuleCollider.enabled = false;
            capsuleCollider.gameObject.layer = LayerMask.NameToLayer("Water");
            Rigidbody rigidbody = bonlyGenericBonly.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            Transform ragdollBone = bonlyGenericBonly.ragdollBones[0];
            bonlyGenericBonly.ragdollBones[1].gameObject.AddComponent<SphereCollider>();
            bonlyGenericBonly.ragdollBones[1].gameObject.AddComponent<Rigidbody>();
            bonlyGenericBonly.ragdollBones[1].GetComponent<SphereCollider>().center = bonlyGenericBonly.ragdollBones[1].InverseTransformPoint((bonlyGenericBonly.ragdollBones[1].position + bonlyGenericBonly.ragdollBones[1].GetChild(0).position) * 0.5f) * bonlyGenericBonly.transform.localScale.y;
            float num1 = bonlyGenericBonly.ragdollBones[1].GetComponent<SphereCollider>().radius = Vector3.Distance(bonlyGenericBonly.ragdollBones[1].position, bonlyGenericBonly.ragdollBones[1].GetChild(0).position) * 0.5f;
            bonlyGenericBonly.ragdollBones[1].GetComponent<Rigidbody>().mass = (float) ((double) num1 * (double) num1 * (double) num1 * 8000.0);
            bonlyGenericBonly.animFollow.masterRigidTransforms[0] = bonlyGenericBonly.masterBones[0];
            bonlyGenericBonly.animFollow.ragdollRigidTransforms[0] = bonlyGenericBonly.ragdollBones[1];
            bonlyGenericBonly.ragdollBones[1].gameObject.AddComponent<Limb_Generic_BOnly>();
            for (int index = 2; index < bonlyGenericBonly.numberOfTransforms; ++index)
            {
              SphereCollider sphereCollider = bonlyGenericBonly.ragdollBones[index].gameObject.AddComponent<SphereCollider>();
              bonlyGenericBonly.ragdollBones[index].gameObject.AddComponent<Rigidbody>();
              bonlyGenericBonly.ragdollBones[index].gameObject.AddComponent<ConfigurableJoint>();
              if (bonlyGenericBonly.ragdollBones[index].childCount > 0)
              {
                sphereCollider.center = bonlyGenericBonly.ragdollBones[index].InverseTransformPoint((bonlyGenericBonly.ragdollBones[index].position + bonlyGenericBonly.ragdollBones[index].GetChild(0).position) * 0.5f) * bonlyGenericBonly.transform.localScale.y;
                sphereCollider.radius = Vector3.Distance(bonlyGenericBonly.ragdollBones[index].position, bonlyGenericBonly.ragdollBones[index].GetChild(0).position) * 0.4f;
              }
              else
                sphereCollider.radius = Vector3.Distance(bonlyGenericBonly.ragdollBones[index].position, bonlyGenericBonly.ragdollBones[index].parent.position) * 0.4f;
              bonlyGenericBonly.ragdollBones[index].GetComponent<Rigidbody>().mass = (float) ((double) sphereCollider.radius * (double) sphereCollider.radius * (double) sphereCollider.radius * 4000.0);
              bonlyGenericBonly.animFollow.masterRigidTransforms[index - 1] = bonlyGenericBonly.masterBones[index - 1];
              bonlyGenericBonly.animFollow.ragdollRigidTransforms[index - 1] = bonlyGenericBonly.ragdollBones[index];
              bonlyGenericBonly.ragdollBones[index].gameObject.AddComponent<Limb_Generic_BOnly>();
              Vector3 vector3_1 = Quaternion.Inverse(bonlyGenericBonly.ragdollBones[index].rotation) * bonlyGenericBonly.transform.forward * 0.707f;
              vector3_1 = new Vector3(Mathf.Round(vector3_1.x), Mathf.Round(vector3_1.y), Mathf.Round(vector3_1.z));
              if (vector3_1 == Vector3.zero)
                Debug.LogWarning((object) ("Auto setup failed.\nYou need to manually set the configurable joint's secondary axis on " + bonlyGenericBonly.ragdollBones[index].name + ".\n"));
              Vector3 vector3_2 = Quaternion.Inverse(bonlyGenericBonly.ragdollBones[index].rotation) * bonlyGenericBonly.transform.right * 0.707f;
              vector3_2 = new Vector3(Mathf.Round(vector3_2.x), Mathf.Round(vector3_2.y), Mathf.Round(vector3_2.z));
              if (vector3_2 == Vector3.zero)
                Debug.LogWarning((object) ("Auto setup failed.\nYou need to manually set the configurable joint's axis on " + bonlyGenericBonly.ragdollBones[index].name + ".\n"));
              ConfigurableJoint component = bonlyGenericBonly.ragdollBones[index].GetComponent<ConfigurableJoint>();
              component.connectedBody = bonlyGenericBonly.ragdollBones[index].parent.GetComponent<Rigidbody>();
              component.axis = vector3_2;
              component.secondaryAxis = vector3_1;
              component.xMotion = ConfigurableJointMotion.Locked;
              component.yMotion = ConfigurableJointMotion.Locked;
              component.zMotion = ConfigurableJointMotion.Locked;
              component.angularXMotion = ConfigurableJointMotion.Limited;
              component.angularYMotion = ConfigurableJointMotion.Limited;
              component.angularZMotion = ConfigurableJointMotion.Limited;
              SoftJointLimit softJointLimit = new SoftJointLimit();
              softJointLimit.limit = -30f;
              component.lowAngularXLimit = softJointLimit;
              softJointLimit.limit = 30f;
              component.highAngularXLimit = softJointLimit;
              softJointLimit.limit = 20f;
              component.angularYLimit = softJointLimit;
              softJointLimit.limit = 20f;
              component.angularZLimit = softJointLimit;
              component.rotationDriveMode = RotationDriveMode.Slerp;
              JointDrive jointDrive = new JointDrive();
              component.slerpDrive = component.slerpDrive;
            }
            if (!(bool) (UnityEngine.Object) (bonlyGenericBonly.gameObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController) Resources.Load("Animator_BOnly_Generic", typeof (RuntimeAnimatorController))))
              Debug.LogWarning((object) "Could not load runtimeAnimatorController \"Animator_BOnly_Generic\".\nIt must be in a folder named \"Resources\".\n");
            bonlyGenericBonly.gameObject.AddComponent<HashIDs_PBC>();
            RagdollControl_PBC ragdollControlPbc = ragdollBone.gameObject.AddComponent<RagdollControl_PBC>();
            ragdollControlPbc.master = bonlyGenericBonly.transform;
            ragdollControlPbc.useFallAnimation = false;
            foreach (Component ragdollRigidTransform in bonlyGenericBonly.animFollow.ragdollRigidTransforms)
              ragdollRigidTransform.gameObject.layer = LayerMask.NameToLayer("Water");
            bonlyGenericBonly.ragdollBones[0].gameObject.layer = LayerMask.NameToLayer("Water");
            Transform[] transformArray = new Transform[bonlyGenericBonly.numberOfTransforms];
            int index3 = 0;
            foreach (Transform ragdollRigidTransform in bonlyGenericBonly.animFollow.ragdollRigidTransforms)
            {
              if ((bool) (UnityEngine.Object) ragdollRigidTransform.GetComponent<ConfigurableJoint>())
              {
                bonlyGenericBonly.animFollow.configurableJoints[index3] = ragdollRigidTransform.GetComponent<ConfigurableJoint>();
                transformArray[index3] = ragdollRigidTransform.GetComponent<ConfigurableJoint>().connectedBody.transform;
              }
              bonlyGenericBonly.animFollow.ragdollRigidbodies[index3] = ragdollRigidTransform.GetComponent<Rigidbody>();
              ++index3;
            }
            int index4 = 0;
            foreach (Transform transform1 in transformArray)
            {
              if ((bool) (UnityEngine.Object) transform1)
              {
                foreach (Transform transform2 in componentsInChildren)
                {
                  if (transform2.name == transform1.name)
                  {
                    bonlyGenericBonly.animFollow.masterConnectedTransforms[index4] = transform2;
                    ++index4;
                    break;
                  }
                }
              }
              else
                ++index4;
            }
            int num2 = bonlyGenericBonly.animFollow.jointStrengthProfile.Length;
            int numberOfTransforms = bonlyGenericBonly.numberOfTransforms;
            Array.Resize<float>(ref bonlyGenericBonly.animFollow.jointStrengthProfile, numberOfTransforms);
            if (num2 == 0)
            {
              bonlyGenericBonly.animFollow.jointStrengthProfile[0] = 1f;
              num2 = 1;
            }
            for (int index5 = 1; index5 <= numberOfTransforms - num2; ++index5)
              bonlyGenericBonly.animFollow.jointStrengthProfile[numberOfTransforms - index5] = bonlyGenericBonly.animFollow.jointStrengthProfile[num2 - 1];
            int num3 = bonlyGenericBonly.animFollow.jointDampingProfile.Length;
            Array.Resize<float>(ref bonlyGenericBonly.animFollow.jointDampingProfile, numberOfTransforms);
            if (num3 == 0)
            {
              bonlyGenericBonly.animFollow.jointDampingProfile[0] = 1f;
              num3 = 1;
            }
            for (int index6 = 1; index6 <= numberOfTransforms - num3; ++index6)
              bonlyGenericBonly.animFollow.jointDampingProfile[numberOfTransforms - index6] = bonlyGenericBonly.animFollow.jointDampingProfile[num3 - 1];
            int num4 = bonlyGenericBonly.animFollow.forceStrengthProfile.Length;
            Array.Resize<float>(ref bonlyGenericBonly.animFollow.forceStrengthProfile, numberOfTransforms);
            if (num4 == 0)
            {
              bonlyGenericBonly.animFollow.forceStrengthProfile[0] = 1f;
              num4 = 1;
            }
            for (int index7 = 1; index7 <= numberOfTransforms - num4; ++index7)
              bonlyGenericBonly.animFollow.forceStrengthProfile[numberOfTransforms - index7] = bonlyGenericBonly.animFollow.forceStrengthProfile[num4 - 1];
            bonlyGenericBonly.transform.parent.position = bonlyGenericBonly.thisPosition;
            bonlyGenericBonly.transform.parent.rotation = bonlyGenericBonly.thisRotation;
            Debug.Log((object) "Ragdoll created.\nCheck that collider sizes are good.\n");
            if (false)
              bonlyGenericBonly.createNow = false;
            else
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) bonlyGenericBonly);
          }
        }
      }
    }
  }
}
