// Decompiled with JetBrains decompiler
// Type: PopupStatsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupStatsManager : MonoBehaviour, IPopupStats
{
  [Header("Main Window")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Transform mainWindow_Trans;
  [Header("Drive Stats")]
  [SerializeField]
  private GameObject driveStats_GO;
  [SerializeField]
  private TextMeshProUGUI driveStats_TeamCity_Txt;
  [SerializeField]
  private Image driveStats_TeamLogo_Img;
  [SerializeField]
  private Image driveStats_TeamBackground_Img;
  [SerializeField]
  private TextMeshProUGUI driveStats_TimeOfPossession_Txt;
  [SerializeField]
  private TextMeshProUGUI driveStats_Plays_Txt;
  [SerializeField]
  private TextMeshProUGUI driveStats_RushPlays_Txt;
  [SerializeField]
  private TextMeshProUGUI driveStats_PassPlays_Txt;
  [SerializeField]
  private TextMeshProUGUI driveStats_Yards_Txt;
  [SerializeField]
  private TextMeshProUGUI driveStats_RushYards_Txt;
  [SerializeField]
  private TextMeshProUGUI driveStats_PassYards_Txt;
  [Header("Player Stats")]
  [SerializeField]
  private GameObject playerStats_GO;
  [SerializeField]
  private TextMeshProUGUI playerStats_TeamCity_Txt;
  [SerializeField]
  private Image playerStats_TeamLogo_Img;
  [SerializeField]
  private Image playerStats_TeamBackground_Img;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI playerNumber_Txt;
  [SerializeField]
  private TextMeshProUGUI playerPosition_Txt;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [Header("QB Stats")]
  [SerializeField]
  private GameObject qbStatsSection_GO;
  [SerializeField]
  private TextMeshProUGUI qbStats_Completions_Txt;
  [SerializeField]
  private TextMeshProUGUI qbStats_PassYards_Txt;
  [SerializeField]
  private TextMeshProUGUI qbStats_Touchdowns_Txt;
  [SerializeField]
  private TextMeshProUGUI qbStats_Interceptions_Txt;
  [Header("Offensive Stats")]
  [SerializeField]
  private GameObject offensiveStatsSection_GO;
  [SerializeField]
  private TextMeshProUGUI offStats_Rushes_Txt;
  [SerializeField]
  private TextMeshProUGUI offStats_Receptions_Txt;
  [SerializeField]
  private TextMeshProUGUI offStats_Touchdowns_Txt;
  [SerializeField]
  private TextMeshProUGUI offStats_RushYards_Txt;
  [SerializeField]
  private TextMeshProUGUI offStats_ReceivingYards_Txt;
  [SerializeField]
  private TextMeshProUGUI offStats_Targets_Txt;
  [Header("Defensive Stats")]
  [SerializeField]
  private GameObject defensiveStatsSection_GO;
  [SerializeField]
  private TextMeshProUGUI defStats_Tackles_Txt;
  [SerializeField]
  private TextMeshProUGUI defStats_Interceptions_Txt;
  [SerializeField]
  private TextMeshProUGUI defStats_DeflectedPasses_Txt;
  [SerializeField]
  private TextMeshProUGUI defStats_Sacks_Txt;
  [SerializeField]
  private TextMeshProUGUI defStats_FumbleRecoveries_Txt;
  private PopupWindow popWindowChoice;
  private TeamData teamData;
  private PlayerData playerData;
  private PlayerStats playerStats;

  private void Awake() => ProEra.Game.Sources.UI.PopupStats = (IPopupStats) this;

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void ShowWindow()
  {
    if (ProEra.Game.MatchState.Stats.CurrentDrivePlays == 0)
      return;
    this.driveStats_GO.SetActive(false);
    this.playerStats_GO.SetActive(false);
    this.qbStatsSection_GO.SetActive(false);
    this.offensiveStatsSection_GO.SetActive(false);
    this.defensiveStatsSection_GO.SetActive(false);
    switch (this.popWindowChoice)
    {
      case PopupWindow.DRIVE:
        this.driveStats_GO.SetActive(true);
        break;
      case PopupWindow.QB:
        this.playerStats_GO.SetActive(true);
        this.qbStatsSection_GO.SetActive(true);
        break;
      case PopupWindow.OFF:
        this.playerStats_GO.SetActive(true);
        this.offensiveStatsSection_GO.SetActive(true);
        break;
      case PopupWindow.DEF:
        this.playerStats_GO.SetActive(true);
        this.defensiveStatsSection_GO.SetActive(true);
        break;
    }
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
  }

  public void HideWindow() => this.mainWindow_CG.alpha = 0.0f;

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void SetupPopupStatsWindow()
  {
    this.popWindowChoice = this.DetermineWhichWindowToShow();
    if (this.popWindowChoice == PopupWindow.DRIVE)
      this.UpdateDriveStats();
    else if (this.popWindowChoice == PopupWindow.QB && PlayStatsUtils.HasValidQbReference())
      this.UpdateQBStats((int) PlayStats.QbIndexReference);
    else if (this.popWindowChoice == PopupWindow.OFF && PlayStatsUtils.HasValidOffPlayerReference())
      this.UpdateOffensivePlayerStats((int) PlayStats.OffPlayerReference);
    else if (this.popWindowChoice == PopupWindow.DEF && PlayStatsUtils.HasValidDefPlayerReference())
    {
      this.UpdateDefensivePlayerStats((int) PlayStats.DefPlayerReference);
    }
    else
    {
      this.popWindowChoice = PopupWindow.DRIVE;
      this.UpdateDriveStats();
    }
  }

  private PopupWindow DetermineWhichWindowToShow()
  {
    int num1 = Random.Range(1, 11);
    int num2 = 6;
    if (ProEra.Game.MatchState.Stats.DrivePassPlays + ProEra.Game.MatchState.Stats.DriveRunPlays == 0)
      num1 = 1;
    return num1 >= num2 ? PopupWindow.DRIVE : (MatchManager.instance.lastPlayYardsGained >= 0 ? (!PlayState.IsPass || !global::Game.PET_IsIncomplete ? (!PlayState.IsPass ? PopupWindow.OFF : (Random.Range(0, 2) % 2 != 0 ? PopupWindow.OFF : PopupWindow.QB)) : PopupWindow.QB) : PopupWindow.DEF);
  }

  private void UpdateDriveStats()
  {
    int num = ProEra.Game.MatchState.Stats.DriveTimeInSeconds % 60;
    this.driveStats_TimeOfPossession_Txt.text = string.Format("{0}:{1}", (object) (ProEra.Game.MatchState.Stats.DriveTimeInSeconds / 60 % 60).ToString("00"), (object) num.ToString("00"));
    this.driveStats_Plays_Txt.text = ProEra.Game.MatchState.Stats.CurrentDrivePlays.ToString();
    this.driveStats_PassPlays_Txt.text = ProEra.Game.MatchState.Stats.DrivePassPlays.ToString();
    this.driveStats_RushPlays_Txt.text = ProEra.Game.MatchState.Stats.DriveRunPlays.ToString();
    this.driveStats_Yards_Txt.text = ProEra.Game.MatchState.Stats.DriveTotalYards.ToString();
    this.driveStats_PassYards_Txt.text = ProEra.Game.MatchState.Stats.DrivePassYards.ToString();
    this.driveStats_RushYards_Txt.text = ProEra.Game.MatchState.Stats.DriveRunYards.ToString();
    this.teamData = global::Game.IsPlayerOneOnOffense ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam();
    this.driveStats_TeamBackground_Img.color = this.teamData.GetPrimaryColor();
    this.driveStats_TeamLogo_Img.sprite = this.teamData.GetMediumLogo();
    this.driveStats_TeamCity_Txt.text = this.teamData.GetCity().ToUpper();
  }

  private void UpdateQBStats(int playerIndex)
  {
    this.teamData = global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.userTeamData : MatchManager.instance.playersManager.compTeamData;
    this.playerData = this.teamData.GetPlayer(playerIndex);
    this.playerStats = this.playerData.CurrentGameStats;
    this.SetPlayerInformation();
    this.SetPlayerStatsTeamInformation();
    this.qbStats_Completions_Txt.text = this.playerStats.QBCompletions.ToString() + "/" + this.playerStats.QBAttempts.ToString();
    this.qbStats_PassYards_Txt.text = this.playerStats.QBPassYards.ToString();
    this.qbStats_Touchdowns_Txt.text = this.playerStats.QBPassTDs.ToString();
    this.qbStats_Interceptions_Txt.text = this.playerStats.QBInts.ToString();
  }

  private void UpdateOffensivePlayerStats(int playerIndex)
  {
    this.teamData = global::Game.IsPlayerOneOnOffense ? MatchManager.instance.playersManager.userTeamData : MatchManager.instance.playersManager.compTeamData;
    this.playerData = this.teamData.GetPlayer(playerIndex);
    this.playerStats = this.playerData.CurrentGameStats;
    this.SetPlayerInformation();
    this.SetPlayerStatsTeamInformation();
    this.offStats_Rushes_Txt.text = this.playerStats.RushAttempts.ToString();
    this.offStats_Receptions_Txt.text = this.playerStats.Receptions.ToString();
    this.offStats_Touchdowns_Txt.text = (this.playerStats.RushTDs + this.playerStats.ReceivingTDs).ToString();
    this.offStats_RushYards_Txt.text = this.playerStats.RushYards.ToString();
    this.offStats_ReceivingYards_Txt.text = this.playerStats.ReceivingYards.ToString();
    this.offStats_Targets_Txt.text = this.playerStats.Targets.ToString();
  }

  private void UpdateDefensivePlayerStats(int playerIndex)
  {
    this.teamData = global::Game.IsPlayerOneOnDefense ? MatchManager.instance.playersManager.userTeamData : MatchManager.instance.playersManager.compTeamData;
    this.playerData = this.teamData.GetPlayer(playerIndex);
    this.playerStats = this.playerData.CurrentGameStats;
    this.SetPlayerInformation();
    this.SetPlayerStatsTeamInformation();
    this.defStats_Tackles_Txt.text = this.playerStats.Tackles.ToString();
    this.defStats_Interceptions_Txt.text = this.playerStats.Interceptions.ToString();
    this.defStats_Sacks_Txt.text = this.playerStats.Sacks.ToString();
    this.defStats_DeflectedPasses_Txt.text = this.playerStats.KnockDowns.ToString();
    this.defStats_FumbleRecoveries_Txt.text = this.playerStats.FumbleRecoveries.ToString();
  }

  private void SetPlayerInformation()
  {
    this.playerNumber_Txt.text = this.playerData.Number.ToString();
    this.playerPosition_Txt.text = this.playerData.PlayerPosition.ToString();
    this.playerName_Txt.text = this.playerData.FullName.ToUpper();
    this.playerPortrait_Img.sprite = PortraitManager.self.GetPlayerPortrait(this.playerData, this.playerData.OnUserTeam);
  }

  private void SetPlayerStatsTeamInformation()
  {
    this.playerStats_TeamBackground_Img.color = this.teamData.GetPrimaryColor();
    this.playerStats_TeamLogo_Img.sprite = this.teamData.GetMediumLogo();
    this.playerStats_TeamCity_Txt.text = this.teamData.GetCity().ToUpper();
  }
}
