// Decompiled with JetBrains decompiler
// Type: MiniCampRunAndShootGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using TB12;
using UnityEngine;

[CreateAssetMenu(menuName = "TB12/States/MiniCampRunAndShootGameState")]
public class MiniCampRunAndShootGameState : MiniCampGameState
{
  public override EAppState Id => EAppState.kMiniCampRunAndShoot;

  protected override void OnEnterState()
  {
    base.OnEnterState();
    if (!((Object) PersistentSingleton<BallsContainerManager>.Instance != (Object) null))
      return;
    PersistentSingleton<BallsContainerManager>.Instance.HasStatusProBall = true;
  }

  protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinicamp_RunAndShoot);
}
