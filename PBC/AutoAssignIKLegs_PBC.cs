// Decompiled with JetBrains decompiler
// Type: PBC.AutoAssignIKLegs_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  [ExecuteInEditMode]
  internal class AutoAssignIKLegs_PBC : MonoBehaviour
  {
    private AdvancedFootIK_PBC advancedFootIK;

    private void Awake()
    {
      if (!(bool) (Object) (this.advancedFootIK = this.GetComponent<AdvancedFootIK_PBC>()))
      {
        Debug.LogWarning((object) ("No script AdvancedFootIK on " + this.name + "\n"));
        Object.DestroyImmediate((Object) this);
      }
      else
      {
        Animator component;
        if (!(bool) (Object) (component = this.GetComponent<Animator>()))
        {
          Debug.LogWarning((object) ("No Animator on " + this.name + "\n"));
          Object.DestroyImmediate((Object) this);
        }
        else
        {
          Avatar avatar;
          if (!(bool) (Object) (avatar = component.avatar))
          {
            Debug.LogWarning((object) ("No avatar on animator of " + this.name + "\n"));
            Object.DestroyImmediate((Object) this);
          }
          else if (!avatar.isHuman)
          {
            Debug.LogWarning((object) ("Avatar on " + this.name + " is not human.\nScript AutoAssignIKLegs requires the avatar to be human"));
            Object.DestroyImmediate((Object) this);
          }
          else
          {
            if ((bool) (Object) this.advancedFootIK)
            {
              if (!(bool) (Object) (this.advancedFootIK.leftThigh = component.GetBoneTransform(HumanBodyBones.LeftUpperLeg)) || !(bool) (Object) (this.advancedFootIK.leftCalf = component.GetBoneTransform(HumanBodyBones.LeftLowerLeg)) || !(bool) (Object) (this.advancedFootIK.leftFoot = component.GetBoneTransform(HumanBodyBones.LeftFoot)))
              {
                Debug.LogWarning((object) ("Avatar on " + this.name + " is no good\n"));
                return;
              }
              if (!(bool) (Object) (this.advancedFootIK.leftToe2 = component.GetBoneTransform(HumanBodyBones.LeftToes)))
              {
                if (this.advancedFootIK.leftFoot.childCount <= 0 || !(bool) (Object) (this.advancedFootIK.leftToe2 = this.advancedFootIK.leftFoot.GetChild(0)))
                  Debug.LogWarning((object) "Cannot find any toe transform on left foot.\nThe current version of the foot IK requires a toe transform\n");
              }
              else
              {
                this.advancedFootIK.leftHeel = this.advancedFootIK.leftToe2;
                if (component.GetBoneTransform(HumanBodyBones.LeftToes).childCount > 0)
                  this.advancedFootIK.leftToe2 = component.GetBoneTransform(HumanBodyBones.LeftToes).GetChild(0);
              }
              if ((bool) (Object) this.advancedFootIK.leftToe2)
              {
                int childCount = this.advancedFootIK.leftFoot.childCount;
                for (int index = 0; index < childCount; ++index)
                {
                  if (this.advancedFootIK.leftFoot.GetChild(index).name.ToLower().Contains("heel"))
                    this.advancedFootIK.leftHeel = this.advancedFootIK.leftFoot.GetChild(index);
                }
              }
              this.advancedFootIK.rightThigh = component.GetBoneTransform(HumanBodyBones.RightUpperLeg);
              this.advancedFootIK.rightCalf = component.GetBoneTransform(HumanBodyBones.RightLowerLeg);
              this.advancedFootIK.rightFoot = component.GetBoneTransform(HumanBodyBones.RightFoot);
              if (!(bool) (Object) (this.advancedFootIK.rightToe2 = component.GetBoneTransform(HumanBodyBones.RightToes)))
              {
                if (this.advancedFootIK.rightFoot.childCount <= 0 || !(bool) (Object) (this.advancedFootIK.rightToe2 = this.advancedFootIK.rightFoot.GetChild(0)))
                  Debug.LogWarning((object) "Cannot find any toe transform on right foot.\nThe current version of the foot IK requires a toe transform\n");
              }
              else
              {
                this.advancedFootIK.rightHeel = this.advancedFootIK.rightToe2;
                if (component.GetBoneTransform(HumanBodyBones.RightToes).childCount > 0)
                  this.advancedFootIK.rightToe2 = component.GetBoneTransform(HumanBodyBones.RightToes).GetChild(0);
              }
              if ((bool) (Object) this.advancedFootIK.rightToe2)
              {
                int childCount = this.advancedFootIK.rightFoot.childCount;
                for (int index = 0; index < childCount; ++index)
                {
                  if (this.advancedFootIK.rightFoot.GetChild(index).name.ToLower().Contains("heel"))
                    this.advancedFootIK.rightHeel = this.advancedFootIK.rightFoot.GetChild(index);
                }
              }
            }
            Rigidbody[] componentsInChildren = this.transform.root.GetComponentsInChildren<Rigidbody>();
            for (int index = 0; index < componentsInChildren.Length; ++index)
            {
              if (!(bool) (Object) this.advancedFootIK.ragdollLeftFoot && componentsInChildren[index].name == this.advancedFootIK.leftFoot.name)
              {
                this.advancedFootIK.ragdollLeftFoot = componentsInChildren[index].transform;
                if ((bool) (Object) this.advancedFootIK.ragdollRightFoot)
                  break;
              }
              else if (!(bool) (Object) this.advancedFootIK.ragdollRightFoot && componentsInChildren[index].name == this.advancedFootIK.rightFoot.name)
              {
                this.advancedFootIK.ragdollRightFoot = componentsInChildren[index].transform;
                if ((bool) (Object) this.advancedFootIK.ragdollLeftFoot)
                  break;
              }
            }
            if ((bool) (Object) this.advancedFootIK)
            {
              if (!(bool) (Object) this.advancedFootIK.leftHeel || !(bool) (Object) this.advancedFootIK.leftToe2 || !(bool) (Object) this.advancedFootIK.rightHeel || !(bool) (Object) this.advancedFootIK.rightToe2)
              {
                Debug.LogWarning((object) "Auto assigning of legs failed.\nAssign legs manually\n");
                Object.DestroyImmediate((Object) this);
                return;
              }
              Debug.Log((object) "Legs in AdvancedFootIK script have been auto assigned\n");
            }
            Object.DestroyImmediate((Object) this);
          }
        }
      }
    }
  }
}
