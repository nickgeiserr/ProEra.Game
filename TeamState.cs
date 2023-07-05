// Decompiled with JetBrains decompiler
// Type: TeamState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using UnityEngine;

public static class TeamState
{
  public static Sprite GetSmallLogo(bool isUserTeam) => (!isUserTeam ? ((bool) Globals.UserIsHome ? PersistentData.GetCompTeam() : PersistentData.GetUserTeam()) : ((bool) Globals.UserIsHome ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam())).GetSmallLogo();

  public static Sprite GetHomeMediumLogo() => ((bool) Globals.UserIsHome ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam()).GetMediumLogo();

  public static Sprite GetAwayMediumLogo() => ((bool) Globals.UserIsHome ? PersistentData.GetCompTeam() : PersistentData.GetUserTeam()).GetMediumLogo();

  public static Color GetBackgroundColor(bool isUserTeam) => (!isUserTeam ? ((bool) Globals.UserIsHome ? PersistentData.GetCompTeam() : PersistentData.GetUserTeam()) : ((bool) Globals.UserIsHome ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam())).GetPrimaryColor();

  public static Color GetHomeBackgroundColor() => PersistentData.GetUserTeam().GetPrimaryColor();

  public static Color GetAwayBackgroundColor() => PersistentData.GetCompTeam().GetPrimaryColor();

  public static string GetHomeTeamAbbreviation() => ((bool) Globals.UserIsHome ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam()).GetAbbreviation();

  public static string GetAwayTeamAbbreviation() => ((bool) Globals.UserIsHome ? PersistentData.GetCompTeam() : PersistentData.GetUserTeam()).GetAbbreviation();

  public static TeamData GetTeam(Player p)
  {
    if (p == Player.One)
      return PersistentData.GetUserTeam();
    return PersistentData.GetCompTeam();
  }
}
