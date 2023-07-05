// Decompiled with JetBrains decompiler
// Type: SaveManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using ProEra.Game;
using ProEra.Game.Achievements;
using SaveSystem;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TB12;
using UnityEngine;
using UnityEngine.Events;

public class SaveManager : PersistentSingleton<SaveManager>, IBootSync
{
  private string saveVersionFile = "SaveVersion";
  public static UnityAction OnInitialized;
  public static bool bIsInitialized;
  private readonly Queue<ISaveSync> _saveQueue = new Queue<ISaveSync>();
  private readonly Queue<ISaveSync> _loadQueue = new Queue<ISaveSync>();
  public SaveIOSettings SaveIOSettings;
  [HideInInspector]
  private Dictionary<string, TeamSeasonData> teamSeasonDatas = new Dictionary<string, TeamSeasonData>();
  [HideInInspector]
  public Save_GameSettings gameSettings;
  [HideInInspector]
  public Save_ExhibitionSettings exhibitionSettings;
  [HideInInspector]
  public Save_MiniCamp miniCamp;
  [HideInInspector]
  public Save_TwoMD twoMinuteDrill;
  [HideInInspector]
  public SGD_SeasonModeData seasonModeData;
  [HideInInspector]
  public SaveKeycloakUserData keycloakUserData;
  [HideInInspector]
  public ProfileProgress profileProgress;
  [HideInInspector]
  public SaveAchievements achievements;
  [HideInInspector]
  public UserCareerStats.SaveData careerStats;
  [HideInInspector]
  public RosterSaveData rosterData;
  private bool bSeasonDataExisted;
  [SerializeField]
  private PlayerProfile _pProfile;
  [SerializeField]
  private AchievementData _achievementData;
  [SerializeField]
  private TeamBallMatStore _teamBallMaterialStore;
  [SerializeField]
  private UniformStore _uniformStore;
  [SerializeField]
  private UniformLogoStore _uniformLogoStore;
  [Space]
  [SerializeField]
  private CharacterCustomizationStore _characterCustomizationFemale;
  [SerializeField]
  private CharacterCustomizationStore _characterCustomizationMale;
  private readonly LinksHandler _linksHandler = new LinksHandler();
  private readonly CancellationTokenSource _saveTokenSource = new CancellationTokenSource();
  private readonly CancellationTokenSource _loadTokenSource = new CancellationTokenSource();
  private bool bBusySaving;
  private bool bBusyLoading;

  private string saveDataPath => SaveIO.DefaultFolderPath;

  private CancellationToken _saveToken => this._saveTokenSource.Token;

  private CancellationToken _loadToken => this._loadTokenSource.Token;

  public void Init() => this.StartSaveManagerInitialization();

  protected void StartSaveManagerInitialization() => this.InitializingSaveManager().ConfigureAwait(false);

  protected async Task InitializingSaveManager()
  {
    await this.ValidateSaveDataVersion(SaveIO.gameVersion);
    this.CheckSaveQueue(this.SaveIOSettings.SaveQueueHeartBeatInMSeconds, this._saveToken);
    this.CheckLoadQueue(this.SaveIOSettings.LoadQueueHeartBeatInMSeconds, this._loadToken);
    this.CreateSaveData();
  }

  private void LinkSaveSystem() => this._linksHandler.SetLinks(new List<EventHandle>()
  {
    AppEvents.CreateOrReplaceAllSaveData.Link(new System.Action(this.HandleCreateOrReplaceAllSaveData)),
    AppEvents.ValidateSaveDirectory.Link(new System.Action(this.HandleValidateSaveDirectory)),
    AppEvents.SaveAllSaveData.Link(new System.Action(this.HandleSaveAll)),
    AppEvents.LoadAllSaveData.Link(new System.Action(this.HandleLoadAll)),
    AppEvents.SaveGameSettings.Link(new System.Action(this.AddToSaveQueue_GameSettings)),
    AppEvents.SaveExhibitionSettings.Link(new System.Action(this.AddToSaveQueue_ExhibitionSettings)),
    AppEvents.SaveMiniCamp.Link(new System.Action(this.AddToSaveQueue_MiniCamp)),
    AppEvents.SaveTwoMinuteDrill.Link(new System.Action(this.AddToSaveQueue_TwoMinuteDrill)),
    AppEvents.SavePlayerCustomization.Link(new System.Action(this.AddToSaveQueue_PlayerProfileCustomization)),
    AppEvents.SaveSeasonMode.Link(new System.Action(this.AddToSaveQueue_SeasonModeData)),
    AppEvents.SaveProfileProgress.Link(new System.Action(this.AddToSaveQueue_ProfileProgress)),
    AppEvents.SaveKeycloak.Link(new System.Action(this.AddToSaveQueue_keycloakUserData)),
    AppEvents.SaveAchievements.Link(new System.Action(this.AddToSaveQueue_Achievements)),
    AppEvents.SaveCareerStats.Link(new System.Action(this.AddToSaveQueue_CareerStats)),
    AppEvents.SaveRosters.Link(new System.Action(this.AddToSaveQueue_Rosters))
  });

  private void AddToSaveQueue_GameSettings() => this.AddToSaveQueue((ISaveSync) this.gameSettings);

  private void AddToSaveQueue_ExhibitionSettings() => this.AddToSaveQueue((ISaveSync) this.exhibitionSettings);

  private void AddToSaveQueue_MiniCamp() => this.AddToSaveQueue((ISaveSync) this.miniCamp);

  private void AddToSaveQueue_TwoMinuteDrill() => this.AddToSaveQueue((ISaveSync) this.twoMinuteDrill);

  private void AddToSaveQueue_PlayerProfileCustomization() => this.AddToSaveQueue((ISaveSync) this._pProfile.Customization);

  private void AddToSaveQueue_SeasonModeData()
  {
    this.AddToSaveQueue((ISaveSync) this.careerStats);
    this.AddToSaveQueue((ISaveSync) this.seasonModeData);
  }

  private void AddToSaveQueue_keycloakUserData() => this.AddToSaveQueue((ISaveSync) this.keycloakUserData);

  private void AddToSaveQueue_ProfileProgress() => this.AddToSaveQueue((ISaveSync) this.profileProgress);

  private void AddToSaveQueue_Achievements() => this.AddToSaveQueue((ISaveSync) this.achievements);

  private void AddToSaveQueue_CareerStats() => this.AddToSaveQueue((ISaveSync) this.careerStats);

  private void AddToSaveQueue_Rosters() => this.AddToSaveQueue((ISaveSync) this.rosterData);

  public SGD_SeasonModeData CreateNewSeasonModeData()
  {
    this.seasonModeData = new SGD_SeasonModeData();
    return this.seasonModeData;
  }

  public bool SeasonModeDataExists() => this.bSeasonDataExisted;

  public void MarkSeasonDataAsExisting() => this.bSeasonDataExisted = true;

  public override void OnDestroy()
  {
    this._saveTokenSource.Cancel();
    this._loadTokenSource.Cancel();
    base.OnDestroy();
  }

  private async void CheckSaveQueue(int heartBeat, CancellationToken token)
  {
    while (!token.IsCancellationRequested)
    {
      if (this._saveQueue.Count != 0 && !this.bBusyLoading)
      {
        this.bBusySaving = true;
        await this._saveQueue.Dequeue().Save();
        this.bBusySaving = false;
      }
      await Task.Delay(heartBeat, token);
    }
  }

  private async void CheckLoadQueue(int heartBeat, CancellationToken token)
  {
    while (!token.IsCancellationRequested)
    {
      if (this._loadQueue.Count != 0 && !this.bBusySaving)
      {
        this.bBusyLoading = true;
        await this._loadQueue.Dequeue().Load();
        this.bBusyLoading = false;
      }
      await Task.Delay(heartBeat, token);
    }
  }

  public void CreateSaveData()
  {
    this.gameSettings = new Save_GameSettings();
    this.exhibitionSettings = new Save_ExhibitionSettings();
    this.miniCamp = new Save_MiniCamp();
    this.twoMinuteDrill = new Save_TwoMD();
    this.seasonModeData = new SGD_SeasonModeData();
    this.keycloakUserData = this.GetKeycloakUserData();
    this.profileProgress = new ProfileProgress();
    this.teamSeasonDatas = new Dictionary<string, TeamSeasonData>();
    this.achievements = this.GetSaveAchievements();
    this.careerStats = new UserCareerStats.SaveData();
    this.LinkSaveSystem();
    SaveManager.bIsInitialized = true;
    UnityAction onInitialized = SaveManager.OnInitialized;
    if (onInitialized != null)
      onInitialized();
    AppEvents.LoadAllSaveData.Trigger();
  }

  private void HandleCreateOrReplaceAllSaveData() => this.HandleSaveAll();

  public void AddToSaveQueue(ISaveSync saveData)
  {
    if (saveData == null)
      return;
    this._saveQueue.Enqueue(saveData);
  }

  public void AddToLoadQueue(ISaveSync saveData)
  {
    if (saveData == null)
      return;
    this._loadQueue.Enqueue(saveData);
  }

  private void HandleSaveAll()
  {
    this.HandleValidateSaveDirectory();
    this.AddToSaveQueue((ISaveSync) this.gameSettings);
    this.AddToSaveQueue((ISaveSync) this.exhibitionSettings);
    this.AddToSaveQueue((ISaveSync) this.miniCamp);
    this.AddToSaveQueue((ISaveSync) this.twoMinuteDrill);
    this.AddToSaveQueue((ISaveSync) this.seasonModeData);
    this.AddToSaveQueue((ISaveSync) this._pProfile.Customization);
    this.AddToSaveQueue((ISaveSync) this.profileProgress);
    this.AddToSaveQueue((ISaveSync) this.keycloakUserData);
    this.AddToSaveQueue((ISaveSync) this.achievements);
    this.AddToSaveQueue((ISaveSync) this.careerStats);
    this.AddToSaveQueue((ISaveSync) this.rosterData);
  }

  private void HandleLoadAll()
  {
    this.HandleValidateSaveDirectory();
    if (this.SaveIOSettings.SaveExists(Save_GameSettings.FileName))
      this.AddToLoadQueue((ISaveSync) this.gameSettings);
    else
      this.AddToSaveQueue((ISaveSync) this.gameSettings);
    if (this.SaveIOSettings.SaveExists(Save_ExhibitionSettings.FileName))
      this.AddToLoadQueue((ISaveSync) this.exhibitionSettings);
    else
      this.AddToSaveQueue((ISaveSync) this.exhibitionSettings);
    if (this.SaveIOSettings.SaveExists(Save_MiniCamp.FileName))
      this.AddToLoadQueue((ISaveSync) this.miniCamp);
    else
      this.AddToSaveQueue((ISaveSync) this.miniCamp);
    if (this.SaveIOSettings.SaveExists(Save_TwoMD.FileName))
      this.AddToLoadQueue((ISaveSync) this.twoMinuteDrill);
    else
      this.AddToSaveQueue((ISaveSync) this.twoMinuteDrill);
    if (this.SaveIOSettings.SaveExists(PlayerCustomization.FileName))
      this.AddToLoadQueue((ISaveSync) this._pProfile.Customization);
    else
      this.AddToSaveQueue((ISaveSync) this._pProfile.Customization);
    if (this.SaveIOSettings.SaveExists(SaveKeycloakUserData.FileName))
      this.AddToLoadQueue((ISaveSync) this.keycloakUserData);
    else
      this.AddToSaveQueue((ISaveSync) this.keycloakUserData);
    if (this.SaveIOSettings.SaveExists(ProfileProgress.FileName))
      this.AddToLoadQueue((ISaveSync) this.profileProgress);
    else
      this.AddToSaveQueue((ISaveSync) this.profileProgress);
    if (this.SaveIOSettings.SaveExists(SaveAchievements.FileName))
      this.AddToLoadQueue((ISaveSync) this.achievements);
    else
      this.AddToSaveQueue((ISaveSync) this.achievements);
    if (this.SaveIOSettings.SaveExists(UserCareerStats.SaveData.FileName))
      this.AddToLoadQueue((ISaveSync) this.careerStats);
    else
      this.AddToSaveQueue((ISaveSync) this.careerStats);
    if (this.SaveIOSettings.SaveExists(RosterSaveData.FileName))
      this.AddToLoadQueue((ISaveSync) this.rosterData);
    else
      PersistentSingleton<RosterApi>.Instance.UpdateRostersOnStart = true;
  }

  public void StartLoadingSeasonModeData()
  {
    if (this.SaveIOSettings.SaveExists(SGD_SeasonModeData.FileName))
    {
      this.bSeasonDataExisted = true;
      this.AddToLoadQueue((ISaveSync) this.seasonModeData);
    }
    else
      this.bSeasonDataExisted = false;
  }

  private void HandleValidateSaveDirectory()
  {
  }

  public static PlayerProfile GetPlayerProfile() => PersistentSingleton<SaveManager>.Instance._pProfile;

  public static PlayerCustomization GetPlayerCustomization() => SaveManager.GetPlayerProfile().Customization;

  public static TeamBallMatStore GetTeamBallMatStore() => PersistentSingleton<SaveManager>.Instance._teamBallMaterialStore;

  public static CharacterCustomizationStore GetCharacterCustomizationStoreMale() => PersistentSingleton<SaveManager>.Instance._characterCustomizationMale;

  public static CharacterCustomizationStore GetCharacterCustomizationStoreFemale() => PersistentSingleton<SaveManager>.Instance._characterCustomizationFemale;

  public static AchievementData GetAchievementData() => PersistentSingleton<SaveManager>.Instance._achievementData;

  public static UniformStore GetUniformStore() => PersistentSingleton<SaveManager>.Instance._uniformStore;

  public static UniformLogoStore GetUniformLogoStore() => PersistentSingleton<SaveManager>.Instance._uniformLogoStore;

  public SaveKeycloakUserData GetKeycloakUserData()
  {
    if (this.keycloakUserData == null)
      this.keycloakUserData = new SaveKeycloakUserData();
    return this.keycloakUserData;
  }

  public SaveAchievements GetSaveAchievements()
  {
    if (this.achievements == null)
      this.achievements = new SaveAchievements();
    return this.achievements;
  }

  public RosterSaveData GetRosterSaveData()
  {
    if (this.rosterData == null)
      this.rosterData = new RosterSaveData();
    return this.rosterData;
  }

  public UserCareerStats.SaveData GetCareerStatsSaveData() => this.careerStats;

  public TeamSeasonData GetTeamSeasonData(string TeamName) => this.teamSeasonDatas.ContainsKey(TeamName) ? this.teamSeasonDatas[TeamName] : (TeamSeasonData) null;

  public void StoreTeamSeasonData(string TeamName, TeamSeasonData teamSeasonData)
  {
    if (this.teamSeasonDatas.ContainsKey(TeamName))
      this.teamSeasonDatas[TeamName] = teamSeasonData;
    else
      this.teamSeasonDatas.Add(TeamName, teamSeasonData);
  }

  private async Task ValidateSaveDataVersion(string version)
  {
    string pathToVersionFile = SaveIO.GetPath(this.saveVersionFile);
    if (!this.SaveIOSettings.SaveExists(this.saveVersionFile))
    {
      Debug.LogWarning((object) ("Cannot find version file. Assuming deletion... " + pathToVersionFile));
      await SaveIO.SaveStringAsync(SaveIO.gameVersion, pathToVersionFile);
      this.DeleteAllSaveGameData();
      pathToVersionFile = (string) null;
    }
    else
    {
      string str = await SaveIO.LoadStringAsync(pathToVersionFile);
      if (SaveIO.gameVersion == str)
      {
        Debug.Log((object) "Save Version Matched!");
        pathToVersionFile = (string) null;
      }
      else if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
      {
        Debug.LogWarning((object) ("Version mismatch detected! PersistentData reads a version of " + SaveIO.gameVersion + " while the save version reads " + str + " Deleting possible corrupted data... and Saving New Version File"));
        await SaveIO.SaveStringAsync(SaveIO.gameVersion, pathToVersionFile);
        this.DeleteAllSaveGameData();
        pathToVersionFile = (string) null;
      }
      else
      {
        int[] versionSemantic1 = SaveSyncUtils.StringToVersionSemantic(str);
        if (versionSemantic1 != null && versionSemantic1.Length >= 3)
        {
          int num1 = versionSemantic1[0];
          int num2 = versionSemantic1[1];
          int num3 = versionSemantic1[2];
          int[] versionSemantic2 = SaveSyncUtils.StringToVersionSemantic(version);
          int num4 = versionSemantic2[0];
          int num5 = versionSemantic2[1];
          int num6 = versionSemantic2[2];
          if (num1 > num4 || num2 > num5 || num3 > num6)
            SaveIO.DeleteAllSaves();
          if (num1 <= num4 && num2 <= num5 && num3 < num6)
          {
            if (num1 == 0 && num2 <= 4 && num3 <= 1)
              SaveIO.DeleteAllSaves();
            else if (num1 == 0 && num2 <= 4 && num3 <= 2)
              this.DeleteAllSaveGameData();
          }
          await SaveIO.SaveStringAsync(SaveIO.gameVersion, pathToVersionFile);
          pathToVersionFile = (string) null;
        }
        else
        {
          SaveIO.DeleteAllSaves();
          await SaveIO.SaveStringAsync(SaveIO.gameVersion, pathToVersionFile);
          pathToVersionFile = (string) null;
        }
      }
    }
  }

  private async Task SetSavedPlaybooksToDefaults()
  {
    string seasonDataPath = SaveIO.GetPath(SGD_SeasonModeData.FileName);
    SGD_SeasonModeData objectTarget = await SaveIO.LoadAsync<SGD_SeasonModeData>(seasonDataPath);
    if (objectTarget == null)
    {
      seasonDataPath = (string) null;
    }
    else
    {
      for (int index = 0; index < objectTarget.NumberOfTeamsInLeague; ++index)
      {
        TeamFile teamFile = objectTarget.GetTeamData(index).teamFile;
        teamFile.SetOffensivePlaybook(PersistentSingleton<TeamResourcesManager>.Instance.GetTeamData(index).DataMap.GetValue("OffPlaybook"));
        teamFile.SetDefensivePlaybook(PersistentSingleton<TeamResourcesManager>.Instance.GetTeamData(index).DataMap.GetValue("DefPlaybook"));
      }
      await SaveIO.SaveAsync<SGD_SeasonModeData>(objectTarget, seasonDataPath);
      seasonDataPath = (string) null;
    }
  }

  public void DeleteAllSaveGameData()
  {
    Debug.Log((object) "DELETING ALL SAVE DATA");
    this.DeleteData(this.saveDataPath, SGD_SeasonModeData.FileName);
    TeamDataCache.ClearTeamDataCache();
  }

  public void ResetPlayerProgress()
  {
    this.profileProgress = new ProfileProgress();
    AppEvents.SaveProfileProgress.Trigger();
  }

  private void DeleteData(string dataPath, string fileName)
  {
    string path = Path.Combine(dataPath, fileName);
    try
    {
      File.Delete(path);
    }
    catch (IOException ex)
    {
      Debug.Log((object) ("Error deleting file: " + ex.Message + " " + ex.ToString()));
    }
  }
}
