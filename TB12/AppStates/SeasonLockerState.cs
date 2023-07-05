// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.SeasonLockerState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/Season Locker State")]
  public class SeasonLockerState : GameState
  {
    public override EAppState Id => EAppState.kSeasonLocker;

    public override bool showTransition => false;

    public override bool allowRetry => false;

    public override bool allowRain => false;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      if (AppState.SeasonMode.Value == ESeasonMode.kNew)
      {
        UIDispatch.LockerRoomScreen.DisplayView(ELockerRoomUI.kSeasonTeamSelect);
      }
      else
      {
        int num = 0;
        if (PersistentSingleton<SaveManager>.Instance.SeasonModeDataExists())
        {
          PersistentData.saveSlot = num.ToString();
          int userTeamIndex = PersistentSingleton<SaveManager>.Instance.seasonModeData.UserTeamIndex;
          PersistentData.SetUserTeam(SeasonModeManager.self.GetTeamData(userTeamIndex));
          SeasonModeManager.self.ShowSeasonMode();
        }
        else
          Debug.LogError((object) ("Cannot continue Franchise-- no saved franchise exists in slot " + num.ToString()));
      }
    }

    protected override void OnExitState() => UIDispatch.LockerRoomScreen.CloseScreen();
  }
}
