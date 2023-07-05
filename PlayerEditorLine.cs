// Decompiled with JetBrains decompiler
// Type: PlayerEditorLine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class PlayerEditorLine : MonoBehaviour
{
  public static PlayerEditor guiParent;
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private GameObject darkCover_GO;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI[] attributeList_Txt;
  [SerializeField]
  private UnityEngine.UI.Button selector_Btn;
  private int playerIndex;

  public void ClearLine()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void SetAttributeValues(PlayerData player, int[] attributeOrder)
  {
    this.mainWindow_CG.alpha = 1f;
    this.mainWindow_CG.blocksRaycasts = true;
    this.selector_Btn.interactable = true;
    this.playerIndex = player.IndexOnTeam;
    this.playerName_Txt.text = player.FullName;
    this.attributeList_Txt[0].text = TeamData.GetAttributeGradeFromNumber(player.GetOverall());
    for (int index = 1; index < this.attributeList_Txt.Length; ++index)
    {
      int attributeIndex = attributeOrder[index - 1];
      this.attributeList_Txt[index].text = TeamData.GetAttributeGradeFromNumber(player.GetAttributeByIndex(attributeIndex));
    }
  }

  public void SelectLine()
  {
    this.selector_Btn.Select();
    PlayerEditorLine.guiParent.ShowActivePlayer(this.playerIndex);
  }

  public int GetPlayerIndex() => this.playerIndex;

  public void ShowDarkBackground() => this.darkCover_GO.SetActive(true);

  public void HideDarkBackground() => this.darkCover_GO.SetActive(false);
}
