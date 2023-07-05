// Decompiled with JetBrains decompiler
// Type: OptionsMenuManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using FootballVR;
using Framework;
using ProEra.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour, IOptionsMenu
{
  [Header("Main")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private RectTransform optionsContainer_Trans;
  [SerializeField]
  private GameObject optionsContainer_GO;
  [SerializeField]
  private GameObject activeIndicator_GO;
  [SerializeField]
  private RectTransform activeIndicator_Trans;
  [SerializeField]
  private TextMeshProUGUI subTitle_Txt;
  [SerializeField]
  private RectTransform audioBtn_Trans;
  [SerializeField]
  private RectTransform videoBtn_Trans;
  [SerializeField]
  private RectTransform gameBtn_Trans;
  [SerializeField]
  private RectTransform penaltyBtn_Trans;
  [SerializeField]
  private RectTransform dataBtn_Trans;
  [SerializeField]
  private UnityEngine.UI.Button audio_Btn;
  [SerializeField]
  private UnityEngine.UI.Button video_Btn;
  [SerializeField]
  private UnityEngine.UI.Button game_Btn;
  [SerializeField]
  private UnityEngine.UI.Button penalty_Btn;
  [SerializeField]
  private UnityEngine.UI.Button data_Btn;
  private float activeIndicatorAnimationSpeed = 0.15f;
  private int optionSectionIndex;
  private int selectedIndex;
  private bool inTitleScreen;
  [Header("Audio Options")]
  [SerializeField]
  private Slider soundFX_Slider;
  [SerializeField]
  private Slider music_Slider;
  [SerializeField]
  private Slider announcer_Slider;
  [SerializeField]
  private TextMeshProUGUI soundFX_Txt;
  [SerializeField]
  private TextMeshProUGUI music_Txt;
  [SerializeField]
  private TextMeshProUGUI announcer_Txt;
  [SerializeField]
  private Animator[] audioOption_Anis;
  [Header("Video Options")]
  [SerializeField]
  private Slider cameraZoom_Slider;
  [SerializeField]
  private Slider cameraHeight_Slider;
  [SerializeField]
  private TextMeshProUGUI cameraZoom_Txt;
  [SerializeField]
  private TextMeshProUGUI cameraHeight_Txt;
  [SerializeField]
  private TextMeshProUGUI sidelinePlayers_Txt;
  [SerializeField]
  private TextMeshProUGUI fullScreen_Txt;
  [SerializeField]
  private TextMeshProUGUI runInBackground_Txt;
  [SerializeField]
  private TextMeshProUGUI resolution_Txt;
  [SerializeField]
  private TextMeshProUGUI graphicsQuality_Txt;
  [SerializeField]
  private Animator[] videoOption_Anis;
  private List<Resolution> availableResolutions;
  private int resolutionIndex;
  [Header("Game Options")]
  [SerializeField]
  private Slider acceleratedClock_Slider;
  [SerializeField]
  private Slider gameSpeed_Slider;
  [SerializeField]
  private Slider clockSpeed_Slider;
  [SerializeField]
  private Slider injuryFrequency_Slider;
  [SerializeField]
  private Slider p1Sensitivity_Slider;
  [SerializeField]
  private Slider p2Sensitivity_Slider;
  [SerializeField]
  private TextMeshProUGUI acceleratedClock_Txt;
  [SerializeField]
  private TextMeshProUGUI gameSpeed_Txt;
  [SerializeField]
  private TextMeshProUGUI clockSpeed_Txt;
  [SerializeField]
  private TextMeshProUGUI injuryFrequency_Txt;
  [SerializeField]
  private TextMeshProUGUI twoMinWarning_Txt;
  [SerializeField]
  private TextMeshProUGUI ignoreControllers_Txt;
  [SerializeField]
  private TextMeshProUGUI p1PassSystem_Txt;
  [SerializeField]
  private TextMeshProUGUI p1Sensitivity_Txt;
  [SerializeField]
  private TextMeshProUGUI p2PassSystem_Txt;
  [SerializeField]
  private TextMeshProUGUI p2Sensitivity_Txt;
  [SerializeField]
  private TextMeshProUGUI controllerIcons_Txt;
  [SerializeField]
  private Animator[] gameOption_Anis;
  [SerializeField]
  private Scrollbar gameOptions_Scrollbar;
  [SerializeField]
  private TextMeshProUGUI additionalInfoGame_Txt;
  [Header("Penalty Options")]
  [SerializeField]
  private Slider penaltyFrequency_Slider;
  [SerializeField]
  private TextMeshProUGUI penaltyFrequency_Txt;
  [SerializeField]
  private TextMeshProUGUI delayOfGame_Txt;
  [SerializeField]
  private TextMeshProUGUI offHolding_Txt;
  [SerializeField]
  private TextMeshProUGUI offPassInt_Txt;
  [SerializeField]
  private TextMeshProUGUI facemask_Txt;
  [SerializeField]
  private TextMeshProUGUI encroachment_Txt;
  [SerializeField]
  private TextMeshProUGUI falseStart_Txt;
  [SerializeField]
  private TextMeshProUGUI offsides_Txt;
  [SerializeField]
  private TextMeshProUGUI defPassInt_Txt;
  [SerializeField]
  private TextMeshProUGUI kickingOutOfBounds_Txt;
  [SerializeField]
  private TextMeshProUGUI intentionalGrounding_Txt;
  [SerializeField]
  private Animator[] penaltyOption_Anis;
  [SerializeField]
  private Scrollbar penaltyOptions_Scrollbar;
  [SerializeField]
  private TextMeshProUGUI additionalInfoPenalty_Txt;
  [Header("Data Options")]
  [SerializeField]
  private TextMeshProUGUI useBaseTeams_Txt;
  [SerializeField]
  private TextMeshProUGUI useMods_Txt;
  [SerializeField]
  private TextMeshProUGUI logGames_Txt;
  [SerializeField]
  private TextMeshProUGUI exportGameStats_Txt;
  [SerializeField]
  private Animator[] dataOption_Anis;
  [SerializeField]
  private CanvasGroup confirmDeleteData_CG;
  [SerializeField]
  private UnityEngine.UI.Button yes_Btn;
  [Header("Scene Specific")]
  [SerializeField]
  private GameObject[] disableInGameScene;
  private bool allowMove;
  private WaitForSecondsRealtime disableMove_WFS;
  private float sliderChangeAmount = 0.05f;
  private int maxVisibleItemsInWindow = 9;
  private int beginScrollItemIndex = 4;

  private void Awake() => ProEra.Game.Sources.UI.OptionsMenu = (IOptionsMenu) this;

  private void Start() => this.disableMove_WFS = new WaitForSecondsRealtime(0.2f);

  public void Init()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.allowMove = true;
    this.inTitleScreen = (Object) ControllerManagerGame.self == (Object) null;
    OptionsMenuHelper.optionsMenu = this;
    if (!this.inTitleScreen)
    {
      for (int index = 0; index < this.disableInGameScene.Length; ++index)
        this.disableInGameScene[index].SetActive(false);
    }
    this.LoadSettingsFromSavedGameData();
    this.SetResolutionOptions();
  }

  private void LoadSettingsFromSavedGameData()
  {
    Application.runInBackground = PersistentSingleton<SaveManager>.Instance.gameSettings.runInBackground;
    Screen.SetResolution(PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionWidth, PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionHeight, PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode);
    if (!this.inTitleScreen)
    {
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.ToggleSidelinePlayers(PersistentSingleton<SaveManager>.Instance.gameSettings.ShowSidelinePlayers);
      this.SetGraphicsQuality();
      PersistentData.globalTimeScale = (float) GameSettings.TimeScale;
    }
    else
      UISoundManager.instance.SetVolume(PersistentSingleton<SaveManager>.Instance.gameSettings.GameSFXVolume);
  }

  private void Update() => this.ManageControllerSupport();

  public void ShowWindow()
  {
    this.mainWindow_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f).setIgnoreTimeScale(true);
    BottomBarManager.instance.ShowWindow();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    if (!this.inTitleScreen)
      return;
    this.HideConfirmDeleteWindow();
  }

  public void HideWindow()
  {
    this.mainWindow_CG.blocksRaycasts = false;
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.3f).setIgnoreTimeScale(true);
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    AppEvents.SaveGameSettings.Trigger();
    if (this.inTitleScreen)
    {
      if (this.IsConfirmDeleteDataVisible())
      {
        this.HideConfirmDeleteWindow();
      }
      else
      {
        this.HideWindow();
        SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.ShowWindow();
        SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.mainMenu.SelectSettings();
      }
    }
    else
    {
      this.HideWindow();
      ProEra.Game.Sources.UI.PauseWindow.ShowWindow();
    }
  }

  private void ResetTabTextColors()
  {
    this.audio_Btn.interactable = true;
    this.video_Btn.interactable = true;
    this.game_Btn.interactable = true;
    this.penalty_Btn.interactable = true;
    this.data_Btn.interactable = true;
  }

  public void SetSelectedIndexOnMouseover(int i) => this.selectedIndex = i;

  public void ShowAdditionalInformation()
  {
    if (this.optionSectionIndex == 2)
    {
      this.ShowAdditionalInformation_Game();
    }
    else
    {
      if (this.optionSectionIndex != 3)
        return;
      this.ShowAdditionalInformation_Penalty();
    }
  }

  public void ShowAudioOptions()
  {
    this.subTitle_Txt.text = "AUDIO";
    UISoundManager.instance.PlayTabSwipe();
    this.optionSectionIndex = 0;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.audioBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.audioBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.optionsContainer_GO, (float) ((double) this.optionsContainer_Trans.rect.width * (double) this.optionSectionIndex * -1.0), this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    this.ResetTabTextColors();
    this.audio_Btn.interactable = false;
    this.SetAudioDisplayValues();
    if (!this.IsUsingController())
      return;
    this.HighlightAudioOption(0);
  }

  private void SetAudioDisplayValues()
  {
    this.soundFX_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.GameSFXVolume;
    this.soundFX_Txt.text = (this.soundFX_Slider.value * 100f).ToString("n0");
    if (this.inTitleScreen)
      UISoundManager.instance.SetVolume(this.soundFX_Slider.value);
    this.announcer_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.AnnouncerVolume;
    this.announcer_Txt.text = (this.announcer_Slider.value * 100f).ToString("n0");
    this.music_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.GameOptionsMusicVolume;
    this.music_Txt.text = (this.music_Slider.value * 100f).ToString("n0");
  }

  private void HighlightAudioOption(int index)
  {
    this.selectedIndex = index;
    this.HideAllAudioOptions();
    this.audioOption_Anis[this.selectedIndex].SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void HideAllAudioOptions()
  {
    for (int index = 0; index < this.audioOption_Anis.Length; ++index)
      this.audioOption_Anis[index].SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void SelectNextAudioOption()
  {
    int index1 = this.selectedIndex + 1;
    if (index1 >= this.audioOption_Anis.Length)
      return;
    if (!this.audioOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 + 1; index2 < this.audioOption_Anis.Length; ++index2)
      {
        if (this.audioOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 >= this.audioOption_Anis.Length)
      return;
    this.HighlightAudioOption(index1);
  }

  private void SelectPreviousAudioOption()
  {
    int index1 = this.selectedIndex - 1;
    if (index1 < 0)
      return;
    if (!this.audioOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (this.audioOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 < 0)
      return;
    this.HighlightAudioOption(index1);
  }

  public void ChangeSoundLevel()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.GameSFXVolume = this.soundFX_Slider.value;
    this.SetAudioDisplayValues();
  }

  public void ChangeMusicLevel()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.GameOptionsMusicVolume = this.music_Slider.value;
    this.SetAudioDisplayValues();
  }

  public void ChangeAnnouncerLevel()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.AnnouncerVolume = this.announcer_Slider.value;
    this.SetAudioDisplayValues();
  }

  public void ResetAudioSettings()
  {
    UISoundManager.instance.PlayButtonClick();
    PersistentSingleton<SaveManager>.Instance.gameSettings.SetDefaultAudioSettings();
    this.LoadSettingsFromSavedGameData();
    this.SetAudioDisplayValues();
  }

  public void ShowVideoOptions()
  {
    this.subTitle_Txt.text = "VIDEO";
    UISoundManager.instance.PlayTabSwipe();
    this.optionSectionIndex = 1;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.videoBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.videoBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.optionsContainer_GO, (float) ((double) this.optionsContainer_Trans.rect.width * (double) this.optionSectionIndex * -1.0), this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    this.ResetTabTextColors();
    this.video_Btn.interactable = false;
    this.SetVideoDisplayValues();
    if (!this.IsUsingController())
      return;
    this.HighlightVideoOption(0);
  }

  private void SetVideoDisplayValues()
  {
    this.cameraZoom_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.CameraDepthZoom;
    this.cameraZoom_Txt.text = (this.cameraZoom_Slider.value * 100f).ToString("n0");
    this.cameraHeight_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.CameraHeightZoom;
    this.cameraHeight_Txt.text = (this.cameraHeight_Slider.value * 100f).ToString("n0");
    this.sidelinePlayers_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ShowSidelinePlayers ? "ON" : "OFF";
    this.fullScreen_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode ? "ON" : "OFF";
    this.runInBackground_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.runInBackground ? "ON" : "OFF";
    this.resolution_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionWidth.ToString() + " x " + PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionHeight.ToString();
    this.graphicsQuality_Txt.text = QualitySettings.names[PersistentSingleton<SaveManager>.Instance.gameSettings.GraphicsQuality];
  }

  private void HighlightVideoOption(int index)
  {
    this.selectedIndex = index;
    this.HideAllVideoOptions();
    this.videoOption_Anis[this.selectedIndex].SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void HideAllVideoOptions()
  {
    for (int index = 0; index < this.videoOption_Anis.Length; ++index)
      this.videoOption_Anis[index].SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void SelectNextVideoOption()
  {
    int index1 = this.selectedIndex + 1;
    if (index1 >= this.videoOption_Anis.Length)
      return;
    if (!this.videoOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 + 1; index2 < this.videoOption_Anis.Length; ++index2)
      {
        if (this.videoOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 >= this.videoOption_Anis.Length)
      return;
    this.HighlightVideoOption(index1);
  }

  private void SelectPreviousVideoOption()
  {
    int index1 = this.selectedIndex - 1;
    if (index1 < 0)
      return;
    if (!this.videoOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (this.videoOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 < 0)
      return;
    this.HighlightVideoOption(index1);
  }

  public void ResetVideoSettings()
  {
    UISoundManager.instance.PlayButtonClick();
    PersistentSingleton<SaveManager>.Instance.gameSettings.SetDefaultVideoSettings();
    this.LoadSettingsFromSavedGameData();
    this.SetVideoDisplayValues();
  }

  public void ChangeCameraZoom()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.CameraDepthZoom = this.cameraZoom_Slider.value;
    this.SetVideoDisplayValues();
  }

  public void ChangeCameraHeight()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.CameraHeightZoom = this.cameraHeight_Slider.value;
    this.SetVideoDisplayValues();
  }

  public void ToggleSidelinePlayers()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ShowSidelinePlayers = !PersistentSingleton<SaveManager>.Instance.gameSettings.ShowSidelinePlayers;
    if (!this.inTitleScreen)
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.ToggleSidelinePlayers(PersistentSingleton<SaveManager>.Instance.gameSettings.ShowSidelinePlayers);
    this.SetVideoDisplayValues();
  }

  public void ToggleFullScreen()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode = !PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode;
    Screen.SetResolution(PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionWidth, PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionHeight, PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode);
    this.SetVideoDisplayValues();
  }

  public void ToggleRunInBackground()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.runInBackground = !PersistentSingleton<SaveManager>.Instance.gameSettings.runInBackground;
    Application.runInBackground = PersistentSingleton<SaveManager>.Instance.gameSettings.runInBackground;
    this.SetVideoDisplayValues();
  }

  private void SetResolutionOptions()
  {
    this.availableResolutions = new List<Resolution>();
    int width = Screen.currentResolution.width;
    int height = Screen.currentResolution.height;
    Resolution[] resolutions = Screen.resolutions;
    for (int index = 0; index < resolutions.Length; ++index)
      this.availableResolutions.Add(resolutions[index]);
    for (int index = 0; index < resolutions.Length; ++index)
    {
      if (resolutions[index].width == width && resolutions[index].height == height)
        this.resolutionIndex = index;
    }
  }

  public void SetNextResolution()
  {
    if (this.resolutionIndex >= this.availableResolutions.Count - 1)
      return;
    ++this.resolutionIndex;
    int width = this.availableResolutions[this.resolutionIndex].width;
    int height = this.availableResolutions[this.resolutionIndex].height;
    PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionWidth = width;
    PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionHeight = height;
    Screen.SetResolution(width, height, PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode);
    this.SetVideoDisplayValues();
  }

  public void SetPreviousResolution()
  {
    if (this.resolutionIndex <= 0)
      return;
    --this.resolutionIndex;
    int width = this.availableResolutions[this.resolutionIndex].width;
    int height = this.availableResolutions[this.resolutionIndex].height;
    PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionWidth = width;
    PersistentSingleton<SaveManager>.Instance.gameSettings.ResolutionHeight = height;
    Screen.SetResolution(width, height, PersistentSingleton<SaveManager>.Instance.gameSettings.IsFullscreenMode);
    this.SetVideoDisplayValues();
  }

  public void IncreaseGraphicsQuality()
  {
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.GraphicsQuality >= QualitySettings.names.Length - 1)
      return;
    ++PersistentSingleton<SaveManager>.Instance.gameSettings.GraphicsQuality;
    this.SetGraphicsQuality();
    this.SetVideoDisplayValues();
  }

  public void DecreaseGraphicsQuality()
  {
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.GraphicsQuality <= 0)
      return;
    --PersistentSingleton<SaveManager>.Instance.gameSettings.GraphicsQuality;
    this.SetGraphicsQuality();
    this.SetVideoDisplayValues();
  }

  private void SetGraphicsQuality() => QualitySettings.SetQualityLevel(PersistentSingleton<SaveManager>.Instance.gameSettings.GraphicsQuality, true);

  public void ShowGameOptions()
  {
    this.subTitle_Txt.text = "GAME";
    UISoundManager.instance.PlayTabSwipe();
    this.optionSectionIndex = 2;
    this.additionalInfoGame_Txt.text = "";
    LeanTween.moveLocalX(this.activeIndicator_GO, this.gameBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.gameBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.optionsContainer_GO, (float) ((double) this.optionsContainer_Trans.rect.width * (double) this.optionSectionIndex * -1.0), this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    this.ResetTabTextColors();
    this.game_Btn.interactable = false;
    this.gameOptions_Scrollbar.value = 1f;
    this.SetGameDisplayValues();
    if (!this.IsUsingController())
      return;
    this.HighlightGameOption(0);
  }

  private void SetGameDisplayValues()
  {
    this.acceleratedClock_Slider.value = (float) PersistentSingleton<SaveManager>.Instance.gameSettings.AcceleratedClockValue;
    this.acceleratedClock_Txt.text = this.acceleratedClock_Slider.value.ToString("n0");
    this.gameSpeed_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.GlobalTimeScale;
    this.gameSpeed_Txt.text = this.gameSpeed_Slider.value.ToString("n2");
    this.clockSpeed_Slider.value = (float) PersistentSingleton<SaveManager>.Instance.gameSettings.ClockSpeed;
    this.clockSpeed_Txt.text = this.clockSpeed_Slider.value.ToString("n0");
    this.injuryFrequency_Slider.value = (float) PersistentSingleton<SaveManager>.Instance.gameSettings.InjuryFrequency;
    this.injuryFrequency_Txt.text = this.injuryFrequency_Slider.value.ToString("n0");
    this.twoMinWarning_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.TwoMinuteWarningEnabled ? "ON" : "OFF";
    this.ignoreControllers_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ignoreControllers ? "ON" : "OFF";
    this.p1PassSystem_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP1 ? "ICON" : "AIMED";
    OptionsState.SensitivityP1.Value = PersistentSingleton<SaveManager>.Instance.gameSettings.SensitivitySliderP1;
    this.p1Sensitivity_Slider.value = (float) OptionsState.SensitivityP1;
    this.p1Sensitivity_Txt.text = (this.p1Sensitivity_Slider.value * 100f).ToString("n0");
    this.p2PassSystem_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP2 ? "ICON" : "AIMED";
    OptionsState.SensitivityP2.Value = PersistentSingleton<SaveManager>.Instance.gameSettings.SensitivitySliderP2;
    this.p2Sensitivity_Slider.value = (float) OptionsState.SensitivityP2;
    this.p2Sensitivity_Txt.text = (this.p2Sensitivity_Slider.value * 100f).ToString("n0");
    this.controllerIcons_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ControllerIconStyle == 2 ? "PLAYSTATION" : "XBOX";
  }

  private void HighlightGameOption(int index)
  {
    this.selectedIndex = index;
    this.HideAllGameOptions();
    this.gameOption_Anis[this.selectedIndex].SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void HideAllGameOptions()
  {
    for (int index = 0; index < this.gameOption_Anis.Length; ++index)
      this.gameOption_Anis[index].SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void ShowAdditionalInformation_Game()
  {
    if (this.selectedIndex == 0)
      this.additionalInfoGame_Txt.text = "Controls clock runoff after each play to simulate normal game speed. The play clock will run quickly until it reaches the value set here.";
    else if (this.selectedIndex == 1)
      this.additionalInfoGame_Txt.text = "Controls the global speed of all player movement.";
    else if (this.selectedIndex == 2)
      this.additionalInfoGame_Txt.text = "Controls the speed of the game and play clocks.";
    else if (this.selectedIndex == 3)
      this.additionalInfoGame_Txt.text = "Controls how often injuries occur. Higher values indicate a greater chance for injuries.";
    else if (this.selectedIndex == 5)
      this.additionalInfoGame_Txt.text = "Ignores all controller input. Turn this option on if you wish to play with mouse and keyboard.";
    else if (this.selectedIndex == 6)
      this.additionalInfoGame_Txt.text = "Toggles between tradiation icon/button passing and the aimed passing system.";
    else if (this.selectedIndex == 7)
      this.additionalInfoGame_Txt.text = "Controls the speed of the crosshairs when using aimed passing with a controller.";
    else if (this.selectedIndex == 8)
      this.additionalInfoGame_Txt.text = "Toggles between tradiation icon/button passing and the aimed passing system.";
    else if (this.selectedIndex == 9)
      this.additionalInfoGame_Txt.text = "Controls the speed of the crosshairs when using aimed passing with a controller.";
    else if (this.selectedIndex == 10)
      this.additionalInfoGame_Txt.text = "Toggles between different controller icon displays.";
    else
      this.additionalInfoGame_Txt.text = "";
  }

  private void SelectNextGameOption()
  {
    int index1 = this.selectedIndex + 1;
    if (index1 >= this.gameOption_Anis.Length)
      return;
    if (!this.gameOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 + 1; index2 < this.gameOption_Anis.Length; ++index2)
      {
        if (this.gameOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 >= this.gameOption_Anis.Length)
      return;
    if (index1 >= this.beginScrollItemIndex)
      this.gameOptions_Scrollbar.value = (float) (1.0 - 100.0 / (double) (this.gameOption_Anis.Length - this.maxVisibleItemsInWindow) * 0.0099999997764825821 * (double) (index1 - this.beginScrollItemIndex));
    this.HighlightGameOption(index1);
  }

  private void SelectPreviousGameOption()
  {
    int index1 = this.selectedIndex - 1;
    if (index1 < 0)
      return;
    if (!this.gameOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (this.gameOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 < 0)
      return;
    if (index1 >= this.beginScrollItemIndex)
      this.gameOptions_Scrollbar.value = (float) (1.0 - 100.0 / (double) (this.gameOption_Anis.Length - this.maxVisibleItemsInWindow) * 0.0099999997764825821 * (double) (index1 - this.beginScrollItemIndex));
    this.HighlightGameOption(index1);
  }

  public void ResetGameSettings()
  {
    UISoundManager.instance.PlayButtonClick();
    PersistentSingleton<SaveManager>.Instance.gameSettings.SetDefaultGameSettings();
    ControllerButtonSwapper.instance.ChangeToStyle(PersistentSingleton<SaveManager>.Instance.gameSettings.ControllerIconStyle);
    this.LoadSettingsFromSavedGameData();
    this.SetGameDisplayValues();
  }

  public void ChangeAcceleratedClock()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.AcceleratedClockValue = (int) this.acceleratedClock_Slider.value;
    this.SetGameDisplayValues();
  }

  public void ChangeGameSpeed()
  {
    PersistentData.globalTimeScale = this.gameSpeed_Slider.value;
    PersistentSingleton<SaveManager>.Instance.gameSettings.GlobalTimeScale = this.gameSpeed_Slider.value;
    this.SetGameDisplayValues();
  }

  public void ChangeClockSpeed()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ClockSpeed = (int) this.clockSpeed_Slider.value;
    this.SetGameDisplayValues();
  }

  public void ChangeInjuryFrequency()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.InjuryFrequency = (int) this.injuryFrequency_Slider.value;
    this.SetGameDisplayValues();
  }

  public void ToggleTwoMinuteWarning()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.TwoMinuteWarningEnabled = !PersistentSingleton<SaveManager>.Instance.gameSettings.TwoMinuteWarningEnabled;
    this.SetGameDisplayValues();
  }

  public void ToggleIgnoreControllers()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ignoreControllers = !PersistentSingleton<SaveManager>.Instance.gameSettings.ignoreControllers;
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.ignoreControllers)
    {
      if ((Object) ControllerManagerTitle.self != (Object) null)
      {
        ControllerManagerTitle.self.usingController = false;
        ControllerManagerTitle.self.player1UsingController = false;
        ControllerManagerTitle.self.player2UsingController = false;
        if ((Object) UserManager.instance.GetUser(Player.One) != (Object) null)
          UserManager.instance.RemoveUser(UserManager.instance.GetUser(Player.One));
      }
      if ((Object) ControllerManagerGame.self != (Object) null)
        ControllerManagerGame.usingController = false;
      this.ShowGameOptions();
    }
    else if ((Object) ControllerManagerTitle.self != (Object) null)
    {
      int num = ControllerManagerTitle.self.usingController ? 1 : 0;
    }
    this.SetGameDisplayValues();
  }

  public void TogglePassingSystemP1()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP1 = !PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP1;
    this.SetGameDisplayValues();
  }

  public void ChangeAimSensitivity_P1()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.SensitivitySliderP1 = this.p1Sensitivity_Slider.value;
    OptionsState.SensitivityP1.Value = this.p1Sensitivity_Slider.value;
    this.SetGameDisplayValues();
  }

  public void TogglePassingSystemP2()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP2 = !PersistentSingleton<SaveManager>.Instance.gameSettings.ButtonPassingP2;
    this.SetGameDisplayValues();
  }

  public void ChangeAimSensitivity_P2()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.SensitivitySliderP2 = this.p2Sensitivity_Slider.value;
    OptionsState.SensitivityP2.Value = this.p2Sensitivity_Slider.value;
    this.SetGameDisplayValues();
  }

  public void ToggleControllerIconStyle()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ControllerIconStyle = PersistentSingleton<SaveManager>.Instance.gameSettings.ControllerIconStyle != 1 ? 1 : 2;
    ControllerButtonSwapper.instance.ChangeToStyle(PersistentSingleton<SaveManager>.Instance.gameSettings.ControllerIconStyle);
    this.SetGameDisplayValues();
  }

  public void ShowPenaltyOptions()
  {
    this.subTitle_Txt.text = "PENALTY";
    UISoundManager.instance.PlayTabSwipe();
    this.optionSectionIndex = 3;
    this.additionalInfoPenalty_Txt.text = "";
    LeanTween.moveLocalX(this.activeIndicator_GO, this.penaltyBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.penaltyBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.optionsContainer_GO, (float) ((double) this.optionsContainer_Trans.rect.width * (double) this.optionSectionIndex * -1.0), this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    this.ResetTabTextColors();
    this.penalty_Btn.interactable = false;
    this.penaltyOptions_Scrollbar.value = 1f;
    this.SetPenaltyDisplayValues();
    if (!this.IsUsingController())
      return;
    this.HighlightPenaltyOption(0);
  }

  private void SetPenaltyDisplayValues()
  {
    this.penaltyFrequency_Slider.value = PersistentSingleton<SaveManager>.Instance.gameSettings.PenaltyFrequency;
    this.penaltyFrequency_Txt.text = this.penaltyFrequency_Slider.value.ToString("n0");
    this.delayOfGame_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.DelayOfGame ? "ON" : "OFF";
    this.offHolding_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.OffensiveHolding ? "ON" : "OFF";
    this.offPassInt_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.OffensivePassInterference ? "ON" : "OFF";
    this.facemask_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.Facemask ? "ON" : "OFF";
    this.encroachment_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.Encroachment ? "ON" : "OFF";
    this.falseStart_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.FalseStart ? "ON" : "OFF";
    this.offsides_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.Offsides ? "ON" : "OFF";
    this.defPassInt_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.DefensivePassInterference ? "ON" : "OFF";
    this.kickingOutOfBounds_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.KickoffOutOfBounds ? "ON" : "OFF";
    this.intentionalGrounding_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.IntentionalGrounding ? "ON" : "OFF";
  }

  private void HighlightPenaltyOption(int index)
  {
    this.selectedIndex = index;
    this.HideAllPenaltyOptions();
    this.penaltyOption_Anis[this.selectedIndex].SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void HideAllPenaltyOptions()
  {
    for (int index = 0; index < this.penaltyOption_Anis.Length; ++index)
      this.penaltyOption_Anis[index].SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void ShowAdditionalInformation_Penalty()
  {
    if (this.selectedIndex == 0)
      this.additionalInfoPenalty_Txt.text = "Controls how often penalties are called. A higher value indicates more penalties.";
    else
      this.additionalInfoPenalty_Txt.text = "";
  }

  private void SelectNextPenaltyOption()
  {
    int index1 = this.selectedIndex + 1;
    if (index1 >= this.penaltyOption_Anis.Length)
      return;
    if (!this.penaltyOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 + 1; index2 < this.penaltyOption_Anis.Length; ++index2)
      {
        if (this.penaltyOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 >= this.penaltyOption_Anis.Length)
      return;
    if (index1 >= this.beginScrollItemIndex)
      this.penaltyOptions_Scrollbar.value = (float) (1.0 - 100.0 / (double) (this.penaltyOption_Anis.Length - this.maxVisibleItemsInWindow) * 0.0099999997764825821 * (double) (index1 - this.beginScrollItemIndex));
    this.HighlightPenaltyOption(index1);
  }

  private void SelectPreviousPenaltyOption()
  {
    int index1 = this.selectedIndex - 1;
    if (index1 < 0)
      return;
    if (!this.penaltyOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (this.penaltyOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 < 0)
      return;
    if (index1 >= this.beginScrollItemIndex)
      this.penaltyOptions_Scrollbar.value = (float) (1.0 - 100.0 / (double) (this.penaltyOption_Anis.Length - this.maxVisibleItemsInWindow) * 0.0099999997764825821 * (double) (index1 - this.beginScrollItemIndex));
    this.HighlightPenaltyOption(index1);
  }

  public void ResetPenaltySettings()
  {
    UISoundManager.instance.PlayButtonClick();
    PersistentSingleton<SaveManager>.Instance.gameSettings.SetDefaultPenaltySettings();
    this.LoadSettingsFromSavedGameData();
    this.SetPenaltyDisplayValues();
  }

  public void ChangePenaltyFrequency()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.PenaltyFrequency = (float) (int) this.penaltyFrequency_Slider.value;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleDelayOfGame()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.DelayOfGame = !PersistentSingleton<SaveManager>.Instance.gameSettings.DelayOfGame;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleOffensiveHolding()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.OffensiveHolding = !PersistentSingleton<SaveManager>.Instance.gameSettings.OffensiveHolding;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleOffensivePassInt()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.OffensivePassInterference = !PersistentSingleton<SaveManager>.Instance.gameSettings.OffensivePassInterference;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleFacemask()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.Facemask = !PersistentSingleton<SaveManager>.Instance.gameSettings.Facemask;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleEncroachment()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.Encroachment = !PersistentSingleton<SaveManager>.Instance.gameSettings.Encroachment;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleFalseStart()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.FalseStart = !PersistentSingleton<SaveManager>.Instance.gameSettings.FalseStart;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleOffsides()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.Offsides = !PersistentSingleton<SaveManager>.Instance.gameSettings.Offsides;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleDefensivePassInt()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.DefensivePassInterference = !PersistentSingleton<SaveManager>.Instance.gameSettings.DefensivePassInterference;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleKickingOutOfBounds()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.KickoffOutOfBounds = !PersistentSingleton<SaveManager>.Instance.gameSettings.KickoffOutOfBounds;
    this.SetPenaltyDisplayValues();
  }

  public void ToggleIntentionalGrounding()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.IntentionalGrounding = !PersistentSingleton<SaveManager>.Instance.gameSettings.IntentionalGrounding;
    this.SetPenaltyDisplayValues();
  }

  public void ShowDataOptions()
  {
    this.subTitle_Txt.text = "DATA";
    UISoundManager.instance.PlayTabSwipe();
    this.optionSectionIndex = 4;
    LeanTween.moveLocalX(this.activeIndicator_GO, this.dataBtn_Trans.localPosition.x, this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    RectTransform activeIndicatorTrans = this.activeIndicator_Trans;
    Rect rect = this.dataBtn_Trans.rect;
    double width = (double) rect.width;
    rect = this.activeIndicator_Trans.rect;
    double height = (double) rect.height;
    Vector2 to = new Vector2((float) width, (float) height);
    double indicatorAnimationSpeed = (double) this.activeIndicatorAnimationSpeed;
    LeanTween.size(activeIndicatorTrans, to, (float) indicatorAnimationSpeed).setIgnoreTimeScale(true);
    LeanTween.moveLocalX(this.optionsContainer_GO, (float) ((double) this.optionsContainer_Trans.rect.width * (double) this.optionSectionIndex * -1.0), this.activeIndicatorAnimationSpeed).setIgnoreTimeScale(true);
    this.ResetTabTextColors();
    this.data_Btn.interactable = false;
    this.SetDataDisplayValues();
    if (!this.IsUsingController())
      return;
    this.HighlightFirstDataOption();
  }

  private void SetDataDisplayValues()
  {
    this.useBaseTeams_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets ? "ON" : "OFF";
    this.useMods_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets ? "ON" : "OFF";
    this.logGames_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.LogGamesToFile ? "ON" : "OFF";
    this.exportGameStats_Txt.text = PersistentSingleton<SaveManager>.Instance.gameSettings.ExportGameStatsToFile ? "ON" : "OFF";
  }

  private void HighlightFirstDataOption()
  {
    for (int index = 0; index < this.dataOption_Anis.Length; ++index)
    {
      if (this.dataOption_Anis[index].gameObject.activeInHierarchy)
      {
        this.HighlightDataOption(index);
        break;
      }
    }
  }

  private void HighlightDataOption(int index)
  {
    this.selectedIndex = index;
    this.HideAllDataOptions();
    this.dataOption_Anis[this.selectedIndex].SetTrigger(HashIDs.self.highlighted_Trigger);
  }

  private void HideAllDataOptions()
  {
    for (int index = 0; index < this.dataOption_Anis.Length; ++index)
      this.dataOption_Anis[index].SetTrigger(HashIDs.self.normal_Trigger);
  }

  private void SelectNextDataOption()
  {
    int index1 = this.selectedIndex + 1;
    if (index1 >= this.dataOption_Anis.Length)
      return;
    if (!this.dataOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 + 1; index2 < this.dataOption_Anis.Length; ++index2)
      {
        if (this.dataOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 >= this.dataOption_Anis.Length)
      return;
    this.HighlightDataOption(index1);
  }

  private void SelectPreviousDataOption()
  {
    int index1 = this.selectedIndex - 1;
    if (index1 < 0)
      return;
    if (!this.dataOption_Anis[index1].gameObject.activeInHierarchy)
    {
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (this.dataOption_Anis[index2].gameObject.activeInHierarchy)
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 < 0)
      return;
    this.HighlightDataOption(index1);
  }

  public void ResetDataSettings()
  {
    UISoundManager.instance.PlayButtonClick();
    PersistentSingleton<SaveManager>.Instance.gameSettings.SetDefaultDataSettings();
    this.SetDataDisplayValues();
  }

  public void ToggleUseBaseTeams()
  {
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets)
    {
      PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets = false;
      if (!PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets)
        PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets = true;
    }
    else
      PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets = true;
    this.SetDataDisplayValues();
  }

  public void ToggleUseMods()
  {
    if (PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets)
    {
      PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets = false;
      if (!PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets)
        PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets = true;
    }
    else
      PersistentSingleton<SaveManager>.Instance.gameSettings.UseModAssets = true;
    this.SetDataDisplayValues();
  }

  public void ToggleLogGamesToFile()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.LogGamesToFile = !PersistentSingleton<SaveManager>.Instance.gameSettings.LogGamesToFile;
    this.SetDataDisplayValues();
  }

  public void ToggleExportGameStatsToFile()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ExportGameStatsToFile = !PersistentSingleton<SaveManager>.Instance.gameSettings.ExportGameStatsToFile;
    this.SetDataDisplayValues();
  }

  public void ShowConfirmDeleteDataWindow()
  {
    UISoundManager.instance.PlayButtonClick();
    this.confirmDeleteData_CG.blocksRaycasts = true;
    LeanTween.alphaCanvas(this.confirmDeleteData_CG, 1f, 0.3f);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.yes_Btn);
  }

  public void HideConfirmDeleteWindow()
  {
    this.confirmDeleteData_CG.blocksRaycasts = false;
    LeanTween.alphaCanvas(this.confirmDeleteData_CG, 0.0f, 0.3f);
    ControllerManagerTitle.self.DeselectCurrentUIElement();
    this.ShowDataOptions();
  }

  private bool IsConfirmDeleteDataVisible() => (double) this.confirmDeleteData_CG.alpha > 0.0;

  public void DeleteAllSaveGameData()
  {
    UISoundManager.instance.PlayButtonClick();
    PersistentSingleton<SaveManager>.Instance.DeleteAllSaveGameData();
    this.ShowDataOptions();
    this.HideConfirmDeleteWindow();
  }

  private void ManageControllerSupport()
  {
    if (!this.IsVisible() || !this.IsUsingController() || !this.allowMove || this.IsConfirmDeleteDataVisible())
      return;
    float h = UserManager.instance.LeftStickX(Player.One);
    float v = UserManager.instance.LeftStickY(Player.One);
    if (UserManager.instance.RightBumperWasPressed(Player.One))
      this.SelectNextCategory();
    else if (UserManager.instance.LeftBumperWasPressed(Player.One))
      this.SelectPreviousCategory();
    if (this.optionSectionIndex == 0)
      this.ManageAudioSectionControls(h, v);
    else if (this.optionSectionIndex == 1)
      this.ManageVideoSectionControls(h, v);
    else if (this.optionSectionIndex == 2)
      this.ManageGameSectionControls(h, v);
    else if (this.optionSectionIndex == 3)
    {
      this.ManagePenaltySectionControls(h, v);
    }
    else
    {
      if (this.optionSectionIndex != 4)
        return;
      this.ManageDataSectionControls(h, v);
    }
  }

  private bool IsUsingController() => this.inTitleScreen ? ControllerManagerTitle.self.usingController : ControllerManagerGame.usingController;

  private void SelectNextCategory()
  {
    if (this.optionSectionIndex == 0)
      this.ShowVideoOptions();
    else if (this.optionSectionIndex == 1)
      this.ShowGameOptions();
    else if (this.optionSectionIndex == 2)
      this.ShowPenaltyOptions();
    else if (this.optionSectionIndex == 3 && this.dataBtn_Trans.gameObject.activeInHierarchy)
      this.ShowDataOptions();
    else
      this.ShowAudioOptions();
  }

  private void SelectPreviousCategory()
  {
    if (this.optionSectionIndex == 3)
      this.ShowGameOptions();
    else if (this.optionSectionIndex == 2)
      this.ShowVideoOptions();
    else if (this.optionSectionIndex == 1)
      this.ShowAudioOptions();
    else if (this.optionSectionIndex == 0 && this.dataBtn_Trans.gameObject.activeInHierarchy)
      this.ShowDataOptions();
    else
      this.ShowPenaltyOptions();
  }

  private void ManageAudioSectionControls(float h, float v)
  {
    if ((double) v < -0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectNextAudioOption();
    }
    else if ((double) v > 0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectPreviousAudioOption();
    }
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeSoundFromController(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeSoundFromController(this.sliderChangeAmount * -1f);
      }
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeMusicFromController(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeMusicFromController(this.sliderChangeAmount * -1f);
      }
    }
    else if (this.selectedIndex == 2)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeAnnouncerFromController(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeAnnouncerFromController(this.sliderChangeAmount * -1f);
      }
    }
    else
    {
      if (this.selectedIndex != 3 || !UserManager.instance.Action1WasPressed(Player.One))
        return;
      this.ResetAudioSettings();
    }
  }

  private void ChangeSoundFromController(float v)
  {
    this.soundFX_Slider.value += v;
    this.ChangeSoundLevel();
  }

  private void ChangeMusicFromController(float v)
  {
    this.music_Slider.value += v;
    this.ChangeMusicLevel();
  }

  private void ChangeAnnouncerFromController(float v)
  {
    this.announcer_Slider.value += v;
    this.ChangeAnnouncerLevel();
  }

  private void ManageVideoSectionControls(float h, float v)
  {
    if ((double) v < -0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectNextVideoOption();
    }
    else if ((double) v > 0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectPreviousVideoOption();
    }
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeCameraZoomFromController(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeCameraZoomFromController(this.sliderChangeAmount * -1f);
      }
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeCameraHeightFromController(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeCameraHeightFromController(this.sliderChangeAmount * -1f);
      }
    }
    else if (this.selectedIndex == 2)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleSidelinePlayers();
    }
    else if (this.selectedIndex == 3)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleFullScreen();
    }
    else if (this.selectedIndex == 4)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleRunInBackground();
    }
    else if (this.selectedIndex == 5)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetNextResolution();
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.SetPreviousResolution();
      }
    }
    else if (this.selectedIndex == 6)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.IncreaseGraphicsQuality();
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.DecreaseGraphicsQuality();
      }
    }
    else
    {
      if (this.selectedIndex != 7 || !UserManager.instance.Action1WasPressed(Player.One))
        return;
      this.ResetVideoSettings();
    }
  }

  private void ChangeCameraZoomFromController(float v)
  {
    this.cameraZoom_Slider.value += v;
    this.ChangeCameraZoom();
  }

  private void ChangeCameraHeightFromController(float v)
  {
    this.cameraHeight_Slider.value += v;
    this.ChangeCameraHeight();
  }

  private void ManageGameSectionControls(float h, float v)
  {
    if ((double) v < -0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectNextGameOption();
    }
    else if ((double) v > 0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectPreviousGameOption();
    }
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeAcceleratedClockFromController(1f);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeAcceleratedClockFromController(-1f);
      }
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeGameSpeedFromController(0.1f);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeGameSpeedFromController(-0.1f);
      }
    }
    else if (this.selectedIndex == 2)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeClockSpeedFromController(1f);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeClockSpeedFromController(-1f);
      }
    }
    else if (this.selectedIndex == 3)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeInjuryFrequencyFromController(1f);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeInjuryFrequencyFromController(-1f);
      }
    }
    else if (this.selectedIndex == 4)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleTwoMinuteWarning();
    }
    else if (this.selectedIndex == 5)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleIgnoreControllers();
    }
    else if (this.selectedIndex == 6)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.TogglePassingSystemP1();
    }
    else if (this.selectedIndex == 7)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeAimSensitivityWithController_P1(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeAimSensitivityWithController_P1(this.sliderChangeAmount * -1f);
      }
    }
    else if (this.selectedIndex == 8)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.TogglePassingSystemP2();
    }
    else if (this.selectedIndex == 9)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangeAimSensitivityWithController_P2(this.sliderChangeAmount);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangeAimSensitivityWithController_P2(this.sliderChangeAmount * -1f);
      }
    }
    else if (this.selectedIndex == 10)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleControllerIconStyle();
    }
    else
    {
      if (this.selectedIndex != 11 || !UserManager.instance.Action1WasPressed(Player.One))
        return;
      this.ResetGameSettings();
    }
  }

  private void ChangeAcceleratedClockFromController(float v)
  {
    this.acceleratedClock_Slider.value += v;
    this.ChangeAcceleratedClock();
  }

  private void ChangeGameSpeedFromController(float v)
  {
    this.gameSpeed_Slider.value += v;
    this.ChangeGameSpeed();
  }

  private void ChangeClockSpeedFromController(float v)
  {
    this.clockSpeed_Slider.value += v;
    this.ChangeClockSpeed();
  }

  private void ChangeInjuryFrequencyFromController(float v)
  {
    this.injuryFrequency_Slider.value += v;
    this.ChangeInjuryFrequency();
  }

  private void ChangeAimSensitivityWithController_P1(float v)
  {
    this.p1Sensitivity_Slider.value += v;
    OptionsState.SensitivityP1.Value = this.p1Sensitivity_Slider.value;
    this.ChangeAimSensitivity_P1();
  }

  private void ChangeAimSensitivityWithController_P2(float v)
  {
    this.p2Sensitivity_Slider.value += v;
    OptionsState.SensitivityP2.Value = this.p2Sensitivity_Slider.value;
    this.ChangeAimSensitivity_P2();
  }

  private void ManagePenaltySectionControls(float h, float v)
  {
    if ((double) v < -0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectNextPenaltyOption();
    }
    else if ((double) v > 0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectPreviousPenaltyOption();
    }
    if (this.selectedIndex == 0)
    {
      if ((double) h > 0.40000000596046448)
      {
        this.StartCoroutine(this.DisableMove());
        this.ChangePenaltyFrequencyFromController(2f);
      }
      else
      {
        if ((double) h >= -0.40000000596046448)
          return;
        this.StartCoroutine(this.DisableMove());
        this.ChangePenaltyFrequencyFromController(-2f);
      }
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleDelayOfGame();
    }
    else if (this.selectedIndex == 2)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleOffensiveHolding();
    }
    else if (this.selectedIndex == 3)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleOffensivePassInt();
    }
    else if (this.selectedIndex == 4)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleFacemask();
    }
    else if (this.selectedIndex == 5)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleEncroachment();
    }
    else if (this.selectedIndex == 6)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleFalseStart();
    }
    else if (this.selectedIndex == 7)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleOffsides();
    }
    else if (this.selectedIndex == 8)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleDefensivePassInt();
    }
    else if (this.selectedIndex == 9)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleKickingOutOfBounds();
    }
    else if (this.selectedIndex == 10)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleIntentionalGrounding();
    }
    else
    {
      if (this.selectedIndex != 11 || !UserManager.instance.Action1WasPressed(Player.One))
        return;
      this.ResetPenaltySettings();
    }
  }

  private void ChangePenaltyFrequencyFromController(float v)
  {
    this.penaltyFrequency_Slider.value += v;
    this.ChangePenaltyFrequency();
  }

  private void ManageDataSectionControls(float h, float v)
  {
    if ((double) v < -0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectNextDataOption();
    }
    else if ((double) v > 0.40000000596046448)
    {
      this.StartCoroutine(this.DisableMove());
      this.SelectPreviousDataOption();
    }
    if (this.selectedIndex == 0)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleUseBaseTeams();
    }
    else if (this.selectedIndex == 1)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleUseMods();
    }
    else if (this.selectedIndex == 2)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleLogGamesToFile();
    }
    else if (this.selectedIndex == 3)
    {
      if ((double) h <= 0.40000000596046448 && (double) h >= -0.40000000596046448)
        return;
      this.StartCoroutine(this.DisableMove());
      this.ToggleExportGameStatsToFile();
    }
    else if (this.selectedIndex == 4)
    {
      if (!UserManager.instance.Action1WasPressed(Player.One))
        return;
      this.ShowConfirmDeleteDataWindow();
    }
    else
    {
      if (this.selectedIndex != 5 || !UserManager.instance.Action1WasPressed(Player.One))
        return;
      this.ResetDataSettings();
    }
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this.disableMove_WFS;
    this.allowMove = true;
  }
}
