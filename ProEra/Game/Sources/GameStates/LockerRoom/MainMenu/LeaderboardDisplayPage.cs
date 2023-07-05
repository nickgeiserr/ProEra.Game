// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.GameStates.LockerRoom.MainMenu.LeaderboardDisplayPage
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
  public class LeaderboardDisplayPage : TabletPage
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
    [SerializeField]
    private TouchUI2DButton _nextPageButton;
    [SerializeField]
    private TouchUI2DButton _prevPageButton;
    private int _currentPageNum;
    private int _maxPageNum;
    private ListElementModel[] _scores;
    private string _displayName;
    private const int PAGE_COUNT = 10;

    private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

    public string Key { get; set; }

    private void Awake() => this.ValidateInspectorBinding();

    private void OnEnable()
    {
      this.UpdateMaxScrollDistance();
      this._backButton.onClick += new System.Action(this.HandleBackButton);
      this._nextPageButton.onClick += new System.Action(this.HandleNextPage);
      this._prevPageButton.onClick += new System.Action(this.HandlePreviousPage);
    }

    private void OnDisable()
    {
      this.UpdateMaxScrollDistance();
      this._backButton.onClick -= new System.Action(this.HandleBackButton);
      this._nextPageButton.onClick -= new System.Action(this.HandleNextPage);
      this._prevPageButton.onClick -= new System.Action(this.HandlePreviousPage);
    }

    public void PopulateLeaderboard()
    {
      this._contentWindow.DestroyAllChildren();
      PersistentSingleton<PlayerApi>.Instance.GetDisplayName((Action<string>) (displayName =>
      {
        this._displayName = displayName;
        PersistentSingleton<LeaderboardApi>.Instance.GetLeaderboard(this.Key, (Action<LeaderboardModel>) (leaderboardModel =>
        {
          this._titleText.text = leaderboardModel.Name.Replace('_', ' ');
          this._scores = leaderboardModel.HighScores;
          this._maxPageNum = this._scores.Length / 10 + (this._scores.Length % 10 > 0 ? 1 : 0);
          this._nextPageButton.gameObject.SetActive(true);
          this._prevPageButton.gameObject.SetActive(false);
          if (this._scores.Length <= 10)
            this._nextPageButton.gameObject.SetActive(false);
          this.UpdateCurrentPage();
        }));
      }));
    }

    private void ValidateInspectorBinding()
    {
    }

    private void UpdateCurrentPage()
    {
      if (this._scores == null || this._scores.Length == 0)
        return;
      for (int index = 10 * this._currentPageNum; index < 10 * (this._currentPageNum + 1); ++index)
      {
        if (this._contentWindow.childCount <= index % 10)
          UnityEngine.Object.Instantiate<LeaderboardElement>(this._leaderboardElementPrefab, (Transform) this._contentWindow);
        LeaderboardElement component = this._contentWindow.GetChild(index % 10).GetComponent<LeaderboardElement>();
        if (index < this._scores.Length)
        {
          component.gameObject.SetActive(true);
          component.Rank = index + 1;
          component.Name = this._scores[index].UserID;
          component.IsHighlighted = component.Name == this._displayName;
          component.Points = this._scores[index].Score;
          component.PointsLabel = this._scores[index].ExtraData.IsNullOrEmpty() ? "pts" : this._scores[index].ExtraData;
        }
        else
          component.gameObject.SetActive(false);
      }
    }

    private void UpdateMaxScrollDistance() => this._scrollRect.MaxScrollDistance = ((IEnumerable<LeaderboardElement>) this._contentWindow.GetComponentsInChildren<LeaderboardElement>()).Sum<LeaderboardElement>((Func<LeaderboardElement, float>) (element => element.RectTransform.rect.height));

    private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.LeaderboardMenu);

    public void HandlePreviousPage()
    {
      --this._currentPageNum;
      this._currentPageNum = Mathf.Max(this._currentPageNum, 0);
      this._nextPageButton.gameObject.SetActive(true);
      this._prevPageButton.gameObject.SetActive(true);
      if (this._currentPageNum == 0)
        this._prevPageButton.gameObject.SetActive(false);
      this.UpdateCurrentPage();
    }

    public void HandleNextPage()
    {
      ++this._currentPageNum;
      this._currentPageNum = Mathf.Min(this._currentPageNum, this._maxPageNum - 1);
      this._nextPageButton.gameObject.SetActive(true);
      this._prevPageButton.gameObject.SetActive(true);
      if (this._currentPageNum == this._maxPageNum - 1)
        this._nextPageButton.gameObject.SetActive(false);
      this.UpdateCurrentPage();
    }
  }
}
