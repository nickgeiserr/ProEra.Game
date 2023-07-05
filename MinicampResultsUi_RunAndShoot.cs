// Decompiled with JetBrains decompiler
// Type: MinicampResultsUi_RunAndShoot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using TB12;
using TB12.UI;

public class MinicampResultsUi_RunAndShoot : MinicampResultsUi
{
  public override Enum ViewId { get; } = (Enum) EScreens.kMinicampRunAndShoot_Results;

  protected override void RetryMinicamp()
  {
    if (VRState.PauseMenu.Value.Equals(true) || (UnityEngine.Object) this.myGameState == (UnityEngine.Object) null)
      return;
    this.myGameState.HandleRetry();
    UIDispatch.FrontScreen.HideView(EScreens.kMinicampRunAndShoot_Results);
  }
}
