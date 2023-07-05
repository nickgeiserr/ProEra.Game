// Decompiled with JetBrains decompiler
// Type: MultiplayerRoomsFilterPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplayerRoomsFilterPage : TabletPage
{
  [SerializeField]
  private TMP_InputField searchInputField;
  [SerializeField]
  private TouchUI2DButton backButton;
  [SerializeField]
  private TouchUI2DButton searchButton;
  [SerializeField]
  private TouchUI2DButton lobbyFocusButton;
  [SerializeField]
  private TouchUI2DButton confirmButton;
  private List<RoomInfoCell> cells = new List<RoomInfoCell>();

  private void Awake()
  {
    this._pageType = TabletPage.Pages.MutiplayerFilter;
    this.searchButton.onClick += new System.Action(this.HandleSearchButton);
    this.backButton.onClick += new System.Action(this.HandleBackButton);
    this.lobbyFocusButton.onClick += new System.Action(this.HandleLobbyFocusButton);
    this.confirmButton.onClick += new System.Action(this.HandleConfirmButton);
    (this.MainPage as MainMenuPage).GetKeyboard().OnConfirmInputText += new Action<string>(this.OnConfirmInputText);
  }

  private void OnConfirmInputText(string value) => this.searchInputField.text = value;

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this.backButton != (UnityEngine.Object) null)
      this.backButton.onClick -= new System.Action(this.HandleBackButton);
    if ((UnityEngine.Object) this.searchButton != (UnityEngine.Object) null)
      this.searchButton.onClick -= new System.Action(this.HandleSearchButton);
    if (!((UnityEngine.Object) this.MainPage != (UnityEngine.Object) null) || !((UnityEngine.Object) (this.MainPage as MainMenuPage).GetKeyboard() != (UnityEngine.Object) null))
      return;
    (this.MainPage as MainMenuPage).GetKeyboard().OnConfirmInputText -= new Action<string>(this.OnConfirmInputText);
  }

  private void HandleSearchButton() => (this.MainPage as MainMenuPage).ShowKeyboard(this.searchInputField);

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MultiplayerJoin);

  private void HandleLobbyFocusButton()
  {
  }

  private void HandleConfirmButton()
  {
  }
}
