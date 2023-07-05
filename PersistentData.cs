// Decompiled with JetBrains decompiler
// Type: PersistentData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework;
using ProEra.Game;
using UDB;
using UnityEngine;
using Vars;

public class PersistentData : SingletonBehaviour<PersistentData, MonoBehaviour>
{
  [SerializeField]
  private UniformLogoStore _uniformLogoStore;
  public VariableUniform HomeTeamUniform = new VariableUniform(ETeamUniformId.Ravens);
  public VariableUniform AwayTeamUniform = new VariableUniform(ETeamUniformId.Steelers);
  public VariableEnum<ETeamUniformFlags> PlayerSide = new VariableEnum<ETeamUniformFlags>(ETeamUniformFlags.Home);
  [SerializeField]
  private PlayerProfile _playerProfile;
  public System.Action OnUserTeamChanged;
  private int _seasonWeek;
  private Award _gameMvp;
  private Award _offPlayerOfTheGame;
  private Award _defPlayerOfTheGame;
  [SerializeField]
  private int _difficulty;
  [SerializeField]
  private int _weather;
  [SerializeField]
  private int _timeOfDay;
  [SerializeField]
  private int _quarterLength;
  [SerializeField]
  private int _windType;
  [SerializeField]
  private int _offDifficulty;
  [SerializeField]
  private int _defDifficulty;
  private bool _watchingNonUserMatch;
  private int _nonUserMatchTier;
  private int _playoffRound;
  [SerializeField]
  private bool _playerFatigueOn;
  [SerializeField]
  private bool _injuriesOn;
  private bool _userCallsPlays;
  private bool _coachCallsPlays;
  private bool _userControlsPlayers;
  private bool _userControlsQB;
  private bool _is2PMatch;
  [SerializeField]
  private GameMode _gameMode;
  [SerializeField]
  private GameType _gameType;
  [SerializeField]
  private StadiumSet _stadiumSet;
  private Texture2D _fieldTexture;
  private static TeamData userTeamData;
  private static TeamData compTeamData;
  public static string previousScene = "";
  public static string saveSlot = "0";
  public static bool showFranchise;
  public static bool simulateWeek;
  public static bool saveGameStats;
  public static bool musicOn;
  public static string awayRecord;
  public static string homeRecord;
  public static float globalTimeScale;
  public static GameSummary userGameSummary;
  public static GameSummary compGameSummary;
  public static UniformSet homeTeamUniform;
  public static UniformSet awayTeamUniform;
  public UniformFactory uniformFactory;
  private RoutineHandle _bundleLoadingHandle = new RoutineHandle();
  [Header("Testing Scenario")]
  public bool startedFromGameScene;
  public bool scenarioAISimGame;
  public int startFromGameScene_UserTeamIndex;
  public int startFromGameScene_CompTeamIndex;
  public bool scenarioDoCoinToss;
  public bool scenarioDoKickoff;
  public bool scenarioRunningPAT;
  public bool scenarioUserOnOffense = true;
  public int scenarioDown = 1;
  public int scenarioLineOfScrimmageYardline = 20;
  public bool scenarioOwnSideOfField = true;
  public int scenarioFirstDownYardline = 30;
  public int scenarioHomeScore;
  public int scenarioAwayScore;
  public int scenarioQuarter = 1;
  public int scenarioSecondsRemaining = 300;
  public bool scenarioRunGameClock;
  public bool scenarioOffenseGoingNorth = true;
  public bool scenarioForceDefPlay;
  public bool scenarioForceOffPlay;
  public bool scenarioShouldFlipPlay;
  public string scenarioForceOffensePlaybook = "SINGLEBACK";
  public string scenarioForceDefensePlaybook = "FOUR THREE";
  public string scenarioForceOffenseFormation = "Shotgun";
  public string scenarioForceDefenseFormation = "4-3";
  public string scenarioForceOffensePlay = "ACE TE DRAG";
  public string scenarioForceDefensePlay = "BASE MAN";
  [Header("Testing Game Parameters (these can be changed live)")]
  public bool testOffenseCantBeTackled;
  public bool testDefenseCantBeTackled;
  public TestType testOffensiveCatch;
  public TestType testDefensiveCatch;
  public bool offenseWillFumble;
  public bool defenseWillFumble;
  public bool defenseWillStandStill;
  public bool defenseWillNotChaseBall;
  public TestType testAIWillAcceptPenalty;
  public TestType aiOffenseWillPass;
  public bool aiQBWillNotThrow;
  public bool aiOffenseWillAlwaysPunt;
  public bool alwaysCallReadOption;
  public bool alwaysCallPlayAction;
  public TestType offensiveLineWillBlock;
  public bool alwaysGoForTwo;
  public float userTeamPlayerScaleFactor = 1f;
  public float cpuTeamPlayerScaleFactor = 1f;
  public bool ignoreSidelineBoundary;
  public bool alwaysKickFieldGoals;
  public bool fieldGoalsAlwaysMiss;
  public bool alwaysForceKickoff;
  public bool alwaysAttemptOnsideKicks;
  public bool alwaysCallQBSpike;
  public bool alwaysCallQBKneel;
  public bool alwaysDefShowBlitz;
  public bool superSimUser;
  public bool superSimComp;
  public bool practiceGoingNorth = true;
  public TeamDataStore[] allTeamDataStores;
  public static GameType LastPlayedGameType;

  public int IntPlayerSide => (int) this.PlayerSide.Value;

  public int IntHomeTeam => (int) this.HomeTeamUniform.Value;

  public int IntAwayTeam => (int) this.AwayTeamUniform.Value;

  public static ETeamUniformId GetUserUniform() => !PersistentData.userIsHome ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.Value : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.Value;

  public static ETeamUniformId GetCompUniform() => !PersistentData.userIsHome ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.Value : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.Value;

  public static TeamData GetHomeTeamData() => PersistentData.userIsHome ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam();

  public static TeamData GetAwayTeamData() => PersistentData.userIsHome ? PersistentData.GetCompTeam() : PersistentData.GetUserTeam();

  public static int GetHomeTeamIndex() => PersistentData.GetHomeTeamData().TeamIndex;

  public static int GetAwayTeamIndex() => PersistentData.GetAwayTeamData().TeamIndex;

  public static int GetUserTeamIndex() => PersistentData.GetUserTeam().TeamIndex;

  public static int GetCompTeamIndex()
  {
    TeamData compTeam = PersistentData.GetCompTeam();
    return compTeam == null ? 0 : compTeam.TeamIndex;
  }

  public static int[] GetUserTeamRating() => PersistentData.GetUserTeam().GetTeamRatings();

  public static int[] GetCompTeamRating() => PersistentData.GetCompTeam().GetTeamRatings();

  public static void SetUserTeam(TeamData team)
  {
    bool flag = PersistentData.userTeamData != team;
    PersistentData.userTeamData = team;
    Debug.Log((object) ("SET PLAYER TEAMDATA: " + team.teamFile?.ToString() + " " + team.GetName() + " " + team.GetCity()));
    ETeamUniformId teamId = SingletonBehaviour<PersistentData, MonoBehaviour>.instance._uniformLogoStore.GetUniformLogo(team.TeamIndex).teamId;
    if (PersistentData.userIsHome)
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.SetValue(teamId);
    else
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.SetValue(teamId);
    if (!flag)
      return;
    if ((UnityEngine.Object) SingletonBehaviour<PersistentData, MonoBehaviour>.instance._playerProfile != (UnityEngine.Object) null)
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance._playerProfile.RefreshGloves(teamId);
    System.Action onUserTeamChanged = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged;
    if (onUserTeamChanged == null)
      return;
    onUserTeamChanged();
  }

  public static TeamData GetUserTeam() => PersistentData.userTeamData ?? (PersistentData.userTeamData = TeamDataCache.GetTeam(PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex));

  public static void SetCompTeam(TeamData team)
  {
    PersistentData.compTeamData = team;
    Debug.Log((object) ("SET COMP TEAMDATA: " + team.teamFile?.ToString() + " " + team.GetName() + " " + team.GetCity()));
    ETeamUniformId teamId = SingletonBehaviour<PersistentData, MonoBehaviour>.instance._uniformLogoStore.GetUniformLogo(team.TeamIndex).teamId;
    if (PersistentData.userIsHome)
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.SetValue(teamId);
    else
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.SetValue(teamId);
  }

  public static void SetCompTeam(int teamIndex)
  {
    PersistentData.compTeamData = TeamDataCache.GetTeam(teamIndex);
    Debug.Log((object) ("SET COMP TEAMDATA: " + PersistentData.compTeamData.teamFile?.ToString() + " " + PersistentData.compTeamData.GetName() + " " + PersistentData.compTeamData.GetCity()));
    ETeamUniformId teamId = SingletonBehaviour<PersistentData, MonoBehaviour>.instance._uniformLogoStore.GetUniformLogo(PersistentData.compTeamData.TeamIndex).teamId;
    if (PersistentData.userIsHome)
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.SetValue(teamId);
    else
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.SetValue(teamId);
  }

  public static TeamData GetCompTeam()
  {
    if (PersistentData.compTeamData == null)
      PersistentData.SetCompTeam(TeamDataCache.GetTeam(0));
    return PersistentData.compTeamData;
  }

  public static UniformSet GetUserUniformSet() => PersistentData.userIsHome ? PersistentData.homeTeamUniform : PersistentData.awayTeamUniform;

  public static UniformSet GetCompUniformSet() => PersistentData.userIsHome ? PersistentData.awayTeamUniform : PersistentData.homeTeamUniform;

  public static Color GetHomeBackgroundColor() => PersistentData.GetHomeTeamData().GetPrimaryColor();

  public static Color GetAwayBackgroundColor() => PersistentData.GetAwayTeamData().GetPrimaryColor();

  public static Sprite GetHomeLargeLogo() => PersistentData.GetHomeTeamData().GetLargeLogo();

  public static Sprite GetHomeMediumLogo() => PersistentData.GetHomeTeamData().GetMediumLogo();

  public static Sprite GetHomeSmallLogo() => PersistentData.GetHomeTeamData().GetSmallLogo();

  public static Sprite GetHomeTinyLogo() => PersistentData.GetHomeTeamData().GetTinyLogo();

  public static Sprite GetAwayLargeLogo() => PersistentData.GetAwayTeamData().GetLargeLogo();

  public static Sprite GetAwayMediumLogo() => PersistentData.GetAwayTeamData().GetMediumLogo();

  public static Sprite GetAwaySmallLogo() => PersistentData.GetAwayTeamData().GetSmallLogo();

  public static Sprite GetAwayTinyLogo() => PersistentData.GetAwayTeamData().GetTinyLogo();

  public static string GetHomeTeamCity() => PersistentData.GetHomeTeamData().GetCity();

  public static string GetAwayTeamCity() => PersistentData.GetAwayTeamData().GetCity();

  public static string GetHomeTeamName()
  {
    TeamData homeTeamData = PersistentData.GetHomeTeamData();
    return homeTeamData == null ? "Ravens" : homeTeamData.GetName();
  }

  public static string GetAwayTeamName() => PersistentData.GetAwayTeamData().GetName();

  public static string GetHomeTeamAbbreviation() => PersistentData.GetHomeTeamData().GetAbbreviation();

  public static string GetAwayTeamAbbreviation() => PersistentData.GetAwayTeamData().GetAbbreviation();

  public static TeamData GetOffensiveTeamData() => global::Game.IsPlayerOneOnOffense ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam();

  public static int GetOffensiveTeamIndex() => PersistentData.GetOffensiveTeamData().TeamIndex;

  public static TeamData GetDefensiveTeamData() => global::Game.IsPlayerOneOnDefense ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam();

  public static int GetDefensiveTeamIndex() => PersistentData.GetDefensiveTeamData().TeamIndex;

  public UniformLogoStore GetUniformLogoStore() => this._uniformLogoStore;

  public static int seasonWeek
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._seasonWeek;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._seasonWeek = value;
  }

  public static Award gameMvp
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._gameMvp;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._gameMvp = value;
  }

  public static Award offPlayerOfTheGame
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._offPlayerOfTheGame;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._offPlayerOfTheGame = value;
  }

  public static Award defPlayerOfTheGame
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._defPlayerOfTheGame;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._defPlayerOfTheGame = value;
  }

  public static int difficulty
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._difficulty;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._difficulty = value;
  }

  public static int weather
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._weather;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._weather = value;
  }

  public static int timeOfDay
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._timeOfDay;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._timeOfDay = value;
  }

  public static int quarterLength
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._quarterLength;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._quarterLength = value;
  }

  public static int windType
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._windType;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._windType = value;
  }

  public static int offDifficulty
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._offDifficulty;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._offDifficulty = Mathf.Clamp(value, -5, 10);
  }

  public static int defDifficulty
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._defDifficulty;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._defDifficulty = Mathf.Clamp(value, -5, 10);
  }

  public static bool userIsHome
  {
    get => (bool) Globals.UserIsHome;
    set
    {
      Globals.UserIsHome.Value = value;
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.SetValue(value ? ETeamUniformFlags.Home : ETeamUniformFlags.Away);
    }
  }

  public static bool watchingNonUserMatch
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._watchingNonUserMatch;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._watchingNonUserMatch = value;
  }

  public static int nonUserMatchTier
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._nonUserMatchTier;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._nonUserMatchTier = value;
  }

  public static int playoffRound
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._playoffRound;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._playoffRound = value;
  }

  public static bool playerFatigueOn
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._playerFatigueOn;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._playerFatigueOn = value;
  }

  public static bool injuriesOn
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._injuriesOn;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._injuriesOn = value;
  }

  public static bool UserCallsPlays
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._userCallsPlays;
    private set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._userCallsPlays = value;
  }

  public static bool CoachCallsPlays
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._coachCallsPlays;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._coachCallsPlays = value;
  }

  public static bool UserControlsPlayers
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._userControlsPlayers;
    private set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._userControlsPlayers = value;
  }

  public static bool UserControlsQB
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._userControlsQB;
    private set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._userControlsQB = value;
  }

  public static bool Is2PMatch
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._is2PMatch;
    private set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._is2PMatch = value;
  }

  public static GameMode GameMode
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._gameMode;
    private set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._gameMode = value;
  }

  public static GameType gameType
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._gameType;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._gameType = value;
  }

  public static StadiumSet stadiumSet
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._stadiumSet;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._stadiumSet = value;
  }

  public static Texture2D FieldTexture
  {
    get => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._fieldTexture;
    set => SingletonBehaviour<PersistentData, MonoBehaviour>.instance._fieldTexture = value;
  }

  public TeamDataStore GetTeamDataStoreByName(string TeamName)
  {
    for (int index = 0; index < this.allTeamDataStores.Length; ++index)
    {
      if (this.allTeamDataStores[index].TeamName == TeamName)
        return this.allTeamDataStores[index];
    }
    return (TeamDataStore) null;
  }

  protected override void OnInstanceInit()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    if (PersistentSingleton<SaveManager>.Exist() && PersistentSingleton<SaveManager>.Instance.gameSettings != null)
      PersistentData.musicOn = PersistentSingleton<SaveManager>.Instance.gameSettings.MusicOn;
    PersistentData.globalTimeScale = (float) GameSettings.TimeScale;
    Time.timeScale = PersistentData.globalTimeScale;
    this.uniformFactory = new UniformFactory();
    if (SingletonBehaviour<PersistentData, MonoBehaviour>.Exists())
      return;
    PersistentData.simulateWeek = false;
  }

  public void UnloadAssetBundle(string assetBundle, bool unloadAllObjects)
  {
  }

  public static void SetGameMode(GameMode mode)
  {
    PersistentData.GameMode = mode;
    switch (mode)
    {
      case GameMode.PlayerVsAI:
        PersistentData.Is2PMatch = false;
        PersistentData.UserCallsPlays = true;
        PersistentData.UserControlsPlayers = false;
        PersistentData.UserControlsQB = true;
        break;
      case GameMode.PlayerVsPlayer:
        PersistentData.Is2PMatch = true;
        PersistentData.UserCallsPlays = true;
        PersistentData.UserControlsPlayers = true;
        break;
      case GameMode.PlayerVsPlayerCoach:
        PersistentData.Is2PMatch = true;
        PersistentData.UserCallsPlays = true;
        PersistentData.UserControlsPlayers = false;
        break;
      case GameMode.Coach:
        PersistentData.Is2PMatch = false;
        PersistentData.UserCallsPlays = true;
        PersistentData.UserControlsPlayers = false;
        PersistentData.UserControlsQB = false;
        break;
      case GameMode.Spectate:
        PersistentData.Is2PMatch = false;
        PersistentData.UserCallsPlays = false;
        PersistentData.UserControlsPlayers = false;
        break;
    }
  }

  public void ClearUniform(UniformAssetType type)
  {
    this.uniformFactory.ClearSavedJerseys();
    this.uniformFactory.ClearUniformSets();
    if (type == UniformAssetType.USER && PersistentData.GetUserUniformSet() != null)
    {
      PersistentData.GetUserUniformSet().ClearAllTextures();
    }
    else
    {
      if (type != UniformAssetType.COMP || PersistentData.GetCompUniformSet() == null)
        return;
      PersistentData.GetCompUniformSet().ClearAllTextures();
    }
  }
}
