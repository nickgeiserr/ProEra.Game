// Decompiled with JetBrains decompiler
// Type: StadiumSetDatabase
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class StadiumSetDatabase : MonoBehaviour
{
  private static List<StadiumSet> stadiumList;
  private static bool isDatabaseLoaded;
  private static int currentIndex;

  private static void ValidateDatabase()
  {
    if (StadiumSetDatabase.stadiumList == null)
      StadiumSetDatabase.stadiumList = new List<StadiumSet>();
    if (StadiumSetDatabase.isDatabaseLoaded)
      return;
    StadiumSetDatabase.LoadDatabase();
  }

  public static void LoadDatabase()
  {
    if (StadiumSetDatabase.isDatabaseLoaded)
      return;
    StadiumSetDatabase.isDatabaseLoaded = true;
    StadiumSetDatabase.LoadDatabaseForce();
  }

  public static void LoadDatabaseForce()
  {
    StadiumSetDatabase.ValidateDatabase();
    StadiumSetDatabase.currentIndex = 0;
    foreach (StadiumSet stadiumSet in Resources.LoadAll<StadiumSet>("Stadium Sets"))
    {
      if (stadiumSet.useInStandaloneBuilds)
        StadiumSetDatabase.stadiumList.Add(stadiumSet);
    }
  }

  public static void SetCurrentStadiumIndex(int index) => StadiumSetDatabase.currentIndex = index;

  public static void SetStadiumForHomeCity(string teamCity)
  {
    StadiumSetDatabase.ValidateDatabase();
    for (int index = 0; index < StadiumSetDatabase.stadiumList.Count; ++index)
    {
      if (StadiumSetDatabase.stadiumList[index].homeTeamStadium.ToUpper().Equals(teamCity.ToUpper()))
      {
        StadiumSetDatabase.SetCurrentStadiumIndex(index);
        return;
      }
    }
    StadiumSetDatabase.SetCurrentStadiumIndex(0);
  }

  public static StadiumSet GetStadiumData()
  {
    StadiumSetDatabase.ValidateDatabase();
    return StadiumSetDatabase.stadiumList[StadiumSetDatabase.currentIndex];
  }

  public static void SetNextStadium()
  {
    if (StadiumSetDatabase.currentIndex < StadiumSetDatabase.stadiumList.Count - 1)
      ++StadiumSetDatabase.currentIndex;
    else
      StadiumSetDatabase.currentIndex = 0;
  }

  public static void SetPrevStadium()
  {
    if (StadiumSetDatabase.currentIndex > 0)
      --StadiumSetDatabase.currentIndex;
    else
      StadiumSetDatabase.currentIndex = StadiumSetDatabase.stadiumList.Count - 1;
  }

  public static int GetTotalStadiums()
  {
    StadiumSetDatabase.ValidateDatabase();
    return StadiumSetDatabase.stadiumList.Count;
  }

  public static StadiumSet GetRandomStadium()
  {
    StadiumSetDatabase.ValidateDatabase();
    return StadiumSetDatabase.stadiumList[Random.Range(0, StadiumSetDatabase.stadiumList.Count - 1)];
  }

  public static void ClearStadiumDatabase()
  {
    StadiumSetDatabase.isDatabaseLoaded = false;
    StadiumSetDatabase.stadiumList.Clear();
  }
}
