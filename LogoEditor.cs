// Decompiled with JetBrains decompiler
// Type: LogoEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using Framework;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class LogoEditor : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [Header("Logo Select Window")]
  [SerializeField]
  private CanvasGroup logoSelectWindow_CG;
  [SerializeField]
  private TextMeshProUGUI categoryDisplay_Txt;
  [SerializeField]
  private TextMeshProUGUI pageDisplay_Txt;
  [SerializeField]
  private CustomLogoItem[] customLogoItems;
  [SerializeField]
  private UnityEngine.UI.Button firstItem_Btn;
  private int numberOfLogosPerPage = 15;
  private string[] logoAssetBundleNames;
  private string[] logoCategoryNames;
  [Header("Color Select Window")]
  [SerializeField]
  private CanvasGroup colorSelectWindow_CG;
  [SerializeField]
  private Image logoDisplay_Img;
  [SerializeField]
  private Image redReplacementColor_Img;
  [SerializeField]
  private Image greenReplacementColor_Img;
  [SerializeField]
  private Image blueReplacementColor_Img;
  [SerializeField]
  private Image whiteReplacementColor_Img;
  [SerializeField]
  private UnityEngine.UI.Button redColor_Btn;
  private Color redReplacementColor;
  private Color greenReplacementColor;
  private Color blueReplacementColor;
  private Color whiteReplacementColor;
  private Texture2D editingLogoTexture;
  private Color32[] sourceLogoColorMap;
  private Color32[] editingLogoTextureMap;
  private LogoColorEditFields editingColor;
  private TeamData selectedTeamData;
  private string selectedLogoName;
  private string currentLogoAssetBundle;
  private int selectedLogoIndex;
  private int assetBundleIndex;
  private int pageIndex;
  private Sprite[] allCustomLogos;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.HideLogoSelectWindow();
    this.HideColorSelectWindow();
  }

  public void Init()
  {
    this.currentLogoAssetBundle = (string) null;
    this.logoAssetBundleNames = new string[10]
    {
      "custom_logos/axis",
      "custom_logos/mascots",
      "custom_logos/block_letters",
      "custom_logos/classic_letters",
      "custom_logos/italic_letters",
      "custom_logos/symbol_letters",
      "custom_logos/locations_1",
      "custom_logos/locations_2",
      "custom_logos/state_logos",
      "custom_logos/generic_logos"
    };
    this.logoCategoryNames = new string[10]
    {
      "AXIS LOGOS",
      "MASCOTS",
      "BLOCK LETTERS",
      "CLASSIC LETTERS",
      "ITALIC LETTERS",
      "SYMBOL LETTERS",
      "LOCATIONS STYLE 1",
      "LOCATIONS STYLE 2",
      "STATE LOGOS",
      "GENERIC LOGOS"
    };
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(TeamData editingTeam)
  {
    this.selectedTeamData = editingTeam;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.ShowBackButton();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    this.ShowLogoSelectWindow();
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.UnloadAssetBundle(this.currentLogoAssetBundle, false);
    this.HideColorSelectWindow();
    this.HideLogoSelectWindow();
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    if (this.IsColorSelectWindowVisible())
    {
      this.HideColorSelectWindow();
      this.ShowLogoSelectWindow();
    }
    else
    {
      if (!this.IsLogoSelectWindowVisible())
        return;
      this.HideWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
    }
  }

  private void ShowLogoSelectWindow()
  {
    LeanTween.alphaCanvas(this.logoSelectWindow_CG, 1f, 0.3f);
    this.logoSelectWindow_CG.blocksRaycasts = true;
    this.SetLogoCategory(0);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.firstItem_Btn);
  }

  private void HideLogoSelectWindow()
  {
    LeanTween.alphaCanvas(this.logoSelectWindow_CG, 0.0f, 0.3f);
    this.logoSelectWindow_CG.blocksRaycasts = false;
  }

  private bool IsLogoSelectWindowVisible() => !this.IsColorSelectWindowVisible();

  public void SelectLogo(int logoIndex)
  {
    this.selectedLogoIndex = logoIndex;
    this.selectedLogoName = this.allCustomLogos[this.selectedLogoIndex].name;
    this.HideLogoSelectWindow();
    this.ShowColorSelectWindow();
  }

  public void SetLogoCategory(int logoCategory)
  {
    int length = this.logoCategoryNames.Length;
    if (logoCategory >= length || logoCategory < 0)
      return;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.UnloadAssetBundle(this.currentLogoAssetBundle, false);
    this.assetBundleIndex = logoCategory;
    this.categoryDisplay_Txt.text = this.logoCategoryNames[this.assetBundleIndex];
    this.LoadLogoAssetBundleCategory(this.logoAssetBundleNames[this.assetBundleIndex]);
    this.SetPage(0);
  }

  public void SetNextLogoCategory() => this.SetLogoCategory(this.assetBundleIndex + 1);

  public void SetPreviousLogoCategory() => this.SetLogoCategory(this.assetBundleIndex - 1);

  private void LoadLogoAssetBundleCategory(string assetBundleCategory)
  {
    this.allCustomLogos = AddressablesData.instance.LoadAssetsSync<Sprite>(AddressablesData.CorrectingAssetKey(assetBundleCategory));
    this.currentLogoAssetBundle = assetBundleCategory;
  }

  public void SetPage(int pIndex)
  {
    int length = this.allCustomLogos.Length;
    int num1 = Mathf.CeilToInt((float) length / (float) this.numberOfLogosPerPage);
    if (pIndex >= num1 || pIndex < 0)
      return;
    this.pageIndex = pIndex;
    this.pageDisplay_Txt.text = "PAGE " + (this.pageIndex + 1).ToString() + "/" + num1.ToString();
    int num2 = this.pageIndex * this.numberOfLogosPerPage;
    int num3 = Mathf.Min(num2 + this.numberOfLogosPerPage, length) - 1 - num2 + 1;
    for (int index = 0; index < num3; ++index)
      this.customLogoItems[index].SetData(this.allCustomLogos[num2 + index], num2 + index);
    for (int index = num3; index < this.customLogoItems.Length; ++index)
      this.customLogoItems[index].HideItem();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.firstItem_Btn);
  }

  public void SetNextPage() => this.SetPage(this.pageIndex + 1);

  public void SetPreviousPage() => this.SetPage(this.pageIndex - 1);

  private void ShowColorSelectWindow()
  {
    LeanTween.alphaCanvas(this.colorSelectWindow_CG, 1f, 0.3f);
    this.colorSelectWindow_CG.blocksRaycasts = true;
    this.editingColor = LogoColorEditFields.None;
    Texture2D texture = this.allCustomLogos[this.selectedLogoIndex].texture;
    this.editingLogoTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
    this.sourceLogoColorMap = texture.GetPixels32();
    this.editingLogoTextureMap = new Color32[this.sourceLogoColorMap.Length];
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.redColor_Btn);
    this.SetDefaultReplacementColors();
    this.SetColorsInButtons();
    this.logoDisplay_Img.sprite = TextureUtility.ColorTextureIntoSprite(this.editingLogoTexture, this.sourceLogoColorMap, this.editingLogoTextureMap, this.redReplacementColor, this.greenReplacementColor, this.blueReplacementColor, this.whiteReplacementColor);
  }

  private void SetDefaultReplacementColors()
  {
    this.redReplacementColor = this.selectedTeamData.GetPrimaryColor();
    this.greenReplacementColor = this.selectedTeamData.GetAlternateColor();
    this.blueReplacementColor = this.selectedTeamData.GetSecondaryColor();
    bool flag1 = false;
    if (this.redReplacementColor == Color.white || this.greenReplacementColor == Color.white || this.blueReplacementColor == Color.white)
      flag1 = true;
    bool flag2 = false;
    if (this.redReplacementColor == Color.black || this.greenReplacementColor == Color.black || this.blueReplacementColor == Color.black)
      flag2 = true;
    if (flag1)
    {
      if (flag2)
        this.whiteReplacementColor = Color.gray;
      else
        this.whiteReplacementColor = Color.black;
    }
    else
      this.whiteReplacementColor = Color.white;
  }

  private void SetColorsInButtons()
  {
    this.redReplacementColor_Img.color = this.redReplacementColor;
    this.greenReplacementColor_Img.color = this.greenReplacementColor;
    this.blueReplacementColor_Img.color = this.blueReplacementColor;
    this.whiteReplacementColor_Img.color = this.whiteReplacementColor;
  }

  public void SelectColorToEdit_Red()
  {
    this.editingColor = LogoColorEditFields.RedColor;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.redReplacementColor);
  }

  public void SelectColorToEdit_Blue()
  {
    this.editingColor = LogoColorEditFields.BlueColor;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.blueReplacementColor);
  }

  public void SelectColorToEdit_Green()
  {
    this.editingColor = LogoColorEditFields.GreenColor;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.greenReplacementColor);
  }

  public void SelectColorToEdit_White()
  {
    this.editingColor = LogoColorEditFields.WhiteColor;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.whiteReplacementColor);
  }

  private void HideColorSelectWindow()
  {
    this.colorSelectWindow_CG.blocksRaycasts = false;
    LeanTween.alphaCanvas(this.colorSelectWindow_CG, 0.0f, 0.3f);
  }

  private bool IsColorSelectWindowVisible() => this.colorSelectWindow_CG.blocksRaycasts;

  public void SaveChanges()
  {
  }

  public void RemoveCustomLogoData()
  {
    if (this.selectedTeamData.CustomLogo != null)
      this.selectedTeamData.SetCustomLogo((CustomLogoData) null);
    this.ReturnToPreviousMenu();
  }

  public void AcceptChanges()
  {
    TeamDataCache.ClearLogoCacheForTeam(this.selectedTeamData);
    this.selectedTeamData.SetCustomLogo(new CustomLogoData(this.selectedLogoName, this.currentLogoAssetBundle, this.redReplacementColor, this.greenReplacementColor, this.blueReplacementColor, this.whiteReplacementColor));
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
  }

  public void ResetChanges()
  {
    this.SetDefaultReplacementColors();
    this.SetColorsInButtons();
    this.logoDisplay_Img.sprite = TextureUtility.ColorTextureIntoSprite(this.editingLogoTexture, this.sourceLogoColorMap, this.editingLogoTextureMap, this.redReplacementColor, this.greenReplacementColor, this.blueReplacementColor, this.whiteReplacementColor);
  }

  public void CancelChanges()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
  }

  public void OnScreenColorSelectorAccepted(Color color)
  {
    if (this.editingColor == LogoColorEditFields.RedColor)
      this.redReplacementColor = color;
    else if (this.editingColor == LogoColorEditFields.GreenColor)
      this.greenReplacementColor = color;
    else if (this.editingColor == LogoColorEditFields.BlueColor)
      this.blueReplacementColor = color;
    else
      this.whiteReplacementColor = color;
    this.SetColorsInButtons();
    this.logoDisplay_Img.sprite = TextureUtility.ColorTextureIntoSprite(this.editingLogoTexture, this.sourceLogoColorMap, this.editingLogoTextureMap, this.redReplacementColor, this.greenReplacementColor, this.blueReplacementColor, this.whiteReplacementColor);
  }

  public void OnScreenColorSelectorClosed() => ControllerManagerTitle.self.SelectUIElement((Selectable) this.redColor_Btn);

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenKeyboard.instance.IsVisible() || OnScreenColorSelector.instance.IsVisible() || !ControllerManagerTitle.self.usingController || !this.IsLogoSelectWindowVisible())
      return;
    if (UserManager.instance.RightTriggerWasPressed(Player.One))
      this.SetNextLogoCategory();
    else if (UserManager.instance.LeftTriggerWasPressed(Player.One))
      this.SetPreviousLogoCategory();
    if (UserManager.instance.RightBumperWasPressed(Player.One))
    {
      this.SetNextPage();
    }
    else
    {
      if (!UserManager.instance.LeftBumperWasPressed(Player.One))
        return;
      this.SetPreviousPage();
    }
  }
}
