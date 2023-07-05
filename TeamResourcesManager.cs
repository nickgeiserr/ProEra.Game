// Decompiled with JetBrains decompiler
// Type: TeamResourcesManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using ProEra.Game.Sources.TeamData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TeamResourcesManager : PersistentSingleton<TeamResourcesManager>
{
  [SerializeField]
  private RosterDataObject[] _rosters;
  [SerializeField]
  private DefaultPlayerDataObject[] _defaultPlayerData;
  [SerializeField]
  private CoachingStaffDataObject[] _coachingStaffs;
  [SerializeField]
  private ScheduleDataObject[] _scheduleData;
  [SerializeField]
  private TeamDataObject[] _teamData;
  [SerializeField]
  private PlayCallingDataObject[] _playCallingData;
  [SerializeField]
  private StadiumFieldLogoStore _stadiumFieldLogoData;
  public static string[] BASE_TEAM_FOLDERS = new string[32]
  {
    "Arizona",
    "Atlanta",
    "Baltimore",
    "Boston",
    "Buffalo",
    "Carolina",
    "Chicago",
    "Cincinnati",
    "Cleveland",
    "Dallas",
    "Denver",
    "Detroit",
    "Green Bay",
    "Houston",
    "Indianapolis",
    "Jacksonville",
    "Kansas City",
    "Las Vegas",
    "Los Angeles C",
    "Los Angeles R",
    "Miami",
    "Minnesota",
    "New Orleans",
    "New York G",
    "New York J",
    "Philadelphia",
    "Pittsburgh",
    "San Francisco",
    "Seattle",
    "Tampa Bay",
    "Tennessee",
    "Washington"
  };
  public static string[] UPPERCASE_BASE_TEAM_FOLDERS = new string[32]
  {
    "ARIZONA",
    "ATLANTA",
    "BALTIMORE",
    "BOSTON",
    "BUFFALO",
    "CAROLINA",
    "CHICAGO",
    "CINCINNATI",
    "CLEVELAND",
    "DALLAS",
    "DENVER",
    "DETROIT",
    "GREEN BAY",
    "HOUSTON",
    "INDIANAPOLIS",
    "JACKSONVILLE",
    "KANSAS CITY",
    "LAS VEGAS",
    "LOS ANGELES C",
    "LOS ANGELES R",
    "MIAMI",
    "MINNESOTA",
    "NEW ORLEANS",
    "NEW YORK G",
    "NEW YORK J",
    "PHILADELPHIA",
    "PITTSBURGH",
    "SAN FRANCISCO",
    "SEATTLE",
    "TAMPA BAY",
    "TENNESSEE",
    "WASHINGTON"
  };

  public TeamDataObject[] GetTeamDataObjects() => this._teamData;

  public PlayCallingDataObject[] GetPlayCallingData() => this._playCallingData;

  public RosterDataObject[] Rosters => this._rosters;

  public DefaultPlayerDataObject[] DefaultPlayerData => this._defaultPlayerData;

  public CoachingStaffDataObject[] CoachingStaffs => this._coachingStaffs;

  public ScheduleDataObject[] ScheduleData => this._scheduleData;

  public TeamDataObject[] TeamData => this._teamData;

  protected void Start()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    this.ValidateInspectorBinding();
    PersistentSingleton<SaveManager>.Instance.rosterData.rosters = ((IEnumerable<RosterDataObject>) this._rosters).Select<RosterDataObject, RosterFileData>((Func<RosterDataObject, RosterFileData>) (x => x.RosterFileData)).ToArray<RosterFileData>();
  }

  private void ValidateInspectorBinding()
  {
  }

  private static Texture2D LoadGraphic(int teamIndex, string filename) => AddressablesData.instance.LoadAssetSync<Texture2D>(AddressablesData.CorrectingAssetKey("teams/team_" + teamIndex.ToString()), filename);

  private static Sprite LoadSprite(int teamIndex, string filename) => AddressablesData.instance.LoadAssetSync<Sprite>(AddressablesData.CorrectingAssetKey("teams/team_" + teamIndex.ToString()), filename);

  public static Texture2D LoadTeamGraphic(int teamIndex, TeamGraphicType type)
  {
    string filename = "";
    switch (type)
    {
      case TeamGraphicType.ENDZONE:
        filename = "endzone";
        break;
      case TeamGraphicType.MIDFIELD:
        filename = "midfield";
        break;
      case TeamGraphicType.SMALL_LOGO:
        filename = "small_logo";
        break;
      case TeamGraphicType.LARGE_LOGO:
        filename = "large_logo";
        break;
    }
    return TeamResourcesManager.LoadGraphic(teamIndex, filename);
  }

  public TeamDataObject GetTeamData(int index) => index < 0 || this._teamData.Length <= index ? (TeamDataObject) null : this._teamData[index];

  public static Sprite LoadLogoSprite(int teamIndex, TeamGraphicType type)
  {
    string filename = "";
    switch (type)
    {
      case TeamGraphicType.SMALL_LOGO:
        filename = "small_logo";
        break;
      case TeamGraphicType.LARGE_LOGO:
        filename = "large_logo_no_word_art";
        break;
      case TeamGraphicType.TINY_LOGO:
        filename = "tiny_logo";
        break;
      case TeamGraphicType.MEDIUM_LOGO:
        filename = "medium_logo";
        break;
    }
    return TeamResourcesManager.LoadSprite(teamIndex, filename);
  }

  public static Texture2D[] LoadFanTextures(int teamIndex)
  {
    Texture2D[] texture2DArray = new Texture2D[2];
    for (int index = 0; index < 2; ++index)
    {
      string filename = "fan" + (index + 1).ToString();
      texture2DArray[index] = TeamResourcesManager.LoadGraphic(teamIndex, filename);
    }
    return texture2DArray;
  }

  internal TeamFile LoadTeamFile(int teamIndex)
  {
    TeamFile teamFile = new TeamFile();
    TeamDataMap dataMap = this._teamData[teamIndex].DataMap;
    List<string> keysList = dataMap.keysList;
    if (this._teamData[teamIndex].DataKeys.Count > 0 && this._teamData[teamIndex].DataKeys.Count == this._teamData[teamIndex].DataValues.Count)
    {
      for (int index = 0; index < this._teamData[teamIndex].DataKeys.Count; ++index)
        teamFile.AddNameValuePair(this._teamData[teamIndex].DataKeys[index], this._teamData[teamIndex].DataValues[index]);
    }
    else
    {
      foreach (string str in keysList)
        teamFile.AddNameValuePair(AssetManager.TrimString(str), AssetManager.TrimString(dataMap.GetValue(str)));
    }
    return teamFile;
  }

  internal TeamPlayCalling LoadTeamPlayCalling(int teamIndex)
  {
    TeamPlayCalling teamPlayCalling = new TeamPlayCalling();
    PlayCallingDataMap dataMap = this._playCallingData[teamIndex].DataMap;
    List<string> keysList = dataMap.keysList;
    if (this._playCallingData[teamIndex].DataKeys.Count > 0 && this._playCallingData[teamIndex].DataKeys.Count == this._playCallingData[teamIndex].DataValues.Count)
    {
      for (int index = 0; index < this._playCallingData[teamIndex].DataKeys.Count; ++index)
        teamPlayCalling.AddNameValuePair(this._playCallingData[teamIndex].DataKeys[index], this._playCallingData[teamIndex].DataValues[index]);
    }
    else
    {
      foreach (string str in keysList)
        teamPlayCalling.AddNameValuePair(AssetManager.TrimString(str), dataMap.GetValue(str));
    }
    return teamPlayCalling;
  }

  public static string LoadTeamTextFileFromResources(int teamIndex, string textFileName) => Resources.Load<TextAsset>(teamIndex.ToString() + "_" + textFileName).text;

  private static string LoadFreeAgentTextFile(string filename) => AddressablesData.instance.LoadAssetSync<TextAsset>("freeagentclasses", filename).text;

  internal static RosterData LoadTeamRoster(int teamIndex) => PersistentSingleton<SaveManager>.Instance.rosterData.rosters[teamIndex].ToRosterData();

  internal static CoachData[] LoadCoachingStaff(int teamIndex) => PersistentSingleton<TeamResourcesManager>.Instance.CoachingStaffs[teamIndex].ToCoachData();

  public static CoachData[] GenerateCoachClass(Dictionary<CoachPositions, int> coachDistribution)
  {
    List<CoachData> coachDataList = new List<CoachData>();
    foreach (KeyValuePair<CoachPositions, int> keyValuePair in coachDistribution)
    {
      for (int index = keyValuePair.Value; index > 0; --index)
        coachDataList.Add(CoachCreator.CreateCoach(keyValuePair.Key));
    }
    return coachDataList.ToArray();
  }

  public static string[] LoadPlayerFirstNamesBank() => AssetManager.SplitAndTrimTextFile(AddressablesData.instance.LoadAssetSync<TextAsset>("players", "playerFirstNames").text);

  public static string[] LoadPlayerLastNameBank() => AssetManager.SplitAndTrimTextFile(AddressablesData.instance.LoadAssetSync<TextAsset>("players", "playerLastNames").text);

  public static int GetCountOfTeams() => TeamResourcesManager.BASE_TEAM_FOLDERS.Length;

  public static string GetTeamAt(int teamIndex) => TeamResourcesManager.BASE_TEAM_FOLDERS[teamIndex];

  internal static Dictionary<string, int> LoadDefaultPlayers(int teamIndex) => PersistentSingleton<TeamResourcesManager>.Instance.DefaultPlayerData[teamIndex].DefaultPlayerData.ToDictionary();

  internal static TeamConferenceData[] LoadTeamConferenceAssignment()
  {
    TextAsset message = AddressablesData.instance.LoadAssetSync<TextAsset>("Assets/TeamsData/Teams/TeamConferences");
    if (!((UnityEngine.Object) message == (UnityEngine.Object) null))
      return TeamAssetManager.ParseConferencesCSVFile(message.text);
    Debug.LogError((object) message);
    return (TeamConferenceData[]) null;
  }

  internal static Dictionary<string, List<ScheduleMatchupData>> LoadSeasonSchedule(int seasonYear)
  {
    int num = ((IEnumerable<ScheduleDataObject>) PersistentSingleton<TeamResourcesManager>.Instance.ScheduleData).Count<ScheduleDataObject>();
    int index = seasonYear % num;
    Dictionary<string, List<ScheduleMatchupData>> dictionary = new Dictionary<string, List<ScheduleMatchupData>>();
    foreach (ProEra.Game.Sources.TeamData.ScheduleData schedule in PersistentSingleton<TeamResourcesManager>.Instance.ScheduleData[index].Schedules)
      dictionary[schedule.team] = ((IEnumerable<ScheduleMatchupData>) schedule.matchups).ToList<ScheduleMatchupData>();
    return dictionary;
  }

  public static string LoadTeamTextFile(int teamIndex, string textFileName, string extFile)
  {
    Debug.Log((object) ("<b>teams/team_" + teamIndex.ToString() + "/" + textFileName + "</b>"));
    TextAsset textAsset = AddressablesData.instance.LoadAssetSync<TextAsset>("Assets/TeamsData/Teams/Team_" + teamIndex.ToString() + "/" + textFileName + "." + extFile);
    if ((UnityEngine.Object) textAsset == (UnityEngine.Object) null)
      Debug.LogError((object) textFileName);
    return textAsset.text;
  }

  public static Texture2D GetFieldLogo(ETeamUniformId teamID)
  {
    if (PersistentSingleton<TeamResourcesManager>.Exist())
    {
      AssetReference fieldLogoAddress = PersistentSingleton<TeamResourcesManager>.Instance._stadiumFieldLogoData.GetFieldLogoAddress(teamID);
      if (fieldLogoAddress != null)
        return AddressablesData.LoadTextureAsync(fieldLogoAddress, new Vector2(2048f, 512f), TextureFormat.DXT5, new CacheParams(false)).Value;
    }
    return (Texture2D) null;
  }
}
