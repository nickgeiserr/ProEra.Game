// Decompiled with JetBrains decompiler
// Type: BOnly.CreateBOnlyCharacter_BOnly
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
  public class CreateBOnlyCharacter_BOnly : MonoBehaviour
  {
    private Animator animator;
    private Main_BOnly main;
    private AnimFollow_PBC animFollow;
    private SeeAnimatedMaster_PBC seeAnimatedMaster;
    public volatile bool createNow;
    public bool ragdollShoulders;
    public bool ragdollNeck;
    private float lengthScale;
    private float volumeScale;
    private bool shortFeet;
    private Avatar avatar;
    private Vector3 thisPosition;
    private Quaternion thisRotation;
    private int numberOfTransforms;
    private Transform hips;
    private Transform leftThigh;
    private Transform leftCalf;
    private Transform leftFoot;
    private Transform leftToe2;
    private Transform rightThigh;
    private Transform rightCalf;
    private Transform rightFoot;
    private Transform rightToe2;
    private Transform spine1;
    private Transform spine2;
    private Transform neck;
    private Transform head;
    private Transform leftClavicle;
    private Transform leftBiceps;
    private Transform leftForearm;
    private Transform leftHand;
    private Transform rightClavicle;
    private Transform rightBiceps;
    private Transform rightForearm;
    private Transform rightHand;

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
      CreateBOnlyCharacter_BOnly bonlyCharacterBonly1 = this;
      while (!bonlyCharacterBonly1.createNow)
      {
        while (!bonlyCharacterBonly1.createNow)
          yield return (object) null;
        if (!(bool) (UnityEngine.Object) (bonlyCharacterBonly1.avatar = bonlyCharacterBonly1.animator.avatar))
        {
          Debug.LogWarning((object) ("No avatar on animator of " + bonlyCharacterBonly1.name + ".\n"));
          bonlyCharacterBonly1.createNow = false;
        }
        else if (!bonlyCharacterBonly1.avatar.isHuman)
        {
          Debug.LogWarning((object) ("Avatar on " + bonlyCharacterBonly1.name + " is not human.\nThis script requires the avatar to be human.\n"));
          bonlyCharacterBonly1.createNow = false;
        }
        else
        {
          if (bonlyCharacterBonly1.numberOfTransforms == 0)
            bonlyCharacterBonly1.numberOfTransforms = bonlyCharacterBonly1.AssignBoneTransforms();
          if (bonlyCharacterBonly1.numberOfTransforms == 0)
          {
            bonlyCharacterBonly1.createNow = false;
          }
          else
          {
            bonlyCharacterBonly1.animFollow = bonlyCharacterBonly1.gameObject.AddComponent<AnimFollow_PBC>();
            bonlyCharacterBonly1.animFollow.accStiffness = 0.15f;
            bonlyCharacterBonly1.seeAnimatedMaster = bonlyCharacterBonly1.gameObject.AddComponent<SeeAnimatedMaster_PBC>();
            bonlyCharacterBonly1.gameObject.AddComponent<UserCustomIK_BOnly>();
            bonlyCharacterBonly1.gameObject.AddComponent<MoveScript_BOnly>();
            bonlyCharacterBonly1.main = bonlyCharacterBonly1.gameObject.AddComponent<Main_BOnly>();
            Array.Resize<Transform>(ref bonlyCharacterBonly1.animFollow.masterRigidTransforms, bonlyCharacterBonly1.numberOfTransforms);
            Array.Resize<Transform>(ref bonlyCharacterBonly1.animFollow.ragdollRigidTransforms, bonlyCharacterBonly1.numberOfTransforms);
            Array.Resize<Rigidbody>(ref bonlyCharacterBonly1.animFollow.ragdollRigidbodies, bonlyCharacterBonly1.numberOfTransforms);
            Array.Resize<Transform>(ref bonlyCharacterBonly1.animFollow.masterConnectedTransforms, bonlyCharacterBonly1.numberOfTransforms);
            Array.Resize<ConfigurableJoint>(ref bonlyCharacterBonly1.animFollow.configurableJoints, bonlyCharacterBonly1.numberOfTransforms);
            CreateBOnlyCharacter_BOnly bonlyCharacterBonly2 = bonlyCharacterBonly1;
            Vector3 vector3_1 = bonlyCharacterBonly1.head.position - bonlyCharacterBonly1.rightFoot.position;
            double num1 = (double) vector3_1.magnitude / 1.5299999713897705;
            bonlyCharacterBonly2.lengthScale = (float) num1;
            bonlyCharacterBonly1.volumeScale = Mathf.Pow(bonlyCharacterBonly1.lengthScale, 3f);
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.main)
              bonlyCharacterBonly1.main.realCharacterScale = bonlyCharacterBonly1.lengthScale;
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.transform.parent)
            {
              GameObject gameObject = bonlyCharacterBonly1.transform.parent.gameObject;
              bonlyCharacterBonly1.transform.parent = (Transform) null;
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject);
            }
            GameObject gameObject1 = new GameObject("BasicOnly_" + bonlyCharacterBonly1.name);
            gameObject1.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            bonlyCharacterBonly1.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            bonlyCharacterBonly1.transform.parent = gameObject1.transform;
            CapsuleCollider capsuleCollider = bonlyCharacterBonly1.gameObject.AddComponent<CapsuleCollider>();
            bonlyCharacterBonly1.animFollow.oneTrigger = capsuleCollider;
            capsuleCollider.radius = 0.5f * bonlyCharacterBonly1.lengthScale;
            capsuleCollider.height = 1.6f * bonlyCharacterBonly1.lengthScale;
            capsuleCollider.center = 1.1f * bonlyCharacterBonly1.lengthScale * Vector3.up;
            capsuleCollider.isTrigger = true;
            capsuleCollider.enabled = false;
            capsuleCollider.gameObject.layer = LayerMask.NameToLayer("Water");
            Rigidbody rigidbody = bonlyCharacterBonly1.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            bonlyCharacterBonly1.transform.rotation = Quaternion.identity;
            vector3_1 = Vector3.Cross(bonlyCharacterBonly1.rightBiceps.position - bonlyCharacterBonly1.leftBiceps.position, bonlyCharacterBonly1.head.position - bonlyCharacterBonly1.hips.position);
            Vector3 vector3_2 = vector3_1.normalized;
            vector3_2 = new Vector3(Mathf.Round(vector3_2.x), Mathf.Round(vector3_2.y), Mathf.Round(vector3_2.z));
            vector3_1 = Vector3.Cross(vector3_2, bonlyCharacterBonly1.rightBiceps.position - bonlyCharacterBonly1.leftBiceps.position);
            Vector3 vector3_3 = vector3_1.normalized;
            vector3_3 = new Vector3(Mathf.Round(vector3_3.x), Mathf.Round(vector3_3.y), Mathf.Round(vector3_3.z));
            vector3_1 = Vector3.Cross(vector3_2, vector3_3);
            Vector3 dir = -vector3_1.normalized;
            dir = new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), Mathf.Round(dir.z));
            Debug.DrawRay(bonlyCharacterBonly1.head.position, vector3_2, Color.blue, 5f);
            Debug.DrawRay(bonlyCharacterBonly1.head.position, vector3_3, Color.yellow, 5f);
            Debug.DrawRay(bonlyCharacterBonly1.head.position, dir, Color.red, 5f);
            GameObject gameObject2 = new GameObject("ragdoll_" + bonlyCharacterBonly1.name);
            gameObject2.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            gameObject2.transform.parent = gameObject1.transform;
            int index1 = 0;
            GameObject gameObject3 = new GameObject(bonlyCharacterBonly1.hips.name, new System.Type[2]
            {
              typeof (SphereCollider),
              typeof (Rigidbody)
            });
            gameObject3.transform.rotation = bonlyCharacterBonly1.hips.rotation;
            gameObject3.transform.position = bonlyCharacterBonly1.hips.position;
            gameObject3.transform.parent = gameObject2.transform;
            gameObject3.GetComponent<SphereCollider>().center = bonlyCharacterBonly1.hips.InverseTransformPoint((bonlyCharacterBonly1.spine1.position + bonlyCharacterBonly1.hips.position) * 0.5f) * bonlyCharacterBonly1.transform.localScale.y;
            gameObject3.GetComponent<SphereCollider>().radius = 0.13f * bonlyCharacterBonly1.lengthScale;
            gameObject3.GetComponent<Rigidbody>().mass = 15f * bonlyCharacterBonly1.volumeScale;
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index1] = bonlyCharacterBonly1.hips;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index1] = gameObject3.transform;
            gameObject3.AddComponent<Limb_BOnly>();
            int index2 = index1 + 1;
            Vector3 secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftThigh.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis1 = Quaternion.Inverse(bonlyCharacterBonly1.leftThigh.rotation) * dir * 0.707f;
            axis1 = new Vector3(Mathf.Round(axis1.x), Mathf.Round(axis1.y), Mathf.Round(axis1.z));
            GameObject ragdollLimb1 = new GameObject(bonlyCharacterBonly1.leftThigh.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftThigh, ragdollLimb1, gameObject3.transform, bonlyCharacterBonly1.leftCalf, new Vector4(-20f, 70f, 20f, 20f), 0.08f, 9f, axis1, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index2] = bonlyCharacterBonly1.leftThigh;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index2] = ragdollLimb1.transform;
            ragdollLimb1.AddComponent<Limb_BOnly>();
            int index3 = index2 + 1;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftCalf.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis2 = Quaternion.Inverse(bonlyCharacterBonly1.leftCalf.rotation) * dir * 0.707f;
            axis2 = new Vector3(Mathf.Round(axis2.x), Mathf.Round(axis2.y), Mathf.Round(axis2.z));
            GameObject ragdollLimb2 = new GameObject(bonlyCharacterBonly1.leftCalf.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            GameObject gameObject4 = new GameObject("tempGo");
            gameObject4.transform.position = bonlyCharacterBonly1.leftFoot.position + (bonlyCharacterBonly1.leftFoot.position - bonlyCharacterBonly1.leftCalf.position) * 0.2f;
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftCalf, ragdollLimb2, ragdollLimb1.transform, bonlyCharacterBonly1.leftFoot, new Vector4(-120f, 0.0f, 5f, 10f), 0.06f, 4f, axis2, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index3] = bonlyCharacterBonly1.leftCalf;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index3] = ragdollLimb2.transform;
            ragdollLimb2.AddComponent<Limb_BOnly>();
            int index4 = index3 + 1;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftFoot.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis3 = Quaternion.Inverse(bonlyCharacterBonly1.leftFoot.rotation) * dir * 0.707f;
            axis3 = new Vector3(Mathf.Round(axis3.x), Mathf.Round(axis3.y), Mathf.Round(axis3.z));
            GameObject ragdollLimb3 = new GameObject(bonlyCharacterBonly1.leftFoot.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = !bonlyCharacterBonly1.shortFeet ? bonlyCharacterBonly1.leftToe2.position : (bonlyCharacterBonly1.leftFoot.position + bonlyCharacterBonly1.leftToe2.position) * 0.5f;
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftFoot, ragdollLimb3, ragdollLimb2.transform, gameObject4.transform, new Vector4(-30f, 30f, 30f, 20f), 0.05f, 2f, axis3, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index4] = bonlyCharacterBonly1.leftFoot;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index4] = ragdollLimb3.transform;
            bonlyCharacterBonly1.seeAnimatedMaster.leftFoot = bonlyCharacterBonly1.leftFoot;
            ragdollLimb3.AddComponent<LimbFoot_BOnly>();
            int index5 = index4 + 1;
            GameObject gameObject5 = bonlyCharacterBonly1.gameObject;
            bool flag1 = true;
            for (int index6 = 0; index6 < bonlyCharacterBonly1.leftFoot.childCount; ++index6)
            {
              if (bonlyCharacterBonly1.leftFoot.GetChild(index6).name.ToLower().Contains("heel"))
              {
                flag1 = false;
                gameObject5 = bonlyCharacterBonly1.leftFoot.GetChild(index6).gameObject;
                break;
              }
            }
            if (flag1)
            {
              gameObject5 = new GameObject("LeftHeel");
              gameObject5.transform.parent = bonlyCharacterBonly1.leftFoot;
              gameObject5.transform.position = bonlyCharacterBonly1.leftFoot.position + Vector3.Dot(bonlyCharacterBonly1.leftToe2.position - bonlyCharacterBonly1.leftFoot.position, bonlyCharacterBonly1.transform.up) * bonlyCharacterBonly1.transform.up;
            }
            bonlyCharacterBonly1.seeAnimatedMaster.leftHeel = gameObject5.transform;
            bonlyCharacterBonly1.seeAnimatedMaster.leftToe2 = bonlyCharacterBonly1.leftToe2;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightThigh.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis4 = Quaternion.Inverse(bonlyCharacterBonly1.rightThigh.rotation) * dir * 0.707f;
            axis4 = new Vector3(Mathf.Round(axis4.x), Mathf.Round(axis4.y), Mathf.Round(axis4.z));
            GameObject ragdollLimb4 = new GameObject(bonlyCharacterBonly1.rightThigh.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightThigh, ragdollLimb4, gameObject3.transform, bonlyCharacterBonly1.rightCalf, new Vector4(-20f, 70f, 20f, 20f), 0.08f, 9f, axis4, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index5] = bonlyCharacterBonly1.rightThigh;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index5] = ragdollLimb4.transform;
            ragdollLimb4.AddComponent<Limb_BOnly>();
            int index7 = index5 + 1;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightCalf.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis5 = Quaternion.Inverse(bonlyCharacterBonly1.rightCalf.rotation) * dir * 0.707f;
            axis5 = new Vector3(Mathf.Round(axis5.x), Mathf.Round(axis5.y), Mathf.Round(axis5.z));
            GameObject ragdollLimb5 = new GameObject(bonlyCharacterBonly1.rightCalf.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = bonlyCharacterBonly1.rightFoot.position + (bonlyCharacterBonly1.rightFoot.position - bonlyCharacterBonly1.rightCalf.position) * 0.2f;
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightCalf, ragdollLimb5, ragdollLimb4.transform, bonlyCharacterBonly1.rightFoot, new Vector4(-120f, 0.0f, 5f, 10f), 0.06f, 4f, axis5, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index7] = bonlyCharacterBonly1.rightCalf;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index7] = ragdollLimb5.transform;
            ragdollLimb5.AddComponent<Limb_BOnly>();
            int index8 = index7 + 1;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightFoot.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis6 = Quaternion.Inverse(bonlyCharacterBonly1.rightFoot.rotation) * dir * 0.707f;
            axis6 = new Vector3(Mathf.Round(axis6.x), Mathf.Round(axis6.y), Mathf.Round(axis6.z));
            GameObject ragdollLimb6 = new GameObject(bonlyCharacterBonly1.rightFoot.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = !bonlyCharacterBonly1.shortFeet ? bonlyCharacterBonly1.rightToe2.position : (bonlyCharacterBonly1.rightFoot.position + bonlyCharacterBonly1.rightToe2.position) * 0.5f;
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightFoot, ragdollLimb6, ragdollLimb5.transform, gameObject4.transform, new Vector4(-30f, 30f, 30f, 20f), 0.05f, 2f, axis6, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index8] = bonlyCharacterBonly1.rightFoot;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index8] = ragdollLimb6.transform;
            bonlyCharacterBonly1.seeAnimatedMaster.rightFoot = bonlyCharacterBonly1.rightFoot;
            ragdollLimb6.AddComponent<LimbFoot_BOnly>();
            int index9 = index8 + 1;
            GameObject gameObject6 = bonlyCharacterBonly1.gameObject;
            bool flag2 = true;
            for (int index10 = 0; index10 < bonlyCharacterBonly1.rightFoot.childCount; ++index10)
            {
              if (bonlyCharacterBonly1.rightFoot.GetChild(index10).name.ToLower().Contains("heel"))
              {
                flag2 = false;
                gameObject6 = bonlyCharacterBonly1.rightFoot.GetChild(index10).gameObject;
                break;
              }
            }
            if (flag2)
            {
              gameObject6 = new GameObject("RightHeel");
              gameObject6.transform.parent = bonlyCharacterBonly1.rightFoot;
              gameObject6.transform.position = bonlyCharacterBonly1.rightFoot.position + Vector3.Dot(bonlyCharacterBonly1.leftToe2.position - bonlyCharacterBonly1.leftFoot.position, bonlyCharacterBonly1.transform.up) * bonlyCharacterBonly1.transform.up;
            }
            bonlyCharacterBonly1.seeAnimatedMaster.rightHeel = gameObject6.transform;
            bonlyCharacterBonly1.seeAnimatedMaster.rightToe2 = bonlyCharacterBonly1.rightToe2;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.spine1.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis7 = Quaternion.Inverse(bonlyCharacterBonly1.spine1.rotation) * dir * 0.707f;
            axis7 = new Vector3(Mathf.Round(axis7.x), Mathf.Round(axis7.y), Mathf.Round(axis7.z));
            GameObject ragdollLimb7 = new GameObject(bonlyCharacterBonly1.spine1.name, new System.Type[3]
            {
              typeof (SphereCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.spine1, ragdollLimb7, gameObject3.transform, bonlyCharacterBonly1.spine2, new Vector4(-20f, 20f, 20f, 20f), -0.13f, 8f, axis7, secondaryAxis);
            else
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.spine1, ragdollLimb7, gameObject3.transform, bonlyCharacterBonly1.spine1, new Vector4(-20f, 20f, 20f, 20f), -0.13f, 8f, axis7, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index9] = bonlyCharacterBonly1.spine1;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index9] = ragdollLimb7.transform;
            ragdollLimb7.AddComponent<Limb_BOnly>();
            int index11 = index9 + 1;
            GameObject ragdollLimb8 = bonlyCharacterBonly1.gameObject;
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
            {
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.spine2.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis8 = Quaternion.Inverse(bonlyCharacterBonly1.spine2.rotation) * dir * 0.707f;
              axis8 = new Vector3(Mathf.Round(axis8.x), Mathf.Round(axis8.y), Mathf.Round(axis8.z));
              ragdollLimb8 = new GameObject(bonlyCharacterBonly1.spine2.name, new System.Type[3]
              {
                typeof (SphereCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.spine2, ragdollLimb8, ragdollLimb7.transform, bonlyCharacterBonly1.head.parent, new Vector4(-20f, 20f, 20f, 20f), -0.13f, 12f, axis8, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index11] = bonlyCharacterBonly1.spine2;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index11] = ragdollLimb8.transform;
              ragdollLimb8.AddComponent<Limb_BOnly>();
              ++index11;
            }
            GameObject ragdollLimb9 = (GameObject) null;
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.neck)
            {
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.neck.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis9 = Quaternion.Inverse(bonlyCharacterBonly1.neck.rotation) * dir * 0.707f;
              axis9 = new Vector3(Mathf.Round(axis9.x), Mathf.Round(axis9.y), Mathf.Round(axis9.z));
              ragdollLimb9 = new GameObject(bonlyCharacterBonly1.neck.name, new System.Type[3]
              {
                typeof (SphereCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              gameObject4.transform.position = bonlyCharacterBonly1.head.position;
              if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.neck, ragdollLimb9, ragdollLimb8.transform, gameObject4.transform, new Vector4(-10f, 10f, 10f, 10f), -0.08f, 3f, axis9, secondaryAxis);
              else
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.neck, ragdollLimb9, ragdollLimb7.transform, gameObject4.transform, new Vector4(-10f, 10f, 10f, 10f), -0.08f, 3f, axis9, secondaryAxis);
              if (bonlyCharacterBonly1.ragdollShoulders && (bool) (UnityEngine.Object) bonlyCharacterBonly1.leftClavicle)
                ragdollLimb9.GetComponent<SphereCollider>().center += 0.06f * (Quaternion.Inverse(ragdollLimb9.transform.rotation) * bonlyCharacterBonly1.transform.rotation * Vector3.up) * bonlyCharacterBonly1.lengthScale;
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index11] = bonlyCharacterBonly1.neck;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index11] = ragdollLimb9.transform;
              ragdollLimb9.AddComponent<Limb_BOnly>();
              ++index11;
            }
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.head.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis10 = Quaternion.Inverse(bonlyCharacterBonly1.head.rotation) * dir * 0.707f;
            axis10 = new Vector3(Mathf.Round(axis10.x), Mathf.Round(axis10.y), Mathf.Round(axis10.z));
            GameObject ragdollLimb10 = new GameObject(bonlyCharacterBonly1.head.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = !(bool) (UnityEngine.Object) bonlyCharacterBonly1.neck ? bonlyCharacterBonly1.head.position + Vector3.up * 0.18f * bonlyCharacterBonly1.lengthScale : bonlyCharacterBonly1.head.position + Vector3.up * 0.21f * bonlyCharacterBonly1.lengthScale;
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.neck)
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.head, ragdollLimb10, ragdollLimb9.transform, gameObject4.transform, new Vector4(-40f, 40f, 40f, 40f), 0.1f, 3f, axis10, secondaryAxis);
            else if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.head, ragdollLimb10, ragdollLimb8.transform, gameObject4.transform, new Vector4(-40f, 40f, 40f, 40f), 0.1f, 3f, axis10, secondaryAxis);
            else
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.head, ragdollLimb10, ragdollLimb7.transform, gameObject4.transform, new Vector4(-40f, 40f, 40f, 40f), 0.1f, 3f, axis10, secondaryAxis);
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.neck)
              ragdollLimb10.GetComponent<CapsuleCollider>().height = 0.24f * bonlyCharacterBonly1.lengthScale;
            else
              ragdollLimb10.GetComponent<CapsuleCollider>().height = 0.3f * bonlyCharacterBonly1.lengthScale;
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index11] = bonlyCharacterBonly1.head;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index11] = ragdollLimb10.transform;
            ragdollLimb10.AddComponent<Limb_BOnly>();
            int index12 = index11 + 1;
            GameObject ragdollLimb11;
            int index13;
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.leftClavicle)
            {
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftClavicle.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis11 = Quaternion.Inverse(bonlyCharacterBonly1.leftClavicle.rotation) * vector3_3 * 0.707f;
              axis11 = new Vector3(Mathf.Round(axis11.x), Mathf.Round(axis11.y), Mathf.Round(axis11.z));
              GameObject ragdollLimb12 = new GameObject(bonlyCharacterBonly1.leftClavicle.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftClavicle, ragdollLimb12, ragdollLimb8.transform, bonlyCharacterBonly1.leftBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis11, secondaryAxis);
              else
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftClavicle, ragdollLimb12, ragdollLimb7.transform, bonlyCharacterBonly1.leftBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis11, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index12] = bonlyCharacterBonly1.leftClavicle;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index12] = ragdollLimb12.transform;
              ragdollLimb12.AddComponent<Limb_BOnly>();
              int index14 = index12 + 1;
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis12 = Quaternion.Inverse(bonlyCharacterBonly1.leftBiceps.rotation) * vector3_3 * 0.707f;
              axis12 = new Vector3(Mathf.Round(axis12.x), Mathf.Round(axis12.y), Mathf.Round(axis12.z));
              ragdollLimb11 = new GameObject(bonlyCharacterBonly1.leftBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftBiceps, ragdollLimb11, ragdollLimb12.transform, bonlyCharacterBonly1.leftForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis12, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index14] = bonlyCharacterBonly1.leftBiceps;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index14] = ragdollLimb11.transform;
              ragdollLimb11.AddComponent<Limb_BOnly>();
              index13 = index14 + 1;
            }
            else
            {
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis13 = Quaternion.Inverse(bonlyCharacterBonly1.leftBiceps.rotation) * vector3_3 * 0.707f;
              axis13 = new Vector3(Mathf.Round(axis13.x), Mathf.Round(axis13.y), Mathf.Round(axis13.z));
              ragdollLimb11 = new GameObject(bonlyCharacterBonly1.leftBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftBiceps, ragdollLimb11, ragdollLimb8.transform, bonlyCharacterBonly1.leftForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis13, secondaryAxis);
              else
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftBiceps, ragdollLimb11, ragdollLimb7.transform, bonlyCharacterBonly1.leftForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis13, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index12] = bonlyCharacterBonly1.leftBiceps;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index12] = ragdollLimb11.transform;
              ragdollLimb11.AddComponent<Limb_BOnly>();
              index13 = index12 + 1;
            }
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftForearm.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis14 = Quaternion.Inverse(bonlyCharacterBonly1.leftForearm.rotation) * vector3_3 * 0.707f;
            axis14 = new Vector3(Mathf.Round(axis14.x), Mathf.Round(axis14.y), Mathf.Round(axis14.z));
            GameObject ragdollLimb13 = new GameObject(bonlyCharacterBonly1.leftForearm.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = bonlyCharacterBonly1.leftHand.position + (bonlyCharacterBonly1.leftHand.position - bonlyCharacterBonly1.leftForearm.position) * 0.6f;
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftForearm, ragdollLimb13, ragdollLimb11.transform, gameObject4.transform, new Vector4(-90f, 0.0f, 5f, 5f), 0.04f, 2f, axis14, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index13] = bonlyCharacterBonly1.leftForearm;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index13] = ragdollLimb13.transform;
            ragdollLimb13.AddComponent<Limb_BOnly>();
            int index15 = index13 + 1;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.leftHand.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis15 = Quaternion.Inverse(bonlyCharacterBonly1.leftHand.rotation) * vector3_3 * 0.707f;
            axis15 = new Vector3(Mathf.Round(axis15.x), Mathf.Round(axis15.y), Mathf.Round(axis15.z));
            GameObject ragdollLimb14 = new GameObject(bonlyCharacterBonly1.leftHand.name, new System.Type[3]
            {
              typeof (SphereCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.leftHand, ragdollLimb14, ragdollLimb13.transform, bonlyCharacterBonly1.leftHand, new Vector4(-20f, 20f, 50f, 50f), -0.04f, 1f, axis15, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index15] = bonlyCharacterBonly1.leftHand;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index15] = ragdollLimb14.transform;
            ragdollLimb14.AddComponent<Limb_BOnly>();
            int index16 = index15 + 1;
            GameObject ragdollLimb15;
            int index17;
            if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.rightClavicle)
            {
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightClavicle.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis16 = Quaternion.Inverse(bonlyCharacterBonly1.rightClavicle.rotation) * vector3_3 * 0.707f;
              axis16 = new Vector3(Mathf.Round(axis16.x), Mathf.Round(axis16.y), Mathf.Round(axis16.z));
              GameObject ragdollLimb16 = new GameObject(bonlyCharacterBonly1.rightClavicle.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightClavicle, ragdollLimb16, ragdollLimb8.transform, bonlyCharacterBonly1.rightBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis16, secondaryAxis);
              else
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightClavicle, ragdollLimb16, ragdollLimb7.transform, bonlyCharacterBonly1.rightBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis16, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index16] = bonlyCharacterBonly1.rightClavicle;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index16] = ragdollLimb16.transform;
              ragdollLimb16.AddComponent<Limb_BOnly>();
              int index18 = index16 + 1;
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis17 = Quaternion.Inverse(bonlyCharacterBonly1.rightBiceps.rotation) * vector3_3 * 0.707f;
              axis17 = new Vector3(Mathf.Round(axis17.x), Mathf.Round(axis17.y), Mathf.Round(axis17.z));
              ragdollLimb15 = new GameObject(bonlyCharacterBonly1.rightBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightBiceps, ragdollLimb15, ragdollLimb16.transform, bonlyCharacterBonly1.rightForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis17, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index18] = bonlyCharacterBonly1.rightBiceps;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index18] = ragdollLimb15.transform;
              ragdollLimb15.AddComponent<Limb_BOnly>();
              index17 = index18 + 1;
            }
            else
            {
              secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              Vector3 axis18 = Quaternion.Inverse(bonlyCharacterBonly1.rightBiceps.rotation) * vector3_3 * 0.707f;
              axis18 = new Vector3(Mathf.Round(axis18.x), Mathf.Round(axis18.y), Mathf.Round(axis18.z));
              ragdollLimb15 = new GameObject(bonlyCharacterBonly1.rightBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) bonlyCharacterBonly1.spine2)
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightBiceps, ragdollLimb15, ragdollLimb8.transform, bonlyCharacterBonly1.rightForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis18, secondaryAxis);
              else
                bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightBiceps, ragdollLimb15, ragdollLimb7.transform, bonlyCharacterBonly1.rightForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis18, secondaryAxis);
              bonlyCharacterBonly1.animFollow.masterRigidTransforms[index16] = bonlyCharacterBonly1.rightBiceps;
              bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index16] = ragdollLimb15.transform;
              ragdollLimb15.AddComponent<Limb_BOnly>();
              index17 = index16 + 1;
            }
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightForearm.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis19 = Quaternion.Inverse(bonlyCharacterBonly1.rightForearm.rotation) * vector3_3 * 0.707f;
            axis19 = new Vector3(Mathf.Round(axis19.x), Mathf.Round(axis19.y), Mathf.Round(axis19.z));
            GameObject ragdollLimb17 = new GameObject(bonlyCharacterBonly1.rightForearm.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = bonlyCharacterBonly1.rightHand.position + (bonlyCharacterBonly1.rightHand.position - bonlyCharacterBonly1.rightForearm.position) * 0.6f;
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightForearm, ragdollLimb17, ragdollLimb15.transform, gameObject4.transform, new Vector4(0.0f, 90f, 5f, 5f), 0.04f, 2f, axis19, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index17] = bonlyCharacterBonly1.rightForearm;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index17] = ragdollLimb17.transform;
            ragdollLimb17.AddComponent<Limb_BOnly>();
            int index19 = index17 + 1;
            secondaryAxis = Quaternion.Inverse(bonlyCharacterBonly1.rightHand.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis20 = Quaternion.Inverse(bonlyCharacterBonly1.rightHand.rotation) * vector3_3 * 0.707f;
            axis20 = new Vector3(Mathf.Round(axis20.x), Mathf.Round(axis20.y), Mathf.Round(axis20.z));
            GameObject ragdollLimb18 = new GameObject(bonlyCharacterBonly1.rightHand.name, new System.Type[3]
            {
              typeof (SphereCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            bonlyCharacterBonly1.SetupCapsuleLimb(bonlyCharacterBonly1.rightHand, ragdollLimb18, ragdollLimb17.transform, bonlyCharacterBonly1.rightHand, new Vector4(-20f, 20f, 50f, 50f), -0.04f, 1f, axis20, secondaryAxis);
            bonlyCharacterBonly1.animFollow.masterRigidTransforms[index19] = bonlyCharacterBonly1.rightHand;
            bonlyCharacterBonly1.animFollow.ragdollRigidTransforms[index19] = ragdollLimb18.transform;
            ragdollLimb18.AddComponent<Limb_BOnly>();
            int num2 = index19 + 1;
            if (!(bool) (UnityEngine.Object) (bonlyCharacterBonly1.gameObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController) Resources.Load("Animator_BOnly", typeof (RuntimeAnimatorController))))
              Debug.LogWarning((object) "Could not load runtimeAnimatorController \"Animator_BOnly\".\nIt must be in a folder named \"Resources\".\n");
            bonlyCharacterBonly1.gameObject.AddComponent<HashIDs_PBC>();
            gameObject2.gameObject.AddComponent<RagdollControl_PBC>().master = bonlyCharacterBonly1.transform;
            foreach (Component ragdollRigidTransform in bonlyCharacterBonly1.animFollow.ragdollRigidTransforms)
              ragdollRigidTransform.gameObject.layer = LayerMask.NameToLayer("Water");
            gameObject2.layer = LayerMask.NameToLayer("Water");
            Transform[] transformArray = new Transform[bonlyCharacterBonly1.numberOfTransforms];
            int index20 = 0;
            foreach (Transform ragdollRigidTransform in bonlyCharacterBonly1.animFollow.ragdollRigidTransforms)
            {
              if ((bool) (UnityEngine.Object) ragdollRigidTransform.GetComponent<ConfigurableJoint>())
              {
                bonlyCharacterBonly1.animFollow.configurableJoints[index20] = ragdollRigidTransform.GetComponent<ConfigurableJoint>();
                transformArray[index20] = ragdollRigidTransform.GetComponent<ConfigurableJoint>().connectedBody.transform;
              }
              bonlyCharacterBonly1.animFollow.ragdollRigidbodies[index20] = ragdollRigidTransform.GetComponent<Rigidbody>();
              ++index20;
            }
            Transform[] componentsInChildren = bonlyCharacterBonly1.GetComponentsInChildren<Transform>();
            int index21 = 0;
            foreach (Transform transform1 in transformArray)
            {
              if ((bool) (UnityEngine.Object) transform1)
              {
                foreach (Transform transform2 in componentsInChildren)
                {
                  if (transform2.name == transform1.name)
                  {
                    bonlyCharacterBonly1.animFollow.masterConnectedTransforms[index21] = transform2;
                    ++index21;
                    break;
                  }
                }
              }
              else
                ++index21;
            }
            int num3 = bonlyCharacterBonly1.animFollow.jointStrengthProfile.Length;
            int numberOfTransforms = bonlyCharacterBonly1.numberOfTransforms;
            Array.Resize<float>(ref bonlyCharacterBonly1.animFollow.jointStrengthProfile, numberOfTransforms);
            if (num3 == 0)
            {
              bonlyCharacterBonly1.animFollow.jointStrengthProfile[0] = 1f;
              num3 = 1;
            }
            for (int index22 = 1; index22 <= numberOfTransforms - num3; ++index22)
              bonlyCharacterBonly1.animFollow.jointStrengthProfile[numberOfTransforms - index22] = bonlyCharacterBonly1.animFollow.jointStrengthProfile[num3 - 1];
            int num4 = bonlyCharacterBonly1.animFollow.jointDampingProfile.Length;
            Array.Resize<float>(ref bonlyCharacterBonly1.animFollow.jointDampingProfile, numberOfTransforms);
            if (num4 == 0)
            {
              bonlyCharacterBonly1.animFollow.jointDampingProfile[0] = 1f;
              num4 = 1;
            }
            for (int index23 = 1; index23 <= numberOfTransforms - num4; ++index23)
              bonlyCharacterBonly1.animFollow.jointDampingProfile[numberOfTransforms - index23] = bonlyCharacterBonly1.animFollow.jointDampingProfile[num4 - 1];
            int num5 = bonlyCharacterBonly1.animFollow.forceStrengthProfile.Length;
            Array.Resize<float>(ref bonlyCharacterBonly1.animFollow.forceStrengthProfile, numberOfTransforms);
            if (num5 == 0)
            {
              bonlyCharacterBonly1.animFollow.forceStrengthProfile[0] = 1f;
              num5 = 1;
            }
            for (int index24 = 1; index24 <= numberOfTransforms - num5; ++index24)
              bonlyCharacterBonly1.animFollow.forceStrengthProfile[numberOfTransforms - index24] = bonlyCharacterBonly1.animFollow.forceStrengthProfile[num5 - 1];
            gameObject1.transform.position = bonlyCharacterBonly1.thisPosition;
            gameObject1.transform.rotation = bonlyCharacterBonly1.thisRotation;
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject4);
            Debug.Log((object) "Ragdoll created.\nCheck that collider sizes are good.\n");
            if (false)
              bonlyCharacterBonly1.createNow = false;
            else
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) bonlyCharacterBonly1);
          }
        }
      }
    }

    public int AssignBoneTransforms()
    {
      int num = 19;
      if (!(bool) (UnityEngine.Object) (this.hips = this.animator.GetBoneTransform(HumanBodyBones.Hips)) || !(bool) (UnityEngine.Object) (this.leftThigh = this.animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg)) || !(bool) (UnityEngine.Object) (this.leftCalf = this.animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg)) || !(bool) (UnityEngine.Object) (this.leftFoot = this.animator.GetBoneTransform(HumanBodyBones.LeftFoot)))
      {
        Debug.LogWarning((object) ("Avatar on " + this.name + " is no good\n"));
        return 0;
      }
      if (!(bool) (UnityEngine.Object) this.leftToe2)
      {
        if (!(bool) (UnityEngine.Object) (this.leftToe2 = this.animator.GetBoneTransform(HumanBodyBones.LeftToes)))
        {
          if (this.leftFoot.childCount <= 0 || !(bool) (UnityEngine.Object) (this.leftToe2 = this.leftFoot.GetChild(0)))
            Debug.LogWarning((object) "Cannot find any toe transform on left foot.\nThe current version of the foot IK requires a toe transform\n");
        }
        else if (this.animator.GetBoneTransform(HumanBodyBones.LeftToes).childCount > 0)
          this.leftToe2 = this.animator.GetBoneTransform(HumanBodyBones.LeftToes).GetChild(0);
      }
      this.rightThigh = this.animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
      this.rightCalf = this.animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
      this.rightFoot = this.animator.GetBoneTransform(HumanBodyBones.RightFoot);
      if (!(bool) (UnityEngine.Object) this.rightToe2)
      {
        if (!(bool) (UnityEngine.Object) (this.rightToe2 = this.animator.GetBoneTransform(HumanBodyBones.RightToes)))
        {
          if (this.rightFoot.childCount <= 0 || !(bool) (UnityEngine.Object) (this.rightToe2 = this.rightFoot.GetChild(0)))
            Debug.LogWarning((object) "Cannot find any toe transform on right foot.\nThe current version of the foot IK requires a toe transform\n");
        }
        else if (this.animator.GetBoneTransform(HumanBodyBones.RightToes).childCount > 0)
          this.rightToe2 = this.animator.GetBoneTransform(HumanBodyBones.RightToes).GetChild(0);
      }
      this.spine1 = this.animator.GetBoneTransform(HumanBodyBones.Spine);
      if (!(bool) (UnityEngine.Object) (this.spine2 = this.animator.GetBoneTransform(HumanBodyBones.Chest)))
        --num;
      this.head = this.animator.GetBoneTransform(HumanBodyBones.Head);
      if (!this.ragdollNeck || !(bool) (UnityEngine.Object) (this.neck = this.animator.GetBoneTransform(HumanBodyBones.Neck)))
        --num;
      if (!this.ragdollShoulders || !(bool) (UnityEngine.Object) (this.leftClavicle = this.animator.GetBoneTransform(HumanBodyBones.LeftShoulder)))
        --num;
      this.leftBiceps = this.animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
      this.leftForearm = this.animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
      this.leftHand = this.animator.GetBoneTransform(HumanBodyBones.LeftHand);
      if (!this.ragdollShoulders || !(bool) (UnityEngine.Object) (this.rightClavicle = this.animator.GetBoneTransform(HumanBodyBones.RightShoulder)))
        --num;
      this.rightBiceps = this.animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
      this.rightForearm = this.animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
      this.rightHand = this.animator.GetBoneTransform(HumanBodyBones.RightHand);
      return num;
    }

    private void SetupCapsuleLimb(
      Transform masterLimb,
      GameObject ragdollLimb,
      Transform connectedTransform,
      Transform masterChild,
      Vector4 limits,
      float capsuleRadius,
      float mass,
      Vector3 axis,
      Vector3 secondaryAxis)
    {
      if ((double) capsuleRadius > 0.0)
      {
        ragdollLimb.transform.rotation = masterLimb.rotation;
        ragdollLimb.transform.position = masterLimb.position;
        ragdollLimb.transform.parent = connectedTransform;
        ragdollLimb.GetComponent<CapsuleCollider>().center = masterLimb.InverseTransformPoint((masterChild.position + masterLimb.position) * 0.5f) * this.transform.localScale.y;
        ragdollLimb.GetComponent<CapsuleCollider>().height = (masterChild.position - masterLimb.position).magnitude;
        ragdollLimb.GetComponent<CapsuleCollider>().radius = capsuleRadius * this.lengthScale;
        if ((double) Mathf.Abs(Vector3.Dot(ragdollLimb.transform.right, (masterChild.position - masterLimb.position).normalized)) > 0.75)
          ragdollLimb.GetComponent<CapsuleCollider>().direction = 0;
        else if ((double) Mathf.Abs(Vector3.Dot(ragdollLimb.transform.up, (masterChild.position - masterLimb.position).normalized)) > 0.75)
          ragdollLimb.GetComponent<CapsuleCollider>().direction = 1;
        else
          ragdollLimb.GetComponent<CapsuleCollider>().direction = 2;
      }
      else if ((double) capsuleRadius < 0.0)
      {
        ragdollLimb.transform.rotation = masterLimb.rotation;
        ragdollLimb.transform.position = masterLimb.position;
        ragdollLimb.transform.parent = connectedTransform;
        ragdollLimb.GetComponent<SphereCollider>().center = masterLimb.InverseTransformPoint((masterChild.position + masterLimb.position) * 0.5f) * this.transform.localScale.y;
        ragdollLimb.GetComponent<SphereCollider>().radius = -capsuleRadius * this.lengthScale;
      }
      ragdollLimb.GetComponent<Rigidbody>().mass = mass * this.volumeScale;
      ConfigurableJoint component = ragdollLimb.GetComponent<ConfigurableJoint>();
      component.connectedBody = ragdollLimb.transform.parent.GetComponent<Rigidbody>();
      component.axis = axis;
      component.secondaryAxis = secondaryAxis;
      component.xMotion = ConfigurableJointMotion.Locked;
      component.yMotion = ConfigurableJointMotion.Locked;
      component.zMotion = ConfigurableJointMotion.Locked;
      component.angularXMotion = ConfigurableJointMotion.Limited;
      component.angularYMotion = ConfigurableJointMotion.Limited;
      component.angularZMotion = ConfigurableJointMotion.Limited;
      SoftJointLimit softJointLimit = new SoftJointLimit();
      softJointLimit.limit = limits[0];
      component.lowAngularXLimit = softJointLimit;
      softJointLimit.limit = limits[1];
      component.highAngularXLimit = softJointLimit;
      softJointLimit.limit = limits[2];
      component.angularYLimit = softJointLimit;
      softJointLimit.limit = limits[3];
      component.angularZLimit = softJointLimit;
      component.rotationDriveMode = RotationDriveMode.Slerp;
      JointDrive jointDrive = new JointDrive();
      component.slerpDrive = component.slerpDrive;
    }
  }
}
