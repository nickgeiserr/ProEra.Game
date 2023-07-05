// Decompiled with JetBrains decompiler
// Type: AnnouncerAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using Framework;
using ProEra.Game;
using System.Collections;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class AnnouncerAudio : MonoBehaviour
{
  [SerializeField]
  private Playbook _playbookP1;
  [SerializeField]
  private Playbook _playbookP2;
  private string rootFolder = "announceraudio/";
  private CommentaryManager commentary;
  public bool isLoaded;
  private AudioPath activePlay_blitz;
  private AudioPath activePlay_compPassGeneric;
  private AudioPath activePlay_diving;
  private AudioPath activePlay_drawPlay;
  private AudioPath activePlay_fieldGoalExtraPoint;
  private AudioPath activePlay_fumble;
  private AudioPath activePlay_interception;
  private AudioPath activePlay_kickoffEndzone;
  private AudioPath activePlay_kickoffFielded;
  private AudioPath activePlay_kickoffOnsideNotRec;
  private AudioPath activePlay_kickoffOnsideRec;
  private AudioPath activePlay_kickoffGeneric;
  private AudioPath activePlay_kickoffLeft;
  private AudioPath activePlay_kickoffShort;
  private AudioPath activePlay_kickoffSquib;
  private AudioPath activePlay_kickoffStartOfGame;
  private AudioPath activePlay_optionQBPitch;
  private AudioPath activePlay_optionStartPlay;
  private AudioPath activePlay_playAction;
  private AudioPath activePlay_puntFielded;
  private AudioPath activePlay_puntIntoEndzone;
  private AudioPath activePlay_puntOutOfBounds;
  private AudioPath activePlay_puntsBall;
  private AudioPath activePlay_QBDroppingBack;
  private AudioPath activePlay_QBHandoffGeneric;
  private AudioPath activePlay_QBHandoffInside;
  private AudioPath activePlay_QBHandoffLeft;
  private AudioPath activePlay_QBHandoffOutside;
  private AudioPath activePlay_QBHandoffRight;
  private AudioPath activePlay_QBHandoffStrong;
  private AudioPath activePlay_QBHandoffWeak;
  private AudioPath activePlay_QBInPocket;
  private AudioPath activePlay_QBScrambling;
  private AudioPath activePlay_QBTossGeneric;
  private AudioPath activePlay_QBTossLeft;
  private AudioPath activePlay_QBTossRight;
  private AudioPath activePlay_QBTossStrong;
  private AudioPath activePlay_QBTossWeak;
  private AudioPath activePlay_TackleBroken;
  private AudioPath activePlay_ThrowEndzoneLeft;
  private AudioPath activePlay_ThrowCenterGeneric;
  private AudioPath activePlay_ThrowDeepCenter;
  private AudioPath activePlay_ThrowDeepEndzone;
  private AudioPath activePlay_ThrowDeepGeneric;
  private AudioPath activePlay_ThrowDeepLeft;
  private AudioPath activePlay_ThrowDeepRight;
  private AudioPath activePlay_ThrowDeepSideline;
  private AudioPath activePlay_ThrowEndzoneCenter;
  private AudioPath activePlay_ThrowEndzoneGeneric;
  private AudioPath activePlay_ThrowEndzoneRight;
  private AudioPath activePlay_ThrowFirstDown;
  private AudioPath activePlay_ThrowLeftGeneric;
  private AudioPath activePlay_ThrowRightGeneric;
  private AudioPath activePlay_ThrowShortCenter;
  private AudioPath activePlay_ThrowShortGeneric;
  private AudioPath activePlay_ThrowLeftFlat;
  private AudioPath activePlay_ThrowRightFlat;
  private AudioPath activePlay_ThrowShortSideline;
  private AudioPath activePlay_ThrowSidelineGeneric;
  private AudioPath activePlay_ThrowTypeBullet;
  private AudioPath activePlay_ThrowTypeDeep;
  private AudioPath activePlay_ThrowTypeGeneric;
  private AudioPath activePlay_ThrowTypeLob;
  private AudioPath activePlay_ThrowTypeScreen;
  private AudioPath activePlay_kickoffOutOfBounds;
  private AudioPath activePlay_kickoffRight;
  private AudioPath afterCommercial;
  private AudioPath beforeCommercial;
  private AudioPath beforePlay_audible;
  private AudioPath beforePlay_doubleTESet;
  private AudioPath beforePlay_extraPointSelect;
  private AudioPath beforePlay_FGSelectGeneric;
  private AudioPath beforePlay_FGSelectLong;
  private AudioPath beforePlay_FGSelectShort;
  private AudioPath beforePlay_firstDownGeneric;
  private AudioPath beforePlay_FirstDownGoal;
  private AudioPath beforePlay_firstPlayOfDrive;
  private AudioPath beforePlay_fiveDBSet;
  private AudioPath beforePlay_fiveRecSet;
  private AudioPath beforePlay_fourRecSet;
  private AudioPath beforePlay_fourthDownGeneric;
  private AudioPath beforePlay_fourthDownGoal;
  private AudioPath beforePlay_fourthDownLong;
  private AudioPath beforePlay_fourthDownShort;
  private AudioPath beforePlay_hotRoute;
  private AudioPath beforePlay_kickoffSelect;
  private AudioPath beforePlay_motionGeneric;
  private AudioPath beforePlay_motionLeft;
  private AudioPath beforePlay_motionRight;
  private AudioPath beforePlay_oneBackSet;
  private AudioPath beforePlay_onsideKickSelect;
  private AudioPath beforePlay_puntSelect;
  private AudioPath beforePlay_secondDownGeneric;
  private AudioPath beforePlay_secondDownGoal;
  private AudioPath beforePlay_secondDownLong;
  private AudioPath beforePlay_secondDownShort;
  private AudioPath beforePlay_shiftingDBBack;
  private AudioPath beforePlay_shiftingDBUp;
  private AudioPath beforePlay_shiftingDLine;
  private AudioPath beforePlay_shiftingLB;
  private AudioPath beforePlay_shotgunSet;
  private AudioPath beforePlay_sixDBSet;
  private AudioPath beforePlay_TELeft;
  private AudioPath beforePlay_TERight;
  private AudioPath beforePlay_thirdDownGeneric;
  private AudioPath beforePlay_thirdDownGoal;
  private AudioPath beforePlay_thirdDownLong;
  private AudioPath beforePlay_thirdDownShort;
  private AudioPath beforePlay_threeBackSet;
  private AudioPath beforePlay_threeReceiverSet;
  private AudioPath beforePlay_twoBackSet;
  private AudioPath beforePlay_twoPointSelect;
  private AudioPath beforePlay_twoReceiverSet;
  private AudioPath beforePlay_underCenterSet;
  private AudioPath beforePlay_playOfTheDrive;
  private AudioPath calldownToReporter;
  private AudioPath callingTimeoutFirst;
  private AudioPath callingTimeoutSecond;
  private AudioPath callingTimeoutThird;
  private AudioPath callingTimeoutGeneric;
  private AudioPath endGameFinalRemarks;
  private AudioPath endGameGenericScore;
  private AudioPath endGameOutro;
  private AudioPath endGameSeasonGameScore;
  private AudioPath endOfQuarter1;
  private AudioPath endOfQuarter2;
  private AudioPath endOfQuarter3;
  private AudioPath endOfQuarter4;
  private AudioPath endOfQuarterOneTeamWinning;
  private AudioPath endOfQuarterTied;
  private AudioPath endOfQuarterScoreZero;
  private AudioPath injury;
  private AudioPath jokeResponses;
  private AudioPath misc_conjunctions;
  private AudioPath misc_nouns;
  private AudioPath misc_phrases;
  private AudioPath misc_prepositions;
  private AudioPath misc_homeTeam_prefix;
  private AudioPath misc_homeTeam_postfix;
  private AudioPath misc_awayTeam_prefix;
  private AudioPath misc_awayTeam_postfix;
  private AudioPath misc_offense_prefix;
  private AudioPath misc_offense_postfix;
  private AudioPath misc_defense_prefix;
  private AudioPath misc_defense_postfix;
  private AudioPath numbers_1To99_prefix;
  private AudioPath numbers_1To99_postfix;
  private AudioPath numbers_1To99_withWord;
  private AudioPath numbers_hundreds;
  private AudioPath numbers_misc;
  private AudioPath numbers_ordinal;
  private AudioPath numbers_thousands;
  private AudioPath penalty_accepting;
  private AudioPath penalty_afterPlay;
  private AudioPath penalty_declining;
  private AudioPath penalty_flagThrown;
  private AudioPath penalty_playEndAddition;
  private AudioPath playEnd_inc_deflected;
  private AudioPath playEnd_inc_dropped;
  private AudioPath playEnd_inc_generic;
  private AudioPath playEnd_madeFG_generic;
  private AudioPath playEnd_madeFG_nearlyMissed;
  private AudioPath playEnd_madeExtraPoint;
  private AudioPath playEnd_madeTwoPoint_Pass;
  private AudioPath playEnd_madeTwoPoint_Run;
  private AudioPath playEnd_missedFG_generic;
  private AudioPath playEnd_missedFG_short;
  private AudioPath playEnd_missedFG_wide;
  private AudioPath playEnd_missedFG_nearlyMade;
  private AudioPath playEnd_addition_failedTwoPoint;
  private AudioPath playEnd_addition_failedFourthDown;
  private AudioPath playEnd_addition_firstDown;
  private AudioPath playEnd_addition_fourthDown;
  private AudioPath playEnd_addition_genericGain;
  private AudioPath playEnd_addition_genericLoss;
  private AudioPath playEnd_addition_secondDown;
  private AudioPath playEnd_addition_shortGain;
  private AudioPath playEnd_addition_shortLoss;
  private AudioPath playEnd_addition_thirdDown;
  private AudioPath playEnd_QBSlide;
  private AudioPath playEnd_runOutOfBounds;
  private AudioPath playEnd_tackle_afterKick;
  private AudioPath playEnd_tackle_generic;
  private AudioPath playEnd_tackle_longGainRun;
  private AudioPath playEnd_tackle_multipleSacks;
  private AudioPath playEnd_tackle_sack;
  private AudioPath playEnd_tackle_safety;
  private AudioPath playEnd_tackle_shortGainRun;
  private AudioPath playEnd_touchdown_diving;
  private AudioPath playEnd_touchdown_generic;
  private AudioPath playEnd_touchdown_passing;
  private AudioPath playEnd_touchdown_running;
  private Dictionary<string, AudioClip> lastNames;
  private AudioPath positions_C_prefix;
  private AudioPath positions_CB_prefix;
  private AudioPath positions_DE_prefix;
  private AudioPath positions_DL_prefix;
  private AudioPath positions_DT_prefix;
  private AudioPath positions_FB_prefix;
  private AudioPath positions_FS_prefix;
  private AudioPath positions_ILB_prefix;
  private AudioPath positions_K_prefix;
  private AudioPath positions_LB_prefix;
  private AudioPath positions_LG_prefix;
  private AudioPath positions_LT_prefix;
  private AudioPath positions_MLB_prefix;
  private AudioPath positions_NT_prefix;
  private AudioPath positions_OL_prefix;
  private AudioPath positions_OLB_prefix;
  private AudioPath positions_P_prefix;
  private AudioPath positions_QB_prefix;
  private AudioPath positions_RB_prefix;
  private AudioPath positions_RG_prefix;
  private AudioPath positions_RT_prefix;
  private AudioPath positions_SLT_prefix;
  private AudioPath positions_SS_prefix;
  private AudioPath positions_TE_prefix;
  private AudioPath positions_WR_prefix;
  private AudioPath positions_DB_prefix;
  private AudioPath positions_RET_prefix;
  private AudioPath positions_C_postfix;
  private AudioPath positions_CB_postfix;
  private AudioPath positions_DE_postfix;
  private AudioPath positions_DL_postfix;
  private AudioPath positions_DT_postfix;
  private AudioPath positions_FB_postfix;
  private AudioPath positions_FS_postfix;
  private AudioPath positions_ILB_postfix;
  private AudioPath positions_K_postfix;
  private AudioPath positions_LB_postfix;
  private AudioPath positions_LG_postfix;
  private AudioPath positions_LT_postfix;
  private AudioPath positions_MLB_postfix;
  private AudioPath positions_NT_postfix;
  private AudioPath positions_OL_postfix;
  private AudioPath positions_OLB_postfix;
  private AudioPath positions_P_postfix;
  private AudioPath positions_QB_postfix;
  private AudioPath positions_RB_postfix;
  private AudioPath positions_RG_postfix;
  private AudioPath positions_RT_postfix;
  private AudioPath positions_SLT_postfix;
  private AudioPath positions_SS_postfix;
  private AudioPath positions_TE_postfix;
  private AudioPath positions_WR_postfix;
  private AudioPath positions_DB_postfix;
  private AudioPath positions_RET_postfix;
  private AudioPath pregame_generic;
  private AudioPath pregame_dayClear;
  private AudioPath pregame_dayRain;
  private AudioPath pregame_daySnow;
  private AudioPath pregame_nightClear;
  private AudioPath pregame_nightRain;
  private AudioPath pregame_nightSnow;
  private AudioPath pregame_announcerIntro;
  private AudioPath pregame_responseToAnalyst;
  private AudioPath pregame_final_generic;
  private AudioPath pregame_final_snowRain;
  private AudioPath pregame_final_closeMatch;
  private AudioPath pregame_final_starPlayer;
  private AudioPath pregame_final_seasonGame;
  private AudioPath pregame_final_playoffs;
  private AudioPath pregame_final_championship;
  private AudioPath responseToReporter;
  private AudioPath scoreUpdate_ExtendsLead;
  private AudioPath scoreUpdate_FirstScore;
  private AudioPath scoreUpdate_StillLosing;
  private AudioPath scoreUpdate_TakesLead;
  private AudioPath stats_beginning;
  private AudioPath stats_defensiveStats;
  private AudioPath stats_ending;
  private AudioPath stats_offensiveStats;
  private AudioPath stats_teamStats;
  private AudioPath homeTeamCityPrefix;
  private AudioPath awayTeamCityPrefix;
  private AudioPath homeTeamCityPostfix;
  private AudioPath awayTeamCityPostfix;
  public AudioPath stadiumLocation;
  private AudioPath homeTeamNamePrefix;
  private AudioPath awayTeamNamePrefix;
  private AudioPath homeTeamNamePostfix;
  private AudioPath awayTeamNamePostfix;
  private bool playedFirstKickoff;
  private bool playedCommentOnPass;
  private bool playedFirstScoreUpdate;
  private bool playedCommentOnShift;
  public PlayerAI lastPlayerTarget;
  public bool playerReferenced;
  private WaitForSeconds _deflectedPassDelay = new WaitForSeconds(0.5f);

  public void LoadAudioPaths()
  {
    if (this.isLoaded)
      return;
    this.isLoaded = true;
    this.commentary = AudioManager.self.commentary;
    string str1 = "ACTIVE_PLAY_DESCRIPTION/";
    this.activePlay_blitz = new AudioPath(this.rootFolder + str1 + "BLITZ");
    this.activePlay_compPassGeneric = new AudioPath(this.rootFolder + str1 + "COMPLETED_PASS_GENERIC");
    this.activePlay_diving = new AudioPath(this.rootFolder + str1 + "DIVING");
    this.activePlay_drawPlay = new AudioPath(this.rootFolder + str1 + "DRAW_PLAYS");
    this.activePlay_fieldGoalExtraPoint = new AudioPath(this.rootFolder + str1 + "FIELD_GOAL_EXTRA_POINT");
    this.activePlay_fumble = new AudioPath(this.rootFolder + str1 + "FUMBLE");
    this.activePlay_interception = new AudioPath(this.rootFolder + str1 + "INTERCEPTION");
    this.activePlay_kickoffEndzone = new AudioPath(this.rootFolder + str1 + "KICKOFF_END_ZONE");
    this.activePlay_kickoffFielded = new AudioPath(this.rootFolder + str1 + "KICKOFF_FIELDED");
    this.activePlay_kickoffGeneric = new AudioPath(this.rootFolder + str1 + "KICKOFF_GENERIC");
    this.activePlay_kickoffLeft = new AudioPath(this.rootFolder + str1 + "KICKOFF_LEFT");
    this.activePlay_kickoffRight = new AudioPath(this.rootFolder + str1 + "KICKOFF_RIGHT");
    this.activePlay_kickoffOnsideNotRec = new AudioPath(this.rootFolder + str1 + "KICKOFF_ONSIDE_NOT_REC");
    this.activePlay_kickoffOnsideRec = new AudioPath(this.rootFolder + str1 + "KICKOFF_ONSIDE_REC");
    this.activePlay_kickoffOutOfBounds = new AudioPath(this.rootFolder + str1 + "KICKOFF_GOES_OUT_OF_BOUNDS");
    this.activePlay_kickoffShort = new AudioPath(this.rootFolder + str1 + "KICKOFF_SHORT");
    this.activePlay_kickoffSquib = new AudioPath(this.rootFolder + str1 + "KICKOFF_SQUIB");
    this.activePlay_kickoffStartOfGame = new AudioPath(this.rootFolder + str1 + "KICKOFF_START_OF_GAME");
    this.activePlay_optionQBPitch = new AudioPath(this.rootFolder + str1 + "OPTION_QB_PITCH");
    this.activePlay_optionStartPlay = new AudioPath(this.rootFolder + str1 + "OPTION_START_OF_PLAY");
    this.activePlay_playAction = new AudioPath(this.rootFolder + str1 + "PLAY_ACTION_PASS");
    this.activePlay_puntFielded = new AudioPath(this.rootFolder + str1 + "PUNT_FIELDED");
    this.activePlay_puntIntoEndzone = new AudioPath(this.rootFolder + str1 + "PUNT_GOES_INTO_ENDZONE");
    this.activePlay_puntOutOfBounds = new AudioPath(this.rootFolder + str1 + "PUNT_GOES_OUT_OF_BOUNDS");
    this.activePlay_puntsBall = new AudioPath(this.rootFolder + str1 + "PUNTS_BALL");
    this.activePlay_QBDroppingBack = new AudioPath(this.rootFolder + str1 + "QB_DROPPING_BACK");
    this.activePlay_QBHandoffGeneric = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_GENERIC");
    this.activePlay_QBHandoffInside = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_INSIDE");
    this.activePlay_QBHandoffLeft = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_LEFT");
    this.activePlay_QBHandoffOutside = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_OUTSIDE");
    this.activePlay_QBHandoffRight = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_RIGHT");
    this.activePlay_QBHandoffStrong = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_STRONG_SIDE");
    this.activePlay_QBHandoffWeak = new AudioPath(this.rootFolder + str1 + "QB_HANDOFF_WEAK_SIDE");
    this.activePlay_QBInPocket = new AudioPath(this.rootFolder + str1 + "QB_IN_POCKET_AFTER");
    this.activePlay_QBScrambling = new AudioPath(this.rootFolder + str1 + "QB_SCRAMBLING");
    this.activePlay_QBTossGeneric = new AudioPath(this.rootFolder + str1 + "QB_TOSS_GENERIC");
    this.activePlay_QBTossLeft = new AudioPath(this.rootFolder + str1 + "QB_TOSS_LEFT");
    this.activePlay_QBTossRight = new AudioPath(this.rootFolder + str1 + "QB_TOSS_RIGHT");
    this.activePlay_QBTossStrong = new AudioPath(this.rootFolder + str1 + "QB_TOSS_STRONG_SIDE");
    this.activePlay_QBTossWeak = new AudioPath(this.rootFolder + str1 + "QB_TOSS_WEAK_SIDE");
    this.activePlay_TackleBroken = new AudioPath(this.rootFolder + str1 + "TACKLE_BROKEN");
    this.activePlay_ThrowEndzoneLeft = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION-END_ZONE_LEFT");
    this.activePlay_ThrowCenterGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_CENTER_GENERIC");
    this.activePlay_ThrowDeepCenter = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_DEEP_CENTER");
    this.activePlay_ThrowDeepEndzone = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_DEEP_END_ZONE");
    this.activePlay_ThrowDeepGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_DEEP_GENERIC");
    this.activePlay_ThrowDeepLeft = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_DEEP_LEFT");
    this.activePlay_ThrowDeepRight = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_DEEP_RIGHT");
    this.activePlay_ThrowDeepSideline = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_DEEP_SIDELINE");
    this.activePlay_ThrowEndzoneCenter = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_END_ZONE_CENTER");
    this.activePlay_ThrowEndzoneGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_END_ZONE_GENERIC");
    this.activePlay_ThrowEndzoneRight = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_END_ZONE_RIGHT");
    this.activePlay_ThrowFirstDown = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_FIRST_DOWN");
    this.activePlay_ThrowLeftGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_LEFT_GENERIC");
    this.activePlay_ThrowRightGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_RIGHT_GENERIC");
    this.activePlay_ThrowShortCenter = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_SHORT_CENTER");
    this.activePlay_ThrowShortGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_SHORT_GENERIC");
    this.activePlay_ThrowLeftFlat = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_SHORT_LEFT_FLAT");
    this.activePlay_ThrowRightFlat = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_SHORT_RIGHT_FLAT");
    this.activePlay_ThrowShortSideline = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_SHORT_SIDELINE");
    this.activePlay_ThrowSidelineGeneric = new AudioPath(this.rootFolder + str1 + "THROW_LOCATION_SIDELINE_GENERIC");
    this.activePlay_ThrowTypeBullet = new AudioPath(this.rootFolder + str1 + "THROW_TYPE_BULLET_PASS");
    this.activePlay_ThrowTypeDeep = new AudioPath(this.rootFolder + str1 + "THROW_TYPE_DEEP_PASS");
    this.activePlay_ThrowTypeGeneric = new AudioPath(this.rootFolder + str1 + "THROW_TYPE_GENERIC");
    this.activePlay_ThrowTypeLob = new AudioPath(this.rootFolder + str1 + "THROW_TYPE_LOB_PASS");
    this.activePlay_ThrowTypeScreen = new AudioPath(this.rootFolder + str1 + "THROW_TYPE_SCREEN_PASS");
    this.afterCommercial = new AudioPath(this.rootFolder + "AFTER_COMMERCIAL", 7, true);
    this.beforeCommercial = new AudioPath(this.rootFolder + "BEFORE_COMMERCIAL", 8, true);
    string str2 = "BEFORE_PLAY_STARTS/";
    this.beforePlay_audible = new AudioPath(this.rootFolder + str2 + "AUDIBLE", 7, true);
    this.beforePlay_doubleTESet = new AudioPath(this.rootFolder + str2 + "DOUBLE_TE_SET", 6, true);
    this.beforePlay_extraPointSelect = new AudioPath(this.rootFolder + str2 + "EXTRA_POINT_SELECTION", 5, true);
    this.beforePlay_FGSelectGeneric = new AudioPath(this.rootFolder + str2 + "FIELD_GOAL_SELECTION_GENERIC", 7, true);
    this.beforePlay_FGSelectLong = new AudioPath(this.rootFolder + str2 + "FIELD_GOAL_SELECTION_LONG", 5, true);
    this.beforePlay_FGSelectShort = new AudioPath(this.rootFolder + str2 + "FIELD_GOAL_SELECTION_SHORT", 4, true);
    this.beforePlay_firstDownGeneric = new AudioPath(this.rootFolder + str2 + "FIRST_DOWN_GENERIC", 6, true);
    this.beforePlay_FirstDownGoal = new AudioPath(this.rootFolder + str2 + "FIRST_DOWN_GOAL", 3, true);
    this.beforePlay_firstPlayOfDrive = new AudioPath(this.rootFolder + str2 + "FIRST_PLAY_OF_DRIVE", 8, true);
    this.beforePlay_fiveDBSet = new AudioPath(this.rootFolder + str2 + "FIVE_DEFENSIVE_BACKS", 6, true);
    this.beforePlay_fiveRecSet = new AudioPath(this.rootFolder + str2 + "FIVE_RECEIVER_SET", 6, true);
    this.beforePlay_fourRecSet = new AudioPath(this.rootFolder + str2 + "FOUR_RECEIVER_SET", 6, true);
    this.beforePlay_fourthDownGeneric = new AudioPath(this.rootFolder + str2 + "FOURTH_DOWN_GENERIC", 7, true);
    this.beforePlay_fourthDownGoal = new AudioPath(this.rootFolder + str2 + "FOURTH_DOWN_GOAL", 3, true);
    this.beforePlay_fourthDownLong = new AudioPath(this.rootFolder + str2 + "FOURTH_DOWN_LONG", 2, true);
    this.beforePlay_fourthDownShort = new AudioPath(this.rootFolder + str2 + "FOURTH_DOWN_SHORT", 2, true);
    this.beforePlay_hotRoute = new AudioPath(this.rootFolder + str2 + "HOT_ROUTE", 5, true);
    this.beforePlay_kickoffSelect = new AudioPath(this.rootFolder + str2 + "KICKOFF_SELECTION", 6, true);
    this.beforePlay_motionGeneric = new AudioPath(this.rootFolder + str2 + "MOTION_GENERIC", 4, true);
    this.beforePlay_motionLeft = new AudioPath(this.rootFolder + str2 + "MOTION_LEFT", 4, true);
    this.beforePlay_motionRight = new AudioPath(this.rootFolder + str2 + "MOTION_RIGHT", 4, true);
    this.beforePlay_oneBackSet = new AudioPath(this.rootFolder + str2 + "ONE_BACK_SET", 7, true);
    this.beforePlay_onsideKickSelect = new AudioPath(this.rootFolder + str2 + "ONSIDE_KICK_SELECTION", 4, true);
    this.beforePlay_puntSelect = new AudioPath(this.rootFolder + str2 + "PUNT_SELECTION", 7, true);
    this.beforePlay_secondDownGeneric = new AudioPath(this.rootFolder + str2 + "SECOND_DOWN_GENERIC", 5, true);
    this.beforePlay_secondDownGoal = new AudioPath(this.rootFolder + str2 + "SECOND_DOWN_GOAL", 3, true);
    this.beforePlay_secondDownLong = new AudioPath(this.rootFolder + str2 + "SECOND_DOWN_LONG", 2, true);
    this.beforePlay_secondDownShort = new AudioPath(this.rootFolder + str2 + "SECOND_DOWN_SHORT", 2, true);
    this.beforePlay_shiftingDBBack = new AudioPath(this.rootFolder + str2 + "SHIFTING_DEFENSIVE_BACKS_BACK", 6, true);
    this.beforePlay_shiftingDBUp = new AudioPath(this.rootFolder + str2 + "SHIFTING_DEFENSIVE_BACKS_UP", 6, true);
    this.beforePlay_shiftingDLine = new AudioPath(this.rootFolder + str2 + "SHIFTING_DEFENSIVE_LINE", 5, true);
    this.beforePlay_shiftingLB = new AudioPath(this.rootFolder + str2 + "SHIFTING_LINEBACKERS", 6, true);
    this.beforePlay_shotgunSet = new AudioPath(this.rootFolder + str2 + "SHOTGUN_SET", 3, true);
    this.beforePlay_sixDBSet = new AudioPath(this.rootFolder + str2 + "SIX_DEFENSIVE_BACKS", 6, true);
    this.beforePlay_TELeft = new AudioPath(this.rootFolder + str2 + "TE_LEFT", 3, true);
    this.beforePlay_TERight = new AudioPath(this.rootFolder + str2 + "TE_RIGHT", 3, true);
    this.beforePlay_thirdDownGeneric = new AudioPath(this.rootFolder + str2 + "THIRD_DOWN_GENERIC", 4, true);
    this.beforePlay_thirdDownGoal = new AudioPath(this.rootFolder + str2 + "THIRD_DOWN_GOAL", 3, true);
    this.beforePlay_thirdDownLong = new AudioPath(this.rootFolder + str2 + "THIRD_DOWN_LONG", 2, true);
    this.beforePlay_thirdDownShort = new AudioPath(this.rootFolder + str2 + "THIRD_DOWN_SHORT", 2, true);
    this.beforePlay_threeBackSet = new AudioPath(this.rootFolder + str2 + "THREE_BACK_SET", 6, true);
    this.beforePlay_threeReceiverSet = new AudioPath(this.rootFolder + str2 + "THREE_RECEIVER_SET", 6, true);
    this.beforePlay_twoBackSet = new AudioPath(this.rootFolder + str2 + "TWO_BACK_SET", 6, true);
    this.beforePlay_twoPointSelect = new AudioPath(this.rootFolder + str2 + "TWO_POINT_CONVERSION_SELECTION", 5, true);
    this.beforePlay_twoReceiverSet = new AudioPath(this.rootFolder + str2 + "TWO_RECEIVER_SET", 6, true);
    this.beforePlay_underCenterSet = new AudioPath(this.rootFolder + str2 + "UNDER_CENTER_SET", 3, true);
    this.beforePlay_playOfTheDrive = new AudioPath(this.rootFolder + str2 + "X_PLAY_OF_DRIVE", 2, true);
    this.calldownToReporter = new AudioPath(this.rootFolder + "CALLDOWN_TO_REPORTER", 9, true);
    this.callingTimeoutFirst = new AudioPath(this.rootFolder + "CALLING_TIMEOUT_1ST", 5, true);
    this.callingTimeoutSecond = new AudioPath(this.rootFolder + "CALLING_TIMEOUT_2ND", 5, true);
    this.callingTimeoutThird = new AudioPath(this.rootFolder + "CALLING_TIMEOUT_3RD", 6, true);
    this.callingTimeoutGeneric = new AudioPath(this.rootFolder + "CALLING_TIMEOUT_GENERIC", 4, true);
    this.endGameFinalRemarks = new AudioPath(this.rootFolder + "END_GAME_FINAL_REMARKS", 4, false);
    this.endGameGenericScore = new AudioPath(this.rootFolder + "END_GAME_GENERIC_SCORE", 5, false);
    this.endGameOutro = new AudioPath(this.rootFolder + "END_GAME_OUTRO", 7, false);
    this.endGameSeasonGameScore = new AudioPath(this.rootFolder + "END_GAME_SEASON_GAME_SCORE", 5, false);
    this.endOfQuarter1 = new AudioPath(this.rootFolder + "END_OF_1ST_QUARTER", 7, false);
    this.endOfQuarter2 = new AudioPath(this.rootFolder + "END_OF_2ND_QUARTER", 7, false);
    this.endOfQuarter3 = new AudioPath(this.rootFolder + "END_OF_3RD_QUARTER", 7, false);
    this.endOfQuarter4 = new AudioPath(this.rootFolder + "END_OF_4TH_QUARTER", 7, false);
    this.endOfQuarterOneTeamWinning = new AudioPath(this.rootFolder + "END_OF_QUARTER_SCORE_ONE_TEAM_WINNING", 7, true);
    this.endOfQuarterTied = new AudioPath(this.rootFolder + "END_OF_QUARTER_SCORE_TIED", 4, true);
    this.endOfQuarterScoreZero = new AudioPath(this.rootFolder + "END_OF_QUARTER_SCORE_ZERO", 5, true);
    this.injury = new AudioPath(this.rootFolder + "INJURY", 3, true);
    this.jokeResponses = new AudioPath(this.rootFolder + "JOKE_RESPONSES", 5, true);
    string str3 = "MISC_WORDS_PHRASES/";
    this.misc_conjunctions = new AudioPath(this.rootFolder + str3 + "CONJUNCTIONS");
    this.misc_nouns = new AudioPath(this.rootFolder + str3 + "NOUNS");
    this.misc_phrases = new AudioPath(this.rootFolder + str3 + "PHRASES");
    this.misc_prepositions = new AudioPath(this.rootFolder + str3 + "PREPOSITIONS");
    this.misc_homeTeam_prefix = new AudioPath(this.rootFolder + str3 + "HOME_PREFIX");
    this.misc_homeTeam_postfix = new AudioPath(this.rootFolder + str3 + "HOME_POSTFIX");
    this.misc_awayTeam_prefix = new AudioPath(this.rootFolder + str3 + "AWAY_PREFIX");
    this.misc_awayTeam_postfix = new AudioPath(this.rootFolder + str3 + "AWAY_POSTFIX");
    this.misc_offense_prefix = new AudioPath(this.rootFolder + str3 + "OFFENSE_PREFIX");
    this.misc_offense_postfix = new AudioPath(this.rootFolder + str3 + "OFFENSE_POSTFIX");
    this.misc_defense_prefix = new AudioPath(this.rootFolder + str3 + "DEFENSE_PREFIX");
    this.misc_defense_postfix = new AudioPath(this.rootFolder + str3 + "DEFENSE_POSTFIX");
    string str4 = "NUMBERS/";
    this.numbers_1To99_prefix = new AudioPath(this.rootFolder + str4 + "1_TO_99_PREFIX");
    this.numbers_1To99_postfix = new AudioPath(this.rootFolder + str4 + "1_TO_99_POSTFIX");
    this.numbers_1To99_withWord = new AudioPath(this.rootFolder + str4 + "1_TO_99_WITH_WORD");
    this.numbers_hundreds = new AudioPath(this.rootFolder + str4 + "HUNDREDS");
    this.numbers_misc = new AudioPath(this.rootFolder + str4 + "MISC");
    this.numbers_ordinal = new AudioPath(this.rootFolder + str4 + "ORDINAL");
    this.numbers_thousands = new AudioPath(this.rootFolder + str4 + "THOUSANDS");
    string str5 = "PENALTIES/";
    this.penalty_accepting = new AudioPath(this.rootFolder + str5 + "ACCEPTING_PENALTY");
    this.penalty_afterPlay = new AudioPath(this.rootFolder + str5 + "AFTER_PLAY");
    this.penalty_declining = new AudioPath(this.rootFolder + str5 + "DECLINING_PENALTY");
    this.penalty_flagThrown = new AudioPath(this.rootFolder + str5 + "FLAG_THROWN");
    this.penalty_playEndAddition = new AudioPath(this.rootFolder + str5 + "PLAY_ENDING_ADDITION_PENALTY");
    string str6 = "PLAY_ENDING_SEQUENCE/";
    this.playEnd_inc_deflected = new AudioPath(this.rootFolder + str6 + "INCOMPLETE_PASS_DEFLECTED", 10, true);
    this.playEnd_inc_dropped = new AudioPath(this.rootFolder + str6 + "INCOMPLETE_PASS_DROPPED", 6, true);
    this.playEnd_inc_generic = new AudioPath(this.rootFolder + str6 + "INCOMPLETE_PASS_GENERIC", 12, true);
    this.playEnd_madeFG_generic = new AudioPath(this.rootFolder + str6 + "MADE_FG_GENERIC", 5, true);
    this.playEnd_madeFG_nearlyMissed = new AudioPath(this.rootFolder + str6 + "MADE_FG_NEARLY_MISS", 3, true);
    this.playEnd_madeExtraPoint = new AudioPath(this.rootFolder + str6 + "MADE_EXTRA_POINT", 3, true);
    this.playEnd_madeTwoPoint_Pass = new AudioPath(this.rootFolder + str6 + "MADE_TWO_POINT_CONVERSION_PASSING", 5, true);
    this.playEnd_madeTwoPoint_Run = new AudioPath(this.rootFolder + str6 + "MADE_TWO_POINT_CONVERSION_RUNNING", 5, true);
    this.playEnd_missedFG_generic = new AudioPath(this.rootFolder + str6 + "MISSED_FG_GENERIC", 4, true);
    this.playEnd_missedFG_short = new AudioPath(this.rootFolder + str6 + "MISSED_FG_SHORT", 3, true);
    this.playEnd_missedFG_wide = new AudioPath(this.rootFolder + str6 + "MISSED_FG_WIDE", 3, true);
    this.playEnd_missedFG_nearlyMade = new AudioPath(this.rootFolder + str6 + "MISSED_FG_WIDE_NEARLY_MADE", 3, true);
    this.playEnd_addition_failedTwoPoint = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_FAILED_2_POINT", 4, true);
    this.playEnd_addition_failedFourthDown = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_FAILED_FOURTH_4TH_DOWN", 4, true);
    this.playEnd_addition_firstDown = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_FIRST_DOWN", 8, true);
    this.playEnd_addition_fourthDown = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_FOURTH_DOWN", 3, true);
    this.playEnd_addition_genericGain = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_GENERIC_GAIN", 7, true);
    this.playEnd_addition_genericLoss = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_GENERIC_LOSS", 4, true);
    this.playEnd_addition_secondDown = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_SECOND_DOWN", 5, true);
    this.playEnd_addition_shortGain = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_SHORT_GAIN", 2, true);
    this.playEnd_addition_shortLoss = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_SHORT_LOSS", 2, true);
    this.playEnd_addition_thirdDown = new AudioPath(this.rootFolder + str6 + "PLAY_ENDING_ADDITION_THIRD_DOWN", 6, true);
    this.playEnd_QBSlide = new AudioPath(this.rootFolder + str6 + "QB_SLIDE", 7, true);
    this.playEnd_runOutOfBounds = new AudioPath(this.rootFolder + str6 + "RUNNING_OUT_OF_BOUNDS", 7, true);
    this.playEnd_tackle_afterKick = new AudioPath(this.rootFolder + str6 + "TACKLE_AFTER_KICK", 3, true);
    this.playEnd_tackle_generic = new AudioPath(this.rootFolder + str6 + "TACKLE_GENERIC", 14, true);
    this.playEnd_tackle_longGainRun = new AudioPath(this.rootFolder + str6 + "TACKLE_LONG_GAIN_RUN", 4, true);
    this.playEnd_tackle_multipleSacks = new AudioPath(this.rootFolder + str6 + "TACKLE_MULTIPLE_SACKS", 3, true);
    this.playEnd_tackle_sack = new AudioPath(this.rootFolder + str6 + "TACKLE_SACK", 12, true);
    this.playEnd_tackle_safety = new AudioPath(this.rootFolder + str6 + "TACKLE_SAFETY", 3, true);
    this.playEnd_tackle_shortGainRun = new AudioPath(this.rootFolder + str6 + "TACKLE_SHORT_GAIN_RUN", 6, true);
    this.playEnd_touchdown_diving = new AudioPath(this.rootFolder + str6 + "TOUCHDOWN_DIVING", 8, true);
    this.playEnd_touchdown_generic = new AudioPath(this.rootFolder + str6 + "TOUCHDOWN_GENERIC", 6, true);
    this.playEnd_touchdown_passing = new AudioPath(this.rootFolder + str6 + "TOUCHDOWN_PASSING", 5, true);
    this.playEnd_touchdown_running = new AudioPath(this.rootFolder + str6 + "TOUCHDOWN_RUNNING", 4, true);
    this.lastNames = new Dictionary<string, AudioClip>();
    string str7 = "POSITIONS_PREFIX/";
    this.positions_C_prefix = new AudioPath(this.rootFolder + str7 + "C");
    this.positions_CB_prefix = new AudioPath(this.rootFolder + str7 + "CB");
    this.positions_DB_prefix = new AudioPath(this.rootFolder + str7 + "DB");
    this.positions_DE_prefix = new AudioPath(this.rootFolder + str7 + "DE");
    this.positions_DL_prefix = new AudioPath(this.rootFolder + str7 + "DL");
    this.positions_DT_prefix = new AudioPath(this.rootFolder + str7 + "DT");
    this.positions_FB_prefix = new AudioPath(this.rootFolder + str7 + "FB");
    this.positions_FS_prefix = new AudioPath(this.rootFolder + str7 + "FS");
    this.positions_ILB_prefix = new AudioPath(this.rootFolder + str7 + "ILB");
    this.positions_K_prefix = new AudioPath(this.rootFolder + str7 + "K");
    this.positions_LB_prefix = new AudioPath(this.rootFolder + str7 + "LB");
    this.positions_LG_prefix = new AudioPath(this.rootFolder + str7 + "LG");
    this.positions_LT_prefix = new AudioPath(this.rootFolder + str7 + "LT");
    this.positions_MLB_prefix = new AudioPath(this.rootFolder + str7 + "MLB");
    this.positions_NT_prefix = new AudioPath(this.rootFolder + str7 + "NT");
    this.positions_OL_prefix = new AudioPath(this.rootFolder + str7 + "OL");
    this.positions_OLB_prefix = new AudioPath(this.rootFolder + str7 + "OLB");
    this.positions_P_prefix = new AudioPath(this.rootFolder + str7 + "P");
    this.positions_QB_prefix = new AudioPath(this.rootFolder + str7 + "QB");
    this.positions_RB_prefix = new AudioPath(this.rootFolder + str7 + "RB");
    this.positions_RET_prefix = new AudioPath(this.rootFolder + str7 + "RET");
    this.positions_RG_prefix = new AudioPath(this.rootFolder + str7 + "RG");
    this.positions_RT_prefix = new AudioPath(this.rootFolder + str7 + "RT");
    this.positions_SLT_prefix = new AudioPath(this.rootFolder + str7 + "SLT");
    this.positions_SS_prefix = new AudioPath(this.rootFolder + str7 + "SS");
    this.positions_TE_prefix = new AudioPath(this.rootFolder + str7 + "TE");
    this.positions_WR_prefix = new AudioPath(this.rootFolder + str7 + "WR");
    string str8 = "POSITIONS_POSTFIX/";
    this.positions_C_postfix = new AudioPath(this.rootFolder + str8 + "C");
    this.positions_CB_postfix = new AudioPath(this.rootFolder + str8 + "CB");
    this.positions_DB_postfix = new AudioPath(this.rootFolder + str8 + "DB");
    this.positions_DE_postfix = new AudioPath(this.rootFolder + str8 + "DE");
    this.positions_DL_postfix = new AudioPath(this.rootFolder + str8 + "DL");
    this.positions_DT_postfix = new AudioPath(this.rootFolder + str8 + "DT");
    this.positions_FB_postfix = new AudioPath(this.rootFolder + str8 + "FB");
    this.positions_FS_postfix = new AudioPath(this.rootFolder + str8 + "FS");
    this.positions_ILB_postfix = new AudioPath(this.rootFolder + str8 + "ILB");
    this.positions_K_postfix = new AudioPath(this.rootFolder + str8 + "K");
    this.positions_LB_postfix = new AudioPath(this.rootFolder + str8 + "LB");
    this.positions_LG_postfix = new AudioPath(this.rootFolder + str8 + "LG");
    this.positions_LT_postfix = new AudioPath(this.rootFolder + str8 + "LT");
    this.positions_MLB_postfix = new AudioPath(this.rootFolder + str8 + "MLB");
    this.positions_NT_postfix = new AudioPath(this.rootFolder + str8 + "NT");
    this.positions_OL_postfix = new AudioPath(this.rootFolder + str8 + "OL");
    this.positions_OLB_postfix = new AudioPath(this.rootFolder + str8 + "OLB");
    this.positions_P_postfix = new AudioPath(this.rootFolder + str8 + "P");
    this.positions_QB_postfix = new AudioPath(this.rootFolder + str8 + "QB");
    this.positions_RB_postfix = new AudioPath(this.rootFolder + str8 + "RB");
    this.positions_RET_postfix = new AudioPath(this.rootFolder + str8 + "RET");
    this.positions_RG_postfix = new AudioPath(this.rootFolder + str8 + "RG");
    this.positions_RT_postfix = new AudioPath(this.rootFolder + str8 + "RT");
    this.positions_SLT_postfix = new AudioPath(this.rootFolder + str8 + "SLT");
    this.positions_SS_postfix = new AudioPath(this.rootFolder + str8 + "SS");
    this.positions_TE_postfix = new AudioPath(this.rootFolder + str8 + "TE");
    this.positions_WR_postfix = new AudioPath(this.rootFolder + str8 + "WR");
    string str9 = "PREGAME_INTRODUCTION/";
    this.pregame_announcerIntro = new AudioPath(this.rootFolder + str9 + "ANNOUNCER_INTRODUCTIONS", 10, false);
    this.pregame_final_championship = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_CHAMPIONSHIP", 3, false);
    this.pregame_final_playoffs = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_PLAYOFFS", 3, false);
    this.pregame_final_closeMatch = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_CLOSE_MATCHUP", 6, false);
    this.pregame_final_generic = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_GENERIC", 6, false);
    this.pregame_final_seasonGame = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_SEASON_GAME", 16, false);
    this.pregame_final_snowRain = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_SNOW_OR_RAIN", 3, false);
    this.pregame_final_starPlayer = new AudioPath(this.rootFolder + str9 + "FINAL_REMARKS_STAR_PLAYER", 3, false);
    this.pregame_generic = new AudioPath(this.rootFolder + str9 + "GENERIC_INTRODUCTION", 2, false);
    this.pregame_responseToAnalyst = new AudioPath(this.rootFolder + str9 + "RESPONSE_TO_ANALYST", 12, false);
    this.responseToReporter = new AudioPath(this.rootFolder + "RESPONSE_TO_REPORTER", 7, true);
    this.scoreUpdate_ExtendsLead = new AudioPath(this.rootFolder + "SCORE_UPDATE_EXTENDS_LEAD", 5, true);
    this.scoreUpdate_FirstScore = new AudioPath(this.rootFolder + "SCORE_UPDATE_FIRST_SCORE", 6, true);
    this.scoreUpdate_StillLosing = new AudioPath(this.rootFolder + "SCORE_UPDATE_STILL_LOSING", 5, true);
    this.scoreUpdate_TakesLead = new AudioPath(this.rootFolder + "SCORE_UPDATE_TAKES_LEAD", 5, true);
    string str10 = "STATS/";
    this.stats_beginning = new AudioPath(this.rootFolder + str10 + "BEGINNING", 33, true);
    this.stats_defensiveStats = new AudioPath(this.rootFolder + str10 + "DEFENSIVE_STATS", 12, true);
    this.stats_ending = new AudioPath(this.rootFolder + str10 + "ENDING", 19, true);
    this.stats_offensiveStats = new AudioPath(this.rootFolder + str10 + "OFFENSIVE_STATS", 48, true);
    this.stats_teamStats = new AudioPath(this.rootFolder + str10 + "TEAM_STATS", 10, true);
  }

  public void LoadTeamAudio()
  {
    string str1 = "team_audio/";
    bool flag1 = PersistentData.GetHomeTeamIndex() < 31 && PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets;
    if (flag1 && (PersistentData.GetHomeTeamIndex() == 19 || PersistentData.GetHomeTeamIndex() == 22 || PersistentData.GetHomeTeamIndex() == 33 || PersistentData.GetHomeTeamIndex() == 35))
      flag1 = false;
    if (flag1)
    {
      string str2 = "team_" + PersistentData.GetHomeTeamIndex().ToString();
      this.homeTeamNamePrefix = new AudioPath(this.rootFolder + str1 + str2, "team_name_prefix");
      this.homeTeamNamePostfix = new AudioPath(this.rootFolder + str1 + str2, "team_name_postfix");
      this.homeTeamCityPrefix = new AudioPath(this.rootFolder + str1 + str2, "team_city_prefix");
      this.homeTeamCityPostfix = new AudioPath(this.rootFolder + str1 + str2, "team_city_postfix");
      this.stadiumLocation = new AudioPath(this.rootFolder + str1 + str2, "team_stadium");
      if (PersistentData.GetHomeTeamIndex() == 2 || PersistentData.GetHomeTeamIndex() == 4 || PersistentData.GetHomeTeamIndex() == 5 || PersistentData.GetHomeTeamIndex() == 9)
      {
        this.homeTeamNamePrefix = this.homeTeamCityPrefix;
        this.homeTeamNamePostfix = this.homeTeamCityPostfix;
      }
    }
    if (!flag1 || this.homeTeamCityPrefix.count == 0)
    {
      this.homeTeamCityPrefix = this.misc_homeTeam_prefix;
      this.homeTeamCityPostfix = this.misc_homeTeam_postfix;
    }
    if (!flag1 || this.homeTeamNamePrefix.count == 0)
    {
      this.homeTeamNamePrefix = this.misc_homeTeam_prefix;
      this.homeTeamNamePostfix = this.misc_homeTeam_postfix;
    }
    bool flag2 = PersistentData.GetAwayTeamIndex() < TeamAssetManager.NUMBER_OF_BASE_TEAMS && PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets;
    if (flag2 && (PersistentData.GetAwayTeamIndex() == 19 || PersistentData.GetAwayTeamIndex() == 22 || PersistentData.GetAwayTeamIndex() == 33 || PersistentData.GetAwayTeamIndex() == 35))
      flag2 = false;
    if (flag2)
    {
      string str3 = "team_" + PersistentData.GetAwayTeamIndex().ToString();
      this.awayTeamNamePrefix = new AudioPath(this.rootFolder + str1 + str3, "team_name_prefix");
      this.awayTeamNamePostfix = new AudioPath(this.rootFolder + str1 + str3, "team_name_postfix");
      this.awayTeamCityPrefix = new AudioPath(this.rootFolder + str1 + str3, "team_city_prefix");
      this.awayTeamCityPostfix = new AudioPath(this.rootFolder + str1 + str3, "team_city_postfix");
      if (PersistentData.GetAwayTeamIndex() == 2 || PersistentData.GetAwayTeamIndex() == 4 || PersistentData.GetAwayTeamIndex() == 5 || PersistentData.GetAwayTeamIndex() == 9)
      {
        this.awayTeamNamePrefix = this.awayTeamCityPrefix;
        this.awayTeamNamePostfix = this.awayTeamCityPostfix;
      }
    }
    if (!flag2 || this.awayTeamNamePrefix.count == 0)
    {
      this.awayTeamNamePrefix = this.misc_awayTeam_prefix;
      this.awayTeamNamePostfix = this.misc_awayTeam_postfix;
    }
    if (flag2 && this.awayTeamCityPrefix.count != 0)
      return;
    this.awayTeamCityPrefix = this.misc_awayTeam_prefix;
    this.awayTeamCityPostfix = this.misc_awayTeam_postfix;
  }

  public void PreloadPopularPlayerNames()
  {
    TeamData userTeam = PersistentData.GetUserTeam();
    TeamData compTeam = PersistentData.GetCompTeam();
    List<string> stringList = new List<string>();
    stringList.Add(userTeam.TeamDepthChart.GetStartingQB().LastName);
    stringList.Add(userTeam.TeamDepthChart.GetStartingRB().LastName);
    stringList.Add(userTeam.TeamDepthChart.GetStartingFB().LastName);
    stringList.Add(userTeam.TeamDepthChart.GetStartingWRX().LastName);
    stringList.Add(userTeam.TeamDepthChart.GetStartingWRY().LastName);
    stringList.Add(userTeam.TeamDepthChart.GetStartingWRZ().LastName);
    stringList.Add(compTeam.TeamDepthChart.GetStartingQB().LastName);
    stringList.Add(compTeam.TeamDepthChart.GetStartingRB().LastName);
    stringList.Add(compTeam.TeamDepthChart.GetStartingFB().LastName);
    stringList.Add(compTeam.TeamDepthChart.GetStartingWRX().LastName);
    stringList.Add(compTeam.TeamDepthChart.GetStartingWRY().LastName);
    stringList.Add(compTeam.TeamDepthChart.GetStartingWRZ().LastName);
    for (int index = 0; index < stringList.Count; ++index)
    {
      AudioClip audioClip = AddressablesData.instance.LoadAssetSync<AudioClip>(AddressablesData.CorrectingAssetKey(this.rootFolder + "PLAYER_LAST_NAMES"), stringList[index]);
      if ((Object) audioClip != (Object) null && !this.lastNames.ContainsKey(stringList[index]))
        this.lastNames.Add(stringList[index], audioClip);
    }
  }

  public void ResetCommentVariables()
  {
    this.playedCommentOnPass = false;
    this.playedCommentOnShift = false;
  }

  private void PlayRandomClip(AudioPath audioPath) => this.PlayRandomClip(audioPath, 0.0f);

  private void PlayRandomClip(AudioPath audioPath, float pauseBefore)
  {
    if ((Object) this.commentary == (Object) null)
      return;
    int index = Random.Range(0, audioPath.count) + 1;
    if ((double) pauseBefore > 0.0)
      this.commentary.AddPause(pauseBefore);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(audioPath, index)));
  }

  public void PlayPlayerID(PlayerAI player, AudioAddition type)
  {
    if ((Object) player == (Object) null)
      return;
    int num = player.onUserTeam ? 1 : 0;
    int indexOnTeam = player.indexOnTeam;
    AudioClip lastName = this.GetLastName(player.lastName);
    if ((Object) lastName != (Object) null)
      this.commentary.AddSegmentToQueue(new AudioSegment(lastName, 0.0f));
    else if (Random.Range(0, 100) < 75)
      this.PlayPlayer_Number(player.number, type);
    else
      this.PlayPlayer_Position(player.formationPosition, type);
  }

  private void PlayPlayerID_QB(AudioAddition type)
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.PlayPlayerID(MatchManager.instance.playersManager.curUserScriptRef[5], type);
    else
      this.PlayPlayerID(MatchManager.instance.playersManager.curCompScriptRef[5], type);
  }

  public void PlayPlayerNameOrNumber(bool userTeam, int indexOnTeam, AudioAddition type)
  {
    TeamData teamData = !userTeam ? MatchManager.instance.playersManager.compTeamData : MatchManager.instance.playersManager.userTeamData;
    AudioClip lastName = this.GetLastName(teamData.GetPlayer(indexOnTeam).LastName);
    if ((Object) lastName != (Object) null)
      this.commentary.AddSegmentToQueue(new AudioSegment(lastName, 0.0f));
    else
      this.PlayPlayer_Number(teamData.GetPlayer(indexOnTeam).Number, type);
  }

  public void PlayByPlayer(PlayerAI player)
  {
    AudioClip lastName = this.GetLastName(player.lastName);
    if ((Object) lastName != (Object) null)
    {
      this.PlayWord_By();
      this.commentary.AddSegmentToQueue(new AudioSegment(lastName, 0.0f));
    }
    else
    {
      this.PlayWord_ByNumber();
      this.PlayNumber(player.number, AudioAddition.Postfix);
    }
  }

  public void PlayTeamID_Offense(AudioAddition type)
  {
    int num = Random.Range(0, 100);
    if (num < 10)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheOffense_Prefix();
      else
        this.PlayWord_TheOffense_Postfix();
    }
    else if (num < 50)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        if (type == AudioAddition.Prefix)
          this.PlayUserTeamCity_Prefix();
        else
          this.PlayUserTeamCity_Postfix();
      }
      else if (type == AudioAddition.Prefix)
        this.PlayCompTeamCity_Prefix();
      else
        this.PlayCompTeamCity_Postfix();
    }
    else if (num < 90)
    {
      if (global::Game.IsPlayerOneOnOffense)
      {
        if (type == AudioAddition.Prefix)
          this.PlayUserTeamName_Prefix();
        else
          this.PlayUserTeamName_Postfix();
      }
      else if (type == AudioAddition.Prefix)
        this.PlayCompTeamName_Prefix();
      else
        this.PlayCompTeamName_Postfix();
    }
    else if (global::Game.IsPlayerOneOnOffense)
    {
      if (PersistentData.userIsHome)
      {
        if (type == AudioAddition.Prefix)
          this.PlayWord_TheHomeTeam_Prefix();
        else
          this.PlayWord_TheHomeTeam_Postfix();
      }
      else if (type == AudioAddition.Prefix)
        this.PlayWord_TheAwayTeam_Prefix();
      else
        this.PlayWord_TheAwayTeam_Postfix();
    }
    else if (!PersistentData.userIsHome)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheHomeTeam_Prefix();
      else
        this.PlayWord_TheHomeTeam_Postfix();
    }
    else if (type == AudioAddition.Prefix)
      this.PlayWord_TheAwayTeam_Prefix();
    else
      this.PlayWord_TheAwayTeam_Postfix();
  }

  public void PlayTeamID_Defense(AudioAddition type)
  {
    int num = Random.Range(0, 100);
    if (num < 10)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheDefense_Prefix();
      else
        this.PlayWord_TheDefense_Postfix();
    }
    else if (num < 50)
    {
      if (global::Game.IsPlayerOneOnDefense)
      {
        if (type == AudioAddition.Prefix)
          this.PlayUserTeamCity_Prefix();
        else
          this.PlayUserTeamCity_Postfix();
      }
      else if (type == AudioAddition.Prefix)
        this.PlayCompTeamCity_Prefix();
      else
        this.PlayCompTeamCity_Postfix();
    }
    else if (num < 90)
    {
      if (global::Game.IsPlayerOneOnDefense)
      {
        if (type == AudioAddition.Prefix)
          this.PlayUserTeamName_Prefix();
        else
          this.PlayUserTeamName_Postfix();
      }
      else if (type == AudioAddition.Prefix)
        this.PlayCompTeamName_Prefix();
      else
        this.PlayCompTeamName_Postfix();
    }
    else if (global::Game.IsPlayerOneOnDefense)
    {
      if (PersistentData.userIsHome)
      {
        if (type == AudioAddition.Prefix)
          this.PlayWord_TheHomeTeam_Prefix();
        else
          this.PlayWord_TheHomeTeam_Postfix();
      }
      else if (type == AudioAddition.Prefix)
        this.PlayWord_TheAwayTeam_Prefix();
      else
        this.PlayWord_TheAwayTeam_Postfix();
    }
    else if (!PersistentData.userIsHome)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheHomeTeam_Prefix();
      else
        this.PlayWord_TheHomeTeam_Postfix();
    }
    else if (type == AudioAddition.Prefix)
      this.PlayWord_TheAwayTeam_Prefix();
    else
      this.PlayWord_TheAwayTeam_Postfix();
  }

  private void PlayTeamID_User(AudioAddition type)
  {
    int num = Random.Range(0, 100);
    if (num < 40)
    {
      if (type == AudioAddition.Prefix)
        this.PlayUserTeamName_Prefix();
      else
        this.PlayUserTeamName_Postfix();
    }
    else if (num < 80)
    {
      if (type == AudioAddition.Prefix)
        this.PlayUserTeamCity_Prefix();
      else
        this.PlayUserTeamCity_Postfix();
    }
    else if (PersistentData.userIsHome)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheHomeTeam_Prefix();
      else
        this.PlayWord_TheHomeTeam_Postfix();
    }
    else if (type == AudioAddition.Prefix)
      this.PlayWord_TheAwayTeam_Prefix();
    else
      this.PlayWord_TheAwayTeam_Postfix();
  }

  private void PlayTeamID_Comp(AudioAddition type)
  {
    int num = Random.Range(0, 100);
    if (num < 40)
    {
      if (type == AudioAddition.Prefix)
        this.PlayCompTeamName_Prefix();
      else
        this.PlayCompTeamName_Postfix();
    }
    else if (num < 80)
    {
      if (type == AudioAddition.Prefix)
        this.PlayCompTeamCity_Prefix();
      else
        this.PlayCompTeamCity_Postfix();
    }
    else if (PersistentData.userIsHome)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheAwayTeam_Prefix();
      else
        this.PlayWord_TheAwayTeam_Postfix();
    }
    else if (type == AudioAddition.Prefix)
      this.PlayWord_TheHomeTeam_Prefix();
    else
      this.PlayWord_TheHomeTeam_Postfix();
  }

  public void PlayCommentAfterCameraFlip()
  {
    if (PlayState.PlayType.Value == PlayType.Punt)
      this.PlayPuntFielded();
    else if (MatchManager.instance.onsideKick)
    {
      if (!(bool) ProEra.Game.MatchState.Turnover)
        return;
      this.PlayOnsideKickNotSuccessful();
    }
    else
    {
      if (PlayState.PlayType.Value != PlayType.Kickoff)
        return;
      this.PlayKickoffFielded();
    }
  }

  public void PlayCommentOnPass()
  {
    if (Random.Range(0, 100) < 20 || (Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    float yards = (float) Field.ConvertDistanceToYards(Field.FindDifference(MatchManager.instance.playersManager.passDestination.z, ProEra.Game.MatchState.BallOn.Value));
    Vector3 passDestination = MatchManager.instance.playersManager.passDestination;
    int num = Random.Range(0, 100);
    bool flag = false;
    if ((double) yards > 25.0 && num < 75)
      this.PlayThrowType_DeepPass();
    else if ((double) yards < 0.0 && num < 75)
    {
      this.PlayThrowType_ScreenPass();
      flag = true;
      this.PlayThrowLocation_Generic();
    }
    else if (MatchManager.instance.playersManager.bulletPass && num < 75)
      this.PlayThrowType_Bullet();
    else
      this.PlayThrowType_Generic();
    if (flag || Random.Range(0, 100) >= 60 || (double) Mathf.Abs(passDestination.x) > 15.0)
      return;
    switch (Random.Range(0, 3))
    {
      case 0:
        if ((double) passDestination.z > 48.0)
        {
          this.PlayThrowLocation_Endzone();
          break;
        }
        if (this.IsThrowToFirstDownMarker())
        {
          this.PlayThrowLocation_FirstDown();
          break;
        }
        if ((double) yards > 19.0)
        {
          this.PlayThrowLocation_Deep();
          break;
        }
        if ((double) yards < 3.0)
        {
          this.PlayThrowLocation_Short();
          break;
        }
        this.PlayThrowLocation_Generic();
        break;
      case 1:
        if ((double) yards > 19.0)
        {
          this.PlayThrowLocation_Deep();
          break;
        }
        if ((double) yards < 3.0)
        {
          this.PlayThrowLocation_Short();
          break;
        }
        if ((double) passDestination.z > 48.0)
        {
          this.PlayThrowLocation_Endzone();
          break;
        }
        if (this.IsThrowToFirstDownMarker())
        {
          this.PlayThrowLocation_FirstDown();
          break;
        }
        this.PlayThrowLocation_Generic();
        break;
      default:
        if (this.IsThrowToFirstDownMarker())
        {
          this.PlayThrowLocation_FirstDown();
          break;
        }
        if (Random.Range(0, 100) < 50)
        {
          this.PlayThrowLocation_Generic();
          break;
        }
        if ((double) passDestination.z > 48.0)
        {
          this.PlayThrowLocation_Endzone();
          break;
        }
        if ((double) yards > 19.0)
        {
          this.PlayThrowLocation_Deep();
          break;
        }
        if ((double) yards < 3.0)
        {
          this.PlayThrowLocation_Short();
          break;
        }
        this.PlayThrowLocation_Generic();
        break;
    }
  }

  private void PlayThrowLocation_Endzone()
  {
    if (Random.Range(0, 100) < 75)
    {
      if ((double) MatchManager.instance.playersManager.passDestination.x < -4.5)
        this.PlayThrowLocation_EndZone_Left();
      else if ((double) MatchManager.instance.playersManager.passDestination.x > 4.5)
        this.PlayThrowLocation_EndZone_Right();
      else
        this.PlayThrowLocation_EndZone_Center();
    }
    else
      this.PlayThrowLocation_EndZone_Generic();
  }

  private void PlayThrowLocation_Deep()
  {
    if (Random.Range(0, 100) < 75)
    {
      if (this.IsThrowToSideline())
        this.PlayThrowLocation_Deep_Sideline();
      else if (this.IsThrowToLeftSide())
        this.PlayThrowLocation_Deep_Left();
      else if (this.IsThrowToCenter())
        this.PlayThrowLocation_Deep_Center();
      else
        this.PlayThrowLocation_Deep_Right();
    }
    else
      this.PlayThrowLocation_Deep_Generic();
  }

  private void PlayThrowLocation_Short()
  {
    if (Random.Range(0, 100) < 75)
    {
      if (this.IsThrowToSideline())
        this.PlayThrowLocation_Short_Sideline();
      else if (this.IsThrowToLeftSide())
        this.PlayThrowLocation_Left_Flat();
      else if (this.IsThrowToCenter())
        this.PlayThrowLocation_Short_Center();
      else
        this.PlayThrowLocation_Right_Flat();
    }
    else
      this.PlayThrowLocation_Short_Generic();
  }

  private void PlayThrowLocation_Generic()
  {
    if (this.IsThrowToSideline())
      this.PlayThrowLocation_Sideline_Generic();
    else if (this.IsThrowToLeftSide())
      this.PlayThrowLocation_Left_Generic();
    else if (this.IsThrowToCenter())
      this.PlayThrowLocation_Center_Generic();
    else
      this.PlayThrowLocation_Right_Generic();
  }

  private bool IsThrowToSideline() => (double) Mathf.Abs(MatchManager.instance.playersManager.passDestination.x) >= 9.0;

  private bool IsThrowToLeftSide()
  {
    float x = MatchManager.instance.playersManager.passDestination.x;
    return (double) x < -4.5 && (double) x > -9.0;
  }

  private bool IsThrowToCenter() => (double) Mathf.Abs(MatchManager.instance.playersManager.passDestination.x) <= 4.5;

  private bool IsThrowToRightSide()
  {
    float x = MatchManager.instance.playersManager.passDestination.x;
    return (double) x > 4.5 && (double) x < 9.0;
  }

  private bool IsThrowToFirstDownMarker()
  {
    if (SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.activeInHierarchy)
    {
      float num = MatchManager.instance.playersManager.passDestination.z - SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownTrans.position.z;
      if ((double) num > 0.0 && (double) num < 4.0)
        return true;
    }
    return false;
  }

  public void PlayQBDroppingBack()
  {
    if (Random.Range(0, 100) < 30 || (Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    AudioPath playQbDroppingBack = this.activePlay_QBDroppingBack;
    int index = Random.Range(0, playQbDroppingBack.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playQbDroppingBack, index);
    if (index < 6)
      this.PlayPlayerID_QB(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayPlayAction()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    AudioPath activePlayPlayAction = this.activePlay_playAction;
    int index = Random.Range(0, activePlayPlayAction.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(activePlayPlayAction, index);
    if (index < 3)
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    else if (index < 5)
      this.PlayPlayerID_QB(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayQBInPocket()
  {
    if (Random.Range(0, 100) < 30 || (Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    this.PlayRandomClip(this.activePlay_QBInPocket);
  }

  public void PlayQBScrambling() => this.PlayRandomClip(this.activePlay_QBScrambling);

  private void PlayThrowType_Generic() => this.PlayRandomClip(this.activePlay_ThrowTypeGeneric);

  private void PlayThrowType_Bullet() => this.PlayRandomClip(this.activePlay_ThrowTypeBullet);

  private void PlayThrowType_Lob() => this.PlayRandomClip(this.activePlay_ThrowTypeLob);

  private void PlayThrowType_DeepPass() => this.PlayRandomClip(this.activePlay_ThrowTypeDeep);

  private void PlayThrowType_ScreenPass() => this.PlayRandomClip(this.activePlay_ThrowTypeScreen);

  private void PlayThrowLocation_Left_Generic() => this.PlayRandomClip(this.activePlay_ThrowLeftGeneric);

  private void PlayThrowLocation_Center_Generic() => this.PlayRandomClip(this.activePlay_ThrowCenterGeneric);

  private void PlayThrowLocation_Right_Generic() => this.PlayRandomClip(this.activePlay_ThrowRightGeneric);

  private void PlayThrowLocation_Sideline_Generic() => this.PlayRandomClip(this.activePlay_ThrowSidelineGeneric);

  private void PlayThrowLocation_Short_Generic() => this.PlayRandomClip(this.activePlay_ThrowShortGeneric);

  private void PlayThrowLocation_Short_Center() => this.PlayRandomClip(this.activePlay_ThrowShortCenter);

  private void PlayThrowLocation_Right_Flat() => this.PlayRandomClip(this.activePlay_ThrowRightFlat);

  private void PlayThrowLocation_Left_Flat() => this.PlayRandomClip(this.activePlay_ThrowLeftFlat);

  private void PlayThrowLocation_Short_Sideline() => this.PlayRandomClip(this.activePlay_ThrowShortSideline);

  private void PlayThrowLocation_Deep_Generic() => this.PlayRandomClip(this.activePlay_ThrowDeepGeneric);

  private void PlayThrowLocation_Deep_Left() => this.PlayRandomClip(this.activePlay_ThrowDeepLeft);

  private void PlayThrowLocation_Deep_Center() => this.PlayRandomClip(this.activePlay_ThrowDeepCenter);

  private void PlayThrowLocation_Deep_Right() => this.PlayRandomClip(this.activePlay_ThrowDeepRight);

  private void PlayThrowLocation_Deep_Sideline() => this.PlayRandomClip(this.activePlay_ThrowDeepSideline);

  private void PlayThrowLocation_Deep_Endzone() => this.PlayRandomClip(this.activePlay_ThrowDeepEndzone);

  private void PlayThrowLocation_EndZone_Generic() => this.PlayRandomClip(this.activePlay_ThrowEndzoneGeneric);

  private void PlayThrowLocation_EndZone_Left() => this.PlayRandomClip(this.activePlay_ThrowEndzoneLeft);

  private void PlayThrowLocation_EndZone_Right() => this.PlayRandomClip(this.activePlay_ThrowEndzoneRight);

  private void PlayThrowLocation_EndZone_Center() => this.PlayRandomClip(this.activePlay_ThrowEndzoneCenter);

  private void PlayThrowLocation_FirstDown() => this.PlayRandomClip(this.activePlay_ThrowFirstDown);

  public void PlayCompletedPass()
  {
    if (this.playedCommentOnPass)
      return;
    this.playedCommentOnPass = true;
    if (Random.Range(0, 100) < 15 || (Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    AudioPath playCompPassGeneric = this.activePlay_compPassGeneric;
    int index = Random.Range(0, playCompPassGeneric.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playCompPassGeneric, index);
    if (index < 8)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 18)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayer_Number(ballHolderScript.number, AudioAddition.Postfix);
    }
    else
    {
      this.PlayPlayerID(ballHolderScript, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayHandoff(bool toss)
  {
    if ((Object) this.commentary == (Object) null || Random.Range(0, 100) < 25)
      return;
    PlayerAI handOffTarget = MatchManager.instance.playManager.handOffTarget;
    int num = Random.Range(0, 100);
    if (toss)
    {
      if (!this.commentary.IsRunnerABack() || num < 30)
        this.PlayToss_Generic();
      else if (num < 65)
      {
        if (this.commentary.IsHandoffToLeftSide())
          this.PlayToss_Left();
        else
          this.PlayToss_Right();
      }
      else if (this.commentary.IsHandoffToWeakSide())
        this.PlayToss_Weak();
      else
        this.PlayToss_Strong();
    }
    else if (!this.commentary.IsRunnerABack() || num < 25)
      this.PlayHandoff_Generic();
    else if (num < 50)
    {
      if (this.commentary.IsHandoffToLeftSide())
        this.PlayHandoff_Left();
      else
        this.PlayHandoff_Right();
    }
    else if (num < 75)
    {
      if (this.commentary.IsHandoffToWeakSide())
        this.PlayHandoff_Weak();
      else
        this.PlayHandoff_Strong();
    }
    else if (this.commentary.IsHandoffInside())
      this.PlayHandoff_Inside();
    else
      this.PlayHandoff_Outside();
  }

  private void PlayHandoff_Generic()
  {
    PlayerAI handOffTarget = MatchManager.instance.playManager.handOffTarget;
    AudioPath qbHandoffGeneric = this.activePlay_QBHandoffGeneric;
    int index = Random.Range(0, qbHandoffGeneric.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(qbHandoffGeneric, index);
    if (index < 6)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 11)
    {
      this.PlayPlayerID_QB(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index < 16)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayer_Number(handOffTarget.number, AudioAddition.Postfix);
    }
    else
    {
      this.PlayPlayerID(handOffTarget, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayHandoff_Left() => this.PlayHandoffType(this.activePlay_QBHandoffLeft);

  private void PlayHandoff_Right() => this.PlayHandoffType(this.activePlay_QBHandoffRight);

  private void PlayHandoff_Strong() => this.PlayHandoffType(this.activePlay_QBHandoffStrong);

  private void PlayHandoff_Weak() => this.PlayHandoffType(this.activePlay_QBHandoffWeak);

  private void PlayHandoff_Inside() => this.PlayHandoffType(this.activePlay_QBHandoffInside);

  private void PlayHandoff_Outside() => this.PlayHandoffType(this.activePlay_QBHandoffInside);

  private void PlayHandoffType(AudioPath audioPath)
  {
    PlayerAI handOffTarget = MatchManager.instance.playManager.handOffTarget;
    int index = Random.Range(0, audioPath.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(audioPath, index);
    if (index < 4)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 7)
    {
      this.PlayPlayerID_QB(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index < 10)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayer_Number(handOffTarget.number, AudioAddition.Postfix);
    }
    else
    {
      this.PlayPlayerID(handOffTarget, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayToss_Generic() => this.PlayTossType(this.activePlay_QBTossGeneric);

  private void PlayToss_Left() => this.PlayTossType(this.activePlay_QBTossLeft);

  private void PlayToss_Right() => this.PlayTossType(this.activePlay_QBTossRight);

  private void PlayToss_Strong() => this.PlayTossType(this.activePlay_QBTossStrong);

  private void PlayToss_Weak() => this.PlayTossType(this.activePlay_QBTossWeak);

  private void PlayTossType(AudioPath audioPath)
  {
    PlayerAI handOffTarget = MatchManager.instance.playManager.handOffTarget;
    int index = Random.Range(0, audioPath.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(audioPath, index);
    if (index < 7)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 11)
    {
      this.PlayPlayerID_QB(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayer_Number(handOffTarget.number, AudioAddition.Postfix);
    }
  }

  public void PlayBrokenTackle()
  {
    if (Random.Range(0, 100) < 40 || (Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    this.PlayRandomClip(this.activePlay_TackleBroken);
  }

  public void PlayDiving() => Random.Range(0, 100);

  public void PlayBlitz()
  {
    if ((Object) this.commentary == (Object) null || this.commentary.IsPlaying() || Random.Range(0, 100) < 50)
      return;
    AudioPath activePlayBlitz = this.activePlay_blitz;
    int index = Random.Range(0, activePlayBlitz.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(activePlayBlitz, index);
    if (index < 9)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamID_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayFumble()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    this.commentary.StopCurrentClip();
    this.PlayRandomClip(this.activePlay_fumble);
  }

  public void PlayInterception(PlayerAI defender)
  {
    if ((Object) this.commentary == (Object) null)
      return;
    this.commentary.StopCurrentClip();
    AudioPath playInterception = this.activePlay_interception;
    int index = Random.Range(0, playInterception.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playInterception, index);
    if (index < 5)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayer_Number(defender.number, AudioAddition.Postfix);
    }
  }

  public void PlayKickFieldGoalOrPAT()
  {
    if (this.commentary.IsPlaying())
      return;
    AudioPath fieldGoalExtraPoint = this.activePlay_fieldGoalExtraPoint;
    int index = Random.Range(0, fieldGoalExtraPoint.count) + 1;
    PlayerAI player = !global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curCompScriptRef[6] : MatchManager.instance.playersManager.curUserScriptRef[6];
    AudioClip audioClip = AudioManager.self.GetAudioClip(fieldGoalExtraPoint, index);
    if (index < 6)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(player, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayPuntBall()
  {
    if (this.commentary.IsPlaying())
      return;
    AudioPath activePlayPuntsBall = this.activePlay_puntsBall;
    int index = Random.Range(0, activePlayPuntsBall.count) + 1;
    PlayerAI player = !global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curCompScriptRef[6] : MatchManager.instance.playersManager.curUserScriptRef[6];
    AudioClip audioClip = AudioManager.self.GetAudioClip(activePlayPuntsBall, index);
    if (index < 3)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(player, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayPuntFielded()
  {
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    if (this.commentary.IsPlaying() || (Object) ballHolderScript == (Object) null)
      return;
    AudioPath activePlayPuntFielded = this.activePlay_puntFielded;
    int index1 = Random.Range(0, activePlayPuntFielded.count) + 1;
    AudioClip audioClip1 = AudioManager.self.GetAudioClip(activePlayPuntFielded, index1);
    int lineByFieldLocation = Field.GetYardLineByFieldLocation(ballHolderScript.trans.position.z);
    if (index1 < 4 && lineByFieldLocation > 0)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip1));
      this.PlayWord_AtThe();
      this.PlayNumber_Yardline(ballHolderScript.trans.position.z);
    }
    else
    {
      int index2 = Random.Range(4, activePlayPuntFielded.count) + 1;
      AudioClip audioClip2 = AudioManager.self.GetAudioClip(activePlayPuntFielded, index2);
      this.PlayPlayerID(ballHolderScript, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip2));
    }
  }

  public void PlayPuntOutOfBounds()
  {
    if (this.commentary.IsPlaying())
      return;
    AudioPath playPuntOutOfBounds = this.activePlay_puntOutOfBounds;
    int index = Random.Range(0, playPuntOutOfBounds.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playPuntOutOfBounds, index);
    if (index < 5)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayNumber_Yardline(Ball.State.BallPosition.z);
    }
  }

  public void PlayPuntGoesIntoEndzone()
  {
    if (this.commentary.IsPlaying())
      return;
    this.PlayRandomClip(this.activePlay_puntIntoEndzone);
  }

  public void PlayKickoffKicked(Vector3 kickDestination, float kickAngle)
  {
    this.playedFirstKickoff = true;
    if (this.commentary.IsPlaying())
      return;
    PlayerAI kicker = !global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.curCompScriptRef[6] : MatchManager.instance.playersManager.curUserScriptRef[6];
    if (MatchManager.instance.onsideKick)
    {
      this.PlayKickoff_Generic(kicker);
    }
    else
    {
      int lineByFieldLocation = Field.GetYardLineByFieldLocation(kickDestination.z);
      if (!this.playedFirstKickoff)
      {
        this.PlayKickoff_Generic(kicker);
        this.PlayKickoff_FirstOfGame();
      }
      else if ((double) kickAngle < 35.0 && Random.Range(0, 100) < 75)
        this.PlayKickoff_Squib(kicker);
      else if (lineByFieldLocation > 20 && Random.Range(0, 100) < 75)
        this.PlayKickoff_Short(kicker);
      else if (lineByFieldLocation < 0 && Random.Range(0, 100) < 75)
        this.PlayKickoff_Endzone(kicker);
      else if ((double) kickDestination.x < -6.0 && Random.Range(0, 100) < 75)
        this.PlayKickoff_Left(kicker);
      else if ((double) kickDestination.x > 6.0 && Random.Range(0, 100) < 75)
        this.PlayKickoff_Right(kicker);
      else
        this.PlayKickoff_Generic(kicker);
    }
  }

  public void PlayKickoff_Generic(PlayerAI kicker)
  {
    AudioPath playKickoffGeneric = this.activePlay_kickoffGeneric;
    int index = Random.Range(0, playKickoffGeneric.count) + 1;
    if (!this.playedFirstKickoff)
      index = Random.Range(7, 9);
    AudioClip audioClip = AudioManager.self.GetAudioClip(playKickoffGeneric, index);
    if (index < 7)
    {
      this.PlayPlayerID(kicker, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  private void PlayKickoff_Left(PlayerAI kicker)
  {
    this.PlayPlayerID(kicker, AudioAddition.Prefix);
    this.PlayRandomClip(this.activePlay_kickoffLeft);
  }

  private void PlayKickoff_Right(PlayerAI kicker)
  {
    this.PlayPlayerID(kicker, AudioAddition.Prefix);
    this.PlayRandomClip(this.activePlay_kickoffRight);
  }

  private void PlayKickoff_Endzone(PlayerAI kicker)
  {
    AudioPath playKickoffEndzone = this.activePlay_kickoffEndzone;
    int index = Random.Range(0, playKickoffEndzone.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playKickoffEndzone, index);
    if (index < 7)
    {
      this.PlayPlayerID(kicker, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  private void PlayKickoff_Short(PlayerAI kicker)
  {
    AudioPath playKickoffShort = this.activePlay_kickoffShort;
    int index = Random.Range(0, playKickoffShort.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playKickoffShort, index);
    if (index < 4)
    {
      this.PlayPlayerID(kicker, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  private void PlayKickoff_Squib(PlayerAI kicker)
  {
    this.PlayPlayerID(kicker, AudioAddition.Prefix);
    this.PlayRandomClip(this.activePlay_kickoffSquib);
  }

  private void PlayKickoff_FirstOfGame()
  {
    AudioPath kickoffStartOfGame = this.activePlay_kickoffStartOfGame;
    int index = Random.Range(1, 6);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(kickoffStartOfGame, index)));
  }

  public void PlayKickoffOutOfBounds()
  {
    if (this.commentary.IsPlaying())
      return;
    this.PlayRandomClip(this.activePlay_kickoffOutOfBounds);
  }

  public void PlayKickoffFielded()
  {
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    if (this.commentary.IsPlaying())
      return;
    AudioPath playKickoffFielded = this.activePlay_kickoffFielded;
    int index1 = Random.Range(0, playKickoffFielded.count) + 1;
    AudioClip audioClip1 = AudioManager.self.GetAudioClip(playKickoffFielded, index1);
    int num = 0;
    if ((Object) ballHolderScript != (Object) null)
      num = Field.GetYardLineByFieldLocation(ballHolderScript.trans.position.z);
    if (index1 < 4 && num > 0)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip1));
      this.PlayWord_AtThe();
      this.PlayNumber_Yardline(ballHolderScript.trans.position.z);
    }
    else
    {
      int index2 = Random.Range(4, playKickoffFielded.count) + 1;
      AudioClip audioClip2 = AudioManager.self.GetAudioClip(playKickoffFielded, index2);
      this.PlayPlayerID(ballHolderScript, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip2));
    }
  }

  public void PlayOnsideKickNotSuccessful() => this.PlayRandomClip(this.activePlay_kickoffOnsideNotRec);

  public void PlayOnsideKickSuccessful()
  {
    if (this.commentary.IsPlaying())
      return;
    AudioPath kickoffOnsideRec = this.activePlay_kickoffOnsideRec;
    int index = Random.Range(0, kickoffOnsideRec.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(kickoffOnsideRec, index)));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Prefix);
  }

  public void PlayAfterCommercial()
  {
    this.commentary.StopCurrentClip();
    this.PlayRandomClip(this.afterCommercial);
  }

  public void PlayBeforeCommercial()
  {
    this.commentary.StopCurrentClip();
    this.PlayRandomClip(this.beforeCommercial);
  }

  public void PlayCommentAfterPlaySelection()
  {
    if ((Object) this.commentary == (Object) null || this.commentary.IsPlaying() || Random.Range(0, 100) < 40)
      return;
    FormationPositions formation1 = MatchManager.instance.playManager.savedOffPlay.GetFormation();
    FormationPositionsDef formation2 = MatchManager.instance.playManager.savedDefPlay.GetFormation();
    int lineByFieldLocation = Field.GetYardLineByFieldLocation(ProEra.Game.MatchState.BallOn.Value);
    int num1 = Random.Range(0, 100);
    if ((bool) ProEra.Game.MatchState.RunningPat)
    {
      if (formation1 == Plays.self.fieldGoalForm)
        this.BeforePlay_ExtraPointSelection();
      else
        this.BeforePlay_TwoPointConvSelection();
    }
    else if (formation1 == Plays.self.fieldGoalForm)
    {
      if (num1 < 35)
        this.BeforePlay_FGSelection_Generic();
      else if (lineByFieldLocation > 30)
        this.BeforePlay_FGSelection_Long();
      else if (lineByFieldLocation < 15)
        this.BeforePlay_FGSelection_Short();
      else
        this.BeforePlay_FGSelection_Generic();
    }
    else if (formation1 == Plays.self.puntForm)
      this.BeforePlay_PuntSelection();
    else if (formation1 == Plays.self.kickoffForm)
      this.BeforePlay_KickoffSelection();
    else if (formation1 == Plays.self.onsideKickForm)
      this.BeforePlay_OnsideKickSelection();
    else if (ProEra.Game.MatchState.Stats.CurrentDrivePlays == 0 && num1 < 75)
    {
      this.BeforePlay_PlayFirstPlayOfDrive();
    }
    else
    {
      int num2 = Random.Range(0, 100);
      int backsInFormation1 = formation2.GetDefensiveBacksInFormation();
      if (num1 < 10)
        this.BeforePlay_PlayXPlayOfDrive();
      else if (num1 < 40)
      {
        int receiversInFormation = formation1.GetReceiversInFormation();
        int backsInFormation2 = formation1.GetBacksInFormation();
        bool flag1 = formation1.IsTELeft();
        bool flag2 = formation1.IsTERight();
        bool flag3 = formation1.IsQBUnderCenter();
        if ((global::Game.IsPlayerOneOnOffense ? (this._playbookP1.PlayFlipped ? 1 : 0) : (this._playbookP2.PlayFlipped ? 1 : 0)) != 0)
        {
          int num3 = flag1 ? 1 : 0;
          flag1 = flag2;
          flag2 = num3 != 0;
        }
        if (receiversInFormation > 0 && num2 < 25)
        {
          switch (receiversInFormation)
          {
            case 2:
              this.BeforePlay_PlayTwoReceiverSet();
              break;
            case 3:
              this.BeforePlay_PlayThreeReceiverSet();
              break;
            case 4:
              this.BeforePlay_PlayFourReceiverSet();
              break;
            case 5:
              this.BeforePlay_PlayFiveReceiverSet();
              break;
          }
        }
        else if (backsInFormation2 > 0 && num2 < 50)
        {
          switch (backsInFormation2)
          {
            case 1:
              this.BeforePlay_PlayOneBackSet();
              break;
            case 2:
              this.BeforePlay_PlayTwoBackSet();
              break;
            case 3:
              this.BeforePlay_PlayThreeBackSet();
              break;
          }
        }
        else if (flag1 | flag2 && num2 < 80)
        {
          if (flag1 & flag2)
            this.BeforePlay_PlayDoubleTESet();
          else if (flag1)
            this.BeforePlay_PlayTELeft();
          else
            this.BeforePlay_PlayTERight();
        }
        else if (flag3)
          this.BeforePlay_PlayUnderCenter();
        else
          this.BeforePlay_PlayShotgunSet();
      }
      else if (backsInFormation1 > 4 && num1 < 50)
      {
        if (backsInFormation1 == 5)
          this.BeforePlay_PlayFiveDefensiveBacks();
        else
          this.BeforePlay_PlaySixDefensiveBacks();
      }
      else
      {
        int yards = Field.ConvertDistanceToYards(Field.FindDifference(ProEra.Game.MatchState.FirstDown.Value, ProEra.Game.MatchState.BallOn.Value));
        if (ProEra.Game.MatchState.Down.Value == 1)
        {
          if (SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.activeInHierarchy)
            this.BeforePlay_FirstDown_Generic();
          else
            this.BeforePlay_FirstDown_Goal();
        }
        else if (ProEra.Game.MatchState.Down.Value == 2)
        {
          if (SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.activeInHierarchy)
          {
            if (yards > 12)
              this.BeforePlay_SecondDown_Long();
            else if (yards < 4)
              this.BeforePlay_SecondDown_Short();
            else
              this.BeforePlay_SecondDown_Generic();
          }
          else
            this.BeforePlay_SecondDown_Goal();
        }
        else if (ProEra.Game.MatchState.Down.Value == 3)
        {
          if (SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.activeInHierarchy)
          {
            if (yards > 12)
              this.BeforePlay_ThirdDown_Long();
            else if (yards < 4)
              this.BeforePlay_ThirdDown_Short();
            else
              this.BeforePlay_ThirdDown_Generic();
          }
          else
            this.BeforePlay_ThirdDown_Goal();
        }
        else if (SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.activeInHierarchy)
        {
          if (yards > 12)
            this.BeforePlay_FourthDown_Long();
          else if (yards < 4)
            this.BeforePlay_FourthDown_Short();
          else
            this.BeforePlay_FourthDown_Generic();
        }
        else
          this.BeforePlay_FourthDown_Goal();
      }
    }
  }

  public void BeforePlay_PlayTwoReceiverSet()
  {
    AudioPath playTwoReceiverSet = this.beforePlay_twoReceiverSet;
    int index = Random.Range(0, playTwoReceiverSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playTwoReceiverSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayThreeReceiverSet()
  {
    AudioPath threeReceiverSet = this.beforePlay_threeReceiverSet;
    int index = Random.Range(0, threeReceiverSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(threeReceiverSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayFourReceiverSet()
  {
    AudioPath beforePlayFourRecSet = this.beforePlay_fourRecSet;
    int index = Random.Range(0, beforePlayFourRecSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayFourRecSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayFiveReceiverSet()
  {
    AudioPath beforePlayFiveRecSet = this.beforePlay_fiveRecSet;
    int index = Random.Range(0, beforePlayFiveRecSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayFiveRecSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayOneBackSet()
  {
    AudioPath beforePlayOneBackSet = this.beforePlay_oneBackSet;
    int index = Random.Range(0, beforePlayOneBackSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayOneBackSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayTwoBackSet()
  {
    AudioPath beforePlayTwoBackSet = this.beforePlay_twoBackSet;
    int index = Random.Range(0, beforePlayTwoBackSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayTwoBackSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayThreeBackSet()
  {
    AudioPath playThreeBackSet = this.beforePlay_threeBackSet;
    int index = Random.Range(0, playThreeBackSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playThreeBackSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayTERight()
  {
    AudioPath beforePlayTeRight = this.beforePlay_TERight;
    int index = Random.Range(0, beforePlayTeRight.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(beforePlayTeRight, index)));
    if (index != 3)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_PlayTELeft()
  {
    AudioPath beforePlayTeLeft = this.beforePlay_TELeft;
    int index = Random.Range(0, beforePlayTeLeft.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(beforePlayTeLeft, index)));
    if (index != 3)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_PlayDoubleTESet()
  {
    AudioPath beforePlayDoubleTeSet = this.beforePlay_doubleTESet;
    int index = Random.Range(0, beforePlayDoubleTeSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayDoubleTeSet, index);
    if (index < 4)
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayShotgunSet()
  {
    AudioPath beforePlayShotgunSet = this.beforePlay_shotgunSet;
    int index = Random.Range(0, beforePlayShotgunSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayShotgunSet, index);
    if (index > 1)
    {
      if (Random.Range(0, 100) < 60)
        this.PlayTeamID_Offense(AudioAddition.Prefix);
      else
        this.PlayPlayerID_QB(AudioAddition.Prefix);
    }
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  private void BeforePlay_PlayUnderCenter()
  {
    AudioPath playUnderCenterSet = this.beforePlay_underCenterSet;
    int index = Random.Range(0, playUnderCenterSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playUnderCenterSet, index);
    if (index > 1)
      this.PlayPlayerID_QB(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  private void BeforePlay_PlayFiveDefensiveBacks()
  {
    AudioPath beforePlayFiveDbSet = this.beforePlay_fiveDBSet;
    int index = Random.Range(0, beforePlayFiveDbSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayFiveDbSet, index);
    if (index < 3)
    {
      this.PlayTeamID_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 3)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Defense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlaySixDefensiveBacks()
  {
    AudioPath beforePlayFiveDbSet = this.beforePlay_fiveDBSet;
    int index = Random.Range(0, beforePlayFiveDbSet.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(beforePlayFiveDbSet, index);
    if (index < 3)
    {
      this.PlayTeamID_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 3)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Defense(AudioAddition.Postfix);
    }
  }

  private void BeforePlay_PlayFirstPlayOfDrive()
  {
    AudioPath firstPlayOfDrive = this.beforePlay_firstPlayOfDrive;
    int index = Random.Range(0, firstPlayOfDrive.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(firstPlayOfDrive, index);
    if (index < 6)
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    this.PlayNumber_Yardline(ProEra.Game.MatchState.BallOn.Value);
  }

  private void BeforePlay_PlayXPlayOfDrive()
  {
    AudioPath playPlayOfTheDrive = this.beforePlay_playOfTheDrive;
    int index = Random.Range(0, playPlayOfTheDrive.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playPlayOfTheDrive, index);
    int number = ProEra.Game.MatchState.Stats.CurrentDrivePlays + 1;
    if (number >= 21)
      return;
    this.PlayNumber_Ordinal(number);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_FirstDown_Generic()
  {
    AudioPath firstDownGeneric = this.beforePlay_firstDownGeneric;
    int index = Random.Range(0, firstDownGeneric.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(firstDownGeneric, index)));
  }

  private void BeforePlay_FirstDown_Goal()
  {
    AudioPath playFirstDownGoal = this.beforePlay_FirstDownGoal;
    int index = Random.Range(0, playFirstDownGoal.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playFirstDownGoal, index)));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_SecondDown_Generic()
  {
    AudioPath secondDownGeneric = this.beforePlay_secondDownGeneric;
    int index = Random.Range(0, secondDownGeneric.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(secondDownGeneric, index)));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_SecondDown_Short() => this.PlayRandomClip(this.beforePlay_secondDownShort);

  private void BeforePlay_SecondDown_Long() => this.PlayRandomClip(this.beforePlay_secondDownLong);

  private void BeforePlay_SecondDown_Goal()
  {
    AudioPath playSecondDownGoal = this.beforePlay_secondDownGoal;
    int index = Random.Range(0, playSecondDownGoal.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playSecondDownGoal, index)));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_ThirdDown_Generic() => this.PlayRandomClip(this.beforePlay_thirdDownGeneric);

  private void BeforePlay_ThirdDown_Short() => this.PlayRandomClip(this.beforePlay_thirdDownShort);

  private void BeforePlay_ThirdDown_Long() => this.PlayRandomClip(this.beforePlay_thirdDownLong);

  private void BeforePlay_ThirdDown_Goal()
  {
    AudioPath playThirdDownGoal = this.beforePlay_thirdDownGoal;
    int index = Random.Range(0, playThirdDownGoal.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playThirdDownGoal, index)));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_FourthDown_Generic()
  {
    AudioPath fourthDownGeneric = this.beforePlay_fourthDownGeneric;
    int index = Random.Range(0, fourthDownGeneric.count) + 1;
    if (index < 3)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(fourthDownGeneric, index)));
  }

  private void BeforePlay_FourthDown_Short() => this.PlayRandomClip(this.beforePlay_fourthDownShort);

  private void BeforePlay_FourthDown_Long() => this.PlayRandomClip(this.beforePlay_fourthDownLong);

  private void BeforePlay_FourthDown_Goal()
  {
    AudioPath playFourthDownGoal = this.beforePlay_fourthDownGoal;
    int index = Random.Range(0, playFourthDownGoal.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playFourthDownGoal, index)));
    if (index != 2)
      return;
    this.PlayTeamID_Offense(AudioAddition.Postfix);
  }

  private void BeforePlay_PuntSelection()
  {
    AudioPath beforePlayPuntSelect = this.beforePlay_puntSelect;
    int index = Random.Range(0, beforePlayPuntSelect.count) + 1;
    if (index < 3)
      this.PlayOffensiveTeamCity_Prefix();
    else if (index == 3)
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(beforePlayPuntSelect, index)));
  }

  private void BeforePlay_FGSelection_Generic()
  {
    AudioPath playFgSelectGeneric = this.beforePlay_FGSelectGeneric;
    int index = Random.Range(0, playFgSelectGeneric.count) + 1;
    if (index == 1)
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    else if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playFgSelectGeneric, index)));
  }

  private void BeforePlay_FGSelection_Short()
  {
    AudioPath playFgSelectShort = this.beforePlay_FGSelectShort;
    int index = Random.Range(0, playFgSelectShort.count) + 1;
    if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playFgSelectShort, index)));
  }

  private void BeforePlay_FGSelection_Long()
  {
    AudioPath playFgSelectLong = this.beforePlay_FGSelectLong;
    int index = Random.Range(0, playFgSelectLong.count) + 1;
    if (index < 5)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playFgSelectLong, index)));
  }

  private void BeforePlay_ExtraPointSelection()
  {
    AudioPath extraPointSelect = this.beforePlay_extraPointSelect;
    int index = Random.Range(0, extraPointSelect.count) + 1;
    if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(extraPointSelect, index)));
  }

  private void BeforePlay_TwoPointConvSelection()
  {
    AudioPath playTwoPointSelect = this.beforePlay_twoPointSelect;
    int index = Random.Range(0, playTwoPointSelect.count) + 1;
    if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playTwoPointSelect, index)));
  }

  private void BeforePlay_KickoffSelection()
  {
    AudioPath playKickoffSelect = this.beforePlay_kickoffSelect;
    int index = Random.Range(0, playKickoffSelect.count) + 1;
    if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playKickoffSelect, index)));
  }

  private void BeforePlay_OnsideKickSelection()
  {
    AudioPath onsideKickSelect = this.beforePlay_onsideKickSelect;
    int index = Random.Range(0, onsideKickSelect.count) + 1;
    if (index < 3)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(onsideKickSelect, index)));
  }

  public void BeforePlay_AudibleSelection()
  {
    this.commentary.ClearQueue();
    this.commentary.AddPause();
    AudioPath beforePlayAudible = this.beforePlay_audible;
    int index = Random.Range(0, beforePlayAudible.count) + 1;
    if (index < 4)
      this.PlayPlayerID_QB(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(beforePlayAudible, index)));
  }

  public void BeforePlay_HotRouteSelection()
  {
    this.commentary.ClearQueue();
    this.commentary.AddPause();
    AudioPath beforePlayHotRoute = this.beforePlay_hotRoute;
    int index = Random.Range(0, beforePlayHotRoute.count) + 1;
    if (index < 4)
      this.PlayPlayerID_QB(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(beforePlayHotRoute, index)));
  }

  public void BeforePlay_Motion_Generic(PlayerAI player)
  {
    this.PlayPlayerID(player, AudioAddition.Prefix);
    this.PlayRandomClip(this.beforePlay_motionGeneric);
  }

  public void BeforePlay_Motion_Left(PlayerAI player)
  {
    this.PlayPlayerID(player, AudioAddition.Prefix);
    this.PlayRandomClip(this.beforePlay_motionLeft);
  }

  public void BeforePlay_Motion_Right(PlayerAI player)
  {
    this.PlayPlayerID(player, AudioAddition.Prefix);
    this.PlayRandomClip(this.beforePlay_motionRight);
  }

  public void BeforePlay_ShiftDefensiveLine()
  {
    if ((Object) this.commentary == (Object) null || this.commentary.IsPlaying() || this.playedCommentOnShift)
      return;
    this.playedCommentOnShift = true;
    if (Random.Range(0, 100) >= 30)
      return;
    this.PlayRandomClip(this.beforePlay_shiftingDLine);
  }

  public void BeforePlay_ShiftLinebackers()
  {
    if (this.commentary.IsPlaying() || this.playedCommentOnShift)
      return;
    this.playedCommentOnShift = true;
    if (Random.Range(0, 100) >= 30)
      return;
    AudioPath beforePlayShiftingLb = this.beforePlay_shiftingLB;
    int index = Random.Range(0, beforePlayShiftingLb.count) + 1;
    if (index == 6)
      this.PlayTeamID_Defense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(beforePlayShiftingLb, index)));
  }

  public void BeforePlay_ShiftingDB_Back()
  {
    if (this.commentary.IsPlaying() || this.playedCommentOnShift)
      return;
    this.playedCommentOnShift = true;
    if (Random.Range(0, 100) >= 30)
      return;
    AudioPath playShiftingDbBack = this.beforePlay_shiftingDBBack;
    int index = Random.Range(0, playShiftingDbBack.count) + 1;
    if (index > 4)
      this.PlayTeamID_Defense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playShiftingDbBack, index)));
  }

  public void BeforePlay_ShiftingDB_Up()
  {
    if (this.commentary.IsPlaying() || this.playedCommentOnShift)
      return;
    this.playedCommentOnShift = true;
    if (Random.Range(0, 100) >= 30)
      return;
    AudioPath playShiftingDbUp = this.beforePlay_shiftingDBUp;
    int index = Random.Range(0, playShiftingDbUp.count) + 1;
    if (index > 4)
      this.PlayTeamID_Defense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(playShiftingDbUp, index)));
  }

  public void PlayCalldownToReporter() => this.PlayRandomClip(this.calldownToReporter);

  public void PlayCallingTimeout(bool userTeam, int timeOutsLeft)
  {
    if (userTeam)
    {
      if (PersistentData.userIsHome)
        this.PlayHomeTeamCity_Prefix();
      else
        this.PlayAwayTeamCity_Prefix();
    }
    else if (PersistentData.userIsHome)
      this.PlayAwayTeamCity_Prefix();
    else
      this.PlayHomeTeamCity_Prefix();
    switch (timeOutsLeft)
    {
      case 1:
        if (Random.Range(0, 100) < 75)
        {
          this.PlayRandomClip(this.callingTimeoutSecond);
          break;
        }
        this.PlayRandomClip(this.callingTimeoutGeneric);
        break;
      case 2:
        if (Random.Range(0, 100) < 75)
        {
          this.PlayRandomClip(this.callingTimeoutFirst);
          break;
        }
        this.PlayRandomClip(this.callingTimeoutGeneric);
        break;
      default:
        this.PlayRandomClip(this.callingTimeoutThird);
        break;
    }
  }

  public void PlayEndOfQuarter()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    this.commentary.ClearQueue();
    this.PlayEndOfQuarterClip(MatchManager.instance.timeManager.GetQuarter());
    this.commentary.AddPause();
    int score1 = ProEra.Game.MatchState.Stats.User.Score;
    int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
    if (score1 != score2)
    {
      if (score1 > score2)
        this.PlayTeamID_User(AudioAddition.Prefix);
      else
        this.PlayTeamID_Comp(AudioAddition.Prefix);
      this.PlayRandomClip(this.endOfQuarterOneTeamWinning);
      this.PlayScore();
    }
    else if (score1 == 0)
    {
      this.PlayRandomClip(this.endOfQuarterScoreZero);
    }
    else
    {
      this.PlayRandomClip(this.endOfQuarterTied);
      this.PlayScore();
    }
    this.commentary.ClearQueueAfterThis();
  }

  public void PlayPlayerOfGameIntro() => this.PlayRandomClip(this.endOfQuarter4);

  public void PlayEndOfGame()
  {
    this.commentary.AddPause();
    this.PlayRandomClip(this.endGameOutro);
    this.commentary.AddPause();
    this.PlayFinalScore_Generic();
    this.commentary.AddPause();
    this.PlayRandomClip(this.endGameFinalRemarks);
  }

  public void PlayInjuredPlayer()
  {
    this.commentary.AddPause();
    this.PlayRandomClip(this.injury);
  }

  public void PlayJoke_RainDrop()
  {
    this.commentary.AddPause();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.jokeResponses, 1)));
  }

  public void PlayJoke_IGotIt()
  {
    this.commentary.AddPause();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.jokeResponses, 2)));
  }

  public void PlayJoke_OldCar()
  {
    this.commentary.AddPause();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.jokeResponses, 3)));
  }

  public void PlayJoke_SackedFrozen()
  {
    this.commentary.AddPause();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.jokeResponses, 4)));
  }

  public void PlayJoke_BadJoke()
  {
    this.commentary.AddPause();
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.jokeResponses, 5)));
  }

  private void PlayRandomYardlinePhrase()
  {
    int gainOrLoss = this.commentary.DetermineGainOrLoss();
    int num = Random.Range(0, 100);
    if (gainOrLoss == -1 && Random.Range(0, 100) < 75)
    {
      if (num < 50)
        this.PlayWord_BackAtThe();
      else
        this.PlayWord_BackNearThe();
    }
    else if (gainOrLoss == 1 && Random.Range(0, 100) < 75)
    {
      if (num < 50)
        this.PlayWord_UpAtThe();
      else
        this.PlayWord_UpNearThe();
    }
    else if (num < 20)
      this.PlayWord_CloseToThe();
    else if (num < 40)
      this.PlayWord_AroundThe();
    else
      this.PlayWord_AtThe();
  }

  public void PlayWord_Number() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_nouns, 9)));

  public void PlayWord_Yardline() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_nouns, 2)));

  public void PlayWord_For() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_conjunctions, 3)));

  public void PlayWord_Nothing() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.numbers_misc, 1)));

  private void PlayWord_CloseToThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 7)));

  private void PlayWord_AroundThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 6)));

  public void PlayWord_AtThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 3)));

  private void PlayWord_UpNearThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 10)));

  private void PlayWord_UpAtThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 9)));

  private void PlayWord_BackNearThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 11)));

  private void PlayWord_BackAtThe() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 8)));

  private void PlayWord_ByNumber() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 12)));

  private void PlayWord_By() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_prepositions, 13)));

  private void PlayWord_YardsOut() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_phrases, 5)));

  public void PlayWord_TheOffense_Prefix() => this.PlayRandomClip(this.misc_offense_prefix);

  public void PlayWord_TheOffense_Postfix() => this.PlayRandomClip(this.misc_offense_postfix);

  public void PlayWord_TheDefense_Prefix() => this.PlayRandomClip(this.misc_defense_prefix);

  public void PlayWord_TheDefense_Postfix() => this.PlayRandomClip(this.misc_defense_postfix);

  public void PlayWord_TheHomeTeam_Prefix() => this.PlayRandomClip(this.misc_homeTeam_prefix);

  public void PlayWord_TheHomeTeam_Postfix() => this.PlayRandomClip(this.misc_homeTeam_postfix);

  public void PlayWord_TheAwayTeam_Prefix() => this.PlayRandomClip(this.misc_awayTeam_prefix);

  public void PlayWord_TheAwayTeam_Postfix() => this.PlayRandomClip(this.misc_awayTeam_postfix);

  public void PlayPlayer_Number(int number, AudioAddition type)
  {
    if (type == AudioAddition.Prefix)
      this.PlayNumberWithWord(number);
    else
      this.PlayNumber(number, type);
  }

  public void PlayNumber(int number, AudioAddition type)
  {
    if (number < 0 || number > 99)
      return;
    if (number == 0)
    {
      this.PlayWord_Nothing();
    }
    else
    {
      AudioPath audioPath = type != AudioAddition.Prefix ? this.numbers_1To99_postfix : this.numbers_1To99_prefix;
      this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(audioPath, number)));
    }
  }

  public void PlayNumberWithWord(int number)
  {
    AudioPath numbers1To99WithWord = this.numbers_1To99_withWord;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(numbers1To99WithWord, number)));
  }

  public void PlayNumber_Yardline(float zPositionOnField)
  {
    this.PlayNumber(Field.GetYardLineByFieldLocation(zPositionOnField), AudioAddition.Postfix);
    if (Random.Range(0, 100) >= 50)
      return;
    this.PlayWord_Yardline();
  }

  public void PlayNumber_Yardline()
  {
    if (global::Game.BallHolderIsNull)
      return;
    this.PlayNumber_Yardline(MatchManager.instance.playersManager.ballHolderScript.trans.position.z);
  }

  public void PlayNumber_Ordinal(int number)
  {
    AudioPath numbersOrdinal = this.numbers_ordinal;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(numbersOrdinal, number), 0.07f));
  }

  public void PlayFlagThrown() => this.PlayRandomClip(this.penalty_flagThrown);

  public void PlayFlagAfterPlay() => this.PlayRandomClip(this.penalty_afterPlay);

  public void PlayEndAddition_Penalty() => this.PlayRandomClip(this.penalty_playEndAddition);

  public void PlayDeclinePenalty()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    AudioPath penaltyDeclining = this.penalty_declining;
    int index = Random.Range(0, penaltyDeclining.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(penaltyDeclining, index);
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetOffenseOrDefense().Equals("Offense"))
    {
      if (index < 4)
        this.PlayDefensiveTeamCity_Prefix();
      else
        this.PlayTeamID_Defense(AudioAddition.Prefix);
    }
    else if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    else
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayAcceptPenalty()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    AudioPath penaltyAccepting = this.penalty_accepting;
    int index = Random.Range(0, penaltyAccepting.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(penaltyAccepting, index);
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetOffenseOrDefense().Equals("Offense"))
    {
      if (index == 1)
        this.PlayTeamID_Defense(AudioAddition.Prefix);
      else
        this.PlayDefensiveTeamCity_Prefix();
    }
    else if (index == 1)
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    else
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayTackled()
  {
    if ((Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    if ((Object) ballHolderScript == (Object) null)
      return;
    if (PlayState.PlayType.Value == PlayType.Kickoff || PlayState.PlayType.Value == PlayType.Punt)
      this.PlayTackle_AfterKick();
    else if ((bool) ProEra.Game.MatchState.Turnover)
    {
      this.PlayTackle_Generic();
    }
    else
    {
      float yards = (float) Field.ConvertDistanceToYards(Field.FindDifference(ballHolderScript.trans.position.z, ProEra.Game.MatchState.BallOn.Value));
      if ((double) ballHolderScript.trans.position.z < -0.5)
        this.PlayTackle_Safety();
      else if (PlayState.IsPass && !MatchManager.instance.playersManager.ballWasThrownOrKicked && (double) yards < 0.0)
      {
        if (ProEra.Game.MatchState.IsPlayerOneOnDefense)
        {
          if (ProEra.Game.MatchState.Stats.User.Sacks < 5)
            this.PlayTackle_Sack(yards);
          else
            this.PlayTackle_MultipleSacks(yards);
        }
        else if (ProEra.Game.MatchState.Stats.Comp.Sacks < 5)
          this.PlayTackle_Sack(yards);
        else
          this.PlayTackle_MultipleSacks(yards);
      }
      else
      {
        if (Random.Range(0, 100) >= 60)
          return;
        if ((double) yards < 0.0 && Random.Range(0, 100) < 30)
          this.PlayTackle_Generic();
        else if (PlayState.PlayType.Value == PlayType.Run)
        {
          if ((double) yards < 3.0)
          {
            this.PlayTackle_ShortRun();
          }
          else
          {
            if ((double) yards <= 12.0)
              return;
            this.PlayTackle_LongRun();
          }
        }
        else
          this.PlayTackle_Generic();
      }
    }
  }

  private void PlayTackle_Generic()
  {
    this.PlayRandomClip(this.playEnd_tackle_generic);
    this.AddYardlineOrDefender();
  }

  private void PlayTackle_LongRun()
  {
    this.PlayRandomClip(this.playEnd_tackle_longGainRun);
    this.AddYardlineOrDefender();
  }

  private void PlayTackle_ShortRun()
  {
    this.PlayRandomClip(this.playEnd_tackle_shortGainRun);
    this.AddYardlineOrDefender();
  }

  private void AddYardlineOrDefender()
  {
    if (Random.Range(0, 100) >= 60)
      return;
    if (Random.Range(0, 100) < 50)
    {
      this.PlayRandomYardlinePhrase();
      this.PlayNumber_Yardline();
    }
    else
      this.PlayByPlayer(this.commentary.defPlayerReference);
  }

  private void PlayTackle_Sack(float yardsGainedOnPlay)
  {
    int number = Mathf.Abs(Mathf.RoundToInt(yardsGainedOnPlay));
    AudioPath playEndTackleSack = this.playEnd_tackle_sack;
    int index = Random.Range(0, playEndTackleSack.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playEndTackleSack, index);
    if (index < 4)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayNumber(number, AudioAddition.Postfix);
    }
    else if (index < 7)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayNumber_Yardline();
    }
    else if (index < 10)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayer_Number(this.commentary.defPlayerReference.number, AudioAddition.Postfix);
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayNumber(number, AudioAddition.Postfix);
    }
  }

  private void PlayTackle_MultipleSacks(float yardsGainedOnPlay)
  {
    int number = Mathf.Abs(Mathf.RoundToInt(yardsGainedOnPlay));
    AudioPath tackleMultipleSacks = this.playEnd_tackle_multipleSacks;
    int index = Random.Range(0, tackleMultipleSacks.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(tackleMultipleSacks, index);
    if (index == 1)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayNumber(number, AudioAddition.Postfix);
    }
    else
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayNumber_Yardline();
    }
  }

  private void PlayTackle_AfterKick()
  {
    AudioPath endTackleAfterKick = this.playEnd_tackle_afterKick;
    int index = Random.Range(0, endTackleAfterKick.count) + 1;
    float tackledAtPos = this.commentary.tackledAtPos;
    int number = Mathf.RoundToInt((float) ((double) Mathf.Abs(MatchManager.instance.caughtPassOrKickAtPos - tackledAtPos) / 24.0 * 50.0));
    if (number < 0)
      index = 3;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(endTackleAfterKick, index)));
    if (index < 3)
      this.PlayNumber(number, AudioAddition.Postfix);
    else
      this.PlayNumber_Yardline();
  }

  private void PlayTackle_Safety()
  {
    AudioPath playEndTackleSafety = this.playEnd_tackle_safety;
    int index = Random.Range(0, playEndTackleSafety.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(playEndTackleSafety, index);
    if (index == 1)
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else if (index == 2)
    {
      this.PlayPlayerID_QB(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayRanOutOfBounds()
  {
    if ((Object) this.commentary == (Object) null || this.commentary.IsPlaying())
      return;
    this.PlayRandomClip(this.playEnd_runOutOfBounds);
    if (Random.Range(0, 100) >= 60)
      return;
    this.PlayRandomYardlinePhrase();
    this.PlayNumber_Yardline();
  }

  public void PlayQBSlide()
  {
    if (this.commentary.IsPlaying())
      return;
    this.PlayRandomClip(this.playEnd_QBSlide);
  }

  public void AddPlayEndingAddition()
  {
    if (PlayState.PlayType.Value != PlayType.Run && PlayState.PlayType.Value != PlayType.Pass || global::Game.BallHolderIsNotNull && (double) MatchManager.instance.playersManager.ballHolderScript.trans.position.z < -0.5 || (bool) ProEra.Game.MatchState.RunningPat || Random.Range(0, 100) < 25)
      return;
    PlayerAI ballHolderScript = MatchManager.instance.playersManager.ballHolderScript;
    int num = ProEra.Game.MatchState.Down.Value;
    if (this.commentary.GotFirstDownOnPlay())
      num = 1;
    float yardsGained1 = this.commentary.GetYardsGained();
    int yardsGained2 = Mathf.RoundToInt(yardsGained1);
    if (yardsGained2 > 0 && Random.Range(0, 100) < 50)
    {
      if ((double) yardsGained1 < 0.0)
      {
        if ((double) yardsGained1 < -3.0 || Random.Range(0, 100) < 40)
          this.PlayAddition_GenericLoss(yardsGained2);
        else
          this.PlayAddition_ShortLoss();
      }
      else if ((double) yardsGained1 > 3.5 || Random.Range(0, 100) < 40)
        this.PlayAddition_GenericGain(yardsGained2);
      else
        this.PlayAddition_ShortGain();
    }
    else
    {
      switch (num)
      {
        case 1:
          this.PlayAddition_FirstDown();
          break;
        case 2:
          this.PlayAddition_SecondDown();
          break;
        case 3:
          this.PlayAddition_ThirdDown();
          break;
        case 4:
          this.PlayAddition_FourthDown();
          break;
      }
    }
  }

  private void PlayAddition_GenericGain(int yardsGained)
  {
    this.PlayRandomClip(this.playEnd_addition_genericGain);
    this.PlayNumber(yardsGained, AudioAddition.Postfix);
  }

  private void PlayAddition_GenericLoss(int yardsGained)
  {
    this.PlayRandomClip(this.playEnd_addition_genericLoss);
    yardsGained = Mathf.Abs(yardsGained);
    this.PlayNumber(yardsGained, AudioAddition.Postfix);
  }

  private void PlayAddition_ShortGain() => this.PlayRandomClip(this.playEnd_addition_shortGain);

  private void PlayAddition_ShortLoss() => this.PlayRandomClip(this.playEnd_addition_shortLoss);

  private void PlayAddition_FirstDown() => this.PlayRandomClip(this.playEnd_addition_firstDown);

  private void PlayAddition_SecondDown() => this.PlayRandomClip(this.playEnd_addition_secondDown);

  private void PlayAddition_ThirdDown() => this.PlayRandomClip(this.playEnd_addition_thirdDown);

  private void PlayAddition_FourthDown() => this.PlayRandomClip(this.playEnd_addition_fourthDown);

  public void PlayIncompletePass()
  {
    if (this.playedCommentOnPass)
      return;
    this.playedCommentOnPass = true;
    this.PlayRandomClip(this.playEnd_inc_generic);
  }

  public void PlayDeflectedPass()
  {
    if (this.playedCommentOnPass)
      return;
    this.StartCoroutine(this.PlayDeflectedPass_Delay());
  }

  private IEnumerator PlayDeflectedPass_Delay()
  {
    yield return (object) this._deflectedPassDelay;
    if (!this.playedCommentOnPass && global::Game.BallHolderIsNull)
    {
      this.playedCommentOnPass = true;
      AudioPath playEndIncDeflected = this.playEnd_inc_deflected;
      int index = Random.Range(0, playEndIncDeflected.count) + 1;
      AudioClip audioClip = AudioManager.self.GetAudioClip(playEndIncDeflected, index);
      if (index > 8)
        this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.commentary.analyst.PlayDeflectedPass();
    }
  }

  public void PlayDroppedPass()
  {
    if (this.playedCommentOnPass)
      return;
    this.StartCoroutine(this.PlayDroppedPass_Delay());
  }

  private IEnumerator PlayDroppedPass_Delay()
  {
    yield return (object) this._deflectedPassDelay;
    if (!this.playedCommentOnPass && global::Game.BallHolderIsNull)
    {
      this.playedCommentOnPass = true;
      this.PlayRandomClip(this.playEnd_inc_dropped);
    }
  }

  public void PlayTouchdown()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    this.commentary.StopCurrentClip();
    int num = Random.Range(0, 100);
    if (PlayState.IsPass && MatchManager.instance.playersManager.ballWasThrownOrKicked && (double) MatchManager.instance.playersManager.passDestination.z > 48.0)
      this.PlayRandomClip(this.playEnd_touchdown_passing);
    else if (num < 50)
      this.PlayRandomClip(this.playEnd_touchdown_running);
    else
      this.PlayRandomClip(this.playEnd_touchdown_generic);
  }

  public void PlayMadeTwoPoint()
  {
    if (PlayState.IsPass && MatchManager.instance.playersManager.ballWasThrownOrKicked && (double) MatchManager.instance.playersManager.passDestination.z > 48.0)
      this.PlayRandomClip(this.playEnd_madeTwoPoint_Pass);
    else
      this.PlayRandomClip(this.playEnd_madeTwoPoint_Run);
  }

  public void PlayFailedTwoPoint()
  {
    AudioPath additionFailedTwoPoint = this.playEnd_addition_failedTwoPoint;
    if ((Object) this.commentary == (Object) null || additionFailedTwoPoint == null)
      return;
    int index = Random.Range(0, additionFailedTwoPoint.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(additionFailedTwoPoint, index);
    if (index < 4)
      this.PlayOffensiveTeamCity_Prefix();
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayFailedFourthDown()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    AudioPath failedFourthDown = this.playEnd_addition_failedFourthDown;
    int index = Random.Range(0, failedFourthDown.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(failedFourthDown, index);
    if (index < 4)
      this.PlayTeamID_Offense(AudioAddition.Prefix);
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
  }

  public void PlayMadeFieldGoal()
  {
    if ((bool) ProEra.Game.MatchState.RunningPat)
      this.PlayRandomClip(this.playEnd_madeExtraPoint);
    else if ((double) Mathf.Abs(Ball.State.BallPosition.x) > 1.25 && Random.Range(0, 100) < 70)
      this.PlayMadeFieldGoal_NearlyMissed();
    else
      this.PlayMadeFieldGoal_Generic();
  }

  private void PlayMadeFieldGoal_NearlyMissed()
  {
    AudioPath madeFgNearlyMissed = this.playEnd_madeFG_nearlyMissed;
    int index = Random.Range(0, madeFgNearlyMissed.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(madeFgNearlyMissed, index)));
    if (index != 1)
      return;
    this.PlayNumber(Field.ConvertDistanceToYards(Field.FindDifference(Field.OFFENSIVE_GOAL_LINE, ProEra.Game.MatchState.BallOn.Value)) + 17, AudioAddition.Postfix);
    this.PlayWord_YardsOut();
  }

  private void PlayMadeFieldGoal_Generic()
  {
    AudioPath endMadeFgGeneric = this.playEnd_madeFG_generic;
    int index = Random.Range(0, endMadeFgGeneric.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(endMadeFgGeneric, index)));
    if (index >= 5)
      return;
    this.PlayNumber(Field.ConvertDistanceToYards(Field.FindDifference(Field.OFFENSIVE_GOAL_LINE, ProEra.Game.MatchState.BallOn.Value)) + 17, AudioAddition.Postfix);
    this.PlayWord_YardsOut();
  }

  public void PlayMissedFieldGoal()
  {
    if ((double) Ball.State.BallPosition.y < 1.6000000238418579)
      this.PlayRandomClip(this.playEnd_missedFG_short);
    else if ((double) Mathf.Abs(Ball.State.BallPosition.x) < 1.8999999761581421)
      this.PlayRandomClip(this.playEnd_missedFG_nearlyMade);
    else if (Random.Range(0, 100) < 50)
      this.PlayRandomClip(this.playEnd_missedFG_wide);
    else
      this.PlayRandomClip(this.playEnd_missedFG_generic);
  }

  public void PlayPlayer_Name(bool userTeam, int indexOnTeam, AudioAddition type)
  {
    TeamData teamData = !userTeam ? MatchManager.instance.playersManager.compTeamData : MatchManager.instance.playersManager.userTeamData;
    AudioClip lastName = this.GetLastName(teamData.GetPlayer(indexOnTeam).LastName);
    if ((Object) lastName != (Object) null)
      this.commentary.AddSegmentToQueue(new AudioSegment(lastName));
    else
      this.PlayPlayer_Number(teamData.GetPlayer(indexOnTeam).Number, type);
  }

  public AudioClip GetLastName(string name)
  {
    AudioClip lastName;
    if (this.lastNames.ContainsKey(name))
    {
      lastName = this.lastNames[name];
    }
    else
    {
      lastName = AddressablesData.instance.LoadAssetSync<AudioClip>(AddressablesData.CorrectingAssetKey(this.rootFolder + "PLAYER_LAST_NAMES"), name);
      if ((Object) lastName != (Object) null)
        this.lastNames.Add(name, lastName);
    }
    return lastName;
  }

  public void PlayPlayer_Position(Position p, AudioAddition type)
  {
    AudioPath audioPath = (AudioPath) null;
    if (type == AudioAddition.Prefix)
    {
      switch (p)
      {
        case Position.QB:
          audioPath = this.positions_QB_prefix;
          break;
        case Position.RB:
          audioPath = this.positions_RB_prefix;
          break;
        case Position.FB:
          audioPath = this.positions_FB_prefix;
          break;
        case Position.WR:
          audioPath = this.positions_WR_prefix;
          break;
        case Position.SLT:
          audioPath = this.positions_SLT_prefix;
          break;
        case Position.TE:
          audioPath = this.positions_TE_prefix;
          break;
        case Position.OL:
          audioPath = this.positions_OL_prefix;
          break;
        case Position.LT:
          audioPath = this.positions_LT_prefix;
          break;
        case Position.LG:
          audioPath = this.positions_LG_prefix;
          break;
        case Position.C:
          audioPath = this.positions_C_prefix;
          break;
        case Position.RG:
          audioPath = this.positions_RG_prefix;
          break;
        case Position.RT:
          audioPath = this.positions_RT_prefix;
          break;
        case Position.K:
          audioPath = this.positions_K_prefix;
          break;
        case Position.P:
          audioPath = this.positions_P_prefix;
          break;
        case Position.KR:
          audioPath = this.positions_RET_prefix;
          break;
        case Position.PR:
          audioPath = this.positions_RET_prefix;
          break;
        case Position.DL:
          audioPath = this.positions_DL_prefix;
          break;
        case Position.DT:
          audioPath = this.positions_DT_prefix;
          break;
        case Position.NT:
          audioPath = this.positions_NT_prefix;
          break;
        case Position.DE:
          audioPath = this.positions_DE_prefix;
          break;
        case Position.LB:
          audioPath = this.positions_LB_prefix;
          break;
        case Position.OLB:
          audioPath = this.positions_OLB_prefix;
          break;
        case Position.ILB:
          audioPath = this.positions_ILB_prefix;
          break;
        case Position.MLB:
          audioPath = this.positions_MLB_prefix;
          break;
        case Position.CB:
          audioPath = this.positions_CB_prefix;
          break;
        case Position.FS:
          audioPath = this.positions_FS_prefix;
          break;
        case Position.SS:
          audioPath = this.positions_SS_prefix;
          break;
        case Position.DB:
          audioPath = this.positions_DB_prefix;
          break;
      }
    }
    else
    {
      switch (p)
      {
        case Position.QB:
          audioPath = this.positions_QB_postfix;
          break;
        case Position.RB:
          audioPath = this.positions_RB_postfix;
          break;
        case Position.FB:
          audioPath = this.positions_FB_postfix;
          break;
        case Position.WR:
          audioPath = this.positions_WR_postfix;
          break;
        case Position.SLT:
          audioPath = this.positions_SLT_postfix;
          break;
        case Position.TE:
          audioPath = this.positions_TE_postfix;
          break;
        case Position.OL:
          audioPath = this.positions_OL_postfix;
          break;
        case Position.LT:
          audioPath = this.positions_LT_postfix;
          break;
        case Position.LG:
          audioPath = this.positions_LG_postfix;
          break;
        case Position.C:
          audioPath = this.positions_C_postfix;
          break;
        case Position.RG:
          audioPath = this.positions_RG_postfix;
          break;
        case Position.RT:
          audioPath = this.positions_RT_postfix;
          break;
        case Position.K:
          audioPath = this.positions_K_postfix;
          break;
        case Position.P:
          audioPath = this.positions_P_postfix;
          break;
        case Position.KR:
          audioPath = this.positions_RET_postfix;
          break;
        case Position.PR:
          audioPath = this.positions_RET_postfix;
          break;
        case Position.DL:
          audioPath = this.positions_DL_postfix;
          break;
        case Position.DT:
          audioPath = this.positions_DT_postfix;
          break;
        case Position.NT:
          audioPath = this.positions_NT_postfix;
          break;
        case Position.DE:
          audioPath = this.positions_DE_postfix;
          break;
        case Position.LB:
          audioPath = this.positions_LB_postfix;
          break;
        case Position.OLB:
          audioPath = this.positions_OLB_postfix;
          break;
        case Position.ILB:
          audioPath = this.positions_ILB_postfix;
          break;
        case Position.MLB:
          audioPath = this.positions_MLB_postfix;
          break;
        case Position.CB:
          audioPath = this.positions_CB_postfix;
          break;
        case Position.FS:
          audioPath = this.positions_FS_postfix;
          break;
        case Position.SS:
          audioPath = this.positions_SS_postfix;
          break;
        case Position.DB:
          audioPath = this.positions_DB_postfix;
          break;
      }
    }
    if (audioPath == null)
      Debug.Log((object) ("No audio for position: " + p.ToString()));
    else
      this.PlayRandomClip(audioPath);
  }

  public void PlayPregameGeneric()
  {
    if (this.stadiumLocation == null || this.stadiumLocation.count == 0)
      this.PlayRandomClip(this.pregame_generic);
    else
      this.PlayRandomClip(this.stadiumLocation);
  }

  public void PlayAnnouncerIntroduction() => this.PlayRandomClip(this.pregame_announcerIntro);

  public void PlayPregameResponseToAnalyst() => this.PlayRandomClip(this.pregame_responseToAnalyst);

  public void PlayPregameFinalRemarksGeneric() => this.PlayRandomClip(this.pregame_final_generic);

  public void PlayPregameFinalRemarksSnowRain() => this.PlayRandomClip(this.pregame_final_snowRain);

  public void PlayPregameFinalRemarksCloseMatchup() => this.PlayRandomClip(this.pregame_final_closeMatch);

  public void PlayPregameFinalRemarksStarPlayer() => this.PlayRandomClip(this.pregame_final_starPlayer);

  public void PlayPregameFinalRemarksSeasonGame()
  {
    AudioPath pregameFinalSeasonGame = this.pregame_final_seasonGame;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(pregameFinalSeasonGame, PersistentData.seasonWeek)));
  }

  public void PlayPregameFinalRemarksPlayoffs() => this.PlayRandomClip(this.pregame_final_playoffs);

  public void PlayPregameFinalRemarksChampionship() => this.PlayRandomClip(this.pregame_final_championship);

  public void PlayResponseToReporter() => this.PlayRandomClip(this.responseToReporter);

  public void PlayScoreUpdate(bool defenseScored, int score)
  {
    if ((Object) this.commentary == (Object) null)
      return;
    int score1 = ProEra.Game.MatchState.Stats.User.Score;
    int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
    int num = 0;
    switch (score)
    {
      case -2:
        num = 2;
        break;
      case 0:
      case 1:
      case 2:
        num = 6 + score;
        break;
      case 3:
        num = 3;
        break;
    }
    this.commentary.AddPause();
    if (!this.playedFirstScoreUpdate)
    {
      this.playedFirstScoreUpdate = true;
      this.PlayScoreUpdate_FirstScore(global::Game.IsPlayerOneOnOffense | defenseScored);
    }
    else if (global::Game.IsPlayerOneOnOffense && !defenseScored || global::Game.IsPlayerOneOnDefense & defenseScored)
    {
      if (score1 - num <= score2)
      {
        if (score1 == score2)
          return;
        if (score1 > score2)
          this.PlayScoreUpdate_TakesLead(true);
        else
          this.PlayScoreUpdate_StillLosing(true);
      }
      else
        this.PlayScoreUpdate_ExtendsLead(true);
    }
    else if (score2 - num <= score1)
    {
      if (score2 == score1)
        return;
      if (score2 > score1)
        this.PlayScoreUpdate_TakesLead(false);
      else
        this.PlayScoreUpdate_StillLosing(false);
    }
    else
      this.PlayScoreUpdate_ExtendsLead(false);
  }

  private void PlayScoreUpdate_FirstScore(bool userScored)
  {
    if (userScored)
      this.PlayUserTeamCity_Prefix();
    else
      this.PlayCompTeamCity_Prefix();
    this.PlayRandomClip(this.scoreUpdate_FirstScore);
    this.PlayScore();
  }

  private void PlayScoreUpdate_TakesLead(bool userScored)
  {
    if (userScored)
      this.PlayUserTeamCity_Prefix();
    else
      this.PlayCompTeamCity_Prefix();
    this.PlayRandomClip(this.scoreUpdate_TakesLead);
    this.PlayScore();
  }

  private void PlayScoreUpdate_ExtendsLead(bool userScored)
  {
    if (userScored)
      this.PlayUserTeamCity_Prefix();
    else
      this.PlayCompTeamCity_Prefix();
    this.PlayRandomClip(this.scoreUpdate_ExtendsLead);
    this.PlayScore();
  }

  private void PlayScoreUpdate_StillLosing(bool userScored)
  {
    if (userScored)
      this.PlayUserTeamCity_Prefix();
    else
      this.PlayCompTeamCity_Prefix();
    this.PlayRandomClip(this.scoreUpdate_StillLosing);
    this.PlayScore();
  }

  private void PlayFinalScore_Generic()
  {
    AudioPath gameGenericScore = this.endGameGenericScore;
    int index = Random.Range(0, gameGenericScore.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(gameGenericScore, index);
    if (index > 2)
    {
      if (ProEra.Game.MatchState.Stats.User.Score > ProEra.Game.MatchState.Stats.Comp.Score)
        this.PlayUserTeamCity_Prefix();
      else
        this.PlayCompTeamCity_Prefix();
    }
    this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    this.commentary.AddPause(0.2f);
    this.PlayScore();
  }

  private void PlayScore()
  {
    if (ProEra.Game.MatchState.Stats.User.Score >= ProEra.Game.MatchState.Stats.Comp.Score)
    {
      this.PlayNumber(ProEra.Game.MatchState.Stats.User.Score, AudioAddition.Postfix);
      this.commentary.AddPause(0.05f);
      this.PlayNumber(ProEra.Game.MatchState.Stats.Comp.Score, AudioAddition.Postfix);
    }
    else
    {
      this.PlayNumber(ProEra.Game.MatchState.Stats.Comp.Score, AudioAddition.Postfix);
      this.commentary.AddPause(0.05f);
      this.PlayNumber(ProEra.Game.MatchState.Stats.User.Score, AudioAddition.Postfix);
    }
  }

  public void PlayHomeTeamCity_Prefix() => this.PlayRandomClip(this.homeTeamCityPrefix);

  public void PlayHomeTeamCity_Postfix() => this.PlayRandomClip(this.homeTeamCityPostfix);

  public void PlayAwayTeamCity_Prefix() => this.PlayRandomClip(this.awayTeamCityPrefix);

  public void PlayAwayTeamCity_Postfix() => this.PlayRandomClip(this.awayTeamCityPostfix);

  public void PlayUserTeamCity_Prefix()
  {
    if (PersistentData.userIsHome)
      this.PlayHomeTeamCity_Prefix();
    else
      this.PlayAwayTeamCity_Prefix();
  }

  public void PlayUserTeamCity_Postfix()
  {
    if (PersistentData.userIsHome)
      this.PlayHomeTeamCity_Postfix();
    else
      this.PlayAwayTeamCity_Postfix();
  }

  public void PlayCompTeamCity_Prefix()
  {
    if (!PersistentData.userIsHome)
      this.PlayHomeTeamCity_Prefix();
    else
      this.PlayAwayTeamCity_Prefix();
  }

  public void PlayCompTeamCity_Postfix()
  {
    if (!PersistentData.userIsHome)
      this.PlayHomeTeamCity_Postfix();
    else
      this.PlayAwayTeamCity_Postfix();
  }

  public void PlayOffensiveTeamCity_Prefix()
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.PlayUserTeamCity_Prefix();
    else
      this.PlayCompTeamCity_Prefix();
  }

  public void PlayOffensiveTeamCity_Postfix()
  {
    if (global::Game.IsPlayerOneOnOffense)
      this.PlayUserTeamCity_Postfix();
    else
      this.PlayCompTeamCity_Postfix();
  }

  public void PlayDefensiveTeamCity_Prefix()
  {
    if (global::Game.IsPlayerOneOnDefense)
      this.PlayUserTeamCity_Prefix();
    else
      this.PlayCompTeamCity_Prefix();
  }

  public void PlayDefensiveTeamCity_Postfix()
  {
    if (global::Game.IsPlayerOneOnDefense)
      this.PlayUserTeamCity_Postfix();
    else
      this.PlayCompTeamCity_Postfix();
  }

  public void PlayHomeTeamName_Prefix() => this.PlayRandomClip(this.homeTeamNamePrefix);

  public void PlayHomeTeamName_Postfix() => this.PlayRandomClip(this.homeTeamNamePostfix);

  public void PlayAwayTeamName_Prefix() => this.PlayRandomClip(this.awayTeamNamePrefix);

  public void PlayAwayTeamName_Postfix() => this.PlayRandomClip(this.awayTeamNamePostfix);

  public void PlayUserTeamName_Prefix()
  {
    if (PersistentData.userIsHome)
      this.PlayHomeTeamName_Prefix();
    else
      this.PlayAwayTeamName_Prefix();
  }

  public void PlayUserTeamName_Postfix()
  {
    if (PersistentData.userIsHome)
      this.PlayHomeTeamName_Postfix();
    else
      this.PlayAwayTeamName_Postfix();
  }

  public void PlayCompTeamName_Prefix()
  {
    if (!PersistentData.userIsHome)
      this.PlayHomeTeamName_Prefix();
    else
      this.PlayAwayTeamName_Prefix();
  }

  public void PlayCompTeamName_Postfix()
  {
    if (!PersistentData.userIsHome)
      this.PlayHomeTeamName_Postfix();
    else
      this.PlayAwayTeamName_Postfix();
  }

  private void PlayEndOfQuarterClip(int quarter)
  {
    switch (quarter)
    {
      case 1:
        this.PlayRandomClip(this.endOfQuarter1);
        break;
      case 2:
        this.PlayRandomClip(this.endOfQuarter2);
        break;
      case 3:
        this.PlayRandomClip(this.endOfQuarter3);
        break;
    }
  }
}
