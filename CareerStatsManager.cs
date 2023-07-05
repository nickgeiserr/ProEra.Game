// Decompiled with JetBrains decompiler
// Type: CareerStatsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CareerStatsManager : MonoBehaviour
{
  [Header("Main Window")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private TextMeshProUGUI currentStatCategory_Txt;
  [SerializeField]
  private Image gradientCover_Img;
  private StatCategory statCategory;
  private bool sortingDescOrder;
  [Header("Active Player Section")]
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private Image playerPortrait_Img;
  [SerializeField]
  private TextMeshProUGUI playerName_Txt;
  [SerializeField]
  private TextMeshProUGUI playerDescription_Txt;
  [Header("Player List")]
  [SerializeField]
  private RectTransform listScrollContents;
  [SerializeField]
  private TextMeshProUGUI[] categoryHeaders_Txt;
  [SerializeField]
  private CareerStatsLine[] playerStatLines;
  [HideInInspector]
  public int selectedLineIndex;
  private int yearsInList;
  private int[] sortedYears;
  private int[] statOrder;
  private List<int> matchedYears = new List<int>();
  private List<PlayerStats> allStats = new List<PlayerStats>();
  private int selectedCategoryIndex;
  [Header("Controller Support")]
  private bool allowMove;
  private WaitForSecondsRealtime disableMove_WFS;
  private int beginScrollAtIndex;
  private int playerSlotRowHeight;
  private FranchiseWindow sourceWindow;
  private float[] _sortedValues;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.disableMove_WFS = new WaitForSecondsRealtime(0.2f);
    this.beginScrollAtIndex = 5;
    this.playerSlotRowHeight = 83;
    for (int index = 0; index < this.playerStatLines.Length; ++index)
    {
      if (index % 2 == 0)
        this.playerStatLines[index].HideDarkBackground();
      else
        this.playerStatLines[index].ShowDarkBackground();
    }
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(PlayerData player, TeamData team, FranchiseWindow source)
  {
    this.sourceWindow = source;
    PortraitManager.self.ClearTeamPlayerPortraits();
    this.gradientCover_Img.color = team.GetPrimaryColor();
    this.teamLogo_Img.sprite = team.GetSmallLogo();
    this.playerName_Txt.text = player.FullName;
    this.playerDescription_Txt.text = player.PlayerPosition.ToString() + ": #" + player.Number.ToString() + "  /  HT: " + player.GetStandardHeight() + "  /  WT: " + player.Weight.ToString() + "  /  AGE: " + player.Age.ToString();
    this.playerPortrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(player);
    this.allowMove = true;
    this.mainWindow_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    BottomBarManager.instance.SetControllerButtonGuide(7);
    this.allStats.Clear();
    if (!SeasonModeManager.self.IsSeasonOver())
      this.allStats.Add(player.CurrentSeasonStats);
    if (player.CareerStats == null)
      player.CreateCareerStats();
    for (int index = 0; index < player.CareerStats.Count; ++index)
      this.allStats.Add(player.CareerStats[index]);
    if (player.PlayerPosition == Position.QB)
      this.SetStatCategory(StatCategory.Passing);
    else if (player.PlayerPosition == Position.RB)
      this.SetStatCategory(StatCategory.Rushing);
    else if (player.PlayerPosition == Position.TE || player.PlayerPosition == Position.WR)
      this.SetStatCategory(StatCategory.Receiving);
    else
      this.SetStatCategory(StatCategory.Defense);
  }

  public void HideWindow()
  {
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu() => this.HideWindow();

  private void SetStatCategory(StatCategory category)
  {
    this.statCategory = category;
    this.sortingDescOrder = true;
    this.selectedCategoryIndex = -1;
    this.statOrder = PlayerStats.GetStatsForCategory(this.statCategory);
    this.currentStatCategory_Txt.text = category.ToString().ToUpper();
    this.SelectStatCategory(0);
  }

  public void SelectStatCategory(int categoryIndex)
  {
    this.sortingDescOrder = categoryIndex != this.selectedCategoryIndex ? categoryIndex != 0 : !this.sortingDescOrder;
    this.selectedCategoryIndex = categoryIndex;
    this.categoryHeaders_Txt[0].text = "YEAR";
    this.categoryHeaders_Txt[0].gameObject.SetActive(true);
    for (int index = 0; index < this.statOrder.Length; ++index)
    {
      this.categoryHeaders_Txt[index + 1].text = PlayerStats.GetStatAbbreviation(this.statOrder[index]);
      this.categoryHeaders_Txt[index + 1].gameObject.SetActive(true);
    }
    for (int index = this.statOrder.Length + 1; index < this.categoryHeaders_Txt.Length; ++index)
      this.categoryHeaders_Txt[index].gameObject.SetActive(false);
    this.PopulateWindow();
  }

  private void PopulateWindow()
  {
    this.listScrollContents.anchoredPosition = Vector2.zero;
    int index1 = 0;
    this.matchedYears.Clear();
    int firstYear = 99999;
    for (int index2 = 0; index2 < this.allStats.Count; ++index2)
    {
      this.matchedYears.Add(index2);
      this.playerStatLines[index1].SetStatValues(this.allStats[index2], this.statOrder, index2);
      if (this.allStats[index2].StatYear < firstYear)
        firstYear = this.allStats[index2].StatYear;
      this.playerStatLines[index1].SetActiveCategory(this.selectedCategoryIndex);
      ++index1;
      if (index1 == this.playerStatLines.Length)
        break;
    }
    this.yearsInList = this.matchedYears.Count;
    this.SortPlayerList();
    for (int index3 = 0; index3 < this.yearsInList; ++index3)
      this.playerStatLines[index3].SetStatValues(this.allStats[this.sortedYears[index3]], this.statOrder, firstYear);
    for (int index4 = index1; index4 < this.playerStatLines.Length; ++index4)
      this.playerStatLines[index4].ClearLine();
    if (index1 > 0)
    {
      this.playerStatLines[0].SelectLine();
      this.SetActiveLine(0);
    }
    else
      this.DeselectCurrentLine();
  }

  private void SortPlayerList()
  {
    this.sortedYears = new int[this.yearsInList];
    this._sortedValues = new float[this.yearsInList];
    for (int index = 0; index < this.sortedYears.Length; ++index)
      this.sortedYears[index] = this.matchedYears[index];
    for (int index = 0; index < this._sortedValues.Length; ++index)
      this._sortedValues[index] = this.playerStatLines[index].GetValueInTextBox(this.selectedCategoryIndex);
    bool sortingDescOrder = this.sortingDescOrder;
    for (int index1 = 0; index1 < this.sortedYears.Length - 1; ++index1)
    {
      int index2 = index1;
      float sortedValue1 = this._sortedValues[index1];
      for (int index3 = index1 + 1; index3 < this.sortedYears.Length; ++index3)
      {
        if (sortingDescOrder)
        {
          if ((double) sortedValue1 < (double) this._sortedValues[index3])
          {
            index2 = index3;
            sortedValue1 = this._sortedValues[index3];
          }
        }
        else if ((double) sortedValue1 > (double) this._sortedValues[index3])
        {
          index2 = index3;
          sortedValue1 = this._sortedValues[index3];
        }
      }
      int sortedYear = this.sortedYears[index1];
      this.sortedYears[index1] = this.sortedYears[index2];
      this.sortedYears[index2] = sortedYear;
      float sortedValue2 = this._sortedValues[index1];
      this._sortedValues[index1] = this._sortedValues[index2];
      this._sortedValues[index2] = sortedValue2;
    }
  }

  public void SetActiveLine(int lineIndex)
  {
    if (this.selectedLineIndex != -1 && lineIndex != this.selectedLineIndex)
      this.playerStatLines[this.selectedLineIndex].DeselectLine();
    this.selectedLineIndex = lineIndex;
  }

  private void DeselectCurrentLine()
  {
    if (this.selectedLineIndex == -1)
      return;
    this.playerStatLines[this.selectedLineIndex].DeselectLine();
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this.disableMove_WFS;
    this.allowMove = true;
  }

  public void SelectPreviousStatCategory()
  {
    UISoundManager.instance.PlayTabSwipe();
    if (this.statCategory == StatCategory.Kicking)
      this.SetStatCategory(StatCategory.Returns);
    else if (this.statCategory == StatCategory.Returns)
      this.SetStatCategory(StatCategory.Defense);
    else if (this.statCategory == StatCategory.Defense)
      this.SetStatCategory(StatCategory.Receiving);
    else if (this.statCategory == StatCategory.Receiving)
      this.SetStatCategory(StatCategory.Rushing);
    else if (this.statCategory == StatCategory.Rushing)
      this.SetStatCategory(StatCategory.Passing);
    else
      this.SetStatCategory(StatCategory.Kicking);
  }

  public void SelectNextStatCategory()
  {
    UISoundManager.instance.PlayTabSwipe();
    if (this.statCategory == StatCategory.Passing)
      this.SetStatCategory(StatCategory.Rushing);
    else if (this.statCategory == StatCategory.Rushing)
      this.SetStatCategory(StatCategory.Receiving);
    else if (this.statCategory == StatCategory.Receiving)
      this.SetStatCategory(StatCategory.Defense);
    else if (this.statCategory == StatCategory.Defense)
      this.SetStatCategory(StatCategory.Returns);
    else if (this.statCategory == StatCategory.Returns)
      this.SetStatCategory(StatCategory.Kicking);
    else
      this.SetStatCategory(StatCategory.Passing);
  }

  public void SelectNextPlayer()
  {
    if (this.selectedLineIndex >= this.yearsInList - 1)
      return;
    this.playerStatLines[this.selectedLineIndex + 1].SelectLine();
    this.SetActiveLine(this.selectedLineIndex + 1);
    UISoundManager.instance.PlayButtonToggle();
    if (this.selectedLineIndex < this.beginScrollAtIndex)
      return;
    this.listScrollContents.anchoredPosition = new Vector2(0.0f, (float) (this.playerSlotRowHeight * (this.selectedLineIndex - this.beginScrollAtIndex + 1)));
  }

  public void SelectPrevPlayer()
  {
    if (this.selectedLineIndex <= 0)
      return;
    this.playerStatLines[this.selectedLineIndex - 1].SelectLine();
    this.SetActiveLine(this.selectedLineIndex - 1);
    UISoundManager.instance.PlayButtonToggle();
    if (this.selectedLineIndex < this.beginScrollAtIndex - 1)
      return;
    this.listScrollContents.anchoredPosition = new Vector2(0.0f, (float) (this.playerSlotRowHeight * (this.selectedLineIndex - this.beginScrollAtIndex + 1)));
  }

  private void SelectNextStat()
  {
    if (this.selectedCategoryIndex >= this.statOrder.Length)
      return;
    UISoundManager.instance.PlayButtonToggle();
    this.SelectStatCategory(this.selectedCategoryIndex + 1);
  }

  private void SelectPrevStat()
  {
    if (this.selectedCategoryIndex <= 0)
      return;
    UISoundManager.instance.PlayButtonToggle();
    this.SelectStatCategory(this.selectedCategoryIndex - 1);
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible())
      return;
    if (UserManager.instance.Action3WasPressed(Player.One))
      this.SelectStatCategory(this.selectedCategoryIndex);
    if (UserManager.instance.LeftBumperWasPressed(Player.One))
      this.SelectPreviousStatCategory();
    else if (UserManager.instance.RightBumperWasPressed(Player.One))
      this.SelectNextStatCategory();
    float num1 = UserManager.instance.LeftStickY(Player.One);
    if (this.allowMove)
    {
      if ((double) num1 > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectPrevPlayer();
      }
      else if ((double) num1 < -0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SelectNextPlayer();
      }
    }
    float num2 = UserManager.instance.LeftStickX(Player.One);
    if (!this.allowMove)
      return;
    if ((double) num2 > 0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectNextStat();
    }
    else
    {
      if ((double) num2 >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.SelectPrevStat();
    }
  }
}
