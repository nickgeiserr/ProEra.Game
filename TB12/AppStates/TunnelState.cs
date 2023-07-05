// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.TunnelState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using System;
using System.Threading.Tasks;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/TunnelState")]
  public class TunnelState : AxisGameState
  {
    private const int TUNNEL_TIME = 13;

    public override EAppState Id => EAppState.kTunnel;

    public override bool allowPause => false;

    public override bool clearFadeOnEntry => true;

    protected override async void OnEnterState()
    {
      TunnelState tunnelState = this;
      // ISSUE: reference to a compiler-generated method
      tunnelState.\u003C\u003En__0();
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0.0f, 180f, 0.0f));
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = true;
      VRState.LocomotionEnabled.SetValue(true);
      PersistentData.CoachCallsPlays = false;
      WorldState.CrowdEnabled.SetValue(false);
      AppSounds.CrowdSound.SetValue(false);
      AppSounds.AnnouncerSound.SetValue(false);
      AppSounds.PlayerChatterSound.ForceValue(false);
      UIDispatch.FrontScreen.CloseScreen();
      AppSounds.StopSfx(ESfxTypes.kTunnel);
      AppSounds.AmbienceSound.ForceValue(false);
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.OnValueChanged += new Action<bool>(tunnelState.HandleStateTransition);
    }

    protected override void OnExitState()
    {
      base.OnExitState();
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.InTransition.OnValueChanged -= new Action<bool>(this.HandleStateTransition);
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = false;
    }

    private void HandleStateTransition(bool loading)
    {
      if (loading)
        return;
      AppSounds.PlaySfx(ESfxTypes.kFTUETunnel);
      AppSounds.PlayVO(EVOTypes.kLamarTunnel);
      this.WaitForAudio();
    }

    private async void WaitForAudio()
    {
      await Task.Delay(13000);
      GameplayManager.LoadLevelActivation(EGameMode.kHeroMoment, ETimeOfDay.Night);
    }
  }
}
