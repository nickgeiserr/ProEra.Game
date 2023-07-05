// Decompiled with JetBrains decompiler
// Type: TB12.SfxSoundManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ClockStone;
using FootballVR;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace TB12
{
  public class SfxSoundManager : MonoBehaviour
  {
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private GameObject _ps43DAudioPrefab;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _multiSpeakerHandler = new RoutineHandle();
    private Dictionary<ESfxTypes, string> _sfxMapping;
    private static Dictionary<EAnnouncerType, string> _announcerMapping;
    private Dictionary<EVOTypes, string> _voMapping;
    private Dictionary<EStingerType, string> _stingerMapping;
    private static Dictionary<ECrowdTypes, string> _crowdMapping;
    private Dictionary<EMusicTypes, string> _musicMapping;
    private Dictionary<EOCTypes, string> _ocMapping;
    private Dictionary<ETutorialType, string> _tutorialMapping;
    private AudioObject _crowdSidelineLoop;
    private readonly RoutineHandle _playerChatterRoutine = new RoutineHandle();
    private readonly RoutineHandle _tutorialRoutine = new RoutineHandle();
    private AudioObject _musicPlaylist;
    private List<AudioObject> _playlistAdditionalSpeakers;
    private List<Transform> _speakerPositions;
    private const int k_crowdFilterMin = 2600;
    private const int k_crowdFilterMax = 22000;
    private static readonly string[] kSoundNames = new string[11]
    {
      "",
      "",
      "an-impressive",
      "an-excellent",
      "an-outstanding",
      "an-comboking",
      "an-bullseye",
      "an-dominating",
      "an-eagleeye",
      "an-unstoppable",
      "an-godlike"
    };

    private void Awake()
    {
      ScriptableSingleton<SettingsStore>.Instance.Initialize();
      this._playlistAdditionalSpeakers = new List<AudioObject>();
      this._speakerPositions = new List<Transform>();
      SingletonMonoBehaviour<AudioController>.Instance.playlistNextEvent += new Action<AudioObject>(this.FinishedPlaylistSong);
      Debug.Log((object) "_linksHandler before");
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VREvents.ThrowResult.Link<bool, float>(new Action<bool, float>(this.ThrowResultHandler)),
        EventHandle.Link<ESfxTypes>(AppSounds.SfxEvent, new Action<ESfxTypes>(this.PlaySfxHandler)),
        EventHandle.Link<ESfxTypes, Vector3>(AppSounds.Sfx3DEvent, new Action<ESfxTypes, Vector3>(this.PlaySfx3DHandler)),
        EventHandle.Link<ESfxTypes, Transform>(AppSounds.Sfx3DParentedEvent, new Action<ESfxTypes, Transform>(this.PlaySfx3DParentedHandler)),
        EventHandle.Link<AppSounds.PlayerChatterData>(AppSounds.PlayerChatterEvent, new Action<AppSounds.PlayerChatterData>(this.PlayerChatterHandler)),
        EventHandle.Link(AppSounds.StopPlayerChatterEvent, new System.Action(this.StopPlayerChatter)),
        EventHandle.Link<EVOTypes, bool>(AppSounds.VoEvent, new Action<EVOTypes, bool>(this.PlayVOHandler)),
        EventHandle.Link<EVOTypes, Transform, bool>(AppSounds.Vo3DEvent, new Action<EVOTypes, Transform, bool>(this.PlayVO3DHandler)),
        EventHandle.Link<EMusicTypes>(AppSounds.MusicEvent, new Action<EMusicTypes>(this.PlayMusicHandler)),
        EventHandle.Link<EMusicTypes>(AppSounds.MusicPlaylistEvent, new Action<EMusicTypes>(this.PlayMusicPlaylistHandler)),
        EventHandle.Link<EMusicTypes, Transform>(AppSounds.MusicPlaylist3DEvent, new Action<EMusicTypes, Transform>(this.PlayMusicPlaylistHandler)),
        EventHandle.Link<EMusicTypes, Transform[]>(AppSounds.MusicPlaylistMultiSpeakerEvent, new Action<EMusicTypes, Transform[]>(this.PlayMusicPlaylistMultiSpeakerHandler)),
        EventHandle.Link(AppSounds.StopMusic, new System.Action(this.StopMusicHandler)),
        EventHandle.Link<EOCTypes>(AppSounds.OCEvent, new Action<EOCTypes>(this.PlayOCHandler)),
        EventHandle.Link<string>(AppSounds.OCStringEvent, new Action<string>(this.PlayOCHandler)),
        EventHandle.Link<EAnnouncerType>(AppSounds.AnnouncerEvent, new Action<EAnnouncerType>(this.PlayAnnouncerHandler)),
        EventHandle.Link<EStingerType>(AppSounds.StingerEvent, new Action<EStingerType>(this.PlayStingerHandler)),
        EventHandle.Link<EPracticeSoundType, Transform>(AppSounds.PracticeSoundEvent, new Action<EPracticeSoundType, Transform>(this.PracticeSoundHandler)),
        EventHandle.Link<bool>((Variable<bool>) AppSounds.AmbienceSound, new Action<bool>(this.AmbienceSoundsHandler)),
        EventHandle.Link<ECrowdTypes, int>(AppSounds.CrowdEvent, new Action<ECrowdTypes, int>(this.PlayCrowdHandler)),
        EventHandle.Link<ECrowdTypes, Vector3>(AppSounds.Crowd3DEvent, new Action<ECrowdTypes, Vector3>(this.PlayCrowd3DHandler)),
        EventHandle.Link<ESfxTypes, int>(AppSounds.StopSfxEvent, new Action<ESfxTypes, int>(this.StopSfxHandler)),
        EventHandle.Link<EAnnouncerType, int>(AppSounds.StopAnnouncerEvent, new Action<EAnnouncerType, int>(this.StopAnnouncerHandler)),
        EventHandle.Link<ECrowdTypes, int>(AppSounds.StopCrowdEvent, new Action<ECrowdTypes, int>(this.StopCrowdHandler)),
        EventHandle.Link<EOCTypes>(AppSounds.StopOCEvent, new Action<EOCTypes>(this.StopOCHandler)),
        EventHandle.Link<EVOTypes>(AppSounds.StopVOEvent, new Action<EVOTypes>(this.StopVOHandler)),
        EventHandle.Link<float>((Variable<float>) ScriptableSingleton<SettingsStore>.Instance.SfxVolume, new Action<float>(this.SfxSettingsHandler)),
        EventHandle.Link<float>((Variable<float>) ScriptableSingleton<SettingsStore>.Instance.BgmVolume, new Action<float>(this.BgmSettingsHandler)),
        EventHandle.Link<float>((Variable<float>) ScriptableSingleton<SettingsStore>.Instance.HostVoVolume, new Action<float>(this.HostVoSettingsHandler)),
        EventHandle.Link<float>((Variable<float>) ScriptableSingleton<SettingsStore>.Instance.HostVoVolume, new Action<float>(this.StadiumSettingsHandler)),
        EventHandle.Link<float>(AppSounds.UpdateSFXVolume, new Action<float>(this.SfxSettingsHandler)),
        EventHandle.Link<float>(AppSounds.UpdateBGMVolume, new Action<float>(this.BgmSettingsHandler)),
        EventHandle.Link<float>(AppSounds.UpdateVOVolume, new Action<float>(this.HostVoSettingsHandler)),
        EventHandle.Link<float>(AppSounds.UpdateStadiumVolume, new Action<float>(this.StadiumSettingsHandler)),
        EventHandle.Link<float>(AppSounds.AmbientSettingsEvent, new Action<float>(this.AmbienceSettingsHandler)),
        EventHandle.Link<bool>(AppSounds.AmbientLowFilterEvent, new Action<bool>(this.AmbienceLowFilterHandler)),
        EventHandle.Link(AppSounds.PlaySidelineEvent, new System.Action(this.PlaySidelineSounds)),
        EventHandle.Link(AppSounds.StopSidelineEvent, new System.Action(this.StopSidelineSounds)),
        EventHandle.Link<int, System.Action>(AppSounds.TutorialEvent, new Action<int, System.Action>(this.TutorialHandler)),
        EventHandle.Link<ETutorialType>(AppSounds.TutorialFeedbackEvent, new Action<ETutorialType>(this.TutorialFeedbackHandler)),
        EventHandle.Link(VREvents.FootstepTaken, new System.Action(this.HandleFootstep))
      });
      Debug.Log((object) "_linksHandler after");
      this._sfxMapping = new Dictionary<ESfxTypes, string>()
      {
        {
          ESfxTypes.kButtonPress,
          "Button Press"
        },
        {
          ESfxTypes.kHoverOn,
          "Button Hover On"
        },
        {
          ESfxTypes.kHoverOff,
          "Button Hover Off"
        },
        {
          ESfxTypes.kWhistle,
          "Whistle"
        },
        {
          ESfxTypes.kCatchBall,
          "sfx_ballCatch"
        },
        {
          ESfxTypes.kTargetHit,
          "Practice Hit"
        },
        {
          ESfxTypes.kRoar,
          "Crowd Cheer"
        },
        {
          ESfxTypes.kCatchFail,
          "Catch Fail"
        },
        {
          ESfxTypes.kCatchSuccess,
          "Catch Success"
        },
        {
          ESfxTypes.kSacked,
          "NPC Collision"
        },
        {
          ESfxTypes.kTackle,
          "sfx_tackle"
        },
        {
          ESfxTypes.kImpactLineRush,
          "sfx_impact_lineRush"
        },
        {
          ESfxTypes.kHuddleBreak,
          "ui_huddle_break"
        },
        {
          ESfxTypes.kKick,
          "sfx_ball_kick"
        },
        {
          ESfxTypes.kPlayerChatter,
          "sideline_player_chatter"
        },
        {
          ESfxTypes.kBallBounce,
          "sfx_ballBounce"
        },
        {
          ESfxTypes.kTunnel,
          "sfx_tunnel"
        },
        {
          ESfxTypes.kRefWhistle,
          "Ref Whistle"
        },
        {
          ESfxTypes.kBodyFall,
          "sfx_bodyFall"
        },
        {
          ESfxTypes.kLineRush2D,
          "sfx_lineRush_2D"
        },
        {
          ESfxTypes.kTackle2D,
          "sfx_tackle_2D"
        },
        {
          ESfxTypes.kFieldChatterPos,
          "field_chatter_pos"
        },
        {
          ESfxTypes.kFieldChatterNeutral,
          "field_chatter_neutral"
        },
        {
          ESfxTypes.kFieldChatterNeg,
          "field_chatter_neg"
        },
        {
          ESfxTypes.kFieldChatterTD,
          "field_chatter_td"
        },
        {
          ESfxTypes.kTrophyUnlock,
          "sfx_trophy_unlock"
        },
        {
          ESfxTypes.kThrowBall,
          "sfx_pass_whoosh"
        },
        {
          ESfxTypes.kUserSackedGround,
          "sfx_player_sacked_ground"
        },
        {
          ESfxTypes.kUserSackedImpact,
          "sfx_player_sacked_impact"
        },
        {
          ESfxTypes.kTimeout,
          "sfx_ref_whistle_timeout"
        },
        {
          ESfxTypes.kQBThrow,
          "sfx_qb_throw"
        },
        {
          ESfxTypes.kQBBreathing,
          "sfx_qb_breathing"
        },
        {
          ESfxTypes.kFootstepsIndoor,
          "sfx_footsteps_indoor"
        },
        {
          ESfxTypes.kFootstepsField,
          "sfx_footsteps_field"
        },
        {
          ESfxTypes.kMiniBallHitPlayer,
          "sfx_mini_ball_hitting_player"
        },
        {
          ESfxTypes.kMiniCountdown,
          "sfx_mini_countdown"
        },
        {
          ESfxTypes.kMiniBallHitDummy,
          "sfx_mini_hit_dummy"
        },
        {
          ESfxTypes.kMiniFiringMachine,
          "sfx_mini_machine_firing"
        },
        {
          ESfxTypes.kMiniObjectSpawn,
          "sfx_mini_object_spawn"
        },
        {
          ESfxTypes.kMiniOutCallout,
          "sfx_mini_out_callout"
        },
        {
          ESfxTypes.kMiniPickupBall,
          "sfx_mini_pickup_ball"
        },
        {
          ESfxTypes.kMiniRingSuccess,
          "sfx_mini_ring_success"
        },
        {
          ESfxTypes.kMiniStartWhistle,
          "sfx_mini_start_whistle"
        },
        {
          ESfxTypes.kMiniStopWhistle,
          "sfx_mini_stop_whistle"
        },
        {
          ESfxTypes.kFTUETunnel,
          "ftue_tunnel"
        },
        {
          ESfxTypes.kSuperBowlTrophyUnlock,
          "sfx_superbowl_trophy"
        },
        {
          ESfxTypes.kCelebrationChatterFD,
          "sfx_celebration_chatter_fd"
        },
        {
          ESfxTypes.kCelebrationChatterINC,
          "sfx_celebration_chatter_inc"
        },
        {
          ESfxTypes.kCelebrationChatterSack,
          "sfx_celebration_chatter_sack"
        },
        {
          ESfxTypes.kCelebrationChatterTD,
          "sfx_celebration_chatter_td"
        },
        {
          ESfxTypes.kCelebrationChatterTurnover,
          "sfx_celebration_chatter_turnover"
        },
        {
          ESfxTypes.kCoachChatter,
          "sideline_coach_chatter"
        },
        {
          ESfxTypes.kOnboardingLoop,
          "sfx_Onboarding_Loop"
        },
        {
          ESfxTypes.kMiniThrow,
          "sfx_mini_throw"
        }
      };
      SfxSoundManager._crowdMapping = new Dictionary<ECrowdTypes, string>()
      {
        {
          ECrowdTypes.kCrowdAwwBig,
          "crowd_reactions_Aww_big"
        },
        {
          ECrowdTypes.kCrowdAwwMed,
          "crowd_reactions_Aww_med"
        },
        {
          ECrowdTypes.kCrowdAwwSmall,
          "crowd_reactions_Aww_small"
        },
        {
          ECrowdTypes.kCrowdCheerBig,
          "crowd_reactions_yeah_big"
        },
        {
          ECrowdTypes.kCrowdCheerMed,
          "crowd_reactions_yeah_med"
        },
        {
          ECrowdTypes.kCrowdCheerSmall,
          "crowd_reactions_yeah_small"
        },
        {
          ECrowdTypes.kCrowdBuildupHigh,
          "crowd_buildup_high"
        },
        {
          ECrowdTypes.kSidelineBed,
          "crowd_sideline"
        },
        {
          ECrowdTypes.kCrowdPayAttention,
          "crowd_payAttention"
        },
        {
          ECrowdTypes.kCrowdSidelineOOB,
          "crowd_sidelineOOB"
        },
        {
          ECrowdTypes.kCrowdDefenseChant,
          "crowd_chant_DEFENSE"
        },
        {
          ECrowdTypes.kCrowdBuildupPrePlay,
          "crowd_buildup_preplay"
        },
        {
          ECrowdTypes.kCrowdHomeTeamChant,
          "crowd_chant_"
        },
        {
          ECrowdTypes.kTouchdownHome,
          "crowd_home_touchdown"
        },
        {
          ECrowdTypes.kCloseGameScore,
          "crowd_close_game_score"
        },
        {
          ECrowdTypes.kCrowdCheerHuge,
          "crowd_reactions_yeah_huge"
        },
        {
          ECrowdTypes.kCrowdLongPlay,
          "crowd_long_play"
        }
      };
      SfxSoundManager._announcerMapping = new Dictionary<EAnnouncerType, string>()
      {
        {
          EAnnouncerType.k1stDownHome,
          "PA_1st_down_home"
        },
        {
          EAnnouncerType.k1stDownAway,
          "PA_1st_down_away"
        },
        {
          EAnnouncerType.kPassComplete,
          "PA_pass_complete"
        },
        {
          EAnnouncerType.kPassIncomplete,
          "PA_pass_incomplete"
        },
        {
          EAnnouncerType.kTouchdownHome,
          "PA_touchdown"
        },
        {
          EAnnouncerType.kTouchdownAway,
          "PA_touchdown"
        },
        {
          EAnnouncerType.kKickGoodHome,
          "PA_Kick_Good_Home"
        },
        {
          EAnnouncerType.kKickNoGood,
          "PA_Kick_No_Good"
        },
        {
          EAnnouncerType.kPassIntercepted,
          "PA_Pass_Intercepted"
        },
        {
          EAnnouncerType.kPenaltyDelay,
          "PA_penalty_delay"
        },
        {
          EAnnouncerType.kPuntBlocked,
          "PA_punt_blocked"
        },
        {
          EAnnouncerType.kQuarter1End,
          "PA_Q1_End"
        },
        {
          EAnnouncerType.kQuarter2End,
          "PA_Q2_End"
        },
        {
          EAnnouncerType.kQuarter3End,
          "PA_Q3_End"
        },
        {
          EAnnouncerType.kQuarter4End,
          "PA_Q4_End"
        },
        {
          EAnnouncerType.k2Minutes,
          "PA_2_min"
        },
        {
          EAnnouncerType.kYardage,
          "PA_yardage_"
        },
        {
          EAnnouncerType.kTimeout,
          "PA_Timeout"
        },
        {
          EAnnouncerType.kHype,
          "PA_hype"
        },
        {
          EAnnouncerType.kHype4thQ,
          "PA_hype_4thQ_"
        },
        {
          EAnnouncerType.kKickGoodAway,
          "PA_Kick_Good_Away"
        },
        {
          EAnnouncerType.kSafetyHome,
          "PA_safety_home"
        },
        {
          EAnnouncerType.kSafetyAway,
          "PA_safety_away"
        }
      };
      this._voMapping = new Dictionary<EVOTypes, string>()
      {
        {
          EVOTypes.kSet,
          "vo_set"
        },
        {
          EVOTypes.kHike,
          "vo_hike"
        },
        {
          EVOTypes.kFindReceiver,
          "VO-Find Receiver"
        },
        {
          EVOTypes.kHut,
          "VO-Hut"
        },
        {
          EVOTypes.kWelcome,
          "VO-Welcome"
        },
        {
          EVOTypes.kAudible,
          "vo_audible"
        },
        {
          EVOTypes.kAudible2,
          "vo_audible2"
        },
        {
          EVOTypes.kTimeout,
          "vo_timeout"
        },
        {
          EVOTypes.kThrowCallLeft,
          "vo_throwcall_left"
        },
        {
          EVOTypes.kThrowCallRight,
          "vo_throwcall_right"
        },
        {
          EVOTypes.kThrowCallMiddle,
          "vo_throwcall_middle"
        },
        {
          EVOTypes.kLamarLockerRoom,
          "vo_lamar_locker_room"
        },
        {
          EVOTypes.kLamarTunnel,
          "vo_lamar_tunnel"
        },
        {
          EVOTypes.kLamarHeroMoment1,
          "vo_lamar_hero_moment_1"
        },
        {
          EVOTypes.kLamarHeroMoment2,
          "vo_lamar_hero_moment_2"
        },
        {
          EVOTypes.kLamarHeroMoment3,
          "vo_lamar_hero_moment_3"
        },
        {
          EVOTypes.kLamarHeroMoment4,
          "vo_lamar_hero_moment_4"
        },
        {
          EVOTypes.kLamarHeroMoment5,
          "vo_lamar_hero_moment_5"
        },
        {
          EVOTypes.kLamarLockerRoomIntro,
          "vo_lamar_locker_room_intro"
        },
        {
          EVOTypes.kLamarLockerRoomArcade,
          "vo_lamar_locker_room_arcade"
        },
        {
          EVOTypes.kLamarLockerRoomLocker,
          "vo_lamar_locker_room_locker"
        },
        {
          EVOTypes.kLamarLockerRoomPostCAP,
          "vo_lamar_locker_room_postcap"
        },
        {
          EVOTypes.kLamarLockerRoomTrophy,
          "vo_lamar_locker_room_trophy"
        },
        {
          EVOTypes.kSet3D,
          "vo_set_3d"
        },
        {
          EVOTypes.kHike3D,
          "vo_hike_3d"
        },
        {
          EVOTypes.kHurryUp,
          "vo_turbo"
        },
        {
          EVOTypes.kTimeout3D,
          "vo_timeout_3d"
        }
      };
      this._stingerMapping = new Dictionary<EStingerType, string>()
      {
        {
          EStingerType.kStadium,
          "stadium_stinger"
        },
        {
          EStingerType.kStinger1,
          "Stinger 1"
        },
        {
          EStingerType.kStinger2,
          "Stinger 2"
        },
        {
          EStingerType.kStinger3,
          "Stinger 3"
        },
        {
          EStingerType.kStinger4,
          "Stinger 4"
        },
        {
          EStingerType.kStinger5,
          "Stinger 5"
        }
      };
      this._musicMapping = new Dictionary<EMusicTypes, string>()
      {
        {
          EMusicTypes.kTunnel,
          "music_tunnel"
        },
        {
          EMusicTypes.kLockerRoom,
          "LockerRoom"
        },
        {
          EMusicTypes.kMainMenu,
          "MainMenu"
        },
        {
          EMusicTypes.kTrophyRoom,
          "TrophyRoom"
        },
        {
          EMusicTypes.kOnboarding,
          "Onboarding_Loop"
        },
        {
          EMusicTypes.kPracticeInst,
          "PracticeMode"
        },
        {
          EMusicTypes.kPracticeLyrics,
          "PracticeMode_lyrics"
        },
        {
          EMusicTypes.k2MD,
          "music_2MD"
        }
      };
      this._ocMapping = new Dictionary<EOCTypes, string>()
      {
        {
          EOCTypes.kBleedClock,
          "oc_bleed_clock"
        },
        {
          EOCTypes.kDefTD,
          "oc_def_td"
        },
        {
          EOCTypes.kDivisionChamp,
          "oc_division_champ"
        },
        {
          EOCTypes.kINT,
          "oc_ints_previous"
        },
        {
          EOCTypes.kOpponentTD,
          "oc_opponent_td"
        },
        {
          EOCTypes.kPreviousTD,
          "oc_user_td_previous"
        },
        {
          EOCTypes.kRedzone30,
          "oc_redzone_30"
        },
        {
          EOCTypes.kRedzoneAmazing,
          "oc_redzone_amazing"
        },
        {
          EOCTypes.kRivalry,
          "oc_rivalry"
        },
        {
          EOCTypes.kSuperBowl,
          "oc_super_bowl"
        },
        {
          EOCTypes.kTD,
          "oc_td"
        },
        {
          EOCTypes.kTotalYards,
          "oc_total_yards"
        },
        {
          EOCTypes.kTurnover,
          "oc_turnover"
        },
        {
          EOCTypes.kVictoryFormation,
          "oc_victory_formation"
        },
        {
          EOCTypes.kWinStreak,
          "oc_win_streak"
        },
        {
          EOCTypes.kWinGame,
          "oc_win_game"
        },
        {
          EOCTypes.kYardsOutOfFormation,
          "oc_yards_out_of_formation"
        },
        {
          EOCTypes.kFG,
          "oc_sideline_fg"
        },
        {
          EOCTypes.kOpeningDrive,
          "oc_sideline_opening_drive"
        },
        {
          EOCTypes.kSidelinePassing300,
          "oc_sideline_passing_300"
        },
        {
          EOCTypes.kSidelinePassing400,
          "oc_sideline_passing_400"
        },
        {
          EOCTypes.kSidelinePassing500,
          "oc_sideline_passing_500"
        },
        {
          EOCTypes.kSidelinePuntUp1,
          "oc_sideline_punt_1_up"
        },
        {
          EOCTypes.kSidelinePuntUp2,
          "oc_sideline_punt_2_up"
        },
        {
          EOCTypes.kSidelinePuntDown1,
          "oc_sideline_punt_1_down"
        },
        {
          EOCTypes.kSidelinePuntDown2,
          "oc_sideline_punt_2_down"
        },
        {
          EOCTypes.kSidelineScoreDown1,
          "oc_sideline_score_1_down"
        },
        {
          EOCTypes.kSidelineScoreDown2,
          "oc_sideline_score_2_down"
        },
        {
          EOCTypes.kSidelineScoreDown3,
          "oc_sideline_score_3_down"
        },
        {
          EOCTypes.kSidelineScoreUp1,
          "oc_sideline_score_1_up"
        },
        {
          EOCTypes.kSidelineScoreUp2,
          "oc_sideline_score_2_up"
        },
        {
          EOCTypes.kSidelineScoreUp3,
          "oc_sideline_score_3_up"
        },
        {
          EOCTypes.kSidelineSeasonFirstINT,
          "oc_sideline_season_first_interception"
        },
        {
          EOCTypes.kSidelineSeasonFirstTD,
          "oc_sideline_season_first_touchdown"
        },
        {
          EOCTypes.kSidelineSeasonStart,
          "oc_sideline_season_start"
        },
        {
          EOCTypes.kTD2,
          "oc_td_2"
        },
        {
          EOCTypes.kTD3,
          "oc_td_3"
        },
        {
          EOCTypes.kSidelineINT,
          "oc_sideline_turnover"
        },
        {
          EOCTypes.kSidelineINT2,
          "oc_sideline_turnover_2"
        },
        {
          EOCTypes.kSidelineINT3,
          "oc_sideline_turnover_3"
        },
        {
          EOCTypes.kPocketPassingIntro,
          "OC_PocketPassing_Intro"
        },
        {
          EOCTypes.kDimeDroppingIntro,
          "OC_DimeDropping_Intro"
        },
        {
          EOCTypes.kRolloutIntro,
          "OC_RolloutEvent_Intro"
        },
        {
          EOCTypes.kRunAndShootIntro,
          "OC_RunAndShoot_Intro"
        },
        {
          EOCTypes.kResults3Stars,
          "OC_Results_3_stars"
        },
        {
          EOCTypes.kResults2Stars,
          "OC_Results_2_stars"
        },
        {
          EOCTypes.kResults1Stars,
          "OC_Results_1_stars"
        },
        {
          EOCTypes.kResults0Stars,
          "OC_Results_0_stars"
        },
        {
          EOCTypes.kResultsFail,
          "OC_Results_Fail"
        },
        {
          EOCTypes.k2MDIntro,
          "OC_2MD_Intro"
        },
        {
          EOCTypes.k2MDOutro,
          "OC_2MD_Outro"
        },
        {
          EOCTypes.k2MDHighscore,
          "OC_2MD_Outro_Highscore"
        },
        {
          EOCTypes.k2MDLowscore,
          "OC_2MD_Outro_Lowscore"
        },
        {
          EOCTypes.kHeroIntro,
          "oc_hero_intro"
        },
        {
          EOCTypes.kHalftimeWinning,
          "oc_halftime_winning"
        },
        {
          EOCTypes.kHalftimeLosing,
          "oc_halftime_losing"
        },
        {
          EOCTypes.kHalftimeTied,
          "oc_halftime_tied"
        },
        {
          EOCTypes.kTD_1pt_4min,
          "oc_touchdown_1pt_q4_4min"
        },
        {
          EOCTypes.kTD_1pt_2min,
          "oc_touchdown_1pt_q4_2min"
        },
        {
          EOCTypes.kTD_1pt_30sec,
          "oc_touchdown_1pt_q4_30sec"
        },
        {
          EOCTypes.kTD_down_1_4min,
          "oc_touchdown_down1scr_q4_4min"
        },
        {
          EOCTypes.kTD_down_1_2min,
          "oc_touchdown_down1scr_q4_2min"
        },
        {
          EOCTypes.kTD_down_1_30sec,
          "oc_touchdown_down1scr_q4_30sec"
        },
        {
          EOCTypes.kTD_down_1_3sec,
          "oc_touchdown_down1scr_q4_3sec"
        },
        {
          EOCTypes.kTD_down_2_4min,
          "oc_touchdown_down2scr_q4_4min"
        },
        {
          EOCTypes.kTD_down_2_3min,
          "oc_touchdown_down2scr_q4_3min"
        },
        {
          EOCTypes.kTD_tie_4min,
          "oc_touchdown_tie_q4_4min"
        },
        {
          EOCTypes.kTD_tie_2min,
          "oc_touchdown_tie_q4_2min"
        },
        {
          EOCTypes.kTD_tie_30sec,
          "oc_touchdown_tie_q4_30sec"
        },
        {
          EOCTypes.kTD_up_1_4min,
          "oc_touchdown_up1scr_q4_4min"
        },
        {
          EOCTypes.kTD_up_1_2min,
          "oc_touchdown_up1scr_q4_2min"
        },
        {
          EOCTypes.kTD_up_1_30sec,
          "oc_touchdown_up1scr_q4_30sec"
        },
        {
          EOCTypes.kTD_up_2_4min,
          "oc_touchdown_up2scr_q4_4min"
        },
        {
          EOCTypes.kTD_up_2_2min,
          "oc_touchdown_up2scr_q4_2min"
        },
        {
          EOCTypes.kTD_up_2_30sec,
          "oc_touchdown_up2scr_q4_30sec"
        },
        {
          EOCTypes.kTD_up_3_3min,
          "oc_touchdown_up3scr_q4_3min"
        },
        {
          EOCTypes.kTurnover_Q2_4min,
          "oc_turnover_q2_4min"
        },
        {
          EOCTypes.kTurnover_Q2_30sec_Above60yards,
          "oc_turnover_q2_30sec_>60yards"
        },
        {
          EOCTypes.kTurnover_Q2_30sec_Below60yards,
          "oc_turnover_q2_30sec_<60yards"
        },
        {
          EOCTypes.kTurnover_Q4_1scr_3min,
          "oc_turnover_1scr_q4_3min"
        },
        {
          EOCTypes.kTurnover_Q4_1scr_2min,
          "oc_turnover_1scr_q4_2min"
        }
      };
      this._tutorialMapping = new Dictionary<ETutorialType, string>()
      {
        {
          ETutorialType.kSelectedPlay,
          "tutorial_selected_play"
        },
        {
          ETutorialType.kPlaySuccess,
          "tutorial_play_success"
        },
        {
          ETutorialType.kPlayFail,
          "tutorial_play_fail"
        }
      };
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      SingletonMonoBehaviour<AudioController>.Instance.playlistNextEvent -= new Action<AudioObject>(this.FinishedPlaylistSong);
    }

    private void ThrowResultHandler(bool hit, float distance)
    {
      if (!(bool) ScriptableSingleton<AppSettings>.Instance.Announcer)
        return;
      if (hit)
      {
        if ((int) this._gameplayStore.BallsThrown == 1)
          AudioController.Play("an-firstblood", 1f, 0.5f);
        else if (this._gameplayStore.ComboModifier < 11)
        {
          AudioController.Play(SfxSoundManager.kSoundNames[this._gameplayStore.ComboModifier], 1f, 0.5f);
        }
        else
        {
          int index = UnityEngine.Random.Range(2, 10);
          AudioController.Play(SfxSoundManager.kSoundNames[index], 1f, 0.5f);
        }
      }
      else
        AudioController.Play("an-denied");
    }

    private void HostVoSettingsHandler(float volume) => AudioController.SetCategoryVolume("MASTER_VO", volume);

    private void BgmSettingsHandler(float volume) => AudioController.SetCategoryVolume("MASTER_MUSIC", volume);

    private void SfxSettingsHandler(float volume) => AudioController.SetCategoryVolume("MASTER_SFX", volume);

    private void StadiumSettingsHandler(float volume) => AudioController.SetCategoryVolume("MASTER_STADIUM", volume);

    private void PlayMusicHandler(EMusicTypes value)
    {
      if (this._musicMapping.ContainsKey(value))
        AudioController.PlayMusic(this._musicMapping[value]);
      else
        Debug.LogWarning((object) ("Attempted to play Music that was not in the _musicMapping; key[" + value.ToString() + "]"));
    }

    private void PlayMusicPlaylistHandler(EMusicTypes value)
    {
      if (AudioController.IsPlaylistPlaying())
        AudioController.SetCurrentMusicPlaylist(this._musicMapping[value]);
      else
        this._musicPlaylist = AudioController.PlayMusicPlaylist(this._musicMapping[value]);
    }

    private void PlayMusicPlaylistHandler(EMusicTypes value, Transform position)
    {
      this._musicPlaylist = AudioController.PlayMusicPlaylist(this._musicMapping[value]);
      if ((UnityEngine.Object) this._musicPlaylist != (UnityEngine.Object) null)
        this._musicPlaylist.transform.position = position.position;
      if (this._speakerPositions == null)
        return;
      this._speakerPositions.Clear();
      this._speakerPositions.Add(position);
    }

    private void PlayMusicPlaylistMultiSpeakerHandler(EMusicTypes value, Transform[] position)
    {
      Debug.Log((object) nameof (PlayMusicPlaylistMultiSpeakerHandler));
      this._speakerPositions.Clear();
      this.StopAdditionalSpeakers();
      AudioClip nextSoundTrack = StadiumSoundSystemManager.GetNextSoundTrack();
      double dspTime = AudioSettings.dspTime;
      for (int index = 0; index < position.Length; ++index)
      {
        AudioSource component = position[index].GetComponent<AudioSource>();
        component.clip = nextSoundTrack;
        component.PlayScheduled(dspTime);
      }
      this._multiSpeakerHandler.Run(this.WaitToPlayNextMultiSpeakerPlaylist(nextSoundTrack.length, position));
    }

    private IEnumerator WaitToPlayNextMultiSpeakerPlaylist(float time, Transform[] position)
    {
      yield return (object) new WaitForSeconds(time * Time.timeScale);
      this.PlayMusicPlaylistMultiSpeakerHandler(EMusicTypes.kLockerRoom, position);
    }

    private void FinishedPlaylistSong(AudioObject newSong)
    {
      this._musicPlaylist = newSong;
      this._musicPlaylist.volume = 0.0f;
      this.StopAdditionalSpeakers();
      for (int index = 0; index < this._speakerPositions.Count; ++index)
      {
        AudioObject audioObject = AudioController.Play(this._musicPlaylist.audioID);
        audioObject.transform.position = this._speakerPositions[index].position;
        this._playlistAdditionalSpeakers.Add(audioObject);
      }
    }

    private void StopMusicHandler()
    {
      AudioController.StopMusic(1f);
      if ((UnityEngine.Object) this._musicPlaylist != (UnityEngine.Object) null)
      {
        this._musicPlaylist = (AudioObject) null;
        this.StopAdditionalSpeakers();
      }
      if (this._multiSpeakerHandler == null)
        return;
      this._multiSpeakerHandler.Stop();
    }

    private void StopAdditionalSpeakers()
    {
      if (this._playlistAdditionalSpeakers.Count <= 0)
        return;
      for (int index = 0; index < this._playlistAdditionalSpeakers.Count; ++index)
      {
        if ((UnityEngine.Object) this._playlistAdditionalSpeakers[index] != (UnityEngine.Object) null)
          this._playlistAdditionalSpeakers[index].Stop();
      }
      this._playlistAdditionalSpeakers.Clear();
    }

    private void StopSfxHandler(ESfxTypes id, int fadeOut = 0)
    {
      if (this._sfxMapping.ContainsKey(id))
        AudioController.Stop(this._sfxMapping[id], (float) fadeOut);
      else
        Debug.LogWarning((object) ("Attempted to stop a SFX that was not in the _sfxMapping; key[" + id.ToString() + "]"));
    }

    private void StopAnnouncerHandler(EAnnouncerType id, int fadeOut = 0)
    {
      if (SfxSoundManager._announcerMapping.ContainsKey(id))
        AudioController.Stop(SfxSoundManager._announcerMapping[id], (float) fadeOut);
      else
        Debug.LogWarning((object) ("Attempted to stop an Announcer that was not in the _announcerMapping; key[" + id.ToString() + "]"));
    }

    private void StopCrowdHandler(ECrowdTypes id, int fadeOut = 0)
    {
      if (SfxSoundManager._crowdMapping.ContainsKey(id))
        AudioController.Stop(SfxSoundManager._crowdMapping[id], (float) fadeOut);
      else
        Debug.LogWarning((object) ("Attempted to stop a crowd that was not in the _crowdMapping; key[" + id.ToString() + "]"));
    }

    private void StopOCHandler(EOCTypes id)
    {
      if (this._ocMapping.ContainsKey(id))
        AudioController.Stop(this._ocMapping[id]);
      else
        Debug.LogWarning((object) ("Attempted to stop an OC that was not in the _ocMapping; key[" + id.ToString() + "]"));
    }

    private void StopVOHandler(EVOTypes id)
    {
      if (this._voMapping.ContainsKey(id))
        AudioController.Stop(this._voMapping[id]);
      else
        Debug.LogWarning((object) ("Attempted to stop an VO that was not in the _voMapping; key[" + id.ToString() + "]"));
    }

    private void PlayAnnouncerHandler(EAnnouncerType id)
    {
      if (SfxSoundManager._announcerMapping.ContainsKey(id))
      {
        string audioID = SfxSoundManager._announcerMapping[id];
        switch (id)
        {
          case EAnnouncerType.k1stDownHome:
            audioID = "PA_1st_down_" + (PersistentData.userIsHome ? PersistentData.GetUserTeamIndex() : PersistentData.GetCompTeamIndex()).ToString();
            break;
          case EAnnouncerType.kTouchdownHome:
            audioID = "PA_touchdown_home_" + (PersistentData.userIsHome ? PersistentData.GetUserTeamIndex() : PersistentData.GetCompTeamIndex()).ToString();
            break;
          case EAnnouncerType.kTouchdownAway:
            audioID = "PA_touchdown_away_" + (PersistentData.userIsHome ? PersistentData.GetCompTeamIndex() : PersistentData.GetUserTeamIndex()).ToString();
            break;
          case EAnnouncerType.kYardage:
            audioID += MatchManager.instance.LastPlayYardsGained?.ToString();
            break;
          case EAnnouncerType.kHype:
            audioID = "PA_hype_" + (PersistentData.userIsHome ? PersistentData.GetUserTeamIndex() : PersistentData.GetCompTeamIndex()).ToString();
            break;
          case EAnnouncerType.kHype4thQ:
            audioID = "PA_hype_4thQ_" + (PersistentData.userIsHome ? PersistentData.GetUserTeamIndex() : PersistentData.GetCompTeamIndex()).ToString();
            break;
        }
        List<AudioObject> objectsInCategory = AudioController.GetPlayingAudioObjectsInCategory("Announcer");
        Debug.Log((object) ("PlayAnnouncerHandler: categorySounds[" + objectsInCategory.Count.ToString() + "] mapping[" + audioID + "]"));
        if (objectsInCategory.Count == 0)
          AudioController.Play(audioID);
        else
          AudioController.PlayAfter(audioID, objectsInCategory[objectsInCategory.Count - 1]);
      }
      else
        Debug.LogWarning((object) ("Attempted to play an Announcer that was not in the _announcerMapping; key[" + id.ToString() + "]"));
    }

    private void PlaySfxHandler(ESfxTypes sfx)
    {
      if (this._sfxMapping.ContainsKey(sfx))
        AudioController.Play(this._sfxMapping[sfx]);
      else
        Debug.LogWarning((object) ("Attempted to play a SFX that was not in the _sfxMapping; key[" + sfx.ToString() + "]"));
    }

    private void PlayCrowdHandler(ECrowdTypes sfx, int delay)
    {
      if (SfxSoundManager._crowdMapping.ContainsKey(sfx))
      {
        string audioID = SfxSoundManager._crowdMapping[sfx];
        if (sfx == ECrowdTypes.kCrowdHomeTeamChant)
          audioID += (PersistentData.userIsHome ? PersistentData.GetUserTeamIndex() : PersistentData.GetCompTeamIndex()).ToString();
        AudioController.Play(audioID, 1f, (float) delay);
      }
      else
        Debug.LogWarning((object) ("Attempted to play a CrowdSFX that was not in the _crowdMapping; key[" + sfx.ToString() + "]"));
    }

    private void PlayCrowd3DHandler(ECrowdTypes sfx, Vector3 pos)
    {
      if (SfxSoundManager._crowdMapping.ContainsKey(sfx))
        AudioController.Play(SfxSoundManager._crowdMapping[sfx], pos);
      else
        Debug.LogWarning((object) ("Attempted to play a Crowd3D that was not in the _crowdMapping; key[" + sfx.ToString() + "]"));
    }

    private void PlaySfx3DHandler(ESfxTypes sfx, Vector3 pos)
    {
      if (this._sfxMapping.ContainsKey(sfx))
        AudioController.Play(this._sfxMapping[sfx], pos);
      else
        Debug.LogWarning((object) ("Attempted to play a SFX3D that was not in the _sfxMapping; key[" + sfx.ToString() + "]"));
    }

    private void PlaySfx3DParentedHandler(ESfxTypes sfx, Transform parent)
    {
      if (this._sfxMapping.ContainsKey(sfx))
        AudioController.Play(this._sfxMapping[sfx], parent);
      else
        Debug.LogWarning((object) ("Attempted to play a SFX3D that was not in the _sfxMapping; key[" + sfx.ToString() + "]"));
    }

    private void PlayVOHandler(EVOTypes soundId, bool male)
    {
      if (this._voMapping.ContainsKey(soundId))
      {
        string audioID = this._voMapping[soundId];
        if (!male)
          audioID += "_female";
        AudioController.Play(audioID);
      }
      else
        Debug.LogWarning((object) ("Attempted to play a VO that was not in the _voMapping; key[" + soundId.ToString() + "]"));
    }

    private void PlayVO3DHandler(EVOTypes soundId, Transform target, bool male)
    {
      if (this._voMapping.ContainsKey(soundId))
      {
        string audioID = this._voMapping[soundId];
        if (!male)
          audioID += "_female";
        AudioController.Play(audioID, target);
      }
      else
        Debug.LogWarning((object) ("Attempted to play a VO that was not in the _voMapping; key[" + soundId.ToString() + "]"));
    }

    private void PlayOCHandler(EOCTypes id)
    {
      if (this._ocMapping.ContainsKey(id))
      {
        List<AudioObject> objectsInCategory = AudioController.GetPlayingAudioObjectsInCategory("OC");
        Debug.Log((object) ("PlayOCHandler EOCTypes: categorySounds.Count" + objectsInCategory.Count.ToString()));
        if (objectsInCategory.Count == 0)
          AudioController.Play(this._ocMapping[id]);
        else
          AudioController.PlayAfter(this._ocMapping[id], objectsInCategory[objectsInCategory.Count - 1]);
      }
      else
        Debug.LogWarning((object) ("Attempted to play a OC that was not in the _ocMapping; key[" + id.ToString() + "]"));
    }

    private void PlayOCHandler(string id)
    {
      List<AudioObject> objectsInCategory = AudioController.GetPlayingAudioObjectsInCategory("OC");
      Debug.Log((object) ("PlayOCHandler string: categorySounds.Count" + objectsInCategory.Count.ToString()));
      if (objectsInCategory.Count == 0)
      {
        if (!((UnityEngine.Object) AudioController.Play(id) == (UnityEngine.Object) null))
          return;
        AudioController.Play("oc_missing_play");
      }
      else
      {
        if (!((UnityEngine.Object) AudioController.PlayAfter(id, objectsInCategory[objectsInCategory.Count - 1]) == (UnityEngine.Object) null))
          return;
        AudioController.PlayAfter("oc_missing_play", objectsInCategory[objectsInCategory.Count - 1]);
      }
    }

    private void PlayStingerHandler(EStingerType stingerId)
    {
      if (this._stingerMapping.ContainsKey(stingerId))
        AudioController.Play(this._stingerMapping[stingerId]);
      else
        Debug.LogWarning((object) ("Attempted to play a Stinger that was not in the _stingerMapping; key[" + stingerId.ToString() + "]"));
    }

    private void PracticeSoundHandler(EPracticeSoundType soundId, Transform target) => AudioController.Play(string.Format("Practice {0}", (object) (int) soundId), target);

    private void AmbienceSoundsHandler(bool state)
    {
      if (state)
        AudioController.PlayAmbienceSound("Crowd");
      else
        AudioController.StopAmbienceSound(0.5f);
    }

    private void AmbienceSettingsHandler(float volume)
    {
      AudioObject currentAmbienceSound = AudioController.GetCurrentAmbienceSound();
      if (!((UnityEngine.Object) currentAmbienceSound != (UnityEngine.Object) null))
        return;
      currentAmbienceSound.volume = volume;
    }

    private void AmbienceLowFilterHandler(bool fadeIn)
    {
    }

    private void PlaySidelineSounds()
    {
      Vector3 position = PersistentSingleton<GamePlayerController>.Instance.position;
      if ((double) position.x > 0.0)
        position.x += 20f;
      else
        position.x -= 20f;
      if ((UnityEngine.Object) this._crowdSidelineLoop == (UnityEngine.Object) null)
        this._crowdSidelineLoop = AudioController.Play(SfxSoundManager._crowdMapping[ECrowdTypes.kSidelineBed], position);
      else
        this._crowdSidelineLoop.transform.position = position;
    }

    private void StopSidelineSounds()
    {
      if (!((UnityEngine.Object) this._crowdSidelineLoop != (UnityEngine.Object) null))
        return;
      this._crowdSidelineLoop.Stop();
      this._crowdSidelineLoop = (AudioObject) null;
    }

    public static bool IsPlayingCrowd(ECrowdTypes type) => AudioController.IsPlaying(SfxSoundManager._crowdMapping[type]);

    public static bool IsPlayingAnnouncer(EAnnouncerType type) => AudioController.IsPlaying(SfxSoundManager._announcerMapping[type]);

    private void PlayerChatterHandler(AppSounds.PlayerChatterData data)
    {
      if (this._sfxMapping.ContainsKey(data.sfx))
      {
        bool flag = data.callback != null;
        AudioObject audioObject = !(bool) (UnityEngine.Object) data.parent ? AudioController.Play(this._sfxMapping[data.sfx], data.offset, (Transform) null, 2f, flag ? 0.0f : data.delay) : AudioController.Play(this._sfxMapping[data.sfx], data.parent, 2f, flag ? 0.0f : data.delay);
        if (!flag)
          return;
        this._playerChatterRoutine.Run(this.WaitForPlayerChatter(data.delay + ((UnityEngine.Object) audioObject != (UnityEngine.Object) null ? audioObject.clipLength : 3f), data.callback));
      }
      else
        Debug.LogWarning((object) ("Attempted to play a PlayerChatter that was not in the _sfxMapping; key[" + data.sfx.ToString() + "]"));
    }

    private IEnumerator WaitForPlayerChatter(float time, System.Action callback)
    {
      yield return (object) new WaitForSeconds(time);
      System.Action action = callback;
      if (action != null)
        action();
    }

    private void StopPlayerChatter() => this._playerChatterRoutine.Stop();

    private void TutorialHandler(int step, System.Action callback)
    {
      this._tutorialRoutine.Stop();
      AudioObject audioObject = AudioController.Play("tutorial_" + step.ToString());
      if (!((UnityEngine.Object) audioObject != (UnityEngine.Object) null))
        return;
      this._tutorialRoutine.Run(this.WaitForPlayerChatter(audioObject.clipLength, callback));
    }

    private void TutorialFeedbackHandler(ETutorialType type)
    {
      if (this._tutorialMapping.ContainsKey(type))
      {
        this._tutorialRoutine.Stop();
        AudioController.Play(this._tutorialMapping[type]);
      }
      else
        Debug.LogWarning((object) ("Attempted to play a tutorial that was not in the _tutorialMapping; key[" + type.ToString() + "]"));
    }

    private void HandleFootstep()
    {
      if ((bool) GameSettings.PlayerOnField)
        AudioController.Play(this._sfxMapping[ESfxTypes.kFootstepsField], PersistentSingleton<GamePlayerController>.Instance.position);
      else
        AudioController.Play(this._sfxMapping[ESfxTypes.kFootstepsIndoor], PersistentSingleton<GamePlayerController>.Instance.position);
    }
  }
}
