// Decompiled with JetBrains decompiler
// Type: TB12.Backend.GameDataUpdater
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.Backend
{
  [CreateAssetMenu(menuName = "TB12/Managers/GameDataUpdater")]
  public class GameDataUpdater : ScriptableObject
  {
    [SerializeField]
    private GameplayDataStore _gameplayDataStore;
    private CancellationTokenSource _cancellationTokenSrc = new CancellationTokenSource();
    private float _lastUpdateStarted = -100f;
    private static bool debug;

    private void OnEnable() => this._lastUpdateStarted = -100f;

    public async Task CheckForUpdate()
    {
      if (GameDataUpdater.debug)
        Debug.Log((object) "Checking for update");
      if ((double) Time.time - (double) this._lastUpdateStarted < 5.0)
      {
        if (!GameDataUpdater.debug)
          return;
        Debug.LogError((object) "Recent update happened.. skipping.");
      }
      else
      {
        this._lastUpdateStarted = Time.time;
        if (await GameDataBackend.GetLatestTimestamp(this._cancellationTokenSrc.Token) <= this._gameplayDataStore.timeStamp && !((UnityEngine.Object) this._gameplayDataStore.GameplayLevels == (UnityEngine.Object) null))
          return;
        Debug.Log((object) "Loading gameplayData from backend.");
        GameDataBackend.JsonEntry latestGameplayData = await GameDataBackend.GetLatestGameplayData(this._cancellationTokenSrc.Token);
        if (latestGameplayData == null)
          return;
        try
        {
          GameplayLevels instance = ScriptableObject.CreateInstance<GameplayLevels>();
          JsonUtility.FromJsonOverwrite(latestGameplayData.data, (object) instance);
          this._gameplayDataStore.UpdateGameplayData(instance, latestGameplayData.timestamp);
          if (BuildSettings.DevelopmentBuild)
            GameplayUI.ShowText("Game data updated.");
          Debug.Log((object) "GameplayData synced with backend");
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ex.Message);
        }
      }
    }

    private async Task AutoUpdateAsync(CancellationToken cancellationToken)
    {
      try
      {
        while (!cancellationToken.IsCancellationRequested)
        {
          await this.CheckForUpdate();
          await Task.Delay(8000, cancellationToken);
        }
      }
      catch (TaskCanceledException ex)
      {
        if (!GameDataUpdater.debug)
          return;
        Debug.Log((object) "AutoUpdate Stopped.");
      }
    }

    public async void EnableAutoUpdate()
    {
      if (!BuildSettings.DevelopmentBuild)
        return;
      this.StopAutoUpdate();
      await this.AutoUpdateAsync(this._cancellationTokenSrc.Token);
    }

    public void StopAutoUpdate()
    {
      this._cancellationTokenSrc.Cancel();
      this._cancellationTokenSrc = new CancellationTokenSource();
    }
  }
}
