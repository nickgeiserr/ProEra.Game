// Decompiled with JetBrains decompiler
// Type: GroupPresenceManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Networked;
using UnityEngine;

public class GroupPresenceManager : MonoBehaviour
{
  public static GroupPresenceManager Instance;
  [Header("Dependencies")]
  [SerializeField]
  private PlayerProfile playerProfile;
  [SerializeField]
  private GroupPresenceHandler_Oculus groupPresence_Oculus;
  [SerializeField]
  private GroupPresenceHandler_PS groupPresence_PS;
  [SerializeField]
  private GroupPresenceHandler_Steam groupPresence_Steam;

  private void OnEnable()
  {
    if ((Object) GroupPresenceManager.Instance == (Object) null)
      GroupPresenceManager.Instance = this;
    else
      Object.Destroy((Object) this.gameObject);
  }

  public void UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination newDestination)
  {
    if (!this.DestinationIsOffline(newDestination))
      Debug.LogError((object) "<color=orange>Tried to update the user's Group Presence status using the function for an offline destination, but 'newDestination' is not an offline one!</color>");
    else
      this.groupPresence_Steam.SetPresence(DestinationDefinitions.GetDestinationApiName(newDestination, EAppPlatform.DesktopVR), false);
  }

  public void UpdateGroupPresenceStatus_Multiplayer(
    DestinationDefinitions.Destination newDestination,
    bool isJoinable)
  {
    string roomName = NetworkState.requestRoomInfo.RoomName;
    string stadiumName = NetworkState.requestRoomInfo.StadiumName;
    string currentLobbySize = NetworkState.requestRoomInfo.PlayersAmount.ToString();
    if (this.DestinationIsOffline(newDestination))
      Debug.LogError((object) "<color=orange>Tried to update the user's Group Presence status using the function for a multiplayer (online) destination, but 'newDestination' is an offline one!</color>");
    else
      this.groupPresence_Steam.SetPresence(DestinationDefinitions.GetDestinationApiName(newDestination, EAppPlatform.DesktopVR), isJoinable, roomName, currentLobbySize);
  }

  public void ClearGroupPresenceStatus(DestinationDefinitions.Platform whichPlatform) => this.groupPresence_Steam.ClearRichPresence();

  public void OpenInvitePanel() => this.groupPresence_Steam.LaunchInvitePanel();

  private bool DestinationIsOffline(
    DestinationDefinitions.Destination whichDestination)
  {
    return whichDestination == DestinationDefinitions.Destination.Core_LockerRoom || whichDestination == DestinationDefinitions.Destination.Core_SeasonMode || whichDestination == DestinationDefinitions.Destination.Core_ExhibitionMode || whichDestination == DestinationDefinitions.Destination.Core_PracticeMode || whichDestination == DestinationDefinitions.Destination.Core_Onboarding || whichDestination == DestinationDefinitions.Destination.SoloMinigame_PassingChallenge || whichDestination == DestinationDefinitions.Destination.SoloMinigame_AgilityDrill || whichDestination == DestinationDefinitions.Destination.SoloMinicamp_PocketPassing_QbPresence || whichDestination == DestinationDefinitions.Destination.SoloMinicamp_DimeDropping_PrecisionPassing || whichDestination == DestinationDefinitions.Destination.SoloMinicamp_Rollout || whichDestination == DestinationDefinitions.Destination.SoloMinicamp_RunAndShoot || whichDestination == DestinationDefinitions.Destination.SoloMinigame_TwoMinuteDrill;
  }
}
