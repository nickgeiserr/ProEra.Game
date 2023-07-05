﻿// Decompiled with JetBrains decompiler
// Type: StateSetAnimationStyle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class StateSetAnimationStyle : BaseStateMachineBehaviour
{
  [SerializeField]
  private AnimationStyleData _animationStyleData;
  private AnimationStyleManager _styleManager;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    this._styleManager = animator.transform.parent.GetComponent<AnimationStyleManager>();
    this._styleManager.SetAnimationStyle(this._animationStyleData);
  }
}
