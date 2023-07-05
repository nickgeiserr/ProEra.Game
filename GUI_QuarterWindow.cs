// Decompiled with JetBrains decompiler
// Type: GUI_QuarterWindow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_QuarterWindow : MonoBehaviour, IQuarterWindow
{
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image awayLogo;
  [SerializeField]
  private Image awayBackground;
  [SerializeField]
  private Image homeLogo;
  [SerializeField]
  private Image homeBackground;
  [SerializeField]
  private TextMeshProUGUI homeTeam_Txt;
  [SerializeField]
  private TextMeshProUGUI awayTeam_Txt;
  [SerializeField]
  private TextMeshProUGUI homeScore;
  [SerializeField]
  private TextMeshProUGUI awayScore;
  [SerializeField]
  private TextMeshProUGUI announcementText;

  private void Awake() => ProEra.Game.Sources.UI.Quarter = (IQuarterWindow) this;

  public void Init()
  {
    this.homeLogo.sprite = PersistentData.GetHomeLargeLogo();
    this.homeBackground.color = PersistentData.GetHomeBackgroundColor();
    this.awayLogo.sprite = PersistentData.GetAwayLargeLogo();
    this.awayBackground.color = PersistentData.GetAwayBackgroundColor();
    this.homeTeam_Txt.text = PersistentData.GetHomeTeamCity();
    this.awayTeam_Txt.text = PersistentData.GetAwayTeamCity();
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void ShowWindow(string announcement, int quarter)
  {
    ProEra.Game.Sources.UI.KickMeter.HideWindow();
    ProEra.Game.Sources.UI.PopupStats.HideWindow();
    this.homeScore.text = ProEra.Game.MatchState.Stats.GetHomeScore().ToString();
    this.awayScore.text = ProEra.Game.MatchState.Stats.GetAwayScore().ToString();
    this.announcementText.text = announcement;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    ScoreClockState.Quarter.Value = MatchManager.instance.timeManager.GetQuarterString();
    this.StartCoroutine(this.ShowWindowBriefly(quarter));
  }

  private IEnumerator ShowWindowBriefly(int quarter)
  {
    ScoreClockState.DownAndDistanceVisible.SetValue(false);
    yield return (object) new WaitForSeconds(3f);
    this.HideWindow();
    if (quarter == 1)
      MatchManager.instance.HandleEndOfQuarter(1);
    else if (quarter == 3)
      MatchManager.instance.HandleEndOfQuarter(3);
    if (quarter == 4)
    {
      if (global::Game.UserCallsPlays)
        PlaybookState.ShowPlaybook.Trigger();
      else
        MatchManager.instance.playManager.SelectNextPlaysForAI();
    }
  }

  public void HideWindow() => LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
}
