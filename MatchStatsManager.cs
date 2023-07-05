// Decompiled with JetBrains decompiler
// Type: MatchStatsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using ProEra;
using ProEra.Game;
using System;
using TB12;
using TB12.UI;
using UDB;
using UnityEngine;

public class MatchStatsManager : SingletonBehaviour<MatchStatsManager, MonoBehaviour>
{
  public GameSummary userGameSummary;
  public GameSummary compGameSummary;
  public TeamGameStats userTeamStats;
  public TeamGameStats compTeamStats;

  public void CallStart()
  {
    this.userGameSummary = new GameSummary();
    this.compGameSummary = new GameSummary();
    this.userGameSummary.TeamIndex = PersistentData.GetUserTeamIndex();
    this.compGameSummary.TeamIndex = PersistentData.GetCompTeamIndex();
    PersistentData.GetUserTeam().CreateNewTeamGameStats();
    PersistentData.GetCompTeam().CreateNewTeamGameStats();
    this.userTeamStats = PersistentData.GetUserTeam().CurrentGameStats;
    this.compTeamStats = PersistentData.GetCompTeam().CurrentGameStats;
    ProEra.Game.MatchState.Stats.User = this.userGameSummary.TeamGameStats = this.userTeamStats;
    ProEra.Game.MatchState.Stats.Comp = this.compGameSummary.TeamGameStats = this.compTeamStats;
    for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
    {
      PersistentData.GetUserTeam().GetPlayer(playerIndex).CreateNewPlayerStatsForGame();
      PersistentData.GetCompTeam().GetPlayer(playerIndex).CreateNewPlayerStatsForGame();
    }
    this.userGameSummary.RecordPlayerStats(PersistentData.GetUserTeam());
    this.compGameSummary.RecordPlayerStats(PersistentData.GetCompTeam());
    GameScoreboardUI[] scoreboards = ScoreboardAnimations.GetScoreboards();
    if (scoreboards != null)
    {
      foreach (GameScoreboardUI gameScoreboardUi in scoreboards)
        gameScoreboardUi.Reinitialize();
    }
    MainGameTablet instance = MainGameTablet.Instance;
    if (!(bool) (UnityEngine.Object) instance)
      return;
    instance.ReinitializeMatchStats();
  }

  private void OnDestroy()
  {
    Debug.Log((object) "MatchStatsManager -> OnDestroy");
    SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance = (MatchStatsManager) null;
  }

  public void ResetDriveStats()
  {
    ProEra.Game.MatchState.Stats.CurrentDrivePlays = 0;
    ProEra.Game.MatchState.Stats.DriveRunPlays = 0;
    ProEra.Game.MatchState.Stats.DrivePassPlays = 0;
    ProEra.Game.MatchState.Stats.DriveTotalYards = 0;
    ProEra.Game.MatchState.Stats.DriveRunYards = 0;
    ProEra.Game.MatchState.Stats.DrivePassYards = 0;
    ProEra.Game.MatchState.Stats.DriveFirstDowns = 0;
    ProEra.Game.MatchState.Stats.DriveTimeInSeconds = 0;
  }

  public void UpdateStats(bool userOnOffense, float savedLineOfScrim)
  {
    float num1 = MatchManager.ballOn;
    if (global::Game.PET_IsTouchdown)
      num1 = Field.OFFENSIVE_GOAL_LINE;
    if (global::Game.IsNotTurnover && global::Game.FumbleDidNotOccured && global::Game.IsRunOrPass)
    {
      int num2 = Field.ConvertDistanceToYards(num1 - savedLineOfScrim) * global::Game.OffensiveFieldDirection;
      if (global::Game.IsRun)
      {
        ProEra.Game.MatchState.Stats.DriveTotalYards += num2;
        ProEra.Game.MatchState.Stats.DriveRunYards += num2;
      }
      else if (global::Game.IsPass && global::Game.BallHolderIsNotNull)
      {
        if (global::Game.BallIsNotThrownOrKicked && num2 > 0)
          ProEra.Game.MatchState.Stats.DriveRunYards += num2;
        else
          ProEra.Game.MatchState.Stats.DrivePassYards += num2;
        ProEra.Game.MatchState.Stats.DriveTotalYards += num2;
      }
      bool flag = global::Game.IsPass && global::Game.BallHolderIsNotNull && global::Game.BallIsNotThrownOrKicked && num2 > 0;
      if (((!global::Game.IsRun ? 0 : (global::Game.BallHolderIsNotNull ? 1 : 0)) | (flag ? 1 : 0)) != 0)
      {
        MatchManager.instance.playManager.AddToCurrentPlayLog(MatchManager.instance.playersManager.ballHolderScript.firstName + " " + MatchManager.instance.playersManager.ballHolderScript.lastName + " gained " + num2.ToString() + " rushing yards on the play.");
        PlayerStats player;
        if (userOnOffense)
        {
          player = MatchManager.instance.playManager.userTeamCurrentPlayStats.players[MatchManager.instance.playersManager.ballHolderScript.indexOnTeam];
          ProEra.Game.MatchState.Stats.User.RushYards += num2;
          if (num2 >= 5)
            ++ProEra.Game.MatchState.Stats.User.RushFiveYards;
          if (num2 >= 50)
          {
            int num3 = global::Game.UserControlsPlayers ? 1 : 0;
          }
          if (ProEra.Game.MatchState.Stats.User.RushYards > 200)
          {
            int num4 = global::Game.UserControlsPlayers ? 1 : 0;
          }
          if (ProEra.Game.MatchState.Stats.User.TotalYards() <= 500)
            ;
        }
        else
        {
          player = MatchManager.instance.playManager.compTeamCurrentPlayStats.players[MatchManager.instance.playersManager.ballHolderScript.indexOnTeam];
          ProEra.Game.MatchState.Stats.Comp.RushYards += num2;
          if (num2 >= 5)
            ++ProEra.Game.MatchState.Stats.Comp.RushFiveYards;
        }
        player.RushYards += num2;
        if (player.LongestRush >= num2)
          return;
        player.LongestRush = num2;
      }
      else
      {
        if (!global::Game.IsPass || !global::Game.BallHolderIsNotNull)
          return;
        PlayerStats player1;
        PlayerStats player2;
        if (userOnOffense)
        {
          ProEra.Game.MatchState.Stats.User.PassYards += num2;
          player1 = MatchManager.instance.playManager.userTeamCurrentPlayStats.players[MatchManager.instance.playersManager.ballHolderScript.indexOnTeam];
          player2 = MatchManager.instance.playManager.userTeamCurrentPlayStats.players[MatchManager.instance.playersManager.curUserScriptRef[5].indexOnTeam];
          if (global::Game.BallIsThrownOrKicked)
          {
            if (num2 >= 75)
            {
              int num5 = global::Game.UserControlsPlayers ? 1 : 0;
            }
            if (ProEra.Game.MatchState.Stats.User.PassYards > 400)
            {
              int num6 = global::Game.UserControlsPlayers ? 1 : 0;
            }
          }
          if (ProEra.Game.MatchState.Stats.User.RushYards + ProEra.Game.MatchState.Stats.User.PassYards <= 500)
            ;
        }
        else
        {
          player1 = MatchManager.instance.playManager.compTeamCurrentPlayStats.players[MatchManager.instance.playersManager.ballHolderScript.indexOnTeam];
          player2 = MatchManager.instance.playManager.compTeamCurrentPlayStats.players[MatchManager.instance.playersManager.curCompScriptRef[5].indexOnTeam];
          ProEra.Game.MatchState.Stats.Comp.PassYards += num2;
        }
        if (!global::Game.BallIsThrownOrKicked)
          return;
        int num7 = Mathf.RoundToInt((float) (((double) num1 - (double) MatchManager.instance.caughtPassOrKickAtPos) / 24.0 * 50.0)) * global::Game.OffensiveFieldDirection;
        MatchManager.instance.playManager.AddToCurrentPlayLog(MatchManager.instance.playersManager.ballHolderScript.firstName + " " + MatchManager.instance.playersManager.ballHolderScript.lastName + " gained " + num2.ToString() + " receiving yards on the play.");
        player1.ReceivingYards += num2;
        player1.YardsAfterCatch += num7;
        player2.QBPassYards += num2;
        if (num2 >= 50)
          ++player2.QBFiftyYardCompletions;
        else if (num2 >= 30)
          ++player2.QBThirtyYardCompletions;
        else if (num2 >= 20)
          ++player2.QBTwentyYardCompletions;
        else if (num2 >= 10)
          ++player2.QBTenYardCompletions;
        if (player2.QBLongestPass < num2)
          player2.QBLongestPass = num2;
        if (player1.LongestReception >= num2)
          return;
        player1.LongestReception = num2;
      }
    }
    else
    {
      if (!global::Game.BallHolderIsNotNull)
        return;
      PlayerStats playerStats = !userOnOffense ? MatchManager.instance.playManager.userTeamCurrentPlayStats.players[MatchManager.instance.playersManager.ballHolderScript.indexOnTeam] : MatchManager.instance.playManager.compTeamCurrentPlayStats.players[MatchManager.instance.playersManager.ballHolderScript.indexOnTeam];
      int num8 = Mathf.RoundToInt((float) (((double) MatchManager.instance.caughtPassOrKickAtPos - (double) num1) / 24.0 * 50.0)) * global::Game.OffensiveFieldDirection;
      if (global::Game.IsPunt)
      {
        ++playerStats.PuntReturns;
        playerStats.PuntReturnYards += num8;
      }
      else
      {
        if (!global::Game.IsKickoff)
          return;
        ++playerStats.KickReturns;
        playerStats.KickReturnYards += num8;
      }
    }
  }

  public void PrintTeamStats()
  {
    Debug.Log((object) "********User Team Stats:************");
    this.userTeamStats.Print();
    Debug.Log((object) "*************************************");
    Debug.Log((object) "********Comp Team Stats:************");
    this.compTeamStats.Print();
    Debug.Log((object) "*************************************");
  }

  public void WriteTeamStatsToCsv()
  {
    string str1 = DateTime.Now.ToString("yyyyMMddHHmmss");
    string str2 = PersistentData.GameMode.ToString();
    string str3 = PersistentData.gameType.ToString();
    string folderUnderMods = "Logs";
    if (AppState.GameMode == EGameMode.kAISimGameMode)
      folderUnderMods = SingletonBehaviour<AISimGameTestManager, MonoBehaviour>.instance.SaveFilePath;
    CsvExport csvExport1 = new CsvExport();
    csvExport1.AddRow(this.userTeamStats.GetCsvRow());
    csvExport1.WriteToFile(folderUnderMods, PersistentData.GetUserTeam().GetAbbreviation() + "_" + str3 + "_" + str2 + "_" + str1 + "_TeamStats.csv");
    CsvExport csvExport2 = new CsvExport();
    csvExport2.AddRow(this.compTeamStats.GetCsvRow());
    csvExport2.WriteToFile(folderUnderMods, PersistentData.GetCompTeam().GetAbbreviation() + "_" + str3 + "_" + str2 + "_" + str1 + "_TeamStats.csv");
  }
}
