// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.AgilityGameStateV1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/old/AgilityGameStateV1")]
  public class AgilityGameStateV1 : GameState
  {
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private GameplayDataStore _gameplayData;
    private readonly RoutineHandle _retryRoutine = new RoutineHandle();
    private AgilityChallengeV1 _agilityChallenge;

    public event Action<AgilityChallengeV1> OnTrainingStarted;

    public event System.Action OnExitTraining;

    public override EAppState Id => EAppState.kAgilityGame;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      WorldState.CrowdEnabled.SetValue(true);
      string levelId = AppState.LevelId;
      Debug.Log((object) ("Running collision experience " + levelId));
      this._agilityChallenge = this._gameplayData.GetAgilityChallengeV1(levelId);
      if (this._agilityChallenge == null)
      {
        Debug.LogError((object) ("Couldn't find profile for " + levelId));
      }
      else
      {
        PersistentSingleton<GamePlayerController>.Instance.ResetPosition();
        AppState.GameInfoUI.SetValue(true);
        Action<AgilityChallengeV1> onTrainingStarted = this.OnTrainingStarted;
        if (onTrainingStarted != null)
          onTrainingStarted(this._agilityChallenge);
        AppEvents.Retry.OnTrigger += new System.Action(this.HandleRetry);
      }
    }

    public override void WillExit()
    {
      System.Action onExitTraining = this.OnExitTraining;
      if (onExitTraining == null)
        return;
      onExitTraining();
    }

    protected override void OnExitState()
    {
      UIDispatch.HideAll();
      WorldState.CrowdEnabled.SetValue(false);
      AppState.GameInfoUI.SetValue(false);
      this._store.ResetStore();
      AppEvents.Retry.OnTrigger -= new System.Action(this.HandleRetry);
    }

    private void HandleRetry() => this._retryRoutine.Run(this.RetryRoutine());

    private IEnumerator RetryRoutine()
    {
      UIDispatch.HideAll();
      yield return (object) GamePlayerController.CameraFade.Fade();
      PersistentSingleton<GamePlayerController>.Instance.ResetPosition();
      Action<AgilityChallengeV1> onTrainingStarted = this.OnTrainingStarted;
      if (onTrainingStarted != null)
        onTrainingStarted(this._agilityChallenge);
      yield return (object) GamePlayerController.CameraFade.Clear();
    }

    public override void ClearState() => this._store.ResetStore();

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinigame_AgilityDrill);
  }
}
