// Decompiled with JetBrains decompiler
// Type: SeasonModeEnvironment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using System.Collections.Generic;
using TB12;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class SeasonModeEnvironment : TransitionEnvironment
{
  [SerializeField]
  private UniformLogoStore _store;
  [SerializeField]
  private Image _homeLogo;
  [SerializeField]
  private LocalizeStringEvent _weekNumberText;
  [SerializeField]
  private TextMeshProUGUI _recordText;
  [SerializeField]
  private TextMeshProUGUI _qbrText;
  private const string LocalizationWeekNumber = "Transition_Text_WeekNumber";
  private const string LocalizationExhibition = "Transition_Text_Exhibition";

  public override void ShowSeasonModeUpdate()
  {
    string[] strArray = new string[1]
    {
      PersistentData.seasonWeek.ToString()
    };
    this._homeLogo.sprite = this._store.GetUniformLogo(PersistentData.GetUserTeamIndex()).teamLogo;
    this._weekNumberText.StringReference.Arguments = (IList<object>) strArray;
    this._weekNumberText.StringReference.TableEntryReference = (TableEntryReference) "Transition_Text_WeekNumber";
    this._recordText.text = PersistentSingleton<SaveManager>.Instance.seasonModeData.seasonWins.ToString() + "-" + PersistentSingleton<SaveManager>.Instance.seasonModeData.seasonLosses.ToString();
    if (AppState.SeasonMode.Value > ESeasonMode.kUnknown)
    {
      PlayerStats currentSeasonStats = PersistentData.GetUserTeam().GetPlayer(0).CurrentSeasonStats;
      PlayerStats currentGameStats = PersistentData.GetUserTeam().GetPlayer(0).CurrentGameStats;
      this._qbrText.text = "Season QBR: " + Mathf.RoundToInt(PlayerStats.CalculatePasserRating(currentSeasonStats.QBAttempts + currentGameStats.QBAttempts, currentSeasonStats.QBCompletions + currentGameStats.QBCompletions, currentSeasonStats.QBPassTDs + currentGameStats.QBPassTDs, currentSeasonStats.QBPassYards + currentGameStats.QBPassYards, currentSeasonStats.Interceptions + currentGameStats.Interceptions)).ToString();
    }
    else
    {
      this._qbrText.text = "Game QBR: " + PersistentData.GetUserTeam().GetPlayer(0).CurrentGameStats?.GetQBRating().ToString();
      this._weekNumberText.StringReference.Arguments = (IList<object>) new string[0];
      this._weekNumberText.StringReference.TableEntryReference = (TableEntryReference) "Transition_Text_Exhibition";
      this._recordText.text = "";
    }
    this._weekNumberText.RefreshString();
  }
}
