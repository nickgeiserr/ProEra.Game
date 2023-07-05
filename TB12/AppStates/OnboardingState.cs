// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.OnboardingState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using System.Threading.Tasks;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/OnboardingState")]
  public class OnboardingState : AxisGameState
  {
    public override EAppState Id => EAppState.kOnboarding;

    public event System.Action FinishedEnable;

    public override bool clearFadeOnEntry => false;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      PersistentData.CoachCallsPlays = false;
      WorldState.CrowdEnabled.SetValue(false);
      AppSounds.CrowdSound.SetValue(false);
      AppSounds.AnnouncerSound.SetValue(false);
      AppSounds.PlayerChatterSound.ForceValue(false);
      UIDispatch.FrontScreen.CloseScreen();
      AppSounds.StopSfx(ESfxTypes.kTunnel);
      AppSounds.AmbienceSound.ForceValue(false);
    }

    protected override void OnExitState()
    {
      AppSounds.MusicSound.SetValue(true);
      base.OnExitState();
    }

    protected override void OnStadiumLoadFinished() => this.OnStadiumLoadFinishedAsync();

    private async Task OnStadiumLoadFinishedAsync()
    {
      await MatchManager.instance.playersManager.IsInitialized();
      System.Action finishedEnable = this.FinishedEnable;
      if (finishedEnable != null)
        finishedEnable();
      MatchManager.instance.CallStart();
      MatchManager.instance.playersManager.PutAllPlayersInHuddle();
      for (int index = 0; index < 11; ++index)
      {
        Game.OffensivePlayers[index].ShowPlayerAvatar();
        Game.DefensivePlayers[index].ShowPlayerAvatar();
      }
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_Onboarding);
  }
}
