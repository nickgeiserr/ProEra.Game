// Decompiled with JetBrains decompiler
// Type: CareerStatsLine
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class CareerStatsLine : MonoBehaviour
{
  [SerializeField]
  private GameObject activeCategoryIndicator_GO;
  [SerializeField]
  private RectTransform activeCategoryIndicator_Trans;
  [SerializeField]
  private GameObject darkCover_GO;
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private GameObject statLineContainer_GO;
  [SerializeField]
  private TextMeshProUGUI year_Txt;
  [SerializeField]
  private TextMeshProUGUI team_Txt;
  [SerializeField]
  private TextMeshProUGUI[] categories_Txt;
  [SerializeField]
  private UnityEngine.UI.Button selector_Btn;

  public void SetStatValues(PlayerStats statsForThisYear, int[] statOrder, int firstYear)
  {
    this.statLineContainer_GO.SetActive(true);
    this.selector_Btn.interactable = true;
    this.year_Txt.text = statsForThisYear.StatYear.ToString();
    this.team_Txt.text = statsForThisYear.StatYearTeam.ToString();
    this.activeCategoryIndicator_GO.SetActive(true);
    this.categories_Txt[0].text = (statsForThisYear.StatYear - firstYear + 1).ToString();
    for (int index = 0; index < statOrder.Length; ++index)
      this.categories_Txt[index + 1].text = statsForThisYear.GetStatByIndex(statOrder[index]);
    for (int index = statOrder.Length + 1; index < this.categories_Txt.Length; ++index)
      this.categories_Txt[index].text = "";
  }

  public void SetActiveCategory(int categoryIndex) => this.activeCategoryIndicator_Trans.anchoredPosition = (Vector2) this.categories_Txt[categoryIndex].rectTransform.localPosition;

  public float GetValueInTextBox(int i) => float.Parse(this.categories_Txt[i].text.TrimEnd('%'));

  public void ShowDarkBackground() => this.darkCover_GO.SetActive(true);

  public void HideDarkBackground() => this.darkCover_GO.SetActive(false);

  public void ClearLine()
  {
    this.statLineContainer_GO.SetActive(false);
    this.selector_Btn.interactable = false;
    this.activeCategoryIndicator_GO.SetActive(false);
  }

  public void SelectLine() => this.selector_Btn.Select();

  public void DeselectLine() => this.animator.SetTrigger(HashIDs.self.normal_Trigger);
}
