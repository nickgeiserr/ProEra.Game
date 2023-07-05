// Decompiled with JetBrains decompiler
// Type: Axis.GameLog
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace Axis
{
  public class GameLog
  {
    private readonly List<PlayLog> plays;
    private readonly PlayLog currentPlay;

    public GameLog()
    {
      this.plays = new List<PlayLog>();
      this.currentPlay = new PlayLog();
    }

    public void BeginNewPlay()
    {
      this.plays.Add(this.currentPlay);
      this.currentPlay.Clear();
    }

    public void AddToPlayLog(string log) => this.currentPlay.Add(log);

    public string CurrentPlayToString() => this.currentPlay.ToString();
  }
}
