// Decompiled with JetBrains decompiler
// Type: GroupPresenceHandler_Oculus
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

public class GroupPresenceHandler_Oculus : MonoBehaviour
{
  [Header("Oculus - Group Presence data")]
  public bool IsJoinable;
  public string LobbySessionID;
  public string MatchSessionID;
  public ulong SuggestedUserID;
  private ulong LoggedInUserID;
  public string CurrentDestinationName;

  public void OnLoggedInUser(Message<User> message)
  {
    if (message.IsError)
      Debug.Log((object) "Cannot get logged in user for Oculus");
    else
      this.LoggedInUserID = message.Data.ID;
  }

  public void OnInviteSentNotif(Message<LaunchInvitePanelFlowResult> message)
  {
    if (message.IsError)
    {
      Debug.Log((object) ("<color=aqua>" + message.GetError().Message + "</color>"));
    }
    else
    {
      UserList invitedUsers = message.Data.InvitedUsers;
      int count = invitedUsers.Count;
      string str = "-Users:\n";
      if (count > 0)
      {
        foreach (User user in (DeserializableList<User>) invitedUsers)
          str = str + user.OculusID + "\n";
      }
      else
        str += "none\n";
      Debug.Log((object) ("<color=aqua>Users sent invite to:\n" + str + "</color>"));
    }
  }

  public void JoinIntentChangeNotif(Message<GroupPresenceJoinIntent> message)
  {
    GroupPresenceJoinIntent data = message.Data;
    string destinationApiName = data.DestinationApiName;
    string matchSessionId = data.MatchSessionId;
    string lobbySessionId = data.LobbySessionId;
    GroupPresenceOptions groupPresenceOptions = new GroupPresenceOptions();
    if (!string.IsNullOrEmpty(destinationApiName))
      groupPresenceOptions.SetDestinationApiName(destinationApiName);
    if (!string.IsNullOrEmpty(matchSessionId))
      groupPresenceOptions.SetMatchSessionId(matchSessionId);
    if (!string.IsNullOrEmpty(lobbySessionId))
      groupPresenceOptions.SetLobbySessionId(lobbySessionId);
    GroupPresence.Set(groupPresenceOptions);
  }

  public void OnLeaveIntentChangeNotif(Message<GroupPresenceLeaveIntent> message)
  {
    if (message.IsError)
    {
      Debug.Log((object) ("<color=aqua>" + message.GetError().Message + "</color>"));
    }
    else
    {
      GroupPresenceLeaveIntent data = message.Data;
      string destinationApiName = data.DestinationApiName;
      this.MatchSessionID = data.MatchSessionId;
      this.LobbySessionID = data.LobbySessionId;
      Debug.Log((object) ("<color=aqua>Clearing presence because user wishes to leave:\n" + ("-Api Name:\n" + destinationApiName + "\n-Lobby Session Id:\n" + this.LobbySessionID + "\n-Match Session Id:\n" + this.MatchSessionID + "\n") + "</color>"));
      GroupPresence.Clear();
    }
  }

  public void SetPresence(
    string destinationApiName,
    bool joinable,
    string matchSessionId,
    string lobbySessionId)
  {
    this.CurrentDestinationName = destinationApiName;
    this.IsJoinable = joinable;
    if (!string.IsNullOrEmpty(lobbySessionId))
      this.LobbySessionID = lobbySessionId;
    if (!string.IsNullOrEmpty(matchSessionId))
      this.MatchSessionID = matchSessionId;
    GroupPresenceOptions groupPresenceOptions = new GroupPresenceOptions();
    groupPresenceOptions.SetDestinationApiName(this.CurrentDestinationName);
    groupPresenceOptions.SetIsJoinable(this.IsJoinable);
    if (!string.IsNullOrEmpty(lobbySessionId))
      groupPresenceOptions.SetLobbySessionId(this.LobbySessionID);
    if (!string.IsNullOrEmpty(matchSessionId))
      groupPresenceOptions.SetMatchSessionId(this.MatchSessionID);
    Debug.Log((object) string.Format("<color=aqua>Updating Group Presence...\nDestination = {0}, Joinable = {1}, LobbySessionID = {2} MatchSessionID = {3}</color>", (object) this.CurrentDestinationName, (object) this.IsJoinable, (object) this.LobbySessionID, (object) this.MatchSessionID));
    GroupPresence.Set(groupPresenceOptions).OnComplete((Message.Callback) (message =>
    {
      if (message.IsError)
        Debug.Log((object) ("<color=aqua>Failed to set Oculus Group Presence: " + message.GetError().Message + "</color>"));
      else
        Users.Get(this.LoggedInUserID).OnComplete((Message<User>.Callback) (message2 =>
        {
          if (message2.IsError)
            Debug.Log((object) "<color=aqua>Success! But presence is unknown!</color>");
          else
            Debug.Log((object) ("<color=aqua>Group Presence set to:\n" + message2.Data.Presence + "\n" + message2.Data.PresenceDeeplinkMessage + "\n" + message2.Data.PresenceDestinationApiName + "</color>"));
        }));
    }));
  }

  public void ClearPresence()
  {
    Debug.Log((object) "<color=aqua>Clearing Group Presence...</color>");
    GroupPresence.Clear().OnComplete((Message.Callback) (message =>
    {
      if (message.IsError)
        Debug.Log((object) ("<color=aqua>" + message.GetError().Message + "</color>"));
      else
        Users.Get(this.LoggedInUserID).OnComplete((Message<User>.Callback) (message2 =>
        {
          if (message2.IsError)
            Debug.Log((object) "<color=aqua>Group Presence cleared! But rich presence is unknown!</color>");
          else
            Debug.Log((object) ("<color=aqua>Group Presence cleared!\n" + message2.Data.Presence + "\n</color>"));
        }));
    }));
  }

  public void LaunchRosterPanel()
  {
    Debug.Log((object) "<color=aqua>Launching Roster Panel...</color>");
    RosterOptions options = new RosterOptions();
    if (this.SuggestedUserID != 0UL)
      options.AddSuggestedUser(this.SuggestedUserID);
    GroupPresence.LaunchRosterPanel(options).OnComplete((Message.Callback) (message =>
    {
      if (!message.IsError)
        return;
      Debug.Log((object) ("<color=aqua>" + message.GetError().Message + "</color>"));
    }));
  }

  public void LaunchInvitePanel()
  {
    Debug.Log((object) "<color=aqua>Launching Invite Panel...</color>");
    InviteOptions options = new InviteOptions();
    if (this.SuggestedUserID != 0UL)
      options.AddSuggestedUser(this.SuggestedUserID);
    GroupPresence.LaunchInvitePanel(options).OnComplete((Message<InvitePanelResultInfo>.Callback) (message =>
    {
      if (!message.IsError)
        return;
      Debug.Log((object) ("<color=aqua>" + message.GetError().Message + "</color>"));
    }));
  }
}
