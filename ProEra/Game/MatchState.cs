// Decompiled with JetBrains decompiler
// Type: ProEra.Game.MatchState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

namespace ProEra.Game
{
  public static class MatchState
  {
    public static readonly VariableInt Difficulty = new VariableInt(0);
    public static readonly VariableInt UserDifficulty = new VariableInt(0);
    public static readonly VariableEnum<EMatchState> CurrentMatchState = new VariableEnum<EMatchState>(EMatchState.Beginning);
    public static readonly VariableFloat BallOn = new VariableFloat(0.0f);
    public static readonly VariableFloat FirstDown = new VariableFloat(0.0f);
    public static readonly VariableInt HalftimeMargin = new VariableInt(0);
    public static readonly VariableInt Down = new VariableInt(0);
    public static readonly VariableInt GameLength = new VariableInt(0);
    public static readonly VariableBool RunningPat = new VariableBool();
    public static readonly VariableBool Turnover = new VariableBool();
    public static readonly VariableBool IsKickoff = new VariableBool();
    public static readonly VariableBool IsSafetyKickoff = new VariableBool();
    public static readonly VariableBool IsPlayInitiated = new VariableBool();

    public static bool IsPlayerOneOnOffense => (EMatchState) (Variable<EMatchState>) MatchState.CurrentMatchState == EMatchState.UserOnOffense;

    public static bool IsPlayerOneOnDefense => (EMatchState) (Variable<EMatchState>) MatchState.CurrentMatchState != EMatchState.UserOnOffense;

    public static bool IsPlayerTwoOnOffense => (EMatchState) (Variable<EMatchState>) MatchState.CurrentMatchState != EMatchState.UserOnOffense;

    public static bool IsPlayerTwoOnDefense => (EMatchState) (Variable<EMatchState>) MatchState.CurrentMatchState == EMatchState.UserOnOffense;

    public static bool IsHomeTeamOnOffense => (bool) Globals.UserIsHome == MatchState.IsPlayerOneOnOffense;

    public static bool IsAwayTeamOnOffense => !MatchState.IsHomeTeamOnOffense;

    public static bool IsDefenseWinning => MatchState.IsOffenseLosing;

    public static bool IsDefenseLosing => MatchState.IsOffenseWinning;

    public static bool IsOffenseWinning => (MatchState.IsPlayerOneOnOffense ? MatchState.Stats.User.Score : MatchState.Stats.Comp.Score) > (MatchState.IsPlayerOneOnOffense ? MatchState.Stats.Comp.Score : MatchState.Stats.User.Score);

    public static bool IsOffenseLosing => (MatchState.IsPlayerOneOnOffense ? MatchState.Stats.User.Score : MatchState.Stats.Comp.Score) < (MatchState.IsPlayerOneOnOffense ? MatchState.Stats.Comp.Score : MatchState.Stats.User.Score);

    public static void AddPossessionTime(float time)
    {
      if ((EMatchState) (Variable<EMatchState>) MatchState.CurrentMatchState == EMatchState.Beginning)
        return;
      if (MatchState.IsPlayerOneOnOffense)
        MatchState.Stats.User.AddPossessionTime(time);
      else
        MatchState.Stats.Comp.AddPossessionTime(time);
    }

    public static void Reset()
    {
      MatchState.Stats.CurrentDrivePlays = 0;
      MatchState.Stats.DriveRunPlays = 0;
      MatchState.Stats.DrivePassPlays = 0;
      MatchState.Stats.DriveTotalYards = 0;
      MatchState.Stats.DriveTimeInSeconds = 0;
      MatchState.Stats.DriveRunYards = 0;
      MatchState.Stats.DrivePassYards = 0;
      MatchState.Stats.DriveFirstDowns = 0;
      MatchState.CurrentMatchState.SetValue(EMatchState.Beginning);
      MatchState.Difficulty.SetValue(0);
      MatchState.UserDifficulty.SetValue(0);
      MatchState.BallOn.SetValue(0.0f);
      MatchState.FirstDown.SetValue(0.0f);
      MatchState.HalftimeMargin.SetValue(0);
      MatchState.Down.SetValue(0);
      MatchState.GameLength.SetValue(0);
      MatchState.RunningPat.SetValue(false);
      MatchState.Turnover.SetValue(false);
      MatchState.IsKickoff.SetValue(false);
      MatchState.IsSafetyKickoff.SetValue(false);
      MatchState.IsPlayInitiated.SetValue(false);
      PlayState.PlayType.SetValue((PlayType) 0);
    }

    public static class Stats
    {
      public static TeamGameStats User;
      public static TeamGameStats Comp;
      public static int CurrentDrivePlays;
      public static int DriveRunPlays;
      public static int DrivePassPlays;
      public static int DriveTotalYards;
      public static int DriveTimeInSeconds;
      public static int DriveRunYards;
      public static int DrivePassYards;
      public static int DriveFirstDowns;

      public static bool IsUserLoosing() => MatchState.Stats.User.Score < MatchState.Stats.Comp.Score;

      public static bool IsCompLoosing() => MatchState.Stats.Comp.Score < MatchState.Stats.User.Score;

      public static int ScoreDifference() => Mathf.Abs(MatchState.Stats.User.Score - MatchState.Stats.Comp.Score);

      public static int GetOffenseScoreDifference() => !MatchState.IsPlayerOneOnOffense ? MatchState.Stats.Comp.Score - MatchState.Stats.User.Score : MatchState.Stats.User.Score - MatchState.Stats.Comp.Score;

      public static bool EqualScore() => MatchState.Stats.User.Score == MatchState.Stats.Comp.Score;

      public static int GetHomeScore() => !(bool) Globals.UserIsHome ? MatchState.Stats.Comp.Score : MatchState.Stats.User.Score;

      public static int GetAwayScore() => !(bool) Globals.UserIsHome ? MatchState.Stats.User.Score : MatchState.Stats.Comp.Score;

      public static bool IsKickingTeamLoosing() => !MatchState.IsPlayerOneOnOffense ? MatchState.Stats.IsCompLoosing() : MatchState.Stats.IsUserLoosing();

      public static bool IsRivalMatch => TeamDataCache.IsRival(PersistentData.GetUserTeamIndex(), PersistentData.GetCompTeamIndex());
    }
  }
}
