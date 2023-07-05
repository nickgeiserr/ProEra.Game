// Decompiled with JetBrains decompiler
// Type: DeeplinkHandler_Oculus
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.Networked;
using Oculus.Platform;
using Oculus.Platform.Models;
using ProEra.Game.Startup;
using TB12;
using TB12.AppStates;
using UnityEngine;

public class DeeplinkHandler_Oculus : MonoBehaviour
{
  public void JoinIntentChangeNotif(Message<GroupPresenceJoinIntent> message)
  {
    GroupPresenceJoinIntent data = message.Data;
    string destinationApiName = data.DestinationApiName;
    string matchSessionId = data.MatchSessionId;
    string lobbySessionId = data.LobbySessionId;
    Debug.Log((object) ("<color=aqua>Got updated Join Intent & setting the user's presence:\n" + ("-Deeplink Message:\n" + data.DeeplinkMessage + "\n-Api Name:\n" + destinationApiName + "\n-Lobby Session Id:\n" + lobbySessionId + "\n-Match Session Id:\n" + matchSessionId + "\n") + "</color>"));
    this.Deeplink(data);
    if (!((Object) GameManager.Instance != (Object) null))
      return;
    GameManager.Instance.GoToDeeplinkDestination();
  }

  private void Deeplink(GroupPresenceJoinIntent joinIntent)
  {
    string destinationApiName = joinIntent.DestinationApiName;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(destinationApiName))
    {
      case 1011950890:
        if (!(destinationApiName == "destination_Core_ExhibitionMode"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kPlay, EMode.kSolo, ETimeOfDay.Clear);
        break;
      case 1263327083:
        if (!(destinationApiName == "destination_Multiplayer_Lobby"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kMultiplayerLobby, EMode.kMultiplayer, ETimeOfDay.Clear);
        break;
      case 1527742445:
        if (!(destinationApiName == "destination_Multiplayer_Dodgeball"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kMultiplayerDodgeball, EMode.kMultiplayer, ETimeOfDay.Clear);
        break;
      case 1865312791:
        if (!(destinationApiName == "destination_Multiplayer_BossMode"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kMultiplayerBossModeGame, EMode.kMultiplayer, ETimeOfDay.Clear);
        break;
      case 1938040118:
        if (!(destinationApiName == "destination_Minigame_AgilityDrill"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kAgilityGame, EMode.kSolo, ETimeOfDay.Clear);
        break;
      case 1949643046:
        if (!(destinationApiName == "destination_Core_PracticeMode"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kPracticeMode, EMode.kSolo, ETimeOfDay.Clear);
        break;
      case 2700695066:
        if (!(destinationApiName == "destination_Minigame_PassingChallenge"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kPassGame, EMode.kSolo, ETimeOfDay.Clear);
        break;
      case 3539688523:
        if (!(destinationApiName == "destination_Multiplayer_ThrowingGame"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kMultiplayerThrowGame, EMode.kMultiplayer, ETimeOfDay.Clear);
        break;
      case 3576543402:
        if (!(destinationApiName == "destination_Core_SeasonMode"))
          break;
        this.PrepareDestinationOptions(joinIntent, EAppState.kPlay, EMode.kSolo, ETimeOfDay.Clear);
        break;
    }
  }

  private void PrepareDestinationOptions(
    GroupPresenceJoinIntent joinIntent,
    EAppState state,
    EMode mode,
    ETimeOfDay timeOfDay)
  {
    DestinationOptions destinationOptions = new DestinationOptions()
    {
      ApiName = joinIntent.DestinationApiName,
      AppState = state,
      Mode = mode,
      TimeOfDay = timeOfDay,
      LobbySessionID = joinIntent.LobbySessionId,
      MatchSessionID = joinIntent.MatchSessionId,
      Password = string.Empty,
      ExpectedPlayerCount = (ulong) NetworkState.PlayerCount.Value,
      StadiumName = joinIntent.MatchSessionId
    };
    StartupState.CurrentStartupDestination.SetValue(destinationOptions);
  }
}
