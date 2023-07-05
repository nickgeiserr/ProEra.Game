// Decompiled with JetBrains decompiler
// Type: UDB.UserSettingAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class UserSettingAudio : SingletonBehaviour<UserSettingAudio, MonoBehaviour>
  {
    private const float volumeDefault = 1f;
    [SerializeField]
    private UserData _userData;
    private float _sfxVolume;
    private float _musicVolume;
    private float _volume;

    private UserData userData
    {
      get
      {
        if ((Object) this._userData == (Object) null)
          this._userData = this.GetComponent<UserData>();
        return this._userData;
      }
    }

    public float sfxVolume
    {
      get => this._sfxVolume;
      set
      {
        if (Math.NearlyEqual((double) this._sfxVolume, (double) value))
          return;
        this._sfxVolume = value;
        this.userData.SetFloat("volumeSfx", this._sfxVolume);
        NotificationCenter.Broadcast("sfxVolumeChange");
      }
    }

    public float musicVolume
    {
      get => this._musicVolume;
      set
      {
        if (Math.NearlyEqual((double) this._musicVolume, (double) value))
          return;
        this._musicVolume = value;
        this.userData.SetFloat("volumeMusic", this._musicVolume);
        NotificationCenter.Broadcast("musicVolumeChange");
      }
    }

    public float volume
    {
      get => this._volume;
      set
      {
        if (Math.NearlyEqual((double) this._volume, (double) value))
          return;
        this._volume = value;
        this.userData.SetFloat("volumeMaster", this._volume);
        NotificationCenter.Broadcast("volumeChange");
      }
    }

    protected float _computedSFXVolume => this.sfxVolume * this.volume;

    protected float _computedMusicVolume => this.musicVolume * this.volume;

    public static float computedSFXVolume => SingletonBehaviour<UserSettingAudio, MonoBehaviour>.instance._computedSFXVolume;

    public static float computedMusicVolume => SingletonBehaviour<UserSettingAudio, MonoBehaviour>.instance._computedMusicVolume;

    public void _Save() => this.userData.Save();

    public void _Load()
    {
      this.volume = this.userData.GetFloat("volumeMaster", 1f);
      this.sfxVolume = this.userData.GetFloat("volumeSfx", 1f);
      this.musicVolume = this.userData.GetFloat("volumeMusic", 1f);
    }

    public static void Save() => SingletonBehaviour<UserSettingAudio, MonoBehaviour>.instance._Save();

    public static void Load() => SingletonBehaviour<UserSettingAudio, MonoBehaviour>.instance._Load();
  }
}
