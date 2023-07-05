// Decompiled with JetBrains decompiler
// Type: FootballVR.PlaybackInfo
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "PlaybackInfo", menuName = "TB12/Core/PlaybackInfo", order = 1)]
  public class PlaybackInfo : ScriptableObject, IPlaybackInfo
  {
    private float _playTime;
    private float _roundedTime;
    private float _cachedRoundedTime;
    private float _fullStartTime;
    private float _fullEndTime;
    private Coroutine _playbackRoutine;
    private bool _playing;
    private bool _hasPausedAtHike;
    private bool _paused;
    public float Speed = 1f;
    public readonly AppEvent<float> OnTick = new AppEvent<float>();
    public readonly AppEvent<float> OnRoundedTick = new AppEvent<float>();
    public readonly AppEvent OnPlaybackFinished = new AppEvent();

    public float PlayTime
    {
      get => this._playTime;
      set => this._playTime = value;
    }

    public float RoundedTime => this._roundedTime;

    public bool pauseBeforeHike { get; set; }

    public bool Playing => this._playing && !this._paused;

    protected void OnEnable() => this.Reset();

    public void StartOrContinue()
    {
      if (this._playing && this._paused)
        this._paused = false;
      else
        this.StartPlayback();
    }

    public void Pause()
    {
      this._paused = true;
      this._hasPausedAtHike = true;
      Debug.Log((object) ("Pausing playback " + this.name));
    }

    public void Reset()
    {
      this._playTime = this._fullStartTime;
      this._roundedTime = (float) Math.Round((double) this._playTime, 3);
      this.StopPlayback();
    }

    public void JumpToSnap()
    {
      this._playTime = -0.01f;
      this._roundedTime = (float) Math.Round((double) this._playTime, 3);
    }

    public void StartPlayback(int direction = 1, bool reset = true)
    {
      this.StopPlayback();
      this._paused = false;
      this._playing = true;
      this._playbackRoutine = RoutineRunner.StartRoutine(this.PlaybackRoutine(direction, reset));
    }

    public void StopPlayback()
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

    private IEnumerator PlaybackRoutine(int direction, bool reset = true)
    {
      if (reset)
      {
        this._hasPausedAtHike = false;
        this._playTime = this._fullStartTime;
        this._roundedTime = (float) Math.Round((double) this._playTime, 3);
      }
      float playEndTime = this._fullEndTime + 1f;
      while ((double) this._playTime < (double) playEndTime)
      {
        if (this._paused)
        {
          yield return (object) null;
        }
        else
        {
          float num = Time.deltaTime * (float) direction * this.Speed;
          if (this.pauseBeforeHike && !this._hasPausedAtHike && (double) this._playTime < -0.20000000298023224 && (double) this._playTime + (double) num > -0.20000000298023224)
          {
            this._paused = true;
            this._hasPausedAtHike = true;
          }
          else
          {
            this._playTime += num;
            this.OnTick?.Trigger(this._playTime);
            this._roundedTime = (float) Math.Round((double) this._playTime, 3);
            if ((double) this._cachedRoundedTime != (double) this._roundedTime)
            {
              this._cachedRoundedTime = this._roundedTime;
              this.OnRoundedTick?.Trigger(this._roundedTime);
            }
            yield return (object) null;
          }
        }
      }
      this.OnPlaybackFinished?.Trigger();
    }
  }
}
