// Decompiled with JetBrains decompiler
// Type: ProEra.Game.MiniGameScoreState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using Vars;

namespace ProEra.Game
{
  public static class MiniGameScoreState
  {
    public static VariableInt Score { get; } = new VariableInt(0);

    public static VariableInt AttemptsRemaining { get; set; } = new VariableInt(0);

    public static VariableFloat TimeRemaining { get; set; } = new VariableFloat(0.0f);

    public static VariableInt StarsEarned { get; set; } = new VariableInt(0);

    public static VariableFloat StarsProgress { get; set; } = new VariableFloat(0.0f);

    public static Variable<MiniGameScoreState.MiniCampPassResult> passResult { get; set; } = new Variable<MiniGameScoreState.MiniCampPassResult>();

    public static int ComboModifier { get; set; }

    public static bool Locked { get; set; }

    public static float BestSpeed { get; private set; }

    public static bool TargetWasHit { get; set; }

    public static void ResetData()
    {
      Console.WriteLine("Reset Mini Game ComboModifier to: 1");
      MiniGameScoreState.Score.Value = 0;
      MiniGameScoreState.ComboModifier = 1;
      MiniGameScoreState.AttemptsRemaining.SetValue(0);
      MiniGameScoreState.TimeRemaining.SetValue(0.0f);
      MiniGameScoreState.Locked = false;
      MiniGameScoreState.BestSpeed = 0.0f;
      MiniGameScoreState.StarsEarned.Value = 0;
      MiniGameScoreState.StarsProgress.Value = 0.0f;
      MiniGameScoreState.passResult.Value = new MiniGameScoreState.MiniCampPassResult(0, MiniGameScoreState.EMiniCampPassResult.ResetGamePassThrough);
    }

    public static void AccumulateScore(int value)
    {
      if (MiniGameScoreState.Locked)
        return;
      MiniGameScoreState.Score.Value += value;
      Debug.Log((object) string.Format("Score is now {0}", (object) MiniGameScoreState.Score.Value));
    }

    public static void DoThrow(float speed, bool countAsAttempt = true)
    {
      if (countAsAttempt)
        --MiniGameScoreState.AttemptsRemaining;
      MiniGameScoreState.BestSpeed = Mathf.Max(MiniGameScoreState.BestSpeed, speed);
    }

    public static void MarkPlayResult(
      MiniGameScoreState.MiniCampPassResult miniCampPlayResult)
    {
      MiniGameScoreState.passResult.Value = miniCampPlayResult;
    }

    public enum EMiniCampPassResult
    {
      Empty,
      Green,
      Yellow,
      Bronze,
      Silver,
      Gold,
      Miss,
      ResetGamePassThrough,
    }

    public class MiniCampPassResult
    {
      public int passResultIndex;
      public MiniGameScoreState.EMiniCampPassResult miniCampPassResult;

      public MiniCampPassResult(int index, MiniGameScoreState.EMiniCampPassResult passResult)
      {
        this.passResultIndex = index;
        this.miniCampPassResult = passResult;
      }

      public MiniCampPassResult(int index, PrecisionPassingGameFlow.ThrowAward throwAward)
      {
        this.passResultIndex = index;
        switch (throwAward)
        {
          case PrecisionPassingGameFlow.ThrowAward.Miss:
            this.miniCampPassResult = MiniGameScoreState.EMiniCampPassResult.Miss;
            break;
          case PrecisionPassingGameFlow.ThrowAward.Gold:
            this.miniCampPassResult = MiniGameScoreState.EMiniCampPassResult.Gold;
            break;
          case PrecisionPassingGameFlow.ThrowAward.Silver:
            this.miniCampPassResult = MiniGameScoreState.EMiniCampPassResult.Silver;
            break;
          case PrecisionPassingGameFlow.ThrowAward.Bronze:
            this.miniCampPassResult = MiniGameScoreState.EMiniCampPassResult.Bronze;
            break;
        }
      }
    }
  }
}
