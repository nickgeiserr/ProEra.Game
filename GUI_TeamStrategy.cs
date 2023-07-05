// Decompiled with JetBrains decompiler
// Type: GUI_TeamStrategy
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class GUI_TeamStrategy : MonoBehaviour
{
  [SerializeField]
  private GameObject mainWindow_GO;
  [SerializeField]
  private TextMeshProUGUI strategyTitle_Txt;
  [SerializeField]
  private TextMeshProUGUI strategyDescription_Txt;
  private TeamStrategyManager teamStrategyManager;
  private bool isOffense;
  private int currentStrategyIndex;
  private string[] strategyNames;
  private string[] strategyDescriptions;

  public void Init(bool offensiveWindow)
  {
    this.isOffense = offensiveWindow;
    this.currentStrategyIndex = 0;
    this.teamStrategyManager = new TeamStrategyManager();
    if (this.isOffense)
    {
      this.strategyNames = TeamStrategyManager.offensiveStrategyNames;
      this.strategyDescriptions = TeamStrategyManager.offensiveStrategyDescriptions;
    }
    else
    {
      this.strategyNames = TeamStrategyManager.defensiveStrategyNames;
      this.strategyDescriptions = TeamStrategyManager.defensiveStrategyDescriptions;
    }
    this.strategyTitle_Txt.text = this.strategyNames[this.currentStrategyIndex];
    this.strategyDescription_Txt.text = this.strategyDescriptions[this.currentStrategyIndex];
  }

  public void ToggleWindow()
  {
    if (this.IsVisible())
      this.HideWindow();
    else
      this.ShowWindow();
  }

  public void HideWindow() => this.mainWindow_GO.SetActive(false);

  public void ShowWindow() => this.mainWindow_GO.SetActive(true);

  public bool IsVisible() => this.mainWindow_GO.activeInHierarchy;

  public void ShowNextStrategy()
  {
    this.currentStrategyIndex = this.currentStrategyIndex + 1 == this.strategyNames.Length ? 0 : this.currentStrategyIndex + 1;
    this.strategyTitle_Txt.text = this.strategyNames[this.currentStrategyIndex];
    this.strategyDescription_Txt.text = this.strategyDescriptions[this.currentStrategyIndex];
  }

  public void ShowPreviousStrategy()
  {
    this.currentStrategyIndex = this.currentStrategyIndex - 1 < 0 ? this.strategyNames.Length - 1 : this.currentStrategyIndex - 1;
    this.strategyTitle_Txt.text = this.strategyNames[this.currentStrategyIndex];
    this.strategyDescription_Txt.text = this.strategyDescriptions[this.currentStrategyIndex];
  }

  public void ApplyCurrentStrategy()
  {
    if (this.isOffense)
      this.teamStrategyManager.SetOffensiveStrategy(this.currentStrategyIndex);
    else
      this.teamStrategyManager.SetDefensiveStrategy(this.currentStrategyIndex);
    this.HideWindow();
    MatchManager.instance.AllowSnap();
  }
}
