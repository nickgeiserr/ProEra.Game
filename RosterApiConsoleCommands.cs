// Decompiled with JetBrains decompiler
// Type: RosterApiConsoleCommands
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using IngameDebugConsole;
using UnityEngine;

public class RosterApiConsoleCommands : MonoBehaviour
{
  [ConsoleMethod("DebugGetRoster", "Gets a roster with the specified city abbreviation", new string[] {})]
  public static void GetRoster(string cityAbbreviation)
  {
    RosterApiConsoleCommands.ValidateInstance();
    PersistentSingleton<RosterApi>.Instance.GetRoster(cityAbbreviation);
  }

  [ConsoleMethod("DebugUpdateRosters", "Updates all rosters to latest version", new string[] {})]
  public static void UpdateRosters()
  {
    RosterApiConsoleCommands.ValidateInstance();
    int[] rosterVersions = new int[32];
    for (int index = 0; index < rosterVersions.Length; ++index)
      rosterVersions[index] = index % 2 == 0 ? -1 : int.MaxValue;
    PersistentSingleton<RosterApi>.Instance.UpdateRosters(rosterVersions);
  }

  private static void ValidateInstance()
  {
  }
}
