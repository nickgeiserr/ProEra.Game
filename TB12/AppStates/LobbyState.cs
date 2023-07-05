// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.LobbyState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/Lobby State")]
  public class LobbyState : GameState
  {
    public Action StartCountdown;

    public override EAppState Id => EAppState.kLobby;

    public override bool showLoadingScreen => false;

    public override bool allowPause => false;

    protected override void OnEnterState() => base.OnEnterState();

    protected override void OnExitState()
    {
    }
  }
}
