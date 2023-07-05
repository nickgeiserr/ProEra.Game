// Decompiled with JetBrains decompiler
// Type: FootballVR.DecoyPlaybackInfo
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "DecoyPlaybackInfo", menuName = "TB12/Core/DecoyPlaybackInfo", order = 1)]
  public class DecoyPlaybackInfo : ScriptableObject, IPlaybackInfo
  {
    private float _playTime;
    private float _roundedTime;
    private float _fullStartTime;
    private float _fullEndTime;
    private Coroutine _playbackRoutine;
    private bool _playing;
    private bool _paused;
    public float Speed = 1f;
    public int PlaybackDirection = 1;
    public float TargetTime;

    public float PlayTime
    {
      get => this._playTime;
      set => this._playTime = value;
    }

    public float RoundedTime => this._roundedTime;

    public bool Playing => this._playing && !this._paused;

    public bool CatchUpMode { get; set; }

    protected void OnEnable()
    {
      this.Reset();
      this.StopPlayback();
    }

    public void StartOrContinue()
    {
      if (this._playing && this._paused)
        this._paused = false;
      else
        this.StartPlayback();
    }

    public void Pause() => this._paused = true;

    public void Reset()
    {
      this._playTime = this._fullStartTime;
      this._roundedTime = (float) Math.Round((double) this._playTime, 3);
      this.CatchUpMode = true;
    }

    public void JumpToSnap()
    {
      this._playTime = -0.01f;
      this._roundedTime = (float) Math.Round((double) this._playTime, 3);
    }

    public void StartPlayback(int direction = 1, bool reset = true)
    {
      this.PlaybackDirection = direction;
      if (!this._playing)
      {
        this._playbackRoutine = RoutineRunner.StartRoutine(this.PlaybackRoutine(reset));
        this._playing = true;
      }
      this._paused = false;
    }

    private void StopPlayback()
    {
      this._playing = false;
      this._paused = false;
      if (this._playbackRoutine == null)
        return;
      RoutineRunner.StopRoutine(this._playbackRoutine);
    }

    public void Setup(float startTime, float endTime)
    {
      this._fullStartTime = startTime;
      this._fullEndTime = endTime;
    }

    private IEnumerator PlaybackRoutine(bool reset = true)
    {
      if (reset)
      {
        this._playTime = this._fullStartTime;
        this._roundedTime = (float) Math.Round((double) this._playTime, 3);
      }
      while (true)
      {
        while (this.ApplyCatchUp() || this._paused)
          yield return (object) null;
        this._playTime += Time.deltaTime * (float) this.PlaybackDirection * this.Speed;
        this._roundedTime = (float) Math.Round((double) this._playTime, 3);
        yield return (object) null;
      }
    }

    private bool ApplyCatchUp()
    {
      if (!this.CatchUpMode)
        return false;
      float f1 = Time.deltaTime * (float) this.PlaybackDirection;
      float f2 = this.TargetTime - this._playTime;
      AvatarPlaybackSettings playbackSettings = ScriptableSingleton<AvatarsSettings>.Instance.AvatarPlaybackSettings;
      bool flag = (double) Mathf.Abs(f2) < (double) Mathf.Abs(f1 * 2f);
      if (!flag)
      {
        float catchUpThreshold = playbackSettings.SoftCatchUpThreshold;
        float f3 = f2 * playbackSettings.CatchupLerpFactor;
        float num;
        if ((double) Mathf.Abs(f2) < (double) catchUpThreshold)
        {
          float softCatchupSpeed = playbackSettings.SoftCatchupSpeed;
          float max = Mathf.Abs(f1 * softCatchupSpeed);
          num = Mathf.Clamp(f3, -max, max);
        }
        else
        {
          float max = playbackSettings.CatchUpMaxSpeed * Mathf.Abs(f1);
          if ((double) Mathf.Abs(f3) < (double) Mathf.Abs(f1) && (double) f3 * (double) f1 > 0.0)
            f3 = f1;
          num = Mathf.Clamp(f3, -max, max);
        }
        this._playTime += num;
        this._roundedTime = (float) Math.Round((double) this._playTime, 3);
      }
      return this.CatchUpMode = !flag;
    }
  }
}
