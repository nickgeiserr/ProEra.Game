// Decompiled with JetBrains decompiler
// Type: TB12.GameplayStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/GameplayStore", fileName = "GameplayStore")]
  [AppStore]
  public class GameplayStore : ScriptableObject
  {
    public VariableInt BallsThrown = new VariableInt(0);
    public VariableInt BallsHitTarget = new VariableInt(0);
    public List<OpponentData> OpponentDatas = new List<OpponentData>();
    public OpponentData OpponentData = new OpponentData();
    public VariableInt TimeBonusScore = new VariableInt(0);
    public VariableInt LevelBonusScore = new VariableInt(0);

    public VariableInt Score { get; } = new VariableInt(0);

    public VariableInt AttemptsRemaining { get; set; } = new VariableInt(0);

    public int ComboModifier { get; set; }

    public bool Locked { get; set; }

    public int BallsCaught { get; set; }

    public int BallsEmitted { get; set; }

    public int PassAttempts { get; set; }

    public int PassSuccesses { get; set; }

    public float BestDistance { get; private set; }

    public float BestSpeed { get; private set; }

    protected virtual void OnEnable() => this.ResetStore();

    public virtual void ResetStore()
    {
      this.Locked = false;
      this.Score.Value = 0;
      this.ComboModifier = 0;
      this.AttemptsRemaining.Value = 0;
      this.BallsCaught = 0;
      this.BallsEmitted = 0;
      this.PassAttempts = 0;
      this.PassSuccesses = 0;
      this.BallsHitTarget.SetValue(0);
      this.BallsThrown.SetValue(0);
      this.BestDistance = 0.0f;
      this.BestSpeed = 0.0f;
      this.TimeBonusScore.SetValue(0);
      this.LevelBonusScore.SetValue(0);
      this.OpponentData.ResetData();
      foreach (OpponentData opponentData in this.OpponentDatas)
        opponentData.ResetData();
    }

    public void AccumulateScore(int value)
    {
      if (this.Locked)
        return;
      this.Score.Value += value;
      ++this.BallsHitTarget;
      Debug.Log((object) string.Format("Score is now {0}", (object) this.Score.Value));
    }

    public void DoThrow(float speed, bool countAsAttempt = true)
    {
      if (countAsAttempt)
        --this.AttemptsRemaining;
      this.BestSpeed = Mathf.Max(this.BestSpeed, speed);
    }

    public void UpdateDistance(float value) => this.BestDistance = Mathf.Max(this.BestDistance, value);

    public virtual void HandleOpponentHit() => ++this.OpponentData.BallsHit;

    public void AddBonusScores(int timeScore, int levelScore)
    {
      this.TimeBonusScore.Value = timeScore;
      this.LevelBonusScore.Value = levelScore;
    }
  }
}
