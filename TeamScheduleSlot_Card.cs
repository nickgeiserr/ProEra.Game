// Decompiled with JetBrains decompiler
// Type: TeamScheduleSlot_Card
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamScheduleSlot_Card : MonoBehaviour
{
  public TextMeshProUGUI winOrLoss_Txt;
  public TextMeshProUGUI score_Txt;
  public Image logo;
  public TextMeshProUGUI week_Txt;
  public TextMeshProUGUI opponent_Txt;
  public TextMeshProUGUI opponentWhenHighlighted_Txt;
  public GameObject activeIndicator;

  public void HighlightLine()
  {
    this.activeIndicator.SetActive(true);
    this.opponentWhenHighlighted_Txt.gameObject.SetActive(true);
    this.opponentWhenHighlighted_Txt.text = this.opponent_Txt.text;
    this.opponent_Txt.gameObject.SetActive(false);
  }

  public void UnhighlightLine()
  {
    this.activeIndicator.SetActive(false);
    this.opponent_Txt.gameObject.SetActive(true);
    this.opponentWhenHighlighted_Txt.gameObject.SetActive(false);
  }

  public void SetAsLoss()
  {
    this.winOrLoss_Txt.text = "L";
    this.winOrLoss_Txt.color = AxisFootballColors.red;
  }

  public void SetAsWin()
  {
    this.winOrLoss_Txt.text = "W";
    this.winOrLoss_Txt.color = AxisFootballColors.green;
  }

  public void SetGameNotPlayed()
  {
    this.winOrLoss_Txt.text = "";
    this.score_Txt.text = "";
    this.winOrLoss_Txt.color = Color.white;
  }
}
