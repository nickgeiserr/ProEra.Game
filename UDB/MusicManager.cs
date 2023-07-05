// Decompiled with JetBrains decompiler
// Type: UDB.MusicManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class MusicManager : MonoBehaviour
  {
    public bool dontDestroy;
    public bool restartOnAppFocus;
    private const double rate = 44100.0;
    private float currentTime;
    public float changeFadeOutDelay;
    private int currentAutoplayIndex = -1;
    private MusicData currentMusic;
    private MusicData nextMusic;
    public MusicData[] trackList;
    private Dictionary<string, MusicData> musicDict;
    public string playOnStart;
    public MusicManager.AutoPlayType autoPlay;
    private MusicManager.State state;

    public event MusicManager.OnMusicFinish MusicFinishCallback;

    public bool isPlaying => this.state == MusicManager.State.Playing;

    private void Awake()
    {
      NotificationCenter.AddListener("volumeChange", new Callback(this.UserSettingsChanged));
      NotificationCenter.AddListener("musicVolumeChange", new Callback(this.UserSettingsChanged));
      this.musicDict = new Dictionary<string, MusicData>(this.trackList.Length);
      foreach (MusicData track in this.trackList)
      {
        track.defaultVolume = track.source.volume;
        this.musicDict.Add(track.name, track);
      }
      if (this.autoPlay != MusicManager.AutoPlayType.Shuffled)
        return;
      this.trackList.Shuffle<MusicData>();
    }

    private void Start()
    {
      if (!this.playOnStart.IsEmptyOrWhiteSpaceOrNull())
      {
        this.Play(this.playOnStart, true);
      }
      else
      {
        if (this.autoPlay == MusicManager.AutoPlayType.None)
          return;
        this.currentAutoplayIndex = -1;
        this.AutoPlaylistNext();
      }
    }

    private void OnDestroy()
    {
      NotificationCenter.RemoveListener("volumeChange", new Callback(this.UserSettingsChanged));
      NotificationCenter.RemoveListener("musicVolumeChange", new Callback(this.UserSettingsChanged));
      this.MusicFinishCallback = (MusicManager.OnMusicFinish) null;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
      switch (this.state)
      {
        case MusicManager.State.Playing:
          this.PlayingStateAction();
          break;
        case MusicManager.State.Changing:
          this.ChangingStateAction();
          break;
      }
    }

    private void OnApplicationFocus(bool focus)
    {
      if (!(this.restartOnAppFocus & focus) || this.state != MusicManager.State.Playing || this.currentMusic == null)
        return;
      this.currentMusic.source.Stop();
      this.currentMusic.source.Play();
    }

    private void UserSettingsChanged()
    {
      if (this.currentMusic == null || this.state != MusicManager.State.Playing)
        return;
      this.currentMusic.source.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
    }

    private void PlayingStateAction()
    {
      if (this.currentMusic.source.loop || this.currentMusic.source.isPlaying)
        return;
      string name = this.currentMusic.name;
      if (this.autoPlay != MusicManager.AutoPlayType.None)
        this.AutoPlaylistNext();
      else if (this.currentMusic.loop)
      {
        this.currentMusic.source.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
        if ((double) this.currentMusic.loopDelay > 0.0)
          this.currentMusic.source.Play((ulong) System.Math.Round(44100.0 * (double) this.currentMusic.loopDelay));
        else
          this.currentMusic.source.Play();
      }
      else
        this.SetState(MusicManager.State.None);
      if (this.MusicFinishCallback == null)
        return;
      this.MusicFinishCallback(name);
    }

    private void ChangingStateAction()
    {
      this.currentTime += Time.deltaTime;
      if ((double) this.currentTime >= (double) this.changeFadeOutDelay)
      {
        this.currentMusic.source.Stop();
        if (this.nextMusic != null)
        {
          this.currentMusic = this.nextMusic;
          this.nextMusic = (MusicData) null;
          this.SetState(MusicManager.State.Playing);
        }
        else
          this.SetState(MusicManager.State.None);
      }
      else
        this.currentMusic.source.volume = this.currentMusic.defaultVolume * (float) (1.0 - (double) this.currentTime / (double) this.changeFadeOutDelay) * UserSettingAudio.computedMusicVolume;
    }

    public bool Exists(string name) => this.musicDict.ContainsKey(name);

    public void Play(string name, bool immediate)
    {
      MusicData musicData;
      if (!this.musicDict.TryGetValue(name, out musicData))
      {
        Debug.LogWarning((object) ("Unknown music: " + name));
      }
      else
      {
        if (this.currentMusic == null || immediate && this.currentMusic != musicData)
        {
          this.Stop(false);
          this.currentMusic = musicData;
          this.currentMusic.source.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
          this.currentMusic.source.Play();
          this.SetState(MusicManager.State.Playing);
        }
        else if (this.currentMusic != musicData)
        {
          this.nextMusic = musicData;
          this.SetState(MusicManager.State.Changing);
        }
        if (this.autoPlay == MusicManager.AutoPlayType.None)
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

    public void Stop(bool fade)
    {
      if (this.state == MusicManager.State.None)
        return;
      if (fade)
      {
        this.nextMusic = (MusicData) null;
        this.SetState(MusicManager.State.Changing);
      }
      else
      {
        this.currentMusic.source.Stop();
        this.SetState(MusicManager.State.None);
      }
    }

    private void AutoPlaylistNext()
    {
      this.Stop(false);
      ++this.currentAutoplayIndex;
      if (this.currentAutoplayIndex >= this.trackList.Length)
        this.currentAutoplayIndex = 0;
      this.currentMusic = this.trackList[this.currentAutoplayIndex];
      this.currentMusic.source.volume = this.currentMusic.defaultVolume * UserSettingAudio.computedMusicVolume;
      this.currentMusic.source.Play();
      this.SetState(MusicManager.State.Playing);
    }

    private void SetState(MusicManager.State newState)
    {
      this.state = newState;
      this.currentTime = 0.0f;
      if (this.state != MusicManager.State.None)
        return;
      this.currentMusic = (MusicData) null;
    }

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

    public delegate void OnMusicFinish(string curMusicName);
  }
}
