// Decompiled with JetBrains decompiler
// Type: TB12.TwoMinuteDrillGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using TB12.UI;

namespace TB12
{
  public class TwoMinuteDrillGameScene : AxisGameScene
  {
    public void ShowIntroScreen()
    {
      AppSounds.PlayOC(EOCTypes.k2MDIntro);
      UIDispatch.FrontScreen.DisplayView(EScreens.k2MD_Intro);
      VRState.LocomotionEnabled.SetValue(true);
    }

    public static void HandlePlay()
    {
      VRState.LocomotionEnabled.SetValue(false);
      ((TwoMinuteDrillGameFlow) AxisGameFlow.instance).ResetGame();
    }
  }
}
