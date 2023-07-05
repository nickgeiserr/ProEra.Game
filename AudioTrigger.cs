// Decompiled with JetBrains decompiler
// Type: AudioTrigger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
  [SerializeField]
  private LockerRoomAudioManager _manager;
  [SerializeField]
  private AudioClip[] _music;
  [SerializeField]
  private AudioClip[] _lyricMusic;
  [SerializeField]
  private AudioSource _speaker;
  private int _currentAudioIndex;
  private readonly RoutineHandle _audioRoutine = new RoutineHandle();
  private bool _isActive;
  private bool _instrumental;

  public AudioSource Speaker => this._speaker;

  public bool IsActive => this._isActive;

  public bool InstrumentalMusic
  {
    set => this._instrumental = value;
  }

  private void Start()
  {
    this._speaker.volume = 0.0f;
    this._instrumental = this._manager.GetInstState();
    this.GetNextSoundTrack();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != LayerMask.NameToLayer("UserAvatar"))
      return;
    this._manager.EnteredTrigger(this);
  }

  private void OnDestroy()
  {
    if (this._audioRoutine == null)
      return;
    this._audioRoutine.Stop();
  }

  public void FadeAudio(bool fadeIn, float volume = 0.0f)
  {
    this._isActive = fadeIn;
    LeanTween.value(fadeIn ? 0.0f : volume, fadeIn ? volume : 0.0f, 1f).setOnUpdate((Action<float>) (value =>
    {
      if (!(bool) (UnityEngine.Object) this._speaker)
        return;
      this._speaker.volume = value;
    }));
  }

  public void GetNextSoundTrack()
  {
    int currentAudioIndex = this._currentAudioIndex;
    AudioClip[] audioClipArray = this._instrumental ? this._music : (this._lyricMusic.Length != 0 ? this._lyricMusic : this._music);
    do
    {
      this._currentAudioIndex = UnityEngine.Random.Range(0, audioClipArray.Length);
    }
    while (this._currentAudioIndex == currentAudioIndex && audioClipArray.Length > 1);
    this._speaker.clip = audioClipArray[this._currentAudioIndex];
    this._speaker.Play();
    this._audioRoutine.Run(this.WaitForAudioToFinish());
  }

  private IEnumerator WaitForAudioToFinish()
  {
    yield return (object) new WaitForSeconds(this._speaker.clip.length);
    this.GetNextSoundTrack();
  }
}
