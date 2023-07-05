// Decompiled with JetBrains decompiler
// Type: AudibleManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using ProEra.Game.Sources;
using UnityEngine;

public class AudibleManager : MonoBehaviour, IAudibleManager
{
  private IPlaybookGUI playbook;
  private IPlaybookGUI playbookP2;

  private void Awake() => UI.AudibleManager = (IAudibleManager) this;

  public void Init()
  {
  }

  public void ShowAudibles(Player player)
  {
    Globals.PauseGame.SetValue(true);
    MatchManager.instance.DisallowSnap();
    if (player == Player.One)
    {
      if (ProEra.Game.MatchState.IsPlayerOneOnOffense)
        this.playbook.HideFlipPlayButton();
      this.playbook.SetAudible(true);
      PlaybookState.ShowPlaybook.Trigger();
      this.playbook.HideFormationSelectWindow();
      this.playbook.ShowPlaySelectWindow();
      this.playbook.HideSubstitutionButton();
    }
    else
    {
      if (ProEra.Game.MatchState.IsPlayerTwoOnOffense)
        this.playbookP2.HideFlipPlayButton();
      this.playbookP2.SetAudible(true);
      this.playbookP2.ShowWindow();
      this.playbookP2.HideFormationSelectWindow();
      this.playbookP2.ShowPlaySelectWindow();
      this.playbookP2.HideSubstitutionButton();
    }
  }

  public void HideAudibles(Player player)
  {
    switch (player)
    {
      case Player.One:
        PlaybookState.HidePlaybook.Trigger();
        break;
      case Player.Two:
        this.playbookP2.HideWindow();
        break;
    }
    MatchManager.instance.AllowSnap();
    Globals.PauseGame.SetValue(false);
  }
}
