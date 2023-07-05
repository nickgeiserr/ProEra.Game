// Decompiled with JetBrains decompiler
// Type: ProEra.Game.IPlaybookGUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace ProEra.Game
{
  public interface IPlaybookGUI
  {
    void SetPlaybookType(EPlaybookType type);

    void ForcePlayType(EShowPlayType type);

    void Init(Player value);

    void ShowWindow();

    void HideWindow();

    bool IsVisible();

    void SetToOffense();

    void SetToDefense();

    void ForceKickoffPlays();

    void ForceKickReturnPlays();

    void HideFlipPlayButton();

    void HideFormationSelectWindow();

    void ShowPlaySelectWindow();

    void HideSubstitutionButton();

    void SetAudible(bool value);

    void ForceOffFormation(FormationType type);

    void ForceDefFormation(FormationType type);
  }
}
