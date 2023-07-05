// Decompiled with JetBrains decompiler
// Type: GroupPresenceHandler_Steam
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Networked;
using Steamworks;
using UnityEngine;

public class GroupPresenceHandler_Steam : MonoBehaviour
{
  public void SetPresence(
    string destinationApiName,
    bool isJoinable,
    string lobbySessionID = "",
    string currentLobbySize = "")
  {
    SteamFriends.SetRichPresence("steam_display", destinationApiName);
    if (!isJoinable || !(bool) NetworkState.InRoom)
      return;
    string str = "steam://run/2165690//?appState=" + NetworkState.requestRoomInfo.GameTypeID + ";lobbySessionID=" + lobbySessionID + ";stadiumID=" + NetworkState.requestRoomInfo.StadiumName + ";";
    Debug.Log((object) str);
    SteamFriends.SetRichPresence("connect", str);
    SteamFriends.SetRichPresence("steam_player_group", lobbySessionID);
    SteamFriends.SetRichPresence("steam_player_group_size", currentLobbySize);
  }

  public void ClearRichPresence() => SteamFriends.ClearRichPresence();

  public void LaunchInvitePanel()
  {
    Debug.Log((object) ("SteamMultiplayerManager.GetSteamLobbyID(): " + SteamMultiplayerManager.GetSteamLobbyID().ToString()));
    SteamFriends.ActivateGameOverlayInviteDialog(SteamMultiplayerManager.GetSteamLobbyID());
  }
}
