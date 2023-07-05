// Decompiled with JetBrains decompiler
// Type: UISoundManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
  public static UISoundManager instance;
  [SerializeField]
  private AudioSource audioSource;
  [SerializeField]
  private AudioClip buttonClick_SFX;
  [SerializeField]
  private AudioClip buttonBack_SFX;
  [SerializeField]
  private AudioClip buttonToggle_SFX;
  [SerializeField]
  private AudioClip tabSwipe_SFX;

  private void Awake()
  {
    if ((Object) UISoundManager.instance == (Object) null)
    {
      UISoundManager.instance = this;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }
    else
      Object.Destroy((Object) this.gameObject);
  }

  private void PlaySoundFX(AudioClip c)
  {
    if (!PersistentSingleton<SaveManager>.Instance.gameSettings.SoundOn || this.audioSource.isPlaying && (Object) c != (Object) this.tabSwipe_SFX && (Object) c != (Object) this.buttonClick_SFX)
      return;
    this.audioSource.clip = c;
    this.audioSource.Play();
  }

  public void SetVolume(float v) => this.audioSource.volume = v;

  public void PlayButtonClick() => this.PlaySoundFX(this.buttonClick_SFX);

  public void PlayButtonBack() => this.PlaySoundFX(this.buttonBack_SFX);

  public void PlayButtonToggle() => this.PlaySoundFX(this.buttonToggle_SFX);

  public void PlayTabSwipe() => this.PlaySoundFX(this.tabSwipe_SFX);
}
