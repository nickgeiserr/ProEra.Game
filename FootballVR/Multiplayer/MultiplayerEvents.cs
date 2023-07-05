// Decompiled with JetBrains decompiler
// Type: FootballVR.Multiplayer.MultiplayerEvents
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Networked;
using Photon.Realtime;
using System;
using UnityEngine;
using Vars;

namespace FootballVR.Multiplayer
{
  [RuntimeState]
  public static class MultiplayerEvents
  {
    public static readonly NetworkedEvent<System.Type, int> SyncViewID = new NetworkedEvent<System.Type, int>((Enum) ENetEvents.SyncViewID);
    public static readonly AppEvent StartGame = new AppEvent();
    public static readonly AppEvent<int> LoadMap = new AppEvent<int>();
    public static readonly AppEvent<float> BallGroundCheckTimer = new AppEvent<float>();
    public static readonly AppEvent<Vector3, Vector3, int> BallWasThrown = new AppEvent<Vector3, Vector3, int>();
    public static readonly AppEvent UpdateNames = new AppEvent();
    public static readonly NetworkedEvent<int, int, bool> BallWasCaught = new NetworkedEvent<int, int, bool>((Enum) ENetEvents.BallCaught, ReceiverGroup.MasterClient);
    public static readonly AppEvent<Vector3> PlayIntro = new AppEvent<Vector3>();
    public static readonly NetworkedEvent<Vector3> FireSpawnEffect = new NetworkedEvent<Vector3>((Enum) ENetEvents.FireSpawnEffect);
    public static readonly ManagedEvent StartThrowChallenge = new ManagedEvent();
    public static readonly NetworkedEvent<string, int> JoinThrowChallenge = new NetworkedEvent<string, int>((Enum) ENetEvents.JoinThrowChallenge);
    public static readonly ManagedEvent StartMinigameLobby = new ManagedEvent();
    public static readonly NetworkedEvent<string, int> JoinMinigameLobby = new NetworkedEvent<string, int>((Enum) ENetEvents.JoinMinigameLobby);
    public static readonly NetworkedEvent DeleteAllUI = new NetworkedEvent((Enum) ENetEvents.DeleteAllUI);
    public static readonly ManagedEvent BackToLobby = new ManagedEvent();
    public static readonly NetworkedEvent<int> KickPlayer = new NetworkedEvent<int>((Enum) ENetEvents.KickPlayer);
    public static readonly NetworkedEvent<int, int, bool> BallHitPlayerMaster = new NetworkedEvent<int, int, bool>((Enum) ENetEvents.BallHitPlayer, ReceiverGroup.MasterClient);
    public static readonly NetworkedEvent<int> BallHitPlayerFX = new NetworkedEvent<int>((Enum) ENetEvents.BallHitPlayerFX);
    public static readonly NetworkedEvent PlayerIsOutFX = new NetworkedEvent((Enum) ENetEvents.PlayerOut);
    public static readonly NetworkedEvent<int, Vector3> SpawnBall = new NetworkedEvent<int, Vector3>((Enum) ENetEvents.SpawnBall, ReceiverGroup.MasterClient);
    public static readonly NetworkedEvent<bool> DisplayHealthBar = new NetworkedEvent<bool>((Enum) ENetEvents.DisplayHealthBar);
    public static readonly NetworkedEvent<int, float> SetHealth = new NetworkedEvent<int, float>((Enum) ENetEvents.SetHealth);
    public static readonly NetworkedEvent<int, float> UpdateHealth = new NetworkedEvent<int, float>((Enum) ENetEvents.UpdateHealth);
    public static readonly NetworkedEvent<string> LoadMultiplayerGame = new NetworkedEvent<string>((Enum) ENetEvents.LoadMinigame);
  }
}
