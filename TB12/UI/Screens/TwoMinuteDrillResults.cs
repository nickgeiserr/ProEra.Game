// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.TwoMinuteDrillResults
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using ProEra.Web;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Networking;

namespace TB12.UI.Screens
{
  public class TwoMinuteDrillResults : UIView
  {
    [SerializeField]
    private RectTransform _scoreListParent;
    [SerializeField]
    private TextMeshProUGUI _totalScore;
    [SerializeField]
    private RectTransform _statValueParent;
    [SerializeField]
    private TouchButton _playAgainButton;
    [SerializeField]
    private TouchButton _mainMenuButton;
    [SerializeField]
    private TextMeshProUGUI _highScoreText;
    private const string NewBestText = "New Personal Best";
    private string[] _difficultyNames = new string[3]
    {
      "Rookie",
      "All Pro",
      "Hall of Fame"
    };
    private const int LOWSCORE = 1000;

    public override Enum ViewId { get; } = (Enum) EScreens.k2MDResults;

    protected override void OnInitialize()
    {
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._playAgainButton.Link(new System.Action(this.HandlePlayAgain)),
        (EventHandle) this._mainMenuButton.Link(AppEvents.LoadMainMenu)
      });
      this.UpdateScores();
      this.UpdateStats();
    }

    protected override void WillAppear()
    {
      base.WillAppear();
      this._highScoreText.gameObject.SetActive(false);
      MinicampIntroUi._minicampMenuIsOpen = true;
    }

    protected override void WillDisappear()
    {
      base.WillDisappear();
      MinicampIntroUi._minicampMenuIsOpen = false;
    }

    private void UpdateScores()
    {
      TeamGameStats user = ProEra.Game.MatchState.Stats.User;
      PlayerStats currentGameStats = MatchManager.instance.playersManager.userTeamData.GetPlayer(0).CurrentGameStats;
      TwoMinuteDrillGameFlow instance = (TwoMinuteDrillGameFlow) AxisGameFlow.instance;
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (user.Score >= 21)
        num2 += 0.3f;
      else if (user.Score >= 14)
        num2 += 0.15f;
      for (int index = 0; index < this._scoreListParent.childCount; ++index)
      {
        Transform child = this._scoreListParent.GetChild(index);
        TextMeshProUGUI component1 = child.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component2 = child.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI component3 = child.GetChild(2).GetComponent<TextMeshProUGUI>();
        int num3 = 0;
        float num4 = 0.0f;
        switch (index)
        {
          case 0:
            component1.text = "Completions";
            num3 = currentGameStats.QBCompletions;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 25);
            component3.text = num4.ToString("#,##0");
            break;
          case 1:
            component1.text = "10 Yard Completions";
            num3 = currentGameStats.QBTenYardCompletions;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 100);
            component3.text = num4.ToString("#,##0");
            break;
          case 2:
            component1.text = "20 Yard Completions";
            num3 = currentGameStats.QBTwentyYardCompletions;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 250);
            component3.text = num4.ToString("#,##0");
            break;
          case 3:
            component1.text = "30 Yard Completions";
            num3 = currentGameStats.QBThirtyYardCompletions;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 350);
            component3.text = num4.ToString("#,##0");
            break;
          case 4:
            component1.text = "+50 Yard Completions";
            num3 = currentGameStats.QBFiftyYardCompletions;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 450);
            component3.text = num4.ToString("#,##0");
            break;
          case 5:
            component1.text = "Interceptions";
            num3 = currentGameStats.QBInts;
            component2.text = num3.ToString();
            num4 = (float) (num3 * -250);
            component3.text = num4.ToString("#,##0");
            break;
          case 6:
            component1.text = "Touchdowns";
            num3 = user.Touchdowns;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 500);
            component3.text = num4.ToString("#,##0");
            break;
          case 7:
            component1.text = "Stop the clock after gains";
            num3 = instance.ClockStops;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 50);
            component3.text = num4.ToString("#,##0");
            break;
          case 8:
            component1.text = "Audible for gains";
            num3 = instance.AudibleCount;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 10);
            component3.text = num4.ToString("#,##0");
            break;
          case 9:
            component1.text = "Sacked";
            num3 = currentGameStats.QBSacked;
            component2.text = num3.ToString();
            num4 = (float) (num3 * -150);
            component3.text = num4.ToString("#,##0");
            break;
          case 10:
            component1.text = "Turnover on Downs";
            num3 = instance.DownTurnovers;
            component2.text = num3.ToString();
            num4 = (float) (num3 * -250);
            component3.text = num4.ToString("#,##0");
            break;
          case 11:
            component1.text = "2 point conversion";
            num3 = user.TwoPointConversions;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 375);
            component3.text = num4.ToString("#,##0");
            break;
          case 12:
            component1.text = "Rush for +5 yards";
            num3 = user.RushFiveYards;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 50);
            component3.text = num4.ToString("#,##0");
            break;
          case 13:
            component1.text = "Rush TD";
            num3 = user.RushTDs;
            component2.text = num3.ToString();
            num4 = (float) (num3 * 500);
            component3.text = num4.ToString("#,##0");
            break;
          case 14:
            component1.text = "Total Yardage";
            num3 = user.TotalYards();
            component2.text = num3.ToString();
            num4 = (float) (num3 * 10);
            component3.text = num4.ToString("#,##0");
            break;
          case 15:
            component1.text = "Total Points";
            num3 = user.Score;
            component2.text = num3.ToString();
            num4 = num2;
            component3.text = num4.ToString("#0%");
            break;
          case 16:
            component1.text = "Difficulty Bonus";
            component2.text = this._difficultyNames[(int) GameSettings.DifficultyLevel];
            num4 = instance.DIFFICULTY_BONUS_PERCENT[(int) GameSettings.DifficultyLevel];
            component3.text = num4.ToString("##%");
            break;
        }
        if (num3 == 0)
          child.gameObject.SetActive(false);
        else if (index < 15)
          num1 += num4;
        else
          num1 += num1 * num4;
      }
      this._totalScore.GetComponent<LocalizeStringEvent>().StringReference.Arguments = (IList<object>) new string[1]
      {
        num1.ToString("#,##0.##")
      };
      Save_TwoMD twoMinuteDrill = PersistentSingleton<SaveManager>.Instance.twoMinuteDrill;
      if ((double) num1 > (double) twoMinuteDrill.BestScore)
      {
        AppSounds.PlayOC(EOCTypes.k2MDHighscore);
        twoMinuteDrill.BestScore = num1;
        PersistentSingleton<PlayerApi>.Instance.PutHighScore(Definitions.HighScore.TwoMinuteDrill, num1, (Action<UnityWebRequest.Result>) (result => Debug.Log((object) result)));
        AppEvents.SaveTwoMinuteDrill.Trigger();
        this._highScoreText.gameObject.SetActive(true);
      }
      else if ((double) num1 < 1000.0)
      {
        AppSounds.PlayOC(EOCTypes.k2MDLowscore);
        this._highScoreText.gameObject.SetActive(false);
      }
      else
      {
        AppSounds.PlayOC(EOCTypes.k2MDOutro);
        this._highScoreText.gameObject.SetActive(false);
      }
    }

    private void UpdateStats()
    {
      TeamGameStats user = ProEra.Game.MatchState.Stats.User;
      PlayerStats currentGameStats = MatchManager.instance.playersManager.userTeamData.GetPlayer(0).CurrentGameStats;
      TwoMinuteDrillGameFlow instance = (TwoMinuteDrillGameFlow) AxisGameFlow.instance;
      for (int index = 0; index < this._statValueParent.childCount; ++index)
      {
        TextMeshProUGUI component = this._statValueParent.GetChild(index).GetComponent<TextMeshProUGUI>();
        int num;
        switch (index)
        {
          case 0:
            TextMeshProUGUI textMeshProUgui1 = component;
            num = currentGameStats.GetQBRating();
            string str1 = num.ToString("##0.#");
            textMeshProUgui1.text = str1;
            break;
          case 1:
            component.text = currentGameStats.QBCompletions.ToString("#,##0");
            break;
          case 2:
            component.text = currentGameStats.QBAttempts.ToString("#,##0");
            break;
          case 3:
            component.text = ((float) currentGameStats.QBCompletions / (float) currentGameStats.QBAttempts).ToString("#0%");
            break;
          case 4:
            TextMeshProUGUI textMeshProUgui2 = component;
            num = user.Touchdowns;
            string str2 = num.ToString();
            textMeshProUgui2.text = str2;
            break;
          case 5:
            component.text = currentGameStats.QBInts.ToString("#,##0");
            break;
          case 6:
            component.text = currentGameStats.QBPassYards.ToString("#,##0");
            break;
        }
      }
    }

    private void HandlePlayAgain()
    {
      UIDispatch.FrontScreen.HideView(EScreens.k2MDResults);
      ((TwoMinuteDrillGameFlow) AxisGameFlow.instance).ResetGame();
    }
  }
}
