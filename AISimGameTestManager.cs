// Decompiled with JetBrains decompiler
// Type: AISimGameTestManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using TB12;
using UDB;
using UnityEngine;

public class AISimGameTestManager : SingletonBehaviour<AISimGameTestManager, MonoBehaviour>
{
  [SerializeField]
  private AISimGameMatchupStore matchupstore;
  private int currentMatchUp;

  public int TestGameQuarterLen => this.matchupstore.quarterLenth;

  public float TestGameTimeScale => this.matchupstore.timeScale;

  public string SaveFilePath => this.matchupstore.statsFileSavePath;

  private void Start()
  {
  }

  public void SetNextMatchup()
  {
    PersistentSingleton<SaveManager>.Instance.gameSettings.ExportGameStatsToFile = true;
    if (this.currentMatchUp >= this.matchupstore.matchups.Count)
      return;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startFromGameScene_UserTeamIndex = this.matchupstore.matchups[this.currentMatchUp].homeTeam;
    SingletonBehaviour<PersistentData, MonoBehaviour>.instance.startFromGameScene_CompTeamIndex = this.matchupstore.matchups[this.currentMatchUp].awayTeam;
    ++this.currentMatchUp;
  }
}
