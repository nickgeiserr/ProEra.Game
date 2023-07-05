// Decompiled with JetBrains decompiler
// Type: TeamColorsEditor
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ControllerSupport;
using System.Collections.Generic;
using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TeamColorsEditor : MonoBehaviour
{
  [Header("Main Section")]
  [SerializeField]
  private CanvasGroup mainWindow_CG;
  private TeamData selectedTeamData;
  [Header("Team Colors Window")]
  [SerializeField]
  private CanvasGroup teamColorsWindow_CG;
  [SerializeField]
  private TextMeshProUGUI teamName_Txt;
  [SerializeField]
  private UnityEngine.UI.Button primaryColor_Btn;
  [SerializeField]
  private Image primaryColorBtn_Img;
  [SerializeField]
  private Image secondaryColorBtn_Img;
  [SerializeField]
  private Image alternateColorBtn_Img;
  [SerializeField]
  private Image largeDisplayPrimary_Img;
  [SerializeField]
  private Image largeDisplaySecondary_Img;
  [SerializeField]
  private Image largeDisplayAlternate_Img;
  private ColorEditingFields editingColor;
  [Header("Select Color Scheme Window")]
  [SerializeField]
  private CanvasGroup selectColorSchemeWindow_CG;
  [SerializeField]
  private ColorSchemeItem[] colorSchemeItems;
  [SerializeField]
  private TextMeshProUGUI leagueDisplay_Txt;
  [SerializeField]
  private TextMeshProUGUI pageDisplay_Txt;
  private string[] axisColorSchemes;
  private string[] countryColorSchemes;
  private string[] europeanColorSchemes;
  private string[] americanColorSchemes;
  private string[] collegeAtlanticColorSchemes;
  private string[] collegeSouthEastColorSchemes;
  private string[] collegeMidWestColorSchemes;
  private string[] collegeCentralColorSchemes;
  private string[] collegePacificColorSchemes;
  private string[] semiProLeague1ColorSchemes;
  private string[] semiProLeague2ColorSchemes;
  private string[] canadianColorSchemes;
  private string[] leagueNames;
  private List<string[]> allColorSchemes;
  private bool haveColorsBeenCreated;
  private int leagueIndex;
  private int pageIndex;

  private void Start()
  {
    this.mainWindow_CG.alpha = 0.0f;
    this.mainWindow_CG.blocksRaycasts = false;
    this.HideSelectColorSchemeWindow();
    this.HideTeamColorsWindow();
  }

  public void Init()
  {
    this.leagueIndex = 0;
    this.pageIndex = 0;
    this.haveColorsBeenCreated = false;
  }

  private void Update() => this.ManageControllerInput();

  public void ShowWindow(TeamData editingTeam)
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 1f, 0.3f);
    this.mainWindow_CG.blocksRaycasts = true;
    this.selectedTeamData = editingTeam;
    this.ShowTeamColorsWindow();
  }

  public void HideWindow()
  {
    LeanTween.alphaCanvas(this.mainWindow_CG, 0.0f, 0.15f);
    this.mainWindow_CG.blocksRaycasts = false;
  }

  public bool IsVisible() => (double) this.mainWindow_CG.alpha > 0.0;

  public void ReturnToPreviousMenu()
  {
    if (this.IsTeamColorsWindowVisible())
    {
      this.HideWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowWindow();
      SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.ShowDashboard();
    }
    else
    {
      if (!this.IsSelectColorSchemeWindowVisible())
        return;
      this.HideSelectColorSchemeWindow();
      this.ShowTeamColorsWindow();
    }
  }

  public void ShowTeamColorsWindow()
  {
    LeanTween.alphaCanvas(this.teamColorsWindow_CG, 1f, 0.3f);
    this.teamColorsWindow_CG.blocksRaycasts = true;
    this.editingColor = ColorEditingFields.None;
    this.teamName_Txt.text = this.selectedTeamData.GetFullDisplayName();
    this.SetTeamColorDisplays();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.primaryColor_Btn);
  }

  public void HideTeamColorsWindow()
  {
    LeanTween.alphaCanvas(this.teamColorsWindow_CG, 0.0f, 0.3f);
    this.teamColorsWindow_CG.blocksRaycasts = false;
  }

  public bool IsTeamColorsWindowVisible() => (double) this.teamColorsWindow_CG.alpha > 0.0;

  private void SetTeamColorDisplays()
  {
    this.primaryColorBtn_Img.color = this.selectedTeamData.GetPrimaryColor();
    this.secondaryColorBtn_Img.color = this.selectedTeamData.GetSecondaryColor();
    this.alternateColorBtn_Img.color = this.selectedTeamData.GetAlternateColor();
    this.largeDisplayPrimary_Img.color = this.selectedTeamData.GetPrimaryColor();
    this.largeDisplaySecondary_Img.color = this.selectedTeamData.GetSecondaryColor();
    this.largeDisplayAlternate_Img.color = this.selectedTeamData.GetAlternateColor();
  }

  public void SelectPrimaryColor()
  {
    this.editingColor = ColorEditingFields.Primary;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.selectedTeamData.GetPrimaryColor());
  }

  public void SelectSecondaryColor()
  {
    this.editingColor = ColorEditingFields.Secondary;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.selectedTeamData.GetSecondaryColor());
  }

  public void SelectAlternateColor()
  {
    this.editingColor = ColorEditingFields.Alternate;
    OnScreenColorSelector.instance.ShowWindow(this.selectedTeamData, this.selectedTeamData.GetAlternateColor());
  }

  public void OpenSelectColorSchemeWindow()
  {
    this.HideTeamColorsWindow();
    this.ShowSelectColorSchemeWindow();
  }

  public void RestoreToDefault()
  {
    this.selectedTeamData.RestoreColorsToDefault();
    this.ShowTeamColorsWindow();
  }

  private void ShowSelectColorSchemeWindow()
  {
    LeanTween.alphaCanvas(this.selectColorSchemeWindow_CG, 1f, 0.3f);
    this.selectColorSchemeWindow_CG.blocksRaycasts = true;
    if (!this.haveColorsBeenCreated)
      this.CreateColorSchemes();
    BottomBarManager.instance.SetControllerButtonGuide(3);
    this.SetLeague(0);
  }

  public void HideSelectColorSchemeWindow()
  {
    LeanTween.alphaCanvas(this.selectColorSchemeWindow_CG, 0.0f, 0.3f);
    this.selectColorSchemeWindow_CG.blocksRaycasts = false;
  }

  public bool IsSelectColorSchemeWindowVisible() => (double) this.selectColorSchemeWindow_CG.alpha > 0.0;

  public void SelectColorScheme(ColorSchemeItem item)
  {
    this.selectedTeamData.SetCustomColorScheme(item.GetPrimaryColor(), item.GetSecondaryColor(), item.GetAlternateColor());
    this.HideSelectColorSchemeWindow();
    this.ShowTeamColorsWindow();
  }

  public void SetPage(int _pageIndex)
  {
    int num1 = 8;
    int length = this.allColorSchemes[this.leagueIndex].Length;
    int num2 = Mathf.CeilToInt((float) length / (float) num1);
    if (_pageIndex >= num2 || _pageIndex < 0)
      return;
    this.pageIndex = _pageIndex;
    this.pageDisplay_Txt.text = "PAGE " + (this.pageIndex + 1).ToString() + "/" + num2.ToString();
    int num3 = this.pageIndex * num1;
    int num4 = Mathf.Min(num3 + num1, length) - 1 - num3 + 1;
    for (int index = 0; index < num4; ++index)
    {
      string str = this.allColorSchemes[this.leagueIndex][num3 + index];
      string hexColor1 = str.Substring(0, 7);
      string hexColor2 = str.Substring(8, 7);
      string hexColor3 = str.Substring(16, 7);
      string colorSchemeName = str.Substring(24);
      Color color1 = TeamColorsEditor.ConvertHexToColor(hexColor1);
      Color color2 = TeamColorsEditor.ConvertHexToColor(hexColor2);
      Color color3 = TeamColorsEditor.ConvertHexToColor(hexColor3);
      this.colorSchemeItems[index].SetData(colorSchemeName, color1, color2, color3);
    }
    for (int index = num4; index < this.colorSchemeItems.Length; ++index)
      this.colorSchemeItems[index].HideItem();
    ControllerManagerTitle.self.SelectUIElement((Selectable) this.colorSchemeItems[0].GetButton());
  }

  private void SetLeague(int _leagueIndex)
  {
    if (_leagueIndex >= this.allColorSchemes.Count || _leagueIndex < 0)
      return;
    this.leagueIndex = _leagueIndex;
    this.leagueDisplay_Txt.text = this.leagueNames[this.leagueIndex];
    this.SetPage(0);
  }

  public void SelectNextLeague() => this.SetLeague(this.leagueIndex + 1);

  public void SelectPreviousLeague() => this.SetLeague(this.leagueIndex - 1);

  public void SetNextPage() => this.SetPage(this.pageIndex + 1);

  public void SetPreviousPage() => this.SetPage(this.pageIndex - 1);

  public void OnScreenColorSelectorAccepted(Color color)
  {
    if (this.editingColor == ColorEditingFields.Primary)
      this.selectedTeamData.SetCustomColorScheme(color, this.selectedTeamData.GetSecondaryColor(), this.selectedTeamData.GetAlternateColor());
    else if (this.editingColor == ColorEditingFields.Secondary)
      this.selectedTeamData.SetCustomColorScheme(this.selectedTeamData.GetPrimaryColor(), color, this.selectedTeamData.GetAlternateColor());
    else
      this.selectedTeamData.SetCustomColorScheme(this.selectedTeamData.GetPrimaryColor(), this.selectedTeamData.GetSecondaryColor(), color);
  }

  public void OnScreenColorSelectorClosed() => this.ShowTeamColorsWindow();

  public static Color ConvertHexToColor(string hexColor)
  {
    Color color;
    if (ColorUtility.TryParseHtmlString(hexColor, out color))
      return color;
    Debug.Log((object) "Invalid HEX string input");
    return Color.cyan;
  }

  private void CreateColorSchemes()
  {
    this.haveColorsBeenCreated = true;
    this.axisColorSchemes = new string[36]
    {
      "#87061A #000000 #FFFFFF ARIZONA",
      "#A6192D #000000 #FFFFFF ATLANTA",
      "#D0B240 #280353 #FFFFFF BALTIMORE",
      "#0D244B #C80815 #FFFFFF BOSTON",
      "#C60C30 #002C7A #FFFFFF BUFFALO",
      "#A5ACAF #0088CE #FFFFFF CAROLINA",
      "#DD4814 #03202F #FFFFFF CHICAGO",
      "#000000 #FB4F14 #FFFFFF CINCINNATI",
      "#FE3C00 #512F2D #FFFFFF CLEVELAND",
      "#0D254C #60666E #FFFFFF DALLAS",
      "#0D254C #FB4F14 #FFFFFF DENVER",
      "#FA813B #000000 #FFFFFF DETROIT",
      "#203731 #F9AE00 #FFFFFF GREEN BAY",
      "#02253A #B31B34 #FFFFFF HOUSTON",
      "#001A36 #86888A #FFFFFF INDIANAPOLIS",
      "#5B0021 #E6BD00 #FFFFFF KANSAS CITY",
      "#CC0000 #000000 #FFFFFF LAS VEGAS",
      "#85754D #13264B #FFFFFF LOS ANGELES",
      "#D2B887 #000000 #FFFFFF LOUISIANA",
      "#CDB87D #192857 #FFFFFF MEXICO CITY",
      "#008D97 #F5811F #FFFFFF MIAMI",
      "#412F86 #F0BF00 #FFFFFF MINNESOTA",
      "#911C24 #FFD41F #FFFFFF MONTREAL",
      "#FB5B1F #000000 #FFFFFF NEBRASKA",
      "#CA001A #192F6B #FFFFFF NEW YORK",
      "#006340 #000000 #FFFFFF OKLAHOMA CITY",
      "#FF3900 #1E65A5 #FFFFFF ORLANDO",
      "#D1BD86 #000000 #FFFFFF PHILADELPHIA",
      "#FFB612 #000000 #FFFFFF PITTSBURGH",
      "#A71930 #000000 #FFFFFF SAN FRANCISCO",
      "#002244 #69BE28 #FFFFFF SEATTLE",
      "#BA0C2F #000000 #FFFFFF ST. LOUIS",
      "#EC1D27 #000000 #FFFFFF TENNESSEE",
      "#5A5A5A #000000 #FFFFFF VANCOUVER",
      "#773141 #ECA500 #FFFFFF WASHINGTON",
      "#88001B #000000 #FFFFFF WINNIPEG"
    };
    this.americanColorSchemes = new string[32]
    {
      "#97233F #000000 #FFB612 Arizona",
      "#A71930 #000000 #A5ACAF Atlanta",
      "#241773 #000000 #9E7C0C Baltimore",
      "#00338D #C60C30 #FFFFFF Buffalo",
      "#0085CA #101820 #BFC0BF Carolina",
      "#0B162A #C83803 #FFFFFF Chicago",
      "#FB4F14 #000000 #FFFFFF Cincinnati",
      "#311D00 #FF3C00 #FFFFFF Cleveland",
      "#041E42 #7F9695 #FFFFFF Dallas",
      "#FB4F14 #002244 #FFFFFF Denver",
      "#0076B6 #B0B7BC #FFFFFF Detroit",
      "#203731 #FFB612 #FFFFFF Green Bay",
      "#03202F #A71930 #FFFFFF Houston",
      "#002C5F #A2AAAD #FFFFFF Indianapolis",
      "#D7A22A #006778 #101820 Jacksonville",
      "#E31837 #FFB81C #FFFFFF Kansas City",
      "#002A5E #FFC20E #0080C6 Los Angeles",
      "#003594 #FFA300 #FFFFFF Los Angeles",
      "#008E97 #FC4C02 #005778 Miami",
      "#4F2683 #FFC62F #FFFFFF Minnesota",
      "#002244 #C60C30 #B0B7BC New England",
      "#D3BC8D #000000 #FFFFFF New Orleans",
      "#0B2265 #A71930 #A5ACAF New York",
      "#125740 #000000 #FFFFFF New York",
      "#000000 #A5ACAF #FFFFFF Oakland",
      "#004C54 #A5ACAF #000000 Philadelphia",
      "#FFB612 #000000 #A5ACAF Pittsburgh",
      "#AA0000 #B3995D #000000 San Francisco",
      "#002244 #69BE28 #A5ACAF Seattle",
      "#D50A0A #FF7900 #0A0A08 Tampa Bay",
      "#0C2340 #4B92DB #C8102E Tennessee",
      "#773141 #FFB612 #FFFFFF Washington"
    };
    this.collegeAtlanticColorSchemes = new string[15]
    {
      "#98002E #BC9B6A #FFFFFF Boston",
      "#F56600 #522D80 #FFFFFF Clemson",
      "#003087 #FFFFFF #000000 Durham",
      "#782F40 #CEB888 #000000 Flordia",
      "#B3A369 #003057 #FFFFFF Georgia",
      "#AD0000 #000000 #FDB913 Louisville",
      "#F47321 #005030 #FFFFFF Miami",
      "#7BAFD4 #13294B #FFFFFF North Carolina",
      "#CC0000 #000000 #FFFFFF Raleigh",
      "#0C2340 #C99700 #FFFFFF Notre Dame",
      "#003594 #FFB81C #FFFFFF Pittsburgh",
      "#D44500 #022150 #FFFFFF Syracuse",
      "#232D4B #F84C1E #FFFFFF Virginia 1",
      "#630031 #CF4420 #FFFFFF Virginia 2",
      "#9E7E38 #000000 #FFFFFF Wake Forest"
    };
    this.collegeSouthEastColorSchemes = new string[14]
    {
      "#9E1B32 #828A8F #FFFFFF Alabama",
      "#9D2235 #000000 #FFFFFF Arkansas",
      "#0C2340 #E87722 #FFFFFF Auburn",
      "#0021A5 #FA4616 #FFFFFF Flordia",
      "#BA0C2F #000000 #FFFFFF Georgia",
      "#0033A0 #FFFFFF #000000 Kentucky",
      "#461D7C #FDD023 #FFFFFF Louisiana",
      "#CE1126 #14213D #FFFFFF Mississippi 1",
      "#660000 #CCCCCC #FFFFFF Mississippi 2",
      "#F1B82D #000000 #FFFFFF Missouri",
      "#73000A #000000 #FFFFFF South Carolina",
      "#FF8200 #58595B #FFFFFF Tennessee",
      "#500000 #FFFFFF #000000 Texas",
      "#866D4B #000000 #FFFFFF Nashville"
    };
    this.collegeMidWestColorSchemes = new string[14]
    {
      "#13294B #E84A27 #FFFFFF Illinois",
      "#990000 #EEEDEB #FFFFFF Indiana",
      "#FFCD00 #000000 #FFFFFF Iowa",
      "#CF102D #FFD520 #000000 Maryland",
      "#00274C #FFCB05 #FFFFFF Michigan 1",
      "#18453B #FFFFFF #000000 Michigan 2",
      "#7A0019 #FFCC33 #FFFFFF Minnesota",
      "#E41C38 #FFFFFF #000000 Nebraska",
      "#4E2A84 #FFFFFF #000000 Northwestern",
      "#BB0000 #666666 #FFFFFF Ohio",
      "#041E42 #FFFFFF #000000 Pennsylvania",
      "#CEB888 #000000 #FFFFFF Purdue",
      "#CC0033 #5F6A72 #000000 New Brunswick",
      "#C5050C #000000 #FFFFFF Wisconsin"
    };
    this.collegeCentralColorSchemes = new string[10]
    {
      "#C8102E #F1BE48 #FFFFFF Iowa",
      "#0051BA #E8000D #FFC82D Kansas 1",
      "#512888 #D1D1D1 #FFFFFF Kansas 2",
      "#841617 #FDF9D8 #FFFFFF Oklahoma 1",
      "#FF7300 #000000 #FFFFFF Oklahoma 2",
      "#003015 #FECB00 #FFFFFF Texas 1",
      "#BF5700 #333F48 #FFFFFF Texas 2",
      "#4D1979 #A3A9AC #FFFFFF Texas 3",
      "#CC0000 #000000 #FFFFFF Texas 4",
      "#002855 #EAAA00 #FFFFFF West Virginia"
    };
    this.collegePacificColorSchemes = new string[12]
    {
      "#CC0033 #003366 #FFFFFF Arizona 1",
      "#8C1D40 #FFC627 #FFFFFF Arizona 2",
      "#003262 #FDB515 #FFFFFF California",
      "#CFB87C #000000 #565A5C Colorado",
      "#154733 #FEE123 #FFFFFF Oregon 1",
      "#DC4405 #000000 #FFFFFF Oregon 2",
      "#8C1515 #4D4F53 #FFFFFF Stanford",
      "#2D68C4 #F2A900 #FFFFFF Los Angeles",
      "#990000 #FFC72C #FFFFFF Southern California",
      "#CC0000 #808080 #FFFFFF Utah",
      "#4B2E83 #B7A57A #FFFFFF Washington 1",
      "#981E32 #5E6A71 #FFFFFF Washington 2"
    };
    this.canadianColorSchemes = new string[9]
    {
      "#F15623 #000000 #FFFFFF British Columbia",
      "#B90000 #000000 #FFFFFF Calgary",
      "#2B5134 #FFB819 #FFFFFF Edmonton",
      "#FFB819 #000000 #FFFFFF Hamilton",
      "#8D1D23 #003871 #FFFFFF Montreal",
      "#AC1F2D #000000 #FFFFFF Ottawa",
      "#006241 #FFFFFF #000000 Saskatchewan",
      "#5F8FB1 #0D2240 #FFFFFF Toronto",
      "#002F87 #FFFFFF #000000 Winnipeg"
    };
    this.semiProLeague1ColorSchemes = new string[8]
    {
      "#154734 #FFB71B #FF4713 Arizona",
      "#470A68 #C5B783 #FFFFFF Atlanta",
      "#000000 #C1C6C8 #7C878E Birmingham",
      "#0C2340 #C8102E #FFFFFF Memphis",
      "#0C2340 #FE5000 #FF8200 Orlando",
      "#0033A0 #4698CB #C1C6C8 Salt Lake City",
      "#651D32 #C8102E #C7C8C6 San Antonio",
      "#FFC72C #333F48 #A2AAAD San Diego"
    };
    this.semiProLeague2ColorSchemes = new string[8]
    {
      "#5F9BCD #B31126 #000000 Dallas",
      "#C80000 #FFFFFF #000000 DC",
      "#B31428 #122047 #FFFFFF Houston",
      "#B50C23 #ED770E #FFFFFF Los Angeles",
      "#000000 #919191 #B31428 New York",
      "#122047 #037426 #E93B16 Seattle",
      "#273270 #919191 #FFFFFF St. Louis",
      "#037426 #FAA818 #05301D Tampa Bay"
    };
    this.countryColorSchemes = new string[33]
    {
      "#006233 #D21034 #FFFFFF Algeria",
      "#3C3B6E #B22234 #FFFFFF America",
      "#74ACDF #F6B40E #8B3C10 Argentina",
      "#00008B #FF0000 #FFFFFF Australia",
      "#000000 #FAE042 #ED2939 Belgium",
      "#009B3A #FEDF00 #002776 Brazil",
      "#FF0000 #FFFFFF #000000 Canada",
      "#0039A6 #FFFFFF #D52B1E Chile",
      "#FCD116 #003893 #CE1126 Colombia",
      "#C60C30 #FFFFFF #000000 Denmark",
      "#003580 #FFFFFF #000000 Finland",
      "#002395 #FFFFFF #ED2939 France",
      "#DD0000 #FFCE00 #000000 Germany",
      "#CD2A3E #436F4D #FFFFFF Hungary",
      "#239F40 #DA0000 #FFFFFF Iran",
      "#CE1126 #449D71 #FFFFFF Iraq",
      "#169B62 #FF883E #FF883E Ireland",
      "#0038B8 #FFFFFF #000000 Israel",
      "#009246 #CE2B37 #FFFFFF Italy",
      "#009B3A #FED100 #000000 Jamaica",
      "#BC002D #FFFFFF #000000 Japan",
      "#006847 #8F4620 #CE1126 Mexico",
      "#AE1C28 #21468B #FFFFFF Netherlands",
      "#00247D #CC132B #FFFFFF New Zealand",
      "#EF2B2D #002868 #FFFFFF Norway",
      "#006600 #FF0000 #FFFF00 Portugal",
      "#002B7F #FCD116 #CE1126 Romania",
      "#0039A6 #D52B1E #FFFFFF Russia",
      "#C60B1E #FFC400 #0039F0 Spain",
      "#006AA7 #FECC00 #FFFFFF Sweden",
      "#005BBB #FFD500 #FFFFFF Ukraine",
      "#00247D #CF142B #FFFFFF United Kingdom",
      "#FCD116 #0038A8 #FFFFFF Uruguay"
    };
    this.europeanColorSchemes = new string[17]
    {
      "#0C2340 #FF8200 #009A44 Amsterdam",
      "#154734 #BA0C2F #FFCD00 Barcelona 1",
      "#010101 #006341 #B9975B Berlin 1",
      "#A6192E #010101 #B1B3B3 Cologne 1",
      "#330072 #DC4405 #FFFFFF Frankfurt 1",
      "#0C2340 #006271 #862633 Hamburg 1",
      "#003087 #BA0C2F #89764B London",
      "#6F263D #010101 #CC8A00 Rhien",
      "#0C2340 #0032A0 #A2AAAD Scotland",
      "#184634 #f1d937 #8e292d Barcelona 2",
      "#d72229 #d22122 #FFFFFF Berlin 2",
      "#e52027 #080a09 #c4d0d6 Cologne 2",
      "#3e1152 #88714c #FFFFFF Frankfurt 2",
      "#6092ce #0e1728 #ed1b24 Hamburg 2",
      "#f7d403 #131315 #FFFFFF Leipzig",
      "#e30613 #d0d0d0 #1d1d1b Struttgart",
      "#008ac9 #000000 #b4b7ba Wroclaw"
    };
    this.leagueNames = new string[12]
    {
      "AXIS",
      "AMERICAN",
      "COLLEGE - ATLANTIC",
      "COLLEGE - SOUTHEAST",
      "COLLEGE - MIDWEST",
      "COLLEGE - CENTRAL",
      "COLLEGE - PACIFIC",
      "CANADIAN",
      "SEMI PRO LEAGUE 1",
      "SEMI PRO LEAGUE 2",
      "EUROPEAN LEAGUE",
      "OLYMPICS"
    };
    this.allColorSchemes = new List<string[]>();
    this.allColorSchemes.Add(this.axisColorSchemes);
    this.allColorSchemes.Add(this.americanColorSchemes);
    this.allColorSchemes.Add(this.collegeAtlanticColorSchemes);
    this.allColorSchemes.Add(this.collegeSouthEastColorSchemes);
    this.allColorSchemes.Add(this.collegeMidWestColorSchemes);
    this.allColorSchemes.Add(this.collegeCentralColorSchemes);
    this.allColorSchemes.Add(this.collegePacificColorSchemes);
    this.allColorSchemes.Add(this.canadianColorSchemes);
    this.allColorSchemes.Add(this.semiProLeague1ColorSchemes);
    this.allColorSchemes.Add(this.semiProLeague2ColorSchemes);
    this.allColorSchemes.Add(this.europeanColorSchemes);
    this.allColorSchemes.Add(this.countryColorSchemes);
  }

  private void ManageControllerInput()
  {
    if (!this.IsVisible() || PopupLoadingScreen.self.IsVisible() || OnScreenColorSelector.instance.IsVisible() || OnScreenKeyboard.instance.IsVisible() || !ControllerManagerTitle.self.usingController || !this.IsSelectColorSchemeWindowVisible())
      return;
    if (UserManager.instance.RightTriggerWasPressed(Player.One))
      this.SelectNextLeague();
    else if (UserManager.instance.LeftTriggerWasPressed(Player.One))
      this.SelectPreviousLeague();
    else if (UserManager.instance.RightBumperWasPressed(Player.One))
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
