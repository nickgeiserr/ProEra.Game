// Decompiled with JetBrains decompiler
// Type: FootballVR.LocomotionActivator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  public class LocomotionActivator : MonoBehaviour
  {
    [SerializeField]
    private HandsDataModel _handData;
    [SerializeField]
    private ArmSwingLocomotion _armSwingLocomotion;
    private LocomotionMechanic _currentMechanic;

    private IEnumerator Start()
    {
      LocomotionActivator locomotionActivator = this;
      locomotionActivator._handData.Initialize();
      yield return (object) null;
      VRState.LocomotionEnabled.OnValueChanged += new Action<bool>(locomotionActivator.HandleLocomotionChanged);
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(locomotionActivator.\u003CStart\u003Eb__3_0));
      locomotionActivator.HandleControllerInitialized();
    }

    private void HandleLocomotionChanged(bool state)
    {
      if (!((UnityEngine.Object) this._currentMechanic != (UnityEngine.Object) null))
        return;
      this._currentMechanic.SetState(state);
    }

    private void HandleControllerInitialized()
    {
      this._currentMechanic = (LocomotionMechanic) this._armSwingLocomotion;
      this._currentMechanic.SetState((bool) VRState.LocomotionEnabled);
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this._currentMechanic != (UnityEngine.Object) null)
        this._currentMechanic.SetState(false);
      VRState.LocomotionEnabled.OnValueChanged -= new Action<bool>(this.HandleLocomotionChanged);
    }
  }
}
