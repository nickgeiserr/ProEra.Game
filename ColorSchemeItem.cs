// Decompiled with JetBrains decompiler
// Type: ColorSchemeItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class ColorSchemeItem : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private UnityEngine.UI.Button selector_Btn;
  [SerializeField]
  private TextMeshProUGUI colorSchemeName_Txt;
  [SerializeField]
  private Image primaryColor_Img;
  [SerializeField]
  private Image secondaryColor_Img;
  [SerializeField]
  private Image alternateColor_Img;

  public void ShowItem() => this.mainWindow_GO.SetActive(true);

  public void HideItem() => this.mainWindow_GO.SetActive(false);

  public void SetData(string colorSchemeName, Color primary, Color secondary, Color alternate)
  {
    this.colorSchemeName_Txt.text = colorSchemeName.ToUpper();
    this.primaryColor_Img.color = primary;
    this.secondaryColor_Img.color = secondary;
    this.alternateColor_Img.color = alternate;
    this.ShowItem();
  }

  public void SelectColorScheme() => SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.teamColorsEditor.SelectColorScheme(this);

  public UnityEngine.UI.Button GetButton() => this.selector_Btn;

  public Color GetPrimaryColor() => this.primaryColor_Img.color;

  public Color GetSecondaryColor() => this.secondaryColor_Img.color;

  public Color GetAlternateColor() => this.alternateColor_Img.color;
}
