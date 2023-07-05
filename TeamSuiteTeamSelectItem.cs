// Decompiled with JetBrains decompiler
// Type: TeamSuiteTeamSelectItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UDB;
using UnityEngine;
using UnityEngine.UI;

public class TeamSuiteTeamSelectItem : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private UnityEngine.UI.Button selector_Btn;
  [SerializeField]
  private TextMeshProUGUI teamName_Txt;
  [SerializeField]
  private Image teamLogo_Img;
  private int teamIndex;

  public void SetTeamData(TeamData team)
  {
    this.teamIndex = team.TeamIndex;
    this.teamName_Txt.text = team.GetFullDisplayName();
    this.teamLogo_Img.sprite = team.GetMediumLogo();
    this.ShowWindow();
  }

  private void ShowWindow() => this.mainWindow_GO.SetActive(true);

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public UnityEngine.UI.Button GetButton() => this.selector_Btn;

  public void SelectTeam() => SingletonBehaviour<TitleScreenManager, MonoBehaviour>.instance.teamSuiteManager.SelectTeamForEditing(this.teamIndex);
}
