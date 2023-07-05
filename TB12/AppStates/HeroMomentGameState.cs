// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.HeroMomentGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using ProEra.Game;
using System;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/HeroMomentGameState")]
  public class HeroMomentGameState : AxisGameState
  {
    private int _defaultDifficulty;

    public override EAppState Id => EAppState.kHeroMoment;

    public event System.Action FinishedEnable;

    public override bool clearFadeOnEntry => false;

    public override bool AlwaysUnloadEnvironment => true;

    public override bool allowPause => false;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      PersistentData.CoachCallsPlays = false;
      VRState.LocomotionEnabled.SetValue(true);
      WorldState.CrowdEnabled.SetValue(false);
      AppSounds.CrowdSound.SetValue(false);
      AppSounds.AnnouncerSound.SetValue(false);
      AppSounds.PlayerChatterSound.ForceValue(false);
      UIDispatch.FrontScreen.CloseScreen();
      AppState.SeasonMode.Value = ESeasonMode.kUnknown;
      AppSounds.StopSfx(ESfxTypes.kTunnel);
      AppSounds.AmbienceSound.ForceValue(false);
      this._defaultDifficulty = (int) GameSettings.DifficultyLevel;
      GameSettings.DifficultyLevel.SetValue(0);
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = true;
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.OnValueChanged += new Action<bool>(this.HandleStateTransition);
    }

    protected override void OnExitState()
    {
      base.OnExitState();
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.OnValueChanged -= new Action<bool>(this.HandleStateTransition);
      GameSettings.DifficultyLevel.SetValue(this._defaultDifficulty);
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = false;
      AppSounds.AmbienceSound.SetValue(false);
    }

    private void HandleStateTransition(bool loading)
    {
      if (loading)
        return;
      Debug.Log((object) "HeroMomentGameState: HandleStateTransition: Finished Loading");
      System.Action finishedEnable = this.FinishedEnable;
      if (finishedEnable != null)
        finishedEnable();
      PlaybookState.CurrentFormation.SetValue(Plays.self.shotgunPlays_Normal);
      MatchManager.instance.playManager.savedOffPlay = (PlayDataOff) PlaybookState.CurrentFormation.Value.GetPlay(1);
      PlaybookState.CurrentPlay.SetValue((PlayData) MatchManager.instance.playManager.savedOffPlay);
      MatchManager.instance.playManager.canUserCallAudible = false;
      for (int index = 0; index < 11; ++index)
      {
        if (index != 5)
          global::Game.OffensivePlayers[index].ShowPlayerAvatar();
        global::Game.DefensivePlayers[index].ShowPlayerAvatar();
      }
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_Onboarding);
  }
}
