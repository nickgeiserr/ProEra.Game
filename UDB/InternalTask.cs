// Decompiled with JetBrains decompiler
// Type: UDB.InternalTask
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;

namespace UDB
{
  public class InternalTask : TaskBase
  {
    private IEnumerator taskCoroutine;

    public InternalTask(IEnumerator coroutine) => this.taskCoroutine = coroutine;

    public override void Start()
    {
      this.isRunning = true;
      TaskManager.StartTask(this.TaskAction());
    }

    public override void Stop()
    {
      this.isStopped = true;
      this.isRunning = false;
    }

    public override void Pause() => this.isPaused = true;

    public override void Unpause() => this.isPaused = false;

    private IEnumerator TaskAction()
    {
      InternalTask internalTask = this;
      yield return (object) null;
      IEnumerator coroutine = internalTask.taskCoroutine;
      while (internalTask.isRunning)
      {
        if (internalTask.isPaused)
          yield return (object) null;
        else if (coroutine != null && coroutine.MoveNext())
          yield return coroutine.Current;
        else
          internalTask.isRunning = false;
      }
      internalTask.FinishedHandlerCallback(internalTask.isStopped);
    }
  }
}
