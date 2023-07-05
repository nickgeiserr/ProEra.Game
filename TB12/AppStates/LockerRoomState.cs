// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.LockerRoomState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/Locker Room State")]
  public class LockerRoomState : GameState
  {
    public override EAppState Id => EAppState.kChangeGear;

    public override bool showTransition => false;

    public override bool allowRetry => false;

    public override bool allowRain => false;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      UIDispatch.LockerRoomScreen.DisplayView(AppState.Mode.Value == EMode.kMultiplayer ? ELockerRoomUI.kMultiplayer : ELockerRoomUI.kCustomizeMain);
      if ((EMode) AppState.Mode == EMode.kMultiplayer)
        VREvents.AdjustPlayerHeight.Trigger();
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = true;
    }

    protected override void OnExitState()
    {
      UIDispatch.LockerRoomScreen.CloseScreen();
      PersistentSingleton<GamePlayerController>.Instance.AvoidWallPenetration = false;
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_LockerRoom);

    public override bool HasCameraFadeOverride(out VRCameraFade.FadeSettings settings)
    {
      settings = new VRCameraFade.FadeSettings(0.5f, 3.5f);
      return true;
    }
  }
}
