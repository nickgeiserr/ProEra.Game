// Decompiled with JetBrains decompiler
// Type: GameTimeEnvironment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class GameTimeEnvironment : TransitionEnvironment
{
  [SerializeField]
  private UniformLogoStore _store;
  [SerializeField]
  private LocalizeStringEvent _titleText;
  [SerializeField]
  private TextMeshProUGUI _homeScore;
  [SerializeField]
  private TextMeshProUGUI _awayScore;
  [SerializeField]
  private Image _homeLogo;
  [SerializeField]
  private Image _awayLogo;
  private const string LocalizationEndOfQuarter = "Transition_Text_EndOfQuarter";
  private const string LocalizationOvertime = "Transition_Text_Overtime";
  private const string Localization2MinWarning = "Transition_Text_2MinWarning";
  private const string LocalizationGameover = "Transition_Text_GameOver";
  private const string LocalizationSimLeague = "Transition_Text_SimLeague";
  private const string LocalizationSimByeWeek = "Transition_Text_SimByeWeek";

  public override void ShowQuarterEnd()
  {
    int quarter = MatchManager.instance.timeManager.GetQuarter();
    bool flag = MatchManager.instance.timeManager.IsInOvertime();
    string titleLocalizationKey = "Transition_Text_EndOfQuarter";
    string[] localizationArgs = new string[0];
    if (flag)
    {
      titleLocalizationKey = "Transition_Text_Overtime";
    }
    else
    {
      switch (quarter)
      {
        case 2:
          localizationArgs = new string[1]{ "1st" };
          break;
        case 3:
          localizationArgs = new string[1]{ "2nd" };
          break;
        case 4:
          localizationArgs = new string[1]{ "3rd" };
          break;
      }
    }
    this.UpdateCanvas(titleLocalizationKey, localizationArgs);
  }

  public override void Show2MinWarning() => this.UpdateCanvas("Transition_Text_2MinWarning");

  public override void ShowGameOver() => this.UpdateCanvas("Transition_Text_GameOver");

  public override void ShowFinishWeek() => this.UpdateCanvas("Transition_Text_SimLeague");

  public override void ShowByeWeek() => this.UpdateCanvas("Transition_Text_SimByeWeek");

  private void UpdateCanvas(string titleLocalizationKey, string[] localizationArgs = null)
  {
    this._titleText.StringReference.TableEntryReference = (TableEntryReference) titleLocalizationKey;
    this._titleText.StringReference.Arguments = (IList<object>) localizationArgs;
    this._homeScore.text = ProEra.Game.MatchState.Stats.GetHomeScore().ToString();
    this._awayScore.text = ProEra.Game.MatchState.Stats.GetAwayScore().ToString();
    this._homeLogo.sprite = this._store.GetUniformLogo(PersistentData.GetHomeTeamIndex()).teamLogo;
    this._awayLogo.sprite = this._store.GetUniformLogo(PersistentData.GetAwayTeamIndex()).teamLogo;
  }
}
