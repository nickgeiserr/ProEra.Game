// Decompiled with JetBrains decompiler
// Type: UDB.SFXPlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class SFXPlayer : MonoBehaviour
  {
    public const float refRate = 44100f;
    public bool playOnActive;
    public float playDelay;
    private bool started;
    private bool listeningForFinish;
    [SerializeField]
    private AudioSource _audioSource;
    private float _defaultVolume = 1f;

    public event SFXPlayer.OnSFXFinish sfxFinishCallback;

    public AudioSource audioSource
    {
      get
      {
        if ((Object) this._audioSource == (Object) null)
        {
          this._audioSource = this.GetComponent<AudioSource>();
          if ((Object) this._audioSource == (Object) null)
          {
            this.gameObject.AddComponent<AudioSource>();
            this._audioSource = this.GetComponent<AudioSource>();
          }
        }
        return this._audioSource;
      }
    }

    public bool isPlaying => this.audioSource.isPlaying;

    public float defaultVolume
    {
      get => this._defaultVolume;
      set => this._defaultVolume = value;
    }

    private void Awake()
    {
      NotificationCenter.AddListener("volumeChange", new Callback(this.UserSettingsChanged));
      NotificationCenter.AddListener("sfxVolumeChange", new Callback(this.UserSettingsChanged));
      this.InitAudioSource();
      this.OnAwake();
    }

    private void Start()
    {
      this.started = true;
      if (!this.playOnActive)
        return;
      this.Play();
    }

    private void OnDestroy()
    {
      NotificationCenter.RemoveListener("volumeChange", new Callback(this.UserSettingsChanged));
      NotificationCenter.RemoveListener("sfxVolumeChange", new Callback(this.UserSettingsChanged));
    }

    private void OnEnable()
    {
      if (!this.started || !this.playOnActive)
        return;
      this.Play();
    }

    private void OnDisable()
    {
    }

    private void Update()
    {
      if (!this.listeningForFinish || this.audioSource.isPlaying)
        return;
      this.AudioClipFinished(this.audioSource.clip);
    }

    private void InitAudioSource()
    {
      this.audioSource.playOnAwake = false;
      this.defaultVolume = this.audioSource.volume;
      this.audioSource.volume = this.defaultVolume * SingletonBehaviour<UserSettingAudio, MonoBehaviour>.instance.sfxVolume;
    }

    protected virtual void OnAwake()
    {
    }

    public virtual void PlayAudioClip(AudioClip audioClip)
    {
      this.audioSource.clip = audioClip;
      this.Play();
    }

    public virtual void Play()
    {
      this.audioSource.volume = this.defaultVolume * SingletonBehaviour<UserSettingAudio, MonoBehaviour>.instance.sfxVolume;
      if ((double) this.playDelay > 0.0)
        this.audioSource.PlayDelayed(this.playDelay);
      else
        this.audioSource.Play();
    }

    public virtual void Stop() => this.audioSource.Stop();

    public virtual void Pause() => this.audioSource.Pause();

    public virtual void UnPause() => this.audioSource.UnPause();

    protected virtual void AudioClipFinished(AudioClip audioClip)
    {
      if (this.sfxFinishCallback != null)
        this.sfxFinishCallback(this.audioSource.clip.name, this);
      this.listeningForFinish = false;
    }

    protected virtual void AudioClipStarted(AudioClip audioClip)
    {
    }

    private void TimerFinishedCallback()
    {
      this.audioSource.Play();
      this.listeningForFinish = true;
    }

    private void UserSettingsChanged()
    {
      if (!((Object) this.audioSource != (Object) null))
        return;
      this.audioSource.volume = this.defaultVolume * UserSettingAudio.computedSFXVolume;
    }

    public delegate void OnSFXFinish(string sfxName, SFXPlayer sfxPlayer);
  }
}
