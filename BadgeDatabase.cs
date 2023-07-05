// Decompiled with JetBrains decompiler
// Type: BadgeDatabase
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections.Generic;
using UnityEngine;

public class BadgeDatabase : MonoBehaviour
{
  private static Dictionary<int, CoachBadge> coachBadgeDatabase;
  private static Dictionary<int, PlayerBadge> playerBadgeDatabase;
  private static bool isCoachDatabaseLoaded;
  private static bool isPlayerDatabaseLoaded;

  private static void ValidateCoachDatabase()
  {
    if (BadgeDatabase.coachBadgeDatabase == null)
      BadgeDatabase.coachBadgeDatabase = new Dictionary<int, CoachBadge>();
    if (BadgeDatabase.isCoachDatabaseLoaded)
      return;
    BadgeDatabase.LoadCoachDatabase();
  }

  private static void LoadCoachDatabase()
  {
    if (BadgeDatabase.isCoachDatabaseLoaded)
      return;
    BadgeDatabase.isCoachDatabaseLoaded = true;
    BadgeDatabase.LoadCoachDatabaseForce();
  }

  private static void LoadCoachDatabaseForce()
  {
    CoachBadge[] coachBadgeArray = AddressablesData.instance.LoadAssetsSync<CoachBadge>("badges");
    for (int index = 0; index < coachBadgeArray.Length; ++index)
      BadgeDatabase.coachBadgeDatabase.Add(coachBadgeArray[index].badgeID, coachBadgeArray[index]);
  }

  public static CoachBadge GetCoachBadge(int badgeID)
  {
    BadgeDatabase.ValidateCoachDatabase();
    if (badgeID == -1)
      return (CoachBadge) null;
    if (BadgeDatabase.coachBadgeDatabase.ContainsKey(badgeID))
      return BadgeDatabase.coachBadgeDatabase[badgeID];
    Debug.Log((object) ("No coach badge was found in the database with badgeID: " + badgeID.ToString()));
    return (CoachBadge) null;
  }

  private static void ValidatePlayerDatabase()
  {
    if (BadgeDatabase.playerBadgeDatabase == null)
      BadgeDatabase.playerBadgeDatabase = new Dictionary<int, PlayerBadge>();
    if (BadgeDatabase.isPlayerDatabaseLoaded)
      return;
    BadgeDatabase.LoadPlayerDatabase();
  }

  private static void LoadPlayerDatabase()
  {
    if (BadgeDatabase.isPlayerDatabaseLoaded)
      return;
    BadgeDatabase.isPlayerDatabaseLoaded = true;
    BadgeDatabase.LoadPlayerDatabaseForce();
  }

  private static void LoadPlayerDatabaseForce()
  {
    PlayerBadge[] playerBadgeArray = Resources.LoadAll<PlayerBadge>("Player Badges");
    for (int index = 0; index < playerBadgeArray.Length; ++index)
      BadgeDatabase.playerBadgeDatabase.Add(playerBadgeArray[index].badgeID, playerBadgeArray[index]);
  }

  public static PlayerBadge GetPlayerBadge(int badgeID)
  {
    BadgeDatabase.ValidatePlayerDatabase();
    if (BadgeDatabase.playerBadgeDatabase.ContainsKey(badgeID))
      return BadgeDatabase.playerBadgeDatabase[badgeID];
    Debug.Log((object) ("No player badge was found in the database with badgeID: " + badgeID.ToString()));
    return (PlayerBadge) null;
  }
}
