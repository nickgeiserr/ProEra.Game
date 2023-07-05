// Decompiled with JetBrains decompiler
// Type: MiniCampGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using ProEra.Game;
using System;
using System.Collections;
using TB12;
using TB12.AppStates;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;
using Vars;

public class MiniCampGameState : GameState
{
  [SerializeField]
  private StadiumConfigsStore _stadiumConfigsStore;
  [SerializeField]
  private GameplayDataStore _gameplayData;
  [SerializeField]
  private GameLevelsStore _levelsStore;
  [SerializeField]
  private ThrowManager _throwManager;
  [SerializeField]
  private GameObject minicampIntroUiPrefab;
  [SerializeField]
  private GameObject minicampResultsUiPrefab;
  private readonly RoutineHandle _retryRoutine = new RoutineHandle();
  public bool DidGiveResultsFeedback;
  private const float standingHeightOffset = 0.33f;
  private const float distSpawnFromPlayer = 0.85f;
  private EOCTypes _eocType;
  [SerializeField]
  protected int[] _starThresholds;
  [SerializeField]
  private Vector3 _defaultStartPosition;
  [SerializeField]
  private Vector3 _defaultStartRotation = new Vector3(0.0f, 180f, 0.0f);

  public override bool clearFadeOnEntry => true;

  public override bool AlwaysUnloadEnvironment => true;

  public override EAppState Id => EAppState.kMiniCampPrecisionPassing;

  public event System.Action OnTrainingStarted;

  public event System.Action OnExitTraining;

  private Save_MiniCamp _miniCampData => PersistentSingleton<SaveManager>.Instance.miniCamp;

  protected override void OnEnterState()
  {
    base.OnEnterState();
    WorldState.CrowdEnabled.SetValue(true);
    VRState.HelmetEnabled.SetValue(true);
    MiniGameScoreState.ResetData();
    AppEvents.Retry.OnTrigger += new System.Action(this.HandleRetry);
    AppEvents.Continue.OnTrigger += new System.Action(this.HandleContinue);
    AppSounds.PlayStinger(EStingerType.kStinger4);
    PersistentSingleton<StateManager<EAppState, GameState>>.Instance.SetStadiumConfigsStoreUsedForLoading(this._stadiumConfigsStore);
    this._stadiumConfigsStore.GetConfiguration(this._miniCampData.SelectedEntry.TeamName, ETimeOfDay.Clear, new Action<StadiumConfigStore>(this.EnterStateStadiumFoundCallback));
    BallThrowMechanic componentInChildren1 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left).hand.transform.parent.GetComponentInChildren<BallThrowMechanic>();
    BallThrowMechanic componentInChildren2 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right).hand.transform.parent.GetComponentInChildren<BallThrowMechanic>();
    componentInChildren1.IsThrowAllowed = true;
    componentInChildren2.IsThrowAllowed = true;
    GameManager.Instance.TurnOffImmersiveTackleInMiniCamp();
    PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._defaultStartPosition, Quaternion.Euler(this._defaultStartRotation));
  }

  private IEnumerator LoadStadiumForNextLevel(StadiumConfigStore obj)
  {
    yield return (object) PersistentSingleton<LevelManager>.Instance.LoadEnvironment(obj.stadiumPrefabRef, this._stadiumConfigsStore.GetTimeOfDayScene(ETimeOfDay.Clear));
    VRState.LocomotionEnabled.SetValue(true);
    this.ShowIntroFTUI();
    yield return (object) this.ClearFade();
  }

  private void EnterStateStadiumFoundCallback(StadiumConfigStore obj)
  {
    VRState.LocomotionEnabled.SetValue(true);
    this.ShowIntroFTUI();
    this.StartRoutine(this.ClearFade());
  }

  private void StadiumFoundCallback(StadiumConfigStore obj) => this.StartRoutine(this.LoadStadiumForNextLevel(obj));

  private IEnumerator ClearFade()
  {
    yield return (object) GamePlayerController.CameraFade.Clear();
    VRState.PausePermission = true;
  }

  private void ShowIntroFTUI()
  {
    switch (this.Id)
    {
      case EAppState.kMiniCampQBPresence:
        AppSounds.PlayOC(EOCTypes.kPocketPassingIntro);
        this._eocType = EOCTypes.kPocketPassingIntro;
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampPocketPassing_Intro);
        break;
      case EAppState.kMiniCampPrecisionPassing:
        AppSounds.PlayOC(EOCTypes.kDimeDroppingIntro);
        this._eocType = EOCTypes.kDimeDroppingIntro;
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampDimeDropping_Intro);
        break;
      case EAppState.kMiniCampRunAndShoot:
        AppSounds.PlayOC(EOCTypes.kRunAndShootIntro);
        this._eocType = EOCTypes.kRunAndShootIntro;
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampRunAndShoot_Intro);
        break;
      case EAppState.kMiniCampRollout:
        AppSounds.PlayOC(EOCTypes.kRolloutIntro);
        this._eocType = EOCTypes.kRolloutIntro;
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampRollout_Intro);
        break;
    }
  }

  public void StartTraining()
  {
    AppState.GameInfoUI.ForceValue(true);
    AppSounds.StopOc(this._eocType);
    foreach (HandData handData in this._throwManager.HandsDataModel.HandDatas)
      handData.EnableCatching(false);
    BallsContainerManager.CanSpawnBall.SetValue(false);
    RoutineRunner.StartRoutine(this.StartOfTrainingDelayBallSpawn());
    System.Action onTrainingStarted = this.OnTrainingStarted;
    if (onTrainingStarted == null)
      return;
    onTrainingStarted();
  }

  public void CalculateStarsEarned()
  {
    int num1 = 0;
    for (int index = 0; index < this._starThresholds.Length; ++index)
    {
      if (this._starThresholds[index] <= (int) MiniGameScoreState.Score)
        ++num1;
    }
    MiniGameScoreState.StarsEarned.Value = num1;
    float num2 = (float) (this._starThresholds[this._starThresholds.Length - 1] + this._starThresholds[0]);
    if ((double) num2 == 0.0)
      return;
    MiniGameScoreState.StarsProgress.Value = num1 == 3 ? 1f : (float) MiniGameScoreState.Score.Value / num2;
  }

  public void ShowEndOfMinicampScreen()
  {
    int num = 0;
    for (int index = 0; index < this._starThresholds.Length; ++index)
    {
      if (this._starThresholds[index] <= (int) MiniGameScoreState.Score)
        ++num;
    }
    bool highScoreEarned = this.SaveMiniCampData(num);
    MinicampResultsUi.SetResultsInfo((int) MiniGameScoreState.Score, num, highScoreEarned);
    switch (this.Id)
    {
      case EAppState.kMiniCampQBPresence:
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampPocketPassing_Results);
        break;
      case EAppState.kMiniCampPrecisionPassing:
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampDimeDropping_Results);
        break;
      case EAppState.kMiniCampRunAndShoot:
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampRunAndShoot_Results);
        break;
      case EAppState.kMiniCampRollout:
        UIDispatch.FrontScreen.DisplayView(EScreens.kMinicampRollout_Results);
        break;
    }
  }

  public bool SaveMiniCampData(int numStarsEarned)
  {
    int num = this._miniCampData.SelectedEntry.SetPersonalBest((int) MiniGameScoreState.Score) ? 1 : 0;
    this._miniCampData.SelectedEntry.SetBestStars(numStarsEarned);
    if (numStarsEarned > 0 && this._miniCampData.SelectedEntry.Level == this._miniCampData.SelectedMiniCamp.CurrentLevel)
      ++this._miniCampData.SelectedMiniCamp.CurrentLevel;
    AppEvents.SaveMiniCamp.Trigger();
    return num != 0;
  }

  protected override void OnExitState()
  {
    base.OnExitState();
    if ((UnityEngine.Object) PersistentSingleton<BallsContainerManager>.Instance != (UnityEngine.Object) null)
    {
      PersistentSingleton<BallsContainerManager>.Instance.ResetBallsContainerPrefabToDefault();
      PersistentSingleton<BallsContainerManager>.Instance.HasStatusProBall = false;
    }
    WorldState.CrowdEnabled.SetValue(false);
    AppState.GameInfoUI.SetValue(false);
    VRState.HelmetEnabled.SetValue(false);
    VRState.LocomotionEnabled.SetValue(false);
    AppEvents.Retry.OnTrigger -= new System.Action(this.HandleRetry);
    AppEvents.Continue.OnTrigger -= new System.Action(this.HandleContinue);
    PersistentSingleton<BallsContainerManager>.Instance.ResetPosition();
    if ((UnityEngine.Object) this._throwManager != (UnityEngine.Object) null)
      this._throwManager.Clear();
    GameManager.Instance.TurnOnImmersiveTackleInMiniCamp();
    System.Action onExitTraining = this.OnExitTraining;
    if (onExitTraining == null)
      return;
    onExitTraining();
  }

  private void HandleContinue()
  {
    if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Game)
      this._levelsStore.NextLevel();
    AppEvents.LoadMainMenu.Trigger();
  }

  public void HandleRetry() => this._retryRoutine.Run(this.RetryRoutine());

  public void HandleNextLevel()
  {
    if (this._miniCampData.SelectedEntry.Level >= this._miniCampData.SelectedMiniCamp.MiniCampEntries.Length)
    {
      Console.Error.WriteLine("ERROR: Index out of bounds");
    }
    else
    {
      MiniGameScoreState.ResetData();
      UIDispatch.HideAll();
      BallsContainerManager.ClearBalls.Trigger();
      RoutineRunner.StartRoutine(this.LoadNextLevel());
    }
  }

  private IEnumerator LoadNextLevel()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    MiniCampGameState miniCampGameState = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      int level = miniCampGameState._miniCampData.SelectedEntry.Level;
      miniCampGameState._miniCampData.SelectedEntry = miniCampGameState._miniCampData.SelectedMiniCamp.MiniCampEntries[level];
      miniCampGameState._stadiumConfigsStore.GetConfiguration(miniCampGameState._miniCampData.SelectedEntry.TeamName, ETimeOfDay.Clear, new Action<StadiumConfigStore>(miniCampGameState.StadiumFoundCallback));
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    VRState.PausePermission = false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) GamePlayerController.CameraFade.Fade();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  private IEnumerator RetryRoutine()
  {
    MiniGameScoreState.ResetData();
    UIDispatch.HideAll();
    yield return (object) GamePlayerController.CameraFade.Fade();
    BallsContainerManager.ClearBalls.Trigger();
    System.Action onTrainingStarted = this.OnTrainingStarted;
    if (onTrainingStarted != null)
      onTrainingStarted();
    yield return (object) GamePlayerController.CameraFade.Clear();
  }

  private IEnumerator StartOfTrainingDelayBallSpawn()
  {
    yield return (object) new WaitForSecondsRealtime(0.5f);
    BallsContainerManager.CanSpawnBall.SetValue(true);
    foreach (HandData handData in this._throwManager.HandsDataModel.HandDatas)
      handData.EnableCatching(true);
  }
}
