// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MiniCampQBPresenceGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/MiniCampQBPresenceGameState")]
  public class MiniCampQBPresenceGameState : MiniCampGameState
  {
    public override EAppState Id => EAppState.kMiniCampQBPresence;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.MaxPitchDistance = 37f;
    }

    protected override void OnExitState()
    {
      base.OnExitState();
      ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.MaxPitchDistance = 9.144f;
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinicamp_PocketPassing_QbPresence);
  }
}
