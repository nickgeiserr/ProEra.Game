// Decompiled with JetBrains decompiler
// Type: TB12.ReceiverGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.AvatarSystem;
using FootballVR.Sequences;
using Framework;
using System;
using System.Collections;
using TB12.Sequences;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class ReceiverGameFlow : MonoBehaviour
  {
    [SerializeField]
    private PlayModeStore _store;
    [SerializeField]
    private ReceiverGameState _state;
    [SerializeField]
    private ReceiverGameScene _scene;
    [SerializeField]
    private AvatarsManager _avatarsManager;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private HandsDataModel _handsData;
    private readonly RoutineHandle _flowRoutine = new RoutineHandle();
    private readonly QbHikeSequence _hikeSequence = new QbHikeSequence();
    private readonly FollowRouteSequence _followRouteSequence = new FollowRouteSequence();
    private const float throwDuration = 0.6f;
    private float debugThrowStartTime;

    private ReceiverModeSettings _settings => ScriptableSingleton<ReceiverModeSettings>.Instance;

    private void Awake()
    {
      this._state.OnGameplayStarted += new System.Action(this.StartGameplay);
      this._state.OnExit += new System.Action(this.HandleExit);
    }

    private void OnDestroy()
    {
      this._state.OnGameplayStarted -= new System.Action(this.StartGameplay);
      this._state.OnExit -= new System.Action(this.HandleExit);
    }

    private void StartGameplay()
    {
      this.StopGameplay();
      this._avatarsManager.Qb.GetComponent<BallThrower>().OnBallThrown += new Action<Transform, Vector3, float>(this.HandleBallThrown);
      this._flowRoutine.Run(this.FlowRoutine());
    }

    private IEnumerator FlowRoutine()
    {
      ReceiverGameFlow receiverGameFlow = this;
      UIDispatch.HideAll();
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(receiverGameFlow._avatarsManager.GetUserPlayerPosition(), Quaternion.Euler(0.0f, 90f, 0.0f));
      yield return (object) new WaitForSeconds(1f);
      receiverGameFlow._scene.GameBall.gameObject.SetActive(true);
      receiverGameFlow._avatarsManager.Center.behaviourController.GrabBall(receiverGameFlow._scene.GameBall.transform);
      receiverGameFlow._scene.GameBall.Pick();
      receiverGameFlow._scene.DisplayUserRoute();
      yield return (object) new WaitForSeconds(1f);
      UIDispatch.FrontScreen.DisplayView(EScreens.kIntroduction);
      yield return (object) receiverGameFlow._flowRoutine.RunAdditive(ControllerInput.WaitDoubleTrigger());
      UIDispatch.HideAll();
      receiverGameFlow._playbackInfo.StartPlayback(reset: false);
      VRState.LocomotionEnabled.Value = true;
      receiverGameFlow._scene.UserRoute.Highlight();
      receiverGameFlow._hikeSequence.RunHikeSequence(receiverGameFlow._avatarsManager.Center, receiverGameFlow._avatarsManager.Qb, receiverGameFlow._scene.GameBall);
      receiverGameFlow._followRouteSequence.RunSequence(receiverGameFlow._scene.UserRoute, receiverGameFlow._settings.completionOffset);
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(receiverGameFlow.\u003CFlowRoutine\u003Eb__15_0));
      if ((double) receiverGameFlow._followRouteSequence.progress <= (double) receiverGameFlow._settings.progressThreshold)
      {
        receiverGameFlow._flowRoutine.RunAdditive(receiverGameFlow.HandleResult(EPlayResult.kRunTooSlow));
      }
      else
      {
        float normalizedPosition = receiverGameFlow._followRouteSequence.progress + receiverGameFlow._followRouteSequence.progressPerSecond * (receiverGameFlow._settings.ballFlightTime + 0.6f) * receiverGameFlow._settings.throwProjectionCoef;
        Vector3 pointOnRoute = receiverGameFlow._scene.UserRoute.GetPointOnRoute(normalizedPosition);
        if (receiverGameFlow._settings.useRouteTipAsProjectedPosition)
        {
          receiverGameFlow._followRouteSequence.SetEndPos(pointOnRoute);
          receiverGameFlow._scene.UserRoute.SetTipPosition(pointOnRoute);
        }
        BallThrower component = receiverGameFlow._avatarsManager.Qb.GetComponent<BallThrower>();
        receiverGameFlow._scene.GameBall.transform.position = component.position.SetY(1.65f);
        receiverGameFlow._scene.GameBall.transform.LookAt(PersistentSingleton<PlayerCamera>.Instance.Position);
        component.ThrowToSpot(pointOnRoute.SetY(receiverGameFlow._settings.catchHeight), receiverGameFlow._settings.ballFlightTime, 0.6f);
        receiverGameFlow.debugThrowStartTime = Time.time;
        yield return (object) new WaitForSeconds(receiverGameFlow._settings.warningDelay);
        GameplayUI.ShowText("INCOMING!");
      }
    }

    private void StopGameplay()
    {
      this._flowRoutine.Stop();
      VRState.LocomotionEnabled.Value = false;
      this._hikeSequence.Stop();
      this._followRouteSequence.Stop();
      this._handsData.ResetHandsState();
      this._store.Clear();
      this._scene.Cleanup();
    }

    private IEnumerator HandleResult(EPlayResult result)
    {
      switch (result)
      {
        case EPlayResult.kPassMissed:
        case EPlayResult.kRunTooSlow:
          yield return (object) new WaitForSeconds(0.7f);
          yield return (object) this._flowRoutine.RunAdditive(AppSounds.FailSequence());
          yield return (object) new WaitForSeconds(1f);
          this._store.Result = result;
          UIDispatch.FrontScreen.DisplayView(EScreens.kPlayFailed);
          break;
        case EPlayResult.kPassCaught:
          yield return (object) new WaitForSeconds(1f);
          yield return (object) this._flowRoutine.RunAdditive(AppSounds.WinSequence());
          UIDispatch.FrontScreen.DisplayView(EScreens.kThrowSuccess);
          break;
      }
      this._avatarsManager.HideAllAvatars();
    }

    private void HandleExit() => this.StopGameplay();

    private void HandleBallThrown(Transform shootPoint, Vector3 targetPos, float flightTime)
    {
      if ((double) shootPoint.position.y < 0.20000000298023224)
        Debug.LogError((object) "WTF");
      this._store.ballFlightTime = flightTime;
      Debug.Log((object) string.Format("Ball thrown in {0} seconds", (object) (float) ((double) Time.time - (double) this.debugThrowStartTime)));
      BallObject gameBall = this._scene.GameBall;
      gameBall.gameObject.SetActive(true);
      gameBall.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
      Vector3 position = gameBall.ThrowToPosition(targetPos, flightTime, false);
      this._flowRoutine.RunAdditive(this.ProcessBallFlightRoutine(gameBall));
      if (!this._settings.showPredictionTrail)
        return;
      this._scene.BallPrediction.ShowTrail(shootPoint.position, position);
    }

    private IEnumerator ProcessBallFlightRoutine(BallObject ballObject)
    {
      ballObject.Graphics.SetHighlight(true);
      Transform ballTx = ballObject.transform;
      while (!ballObject.inHand && (double) ballTx.position.y > 0.20000000298023224)
        yield return (object) null;
      GameplayUI.Hide();
      ballObject.Graphics.SetHighlight(false);
      yield return (object) this._flowRoutine.RunAdditive(this.HandleResult(this._scene.GameBall.inHand ? EPlayResult.kPassCaught : EPlayResult.kPassMissed));
    }
  }
}
