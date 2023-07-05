// Decompiled with JetBrains decompiler
// Type: FootballVR.ScreenWipeTransition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  public class ScreenWipeTransition : MonoBehaviour
  {
    public static bool bFadingDone;
    public static bool bAnimationFinished;
    private static Action RequestFading;
    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
      this._animator.enabled = false;
      this.gameObject.SetActive(false);
      ScreenWipeTransition.RequestFading += new Action(this.HandleFadingRequest);
    }

    public void BroadcastVrFadeStart() => ScreenWipeTransition.bFadingDone = true;

    public void BroadcastVrFadeFinish() => RoutineRunner.StartRoutine(this.DelayedContinue());

    private IEnumerator DelayedContinue()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ScreenWipeTransition screenWipeTransition = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        ScreenWipeTransition.bAnimationFinished = true;
        ScreenWipeTransition.bFadingDone = false;
        screenWipeTransition._animator.enabled = false;
        screenWipeTransition.gameObject.SetActive(false);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) new WaitForSeconds(0.2f);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private void HandleFadingRequest()
    {
      if (this._animator.enabled)
        this._animator.enabled = false;
      this._animator.enabled = true;
      this.gameObject.SetActive(true);
    }

    public static void StartFading()
    {
      ScreenWipeTransition.bAnimationFinished = false;
      ScreenWipeTransition.bFadingDone = false;
      Action requestFading = ScreenWipeTransition.RequestFading;
      if (requestFading == null)
        return;
      requestFading();
    }
  }
}
