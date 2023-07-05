// Decompiled with JetBrains decompiler
// Type: UDB.TaskBase
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace UDB
{
  public abstract class TaskBase
  {
    private bool _isRunning;
    private bool _isPaused;
    private bool _isStopped;

    public virtual bool isRunning
    {
      get => this._isRunning;
      protected set => this._isRunning = value;
    }

    public virtual bool isPaused
    {
      get => this._isPaused;
      protected set => this._isPaused = value;
    }

    public virtual bool isStopped
    {
      get => this._isStopped;
      protected set => this._isStopped = value;
    }

    public event TaskBase.FinishedHandler Finished;

    protected void FinishedHandlerCallback(bool manual)
    {
      TaskBase.FinishedHandler finished = this.Finished;
      if (finished == null)
        return;
      finished(manual);
    }

    public virtual void Start()
    {
    }

    public virtual void Stop()
    {
    }

    public virtual void Pause()
    {
    }

    public virtual void Unpause()
    {
    }

    public delegate void FinishedHandler(bool manual);
  }
}
