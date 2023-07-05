// Decompiled with JetBrains decompiler
// Type: ProEra.Game.ScoreClockState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace ProEra.Game
{
  [RuntimeState]
  public static class ScoreClockState
  {
    public static VariableInt PlayClock = new VariableInt(0);
    public static VariableBool PlayClockVisible = new VariableBool(true);
    public static VariableBool PenaltyVisible = new VariableBool(true);
    public static VariableBool DownAndDistanceVisible = new VariableBool(true);
    public static VariableBool PersonnelVisible = new VariableBool(true);
    public static AppEvent TEMP_InitializeScoreClock = new AppEvent();
    public static AppEvent<string> SetDownAndDistance = new AppEvent<string>();
    public static AppEvent<bool> ShowTimeout = new AppEvent<bool>();
    public static AppEvent SetYardLine = new AppEvent();
    public static AppEvent EnableAllTimeouts = new AppEvent();
    public static VariableString GameClockString = new VariableString("");
    public static VariableString HomeDownAndDistance = new VariableString("");
    public static VariableString AwayDownAndDistance = new VariableString("");
    public static VariableString Quarter = new VariableString("");
    public static VariableString Personnel = new VariableString("");
    public static int _minutes = 0;
    public static int _seconds = 0;

    public static bool IsGameClockStringZero() => (string) ScoreClockState.GameClockString == "0:00";

    public static string GetDownAndDistance(bool isHomeTeamOnOffense) => isHomeTeamOnOffense ? ScoreClockState.HomeDownAndDistance.Value : ScoreClockState.AwayDownAndDistance.Value;

    public static void SetGameClock(int minutes, int seconds)
    {
      if (minutes <= 0 && seconds <= 0)
      {
        ScoreClockState._minutes = 0;
        ScoreClockState._seconds = 0;
        ScoreClockState.GameClockString.Value = "0:00";
      }
      else
      {
        ScoreClockState._minutes = minutes;
        ScoreClockState._seconds = seconds;
        string str = seconds > 9 ? minutes.ToString() + ":" + seconds.ToString() : minutes.ToString() + ":0" + seconds.ToString();
        ScoreClockState.GameClockString.Value = str;
      }
    }
  }
}
