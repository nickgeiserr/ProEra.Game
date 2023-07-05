// Decompiled with JetBrains decompiler
// Type: AnalystAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class AnalystAudio : MonoBehaviour
{
  private string rootFolder = "analystaudio/";
  public CommentaryManager commentary;
  public bool playedCommentOnPass;
  public bool playedFirstScoreUpdate;
  public bool isLoaded;
  private AudioPath changePos_afterTurnover;
  private AudioPath changePos_offLosingByOnePos;
  private AudioPath changePos_offLosingByTwoPos;
  private AudioPath changePos_offWinningByOnePos;
  private AudioPath changePos_offWinningByTwoPos;
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
  private AudioPath pregame_championship;
  private AudioPath pregame_closeMatchup;
  private AudioPath pregame_generic;
  private AudioPath pregame_lopsidedMatchup;
  private AudioPath pregame_playoff;
  private AudioPath pregame_rain;
  private AudioPath pregame_snow;
  private AudioPath pregame_starPlayer_intro;
  private AudioPath pregame_starPlayer_stat;
  private AudioPath pregame_starPlayer_generic;
  private AudioPath penalty_defensiveHolding;
  private AudioPath penalty_delayOfGame;
  private AudioPath penalty_delayOfGameOnPurpose;
  private AudioPath penalty_falseStart;
  private AudioPath penalty_generic;
  private AudioPath penalty_holding;
  private AudioPath penalty_holdingCloseToEndzone;
  private AudioPath penalty_offsides;
  private AudioPath penalty_passInterference;
  private AudioPath playComments_deflectedPass;
  private AudioPath playComments_dropPass_generic;
  private AudioPath playComments_dropPass_multiple;
  private AudioPath playComments_dropPass_rain;
  private AudioPath playComments_longGain_insideRun;
  private AudioPath playComments_longGain_option;
  private AudioPath playComments_longGain_outsideRun;
  private AudioPath playComments_longGain_pass;
  private AudioPath playComments_longGain_screen;
  private AudioPath playComments_loss_blitz;
  private AudioPath playComments_loss_insideRun;
  private AudioPath playComments_loss_option;
  private AudioPath playComments_loss_outsideRun;
  private AudioPath playComments_loss_screen;
  private AudioPath playComments_loss_WRScreen;
  private AudioPath playComments_multipleBrokenTackles;
  private AudioPath playComments_passCaught_bullet;
  private AudioPath playComments_passCaught_inTraffic;
  private AudioPath playComments_passCaught_wideOpen;
  private AudioPath playComments_sack_generic;
  private AudioPath playComments_sack_heldTooLong;
  private AudioPath playComments_sacks_multiple;
  private AudioPath playComments_TD_fumbleRecovery;
  private AudioPath playComments_TD_generic;
  private AudioPath playComments_TD_interception;
  private AudioPath playComments_TD_kickReturn;
  private AudioPath playComments_TD_longPass;
  private AudioPath playComments_TD_longRun;
  private AudioPath playComments_puntReturn;
  private AudioPath playComments_TD_shortPass;
  private AudioPath playComments_TD_shortRun;
  private AudioPath playComments_turnover_fumble_generic;
  private AudioPath playComments_turnover_fumble_rain;
  private AudioPath playComments_turnover_fumble_snow;
  private AudioPath playComments_turnover_interception;
  private AudioPath playComments_TD_puntReturn;
  private AudioPath downAndDist_comingUp_secondLong;
  private AudioPath downAndDist_comingUp_secondShort;
  private AudioPath downAndDist_comingUp_thirdLong;
  private AudioPath downAndDist_comingUp_thirdLongOwnTerr;
  private AudioPath downAndDist_comingUp_thirdShort;
  private AudioPath downAndDist_firstDownPass;
  private AudioPath downAndDist_firstDownRun;
  private AudioPath downAndDist_fourthDownAtt_failed;
  private AudioPath downAndDist_fourthDownAtt_success;
  private AudioPath downAndDist_wasFirst_gotFirst;
  private AudioPath downAndDist_wasSecondShort_incDeep;
  private AudioPath downAndDist_wasThirdLong_gotFirst;
  private AudioPath downAndDist_wasThirdLong_incDeep;
  private AudioPath downAndDist_wasThirdLong_shortPass;
  private AudioPath downAndDist_wasThirdLong_shortRun;
  private AudioPath downAndDist_wasThirdShort_firstDown;
  private AudioPath downAndDist_wasThirdDown_loss;
  private AudioPath scoreUpdate_firstFG;
  private AudioPath scoreUpdate_firstTD;
  private AudioPath scoreUpdate_stillLosing_onePoss;
  private AudioPath scoreUpdate_stillLosing_twoPoss;
  private AudioPath scoreUpdate_stillWinning_onePoss;
  private AudioPath scoreUpdate_stillWinning_TwoPoss;
  private AudioPath scoreUpdate_takesLead_Q1To3;
  private AudioPath scoreUpdate_takesLead_AlmostOver;
  private AudioPath stats_beginning;
  private AudioPath stats_defensiveStats;
  private AudioPath stats_ending;
  private AudioPath stats_offensiveStats;
  private AudioPath stats_teamStats;
  private AudioPath stud_finalRemarks;
  private AudioPath stud_player;
  private AudioPath homeTeamCityPrefix;
  private AudioPath awayTeamCityPrefix;
  private AudioPath homeTeamCityPostfix;
  private AudioPath awayTeamCityPostfix;
  private AudioPath homeTeamNamePrefix;
  private AudioPath awayTeamNamePrefix;
  private AudioPath homeTeamNamePostfix;
  private AudioPath awayTeamNamePostfix;
  private WaitForSeconds _droppedPassDelay = new WaitForSeconds(2f);

  public void LoadAudioPaths()
  {
    if (this.isLoaded)
      return;
    this.isLoaded = true;
    this.commentary = AudioManager.self.commentary;
    string str1 = "CHANGE_OF_POSSESION/";
    this.changePos_afterTurnover = new AudioPath(this.rootFolder + str1 + "AFTER_TURNOVER", 7, true);
    this.changePos_offLosingByOnePos = new AudioPath(this.rootFolder + str1 + "OFFENSE_LOSING_BY_ONE_POSSESION", 7, true);
    this.changePos_offLosingByTwoPos = new AudioPath(this.rootFolder + str1 + "OFFENSE_LOSING_BY_TWO_POSSESIONS", 8, true);
    this.changePos_offWinningByOnePos = new AudioPath(this.rootFolder + str1 + "OFFENSE_WINNING_BY_ONE_POSSESION", 8, true);
    this.changePos_offWinningByTwoPos = new AudioPath(this.rootFolder + str1 + "OFFENSE_WINNING_BY_TWO_POSSESIONS", 8, true);
    string str2 = "MISC_WORDS_PHRASES/";
    this.misc_conjunctions = new AudioPath(this.rootFolder + str2 + "CONJUNCTIONS", 5, true);
    this.misc_nouns = new AudioPath(this.rootFolder + str2 + "NOUNS", 9, true);
    this.misc_phrases = new AudioPath(this.rootFolder + str2 + "PHRASES", 5, true);
    this.misc_prepositions = new AudioPath(this.rootFolder + str2 + "PREPOSITIONS", 10, true);
    this.misc_homeTeam_prefix = new AudioPath(this.rootFolder + str2 + "HOME_PREFIX", 1, true);
    this.misc_homeTeam_postfix = new AudioPath(this.rootFolder + str2 + "HOME_POSTFIX", 1, true);
    this.misc_awayTeam_prefix = new AudioPath(this.rootFolder + str2 + "AWAY_PREFIX", 1, true);
    this.misc_awayTeam_postfix = new AudioPath(this.rootFolder + str2 + "AWAY_POSTFIX", 1, true);
    this.misc_offense_prefix = new AudioPath(this.rootFolder + str2 + "OFFENSE_PREFIX", 1, true);
    this.misc_offense_postfix = new AudioPath(this.rootFolder + str2 + "OFFENSE_POSTFIX", 1, true);
    this.misc_defense_prefix = new AudioPath(this.rootFolder + str2 + "DEFENSE_PREFIX", 1, true);
    this.misc_defense_postfix = new AudioPath(this.rootFolder + str2 + "DEFENSE_POSTFIX", 1, true);
    string str3 = "NUMBERS/";
    this.numbers_1To99_prefix = new AudioPath(this.rootFolder + str3 + "1_TO_99_PREFIX", 99, true);
    this.numbers_1To99_postfix = new AudioPath(this.rootFolder + str3 + "1_TO_99_POSTFIX", 99, true);
    this.numbers_1To99_withWord = new AudioPath(this.rootFolder + str3 + "1_TO_99_WITH_WORD", 99, true);
    this.numbers_hundreds = new AudioPath(this.rootFolder + str3 + "HUNDREDS", 9, true);
    this.numbers_misc = new AudioPath(this.rootFolder + str3 + "MISC", 5, true);
    this.numbers_ordinal = new AudioPath(this.rootFolder + str3 + "ORDINAL", 20, true);
    this.numbers_thousands = new AudioPath(this.rootFolder + str3 + "THOUSANDS", 6, true);
    this.lastNames = new Dictionary<string, AudioClip>();
    string str4 = "POSITIONS_PREFIX/";
    this.positions_C_prefix = new AudioPath(this.rootFolder + str4 + "C", 1, true);
    this.positions_CB_prefix = new AudioPath(this.rootFolder + str4 + "CB", 1, true);
    this.positions_DE_prefix = new AudioPath(this.rootFolder + str4 + "DE", 1, true);
    this.positions_DL_prefix = new AudioPath(this.rootFolder + str4 + "DL", 2, true);
    this.positions_DT_prefix = new AudioPath(this.rootFolder + str4 + "DT", 1, true);
    this.positions_FB_prefix = new AudioPath(this.rootFolder + str4 + "FB", 1, true);
    this.positions_FS_prefix = new AudioPath(this.rootFolder + str4 + "FS", 2, true);
    this.positions_ILB_prefix = new AudioPath(this.rootFolder + str4 + "ILB", 2, true);
    this.positions_K_prefix = new AudioPath(this.rootFolder + str4 + "K", 1, true);
    this.positions_LB_prefix = new AudioPath(this.rootFolder + str4 + "LB", 1, true);
    this.positions_LG_prefix = new AudioPath(this.rootFolder + str4 + "LG", 1, true);
    this.positions_LT_prefix = new AudioPath(this.rootFolder + str4 + "LT", 1, true);
    this.positions_MLB_prefix = new AudioPath(this.rootFolder + str4 + "MLB", 2, true);
    this.positions_NT_prefix = new AudioPath(this.rootFolder + str4 + "NT", 1, true);
    this.positions_OL_prefix = new AudioPath(this.rootFolder + str4 + "OL", 1, true);
    this.positions_OLB_prefix = new AudioPath(this.rootFolder + str4 + "OLB", 2, true);
    this.positions_P_prefix = new AudioPath(this.rootFolder + str4 + "P", 1, true);
    this.positions_QB_prefix = new AudioPath(this.rootFolder + str4 + "QB", 2, true);
    this.positions_RB_prefix = new AudioPath(this.rootFolder + str4 + "RB", 1, true);
    this.positions_RG_prefix = new AudioPath(this.rootFolder + str4 + "RG", 1, true);
    this.positions_RT_prefix = new AudioPath(this.rootFolder + str4 + "RT", 1, true);
    this.positions_SLT_prefix = new AudioPath(this.rootFolder + str4 + "SLT", 2, true);
    this.positions_SS_prefix = new AudioPath(this.rootFolder + str4 + "SS", 2, true);
    this.positions_TE_prefix = new AudioPath(this.rootFolder + str4 + "TE", 1, true);
    this.positions_WR_prefix = new AudioPath(this.rootFolder + str4 + "WR", 2, true);
    this.positions_DB_prefix = new AudioPath(this.rootFolder + str4 + "DB", 2, true);
    this.positions_RET_prefix = new AudioPath(this.rootFolder + str4 + "RET", 1, true);
    string str5 = "POSITIONS_POSTFIX/";
    this.positions_C_postfix = new AudioPath(this.rootFolder + str5 + "C", 1, true);
    this.positions_CB_postfix = new AudioPath(this.rootFolder + str5 + "CB", 1, true);
    this.positions_DE_postfix = new AudioPath(this.rootFolder + str5 + "DE", 1, true);
    this.positions_DL_postfix = new AudioPath(this.rootFolder + str5 + "DL", 2, true);
    this.positions_DT_postfix = new AudioPath(this.rootFolder + str5 + "DT", 1, true);
    this.positions_FB_postfix = new AudioPath(this.rootFolder + str5 + "FB", 1, true);
    this.positions_FS_postfix = new AudioPath(this.rootFolder + str5 + "FS", 2, true);
    this.positions_ILB_postfix = new AudioPath(this.rootFolder + str5 + "ILB", 2, true);
    this.positions_K_postfix = new AudioPath(this.rootFolder + str5 + "K", 1, true);
    this.positions_LB_postfix = new AudioPath(this.rootFolder + str5 + "LB", 1, true);
    this.positions_LG_postfix = new AudioPath(this.rootFolder + str5 + "LG", 1, true);
    this.positions_LT_postfix = new AudioPath(this.rootFolder + str5 + "LT", 1, true);
    this.positions_MLB_postfix = new AudioPath(this.rootFolder + str5 + "MLB", 2, true);
    this.positions_NT_postfix = new AudioPath(this.rootFolder + str5 + "NT", 1, true);
    this.positions_OL_postfix = new AudioPath(this.rootFolder + str5 + "OL", 1, true);
    this.positions_OLB_postfix = new AudioPath(this.rootFolder + str5 + "OLB", 2, true);
    this.positions_P_postfix = new AudioPath(this.rootFolder + str5 + "P", 1, true);
    this.positions_QB_postfix = new AudioPath(this.rootFolder + str5 + "QB", 2, true);
    this.positions_RB_postfix = new AudioPath(this.rootFolder + str5 + "RB", 1, true);
    this.positions_RG_postfix = new AudioPath(this.rootFolder + str5 + "RG", 1, true);
    this.positions_RT_postfix = new AudioPath(this.rootFolder + str5 + "RT", 1, true);
    this.positions_SLT_postfix = new AudioPath(this.rootFolder + str5 + "SLT", 2, true);
    this.positions_SS_postfix = new AudioPath(this.rootFolder + str5 + "SS", 2, true);
    this.positions_TE_postfix = new AudioPath(this.rootFolder + str5 + "TE", 1, true);
    this.positions_WR_postfix = new AudioPath(this.rootFolder + str5 + "WR", 2, true);
    this.positions_DB_postfix = new AudioPath(this.rootFolder + str5 + "DB", 2, true);
    this.positions_RET_postfix = new AudioPath(this.rootFolder + str5 + "RET", 1, true);
    string str6 = "PREGAME_INTRODUCTION/";
    this.pregame_championship = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_CHAMPIONSHIP", 9, false);
    this.pregame_closeMatchup = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_CLOSE_MATCHUP", 10, false);
    this.pregame_generic = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_GENERIC", 11, false);
    this.pregame_lopsidedMatchup = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_LOPSIDED_MATCHUP", 12, false);
    this.pregame_playoff = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_PLAYOFF_GAME", 11, false);
    this.pregame_rain = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_RAIN", 12, false);
    this.pregame_snow = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_SNOW", 12, false);
    this.pregame_starPlayer_intro = new AudioPath(this.rootFolder + str6 + "INTRODUCTION_STAR_PLAYER", 14, false);
    this.pregame_starPlayer_stat = new AudioPath(this.rootFolder + str6 + "STAR_PLAYER_FOLLOWING_STAT", 11, false);
    this.pregame_starPlayer_generic = new AudioPath(this.rootFolder + str6 + "STAR_PLAYER_GENERIC", 10, false);
    string str7 = "REPONSE_TO_PLAY_PENALTY/";
    this.penalty_defensiveHolding = new AudioPath(this.rootFolder + str7 + "DEFENSIVE_HOLDING", 10, true);
    this.penalty_delayOfGame = new AudioPath(this.rootFolder + str7 + "DELAY_OF_GAME_NOT_ON_PURPOSE", 10, true);
    this.penalty_delayOfGameOnPurpose = new AudioPath(this.rootFolder + str7 + "DELAY_OF_GAME_ON_PURPOSE", 2, true);
    this.penalty_falseStart = new AudioPath(this.rootFolder + str7 + "FALSE_START", 11, true);
    this.penalty_generic = new AudioPath(this.rootFolder + str7 + "GENERIC", 8, true);
    this.penalty_holding = new AudioPath(this.rootFolder + str7 + "HOLDING", 10, true);
    this.penalty_holdingCloseToEndzone = new AudioPath(this.rootFolder + str7 + "HOLDING_5_YARDS_TO_ENDZONE", 4, true);
    this.penalty_offsides = new AudioPath(this.rootFolder + str7 + "OFFSIDES", 12, true);
    this.penalty_passInterference = new AudioPath(this.rootFolder + str7 + "PASS_INTERFERENCE", 9, true);
    string str8 = "RESPONSE_TO_PLAY_COMMENTS/";
    this.playComments_deflectedPass = new AudioPath(this.rootFolder + str8 + "DEFLECTED_PASS", 13, true);
    this.playComments_dropPass_generic = new AudioPath(this.rootFolder + str8 + "DROPPED_PASS_GENERIC", 16, true);
    this.playComments_dropPass_multiple = new AudioPath(this.rootFolder + str8 + "DROPPED_PASS_MULTIPLE", 15, true);
    this.playComments_dropPass_rain = new AudioPath(this.rootFolder + str8 + "DROPPED_PASS_RAIN", 6, true);
    this.playComments_longGain_insideRun = new AudioPath(this.rootFolder + str8 + "LONG_GAIN_INSIDE_RUN", 13, true);
    this.playComments_longGain_option = new AudioPath(this.rootFolder + str8 + "LONG_GAIN_OPTION", 8, true);
    this.playComments_longGain_outsideRun = new AudioPath(this.rootFolder + str8 + "LONG_GAIN_OUTSIDE_RUN", 15, true);
    this.playComments_longGain_pass = new AudioPath(this.rootFolder + str8 + "LONG_GAIN_PASS", 26, true);
    this.playComments_longGain_screen = new AudioPath(this.rootFolder + str8 + "LONG_GAIN_SCREEN", 9, true);
    this.playComments_loss_blitz = new AudioPath(this.rootFolder + str8 + "LOSS_ON_PLAY_DEFENSE_BLITZ", 12, true);
    this.playComments_loss_insideRun = new AudioPath(this.rootFolder + str8 + "LOSS_ON_PLAY_INSIDE_RUN", 17, true);
    this.playComments_loss_option = new AudioPath(this.rootFolder + str8 + "LOSS_ON_PLAY_OPTION", 14, true);
    this.playComments_loss_outsideRun = new AudioPath(this.rootFolder + str8 + "LOSS_ON_PLAY_OUTSIDE_RUN", 16, true);
    this.playComments_loss_screen = new AudioPath(this.rootFolder + str8 + "LOSS_ON_PLAY_SCREEN", 11, true);
    this.playComments_loss_WRScreen = new AudioPath(this.rootFolder + str8 + "LOSS_ON_PLAY_WR_SCREEN_PASS", 12, true);
    this.playComments_multipleBrokenTackles = new AudioPath(this.rootFolder + str8 + "MULTIPLE_BROKEN_TACKLES_ON_PLAY", 16, true);
    this.playComments_passCaught_bullet = new AudioPath(this.rootFolder + str8 + "PASS_CAUGHT_BULLET_PASS", 20, true);
    this.playComments_passCaught_inTraffic = new AudioPath(this.rootFolder + str8 + "PASS_CAUGHT_IN_TRAFFIC", 20, true);
    this.playComments_passCaught_wideOpen = new AudioPath(this.rootFolder + str8 + "PASS_CAUGHT_WIDE_OPEN", 18, true);
    this.playComments_sack_generic = new AudioPath(this.rootFolder + str8 + "SACK_GENERIC", 13, true);
    this.playComments_sack_heldTooLong = new AudioPath(this.rootFolder + str8 + "SACK_HELD_BALL_TOO_LONG", 13, true);
    this.playComments_sacks_multiple = new AudioPath(this.rootFolder + str8 + "SACKS_MULTIPLE", 13, true);
    this.playComments_TD_fumbleRecovery = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_FUMBLE_RECOVERY", 8, true);
    this.playComments_TD_generic = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_GENERIC", 13, true);
    this.playComments_TD_interception = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_INTERCEPTION", 11, true);
    this.playComments_TD_kickReturn = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_KICKOFF_RETURNED", 9, true);
    this.playComments_TD_puntReturn = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_PUNT_RETURNED", 9, true);
    this.playComments_TD_longPass = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_LONG_PASS", 15, true);
    this.playComments_TD_longRun = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_LONG_RUN", 15, true);
    this.playComments_puntReturn = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_PUNT_RETURNED", 9, true);
    this.playComments_TD_shortPass = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_SHORT_PASS", 17, true);
    this.playComments_TD_shortRun = new AudioPath(this.rootFolder + str8 + "TOUCHDOWN_SHORT_RUN", 12, true);
    this.playComments_turnover_fumble_generic = new AudioPath(this.rootFolder + str8 + "TURNOVER_FUMBLE_GENERIC", 13, true);
    this.playComments_turnover_fumble_rain = new AudioPath(this.rootFolder + str8 + "TURNOVER_FUMBLE_IN_RAIN", 6, true);
    this.playComments_turnover_fumble_snow = new AudioPath(this.rootFolder + str8 + "TURNOVER_FUMBLE_IN_SNOW", 5, true);
    this.playComments_turnover_interception = new AudioPath(this.rootFolder + str8 + "TURNOVER_INTERCEPTION", 20, true);
    string str9 = "RESPONSE_TO_PLAY_DOWN_AND_DISTANCE/";
    this.downAndDist_comingUp_secondLong = new AudioPath(this.rootFolder + str9 + "COMING_UP_SECOND_AND_LONG", 8, true);
    this.downAndDist_comingUp_secondShort = new AudioPath(this.rootFolder + str9 + "COMING_UP_SECOND_AND_SHORT", 8, true);
    this.downAndDist_comingUp_thirdLong = new AudioPath(this.rootFolder + str9 + "COMING_UP_THIRD_AND_LONG", 8, true);
    this.downAndDist_comingUp_thirdLongOwnTerr = new AudioPath(this.rootFolder + str9 + "COMING_UP_THIRD_AND_LONG_DEEP_IN_OWN_TERRITORY", 7, true);
    this.downAndDist_comingUp_thirdShort = new AudioPath(this.rootFolder + str9 + "COMING_UP_THIRD_AND_SHORT", 8, true);
    this.downAndDist_firstDownPass = new AudioPath(this.rootFolder + str9 + "FIRST_DOWN_GENERIC_PASS", 13, true);
    this.downAndDist_firstDownRun = new AudioPath(this.rootFolder + str9 + "FIRST_DOWN_GENERIC_RUN", 16, true);
    this.downAndDist_fourthDownAtt_failed = new AudioPath(this.rootFolder + str9 + "FOURTH_DOWN_CONVERSION_FAILED", 6, true);
    this.downAndDist_fourthDownAtt_success = new AudioPath(this.rootFolder + str9 + "FOURTH_DOWN_CONVERSION_SUCCESS", 6, true);
    this.downAndDist_wasFirst_gotFirst = new AudioPath(this.rootFolder + str9 + "WAS_FIRST_DOWN_FIRST_DOWN", 17, true);
    this.downAndDist_wasSecondShort_incDeep = new AudioPath(this.rootFolder + str9 + "WAS_SECOND_AND_SHORT_INCOMPLETE_DEEP", 8, true);
    this.downAndDist_wasThirdLong_gotFirst = new AudioPath(this.rootFolder + str9 + "WAS_THIRD_AND_LONG_FIRST_DOWN", 7, true);
    this.downAndDist_wasThirdLong_incDeep = new AudioPath(this.rootFolder + str9 + "WAS_THIRD_AND_LONG_INCOMPLETE_DEEP", 7, true);
    this.downAndDist_wasThirdLong_shortPass = new AudioPath(this.rootFolder + str9 + "WAS_THIRD_AND_LONG_SHORT_GAIN_PASS", 8, true);
    this.downAndDist_wasThirdLong_shortRun = new AudioPath(this.rootFolder + str9 + "WAS_THIRD_AND_LONG_SHORT_GAIN_RUN", 8, true);
    this.downAndDist_wasThirdShort_firstDown = new AudioPath(this.rootFolder + str9 + "WAS_THIRD_AND_SHORT_FIRST_DOWN", 8, true);
    this.downAndDist_wasThirdDown_loss = new AudioPath(this.rootFolder + str9 + "WAS_THIRD_DOWN_LOSS_ON_PLAY", 8, true);
    string str10 = "SCORE_UPDATE/";
    this.scoreUpdate_firstFG = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_FIRST_SCORE_FG", 17, true);
    this.scoreUpdate_firstTD = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_FIRST_SCORE_TOUCHDOWN", 13, true);
    this.scoreUpdate_stillLosing_onePoss = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_STILL_LOSING_ONE_POSSESSION_GAME", 14, true);
    this.scoreUpdate_stillLosing_twoPoss = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_STILL_LOSING_TWO_POSSESSION_GAME", 14, true);
    this.scoreUpdate_stillWinning_onePoss = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_STILL_WINNING_ONE_POSSESSION_GAME", 8, true);
    this.scoreUpdate_stillWinning_TwoPoss = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_STILL_WINNING_TWO_POSSESSION_GAME", 18, true);
    this.scoreUpdate_takesLead_Q1To3 = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_TAKES_LEAD_QUARTERS_1-3", 14, true);
    this.scoreUpdate_takesLead_AlmostOver = new AudioPath(this.rootFolder + str10 + "SCORE_UPDATE_TAKES_LEAD_WITH_1-3_MINUTES_TO_PLAY", 9, true);
    string str11 = "STATS/";
    this.stats_beginning = new AudioPath(this.rootFolder + str11 + "BEGINNING", 31, true);
    this.stats_defensiveStats = new AudioPath(this.rootFolder + str11 + "DEFENSIVE_STATS", 12, true);
    this.stats_ending = new AudioPath(this.rootFolder + str11 + "ENDING", 19, true);
    this.stats_offensiveStats = new AudioPath(this.rootFolder + str11 + "OFFENSIVE_STATS", 47, true);
    this.stats_teamStats = new AudioPath(this.rootFolder + str11 + "TEAM_STATS", 10, true);
    this.stud_finalRemarks = new AudioPath(this.rootFolder + "STUD_FINAL_REMARKS", 16, true);
    this.stud_player = new AudioPath(this.rootFolder + "STUD_PLAYER", 14, true);
  }

  public void LoadTeamAudio()
  {
    string str1 = "team_audio/";
    bool flag1 = PersistentData.GetHomeTeamIndex() < TeamAssetManager.NUMBER_OF_BASE_TEAMS && PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets;
    if (flag1 && (PersistentData.GetHomeTeamIndex() == 19 || PersistentData.GetHomeTeamIndex() == 22 || PersistentData.GetHomeTeamIndex() == 33 || PersistentData.GetHomeTeamIndex() == 35))
      flag1 = false;
    if (flag1)
    {
      string str2 = "team_" + PersistentData.GetHomeTeamIndex().ToString();
      this.homeTeamNamePrefix = new AudioPath(this.rootFolder + str1 + str2, "team_name_prefix");
      this.homeTeamNamePostfix = new AudioPath(this.rootFolder + str1 + str2, "team_name_postfix");
      this.homeTeamCityPrefix = new AudioPath(this.rootFolder + str1 + str2, "team_city_prefix");
      this.homeTeamCityPostfix = new AudioPath(this.rootFolder + str1 + str2, "team_city_postfix");
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

  public void ResetCommentVariables() => this.playedCommentOnPass = false;

  private void PlayRandomClip(AudioPath audioPath) => this.PlayRandomClip(audioPath, 0.0f);

  private void PlayRandomClip(AudioPath audioPath, float pauseBefore)
  {
  }

  public void PlayPlayerID(bool userTeam, int indexOnTeam, AudioAddition type)
  {
    TeamData teamData = !userTeam ? MatchManager.instance.playersManager.compTeamData : MatchManager.instance.playersManager.userTeamData;
    AudioClip lastName = this.GetLastName(teamData.GetPlayer(indexOnTeam).LastName);
    if ((Object) lastName != (Object) null)
      this.commentary.AddSegmentToQueue(new AudioSegment(lastName, 0.0f));
    else if (Random.Range(0, 100) < 75)
      this.PlayPlayer_Number(teamData.GetPlayer(indexOnTeam).Number, type);
    else
      this.PlayPlayer_Position(teamData.GetPlayer(indexOnTeam).PlayerPosition, type);
  }

  public void PlayPlayerID(PlayerAI player, AudioAddition type)
  {
    if ((Object) player == (Object) null)
      Debug.Log((object) "Analyst is attempting to name a player when PlayerAI reference is null.");
    else
      this.PlayPlayerID(player.onUserTeam, player.indexOnTeam, type);
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
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(lastName, 0.0f));
    }
    else
    {
      this.PlayWord_Number();
      this.PlayPlayer_Number(teamData.GetPlayer(indexOnTeam).Number, type);
    }
  }

  public void PlayChangeOfPossession()
  {
    int playType = (int) MatchManager.instance.playManager.playType;
    int score1;
    int score2;
    if (global::Game.IsPlayerOneOnOffense)
    {
      score1 = ProEra.Game.MatchState.Stats.User.Score;
      score2 = ProEra.Game.MatchState.Stats.Comp.Score;
    }
    else
    {
      score1 = ProEra.Game.MatchState.Stats.Comp.Score;
      score2 = ProEra.Game.MatchState.Stats.User.Score;
    }
    int num = score1 - score2;
    if (global::Game.IsTurnover && global::Game.IsRunOrPass && Random.Range(0, 100) < 75)
      this.PlayDriveStart_AfterTurnover();
    else if (num > 8)
      this.PlayDriveStart_WinningTwoPos();
    else if (num > 0)
      this.PlayDriveStart_WinningOnePos();
    else if (num < -8)
    {
      this.PlayDriveStart_LosingTwoPos();
    }
    else
    {
      if (num >= 0)
        return;
      this.PlayDriveStart_LosingOnePos();
    }
  }

  private void PlayDriveStart_LosingOnePos()
  {
  }

  private void PlayDriveStart_LosingTwoPos()
  {
  }

  private void PlayDriveStart_WinningOnePos()
  {
  }

  private void PlayDriveStart_WinningTwoPos()
  {
  }

  private void PlayDriveStart_AfterTurnover()
  {
    AudioPath posAfterTurnover = this.changePos_afterTurnover;
    int index = Random.Range(0, posAfterTurnover.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(posAfterTurnover, index);
    if (index < 6)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayWord_Number() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_nouns, 9)));

  public void PlayWord_For() => this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(this.misc_conjunctions, 3)));

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
    AudioPath audioPath = type != AudioAddition.Prefix ? this.numbers_1To99_postfix : this.numbers_1To99_prefix;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(audioPath, number)));
  }

  public void PlayNumberWithWord(int number)
  {
    AudioPath numbers1To99WithWord = this.numbers_1To99_withWord;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(numbers1To99WithWord, number)));
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

  private AudioClip GetLastName(string name)
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

  public void PlayPregameGeneric() => this.PlayRandomClip(this.pregame_generic, CommentaryManager.defaultPauseTime);

  public void PlayPregameChampionship() => this.PlayRandomClip(this.pregame_championship, CommentaryManager.defaultPauseTime);

  public void PlayPregamePlayoffs() => this.PlayRandomClip(this.pregame_playoff, CommentaryManager.defaultPauseTime);

  public void PlayPregameRain() => this.PlayRandomClip(this.pregame_rain, CommentaryManager.defaultPauseTime);

  public void PlayPregameSnow() => this.PlayRandomClip(this.pregame_snow, CommentaryManager.defaultPauseTime);

  public void PlayPregameCloseMatchup() => this.PlayRandomClip(this.pregame_closeMatchup, CommentaryManager.defaultPauseTime);

  public void PlayPregameLopsidedMatchup() => this.PlayRandomClip(this.pregame_lopsidedMatchup, CommentaryManager.defaultPauseTime);

  public void PlayPregameStarPlayerIntro() => this.PlayRandomClip(this.pregame_starPlayer_intro, CommentaryManager.defaultPauseTime);

  public void PlayPregameStarPlayerGeneric() => this.PlayRandomClip(this.pregame_starPlayer_generic, CommentaryManager.defaultPauseTime);

  public void PlayCommentOnPlay_Penalty(Penalty pen)
  {
    int penaltyType = (int) pen.GetPenaltyType();
    if (Random.Range(0, 100) < 15)
    {
      this.PlayPenalty_Generic();
    }
    else
    {
      switch (pen.GetPenaltyType())
      {
        case PenaltyType.Holding:
          if (pen.GetOffenseOrDefense() == "Offense")
          {
            if ((double) Field.FindDifference(MatchManager.ballOn, Field.DEFENSIVE_GOAL_LINE) < 4.0)
            {
              this.PlayPenalty_Holding_NearEndzone();
              break;
            }
            this.PlayPenalty_Holding();
            break;
          }
          this.PlayPenalty_DefensiveHolding();
          break;
        case PenaltyType.FalseStart:
          this.PlayPenalty_FalseStart();
          break;
        case PenaltyType.PassInterference:
          this.PlayPenalty_PassInterference();
          break;
        case PenaltyType.Encroachment:
          this.PlayPenalty_Generic();
          break;
        case PenaltyType.Offsides:
          this.PlayPenalty_Offsides();
          break;
        case PenaltyType.Facemask:
          this.PlayPenalty_Generic();
          break;
        case PenaltyType.DelayOfGame:
          this.PlayPenalty_DelayOfGame();
          break;
        case PenaltyType.IllegalForwardPass:
          this.PlayPenalty_Generic();
          break;
        case PenaltyType.KickoffOutOfBounds:
          this.PlayPenalty_Generic();
          break;
        default:
          this.PlayPenalty_Generic();
          break;
      }
    }
    this.commentary.ClearQueueAfterThis();
  }

  private void PlayPenalty_Generic() => this.PlayRandomClip(this.penalty_generic, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_Offsides() => this.PlayRandomClip(this.penalty_offsides, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_DefensiveHolding() => this.PlayRandomClip(this.penalty_defensiveHolding, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_PassInterference() => this.PlayRandomClip(this.penalty_passInterference, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_FalseStart() => this.PlayRandomClip(this.penalty_falseStart, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_DelayOfGame() => this.PlayRandomClip(this.penalty_delayOfGame, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_DelayOfGame_OnPurpose() => this.PlayRandomClip(this.penalty_delayOfGameOnPurpose, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_Holding() => this.PlayRandomClip(this.penalty_holding, CommentaryManager.defaultPauseTime);

  private void PlayPenalty_Holding_NearEndzone() => this.PlayRandomClip(this.penalty_holdingCloseToEndzone, CommentaryManager.defaultPauseTime);

  public void PlayCommentOnPlay_BallHolder()
  {
    if (global::Game.BallHolderIsNull)
      return;
    PlayType playType = MatchManager.instance.playManager.playType;
    switch (playType)
    {
      case PlayType.Run:
      case PlayType.Pass:
        if (Random.Range(0, 100) < 20)
          break;
        this.commentary.AddPause();
        PlayDirection playDirection = this.commentary.playDirection;
        float yardsGained = this.commentary.GetYardsGained();
        bool flag1 = playType == PlayType.Pass && MatchManager.instance.playersManager.ballWasThrownOrKicked;
        int num1 = MatchManager.down - 1;
        bool flag2 = this.commentary.GotFirstDownOnPlay();
        if (((num1 == 4 ? 1 : (num1 == 1 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && Random.Range(0, 100) < 60)
        {
          this.PlayCommentOnPlay_DownAndDist();
          break;
        }
        if ((double) yardsGained < 0.0)
        {
          int num2 = !global::Game.IsPlayerOneOnDefense ? ProEra.Game.MatchState.Stats.Comp.Sacks : ProEra.Game.MatchState.Stats.User.Sacks;
          if (playType == PlayType.Run && playDirection == PlayDirection.InsideRun)
            this.PlayLossOnPlay_InsideRun();
          else if (playType == PlayType.Run && playDirection == PlayDirection.OutsideRun)
            this.PlayLossOnPlay_OutsideRun();
          else if (flag1 && playDirection == PlayDirection.WRScreen)
            this.PlayLossOnPlay_WRScreen();
          else if (flag1 && playDirection == PlayDirection.InsideScreen)
            this.PlayLossOnPlay_Screen();
          else if (this.commentary.defenseBlitzed)
            this.PlayLossOnPlay_Blitz();
          else if (playType == PlayType.Pass && this.commentary.sacked)
          {
            if ((double) MatchManager.instance.playTimer > 5.0)
              this.PlayLossOnPlay_Sack_HeldTooLong();
            else if (num2 > 4 && Random.Range(0, 100) < 75)
              this.PlayLossOnPlay_Sack_Multiple();
            else
              this.PlayLossOnPlay_Sack_Generic();
          }
        }
        else if ((double) yardsGained > 13.0)
        {
          if (playType == PlayType.Run && playDirection == PlayDirection.InsideRun)
            this.PlayGainOnPlay_InsideRun();
          else if (playType == PlayType.Run && playDirection == PlayDirection.OutsideRun)
            this.PlayGainOnPlay_OutsideRun();
          else if (flag1 && (playDirection == PlayDirection.WRScreen || playDirection == PlayDirection.InsideScreen))
            this.PlayGainOnPlay_Screen();
          else if (MatchManager.instance.brokenTackles > 2)
            this.PlayMultipleBrokenTackles();
          else if (flag1)
          {
            if (this.commentary.passCaughtInTraffic && Random.Range(0, 100) < 70)
              this.PlayPassCaught_InTraffic();
            else if (this.commentary.passCaughtWideOpen && Random.Range(0, 100) < 70)
              this.PlayPassCaught_WideOpen();
            else if (MatchManager.instance.playersManager.bulletPass && Random.Range(0, 100) < 60)
              this.PlayPassCaught_Bullet();
            else
              this.PlayGainOnPlay_Pass();
          }
        }
        else if (flag1)
        {
          if (this.commentary.passCaughtInTraffic && Random.Range(0, 100) < 80)
            this.PlayPassCaught_InTraffic();
          else if (this.commentary.passCaughtWideOpen && Random.Range(0, 100) < 80)
            this.PlayPassCaught_WideOpen();
          else if (MatchManager.instance.playersManager.bulletPass && Random.Range(0, 100) < 70)
            this.PlayPassCaught_Bullet();
        }
        else
          this.PlayCommentOnPlay_DownAndDist();
        this.commentary.ClearQueueAfterThis();
        break;
    }
  }

  public void PlayDroppedPass()
  {
    if (this.playedCommentOnPass || Random.Range(0, 100) < 10)
      return;
    this.StartCoroutine(this.PlayDroppedPass_Delay());
  }

  private IEnumerator PlayDroppedPass_Delay()
  {
    yield return (object) this._droppedPassDelay;
    if (!this.playedCommentOnPass && global::Game.BallHolderIsNull)
    {
      this.playedCommentOnPass = true;
      int num = !global::Game.IsPlayerOneOnOffense ? ProEra.Game.MatchState.Stats.Comp.DroppedPasses : ProEra.Game.MatchState.Stats.User.DroppedPasses;
      this.commentary.AddPause();
      if (PersistentData.weather == 1 && Random.Range(0, 100) < 75)
        this.PlayDroppedPass_Rain();
      else if (num > 4 && Random.Range(0, 100) < 75)
        this.PlayDroppedPass_Multiple();
      else
        this.PlayDroppedPass_Generic();
    }
  }

  public void PlayDeflectedPass()
  {
    if (this.playedCommentOnPass)
      return;
    this.playedCommentOnPass = true;
    this.commentary.AddPause();
    AudioPath commentsDeflectedPass = this.playComments_deflectedPass;
    int index = Random.Range(0, commentsDeflectedPass.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsDeflectedPass, index);
    if (index < 11)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayInterception()
  {
    this.commentary.AddPause();
    if (global::Game.PET_IsTouchdown)
      this.PlayInterception_Touchdown();
    else
      this.PlayInterception_Generic();
  }

  public void PlayFumble()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    this.commentary.AddPause();
    if (global::Game.PET_IsTouchdown)
      this.PlayFumble_Touchdown();
    else if (PersistentData.weather == 2 && Random.Range(0, 100) < 75)
      this.PlayFumble_Snow();
    else if (PersistentData.weather == 1 && Random.Range(0, 100) < 75)
      this.PlayFumble_Rain();
    else
      this.PlayFumble_Generic();
  }

  public void PlayTouchdown()
  {
    this.commentary.AddPause();
    int yards = Field.ConvertDistanceToYards(Field.FindDifference(Field.OFFENSIVE_GOAL_LINE, MatchManager.instance.savedLineOfScrim));
    if (global::Game.IsPass && global::Game.BallIsThrownOrKicked)
    {
      if (yards > 20)
        this.PlayTouchdown_LongPass();
      else if (yards < 8 && Random.Range(0, 100) < 65)
        this.PlayTouchdown_ShortPass();
      else
        this.PlayTouchdown_Generic();
    }
    else if (global::Game.IsRun)
    {
      if (yards > 18)
        this.PlayTouchdown_LongRun();
      else if (yards < 5 && Random.Range(0, 100) < 65)
        this.PlayTouchdown_ShortRun();
      else
        this.PlayTouchdown_Generic();
    }
    else if (global::Game.IsKickoff)
      this.PlayTouchdown_KickoffReturned();
    else if (global::Game.IsPunt)
      this.PlayTouchdown_PuntReturned();
    else
      this.PlayTouchdown_Generic();
  }

  private void PlayTouchdown_Generic()
  {
    AudioPath commentsTdGeneric = this.playComments_TD_generic;
    int index = Random.Range(0, commentsTdGeneric.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsTdGeneric, index);
    if (index < 12)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayTouchdown_ShortRun()
  {
    AudioPath commentsTdShortRun = this.playComments_TD_shortRun;
    int index = Random.Range(0, commentsTdShortRun.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsTdShortRun, index);
    if (index < 11)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayTouchdown_LongRun()
  {
    AudioPath commentsTdLongRun = this.playComments_TD_longRun;
    int index = Random.Range(0, commentsTdLongRun.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsTdLongRun, index);
    if (index < 14)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayTouchdown_ShortPass() => this.PlayRandomClip(this.playComments_TD_shortPass);

  private void PlayTouchdown_LongPass()
  {
    AudioPath commentsTdLongPass = this.playComments_TD_longPass;
    int index = Random.Range(0, commentsTdLongPass.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsTdLongPass, index);
    if (index < 13)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayTouchdown_KickoffReturned() => this.PlayRandomClip(this.playComments_TD_kickReturn);

  private void PlayTouchdown_PuntReturned() => this.PlayRandomClip(this.playComments_TD_puntReturn);

  private void PlayInterception_Generic()
  {
    AudioPath turnoverInterception = this.playComments_turnover_interception;
    int index = Random.Range(0, turnoverInterception.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(turnoverInterception, index);
    if (index < 18)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayInterception_Touchdown() => this.PlayRandomClip(this.playComments_TD_interception);

  private void PlayFumble_Generic()
  {
    if ((Object) this.commentary == (Object) null)
      return;
    AudioPath turnoverFumbleGeneric = this.playComments_turnover_fumble_generic;
    int index = Random.Range(0, turnoverFumbleGeneric.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(turnoverFumbleGeneric, index);
    if (index < 10)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 12)
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayFumble_Rain() => this.PlayRandomClip(this.playComments_turnover_fumble_rain);

  private void PlayFumble_Snow() => this.PlayRandomClip(this.playComments_turnover_fumble_snow);

  private void PlayFumble_Touchdown() => this.PlayRandomClip(this.playComments_TD_fumbleRecovery);

  private void PlayDroppedPass_Generic()
  {
    AudioPath commentsDropPassGeneric = this.playComments_dropPass_generic;
    int index = Random.Range(0, commentsDropPassGeneric.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsDropPassGeneric, index);
    if (index < 15)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayDroppedPass_Rain()
  {
    AudioPath commentsDropPassRain = this.playComments_dropPass_rain;
    int index = Random.Range(0, commentsDropPassRain.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(commentsDropPassRain, index)));
    if (index != 5)
      return;
    this.commentary.AddPause();
    this.commentary.announcer.PlayJoke_RainDrop();
  }

  private void PlayDroppedPass_Multiple()
  {
    AudioPath dropPassMultiple = this.playComments_dropPass_multiple;
    int index = Random.Range(0, dropPassMultiple.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(dropPassMultiple, index);
    if (index < 13)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayGainOnPlay_InsideRun() => this.PlayRandomClip(this.playComments_longGain_insideRun);

  private void PlayGainOnPlay_OutsideRun() => this.PlayRandomClip(this.playComments_longGain_outsideRun);

  private void PlayGainOnPlay_Screen()
  {
    AudioPath commentsLongGainScreen = this.playComments_longGain_screen;
    int index = Random.Range(0, commentsLongGainScreen.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLongGainScreen, index);
    if (index < 8)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayMultipleBrokenTackles()
  {
    AudioPath multipleBrokenTackles = this.playComments_multipleBrokenTackles;
    int index = Random.Range(0, multipleBrokenTackles.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(multipleBrokenTackles, index);
    if (index < 13)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayPassCaught_InTraffic() => this.PlayRandomClip(this.playComments_passCaught_inTraffic);

  private void PlayPassCaught_WideOpen() => this.PlayRandomClip(this.playComments_passCaught_wideOpen);

  private void PlayPassCaught_Bullet() => this.PlayRandomClip(this.playComments_passCaught_bullet);

  private void PlayGainOnPlay_Pass()
  {
    AudioPath commentsLongGainPass = this.playComments_longGain_pass;
    int index = Random.Range(0, commentsLongGainPass.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLongGainPass, index);
    if (index < 25)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index == 25)
    {
      this.PlayPlayerID_QB(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayLossOnPlay_InsideRun()
  {
    AudioPath commentsLossInsideRun = this.playComments_loss_insideRun;
    int index = Random.Range(0, commentsLossInsideRun.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLossInsideRun, index);
    if (index < 14)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 16)
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayLossOnPlay_OutsideRun()
  {
    AudioPath commentsLossOutsideRun = this.playComments_loss_outsideRun;
    int index = Random.Range(0, commentsLossOutsideRun.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLossOutsideRun, index);
    if (index < 11)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 14)
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayLossOnPlay_WRScreen()
  {
    AudioPath commentsLossWrScreen = this.playComments_loss_WRScreen;
    int index = Random.Range(0, commentsLossWrScreen.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLossWrScreen, index);
    if (index < 10)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index == 10)
    {
      this.PlayPlayerID(this.commentary.offPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    if (index != 6)
      return;
    this.commentary.AddPause();
    this.commentary.announcer.PlayJoke_OldCar();
  }

  private void PlayLossOnPlay_Screen()
  {
    AudioPath commentsLossScreen = this.playComments_loss_screen;
    int index = Random.Range(0, commentsLossScreen.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLossScreen, index);
    if (index < 9)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayLossOnPlay_Blitz()
  {
    AudioPath commentsLossBlitz = this.playComments_loss_blitz;
    int index = Random.Range(0, commentsLossBlitz.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsLossBlitz, index);
    if (index < 9)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index == 10)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayLossOnPlay_Sack_Generic()
  {
    AudioPath commentsSackGeneric = this.playComments_sack_generic;
    int index = Random.Range(0, commentsSackGeneric.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(commentsSackGeneric, index);
    if (index < 11)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayPlayerID(this.commentary.defPlayerReference, AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayLossOnPlay_Sack_HeldTooLong()
  {
    AudioPath commentsSackHeldTooLong = this.playComments_sack_heldTooLong;
    int index = Random.Range(0, commentsSackHeldTooLong.count) + 1;
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(commentsSackHeldTooLong, index)));
    if (index != 11)
      return;
    this.commentary.AddPause();
    this.commentary.announcer.PlayJoke_SackedFrozen();
  }

  private void PlayLossOnPlay_Sack_Multiple() => this.PlayRandomClip(this.playComments_sacks_multiple);

  public void PlayCommentOnPlay_DownAndDist()
  {
    if (global::Game.IsNotRun && global::Game.IsNotPass || Random.Range(0, 100) < 20 || MatchManager.runningPat)
      return;
    this.commentary.AddPause();
    bool flag1 = global::Game.IsPass && global::Game.IsNotTurnover && global::Game.PET_IsNotIncomplete;
    bool flag2 = global::Game.IsPass && global::Game.PET_IsIncomplete;
    float num1 = 0.0f;
    if (global::Game.IsPass && global::Game.BallIsThrownOrKicked)
      num1 = MatchManager.instance.playersManager.passDistanceInYards;
    float yardsGained = this.commentary.GetYardsGained();
    float yardsToGo = SingletonBehaviour<FieldManager, MonoBehaviour>.instance.yardsToGo;
    float num2 = yardsToGo - yardsGained;
    int num3 = MatchManager.down - 1;
    bool flag3 = this.commentary.GotFirstDownOnPlay();
    if (flag3)
    {
      if (num3 == 1 && Random.Range(0, 100) < 80)
        this.PlayWasFirstDown_GotFirstDown();
      else if (num3 == 4)
        this.PlayFourthDownConv_Success();
      else if (num3 == 3 && (double) yardsToGo < 4.0 && Random.Range(0, 100) < 80)
        this.PlayWasThirdShort_GotFirstDown();
      else if (num3 == 3 && (double) yardsToGo > 12.0 && Random.Range(0, 100) < 80)
        this.PlayWasThirdLong_GotFirstDown();
      else if (flag1)
        this.PlayGotFirstDown_Pass();
      else if (global::Game.IsRun || global::Game.IsPass && global::Game.BallIsNotThrownOrKicked)
        this.PlayGotFirstDown_Run();
    }
    else if (((num3 != 2 ? 0 : ((double) yardsToGo < 5.0 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0 && (double) num1 > 20.0)
      this.PlayWasSecondShort_IncDeep();
    else if (num3 == 4 && !flag3)
    {
      this.PlayFourthDownConv_Failed();
    }
    else
    {
      switch (num3)
      {
        case 1:
          if ((double) num2 < 5.0)
          {
            this.PlayComingUp_SecondShort();
            break;
          }
          if ((double) num2 > 10.5)
          {
            this.PlayComingUp_SecondLong();
            break;
          }
          break;
        case 2:
          if ((double) num2 < 5.0)
          {
            this.PlayComingUp_ThirdShort();
            break;
          }
          if ((double) num2 > 13.0)
          {
            bool playerOneOnOffense = global::Game.IsPlayerOneOnOffense;
            bool flag4 = ProEra.Game.MatchState.Stats.User.Score < ProEra.Game.MatchState.Stats.Comp.Score;
            bool flag5 = ProEra.Game.MatchState.Stats.Comp.Score < ProEra.Game.MatchState.Stats.User.Score;
            if (Field.FurtherDownfield(Field.GetFieldLocationByYardline(30, true), MatchManager.ballOn) && MatchManager.instance.timeManager.GetQuarter() < 4 && playerOneOnOffense & flag5 || !playerOneOnOffense & flag4)
            {
              this.PlayComingUp_ThirdLong_OwnTerr();
              break;
            }
            this.PlayComingUp_ThirdLong();
            break;
          }
          break;
        case 3:
          if (global::Game.IsRun && (double) yardsGained < 5.0)
          {
            this.PlayWasThirdLong_ShortRun();
            break;
          }
          if (flag1 && (double) yardsGained < 5.0)
          {
            this.PlayWasThirdLong_ShortPass();
            break;
          }
          if (flag2 && (double) num1 > 15.0)
          {
            this.PlayWasThirdLong_IncDeep();
            break;
          }
          if ((double) yardsGained < 0.0)
          {
            this.PlayWasThirdDown_LossOnPlay();
            break;
          }
          break;
      }
    }
    this.commentary.ClearQueueAfterThis();
  }

  private void PlayGotFirstDown_Run() => this.PlayRandomClip(this.downAndDist_firstDownRun);

  private void PlayGotFirstDown_Pass() => this.PlayRandomClip(this.downAndDist_firstDownPass);

  private void PlayWasFirstDown_GotFirstDown() => this.PlayRandomClip(this.downAndDist_wasFirst_gotFirst);

  private void PlayWasSecondShort_IncDeep() => this.PlayRandomClip(this.downAndDist_wasSecondShort_incDeep);

  private void PlayWasThirdShort_GotFirstDown() => this.PlayRandomClip(this.downAndDist_wasThirdShort_firstDown);

  private void PlayWasThirdLong_GotFirstDown()
  {
    AudioPath thirdLongGotFirst = this.downAndDist_wasThirdLong_gotFirst;
    int index = Random.Range(0, thirdLongGotFirst.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(thirdLongGotFirst, index);
    if (index < 6)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamID_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayWasThirdLong_ShortRun() => this.PlayRandomClip(this.downAndDist_wasThirdLong_shortRun);

  private void PlayWasThirdLong_ShortPass() => this.PlayRandomClip(this.downAndDist_wasThirdLong_shortPass);

  private void PlayWasThirdLong_IncDeep() => this.PlayRandomClip(this.downAndDist_wasThirdLong_incDeep);

  private void PlayWasThirdDown_LossOnPlay() => this.PlayRandomClip(this.downAndDist_wasThirdDown_loss);

  private void PlayComingUp_SecondShort() => this.PlayRandomClip(this.downAndDist_comingUp_secondShort);

  private void PlayComingUp_SecondLong() => this.PlayRandomClip(this.downAndDist_comingUp_secondLong);

  private void PlayComingUp_ThirdShort() => this.PlayRandomClip(this.downAndDist_comingUp_thirdShort);

  private void PlayComingUp_ThirdLong() => this.PlayRandomClip(this.downAndDist_comingUp_thirdLong);

  private void PlayComingUp_ThirdLong_OwnTerr() => this.PlayRandomClip(this.downAndDist_comingUp_thirdLongOwnTerr);

  private void PlayFourthDownConv_Success() => this.PlayRandomClip(this.downAndDist_fourthDownAtt_success);

  private void PlayFourthDownConv_Failed() => this.PlayRandomClip(this.downAndDist_fourthDownAtt_failed);

  public void PlayScoreUpdate(int score)
  {
    if ((Object) this.commentary == (Object) null)
      return;
    int score1 = ProEra.Game.MatchState.Stats.User.Score;
    int score2 = ProEra.Game.MatchState.Stats.Comp.Score;
    int num1 = 0;
    bool flag = false;
    float num2 = (float) MatchManager.gameLength - MatchManager.instance.timeManager.GetGameClockTimer();
    switch (score)
    {
      case -2:
        return;
      case 0:
      case 1:
      case 2:
        num1 = 6 + score;
        flag = true;
        break;
      case 3:
        num1 = 3;
        break;
    }
    this.commentary.AddPause();
    if (!this.playedFirstScoreUpdate)
    {
      this.playedFirstScoreUpdate = true;
      if (!flag)
        this.PlayScoreUpdate_FirstScore_FG();
      else
        this.PlayScoreUpdate_FirstScore_TD();
    }
    else if (global::Game.IsPlayerOneOnOffense)
    {
      if (score1 - num1 <= score2)
      {
        if (score1 == score2)
          return;
        if (score1 > score2)
        {
          if (MatchManager.instance.timeManager.IsFourthQuarter() && (double) num2 < 180.0)
            this.PlayScoreUpdate_TakesLead_Q4();
          else
            this.PlayScoreUpdate_TakesLead_Q13();
        }
        else if (score2 - score1 <= 8)
          this.PlayScoreUpdate_StillLosing_OnePos();
        else
          this.PlayScoreUpdate_StillLosing_TwoPos();
      }
      else if (score1 - score2 <= 8)
        this.PlayScoreUpdate_StillWinning_OnePos();
      else
        this.PlayScoreUpdate_StillWinning_TwoPos();
    }
    else if (score2 - num1 <= score1)
    {
      if (score2 == score1)
        return;
      if (score2 > score1)
      {
        if (MatchManager.instance.timeManager.IsFourthQuarter() && (double) num2 < 180.0)
          this.PlayScoreUpdate_TakesLead_Q4();
        else
          this.PlayScoreUpdate_TakesLead_Q13();
      }
      else if (score1 - score2 <= 8)
        this.PlayScoreUpdate_StillLosing_OnePos();
      else
        this.PlayScoreUpdate_StillLosing_TwoPos();
    }
    else if (score2 - score1 <= 8)
      this.PlayScoreUpdate_StillWinning_OnePos();
    else
      this.PlayScoreUpdate_StillWinning_TwoPos();
  }

  private void PlayScoreUpdate_FirstScore_FG()
  {
    AudioPath scoreUpdateFirstFg = this.scoreUpdate_firstFG;
    int index = Random.Range(0, scoreUpdateFirstFg.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(scoreUpdateFirstFg, index);
    if (index < 12)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 16)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_FirstScore_TD()
  {
    AudioPath scoreUpdateFirstTd = this.scoreUpdate_firstTD;
    if ((Object) this.commentary == (Object) null || scoreUpdateFirstTd == null)
      return;
    int index = Random.Range(0, scoreUpdateFirstTd.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(scoreUpdateFirstTd, index);
    if (index < 10)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 12)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_TakesLead_Q13()
  {
    AudioPath updateTakesLeadQ1To3 = this.scoreUpdate_takesLead_Q1To3;
    int index = Random.Range(0, updateTakesLeadQ1To3.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(updateTakesLeadQ1To3, index);
    if (index < 13)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_TakesLead_Q4()
  {
    AudioPath takesLeadAlmostOver = this.scoreUpdate_takesLead_AlmostOver;
    int index = Random.Range(0, takesLeadAlmostOver.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(takesLeadAlmostOver, index);
    if (index < 7)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_StillWinning_OnePos()
  {
    AudioPath stillWinningOnePoss = this.scoreUpdate_stillWinning_onePoss;
    if (stillWinningOnePoss == null)
      return;
    int index = Random.Range(0, stillWinningOnePoss.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(stillWinningOnePoss, index);
    if (index < 4)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 7)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_StillLosing_OnePos()
  {
    AudioPath stillLosingOnePoss = this.scoreUpdate_stillLosing_onePoss;
    if (stillLosingOnePoss == null)
      return;
    int index = Random.Range(0, stillLosingOnePoss.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(stillLosingOnePoss, index);
    if (index < 9)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 12)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_StillWinning_TwoPos()
  {
    AudioPath stillWinningTwoPoss = this.scoreUpdate_stillWinning_TwoPoss;
    if (stillWinningTwoPoss == null)
      return;
    int index = Random.Range(0, stillWinningTwoPoss.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(stillWinningTwoPoss, index);
    if (index < 11)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 14)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayTeamID_Offense(AudioAddition.Postfix);
    }
    else if (index < 16)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  private void PlayScoreUpdate_StillLosing_TwoPos()
  {
    AudioPath stillLosingTwoPoss = this.scoreUpdate_stillLosing_twoPoss;
    if (stillLosingTwoPoss == null)
      return;
    int index = Random.Range(0, stillLosingTwoPoss.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(stillLosingTwoPoss, index);
    if (index < 9)
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    else if (index < 12)
    {
      this.PlayTeamCity_Offense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
    else
    {
      this.PlayTeamCity_Defense(AudioAddition.Prefix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayPlayerOfGameSelection(bool onUserTeam, int indexOnTeam)
  {
    AudioPath studPlayer = this.stud_player;
    int index = Random.Range(0, studPlayer.count) + 1;
    AudioClip audioClip = AudioManager.self.GetAudioClip(studPlayer, index);
    if (index < 11)
    {
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
      this.PlayPlayerID(onUserTeam, indexOnTeam, AudioAddition.Prefix);
      if (onUserTeam)
        this.PlayTeamID_User(AudioAddition.Postfix);
      else
        this.PlayTeamID_Comp(AudioAddition.Postfix);
    }
    else
    {
      this.PlayPlayerID(onUserTeam, indexOnTeam, AudioAddition.Prefix);
      if (onUserTeam)
        this.PlayTeamID_User(AudioAddition.Postfix);
      else
        this.PlayTeamID_Comp(AudioAddition.Postfix);
      this.commentary.AddSegmentToQueue(new AudioSegment(audioClip));
    }
  }

  public void PlayPlayerOfGameOutro()
  {
    this.PlayRandomClip(this.stud_finalRemarks);
    this.commentary.ClearQueueAfterThis();
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

  public void PlayTeamCity_Offense(AudioAddition type)
  {
    int num = Random.Range(0, 100);
    if (num < 10)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheOffense_Prefix();
      else
        this.PlayWord_TheOffense_Postfix();
    }
    else if (num < 90)
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

  public void PlayTeamCity_Defense(AudioAddition type)
  {
    int num = Random.Range(0, 100);
    if (num < 10)
    {
      if (type == AudioAddition.Prefix)
        this.PlayWord_TheDefense_Prefix();
      else
        this.PlayWord_TheDefense_Postfix();
    }
    else if (num < 90)
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

  public void PlayCompTeamCity_Prefix()
  {
    if (!PersistentData.userIsHome)
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

  public void PlayCompTeamCity_Postfix()
  {
    if (!PersistentData.userIsHome)
      this.PlayHomeTeamCity_Postfix();
    else
      this.PlayAwayTeamCity_Postfix();
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
}
