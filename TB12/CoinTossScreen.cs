// Decompiled with JetBrains decompiler
// Type: TB12.CoinTossScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using Framework.UI;
using System;
using System.Collections;
using TB12.AppStates;
using TB12.UI;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

namespace TB12
{
  public class CoinTossScreen : UIView
  {
    [SerializeField]
    private UniformLogoStore _store;
    [SerializeField]
    private GameObject _coinFlipView;
    [SerializeField]
    private GameObject _coin;
    [Header("Options Modal")]
    [SerializeField]
    private GameObject _optionsModal;
    [SerializeField]
    private float _autoSelectTime = 8f;
    [SerializeField]
    private TouchButton _leftOptionButton;
    [SerializeField]
    private TouchButton _rightOptionButton;
    [SerializeField]
    private Image _countdownProgressSlider;
    [SerializeField]
    private LocalizeStringEvent _leftOptionText;
    [SerializeField]
    private LocalizeStringEvent _rightOptionText;
    [SerializeField]
    private LocalizeStringEvent _optionsModalTitleText;
    [SerializeField]
    private Color _selectedButtonColor = Color.green;
    [Header("Summary")]
    [SerializeField]
    private GameObject _summaryView;
    [SerializeField]
    private Image _summaryLeftLogo;
    [SerializeField]
    private Image _summaryRightLogo;
    [SerializeField]
    private LocalizeStringEvent _summaryLeftText;
    [SerializeField]
    private LocalizeStringEvent _summaryRightText;
    private CoinTossScreen.TeamInfo _winningTeamInfo;
    private CoinTossScreen.CoinFlipOption _coinFlipPrediction;
    private CoinTossScreen.CoinFlipOption _coinFlipResult;
    private bool _isFlipAnimComplete;
    private Quaternion _defaultCoinRotation = Quaternion.identity;
    [SerializeField]
    private CoinTossScreen.TossTweenInfo _tweenInfo = new CoinTossScreen.TossTweenInfo()
    {
      TossHeight = 150f,
      LoopCount = 14,
      SingleFlipDuration = 0.15f,
      TranslateTweenDuration = 0.5f,
      DefaultDelay = 0.5f,
      PingPongCount = 1
    };
    private CoinTossScreen.TeamInfo _kickingTeamInfo;
    private bool _userIsAway;
    private CoinTossScreen.TeamInfo _homeTeamInfo = new CoinTossScreen.TeamInfo();
    private CoinTossScreen.TeamInfo _awayTeamInfo = new CoinTossScreen.TeamInfo();
    private CoinTossScreen.TeamInfo _userTeamInfo;
    private CoinTossScreen.TeamInfo _aiTeamInfo;
    private RoutineHandle _routineHandle = new RoutineHandle();
    private bool _defenseLeft;
    private EMatchState _matchState;
    private const string LocalizeHeaderSummary = "CoinToss_Header_Summary";
    private const string LocalizeHeaderHeadsOrTails = "CoinToss_Header_HeadsOrTails";
    private const string LocalizeHeaderKickOrRecieve = "CoinToss_Header_KickOrRecieve";
    private const string LocalizeButtonHeads = "CoinToss_Button_Heads";
    private const string LocalizeButtonTails = "CoinToss_Button_Tails";
    private const string LocalizeButtonKick = "CoinToss_Text_Kick";
    private const string LocalizeButtonReceive = "CoinToss_Text_Receive";
    private static readonly int MaxNumberAutoWins = 5;
    private static readonly float UserWinChance = 0.69f;
    private static readonly float TransitionDelay = 1f;
    private static readonly float DelayAfterAutoSelect = 0.5f;
    private static readonly Quaternion CoinFlip180Rotation = Quaternion.Euler(180f, 0.0f, 0.0f);
    private WaitForEndOfFrame _frameWait = new WaitForEndOfFrame();
    private WaitForSeconds _transition = new WaitForSeconds(CoinTossScreen.TransitionDelay);
    private WaitForSeconds _autoSelectDelay = new WaitForSeconds(CoinTossScreen.DelayAfterAutoSelect);

    public override Enum ViewId { get; } = (Enum) EScreens.kCoinToss;

    protected override void WillAppear()
    {
      base.WillAppear();
      this.Init();
      this.StartToss();
    }

    private void Init()
    {
      this._userIsAway = !PersistentData.userIsHome;
      this._homeTeamInfo.TeamIndex = PersistentData.GetHomeTeamIndex();
      this._awayTeamInfo.TeamIndex = PersistentData.GetAwayTeamIndex();
      this._userTeamInfo = this._userIsAway ? this._awayTeamInfo : this._homeTeamInfo;
      this._aiTeamInfo = this._userIsAway ? this._homeTeamInfo : this._awayTeamInfo;
      this._defenseLeft = (double) UnityEngine.Random.value > 0.5;
      this._defaultCoinRotation = this._coin.transform.localRotation;
      int num = this._leftOptionText.StringReference == null ? 0 : (this._rightOptionText.StringReference != null ? 1 : 0);
    }

    protected override void WillDisappear()
    {
      base.WillDisappear();
      this._routineHandle.Stop();
      AppState.InCoinToss = false;
      PersistentSingleton<StateManager<EAppState, GameState>>.Instance.OnCameraFadeComplete -= new System.Action(this.StartToss);
    }

    private void OnDestroy() => AppState.InCoinToss = false;

    private void StartToss() => this._routineHandle.Run(this.RunCoinToss());

    private IEnumerator RunCoinToss()
    {
      CoinTossScreen coinTossScreen = this;
      AppState.InCoinToss = true;
      coinTossScreen.ShowHeadsOrTailsSelection();
      coinTossScreen.StartTossAnimation();
      bool autoSelectPrediction = !coinTossScreen._userIsAway;
      if (autoSelectPrediction)
      {
        coinTossScreen.AutoSelectPrediction();
        yield return (object) coinTossScreen._autoSelectDelay;
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        yield return (object) coinTossScreen.RunTimerWithCondition(coinTossScreen._autoSelectTime, new Func<bool>(coinTossScreen.\u003CRunCoinToss\u003Eb__59_0));
        if (coinTossScreen._coinFlipPrediction == CoinTossScreen.CoinFlipOption.Invalid)
          coinTossScreen._leftOptionButton.SimulateClick();
      }
      coinTossScreen._leftOptionButton.onClick -= new System.Action(coinTossScreen.OnHeadsSelected);
      coinTossScreen._rightOptionButton.onClick -= new System.Action(coinTossScreen.OnTailsSelected);
      coinTossScreen._coinFlipResult = coinTossScreen.GetCoinFlipResult();
      if (coinTossScreen._coinFlipResult == CoinTossScreen.CoinFlipOption.Heads)
        coinTossScreen._tweenInfo.RotateTween.optional.origRotation = coinTossScreen._defaultCoinRotation * CoinTossScreen.CoinFlip180Rotation;
      coinTossScreen.SetOptionsPanelActive(false);
      if (autoSelectPrediction)
      {
        while (!coinTossScreen._isFlipAnimComplete)
          yield return (object) coinTossScreen._frameWait;
      }
      else
        coinTossScreen.ShowFlipResult();
      yield return (object) coinTossScreen._transition;
      coinTossScreen._coinFlipView.SetActive(false);
      coinTossScreen._winningTeamInfo = coinTossScreen._coinFlipResult == coinTossScreen._coinFlipPrediction ? coinTossScreen._awayTeamInfo : coinTossScreen._homeTeamInfo;
      coinTossScreen._winningTeamInfo.WonToss = true;
      if (coinTossScreen._winningTeamInfo == coinTossScreen._userTeamInfo)
      {
        coinTossScreen.ShowKickOrReceiveSelection();
        // ISSUE: reference to a compiler-generated method
        yield return (object) coinTossScreen.RunTimerWithCondition(coinTossScreen._autoSelectTime, new Func<bool>(coinTossScreen.\u003CRunCoinToss\u003Eb__59_1));
        coinTossScreen._leftOptionButton.onClick -= new System.Action(coinTossScreen.OnKickSelected);
        coinTossScreen._rightOptionButton.onClick -= new System.Action(coinTossScreen.OnReceiveSelected);
        bool autoSelectChoice = coinTossScreen._userTeamInfo.KickOrReceiveSelection == CoinTossScreen.KickReceiveOption.Invalid;
        coinTossScreen.SetupKickoffChoices(autoSelectChoice);
        if (autoSelectChoice)
          yield return (object) coinTossScreen._autoSelectDelay;
        coinTossScreen.SetOptionsPanelActive(false);
        yield return (object) coinTossScreen._transition;
      }
      else
        coinTossScreen.SetAiKickRecieveSelection();
      coinTossScreen.ShowSummary();
      yield return (object) coinTossScreen._transition;
      MatchManager.instance.StartFromCoinFlip(coinTossScreen._defenseLeft, coinTossScreen._matchState);
      yield return (object) new WaitForSeconds(0.1f);
      UIDispatch.FrontScreen.CloseScreen();
      AppState.InCoinToss = false;
    }

    private void ShowHeadsOrTailsSelection()
    {
      this._leftOptionButton.onClick += new System.Action(this.OnHeadsSelected);
      this._rightOptionButton.onClick += new System.Action(this.OnTailsSelected);
      this.SetOptionsText("CoinToss_Header_HeadsOrTails", "CoinToss_Button_Heads", "CoinToss_Button_Tails");
      this.SetOptionsPanelActive(true);
    }

    private void AutoSelectPrediction()
    {
      this._coinFlipPrediction = (double) UnityEngine.Random.value > 0.5 ? CoinTossScreen.CoinFlipOption.Heads : CoinTossScreen.CoinFlipOption.Tails;
      if (this._coinFlipPrediction == CoinTossScreen.CoinFlipOption.Heads)
        this._leftOptionButton.SimulateClick();
      else
        this._rightOptionButton.SimulateClick();
    }

    private void ShowKickOrReceiveSelection()
    {
      this._leftOptionButton.onClick += new System.Action(this.OnKickSelected);
      this._rightOptionButton.onClick += new System.Action(this.OnReceiveSelected);
      this.SetOptionsText("CoinToss_Header_KickOrRecieve", "CoinToss_Text_Kick", "CoinToss_Text_Receive");
      this.SetOptionsPanelActive(true);
      this.SetOptionModalInteractable(true);
    }

    private void SetupKickoffChoices(bool autoSelectChoice)
    {
      if (autoSelectChoice || this._userTeamInfo.KickOrReceiveSelection == CoinTossScreen.KickReceiveOption.Receive)
      {
        if (autoSelectChoice)
        {
          this._userTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Receive;
          this._aiTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Kick;
          this._leftOptionButton.SimulateClick();
        }
        this._matchState = EMatchState.UserOnDefense;
        this._kickingTeamInfo = this._aiTeamInfo;
      }
      else
      {
        this._matchState = EMatchState.UserOnOffense;
        this._kickingTeamInfo = this._userTeamInfo;
        this._aiTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Receive;
      }
    }

    private void SetAiKickRecieveSelection()
    {
      this._winningTeamInfo.KickOrReceiveSelection = (double) UnityEngine.Random.value < (double) CoinTossScreen.UserWinChance ? CoinTossScreen.KickReceiveOption.Kick : CoinTossScreen.KickReceiveOption.Receive;
      if (this._winningTeamInfo.KickOrReceiveSelection == CoinTossScreen.KickReceiveOption.Kick)
      {
        this._kickingTeamInfo = this._winningTeamInfo;
        this._userTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Receive;
      }
      else
      {
        this._kickingTeamInfo = this._userTeamInfo;
        this._userTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Kick;
      }
      this._matchState = this._winningTeamInfo.KickOrReceiveSelection == CoinTossScreen.KickReceiveOption.Kick ? EMatchState.UserOnDefense : EMatchState.UserOnOffense;
    }

    private void ShowSummary()
    {
      if (!((UnityEngine.Object) this._summaryView != (UnityEngine.Object) null))
        return;
      this._summaryView.SetActive(true);
      Sprite teamLogo1 = this._store.GetUniformLogo(this._kickingTeamInfo.TeamIndex)?.teamLogo;
      Sprite teamLogo2 = this._store.GetUniformLogo(this._kickingTeamInfo == this._userTeamInfo ? this._aiTeamInfo.TeamIndex : this._userTeamInfo.TeamIndex)?.teamLogo;
      if (this._defenseLeft)
      {
        this.SetLocalizedSummaryTexts("CoinToss_Text_Receive", "CoinToss_Text_Kick");
        this.SetSummaryLogos(teamLogo2, teamLogo1);
      }
      else
      {
        this.SetLocalizedSummaryTexts("CoinToss_Text_Kick", "CoinToss_Text_Receive");
        this.SetSummaryLogos(teamLogo1, teamLogo2);
      }
    }

    private void SetSummaryLogos(Sprite left, Sprite right)
    {
      this._summaryLeftLogo.sprite = left;
      this._summaryRightLogo.sprite = right;
    }

    private void SetLocalizedSummaryTexts(string tableEntryLeft, string tableEntryRight)
    {
      this._summaryLeftText.StringReference.TableEntryReference = (TableEntryReference) tableEntryLeft;
      this._summaryRightText.StringReference.TableEntryReference = (TableEntryReference) tableEntryRight;
    }

    private void OnFlipAnimationComplete() => this._isFlipAnimComplete = true;

    private void ShowFlipResult()
    {
      if (this._tweenInfo.RotateTween != null)
        LeanTween.cancel(this._tweenInfo.RotateTween.id);
      if (this._tweenInfo.TranslateTween != null)
        LeanTween.cancel(this._tweenInfo.TranslateTween.id);
      if (this._coinFlipResult == CoinTossScreen.CoinFlipOption.Tails)
        this._coin.transform.localRotation = this._defaultCoinRotation * CoinTossScreen.CoinFlip180Rotation;
      else
        this._coin.transform.localRotation = this._defaultCoinRotation;
    }

    private void StartTossAnimation()
    {
      this._coinFlipView.SetActive(true);
      this._summaryView.SetActive(false);
      int num = this._tweenInfo.LoopCount;
      if (this._userIsAway)
        num = Mathf.Max((int) Mathf.Ceil(this._autoSelectTime / this._tweenInfo.SingleFlipDuration), this._tweenInfo.LoopCount);
      LTDescr ltDescr1 = LeanTween.rotateAroundLocal(this._coin, this._coin.transform.right, 180f, this._tweenInfo.SingleFlipDuration);
      ltDescr1.loopCount = num;
      ltDescr1.setLoopType(LeanTweenType.linear);
      ltDescr1.setOnComplete(new System.Action(this.OnFlipAnimationComplete));
      ltDescr1.setDelay(this._tweenInfo.DefaultDelay);
      this._tweenInfo.RotateTween = ltDescr1;
      LTDescr ltDescr2 = LeanTween.moveLocal(this._coin.transform.parent.gameObject, this._coin.transform.parent.localPosition + Vector3.up * this._tweenInfo.TossHeight, (float) ((double) this._tweenInfo.LoopCount * (double) this._tweenInfo.SingleFlipDuration / 2.0));
      ltDescr2.setLoopPingPong(this._tweenInfo.PingPongCount);
      ltDescr2.setDelay(this._tweenInfo.DefaultDelay);
      this._tweenInfo.TranslateTween = ltDescr2;
    }

    private CoinTossScreen.CoinFlipOption GetCoinFlipResult()
    {
      CoinTossScreen.CoinFlipOption coinFlipOption = this._coinFlipPrediction == CoinTossScreen.CoinFlipOption.Heads ? CoinTossScreen.CoinFlipOption.Tails : CoinTossScreen.CoinFlipOption.Heads;
      return SeasonModeManager.self.CareerGames > CoinTossScreen.MaxNumberAutoWins ? ((double) UnityEngine.Random.value >= (double) CoinTossScreen.UserWinChance ? coinFlipOption : this._coinFlipPrediction) : (this._userIsAway ? this._coinFlipPrediction : coinFlipOption);
    }

    private void OnHeadsSelected() => this._coinFlipPrediction = CoinTossScreen.CoinFlipOption.Heads;

    private void OnTailsSelected() => this._coinFlipPrediction = CoinTossScreen.CoinFlipOption.Tails;

    public void OnKickSelected() => this._userTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Kick;

    public void OnReceiveSelected() => this._userTeamInfo.KickOrReceiveSelection = CoinTossScreen.KickReceiveOption.Receive;

    private void SetOptionsText(string titleText, string leftText, string rightText)
    {
      this._optionsModalTitleText.StringReference.TableEntryReference = (TableEntryReference) titleText;
      this._leftOptionText.StringReference.TableEntryReference = (TableEntryReference) leftText;
      this._rightOptionText.StringReference.TableEntryReference = (TableEntryReference) rightText;
    }

    private void SetOptionsPanelActive(bool isActive)
    {
      if (!((UnityEngine.Object) this._optionsModal != (UnityEngine.Object) null))
        return;
      this._optionsModal.SetActive(isActive);
      if (!isActive)
        return;
      if (this._leftOptionButton.IsInitialized)
        this.ClearButtonHighlight(this._leftOptionButton.gameObject);
      if (!this._rightOptionButton.IsInitialized)
        return;
      this.ClearButtonHighlight(this._rightOptionButton.gameObject);
    }

    private void ClearButtonHighlight(GameObject button)
    {
      TouchButtonHoverHandler component = button.GetComponent<TouchButtonHoverHandler>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.ForceClearHighlight();
    }

    public void SetOptionModalInteractable(bool _isInteractable)
    {
      this._leftOptionButton.SetInteractible(_isInteractable);
      this._rightOptionButton.SetInteractible(_isInteractable);
    }

    private IEnumerator RunTimerWithCondition(float duration, Func<bool> condition)
    {
      float timeRemaining = duration;
      while ((double) timeRemaining > 0.0 && condition())
      {
        this.UpdateSelectionTimer(timeRemaining / duration);
        timeRemaining -= Time.deltaTime;
        yield return (object) this._frameWait;
      }
    }

    private void UpdateSelectionTimer(float fillAmount)
    {
      if (!((UnityEngine.Object) this._countdownProgressSlider != (UnityEngine.Object) null))
        return;
      this._countdownProgressSlider.fillAmount = fillAmount;
    }

    private enum CoinFlipOption
    {
      Invalid,
      Heads,
      Tails,
    }

    private enum KickReceiveOption
    {
      Invalid,
      Kick,
      Receive,
    }

    private sealed class TeamInfo
    {
      public int TeamIndex = -1;
      public bool WonToss;
      public CoinTossScreen.KickReceiveOption KickOrReceiveSelection;
    }

    [Serializable]
    private struct TossTweenInfo
    {
      public float TossHeight;
      public int LoopCount;
      public float SingleFlipDuration;
      public float TranslateTweenDuration;
      public float DefaultDelay;
      public int PingPongCount;
      public LTDescr RotateTween;
      public LTDescr TranslateTween;
    }
  }
}
