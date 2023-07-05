// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MultiplayerLobbyState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/MultiplayerLobby")]
  public class MultiplayerLobbyState : MultiplayerGameState
  {
    public override EAppState Id { get; } = EAppState.kMultiplayerLobby;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      VRState.LocomotionEnabled.SetValue(true);
      Debug.Log((object) ("MP Locomotion: " + VRState.LocomotionEnabled.Value.ToString()));
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Multiplayer(DestinationDefinitions.Destination.Multiplayer_Lobby, true);
  }
}
