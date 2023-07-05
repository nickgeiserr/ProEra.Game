// Decompiled with JetBrains decompiler
// Type: LockerRoomAudioManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using TB12;
using UnityEngine;
using Vars;

public class LockerRoomAudioManager : MonoBehaviour
{
  [SerializeField]
  private AudioTrigger[] _speakers;
  private AudioTrigger _currentPlayer;
  private readonly LinksHandler _linksHandler = new LinksHandler();

  private void Start() => this._linksHandler.SetLinks(new List<EventHandle>()
  {
    EventHandle.Link<float>(AppSounds.UpdateBGMVolume, new Action<float>(this.SetSpeakerVolume)),
    EventHandle.Link<float>((Variable<float>) ScriptableSingleton<SettingsStore>.Instance.BgmVolume, new Action<float>(this.SetSpeakerVolume)),
    EventHandle.Link<bool>((Variable<bool>) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic, new Action<bool>(this.ToggleLyrics)),
    EventHandle.Link<bool>(AppSounds.UpdateInstrumental, new Action<bool>(this.ToggleLyrics))
  });

  private void OnDestroy() => this._linksHandler.Clear();

  public void EnteredTrigger(AudioTrigger trigger)
  {
    if (!((UnityEngine.Object) this._currentPlayer != (UnityEngine.Object) trigger))
      return;
    if ((bool) (UnityEngine.Object) this._currentPlayer)
      this._currentPlayer.FadeAudio(false, (float) ScriptableSingleton<SettingsStore>.Instance.BgmVolume);
    this._currentPlayer = trigger;
    this._currentPlayer.FadeAudio(true, (float) ScriptableSingleton<SettingsStore>.Instance.BgmVolume);
  }

  private void SetSpeakerVolume(float volume)
  {
    if (this._speakers == null)
      return;
    int length = this._speakers.Length;
    for (int index = 0; index < length; ++index)
    {
      if (this._speakers[index].IsActive)
        this._speakers[index].Speaker.volume = volume;
    }
  }

  private void ToggleLyrics(bool instrumental)
  {
    if (this._speakers == null)
      return;
    int length = this._speakers.Length;
    for (int index = 0; index < length; ++index)
    {
      this._speakers[index].InstrumentalMusic = instrumental;
      if (this._speakers[index].IsActive)
        this._speakers[index].GetNextSoundTrack();
    }
  }

  public bool GetInstState() => (bool) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic;
}
