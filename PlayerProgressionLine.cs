// Decompiled with JetBrains decompiler
// Type: PlayerProgressionLine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class PlayerProgressionLine : MonoBehaviour
{
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private GameObject darkCover_GO;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI[] attributeList_Txt;
  [SerializeField]
  private TextMeshProUGUI[] attributeListHighlighted_Txt;
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private UnityEngine.UI.Button selector_Btn;
  private int lineIndex;
  private PlayerData playerData;

  public void Init(int index) => this.lineIndex = index;

  public void ClearLine()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void SetAttributeValues(
    PlayerData currentPlayerData,
    PlayerData oldPlayerData,
    int[] attributeOrder)
  {
    this.mainWindow_CG.alpha = 1f;
    this.mainWindow_CG.blocksRaycasts = true;
    this.selector_Btn.interactable = true;
    this.playerData = currentPlayerData;
    this.playerName_Txt.text = this.playerData.FullName;
    this.attributeList_Txt[0].text = this.GetAttributeText(this.playerData.GetOverall(), oldPlayerData.GetOverall(), 0);
    this.attributeListHighlighted_Txt[0].text = this.attributeList_Txt[0].text;
    for (int attributeIndex1 = 1; attributeIndex1 < this.attributeList_Txt.Length; ++attributeIndex1)
    {
      int attributeIndex2 = attributeOrder[attributeIndex1 - 1];
      int attributeByIndex1 = oldPlayerData.GetAttributeByIndex(attributeIndex2);
      int attributeByIndex2 = this.playerData.GetAttributeByIndex(attributeIndex2);
      this.attributeList_Txt[attributeIndex1].text = this.GetAttributeText(attributeByIndex2, attributeByIndex1, attributeIndex1);
      this.attributeListHighlighted_Txt[attributeIndex1].text = this.attributeList_Txt[attributeIndex1].text;
    }
  }

  private string GetAttributeText(int newValue, int oldValue, int attributeIndex)
  {
    int num = newValue - oldValue;
    string str;
    if (num == 0)
    {
      str = "";
      this.attributeList_Txt[attributeIndex].color = Color.white;
    }
    else if (num > 0)
    {
      str = " (+" + num.ToString() + ")";
      this.attributeList_Txt[attributeIndex].color = AxisFootballColors.green;
    }
    else
    {
      str = " (" + num.ToString() + ")";
      this.attributeList_Txt[attributeIndex].color = AxisFootballColors.red;
    }
    return TeamData.GetAttributeGradeFromNumber(newValue) + str;
  }

  public void HighlightLine() => this.selector_Btn.Select();

  public void SelectLine()
  {
  }

  public void ShowDarkBackground() => this.darkCover_GO.SetActive(true);

  public void HideDarkBackground() => this.darkCover_GO.SetActive(false);
}
