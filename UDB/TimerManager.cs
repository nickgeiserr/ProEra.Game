// Decompiled with JetBrains decompiler
// Type: UDB.TimerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace UDB
{
  public class TimerManager : SerializedMonoBehaviour
  {
    private List<Timer> _timers;
    private List<Timer> _timersToAdd;

    private List<Timer> timers
    {
      get
      {
        if (this._timers == null)
          this._timers = new List<Timer>();
        return this._timers;
      }
      set => this._timers = value;
    }

    private List<Timer> timersToAdd
    {
      get
      {
        if (this._timersToAdd == null)
          this._timersToAdd = new List<Timer>();
        return this._timersToAdd;
      }
      set => this._timersToAdd = value;
    }

    private void Update() => this.UpdateAllTimers();

    private void UpdateAllTimers()
    {
      if (this.timersToAdd.Count > 0)
      {
        this.timers.AddRange((IEnumerable<Timer>) this._timersToAdd);
        this.timersToAdd.Clear();
      }
      for (int index = 0; index < this.timers.Count; ++index)
        this.timers.ToArray()[index].Update();
      this.timers.RemoveAll((Predicate<Timer>) (t => t.isDone));
    }

    public void RegisterTimer(Timer timer) => this.timersToAdd.Add(timer);

    public void CancelAllTimers()
    {
      for (int index = 0; index < this.timers.Count; ++index)
        this.timers.ToArray()[index].Cancel();
      this.timers = new List<Timer>();
      this.timersToAdd = new List<Timer>();
    }
  }
}
