// Decompiled with JetBrains decompiler
// Type: PBC.SeeAnimatedMaster_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class SeeAnimatedMaster_PBC : MonoBehaviour
  {
    private AnimFollow_PBC animFollow;
    private bool human;
    public Transform leftFoot;
    public Transform leftToe2;
    public Transform leftHeel;
    public Transform rightFoot;
    public Transform rightToe2;
    public Transform rightHeel;
    public bool drawMaster = true;

    private void Awake()
    {
      this.animFollow = this.GetComponent<AnimFollow_PBC>();
      this.human = (bool) (Object) this.leftFoot && (bool) (Object) this.leftToe2 && (bool) (Object) this.leftHeel && (bool) (Object) this.rightFoot && (bool) (Object) this.rightToe2 && (bool) (Object) this.rightHeel;
    }

    public void DrawMaster(Color color)
    {
      if (!this.drawMaster)
        return;
      for (int index = 1; index < this.animFollow.masterRigidTransforms.Length; ++index)
        Debug.DrawLine(this.animFollow.masterRigidTransforms[index].position, this.animFollow.masterConnectedTransforms[index].position, color);
      if (!this.human)
        return;
      Debug.DrawLine(this.leftFoot.position, this.leftToe2.position, color);
      Debug.DrawLine(this.leftFoot.position, this.leftHeel.position, color);
      Debug.DrawLine(this.rightFoot.position, this.rightToe2.position, color);
      Debug.DrawLine(this.rightFoot.position, this.rightHeel.position, color);
    }
  }
}
