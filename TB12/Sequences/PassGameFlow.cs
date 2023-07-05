// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.PassGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12.Sequences
{
  public class PassGameFlow : MonoBehaviour
  {
    [SerializeField]
    private GameplayDataStore _gameplayData;
    [SerializeField]
    private PassGameState _state;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private PassGameScene _scene;
    [SerializeField]
    private ReceiversHighlighter _receiversHighlighter;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private HandsDataModel _handsDataModel;
    private PassChallenge _level;
    private bool _pickedUpBall;
    private bool _ballThrown;
    private bool _throwSuccess;
    private bool _timeElapsed;
    private bool _playbackFinished;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private const float _playerDistanceToLine = 3.5f;
    private LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      this._state.OnTrainingStarted += new Action<PassChallenge>(this.StartTraining);
      this._state.OnExitTraining += new System.Action(this.StopTraining);
      this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
      this._handsDataModel.OnBallPicked += new System.Action(this.HandlePickedUpBall);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._playbackInfo.OnPlaybackFinished.Link(new System.Action(this.HandlePlaybackFinished))
      });
    }

    private void OnDestroy()
    {
      this._state.OnTrainingStarted -= new Action<PassChallenge>(this.StartTraining);
      this._state.OnExitTraining -= new System.Action(this.StopTraining);
      this._throwManager.OnThrowProcessed -= new Action<ThrowData>(this.HandleThrowProcessed);
      this._handsDataModel.OnBallPicked -= new System.Action(this.HandlePickedUpBall);
      this._linksHandler.Clear();
    }

    private void StartTraining(PassChallenge level)
    {
      this._level = level;
      this._store.ResetStore();
      this._gameplayData.Initialize();
      this._routineHandle.Run(this.PassTrainingGameplay());
    }

    private void StopTraining()
    {
      this._routineHandle.Stop();
      GameplayUI.Hide();
    }

    private void HandlePlaybackFinished() => this._playbackFinished = true;

    private void HandleThrowProcessed(ThrowData throwData)
    {
      BallsContainerManager.CanSpawnBall.SetValue(false);
      this._ballThrown = true;
      this._throwSuccess = throwData.hasTarget;
      if (!throwData.hasTarget)
        return;
      EventData eventData = PassGameFlow.GenerateThrowEventData(throwData, this._playbackInfo);
      eventData.OnEventKeyMoment = (Action<object>) (x => this._scene.HideActiveBall());
      eventData.OnEventKeyMoment += (Action<object>) (x =>
      {
        Transform receiver = this._scene.Receivers[0];
        for (int index = 1; index < this._scene.Receivers.Count; ++index)
        {
          if ((double) Vector3.Distance(this._scene.Receivers[index].position, eventData.position) < (double) Vector3.Distance(receiver.position, eventData.position))
            receiver = this._scene.Receivers[index];
        }
        receiver.GetComponent<BehaviourController>().GoTo(0.5f, (Vector2) new Vector3(50f, 0.0f, 0.0f), 4.5f);
      });
      if (!throwData.closestTarget.ReceiveBall(eventData))
        return;
      ++this._store.PassSuccesses;
      this._routineHandle.RunAdditive(this.WaitAndHideBall(throwData.ball, throwData.flightTime));
    }

    public static EventData GenerateThrowEventData(ThrowData throwData, PlaybackInfo playbackInfo)
    {
      EventData throwEventData = new EventData();
      throwEventData.time = playbackInfo.PlayTime + throwData.timeToGetToTarget;
      throwEventData.position = new Vector3(throwData.targetPosition.x, Mathf.Clamp(throwData.targetPosition.y, 0.7f, 2.3f), throwData.targetPosition.z);
      Vector3 vector3 = throwData.startPosition - throwData.targetPosition;
      throwEventData.orientation = Vector2.SignedAngle(new Vector2(vector3.x, vector3.z), new Vector2(Vector3.forward.x, Vector3.forward.z));
      return throwEventData;
    }

    private IEnumerator WaitAndHideBall(BallObject ball, float delay)
    {
      yield return (object) new WaitForSeconds(delay);
      ball.gameObject.SetActive(false);
    }

    private void ResetState()
    {
      this._pickedUpBall = false;
      this._ballThrown = false;
      this._throwSuccess = false;
      this._playbackFinished = false;
      this._receiversHighlighter.Stop();
    }

    private IEnumerator PassTrainingGameplay()
    {
      PassGameFlow passGameFlow = this;
      GameplayUI.Hide();
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Utilities.YardsToGamePos(new Vector2((float) passGameFlow._level.yardLine - 3.5f, 26.675f)), Quaternion.Euler(0.0f, 90f, 0.0f));
      UIDispatch.FrontScreen.DisplayView(EScreens.kIntroduction);
      List<PassScenario> scenarios = passGameFlow._gameplayData.GetPassScenarios(passGameFlow._level.passSet);
      int scenarioCount = scenarios.Count;
      while (passGameFlow._store.PassAttempts < scenarioCount)
      {
        PassScenario passScenario = scenarios[passGameFlow._store.PassAttempts];
        passGameFlow.ResetState();
        List<ReceiverRoute> routes = passGameFlow._gameplayData.GetRoutes((IEnumerable<string>) passScenario.routes);
        passGameFlow._scene.InitializeScene(routes, passGameFlow._level.yardLine);
        passGameFlow._playbackInfo.StartPlayback();
        // ISSUE: reference to a compiler-generated method
        yield return (object) new WaitUntil(new Func<bool>(passGameFlow.\u003CPassTrainingGameplay\u003Eb__26_0));
        passGameFlow._playbackInfo.StopPlayback();
        // ISSUE: reference to a compiler-generated method
        yield return (object) new WaitUntil(new Func<bool>(passGameFlow.\u003CPassTrainingGameplay\u003Eb__26_1));
        GameplayUI.Hide();
        yield return (object) null;
        List<Transform> receivers = passGameFlow._scene.Receivers;
        // ISSUE: explicit non-virtual call
        if ((receivers != null ? (__nonvirtual (receivers.Count) > 0 ? 1 : 0) : 0) != 0)
          GameplayUI.PointTo(passGameFlow._scene.Receivers[0].transform, "Receiver");
        GameplayUI.Hide();
        yield return (object) new WaitForSeconds(0.2f);
        passGameFlow._playbackInfo.StartPlayback(reset: false);
        yield return (object) new WaitForSeconds(0.3f);
        passGameFlow._receiversHighlighter.HighlightReceivers(passGameFlow._scene.Avatars);
        while (!passGameFlow._ballThrown && !passGameFlow._playbackFinished)
          yield return (object) null;
        passGameFlow._receiversHighlighter.Stop();
        ++passGameFlow._store.PassAttempts;
        GameplayUI.Hide();
        GameplayUI.ShowPassStats(passGameFlow._store.PassSuccesses, passGameFlow._store.PassAttempts);
        if (!passGameFlow._playbackFinished)
          yield return (object) new WaitForSeconds(1f);
        foreach (FootballVR.Avatar avatar in (IEnumerable<FootballVR.Avatar>) passGameFlow._scene.Avatars)
          avatar.Disappear();
        yield return (object) new WaitForSeconds(1f);
        if (passGameFlow._throwSuccess)
        {
          yield return (object) passGameFlow._routineHandle.RunAdditive(AppSounds.WinSequence());
          // ISSUE: reference to a compiler-generated method
          yield return (object) new WaitUntil(new Func<bool>(passGameFlow.\u003CPassTrainingGameplay\u003Eb__26_2));
        }
        else
        {
          yield return (object) new WaitForSeconds(0.7f);
          yield return (object) passGameFlow._routineHandle.RunAdditive(AppSounds.FailSequence());
        }
        passGameFlow._scene.CleanupScene();
      }
      AppEvents.ChallengeComplete.Trigger((int) passGameFlow._store.Score);
      UIDispatch.FrontScreen.DisplayView(EScreens.kCompletedSuccessfully);
    }

    private void HandlePickedUpBall() => this._pickedUpBall = true;
  }
}
