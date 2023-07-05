// Decompiled with JetBrains decompiler
// Type: PlayerDisplaySlot_Small
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplaySlot_Small : MonoBehaviour
{
  [SerializeField]
  private GameObject playerDisplay_GO;
  [SerializeField]
  private Image portrait_Img;
  [SerializeField]
  private TextMeshProUGUI topLine_Txt;
  [SerializeField]
  private TextMeshProUGUI middleLine_Txt;
  [SerializeField]
  private TextMeshProUGUI bottomLine_Txt;

  public void ShowDisplay() => this.playerDisplay_GO.SetActive(true);

  public void HideDisplay() => this.playerDisplay_GO.SetActive(false);

  public void SetPlayerData(PlayerData player)
  {
    this.portrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    this.topLine_Txt.text = player.FullName;
    this.middleLine_Txt.text = player.PlayerPosition.ToString() + " | #" + player.Number.ToString() + " | AGE: " + player.Age.ToString();
    this.bottomLine_Txt.text = "OVR: " + TeamData.GetAttributeGradeFromNumber(player.GetOverall());
    this.ShowDisplay();
  }

  public void SetPlayerData(
    PlayerData player,
    string topLine,
    string middleLine,
    string bottomLine)
  {
    this.portrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    this.topLine_Txt.text = topLine;
    this.middleLine_Txt.text = middleLine;
    this.bottomLine_Txt.text = bottomLine;
    this.ShowDisplay();
  }

  public void SetCoachData(CoachData coach)
  {
    this.portrait_Img.sprite = PortraitManager.self.LoadCoachPortrait(coach.Skin, coach.Portrait, coach.Age);
    this.topLine_Txt.text = coach.FullName;
    this.middleLine_Txt.text = Common.EnumToString(coach.CoachPosition.ToString());
    this.bottomLine_Txt.text = "OVR: " + TeamData.GetAttributeGradeFromNumber(CoachData.GetOverall(coach, coach.CoachPosition));
    this.ShowDisplay();
  }
}
