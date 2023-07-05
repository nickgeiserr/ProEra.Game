// Decompiled with JetBrains decompiler
// Type: TB12.RandomizedTargetsController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class RandomizedTargetsController : TargetsController
  {
    [SerializeField]
    private TargetsController[] _targetsControllers;
    private readonly RoutineHandle _switchingRoutine = new RoutineHandle();
    private TargetsController _currentController;
    private bool _targetWasHit;

    private void Awake()
    {
      foreach (Component targetsController in this._targetsControllers)
        targetsController.gameObject.SetActive(false);
      VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
    }

    private void OnDestroy()
    {
      this._switchingRoutine.Stop();
      VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
    }

    private void OnDisable() => this._switchingRoutine.Stop();

    private void HandleThrowResult(bool targetHit, float distance)
    {
      if (!targetHit)
        return;
      this._targetWasHit = true;
    }

    public override void SetState(bool state)
    {
      if (state == this._shown)
        return;
      this._shown = state;
      if (state)
      {
        this._switchingRoutine.Run(this.RandomizeRoutine());
      }
      else
      {
        this._switchingRoutine.Stop();
        if (!((UnityEngine.Object) this._currentController != (UnityEngine.Object) null))
          return;
        this._currentController.SetState(false);
      }
    }

    private IEnumerator RandomizeRoutine()
    {
      RandomizedTargetsController targetsController = this;
      while (true)
      {
        if ((UnityEngine.Object) targetsController._currentController != (UnityEngine.Object) null)
        {
          targetsController._currentController.SetState(false);
          yield return (object) new WaitForSeconds(0.8f);
        }
        // ISSUE: reference to a compiler-generated method
        targetsController._currentController = ((IReadOnlyList<TargetsController>) targetsController._targetsControllers).GetRandom<TargetsController>(new Func<TargetsController, bool>(targetsController.\u003CRandomizeRoutine\u003Eb__9_0));
        targetsController._currentController.gameObject.SetActive(true);
        targetsController._currentController.SetState(true);
        // ISSUE: reference to a compiler-generated method
        yield return (object) new WaitUntil(new Func<bool>(targetsController.\u003CRandomizeRoutine\u003Eb__9_1));
        targetsController._currentController.ShowForSeconds(3f);
        yield return (object) new WaitForSeconds(3.1f);
        targetsController._targetWasHit = false;
      }
    }
  }
}
