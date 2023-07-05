// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerRandomizedTargets
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class MultiplayerRandomizedTargets : TargetsController
  {
    [SerializeField]
    private TargetsController[] _targetsControllers;
    private readonly RoutineHandle _switchingRoutine = new RoutineHandle();
    [SerializeField]
    private TargetsController _currentController;

    public int targetGroupsCount => this._targetsControllers.Length;

    private void Awake()
    {
      foreach (Component targetsController in this._targetsControllers)
        targetsController.gameObject.SetActive(false);
    }

    private void OnDisable() => this._switchingRoutine.Stop();

    public override void SetState(bool state)
    {
      if (state == this._shown)
        return;
      this._shown = state;
      if (state || !((Object) this._currentController != (Object) null))
        return;
      this._currentController.SetState(false);
      this._switchingRoutine.Stop();
    }

    public void LoadTargetGroup(int groupId)
    {
      if (this._switchingRoutine.running)
        return;
      this._switchingRoutine.Run(this.SwitchTargetsGroupRoutine(groupId));
    }

    private IEnumerator SwitchTargetsGroupRoutine(int newTargetGroup)
    {
      if ((Object) this._currentController != (Object) null)
      {
        this._currentController.ShowForSeconds(10f);
        this._currentController.SetState(false);
      }
      if (newTargetGroup < 0 || newTargetGroup >= this._targetsControllers.Length)
      {
        Debug.LogError((object) string.Format("Invalid targetGroupId: {0}", (object) newTargetGroup));
        this._switchingRoutine.Stop();
      }
      else
      {
        this._currentController = this._targetsControllers[newTargetGroup];
        this._currentController.gameObject.SetActive(true);
        this._currentController.SetState(true);
        this._switchingRoutine.Stop();
        yield break;
      }
    }

    public int GetRandomTargetIndex()
    {
      int num = (Object) this._currentController != (Object) null ? ((IReadOnlyList<TargetsController>) this._targetsControllers).IndexOf<TargetsController>(this._currentController) : -1;
      int length = this._targetsControllers.Length;
      if (length < 2)
      {
        Debug.LogError((object) "Cant get random index when only one target..");
        return 0;
      }
      int randomTargetIndex;
      do
      {
        randomTargetIndex = Random.Range(0, length);
      }
      while (randomTargetIndex == num);
      return randomTargetIndex;
    }
  }
}
