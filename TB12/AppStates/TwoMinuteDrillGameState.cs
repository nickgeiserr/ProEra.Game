// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.TwoMinuteDrillGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/TwoMinuteDrillGameState")]
  public class TwoMinuteDrillGameState : AxisGameState
  {
    public override EAppState Id => EAppState.k2MD;

    protected override void OnStadiumLoadFinished()
    {
      base.OnStadiumLoadFinished();
      AppSounds.PlayMusic(EMusicTypes.k2MD);
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinigame_TwoMinuteDrill);
  }
}
