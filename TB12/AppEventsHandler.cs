// Decompiled with JetBrains decompiler
// Type: TB12.AppEventsHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using Framework.DeveloperSettings;
using System;
using System.Collections.Generic;
using TB12.Activator.UI;
using TB12.Solo.Data;
using UnityEngine;
using Vars;

namespace TB12
{
  public class AppEventsHandler : MonoBehaviour
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private PlayerProgressStore _progressStore;
    [SerializeField]
    private SoloLeaderboardData _soloLeaderboard;
    [SerializeField]
    private SceneAssetString _statusProScene;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private AppSettings _appSettings => ScriptableSingleton<AppSettings>.Instance;

    private void Awake()
    {
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._appSettings.GameMode.Link<EAppMode>(new Action<EAppMode>(this.HandleGameMode), false),
        AppEvents.ChangeGameMode.Link(new System.Action(this.HandleChangeGameMode)),
        AppEvents.ChallengeComplete.Link<int>(new Action<int>(this.HandleChallengeComplete)),
        AppState.DifficultyLevel.Link<EDifficulty>(new Action<EDifficulty>(this.HandleDifficultySetting)),
        DevSettingsActivator.State.Link<bool>(new Action<bool>(this.HandleDevSettings)),
        DevControls.LoadStatusProStadium.Link(new System.Action(this.HandleStatusProStadium)),
        this._appSettings.OptimizationSettings.PredictedFrameCount.Link<int>((Action<int>) (frameCount => MxMTrajectoryGeneratorBaseExtentions.SetSampleRateHack((float) frameCount))),
        this._appSettings.OptimizationSettings.AsyncTrajectoryGeneration.Link<bool>(new Action<bool>(this.HandleAsyncTrajectoryGeneration))
      });
      TrajGeneratorManager.OnReportUpdateTime += new Action<float>(this.HandleReport);
      TrajGeneratorManager.OnReportLog += new Action<string>(this.HandleReportLog);
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      this._progressStore.Clear();
    }

    private void HandleAsyncTrajectoryGeneration(bool useAsync)
    {
      useAsync = false;
      TrajectoryGenerator.asyncGeneration = useAsync;
    }

    private void HandleDifficultySetting(EDifficulty difficulty)
    {
      DifficultySetting difficultySetting = this._appSettings.GetDifficultySetting(difficulty);
      AppState.Difficulty = difficultySetting;
      this._throwManager.AutoAimRange = difficultySetting.AutoAimRange;
      this._throwManager.AutoAimStrength = difficultySetting.AutoAimStrength;
      this._throwManager.HandsDataModel.catchRadius = difficultySetting.CatchRadius;
      this._throwManager.HandsDataModel.catchRadiusOneHand = difficultySetting.CatchRadiusOneHand;
    }

    private void HandleReportLog(string obj) => UnityMainThreadDispatcher.Enqueue((System.Action) (() => Debug.LogError((object) obj)));

    private void HandleReport(float time) => UnityMainThreadDispatcher.Enqueue((System.Action) (() => Debug.Log((object) string.Format("TrajUpdate done every {0} ms on average", (object) time))));

    private void HandleChangeGameMode()
    {
      AppState.AppMode.SetValue(AppState.AppMode.Value == EAppMode.Activation ? EAppMode.Game : EAppMode.Activation);
      AppEvents.LoadMainMenu.Trigger();
    }

    private void HandleChallengeComplete(int score)
    {
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode != EAppMode.Game)
        return;
      this._progressStore.Apply(this._levelsStore.CurrentLevel.Value.id, (int) (AppState.Difficulty.Level + 1), score);
      this._soloLeaderboard.SubmitScore(ASummaryScreen.GetLeaderURLByGameType(AppState.GameMode), this._progressStore.PlayerName, score);
    }

    private void HandleGameMode(EAppMode mode)
    {
      AppState.AppMode.SetValue(mode);
      AppEvents.LoadMainMenu.Trigger();
    }

    private void HandleDevSettings(bool state)
    {
      if (state)
        VRState.LaserEnabled.Value = true;
      if (state)
        return;
      VRState.LaserEnabled.Value = false;
    }

    private void HandleStatusProStadium()
    {
      if (this._statusProScene.IsValid())
        RoutineRunner.StartRoutine(PersistentSingleton<LevelManager>.Instance.LoadEnvironment(this._statusProScene));
      else
        Debug.LogError((object) "Invalid scene");
    }
  }
}
