// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.GameStates.LockerRoom.MainMenu.LeaderboardMenuPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProEra.Game.Sources.GameStates.LockerRoom.MainMenu
{
  public class LeaderboardMenuPage : TabletPage
  {
    [SerializeField]
    private TouchUI2DButton _leaderboardButtonPrefab;
    [SerializeField]
    private RectTransform _contentWindow;
    [SerializeField]
    private TouchUI2DScrollRect _scrollRect;
    [SerializeField]
    private TouchUI2DButton _backButton;
    [SerializeField]
    private LeaderboardDisplayPage _displayPage;

    private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

    private void Awake() => this.ValidateInspectorBinding();

    private void OnEnable()
    {
      this._backButton.onClick += new System.Action(this.HandleBackButton);
      PlayerApi.LoginSuccess += new Action<SaveKeycloakUserData>(this.OnKeycloakAuthSuccess);
      if (!PersistentSingleton<PlayerApi>.Instance.IsLoggedIn)
        return;
      this.OnKeycloakAuthSuccess();
    }

    private void OnDisable()
    {
      this._backButton.onClick -= new System.Action(this.HandleBackButton);
      PlayerApi.LoginSuccess -= new Action<SaveKeycloakUserData>(this.OnKeycloakAuthSuccess);
    }

    private void OnKeycloakAuthSuccess(SaveKeycloakUserData userData = null)
    {
      this._contentWindow.DestroyAllChildren();
      PersistentSingleton<LeaderboardApi>.Instance.GetKeys((Action<List<string>>) (keys =>
      {
        keys.ForEach((Action<string>) (key =>
        {
          TouchUI2DButton a_button = UnityEngine.Object.Instantiate<TouchUI2DButton>(this._leaderboardButtonPrefab, (Transform) this._contentWindow);
          this.SetPageButtonText(a_button, key);
          a_button.onClick += (System.Action) (() =>
          {
            this._displayPage.Key = key;
            (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.LeaderboardDisplay);
            this._displayPage.PopulateLeaderboard();
          });
        }));
        this.UpdateMaxScrollDistance();
      }));
    }

    private void SetPageButtonText(TouchUI2DButton a_button, string a_text)
    {
      string str = a_text.ToLower().Replace('_', ' ');
      a_button.SetLabelText(str);
    }

    private void UpdateMaxScrollDistance() => this._scrollRect.MaxScrollDistance = ((IEnumerable<RectTransform>) this._contentWindow.GetComponentsInChildren<RectTransform>()).Sum<RectTransform>((Func<RectTransform, float>) (element => element.rect.height));

    private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);

    private void ValidateInspectorBinding()
    {
    }
  }
}
