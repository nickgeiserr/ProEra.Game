// Decompiled with JetBrains decompiler
// Type: Analytics.SeasonGameCompletedArgs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Analytics
{
  public class SeasonGameCompletedArgs : AnalyticEventArgs
  {
    public SeasonGameCompletedArgs(
      int seasonGamesPlayed,
      bool playerWonSeasonGame,
      int seasonWins,
      int seasonLosses,
      float timeSpentInScene,
      float fistLocomotionTime,
      float thumbLocomotionTime,
      int playerScore,
      int cpuScore,
      int quarterLength,
      int difficulty,
      bool passAssistEnabled)
    {
      this.SeasonGamesPlayed = seasonGamesPlayed;
      this.PlayerWonSeasonGame = playerWonSeasonGame;
      this.SeasonWins = seasonWins;
      this.SeasonLosses = seasonLosses;
      this.TimeSpentInScene = timeSpentInScene;
      this.FistLocomotionTime = fistLocomotionTime;
      this.ThumbLocomotionTime = thumbLocomotionTime;
      this.PlayerScore = playerScore;
      this.CpuScore = cpuScore;
      this.QuarterLength = quarterLength;
      this.Difficulty = difficulty;
      this.PassAssistEnabled = passAssistEnabled;
    }

    public override string EventName => AnalyticEventName.SeasonGameCompleted.ServiceName();

    public int SeasonGamesPlayed { get; set; }

    public bool PlayerWonSeasonGame { get; set; }

    public int SeasonWins { get; set; }

    public int SeasonLosses { get; set; }

    public float TimeSpentInScene { get; set; }

    public float FistLocomotionTime { get; set; }

    public float ThumbLocomotionTime { get; set; }

    public int PlayerScore { get; set; }

    public int CpuScore { get; set; }

    public int QuarterLength { get; set; }

    public int Difficulty { get; set; }

    public bool PassAssistEnabled { get; set; }

    public override Dictionary<string, object> Parameters => new Dictionary<string, object>()
    {
      {
        "SeasonGamesPlayed",
        (object) this.SeasonGamesPlayed
      },
      {
        "PlayerWonSeasonGame",
        (object) this.PlayerWonSeasonGame
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
        "TimeSpentInScene",
        (object) this.TimeSpentInScene
      },
      {
        "FistLocomotionTime",
        (object) this.FistLocomotionTime
      },
      {
        "ThumbLocomotionTime",
        (object) this.ThumbLocomotionTime
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
      }
    };
  }
}
