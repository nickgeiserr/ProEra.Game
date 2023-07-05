// Decompiled with JetBrains decompiler
// Type: TeamStandingItem_Window
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamStandingItem_Window : MonoBehaviour
{
  [SerializeField]
  private Image playoffIndicator_Img;
  [SerializeField]
  private TextMeshProUGUI rank_Txt;
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private TextMeshProUGUI teamName_Txt;
  [SerializeField]
  private TextMeshProUGUI record_Txt;
  [SerializeField]
  private TextMeshProUGUI plusMinus_Txt;
  [SerializeField]
  private TextMeshProUGUI streak_Txt;
  [SerializeField]
  private TextMeshProUGUI winPerc_Txt;
  [SerializeField]
  private GameObject darkBackground_GO;
  [SerializeField]
  private GameObject userBackground_GO;

  public void SetAsPromotionSeed() => this.playoffIndicator_Img.color = AxisFootballColors.green;

  public void SetAsDemotionSeed() => this.playoffIndicator_Img.color = AxisFootballColors.red;

  public void ClearPlayoffIndicator() => this.playoffIndicator_Img.color = Color.clear;

  public void SetAsConferenceLeader() => this.playoffIndicator_Img.color = AxisFootballColors.brightBlue;

  public void SetAsDivisionLeader() => this.playoffIndicator_Img.color = AxisFootballColors.green;

  public void SetAsWildcard() => this.playoffIndicator_Img.color = AxisFootballColors.yellow;

  public void SetRank(int rank) => this.rank_Txt.text = rank.ToString();

  public void SetTeamData(TeamData team)
  {
    this.teamLogo_Img.sprite = team.GetTinyLogo();
    this.teamName_Txt.text = team.GetFullDisplayName();
  }

  public void SetRecord(int wins, int losses) => this.record_Txt.text = wins.ToString() + "-" + losses.ToString();

  public void SetPlusMinus(int plusMinus)
  {
    if (plusMinus == 0)
      this.plusMinus_Txt.text = "0";
    else if (plusMinus < 0)
      this.plusMinus_Txt.text = "-" + Mathf.Abs(plusMinus).ToString();
    else
      this.plusMinus_Txt.text = "+" + plusMinus.ToString();
  }

  public void SetStreak(int streak)
  {
    if (streak == 0)
    {
      this.streak_Txt.text = "-";
      this.streak_Txt.color = Color.white;
    }
    else if (streak < 0)
    {
      this.streak_Txt.color = AxisFootballColors.red;
      this.streak_Txt.text = "L" + Mathf.Abs(streak).ToString();
    }
    else
    {
      this.streak_Txt.color = AxisFootballColors.green;
      this.streak_Txt.text = "W" + streak.ToString();
    }
  }

  public void SetWinPercentage(int wins, int losses)
  {
    int num = wins + losses;
    if (num == 0)
      this.winPerc_Txt.text = ".000";
    else if (wins == num)
      this.winPerc_Txt.text = "1.00";
    else
      this.winPerc_Txt.text = ((float) wins / (float) num).ToString("F3").Substring(1, 4);
  }

  public void ColorBackground_Dark()
  {
    this.darkBackground_GO.SetActive(true);
    this.userBackground_GO.SetActive(false);
  }

  public void ColorBackground_User()
  {
    this.darkBackground_GO.SetActive(false);
    this.userBackground_GO.SetActive(true);
  }

  public void ColorBackground_None()
  {
    this.darkBackground_GO.SetActive(false);
    this.userBackground_GO.SetActive(false);
  }
}
