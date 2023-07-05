// Decompiled with JetBrains decompiler
// Type: PBC.ReplaceJoints_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  [ExecuteInEditMode]
  public class ReplaceJoints_PBC : MonoBehaviour
  {
    private void Start()
    {
      CharacterJoint[] componentsInChildren = this.GetComponentsInChildren<CharacterJoint>();
      int num = 0;
      foreach (CharacterJoint characterJoint in componentsInChildren)
      {
        if (!(bool) (Object) characterJoint.transform.GetComponent<ConfigurableJoint>())
        {
          ++num;
          ConfigurableJoint configurableJoint = characterJoint.gameObject.AddComponent<ConfigurableJoint>();
          configurableJoint.connectedBody = characterJoint.connectedBody;
          configurableJoint.anchor = characterJoint.anchor;
          configurableJoint.axis = characterJoint.axis;
          configurableJoint.secondaryAxis = characterJoint.swingAxis;
          configurableJoint.xMotion = ConfigurableJointMotion.Locked;
          configurableJoint.yMotion = ConfigurableJointMotion.Locked;
          configurableJoint.zMotion = ConfigurableJointMotion.Locked;
          configurableJoint.angularXMotion = ConfigurableJointMotion.Limited;
          configurableJoint.angularYMotion = ConfigurableJointMotion.Limited;
          configurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
          configurableJoint.lowAngularXLimit = characterJoint.lowTwistLimit;
          configurableJoint.highAngularXLimit = characterJoint.highTwistLimit;
          configurableJoint.angularYLimit = characterJoint.swing1Limit;
          configurableJoint.angularZLimit = characterJoint.swing2Limit;
          configurableJoint.rotationDriveMode = RotationDriveMode.Slerp;
        }
        Object.DestroyImmediate((Object) characterJoint);
      }
      Debug.Log((object) ("Replaced " + num.ToString() + " CharacterJoints with ConfigurableJoints on " + this.name));
      Object.DestroyImmediate((Object) this);
    }
  }
}
