// Decompiled with JetBrains decompiler
// Type: PauseWindowPlayerDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindowPlayerDisplay : MonoBehaviour
{
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI playerNumber_Txt;
  [SerializeField]
  private TextMeshProUGUI playerPosition_Txt;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI completions_Txt;
  [SerializeField]
  private TextMeshProUGUI yards_Txt;
  [SerializeField]
  private TextMeshProUGUI td_Txt;
  [SerializeField]
  private TextMeshProUGUI int_Txt;

  public void SetPlayerDisplay(TeamData team, PlayerData player)
  {
    this.teamLogo_Img.sprite = team.GetMediumLogo();
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    this.playerNumber_Txt.text = player.Number.ToString();
    this.playerPosition_Txt.text = player.PlayerPosition.ToString();
    this.playerName_Txt.text = player.FullName;
    PlayerStats currentGameStats = player.CurrentGameStats;
    this.completions_Txt.text = currentGameStats.QBCompletions.ToString() + " / " + currentGameStats.QBAttempts.ToString();
    this.yards_Txt.text = currentGameStats.QBPassYards.ToString();
    this.td_Txt.text = currentGameStats.QBPassTDs.ToString();
    this.int_Txt.text = currentGameStats.QBInts.ToString();
  }
}
