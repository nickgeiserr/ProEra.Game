// Decompiled with JetBrains decompiler
// Type: ProEra.Game.PlayInfo
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace ProEra.Game
{
  public static class PlayInfo
  {
    public static float GameClockTimer;
    public static float GameClockLastDown;
    public static float PlayClockTimer;
    public static int Quarter;
    public static bool RunPlayClock;
    public static bool RunGameClock;
    public static bool StopClockAfterPlay;
    public static bool TwoMinWarnDone;
    public static bool ShowTwoMinWarningAfterPlay;
    public static int PlayClockLength;
    public static int DisplaySeconds;
    public static int DisplayMinutes;

    public static event Action OnPlayClockReset;

    public static event Action OnGameClockReset;

    public static event Action<int> OnQuarterEnded;

    public static int GetPlayClockTime() => PlayInfo.PlayClockLength - (int) PlayInfo.PlayClockTimer;

    public static string GetQuarterString()
    {
      switch (PlayInfo.Quarter)
      {
        case 1:
          return "1ST QTR";
        case 2:
          return "2ND QTR";
        case 3:
          return "3RD QTR";
        case 4:
          return "4TH QTR";
        default:
          return "OT";
      }
    }

    public static bool IsGameClockRunning() => PlayInfo.RunGameClock;

    public static bool HasGameClockExpired() => (double) PlayInfo.GameClockTimer >= (double) MatchInfo.GameLength;

    public static void ResetPlayClock()
    {
      PlayInfo.PlayClockTimer = 0.0f;
      Action onPlayClockReset = PlayInfo.OnPlayClockReset;
      if (onPlayClockReset == null)
        return;
      onPlayClockReset();
    }

    public static void ResetGameClock()
    {
      PlayInfo.GameClockTimer = 0.0f;
      PlayInfo.DisplayMinutes = (MatchInfo.GameLength - Mathf.CeilToInt(PlayInfo.GameClockTimer)) / 60 % 60;
      PlayInfo.DisplaySeconds = 0;
      Action onGameClockReset = PlayInfo.OnGameClockReset;
      if (onGameClockReset == null)
        return;
      onGameClockReset();
    }

    public static bool IsFirstQuarter() => PlayInfo.Quarter == 1;

    public static bool IsSecondQuarter() => PlayInfo.Quarter == 2;

    public static bool IsThirdQuarter() => PlayInfo.Quarter == 3;

    public static bool IsFourthQuarter() => PlayInfo.Quarter == 4;

    public static void EndQuarter()
    {
      Action<int> onQuarterEnded = PlayInfo.OnQuarterEnded;
      if (onQuarterEnded != null)
        onQuarterEnded(PlayInfo.Quarter);
      if (PlayInfo.Quarter == 1)
        ++PlayInfo.Quarter;
      if (PlayInfo.Quarter == 2)
        ++PlayInfo.Quarter;
      if (PlayInfo.Quarter != 3)
        return;
      ++PlayInfo.Quarter;
    }
  }
}
