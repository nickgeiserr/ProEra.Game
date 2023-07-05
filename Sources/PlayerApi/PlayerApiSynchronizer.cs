// Decompiled with JetBrains decompiler
// Type: Sources.PlayerApi.PlayerApiSynchronizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game.Achievements;
using ProEra.Web;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Sources.PlayerApi
{
  public class PlayerApiSynchronizer : MonoBehaviour
  {
    private AchievementData _achievementData => SaveManager.GetAchievementData();

    private void Awake() => this.ValidateInspectorBinding();

    private void OnEnable()
    {
      ProEra.Web.PlayerApi.LoginSuccess += new Action<SaveKeycloakUserData>(this.SyncAllScores);
      ProEra.Web.PlayerApi.SyncProEraScore += new System.Action(this.PlayerApi_SyncProEraScore);
      this.SyncAllScores();
    }

    private void PlayerApi_SyncProEraScore() => this.SyncProEraScore();

    private void OnDisable()
    {
      ProEra.Web.PlayerApi.LoginSuccess -= new Action<SaveKeycloakUserData>(this.SyncAllScores);
      ProEra.Web.PlayerApi.SyncProEraScore -= new System.Action(this.PlayerApi_SyncProEraScore);
    }

    private void SyncAllScores(SaveKeycloakUserData _ = null)
    {
      this.SyncProEraScore();
      this.SyncTwoMinuteDrillHighScore();
    }

    private void SyncProEraScore()
    {
      Debug.Log((object) "Syncing pro era score...");
      PersistentSingleton<ProEra.Web.PlayerApi>.Instance.PutHighScore(Definitions.HighScore.ProEra, new ProEraScoreModel()
      {
        DataSources = {
          [ProEraScoreModel.DataSource.Achievements] = new ProEraScoreModel.WeightedData((float) this._achievementData.AchievementScore),
          [ProEraScoreModel.DataSource.TwoMinuteDrill] = new ProEraScoreModel.WeightedData(PersistentSingleton<SaveManager>.Instance.twoMinuteDrill.BestScore)
        }
      }.Value, (Action<UnityWebRequest.Result>) (_ => Debug.Log((object) "Syncing pro era score finished")));
    }

    private void SyncTwoMinuteDrillHighScore() => PersistentSingleton<ProEra.Web.PlayerApi>.Instance.PutHighScore(Definitions.HighScore.TwoMinuteDrill, PersistentSingleton<SaveManager>.Instance.twoMinuteDrill.BestScore, (Action<UnityWebRequest.Result>) null);

    private void ValidateInspectorBinding()
    {
    }
  }
}
