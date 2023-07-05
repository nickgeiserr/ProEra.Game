// Decompiled with JetBrains decompiler
// Type: SocialFeaturesInitializer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

public class SocialFeaturesInitializer : MonoBehaviour
{
  [Header("Dependencies")]
  [SerializeField]
  private GroupPresenceManager groupPresenceManager;
  [SerializeField]
  private GroupPresenceHandler_Oculus groupPresence_Oculus;
  [SerializeField]
  private DeeplinkHandler_Oculus deeplink_oculus;
  [SerializeField]
  private DeeplinkHandler_Steam deeplink_steam;

  private void Awake() => this.deeplink_steam.CheckForDeeplink();

  private void InitSocialFeatures_Oculus() => Core.AsyncInitialize().OnComplete((Message<PlatformInitialize>.Callback) (msg =>
  {
    if (msg.IsError)
    {
      Debug.Log((object) ("<color=aqua>Oculus init failed: " + msg.GetError().Message + "</color>"));
    }
    else
    {
      Debug.Log((object) "<color=aqua>Oculus init complete</color>");
      Users.GetLoggedInUser().OnComplete(new Message<User>.Callback(this.groupPresence_Oculus.OnLoggedInUser));
      GroupPresence.SetJoinIntentReceivedNotificationCallback(new Message<GroupPresenceJoinIntent>.Callback(this.OnJoinIntentChangeNotif));
      GroupPresence.SetLeaveIntentReceivedNotificationCallback(new Message<GroupPresenceLeaveIntent>.Callback(this.groupPresence_Oculus.OnLeaveIntentChangeNotif));
      GroupPresence.SetInvitationsSentNotificationCallback(new Message<LaunchInvitePanelFlowResult>.Callback(this.groupPresence_Oculus.OnInviteSentNotif));
      this.groupPresence_Oculus.SetPresence("destination_Core_LockerRoom", false, (string) null, (string) null);
    }
  }));

  private void OnJoinIntentChangeNotif(Message<GroupPresenceJoinIntent> message)
  {
    if (message.IsError)
    {
      Debug.Log((object) ("<color=aqua>" + message.GetError().Message + "</color>"));
    }
    else
    {
      this.groupPresence_Oculus.JoinIntentChangeNotif(message);
      this.deeplink_oculus.JoinIntentChangeNotif(message);
    }
  }
}
