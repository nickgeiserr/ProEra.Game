// Decompiled with JetBrains decompiler
// Type: ProEra.Game.IPenaltiesGUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;

namespace ProEra.Game
{
  public interface IPenaltiesGUI
  {
    void ShowWindow(Penalty penaltyOnPlay, bool userCanDecide);

    bool IsVisible();

    void HideWindow();

    IEnumerator ShowPenalty(float showInBarWaitTime);
  }
}
