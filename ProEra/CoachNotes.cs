// Decompiled with JetBrains decompiler
// Type: ProEra.CoachNotes
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace ProEra
{
  public class CoachNotes : MonoBehaviour
  {
    [SerializeField]
    private LocalizeStringEvent _headerWeekText;
    [SerializeField]
    private TextMeshProUGUI _headerTeamRecordText;
    private static readonly string _recordString = "({0} - {1})";
    [SerializeField]
    private TextMeshProUGUI _headerTeamAbbrText;
    [SerializeField]
    private RectTransform _bodyKeysToGameParent;
    [SerializeField]
    private TextMeshProUGUI _bodyDefenseScheme;
    [SerializeField]
    private RectTransform _bodyDefenseParent;
    [SerializeField]
    private TextMeshProUGUI _bodyOffenseScheme;
    [SerializeField]
    private RectTransform _bodyOffenseParent;
    [SerializeField]
    private LocalizeStringEvent[] _localizedKeysToTheGameTexts;
    [SerializeField]
    private TMP_Text[] _keysToTheGameTexts;
    [SerializeField]
    private LocalizeStringEvent _keysHeader;
    private string[] _seasonSummaryStrings;
    private const string LocalizationWeekHeaderSeason = "Whiteboard_Header_WeekPlan";
    private const string LocalizationWeekHeaderWildCard = "Whiteboard_Header_WeekWildCard";
    private const string LocalizationWeekHeaderDivisional = "Whiteboard_Header_WeekDivisional";
    private const string LocalizationWeekHeaderConference = "Whiteboard_Header_WeekConference";
    private const string LocalizationWeekHeaderSuperBowl = "Whiteboard_Header_WeekSuperBowl";
    private const string LocalizationWeekHeaderSeasonSummary = "Whiteboard_Header_WeekPostSeason";
    private const string LocalizationHeaderSeasonTotals = "Whiteboard_Header_SeasonTotals";

    private void Start() => SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);

    private void OnDestroy()
    {
      if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
        return;
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
    }

    private void Init()
    {
      SeasonModeManager self = SeasonModeManager.self;
      int teamIndex1 = self.userTeamData.TeamIndex;
      int seasonWeek = PersistentData.seasonWeek;
      int num1 = self.seasonModeData.NumberOfWeeksInSeason + self.seasonModeData.NumberOfWeeksInPlayoffs;
      int teamIndex2 = seasonWeek > num1 ? -1 : self.GetTeamOpponentForWeek(teamIndex1, seasonWeek, out int _, out int _);
      bool flag = teamIndex2 >= 0;
      if (seasonWeek <= self.seasonModeData.NumberOfWeeksInSeason)
      {
        this._headerWeekText.StringReference.Arguments = (IList<object>) new string[1]
        {
          seasonWeek.ToString()
        };
        this._headerWeekText.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_WeekPlan";
      }
      else if (self.IsFirstRoundOfPlayoffs() & flag)
        this._headerWeekText.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_WeekWildCard";
      else if (self.IsSecondRoundOfPlayoffs() & flag)
        this._headerWeekText.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_WeekDivisional";
      else if (self.IsThirdRoundOfPlayoffs() & flag)
        this._headerWeekText.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_WeekConference";
      else if (self.IsFourthRoundOfNFLPlayoffs() & flag)
      {
        this._headerWeekText.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_WeekSuperBowl";
      }
      else
      {
        this._headerWeekText.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_WeekPostSeason";
        this._keysHeader.StringReference.TableEntryReference = (TableEntryReference) "Whiteboard_Header_SeasonTotals";
      }
      if (teamIndex2 < 0)
      {
        this._headerTeamAbbrText.text = string.Empty;
        teamIndex2 = self.userTeamData.TeamIndex;
      }
      TeamData teamData = self.GetTeamData(teamIndex2);
      TeamSeasonStats currentSeasonStats = teamData.CurrentSeasonStats;
      int num2 = Mathf.Max(1, currentSeasonStats.wins + currentSeasonStats.losses);
      this._headerTeamRecordText.text = string.Format(CoachNotes._recordString, (object) currentSeasonStats.wins, (object) currentSeasonStats.losses);
      this._headerTeamAbbrText.text = teamData.GetAbbreviation();
      this._headerTeamAbbrText.color = teamData.GetPrimaryColor();
      if (flag)
      {
        List<string> keysToTheGame = teamData.GetKeysToTheGame();
        for (int index = 0; index < keysToTheGame.Count; ++index)
        {
          this._localizedKeysToTheGameTexts[index].StringReference.TableEntryReference = (TableEntryReference) keysToTheGame[index];
          this._bodyKeysToGameParent.GetChild(index).gameObject.SetActive(true);
        }
        for (int count = keysToTheGame.Count; count < this._bodyKeysToGameParent.childCount; ++count)
          this._bodyKeysToGameParent.GetChild(count).gameObject.SetActive(false);
      }
      else
      {
        this.BuildSummaryStrings(teamData);
        int length1 = this._keysToTheGameTexts.Length;
        int length2 = this._seasonSummaryStrings.Length;
        for (int index = 0; index < length1; ++index)
        {
          if (index < length2)
          {
            TMP_Text keysToTheGameText = this._keysToTheGameTexts[index];
            if (!keysToTheGameText.gameObject.activeSelf)
              keysToTheGameText.gameObject.SetActive(true);
            keysToTheGameText.text = this._seasonSummaryStrings[index];
          }
          else
            this._keysToTheGameTexts[index].gameObject.SetActive(false);
        }
      }
      this._bodyDefenseScheme.text = "SCHEME: " + teamData.GetDefensivePlaybook();
      Debug.Log((object) ("seasonStats.pointsAllowed: " + currentSeasonStats.pointsAllowed.ToString()));
      for (int index = 0; index < this._bodyDefenseParent.childCount; ++index)
      {
        TextMeshProUGUI component = this._bodyDefenseParent.GetChild(index).Find("StatValue").GetComponent<TextMeshProUGUI>();
        switch (index)
        {
          case 0:
            float num3 = (float) currentSeasonStats.pointsAllowed / (float) num2;
            component.text = string.Format("{0:0.##}", (object) num3);
            break;
          case 1:
            component.text = string.Format("{0}%", (object) teamData.GetAvgBlitzPercent());
            break;
          case 2:
            component.text = string.Format("{0}%", (object) teamData.GetAvgManPercent());
            break;
          case 3:
            float num4 = (float) (currentSeasonStats.interceptions + currentSeasonStats.fumbleRecoveries) / (float) num2;
            component.text = string.Format("{0:0.##}", (object) num4);
            break;
          case 4:
            float num5 = (float) currentSeasonStats.passYardsAllowed / (float) num2;
            component.text = string.Format("{0:0.##}", (object) num5);
            break;
          case 5:
            float num6 = (float) currentSeasonStats.rushYardsAllowed / (float) num2;
            component.text = string.Format("{0:0.##}", (object) num6);
            break;
        }
      }
      this._bodyOffenseScheme.text = "SCHEME: " + teamData.GetOffensivePlaybook();
      for (int index = 0; index < this._bodyOffenseParent.childCount; ++index)
      {
        TextMeshProUGUI component = this._bodyOffenseParent.GetChild(index).Find("StatValue").GetComponent<TextMeshProUGUI>();
        switch (index)
        {
          case 0:
            float num7 = (float) currentSeasonStats.pointsScored / (float) num2;
            component.text = string.Format(string.Format("{0:0.##}", (object) num7));
            break;
          case 1:
            component.text = string.Format("{0}%", (object) teamData.GetAvgPassPercent());
            break;
          case 2:
            component.text = string.Format("{0}%", (object) teamData.GetAvgRunPercent());
            break;
          case 3:
            float num8 = (float) currentSeasonStats.turnovers / (float) num2;
            component.text = string.Format(string.Format("{0:0.##}", (object) num8));
            break;
          case 4:
            float num9 = (float) currentSeasonStats.passYards / (float) num2;
            component.text = string.Format(string.Format("{0:0.##}", (object) num9));
            break;
          case 5:
            float num10 = (float) currentSeasonStats.rushYards / (float) num2;
            component.text = string.Format(string.Format("{0:0.##}", (object) num10));
            break;
        }
      }
    }

    private IEnumerator RunOptimizer()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      CoachNotes coachNotes = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        CanvasOptimizer.OptimizeCanvas(coachNotes.transform.GetChild(1).gameObject);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) new WaitForSeconds(0.1f);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private void BuildSummaryStrings(TeamData teamdata)
    {
      TeamSeasonStats currentSeasonStats = teamdata.CurrentSeasonStats;
      int seasonPassCompletions = teamdata.CurrentSeasonPassCompletions;
      int totalPassPlays = currentSeasonStats.totalPassPlays;
      float num = (double) totalPassPlays > 0.0 ? (float) seasonPassCompletions / (float) totalPassPlays : 0.0f;
      int seasonTouchdownPasses = teamdata.CurrentSeasonTouchdownPasses;
      int passYards = currentSeasonStats.passYards;
      int qbInts = currentSeasonStats.qbInts;
      float passerRating = PlayerStats.CalculatePasserRating(totalPassPlays, seasonPassCompletions, seasonTouchdownPasses, passYards, qbInts);
      this._seasonSummaryStrings = new string[7]
      {
        string.Format("Passing Yards: {0}", (object) passYards),
        string.Format("Passing TouchDowns: {0}", (object) seasonTouchdownPasses),
        string.Format("Completion Percent: {0:P2}", (object) num),
        string.Format("INTS: {0}", (object) qbInts),
        string.Format("Attempts: {0}", (object) totalPassPlays),
        string.Format("Completions: {0}", (object) seasonPassCompletions),
        string.Format("QBR: {0:F1}", (object) passerRating)
      };
    }
  }
}
