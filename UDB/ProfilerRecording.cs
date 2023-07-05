// Decompiled with JetBrains decompiler
// Type: UDB.ProfilerRecording
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class ProfilerRecording
  {
    public string id;
    private bool started;
    private float startTime;
    public float seconds;
    public int count;

    public ProfilerRecording(string id) => this.id = id;

    private void BalanceError()
    {
      if (!DebugManager.StateForKey("Profiler Recording Message"))
        return;
      Debug.LogError((object) ("ProfilerRecording start/stops not balanced for '" + this.id + "'"));
    }

    public void Start()
    {
      if (this.started)
        this.BalanceError();
      ++this.count;
      this.started = true;
      this.startTime = Time.realtimeSinceStartup;
    }

    public void Stop()
    {
      double realtimeSinceStartup = (double) Time.realtimeSinceStartup;
      if (!this.started)
        this.BalanceError();
      this.started = false;
      double startTime = (double) this.startTime;
      this.seconds += (float) (realtimeSinceStartup - startTime);
    }

    public void Reset()
    {
      this.seconds = 0.0f;
      this.count = 0;
      this.started = false;
    }
  }
}
