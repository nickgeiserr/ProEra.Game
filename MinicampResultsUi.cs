// Decompiled with JetBrains decompiler
// Type: MinicampResultsUi
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using Framework.UI;
using ProEra.Game;
using System;
using System.Collections.Generic;
using TB12;
using TB12.AppStates;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class MinicampResultsUi : UIView
{
  [Header("Asset References")]
  [SerializeField]
  private Sprite zeroStarImg;
  [SerializeField]
  private Sprite oneStarImg;
  [SerializeField]
  private Sprite twoStarImg;
  [SerializeField]
  private Sprite threeStarImg;
  [SerializeField]
  private Sprite resultImg_Miss;
  [SerializeField]
  private Sprite resultImg_GreenCheck;
  [SerializeField]
  private Sprite resultImg_Bronze;
  [SerializeField]
  private Sprite resultImg_Silver;
  [SerializeField]
  private Sprite resultImg_Gold;
  [Header("UI References")]
  [SerializeField]
  private GameObject normalButtonLayout;
  [SerializeField]
  private GameObject lastLevelButtonLayout;
  [SerializeField]
  private TextMeshProUGUI headerTxt;
  [SerializeField]
  private Image[] teamLogoImgs;
  [SerializeField]
  private Image[] resultsStarsImgSets;
  [SerializeField]
  private TextMeshProUGUI[] scoreTexts;
  [SerializeField]
  private GameObject[] newPersonalBestTxtGOs;
  [SerializeField]
  private TouchButton nextLevelBtn;
  [SerializeField]
  private TouchButton retryBtn;
  [SerializeField]
  private TouchButton returnToLockerRoomBtn;
  [SerializeField]
  private TouchButton lastLevelLayoutReturnToLockerRoomBtn;
  [SerializeField]
  private TouchButton lastLevelLayoutRetryBtn;
  [SerializeField]
  private GameObject group_PocketPassing;
  [SerializeField]
  private GameObject group_DimeDropping;
  [SerializeField]
  private GameObject group_RunAndShoot;
  [SerializeField]
  private GameObject group_Rollout;
  [SerializeField]
  private Image[] precisionPassingGrid_Row1;
  [SerializeField]
  private Image[] precisionPassingGrid_Row2;
  [SerializeField]
  private Image[] precisionPassingGrid_Row3;
  [SerializeField]
  private Image[] rolloutGrid_Row1;
  [SerializeField]
  private Image[] rolloutGrid_Row2;
  [SerializeField]
  private Image[] rolloutGrid_Row3;
  [SerializeField]
  protected MiniCampGameState myGameState;
  private int starsEarned;
  private const string LocMinicampIntroPocketPassingTitle = "Minicamp_Intro_PocketPassing_Title";
  private const string LocMinicampIntroDimeDroppingTitle = "Minicamp_Intro_DimeDropping_Title";
  private const string LocMinicampIntroRunAndShootTitle = "Minicamp_Intro_RunAndShoot_Title";
  private const string LocMinicampIntroRolloutTitle = "Minicamp_Intro_Rollout_Title";
  private LocalizeStringEvent headerTextLSE;
  private LocalizeStringEvent[] scoreTextsLSE;
  private static int m_score;
  private static int m_starsEarned;
  private static bool m_highScoreEarned;

  public override Enum ViewId { get; } = (Enum) EScreens.kUnknown;

  protected override void OnInitialize()
  {
    if (PersistentSingleton<StateManager<EAppState, GameState>>.Exist() && (UnityEngine.Object) PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState != (UnityEngine.Object) null)
    {
      MiniCampGameState miniCampGameState = (MiniCampGameState) null;
      if (PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState is MiniCampGameState)
        miniCampGameState = (MiniCampGameState) PersistentSingleton<StateManager<EAppState, GameState>>.Instance.activeState;
      if ((UnityEngine.Object) miniCampGameState != (UnityEngine.Object) null)
        this.myGameState = miniCampGameState;
    }
    LinksHandler linksHandler = new LinksHandler();
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.nextLevelBtn, new System.Action(this.NextLevel)));
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.retryBtn, new System.Action(this.RetryMinicamp)));
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.returnToLockerRoomBtn, new System.Action(this.ReturnToLockerRoom)));
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.lastLevelLayoutReturnToLockerRoomBtn, new System.Action(this.ReturnToLockerRoom)));
    linksHandler.AddLink((EventHandle) UIHandle.Link((IButton) this.lastLevelLayoutRetryBtn, new System.Action(this.RetryMinicamp)));
    this.headerTextLSE = this.headerTxt.GetComponent<LocalizeStringEvent>();
    this.scoreTextsLSE = new LocalizeStringEvent[this.scoreTexts.Length];
    for (int index = 0; index < this.scoreTexts.Length; ++index)
      this.scoreTextsLSE[index] = this.scoreTexts[index].GetComponent<LocalizeStringEvent>();
  }

  protected override void DidAppear()
  {
    MinicampIntroUi._minicampMenuIsOpen = true;
    this.nextLevelBtn.SetInteractible(this.starsEarned > 0);
  }

  protected override void DidDisappear() => MinicampIntroUi._minicampMenuIsOpen = false;

  private void NextLevel()
  {
    if (VRState.PauseMenu.Value.Equals(true))
      return;
    this.myGameState.HandleNextLevel();
  }

  protected virtual void RetryMinicamp()
  {
  }

  private void ReturnToLockerRoom()
  {
    if (VRState.PauseMenu.Value.Equals(true))
      return;
    AppEvents.LoadMainMenu.Trigger();
  }

  public static void SetResultsInfo(int score, int starsEarned, bool highScoreEarned)
  {
    MinicampResultsUi.m_score = score;
    MinicampResultsUi.m_starsEarned = starsEarned;
    MinicampResultsUi.m_highScoreEarned = highScoreEarned;
  }

  protected override void WillAppear() => this.ShowResults(MinicampResultsUi.m_score, MinicampResultsUi.m_starsEarned, MinicampResultsUi.m_highScoreEarned);

  private void ShowResults(int score, int starsEarned, bool newHighScore)
  {
    EAppState id = this.myGameState.Id;
    this.group_PocketPassing.SetActive(id == EAppState.kMiniCampQBPresence);
    this.group_DimeDropping.SetActive(id == EAppState.kMiniCampPrecisionPassing);
    this.group_RunAndShoot.SetActive(id == EAppState.kMiniCampRunAndShoot);
    this.group_Rollout.SetActive(id == EAppState.kMiniCampRollout);
    switch (id)
    {
      case EAppState.kMiniCampQBPresence:
        this.headerTextLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_PocketPassing_Title";
        break;
      case EAppState.kMiniCampPrecisionPassing:
        this.headerTextLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_DimeDropping_Title";
        break;
      case EAppState.kMiniCampRunAndShoot:
        this.headerTextLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_RunAndShoot_Title";
        break;
      case EAppState.kMiniCampRollout:
        this.headerTextLSE.StringReference.TableEntryReference = (TableEntryReference) "Minicamp_Intro_Rollout_Title";
        break;
    }
    Save_MiniCamp miniCamp = PersistentSingleton<SaveManager>.Instance.miniCamp;
    bool flag = miniCamp.SelectedMiniCamp.MiniCampEntries.Length != 0 && miniCamp.SelectedMiniCamp.MiniCampEntries.Length == miniCamp.SelectedEntry.Level;
    this.normalButtonLayout.SetActive(!flag);
    this.lastLevelButtonLayout.SetActive(flag);
    foreach (GameObject personalBestTxtGo in this.newPersonalBestTxtGOs)
      personalBestTxtGo.SetActive(newHighScore);
    foreach (Image teamLogoImg in this.teamLogoImgs)
      teamLogoImg.sprite = PersistentSingleton<SaveManager>.Instance.miniCamp.SelectedEntry.GetLogo();
    foreach (LocalizeStringEvent localizeStringEvent in this.scoreTextsLSE)
    {
      if ((UnityEngine.Object) localizeStringEvent != (UnityEngine.Object) null)
        localizeStringEvent.StringReference.Arguments = (IList<object>) new string[1]
        {
          score.ToString()
        };
    }
    if (!this.myGameState.DidGiveResultsFeedback)
    {
      this.myGameState.DidGiveResultsFeedback = false;
      Sprite starsImage = this.GetStarsImage(starsEarned);
      foreach (Image resultsStarsImgSet in this.resultsStarsImgSets)
        resultsStarsImgSet.sprite = starsImage;
      AppSounds.PlayOC(this.GetStarsSfx(starsEarned));
    }
    if (id == EAppState.kMiniCampPrecisionPassing)
      this.PopulateResultsGrid_PrecisionPassing();
    if (id == EAppState.kMiniCampRollout)
      this.PopulateResultsGrid_Rollout();
    this.starsEarned = starsEarned;
  }

  private string GetGameModeHeader(EAppState gameMode)
  {
    switch (gameMode)
    {
      case EAppState.kMiniCampQBPresence:
        return "Pocket Passing Results";
      case EAppState.kMiniCampPrecisionPassing:
        return "Dime Drop Results";
      case EAppState.kMiniCampRunAndShoot:
        return "Run and Shoot Results";
      case EAppState.kMiniCampRollout:
        return "Rollout Results";
      default:
        return (string) null;
    }
  }

  private Sprite GetStarsImage(int starsEarned)
  {
    switch (starsEarned)
    {
      case 1:
        return this.oneStarImg;
      case 2:
        return this.twoStarImg;
      case 3:
        return this.threeStarImg;
      default:
        return this.zeroStarImg;
    }
  }

  private EOCTypes GetStarsSfx(int starsEarned)
  {
    switch (starsEarned)
    {
      case 1:
        return EOCTypes.kResults1Stars;
      case 2:
        return EOCTypes.kResults2Stars;
      case 3:
        return EOCTypes.kResults3Stars;
      default:
        return EOCTypes.kResults0Stars;
    }
  }

  private void PopulateResultsGrid_PrecisionPassing()
  {
    PrecisionPassingGameFlow.AwardPoints[,] throwAwards = UnityEngine.Object.FindObjectOfType<PrecisionPassingGameFlow>().ThrowAwards;
    for (int index1 = 0; index1 < 3; ++index1)
    {
      for (int index2 = 0; index2 < this.precisionPassingGrid_Row1.Length; ++index2)
      {
        int index3 = this.precisionPassingGrid_Row1.Length * index1 + index2;
        switch (index1)
        {
          case 0:
            this.precisionPassingGrid_Row1[index2].sprite = this.GetResultsIcon(throwAwards[0, index3].award);
            break;
          case 1:
            this.precisionPassingGrid_Row2[index2].sprite = this.GetResultsIcon(throwAwards[0, index3].award);
            break;
          case 2:
            this.precisionPassingGrid_Row3[index2].sprite = this.GetResultsIcon(throwAwards[0, index3].award);
            break;
        }
      }
    }
  }

  private void PopulateResultsGrid_Rollout()
  {
    MiniGameScoreState.MiniCampPassResult[,] playerResults = UnityEngine.Object.FindObjectOfType<MiniCampRolloutFlow>().PlayerResults;
    for (int index1 = 0; index1 < 3; ++index1)
    {
      for (int index2 = 0; index2 < this.rolloutGrid_Row1.Length; ++index2)
      {
        switch (index1)
        {
          case 0:
            this.rolloutGrid_Row1[index2].sprite = this.GetResultsImg(playerResults[index1, index2].miniCampPassResult);
            break;
          case 1:
            this.rolloutGrid_Row2[index2].sprite = this.GetResultsImg(playerResults[index1, index2].miniCampPassResult);
            break;
          case 2:
            this.rolloutGrid_Row3[index2].sprite = this.GetResultsImg(playerResults[index1, index2].miniCampPassResult);
            break;
        }
      }
    }
  }

  private Sprite GetResultsIcon(PrecisionPassingGameFlow.ThrowAward playerResult)
  {
    switch (playerResult)
    {
      case PrecisionPassingGameFlow.ThrowAward.Miss:
        return this.resultImg_Miss;
      case PrecisionPassingGameFlow.ThrowAward.Gold:
        return this.resultImg_Gold;
      case PrecisionPassingGameFlow.ThrowAward.Silver:
        return this.resultImg_Silver;
      case PrecisionPassingGameFlow.ThrowAward.Bronze:
        return this.resultImg_Bronze;
      default:
        return (Sprite) null;
    }
  }

  private Sprite GetResultsImg(
    MiniGameScoreState.EMiniCampPassResult playerResult)
  {
    return playerResult == MiniGameScoreState.EMiniCampPassResult.Miss || playerResult == MiniGameScoreState.EMiniCampPassResult.Empty ? this.resultImg_Miss : this.resultImg_GreenCheck;
  }
}
