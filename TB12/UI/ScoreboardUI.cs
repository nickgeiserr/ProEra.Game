// Decompiled with JetBrains decompiler
// Type: TB12.UI.ScoreboardUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.GameplayData;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class ScoreboardUI : UIPanel
  {
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private TextMeshProUGUI _userName;
    [SerializeField]
    private TextMeshProUGUI _userScore;
    [SerializeField]
    private TextMeshProUGUI _userBalls;
    [SerializeField]
    private TextMeshProUGUI _qbName;
    [SerializeField]
    private TextMeshProUGUI _qbScore;
    [SerializeField]
    private TextMeshProUGUI _qbBalls;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      this._gameplayStore.AttemptsRemaining.Link<int>(new Action<int>(this.HandleAttemptsRemaining)),
      this._gameplayStore.BallsThrown.Link<int>(new Action<int>(this.HandleBallsThrown)),
      this._gameplayStore.OpponentData.BallsHit.Link<int>(new Action<int>(this.HandleQbHit)),
      this._gameplayStore.OpponentData.BallsThrown.Link<int>(new Action<int>(this.HandleQbBallsThrown)),
      this._gameplayStore.OpponentData.TotalAttempts.Link<int>(new Action<int>(this.HandleTotalAttemptsChanged))
    });

    private void HandleTotalAttemptsChanged(int totalAttempts) => this._qbBalls.text = (totalAttempts - (int) this._gameplayStore.OpponentData.BallsThrown).ToString();

    protected override void WillAppear()
    {
      this._userName.text = this._playerProfile.PlayerLastName;
      Variable<GameLevel> currentLevel = this._levelsStore.CurrentLevel;
      if (currentLevel?.Value == null)
        Debug.LogError((object) "Current level null");
      else
        this._qbName.text = currentLevel.Value.QbName;
    }

    private void HandleAttemptsRemaining(int remainingAttempts) => this._userBalls.text = remainingAttempts.ToString();

    private void HandleBallsThrown(int ballsThrown) => this._userScore.text = string.Format("{0} out of {1}", (object) this._gameplayStore.BallsHitTarget, (object) ballsThrown);

    private void HandleQbHit(int ballsHit)
    {
      OpponentData opponentData = this._gameplayStore.OpponentData;
      this._qbScore.text = string.Format("{0} out of {1}", (object) ballsHit, (object) opponentData.BallsThrown);
    }

    private void HandleQbBallsThrown(int ballsThrown)
    {
      OpponentData opponentData = this._gameplayStore.OpponentData;
      this._qbBalls.text = ((int) this._gameplayStore.OpponentData.TotalAttempts - ballsThrown).ToString();
      this._qbScore.text = string.Format("{0} out of {1}", (object) opponentData.BallsHit, (object) opponentData.BallsThrown);
    }
  }
}
