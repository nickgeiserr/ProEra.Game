// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.GameStates.LockerRoom.MainMenu.TwoMinuteDrillLeaderboardPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Web;
using ProEra.Web.Models.Leaderboard;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WebSocketSharp;

namespace ProEra.Game.Sources.GameStates.LockerRoom.MainMenu
{
  public class TwoMinuteDrillLeaderboardPage : TabletPage
  {
    [SerializeField]
    private LeaderboardElement _leaderboardElementPrefab;
    [SerializeField]
    private RectTransform _contentWindow;
    [SerializeField]
    private TouchUI2DScrollRect _scrollRect;
    [SerializeField]
    private TouchUI2DButton _backButton;
    [SerializeField]
    private TextMeshProUGUI _titleText;
    private const int ITEMS_PER_PAGE = 50;

    private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

    public string Key { get; set; }

    private void Awake()
    {
      this._pageType = TabletPage.Pages.TwoMDLeaderboard;
      this.ValidateInspectorBinding();
      this.UpdateMaxScrollDistance();
      this.Key = Definitions.HighScoreNames[Definitions.HighScore.TwoMinuteDrill];
      this.PopulateLeaderboard();
    }

    private void OnEnable() => this._backButton.onClick += new System.Action(this.HandleBackButton);

    private void OnDisable() => this._backButton.onClick -= new System.Action(this.HandleBackButton);

    public void PopulateLeaderboard()
    {
      this._contentWindow.DestroyAllChildren();
      PersistentSingleton<LeaderboardApi>.Instance.GetLeaderboard(this.Key, (Action<LeaderboardModel>) (leaderboardModel =>
      {
        this._titleText.text = "2 Minute Drill Rankings";
        ListElementModel[] highScores = leaderboardModel.HighScores;
        for (int index = 0; index < Mathf.Min(highScores.Length, 50); ++index)
        {
          LeaderboardElement leaderboardElement = UnityEngine.Object.Instantiate<LeaderboardElement>(this._leaderboardElementPrefab, (Transform) this._contentWindow);
          leaderboardElement.Rank = index + 1;
          leaderboardElement.Name = highScores[index].UserID;
          leaderboardElement.Points = highScores[index].Score;
          leaderboardElement.PointsLabel = highScores[index].ExtraData.IsNullOrEmpty() ? "pts" : highScores[index].ExtraData;
        }
      }));
    }

    private void ValidateInspectorBinding()
    {
    }

    private void UpdateMaxScrollDistance() => this._scrollRect.MaxScrollDistance = ((IEnumerable<LeaderboardElement>) this._contentWindow.GetComponentsInChildren<LeaderboardElement>()).Sum<LeaderboardElement>((Func<LeaderboardElement, float>) (element => element.RectTransform.rect.height));

    private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.LeaderboardMenu);
  }
}
