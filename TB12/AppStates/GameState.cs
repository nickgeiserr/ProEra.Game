// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.GameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using FootballVR;
using Framework;
using UnityEngine;
using Vars;

namespace TB12.AppStates
{
  public abstract class GameState : BaseState<EAppState>
  {
    public readonly AppEvent OnEnterStateFinished = new AppEvent();
    [SerializeField]
    private Rect bounds = new Rect(-24.4f, -55f, 53f, 114f);
    private Rect _savedBounds;
    private float _stateEnterTime;

    public virtual bool allowRetry => true;

    protected override void OnEnterState()
    {
      this.TriggerGameStartAnalytics();
      this.UpdateBounds();
      this.UpdateGroupPresence();
      VRState.HandsVisible.SetValue(true);
    }

    protected override void OnExitState() => this.TriggerGameEndAnalytics();

    public void UpdateBounds()
    {
      this._savedBounds = this.bounds;
      PersistentSingleton<GamePlayerController>.Instance.SetNewBoundingRect(this.bounds);
    }

    protected virtual void UpdateGroupPresence()
    {
    }

    private void TriggerGameStartAnalytics()
    {
      this._stateEnterTime = Time.time;
      EAppState id = this.Id;
      if (id <= EAppState.kPassGame)
      {
        if (id <= EAppState.kChangeGear)
        {
          if (id != EAppState.kUnknown && id != EAppState.kChangeGear)
            return;
        }
        else
        {
          if (id != EAppState.kLobby)
            return;
          AnalyticEvents.Record<LobbyEnteredArgs>(new LobbyEnteredArgs());
          return;
        }
      }
      else if (id <= EAppState.kMainMenu)
      {
        if (id != EAppState.kMainMenuActivation)
          return;
      }
      else
      {
        switch (id - 3877816049U)
        {
          case EAppState.kUnknown:
          case (EAppState) 1:
          case (EAppState) 2:
          case (EAppState) 5:
          case (EAppState) 13:
          case (EAppState) 14:
          case (EAppState) 18:
          case (EAppState) 22:
            break;
          case (EAppState) 3:
            AnalyticEvents.Record<ThrowingGameStartedArgs>(new ThrowingGameStartedArgs());
            return;
          case (EAppState) 4:
            AnalyticEvents.Record<BossModeGameStartedArgs>(new BossModeGameStartedArgs());
            return;
          case (EAppState) 6:
            return;
          case (EAppState) 7:
            AnalyticEvents.Record<PracticeModeEnteredArgs>(new PracticeModeEnteredArgs());
            return;
          case (EAppState) 8:
            AnalyticEvents.Record<LockerRoomEnteredArgs>(new LockerRoomEnteredArgs());
            return;
          case (EAppState) 9:
            return;
          case (EAppState) 10:
            AnalyticEvents.Record<DodgeBallGameStartedArgs>(new DodgeBallGameStartedArgs());
            return;
          case (EAppState) 11:
            return;
          case (EAppState) 12:
            AnalyticEvents.Record<LobbyEnteredArgs>(new LobbyEnteredArgs());
            return;
          case (EAppState) 15:
            AnalyticEvents.Record<PocketPassingStartedArgs>(new PocketPassingStartedArgs());
            return;
          case (EAppState) 16:
            AnalyticEvents.Record<DimeDroppingStartedArgs>(new DimeDroppingStartedArgs());
            return;
          case (EAppState) 17:
            AnalyticEvents.Record<RunAndShootStartedArgs>(new RunAndShootStartedArgs());
            return;
          case (EAppState) 19:
            return;
          case (EAppState) 20:
            AnalyticEvents.Record<RollOutEventStartedArgs>(new RollOutEventStartedArgs());
            return;
          case (EAppState) 21:
            AnalyticEvents.Record<TwoMinuteDrillGameStartedArgs>(new TwoMinuteDrillGameStartedArgs());
            return;
          case (EAppState) 23:
          case (EAppState) 24:
            AnalyticEvents.Record<HeroMomentStartedArgs>(new HeroMomentStartedArgs());
            return;
          default:
            if (id != EAppState.kThrowGame)
              return;
            AnalyticEvents.Record<ThrowingGameStartedArgs>(new ThrowingGameStartedArgs());
            return;
        }
      }
      AnalyticEvents.Record<FtueStartedArgs>(new FtueStartedArgs());
    }

    private void TriggerGameEndAnalytics()
    {
      float timeSpentInScene = Time.time - this._stateEnterTime;
      EAppState id = this.Id;
      if (id <= EAppState.kPassGame)
      {
        if (id <= EAppState.kChangeGear)
        {
          if (id == EAppState.kUnknown)
            ;
        }
        else if (id == EAppState.kLobby)
          AnalyticEvents.Record<LobbyExitedArgs>(new LobbyExitedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
      }
      else if (id <= EAppState.kHeroMoment)
      {
        if (id - 3778609171U <= (EAppState) 1)
          return;
        switch (id - 3877816049U)
        {
          case (EAppState) 3:
            AnalyticEvents.Record<ThrowingGameCompletedArgs>(new ThrowingGameCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 4:
            AnalyticEvents.Record<BossModeGameCompletedArgs>(new BossModeGameCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 7:
            AnalyticEvents.Record<PracticeModeExitedArgs>(new PracticeModeExitedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 8:
            AnalyticEvents.Record<LockerRoomExitedArgs>(new LockerRoomExitedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 10:
            AnalyticEvents.Record<DodgeBallGameCompletedArgs>(new DodgeBallGameCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 12:
            AnalyticEvents.Record<LobbyExitedArgs>(new LobbyExitedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 15:
            AnalyticEvents.Record<PocketPassingCompletedArgs>(new PocketPassingCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 16:
            AnalyticEvents.Record<DimeDroppingCompletedArgs>(new DimeDroppingCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 17:
            AnalyticEvents.Record<RunAndShootCompletedArgs>(new RunAndShootCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 20:
            AnalyticEvents.Record<RollOutEventCompletedArgs>(new RollOutEventCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
          case (EAppState) 21:
            AnalyticEvents.Record<TwoMinuteDrillGameCompletedArgs>(new TwoMinuteDrillGameCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
            break;
        }
      }
      else if (id == EAppState.kThrowGame)
        AnalyticEvents.Record<ThrowingGameCompletedArgs>(new ThrowingGameCompletedArgs(timeSpentInScene, ArmSwingLocomotion.LocomotionTimeSinceSceneLoad, ThumbstickLocomotion.LocomotionTimeSinceSceneLoad));
    }
  }
}
