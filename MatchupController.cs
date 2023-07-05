// Decompiled with JetBrains decompiler
// Type: MatchupController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources;
using UDB;
using UnityEngine;

public static class MatchupController
{
  public static void StartGame()
  {
    if (global::Game.IsSpectateMode && !SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startedFromGameScene)
    {
      MatchupController.StartAIVsAIGame();
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetBallOnTee();
      UI.ScoreClock.ShowWindow();
    }
    else
      UI.CoinToss.ShowWindow();
  }

  private static void StartAIVsAIGame()
  {
    int num = Random.Range(0, 2);
    EMatchState newMatchState = num == 1 ? EMatchState.UserOnOffense : EMatchState.UserOnDefense;
    MatchManager.instance.SetCurrentMatchState(newMatchState);
    bool flag = num != 1 ? !PersistentData.userIsHome : PersistentData.userIsHome;
    MatchManager.instance.homeTeamGetsBallAtHalf = flag;
  }
}
