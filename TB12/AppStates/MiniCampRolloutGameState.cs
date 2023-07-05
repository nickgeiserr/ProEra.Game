// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MiniCampRolloutGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/MiniCampRolloutGameState")]
  public class MiniCampRolloutGameState : MiniCampGameState
  {
    public override EAppState Id => EAppState.kMiniCampRollout;

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinicamp_Rollout);
  }
}
