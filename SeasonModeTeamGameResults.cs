// Decompiled with JetBrains decompiler
// Type: SeasonModeTeamGameResults
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SeasonModeTeamGameResults
{
  public int teamIndex;
  public string teamAbbrev;
  public List<SeasonModeGameInfo> games;

  public SeasonModeTeamGameResults() => this.games = new List<SeasonModeGameInfo>();

  public void Print()
  {
    Debug.Log((object) ("**Team Schedule For : " + this.teamAbbrev));
    foreach (SeasonModeGameInfo game in this.games)
    {
      if (game.homeTeamIndex >= 0 && game.awayTeamIndex >= 0)
      {
        string str1 = "--";
        string str2 = " at ";
        string str3 = " Not played ";
        bool flag = this.teamIndex == game.homeTeamIndex;
        string str4 = flag ? game.awayTeamAbbrev : game.homeTeamAbbrev;
        int num;
        if (game.homeScore >= 0)
        {
          if (flag)
          {
            str2 = "vs";
            num = game.homeScore;
            string str5 = num.ToString();
            num = game.awayScore;
            string str6 = num.ToString();
            str1 = str5 + "-" + str6;
            str3 = game.homeScore > game.awayScore ? "W" : "L";
          }
          else
          {
            num = game.awayScore;
            string str7 = num.ToString();
            num = game.homeScore;
            string str8 = num.ToString();
            str1 = str7 + "-" + str8;
            str3 = game.awayScore > game.homeScore ? "W" : "L";
          }
        }
        Debug.Log((object) string.Format("     Week {0} : {1} {2}, {3}-{4}", (object) game.week, (object) str2, (object) str4, (object) str3, (object) str1));
      }
      else
        Debug.Log((object) string.Format("     Week {0} : BYE", (object) game.week));
    }
  }
}
