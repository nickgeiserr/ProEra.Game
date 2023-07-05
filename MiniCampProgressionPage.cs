// Decompiled with JetBrains decompiler
// Type: MiniCampProgressionPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class MiniCampProgressionPage : TabletPage
{
  [SerializeField]
  private TouchUI2DButton _backButton;
  [SerializeField]
  private TouchUI2DButton _prevButton;
  [SerializeField]
  private TouchUI2DButton _nextButton;
  [SerializeField]
  private LocalizeStringEvent _miniCampTourNameText;
  [SerializeField]
  private Image _logoImage;
  [SerializeField]
  private Image _lockImage;
  [SerializeField]
  private LocalizeStringEvent _personalBestText;
  [SerializeField]
  private Image _star1Image;
  [SerializeField]
  private Image _star2Image;
  [SerializeField]
  private Image _star3Image;
  [SerializeField]
  private MiniCampSelectionUIController[] _selectionUIControllers;
  private int m_currentSelectedLevel = 1;
  private Dictionary<MiniCampData, string> miniCampNameLocalizationMap = new Dictionary<MiniCampData, string>();

  private Save_MiniCamp _miniCampData => PersistentSingleton<SaveManager>.Instance.miniCamp;

  private void Awake()
  {
    this._pageType = TabletPage.Pages.MiniCampProgression;
    this._backButton.onClick += new System.Action(this.HandleBackButton);
    this._prevButton.onClick += new System.Action(this.SelectPreviousEntry);
    this._nextButton.onClick += new System.Action(this.SelectNextEntry);
    this.LoadEntries();
  }

  private void OnEnable() => this.LoadEntries();

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._backButton != (UnityEngine.Object) null)
      this._backButton.onClick -= new System.Action(this.HandleBackButton);
    if ((UnityEngine.Object) this._prevButton != (UnityEngine.Object) null)
      this._prevButton.onClick -= new System.Action(this.SelectPreviousEntry);
    if (!((UnityEngine.Object) this._nextButton != (UnityEngine.Object) null))
      return;
    this._nextButton.onClick -= new System.Action(this.SelectNextEntry);
  }

  private void LoadEntries()
  {
    int level = this.m_currentSelectedLevel - 2;
    int num1 = 0;
    foreach (MiniCampSelectionUIController selectionUiController in this._selectionUIControllers)
    {
      if (level >= 1 && level <= this._miniCampData.MiniCamps.Length + num1)
      {
        int num2 = level;
        selectionUiController.Inject(this._miniCampData.SelectedMiniCamp.GetEntryByLevel(level).GetLogo(), this._miniCampData.SelectedMiniCamp.GetEntryByLevel(level).EarnedStars, this._miniCampData.SelectedMiniCamp.CurrentLevel < this._miniCampData.SelectedMiniCamp.GetEntryByLevel(level).Level, this._miniCampData.SelectedMiniCamp.GameplayMode, this._miniCampData.SelectedMiniCamp.GetEntryByLevel(level).TeamName);
        selectionUiController.ButtonEnabled = this.m_currentSelectedLevel == num2;
      }
      else
        selectionUiController.SetSelectionState(false);
      ++level;
    }
    this.LoadEntryIntoSummary(this._miniCampData.SelectedMiniCamp.GetEntryByLevel(this.m_currentSelectedLevel));
    if (this._miniCampData.SelectedMiniCamp.MiniCampEntries.Length == 0)
      return;
    this._miniCampTourNameText.StringReference.TableEntryReference = (TableEntryReference) this._miniCampData.SelectedMiniCamp.MiniCampTourName;
    this._personalBestText.StringReference.Arguments = (IList<object>) new string[1]
    {
      this._miniCampData.SelectedEntry.PersonalBest.ToString()
    };
  }

  private void LoadEntryIntoSummary(MiniCampEntry entry)
  {
    this._logoImage.sprite = entry.GetLogo();
    this._star1Image.enabled = entry.EarnedStars >= 1;
    this._star2Image.enabled = entry.EarnedStars >= 2;
    this._star3Image.enabled = entry.EarnedStars >= 3;
    this._lockImage.enabled = this._miniCampData.SelectedMiniCamp.CurrentLevel < entry.Level;
    this._miniCampData.SelectedEntry = entry;
  }

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MiniCamp);

  public void SelectPreviousEntry()
  {
    int num = 0;
    --this.m_currentSelectedLevel;
    this.m_currentSelectedLevel = Mathf.Clamp(this.m_currentSelectedLevel, 1, this._miniCampData.MiniCamps.Length + num);
    this.LoadEntries();
  }

  public void SelectNextEntry()
  {
    int num = 0;
    ++this.m_currentSelectedLevel;
    this.m_currentSelectedLevel = Mathf.Clamp(this.m_currentSelectedLevel, 1, this._miniCampData.MiniCamps.Length + num);
    this.LoadEntries();
  }
}
