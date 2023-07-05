// Decompiled with JetBrains decompiler
// Type: UDB.Task
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;

namespace UDB
{
  public class Task : TaskBase
  {
    private InternalTask internalTask;

    public override bool isRunning => this.internalTask.isRunning;

    public override bool isPaused => this.internalTask.isPaused;

    public override bool isStopped => this.internalTask.isStopped;

    public Task(IEnumerator task, bool autoStart = true)
    {
      this.internalTask = new InternalTask(task);
      this.internalTask.Finished += new TaskBase.FinishedHandler(this.TaskFinished);
      if (!autoStart)
        return;
      this.Start();
    }

    private void TaskFinished(bool manual) => this.FinishedHandlerCallback(manual);

    public override void Start() => this.internalTask.Start();

    public override void Stop() => this.internalTask.Stop();

    public override void Pause() => this.internalTask.Pause();

    public override void Unpause() => this.internalTask.Unpause();

    public static Task NewTask(IEnumerator task, bool autoStart = true) => new Task(task, autoStart);
  }
}
