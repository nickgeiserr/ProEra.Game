// Decompiled with JetBrains decompiler
// Type: StarPlayerSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarPlayerSlot : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI rating_Txt;
  [SerializeField]
  private TextMeshProUGUI position_Txt;
  [SerializeField]
  private TextMeshProUGUI number_Txt;
  [SerializeField]
  private Image portrait_Img;

  public void ShowPlayer(PlayerData player)
  {
    this.portrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    this.playerName_Txt.text = player.FirstInitalAndLastName;
    this.rating_Txt.text = TeamData.GetAttributeGradeFromNumber(player.GetOverall());
    this.position_Txt.text = player.PlayerPosition.ToString();
    this.number_Txt.text = player.Number.ToString();
  }
}
