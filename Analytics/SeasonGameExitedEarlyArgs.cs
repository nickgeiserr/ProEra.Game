// Decompiled with JetBrains decompiler
// Type: Analytics.SeasonGameExitedEarlyArgs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Analytics
{
  public class SeasonGameExitedEarlyArgs : AnalyticEventArgs
  {
    public SeasonGameExitedEarlyArgs(
      int seasonGamesPlayed,
      int seasonWins,
      int seasonLosses,
      int playerScore,
      int cpuScore,
      int quarterLength,
      int difficulty,
      bool passAssistEnabled,
      float percentageOfGameCompleted)
    {
      this.SeasonGamesPlayed = seasonGamesPlayed;
      this.SeasonWins = seasonWins;
      this.SeasonLosses = seasonLosses;
      this.PlayerScore = playerScore;
      this.CpuScore = cpuScore;
      this.QuarterLength = quarterLength;
      this.Difficulty = difficulty;
      this.PassAssistEnabled = passAssistEnabled;
      this.PercentageOfGameCompleted = percentageOfGameCompleted;
    }

    public override string EventName => AnalyticEventName.SeasonGameExitedEarly.ServiceName();

    public int SeasonGamesPlayed { get; set; }

    public bool PlayerWinning => this.PlayerScore > this.CpuScore;

    public int SeasonWins { get; set; }

    public int SeasonLosses { get; set; }

    public int PlayerScore { get; set; }

    public int CpuScore { get; set; }

    public int QuarterLength { get; set; }

    public int Difficulty { get; set; }

    public bool PassAssistEnabled { get; set; }

    public float PercentageOfGameCompleted { get; set; }

    public override Dictionary<string, object> Parameters => new Dictionary<string, object>()
    {
      {
        "SeasonGamesPlayed",
        (object) this.SeasonGamesPlayed
      },
      {
        "PlayerWinning",
        (object) this.PlayerWinning
      },
      {
        "SeasonWins",
        (object) this.SeasonWins
      },
      {
        "SeasonLosses",
        (object) this.SeasonLosses
      },
      {
        "PlayerScore",
        (object) this.PlayerScore
      },
      {
        "CpuScore",
        (object) this.CpuScore
      },
      {
        "QuarterLength",
        (object) this.QuarterLength
      },
      {
        "Difficulty",
        (object) this.Difficulty
      },
      {
        "PassAssistEnabled",
        (object) this.PassAssistEnabled
      },
      {
        "PercentageOfGameCompleted",
        (object) this.PercentageOfGameCompleted
      }
    };
  }
}
