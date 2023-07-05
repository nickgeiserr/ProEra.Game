// Decompiled with JetBrains decompiler
// Type: UDB.AnimationController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class AnimationController : MonoBehaviour
  {
    public bool isAnimating;
    public bool animateOnAwake;

    private void Awake()
    {
      this.SetAnimationState(this.animateOnAwake);
      this.AwakeAction();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void SetAnimationState(bool animate)
    {
      this.isAnimating = animate;
      this.gameObject.SetActive(this.isAnimating);
    }

    public void StartAnimating() => this.SetAnimationState(true);

    public void StopAnimating() => this.SetAnimationState(false);

    protected virtual void AwakeAction()
    {
    }
  }
}
