// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.IntelAnimationState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/IntelAnimation")]
  public class IntelAnimationState : GameState
  {
    public override EAppState Id { get; } = EAppState.kIntelAnimation;

    public override bool allowRain { get; }

    protected override void OnEnterState()
    {
      base.OnEnterState();
      VRState.LocomotionEnabled.SetValue(true);
      VRState.HelmetEnabled.SetValue(false);
      VRState.ControllerGraphics.SetValue(true);
    }

    protected override void OnExitState()
    {
      VRState.LocomotionEnabled.SetValue(false);
      VRState.ControllerGraphics.SetValue(false);
      UIDispatch.HideAll();
    }
  }
}
