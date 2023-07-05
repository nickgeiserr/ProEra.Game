// Decompiled with JetBrains decompiler
// Type: MinicampIntroUi
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using Framework.UI;
using System;
using System.Collections.Generic;
using TB12;
using TB12.AppStates;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public class MinicampIntroUi : UIView
{
  [Header("UI Values per Game Mode")]
  [SerializeField]
  private MinicampIntroUi.MinicampUiValues pocketPassing_QbPresence;
  [SerializeField]
  private MinicampIntroUi.MinicampUiValues dimeDropping_PrecisionPassing;
  [SerializeField]
  private MinicampIntroUi.MinicampUiValues rolloutEvents;
  [SerializeField]
  private MinicampIntroUi.MinicampUiValues runAndShoot;
  [SerializeField]
  private MinicampIntroUi.MinicampUiValues twoMinuteDrill;
  [Header("UI References")]
  [SerializeField]
  private GameObject StarParent;
  [SerializeField]
  private GameObject HighScoreParent;
  [SerializeField]
  private TextMeshProUGUI minicampTitleTxt;
  [SerializeField]
  private TextMeshProUGUI scoreNumberTxt_1star;
  [SerializeField]
  private TextMeshProUGUI scoreNumberTxt_2star;
  [SerializeField]
  private TextMeshProUGUI scoreNumberTxt_3star;
  [SerializeField]
  private TextMeshProUGUI highScoreText;
  [SerializeField]
  private TextMeshProUGUI rankText;
  [SerializeField]
  private TextMeshProUGUI tip1Txt;
  [SerializeField]
  private TextMeshProUGUI tip2Txt;
  [SerializeField]
  private TextMeshProUGUI tip3Txt;
  [SerializeField]
  private TextMeshProUGUI tip4Txt;
  [SerializeField]
  private GameObject tip4GO;
  [SerializeField]
  private TouchButton playBtn;
  [SerializeField]
  private TouchButton returnToLockerRoomBtn;
  public static bool _minicampMenuIsOpen;
  private System.Action _playCallback;
  [SerializeField]
  protected MiniCampGameState gameModeState;
  private const string LocTwoMDIntroTitle = "TwoMD_Intro_Title";
  private const string LocMinicampIntroPocketPassingTitle = "Minicamp_Intro_PocketPassing_Title";
  private const string LocMinicampIntroDimeDroppingTitle = "Minicamp_Intro_DimeDropping_Title";
  private const string LocMinicampIntroRunAndShootTitle = "Minicamp_Intro_RunAndShoot_Title";
  private const string LocMinicampIntroRolloutTitle = "Minicamp_Intro_Rollout_Title";
  private const string LocTwoMDIntroTip1 = "TwoMD_Intro_Tip1";
  private const string LocTwoMDIntroTip2 = "TwoMD_Intro_Tip2";
  private const string LocTwoMDIntroTip3 = "TwoMD_Intro_Tip3";
  private const string LocTwoMDIntroTip4 = "TwoMD_Intro_Tip4";
  private const string LocMinicampIntroPocketPassingTip1 = "Minicamp_Intro_PocketPassing_Tip1";
  private const string LocMinicampIntroPocketPassingTip2 = "Minicamp_Intro_PocketPassing_Tip2";
  private const string LocMinicampIntroPocketPassingTip3 = "Minicamp_Intro_PocketPassing_Tip3";
  private const string LocMinicampIntroPocketPassingTip4 = "Minicamp_Intro_PocketPassing_Tip4";
  private const string LocMinicampIntroDimeDroppingTip1 = "Minicamp_Intro_DimeDropping_Tip1";
  private const string LocMinicampIntroDimeDroppingTip2 = "Minicamp_Intro_DimeDropping_Tip2";
  private const string LocMinicampIntroDimeDroppingTip3 = "Minicamp_Intro_DimeDropping_Tip3";
  private const string LocMinicampIntroRunAndShootTip1 = "Minicamp_Intro_RunAndShoot_Tip1";
  private const string LocMinicampIntroRunAndShootTip2 = "Minicamp_Intro_RunAndShoot_Tip2";
  private const string LocMinicampIntroRunAndShootTip3 = "Minicamp_Intro_RunAndShoot_Tip3";
  private const string LocMinicampIntroRunAndShootTip4 = "Minicamp_Intro_RunAndShoot_Tip4";
  private const string LocMinicampIntroRolloutTip1 = "Minicamp_Intro_Rollout_Tip1";
  private const string LocMinicampIntroRolloutTip2 = "Minicamp_Intro_Rollout_Tip2";
  private const string LocMinicampIntroRolloutTip3 = "Minicamp_Intro_Rollout_Tip3";
  private LocalizeStringEvent minicampTitleTxtLSE;
  private LocalizeStringEvent tip1LSE;
  private LocalizeStringEvent tip2LSE;
  private LocalizeStringEvent tip3LSE;
  private LocalizeStringEvent tip4LSE;
  private LocalizeStringEvent highScoreLSE;
  private LocalizeStringEvent rankLSE;

  public override Enum ViewId { get; } = (Enum) EScreens.kUnknown;

  protected override void OnInitialize()
  {
    if (PersistentSingleton<StateManager<EAppState, GameState>>.Exist() && (UnityEngine.Object) PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState != (UnityEngine.Object) null)
    {
      MiniCampGameState miniCampGameState = (MiniCampGameState) null;
      if (PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState is MiniCampGameState)
        miniCampGameState = (MiniCampGameState) PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState;
      if ((UnityEngine.Object) miniCampGameState != (UnityEngine.Object) null)
        this.gameModeState = miniCampGameState;
    }
    LinksHandler linksHandler = new LinksHandler();
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.playBtn, new System.Action(this.PlayMinicamp)));
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.returnToLockerRoomBtn, new System.Action(this.ReturnToLockerRoom)));
    this.minicampTitleTxtLSE = this.minicampTitleTxt.GetComponent<LocalizeStringEvent>();
    this.tip1LSE = this.tip1Txt.GetComponent<LocalizeStringEvent>();
    this.tip2LSE = this.tip2Txt.GetComponent<LocalizeStringEvent>();
    this.tip3LSE = this.tip3Txt.GetComponent<LocalizeStringEvent>();
    this.tip4LSE = this.tip4Txt.GetComponent<LocalizeStringEvent>();
    this.highScoreLSE = this.highScoreText.GetComponent<LocalizeStringEvent>();
    this.rankLSE = this.rankText.GetComponent<LocalizeStringEvent>();
  }

  protected override void WillAppear()
  {
    if ((UnityEngine.Object) this.gameModeState != (UnityEngine.Object) null)
      this.UpdateUiBasedOnGamemode(this.gameModeState.Id);
    else
      this.UpdateUiBasedOnGamemode(EAppState.k2MD);
  }

  protected override void DidAppear() => MinicampIntroUi._minicampMenuIsOpen = true;

  protected override void DidDisappear() => MinicampIntroUi._minicampMenuIsOpen = false;

  protected virtual void PlayMinicamp()
  {
  }

  private void ReturnToLockerRoom()
  {
    if (VRState.PauseMenu.Value.Equals(true))
      return;
    AppEvents.LoadMainMenu.Trigger();
  }

  public void UpdateUiBasedOnGamemode(EAppState gameMode)
  {
    MinicampIntroUi.MinicampUiValues minicampUiValues;
    switch (gameMode)
    {
      case EAppState.kMiniCampQBPresence:
        minicampUiValues = this.pocketPassing_QbPresence;
        this.minicampTitleTxtLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_PocketPassing_Title";
        this.tip1LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_PocketPassing_Tip1";
        this.tip2LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_PocketPassing_Tip2";
        this.tip3LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_PocketPassing_Tip3";
        this.tip4LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_PocketPassing_Tip4";
        break;
      case EAppState.kMiniCampPrecisionPassing:
        minicampUiValues = this.dimeDropping_PrecisionPassing;
        this.minicampTitleTxtLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_DimeDropping_Title";
        this.tip1LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_DimeDropping_Tip1";
        this.tip2LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_DimeDropping_Tip2";
        this.tip3LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_DimeDropping_Tip3";
        break;
      case EAppState.kMiniCampRunAndShoot:
        minicampUiValues = this.runAndShoot;
        this.minicampTitleTxtLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_RunAndShoot_Title";
        this.tip1LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_RunAndShoot_Tip1";
        this.tip2LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_RunAndShoot_Tip2";
        this.tip3LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_RunAndShoot_Tip3";
        this.tip4LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_RunAndShoot_Tip4";
        break;
      case EAppState.kMiniCampRollout:
        minicampUiValues = this.rolloutEvents;
        this.minicampTitleTxtLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_Rollout_Title";
        this.tip1LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_Rollout_Tip1";
        this.tip2LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_Rollout_Tip2";
        this.tip3LSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_Rollout_Tip3";
        break;
      case EAppState.k2MD:
        minicampUiValues = this.twoMinuteDrill;
        this.minicampTitleTxtLSE.StringReference.TableEntryReference = (TableEntryReference) "TwoMD_Intro_Title";
        this.tip1LSE.StringReference.TableEntryReference = (TableEntryReference) "TwoMD_Intro_Tip1";
        this.tip2LSE.StringReference.TableEntryReference = (TableEntryReference) "TwoMD_Intro_Tip2";
        this.tip3LSE.StringReference.TableEntryReference = (TableEntryReference) "TwoMD_Intro_Tip3";
        this.tip4LSE.StringReference.TableEntryReference = (TableEntryReference) "TwoMD_Intro_Tip4";
        break;
      default:
        return;
    }
    if (minicampUiValues.ScoreType == MinicampIntroUi.ScoreType.Stars)
    {
      this.StarParent.SetActive(true);
      this.HighScoreParent.SetActive(false);
      this.scoreNumberTxt_1star.text = minicampUiValues.scoreReq_1star.ToString();
      this.scoreNumberTxt_2star.text = minicampUiValues.scoreReq_2star.ToString();
      this.scoreNumberTxt_3star.text = minicampUiValues.scoreReq_3star.ToString();
    }
    else if (minicampUiValues.ScoreType == MinicampIntroUi.ScoreType.HighScore)
    {
      this.StarParent.SetActive(false);
      this.HighScoreParent.SetActive(true);
      this.highScoreLSE.StringReference.Arguments = (IList<object>) new string[1]
      {
        string.Format("<color=\"white\"> {0}", (object) PersistentSingleton<SaveManager>.Instance.twoMinuteDrill.BestScore.ToString("#,##0.##"))
      };
      this.rankLSE.StringReference.Arguments = (IList<object>) new string[1]
      {
        string.Format("<color=\"white\"> {0}", (object) "TODO")
      };
    }
    if (minicampUiValues.HasFourthTipText)
      this.tip4GO.SetActive(true);
    else
      this.tip4GO.SetActive(false);
  }

  public void SetPlayCallback(System.Action callback) => this._playCallback = callback;

  private enum ScoreType
  {
    Stars,
    HighScore,
  }

  [Serializable]
  private class MinicampUiValues
  {
    public string Title;
    public MinicampIntroUi.ScoreType ScoreType;
    public int scoreReq_1star;
    public int scoreReq_2star;
    public int scoreReq_3star;
    public bool HasFourthTipText;
    public string Tip1;
    public string Tip2;
    public string Tip3;
    public string Tip4;
  }
}
