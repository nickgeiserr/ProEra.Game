// Decompiled with JetBrains decompiler
// Type: TB12.UI.IntroScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.GameplayData;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class IntroScreen : UIView
  {
    [SerializeField]
    private TouchButton _closeButton;
    [SerializeField]
    private TextMeshProUGUI _headerText;
    [SerializeField]
    private TextMeshProUGUI _detailsText;
    [SerializeField]
    private GameObject _passImageGo;
    [SerializeField]
    private GameObject _catchImageGo;
    [SerializeField]
    private GameplayDataStore _gameplayData;
    [SerializeField]
    private GameLevelsStore _levelsStore;

    public override Enum ViewId { get; } = (Enum) EScreens.kIntroduction;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._closeButton, AppEvents.LoadMainMenu)
    });

    protected override void WillAppear()
    {
      switch (AppState.GameMode)
      {
        case EGameMode.kCatch:
          this._headerText.text = "CATCH CHALLENGE";
          this._detailsText.text = "SQUEEZE\nIndex Triggers to Start!";
          break;
        case EGameMode.kPass:
          this._headerText.text = "PASS TRAINING";
          this._detailsText.text = "Throw five pass. Complete 1 to go to the next level";
          break;
        default:
          GameLevel gameLevel = this._levelsStore.CurrentLevel.Value;
          ThrowLevel throwChallenge = this._gameplayData.GetThrowChallenge(AppState.LevelId);
          if (throwChallenge != null && gameLevel != null)
          {
            this._headerText.text = gameLevel.description;
            this._detailsText.text = string.Format("Grab a ball from the bucket to your right to start!\r\n\r\nLand at least {0} out of {1} throws to win!", (object) throwChallenge.throwsToWin, (object) throwChallenge.totalThrows);
            break;
          }
          this._headerText.text = "PASS TRAINING";
          this._detailsText.text = "Pass Training - try hitting as many\ntargets as you can";
          break;
      }
      bool flag = AppState.GameMode != EGameMode.kCatch;
      this._passImageGo.SetActive(flag);
      this._catchImageGo.SetActive(!flag);
    }
  }
}
