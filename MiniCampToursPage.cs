// Decompiled with JetBrains decompiler
// Type: MiniCampToursPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using TB12;
using TMPro;
using UnityEngine;

public class MiniCampToursPage : TabletPage
{
  [SerializeField]
  private MiniCampTourStore _miniCampTourStore;
  [SerializeField]
  private TouchUI2DButton _precisionPassingButton;
  [SerializeField]
  private TouchUI2DButton _pocketPresenceButton;
  [SerializeField]
  private TouchUI2DButton _readOptionButton;
  [SerializeField]
  private TouchUI2DButton _agilityButton;
  [SerializeField]
  private TouchUI2DButton _backButton;
  [SerializeField]
  private TMP_Text _precisionPassingTotalStarText;
  [SerializeField]
  private TMP_Text _pocketPresenceTotalStarText;
  [SerializeField]
  private TMP_Text _readOptionTotalStarText;
  [SerializeField]
  private TMP_Text _agilityTotalStarText;
  private Save_MiniCamp _miniCampData;

  private void Awake()
  {
    this._miniCampData = PersistentSingleton<SaveManager>.Instance.miniCamp;
    if (!this._miniCampData.LoadedTemplate)
      this.LoadIntoSaveFile();
    this._pageType = TabletPage.Pages.MiniCamp;
    this._precisionPassingButton.onClick += new System.Action(this.HandlePrecisionPassingButton);
    this._pocketPresenceButton.onClick += new System.Action(this.HandlePocketPresenceButton);
    this._readOptionButton.onClick += new System.Action(this.HandleReadOptionButton);
    this._agilityButton.onClick += new System.Action(this.HandleAgilityButton);
    this._backButton.onClick += new System.Action(this.HandleBackButton);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._precisionPassingButton != (UnityEngine.Object) null)
      this._precisionPassingButton.onClick -= new System.Action(this.HandlePrecisionPassingButton);
    if ((UnityEngine.Object) this._pocketPresenceButton != (UnityEngine.Object) null)
      this._pocketPresenceButton.onClick -= new System.Action(this.HandlePocketPresenceButton);
    if ((UnityEngine.Object) this._readOptionButton != (UnityEngine.Object) null)
      this._readOptionButton.onClick -= new System.Action(this.HandleReadOptionButton);
    if ((UnityEngine.Object) this._agilityButton != (UnityEngine.Object) null)
      this._agilityButton.onClick -= new System.Action(this.HandleAgilityButton);
    if (!((UnityEngine.Object) this._backButton != (UnityEngine.Object) null))
      return;
    this._backButton.onClick -= new System.Action(this.HandleBackButton);
  }

  private void OnEnable()
  {
    PersistentSingleton<BallsContainerManager>.Instance.IsNetworked = false;
    if (!this._miniCampData.LoadedTemplate)
      this.LoadIntoSaveFile();
    this.UpdateAllTotalStarText();
  }

  public void LoadIntoSaveFile()
  {
    this._miniCampData.MiniCamps = this._miniCampTourStore.miniCampsTemplate;
    this._miniCampData.LoadedTemplate = true;
  }

  private void UpdateAllTotalStarText()
  {
    if (!this._miniCampData.LoadedTemplate)
      return;
    this._precisionPassingTotalStarText.text = this.GetTotalStarsText(EMiniCampTourType.kPrecisionPassing);
    this._pocketPresenceTotalStarText.text = this.GetTotalStarsText(EMiniCampTourType.kQBPocketPresence);
    this._readOptionTotalStarText.text = this.GetTotalStarsText(EMiniCampTourType.kReadOption);
    this._agilityTotalStarText.text = this.GetTotalStarsText(EMiniCampTourType.kAgility);
  }

  private string GetTotalStarsText(EMiniCampTourType a_miniCampTourType)
  {
    MiniCampData miniCampData = this._miniCampData.GetMiniCampData(a_miniCampTourType);
    return string.Format("{0}/{1}", (object) ((IEnumerable<MiniCampEntry>) miniCampData.MiniCampEntries).Sum<MiniCampEntry>((Func<MiniCampEntry, int>) (x => x.EarnedStars)), (object) ((IEnumerable<MiniCampEntry>) miniCampData.MiniCampEntries).Sum<MiniCampEntry>((Func<MiniCampEntry, int>) (x => x.TotalStars)));
  }

  private void HandlePrecisionPassingButton()
  {
    this._miniCampData.Select(EMiniCampTourType.kPrecisionPassing);
    this.GoToNextPage();
  }

  private void HandlePocketPresenceButton()
  {
    this._miniCampData.Select(EMiniCampTourType.kQBPocketPresence);
    this.GoToNextPage();
  }

  private void HandleReadOptionButton()
  {
    this._miniCampData.Select(EMiniCampTourType.kReadOption);
    this.GoToNextPage();
  }

  private void HandleAgilityButton()
  {
    this._miniCampData.Select(EMiniCampTourType.kAgility);
    this.GoToNextPage();
  }

  private void GoToNextPage() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.MiniCampProgression);

  private void HandleBackButton() => (this.MainPage as MainMenuPage).OpenPage(TabletPage.Pages.Main);
}
