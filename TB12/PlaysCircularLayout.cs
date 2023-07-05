// Decompiled with JetBrains decompiler
// Type: TB12.PlaysCircularLayout
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game;
using UnityEngine;

namespace TB12
{
  public class PlaysCircularLayout : CircularLayout
  {
    public void SetIndexByPlayData(PlayData InPlayData)
    {
      PlaysImageDataProvider dataSource = (PlaysImageDataProvider) this._dataSource;
      if (!((Object) dataSource != (Object) null))
        return;
      for (int i = 0; i < dataSource.itemCount; ++i)
      {
        PlayData play = PlaybookState.CurrentFormation.Value?.GetPlay(i);
        if (play != null && play == InPlayData)
        {
          this.CurrentIndex = i;
          break;
        }
      }
    }

    public void SetNextIndex(int dir)
    {
      PlaysImageDataProvider dataSource = (PlaysImageDataProvider) this._dataSource;
      if (!((Object) dataSource != (Object) null))
        return;
      int num = this.CurrentIndex + dir;
      if (num <= 0)
        num = dataSource.itemCount - 1;
      else if (num >= dataSource.itemCount)
        num = 0;
      this.CurrentIndex = num;
    }
  }
}
