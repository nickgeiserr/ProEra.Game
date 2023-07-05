// Decompiled with JetBrains decompiler
// Type: UDB.UpdatePumpManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public class UpdatePumpManager : SingletonBehaviour<UpdatePumpManager, MonoBehaviour>
  {
    private event System.Action fixedUpdateAction;

    private event System.Action updateAction;

    private event System.Action lateUpdateAction;

    private void OnDestroy()
    {
      this.RemoveAll(this.fixedUpdateAction);
      this.RemoveAll(this.updateAction);
      this.RemoveAll(this.lateUpdateAction);
    }

    private void Update() => this.UpdateScripts(this.updateAction);

    private void FixedUpdate() => this.UpdateScripts(this.fixedUpdateAction);

    private void LateUpdate() => this.UpdateScripts(this.lateUpdateAction);

    private void UpdateScripts(System.Action action)
    {
      if (action == null)
        return;
      action();
    }

    private void RemoveAll(System.Action deadAction)
    {
      if (deadAction == null)
        return;
      foreach (Delegate invocation in deadAction.GetInvocationList())
        deadAction -= invocation as System.Action;
    }

    public void _Register(System.Action action, UpdateType updateType)
    {
      switch (updateType)
      {
        case UpdateType.FixedUpdate:
          this.fixedUpdateAction += action;
          break;
        case UpdateType.Update:
          this.updateAction += action;
          break;
        case UpdateType.LateUpdate:
          this.lateUpdateAction += action;
          break;
      }
    }

    public void _UnRegister(System.Action action, UpdateType updateType)
    {
      switch (updateType)
      {
        case UpdateType.FixedUpdate:
          this.fixedUpdateAction -= action;
          break;
        case UpdateType.Update:
          this.updateAction -= action;
          break;
        case UpdateType.LateUpdate:
          this.lateUpdateAction -= action;
          break;
      }
    }

    public void Register(System.Action action, UpdateType updateType) => SingletonBehaviour<UpdatePumpManager, MonoBehaviour>.instance._Register(action, updateType);

    public void UnRegister(System.Action action, UpdateType updateType) => SingletonBehaviour<UpdatePumpManager, MonoBehaviour>.instance._UnRegister(action, updateType);
  }
}
