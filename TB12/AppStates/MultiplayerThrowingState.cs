﻿// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MultiplayerThrowingState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/MultiplayerThrowing")]
  public class MultiplayerThrowingState : MultiplayerGameState
  {
    public override EAppState Id { get; } = EAppState.kMultiplayerThrowGame;

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Multiplayer(DestinationDefinitions.Destination.Multiplayer_ThrowingGame, true);
  }
}
