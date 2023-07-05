// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.GameStates.LockerRoom.MainMenu.MainMenuPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using System;
using TB12;
using TMPro;
using UnityEngine;
using VRKeyboard.Utils;

namespace ProEra.Game.Sources.GameStates.LockerRoom.MainMenu
{
  public class MainMenuPage : TabletPage, ITabletPagePrevious
  {
    [SerializeField]
    private KeyboardManager _keyboard;
    private ITabletPageBase _currentWindow;
    private ITabletPageBase _previousWindow;
    [SerializeField]
    private TabletPage[] _childPages;
    [SerializeField]
    private TouchUI2DButton _exhibitionButton;
    [SerializeField]
    private TouchUI2DButton _multiplayerButton;
    [SerializeField]
    private TouchUI2DButton _twoMinuteDrillButton;
    [SerializeField]
    private TouchUI2DButton _miniGamesButton;
    [SerializeField]
    private TouchUI2DButton _settingsScreenButton;
    [SerializeField]
    private TouchUI2DButton _leaderboardButton;
    [SerializeField]
    private TouchUI2DButton _todButton;
    [SerializeField]
    private TouchDrag3D _ownerTouchDrag3D;
    private ETimeOfDay _currentTOD = ETimeOfDay.Clear;
    [SerializeField]
    private Sprite[] _todSprites;

    public ETimeOfDay CurrentTOD => this._currentTOD;

    private void Awake()
    {
      this._pageType = TabletPage.Pages.Main;
      this._currentWindow = (ITabletPageBase) this;
      this._previousWindow = (ITabletPageBase) this;
      this.OpenWindow();
      foreach (TabletPage childPage in this._childPages)
      {
        if (!((UnityEngine.Object) childPage == (UnityEngine.Object) null))
          childPage.RegisterMainPage((TabletPage) this);
      }
      this._exhibitionButton.onClick += new Action(this.HandleExhibitionButton);
      this._multiplayerButton.onClick += new Action(this.HandleMultiplayerButton);
      this._twoMinuteDrillButton.onClick += new Action(this.HandleTwoMinuteDrillButton);
      this._miniGamesButton.onClick += new Action(this.HandleMiniGamesButton);
      this._settingsScreenButton.onClick += new Action(this.HandleSettingsScreenButton);
      this._leaderboardButton.onClick += new Action(this.HandleLeaderboardButton);
      this._todButton.onClickInfo += new Action<TouchUI2DButton>(this.HandleTODToggle);
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this._exhibitionButton != (UnityEngine.Object) null)
        this._exhibitionButton.onClick -= new Action(this.HandleExhibitionButton);
      if ((UnityEngine.Object) this._multiplayerButton != (UnityEngine.Object) null)
        this._multiplayerButton.onClick -= new Action(this.HandleMultiplayerButton);
      if ((UnityEngine.Object) this._twoMinuteDrillButton != (UnityEngine.Object) null)
        this._twoMinuteDrillButton.onClick -= new Action(this.HandleTwoMinuteDrillButton);
      if ((UnityEngine.Object) this._miniGamesButton != (UnityEngine.Object) null)
        this._miniGamesButton.onClick -= new Action(this.HandleMiniGamesButton);
      if ((UnityEngine.Object) this._settingsScreenButton != (UnityEngine.Object) null)
        this._settingsScreenButton.onClick -= new Action(this.HandleSettingsScreenButton);
      if (!((UnityEngine.Object) this._leaderboardButton != (UnityEngine.Object) null))
        return;
      this._leaderboardButton.onClick -= new Action(this.HandleLeaderboardButton);
    }

    public void OpenPrevWindow()
    {
      this._currentWindow.CloseWindow();
      this._currentWindow = this._previousWindow;
      this._currentWindow.OpenWindow();
    }

    public void OpenPage(TabletPage.Pages pageType)
    {
      ITabletPageBase tabletBaseByType = this.GetTabletBaseByType(pageType);
      if (tabletBaseByType == null)
        return;
      this._previousWindow = this._currentWindow;
      this._currentWindow = tabletBaseByType;
      this._previousWindow.CloseWindow();
      this._currentWindow.OpenWindow();
    }

    private ITabletPageBase GetTabletBaseByType(TabletPage.Pages type)
    {
      foreach (TabletPage childPage in this._childPages)
      {
        if (!((UnityEngine.Object) childPage == (UnityEngine.Object) null) && childPage.GetPageType() == type)
          return (ITabletPageBase) childPage;
      }
      return (ITabletPageBase) null;
    }

    private void HandleExhibitionButton() => this.OpenPage(TabletPage.Pages.ExhibitionTeamSelect);

    private void HandleMultiplayerButton() => this.OpenPage(TabletPage.Pages.MultiplayerLoading);

    private void HandleMiniGamesButton() => this.OpenPage(TabletPage.Pages.MiniCamp);

    private void HandleSettingsScreenButton() => this.OpenPage(TabletPage.Pages.Settings);

    private void HandleTwoMinuteDrillButton() => this.OpenPage(TabletPage.Pages.TwoMDTop);

    private void HandleLeaderboardButton() => this.OpenPage(TabletPage.Pages.LeaderboardMenu);

    public KeyboardManager GetKeyboard() => this._keyboard;

    public void ShowKeyboard(TMP_InputField input)
    {
      this._keyboard.input = input;
      this._keyboard.gameObject.SetActive(true);
    }

    public void PutMeBack()
    {
      if (!((UnityEngine.Object) this._ownerTouchDrag3D != (UnityEngine.Object) null))
        return;
      this._ownerTouchDrag3D.Reset((ITouchInput) null);
    }

    public EGameMode MiniGameType
    {
      get => this.miniGameType;
      set => this.miniGameType = value;
    }

    public void EnableTODToggle(bool enabled) => this._todButton.gameObject.SetActive(enabled);

    public void HandleTODToggle(TouchUI2DButton info)
    {
      bool flag = this._currentTOD == ETimeOfDay.Clear;
      this._currentTOD = flag ? ETimeOfDay.Night : ETimeOfDay.Clear;
      info.SwapBGSprite(this._todSprites[flag ? 1 : 0]);
    }
  }
}
