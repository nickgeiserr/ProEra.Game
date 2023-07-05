// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MainMenuState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using System;
using TB12.Activator.Data;
using TB12.Backend;
using TB12.UI;
using UnityEngine;
using Vars;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/MainMenuState")]
  public class MainMenuState : GameState
  {
    [SerializeField]
    private ALeaderboardData _leaderboardData;
    [SerializeField]
    private GameDataUpdater _gameDataUpdater;
    private bool _firstLoad = true;

    public override EAppState Id => EAppState.kMainMenu;

    public override bool showLoadingScreen => this._firstLoad;

    public override bool showTransition => false;

    public override bool allowRain => false;

    public override bool UnloadGameplayScene => (EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Activation;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      VRState.HandsVisible.SetValue(true);
      UIDispatch.HideAll(false);
      BallsContainerManager.ClearBalls.Trigger();
      DevControls.PlaybackMode.SetValue(false);
      WorldState.CrowdEnabled.SetValue(false);
      WorldState.WorldRevealed.Link(new Action(this.WorldRevealedHandler));
      if ((bool) WorldState.WorldRevealed)
        return;
      this.DelayedExecute(0.25f, new Action(TransitionController.PlayTransition.Trigger));
    }

    protected override void OnExitState()
    {
      WorldState.WorldRevealed.Unlink(new Action(this.WorldRevealedHandler));
      UIDispatch.HideAll();
      AppSounds.StopMusic.Trigger();
      BallsContainerManager.IsEnabled.SetValue(false);
      this._gameDataUpdater.StopAutoUpdate();
    }

    private void WorldRevealedHandler()
    {
      if (AppState.Mode.Value == TB12.EMode.kMultiplayer)
        AppState.Mode.SetValue(TB12.EMode.kUnknown);
      BallsContainerManager.IsEnabled.SetValue(true);
      BallsContainerManager.CanSpawnBall.SetValue(true);
      AppSounds.PlayMusic(EMusicTypes.kMainMenu);
      if (!this._firstLoad)
        return;
      this._firstLoad = false;
    }

    public override void ClearState() => this._firstLoad = true;
  }
}
