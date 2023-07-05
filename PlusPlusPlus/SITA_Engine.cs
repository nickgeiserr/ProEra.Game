// Decompiled with JetBrains decompiler
// Type: PlusPlusPlus.SITA_Engine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MovementEffects;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace PlusPlusPlus
{
  public class SITA_Engine : MonoBehaviour
  {
    public static SITA_Engine instance;
    public List<SITA_Engine.ReplayMeta> replays = new List<SITA_Engine.ReplayMeta>();
    public SITA_Engine.ReplayMeta curRecording = new SITA_Engine.ReplayMeta();
    public int replayIndex;
    public int frame;
    public PlaybackModes controller;
    private bool _replaying;
    public bool recording;
    public float timeScale;
    public int framesPerSecond;
    public float frameLength;
    public float timeStep;
    public List<SITA_Master> masters = new List<SITA_Master>();

    public event SITA_Engine.OnRecordFrame RecordFrame;

    public event SITA_Engine.OnPlayFrame PlayFrame;

    public event SITA_Engine.OnReplayingEvent ReplayingEvent;

    public bool replaying
    {
      get => this._replaying;
      set
      {
        this._replaying = value;
        this.ReplayingEvent(value);
      }
    }

    private void Awake()
    {
      if ((Object) SITA_Engine.instance == (Object) null)
        SITA_Engine.instance = this;
      else
        Object.Destroy((Object) this);
      this.Initialize();
    }

    private void Start() => LeanTween.init(100000);

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void Initialize()
    {
      this.SetFrameLength();
      this.SetTimestep();
    }

    public void Register(SITA_Master obj) => this.masters.Add(obj);

    public void Unregister(SITA_Master obj)
    {
      for (int index = 0; index < this.masters.Count; ++index)
      {
        if ((Object) obj == (Object) this.masters[index])
        {
          this.masters.RemoveAt(index);
          break;
        }
      }
    }

    public void SetFrameLength() => this.frameLength = 1f / (float) this.framesPerSecond;

    public void SetTimestep() => this.timeStep = 1f / (float) this.framesPerSecond * this.timeScale;

    public void StartRecording()
    {
      this.recording = true;
      Timing.RunCoroutine(this._RecordController());
    }

    private IEnumerator<float> _RecordController()
    {
      int frameCount = 0;
      this.curRecording = new SITA_Engine.ReplayMeta();
      this.curRecording.frameRate = this.framesPerSecond;
      while (this.recording)
      {
        ++frameCount;
        this.RecordFrame();
        yield return Timing.WaitForSeconds(this.frameLength);
      }
      this.curRecording.frameCount = frameCount;
      this.replays.Add(this.curRecording);
    }

    public void StopRecording()
    {
      this.recording = false;
      new Thread((ThreadStart) (() =>
      {
        for (int index = 0; index < this.masters.Count; ++index)
          this.masters[index].SaveRecording();
      }))
      {
        IsBackground = true
      }.Start();
    }

    public void StartReplay(int index)
    {
      this.recording = false;
      this.SetTimestep();
      this.replayIndex = index;
      this.replaying = true;
      Timing.RunCoroutine(this._ReplayController());
    }

    private IEnumerator<float> _ReplayController()
    {
      yield return 0.0f;
      this.controller = PlaybackModes.Pause;
      this.frame = 0;
      while (this.replaying)
      {
        while (this.replaying && this.controller == PlaybackModes.Pause)
          yield return 0.0f;
        while (this.replaying && this.controller == PlaybackModes.Play && this.frame < this.replays[this.replayIndex].frameCount)
        {
          this.PlayFrame();
          ++this.frame;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        while (this.replaying && this.controller == PlaybackModes.SlowForward && this.frame < this.replays[this.replayIndex].frameCount)
        {
          this.PlayFrame();
          ++this.frame;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        while (this.replaying && this.controller == PlaybackModes.FastForward && this.frame < this.replays[this.replayIndex].frameCount)
        {
          this.PlayFrame();
          ++this.frame;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        if (this.replaying && this.controller == PlaybackModes.FrameForward && this.frame < this.replays[this.replayIndex].frameCount)
        {
          this.PlayFrame();
          ++this.frame;
          this.controller = PlaybackModes.Pause;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        while (this.replaying && this.controller == PlaybackModes.Rewind && this.frame > 0)
        {
          this.PlayFrame();
          --this.frame;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        while (this.replaying && this.controller == PlaybackModes.SlowBack && this.frame > 0)
        {
          this.PlayFrame();
          --this.frame;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        if (this.replaying && this.controller == PlaybackModes.FrameBack && this.frame > 0)
        {
          this.PlayFrame();
          --this.frame;
          this.controller = PlaybackModes.Pause;
          yield return Timing.WaitForSeconds(this.timeStep);
        }
        if (this.replaying && (this.frame >= this.replays[this.replayIndex].frameCount || this.frame <= this.replays[this.replayIndex].frameCount))
        {
          if (this.frame >= this.replays[this.replayIndex].frameCount)
            this.frame = this.replays[this.replayIndex].frameCount - 1;
          else if (this.frame <= 0)
            this.frame = 0;
          this.controller = PlaybackModes.Pause;
          yield return 0.0f;
        }
      }
      this.controller = PlaybackModes.Stop;
    }

    public void ChangePlaybackMode(PlaybackModes mode) => this.controller = mode;

    public void StopReplay() => this.replaying = false;

    public class ReplayMeta
    {
      public List<SITA_Engine.ReplayMeta.Events> events = new List<SITA_Engine.ReplayMeta.Events>();
      public int frameRate;
      public int frameCount;

      public ReplayMeta()
      {
      }

      public ReplayMeta(int count, int rate)
      {
        this.frameCount = count;
        this.frameRate = rate;
      }

      public void InsertEvent(int frame, string type) => this.events.Add(new SITA_Engine.ReplayMeta.Events(frame, type));

      public class Events
      {
        public int onFrame;
        public string type;

        public Events(int frame, string t)
        {
          this.onFrame = frame;
          this.type = t;
        }
      }
    }

    public delegate void OnRecordFrame();

    public delegate void OnPlayFrame();

    public delegate void OnReplayingEvent(bool replaying);
  }
}
