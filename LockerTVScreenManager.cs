// Decompiled with JetBrains decompiler
// Type: LockerTVScreenManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockerTVScreenManager : MonoBehaviour
{
  [SerializeField]
  private CanvasGroupFadeSwitch m_leftScreenFade;
  [SerializeField]
  private CanvasGroupFadeSwitch m_centerScreenFade;
  [SerializeField]
  private CanvasGroupFadeSwitch m_rightScreenFade;
  [SerializeField]
  private Sprite m_placeholderTeamIcon;
  [Header("ACF 1st Round")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R1_1;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R1_2;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R1_3;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R1_4;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R1_5;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R1_6;
  [Header("ACF 2nd Round")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R2_1;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R2_2;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R2_3;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_R2_4;
  [Header("ACF Semi-finals")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_SF_1;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_SF_2;
  [Header("ACF Finals")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_AFC_F;
  [Header("NFC 1st Round")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R1_1;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R1_2;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R1_3;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R1_4;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R1_5;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R1_6;
  [Header("NFC 2nd Round")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R2_1;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R2_2;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R2_3;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_R2_4;
  [Header("NFC Semi-finals")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_SF_1;
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_SF_2;
  [Header("NFC Finals")]
  [SerializeField]
  private LockerTvElement m_teamSeasonBracket_NFC_F;
  private SeasonModeManager m_seasonManager;
  private SGD_SeasonModeData m_seasonData;

  private void Start()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
      return;
    SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);
  }

  private void OnDestroy()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
      return;
    SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
  }

  private void Init() => this.StartCoroutine(this.InitDelay());

  private IEnumerator InitDelay()
  {
    yield return (object) new WaitForSeconds(1f);
    this.m_seasonManager = SeasonModeManager.self;
    this.m_seasonData = this.m_seasonManager.seasonModeData;
    this.InitializePlayoffScreens();
    if (this.m_seasonData.seasonState != ProEraSeasonState.RegularSeason)
      this.AllFadeToScreen(1);
    else
      this.AllFadeToScreen(0);
    MonoBehaviour.print((object) "Screen Init");
  }

  private void InitializePlayoffScreens() => this.InitializeAllPlayoffTeamElements();

  private void InitializeAllPlayoffTeamElements()
  {
    int[] scheduleByTierR1 = this.m_seasonData.GetPlayoffScheduleByTier_R1(1);
    if (scheduleByTierR1 != null && scheduleByTierR1.Length >= 12)
    {
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R1_1, scheduleByTierR1[0]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R1_2, scheduleByTierR1[1]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R1_3, scheduleByTierR1[2]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R1_4, scheduleByTierR1[3]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R1_5, scheduleByTierR1[4]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R1_6, scheduleByTierR1[5]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R1_1, scheduleByTierR1[6]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R1_2, scheduleByTierR1[7]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R1_3, scheduleByTierR1[8]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R1_4, scheduleByTierR1[9]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R1_5, scheduleByTierR1[10]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R1_6, scheduleByTierR1[11]);
    }
    int[] scheduleByTierR2 = this.m_seasonData.GetPlayoffScheduleByTier_R2(1);
    if (scheduleByTierR2 != null && scheduleByTierR2.Length >= 8)
    {
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R2_1, scheduleByTierR2[0]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R2_2, scheduleByTierR2[1]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R2_3, scheduleByTierR2[2]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_R2_4, scheduleByTierR2[3]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R2_1, scheduleByTierR2[4]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R2_2, scheduleByTierR2[5]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R2_3, scheduleByTierR2[6]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_R2_4, scheduleByTierR2[7]);
    }
    int[] scheduleByTierR3 = this.m_seasonData.GetPlayoffScheduleByTier_R3(1);
    if (scheduleByTierR3 != null && scheduleByTierR3.Length >= 4)
    {
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_SF_1, scheduleByTierR3[0]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_SF_2, scheduleByTierR3[1]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_SF_1, scheduleByTierR3[2]);
      this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_SF_2, scheduleByTierR3[3]);
    }
    int[] playoffScheduleR4 = this.m_seasonData.GetPlayoffSchedule_R4();
    if (playoffScheduleR4 == null || playoffScheduleR4.Length < 2)
      return;
    this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_AFC_F, playoffScheduleR4[0]);
    this.InitializePlayoffTeamElement(ref this.m_teamSeasonBracket_NFC_F, playoffScheduleR4[1]);
  }

  private void InitializePlayoffTeamElement(ref LockerTvElement a_teamElement, int a_teamIndex)
  {
    TextMeshProUGUI teamNameText = a_teamElement.TeamNameText;
    Image logo = a_teamElement.Logo;
    string str1 = "TBD";
    Sprite sprite = this.m_placeholderTeamIcon;
    if (a_teamIndex > -1)
    {
      TeamDataStore teamDataStore = SeasonTeamDataHolder.GetTeamData()[a_teamIndex];
      str1 = teamDataStore.TeamName;
      sprite = teamDataStore.Logo;
    }
    else
      logo.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    string str2 = str1;
    teamNameText.text = str2;
    logo.sprite = sprite;
  }

  public void AllFadeToScreen(int a_screen)
  {
    this.m_leftScreenFade.FadeToCanvas(a_screen, true);
    this.m_centerScreenFade.FadeToCanvas(a_screen, true);
    this.m_rightScreenFade.FadeToCanvas(a_screen, true);
  }

  [ContextMenu("Fade 0")]
  public void AllFadeTo0() => this.AllFadeToScreen(0);

  [ContextMenu("Fade 1")]
  public void AllFadeTo1() => this.AllFadeToScreen(1);

  [ContextMenu("Fade 2")]
  public void AllFadeTo2() => this.AllFadeToScreen(2);

  [ContextMenu("Fade 3")]
  public void AllFadeTo3() => this.AllFadeToScreen(3);
}
