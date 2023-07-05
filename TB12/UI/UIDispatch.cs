// Decompiled with JetBrains decompiler
// Type: TB12.UI.UIDispatch
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace TB12.UI
{
  public static class UIDispatch
  {
    public static readonly UIScreenDispatch<EScreens> FrontScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kFront);
    public static readonly UIScreenDispatch<EScreens> LeftScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kLeft);
    public static readonly UIScreenDispatch<EScreens> RightScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kRight);
    public static readonly UIScreenDispatch<ELockerRoomUI> LockerRoomScreen = new UIScreenDispatch<ELockerRoomUI>((Enum) EScreenParent.kLockerRoom);
    public static readonly UIScreenDispatch<EScreens> LeftTopWristScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kLeftTopWrist);
    public static readonly UIScreenDispatch<EScreens> LeftTopWristScreen2 = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kLeftTopWrist2);
    public static readonly UIScreenDispatch<EScreens> LeftTopWristScreen3 = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kLeftTopWrist3);
    public static readonly UIScreenDispatch<EScreens> LeftBottomWristScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kLeftBottomWrist);
    public static readonly UIScreenDispatch<EScreens> RightTopWristScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kRightTopWrist);
    public static readonly UIScreenDispatch<EScreens> RightTopWristScreen2 = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kRightTopWrist2);
    public static readonly UIScreenDispatch<EScreens> RightTopWristScreen3 = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kRightTopWrist3);
    public static readonly UIScreenDispatch<EScreens> RightBottomWristScreen = new UIScreenDispatch<EScreens>((Enum) EScreenParent.kRightBottomWrist);

    public static void HideScreen(EScreenParent screen) => UIBaseDispatch.CloseScreen((Enum) screen);

    public static void HideAll(bool animate = true) => UIBaseDispatch.CloseAll(animate);

    public static void SetScreensVisible(bool visible) => UIBaseDispatch.SetScreensVisible(visible);

    public static async Task DisplayCAP(bool useFade = true)
    {
      float num = 1.85f;
      Vector3 pos = new Vector3(-3f, 0.0f, 0.0f);
      Quaternion quat = Quaternion.Euler(0.0f, 45f, 0.0f);
      if (useFade)
      {
        VREvents.BlinkMovePlayer.Trigger(num, pos, quat);
        await Task.Delay(TimeSpan.FromSeconds((double) num / 2.0));
      }
      else
        PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(pos, quat);
      UIDispatch.FrontScreen.DisplayView(EScreens.kCustomizeMain);
      VRState.LocomotionEnabled.SetValue(false);
    }
  }
}
