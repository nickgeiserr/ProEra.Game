// Decompiled with JetBrains decompiler
// Type: DeeplinkHandler_Steam
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game.Startup;
using Steamworks;
using System;
using System.Collections.Generic;
using TB12;
using TB12.AppStates;
using UnityEngine;

public class DeeplinkHandler_Steam : MonoBehaviour
{
  protected Callback<NewUrlLaunchParameters_t> m_NewUrlLaunchParameters;
  protected Callback<GameLobbyJoinRequested_t> m_GameLobbyJoinRequested;

  private void OnEnable()
  {
    this.m_NewUrlLaunchParameters = Callback<NewUrlLaunchParameters_t>.Create(new Callback<NewUrlLaunchParameters_t>.DispatchDelegate(this.OnNewUrlLaunchParameters));
    this.m_GameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnGameLobbyJoinRequested));
  }

  private void OnNewUrlLaunchParameters(NewUrlLaunchParameters_t pCallback)
  {
    Debug.Log((object) ("OnNewUrlLaunchParameters: [" + 1014.ToString() + " - NewUrlLaunchParameters]"));
    string pszCommandLine;
    SteamApps.GetLaunchCommandLine(out pszCommandLine, 400);
    Debug.Log((object) ("OnNewUrlLaunchParameters: cmdLine: " + pszCommandLine));
    Debug.Log((object) ("DeeplinkHandler_Steam: CheckForDeeplink: lobbySessionID[" + SteamApps.GetLaunchQueryParam("lobbySessionID") + "]"));
  }

  public void CheckForDeeplink()
  {
    string pszCommandLine;
    SteamApps.GetLaunchCommandLine(out pszCommandLine, 400);
    Debug.Log((object) ("CheckForDeeplink: cmdLine: " + pszCommandLine));
    if (string.IsNullOrEmpty(pszCommandLine))
      return;
    string[] strArray1 = pszCommandLine.Split("?", StringSplitOptions.None);
    Debug.Log((object) ("CheckForDeeplink: paramsString[1][" + strArray1[1] + "]"));
    string[] strArray2 = strArray1[1].Split(";", StringSplitOptions.None);
    List<string> stringList = new List<string>();
    for (int index = 0; index < strArray2.Length; ++index)
    {
      Debug.Log((object) ("CheckForDeeplink: paramsKVP[i][" + strArray2[index] + "]"));
      if (strArray2[index].Length > 0)
      {
        string[] strArray3 = strArray2[index].Split("=", StringSplitOptions.None);
        Debug.Log((object) ("CheckForDeeplink: temp[1][" + strArray3[1] + "]"));
        stringList.Add(strArray3[1]);
      }
    }
    string str1 = stringList[1];
    Debug.Log((object) ("DeeplinkHandler_Steam: CheckForDeeplink: lobbySessionID[" + str1 + "]"));
    if (!(str1 != ""))
      return;
    int num = int.Parse(stringList[0]);
    string str2 = stringList[2];
    string str3 = "";
    EAppState eappState = EAppState.kMultiplayerLobby;
    switch (num)
    {
      case 0:
        str3 = "#ThrowingGame";
        eappState = EAppState.kMultiplayerThrowGame;
        break;
      case 1:
        str3 = "#Dodgeball";
        eappState = EAppState.kMultiplayerDodgeball;
        break;
      case 2:
        str3 = "#BossMode";
        eappState = EAppState.kMultiplayerBossModeGame;
        break;
      case 3:
        str3 = "#MultiplayerLobby";
        eappState = EAppState.kMultiplayerLobby;
        break;
    }
    DestinationOptions destinationOptions = new DestinationOptions()
    {
      ApiName = str3,
      AppState = eappState,
      Mode = EMode.kMultiplayer,
      TimeOfDay = ETimeOfDay.Clear,
      LobbySessionID = str1,
      StadiumName = str2
    };
    Debug.Log((object) ("DeeplinkHandler_Steam: apiName[" + str3 + "] appState[" + num.ToString() + "] lobbySessionID[" + str1 + "] stadiumID[" + str2 + "]"));
    StartupState.CurrentStartupDestination.SetValue(destinationOptions);
    if (!((UnityEngine.Object) GameManager.Instance != (UnityEngine.Object) null))
      return;
    GameManager.Instance.GoToDeeplinkDestination();
  }

  private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t pCallback)
  {
    Debug.Log((object) ("[" + 333.ToString() + " - GameLobbyJoinRequested] - " + pCallback.m_steamIDLobby.ToString() + " -- " + pCallback.m_steamIDFriend.ToString()));
    int num1 = int.Parse(SteamMatchmaking.GetLobbyData(pCallback.m_steamIDLobby, "appState"));
    string lobbyData1 = SteamMatchmaking.GetLobbyData(pCallback.m_steamIDLobby, "lobbySessionID");
    string lobbyData2 = SteamMatchmaking.GetLobbyData(pCallback.m_steamIDLobby, "stadiumID");
    int num2 = int.Parse(SteamMatchmaking.GetLobbyData(pCallback.m_steamIDLobby, "timeOfDay"));
    string str = "";
    EAppState eappState = EAppState.kMultiplayerLobby;
    switch (num1)
    {
      case 0:
        str = "#ThrowingGame";
        eappState = EAppState.kMultiplayerThrowGame;
        break;
      case 1:
        str = "#Dodgeball";
        eappState = EAppState.kMultiplayerDodgeball;
        break;
      case 2:
        str = "#BossMode";
        eappState = EAppState.kMultiplayerBossModeGame;
        break;
      case 3:
        str = "#MultiplayerLobby";
        eappState = EAppState.kMultiplayerLobby;
        break;
    }
    DestinationOptions destinationOptions = new DestinationOptions()
    {
      ApiName = str,
      AppState = eappState,
      Mode = EMode.kMultiplayer,
      TimeOfDay = (ETimeOfDay) num2,
      LobbySessionID = lobbyData1,
      StadiumName = lobbyData2
    };
    Debug.Log((object) ("DeeplinkHandler_Steam: apiName[" + str + "] appState[" + num1.ToString() + "] lobbySessionID[" + lobbyData1 + "] stadiumID[" + lobbyData2 + "]"));
    StartupState.CurrentStartupDestination.SetValue(destinationOptions);
    if (!((UnityEngine.Object) GameManager.Instance != (UnityEngine.Object) null))
      return;
    GameManager.Instance.GoToDeeplinkDestination();
  }
}
