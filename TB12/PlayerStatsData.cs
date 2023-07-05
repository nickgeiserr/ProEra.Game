// Decompiled with JetBrains decompiler
// Type: TB12.PlayerStatsData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Photon.Pun;
using System;
using UnityEngine;

namespace TB12
{
  [Serializable]
  public class PlayerStatsData
  {
    public int playerId;
    public int gameId;
    public int score;
    public int throwsMade;
    public int throwHits;
    public float health = 2f;
    public bool isBoss;
    public bool onTeamOne = true;

    public bool isLocalPlayer => PhotonNetwork.LocalPlayer.ActorNumber == this.playerId;

    public bool isValid()
    {
      if (PhotonNetwork.CurrentRoom == null)
        return false;
      Debug.Log((object) ("isValid: PhotonNetwork.CurrentRoom.Players.Count[" + PhotonNetwork.CurrentRoom.Players.Count.ToString() + "] playerId[" + this.playerId.ToString() + "] valid[" + PhotonNetwork.CurrentRoom.Players.ContainsKey(this.playerId).ToString() + "]"));
      return PhotonNetwork.CurrentRoom.Players.ContainsKey(this.playerId);
    }

    public bool isAlive => (double) this.health > 0.0;
  }
}
