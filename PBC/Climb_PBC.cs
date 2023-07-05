﻿// Decompiled with JetBrains decompiler
// Type: PBC.Climb_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class Climb_PBC : StateMachineBehaviour
  {
    private MoveBaseClass_PBC moveScript;
    [SerializeField]
    private bool useVerticalRootMotion = true;
    [SerializeField]
    private bool alwaysGrounded = true;
    [SerializeField]
    private bool entry;
    [SerializeField]
    private bool exit;

    public override void OnStateEnter(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!(bool) (Object) this.moveScript)
      {
        if (!(bool) (Object) (this.moveScript = animator.transform.GetComponent<MoveBaseClass_PBC>()) || !this.entry)
          return;
        this.moveScript.useVerticalRootMotion = this.useVerticalRootMotion;
        this.moveScript.alwaysGrounded = this.alwaysGrounded;
      }
      else
      {
        if (!this.entry)
          return;
        this.moveScript.useVerticalRootMotion = this.useVerticalRootMotion;
        this.moveScript.alwaysGrounded = this.alwaysGrounded;
      }
    }

    public override void OnStateExit(
      Animator animator,
      AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      if (!this.exit || !(bool) (Object) this.moveScript)
        return;
      this.moveScript.useVerticalRootMotion = this.useVerticalRootMotion;
      this.moveScript.alwaysGrounded = this.alwaysGrounded;
    }
  }
}
