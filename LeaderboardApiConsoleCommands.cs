// Decompiled with JetBrains decompiler
// Type: LeaderboardApiConsoleCommands
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using IngameDebugConsole;
using ProEra.Web.Models.Leaderboard;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardApiConsoleCommands : MonoBehaviour
{
  [ConsoleMethod("LeaderboardGetKeys", "Gets a list of keys for all leaderboards", new string[] {})]
  public static void GetKeys()
  {
    LeaderboardApiConsoleCommands.ValidateInstance();
    PersistentSingleton<LeaderboardApi>.Instance.GetKeys((Action<List<string>>) (keys => keys.ForEach((Action<string>) (x => Debug.Log((object) x)))));
  }

  [ConsoleMethod("LeaderboardGetLeaderboard", "Gets a leaderboard associated with a key", new string[] {})]
  public static void GetLeaderboard(string highScoreName, float value)
  {
    LeaderboardApiConsoleCommands.ValidateInstance();
    PersistentSingleton<LeaderboardApi>.Instance.GetLeaderboard(highScoreName, new Action<LeaderboardModel>(Debug.Log));
  }

  private static void ValidateInstance()
  {
  }
}
