// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.PassGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using System;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/PassGameState")]
  public class PassGameState : GameState
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private GameplayDataStore _gameplayData;
    private PassChallenge _currentLevel;

    public event Action<PassChallenge> OnTrainingStarted;

    public event System.Action OnExitTraining;

    public override EAppState Id => EAppState.kPassGame;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      WorldState.CrowdEnabled.SetValue(true);
      AppEvents.Retry.OnTrigger += new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger += new System.Action(this.HandleContinue);
      this._throwManager.ForceAutoAim = true;
      this.LoadChallenge();
    }

    protected override void OnExitState()
    {
      WorldState.CrowdEnabled.SetValue(false);
      AppEvents.Retry.OnTrigger -= new System.Action(this.HandleRetry);
      AppEvents.Continue.OnTrigger -= new System.Action(this.HandleContinue);
      System.Action onExitTraining = this.OnExitTraining;
      if (onExitTraining != null)
        onExitTraining();
      this._throwManager.ForceAutoAim = false;
      UIDispatch.HideAll();
    }

    private void LoadChallenge()
    {
      string levelId = AppState.LevelId;
      Debug.Log((object) ("Running pass training " + levelId));
      PassChallenge passingChallenge = this._gameplayData.GetPassingChallenge(levelId);
      if (passingChallenge == null)
      {
        Debug.LogError((object) ("Couldn't find profile for " + levelId));
      }
      else
      {
        this._currentLevel = passingChallenge;
        Action<PassChallenge> onTrainingStarted = this.OnTrainingStarted;
        if (onTrainingStarted == null)
          return;
        onTrainingStarted(this._currentLevel);
      }
    }

    private void HandleRetry()
    {
      UIDispatch.HideAll();
      Action<PassChallenge> onTrainingStarted = this.OnTrainingStarted;
      if (onTrainingStarted == null)
        return;
      onTrainingStarted(this._currentLevel);
    }

    private void HandleContinue() => AppEvents.LoadMainMenu.Trigger();

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.SoloMinigame_PassingChallenge);
  }
}
