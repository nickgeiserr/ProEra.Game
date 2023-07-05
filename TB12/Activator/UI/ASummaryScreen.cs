// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.ASummaryScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.Activator.Data;
using TB12.Solo.Data;
using TMPro;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class ASummaryScreen : UIView
  {
    [SerializeField]
    private TouchButton _declineButton;
    [SerializeField]
    private TouchButton _postButton;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _playerName;
    [SerializeField]
    private ALeaderboardData _leaderboardData;
    [SerializeField]
    private SoloLeaderboardData _newLeaderboardData;
    private bool _postResult;

    public override Enum ViewId { get; } = (Enum) EScreens.kActivatorSummary;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._declineButton, (Action) (() => this.HandleOption(false))),
      (EventHandle) UIHandle.Link((IButton) this._postButton, (Action) (() => this.HandleOption(true)))
    });

    private void HandleOption(bool postResult)
    {
      this._postResult = postResult;
      this.Hide();
    }

    protected override void WillAppear()
    {
      this._timeText.text = this._leaderboardData.Date;
      this._scoreText.text = ActivationState.Score.ToString();
      this._playerName.text = ActivationState.PlayerName;
      this._postResult = false;
    }

    protected override void DidDisappear()
    {
      if (!this._postResult)
        return;
      this._leaderboardData.PushHighscore();
      this._newLeaderboardData.SubmitScore(ASummaryScreen.GetLeaderURLByGameType(AppState.GameMode), ActivationState.PlayerName, ActivationState.Score);
    }

    public static string GetLeaderURLByGameType(EGameMode gameMode)
    {
      switch (gameMode)
      {
        case EGameMode.kThrow:
          return "passing";
        case EGameMode.kCatch:
          return "catching";
        case EGameMode.kAgility:
          return "agility";
        case EGameMode.kPass:
          return "passing";
        default:
          return string.Empty;
      }
    }
  }
}
