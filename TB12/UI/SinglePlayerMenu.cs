// Decompiled with JetBrains decompiler
// Type: TB12.UI.SinglePlayerMenu
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12.GameplayData;
using UnityEngine;

namespace TB12.UI
{
  public class SinglePlayerMenu : UIView
  {
    [SerializeField]
    private SinglePlayerFrontPanel _frontPanel;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private PlayerProgressStore _playerProgress;
    [SerializeField]
    private TouchButton _nextButton;
    [SerializeField]
    private TouchButton _prevButton;
    [SerializeField]
    private TouchButton _playButton;
    [SerializeField]
    private TouchButton _backButton;

    public override Enum ViewId { get; } = (Enum) EScreens.kSinglePlayer;

    protected override void OnInitialize()
    {
      this._levelsStore.Initialize(true);
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) UIHandle.Link((IButton) this._nextButton, new System.Action(this._levelsStore.NextLevel)),
        (EventHandle) UIHandle.Link((IButton) this._prevButton, new System.Action(this._levelsStore.PreviousLevel)),
        (EventHandle) UIHandle.Link((IButton) this._playButton, new System.Action(this.HandlePlay)),
        (EventHandle) UIHandle.Link((IButton) this._backButton, (System.Action) (() => UIDispatch.FrontScreen.DisplayView(EScreens.kWelcome))),
        EventHandle.Link<GameLevel>(this._levelsStore.CurrentLevel, new Action<GameLevel>(this.HandleLevelChanged))
      });
      this._frontPanel.Initialize();
      this._playerProgress.Initialize();
    }

    private void HandleLevelChanged(GameLevel level)
    {
      if (level == null)
        Debug.LogError((object) "Trying to load null level.");
      else if (this._playerProgress.Progress == null)
      {
        Debug.LogError((object) "Progress data missing");
      }
      else
      {
        this._prevButton.gameObject.SetActive(level.id > 1);
        this._nextButton.gameObject.SetActive(level.id < this._levelsStore.GameLevels.Length);
        this._playButton.Interactable.SetValue(this._playerProgress.IsLevelAvailable(level.id) || (bool) DeveloperMode.Activated);
        ProfileProgress.Entry data = this._playerProgress.Progress.GetData(level.id);
        this._frontPanel.ShowChallenge(level, data);
      }
    }

    private void HandlePlay() => GameplayManager.LoadLevel(this._levelsStore.CurrentLevel.Value);

    protected override void WillAppear()
    {
      this.HandleLevelChanged((GameLevel) this._levelsStore.CurrentLevel);
      this._frontPanel.Show();
      UIDispatch.LeftScreen.DisplayView(EScreens.kLeaderboards);
      UIDispatch.RightScreen.DisplayView(EScreens.kGameLevels);
    }

    protected override void WillDisappear()
    {
      UIDispatch.LeftScreen.HideView(EScreens.kLeaderboards);
      UIDispatch.RightScreen.HideView(EScreens.kGameLevels);
    }

    protected override void DidDisappear() => this._frontPanel.Hide();
  }
}
