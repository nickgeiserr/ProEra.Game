// Decompiled with JetBrains decompiler
// Type: ProEra.Game.IPrePlayWindow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

namespace ProEra.Game
{
  public interface IPrePlayWindow
  {
    void HideWindow();

    bool IsVisible();

    void SetHotRouteNames(string[] names);

    bool IsOffensiveControlVisible();

    bool CanSnapBallWithController();

    void ShowDefensiveHotRouteControls();

    void CancleShiftPlayerControls();

    void ShowHomeWindow();

    bool IsShiftControlVisible();

    void ShowHotRouteControls();

    void EndHotRouteSelection();

    TextMeshProUGUI[] GetHotRouteNamesTMPros();

    GameObject GetHotRouteBlockButton();

    void EndDefensiveHotRouteSelection();

    void ShowDefensiveHotRouteReceiverSelect();
  }
}
