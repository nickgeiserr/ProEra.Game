// Decompiled with JetBrains decompiler
// Type: UDB.SlowMotionController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class SlowMotionController : SingletonBehaviour<SlowMotionController, MonoBehaviour>
  {
    public bool slowMotionEnabled;
    private float slowMotionSpeed;
    private float slowMotionDuration;
    private float slowMotionChangeSpeed;
    private float regularTimeScale;
    private float regularFixedDeltaTime;
    private float previousRealTime;
    private float deltaRealTime;

    private void Start()
    {
      this.regularTimeScale = Time.timeScale;
      this.regularFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
      if (!this.slowMotionEnabled)
        return;
      this.deltaRealTime = Time.realtimeSinceStartup - this.previousRealTime;
      if ((double) this.slowMotionDuration > 0.0)
      {
        this.slowMotionDuration = Mathf.MoveTowards(this.slowMotionDuration, 0.0f, this.deltaRealTime);
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, this.slowMotionSpeed, this.slowMotionChangeSpeed * this.deltaRealTime);
        Time.fixedDeltaTime = Mathf.Lerp(0.0f, this.regularFixedDeltaTime, Time.timeScale);
      }
      else if ((double) Time.timeScale < 1.0)
      {
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, this.slowMotionChangeSpeed * this.deltaRealTime);
        Time.fixedDeltaTime = Mathf.Lerp(0.0f, this.regularFixedDeltaTime, Time.timeScale);
      }
      this.previousRealTime = Time.realtimeSinceStartup;
    }

    private void _SetupSlowMotion(float slowSpeed, float duration, float changeSpeed = 1000f)
    {
      this.slowMotionSpeed = slowSpeed;
      this.slowMotionDuration = duration;
      this.slowMotionChangeSpeed = changeSpeed;
    }

    private void _Enable()
    {
      this.previousRealTime = Time.realtimeSinceStartup;
      this.slowMotionEnabled = true;
    }

    private void _Disable()
    {
      this.slowMotionEnabled = false;
      Time.timeScale = this.regularTimeScale;
      Time.fixedDeltaTime = this.regularFixedDeltaTime;
    }

    public static void SetupSlowMotion(float slowSpeed, float duration, float changeSpeed = 1000f) => SingletonBehaviour<SlowMotionController, MonoBehaviour>.instance._SetupSlowMotion(slowSpeed, duration, changeSpeed);
  }
}
