// Decompiled with JetBrains decompiler
// Type: CrowdAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class CrowdAudio : MonoBehaviour
{
  [SerializeField]
  private AudioSource mainCrowdAudio;
  [SerializeField]
  private AudioSource changingCrowdAudio;
  private string rootFolder = "crowdaudio/";
  public AudioPath crowdBoo;
  public AudioPath crowdCheer;
  public AudioPath crowdDisappointed;

  public void LoadAudioPaths()
  {
    this.crowdBoo = new AudioPath(this.rootFolder + "CROWD_BOO", 1, true);
    this.crowdCheer = new AudioPath(this.rootFolder + "CROWD_CHEER", 1, true);
    this.crowdDisappointed = new AudioPath(this.rootFolder + "CROWD_DISAPPOINTED", 1, true);
  }

  public void SetVolume(float vol)
  {
    this.mainCrowdAudio.volume = vol;
    this.changingCrowdAudio.volume = vol;
  }

  private AudioClip GetCrowdBooSound() => AudioManager.self.GetAudioClip(this.crowdBoo, Random.Range(0, this.crowdBoo.count) + 1);

  private AudioClip GetCrowdCheerSound() => AudioManager.self.GetAudioClip(this.crowdCheer, Random.Range(0, this.crowdCheer.count) + 1);

  private AudioClip GetCrowdDisappointed() => AudioManager.self.GetAudioClip(this.crowdDisappointed, Random.Range(0, this.crowdDisappointed.count) + 1);

  public void PlayCrowdCheer()
  {
    this.changingCrowdAudio.clip = this.GetCrowdCheerSound();
    this.PlayChangingCrowdAudio();
  }

  public void PlayCrowdBoo()
  {
    this.changingCrowdAudio.clip = this.GetCrowdBooSound();
    this.PlayChangingCrowdAudio();
  }

  public void PlayCrowdDisappointed()
  {
    this.changingCrowdAudio.clip = this.GetCrowdDisappointed();
    this.PlayChangingCrowdAudio();
  }

  private void PlayChangingCrowdAudio()
  {
    if (!((Object) this.changingCrowdAudio.clip != (Object) null))
      return;
    this.changingCrowdAudio.Play();
  }
}
