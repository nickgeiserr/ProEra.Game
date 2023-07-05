// Decompiled with JetBrains decompiler
// Type: MiniCampCrowdAudioManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MiniCampCrowdAudioManager : MonoBehaviour
{
  [SerializeField]
  private AudioSource crowdAudio;
  [SerializeField]
  private AudioSource changingCrowdAudio;
  [SerializeField]
  private AudioClip[] cheeringAudio;
  [SerializeField]
  private AudioClip[] booingAudio;
  [SerializeField]
  private AudioClip[] disapointedAudio;

  public void SetVolume(float vol)
  {
    if ((Object) this.crowdAudio != (Object) null)
      this.crowdAudio.volume = vol;
    if (!((Object) this.changingCrowdAudio != (Object) null))
      return;
    this.changingCrowdAudio.volume = vol;
  }

  private AudioClip GetCrowdBooSound() => this.booingAudio.Length != 0 ? this.booingAudio[Random.Range(0, this.booingAudio.Length - 1)] : (AudioClip) null;

  private AudioClip GetCrowdCheerSound() => this.cheeringAudio.Length != 0 ? this.cheeringAudio[Random.Range(0, this.cheeringAudio.Length - 1)] : (AudioClip) null;

  private AudioClip GetCrowdDisappointed() => this.disapointedAudio.Length != 0 ? this.disapointedAudio[Random.Range(0, this.disapointedAudio.Length - 1)] : (AudioClip) null;

  public void PlayCrowdCheer()
  {
    Debug.Log((object) nameof (PlayCrowdCheer));
    if (!((Object) this.changingCrowdAudio != (Object) null))
      return;
    this.changingCrowdAudio.clip = this.GetCrowdCheerSound();
    this.PlayChangingCrowdAudio();
  }

  public void PlayCrowdBoo()
  {
    if (!((Object) this.changingCrowdAudio != (Object) null))
      return;
    this.changingCrowdAudio.clip = this.GetCrowdBooSound();
    this.PlayChangingCrowdAudio();
  }

  public void PlayCrowdDisappointed()
  {
    if (!((Object) this.changingCrowdAudio != (Object) null))
      return;
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
