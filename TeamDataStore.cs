// Decompiled with JetBrains decompiler
// Type: TeamDataStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using TB12;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ProEra/Stores/TeamDataStore", fileName = "TeamDataStore")]
[AppStore]
public class TeamDataStore : ScriptableObject
{
  public string TeamName;
  public Sprite Logo;
  public ETeamTypes TeamType = ETeamTypes.kPass;
  public PlayersToWatchData[] PlayersToWatch;
  public TeamSeasonData teamSeasonData;
  public string[] KeysToGame;
  public int OffensivePower = 85;
  public int DefensivePower = 85;
  public int TeamDataIndex = 1;
  public SceneAssetString stadiumScene;

  public TeamDataStore()
  {
    if (SaveManager.bIsInitialized)
      this.LoadTeamSeasonData();
    else
      SaveManager.OnInitialized += new UnityAction(this.LoadTeamSeasonData);
  }

  private void LoadTeamSeasonData()
  {
    this.teamSeasonData = new TeamSeasonData(this.TeamName);
    PersistentSingleton<SaveManager>.Instance.AddToLoadQueue((ISaveSync) this.teamSeasonData);
  }

  public void AddWin()
  {
    ++this.teamSeasonData.win;
    this.SaveTeamSeasonData();
  }

  public void AddLoss()
  {
    ++this.teamSeasonData.loss;
    this.SaveTeamSeasonData();
  }

  public void AddTie()
  {
    ++this.teamSeasonData.tie;
    this.SaveTeamSeasonData();
  }

  public string GetTeamRecord() => "(" + this.teamSeasonData.win.ToString() + " - " + this.teamSeasonData.loss.ToString() + " - " + this.teamSeasonData.tie.ToString() + ")";

  public void ClearSeasonData()
  {
    this.teamSeasonData = new TeamSeasonData(this.TeamName);
    this.SaveTeamSeasonData();
  }

  private void SaveTeamSeasonData()
  {
    if (!PersistentSingleton<SaveManager>.Exist())
      return;
    PersistentSingleton<SaveManager>.Instance.AddToSaveQueue((ISaveSync) this.teamSeasonData);
  }
}
