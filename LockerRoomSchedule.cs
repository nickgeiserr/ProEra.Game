// Decompiled with JetBrains decompiler
// Type: LockerRoomSchedule
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class LockerRoomSchedule : MonoBehaviour
{
  [Header("UI Assets")]
  [SerializeField]
  private Sprite _matchWinSprite;
  [SerializeField]
  private Sprite _matchLossSprite;
  [Header("UI Components")]
  [SerializeField]
  private GameObject _regularParent;
  [SerializeField]
  private GameObject _playoffParent;
  [SerializeField]
  private Image _currentWeekCircle;
  [SerializeField]
  private LockerRoomSchedule.ScheduleHeaderSegment _headerSegment;
  [SerializeField]
  private List<LockerRoomSchedule.ScheduleWeekSegment> _allWeekSegments = new List<LockerRoomSchedule.ScheduleWeekSegment>();
  [SerializeField]
  private MeshRenderer _bg;
  [SerializeField]
  private Material _regularBGMat;
  [SerializeField]
  private Material _playoffBGMat;
  [SerializeField]
  private List<LockerRoomSchedule.PlayoffSegment> _playoffSegments = new List<LockerRoomSchedule.PlayoffSegment>();
  private static readonly string _recordFormatString = "({0}-{1})";
  private bool _initialized;

  private void Start()
  {
    SeasonModeManager.self.OnInitComplete += new System.Action(this.Refresh);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged += new System.Action(this.OnTeamChanged);
  }

  private void OnDestroy()
  {
    if ((bool) (UnityEngine.Object) SeasonModeManager.self)
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.Refresh);
    if (!((UnityEngine.Object) SingletonBehaviour<PersistentData, MonoBehaviour>.instance != (UnityEngine.Object) null))
      return;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.OnUserTeamChanged -= new System.Action(this.OnTeamChanged);
  }

  private void OnTeamChanged()
  {
    if (!this._initialized)
      return;
    this.Refresh();
  }

  private void Refresh()
  {
    SeasonModeManager self = SeasonModeManager.self;
    int seasonWeek = PersistentData.seasonWeek;
    TeamData playerTeamData = self.userTeamData;
    SeasonModeTeamGameResults seasonTeamGameResults = self.GetSeasonTeamGameResults(playerTeamData.TeamIndex);
    TeamDataStore[] teamData = SeasonTeamDataHolder.GetTeamData();
    Sprite logo1 = ((IEnumerable<TeamDataStore>) teamData).First<TeamDataStore>((Func<TeamDataStore, bool>) (team => team.TeamName.ToUpper().Equals(playerTeamData.GetName().ToUpper()))).Logo;
    this._headerSegment.UpdateHeader(logo1, playerTeamData.CurrentSeasonStats.wins, playerTeamData.CurrentSeasonStats.losses);
    this._headerSegment.EnableHeader(true);
    if (seasonWeek - 1 < self.seasonModeData.NumberOfWeeksInSeason)
    {
      this._regularParent.SetActive(true);
      this._playoffParent.SetActive(false);
      this._bg.material = this._regularBGMat;
      Transform transform = this._currentWeekCircle.transform;
      transform.SetParent(this._allWeekSegments[seasonWeek - 1].CurrentWeekMarkerParent);
      transform.localPosition = Vector3.zero;
      this._currentWeekCircle.gameObject.SetActive(true);
      for (int index = 0; index < self.seasonModeData.NumberOfWeeksInSeason && index < 18; ++index)
      {
        this._allWeekSegments[index].EnableSegment(true);
        int teamInWeek = self.FindTeamInWeek(self.seasonModeData.UserTeamIndex, self.seasonModeData.currentWeek);
        int teamIndexInWeek;
        int opponentIndexInWeek;
        int teamOpponentForWeek = self.GetTeamOpponentForWeek(playerTeamData.TeamIndex, index + 1, out teamIndexInWeek, out opponentIndexInWeek);
        if ((teamInWeek == -1 ? 1 : (teamOpponentForWeek == -1 ? 1 : 0)) != 0)
        {
          this._allWeekSegments[index].UpdateWeekInfo_Bye(index + 1);
          this._allWeekSegments[index].EnableScoresAndResult(false);
        }
        else
        {
          bool flag1 = teamIndexInWeek < opponentIndexInWeek;
          TeamData opponentTeamData = self.GetTeamData(teamOpponentForWeek);
          Sprite logo2 = ((IEnumerable<TeamDataStore>) teamData).First<TeamDataStore>((Func<TeamDataStore, bool>) (team => team.TeamName.ToUpper().Equals(opponentTeamData.GetName().ToUpper()))).Logo;
          if (flag1)
            this._allWeekSegments[index].UpdateWeekInfo(index + 1, logo2, logo1, LockerRoomSchedule.FormatName(opponentTeamData.GetName()), LockerRoomSchedule.FormatName(playerTeamData.GetName()));
          else
            this._allWeekSegments[index].UpdateWeekInfo(index + 1, logo1, logo2, LockerRoomSchedule.FormatName(playerTeamData.GetName()), LockerRoomSchedule.FormatName(opponentTeamData.GetName()));
          if (index < seasonWeek - 1)
          {
            SeasonModeGameInfo game = seasonTeamGameResults.games[index];
            bool flag2 = (flag1 ? game.homeScore : game.awayScore) > (flag1 ? game.awayScore : game.homeScore);
            this._allWeekSegments[index].UpdateWeekScores(game.awayScore, game.homeScore, flag2 ? this._matchWinSprite : this._matchLossSprite);
            this._allWeekSegments[index].EnableScoresAndResult(true);
          }
          else
            this._allWeekSegments[index].EnableScoresAndResult(false);
        }
      }
    }
    else if (self.seasonModeData.currentWeek < self.seasonModeData.NumberOfWeeksInSeason + self.seasonModeData.NumberOfWeeksInPlayoffs + 1)
    {
      this._regularParent.SetActive(false);
      this._playoffParent.SetActive(true);
      this._bg.material = this._playoffBGMat;
      int num = seasonWeek - self.seasonModeData.NumberOfWeeksInSeason;
      Transform transform = this._currentWeekCircle.transform;
      transform.SetParent(this._playoffSegments[num - 1]._currentWeekMarkerParent);
      transform.localPosition = Vector3.zero;
      transform.localScale *= 2f;
      this._currentWeekCircle.gameObject.SetActive(true);
      int userTeamIndex = self.seasonModeData.UserTeamIndex;
      for (int index1 = 0; index1 < num; ++index1)
      {
        int[] playoffScheduleByWeek = self.seasonModeData.GetPlayoffScheduleByWeek(index1);
        int week = self.seasonModeData.NumberOfWeeksInSeason + index1 + 1;
        int teamInWeek = self.FindTeamInWeek(self.seasonModeData.UserTeamIndex, week);
        bool flag3 = teamInWeek % 2 == 0;
        int index2 = flag3 ? teamInWeek + 1 : teamInWeek - 1;
        if ((teamInWeek == -1 ? 1 : (index2 == -1 ? 1 : 0)) != 0)
        {
          this._playoffSegments[index1].UpdatePlayoffInfo_Bye();
        }
        else
        {
          TeamData opponentTeamData = self.GetTeamData(playoffScheduleByWeek[index2]);
          Sprite logo3 = ((IEnumerable<TeamDataStore>) teamData).First<TeamDataStore>((Func<TeamDataStore, bool>) (team => team.TeamName.ToUpper().Equals(opponentTeamData.GetName().ToUpper()))).Logo;
          if (flag3)
            this._playoffSegments[index1].UpdatePlayoffInfo(logo3, logo1, LockerRoomSchedule.FormatName(opponentTeamData.GetName()), LockerRoomSchedule.FormatName(playerTeamData.GetName()));
          else
            this._playoffSegments[index1].UpdatePlayoffInfo(logo1, logo3, LockerRoomSchedule.FormatName(playerTeamData.GetName()), LockerRoomSchedule.FormatName(opponentTeamData.GetName()));
          if (index1 < num - 1)
          {
            GameSummary[] playoffScoresByWeek = self.seasonModeData.GetPlayoffScoresByWeek(index1);
            if ((playoffScoresByWeek == null || playoffScheduleByWeek == null || teamInWeek == -1 ? 0 : (index2 != -1 ? 1 : 0)) != 0)
            {
              int score1 = playoffScoresByWeek[teamInWeek].TeamGameStats.Score;
              int score2 = playoffScoresByWeek[index2].TeamGameStats.Score;
              bool flag4 = score1 > score2;
              int homeScore = flag3 ? score1 : score2;
              int awayScore = flag3 ? score2 : score1;
              this._playoffSegments[index1].UpdateWeekScores(awayScore, homeScore, flag4 ? this._matchWinSprite : this._matchLossSprite);
              this._playoffSegments[index1].EnableScoresAndResult(true);
            }
            else
              Debug.LogError((object) ("Attempting to get info for playoff games for week " + index1.ToString() + "resulted in an error"));
          }
          else
            this._playoffSegments[index1].EnableScoresAndResult(false);
        }
      }
      for (int index = num; index < 4; ++index)
        this._playoffSegments[index].UpdatePlayoffInfo_TBD();
    }
    this._initialized = true;
  }

  private static string FormatName(string unformattedName) => unformattedName.Contains(" ") ? string.Join(" ", (IEnumerable<string>) ((IEnumerable<string>) unformattedName.Split(' ', StringSplitOptions.None)).ToList<string>().ConvertAll<string>((Converter<string, string>) (word => word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower()))) : unformattedName.Substring(0, 1).ToUpper() + unformattedName.Substring(1).ToLower();

  [Serializable]
  private class ScheduleHeaderSegment
  {
    [SerializeField]
    private GameObject _parentMostObject;
    [SerializeField]
    private Image _playerTeamIcon;
    [SerializeField]
    private TextMeshProUGUI _winLossTiesTxt;

    public void UpdateHeader(Sprite teamIcon, int wins, int losses)
    {
      this._playerTeamIcon.sprite = teamIcon;
      this._winLossTiesTxt.SetText(string.Format(LockerRoomSchedule._recordFormatString, (object) wins, (object) losses));
    }

    public void EnableHeader(bool enabled) => this._parentMostObject.SetActive(enabled);
  }

  [Serializable]
  private class ScheduleWeekSegment
  {
    [SerializeField]
    private GameObject _parentObject;
    [SerializeField]
    private TextMeshProUGUI _weekNumberTxt;
    [SerializeField]
    private Image _awayTeamIcon;
    [SerializeField]
    private Image _homeTeamIcon;
    [SerializeField]
    private TextMeshProUGUI _awayTeamNameTxt;
    [SerializeField]
    private TextMeshProUGUI _homeTeamNameTxt;
    [SerializeField]
    private GameObject _byeWeekTxt;
    [SerializeField]
    private TextMeshProUGUI _playerScoreTxt;
    [SerializeField]
    private TextMeshProUGUI _pcScoreTxt;
    [SerializeField]
    private Image _matchResultIcon;

    [field: SerializeField]
    public Transform CurrentWeekMarkerParent { get; private set; }

    public void EnableSegment(bool enabled) => this._parentObject.SetActive(enabled);

    public void UpdateWeekInfo(
      int weekNumber,
      Sprite awayTeam,
      Sprite homeTeam,
      string awayTeamName,
      string homeTeamName)
    {
      this._awayTeamIcon.gameObject.SetActive(true);
      this._homeTeamIcon.gameObject.SetActive(true);
      this._awayTeamNameTxt.gameObject.SetActive(true);
      this._homeTeamNameTxt.gameObject.SetActive(true);
      this._weekNumberTxt.SetText(weekNumber.ToString());
      this._awayTeamIcon.sprite = awayTeam;
      this._homeTeamIcon.sprite = homeTeam;
      this._awayTeamNameTxt.SetText(awayTeamName);
      this._homeTeamNameTxt.SetText(homeTeamName);
      this._byeWeekTxt.SetActive(false);
    }

    public void UpdateWeekInfo_Bye(int weekNumber)
    {
      this._weekNumberTxt.text = weekNumber.ToString();
      this._awayTeamIcon.gameObject.SetActive(false);
      this._homeTeamIcon.gameObject.SetActive(false);
      this._awayTeamNameTxt.gameObject.SetActive(false);
      this._homeTeamNameTxt.gameObject.SetActive(false);
      this._byeWeekTxt.SetActive(true);
    }

    public void UpdateWeekScores(int playerScore, int pcScore, Sprite matchResultSprite)
    {
      this._playerScoreTxt.SetText(playerScore.ToString());
      this._pcScoreTxt.SetText(pcScore.ToString());
      this._matchResultIcon.sprite = matchResultSprite;
    }

    public void EnableScoresAndResult(bool enabled)
    {
      this._playerScoreTxt.gameObject.SetActive(enabled);
      this._pcScoreTxt.gameObject.SetActive(enabled);
      this._matchResultIcon.gameObject.SetActive(enabled);
    }
  }

  [Serializable]
  private class PlayoffSegment
  {
    [SerializeField]
    private GameObject _hasOpponentObject;
    [SerializeField]
    private GameObject _noOpponentObject;
    [SerializeField]
    private Image _awayTeamIcon;
    [SerializeField]
    private Image _homeTeamIcon;
    [SerializeField]
    private TextMeshProUGUI _awayTeamNameTxt;
    [SerializeField]
    private TextMeshProUGUI _homeTeamNameTxt;
    [SerializeField]
    private LocalizeStringEvent _byeWeekTxt;
    [SerializeField]
    private TextMeshProUGUI _awayTeamScoreTxt;
    [SerializeField]
    private TextMeshProUGUI _homeTeamScoreTxt;
    [SerializeField]
    private Image _matchResultIcon;
    [SerializeField]
    public Transform _currentWeekMarkerParent;
    private const string LocalizationByeWeek = "LockerSchedule_Text_ByeWeek";
    private const string LocalizationTBD = "LockerSchedule_Text_TBD";

    public void UpdatePlayoffInfo(
      Sprite awayTeam,
      Sprite homeTeam,
      string awayTeamName,
      string homeTeamName)
    {
      this._noOpponentObject.SetActive(false);
      this._hasOpponentObject.SetActive(true);
      this._awayTeamIcon.sprite = awayTeam;
      this._homeTeamIcon.sprite = homeTeam;
      this._awayTeamNameTxt.SetText(awayTeamName);
      this._homeTeamNameTxt.SetText(homeTeamName);
    }

    public void UpdatePlayoffInfo_Bye()
    {
      this._noOpponentObject.SetActive(true);
      this._hasOpponentObject.SetActive(false);
      this._byeWeekTxt.StringReference.TableEntryReference = (TableEntryReference) "LockerSchedule_Text_ByeWeek";
    }

    public void UpdatePlayoffInfo_TBD()
    {
      this._noOpponentObject.SetActive(true);
      this._hasOpponentObject.SetActive(false);
      this._byeWeekTxt.StringReference.TableEntryReference = (TableEntryReference) "LockerSchedule_Text_TBD";
    }

    public void UpdateWeekScores(int awayScore, int homeScore, Sprite matchResultSprite)
    {
      this._awayTeamScoreTxt.SetText(awayScore.ToString());
      this._homeTeamScoreTxt.SetText(homeScore.ToString());
      this._matchResultIcon.sprite = matchResultSprite;
    }

    public void EnableScoresAndResult(bool enabled)
    {
      this._awayTeamScoreTxt.gameObject.SetActive(enabled);
      this._homeTeamScoreTxt.gameObject.SetActive(enabled);
      this._matchResultIcon.gameObject.SetActive(enabled);
    }
  }
}
