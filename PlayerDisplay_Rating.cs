// Decompiled with JetBrains decompiler
// Type: PlayerDisplay_Rating
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_Rating : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerInfo_Txt;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI cardTitle_Txt;
  [SerializeField]
  private TextMeshProUGUI overall_Txt;

  public void ShowWindow() => this.mainWindow_GO.SetActive(true);

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public void SetData(string title, PlayerData player)
  {
    this.cardTitle_Txt.text = title;
    this.playerName_Txt.text = player.FirstInitalAndLastName;
    this.playerInfo_Txt.text = player.PlayerPosition.ToString() + "  |  #" + player.Number.ToString();
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    this.overall_Txt.text = TeamData.GetAttributeGradeFromNumber(player.GetOverall());
    this.ShowWindow();
  }
}
