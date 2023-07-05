// Decompiled with JetBrains decompiler
// Type: FreeplayMinigameSelectionPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using FootballVR.UI;
using Framework.Data;
using Framework.Networked;
using Photon.Pun;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using UnityEngine;

public class FreeplayMinigameSelectionPage : TabletPage
{
  [SerializeField]
  private GameObject _tabletObject;
  [SerializeField]
  private TouchButton _bossModeButton;
  [SerializeField]
  private TouchButton _throwingGameButton;
  [SerializeField]
  private TouchButton _dodgeballButton;
  [SerializeField]
  private TouchButton _freeplayButton;
  [SerializeField]
  private TouchButton _psvrInviteButton;
  private bool _selectedButton;
  private readonly LinksHandler _linksHandler = new LinksHandler();
  private readonly RoutineHandle _routineHandle = new RoutineHandle();
  private PhotonView _photonView;
  public static FreeplayMinigameSelectionPage Instance;

  private static RequestRoomInfo requestRoomInfo => NetworkState.requestRoomInfo;

  private void OnEnable() => FreeplayMinigameSelectionPage.Instance = this;

  private void OnDisable() => FreeplayMinigameSelectionPage.Instance = (FreeplayMinigameSelectionPage) null;

  private void Awake()
  {
    this._pageType = TabletPage.Pages.Main;
    this.OpenWindow();
    this._bossModeButton.onClick += new System.Action(this.HandleBossModeButton);
    this._throwingGameButton.onClick += new System.Action(this.HandleThrowingGameButton);
    this._dodgeballButton.onClick += new System.Action(this.HandleDodgeballButton);
    this._freeplayButton.onClick += new System.Action(this.HandleFreeplayButton);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._bossModeButton != (UnityEngine.Object) null)
      this._bossModeButton.onClick -= new System.Action(this.HandleBossModeButton);
    if ((UnityEngine.Object) this._throwingGameButton != (UnityEngine.Object) null)
      this._throwingGameButton.onClick -= new System.Action(this.HandleThrowingGameButton);
    if ((UnityEngine.Object) this._dodgeballButton != (UnityEngine.Object) null)
      this._dodgeballButton.onClick -= new System.Action(this.HandleDodgeballButton);
    if (!((UnityEngine.Object) this._freeplayButton != (UnityEngine.Object) null))
      return;
    this._freeplayButton.onClick -= new System.Action(this.HandleFreeplayButton);
  }

  private void HandleBossModeButton() => this.HandleGameTypeButton(this._bossModeButton);

  private void HandleThrowingGameButton() => this.HandleGameTypeButton(this._throwingGameButton);

  private void HandleDodgeballButton() => this.HandleGameTypeButton(this._dodgeballButton);

  private void HandleFreeplayButton() => this.HandleGameTypeButton(this._freeplayButton);

  private void HandleGameTypeButton(TouchButton button)
  {
    if (this._selectedButton || !PhotonNetwork.IsMasterClient)
      return;
    this._selectedButton = true;
    FreeplayMinigameSelectionPage.requestRoomInfo.GameTypeID = button.GetID().ToString();
    if (PhotonNetwork.IsMasterClient)
      NetworkState.InstantiatedMultiplayerManager.UpdateRoom(FreeplayMinigameSelectionPage.requestRoomInfo.GameTypeID, NetworkState.requestRoomInfo.HostName, NetworkState.requestRoomInfo.Password, NetworkState.requestRoomInfo.StadiumName, (int) NetworkState.requestRoomInfo.TimeOfDay, NetworkState.requestRoomInfo.Platform);
    Debug.Log((object) ("Loading Multiplayer ID: " + FreeplayMinigameSelectionPage.requestRoomInfo.GameTypeID));
    MultiplayerEvents.LoadMultiplayerGame.Trigger(MultiplayerManager.GetMultiplayerAppStateByID(FreeplayMinigameSelectionPage.requestRoomInfo.GameTypeID, true));
  }
}
