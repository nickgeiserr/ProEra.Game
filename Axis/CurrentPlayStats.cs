// Decompiled with JetBrains decompiler
// Type: Axis.CurrentPlayStats
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Axis
{
  public class CurrentPlayStats
  {
    public Dictionary<int, PlayerStats> players;

    public CurrentPlayStats()
    {
      this.players = new Dictionary<int, PlayerStats>();
      this.ClearCurrentPlayStats();
    }

    public void ClearCurrentPlayStats()
    {
      this.players.Clear();
      for (int key = 0; key < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++key)
        this.players.Add(key, new PlayerStats());
    }
  }
}
