// Decompiled with JetBrains decompiler
// Type: SeasonMonitorStandingScreenSingle
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class SeasonMonitorStandingScreenSingle : MonoBehaviour
{
  [SerializeField]
  private SeasonMonitorStandingScreenSingle.Conference _conference = SeasonMonitorStandingScreenSingle.Conference.Afc;
  [SerializeField]
  private SeasonMonitorStandingScreenSingle.Division _division = SeasonMonitorStandingScreenSingle.Division.North;
  [SerializeField]
  private TextMeshProUGUI _headerText;
  [SerializeField]
  private SeasonMonitorTeamDisplay[] _teamDisplay;
  private int curDivision;
  private SeasonMonitorStandingScreenSingle.StandingsData[] StandingsDataArray;
  public bool m_autoPlay;
  public float m_autoPlayDelay = 8f;
  private float m_autoPlayTimer;
  public CanvasGroup m_thisCanvasGroup;
  private bool m_isFading;
  private bool m_initialized;

  private void Start() => SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);

  private void OnDestroy()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
      return;
    SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
  }

  private void Init()
  {
    this.ValidateInspectorBinding();
    this.GenerateStandingsData();
    this.curDivision = (int) this._division;
    this.FillData(this.curDivision);
    this.m_initialized = true;
  }

  private void GenerateStandingsData()
  {
    this.StandingsDataArray = new SeasonMonitorStandingScreenSingle.StandingsData[4];
    SeasonModeManager self = SeasonModeManager.self;
    SGD_SeasonModeData seasonModeData = self.seasonModeData;
    TeamDataStore[] teamData1 = SeasonTeamDataHolder.GetTeamData();
    int conference = (int) this._conference;
    for (int divisionIndex = 0; divisionIndex < 4; ++divisionIndex)
    {
      this.StandingsDataArray[divisionIndex].TeamStandingsDataArray = new SeasonMonitorStandingScreenSingle.TeamStandingData[this._teamDisplay.Length];
      int[] sortedTeamsInDivision = self.GetSortedTeamsInDivision(conference, divisionIndex + 1);
      string conferenceName = seasonModeData.GetConferenceName(conference - 1);
      string divisionName = seasonModeData.GetDivisionName(divisionIndex);
      this.StandingsDataArray[divisionIndex].DivisionName = conferenceName + " " + divisionName;
      for (int index = 0; index < this.StandingsDataArray[divisionIndex].TeamStandingsDataArray.Length; ++index)
      {
        TeamData teamData2 = self.GetTeamData(sortedTeamsInDivision[index]);
        TeamSeasonStats currentSeasonStats = teamData2.CurrentSeasonStats;
        this.StandingsDataArray[divisionIndex].TeamStandingsDataArray[index].Rank = (index + 1).ToString();
        this.StandingsDataArray[divisionIndex].TeamStandingsDataArray[index].TeamName = teamData2.GetName();
        this.StandingsDataArray[divisionIndex].TeamStandingsDataArray[index].logo = teamData1[teamData2.TeamIndex].Logo;
        this.StandingsDataArray[divisionIndex].TeamStandingsDataArray[index].Rec = currentSeasonStats.GetRecordString(TeamStatGameType.NonConference);
        this.StandingsDataArray[divisionIndex].TeamStandingsDataArray[index].Div = currentSeasonStats.GetRecordString(TeamStatGameType.Division);
        this.StandingsDataArray[divisionIndex].TeamStandingsDataArray[index].Stk = currentSeasonStats.GetStreakString();
      }
    }
  }

  private void FillData(int div)
  {
    this._headerText.text = this.StandingsDataArray[div].DivisionName;
    for (int index = 0; index < this._teamDisplay.Length; ++index)
    {
      this._teamDisplay[index].Rank.text = this.StandingsDataArray[div].TeamStandingsDataArray[index].Rank;
      this._teamDisplay[index].Name.text = this.StandingsDataArray[div].TeamStandingsDataArray[index].TeamName;
      this._teamDisplay[index].Logo.sprite = this.StandingsDataArray[div].TeamStandingsDataArray[index].logo;
      this._teamDisplay[index].Rec.text = this.StandingsDataArray[div].TeamStandingsDataArray[index].Rec;
      this._teamDisplay[index].Div.text = this.StandingsDataArray[div].TeamStandingsDataArray[index].Div;
      this._teamDisplay[index].Stk.text = this.StandingsDataArray[div].TeamStandingsDataArray[index].Stk;
    }
  }

  private void ValidateInspectorBinding()
  {
  }

  private void Update()
  {
    if (!this.m_autoPlay || !this.m_initialized)
      return;
    this.m_autoPlayTimer += Time.deltaTime;
    if ((double) this.m_autoPlayTimer < (double) this.m_autoPlayDelay)
      return;
    this.m_autoPlayTimer = 0.0f;
    this.FadeToNewScreen();
  }

  [ExecuteInEditMode]
  public void FadeToNewScreen() => this.StartCoroutine(this.FadeToNewScreenRoutine());

  [ExecuteInEditMode]
  private IEnumerator FadeToNewScreenRoutine()
  {
    SeasonMonitorStandingScreenSingle standingScreenSingle1 = this;
    if (!standingScreenSingle1.m_isFading)
    {
      standingScreenSingle1.m_isFading = true;
      float fadeSpeed = 2f;
      if ((UnityEngine.Object) standingScreenSingle1.m_thisCanvasGroup != (UnityEngine.Object) null)
      {
        standingScreenSingle1.m_thisCanvasGroup.alpha = 1f;
        while ((double) standingScreenSingle1.m_thisCanvasGroup.alpha > 0.0)
        {
          standingScreenSingle1.m_thisCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
          yield return (object) new WaitForEndOfFrame();
        }
      }
      SeasonMonitorStandingScreenSingle standingScreenSingle2 = standingScreenSingle1;
      int num1 = standingScreenSingle1.curDivision + 1;
      int num2 = num1;
      standingScreenSingle2.curDivision = num2;
      if (num1 >= standingScreenSingle1.StandingsDataArray.Length)
        standingScreenSingle1.curDivision = 0;
      standingScreenSingle1.FillData(standingScreenSingle1.curDivision);
      if ((UnityEngine.Object) standingScreenSingle1.m_thisCanvasGroup != (UnityEngine.Object) null)
      {
        while ((double) standingScreenSingle1.m_thisCanvasGroup.alpha < 1.0)
        {
          standingScreenSingle1.m_thisCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
          yield return (object) new WaitForEndOfFrame();
        }
      }
      standingScreenSingle1.m_isFading = false;
    }
  }

  private enum Conference
  {
    Afc = 1,
    Nfc = 2,
  }

  private enum Division
  {
    North = 1,
    South = 2,
    East = 3,
    West = 4,
  }

  public struct StandingsData
  {
    public string DivisionName;
    public SeasonMonitorStandingScreenSingle.TeamStandingData[] TeamStandingsDataArray;
  }

  public struct TeamStandingData
  {
    public string Rank;
    public string TeamName;
    public Sprite logo;
    public string Rec;
    public string Div;
    public string Stk;
  }
}
