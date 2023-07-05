// Decompiled with JetBrains decompiler
// Type: PBC.CreatePBCCharacter_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using BOnly;
using System;
using System.Collections;
using UnityEngine;

namespace PBC
{
  [ExecuteInEditMode]
  public class CreatePBCCharacter_PBC : MonoBehaviour
  {
    private Animator animator;
    private Main_PBC main;
    private AnimFollow_PBC animFollow;
    private SeeAnimatedMaster_PBC seeAnimatedMaster;
    private AdvancedFootIK_PBC footIK;
    public bool createNow;
    public bool basicOnly;
    public bool addFootIK = true;
    public bool ragdollShoulders;
    public bool ragdollNeck;
    private float lengthScale;
    private float volumeScale;
    public ParticleSystem bloodPrefab;
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
      CreatePBCCharacter_PBC createPbcCharacterPbc1 = this;
      while (!createPbcCharacterPbc1.createNow)
      {
        while (!createPbcCharacterPbc1.createNow)
        {
          if (createPbcCharacterPbc1.basicOnly)
            createPbcCharacterPbc1.addFootIK = false;
          yield return (object) null;
        }
        if (createPbcCharacterPbc1.basicOnly)
        {
          CreateBOnlyCharacter_BOnly bonlyCharacterBonly = createPbcCharacterPbc1.gameObject.AddComponent<CreateBOnlyCharacter_BOnly>();
          bonlyCharacterBonly.ragdollShoulders = createPbcCharacterPbc1.ragdollShoulders;
          bonlyCharacterBonly.ragdollNeck = createPbcCharacterPbc1.ragdollNeck;
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) createPbcCharacterPbc1);
          createPbcCharacterPbc1.createNow = false;
        }
        else if (!(bool) (UnityEngine.Object) (createPbcCharacterPbc1.avatar = createPbcCharacterPbc1.animator.avatar))
        {
          Debug.LogWarning((object) ("No avatar on animator of " + createPbcCharacterPbc1.name + "\n"));
          createPbcCharacterPbc1.createNow = false;
        }
        else if (!createPbcCharacterPbc1.avatar.isHuman)
        {
          Debug.LogWarning((object) ("Avatar on " + createPbcCharacterPbc1.name + " is not human.\nThis script requires the avatar to be human"));
          createPbcCharacterPbc1.createNow = false;
        }
        else
        {
          if (createPbcCharacterPbc1.numberOfTransforms == 0)
            createPbcCharacterPbc1.numberOfTransforms = createPbcCharacterPbc1.AssignBoneTransforms();
          if (createPbcCharacterPbc1.numberOfTransforms == 0)
          {
            createPbcCharacterPbc1.createNow = false;
          }
          else
          {
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) createPbcCharacterPbc1.gameObject.GetComponent<AnimFollow_PBC>());
            createPbcCharacterPbc1.animFollow = createPbcCharacterPbc1.gameObject.AddComponent<AnimFollow_PBC>();
            createPbcCharacterPbc1.seeAnimatedMaster = createPbcCharacterPbc1.gameObject.AddComponent<SeeAnimatedMaster_PBC>();
            createPbcCharacterPbc1.gameObject.AddComponent<UserCustomIK_PBC>();
            createPbcCharacterPbc1.gameObject.AddComponent<AdvancedMoveScript_PBC>();
            createPbcCharacterPbc1.main = createPbcCharacterPbc1.gameObject.AddComponent<Main_PBC>();
            Array.Resize<Transform>(ref createPbcCharacterPbc1.animFollow.masterRigidTransforms, createPbcCharacterPbc1.numberOfTransforms);
            Array.Resize<Transform>(ref createPbcCharacterPbc1.animFollow.ragdollRigidTransforms, createPbcCharacterPbc1.numberOfTransforms);
            Array.Resize<Rigidbody>(ref createPbcCharacterPbc1.animFollow.ragdollRigidbodies, createPbcCharacterPbc1.numberOfTransforms);
            Array.Resize<Transform>(ref createPbcCharacterPbc1.animFollow.masterConnectedTransforms, createPbcCharacterPbc1.numberOfTransforms);
            Array.Resize<ConfigurableJoint>(ref createPbcCharacterPbc1.animFollow.configurableJoints, createPbcCharacterPbc1.numberOfTransforms);
            CreatePBCCharacter_PBC createPbcCharacterPbc2 = createPbcCharacterPbc1;
            Vector3 vector3_1 = createPbcCharacterPbc1.head.position - createPbcCharacterPbc1.rightFoot.position;
            double num1 = (double) vector3_1.magnitude / 1.5299999713897705;
            createPbcCharacterPbc2.lengthScale = (float) num1;
            createPbcCharacterPbc1.volumeScale = Mathf.Pow(createPbcCharacterPbc1.lengthScale, 3f);
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.main)
              createPbcCharacterPbc1.main.realCharacterScale = createPbcCharacterPbc1.lengthScale;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.transform.parent)
            {
              GameObject gameObject = createPbcCharacterPbc1.transform.parent.gameObject;
              createPbcCharacterPbc1.transform.parent = (Transform) null;
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject);
            }
            GameObject gameObject1 = new GameObject("Complete_" + createPbcCharacterPbc1.name);
            gameObject1.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            createPbcCharacterPbc1.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            createPbcCharacterPbc1.transform.parent = gameObject1.transform;
            CapsuleCollider capsuleCollider = createPbcCharacterPbc1.gameObject.AddComponent<CapsuleCollider>();
            createPbcCharacterPbc1.animFollow.oneTrigger = capsuleCollider;
            capsuleCollider.radius = 0.5f * createPbcCharacterPbc1.lengthScale;
            capsuleCollider.height = 1.6f * createPbcCharacterPbc1.lengthScale;
            capsuleCollider.center = 1.1f * createPbcCharacterPbc1.lengthScale * Vector3.up;
            capsuleCollider.isTrigger = true;
            capsuleCollider.enabled = false;
            capsuleCollider.gameObject.layer = LayerMask.NameToLayer("Water");
            Rigidbody rigidbody = createPbcCharacterPbc1.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            createPbcCharacterPbc1.transform.rotation = Quaternion.identity;
            vector3_1 = Vector3.Cross(createPbcCharacterPbc1.rightBiceps.position - createPbcCharacterPbc1.leftBiceps.position, createPbcCharacterPbc1.head.position - createPbcCharacterPbc1.hips.position);
            Vector3 vector3_2 = vector3_1.normalized;
            vector3_2 = new Vector3(Mathf.Round(vector3_2.x), Mathf.Round(vector3_2.y), Mathf.Round(vector3_2.z));
            vector3_1 = Vector3.Cross(vector3_2, createPbcCharacterPbc1.rightBiceps.position - createPbcCharacterPbc1.leftBiceps.position);
            Vector3 vector3_3 = vector3_1.normalized;
            vector3_3 = new Vector3(Mathf.Round(vector3_3.x), Mathf.Round(vector3_3.y), Mathf.Round(vector3_3.z));
            vector3_1 = Vector3.Cross(vector3_2, vector3_3);
            Vector3 dir = -vector3_1.normalized;
            dir = new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), Mathf.Round(dir.z));
            Debug.DrawRay(createPbcCharacterPbc1.head.position, vector3_2, Color.blue, 5f);
            Debug.DrawRay(createPbcCharacterPbc1.head.position, vector3_3, Color.yellow, 5f);
            Debug.DrawRay(createPbcCharacterPbc1.head.position, dir, Color.red, 5f);
            GameObject gameObject2 = new GameObject("ragdoll_" + createPbcCharacterPbc1.name);
            gameObject2.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            gameObject2.transform.parent = gameObject1.transform;
            int index1 = 0;
            GameObject gameObject3 = new GameObject(createPbcCharacterPbc1.hips.name, new System.Type[2]
            {
              typeof (SphereCollider),
              typeof (Rigidbody)
            });
            gameObject3.transform.rotation = createPbcCharacterPbc1.hips.rotation;
            gameObject3.transform.position = createPbcCharacterPbc1.hips.position;
            gameObject3.transform.parent = gameObject2.transform;
            gameObject3.GetComponent<SphereCollider>().center = createPbcCharacterPbc1.hips.InverseTransformPoint((createPbcCharacterPbc1.spine1.position + createPbcCharacterPbc1.hips.position) * 0.5f) * createPbcCharacterPbc1.transform.localScale.y;
            gameObject3.GetComponent<SphereCollider>().radius = 0.13f * createPbcCharacterPbc1.lengthScale;
            gameObject3.GetComponent<Rigidbody>().mass = 15f * createPbcCharacterPbc1.volumeScale;
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index1] = createPbcCharacterPbc1.hips;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index1] = gameObject3.transform;
            gameObject3.AddComponent<Limb_PBC>();
            int index2 = index1 + 1;
            Vector3 secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftThigh.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            Vector3 axis = Quaternion.Inverse(createPbcCharacterPbc1.leftThigh.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb1 = new GameObject(createPbcCharacterPbc1.leftThigh.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftThigh, ragdollLimb1, gameObject3.transform, createPbcCharacterPbc1.leftCalf, new Vector4(-20f, 70f, 20f, 20f), 0.08f, 9f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index2] = createPbcCharacterPbc1.leftThigh;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index2] = ragdollLimb1.transform;
            ragdollLimb1.AddComponent<Limb_PBC>();
            int index3 = index2 + 1;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftCalf.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.leftCalf.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb2 = new GameObject(createPbcCharacterPbc1.leftCalf.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            GameObject gameObject4 = new GameObject("tempGo");
            gameObject4.transform.position = createPbcCharacterPbc1.leftFoot.position + (createPbcCharacterPbc1.leftFoot.position - createPbcCharacterPbc1.leftCalf.position) * 0.2f;
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftCalf, ragdollLimb2, ragdollLimb1.transform, createPbcCharacterPbc1.leftFoot, new Vector4(-120f, 0.0f, 5f, 10f), 0.06f, 4f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index3] = createPbcCharacterPbc1.leftCalf;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index3] = ragdollLimb2.transform;
            ragdollLimb2.AddComponent<Limb_PBC>();
            int index4 = index3 + 1;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftFoot.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.leftFoot.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb3 = new GameObject(createPbcCharacterPbc1.leftFoot.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = !createPbcCharacterPbc1.shortFeet ? createPbcCharacterPbc1.leftToe2.position : (createPbcCharacterPbc1.leftFoot.position + createPbcCharacterPbc1.leftToe2.position) * 0.5f;
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftFoot, ragdollLimb3, ragdollLimb2.transform, gameObject4.transform, new Vector4(-30f, 30f, 30f, 20f), 0.05f, 2f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index4] = createPbcCharacterPbc1.leftFoot;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index4] = ragdollLimb3.transform;
            createPbcCharacterPbc1.seeAnimatedMaster.leftFoot = createPbcCharacterPbc1.leftFoot;
            ragdollLimb3.AddComponent<LimbFoot_PBC>();
            int index5 = index4 + 1;
            GameObject gameObject5 = createPbcCharacterPbc1.gameObject;
            bool flag1 = true;
            for (int index6 = 0; index6 < createPbcCharacterPbc1.leftFoot.childCount; ++index6)
            {
              if (createPbcCharacterPbc1.leftFoot.GetChild(index6).name.ToLower().Contains("heel"))
              {
                flag1 = false;
                gameObject5 = createPbcCharacterPbc1.leftFoot.GetChild(index6).gameObject;
                break;
              }
            }
            if (flag1)
            {
              gameObject5 = new GameObject("LeftHeel");
              gameObject5.transform.parent = createPbcCharacterPbc1.leftFoot;
              gameObject5.transform.position = createPbcCharacterPbc1.leftFoot.position + Vector3.Dot(createPbcCharacterPbc1.leftToe2.position - createPbcCharacterPbc1.leftFoot.position, createPbcCharacterPbc1.transform.up) * createPbcCharacterPbc1.transform.up;
            }
            createPbcCharacterPbc1.seeAnimatedMaster.leftHeel = gameObject5.transform;
            createPbcCharacterPbc1.seeAnimatedMaster.leftToe2 = createPbcCharacterPbc1.leftToe2;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightThigh.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.rightThigh.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb4 = new GameObject(createPbcCharacterPbc1.rightThigh.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightThigh, ragdollLimb4, gameObject3.transform, createPbcCharacterPbc1.rightCalf, new Vector4(-20f, 70f, 20f, 20f), 0.08f, 9f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index5] = createPbcCharacterPbc1.rightThigh;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index5] = ragdollLimb4.transform;
            ragdollLimb4.AddComponent<Limb_PBC>();
            int index7 = index5 + 1;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightCalf.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.rightCalf.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb5 = new GameObject(createPbcCharacterPbc1.rightCalf.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = createPbcCharacterPbc1.rightFoot.position + (createPbcCharacterPbc1.rightFoot.position - createPbcCharacterPbc1.rightCalf.position) * 0.2f;
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightCalf, ragdollLimb5, ragdollLimb4.transform, createPbcCharacterPbc1.rightFoot, new Vector4(-120f, 0.0f, 5f, 10f), 0.06f, 4f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index7] = createPbcCharacterPbc1.rightCalf;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index7] = ragdollLimb5.transform;
            ragdollLimb5.AddComponent<Limb_PBC>();
            int index8 = index7 + 1;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightFoot.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.rightFoot.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb6 = new GameObject(createPbcCharacterPbc1.rightFoot.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = !createPbcCharacterPbc1.shortFeet ? createPbcCharacterPbc1.rightToe2.position : (createPbcCharacterPbc1.rightFoot.position + createPbcCharacterPbc1.rightToe2.position) * 0.5f;
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightFoot, ragdollLimb6, ragdollLimb5.transform, gameObject4.transform, new Vector4(-30f, 30f, 30f, 20f), 0.05f, 2f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index8] = createPbcCharacterPbc1.rightFoot;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index8] = ragdollLimb6.transform;
            createPbcCharacterPbc1.seeAnimatedMaster.rightFoot = createPbcCharacterPbc1.rightFoot;
            ragdollLimb6.AddComponent<LimbFoot_PBC>();
            int index9 = index8 + 1;
            GameObject gameObject6 = createPbcCharacterPbc1.gameObject;
            bool flag2 = true;
            for (int index10 = 0; index10 < createPbcCharacterPbc1.rightFoot.childCount; ++index10)
            {
              if (createPbcCharacterPbc1.rightFoot.GetChild(index10).name.ToLower().Contains("heel"))
              {
                flag2 = false;
                gameObject6 = createPbcCharacterPbc1.rightFoot.GetChild(index10).gameObject;
                break;
              }
            }
            if (flag2)
            {
              gameObject6 = new GameObject("RightHeel");
              gameObject6.transform.parent = createPbcCharacterPbc1.rightFoot;
              gameObject6.transform.position = createPbcCharacterPbc1.rightFoot.position + Vector3.Dot(createPbcCharacterPbc1.leftToe2.position - createPbcCharacterPbc1.leftFoot.position, createPbcCharacterPbc1.transform.up) * createPbcCharacterPbc1.transform.up;
            }
            createPbcCharacterPbc1.seeAnimatedMaster.rightHeel = gameObject6.transform;
            createPbcCharacterPbc1.seeAnimatedMaster.rightToe2 = createPbcCharacterPbc1.rightToe2;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.spine1.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.spine1.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb7 = new GameObject(createPbcCharacterPbc1.spine1.name, new System.Type[3]
            {
              typeof (SphereCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.spine1, ragdollLimb7, gameObject3.transform, createPbcCharacterPbc1.spine2, new Vector4(-20f, 20f, 20f, 20f), -0.13f, 8f, axis, secondaryAxis);
            else
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.spine1, ragdollLimb7, gameObject3.transform, createPbcCharacterPbc1.spine1, new Vector4(-20f, 20f, 20f, 20f), -0.13f, 8f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index9] = createPbcCharacterPbc1.spine1;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index9] = ragdollLimb7.transform;
            ragdollLimb7.AddComponent<Limb_PBC>();
            int index11 = index9 + 1;
            GameObject ragdollLimb8 = createPbcCharacterPbc1.gameObject;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
            {
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.spine2.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.spine2.rotation) * dir * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              ragdollLimb8 = new GameObject(createPbcCharacterPbc1.spine2.name, new System.Type[3]
              {
                typeof (SphereCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.spine2, ragdollLimb8, ragdollLimb7.transform, createPbcCharacterPbc1.head.parent, new Vector4(-20f, 20f, 20f, 20f), -0.13f, 12f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index11] = createPbcCharacterPbc1.spine2;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index11] = ragdollLimb8.transform;
              ragdollLimb8.AddComponent<Limb_PBC>();
              ++index11;
            }
            GameObject ragdollLimb9 = (GameObject) null;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.neck)
            {
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.neck.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.neck.rotation) * dir * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              ragdollLimb9 = new GameObject(createPbcCharacterPbc1.neck.name, new System.Type[3]
              {
                typeof (SphereCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              gameObject4.transform.position = createPbcCharacterPbc1.head.position;
              if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.neck, ragdollLimb9, ragdollLimb8.transform, gameObject4.transform, new Vector4(-10f, 10f, 10f, 10f), -0.08f, 3f, axis, secondaryAxis);
              else
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.neck, ragdollLimb9, ragdollLimb7.transform, gameObject4.transform, new Vector4(-10f, 10f, 10f, 10f), -0.08f, 3f, axis, secondaryAxis);
              if (createPbcCharacterPbc1.ragdollShoulders && (bool) (UnityEngine.Object) createPbcCharacterPbc1.leftClavicle)
                ragdollLimb9.GetComponent<SphereCollider>().center += 0.06f * (Quaternion.Inverse(ragdollLimb9.transform.rotation) * createPbcCharacterPbc1.transform.rotation * Vector3.up) * createPbcCharacterPbc1.lengthScale;
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index11] = createPbcCharacterPbc1.neck;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index11] = ragdollLimb9.transform;
              ragdollLimb9.AddComponent<Limb_PBC>();
              ++index11;
            }
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.head.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.head.rotation) * dir * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb10 = new GameObject(createPbcCharacterPbc1.head.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = !(bool) (UnityEngine.Object) createPbcCharacterPbc1.neck ? createPbcCharacterPbc1.head.position + Vector3.up * 0.18f * createPbcCharacterPbc1.lengthScale : createPbcCharacterPbc1.head.position + Vector3.up * 0.21f * createPbcCharacterPbc1.lengthScale;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.neck)
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.head, ragdollLimb10, ragdollLimb9.transform, gameObject4.transform, new Vector4(-40f, 40f, 40f, 40f), 0.1f, 3f, axis, secondaryAxis);
            else if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.head, ragdollLimb10, ragdollLimb8.transform, gameObject4.transform, new Vector4(-40f, 40f, 40f, 40f), 0.1f, 3f, axis, secondaryAxis);
            else
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.head, ragdollLimb10, ragdollLimb7.transform, gameObject4.transform, new Vector4(-40f, 40f, 40f, 40f), 0.1f, 3f, axis, secondaryAxis);
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.neck)
              ragdollLimb10.GetComponent<CapsuleCollider>().height = 0.24f * createPbcCharacterPbc1.lengthScale;
            else
              ragdollLimb10.GetComponent<CapsuleCollider>().height = 0.3f * createPbcCharacterPbc1.lengthScale;
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index11] = createPbcCharacterPbc1.head;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index11] = ragdollLimb10.transform;
            ragdollLimb10.AddComponent<LimbHead_PBC>();
            int index12 = index11 + 1;
            GameObject ragdollLimb11;
            int index13;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.leftClavicle)
            {
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftClavicle.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.leftClavicle.rotation) * vector3_3 * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              GameObject ragdollLimb12 = new GameObject(createPbcCharacterPbc1.leftClavicle.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftClavicle, ragdollLimb12, ragdollLimb8.transform, createPbcCharacterPbc1.leftBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis, secondaryAxis);
              else
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftClavicle, ragdollLimb12, ragdollLimb7.transform, createPbcCharacterPbc1.leftBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index12] = createPbcCharacterPbc1.leftClavicle;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index12] = ragdollLimb12.transform;
              ragdollLimb12.AddComponent<Limb_PBC>();
              int index14 = index12 + 1;
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.leftBiceps.rotation) * vector3_3 * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              ragdollLimb11 = new GameObject(createPbcCharacterPbc1.leftBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftBiceps, ragdollLimb11, ragdollLimb12.transform, createPbcCharacterPbc1.leftForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index14] = createPbcCharacterPbc1.leftBiceps;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index14] = ragdollLimb11.transform;
              ragdollLimb11.AddComponent<Limb_PBC>();
              index13 = index14 + 1;
            }
            else
            {
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.leftBiceps.rotation) * vector3_3 * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              ragdollLimb11 = new GameObject(createPbcCharacterPbc1.leftBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftBiceps, ragdollLimb11, ragdollLimb8.transform, createPbcCharacterPbc1.leftForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis, secondaryAxis);
              else
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftBiceps, ragdollLimb11, ragdollLimb7.transform, createPbcCharacterPbc1.leftForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index12] = createPbcCharacterPbc1.leftBiceps;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index12] = ragdollLimb11.transform;
              ragdollLimb11.AddComponent<Limb_PBC>();
              index13 = index12 + 1;
            }
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftForearm.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.leftForearm.rotation) * vector3_3 * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb13 = new GameObject(createPbcCharacterPbc1.leftForearm.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = createPbcCharacterPbc1.leftHand.position + (createPbcCharacterPbc1.leftHand.position - createPbcCharacterPbc1.leftForearm.position) * 0.6f;
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftForearm, ragdollLimb13, ragdollLimb11.transform, gameObject4.transform, new Vector4(-90f, 0.0f, 5f, 5f), 0.04f, 2f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index13] = createPbcCharacterPbc1.leftForearm;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index13] = ragdollLimb13.transform;
            ragdollLimb13.AddComponent<Limb_PBC>();
            int index15 = index13 + 1;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.leftHand.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.leftHand.rotation) * vector3_3 * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb14 = new GameObject(createPbcCharacterPbc1.leftHand.name, new System.Type[3]
            {
              typeof (SphereCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.leftHand, ragdollLimb14, ragdollLimb13.transform, createPbcCharacterPbc1.leftHand, new Vector4(-20f, 20f, 50f, 50f), -0.04f, 1f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index15] = createPbcCharacterPbc1.leftHand;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index15] = ragdollLimb14.transform;
            ragdollLimb14.AddComponent<Limb_PBC>();
            int index16 = index15 + 1;
            GameObject ragdollLimb15;
            int index17;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.rightClavicle)
            {
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightClavicle.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.rightClavicle.rotation) * vector3_3 * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              GameObject ragdollLimb16 = new GameObject(createPbcCharacterPbc1.rightClavicle.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightClavicle, ragdollLimb16, ragdollLimb8.transform, createPbcCharacterPbc1.rightBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis, secondaryAxis);
              else
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightClavicle, ragdollLimb16, ragdollLimb7.transform, createPbcCharacterPbc1.rightBiceps, new Vector4(-10f, 10f, 10f, 10f), 0.06f, 5f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index16] = createPbcCharacterPbc1.rightClavicle;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index16] = ragdollLimb16.transform;
              ragdollLimb16.AddComponent<Limb_PBC>();
              int index18 = index16 + 1;
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.rightBiceps.rotation) * vector3_3 * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              ragdollLimb15 = new GameObject(createPbcCharacterPbc1.rightBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightBiceps, ragdollLimb15, ragdollLimb16.transform, createPbcCharacterPbc1.rightForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index18] = createPbcCharacterPbc1.rightBiceps;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index18] = ragdollLimb15.transform;
              ragdollLimb15.AddComponent<Limb_PBC>();
              index17 = index18 + 1;
            }
            else
            {
              secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightBiceps.rotation) * vector3_2 * 0.707f;
              secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
              axis = Quaternion.Inverse(createPbcCharacterPbc1.rightBiceps.rotation) * vector3_3 * 0.707f;
              axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
              ragdollLimb15 = new GameObject(createPbcCharacterPbc1.rightBiceps.name, new System.Type[3]
              {
                typeof (CapsuleCollider),
                typeof (Rigidbody),
                typeof (ConfigurableJoint)
              });
              if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.spine2)
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightBiceps, ragdollLimb15, ragdollLimb8.transform, createPbcCharacterPbc1.rightForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis, secondaryAxis);
              else
                createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightBiceps, ragdollLimb15, ragdollLimb7.transform, createPbcCharacterPbc1.rightForearm, new Vector4(-80f, 20f, 50f, 20f), 0.06f, 3f, axis, secondaryAxis);
              createPbcCharacterPbc1.animFollow.masterRigidTransforms[index16] = createPbcCharacterPbc1.rightBiceps;
              createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index16] = ragdollLimb15.transform;
              ragdollLimb15.AddComponent<Limb_PBC>();
              index17 = index16 + 1;
            }
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightForearm.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.rightForearm.rotation) * vector3_3 * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb17 = new GameObject(createPbcCharacterPbc1.rightForearm.name, new System.Type[3]
            {
              typeof (CapsuleCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            gameObject4.transform.position = createPbcCharacterPbc1.rightHand.position + (createPbcCharacterPbc1.rightHand.position - createPbcCharacterPbc1.rightForearm.position) * 0.6f;
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightForearm, ragdollLimb17, ragdollLimb15.transform, gameObject4.transform, new Vector4(0.0f, 90f, 5f, 5f), 0.04f, 2f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index17] = createPbcCharacterPbc1.rightForearm;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index17] = ragdollLimb17.transform;
            ragdollLimb17.AddComponent<Limb_PBC>();
            int index19 = index17 + 1;
            secondaryAxis = Quaternion.Inverse(createPbcCharacterPbc1.rightHand.rotation) * vector3_2 * 0.707f;
            secondaryAxis = new Vector3(Mathf.Round(secondaryAxis.x), Mathf.Round(secondaryAxis.y), Mathf.Round(secondaryAxis.z));
            axis = Quaternion.Inverse(createPbcCharacterPbc1.rightHand.rotation) * vector3_3 * 0.707f;
            axis = new Vector3(Mathf.Round(axis.x), Mathf.Round(axis.y), Mathf.Round(axis.z));
            GameObject ragdollLimb18 = new GameObject(createPbcCharacterPbc1.rightHand.name, new System.Type[3]
            {
              typeof (SphereCollider),
              typeof (Rigidbody),
              typeof (ConfigurableJoint)
            });
            createPbcCharacterPbc1.SetupCapsuleLimb(createPbcCharacterPbc1.rightHand, ragdollLimb18, ragdollLimb17.transform, createPbcCharacterPbc1.rightHand, new Vector4(-20f, 20f, 50f, 50f), -0.04f, 1f, axis, secondaryAxis);
            createPbcCharacterPbc1.animFollow.masterRigidTransforms[index19] = createPbcCharacterPbc1.rightHand;
            createPbcCharacterPbc1.animFollow.ragdollRigidTransforms[index19] = ragdollLimb18.transform;
            ragdollLimb18.AddComponent<Limb_PBC>();
            int num2 = index19 + 1;
            if (!(bool) (UnityEngine.Object) (createPbcCharacterPbc1.gameObject.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController) Resources.Load("Animator_PBM", typeof (RuntimeAnimatorController))))
              Debug.LogWarning((object) "Could not load runtimeAnimatorController \"Animator_PBM\".\nIt must be in a folder named \"Resources\".\n");
            if (!(bool) (UnityEngine.Object) createPbcCharacterPbc1.bloodPrefab && !(bool) (UnityEngine.Object) (createPbcCharacterPbc1.bloodPrefab = (ParticleSystem) Resources.Load("DefaultBlood", typeof (ParticleSystem))))
              Debug.LogWarning((object) "Could not load prefab \"DefaultBlood\".It must be in a folder named \"Resources\".\n");
            createPbcCharacterPbc1.gameObject.AddComponent<HashIDs_PBC>();
            gameObject2.gameObject.AddComponent<RagdollControl_PBC>();
            RagdollControl_PBC component = gameObject2.GetComponent<RagdollControl_PBC>();
            component.master = createPbcCharacterPbc1.transform;
            if ((bool) (UnityEngine.Object) createPbcCharacterPbc1.bloodPrefab)
            {
              for (int index20 = 0; index20 < 4; ++index20)
              {
                component.blood[index20] = UnityEngine.Object.Instantiate<ParticleSystem>(createPbcCharacterPbc1.bloodPrefab, gameObject2.transform.position, Quaternion.identity);
                component.blood[index20].transform.parent = gameObject2.transform;
              }
            }
            foreach (Component ragdollRigidTransform in createPbcCharacterPbc1.animFollow.ragdollRigidTransforms)
              ragdollRigidTransform.gameObject.layer = LayerMask.NameToLayer("Water");
            gameObject2.layer = LayerMask.NameToLayer("Water");
            if (createPbcCharacterPbc1.addFootIK)
            {
              createPbcCharacterPbc1.footIK = createPbcCharacterPbc1.gameObject.AddComponent<AdvancedFootIK_PBC>();
              AudioSource audioSource;
              if (!(bool) (UnityEngine.Object) (audioSource = createPbcCharacterPbc1.GetComponent<AudioSource>()))
              {
                audioSource = createPbcCharacterPbc1.gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.spatialBlend = 1f;
              }
              if (!(bool) (UnityEngine.Object) audioSource.clip && !(bool) (UnityEngine.Object) (audioSource.clip = (AudioClip) Resources.Load("DefaultFootstepSound", typeof (AudioClip))))
                Debug.LogWarning((object) "Could not load AudioClip named \"DefaultFootstepSound\".\nIt must be in a folder named \"Resources\".\n");
              createPbcCharacterPbc1.footIK.leftThigh = createPbcCharacterPbc1.leftThigh;
              createPbcCharacterPbc1.footIK.leftCalf = createPbcCharacterPbc1.leftCalf;
              createPbcCharacterPbc1.footIK.leftFoot = createPbcCharacterPbc1.leftFoot;
              createPbcCharacterPbc1.footIK.leftToe2 = createPbcCharacterPbc1.leftToe2;
              createPbcCharacterPbc1.footIK.leftHeel = gameObject5.transform;
              createPbcCharacterPbc1.footIK.rightThigh = createPbcCharacterPbc1.rightThigh;
              createPbcCharacterPbc1.footIK.rightCalf = createPbcCharacterPbc1.rightCalf;
              createPbcCharacterPbc1.footIK.rightFoot = createPbcCharacterPbc1.rightFoot;
              createPbcCharacterPbc1.footIK.rightToe2 = createPbcCharacterPbc1.rightToe2;
              createPbcCharacterPbc1.footIK.rightHeel = gameObject6.transform;
              createPbcCharacterPbc1.footIK.ragdollLeftFoot = ragdollLimb3.transform;
              createPbcCharacterPbc1.footIK.ragdollRightFoot = ragdollLimb6.transform;
            }
            Transform[] transformArray = new Transform[createPbcCharacterPbc1.numberOfTransforms];
            int index21 = 0;
            foreach (Transform ragdollRigidTransform in createPbcCharacterPbc1.animFollow.ragdollRigidTransforms)
            {
              if ((bool) (UnityEngine.Object) ragdollRigidTransform.GetComponent<ConfigurableJoint>())
              {
                createPbcCharacterPbc1.animFollow.configurableJoints[index21] = ragdollRigidTransform.GetComponent<ConfigurableJoint>();
                transformArray[index21] = ragdollRigidTransform.GetComponent<ConfigurableJoint>().connectedBody.transform;
              }
              createPbcCharacterPbc1.animFollow.ragdollRigidbodies[index21] = ragdollRigidTransform.GetComponent<Rigidbody>();
              ++index21;
            }
            Transform[] componentsInChildren = createPbcCharacterPbc1.GetComponentsInChildren<Transform>();
            int index22 = 0;
            foreach (Transform transform1 in transformArray)
            {
              if ((bool) (UnityEngine.Object) transform1)
              {
                foreach (Transform transform2 in componentsInChildren)
                {
                  if (transform2.name == transform1.name)
                  {
                    createPbcCharacterPbc1.animFollow.masterConnectedTransforms[index22] = transform2;
                    ++index22;
                    break;
                  }
                }
              }
              else
                ++index22;
            }
            int num3 = createPbcCharacterPbc1.animFollow.jointStrengthProfile.Length;
            int numberOfTransforms = createPbcCharacterPbc1.numberOfTransforms;
            Array.Resize<float>(ref createPbcCharacterPbc1.animFollow.jointStrengthProfile, numberOfTransforms);
            if (num3 == 0)
            {
              createPbcCharacterPbc1.animFollow.jointStrengthProfile[0] = 1f;
              num3 = 1;
            }
            for (int index23 = 1; index23 <= numberOfTransforms - num3; ++index23)
              createPbcCharacterPbc1.animFollow.jointStrengthProfile[numberOfTransforms - index23] = createPbcCharacterPbc1.animFollow.jointStrengthProfile[num3 - 1];
            int num4 = createPbcCharacterPbc1.animFollow.jointDampingProfile.Length;
            Array.Resize<float>(ref createPbcCharacterPbc1.animFollow.jointDampingProfile, numberOfTransforms);
            if (num4 == 0)
            {
              createPbcCharacterPbc1.animFollow.jointDampingProfile[0] = 1f;
              num4 = 1;
            }
            for (int index24 = 1; index24 <= numberOfTransforms - num4; ++index24)
              createPbcCharacterPbc1.animFollow.jointDampingProfile[numberOfTransforms - index24] = createPbcCharacterPbc1.animFollow.jointDampingProfile[num4 - 1];
            int num5 = createPbcCharacterPbc1.animFollow.forceStrengthProfile.Length;
            Array.Resize<float>(ref createPbcCharacterPbc1.animFollow.forceStrengthProfile, numberOfTransforms);
            if (num5 == 0)
            {
              createPbcCharacterPbc1.animFollow.forceStrengthProfile[0] = 1f;
              num5 = 1;
            }
            for (int index25 = 1; index25 <= numberOfTransforms - num5; ++index25)
              createPbcCharacterPbc1.animFollow.forceStrengthProfile[numberOfTransforms - index25] = createPbcCharacterPbc1.animFollow.forceStrengthProfile[num5 - 1];
            gameObject1.transform.position = createPbcCharacterPbc1.thisPosition;
            gameObject1.transform.rotation = createPbcCharacterPbc1.thisRotation;
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject4);
            Debug.Log((object) "Ragdoll created. Adjust heel and toe2 positions.\nCheck that collider sizes are good.\n");
            if (false)
              createPbcCharacterPbc1.createNow = false;
            else
              UnityEngine.Object.DestroyImmediate((UnityEngine.Object) createPbcCharacterPbc1);
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
