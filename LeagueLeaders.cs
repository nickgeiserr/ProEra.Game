// Decompiled with JetBrains decompiler
// Type: LeagueLeaders
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class LeagueLeaders : MonoBehaviour
{
  [Header("Main")]
  private int selectedCategoryIndex;
  private bool calculatedPass;
  private bool calculatedRush;
  private bool calculatedRec;
  private bool calculatedDef;
  private bool calculatedKicking;
  private bool calculatedReturns;
  private int selectedBlockIndex;
  private SeasonTabletStatsScreen _stss;
  [HideInInspector]
  public int[] passYards_playerIndex;
  [HideInInspector]
  public int[] passYards_teamIndex;
  [HideInInspector]
  public int[] passYards_value;
  private int[] qbRating_playerIndex;
  private int[] qbRating_teamIndex;
  private int[] qbRating_value;
  private int[] passTDs_playerIndex;
  private int[] passTDs_teamIndex;
  private int[] passTDs_value;
  private int[] compPer_playerIndex;
  private int[] compPer_teamIndex;
  private float[] compPer_value;
  private int[] passYardsPerGame_playerIndex;
  private int[] passYardsPerGame_teamIndex;
  private float[] passYardsPerGame_value;
  private int[] passYardsPerPass_playerIndex;
  private int[] passYardsPerPass_teamIndex;
  private float[] passYardsPerPass_value;
  private int[] qbInts_playerIndex;
  private int[] qbInts_teamIndex;
  private int[] qbInts_value;
  private int[] passComp_playerIndex;
  private int[] passComp_teamIndex;
  private int[] passComp_value;
  private int[] passAtt_playerIndex;
  private int[] passAtt_teamIndex;
  private int[] passAtt_value;
  [HideInInspector]
  public int[] rushYards_playerIndex;
  [HideInInspector]
  public int[] rushYards_teamIndex;
  [HideInInspector]
  public int[] rushYards_value;
  private int[] rushTDs_playerIndex;
  private int[] rushTDs_teamIndex;
  private int[] rushTDs_value;
  private int[] yardsPerRush_playerIndex;
  private int[] yardsPerRush_teamIndex;
  private float[] yardsPerRush_value;
  private int[] yardsPerGame_playerIndex;
  private int[] yardsPerGame_teamIndex;
  private float[] yardsPerGame_value;
  private int[] rushAttempts_playerIndex;
  private int[] rushAttempts_teamIndex;
  private int[] rushAttempts_value;
  private int[] fumbles_playerIndex;
  private int[] fumbles_teamIndex;
  private int[] fumbles_value;
  [HideInInspector]
  public int[] recYards_playerIndex;
  [HideInInspector]
  public int[] recYards_teamIndex;
  [HideInInspector]
  public int[] recYards_value;
  private int[] recTDs_playerIndex;
  private int[] recTDs_teamIndex;
  private int[] recTDs_value;
  private int[] yardsPerCatch_playerIndex;
  private int[] yardsPerCatch_teamIndex;
  private float[] yardsPerCatch_value;
  private int[] recYardsPerGame_playerIndex;
  private int[] recYardsPerGame_teamIndex;
  private float[] recYardsPerGame_value;
  private int[] catches_playerIndex;
  private int[] catches_teamIndex;
  private int[] catches_value;
  private int[] yac_playerIndex;
  private int[] yac_teamIndex;
  private int[] yac_value;
  private int[] drops_playerIndex;
  private int[] drops_teamIndex;
  private int[] drops_value;
  private int[] tackles_playerIndex;
  private int[] tackles_teamIndex;
  private int[] tackles_value;
  private int[] sacks_playerIndex;
  private int[] sacks_teamIndex;
  private int[] sacks_value;
  private int[] ints_playerIndex;
  private int[] ints_teamIndex;
  private int[] ints_value;
  private int[] deflectedPasses_playerIndex;
  private int[] deflectedPasses_teamIndex;
  private int[] deflectedPasses_value;
  private int[] tacklesForLoss_playerIndex;
  private int[] tacklesForLoss_teamIndex;
  private int[] tacklesForLoss_value;
  private int[] forcedFumbles_playerIndex;
  private int[] forcedFumbles_teamIndex;
  private int[] forcedFumbles_value;
  private int[] fumRec_playerIndex;
  private int[] fumRec_teamIndex;
  private int[] fumRec_value;
  private int[] defTDs_playerIndex;
  private int[] defTDs_teamIndex;
  private int[] defTDs_value;
  private int[] fgAttempts_playerIndex;
  private int[] fgAttempts_teamIndex;
  private int[] fgAttempts_value;
  private int[] fgPercentage_playerIndex;
  private int[] fgPercentage_teamIndex;
  private float[] fgPercentage_value;
  private int[] xpAttempts_playerIndex;
  private int[] xpAttempts_teamIndex;
  private int[] xpAttempts_value;
  private int[] xpPercentage_playerIndex;
  private int[] xpPercentage_teamIndex;
  private float[] xpPercentage_value;
  private int[] punts_playerIndex;
  private int[] punts_teamIndex;
  private int[] punts_value;
  private int[] puntAverage_playerIndex;
  private int[] puntAverage_teamIndex;
  private float[] puntAverage_value;
  private int[] puntsIn20_playerIndex;
  private int[] puntsIn20_teamIndex;
  private int[] puntsIn20_value;
  private int[] kickReturns_playerIndex;
  private int[] kickReturns_teamIndex;
  private int[] kickReturns_value;
  private int[] kickReturnAverage_playerIndex;
  private int[] kickReturnAverage_teamIndex;
  private float[] kickReturnAverage_value;
  private int[] kickReturnTDs_playerIndex;
  private int[] kickReturnTDs_teamIndex;
  private int[] kickReturnTDs_value;
  private int[] puntReturns_playerIndex;
  private int[] puntReturns_teamIndex;
  private int[] puntReturns_value;
  private int[] puntReturnAverage_playerIndex;
  private int[] puntReturnAverage_teamIndex;
  private float[] puntReturnAverage_value;
  private int[] puntReturnTDs_playerIndex;
  private int[] puntReturnTDs_teamIndex;
  private int[] puntReturnTDs_value;
  private SeasonModeManager seasonMode;
  private SGD_SeasonModeData seasonModeData;

  public void Init()
  {
    this.seasonMode = SeasonModeManager.self;
    this.seasonModeData = this.seasonMode.seasonModeData;
  }

  public void ShowWindow()
  {
    this.ResetCalculatedStatus();
    this.ShowPassingCategory();
  }

  public void SelectPrevCategory()
  {
    if (this.selectedCategoryIndex == 0)
      this.ShowReturnsCategory();
    else if (this.selectedCategoryIndex == 1)
      this.ShowPassingCategory();
    else if (this.selectedCategoryIndex == 2)
      this.ShowRushingCategory();
    else if (this.selectedCategoryIndex == 3)
      this.ShowReceivingCategory();
    else if (this.selectedCategoryIndex == 4)
    {
      this.ShowDefenseCategory();
    }
    else
    {
      if (this.selectedCategoryIndex != 5)
        return;
      this.ShowKickingCategory();
    }
  }

  public void SelectNextCategory()
  {
    if (this.selectedCategoryIndex == 0)
      this.ShowRushingCategory();
    else if (this.selectedCategoryIndex == 1)
      this.ShowReceivingCategory();
    else if (this.selectedCategoryIndex == 2)
      this.ShowDefenseCategory();
    else if (this.selectedCategoryIndex == 3)
      this.ShowKickingCategory();
    else if (this.selectedCategoryIndex == 4)
    {
      this.ShowReturnsCategory();
    }
    else
    {
      if (this.selectedCategoryIndex != 5)
        return;
      this.ShowPassingCategory();
    }
  }

  private void ShowPassingCategory()
  {
    this.selectedCategoryIndex = 0;
    this.ShowPassingLeaders();
  }

  private void ShowRushingCategory()
  {
    this.selectedCategoryIndex = 1;
    this.ShowRushingLeaders();
  }

  private void ShowReceivingCategory() => this.selectedCategoryIndex = 2;

  private void ShowDefenseCategory()
  {
    this.selectedCategoryIndex = 3;
    this.ShowDefenseLeaders();
  }

  private void ShowKickingCategory()
  {
    this.selectedCategoryIndex = 4;
    this.ShowKickingLeaders();
  }

  private void ShowReturnsCategory()
  {
    this.selectedCategoryIndex = 5;
    this.ShowReturnsLeaders();
  }

  public LeagueLeaders.Leaders GetPassingLeaders()
  {
    this.CalculateLeaders_Pass();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.passYards_value[index].ToString();
    return new LeagueLeaders.Leaders(this.passYards_playerIndex, this.passYards_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetRushingLeaders()
  {
    this.CalculateLeaders_Rush();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.rushYards_value[index].ToString();
    return new LeagueLeaders.Leaders(this.rushYards_playerIndex, this.rushYards_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetReceivingLeaders()
  {
    this.CalculateLeaders_Rec();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.recYards_value[index].ToString();
    return new LeagueLeaders.Leaders(this.recYards_playerIndex, this.recYards_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetTackleLeaders()
  {
    this.CalculateLeaders_Defense();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.tackles_value[index].ToString();
    return new LeagueLeaders.Leaders(this.tackles_playerIndex, this.tackles_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetINTLeaders()
  {
    this.CalculateLeaders_Defense();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.ints_value[index].ToString();
    return new LeagueLeaders.Leaders(this.ints_playerIndex, this.ints_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetSackLeaders()
  {
    this.CalculateLeaders_Defense();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.sacks_value[index].ToString();
    return new LeagueLeaders.Leaders(this.sacks_playerIndex, this.sacks_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetFGLeaders()
  {
    this.CalculateLeaders_Kicking();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = (this.fgPercentage_value[index] * 100f).ToString();
    return new LeagueLeaders.Leaders(this.fgPercentage_playerIndex, this.fgPercentage_teamIndex, v);
  }

  public LeagueLeaders.Leaders GetPuntLeaders()
  {
    this.CalculateLeaders_Kicking();
    string[] v = new string[5];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.puntAverage_value[index].ToString("0.##");
    return new LeagueLeaders.Leaders(this.puntAverage_playerIndex, this.puntAverage_teamIndex, v);
  }

  private void ShowPassingLeaders()
  {
    this.CalculateLeaders_Pass();
    string[] v = new string[5];
    LeagueLeaders.Leaders[] leadersArray = new LeagueLeaders.Leaders[9];
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.qbRating_value[index].ToString();
    leadersArray[0] = new LeagueLeaders.Leaders(this.qbRating_playerIndex, this.qbRating_teamIndex, v);
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.passYards_value[index].ToString();
    leadersArray[1] = new LeagueLeaders.Leaders(this.passYards_playerIndex, this.passYards_teamIndex, v);
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.passTDs_value[index].ToString();
    for (int index = 0; index < v.Length; ++index)
      v[index] = string.Format("{0:0.#}", (object) (float) ((double) this.compPer_value[index] * 100.0)) + "%";
    for (int index = 0; index < v.Length; ++index)
      v[index] = string.Format("{0:0.#}", (object) this.passYardsPerGame_value[index]);
    for (int index = 0; index < v.Length; ++index)
      v[index] = string.Format("{0:0.#}", (object) this.passYardsPerPass_value[index]);
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.qbInts_value[index].ToString();
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.passComp_value[index].ToString();
    for (int index = 0; index < v.Length; ++index)
      v[index] = this.passAtt_value[index].ToString();
  }

  private void ShowRushingLeaders()
  {
    this.CalculateLeaders_Rush();
    string[] strArray = new string[5];
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.rushYards_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.rushTDs_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.yardsPerRush_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.yardsPerGame_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.rushAttempts_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.fumbles_value[index].ToString();
  }

  private void ShowReceivingLeaders()
  {
    this.CalculateLeaders_Rec();
    string[] strArray = new string[5];
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.recYards_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.recTDs_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.yardsPerCatch_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.recYardsPerGame_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.catches_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.yac_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.drops_value[index].ToString();
  }

  private void ShowDefenseLeaders()
  {
    this.CalculateLeaders_Defense();
    string[] strArray = new string[5];
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.tackles_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.sacks_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.ints_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.deflectedPasses_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.tacklesForLoss_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.forcedFumbles_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.fumRec_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.defTDs_value[index].ToString();
  }

  private void ShowKickingLeaders()
  {
    this.CalculateLeaders_Kicking();
    string[] strArray = new string[5];
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.fgAttempts_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) (float) ((double) this.fgPercentage_value[index] * 100.0)) + "%";
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.xpAttempts_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) (float) ((double) this.xpPercentage_value[index] * 100.0)) + "%";
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.punts_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.puntAverage_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.puntsIn20_value[index].ToString();
  }

  private void ShowReturnsLeaders()
  {
    this.CalculateLeaders_Returns();
    string[] strArray = new string[5];
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.kickReturns_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.kickReturnAverage_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.kickReturnTDs_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.puntReturns_value[index].ToString();
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = string.Format("{0:0.#}", (object) this.puntReturnAverage_value[index]);
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = this.puntReturnTDs_value[index].ToString();
  }

  public void CalculateLeaders_Pass()
  {
    if (this.calculatedPass)
      return;
    this.calculatedPass = true;
    this.CreateArrayCategories_Pass();
    int num1 = Mathf.Max(1, this.seasonModeData.currentWeek - 1);
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null && player.CurrentSeasonStats.QBAttempts > 0)
        {
          int qbRating = player.CurrentSeasonStats.GetQBRating();
          for (int index2 = 0; index2 < 5; ++index2)
          {
            if (qbRating > this.qbRating_value[index2])
            {
              for (int index3 = 3; index3 >= index2; --index3)
              {
                this.qbRating_value[index3 + 1] = this.qbRating_value[index3];
                this.qbRating_playerIndex[index3 + 1] = this.qbRating_playerIndex[index3];
                this.qbRating_teamIndex[index3 + 1] = this.qbRating_teamIndex[index3];
              }
              this.qbRating_value[index2] = qbRating;
              this.qbRating_playerIndex[index2] = playerIndex;
              this.qbRating_teamIndex[index2] = teamData.TeamIndex;
              break;
            }
          }
          int qbPassYards = player.CurrentSeasonStats.QBPassYards;
          for (int index4 = 0; index4 < 5; ++index4)
          {
            if (qbPassYards > this.passYards_value[index4])
            {
              for (int index5 = 3; index5 >= index4; --index5)
              {
                this.passYards_value[index5 + 1] = this.passYards_value[index5];
                this.passYards_teamIndex[index5 + 1] = this.passYards_teamIndex[index5];
                this.passYards_playerIndex[index5 + 1] = this.passYards_playerIndex[index5];
              }
              this.passYards_value[index4] = qbPassYards;
              this.passYards_playerIndex[index4] = playerIndex;
              this.passYards_teamIndex[index4] = teamData.TeamIndex;
              break;
            }
          }
          int qbPassTds = player.CurrentSeasonStats.QBPassTDs;
          for (int index6 = 0; index6 < 5; ++index6)
          {
            if (qbPassTds > this.passTDs_value[index6])
            {
              for (int index7 = 3; index7 >= index6; --index7)
              {
                this.passTDs_value[index7 + 1] = this.passTDs_value[index7];
                this.passTDs_teamIndex[index7 + 1] = this.passTDs_teamIndex[index7];
                this.passTDs_playerIndex[index7 + 1] = this.passTDs_playerIndex[index7];
              }
              this.passTDs_value[index6] = qbPassTds;
              this.passTDs_teamIndex[index6] = teamData.TeamIndex;
              this.passTDs_playerIndex[index6] = playerIndex;
              break;
            }
          }
          float num2 = (float) player.CurrentSeasonStats.QBCompletions / (float) player.CurrentSeasonStats.QBAttempts;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            if ((double) num2 > (double) this.compPer_value[index8])
            {
              for (int index9 = 3; index9 >= index8; --index9)
              {
                this.compPer_value[index9 + 1] = this.compPer_value[index9];
                this.compPer_teamIndex[index9 + 1] = this.compPer_teamIndex[index9];
                this.compPer_playerIndex[index9 + 1] = this.compPer_playerIndex[index9];
              }
              this.compPer_value[index8] = num2;
              this.compPer_teamIndex[index8] = teamData.TeamIndex;
              this.compPer_playerIndex[index8] = playerIndex;
              break;
            }
          }
          float num3 = (float) player.CurrentSeasonStats.QBPassYards / (float) num1;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            if ((double) num3 > (double) this.passYardsPerGame_value[index10])
            {
              for (int index11 = 3; index11 >= index10; --index11)
              {
                this.passYardsPerGame_value[index11 + 1] = this.passYardsPerGame_value[index11];
                this.passYardsPerGame_teamIndex[index11 + 1] = this.passYardsPerGame_teamIndex[index11];
                this.passYardsPerGame_playerIndex[index11 + 1] = this.passYardsPerGame_playerIndex[index11];
              }
              this.passYardsPerGame_value[index10] = num3;
              this.passYardsPerGame_teamIndex[index10] = teamData.TeamIndex;
              this.passYardsPerGame_playerIndex[index10] = playerIndex;
              break;
            }
          }
          float num4 = (float) player.CurrentSeasonStats.QBPassYards / (float) player.CurrentSeasonStats.QBCompletions;
          for (int index12 = 0; index12 < 5; ++index12)
          {
            if ((double) num4 > (double) this.passYardsPerPass_value[index12])
            {
              for (int index13 = 3; index13 >= index12; --index13)
              {
                this.passYardsPerPass_value[index13 + 1] = this.passYardsPerPass_value[index13];
                this.passYardsPerPass_teamIndex[index13 + 1] = this.passYardsPerPass_teamIndex[index13];
                this.passYardsPerPass_playerIndex[index13 + 1] = this.passYardsPerPass_playerIndex[index13];
              }
              this.passYardsPerPass_value[index12] = num4;
              this.passYardsPerPass_teamIndex[index12] = teamData.TeamIndex;
              this.passYardsPerPass_playerIndex[index12] = playerIndex;
              break;
            }
          }
          int qbInts = player.CurrentSeasonStats.QBInts;
          if (player.CurrentSeasonStats.QBAttempts > 30)
          {
            for (int index14 = 0; index14 < 5; ++index14)
            {
              if (qbInts < this.qbInts_value[index14])
              {
                for (int index15 = 3; index15 >= index14; --index15)
                {
                  this.qbInts_value[index15 + 1] = this.qbInts_value[index15];
                  this.qbInts_teamIndex[index15 + 1] = this.qbInts_teamIndex[index15];
                  this.qbInts_playerIndex[index15 + 1] = this.qbInts_playerIndex[index15];
                }
                this.qbInts_value[index14] = qbInts;
                this.qbInts_teamIndex[index14] = teamData.TeamIndex;
                this.qbInts_playerIndex[index14] = playerIndex;
                break;
              }
            }
          }
          int qbCompletions = player.CurrentSeasonStats.QBCompletions;
          for (int index16 = 0; index16 < 5; ++index16)
          {
            if (qbCompletions > this.passComp_value[index16])
            {
              for (int index17 = 3; index17 >= index16; --index17)
              {
                this.passComp_value[index17 + 1] = this.passComp_value[index17];
                this.passComp_teamIndex[index17 + 1] = this.passComp_teamIndex[index17];
                this.passComp_playerIndex[index17 + 1] = this.passComp_playerIndex[index17];
              }
              this.passComp_value[index16] = qbCompletions;
              this.passComp_teamIndex[index16] = teamData.TeamIndex;
              this.passComp_playerIndex[index16] = playerIndex;
              break;
            }
          }
          int qbAttempts = player.CurrentSeasonStats.QBAttempts;
          for (int index18 = 0; index18 < 5; ++index18)
          {
            if (qbAttempts > this.passAtt_value[index18])
            {
              for (int index19 = 3; index19 >= index18; --index19)
              {
                this.passAtt_value[index19 + 1] = this.passAtt_value[index19];
                this.passAtt_teamIndex[index19 + 1] = this.passAtt_teamIndex[index19];
                this.passAtt_playerIndex[index19 + 1] = this.passAtt_playerIndex[index19];
              }
              this.passAtt_value[index18] = qbAttempts;
              this.passAtt_teamIndex[index18] = teamData.TeamIndex;
              this.passAtt_playerIndex[index18] = playerIndex;
              break;
            }
          }
        }
      }
    }
  }

  public void CalculateLeaders_Rush()
  {
    if (this.calculatedRush)
      return;
    this.calculatedRush = true;
    this.CreateArrayCategories_Rush();
    int num1 = Mathf.Max(1, this.seasonModeData.currentWeek - 1);
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null && player.CurrentSeasonStats.RushAttempts > 0)
        {
          int rushYards = player.CurrentSeasonStats.RushYards;
          for (int index2 = 0; index2 < 5; ++index2)
          {
            if (rushYards > this.rushYards_value[index2])
            {
              for (int index3 = 3; index3 >= index2; --index3)
              {
                this.rushYards_value[index3 + 1] = this.rushYards_value[index3];
                this.rushYards_playerIndex[index3 + 1] = this.rushYards_playerIndex[index3];
                this.rushYards_teamIndex[index3 + 1] = this.rushYards_teamIndex[index3];
              }
              this.rushYards_value[index2] = rushYards;
              this.rushYards_playerIndex[index2] = playerIndex;
              this.rushYards_teamIndex[index2] = teamData.TeamIndex;
              break;
            }
          }
          int rushTds = player.CurrentSeasonStats.RushTDs;
          for (int index4 = 0; index4 < 5; ++index4)
          {
            if (rushTds > this.rushTDs_value[index4])
            {
              for (int index5 = 3; index5 >= index4; --index5)
              {
                this.rushTDs_value[index5 + 1] = this.rushTDs_value[index5];
                this.rushTDs_playerIndex[index5 + 1] = this.rushTDs_playerIndex[index5];
                this.rushTDs_teamIndex[index5 + 1] = this.rushTDs_teamIndex[index5];
              }
              this.rushTDs_value[index4] = rushTds;
              this.rushTDs_playerIndex[index4] = playerIndex;
              this.rushTDs_teamIndex[index4] = teamData.TeamIndex;
              break;
            }
          }
          float num2 = (float) player.CurrentSeasonStats.RushYards / (float) player.CurrentSeasonStats.RushAttempts;
          for (int index6 = 0; index6 < 5; ++index6)
          {
            if ((double) num2 > (double) this.yardsPerRush_value[index6])
            {
              for (int index7 = 3; index7 >= index6; --index7)
              {
                this.yardsPerRush_value[index7 + 1] = this.yardsPerRush_value[index7];
                this.yardsPerRush_playerIndex[index7 + 1] = this.yardsPerRush_playerIndex[index7];
                this.yardsPerRush_teamIndex[index7 + 1] = this.yardsPerRush_teamIndex[index7];
              }
              this.yardsPerRush_value[index6] = num2;
              this.yardsPerRush_playerIndex[index6] = playerIndex;
              this.yardsPerRush_teamIndex[index6] = teamData.TeamIndex;
              break;
            }
          }
          float num3 = (float) player.CurrentSeasonStats.RushYards / (float) num1;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            if ((double) num3 > (double) this.yardsPerGame_value[index8])
            {
              for (int index9 = 3; index9 >= index8; --index9)
              {
                this.yardsPerGame_value[index9 + 1] = this.yardsPerGame_value[index9];
                this.yardsPerGame_playerIndex[index9 + 1] = this.yardsPerGame_playerIndex[index9];
                this.yardsPerGame_teamIndex[index9 + 1] = this.yardsPerGame_teamIndex[index9];
              }
              this.yardsPerGame_value[index8] = num3;
              this.yardsPerGame_playerIndex[index8] = playerIndex;
              this.yardsPerGame_teamIndex[index8] = teamData.TeamIndex;
              break;
            }
          }
          int rushAttempts = player.CurrentSeasonStats.RushAttempts;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            if (rushAttempts > this.rushAttempts_value[index10])
            {
              for (int index11 = 3; index11 >= index10; --index11)
              {
                this.rushAttempts_value[index11 + 1] = this.rushAttempts_value[index11];
                this.rushAttempts_playerIndex[index11 + 1] = this.rushAttempts_playerIndex[index11];
                this.rushAttempts_teamIndex[index11 + 1] = this.rushAttempts_teamIndex[index11];
              }
              this.rushAttempts_value[index10] = rushAttempts;
              this.rushAttempts_playerIndex[index10] = playerIndex;
              this.rushAttempts_teamIndex[index10] = teamData.TeamIndex;
              break;
            }
          }
          if (player.CurrentSeasonStats.RushAttempts > 30)
          {
            int fumbles = player.CurrentSeasonStats.Fumbles;
            for (int index12 = 0; index12 < 5; ++index12)
            {
              if (fumbles < this.fumbles_value[index12])
              {
                for (int index13 = 3; index13 >= index12; --index13)
                {
                  this.fumbles_value[index13 + 1] = this.fumbles_value[index13];
                  this.fumbles_playerIndex[index13 + 1] = this.fumbles_playerIndex[index13];
                  this.fumbles_teamIndex[index13 + 1] = this.fumbles_teamIndex[index13];
                }
                this.fumbles_value[index12] = fumbles;
                this.fumbles_playerIndex[index12] = playerIndex;
                this.fumbles_teamIndex[index12] = teamData.TeamIndex;
                break;
              }
            }
          }
        }
      }
    }
  }

  public void CalculateLeaders_Rec()
  {
    if (this.calculatedRec)
      return;
    this.calculatedRec = true;
    this.CreateArrayCategories_Receiving();
    int num1 = Mathf.Max(1, this.seasonModeData.currentWeek - 1);
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null && player.CurrentSeasonStats.Receptions > 0)
        {
          int receivingYards = player.CurrentSeasonStats.ReceivingYards;
          for (int index2 = 0; index2 < 5; ++index2)
          {
            if (receivingYards > this.recYards_value[index2])
            {
              for (int index3 = 3; index3 >= index2; --index3)
              {
                this.recYards_value[index3 + 1] = this.recYards_value[index3];
                this.recYards_playerIndex[index3 + 1] = this.recYards_playerIndex[index3];
                this.recYards_teamIndex[index3 + 1] = this.recYards_teamIndex[index3];
              }
              this.recYards_value[index2] = receivingYards;
              this.recYards_playerIndex[index2] = playerIndex;
              this.recYards_teamIndex[index2] = teamData.TeamIndex;
              break;
            }
          }
          int receivingTds = player.CurrentSeasonStats.ReceivingTDs;
          for (int index4 = 0; index4 < 5; ++index4)
          {
            if (receivingTds > this.recTDs_value[index4])
            {
              for (int index5 = 3; index5 >= index4; --index5)
              {
                this.recTDs_value[index5 + 1] = this.recTDs_value[index5];
                this.recTDs_playerIndex[index5 + 1] = this.recTDs_playerIndex[index5];
                this.recTDs_teamIndex[index5 + 1] = this.recTDs_teamIndex[index5];
              }
              this.recTDs_value[index4] = receivingTds;
              this.recTDs_playerIndex[index4] = playerIndex;
              this.recTDs_teamIndex[index4] = teamData.TeamIndex;
              break;
            }
          }
          float num2 = (float) player.CurrentSeasonStats.ReceivingYards / (float) player.CurrentSeasonStats.Receptions;
          for (int index6 = 0; index6 < 5; ++index6)
          {
            if ((double) num2 > (double) this.yardsPerCatch_value[index6])
            {
              for (int index7 = 3; index7 >= index6; --index7)
              {
                this.yardsPerCatch_value[index7 + 1] = this.yardsPerCatch_value[index7];
                this.yardsPerCatch_playerIndex[index7 + 1] = this.yardsPerCatch_playerIndex[index7];
                this.yardsPerCatch_teamIndex[index7 + 1] = this.yardsPerCatch_teamIndex[index7];
              }
              this.yardsPerCatch_value[index6] = num2;
              this.yardsPerCatch_playerIndex[index6] = playerIndex;
              this.yardsPerCatch_teamIndex[index6] = teamData.TeamIndex;
              break;
            }
          }
          float num3 = (float) player.CurrentSeasonStats.ReceivingYards / (float) num1;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            if ((double) num3 > (double) this.recYardsPerGame_value[index8])
            {
              for (int index9 = 3; index9 >= index8; --index9)
              {
                this.recYardsPerGame_value[index9 + 1] = this.recYardsPerGame_value[index9];
                this.recYardsPerGame_playerIndex[index9 + 1] = this.recYardsPerGame_playerIndex[index9];
                this.recYardsPerGame_teamIndex[index9 + 1] = this.recYardsPerGame_teamIndex[index9];
              }
              this.recYardsPerGame_value[index8] = num3;
              this.recYardsPerGame_playerIndex[index8] = playerIndex;
              this.recYardsPerGame_teamIndex[index8] = teamData.TeamIndex;
              break;
            }
          }
          int receptions = player.CurrentSeasonStats.Receptions;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            if (receptions > this.catches_value[index10])
            {
              for (int index11 = 3; index11 >= index10; --index11)
              {
                this.catches_value[index11 + 1] = this.catches_value[index11];
                this.catches_playerIndex[index11 + 1] = this.catches_playerIndex[index11];
                this.catches_teamIndex[index11 + 1] = this.catches_teamIndex[index11];
              }
              this.catches_value[index10] = receptions;
              this.catches_playerIndex[index10] = playerIndex;
              this.catches_teamIndex[index10] = teamData.TeamIndex;
              break;
            }
          }
          int yardsAfterCatch = player.CurrentSeasonStats.YardsAfterCatch;
          for (int index12 = 0; index12 < 5; ++index12)
          {
            if (yardsAfterCatch > this.yac_value[index12])
            {
              for (int index13 = 3; index13 >= index12; --index13)
              {
                this.yac_value[index13 + 1] = this.yac_value[index13];
                this.yac_playerIndex[index13 + 1] = this.yac_playerIndex[index13];
                this.yac_teamIndex[index13 + 1] = this.yac_teamIndex[index13];
              }
              this.yac_value[index12] = yardsAfterCatch;
              this.yac_playerIndex[index12] = playerIndex;
              this.yac_teamIndex[index12] = teamData.TeamIndex;
              break;
            }
          }
          if (player.CurrentSeasonStats.Receptions > 20)
          {
            int drops = player.CurrentSeasonStats.Drops;
            for (int index14 = 0; index14 < 5; ++index14)
            {
              if (drops < this.drops_value[index14])
              {
                for (int index15 = 3; index15 >= index14; --index15)
                {
                  this.drops_value[index15 + 1] = this.drops_value[index15];
                  this.drops_playerIndex[index15 + 1] = this.drops_playerIndex[index15];
                  this.drops_teamIndex[index15 + 1] = this.drops_teamIndex[index15];
                }
                this.drops_value[index14] = drops;
                this.drops_playerIndex[index14] = playerIndex;
                this.drops_teamIndex[index14] = teamData.TeamIndex;
                break;
              }
            }
          }
        }
      }
    }
  }

  public void CalculateLeaders_Defense()
  {
    if (this.calculatedDef)
      return;
    this.calculatedDef = true;
    this.CreateArrayCategories_Defense();
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null && (player.CurrentSeasonStats.Tackles > 0 || player.CurrentSeasonStats.Interceptions > 0 || player.CurrentSeasonStats.KnockDowns > 0))
        {
          int tackles = player.CurrentSeasonStats.Tackles;
          for (int index2 = 0; index2 < 5; ++index2)
          {
            if (tackles > this.tackles_value[index2])
            {
              for (int index3 = 3; index3 >= index2; --index3)
              {
                this.tackles_value[index3 + 1] = this.tackles_value[index3];
                this.tackles_playerIndex[index3 + 1] = this.tackles_playerIndex[index3];
                this.tackles_teamIndex[index3 + 1] = this.tackles_teamIndex[index3];
              }
              this.tackles_value[index2] = tackles;
              this.tackles_playerIndex[index2] = playerIndex;
              this.tackles_teamIndex[index2] = teamData.TeamIndex;
              break;
            }
          }
          int sacks = player.CurrentSeasonStats.Sacks;
          for (int index4 = 0; index4 < 5; ++index4)
          {
            if (sacks > this.sacks_value[index4])
            {
              for (int index5 = 3; index5 >= index4; --index5)
              {
                this.sacks_value[index5 + 1] = this.sacks_value[index5];
                this.sacks_playerIndex[index5 + 1] = this.sacks_playerIndex[index5];
                this.sacks_teamIndex[index5 + 1] = this.sacks_teamIndex[index5];
              }
              this.sacks_value[index4] = sacks;
              this.sacks_playerIndex[index4] = playerIndex;
              this.sacks_teamIndex[index4] = teamData.TeamIndex;
              break;
            }
          }
          int interceptions = player.CurrentSeasonStats.Interceptions;
          for (int index6 = 0; index6 < 5; ++index6)
          {
            if (interceptions > this.ints_value[index6])
            {
              for (int index7 = 3; index7 >= index6; --index7)
              {
                this.ints_value[index7 + 1] = this.ints_value[index7];
                this.ints_playerIndex[index7 + 1] = this.ints_playerIndex[index7];
                this.ints_teamIndex[index7 + 1] = this.ints_teamIndex[index7];
              }
              this.ints_value[index6] = interceptions;
              this.ints_playerIndex[index6] = playerIndex;
              this.ints_teamIndex[index6] = teamData.TeamIndex;
              break;
            }
          }
          int knockDowns = player.CurrentSeasonStats.KnockDowns;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            if (knockDowns > this.deflectedPasses_value[index8])
            {
              for (int index9 = 3; index9 >= index8; --index9)
              {
                this.deflectedPasses_value[index9 + 1] = this.deflectedPasses_value[index9];
                this.deflectedPasses_playerIndex[index9 + 1] = this.deflectedPasses_playerIndex[index9];
                this.deflectedPasses_teamIndex[index9 + 1] = this.deflectedPasses_teamIndex[index9];
              }
              this.deflectedPasses_value[index8] = knockDowns;
              this.deflectedPasses_playerIndex[index8] = playerIndex;
              this.deflectedPasses_teamIndex[index8] = teamData.TeamIndex;
              break;
            }
          }
          int forcedFumbles = player.CurrentSeasonStats.ForcedFumbles;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            if (forcedFumbles > this.forcedFumbles_value[index10])
            {
              for (int index11 = 3; index11 >= index10; --index11)
              {
                this.forcedFumbles_value[index11 + 1] = this.forcedFumbles_value[index11];
                this.forcedFumbles_playerIndex[index11 + 1] = this.forcedFumbles_playerIndex[index11];
                this.forcedFumbles_teamIndex[index11 + 1] = this.forcedFumbles_teamIndex[index11];
              }
              this.forcedFumbles_value[index10] = forcedFumbles;
              this.forcedFumbles_playerIndex[index10] = playerIndex;
              this.forcedFumbles_teamIndex[index10] = teamData.TeamIndex;
              break;
            }
          }
          int fumbleRecoveries = player.CurrentSeasonStats.FumbleRecoveries;
          for (int index12 = 0; index12 < 5; ++index12)
          {
            if (fumbleRecoveries > this.fumRec_value[index12])
            {
              for (int index13 = 3; index13 >= index12; --index13)
              {
                this.fumRec_value[index13 + 1] = this.fumRec_value[index13];
                this.fumRec_playerIndex[index13 + 1] = this.fumRec_playerIndex[index13];
                this.fumRec_teamIndex[index13 + 1] = this.fumRec_teamIndex[index13];
              }
              this.fumRec_value[index12] = fumbleRecoveries;
              this.fumRec_playerIndex[index12] = playerIndex;
              this.fumRec_teamIndex[index12] = teamData.TeamIndex;
              break;
            }
          }
          int tacklesForLoss = player.CurrentSeasonStats.TacklesForLoss;
          for (int index14 = 0; index14 < 5; ++index14)
          {
            if (tacklesForLoss > this.tacklesForLoss_value[index14])
            {
              for (int index15 = 3; index15 >= index14; --index15)
              {
                this.tacklesForLoss_value[index15 + 1] = this.tacklesForLoss_value[index15];
                this.tacklesForLoss_playerIndex[index15 + 1] = this.tacklesForLoss_playerIndex[index15];
                this.tacklesForLoss_teamIndex[index15 + 1] = this.tacklesForLoss_teamIndex[index15];
              }
              this.tacklesForLoss_value[index14] = tacklesForLoss;
              this.tacklesForLoss_playerIndex[index14] = playerIndex;
              this.tacklesForLoss_teamIndex[index14] = teamData.TeamIndex;
              break;
            }
          }
          int defensiveTds = player.CurrentSeasonStats.DefensiveTDs;
          for (int index16 = 0; index16 < 5; ++index16)
          {
            if (defensiveTds > this.defTDs_value[index16])
            {
              for (int index17 = 3; index17 >= index16; --index17)
              {
                this.defTDs_value[index17 + 1] = this.defTDs_value[index17];
                this.defTDs_playerIndex[index17 + 1] = this.defTDs_playerIndex[index17];
                this.defTDs_teamIndex[index17 + 1] = this.defTDs_teamIndex[index17];
              }
              this.defTDs_value[index16] = defensiveTds;
              this.defTDs_playerIndex[index16] = playerIndex;
              this.defTDs_teamIndex[index16] = teamData.TeamIndex;
              break;
            }
          }
        }
      }
    }
  }

  public void CalculateLeaders_Kicking()
  {
    if (this.calculatedKicking)
      return;
    this.calculatedKicking = true;
    this.CreateArrayCategories_Kicking();
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null && (player.CurrentSeasonStats.FGAttempted > 0 || player.CurrentSeasonStats.Punts > 0 || player.CurrentSeasonStats.XPAttempted > 0))
        {
          int fgAttempted = player.CurrentSeasonStats.FGAttempted;
          for (int index2 = 0; index2 < 5; ++index2)
          {
            if (fgAttempted > this.fgAttempts_value[index2])
            {
              for (int index3 = 3; index3 >= index2; --index3)
              {
                this.fgAttempts_value[index3 + 1] = this.fgAttempts_value[index3];
                this.fgAttempts_playerIndex[index3 + 1] = this.fgAttempts_playerIndex[index3];
                this.fgAttempts_teamIndex[index3 + 1] = this.fgAttempts_teamIndex[index3];
              }
              this.fgAttempts_value[index2] = fgAttempted;
              this.fgAttempts_playerIndex[index2] = playerIndex;
              this.fgAttempts_teamIndex[index2] = teamData.TeamIndex;
              break;
            }
          }
          float num1 = player.CurrentSeasonStats.FGAttempted > 0 ? (float) player.CurrentSeasonStats.FGMade / (float) player.CurrentSeasonStats.FGAttempted : 0.0f;
          for (int index4 = 0; index4 < 5; ++index4)
          {
            if ((double) num1 > (double) this.fgPercentage_value[index4])
            {
              for (int index5 = 3; index5 >= index4; --index5)
              {
                this.fgPercentage_value[index5 + 1] = this.fgPercentage_value[index5];
                this.fgPercentage_playerIndex[index5 + 1] = this.fgPercentage_playerIndex[index5];
                this.fgPercentage_teamIndex[index5 + 1] = this.fgPercentage_teamIndex[index5];
              }
              this.fgPercentage_value[index4] = num1;
              this.fgPercentage_playerIndex[index4] = playerIndex;
              this.fgPercentage_teamIndex[index4] = teamData.TeamIndex;
              break;
            }
          }
          int xpAttempted = player.CurrentSeasonStats.XPAttempted;
          for (int index6 = 0; index6 < 5; ++index6)
          {
            if (xpAttempted > this.xpAttempts_value[index6])
            {
              for (int index7 = 3; index7 >= index6; --index7)
              {
                this.xpAttempts_value[index7 + 1] = this.xpAttempts_value[index7];
                this.xpAttempts_playerIndex[index7 + 1] = this.xpAttempts_playerIndex[index7];
                this.xpAttempts_teamIndex[index7 + 1] = this.xpAttempts_teamIndex[index7];
              }
              this.xpAttempts_value[index6] = xpAttempted;
              this.xpAttempts_playerIndex[index6] = playerIndex;
              this.xpAttempts_teamIndex[index6] = teamData.TeamIndex;
              break;
            }
          }
          float num2 = player.CurrentSeasonStats.XPAttempted > 0 ? (float) player.CurrentSeasonStats.XPMade / (float) player.CurrentSeasonStats.XPAttempted : 0.0f;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            if ((double) num2 > (double) this.xpPercentage_value[index8])
            {
              for (int index9 = 3; index9 >= index8; --index9)
              {
                this.xpPercentage_value[index9 + 1] = this.xpPercentage_value[index9];
                this.xpPercentage_playerIndex[index9 + 1] = this.xpPercentage_playerIndex[index9];
                this.xpPercentage_teamIndex[index9 + 1] = this.xpPercentage_teamIndex[index9];
              }
              this.xpPercentage_value[index8] = num2;
              this.xpPercentage_playerIndex[index8] = playerIndex;
              this.xpPercentage_teamIndex[index8] = teamData.TeamIndex;
              break;
            }
          }
          int punts = player.CurrentSeasonStats.Punts;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            if (punts > this.punts_value[index10])
            {
              for (int index11 = 3; index11 >= index10; --index11)
              {
                this.punts_value[index11 + 1] = this.punts_value[index11];
                this.punts_playerIndex[index11 + 1] = this.punts_playerIndex[index11];
                this.punts_teamIndex[index11 + 1] = this.punts_teamIndex[index11];
              }
              this.punts_value[index10] = punts;
              this.punts_playerIndex[index10] = playerIndex;
              this.punts_teamIndex[index10] = teamData.TeamIndex;
              break;
            }
          }
          float num3 = player.CurrentSeasonStats.Punts > 0 ? (float) player.CurrentSeasonStats.PuntYards / (float) player.CurrentSeasonStats.Punts : 0.0f;
          for (int index12 = 0; index12 < 5; ++index12)
          {
            if ((double) num3 > (double) this.puntAverage_value[index12])
            {
              for (int index13 = 3; index13 >= index12; --index13)
              {
                this.puntAverage_value[index13 + 1] = this.puntAverage_value[index13];
                this.puntAverage_playerIndex[index13 + 1] = this.puntAverage_playerIndex[index13];
                this.puntAverage_teamIndex[index13 + 1] = this.puntAverage_teamIndex[index13];
              }
              this.puntAverage_value[index12] = num3;
              this.puntAverage_playerIndex[index12] = playerIndex;
              this.puntAverage_teamIndex[index12] = teamData.TeamIndex;
              break;
            }
          }
          int puntsInside20 = player.CurrentSeasonStats.PuntsInside20;
          for (int index14 = 0; index14 < 5; ++index14)
          {
            if (puntsInside20 > this.puntsIn20_value[index14])
            {
              for (int index15 = 3; index15 >= index14; --index15)
              {
                this.puntsIn20_value[index15 + 1] = this.puntsIn20_value[index15];
                this.puntsIn20_playerIndex[index15 + 1] = this.puntsIn20_playerIndex[index15];
                this.puntsIn20_teamIndex[index15 + 1] = this.puntsIn20_teamIndex[index15];
              }
              this.puntsIn20_value[index14] = puntsInside20;
              this.puntsIn20_playerIndex[index14] = playerIndex;
              this.puntsIn20_teamIndex[index14] = teamData.TeamIndex;
              break;
            }
          }
        }
      }
    }
  }

  public void CalculateLeaders_Returns()
  {
    if (this.calculatedReturns)
      return;
    this.calculatedReturns = true;
    this.CreateArrayCategories_Returns();
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
      {
        PlayerData player = teamData.GetPlayer(playerIndex);
        if (player != null && player.CurrentSeasonStats != null && (player.CurrentSeasonStats.KickReturns > 0 || player.CurrentSeasonStats.PuntReturns > 0))
        {
          int kickReturns = player.CurrentSeasonStats.KickReturns;
          for (int index2 = 0; index2 < 5; ++index2)
          {
            if (kickReturns > this.kickReturns_value[index2])
            {
              for (int index3 = 3; index3 >= index2; --index3)
              {
                this.kickReturns_value[index3 + 1] = this.kickReturns_value[index3];
                this.kickReturns_playerIndex[index3 + 1] = this.kickReturns_playerIndex[index3];
                this.kickReturns_teamIndex[index3 + 1] = this.kickReturns_teamIndex[index3];
              }
              this.kickReturns_value[index2] = kickReturns;
              this.kickReturns_playerIndex[index2] = playerIndex;
              this.kickReturns_teamIndex[index2] = teamData.TeamIndex;
              break;
            }
          }
          float num1 = (float) player.CurrentSeasonStats.KickReturnYards / (float) player.CurrentSeasonStats.KickReturns;
          for (int index4 = 0; index4 < 5; ++index4)
          {
            if ((double) num1 > (double) this.kickReturnAverage_value[index4])
            {
              for (int index5 = 3; index5 >= index4; --index5)
              {
                this.kickReturnAverage_value[index5 + 1] = this.kickReturnAverage_value[index5];
                this.kickReturnAverage_playerIndex[index5 + 1] = this.kickReturnAverage_playerIndex[index5];
                this.kickReturnAverage_teamIndex[index5 + 1] = this.kickReturnAverage_teamIndex[index5];
              }
              this.kickReturnAverage_value[index4] = num1;
              this.kickReturnAverage_playerIndex[index4] = playerIndex;
              this.kickReturnAverage_teamIndex[index4] = teamData.TeamIndex;
              break;
            }
          }
          int kickReturnTds = player.CurrentSeasonStats.KickReturnTDs;
          for (int index6 = 0; index6 < 5; ++index6)
          {
            if (kickReturnTds > this.kickReturnTDs_value[index6])
            {
              for (int index7 = 3; index7 >= index6; --index7)
              {
                this.kickReturnTDs_value[index7 + 1] = this.kickReturnTDs_value[index7];
                this.kickReturnTDs_playerIndex[index7 + 1] = this.kickReturnTDs_playerIndex[index7];
                this.kickReturnTDs_teamIndex[index7 + 1] = this.kickReturnTDs_teamIndex[index7];
              }
              this.kickReturnTDs_value[index6] = kickReturnTds;
              this.kickReturnTDs_playerIndex[index6] = playerIndex;
              this.kickReturnTDs_teamIndex[index6] = teamData.TeamIndex;
              break;
            }
          }
          int puntReturns = player.CurrentSeasonStats.PuntReturns;
          for (int index8 = 0; index8 < 5; ++index8)
          {
            if (puntReturns > this.puntReturns_value[index8])
            {
              for (int index9 = 3; index9 >= index8; --index9)
              {
                this.puntReturns_value[index9 + 1] = this.puntReturns_value[index9];
                this.puntReturns_playerIndex[index9 + 1] = this.puntReturns_playerIndex[index9];
                this.puntReturns_teamIndex[index9 + 1] = this.puntReturns_teamIndex[index9];
              }
              this.puntReturns_value[index8] = puntReturns;
              this.puntReturns_playerIndex[index8] = playerIndex;
              this.puntReturns_teamIndex[index8] = teamData.TeamIndex;
              break;
            }
          }
          float num2 = (float) player.CurrentSeasonStats.PuntReturnYards / (float) player.CurrentSeasonStats.PuntReturns;
          for (int index10 = 0; index10 < 5; ++index10)
          {
            if ((double) num2 > (double) this.puntReturnAverage_value[index10])
            {
              for (int index11 = 3; index11 >= index10; --index11)
              {
                this.puntReturnAverage_value[index11 + 1] = this.puntReturnAverage_value[index11];
                this.puntReturnAverage_playerIndex[index11 + 1] = this.puntReturnAverage_playerIndex[index11];
                this.puntReturnAverage_teamIndex[index11 + 1] = this.puntReturnAverage_teamIndex[index11];
              }
              this.puntReturnAverage_value[index10] = num2;
              this.puntReturnAverage_playerIndex[index10] = playerIndex;
              this.puntReturnAverage_teamIndex[index10] = teamData.TeamIndex;
              break;
            }
          }
          int puntReturnTds = player.CurrentSeasonStats.PuntReturnTDs;
          for (int index12 = 0; index12 < 5; ++index12)
          {
            if (puntReturnTds > this.puntReturnTDs_value[index12])
            {
              for (int index13 = 3; index13 >= index12; --index13)
              {
                this.puntReturnTDs_value[index13 + 1] = this.puntReturnTDs_value[index13];
                this.puntReturnTDs_playerIndex[index13 + 1] = this.puntReturnTDs_playerIndex[index13];
                this.puntReturnTDs_teamIndex[index13 + 1] = this.puntReturnTDs_teamIndex[index13];
              }
              this.puntReturnTDs_value[index12] = puntReturnTds;
              this.puntReturnTDs_playerIndex[index12] = playerIndex;
              this.puntReturnTDs_teamIndex[index12] = teamData.TeamIndex;
              break;
            }
          }
        }
      }
    }
  }

  public void ResetCalculatedStatus()
  {
    this.calculatedPass = false;
    this.calculatedRush = false;
    this.calculatedRec = false;
    this.calculatedDef = false;
    this.calculatedKicking = false;
    this.calculatedReturns = false;
  }

  private void CreateArrayCategories_Pass()
  {
    this.calculatedPass = true;
    this.qbRating_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.qbRating_teamIndex = new int[5];
    this.qbRating_value = new int[5];
    this.passYards_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.passYards_teamIndex = new int[5];
    this.passYards_value = new int[5];
    this.passTDs_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.passTDs_teamIndex = new int[5];
    this.passTDs_value = new int[5];
    this.compPer_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.compPer_teamIndex = new int[5];
    this.compPer_value = new float[5];
    this.passYardsPerGame_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.passYardsPerGame_teamIndex = new int[5];
    this.passYardsPerGame_value = new float[5];
    this.passYardsPerPass_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.passYardsPerPass_teamIndex = new int[5];
    this.passYardsPerPass_value = new float[5];
    this.qbInts_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.qbInts_teamIndex = new int[5];
    this.qbInts_value = new int[5]{ 99, 99, 99, 99, 99 };
    this.passComp_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.passComp_teamIndex = new int[5];
    this.passComp_value = new int[5];
    this.passAtt_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.passAtt_teamIndex = new int[5];
    this.passAtt_value = new int[5];
  }

  private void CreateArrayCategories_Rush()
  {
    this.rushYards_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.rushYards_teamIndex = new int[5];
    this.rushYards_value = new int[5];
    this.rushTDs_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.rushTDs_teamIndex = new int[5];
    this.rushTDs_value = new int[5];
    this.yardsPerRush_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.yardsPerRush_teamIndex = new int[5];
    this.yardsPerRush_value = new float[5];
    this.yardsPerGame_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.yardsPerGame_teamIndex = new int[5];
    this.yardsPerGame_value = new float[5];
    this.rushAttempts_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.rushAttempts_teamIndex = new int[5];
    this.rushAttempts_value = new int[5];
    this.fumbles_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.fumbles_teamIndex = new int[5];
    this.fumbles_value = new int[5]{ 99, 99, 99, 99, 99 };
  }

  private void CreateArrayCategories_Receiving()
  {
    this.recYards_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.recYards_teamIndex = new int[5];
    this.recYards_value = new int[5];
    this.recTDs_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.recTDs_teamIndex = new int[5];
    this.recTDs_value = new int[5];
    this.yardsPerCatch_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.yardsPerCatch_teamIndex = new int[5];
    this.yardsPerCatch_value = new float[5];
    this.recYardsPerGame_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.recYardsPerGame_teamIndex = new int[5];
    this.recYardsPerGame_value = new float[5];
    this.catches_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.catches_teamIndex = new int[5];
    this.catches_value = new int[5];
    this.yac_playerIndex = new int[5]{ -1, -1, -1, -1, -1 };
    this.yac_teamIndex = new int[5];
    this.yac_value = new int[5];
    this.drops_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.drops_teamIndex = new int[5];
    this.drops_value = new int[5]{ 99, 99, 99, 99, 99 };
  }

  private void CreateArrayCategories_Defense()
  {
    this.tackles_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.tackles_teamIndex = new int[5];
    this.tackles_value = new int[5];
    this.sacks_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.sacks_teamIndex = new int[5];
    this.sacks_value = new int[5];
    this.ints_playerIndex = new int[5]{ -1, -1, -1, -1, -1 };
    this.ints_teamIndex = new int[5];
    this.ints_value = new int[5];
    this.deflectedPasses_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.deflectedPasses_teamIndex = new int[5];
    this.deflectedPasses_value = new int[5];
    this.forcedFumbles_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.forcedFumbles_teamIndex = new int[5];
    this.forcedFumbles_value = new int[5];
    this.fumRec_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.fumRec_teamIndex = new int[5];
    this.fumRec_value = new int[5];
    this.tacklesForLoss_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.tacklesForLoss_teamIndex = new int[5];
    this.tacklesForLoss_value = new int[5];
    this.defTDs_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.defTDs_teamIndex = new int[5];
    this.defTDs_value = new int[5];
  }

  private void CreateArrayCategories_Kicking()
  {
    this.fgAttempts_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.fgAttempts_teamIndex = new int[5];
    this.fgAttempts_value = new int[5];
    this.fgPercentage_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.fgPercentage_teamIndex = new int[5];
    this.fgPercentage_value = new float[5];
    this.xpAttempts_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.xpAttempts_teamIndex = new int[5];
    this.xpAttempts_value = new int[5];
    this.xpPercentage_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.xpPercentage_teamIndex = new int[5];
    this.xpPercentage_value = new float[5];
    this.punts_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.punts_teamIndex = new int[5];
    this.punts_value = new int[5];
    this.puntAverage_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.puntAverage_teamIndex = new int[5];
    this.puntAverage_value = new float[5];
    this.puntsIn20_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.puntsIn20_teamIndex = new int[5];
    this.puntsIn20_value = new int[5];
  }

  private void CreateArrayCategories_Returns()
  {
    this.kickReturns_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.kickReturns_teamIndex = new int[5];
    this.kickReturns_value = new int[5];
    this.kickReturnAverage_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.kickReturnAverage_teamIndex = new int[5];
    this.kickReturnAverage_value = new float[5];
    this.kickReturnTDs_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.kickReturnTDs_teamIndex = new int[5];
    this.kickReturnTDs_value = new int[5];
    this.puntReturns_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.puntReturns_teamIndex = new int[5];
    this.puntReturns_value = new int[5];
    this.puntReturnAverage_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.puntReturnAverage_teamIndex = new int[5];
    this.puntReturnAverage_value = new float[5];
    this.puntReturnTDs_playerIndex = new int[5]
    {
      -1,
      -1,
      -1,
      -1,
      -1
    };
    this.puntReturnTDs_teamIndex = new int[5];
    this.puntReturnTDs_value = new int[5];
  }

  public List<LeagueLeaders.LeagueLeaderItem> FilterPlayersByCategory(int statCategoryIndex)
  {
    List<LeagueLeaders.LeagueLeaderItem> leagueLeaderItemList = new List<LeagueLeaders.LeagueLeaderItem>();
    for (int index1 = 0; index1 < this.seasonModeData.NumberOfTeamsInLeague; ++index1)
    {
      TeamData teamData = this.seasonMode.GetTeamData(this.seasonModeData.TeamIndexMasterList[index1]);
      for (int index2 = 0; index2 < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++index2)
      {
        PlayerData player = teamData.GetPlayer(index2);
        if (player != null)
        {
          switch (statCategoryIndex)
          {
            case 0:
              if (player.CurrentSeasonStats.QBAttempts > 0)
              {
                leagueLeaderItemList.Add(new LeagueLeaders.LeagueLeaderItem(this.seasonModeData.TeamIndexMasterList[index1], index2));
                continue;
              }
              continue;
            case 1:
              if (player.CurrentSeasonStats.RushAttempts > 0)
              {
                leagueLeaderItemList.Add(new LeagueLeaders.LeagueLeaderItem(this.seasonModeData.TeamIndexMasterList[index1], index2));
                continue;
              }
              continue;
            case 2:
              if (player.CurrentSeasonStats.Receptions > 0)
              {
                leagueLeaderItemList.Add(new LeagueLeaders.LeagueLeaderItem(this.seasonModeData.TeamIndexMasterList[index1], index2));
                continue;
              }
              continue;
            case 3:
              if (player.CurrentSeasonStats.Tackles > 0 || player.CurrentSeasonStats.Interceptions > 0 || player.CurrentSeasonStats.KnockDowns > 0)
              {
                leagueLeaderItemList.Add(new LeagueLeaders.LeagueLeaderItem(this.seasonModeData.TeamIndexMasterList[index1], index2));
                continue;
              }
              continue;
            case 4:
              if (player.CurrentSeasonStats.FGAttempted > 0 || player.CurrentSeasonStats.XPAttempted > 0 || player.CurrentSeasonStats.Punts > 0)
              {
                leagueLeaderItemList.Add(new LeagueLeaders.LeagueLeaderItem(this.seasonModeData.TeamIndexMasterList[index1], index2));
                continue;
              }
              continue;
            default:
              if (player.CurrentSeasonStats.KickReturns > 0 || player.CurrentSeasonStats.PuntReturns > 0)
              {
                leagueLeaderItemList.Add(new LeagueLeaders.LeagueLeaderItem(this.seasonModeData.TeamIndexMasterList[index1], index2));
                continue;
              }
              continue;
          }
        }
      }
    }
    return leagueLeaderItemList;
  }

  private void SortByQBRating(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.GetQBRating();
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  public void SortByQBTotalPassYards(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.QBPassYards;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByQBPassTDs(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.QBPassTDs;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByQBCompletionPerc(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.QBAttempts > 0)
      {
        list[index].value = (float) ((double) currentSeasonStats.QBCompletions / (double) currentSeasonStats.QBAttempts * 100.0);
        list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value) + "%";
      }
      else
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByQBPassYardsPerGame(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    int num = Mathf.Max(1, this.seasonModeData.currentWeek - 1);
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.QBPassYards / (float) num;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByQBYardsPerCompletion(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.QBAttempts > 0)
      {
        list[index].value = (float) currentSeasonStats.QBPassYards / (float) currentSeasonStats.QBCompletions;
        list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      }
      else
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByQBInterceptions(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.QBAttempts < 30)
      {
        list.RemoveAt(index);
      }
      else
      {
        list[index].value = (float) currentSeasonStats.QBInts;
        list[index].displayValue = list[index].value.ToString();
      }
    }
    this.SortASC(ref list, numberOfPlayers);
  }

  private void SortByQBCompletions(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.QBCompletions;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByQBAttempts(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.QBAttempts;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  public void SortByRushYards(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.RushYards;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByRushTDs(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.RushTDs;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByYardsPerCarry(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.RushAttempts > 0)
      {
        list[index].value = (float) currentSeasonStats.RushYards / (float) currentSeasonStats.RushAttempts;
        list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      }
      else
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByRushYardsPerGame(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    int num = Mathf.Max(1, this.seasonModeData.currentWeek - 1);
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.RushYards / (float) num;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByCarries(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.RushAttempts;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByFumbles(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.RushAttempts < 30)
      {
        list.RemoveAt(index);
      }
      else
      {
        list[index].value = (float) currentSeasonStats.Fumbles;
        list[index].displayValue = list[index].value.ToString();
      }
    }
    this.SortASC(ref list, numberOfPlayers);
  }

  public void SortByRecYards(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.ReceivingYards;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByRecTDs(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.ReceivingTDs;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByYardsPerCatch(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.Receptions > 0)
      {
        list[index].value = (float) currentSeasonStats.ReceivingYards / (float) currentSeasonStats.Receptions;
        list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      }
      else
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByRecYardsPerGame(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    int num = Mathf.Max(1, this.seasonModeData.currentWeek - 1);
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.ReceivingYards / (float) num;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByReceptions(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.Receptions;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByYardsAfterCatch(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.YardsAfterCatch;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByDrops(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      if (currentSeasonStats.Receptions < 20)
      {
        list.RemoveAt(index);
      }
      else
      {
        list[index].value = (float) currentSeasonStats.Drops;
        list[index].displayValue = list[index].value.ToString();
      }
    }
    this.SortASC(ref list, numberOfPlayers);
  }

  public void SortByTackles(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.Tackles;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  public void SortBySacks(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.Sacks;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  public void SortByInts(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.Interceptions;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByDeflectedPasses(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.KnockDowns;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByForcedFumbles(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.ForcedFumbles;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByFumbleRec(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.FumbleRecoveries;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByTacklesForLoss(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.TacklesForLoss;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByDefensiveTDs(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.DefensiveTDs;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByFGAttempts(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.FGAttempted;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  public void SortByFGPercentage(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = currentSeasonStats.FGAttempted > 0 ? (float) ((double) currentSeasonStats.FGMade / (double) currentSeasonStats.FGAttempted * 100.0) : 0.0f;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value) + "%";
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByXPAttempts(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.XPAttempted;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByXPPercentage(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = currentSeasonStats.XPAttempted > 0 ? (float) ((double) currentSeasonStats.XPMade / (double) currentSeasonStats.XPAttempted * 100.0) : 0.0f;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value) + "%";
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByPunts(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.Punts;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  public void SortByPuntAverage(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = currentSeasonStats.Punts > 0 ? (float) currentSeasonStats.PuntYards / (float) currentSeasonStats.Punts : 0.0f;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByPuntsIn20(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.PuntsInside20;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByKickReturns(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.KickReturns;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByKickReturnAverage(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = currentSeasonStats.KickReturns > 0 ? (float) currentSeasonStats.KickReturnYards / (float) currentSeasonStats.KickReturns : 0.0f;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByKickReturnTDs(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.KickReturnTDs;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByPuntReturns(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.PuntReturns;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByPuntReturnAverage(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = currentSeasonStats.PuntReturns > 0 ? (float) currentSeasonStats.PuntReturnYards / (float) currentSeasonStats.PuntReturns : 0.0f;
      list[index].displayValue = string.Format("{0:0.#}", (object) list[index].value);
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortByPuntReturnTDs(
    ref List<LeagueLeaders.LeagueLeaderItem> list,
    int numberOfPlayers)
  {
    for (int index = list.Count - 1; index >= 0; --index)
    {
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index];
      PlayerStats currentSeasonStats = this.seasonMode.GetTeamData(leagueLeaderItem.teamIndex).GetPlayer(leagueLeaderItem.playerIndex).CurrentSeasonStats;
      list[index].value = (float) currentSeasonStats.PuntReturnTDs;
      list[index].displayValue = list[index].value.ToString();
      if ((double) list[index].value == 0.0)
        list.RemoveAt(index);
    }
    this.SortDESC(ref list, numberOfPlayers);
  }

  private void SortDESC(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    if (list.Count < numberOfPlayers)
      numberOfPlayers = list.Count;
    for (int index1 = 0; index1 < numberOfPlayers; ++index1)
    {
      float num = 0.0f;
      int index2 = index1;
      for (int index3 = index1; index3 < list.Count; ++index3)
      {
        if ((double) list[index3].value > (double) num)
        {
          num = list[index3].value;
          index2 = index3;
        }
      }
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index1];
      list[index1] = list[index2];
      list[index2] = leagueLeaderItem;
    }
  }

  private void SortASC(ref List<LeagueLeaders.LeagueLeaderItem> list, int numberOfPlayers = 2147483647)
  {
    if (list.Count < numberOfPlayers)
      numberOfPlayers = list.Count;
    for (int index1 = 0; index1 < numberOfPlayers; ++index1)
    {
      float num = 10000f;
      int index2 = index1;
      for (int index3 = index1; index3 < list.Count; ++index3)
      {
        if ((double) list[index3].value < (double) num)
        {
          num = list[index3].value;
          index2 = index3;
        }
      }
      LeagueLeaders.LeagueLeaderItem leagueLeaderItem = list[index1];
      list[index1] = list[index2];
      list[index2] = leagueLeaderItem;
    }
  }

  public struct Leaders
  {
    public int[] playerIndex;
    public int[] teamIndex;
    public string[] value;

    public Leaders(int[] pIndex, int[] tIndex, string[] v)
    {
      this.playerIndex = pIndex;
      this.teamIndex = tIndex;
      this.value = v;
    }
  }

  public class LeagueLeaderItem
  {
    public int teamIndex;
    public int playerIndex;
    public float value;
    public string displayValue;

    public LeagueLeaderItem(int t, int p)
    {
      this.teamIndex = t;
      this.playerIndex = p;
    }
  }
}
