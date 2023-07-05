// Decompiled with JetBrains decompiler
// Type: GUI_InjuryPopup
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI_InjuryPopup : MonoBehaviour, IInjuryPopup
{
  [Header("Injured Player Popup")]
  [SerializeField]
  private GameObject injuredPlayerPopup_GO;
  [SerializeField]
  private Image injuredPlayerLogo_Img;
  [SerializeField]
  private Image injuredPlayerBackground_Img;
  [SerializeField]
  private TextMeshProUGUI injuredPlayerPosAndNumber_Txt;
  [SerializeField]
  private TextMeshProUGUI injuredPlayerName_Txt;
  [SerializeField]
  private TextMeshProUGUI injuredPlayerInjury_Txt;
  [SerializeField]
  private TextMeshProUGUI injuredExpectedReturn_Txt;
  [Header("Returning Player Popup")]
  [SerializeField]
  private GameObject returningPlayerPopup_GO;
  [SerializeField]
  private Image returningPlayerLogo_Img;
  [SerializeField]
  private Image returningPlayerBackground_Img;
  [SerializeField]
  private TextMeshProUGUI returningPlayerPosAndNumber_Txt;
  [SerializeField]
  private TextMeshProUGUI returningPlayerName_Txt;
  private WaitForSeconds showWindow_WFS = new WaitForSeconds(2f);
  private WaitForSeconds hideWindow_WFS = new WaitForSeconds(4f);

  private void Awake() => ProEra.Game.Sources.UI.InjuryPopup = (IInjuryPopup) this;

  public void ShowInjuredPlayer(PlayerData player, bool isUserTeam)
  {
    this.injuredPlayerLogo_Img.sprite = TeamState.GetSmallLogo(isUserTeam);
    this.injuredPlayerBackground_Img.color = TeamState.GetBackgroundColor(isUserTeam);
    this.injuredPlayerPosAndNumber_Txt.text = player.PlayerPosition.ToString() + " #" + player.Number.ToString();
    this.injuredPlayerName_Txt.text = player.FirstInitalAndLastName;
    this.injuredPlayerInjury_Txt.text = player.CurrentInjury.injuryCategory;
    this.injuredExpectedReturn_Txt.text = player.CurrentInjury.weeksRemaining > 0 ? "NOT EXPECTED TO RETURN" : "EXPECTED TO RETURN";
    this.StartCoroutine(this.ShowInjuryWindow());
    this.StartCoroutine(this.HideInjuredPlayerPopup());
  }

  public void ShowReturningPlayer(PlayerData player, bool isUserTeam)
  {
    this.returningPlayerLogo_Img.sprite = TeamState.GetSmallLogo(isUserTeam);
    this.returningPlayerBackground_Img.color = TeamState.GetBackgroundColor(isUserTeam);
    this.returningPlayerPosAndNumber_Txt.text = player.PlayerPosition.ToString() + " #" + player.Number.ToString();
    this.returningPlayerName_Txt.text = player.FirstInitalAndLastName;
    this.StartCoroutine(this.ShowReturningPlayerWindow());
    this.StartCoroutine(this.HideReturningPlayerPopup());
  }

  private IEnumerator ShowInjuryWindow()
  {
    yield return (object) this.showWindow_WFS;
    this.injuredPlayerPopup_GO.SetActive(true);
  }

  private IEnumerator ShowReturningPlayerWindow()
  {
    yield return (object) this.showWindow_WFS;
    this.returningPlayerPopup_GO.SetActive(true);
  }

  private IEnumerator HideReturningPlayerPopup()
  {
    yield return (object) this.hideWindow_WFS;
    yield return (object) this.hideWindow_WFS;
    this.returningPlayerPopup_GO.SetActive(false);
  }

  private IEnumerator HideInjuredPlayerPopup()
  {
    yield return (object) this.hideWindow_WFS;
    yield return (object) this.hideWindow_WFS;
    this.injuredPlayerPopup_GO.SetActive(false);
  }
}
