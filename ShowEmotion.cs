// Decompiled with JetBrains decompiler
// Type: ShowEmotion
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class ShowEmotion : StateMachineBehaviour
{
  [SerializeField]
  private string _facialEmotionTriggerName;
  private Animator _faceAnimator;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if ((Object) this._faceAnimator == (Object) null)
      this._faceAnimator = animator.transform.Find("Player_Mesh_Grp/Heads").GetComponent<Animator>();
    if (!((Object) this._faceAnimator != (Object) null))
      return;
    this._faceAnimator.SetTrigger(this._facialEmotionTriggerName);
  }
}
