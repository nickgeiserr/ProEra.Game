// Decompiled with JetBrains decompiler
// Type: UDB.Timer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public class Timer
  {
    private readonly System.Action onComplete;
    private readonly Action<float> onUpdate;
    private float startTime;
    private float lastUpdateTime;
    private float? timeElapsedBeforeCancel;
    private float? timeElapsedBeforePause;
    private readonly MonoBehaviour autoDestroyOwner;
    private readonly bool hasAutoDestroyOwner;
    private static TimerManager manager;

    public float duration { get; private set; }

    public bool isLooped { get; set; }

    public bool isCompleted { get; private set; }

    public bool usesRealTime { get; private set; }

    public bool isPaused => this.timeElapsedBeforePause.HasValue;

    public bool isCancelled => this.timeElapsedBeforeCancel.HasValue;

    public bool isDone => this.isCompleted || this.isCancelled || this.isOwnerDestroyed;

    private bool isOwnerDestroyed => this.hasAutoDestroyOwner && (UnityEngine.Object) this.autoDestroyOwner == (UnityEngine.Object) null;

    private Timer(
      float duration,
      System.Action onComplete,
      Action<float> onUpdate,
      bool isLooped,
      bool usesRealTime,
      MonoBehaviour autoDestroyOwner)
    {
      this.duration = duration;
      this.onComplete = onComplete;
      this.onUpdate = onUpdate;
      this.isLooped = isLooped;
      this.usesRealTime = usesRealTime;
      this.autoDestroyOwner = autoDestroyOwner;
      this.hasAutoDestroyOwner = (UnityEngine.Object) autoDestroyOwner != (UnityEngine.Object) null;
      this.startTime = this.GetWorldTime();
      this.lastUpdateTime = this.startTime;
    }

    private float GetWorldTime() => this.usesRealTime ? Time.realtimeSinceStartup : Time.time;

    private float GetFireTime() => this.startTime + this.duration;

    private float GetTimeDelta() => this.GetWorldTime() - this.lastUpdateTime;

    public void Update()
    {
      if (this.isDone)
        return;
      if (this.isPaused)
      {
        this.startTime += this.GetTimeDelta();
        this.lastUpdateTime = this.GetWorldTime();
      }
      else
      {
        this.lastUpdateTime = this.GetWorldTime();
        if (this.onUpdate != null)
          this.onUpdate(this.GetTimeElapsed());
        if ((double) this.GetWorldTime() < (double) this.GetFireTime())
          return;
        if (this.onComplete != null)
          this.onComplete();
        if (this.isLooped)
          this.startTime = this.GetWorldTime();
        else
          this.isCompleted = true;
      }
    }

    public void Cancel()
    {
      if (this.isDone)
        return;
      this.timeElapsedBeforeCancel = new float?(this.GetTimeElapsed());
      this.timeElapsedBeforePause = new float?();
    }

    public void Pause()
    {
      if (this.isPaused || this.isDone)
        return;
      this.timeElapsedBeforePause = new float?(this.GetTimeElapsed());
    }

    public void Resume()
    {
      if (!this.isPaused || this.isDone)
        return;
      this.timeElapsedBeforePause = new float?();
    }

    public float GetTimeElapsed() => this.isCompleted || (double) this.GetWorldTime() >= (double) this.GetFireTime() ? this.duration : this.timeElapsedBeforeCancel ?? this.timeElapsedBeforePause ?? this.GetWorldTime() - this.startTime;

    public float GetTimeRemaining() => this.duration - this.GetTimeElapsed();

    public float GetRatioComplete() => this.GetTimeElapsed() / this.duration;

    public float GetRatioRemaining() => this.GetTimeRemaining() / this.duration;

    public static Timer Register(
      float duration,
      System.Action onComplete,
      Action<float> onUpdate = null,
      bool isLooped = false,
      bool useRealTime = false,
      MonoBehaviour autoDestroyOwner = null)
    {
      if ((UnityEngine.Object) Timer.manager == (UnityEngine.Object) null)
      {
        TimerManager objectOfType = UnityEngine.Object.FindObjectOfType<TimerManager>();
        if ((UnityEngine.Object) objectOfType != (UnityEngine.Object) null)
        {
          Timer.manager = objectOfType;
        }
        else
        {
          GameObject gameObject = new GameObject();
          gameObject.name = "TimerManager";
          Timer.manager = gameObject.AddComponent<TimerManager>();
        }
      }
      Timer timer = new Timer(duration, onComplete, onUpdate, isLooped, useRealTime, autoDestroyOwner);
      Timer.manager.RegisterTimer(timer);
      return timer;
    }

    public static void Cancel(Timer timer) => timer?.Cancel();

    public static void Pause(Timer timer) => timer?.Pause();

    public static void Resume(Timer timer) => timer?.Resume();

    public static void CancelAllRegisteredTimers()
    {
      if (!((UnityEngine.Object) Timer.manager != (UnityEngine.Object) null))
        return;
      Timer.manager.CancelAllTimers();
    }
  }
}
