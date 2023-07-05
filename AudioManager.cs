// Decompiled with JetBrains decompiler
// Type: AudioManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager self;
  public CommentaryManager commentary;
  public GameAudio gameAudio;
  public QBAudio qbAudio;
  public CrowdAudio crowdAudio;
  public float gameSFXVolume;

  private void Awake() => AudioManager.self = this;

  public void CallStart()
  {
    this.commentary.Init();
    this.qbAudio.LoadAudioPaths();
    this.gameAudio.LoadAudioPaths();
    this.crowdAudio.LoadAudioPaths();
    this.gameSFXVolume = PersistentSingleton<SaveManager>.Instance.gameSettings.GameSFXVolume;
  }

  public float GetGameSFXVolume() => this.gameSFXVolume;

  public void SetGameSFXVolume(float value)
  {
    this.gameSFXVolume = value;
    PersistentSingleton<SaveManager>.Instance.gameSettings.GameSFXVolume = value;
    Ball.State.Volume.SetValue(value);
    MatchManager.instance.playersManager.SetVolume(value);
    this.crowdAudio.SetVolume(value);
    this.qbAudio.SetVolume(value);
  }

  public AudioClip GetAudioClip(AudioPath audioPath, int index)
  {
    if (!audioPath.saveAudioAfterUse)
      return this.LoadAudio(audioPath, index);
    if ((Object) audioPath.audioFiles[index - 1] == (Object) null)
      audioPath.audioFiles[index - 1] = this.LoadAudio(audioPath, index);
    return audioPath.audioFiles[index - 1];
  }

  private AudioClip LoadAudio(AudioPath audioPath, int index) => AddressablesData.instance.LoadAssetSync<AudioClip>(AddressablesData.CorrectingAssetKey(audioPath.path), this.ConvertToFileString(index));

  private string ConvertToFileString(int i) => i < 10 ? "0" + i.ToString() : i.ToString();
}
