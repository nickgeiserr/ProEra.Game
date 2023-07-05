// Decompiled with JetBrains decompiler
// Type: StadiumSoundSystemManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TB12;
using UnityEngine;

public class StadiumSoundSystemManager : MonoBehaviour
{
  public SettingsStore _settingStore;
  private static StadiumSoundSystemManager Instance;
  [SerializeField]
  private AudioSource[] SpeakerAudioSources;
  [SerializeField]
  private AudioClip[] Playlist;
  private int _currentAudioIndex = -1;

  private void Awake()
  {
    StadiumSoundSystemManager.Instance = this;
    this.InitializeMusicVolume();
  }

  public static AudioClip GetNextSoundTrack() => StadiumSoundSystemManager.Instance._GetNextSoundTrack();

  public AudioClip _GetNextSoundTrack()
  {
    int currentAudioIndex = this._currentAudioIndex;
    do
    {
      this._currentAudioIndex = UnityEngine.Random.Range(0, this.Playlist.Length);
    }
    while (this._currentAudioIndex == currentAudioIndex && this.Playlist.Length > 1);
    return this.Playlist[this._currentAudioIndex];
  }

  private void InitializeMusicVolume()
  {
    if (!((UnityEngine.Object) this._settingStore != (UnityEngine.Object) null))
      return;
    ((IEnumerable<AudioSource>) this.SpeakerAudioSources).ToList<AudioSource>().ForEach((Action<AudioSource>) (source => source.volume = (float) this._settingStore.StadiumVolume));
  }
}
