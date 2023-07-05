// Decompiled with JetBrains decompiler
// Type: SeasonTablet
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System.Collections;
using UnityEngine;

public class SeasonTablet : MonoBehaviour
{
  [SerializeField]
  private TouchUI2DButton _rosterButton;
  [SerializeField]
  private TouchUI2DButton _statsButton;
  [SerializeField]
  private TouchUI2DButton _standingsButton;
  [SerializeField]
  private TouchUI2DButton _backButton;
  [SerializeField]
  private TouchUI2DButton _awardsRaceButton;
  [SerializeField]
  private GameObject[] _screens;

  private void Start()
  {
    this._rosterButton.onClick += new System.Action(this.ShowRoster);
    this._statsButton.onClick += new System.Action(this.ShowStats);
    this._standingsButton.onClick += new System.Action(this.ShowStandings);
    this._awardsRaceButton.onClick += new System.Action(this.ShowAwards);
    this._backButton.onClick += new System.Action(this.ShowMain);
    this.StartCoroutine(this.WaitToShowMain());
  }

  private IEnumerator WaitToShowMain()
  {
    yield return (object) new WaitForSeconds(0.1f);
    this.ShowMain();
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) this._rosterButton != (UnityEngine.Object) null)
      this._rosterButton.onClick -= new System.Action(this.ShowRoster);
    if ((UnityEngine.Object) this._statsButton != (UnityEngine.Object) null)
      this._statsButton.onClick -= new System.Action(this.ShowStats);
    if ((UnityEngine.Object) this._standingsButton != (UnityEngine.Object) null)
      this._standingsButton.onClick -= new System.Action(this.ShowStandings);
    if ((UnityEngine.Object) this._awardsRaceButton != (UnityEngine.Object) null)
      this._awardsRaceButton.onClick -= new System.Action(this.ShowAwards);
    if (!((UnityEngine.Object) this._backButton != (UnityEngine.Object) null))
      return;
    this._backButton.onClick -= new System.Action(this.ShowMain);
  }

  private void ShowRoster() => this.ShowScreen(SeasonTablet.Screens.Roster);

  private void ShowStats() => this.ShowScreen(SeasonTablet.Screens.Stats);

  private void ShowStandings() => this.ShowScreen(SeasonTablet.Screens.Standings);

  private void ShowMain() => this.ShowScreen(SeasonTablet.Screens.Main);

  private void ShowAwards() => this.ShowScreen(SeasonTablet.Screens.Awards);

  private void ShowScreen(SeasonTablet.Screens screen)
  {
    for (int index = 0; index < this._screens.Length; ++index)
      this._screens[index].SetActive(screen == (SeasonTablet.Screens) index);
    this._backButton.gameObject.SetActive(screen > SeasonTablet.Screens.Main);
  }

  private enum Screens
  {
    Main,
    Roster,
    Stats,
    Standings,
    Awards,
  }
}
