// Decompiled with JetBrains decompiler
// Type: AssetCache
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class AssetCache : MonoBehaviour
{
  private static Dictionary<Transform, PlayerAI> playerAICache;

  public static void CreatePlayerAICache() => AssetCache.playerAICache = new Dictionary<Transform, PlayerAI>();

  public static void AddPlayersToPlayerAICache(List<PlayerAI> players)
  {
    for (int index = 0; index < players.Count; ++index)
      AssetCache.playerAICache.Add(players[index].trans, players[index]);
  }

  public static PlayerAI GetPlayerAI(Transform trans) => AssetCache.playerAICache.ContainsKey(trans) ? AssetCache.playerAICache[trans] : (PlayerAI) null;

  public static void Clear()
  {
    if (AssetCache.playerAICache == null)
      return;
    AssetCache.playerAICache.Clear();
    AssetCache.playerAICache = (Dictionary<Transform, PlayerAI>) null;
  }
}
