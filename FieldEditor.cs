// Decompiled with JetBrains decompiler
// Type: FieldEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using Framework;
using System.Collections;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class FieldEditor : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  [SerializeField]
  private UnityEngine.UI.Button firstItem_Btn;
  [Header("Field Selection")]
  [SerializeField]
  private TextMeshProUGUI fieldStyle_Txt;
  [SerializeField]
  private GameObject fieldStyle_GO;
  [SerializeField]
  private UnityEngine.UI.Button firstSelection_Btn;
  [SerializeField]
  private Image field_Img;
  private int fieldIndex;
  [Header("Endzone Selection")]
  [SerializeField]
  private TextMeshProUGUI endzoneStyle_Txt;
  [SerializeField]
  private GameObject endzoneStyle_GO;
  private int endzoneIndex;
  private TeamData selectedTeamData;
  private StadiumSet teamStadiumSet;
  private Texture2D[] allCustomFields;
  private Texture2D[] allCustomEndzones;
  private bool allowMove;
  private WaitForSecondsRealtime disableMove_WFS;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public void Init()
  {
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(TeamData editingTeam)
  {
    this.selectedTeamData = editingTeam;
    PopupLoadingScreen.self.ShowPopupLoadingScreen("LOADING FIELD OPTIONS");
    this.StartCoroutine(this.Continue_ShowWindow());
  }

  private IEnumerator Continue_ShowWindow()
  {
    yield return (object) null;
    this.disableMove_WFS = new WaitForSecondsRealtime(0.2f);
    this.allowMove = true;
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    BottomBarManager.instance.ShowBackButton();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.firstItem_Btn);
    this.allCustomFields = AddressablesData.instance.LoadAssetsSync<Texture2D>(AddressablesData.CorrectingAssetKey("fieldtextures"));
    this.allCustomEndzones = AddressablesData.instance.LoadAssetsSync<Texture2D>(AddressablesData.CorrectingAssetKey("custom_endzones"));
    StadiumSetDatabase.SetStadiumForHomeCity(this.selectedTeamData.GetCity());
    this.teamStadiumSet = StadiumSetDatabase.GetStadiumData();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.firstSelection_Btn);
    this.SetFieldDisplay(this.FindIndexOfFieldTexture(this.selectedTeamData.GetCustomBuiltInFieldTextureName()));
    this.SetEndzoneDisplay(this.FindIndexOfEndzoneGraphic(this.selectedTeamData.GetCustomEndzoneName()));
    PopupLoadingScreen.self.HidePopupLoadingScreen();
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.UnloadAssetBundle("fieldtextures", true);
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.UnloadAssetBundle("custome_endzones", true);
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    this.HideWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowWindow();
    SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
  }

  private void SetFieldDisplay(int index)
  {
    if (index < -1)
      index = this.allCustomFields.Length - 1;
    else if (index >= this.allCustomFields.Length)
      index = -1;
    this.fieldIndex = index;
    if (this.fieldIndex == -1)
    {
      this.fieldStyle_Txt.text = "STADIUM DEFAULT";
      this.selectedTeamData.SetCustomField("");
    }
    else
    {
      TextMeshProUGUI fieldStyleTxt = this.fieldStyle_Txt;
      string[] strArray = new string[6]
      {
        this.allCustomFields[this.fieldIndex].name.ToUpper(),
        " (",
        null,
        null,
        null,
        null
      };
      int num = this.fieldIndex + 1;
      strArray[2] = num.ToString();
      strArray[3] = "/";
      num = this.allCustomFields.Length;
      strArray[4] = num.ToString();
      strArray[5] = ")";
      string str = string.Concat(strArray);
      fieldStyleTxt.text = str;
      this.selectedTeamData.SetCustomField(this.allCustomFields[this.fieldIndex].name);
    }
  }

  public void SetNextField() => this.SetFieldDisplay(this.fieldIndex + 1);

  public void SetPreviousField() => this.SetFieldDisplay(this.fieldIndex - 1);

  private int FindIndexOfFieldTexture(string name)
  {
    for (int indexOfFieldTexture = 0; indexOfFieldTexture < this.allCustomFields.Length; ++indexOfFieldTexture)
    {
      if (this.allCustomFields[indexOfFieldTexture].name == name)
        return indexOfFieldTexture;
    }
    return -1;
  }

  private void SetEndzoneDisplay(int index)
  {
    if (index < -1)
      index = this.allCustomEndzones.Length - 1;
    else if (index >= this.allCustomEndzones.Length)
      index = -1;
    this.endzoneIndex = index;
    if (this.endzoneIndex == -1)
    {
      this.endzoneStyle_Txt.text = "DEFAULT";
      this.selectedTeamData.SetCustomEndzone("");
    }
    else
    {
      TextMeshProUGUI endzoneStyleTxt = this.endzoneStyle_Txt;
      string[] strArray = new string[6]
      {
        this.allCustomEndzones[this.endzoneIndex].name.ToUpper(),
        " (",
        null,
        null,
        null,
        null
      };
      int num = this.endzoneIndex + 1;
      strArray[2] = num.ToString();
      strArray[3] = "/";
      num = this.allCustomEndzones.Length;
      strArray[4] = num.ToString();
      strArray[5] = ")";
      string str = string.Concat(strArray);
      endzoneStyleTxt.text = str;
      this.selectedTeamData.SetCustomEndzone(this.allCustomEndzones[this.endzoneIndex].name);
    }
    this.SetFieldDisplay(this.fieldIndex);
  }

  public void SetNextEndzone() => this.SetEndzoneDisplay(this.endzoneIndex + 1);

  public void SetPreviousEndzone() => this.SetEndzoneDisplay(this.endzoneIndex - 1);

  private int FindIndexOfEndzoneGraphic(string name)
  {
    for (int ofEndzoneGraphic = 0; ofEndzoneGraphic < this.allCustomEndzones.Length; ++ofEndzoneGraphic)
    {
      if (this.allCustomEndzones[ofEndzoneGraphic].name == name)
        return ofEndzoneGraphic;
    }
    return -1;
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || !ControllerManagerTitle.self.usingController)
      return;
    GameObject selectedUiElement = ControllerManagerTitle.self.GetCurrentSelectedUIElement();
    float num = UserManager.instance.LeftStickX(Player.One);
    if (!this.allowMove)
      return;
    if ((Object) selectedUiElement == (Object) this.fieldStyle_GO)
    {
      if ((double) num > 0.5)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetNextField();
      }
      else
      {
        if ((double) num >= -0.5)
          return;
        this.StartCoroutine(this.DisableMove());
        this.SetPreviousField();
      }
    }
    else
    {
      if (!((Object) selectedUiElement == (Object) this.endzoneStyle_GO))
        return;
      MonoBehaviour.print((object) ("Selected endonze: " + num.ToString()));
      if ((double) num > 0.5)
      {
        this.StartCoroutine(this.DisableMove());
        this.SetNextEndzone();
      }
      else
      {
        if ((double) num >= -0.5)
          return;
        this.StartCoroutine(this.DisableMove());
        this.SetPreviousEndzone();
      }
    }
  }

  private IEnumerator DisableMove()
  {
    this.allowMove = false;
    yield return (object) this.disableMove_WFS;
    this.allowMove = true;
  }
}
