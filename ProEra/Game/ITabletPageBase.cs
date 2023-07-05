// Decompiled with JetBrains decompiler
// Type: ProEra.Game.ITabletPageBase
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;

namespace ProEra.Game
{
  public interface ITabletPageBase
  {
    void OpenWindow();

    void CloseWindow();

    TabletPage.Pages GetPageType();
  }
}
