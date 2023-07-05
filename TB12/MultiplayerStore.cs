// Decompiled with JetBrains decompiler
// Type: TB12.MultiplayerStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/PassMultiplayerStore", fileName = "PassMultiplayerStore")]
  [AppStore]
  public class MultiplayerStore : GameplayStore
  {
    public List<PlayerStatsData> PlayerDatas = new List<PlayerStatsData>();
    public readonly AppEvent<int, float> OnHealthChanged = new AppEvent<int, float>();
    public readonly AppEvent OnDataChanged = new AppEvent();
    public BossModeResultsUIData bossResultsData;
    public DodgeballResultsUIData dodgeballResultsData;
    public ThrowGameResultsUIData throwGameResultsData;
    private PlayerStatsData playerData;
    private int len;

    public int LocalPlayerId { get; set; }

    public PlayerStatsData LocalPlayerData => this.GetOrCreatePlayerData(this.LocalPlayerId);

    public PlayerStatsData GetOrCreatePlayerData(
      int playerId,
      float health = 2f,
      bool onTeamOne = false,
      bool isBoss = false,
      bool createNewIfNotFound = true)
    {
      this.playerData = (PlayerStatsData) null;
      this.len = this.PlayerDatas.Count;
      if (this.len > 0)
      {
        for (int index = 0; index < this.len; ++index)
        {
          this.playerData = this.PlayerDatas[index];
          if (this.playerData.playerId == playerId)
            return this.playerData;
        }
      }
      if (createNewIfNotFound)
      {
        this.playerData = new PlayerStatsData()
        {
          playerId = playerId,
          health = health,
          onTeamOne = onTeamOne,
          isBoss = isBoss
        };
        this.PlayerDatas.Add(this.playerData);
      }
      else
        this.playerData = (PlayerStatsData) null;
      return this.playerData;
    }

    public void SetPlayerData(PlayerStatsData data)
    {
      this.playerData = this.GetOrCreatePlayerData(data.playerId);
      this.playerData = data;
    }

    public void SetPlayerData(
      int playerId,
      float health,
      int score,
      bool onTeamOne,
      bool isBoss,
      int gameId)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.health = health;
      this.playerData.score = score;
      this.playerData.onTeamOne = onTeamOne;
      this.playerData.isBoss = isBoss;
      this.playerData.gameId = gameId;
      this.OnDataChanged?.Trigger();
    }

    public override void ResetStore()
    {
      if (!PhotonNetwork.InRoom)
        return;
      Debug.Log((object) "Resetting Multiplayer Store...");
      base.ResetStore();
      this.PlayerDatas.Clear();
    }

    public void SetHealth(int playerId, float health)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.health = health;
      this.OnDataChanged?.Trigger();
    }

    public void RecordPoints(int playerId, int score)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.score = score;
      this.OnDataChanged?.Trigger();
    }

    public float ChangeHealth(int playerId, float newHealth)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.health = newHealth;
      this.OnHealthChanged?.Trigger(playerId, this.playerData.health);
      this.OnDataChanged?.Trigger();
      return this.playerData.health;
    }

    public float SetHealthToZero(int playerId)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.health = 0.0f;
      this.OnHealthChanged?.Trigger(playerId, this.playerData.health);
      this.OnDataChanged?.Trigger();
      return this.playerData.health;
    }

    public PlayerStatsData SetPlayerToBoss(int playerId, float bossHealth)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.isBoss = true;
      this.playerData.health = bossHealth;
      this.OnDataChanged?.Trigger();
      return this.playerData;
    }

    public void SetThrowsMade(int playerId, int throws)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.throwsMade = throws;
      this.OnDataChanged?.Trigger();
    }

    public void SetToTeamOne(int playerId, bool onTeamOne)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.playerData.onTeamOne = onTeamOne;
      this.OnDataChanged?.Trigger();
    }

    public void RemovePlayer(int playerId)
    {
      this.playerData = this.GetOrCreatePlayerData(playerId);
      this.PlayerDatas.Remove(this.playerData);
      this.OnDataChanged?.Trigger();
    }

    public override void HandleOpponentHit()
    {
    }

    public List<int> GetCurrentLobbyPlayers() => PhotonNetwork.CurrentRoom != null ? new List<int>((IEnumerable<int>) PhotonNetwork.CurrentRoom.Players.Keys) : (List<int>) null;
  }
}
