// Decompiled with JetBrains decompiler
// Type: TB12.UI.SearchRoomView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class SearchRoomView : UIView
  {
    [SerializeField]
    private TouchButton backButton;
    [SerializeField]
    private TouchButton confirmButton;
    [SerializeField]
    private TouchButton roomFocusButton;
    [SerializeField]
    private TouchButton inputFieldButton;
    [SerializeField]
    private MultiplayerRoomSelector roomSelector;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TouchButton passYesToggle;
    [SerializeField]
    private TouchButton passNoToggle;
    [SerializeField]
    private string searchValue;
    private static RequestRoomInfo searchRoomInfo = new RequestRoomInfo();
    private RoutineHandle routine = new RoutineHandle();

    public override Enum ViewId { get; } = (Enum) ELockerRoomUI.kSearchRoom;

    protected override void OnInitialize() => this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) this.inputFieldButton.Link(new System.Action(this.HandleInputFieldButton)),
      (EventHandle) this.backButton.Link(new System.Action(this.HandleBackButton)),
      (EventHandle) this.confirmButton.Link(new System.Action(this.HandleConfirmButton)),
      (EventHandle) this.passYesToggle.Link(new System.Action(this.HandlePassToggle)),
      (EventHandle) this.passNoToggle.Link(new System.Action(this.HandlePassToggle)),
      (EventHandle) this.roomFocusButton.Link(new System.Action(this.HandleRoomFocusButton))
    });

    protected override void WillAppear()
    {
      this.passYesToggle.HighlighAsSelected(SearchRoomView.searchRoomInfo.UsePassword, Color.white, new Color(0.9f, 0.9f, 0.1f, 1f));
      this.passNoToggle.HighlighAsSelected(!SearchRoomView.searchRoomInfo.UsePassword, Color.white, new Color(0.9f, 0.9f, 0.1f, 1f));
      this.roomFocusButton.GetComponent<ButtonText>().text = MultiplayerManager.GetMultiplayerAppStateByID(SearchRoomView.searchRoomInfo.GameTypeID);
      this.roomSelector.OnSelectedIndex += new Action<int>(this.SetGameTypeID);
    }

    protected override void WillDisappear() => this.roomSelector.OnSelectedIndex -= new Action<int>(this.SetGameTypeID);

    private void HandleBackButton() => UIDispatch.LockerRoomScreen.DisplayDialog(ELockerRoomUI.kMultiplayer);

    private void HandleRoomFocusButton()
    {
      this.roomSelector.gameObject.SetActive(true);
      this.roomSelector.Show();
    }

    private void HandleConfirmButton() => this.SetRoomName(this.inputField.text);

    private void HandlePassToggle()
    {
      bool flag = !SearchRoomView.searchRoomInfo.UsePassword;
      this.passYesToggle.HighlighAsSelected(flag, Color.white, new Color(0.9f, 0.9f, 0.1f, 1f));
      this.passNoToggle.HighlighAsSelected(!flag, Color.white, new Color(0.9f, 0.9f, 0.1f, 1f));
      this.SetPassword(flag);
    }

    private async void HandleInputFieldButton()
    {
      UIDispatch.LockerRoomScreen.CloseScreen();
      TextInputRequest request = new TextInputRequest(this.searchValue, true);
      await UIDispatch.FrontScreen.ProcessDialogRequest<TextInputRequest>(request);
      if (request.IsComplete)
      {
        this.searchValue = request.inputString;
        this.inputField.text = this.searchValue;
      }
      UIDispatch.FrontScreen.CloseScreen();
      UIDispatch.LockerRoomScreen.DisplayView(ELockerRoomUI.kSearchRoom);
      request = (TextInputRequest) null;
    }

    private void SetRoomName(string roomName) => SearchRoomView.searchRoomInfo.RoomName = roomName;

    private void SetPassword(bool hasPass) => SearchRoomView.searchRoomInfo.Password = hasPass ? "xxx" : string.Empty;

    public void SetGameTypeID(int gameTypeID)
    {
      SearchRoomView.searchRoomInfo.GameTypeID = gameTypeID.ToString();
      this.roomFocusButton.GetComponent<ButtonText>().text = MultiplayerManager.GetMultiplayerAppStateByID(SearchRoomView.searchRoomInfo.GameTypeID);
    }
  }
}
