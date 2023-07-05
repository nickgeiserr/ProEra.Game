// Decompiled with JetBrains decompiler
// Type: MultiplayerCreatePage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.Multiplayer;
using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.Networked;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using TB12.UI;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerCreatePage : TabletPage
{
  [SerializeField]
  private PlayerProfile _playerProfile;
  [SerializeField]
  private PinView pinView;
  [SerializeField]
  private StadiumConfigsStore stadiumConfigStore;
  [SerializeField]
  private TMP_Text labelRoomSize;
  [SerializeField]
  private Transform roomSizeParentTransform;
  [SerializeField]
  private Image logoSprite;
  [SerializeField]
  private TouchUI2DButton previousStadiumButton;
  [SerializeField]
  private TouchUI2DButton nextStadiumButton;
  [SerializeField]
  private TouchUI2DButton createRoomButton;
  [SerializeField]
  private TouchUI2DButton backButton;
  [SerializeField]
  private TouchUI2DButton publicButton;
  [SerializeField]
  private TouchUI2DButton privateButton;
  private List<TouchUI2DButton> _roomSizeButtons = new List<TouchUI2DButton>();
  private bool _selectedStartButton;
  private int _storeIndex;
  private string currentRoomSize;
  private readonly LinksHandler _linksHandler = new LinksHandler();

  private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

  private TeamDataStore[] _allStores => SingletonBehaviour<PersistentData, MonoBehaviour>.instance.allTeamDataStores;

  public string CurrentRoomSize
  {
    get => this.currentRoomSize;
    private set
    {
      this.currentRoomSize = value;
      this.labelRoomSize.text = this.currentRoomSize;
    }
  }

  private void Awake()
  {
    this._pageType = TabletPage.Pages.MultiplayerCreate;
    this.backButton.onClick += new System.Action(this.HandleBackButton);
    this.publicButton.onClick += new System.Action(this.HandlePublicButton);
    this.privateButton.onClick += new System.Action(this.HandlePrivateButton);
    for (int index = 0; index < this.roomSizeParentTransform.childCount; ++index)
    {
      TouchUI2DButton component;
      if (this.roomSizeParentTransform.GetChild(index).TryGetComponent<TouchUI2DButton>(out component))
      {
        if (index + 1 == MultiplayerCreatePage.requestRoomInfo.PlayersAmount)
          component.OnClick();
        component.onClickInfo += new Action<TouchUI2DButton>(this.HandleRoomSizeButton);
        this._roomSizeButtons.Add(component);
      }
    }
    this.previousStadiumButton.onClick += new System.Action(this.HandlePreviousStadium);
    this.nextStadiumButton.onClick += new System.Action(this.HandleNextStadium);
    this.createRoomButton.onClick += new System.Action(this.HandleCreateRoomButton);
    this.HandleRoomSizeButton(MultiplayerCreatePage.requestRoomInfo.MaxPlayersAmount);
    this.HandleStadiumSelection();
    this.HandlePublicButton();
    this._linksHandler.SetLinks(new List<EventHandle>()
    {
      AppEvents.PinCodeSubmitted.Link<string>(new Action<string>(this.OnAddedPin))
    });
  }

  private void OnEnable() => NetworkState.requestRoomInfo = new RequestRoomInfo();

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);

  private void HandlePublicButton()
  {
    MultiplayerCreatePage.requestRoomInfo.IsVisible = true;
    this.publicButton.GetComponent<UnityEngine.UI.Button>().interactable = this.publicButton.enabled = false;
    this.privateButton.GetComponent<UnityEngine.UI.Button>().interactable = this.privateButton.enabled = true;
  }

  private void HandlePrivateButton()
  {
    MultiplayerCreatePage.requestRoomInfo.IsVisible = false;
    this.publicButton.GetComponent<UnityEngine.UI.Button>().interactable = this.publicButton.enabled = true;
    this.privateButton.GetComponent<UnityEngine.UI.Button>().interactable = this.privateButton.enabled = false;
  }

  private void HandleRoomSizeButton(TouchUI2DButton button) => this.HandleRoomSizeButton(button.GetID());

  private void HandleRoomSizeButton(int roomSize)
  {
    Debug.Log((object) ("HandleRoomSizeButton: roomSize[" + roomSize.ToString() + "]"));
    for (int index = 0; index < this.roomSizeParentTransform.childCount; ++index)
    {
      TouchUI2DButton component;
      if (this.roomSizeParentTransform.GetChild(index).TryGetComponent<TouchUI2DButton>(out component))
      {
        if (component.GetID() == roomSize)
          component.GetComponent<UnityEngine.UI.Button>().interactable = component.enabled = false;
        else
          component.GetComponent<UnityEngine.UI.Button>().interactable = component.enabled = true;
      }
    }
    this.CurrentRoomSize = roomSize.ToString();
    MultiplayerCreatePage.requestRoomInfo.MaxPlayersAmount = roomSize;
  }

  public void OnAddedPin(string value)
  {
    Debug.Log((object) "PIN set!");
    NetworkState.requestRoomInfo.Password = value;
  }

  private void HandlePreviousStadium()
  {
    --this._storeIndex;
    if (this._storeIndex < 0)
      this._storeIndex = this._allStores.Length - 1;
    this.HandleStadiumSelection();
  }

  private void HandleNextStadium()
  {
    ++this._storeIndex;
    if (this._storeIndex >= this._allStores.Length)
      this._storeIndex = 0;
    this.HandleStadiumSelection();
  }

  private void HandleStadiumSelection()
  {
    TeamDataStore allStore = this._allStores[this._storeIndex];
    NetworkState.requestRoomInfo.StadiumName = allStore.TeamName;
    Debug.Log((object) ("Lobby Environment Set To: " + NetworkState.requestRoomInfo.StadiumName));
    this.logoSprite.sprite = allStore.Logo;
  }

  private void HandleCreateRoomButton()
  {
    if (Application.internetReachability != NetworkReachability.NotReachable)
    {
      if (this._selectedStartButton)
        return;
      this._selectedStartButton = true;
      MultiplayerCreatePage.requestRoomInfo.HostName = this._playerProfile.PlayerFirstName + this._playerProfile.PlayerLastName;
      MultiplayerCreatePage.requestRoomInfo.TimeOfDay = (this.MainPage as MainMenuPage).CurrentTOD;
      MultiplayerCreatePage.requestRoomInfo.Platform = (int) Application.platform;
      WorldState.TimeOfDay.Value = MultiplayerCreatePage.requestRoomInfo.TimeOfDay;
      Debug.Log((object) ("Loading Multiplayer ID: " + MultiplayerCreatePage.requestRoomInfo.GameTypeID));
      PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
      SteamMultiplayerManager.CreateSteamLobby();
      MultiplayerEvents.LoadMultiplayerGame.Trigger(MultiplayerManager.GetMultiplayerAppStateByID(MultiplayerCreatePage.requestRoomInfo.GameTypeID, true));
    }
    else
      (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);
  }
}
