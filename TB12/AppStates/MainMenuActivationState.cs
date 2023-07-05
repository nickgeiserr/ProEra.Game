// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MainMenuActivationState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Backtrace.Unity;
using FootballVR;
using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12.Activator.Data;
using TB12.Backend;
using TB12.UI;
using UnityEngine;
using Vars;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/MainMenuActivationState")]
  public class MainMenuActivationState : GameState
  {
    [SerializeField]
    private ALeaderboardData _leaderboardData;
    [SerializeField]
    private ShaderVariantCollection _prewarmedShaders;
    [SerializeField]
    private GameDataUpdater _gameDataUpdater;
    private bool _firstLoad = true;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public override EAppState Id => EAppState.kMainMenuActivation;

    public override bool showLoadingScreen => this._firstLoad;

    public override bool showTransition => false;

    public override bool allowPause => false;

    public override bool allowRain => false;

    public override bool UnloadGameplayScene => (EAppMode) (Variable<EAppMode>) AppState.AppMode != EAppMode.Activation;

    public override IEnumerator Load()
    {
      yield return (object) base.Load();
      if ((UnityEngine.Object) this._prewarmedShaders != (UnityEngine.Object) null && !this._prewarmedShaders.isWarmedUp)
        this._prewarmedShaders.WarmUp();
    }

    protected override void OnEnterState()
    {
      base.OnEnterState();
      VRState.PausePermission = false;
      PersistentSingleton<GamePlayerController>.Instance.AdjustOneHandedOffHandTransform(GamePlayerController.EOneHandedModes.LockerRoom);
      int num = 0;
      if (this._firstLoad)
      {
        Debug.Log((object) "First load!");
        TouchUI.Enabled = false;
        if (PersistentSingleton<SaveManager>.Instance.SeasonModeDataExists())
        {
          try
          {
            AppState.SeasonMode.Value = ESeasonMode.kLoad;
            PersistentData.saveSlot = num.ToString();
            SeasonModeManager.self.ShowSeasonMode();
            if ((bool) SaveManager.GetPlayerProfile().Customization.IsNewCustomization)
              UIDispatch.DisplayCAP().SafeFireAndForget();
          }
          catch (NullReferenceException ex)
          {
            MainMenuActivationState.LogBacktraceNewSeasonException((Exception) ex);
            Debug.Log((object) ("UICreateNewSeason!" + ex.Message));
            SeasonModeManager.self.UICreateNewSeason();
          }
        }
        else
        {
          Debug.Log((object) "UICreateNewSeason!");
          SeasonModeManager.self.UICreateNewSeason();
        }
      }
      else if (PersistentSingleton<SaveManager>.Instance.SeasonModeDataExists())
      {
        try
        {
          AppState.SeasonMode.Value = ESeasonMode.kLoad;
          SeasonModeManager.self.ShowSeasonMode();
        }
        catch (NullReferenceException ex)
        {
          MainMenuActivationState.LogBacktraceNewSeasonException((Exception) ex);
          Debug.LogError((object) ("UICreateNewSeason! " + ex.Message));
          SeasonModeManager.self.UICreateNewSeason();
        }
      }
      UniformCapture.ClearAllCachedTextures();
      UIDispatch.HideAll(false);
      AppState.Mode.SetValue(TB12.EMode.kUnknown);
      AppState.GameMode = EGameMode.kUnknown;
      WorldState.CrowdEnabled.SetValue(false);
      ActivationState.PlayerName = string.Empty;
      BallsContainerManager.ClearBalls.Trigger();
      GameSettings.PlayerOnField.SetValue(false);
      this._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.Link<bool>(new Action<bool>(this.HandleStateTransition))
      });
      ProEra.Game.MatchState.Reset();
      WorldState.WorldRevealed.Link(new System.Action(this.WorldRevealedHandler));
      Debug.Log((object) "MainMenuActivationState: Setup the world revealed handler");
      if (!(bool) WorldState.WorldRevealed)
        this.DelayedExecute(0.25f, new System.Action(TransitionController.PlayTransition.Trigger));
      DevControls.PlaybackMode.SetValue(false);
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = true;
    }

    protected override void OnExitState()
    {
      TouchUI.Enabled = true;
      WorldState.WorldRevealed.Unlink(new System.Action(this.WorldRevealedHandler));
      UIDispatch.HideAll();
      GameSettings.PlayerOnField.SetValue(true);
      this._linksHandler.Clear();
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnCameraFadeComplete -= new System.Action(this.OnStateTranisitionComplete);
      UniformCapture.ClearAllCachedTextures();
      AppSounds.StopMusic.Trigger();
      this._gameDataUpdater.StopAutoUpdate();
      VrOrientationRotator.Instance.ResetToDefaultRotation();
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = false;
      VRState.PausePermission = true;
    }

    private async void WorldRevealedHandler()
    {
      Debug.Log((object) nameof (WorldRevealedHandler));
      TouchUI.Enabled = true;
      TouchButtonGradientEffect.PlaySpawnEffect();
      await Task.Delay(150);
      if (!this._firstLoad)
        return;
      this._firstLoad = false;
    }

    public override void ClearState() => this._firstLoad = true;

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_LockerRoom);

    public override bool HasCameraFadeOverride(out VRCameraFade.FadeSettings settings)
    {
      settings = new VRCameraFade.FadeSettings(0.5f, 3.5f);
      return true;
    }

    private void OnStateTranisitionComplete() => VRState.PausePermission = true;

    private void HandleStateTransition(bool inTransition)
    {
      if (inTransition)
        return;
      VRState.HandsVisible.SetValue(true);
      TransitionScreenController.CheckForNetworkMessages();
    }

    private static void LogBacktraceNewSeasonException(Exception e) => BacktraceClient.Instance.Send(new Exception("Forced generation of new season mode data due to data corruption", e), (List<string>) null, (Dictionary<string, string>) null);
  }
}
