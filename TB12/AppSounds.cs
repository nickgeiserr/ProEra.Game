// Decompiled with JetBrains decompiler
// Type: TB12.AppSounds
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections;
using UnityEngine;
using Vars;

namespace TB12
{
  public static class AppSounds
  {
    public static readonly VariableBool CrowdSound = new VariableBool(true);
    public static readonly VariableBool AnnouncerSound = new VariableBool(true);
    public static readonly VariableBool MusicSound = new VariableBool(true);
    public static readonly VariableBool PlayerChatterSound = new VariableBool(true);
    public static readonly AppEvent<ESfxTypes> SfxEvent = new AppEvent<ESfxTypes>();
    public static readonly AppEvent<ESfxTypes, Vector3> Sfx3DEvent = new AppEvent<ESfxTypes, Vector3>();
    public static readonly AppEvent<ESfxTypes, Transform> Sfx3DParentedEvent = new AppEvent<ESfxTypes, Transform>();
    public static readonly AppEvent<AppSounds.PlayerChatterData> PlayerChatterEvent = new AppEvent<AppSounds.PlayerChatterData>();
    public static readonly AppEvent<EVOTypes, bool> VoEvent = new AppEvent<EVOTypes, bool>();
    public static readonly AppEvent<EVOTypes, Transform, bool> Vo3DEvent = new AppEvent<EVOTypes, Transform, bool>();
    public static readonly AppEvent<EPracticeSoundType, Transform> PracticeSoundEvent = new AppEvent<EPracticeSoundType, Transform>();
    public static readonly AppEvent<EMusicTypes> MusicEvent = new AppEvent<EMusicTypes>();
    public static readonly AppEvent<EMusicTypes> MusicPlaylistEvent = new AppEvent<EMusicTypes>();
    public static readonly AppEvent<EMusicTypes, Transform> MusicPlaylist3DEvent = new AppEvent<EMusicTypes, Transform>();
    public static readonly AppEvent<EMusicTypes, Transform[]> MusicPlaylistMultiSpeakerEvent = new AppEvent<EMusicTypes, Transform[]>();
    public static readonly AppEvent<EAnnouncerType> AnnouncerEvent = new AppEvent<EAnnouncerType>();
    public static readonly AppEvent<EOCTypes> OCEvent = new AppEvent<EOCTypes>();
    public static readonly AppEvent<string> OCStringEvent = new AppEvent<string>();
    public static readonly AppEvent StopMusic = new AppEvent();
    public static readonly AppEvent<EStingerType> StingerEvent = new AppEvent<EStingerType>();
    public static readonly AppEvent<float> AmbientSettingsEvent = new AppEvent<float>();
    public static readonly AppEvent<bool> AmbientLowFilterEvent = new AppEvent<bool>();
    public static readonly AppEvent<ECrowdTypes, int> CrowdEvent = new AppEvent<ECrowdTypes, int>();
    public static readonly AppEvent<ECrowdTypes, Vector3> Crowd3DEvent = new AppEvent<ECrowdTypes, Vector3>();
    public static readonly AppEvent PlaySidelineEvent = new AppEvent();
    public static readonly AppEvent<int, System.Action> TutorialEvent = new AppEvent<int, System.Action>();
    public static readonly AppEvent<ETutorialType> TutorialFeedbackEvent = new AppEvent<ETutorialType>();
    public static readonly AppEvent<float> UpdateBGMVolume = new AppEvent<float>();
    public static readonly AppEvent<float> UpdateVOVolume = new AppEvent<float>();
    public static readonly AppEvent<float> UpdateSFXVolume = new AppEvent<float>();
    public static readonly AppEvent<float> UpdateStadiumVolume = new AppEvent<float>();
    public static readonly AppEvent<bool> UpdateInstrumental = new AppEvent<bool>();
    public static readonly AppEvent<ESfxTypes, int> StopSfxEvent = new AppEvent<ESfxTypes, int>();
    public static readonly AppEvent<EAnnouncerType, int> StopAnnouncerEvent = new AppEvent<EAnnouncerType, int>();
    public static readonly AppEvent<ECrowdTypes, int> StopCrowdEvent = new AppEvent<ECrowdTypes, int>();
    public static readonly AppEvent<EOCTypes> StopOCEvent = new AppEvent<EOCTypes>();
    public static readonly AppEvent<EVOTypes> StopVOEvent = new AppEvent<EVOTypes>();
    public static readonly AppEvent StopSidelineEvent = new AppEvent();
    public static readonly AppEvent StopPlayerChatterEvent = new AppEvent();
    public static readonly VariableBool AmbienceSound = new VariableBool();
    public static readonly float AMBIENT_LOWEST = 0.65f;
    public static readonly float AMBIENT_START = 0.8f;
    public static readonly float AMBIENT_HIGHEST = 0.9f;
    public static float AMBIENT_MOD = 0.0f;
    public static EMusicTypes _lastPlayedPlaylist;
    public static float CurrentAmbientVolume = AppSounds.AMBIENT_LOWEST;
    private static float TweenToAmbientVolume = AppSounds.AMBIENT_LOWEST;
    private static int tweenID;

    public static void PlaySfx(ESfxTypes sound) => AppSounds.SfxEvent.Trigger(sound);

    public static void PlayVO(EVOTypes vo, bool male = true) => AppSounds.VoEvent.Trigger(vo, male);

    public static void PlayVO3D(EVOTypes vo, Transform target, bool male = true) => AppSounds.Vo3DEvent.Trigger(vo, target, male);

    public static void PlayPracticeSound(EPracticeSoundType id, Transform target) => AppSounds.PracticeSoundEvent.Trigger(id, target);

    public static void PlayMusic(EMusicTypes music)
    {
      if (!(bool) AppSounds.MusicSound)
        return;
      AppSounds.MusicEvent.Trigger(music);
    }

    public static void PlayMusicPlaylist(EMusicTypes music)
    {
      if (!(bool) AppSounds.MusicSound)
        return;
      AppSounds._lastPlayedPlaylist = music;
      AppSounds.MusicPlaylistEvent.Trigger(music);
    }

    public static void PlayMusicPlaylist(EMusicTypes music, Transform position)
    {
      if (!(bool) AppSounds.MusicSound)
        return;
      AppSounds._lastPlayedPlaylist = music;
      AppSounds.MusicPlaylist3DEvent.Trigger(music, position);
    }

    public static void PlayMusicPlaylist(EMusicTypes music, Transform[] position)
    {
      if (!(bool) AppSounds.MusicSound)
        return;
      AppSounds._lastPlayedPlaylist = music;
      AppSounds.MusicPlaylistMultiSpeakerEvent.Trigger(music, position);
    }

    public static void PlayStinger(EStingerType id)
    {
      if (!(bool) AppSounds.MusicSound)
        return;
      AppSounds.StingerEvent.Trigger(id);
    }

    public static void AdjustAmbientVolume(float volume, bool noTween = false)
    {
      AppSounds.TweenToAmbientVolume = volume;
      LeanTween.cancel(AppSounds.tweenID);
      if (!noTween)
      {
        AppSounds.tweenID = LeanTween.value(AppSounds.CurrentAmbientVolume, AppSounds.TweenToAmbientVolume, 3f).setOnUpdate((Action<float>) (val =>
        {
          AppSounds.CurrentAmbientVolume = val;
          AppSounds.AmbientSettingsEvent.Trigger(AppSounds.CurrentAmbientVolume);
        })).uniqueId;
      }
      else
      {
        AppSounds.CurrentAmbientVolume = AppSounds.TweenToAmbientVolume;
        AppSounds.AmbientSettingsEvent.Trigger(AppSounds.CurrentAmbientVolume);
      }
    }

    public static void PlayAnnouncer(EAnnouncerType id)
    {
      if (!(bool) AppSounds.AnnouncerSound)
        return;
      AppSounds.AnnouncerEvent.Trigger(id);
    }

    public static void Play3DSfx(ESfxTypes sound, Vector3 pos) => AppSounds.Sfx3DEvent.Trigger(sound, pos);

    public static void Play3DSfx(ESfxTypes sound, Transform pos) => AppSounds.Sfx3DParentedEvent.Trigger(sound, pos);

    public static void PlayPlayerChatter(
      ESfxTypes sound,
      Vector3 pos,
      Transform trans,
      float delay,
      System.Action callback)
    {
      if (!(bool) AppSounds.PlayerChatterSound)
        return;
      AppSounds.PlayerChatterData playerChatterData = new AppSounds.PlayerChatterData(sound, pos, trans, delay, callback);
      AppSounds.PlayerChatterEvent.Trigger(playerChatterData);
    }

    public static void PlayPlayerCelebrationChatter(ESfxTypes sound, Transform trans)
    {
      if (!(bool) AppSounds.PlayerChatterSound)
        return;
      AppSounds.PlayerChatterData playerChatterData = new AppSounds.PlayerChatterData(sound, Vector3.zero, trans, UnityEngine.Random.Range(0.1f, 0.33f), (System.Action) null);
      AppSounds.PlayerChatterEvent.Trigger(playerChatterData);
    }

    public static void StopSfx(ESfxTypes id, int fadeOut = 0) => AppSounds.StopSfxEvent.Trigger(id, fadeOut);

    public static void StopAnnouncer(EAnnouncerType id, int fadeOut = 0) => AppSounds.StopAnnouncerEvent.Trigger(id, fadeOut);

    public static void StopCrowd(ECrowdTypes id, int fadeOut = 0) => AppSounds.StopCrowdEvent.Trigger(id, fadeOut);

    public static void StopOc(EOCTypes id) => AppSounds.StopOCEvent.Trigger(id);

    public static void StopVO(EVOTypes id) => AppSounds.StopVOEvent.Trigger(id);

    public static void SetAmbientMuffle(bool fadeIn) => AppSounds.AmbientLowFilterEvent.Trigger(fadeIn);

    public static void PlayCrowd(ECrowdTypes sound, int delay = 0)
    {
      if (!(bool) AppSounds.CrowdSound)
        return;
      AppSounds.CrowdEvent.Trigger(sound, delay);
    }

    public static void Play3DCrowd(ECrowdTypes sound, Vector3 pos)
    {
      if (!(bool) AppSounds.CrowdSound)
        return;
      AppSounds.Crowd3DEvent.Trigger(sound, pos);
    }

    public static void PlaySideline()
    {
      if (!(bool) AppSounds.CrowdSound)
        return;
      AppSounds.PlaySidelineEvent.Trigger();
    }

    public static void StopSideline() => AppSounds.StopSidelineEvent.Trigger();

    public static void StopPlayerChatter() => AppSounds.StopPlayerChatterEvent.Trigger();

    public static void PlayOC(EOCTypes id)
    {
      if (!(bool) AppSounds.AnnouncerSound)
        return;
      AppSounds.OCEvent.Trigger(id);
    }

    public static void PlayOC(string id)
    {
      if (!(bool) AppSounds.AnnouncerSound)
        return;
      AppSounds.OCStringEvent.Trigger(id);
    }

    public static EMusicTypes GetLastPlayedPlaylist() => AppSounds._lastPlayedPlaylist;

    public static void PlayTutorial(int step, System.Action callback) => AppSounds.TutorialEvent.Trigger(step, callback);

    public static void PlayTutorialFeedback(ETutorialType type) => AppSounds.TutorialFeedbackEvent.Trigger(type);

    public static IEnumerator WinSequence()
    {
      AppSounds.PlaySfx(ESfxTypes.kRoar);
      AppSounds.PlaySfx(ESfxTypes.kWhistle);
      yield return (object) new WaitForSeconds(0.7f);
      AppSounds.PlayStinger(EStingerType.kStinger1);
      yield return (object) new WaitForSeconds(0.5f);
    }

    public static IEnumerator FailSequence()
    {
      AppSounds.PlaySfx(ESfxTypes.kWhistle);
      yield return (object) new WaitForSeconds(0.3f);
      AppSounds.PlaySfx(ESfxTypes.kCatchFail);
      yield return (object) new WaitForSeconds(0.7f);
      AppSounds.PlayStinger(EStingerType.kStinger3);
    }

    public static void PlayCrowdReaction(bool positive, AppSounds.CrowdReactionSizes size)
    {
      Debug.Log((object) ("PlayCrowdReaction: positive[" + positive.ToString() + "]  Game.IsHomeTeamOnOffense[" + Game.IsHomeTeamOnOffense.ToString() + "]"));
      if (positive && Game.IsHomeTeamOnOffense || !positive && !Game.IsHomeTeamOnOffense)
      {
        switch (size)
        {
          case AppSounds.CrowdReactionSizes.Big:
            AppSounds.PlayCrowd(ECrowdTypes.kCrowdCheerBig);
            break;
          case AppSounds.CrowdReactionSizes.Med:
            AppSounds.PlayCrowd(ECrowdTypes.kCrowdCheerMed);
            break;
          case AppSounds.CrowdReactionSizes.Small:
            AppSounds.PlayCrowd(ECrowdTypes.kCrowdCheerSmall);
            break;
        }
      }
      else
      {
        switch (size)
        {
          case AppSounds.CrowdReactionSizes.Big:
            AppSounds.PlayCrowd(ECrowdTypes.kCrowdAwwBig);
            break;
          case AppSounds.CrowdReactionSizes.Med:
            AppSounds.PlayCrowd(ECrowdTypes.kCrowdAwwMed);
            break;
          case AppSounds.CrowdReactionSizes.Small:
            AppSounds.PlayCrowd(ECrowdTypes.kCrowdAwwSmall);
            break;
        }
      }
    }

    public static void PlayKickReturnCrowdBed()
    {
      AppSounds.PlayCrowd(ECrowdTypes.kCrowdBuildupHigh);
      AppSounds.AdjustAmbientVolume(AppSounds.AMBIENT_HIGHEST);
    }

    public static void PlayCrowdRamp(bool up)
    {
      float tweenToAmbientVolume = AppSounds.TweenToAmbientVolume;
      AppSounds.AdjustAmbientVolume(!up ? tweenToAmbientVolume * 0.7f : tweenToAmbientVolume / 0.7f);
    }

    public static void PlayOCPlayCall(PlayDataOff data)
    {
      if (!(bool) AppSounds.AnnouncerSound)
        return;
      BaseFormation baseFormation = data.GetFormation().GetBaseFormation();
      string str1 = "OC ";
      switch (baseFormation)
      {
        case BaseFormation.I_Form:
          str1 += "I form ";
          goto case BaseFormation.Hail_Mary;
        case BaseFormation.Single_Back:
          str1 += "Single back ";
          goto case BaseFormation.Hail_Mary;
        case BaseFormation.Hail_Mary:
          string str2 = str1 + (data.GetFormation().GetSubFormation().ToString().Replace("_", " ") + " ");
          string playName = data.GetPlayName();
          string id = str1 + playName;
          string str3 = str2 + playName;
          if (data.GetPlayType() == global::PlayType.FG && MatchManager.down == 4)
          {
            string str4 = " 4";
            id += str4;
            str3 += str4;
          }
          Debug.Log((object) ("oldAudioName [" + id + "] fullAudioName[" + str3 + "]"));
          if (AudioController.GetAudioItem(str3) != null)
          {
            AppSounds.PlayOC(str3);
            break;
          }
          AppSounds.PlayOC(id);
          break;
        case BaseFormation.Special_Teams:
          str1 += "Special Teams ";
          goto case BaseFormation.Hail_Mary;
        default:
          str1 = str1 + baseFormation.ToString() + " ";
          goto case BaseFormation.Hail_Mary;
      }
    }

    public static bool IsPlaying(ECrowdTypes type) => SfxSoundManager.IsPlayingCrowd(type);

    public static bool IsPlaying(EAnnouncerType type) => SfxSoundManager.IsPlayingAnnouncer(type);

    public static void PlayTunnelSounds()
    {
      if (!(bool) AppSounds.CrowdSound)
        return;
      AppSounds.PlaySfx(ESfxTypes.kTunnel);
      AppSounds.PlayMusic(EMusicTypes.kTunnel);
    }

    public static IEnumerator PlayTimeout(bool male, bool isPlayer)
    {
      if (isPlayer)
        AppSounds.PlayVO(EVOTypes.kTimeout, male);
      else
        AppSounds.PlayVO3D(EVOTypes.kTimeout3D, Game.OffensiveQB.trans);
      yield return (object) new WaitForSeconds(1f);
      AppSounds.PlaySfx(ESfxTypes.kTimeout);
      yield return (object) new WaitForSeconds(2f);
      AppSounds.PlayAnnouncer(EAnnouncerType.kTimeout);
    }

    public static IEnumerator FadeBGM(bool fadeIn, float time)
    {
      float startTime = Time.realtimeSinceStartup;
      float endTime = Time.realtimeSinceStartup + time;
      float currentTime = startTime;
      while ((double) currentTime < (double) endTime)
      {
        currentTime = Time.realtimeSinceStartup;
        float num = (currentTime - startTime) / time;
        if (!fadeIn)
          num = 1f - num;
        AppSounds.UpdateBGMVolume.Trigger(ScriptableSingleton<SettingsStore>.Instance.BgmVolume.Value * num);
        yield return (object) null;
      }
      AppSounds.UpdateBGMVolume.Trigger((float) ScriptableSingleton<SettingsStore>.Instance.BgmVolume);
    }

    public enum CrowdReactionSizes
    {
      Big,
      Med,
      Small,
    }

    public struct PlayerChatterData
    {
      public ESfxTypes sfx;
      public Vector3 offset;
      public Transform parent;
      public float delay;
      public System.Action callback;

      public PlayerChatterData(ESfxTypes s, Vector3 v, Transform t, float d, System.Action c)
      {
        this.sfx = s;
        this.offset = v;
        this.parent = t;
        this.delay = d;
        this.callback = c;
      }
    }
  }
}
