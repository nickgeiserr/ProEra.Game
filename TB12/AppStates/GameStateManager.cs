// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.GameStateManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.StateManagement;
using System;
using System.Collections;
using UnityEngine;

namespace TB12.AppStates
{
  public class GameStateManager : StateManager<EAppState, GameState>
  {
    [SerializeField]
    private StadiumConfigsStore _stadiumStore;

    public override bool TryGetEnvironmentScene(
      string sceneId,
      GameState state,
      out SceneAssetString scene)
    {
      if (string.IsNullOrEmpty(sceneId))
      {
        ref SceneAssetString local = ref scene;
        SceneGroupBundle sceneBundle = state.SceneBundle;
        SceneAssetString sceneAssetString = sceneBundle != null ? sceneBundle.EnvironmentScene : new SceneAssetString();
        local = sceneAssetString;
        return scene.IsValid();
      }
      scene = new SceneAssetString();
      return false;
    }

    public void GetStadiumObject(
      string sceneId,
      ETimeOfDay timeOfDay,
      Action<StadiumConfigStore> callback)
    {
      this._stadiumStore.GetConfiguration(sceneId, timeOfDay, callback);
    }

    public SceneAssetString GetTimeOfDayScene(ETimeOfDay value) => this._stadiumStore.GetTimeOfDayScene(value);

    protected override IEnumerator OnNewStateLoad()
    {
      GameStateManager gameStateManager = this;
      PersistentSingleton<GamePlayerController>.Instance.OnStateChange(gameStateManager.activeState.Id == EAppState.kMainMenu || gameStateManager.activeState.Id == EAppState.kMainMenuActivation);
      if (gameStateManager.activeState.Id == EAppState.kMultiplayerLobby || gameStateManager.activeState.Id == EAppState.kMultiplayerBossModeGame || gameStateManager.activeState.Id == EAppState.kMultiplayerDodgeball || gameStateManager.activeState.Id == EAppState.kMultiplayerThrowGame)
        yield return (object) PersistentSingleton<LevelManager>.Instance.LoadEnvironment(gameStateManager.GetTimeOfDayScene(WorldState.TimeOfDay.Value));
    }
  }
}
