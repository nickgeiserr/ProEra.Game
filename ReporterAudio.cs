// Decompiled with JetBrains decompiler
// Type: ReporterAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

public class ReporterAudio : MonoBehaviour
{
  private string rootFolder = "reporteraudio/";
  private CommentaryManager commentary;
  public bool playedHalftimeReport;
  public bool playedWeatherReport;
  public bool isLoaded;
  private AudioPath gaveUpSacks;
  private AudioPath goodPassDefense;
  private AudioPath goodPassing;
  private AudioPath goodRunDefense;
  private AudioPath goodRunning;
  private AudioPath goodSacks;
  private AudioPath goodThirdDowns;
  private AudioPath goodTurnovers;
  private AudioPath intro;
  private AudioPath losingOnePossession;
  private AudioPath losingTwoPossessions;
  private AudioPath outro;
  private AudioPath poorPassDefense;
  private AudioPath poorPassing;
  private AudioPath poorRunDefense;
  private AudioPath poorRunning;
  private AudioPath poorThirdDowns;
  private AudioPath poorTurnovers;
  private AudioPath rainReport;
  private AudioPath snowReport;
  private AudioPath halftimeIntro_homeTeamCity;
  private AudioPath halftimeIntro_awayTeamCity;
  private AudioPath halftimeIntro_homeTeamName;
  private AudioPath halftimeIntro_awayTeamName;
  private AudioPath windReport;
  private AudioPath winningOnePossession;
  private AudioPath winningTwoPossessions;

  public void LoadAudioPaths()
  {
    if (this.isLoaded)
      return;
    this.isLoaded = true;
    this.commentary = AudioManager.self.commentary;
    this.gaveUpSacks = new AudioPath(this.rootFolder + "GAVE_UP_SACKS", 5, false);
    this.goodPassDefense = new AudioPath(this.rootFolder + "GOOD_PASS_DEFENSE", 5, false);
    this.goodPassing = new AudioPath(this.rootFolder + "GOOD_PASSING", 7, false);
    this.goodRunDefense = new AudioPath(this.rootFolder + "GOOD_RUN_DEFENSE", 5, false);
    this.goodRunning = new AudioPath(this.rootFolder + "GOOD_RUNNING", 5, false);
    this.goodSacks = new AudioPath(this.rootFolder + "GOOD_SACKS", 5, false);
    this.goodThirdDowns = new AudioPath(this.rootFolder + "GOOD_THIRD_DOWNS", 5, false);
    this.goodTurnovers = new AudioPath(this.rootFolder + "GOOD_TURNOVERS", 5, false);
    this.intro = new AudioPath(this.rootFolder + "INTRO", 4, false);
    this.losingOnePossession = new AudioPath(this.rootFolder + "LOSING_ONE_POSSESSION", 5, false);
    this.losingTwoPossessions = new AudioPath(this.rootFolder + "LOSING_TWO_POSSESSIONS", 4, false);
    this.outro = new AudioPath(this.rootFolder + "OUTRO", 4, false);
    this.poorPassDefense = new AudioPath(this.rootFolder + "POOR_PASS_DEFENSE", 5, false);
    this.poorPassing = new AudioPath(this.rootFolder + "POOR_PASSING", 5, false);
    this.poorRunDefense = new AudioPath(this.rootFolder + "POOR_RUN_DEFENSE", 5, false);
    this.poorRunning = new AudioPath(this.rootFolder + "POOR_RUNNING", 5, false);
    this.poorThirdDowns = new AudioPath(this.rootFolder + "POOR_THIRD_DOWN", 5, false);
    this.poorTurnovers = new AudioPath(this.rootFolder + "POOR_TURNOVERS", 5, false);
    this.rainReport = new AudioPath(this.rootFolder + "RAIN", 5, false);
    this.snowReport = new AudioPath(this.rootFolder + "SNOW", 5, false);
    this.windReport = new AudioPath(this.rootFolder + "WIND", 5, false);
    this.winningOnePossession = new AudioPath(this.rootFolder + "WINNING_ONE_POSSESSION", 5, false);
    this.winningTwoPossessions = new AudioPath(this.rootFolder + "WINNING_TWO_POSSESSIONS", 5, false);
  }

  public void LoadTeamAudio()
  {
    bool flag1 = PersistentData.GetHomeTeamIndex() < TeamAssetManager.NUMBER_OF_BASE_TEAMS && PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets;
    if (flag1 && (PersistentData.GetHomeTeamIndex() == 19 || PersistentData.GetHomeTeamIndex() == 22 || PersistentData.GetHomeTeamIndex() == 33 || PersistentData.GetHomeTeamIndex() == 35))
      flag1 = false;
    int num;
    if (flag1)
    {
      num = PersistentData.GetHomeTeamIndex();
      string str = "team_" + num.ToString();
      this.halftimeIntro_homeTeamCity = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_CITIES/" + str);
      this.halftimeIntro_homeTeamName = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_NAMES/" + str);
      if (PersistentData.GetHomeTeamIndex() == 2 || PersistentData.GetHomeTeamIndex() == 4 || PersistentData.GetHomeTeamIndex() == 5 || PersistentData.GetHomeTeamIndex() == 9)
        this.halftimeIntro_homeTeamName = this.halftimeIntro_homeTeamCity;
    }
    if (!flag1 || this.halftimeIntro_homeTeamCity.count == 0)
    {
      this.halftimeIntro_homeTeamCity = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_CITIES/HOME");
      this.halftimeIntro_homeTeamName = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_CITIES/HOME");
    }
    bool flag2 = PersistentData.GetAwayTeamIndex() < TeamAssetManager.NUMBER_OF_BASE_TEAMS && PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets;
    if (flag2 && (PersistentData.GetAwayTeamIndex() == 19 || PersistentData.GetAwayTeamIndex() == 22 || PersistentData.GetAwayTeamIndex() == 33 || PersistentData.GetAwayTeamIndex() == 35))
      flag2 = false;
    if (flag2)
    {
      num = PersistentData.GetAwayTeamIndex();
      string str = "team_" + num.ToString();
      this.halftimeIntro_awayTeamCity = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_CITIES/" + str);
      this.halftimeIntro_awayTeamName = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_NAMES/" + str);
      if (PersistentData.GetAwayTeamIndex() == 2 || PersistentData.GetAwayTeamIndex() == 4 || PersistentData.GetAwayTeamIndex() == 5 || PersistentData.GetAwayTeamIndex() == 9)
        this.halftimeIntro_awayTeamName = this.halftimeIntro_awayTeamCity;
    }
    if (!flag2 || this.halftimeIntro_awayTeamCity.count == 0)
    {
      this.halftimeIntro_awayTeamCity = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_CITIES/AWAY");
      this.halftimeIntro_awayTeamName = new AudioPath(this.rootFolder + "HALFTIME_INTRO_TEAM_CITIES/AWAY");
    }
    this.playedHalftimeReport = false;
    this.playedWeatherReport = false;
  }

  private void PlayRandomClip(AudioPath audioPath) => this.PlayRandomClip(audioPath, 0.0f);

  private void PlayRandomClip(AudioPath audioPath, float pauseBefore)
  {
    int index = Random.Range(0, audioPath.count) + 1;
    if ((double) pauseBefore > 0.0)
      this.commentary.AddPause(pauseBefore);
    this.commentary.AddSegmentToQueue(new AudioSegment(AudioManager.self.GetAudioClip(audioPath, index)));
  }

  private void PlayIntro() => this.PlayRandomClip(this.intro);

  private void PlayOutro() => this.PlayRandomClip(this.outro);

  private void PlayHalftimeIntro_UserTeam()
  {
    if (PersistentData.userIsHome)
      this.PlayHalftimeIntro_HomeTeam();
    else
      this.PlayHalftimeIntro_AwayTeam();
  }

  private void PlayHalftimeIntro_CompTeam()
  {
    if (!PersistentData.userIsHome)
      this.PlayHalftimeIntro_HomeTeam();
    else
      this.PlayHalftimeIntro_AwayTeam();
  }

  private void PlayHalftimeIntro_HomeTeam()
  {
    if (Random.Range(0, 100) < 50)
      this.PlayHalftimeIntro_HomeTeamName();
    else
      this.PlayHalftimeIntro_HomeTeamCity();
  }

  private void PlayHalftimeIntro_AwayTeam()
  {
    if (Random.Range(0, 100) < 50)
      this.PlayHalftimeIntro_AwayTeamName();
    else
      this.PlayHalftimeIntro_AwayTeamCity();
  }

  private void PlayHalftimeIntro_HomeTeamName() => this.PlayRandomClip(this.halftimeIntro_homeTeamName);

  private void PlayHalftimeIntro_HomeTeamCity() => this.PlayRandomClip(this.halftimeIntro_homeTeamCity);

  private void PlayHalftimeIntro_AwayTeamName() => this.PlayRandomClip(this.halftimeIntro_homeTeamName);

  private void PlayHalftimeIntro_AwayTeamCity() => this.PlayRandomClip(this.halftimeIntro_homeTeamCity);

  public void PlayWeatherReport()
  {
    this.playedWeatherReport = true;
    if (PersistentData.weather == 1)
      this.PlayWeatherReport_Rain();
    else if (PersistentData.weather == 2)
    {
      this.PlayWeatherReport_Snow();
    }
    else
    {
      if (PersistentData.windType != 2)
        return;
      this.PlayWeatherReport_Wind();
    }
  }

  public void PlayHalftimeReport()
  {
    this.playedHalftimeReport = true;
    bool reportingForUserTeam = Random.Range(0, 100) < 50;
    this.PlayIntro();
    this.commentary.AddPause();
    if (reportingForUserTeam)
      this.PlayHalftimeIntro_UserTeam();
    else
      this.PlayHalftimeIntro_CompTeam();
    this.commentary.AddPause();
    this.PlayReportOnStat(reportingForUserTeam);
    this.commentary.AddPause();
    this.PlayOutro();
  }

  private void PlayReportOnStat(bool reportingForUserTeam)
  {
    int num1 = ProEra.Game.MatchState.Stats.User.Turnovers - ProEra.Game.MatchState.Stats.Comp.Turnovers;
    int num2 = ProEra.Game.MatchState.Stats.Comp.Turnovers - ProEra.Game.MatchState.Stats.User.Turnovers;
    int num3 = ProEra.Game.MatchState.Stats.User.Score - ProEra.Game.MatchState.Stats.Comp.Score;
    int num4 = ProEra.Game.MatchState.Stats.Comp.Score - ProEra.Game.MatchState.Stats.User.Score;
    float num5 = 0.0f;
    float num6 = 0.0f;
    if (ProEra.Game.MatchState.Stats.User.ThirdDownAtt > 0)
      num5 = (float) ProEra.Game.MatchState.Stats.User.ThirdDownSuc / (float) ProEra.Game.MatchState.Stats.User.ThirdDownAtt;
    if (ProEra.Game.MatchState.Stats.Comp.ThirdDownAtt > 0)
      num6 = (float) ProEra.Game.MatchState.Stats.Comp.ThirdDownSuc / (float) ProEra.Game.MatchState.Stats.Comp.ThirdDownAtt;
    int num7 = Random.Range(0, 100);
    if (num7 < 25)
    {
      if (ProEra.Game.MatchState.Stats.User.Sacks > 4)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodSacks();
        else
          this.PlayReport_GaveUpSacks();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.Sacks > 4)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodSacks();
        else
          this.PlayReport_GaveUpSacks();
      }
      else if (ProEra.Game.MatchState.Stats.User.PassYards > 250)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodPassing();
        else
          this.PlayReport_PoorPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.PassYards > 250)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodPassing();
        else
          this.PlayReport_PoorPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.PassYards < 100)
      {
        if (reportingForUserTeam)
          this.PlayReport_PoorPassing();
        else
          this.PlayReport_GoodPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.PassYards < 100)
      {
        if (!reportingForUserTeam)
          this.PlayReport_PoorPassing();
        else
          this.PlayReport_GoodPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.RushYards > 100)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 100)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.RushYards < 40)
      {
        if (reportingForUserTeam)
          this.PlayReport_PoorRunning();
        else
          this.PlayReport_GoodRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 40)
      {
        if (!reportingForUserTeam)
          this.PlayReport_PoorRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if ((double) num5 > 0.75 & reportingForUserTeam)
        this.PlayReport_GoodThirdDownConversion();
      else if ((double) num6 > 0.75 && !reportingForUserTeam)
        this.PlayReport_GoodThirdDownConversion();
      else if (num1 > 0)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodTurnoverMargin();
        else
          this.PlayReport_PoorTurnoverMargin();
      }
      else if (num2 > 0)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodTurnoverMargin();
        else
          this.PlayReport_PoorTurnoverMargin();
      }
      else if (num3 > 0 && num3 <= 8)
      {
        if (reportingForUserTeam)
          this.PlayReport_WinningByOnePossession();
        else
          this.PlayReport_LosingByOnePossession();
      }
      else if (num4 > 0 && num4 <= 8)
      {
        if (!reportingForUserTeam)
          this.PlayReport_WinningByOnePossession();
        else
          this.PlayReport_LosingByOnePossession();
      }
      else if (num3 > 8)
      {
        if (reportingForUserTeam)
          this.PlayReport_WinningByTwoPossessions();
        else
          this.PlayReport_LosingByTwoPossessions();
      }
      else if (num4 > 8)
      {
        if (!reportingForUserTeam)
          this.PlayReport_WinningByTwoPossessions();
        else
          this.PlayReport_LosingByTwoPossessions();
      }
      else
        this.PlayReport_GoodThirdDownConversion();
    }
    else if (num7 < 50)
    {
      if (num3 > 0 && num3 <= 8)
      {
        if (reportingForUserTeam)
          this.PlayReport_WinningByOnePossession();
        else
          this.PlayReport_LosingByOnePossession();
      }
      else if (num4 > 0 && num4 <= 8)
      {
        if (!reportingForUserTeam)
          this.PlayReport_WinningByOnePossession();
        else
          this.PlayReport_LosingByOnePossession();
      }
      else if (num3 > 8)
      {
        if (reportingForUserTeam)
          this.PlayReport_WinningByTwoPossessions();
        else
          this.PlayReport_LosingByTwoPossessions();
      }
      else if (num4 > 8)
      {
        if (!reportingForUserTeam)
          this.PlayReport_WinningByTwoPossessions();
        else
          this.PlayReport_LosingByTwoPossessions();
      }
      else if (num1 > 0)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodTurnoverMargin();
        else
          this.PlayReport_PoorTurnoverMargin();
      }
      else if (num2 > 0)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodTurnoverMargin();
        else
          this.PlayReport_PoorTurnoverMargin();
      }
      else if ((double) num5 > 0.75 & reportingForUserTeam)
        this.PlayReport_GoodThirdDownConversion();
      else if ((double) num6 > 0.75 && !reportingForUserTeam)
        this.PlayReport_GoodThirdDownConversion();
      else if (ProEra.Game.MatchState.Stats.User.RushYards < 40)
      {
        if (reportingForUserTeam)
          this.PlayReport_PoorRunning();
        else
          this.PlayReport_GoodRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 40)
      {
        if (!reportingForUserTeam)
          this.PlayReport_PoorRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.RushYards > 100)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 100)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.PassYards < 100)
      {
        if (reportingForUserTeam)
          this.PlayReport_PoorPassing();
        else
          this.PlayReport_GoodPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.PassYards < 100)
      {
        if (!reportingForUserTeam)
          this.PlayReport_PoorPassing();
        else
          this.PlayReport_GoodPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.PassYards > 250)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodPassing();
        else
          this.PlayReport_PoorPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.PassYards > 250)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodPassing();
        else
          this.PlayReport_PoorPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.Sacks > 4)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodSacks();
        else
          this.PlayReport_GaveUpSacks();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.Sacks > 4)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodSacks();
        else
          this.PlayReport_GaveUpSacks();
      }
      else
        this.PlayReport_GoodThirdDownConversion();
    }
    else if (num7 < 75)
    {
      if (ProEra.Game.MatchState.Stats.Comp.RushYards > 40)
      {
        if (!reportingForUserTeam)
          this.PlayReport_PoorRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.RushYards < 40)
      {
        if (reportingForUserTeam)
          this.PlayReport_PoorRunning();
        else
          this.PlayReport_GoodRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 100)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.RushYards > 100)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodRunning();
        else
          this.PlayReport_PoorRunDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.PassYards < 100)
      {
        if (!reportingForUserTeam)
          this.PlayReport_PoorPassing();
        else
          this.PlayReport_GoodPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.PassYards < 100)
      {
        if (reportingForUserTeam)
          this.PlayReport_PoorPassing();
        else
          this.PlayReport_GoodPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.PassYards > 250)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodPassing();
        else
          this.PlayReport_PoorPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.User.PassYards > 250)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodPassing();
        else
          this.PlayReport_PoorPassDefense();
      }
      else if (ProEra.Game.MatchState.Stats.Comp.Sacks > 4)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodSacks();
        else
          this.PlayReport_GaveUpSacks();
      }
      else if (ProEra.Game.MatchState.Stats.User.Sacks > 4)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodSacks();
        else
          this.PlayReport_GaveUpSacks();
      }
      else if ((double) num6 > 0.75 && !reportingForUserTeam)
        this.PlayReport_GoodThirdDownConversion();
      else if ((double) num5 > 0.75 & reportingForUserTeam)
        this.PlayReport_GoodThirdDownConversion();
      else if (num2 > 0)
      {
        if (!reportingForUserTeam)
          this.PlayReport_GoodTurnoverMargin();
        else
          this.PlayReport_PoorTurnoverMargin();
      }
      else if (num1 > 0)
      {
        if (reportingForUserTeam)
          this.PlayReport_GoodTurnoverMargin();
        else
          this.PlayReport_PoorTurnoverMargin();
      }
      else if (num4 > 0 && num4 <= 8)
      {
        if (!reportingForUserTeam)
          this.PlayReport_WinningByOnePossession();
        else
          this.PlayReport_LosingByOnePossession();
      }
      else if (num3 > 0 && num3 <= 8)
      {
        if (reportingForUserTeam)
          this.PlayReport_WinningByOnePossession();
        else
          this.PlayReport_LosingByOnePossession();
      }
      else if (num4 > 8)
      {
        if (!reportingForUserTeam)
          this.PlayReport_WinningByTwoPossessions();
        else
          this.PlayReport_LosingByTwoPossessions();
      }
      else if (num3 > 8)
      {
        if (reportingForUserTeam)
          this.PlayReport_WinningByTwoPossessions();
        else
          this.PlayReport_LosingByTwoPossessions();
      }
      else
        this.PlayReport_GoodThirdDownConversion();
    }
    else if (num4 > 0 && num4 <= 8)
    {
      if (!reportingForUserTeam)
        this.PlayReport_WinningByOnePossession();
      else
        this.PlayReport_LosingByOnePossession();
    }
    else if (num3 > 0 && num3 <= 8)
    {
      if (reportingForUserTeam)
        this.PlayReport_WinningByOnePossession();
      else
        this.PlayReport_LosingByOnePossession();
    }
    else if (num4 > 8)
    {
      if (!reportingForUserTeam)
        this.PlayReport_WinningByTwoPossessions();
      else
        this.PlayReport_LosingByTwoPossessions();
    }
    else if (num3 > 8)
    {
      if (reportingForUserTeam)
        this.PlayReport_WinningByTwoPossessions();
      else
        this.PlayReport_LosingByTwoPossessions();
    }
    else if (num2 > 0)
    {
      if (!reportingForUserTeam)
        this.PlayReport_GoodTurnoverMargin();
      else
        this.PlayReport_PoorTurnoverMargin();
    }
    else if (num1 > 0)
    {
      if (reportingForUserTeam)
        this.PlayReport_GoodTurnoverMargin();
      else
        this.PlayReport_PoorTurnoverMargin();
    }
    else if ((double) num6 > 0.75 && !reportingForUserTeam)
      this.PlayReport_GoodThirdDownConversion();
    else if ((double) num5 > 0.75 & reportingForUserTeam)
      this.PlayReport_GoodThirdDownConversion();
    else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 40)
    {
      if (!reportingForUserTeam)
        this.PlayReport_PoorRunning();
      else
        this.PlayReport_PoorRunDefense();
    }
    else if (ProEra.Game.MatchState.Stats.User.RushYards < 40)
    {
      if (reportingForUserTeam)
        this.PlayReport_PoorRunning();
      else
        this.PlayReport_GoodRunDefense();
    }
    else if (ProEra.Game.MatchState.Stats.Comp.RushYards > 100)
    {
      if (!reportingForUserTeam)
        this.PlayReport_GoodRunning();
      else
        this.PlayReport_PoorRunDefense();
    }
    else if (ProEra.Game.MatchState.Stats.User.RushYards > 100)
    {
      if (reportingForUserTeam)
        this.PlayReport_GoodRunning();
      else
        this.PlayReport_PoorRunDefense();
    }
    else if (ProEra.Game.MatchState.Stats.Comp.PassYards < 100)
    {
      if (!reportingForUserTeam)
        this.PlayReport_PoorPassing();
      else
        this.PlayReport_GoodPassDefense();
    }
    else if (ProEra.Game.MatchState.Stats.User.PassYards < 100)
    {
      if (reportingForUserTeam)
        this.PlayReport_PoorPassing();
      else
        this.PlayReport_GoodPassDefense();
    }
    else if (ProEra.Game.MatchState.Stats.Comp.PassYards > 250)
    {
      if (!reportingForUserTeam)
        this.PlayReport_GoodPassing();
      else
        this.PlayReport_PoorPassDefense();
    }
    else if (ProEra.Game.MatchState.Stats.User.PassYards > 250)
    {
      if (reportingForUserTeam)
        this.PlayReport_GoodPassing();
      else
        this.PlayReport_PoorPassDefense();
    }
    else if (ProEra.Game.MatchState.Stats.Comp.Sacks > 4)
    {
      if (!reportingForUserTeam)
        this.PlayReport_GoodSacks();
      else
        this.PlayReport_GaveUpSacks();
    }
    else if (ProEra.Game.MatchState.Stats.User.Sacks > 4)
    {
      if (reportingForUserTeam)
        this.PlayReport_GoodSacks();
      else
        this.PlayReport_GaveUpSacks();
    }
    else
      this.PlayReport_GoodThirdDownConversion();
  }

  private void PlayReport_GaveUpSacks() => this.PlayRandomClip(this.gaveUpSacks);

  private void PlayReport_GoodSacks() => this.PlayRandomClip(this.goodSacks);

  private void PlayReport_GoodPassing() => this.PlayRandomClip(this.goodPassing);

  private void PlayReport_PoorPassDefense() => this.PlayRandomClip(this.poorPassDefense);

  private void PlayReport_PoorPassing() => this.PlayRandomClip(this.poorPassing);

  private void PlayReport_GoodPassDefense() => this.PlayRandomClip(this.goodPassDefense);

  private void PlayReport_GoodRunning() => this.PlayRandomClip(this.goodRunning);

  private void PlayReport_PoorRunDefense() => this.PlayRandomClip(this.poorRunDefense);

  private void PlayReport_PoorRunning() => this.PlayRandomClip(this.poorRunning);

  private void PlayReport_GoodRunDefense() => this.PlayRandomClip(this.goodRunDefense);

  private void PlayReport_GoodThirdDownConversion() => this.PlayRandomClip(this.goodThirdDowns);

  private void PlayReport_PoorThirdDownConversion() => this.PlayRandomClip(this.poorThirdDowns);

  private void PlayReport_GoodTurnoverMargin() => this.PlayRandomClip(this.goodTurnovers);

  private void PlayReport_PoorTurnoverMargin() => this.PlayRandomClip(this.poorTurnovers);

  private void PlayReport_WinningByOnePossession() => this.PlayRandomClip(this.winningOnePossession);

  private void PlayReport_LosingByOnePossession() => this.PlayRandomClip(this.losingOnePossession);

  private void PlayReport_LosingByTwoPossessions() => this.PlayRandomClip(this.losingTwoPossessions);

  private void PlayReport_WinningByTwoPossessions() => this.PlayRandomClip(this.winningTwoPossessions);

  public void PlayWeatherReport_Rain() => this.PlayRandomClip(this.rainReport);

  public void PlayWeatherReport_Snow() => this.PlayRandomClip(this.snowReport);

  public void PlayWeatherReport_Wind() => this.PlayRandomClip(this.windReport);
}
