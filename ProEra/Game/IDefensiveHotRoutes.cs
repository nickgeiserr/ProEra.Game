// Decompiled with JetBrains decompiler
// Type: ProEra.Game.IDefensiveHotRoutes
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace ProEra.Game
{
  public interface IDefensiveHotRoutes
  {
    void ShowAvailablePlayers_DL();

    void ShowAvailablePlayers_LB();

    void ShowAvailablePlayers_DB();

    bool IsSelectingHotRoute();

    void SelectHotRoute(int index);

    bool ShouldCameraZoomOut();

    void HidePlayerSelectButtons();

    void Deactivate();

    void SelectActivePlayer();

    bool IsSelectingPlayer();
  }
}
