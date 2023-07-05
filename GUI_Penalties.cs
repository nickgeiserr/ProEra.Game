// Decompiled with JetBrains decompiler
// Type: GUI_Penalties
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using ProEra.Game;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Penalties : MonoBehaviour, IPenaltiesGUI
{
  [Header("Main Window")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [Header("Penalty Information")]
  [SerializeField]
  private TextMeshProUGUI penaltyCommitted_Txt;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerNumber_Txt;
  [SerializeField]
  private TextMeshProUGUI playerPosition_Txt;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private Image infoLogoBackground_Img;
  [SerializeField]
  private Image infoLogo_Img;
  [SerializeField]
  private TextMeshProUGUI waiting_Txt;
  [Header("Penalty Decision")]
  [SerializeField]
  private TextMeshProUGUI penaltySummary_Txt;
  [SerializeField]
  private UnityEngine.UI.Button accept_Btn;
  [SerializeField]
  private UnityEngine.UI.Button decline_Btn;
  [SerializeField]
  private Animator accept_Ani;
  [SerializeField]
  private Animator decline_Ani;
  [SerializeField]
  private TextMeshProUGUI acceptDownAndDistance_Txt;
  [SerializeField]
  private TextMeshProUGUI declineDownAndDistance_Txt;
  [SerializeField]
  private Image decideLogoBackground_Img;
  [SerializeField]
  private Image decideLogo_Img;
  [SerializeField]
  private TextMeshProUGUI choose_Txt;
  private Penalty penalty;
  private bool userDecides;
  private PlayerData penalizedPlayer;
  private int decidingTeamIndex;
  private WaitForSeconds aiDecideDelay_WFS;
  private WaitForSeconds oneSecond_WFS;

  private void Awake() => ProEra.Game.Sources.UI.PenaltyGUI = (IPenaltiesGUI) this;

  public void Init()
  {
    this.aiDecideDelay_WFS = new WaitForSeconds(2.5f);
    this.oneSecond_WFS = new WaitForSeconds(1f);
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void ShowWindow(Penalty penaltyOnPlay, bool userCanDecide)
  {
    this.penalty = penaltyOnPlay;
    this.userDecides = userCanDecide;
    this.penalizedPlayer = this.penalty.GetPenalizedPlayer();
    MatchManager.instance.timeManager.SetRunPlayClock(false);
    MatchManager.instance.timeManager.ResetPlayClock();
    ScoreClockState.PenaltyVisible.Value = true;
    ProEra.Game.Sources.UI.PrePlayWindowP1.HideWindow();
    ProEra.Game.Sources.UI.PrePlayWindowP2.HideWindow();
    PlaybookState.HidePlaybook.Trigger();
    ProEra.Game.Sources.UI.PlaybookP2.HideWindow();
    ProEra.Game.Sources.UI.PopupStats.HideWindow();
    this.StartCoroutine(ProEra.Game.Sources.UI.HotRouteManager.SetIsSelectingReceiver(false));
    this.SetInformationWindow();
    this.SetDecisionWindow();
    this.mainWindow_CG.alpha = 1f;
    this.mainWindow_CG.blocksRaycasts = true;
  }

  public IEnumerator ShowPenalty(float showInBarWaitTime)
  {
    this.penalty = SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay();
    if (this.penalty.GetPenaltyType() != PenaltyType.None)
    {
      MatchManager.instance.ResetPlay();
      if (this.penalty.GetPenaltyTime() == PenaltyTime.PrePlay)
      {
        ScoreClockState.PenaltyVisible.Value = true;
        yield return (object) new WaitForSeconds(showInBarWaitTime);
        if (global::Game.Is2PMatch || global::Game.UserCallsPlays && this.penalty.GetTeamIndex() != PersistentData.GetUserTeamIndex())
          this.ShowWindow(this.penalty, false);
        else
          this.ShowWindow(this.penalty, false);
      }
    }
  }

  public void HideWindow()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    ScoreClockState.PenaltyVisible.Value = false;
    MatchManager.instance.timeManager.ResetPlayClock();
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha == 1.0;

  private void SetInformationWindow()
  {
    this.penaltyCommitted_Txt.text = this.penalty.GetPenaltyText().ToUpper() + " " + System.Math.Abs(this.penalty.GetPenaltyYards()).ToString() + " YARDS - AGAINST THE " + this.penalty.GetOffenseOrDefense().ToUpper();
    this.playerPortrait_Img.sprite = PortraitManager.self.GetPlayerPortrait(this.penalizedPlayer, this.penalizedPlayer.OnUserTeam);
    this.playerName_Txt.text = this.penalizedPlayer.FullName.ToUpper();
    this.playerNumber_Txt.text = this.penalizedPlayer.Number.ToString();
    this.playerPosition_Txt.text = this.penalizedPlayer.PlayerPosition.ToString();
    if (this.penalty.GetTeamIndex() == PersistentData.GetHomeTeamIndex())
    {
      this.infoLogoBackground_Img.color = PersistentData.GetHomeBackgroundColor();
      this.infoLogo_Img.sprite = PersistentData.GetHomeMediumLogo();
      this.waiting_Txt.text = PersistentData.GetHomeTeamAbbreviation() + " WAITING";
    }
    else
    {
      this.infoLogoBackground_Img.color = PersistentData.GetAwayBackgroundColor();
      this.infoLogo_Img.sprite = PersistentData.GetAwayMediumLogo();
      this.waiting_Txt.text = PersistentData.GetAwayTeamAbbreviation() + " WAITING";
    }
  }

  private void SetDecisionWindow()
  {
    this.accept_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    this.decline_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    int newDown = 0;
    switch (this.penalty.GetPenaltyDownResult())
    {
      case PenaltyDownResult.Repeat:
        newDown = MatchManager.instance.beforePlay.Down;
        break;
      case PenaltyDownResult.LossOfDown:
        newDown = MatchManager.instance.beforePlay.Down + 1;
        break;
      case PenaltyDownResult.FirstDown:
        newDown = 1;
        break;
    }
    this.penaltySummary_Txt.text = this.penalty.GetOffenseOrDefense().ToUpper() + " - " + System.Math.Abs(this.penalty.GetPenaltyYards()).ToString() + " YARD PENALTY";
    this.acceptDownAndDistance_Txt.text = MatchManager.instance.GetAcceptPenaltyDownAndDistance(this.penalty, newDown);
    this.declineDownAndDistance_Txt.text = MatchManager.instance.GetDeclinePenaltyDownAndDistance();
    if (this.penalty.GetTeamIndex() == PersistentData.GetHomeTeamIndex())
    {
      this.decideLogoBackground_Img.color = PersistentData.GetAwayBackgroundColor();
      this.decideLogo_Img.sprite = PersistentData.GetAwayMediumLogo();
      this.choose_Txt.text = PersistentData.GetAwayTeamAbbreviation() + " CHOOSE";
    }
    else
    {
      this.decideLogoBackground_Img.color = PersistentData.GetHomeBackgroundColor();
      this.decideLogo_Img.sprite = PersistentData.GetHomeMediumLogo();
      this.choose_Txt.text = PersistentData.GetHomeTeamAbbreviation() + " CHOOSE";
    }
    this.decidingTeamIndex = this.penalty.GetTeamIndex() == PersistentData.GetHomeTeamIndex() ? PersistentData.GetAwayTeamIndex() : PersistentData.GetHomeTeamIndex();
    if (this.userDecides)
    {
      this.accept_Btn.interactable = true;
      this.decline_Btn.interactable = true;
      if (!ControllerManagerGame.usingController)
        return;
      this.accept_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else
    {
      this.accept_Btn.interactable = false;
      this.decline_Btn.interactable = false;
      this.StartCoroutine(this.DecideForAI());
    }
  }

  private void Update() => this.ManageControllerInput();

  public IEnumerator DecideForAI()
  {
    yield return (object) this.aiDecideDelay_WFS;
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.DecidePenaltyForAI(this.decidingTeamIndex))
    {
      this.accept_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      yield return (object) this.oneSecond_WFS;
      this.AcceptPenalty();
    }
    else
    {
      this.decline_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      yield return (object) this.oneSecond_WFS;
      this.DeclinePenalty();
    }
  }

  public void AcceptPenalty()
  {
    SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted = true;
    this.HideWindow();
    MatchManager.instance.checkForEndOfQuarter = false;
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyTime() != PenaltyTime.PrePlay)
      return;
    MatchManager.instance.playManager.PlayActive = false;
    PlayState.PlayOver.Value = true;
    MatchManager.instance.playManager.playIsCleanedUp = false;
    MatchManager.instance.EndPlay(PlayEndType.PrePlayPenalty);
  }

  public void DeclinePenalty()
  {
    SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.isPenaltyAccepted = false;
    this.HideWindow();
    MatchManager.instance.checkForEndOfQuarter = false;
    if (SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.GetPenaltyOnPlay().GetPenaltyTime() != PenaltyTime.PrePlay)
      return;
    MatchManager.instance.playManager.PlayActive = false;
    PlayState.PlayOver.Value = true;
    MatchManager.instance.playManager.playIsCleanedUp = false;
    MatchManager.instance.EndPlay(PlayEndType.PrePlayPenalty);
  }

  public void ShowPenaltyInScoreClock()
  {
  }

  public void HidePenaltyInScoreClock()
  {
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || !ControllerManagerGame.usingController || global::Game.HasScreenOverlay || !this.userDecides || global::Game.IsSpectateMode)
      return;
    Player userIndex = !SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.IsAgainstPlayer2() ? Player.Two : Player.One;
    float num = UserManager.instance.LeftStickY(userIndex);
    if ((double) num < -0.40000000596046448 && !ControllerManagerGame.IsUIElementSelected(this.decline_Ani))
    {
      this.accept_Ani.SetTrigger(HashIDs.self.normal_Trigger);
      this.decline_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
    }
    else if ((double) num > 0.40000000596046448 && !ControllerManagerGame.IsUIElementSelected(this.accept_Ani))
    {
      this.accept_Ani.SetTrigger(HashIDs.self.highlighted_Trigger);
      this.decline_Ani.SetTrigger(HashIDs.self.normal_Trigger);
    }
    if (!UserManager.instance.Action1WasPressed(userIndex))
      return;
    if (ControllerManagerGame.IsUIElementSelected(this.accept_Ani))
    {
      this.AcceptPenalty();
    }
    else
    {
      if (!ControllerManagerGame.IsUIElementSelected(this.decline_Ani))
        return;
      this.DeclinePenalty();
    }
  }
}
