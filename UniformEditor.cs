// Decompiled with JetBrains decompiler
// Type: UniformEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UniformEditor : MonoBehaviour
{
  [Header("Main")]
  public UniformManager uniformManager;
  public Transform playerTrans;
  [HideInInspector]
  public Transform cameraTrans;
  [HideInInspector]
  public Transform cameraHolderTrans;
  [SerializeField]
  private Camera cam;
  private int totalNumberOfTeams;
  [Header("Left Panel")]
  [SerializeField]
  private ColorPicker colorSelector;
  [SerializeField]
  private Text letterFont_Txt;
  [SerializeField]
  private Text letterIndex_Txt;
  [SerializeField]
  private Text numberFont_Txt;
  [SerializeField]
  private Text numberIndex_Txt;
  [SerializeField]
  private Image letterFillImg;
  [SerializeField]
  private Image letterOutlineImg;
  [SerializeField]
  private Image numFillImg;
  [SerializeField]
  private Image numOuterOutlineImg;
  [SerializeField]
  private Image numInnerOutlineImg;
  [SerializeField]
  private Image sleeveColImg;
  [SerializeField]
  private Image armBandColImg;
  [SerializeField]
  private Image visorColImg;
  [SerializeField]
  private Image backgroundColorImg;
  [SerializeField]
  private Toggle letterOutlineTog;
  [SerializeField]
  private Toggle numOuterOutlineTog;
  [SerializeField]
  private Toggle numInnerOutlineTog;
  [SerializeField]
  private Toggle sleeveNumbersTog;
  [SerializeField]
  private Toggle shoulderNumbersTog;
  [SerializeField]
  private Toggle randomName;
  [SerializeField]
  private Toggle randomNumber;
  public InputField inputName;
  public InputField inputNumber;
  private int totalNumberFonts;
  private int numberFontIndex;
  private int totalLetterFonts;
  private int letterFontIndex;
  [HideInInspector]
  public Toggle armSleevesTog;
  [HideInInspector]
  public Toggle armBandTog;
  [HideInInspector]
  public Toggle visorTog;
  [HideInInspector]
  public Toggle chromeHelmetTog;
  [HideInInspector]
  public Toggle matteHelmetTog;
  [Header("Right Panel")]
  public Selectable saveUniformButton;
  public Selectable deleteUniformButton;
  public Selectable exitButton;
  [SerializeField]
  private Text teamSelectTxt;
  [SerializeField]
  private Text uniformSetTxt;
  [SerializeField]
  private Text helmetTxt;
  [SerializeField]
  private Text jerseyTxt;
  [SerializeField]
  private Text pantsTxt;
  [SerializeField]
  private Text teamSelectIndexTxt;
  [SerializeField]
  private Text uniformIndexTxt;
  [SerializeField]
  private Text helmetIndexTxt;
  [SerializeField]
  private Text jerseyIndexTxt;
  [SerializeField]
  private Text pantsIndexTxt;
  [SerializeField]
  private Dropdown teamSelect_Drp;
  [Header("Save Uniform Window")]
  public InputField uniformNameInput;
  public Transform saveUniformWindow;
  public Transform confirmOverrideWindow;
  public UniformEditorKeyboard keyboard;
  public Dropdown savedUniformSelect;
  [SerializeField]
  private Text statusText;
  [SerializeField]
  private Text confirmOverrideText;
  [SerializeField]
  private Selectable saveWindowFirstSelect;
  [SerializeField]
  private Selectable confirmOverrideFirstSelect;
  [Header("Message Box")]
  [SerializeField]
  private Text messageBoxTitle;
  [SerializeField]
  private Text messageBoxText;
  [Header("Delete Uniform Window")]
  public Transform deleteUniformWindow;
  [SerializeField]
  private Text warningText;
  [SerializeField]
  private Text delUniformText;
  [SerializeField]
  private Selectable deleteWindowFirstSelect;
  [Header("Confirm Exit Window")]
  public Transform confirmExitWindow;
  [SerializeField]
  private Selectable confirmExitFirstSelect;
  private Material armBandMaterial;
  private Material armSleeveMaterial;
  private Material visorMaterial;
  private Renderer armSleevesRenderer;
  private Renderer armBandsRenderer;
  private Color numberFill;
  private Color numberOuterOutline;
  private Color numberInnerOutline;
  private Color letterFill;
  private Color letterOutline;
  private Color armBandColor;
  private Color armSleeveColor;
  private Color visorColor;
  private FontType numberFont;
  private FontType letterFont;
  private Texture2D blankJersey;
  private ColorEditor editingColor;
  private Image activeImage;
  public bool playerHidden;
  [Header("Uniform Controller Manager")]
  public UniformEditorControllerManager controllerManager;
  [HideInInspector]
  public string[] sampleNames;
  private UniformSet currentUniformSet;
  [HideInInspector]
  public int currentUniformIndex;
  private int currentHelmetIndex;
  private int currentJerseyIndex;
  private int currentPantIndex;
  private int currentTeamIndex;
  private string overrideFilename;
  private float h;
  private float v;
  private float m;
  private WaitForSeconds _delaySaveWindowHide;

  private void Start()
  {
    this.CloseColorSelector();
    this.HideOverrideConfirmation();
    this.HideUniformSaveWindow();
    this.HideConfirmExitWindow();
    this.LoadTeamList();
    this.sampleNames = new string[5]
    {
      "RICHARDS",
      "JUGAN",
      "ACCARDI",
      "HAGUE",
      "BRADY"
    };
    this.playerHidden = false;
    this.backgroundColorImg.color = this.cam.backgroundColor;
    this.numberFont = FontType.Classic;
    this.letterFont = FontType.Classic;
    this.numberFill = Color.black;
    this.letterFill = Color.black;
    this.numberOuterOutline = Color.white;
    this.numberInnerOutline = Color.white;
    this.letterOutline = Color.white;
    this.armBandColor = Color.white;
    this.armSleeveColor = Color.white;
    this.visorColor = Color.white;
    this.currentTeamIndex = 0;
    this.totalLetterFonts = 2;
    this.totalNumberFonts = 39;
    this.visorMaterial = this.uniformManager.GetVisorMaterial();
    this.armBandMaterial = this.uniformManager.GetArmBandMaterial();
    this.armSleeveMaterial = this.uniformManager.GetArmSleeveMaterial();
    this.armBandsRenderer = this.uniformManager.GetArmBandsRenderer();
    this.armSleevesRenderer = this.uniformManager.GetArmSleevesRenderer();
    this.ToggleArmBands();
    this.ToggleArmSleeves();
    this.ToggleVisor();
    LoadingScreenManager.self.HideWindow();
    this.SetTeam();
  }

  public void ShowConfirmExitWindow()
  {
    this.confirmExitWindow.gameObject.SetActive(true);
    this.StartCoroutine(this.SelectGUIItem(this.confirmExitFirstSelect));
  }

  public void HideConfirmExitWindow()
  {
    this.confirmExitWindow.gameObject.SetActive(false);
    this.StartCoroutine(this.SelectGUIItem(this.exitButton));
  }

  public void Exit()
  {
    this.HideConfirmExitWindow();
    LoadingScreenManager.self.LoadScene("Title Screen", "Loading Title Screen");
  }

  public void SetTeamFromDropdown()
  {
    this.currentTeamIndex = this.teamSelect_Drp.value;
    this.SetTeam();
  }

  public void SetTeam()
  {
    TeamFile teamFile = TeamAssetManager.LoadTeamFile(this.currentTeamIndex);
    this.teamSelectTxt.text = teamFile.GetCity() + "\n" + teamFile.GetName();
    this.teamSelectIndexTxt.text = "(" + (this.currentTeamIndex + 1).ToString() + "/" + this.totalNumberOfTeams.ToString() + ")";
    PersistentData.homeTeamUniform = UniformAssetManager.GetUniformSet(this.currentTeamIndex);
    this.currentUniformSet = PersistentData.homeTeamUniform;
    this.currentUniformIndex = 0;
    this.currentHelmetIndex = this.currentUniformSet.GetPieceForUniformSet(this.currentUniformIndex, UniformPiece.Helmets);
    this.currentJerseyIndex = this.currentUniformSet.GetPieceForUniformSet(this.currentUniformIndex, UniformPiece.Jerseys);
    this.currentPantIndex = this.currentUniformSet.GetPieceForUniformSet(this.currentUniformIndex, UniformPiece.Pants);
    this.SetUniform(this.currentUniformIndex);
  }

  public void SelectPreviousTeam()
  {
    if (this.currentTeamIndex == 0)
      this.currentTeamIndex = this.totalNumberOfTeams - 1;
    else
      --this.currentTeamIndex;
    this.SetTeam();
  }

  public void SelectNextTeam()
  {
    if (this.currentTeamIndex == this.totalNumberOfTeams - 1)
      this.currentTeamIndex = 0;
    else
      ++this.currentTeamIndex;
    this.SetTeam();
  }

  private void LoadTeamList()
  {
    this.totalNumberOfTeams = 0;
    if (AssetManager.UseBaseAssets)
    {
      this.totalNumberOfTeams += TeamAssetManager.NUMBER_OF_BASE_TEAMS;
      for (int index = 0; index < TeamResourcesManager.BASE_TEAM_FOLDERS.Length; ++index)
        this.teamSelect_Drp.options.Add(new Dropdown.OptionData()
        {
          text = TeamResourcesManager.UPPERCASE_BASE_TEAM_FOLDERS[index]
        });
    }
    if (AssetManager.UseModsAssets)
    {
      this.totalNumberOfTeams += ModManager.self.GetCountOfTeamFolders();
      for (int i = 0; i < ModManager.self.GetCountOfTeamFolders(); ++i)
        this.teamSelect_Drp.options.Add(new Dropdown.OptionData()
        {
          text = ModManager.self.GetTeamFolderAt(i).ToUpper()
        });
    }
    this.teamSelect_Drp.captionText.text = this.teamSelect_Drp.options[0].text;
  }

  public void SetNextUniform()
  {
    this.currentUniformIndex = this.currentUniformIndex + 1 < this.currentUniformSet.GetNumberOfUniforms() ? this.currentUniformIndex + 1 : 0;
    this.SetUniform(this.currentUniformIndex);
  }

  public void SetPrevUniform()
  {
    int numberOfUniforms = this.currentUniformSet.GetNumberOfUniforms();
    this.currentUniformIndex = this.currentUniformIndex - 1 < 0 ? numberOfUniforms - 1 : this.currentUniformIndex - 1;
    this.SetUniform(this.currentUniformIndex);
  }

  public void GetNextHelmet()
  {
    this.currentHelmetIndex = this.currentHelmetIndex + 1 < this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets) ? this.currentHelmetIndex + 1 : 0;
    this.SetHelmetTexture(this.currentUniformSet.GetHelmetTexture(this.currentHelmetIndex));
  }

  public void GetPrevHelmet()
  {
    int numberOfUniformPieces = this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets);
    this.currentHelmetIndex = this.currentHelmetIndex - 1 < 0 ? numberOfUniformPieces - 1 : this.currentHelmetIndex - 1;
    this.SetHelmetTexture(this.currentUniformSet.GetHelmetTexture(this.currentHelmetIndex));
  }

  private void SetHelmetTexture(Texture2D tex)
  {
    this.uniformManager.SetHelmetTexture(tex);
    this.helmetTxt.text = this.currentUniformSet.GetUniformPieceNameByIndex(UniformPiece.Helmets, this.currentHelmetIndex).ToUpper().Replace("_", " ");
    this.helmetIndexTxt.text = "(" + (this.currentHelmetIndex + 1).ToString() + "/" + this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Helmets).ToString() + ")";
  }

  public void GetNextJersey()
  {
    UnityEngine.Resources.UnloadUnusedAssets();
    this.currentJerseyIndex = this.currentJerseyIndex + 1 < this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys) ? this.currentJerseyIndex + 1 : 0;
    this.SetJerseyTexture(this.currentUniformSet.GetJerseyTexture(this.currentJerseyIndex));
    this.ApplyJersey();
  }

  public void GetPrevJersey()
  {
    UnityEngine.Resources.UnloadUnusedAssets();
    int numberOfUniformPieces = this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys);
    this.currentJerseyIndex = this.currentJerseyIndex - 1 < 0 ? numberOfUniformPieces - 1 : this.currentJerseyIndex - 1;
    this.SetJerseyTexture(this.currentUniformSet.GetJerseyTexture(this.currentJerseyIndex));
    this.ApplyJersey();
  }

  private void SetJerseyTexture(Texture2D tex)
  {
    this.uniformManager.SetJerseyTexture(tex);
    this.blankJersey = UnityEngine.Object.Instantiate<Texture2D>(tex);
    this.jerseyTxt.text = this.currentUniformSet.GetUniformPieceNameByIndex(UniformPiece.Jerseys, this.currentJerseyIndex).ToUpper().Replace("_", " ");
    this.jerseyIndexTxt.text = "(" + (this.currentJerseyIndex + 1).ToString() + "/" + this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Jerseys).ToString() + ")";
  }

  public void GetNextPant()
  {
    this.currentPantIndex = this.currentPantIndex + 1 < this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants) ? this.currentPantIndex + 1 : 0;
    this.SetPantTexture(this.currentUniformSet.GetPantTexture(this.currentPantIndex));
  }

  public void GetPrevPant()
  {
    int numberOfUniformPieces = this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants);
    this.currentPantIndex = this.currentPantIndex - 1 < 0 ? numberOfUniformPieces - 1 : this.currentPantIndex - 1;
    this.SetPantTexture(this.currentUniformSet.GetPantTexture(this.currentPantIndex));
  }

  private void SetPantTexture(Texture2D tex)
  {
    this.uniformManager.SetPantTexture(tex);
    this.pantsTxt.text = this.currentUniformSet.GetUniformPieceNameByIndex(UniformPiece.Pants, this.currentPantIndex).ToUpper().Replace("_", " ");
    this.pantsIndexTxt.text = "(" + (this.currentPantIndex + 1).ToString() + "/" + this.currentUniformSet.GetNumberOfUniformPieces(UniformPiece.Pants).ToString() + ")";
  }

  private void SetUniform(int uniformIndex)
  {
    this.currentUniformIndex = uniformIndex;
    this.currentHelmetIndex = this.currentUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Helmets);
    this.currentJerseyIndex = this.currentUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Jerseys);
    this.currentPantIndex = this.currentUniformSet.GetPieceForUniformSet(uniformIndex, UniformPiece.Pants);
    this.SetHelmetTexture(this.currentUniformSet.GetHelmetTexture(this.currentHelmetIndex));
    this.SetJerseyTexture(this.currentUniformSet.GetJerseyTexture(this.currentJerseyIndex));
    this.SetPantTexture(this.currentUniformSet.GetPantTexture(this.currentPantIndex));
    this.uniformIndexTxt.text = "(" + (uniformIndex + 1).ToString() + "/" + this.currentUniformSet.GetNumberOfUniforms().ToString() + ")";
    this.uniformSetTxt.text = this.currentUniformSet.GetUniformNameByIndex(uniformIndex).ToUpper();
    UniformConfig uniformConfig = this.currentUniformSet.GetUniformConfig(uniformIndex);
    this.SetNumberFont(uniformConfig.GetNumberFontType());
    this.numberFill = uniformConfig.GetNumberFillColor();
    this.numFillImg.color = this.numberFill;
    this.numOuterOutlineTog.isOn = uniformConfig.JerseyHasNumberOutline1();
    if (this.numOuterOutlineTog.isOn)
    {
      this.numberOuterOutline = uniformConfig.GetNumberOutlineColor1();
      this.numOuterOutlineImg.color = this.numberOuterOutline;
    }
    this.numInnerOutlineTog.isOn = uniformConfig.JerseyHasNumberOutline2();
    if (this.numInnerOutlineTog.isOn)
    {
      this.numberInnerOutline = uniformConfig.GetNumberOutlineColor2();
      this.numInnerOutlineImg.color = this.numberInnerOutline;
    }
    this.SetLetterFont(uniformConfig.GetLetterFontType());
    this.letterFill = uniformConfig.GetLetterFillColor();
    this.letterFillImg.color = this.letterFill;
    this.letterOutlineTog.isOn = uniformConfig.JerseyHasLetterOutline();
    if (this.letterOutlineTog.isOn)
    {
      this.letterOutline = uniformConfig.GetLetterOutlineColor();
      this.letterOutlineImg.color = this.letterOutline;
    }
    this.armSleeveColor = uniformConfig.GetArmSleevesColor();
    this.sleeveColImg.color = this.armSleeveColor;
    this.ToggleArmSleeves();
    this.armBandColor = uniformConfig.GetArmBandColor();
    this.armBandColImg.color = this.armBandColor;
    this.ToggleArmBands();
    this.visorColor = uniformConfig.GetHelmetVisorColor();
    this.visorColImg.color = this.visorColor;
    this.ToggleVisor();
    this.sleeveNumbersTog.isOn = uniformConfig.JerseyHasSleeveNumbers();
    this.shoulderNumbersTog.isOn = uniformConfig.JerseyHasShoulderNumbers();
    this.chromeHelmetTog.isOn = uniformConfig.HelmetIsChrome();
    this.matteHelmetTog.isOn = uniformConfig.HelmetIsMatte();
    if (this.chromeHelmetTog.isOn)
      this.ToggleChromeHelmet();
    else if (this.matteHelmetTog.isOn)
      this.ToggleMatteHelmet();
    this.ApplyJersey();
  }

  public void ApplyJersey()
  {
    int jerseyNumber = this.randomNumber.isOn || this.inputNumber.text == "" ? UnityEngine.Random.Range(1, 100) : int.Parse(this.inputNumber.text);
    this.uniformManager.SetJerseyTexture(this.CreateJersey(!this.randomName.isOn ? this.inputName.text : this.sampleNames[UnityEngine.Random.Range(0, this.sampleNames.Length)], jerseyNumber));
    this.ToggleArmBands();
    this.ToggleArmSleeves();
    this.ToggleVisor();
  }

  private Texture2D CreateJersey(string playerName, int jerseyNumber)
  {
    UniformWriter.jerseyTexture = UnityEngine.Object.Instantiate<Texture2D>(this.blankJersey);
    try
    {
      UniformWriter.jerseyTextureMap = UniformWriter.jerseyTexture.GetPixels32();
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Caught exception at CreateJersey () of Uniform Writer. Error: " + ex?.ToString()));
      return UniformWriter.jerseyTexture;
    }
    if (this.numInnerOutlineTog.isOn)
      UniformWriter.WriteNumbers(jerseyNumber, this.numberFont, this.numberFill, this.numberOuterOutline, this.numberInnerOutline, this.shoulderNumbersTog.isOn, this.sleeveNumbersTog.isOn);
    else if (this.numOuterOutlineTog.isOn)
      UniformWriter.WriteNumbers(jerseyNumber, this.numberFont, this.numberFill, this.numberOuterOutline, this.shoulderNumbersTog.isOn, this.sleeveNumbersTog.isOn);
    else
      UniformWriter.WriteNumbers(jerseyNumber, this.numberFont, this.numberFill, this.shoulderNumbersTog.isOn, this.sleeveNumbersTog.isOn);
    if (this.letterOutlineTog.isOn)
      UniformWriter.WriteLetters(playerName, this.letterFont, this.letterFill, this.letterOutline);
    else
      UniformWriter.WriteLetters(playerName, this.letterFont, this.letterFill);
    try
    {
      UniformWriter.jerseyTexture.Compress(true);
      UniformWriter.jerseyTexture.anisoLevel = 1;
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Caught excpetion in SetJersey() of UniformManager. Trying to Compress texture. Error: " + ex.ToString()));
    }
    return UniformWriter.jerseyTexture;
  }

  private void SetNumberFont(FontType fontType)
  {
    this.numberFontIndex = (int) fontType;
    this.SetNumberFont();
  }

  private void SetNumberFont()
  {
    this.numberFont = (FontType) this.numberFontIndex;
    this.numberFont_Txt.text = Common.EnumToString(this.numberFont.ToString());
    this.numberIndex_Txt.text = "(" + (this.numberFontIndex + 1).ToString() + "/" + this.totalNumberFonts.ToString() + ")";
    this.ApplyJersey();
  }

  public void SelectPreviousNumberFont()
  {
    if (this.numberFontIndex == 0)
      this.numberFontIndex = this.totalNumberFonts - 1;
    else
      --this.numberFontIndex;
    this.SetNumberFont();
  }

  public void SelectNextNumberFont()
  {
    if (this.numberFontIndex == this.totalNumberFonts - 1)
      this.numberFontIndex = 0;
    else
      ++this.numberFontIndex;
    this.SetNumberFont();
  }

  private void SetLetterFont(FontType fontType)
  {
    this.letterFontIndex = (int) fontType;
    this.SetLetterFont();
  }

  private void SetLetterFont()
  {
    this.letterFont = (FontType) this.letterFontIndex;
    this.letterFont_Txt.text = Common.EnumToString(this.letterFont.ToString());
    this.letterIndex_Txt.text = "(" + (this.letterFontIndex + 1).ToString() + "/" + this.totalLetterFonts.ToString() + ")";
    this.ApplyJersey();
  }

  public void SelectPreviousLetterFont()
  {
    if (this.letterFontIndex == 0)
      this.letterFontIndex = this.totalLetterFonts - 1;
    else
      --this.letterFontIndex;
    this.SetLetterFont();
  }

  public void SelectNextLetterFont()
  {
    if (this.letterFontIndex == this.totalLetterFonts - 1)
      this.letterFontIndex = 0;
    else
      ++this.letterFontIndex;
    this.SetLetterFont();
  }

  public void SetLetterFill() => this.ShowColorSelector(this.letterFillImg, ColorEditor.NameFill);

  public void SetLetterOutline() => this.ShowColorSelector(this.letterOutlineImg, ColorEditor.NameOutline);

  public void SetNumberFill() => this.ShowColorSelector(this.numFillImg, ColorEditor.NumberFill);

  public void SetNumberOuterOutline() => this.ShowColorSelector(this.numOuterOutlineImg, ColorEditor.NumberOuterOutline);

  public void SetNumberInnerOutline() => this.ShowColorSelector(this.numInnerOutlineImg, ColorEditor.NumberInnerOutline);

  public void SetBackgroundColor() => this.ShowColorSelector(this.backgroundColorImg, ColorEditor.Background);

  public void SetSleeveColor() => this.ShowColorSelector(this.sleeveColImg, ColorEditor.Sleeves);

  public void SetArmBandsColor() => this.ShowColorSelector(this.armBandColImg, ColorEditor.ArmBands);

  public void SetVisorColor() => this.ShowColorSelector(this.visorColImg, ColorEditor.Visor);

  public void ToggleNumOuterOutline()
  {
    if (!this.numOuterOutlineTog.isOn)
      this.numInnerOutlineTog.isOn = false;
    this.ApplyJersey();
  }

  public void ToggleNumInnerOutline()
  {
    if (this.numInnerOutlineTog.isOn)
      this.numOuterOutlineTog.isOn = true;
    this.ApplyJersey();
  }

  public void ToggleSleeveNumbers() => this.ApplyJersey();

  public void ToggleShoulderNumbers() => this.ApplyJersey();

  public void ToggleChromeHelmet()
  {
    if (this.chromeHelmetTog.isOn)
    {
      this.matteHelmetTog.isOn = false;
      this.uniformManager.SetHelmetType(true, false);
    }
    else
      this.uniformManager.SetHelmetType(false, false);
  }

  public void ToggleMatteHelmet()
  {
    if (this.matteHelmetTog.isOn)
    {
      this.chromeHelmetTog.isOn = false;
      this.uniformManager.SetHelmetType(false, true);
    }
    else
      this.uniformManager.SetHelmetType(false, false);
  }

  public void ToggleArmBands(bool forceShow = false)
  {
    if (this.armBandTog.isOn | forceShow)
    {
      this.armBandsRenderer.enabled = true;
      this.armBandMaterial.color = new Color(this.armBandColor.r, this.armBandColor.g, this.armBandColor.b, 1f);
      this.armSleevesTog.isOn = false;
      this.ToggleArmSleeves();
    }
    else
      this.armBandsRenderer.enabled = false;
  }

  public void ToggleArmSleeves(bool forceShow = false)
  {
    if (this.armSleevesTog.isOn | forceShow)
    {
      this.armSleevesRenderer.enabled = true;
      this.armSleeveMaterial.color = new Color(this.armSleeveColor.r, this.armSleeveColor.g, this.armSleeveColor.b, 1f);
      this.armBandTog.isOn = false;
      this.ToggleArmBands();
    }
    else
      this.armSleevesRenderer.enabled = false;
  }

  public void ToggleVisor(bool forceShow = false)
  {
    if (this.visorTog.isOn | forceShow)
      this.visorMaterial.color = new Color(this.visorColor.r, this.visorColor.g, this.visorColor.b, 0.6f);
    else
      this.visorMaterial.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  }

  public void ShowColorSelector(Image colorImage, ColorEditor c)
  {
    this.activeImage = (Image) null;
    this.colorSelector.CurrentColor = colorImage.color;
    this.activeImage = colorImage;
    this.editingColor = c;
    this.colorSelector.gameObject.SetActive(true);
    this.controllerManager.firstColorSelectorSlider.Select();
  }

  public void CloseColorSelector()
  {
    this.colorSelector.gameObject.SetActive(false);
    this.controllerManager.SelectDefaultLeft();
  }

  public bool IsColorSelectorActive() => this.colorSelector.gameObject.activeInHierarchy;

  public void UpdateColor()
  {
    if (!((UnityEngine.Object) this.activeImage != (UnityEngine.Object) null))
      return;
    this.activeImage.color = this.colorSelector.CurrentColor;
    if (this.editingColor == ColorEditor.NameFill)
      this.letterFill = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.NameOutline)
      this.letterOutline = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.NumberFill)
      this.numberFill = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.NumberOuterOutline)
      this.numberOuterOutline = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.NumberInnerOutline)
      this.numberInnerOutline = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.ArmBands)
      this.armBandColor = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.Sleeves)
      this.armSleeveColor = this.colorSelector.CurrentColor;
    else if (this.editingColor == ColorEditor.Visor)
    {
      this.visorColor = this.colorSelector.CurrentColor;
    }
    else
    {
      if (this.editingColor != ColorEditor.Background)
        return;
      this.cam.backgroundColor = this.colorSelector.CurrentColor;
    }
  }

  public void ShowUniformDeleteWindow()
  {
    this.warningText.text = "";
    this.delUniformText.text = this.uniformSetTxt.text;
    this.deleteUniformWindow.gameObject.SetActive(true);
    this.StartCoroutine(this.SelectGUIItem(this.deleteWindowFirstSelect));
  }

  public void HideUniformDeleteWindow()
  {
    this.deleteUniformWindow.gameObject.SetActive(false);
    this.StartCoroutine(this.SelectGUIItem(this.deleteUniformButton));
  }

  public void DeleteUniform()
  {
  }

  public void ShowUniformSaveWindow()
  {
    this.SetUniformDropdown(this.IsModTeam());
    this.uniformNameInput.text = "";
    this.statusText.text = "";
    this.saveUniformWindow.gameObject.SetActive(true);
    this.StartCoroutine(this.SelectGUIItem(this.saveWindowFirstSelect));
  }

  public void HideUniformSaveWindow()
  {
    this.saveUniformWindow.gameObject.SetActive(false);
    this.StartCoroutine(this.SelectGUIItem(this.saveUniformButton));
  }

  public void HideMessageBox() => this.messageBoxTitle.transform.parent.gameObject.SetActive(false);

  private void SetUniformDropdown(bool isModTeam)
  {
    this.savedUniformSelect.ClearOptions();
    this.savedUniformSelect.captionText.text = "SELECT UNIFORM";
    this.savedUniformSelect.options.Add(new Dropdown.OptionData()
    {
      text = ""
    });
    this.savedUniformSelect.value = 0;
    string[] namesOfUniforms = PersistentData.homeTeamUniform.GetNamesOfUniforms();
    for (int uniformIndex = 0; uniformIndex < namesOfUniforms.Length; ++uniformIndex)
    {
      UniformConfig uniformConfig = PersistentData.homeTeamUniform.GetUniformConfig(uniformIndex);
      if (isModTeam || uniformConfig.IsCustomBaseUniform)
        this.savedUniformSelect.options.Add(new Dropdown.OptionData()
        {
          text = namesOfUniforms[uniformIndex]
        });
    }
  }

  public void SelectSavedUniform() => this.uniformNameInput.text = this.savedUniformSelect.captionText.text.Trim();

  private void SaveNew_TabToNextField()
  {
  }

  private void ShowMessageBox(string title, string message)
  {
    this.messageBoxTitle.transform.parent.gameObject.SetActive(true);
    this.messageBoxTitle.text = title;
    this.messageBoxText.text = message;
  }

  public void SaveUniformSet()
  {
    if (this.uniformNameInput.text == "")
    {
      this.statusText.text = "ENTER A UNIFORM NAME";
    }
    else
    {
      if (!this.IsModTeam())
        return;
      string upper = this.uniformNameInput.text.ToUpper();
      if (!UniformAssetManager.SaveUniformSet_IsFilenameAvailable(this.currentTeamIndex, upper))
      {
        this.ShowOverrideConfirmation(upper);
      }
      else
      {
        this.currentUniformSet.LockInUniformPieces(this.currentUniformIndex, this.currentHelmetIndex, this.currentJerseyIndex, this.currentPantIndex);
        this.SetCurrentUniformSetConfig();
        UniformAssetManager.SaveUniformSet_WriteToFile(this.currentTeamIndex, upper, this.currentUniformSet);
        PersistentData.homeTeamUniform = UniformAssetManager.GetUniformSet(this.currentTeamIndex);
        this.RefreshTextures(this.currentUniformIndex);
        this.HideUniformSaveWindow();
      }
    }
  }

  private void SetCurrentUniformSetConfig()
  {
    UniformConfig uniformConfig = this.currentUniformSet.GetUniformConfig(this.currentUniformIndex);
    uniformConfig.SetHelmetType(this.chromeHelmetTog.isOn, this.matteHelmetTog.isOn);
    uniformConfig.SetJerseyHasShoulderNumbers(this.shoulderNumbersTog.isOn);
    uniformConfig.SetJerseyHasSleeveNumbers(this.sleeveNumbersTog.isOn);
    uniformConfig.SetLetterFontType(this.letterFont);
    uniformConfig.SetNumberFontType(this.numberFont);
    uniformConfig.SetNumberFillColor(this.numberFill);
    uniformConfig.SetNumberOutlineColor1(this.numOuterOutlineTog.isOn, this.numberOuterOutline);
    uniformConfig.SetNumberOutlineColor2(this.numInnerOutlineTog.isOn, this.numberInnerOutline);
    uniformConfig.SetLetterOutlineColor(this.letterOutlineTog.isOn, this.letterOutline);
    uniformConfig.SetLetterFillColor(this.letterFill);
    uniformConfig.SetArmSleevesColor(this.armSleeveColor);
    uniformConfig.SetArmBandColor(this.armBandColor);
    uniformConfig.SetHelmetVisorColor(this.visorColor);
    this.currentUniformSet.SetUniformConfig(this.currentUniformIndex, uniformConfig);
  }

  private UniformConfig CreateUniformConfigFromCurrentSettings()
  {
    UniformConfig fromCurrentSettings = new UniformConfig();
    fromCurrentSettings.HelmetName = this.helmetTxt.text;
    fromCurrentSettings.JerseyName = this.jerseyTxt.text;
    fromCurrentSettings.PantName = this.pantsTxt.text;
    fromCurrentSettings.SetHelmetType(this.chromeHelmetTog.isOn, this.matteHelmetTog.isOn);
    fromCurrentSettings.SetJerseyHasShoulderNumbers(this.shoulderNumbersTog.isOn);
    fromCurrentSettings.SetJerseyHasSleeveNumbers(this.sleeveNumbersTog.isOn);
    fromCurrentSettings.SetLetterFontType(this.letterFont);
    fromCurrentSettings.SetNumberFontType(this.numberFont);
    fromCurrentSettings.SetNumberFillColor(this.numberFill);
    fromCurrentSettings.SetNumberOutlineColor1(this.numOuterOutlineTog.isOn, this.numberOuterOutline);
    fromCurrentSettings.SetNumberOutlineColor2(this.numInnerOutlineTog.isOn, this.numberInnerOutline);
    fromCurrentSettings.SetLetterOutlineColor(this.letterOutlineTog.isOn, this.letterOutline);
    fromCurrentSettings.SetLetterFillColor(this.letterFill);
    fromCurrentSettings.SetArmSleevesColor(this.armSleeveColor);
    fromCurrentSettings.SetArmBandColor(this.armBandColor);
    fromCurrentSettings.SetHelmetVisorColor(this.visorColor);
    return fromCurrentSettings;
  }

  public void ShowOverrideConfirmation(string fileName)
  {
    this.overrideFilename = fileName;
    this.confirmOverrideText.text = "OVERRIDE   " + fileName + "   UNIFORM?";
    this.confirmOverrideWindow.gameObject.SetActive(true);
    this.StartCoroutine(this.SelectGUIItem(this.confirmOverrideFirstSelect));
  }

  public void HideOverrideConfirmation()
  {
    this.confirmOverrideWindow.gameObject.SetActive(false);
    this.StartCoroutine(this.SelectGUIItem(this.saveWindowFirstSelect));
  }

  public void ConfirmOverride()
  {
    if (this.IsModTeam())
    {
      this.currentUniformSet.LockInUniformPieces(this.currentUniformIndex, this.currentHelmetIndex, this.currentJerseyIndex, this.currentPantIndex);
      this.SetCurrentUniformSetConfig();
      UniformAssetManager.SaveUniformSet_WriteToFile(this.currentTeamIndex, this.overrideFilename, this.currentUniformSet);
    }
    PersistentData.homeTeamUniform = UniformAssetManager.GetUniformSet(this.currentTeamIndex);
    this.RefreshTextures(this.currentUniformIndex);
    this.overrideFilename = "";
    this.HideOverrideConfirmation();
    this.HideUniformSaveWindow();
  }

  public void UnrandomName()
  {
    this.randomName.isOn = this.inputName.text == "";
    this.ApplyJersey();
  }

  public void UnrandomNumber()
  {
    this.randomNumber.isOn = this.inputNumber.text == "";
    this.ApplyJersey();
  }

  public void RandomizeName() => this.inputName.text = "";

  public void RandomizeNumber() => this.inputNumber.text = "";

  private bool IsOnPC() => true;

  public void LinkToUniformEditorTutorial() => Application.OpenURL("https://youtu.be/ah70D-1Ku34");

  public bool IsModTeam() => this.currentTeamIndex >= TeamAssetManager.NUMBER_OF_BASE_TEAMS || !PersistentSingleton<SaveManager>.Instance.gameSettings.UseBaseAssets;

  public IEnumerator SelectGUIItem(Selectable s)
  {
    yield return (object) null;
    if (this.controllerManager.usingController)
      s.Select();
  }

  private IEnumerator DelayedSaveWindowHide()
  {
    this._delaySaveWindowHide = new WaitForSeconds(0.2f);
    yield return (object) this._delaySaveWindowHide;
    this.HideUniformSaveWindow();
  }

  public void RefreshTextures(int uniformIndex = 0)
  {
    this.currentUniformSet.ClearAllTextures();
    this.SetTeam();
    this.SetUniform(uniformIndex);
  }
}
