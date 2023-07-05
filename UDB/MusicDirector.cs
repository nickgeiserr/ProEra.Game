// Decompiled with JetBrains decompiler
// Type: UDB.MusicDirector
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (AudioSource))]
  public class MusicDirector : SingletonBehaviour<MusicDirector, MonoBehaviour>
  {
    private static bool isFirstTime = true;
    public bool restartOnAppFocus;
    private const double rate = 44100.0;
    private float currentTime;
    public float changeFadeOutDelay;
    private int currentAutoplayIndex = -1;
    private MusicTrack currentMusic;
    private MusicTrack nextMusic;
    public MusicTrack[] trackList;
    private Dictionary<string, MusicTrack> musicDict;
    public AudioSource _audioSource;
    public string playOnStart;
    public MusicDirector.AutoPlayType autoPlay;
    private MusicDirector.State state;

    public AudioSource audioSource
    {
      get
      {
        if ((UnityEngine.Object) this._audioSource == (UnityEngine.Object) null)
        {
          this._audioSource = this.GetComponent<AudioSource>();
          if ((UnityEngine.Object) this._audioSource == (UnityEngine.Object) null)
            this._audioSource = this.gameObject.AddComponent<AudioSource>();
        }
        return this._audioSource;
      }
    }

    public bool isPlaying => this.state == MusicDirector.State.Playing;

    private new void Awake()
    {
      if (MusicDirector.isFirstTime)
      {
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
        MusicDirector.isFirstTime = false;
      }
      else
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.gameObject);
      this.musicDict = new Dictionary<string, MusicTrack>(this.trackList.Length);
      for (int index = 0; index < this.trackList.Length; ++index)
      {
        this.trackList[index].defaultVolume = this.audioSource.volume;
        this.musicDict.Add(this.trackList[index].name, this.trackList[index]);
      }
      if (this.autoPlay != MusicDirector.AutoPlayType.Shuffled)
        return;
      this.trackList.Shuffle<MusicTrack>();
    }

    private void Start()
    {
      if (!this.playOnStart.IsEmptyOrWhiteSpaceOrNull())
      {
        MusicDirector.Play(this.playOnStart, true);
      }
      else
      {
        if (this.autoPlay == MusicDirector.AutoPlayType.None)
          return;
        this.currentAutoplayIndex = -1;
        this.AutoPlaylistNext();
      }
    }

    private void OnDestroy()
    {
    }

    private void OnEnable()
    {
      NotificationCenter.AddListener("volumeChange", new Callback(this.UserSettingsChanged));
      NotificationCenter.AddListener("musicVolumeChange", new Callback(this.UserSettingsChanged));
    }

    private void OnDisable()
    {
      NotificationCenter.RemoveListener("volumeChange", new Callback(this.UserSettingsChanged));
      NotificationCenter.RemoveListener("musicVolumeChange", new Callback(this.UserSettingsChanged));
    }

    private void Update()
    {
      switch (this.state)
      {
        case MusicDirector.State.Playing:
          this.PlayingStateAction();
          break;
        case MusicDirector.State.Changing:
          this.ChangingStateAction();
          break;
      }
    }

    private void OnApplicationFocus(bool focus)
    {
      if (!(this.restartOnAppFocus & focus) || this.state != MusicDirector.State.Playing || this.currentMusic == null)
        return;
      this.audioSource.Stop();
      this.audioSource.clip = this.currentMusic.audioClip;
      this.audioSource.Play();
    }

    private void UserSettingsChanged()
    {
      if (this.currentMusic == null || this.state != MusicDirector.State.Playing)
        return;
      this.audioSource.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
    }

    private void PlayingStateAction()
    {
      if (this.audioSource.loop || this.audioSource.isPlaying)
        return;
      string name = this.currentMusic.name;
      if (this.autoPlay != MusicDirector.AutoPlayType.None)
        this.AutoPlaylistNext();
      else if (this.currentMusic.loop)
      {
        this.audioSource.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
        if ((double) this.currentMusic.loopDelay > 0.0)
        {
          this.audioSource.clip = this.currentMusic.audioClip;
          this.audioSource.Play((ulong) System.Math.Round(44100.0 * (double) this.currentMusic.loopDelay));
        }
        else
        {
          this.audioSource.clip = this.currentMusic.audioClip;
          this.audioSource.Play();
        }
      }
      else
        this.SetState(MusicDirector.State.None);
      NotificationCenter<string>.Broadcast("musicFinished", name);
    }

    private void ChangingStateAction()
    {
      this.currentTime += Time.deltaTime;
      if ((double) this.currentTime >= (double) this.changeFadeOutDelay)
      {
        this.audioSource.Stop();
        if (this.nextMusic != null)
        {
          this.currentMusic = this.nextMusic;
          this.nextMusic = (MusicTrack) null;
          this.SetState(MusicDirector.State.Playing);
        }
        else
          this.SetState(MusicDirector.State.None);
      }
      else
        this.audioSource.volume = this.currentMusic.defaultVolume * (float) (1.0 - (double) this.currentTime / (double) this.changeFadeOutDelay) * UserSettingAudio.computedMusicVolume;
    }

    private void AutoPlaylistNext()
    {
      MusicDirector.Stop(false);
      ++this.currentAutoplayIndex;
      if (this.currentAutoplayIndex >= this.trackList.Length)
        this.currentAutoplayIndex = 0;
      this.currentMusic = this.trackList[this.currentAutoplayIndex];
      this.audioSource.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
      this.audioSource.clip = this.currentMusic.audioClip;
      this.audioSource.Play();
      this.SetState(MusicDirector.State.Playing);
    }

    private void SetState(MusicDirector.State newState)
    {
      this.state = newState;
      this.currentTime = 0.0f;
      if (this.state != MusicDirector.State.None)
        return;
      this.currentMusic = (MusicTrack) null;
    }

    public bool _Exists(string name) => this.musicDict.ContainsKey(name);

    public void _Play(string name, bool immediate)
    {
      MusicTrack musicTrack;
      if (!this.musicDict.TryGetValue(name, out musicTrack))
      {
        Debug.LogWarning((object) ("Unknown music: " + name));
      }
      else
      {
        if (this.currentMusic == null || immediate && this.currentMusic != musicTrack)
        {
          MusicDirector.Stop(false);
          this.currentMusic = musicTrack;
          this.audioSource.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
          this.audioSource.clip = this.currentMusic.audioClip;
          this.audioSource.Play();
          this.SetState(MusicDirector.State.Playing);
        }
        else if (this.currentMusic != musicTrack)
        {
          this.nextMusic = musicTrack;
          this.SetState(MusicDirector.State.Changing);
        }
        if (this.autoPlay == MusicDirector.AutoPlayType.None)
          return;
        for (int index = 0; index < this.trackList.Length; ++index)
        {
          if (this.trackList[index].name.IsEqual(name))
          {
            this.currentAutoplayIndex = index;
            break;
          }
        }
      }
    }

    public void _Stop(bool fade)
    {
      if (this.state == MusicDirector.State.None)
        return;
      if (fade)
      {
        this.nextMusic = (MusicTrack) null;
        this.SetState(MusicDirector.State.Changing);
      }
      else
      {
        this.audioSource.Stop();
        this.SetState(MusicDirector.State.None);
      }
    }

    public static bool Exists(string name) => SingletonBehaviour<MusicDirector, MonoBehaviour>.instance._Exists(name);

    public static void Play(string name, bool immediate) => SingletonBehaviour<MusicDirector, MonoBehaviour>.instance._Play(name, immediate);

    public static void Stop(bool fade) => SingletonBehaviour<MusicDirector, MonoBehaviour>.instance._Stop(fade);

    public enum AutoPlayType
    {
      None,
      Order,
      Shuffled,
    }

    private enum State
    {
      None,
      Playing,
      Changing,
    }
  }
}
