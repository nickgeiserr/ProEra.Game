// Decompiled with JetBrains decompiler
// Type: ScoreclockManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class ScoreclockManager : MonoBehaviour, IScoreClockManager
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image homeLogo_Img;
  [SerializeField]
  private Image awayLogo_Img;
  [SerializeField]
  private Image homeBackground_Img;
  [SerializeField]
  private Image awayBackground_Img;
  [SerializeField]
  private Image[] homeTimeouts_Img;
  [SerializeField]
  private Image[] awayTimeouts_Img;
  [SerializeField]
  private GameObject homePossessionIndicator_GO;
  [SerializeField]
  private GameObject awayPossessionIndicator_GO;
  [SerializeField]
  private TextMeshProUGUI homeScore_Txt;
  [SerializeField]
  private TextMeshProUGUI awayScore_Txt;
  [SerializeField]
  private TextMeshProUGUI homeTeam_Txt;
  [SerializeField]
  private TextMeshProUGUI awayTeam_Txt;
  [SerializeField]
  private TextMeshProUGUI gameClock_Txt;
  [SerializeField]
  private TextMeshProUGUI quarter_Txt;
  private bool runGameClock;
  private float gameClockTime;
  private float gameClockLastDown;
  [Header("Play Clock")]
  [SerializeField]
  private GameObject homePlayClock_GO;
  [SerializeField]
  private GameObject awayPlayClock_GO;
  [SerializeField]
  private TextMeshProUGUI homePlayClock_Txt;
  [SerializeField]
  private TextMeshProUGUI awayPlayClock_Txt;
  [Header("Down and Distance / Personnel")]
  [SerializeField]
  private CanvasGroup homeDownAndDistanceParent_CG;
  [SerializeField]
  private CanvasGroup awayDownAndDistanceParent_CG;
  [SerializeField]
  private CanvasGroup homeDownAndDistance_CG;
  [SerializeField]
  private CanvasGroup awayDownAndDistance_CG;
  [SerializeField]
  private Image homeForwardArrow_Img;
  [SerializeField]
  private Image homeBackwardArrow_Img;
  [SerializeField]
  private Image awayForwardArrow_Img;
  [SerializeField]
  private Image awayBackwardArrow_Img;
  [SerializeField]
  private TextMeshProUGUI homeYardline_Txt;
  [SerializeField]
  private TextMeshProUGUI awayYardline_Txt;
  [SerializeField]
  private TextMeshProUGUI homeDownAndDistance_Txt;
  [SerializeField]
  private TextMeshProUGUI awayDownAndDistance_Txt;
  [SerializeField]
  private CanvasGroup homePersonnel_CG;
  [SerializeField]
  private CanvasGroup awayPersonnel_CG;
  [SerializeField]
  private TextMeshProUGUI homePersonnel_Txt;
  [SerializeField]
  private TextMeshProUGUI awayPersonnel_Txt;
  [Header("Timeout")]
  [SerializeField]
  private CanvasGroup homeTimeout_CG;
  [SerializeField]
  private CanvasGroup awayTimeout_CG;
  [SerializeField]
  private Image homeTimeout_Img;
  [SerializeField]
  private Image awayTimeout_Img;
  [Header("Penalty")]
  [SerializeField]
  private CanvasGroup homePenalty_CG;
  [SerializeField]
  private CanvasGroup awayPenalty_CG;
  [Header("Score Popup")]
  [SerializeField]
  private CanvasGroup homeScorePopup_CG;
  [SerializeField]
  private CanvasGroup awayScorePopup_CG;
  [SerializeField]
  private Image homeScorePopupBG_Img;
  [SerializeField]
  private Image awayScorePopupBG_Img;
  [SerializeField]
  private TextMeshProUGUI homeScorePopup_Txt;
  [SerializeField]
  private TextMeshProUGUI awayScorePopup_Txt;
  private WaitForSeconds scorePopup_WFS;

  private void Awake() => ProEra.Game.Sources.UI.ScoreClock = (IScoreClockManager) this;

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.SetQuarter();
    this.HidePlayClock();
    this.HideScorePopups();
    this.HideDownAndDistanceParent();
    this.UpdateCompTeamScore();
    this.UpdateUserTeamScore();
    this.homeBackground_Img.color = PersistentData.GetHomeBackgroundColor();
    this.awayBackground_Img.color = PersistentData.GetAwayBackgroundColor();
    this.homeScorePopupBG_Img.color = PersistentData.GetHomeBackgroundColor();
    this.awayScorePopupBG_Img.color = PersistentData.GetAwayBackgroundColor();
    this.homeTimeout_Img.color = PersistentData.GetHomeBackgroundColor();
    this.awayTimeout_Img.color = PersistentData.GetAwayBackgroundColor();
    this.homeLogo_Img.sprite = PersistentData.GetHomeTeamData().GetMediumLogo();
    this.awayLogo_Img.sprite = PersistentData.GetAwayTeamData().GetMediumLogo();
    this.homeTeam_Txt.text = PersistentData.GetHomeTeamAbbreviation();
    this.awayTeam_Txt.text = PersistentData.GetAwayTeamAbbreviation();
    this.scorePopup_WFS = new WaitForSeconds(3f);
  }

  public void SetMatchState()
  {
    this.homePossessionIndicator_GO.SetActive(false);
    this.awayPossessionIndicator_GO.SetActive(false);
    if (MatchManager.instance.currentMatchState == EMatchState.UserOnOffense)
    {
      if (PersistentData.userIsHome)
        this.homePossessionIndicator_GO.SetActive(true);
      else
        this.awayPossessionIndicator_GO.SetActive(true);
    }
    else
    {
      if (MatchManager.instance.currentMatchState != EMatchState.UserOnDefense)
        return;
      if (PersistentData.userIsHome)
        this.awayPossessionIndicator_GO.SetActive(true);
      else
        this.homePossessionIndicator_GO.SetActive(true);
    }
  }

  public void ShowWindow() => LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);

  public void HideWindow() => LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);

  public void EnableAllTimeouts()
  {
    for (int index = 0; index < this.homeTimeouts_Img.Length; ++index)
    {
      this.homeTimeouts_Img[index].enabled = true;
      this.awayTimeouts_Img[index].enabled = true;
    }
  }

  public void SetUserTimeouts(int timeoutsUsed)
  {
    if (PersistentData.userIsHome)
      this.homeTimeouts_Img[timeoutsUsed].enabled = false;
    else
      this.awayTimeouts_Img[timeoutsUsed].enabled = false;
  }

  public void SetCompTimeouts(int timeoutsUsed)
  {
    if (PersistentData.userIsHome)
      this.awayTimeouts_Img[timeoutsUsed].enabled = false;
    else
      this.homeTimeouts_Img[timeoutsUsed].enabled = false;
  }

  public void ShowTimeout(bool userCalledTimeout)
  {
    this.HideDownAndDistanceParent();
    if (PersistentData.userIsHome)
    {
      if (userCalledTimeout)
        LeanTween.alphaCanvas(this.homeTimeout_CG, 1f, 0.3f);
      else
        LeanTween.alphaCanvas(this.awayTimeout_CG, 1f, 0.3f);
    }
    else if (userCalledTimeout)
      LeanTween.alphaCanvas(this.awayTimeout_CG, 1f, 0.3f);
    else
      LeanTween.alphaCanvas(this.homeTimeout_CG, 1f, 0.3f);
    this.StartCoroutine(this.HideTimeoutCoroutine());
  }

  private IEnumerator HideTimeoutCoroutine()
  {
    yield return (object) this.scorePopup_WFS;
    this.HideTimeout();
  }

  public void HideTimeout()
  {
    LeanTween.alphaCanvas(this.homeTimeout_CG, 0.0f, 0.3f);
    LeanTween.alphaCanvas(this.awayTimeout_CG, 0.0f, 0.3f);
  }

  public void ShowPenalty()
  {
    this.HideDownAndDistanceParent();
    if (global::Game.IsHomeTeamOnOffense)
      LeanTween.alphaCanvas(this.homePenalty_CG, 1f, 0.3f);
    else
      LeanTween.alphaCanvas(this.awayPenalty_CG, 1f, 0.3f);
  }

  public void HidePenalty()
  {
    LeanTween.alphaCanvas(this.homePenalty_CG, 0.0f, 0.3f);
    LeanTween.alphaCanvas(this.awayPenalty_CG, 0.0f, 0.3f);
  }

  public void UpdateCompTeamScore()
  {
    if (PersistentData.userIsHome)
      this.awayScore_Txt.text = ProEra.Game.MatchState.Stats.Comp.Score.ToString();
    else
      this.homeScore_Txt.text = ProEra.Game.MatchState.Stats.Comp.Score.ToString();
  }

  public void UpdateUserTeamScore()
  {
    if (PersistentData.userIsHome)
      this.homeScore_Txt.text = ProEra.Game.MatchState.Stats.User.Score.ToString();
    else
      this.awayScore_Txt.text = ProEra.Game.MatchState.Stats.User.Score.ToString();
  }

  public void ShowScorePopup(bool showForUserTeam, string message) => this.StartCoroutine(this.ShowScorePopupCoroutine(showForUserTeam, message));

  private IEnumerator ShowScorePopupCoroutine(bool showForUserTeam, string message)
  {
    if (showForUserTeam)
    {
      if (PersistentData.userIsHome)
      {
        this.homeScorePopup_Txt.text = message;
        LeanTween.alphaCanvas(this.homeScorePopup_CG, 1f, 0.3f);
      }
      else
      {
        this.awayScorePopup_Txt.text = message;
        LeanTween.alphaCanvas(this.awayScorePopup_CG, 1f, 0.3f);
      }
    }
    else if (PersistentData.userIsHome)
    {
      this.awayScorePopup_Txt.text = message;
      LeanTween.alphaCanvas(this.awayScorePopup_CG, 1f, 0.3f);
    }
    else
    {
      this.homeScorePopup_Txt.text = message;
      LeanTween.alphaCanvas(this.homeScorePopup_CG, 1f, 0.3f);
    }
    yield return (object) this.scorePopup_WFS;
    this.HideScorePopups();
  }

  public void HideScorePopups()
  {
    LeanTween.alphaCanvas(this.homeScorePopup_CG, 0.0f, 0.3f);
    LeanTween.alphaCanvas(this.awayScorePopup_CG, 0.0f, 0.3f);
  }

  public void DisplayPlayClock()
  {
    string str = ":";
    int playClockTime = MatchManager.instance.timeManager.GetPlayClockTime();
    this.SetPlayClock(playClockTime <= 9 ? str + "0" + playClockTime.ToString() : str + playClockTime.ToString());
  }

  public void SetPlayClock(string v)
  {
    if (global::Game.IsHomeTeamOnOffense)
      this.homePlayClock_Txt.text = v;
    else
      this.awayPlayClock_Txt.text = v;
  }

  public string GetPlayClockString() => global::Game.IsHomeTeamOnOffense ? this.homePlayClock_Txt.text : this.awayPlayClock_Txt.text;

  public void ShowPlayClock()
  {
    if (global::Game.IsHomeTeamOnOffense)
      this.homePlayClock_GO.SetActive(true);
    else
      this.awayPlayClock_GO.SetActive(true);
  }

  public void HidePlayClock()
  {
    this.homePlayClock_GO.SetActive(false);
    this.awayPlayClock_GO.SetActive(false);
  }

  public void SetGameClock(int mins, int secs)
  {
    string str = mins.ToString() + ":";
    this.gameClock_Txt.text = secs <= 9 ? str + "0" + secs.ToString() : str + secs.ToString();
    if (mins > 0 || secs > 0)
      return;
    this.gameClock_Txt.text = "0:00";
  }

  public string GetGameClockString() => this.gameClock_Txt.text;

  public void SetQuarter() => this.quarter_Txt.text = MatchManager.instance.timeManager.GetQuarterString();

  public void SetYardLine()
  {
    float ballOn = MatchManager.ballOn;
    float firstDown = MatchManager.firstDown;
    int down = MatchManager.down;
    if (global::Game.IsRunningPAT && global::Game.PlayIsNotOver)
      this.SetDownAndDistance("PAT");
    else if (global::Game.IsKickoff && global::Game.PlayIsNotOver)
    {
      this.SetDownAndDistance("KICKOFF");
    }
    else
    {
      if (FieldState.IsBallInOpponentTerritory())
        this.ShowForwardArrow();
      else
        this.ShowBackwardArrow();
      float lineByFieldLocation = (float) Field.GetYardLineByFieldLocation(ballOn);
      if ((double) lineByFieldLocation < 1.0)
        this.SetYardLine("IN");
      else
        this.SetYardLine(lineByFieldLocation.ToString());
      float f = Mathf.Abs(firstDown - ballOn) / Field.ONE_YARD;
      string str = !SingletonBehaviour<FieldManager, MonoBehaviour>.instance.firstDownLine.activeInHierarchy || Field.FurtherDownfield(firstDown, Field.OFFENSIVE_GOAL_LINE) ? "GL" : ((double) f >= (double) Field.ONE_YARD ? Mathf.Max(Mathf.Round(f), 1f).ToString() : "IN");
      string v = "1st & " + str;
      switch (down)
      {
        case 2:
          v = "2nd & " + str;
          break;
        case 3:
          v = "3rd & " + str;
          break;
        case 4:
          v = "4th & " + str;
          break;
      }
      this.SetDownAndDistance(v);
    }
  }

  public void SetYardLine(string v)
  {
    if (global::Game.IsHomeTeamOnOffense)
      this.homeYardline_Txt.text = v;
    else
      this.awayYardline_Txt.text = v;
  }

  public void ShowForwardArrow()
  {
    if (global::Game.IsHomeTeamOnOffense)
    {
      this.homeForwardArrow_Img.enabled = true;
      this.homeBackwardArrow_Img.enabled = false;
    }
    else
    {
      this.awayForwardArrow_Img.enabled = true;
      this.awayBackwardArrow_Img.enabled = false;
    }
  }

  public void ShowBackwardArrow()
  {
    if (global::Game.IsHomeTeamOnOffense)
    {
      this.homeForwardArrow_Img.enabled = false;
      this.homeBackwardArrow_Img.enabled = true;
    }
    else
    {
      this.awayForwardArrow_Img.enabled = false;
      this.awayBackwardArrow_Img.enabled = true;
    }
  }

  public void ShowDownAndDistanceParent()
  {
    if (global::Game.IsHomeTeamOnOffense)
      LeanTween.alphaCanvas(this.homeDownAndDistanceParent_CG, 1f, 0.3f);
    else
      LeanTween.alphaCanvas(this.awayDownAndDistanceParent_CG, 1f, 0.3f);
  }

  public void HideDownAndDistanceParent()
  {
    this.homeDownAndDistanceParent_CG.alpha = 0.0f;
    this.awayDownAndDistanceParent_CG.alpha = 0.0f;
  }

  public void SetDownAndDistance(string v)
  {
    this.ResetDownAndDistance();
    this.ResetPersonnel();
    this.homeDownAndDistance_Txt.text = v;
    this.awayDownAndDistance_Txt.text = v;
    if (global::Game.IsHomeTeamOnOffense)
    {
      LeanTween.alphaCanvas(this.homeDownAndDistanceParent_CG, 1f, 0.3f);
      this.homeDownAndDistance_Txt.text = v;
    }
    else
    {
      LeanTween.alphaCanvas(this.awayDownAndDistanceParent_CG, 1f, 0.3f);
      this.awayDownAndDistance_Txt.text = v;
    }
  }

  public void ResetDownAndDistance()
  {
    this.homeDownAndDistance_CG.alpha = 1f;
    this.awayDownAndDistance_CG.alpha = 1f;
  }

  public string GetDownAndDistance() => global::Game.IsHomeTeamOnOffense ? this.homeDownAndDistance_Txt.text : this.awayDownAndDistance_Txt.text;

  public void SetPersonnel(string v)
  {
    if (global::Game.IsHomeTeamOnOffense)
    {
      LeanTween.alphaCanvas(this.homePersonnel_CG, 1f, 0.3f);
      this.homePersonnel_Txt.text = v;
    }
    else
    {
      LeanTween.alphaCanvas(this.awayPersonnel_CG, 1f, 0.3f);
      this.awayPersonnel_Txt.text = v;
    }
  }

  public void ResetPersonnel()
  {
    this.homePersonnel_CG.alpha = 0.0f;
    this.awayPersonnel_CG.alpha = 0.0f;
  }
}
