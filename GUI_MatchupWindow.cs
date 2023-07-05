// Decompiled with JetBrains decompiler
// Type: GUI_MatchupWindow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_MatchupWindow : MonoBehaviour, IMatchupWindow
{
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private Image awayLogo_Img;
  [SerializeField]
  private Image awayBackground_Img;
  [SerializeField]
  private TextMeshProUGUI awayRecord_Txt;
  [SerializeField]
  private TextMeshProUGUI awayName_Txt;
  [SerializeField]
  private Image homeLogo_Img;
  [SerializeField]
  private Image homeBackground_Img;
  [SerializeField]
  private TextMeshProUGUI homeRecord_Txt;
  [SerializeField]
  private TextMeshProUGUI homeName_Txt;
  [SerializeField]
  private TextMeshProUGUI stadium_Txt;
  [SerializeField]
  private TextMeshProUGUI city_Txt;
  [SerializeField]
  private TextMeshProUGUI week_Txt;

  private void Awake() => ProEra.Game.Sources.UI.MatchupWindow = (IMatchupWindow) this;

  public void Init()
  {
    this.mainWindow_CG.blocksRaycasts = false;
    if (PersistentData.gameType == GameType.SeasonMode)
    {
      this.homeRecord_Txt.text = PersistentData.homeRecord;
      this.awayRecord_Txt.text = PersistentData.awayRecord;
      if (PersistentData.seasonWeek < 17)
        this.week_Txt.text = string.Format("WEEK {0}", (object) PersistentData.seasonWeek);
      else if (PersistentData.seasonWeek == 17)
        this.week_Txt.text = "PLAYOFFS";
    }
    else
    {
      this.homeRecord_Txt.text = "0-0";
      this.awayRecord_Txt.text = "0-0";
      this.week_Txt.text = "EXHIBITION";
    }
    Debug.LogWarning((object) "CD: Debug stadium and city names here");
    this.stadium_Txt.text = "DEBUG STADIUM NAME";
    this.city_Txt.text = "DEBUG CITY NAME";
    this.awayName_Txt.text = PersistentData.GetAwayTeamAbbreviation();
    this.awayLogo_Img.sprite = PersistentData.GetAwayLargeLogo();
    this.awayBackground_Img.color = PersistentData.GetAwayBackgroundColor();
    this.homeName_Txt.text = PersistentData.GetHomeTeamAbbreviation();
    this.homeLogo_Img.sprite = PersistentData.GetHomeLargeLogo();
    this.homeBackground_Img.color = PersistentData.GetHomeBackgroundColor();
    this.ShowWindow();
  }

  public void ShowWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    this.StartCoroutine(this.StartGameCoroutine());
  }

  private IEnumerator StartGameCoroutine()
  {
    yield return (object) new WaitForSeconds(5f);
    this.HideWindow();
    MatchupController.StartGame();
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void SetActive(bool value) => this.gameObject.SetActive(value);
}
