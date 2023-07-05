// Decompiled with JetBrains decompiler
// Type: MinicampIntroUi_2MD
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using TB12;
using TB12.UI;

public class MinicampIntroUi_2MD : MinicampIntroUi
{
  public override Enum ViewId { get; } = (Enum) EScreens.k2MD_Intro;

  protected override void PlayMinicamp()
  {
    TwoMinuteDrillGameScene.HandlePlay();
    UIDispatch.FrontScreen.HideView(EScreens.k2MD_Intro);
  }
}
