// Decompiled with JetBrains decompiler
// Type: TB12.GameplayScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using FootballVR;
using FootballVR.AvatarSystem;
using Framework;
using Framework.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12.AppStates;
using TB12.Sequences;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class GameplayScene : MonoBehaviour
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private HandsDataModel _handsDataModel;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private PlayState _playState;
    [SerializeField]
    private PlayModeStore _playModeStore;
    [SerializeField]
    private AvatarsManager _avatarsManager;
    [SerializeField]
    private BallObject _ballPrefab;
    [SerializeField]
    private ReceiversHighlighter _receiversHighlighter;
    [EditorSetting(ESettingType.Utility)]
    private static bool skipIdentifyReceiver;
    private BallHikeSequence _hikeSequence;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private const float _receiverOpenTiming = 2.4f;
    private readonly TimeSlot _receiverOpenTimeSlot = new TimeSlot(2.4f, 4.5f);
    private bool _ballThrown;
    private BallObject _gameBall;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      this._gameBall = UnityEngine.Object.Instantiate<BallObject>(this._ballPrefab, this.transform);
      this._gameBall.gameObject.SetActive(false);
      ScriptableSingleton<Gameboard>.Instance.football = this._gameBall.gameObject;
      SerializedDataManager.OnPlayLoaded += new Action<DataSensitiveStructs_v5.PlayData>(ScriptableSingleton<Gameboard>.Instance.ExtractPlayContext);
      this._hikeSequence = new BallHikeSequence(this._playbackInfo, this._throwManager);
      this._playState.OnGameplayStarted += new System.Action(this.StartGameplay);
      this._playState.OnGameplayStopped += new System.Action(this.StopGameplay);
      this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
      this._avatarsManager.OnAvatarsInitialized += new System.Action(this.HandleAvatarsInitialized);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VREvents.UserCollision.Link<Collider>(new Action<Collider>(this.HandleTackle)),
        VREvents.ThrowResult.Link<bool, float>(new Action<bool, float>(this.HandleThrowResult))
      });
    }

    private void OnDestroy()
    {
      this._playState.OnGameplayStarted -= new System.Action(this.StartGameplay);
      this._playState.OnGameplayStopped -= new System.Action(this.StopGameplay);
      this._throwManager.OnThrowProcessed -= new Action<ThrowData>(this.HandleThrowProcessed);
      this._avatarsManager.OnAvatarsInitialized -= new System.Action(this.HandleAvatarsInitialized);
      this._linksHandler.Clear();
      SerializedDataManager.OnPlayLoaded -= new Action<DataSensitiveStructs_v5.PlayData>(ScriptableSingleton<Gameboard>.Instance.ExtractPlayContext);
      if (!((UnityEngine.Object) this._gameBall != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this._gameBall.gameObject);
    }

    private void Update() => ScriptableSingleton<Gameboard>.Instance.Tick();

    private void StopGameplay()
    {
      GameplayUI.Hide();
      this._ballThrown = false;
      this._routineHandle.Stop();
      this._hikeSequence.Stop();
      VRState.CollisionEnabled.SetValue(false);
      this._receiversHighlighter.Stop();
      this._throwManager.Clear();
      if (!((UnityEngine.Object) this._gameBall != (UnityEngine.Object) null))
        return;
      this._gameBall.Release();
      this._gameBall.gameObject.SetActive(false);
      this._gameBall.transform.parent = (Transform) null;
    }

    private void HandleAvatarsInitialized()
    {
      this._playModeStore.Clear();
      if ((UnityEngine.Object) this._avatarsManager.Center != (UnityEngine.Object) null)
        this._playModeStore.Center = this._avatarsManager.Center.transform;
      foreach (Component receiver in this._avatarsManager.Receivers)
        this._playModeStore.Receivers.Add(receiver.transform);
      this._avatarsManager.ShowAllAvatars();
      this._playbackInfo.StartPlayback();
    }

    private void StartGameplay()
    {
      this.StopGameplay();
      this._routineHandle.Run(this.GameplayRoutine());
    }

    private IEnumerator GameplayRoutine()
    {
      yield return (object) null;
      foreach (FootballVR.Avatar receiver in this._avatarsManager.Receivers)
        this._throwManager.RegisterTarget((IThrowTarget) new PlayerThrowTarget(receiver.behaviourController, this._playbackInfo, 2.4f));
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._avatarsManager.GetUserPlayerPosition(), Quaternion.Euler(0.0f, 90f, 0.0f));
      yield return (object) new WaitForSeconds(1f);
      this._gameBall.gameObject.SetActive(true);
      this._gameBall.Pick();
      this._avatarsManager.Center.behaviourController.GrabBall(this._gameBall.transform);
      Transform camTx = PlayerCamera.Camera.transform;
      yield return (object) new WaitForSeconds(2f);
      if (!this._playState.skipIdentification && !GameplayScene.skipIdentifyReceiver)
      {
        GameplayUI.PointTo(this._playModeStore.Receivers[0], "Find the receiver", true);
        foreach (FootballVR.Avatar receiver in this._avatarsManager.Receivers)
          yield return (object) this._routineHandle.RunAdditive(GameplaySequences.LocatePlayer(receiver, camTx));
        GameplayUI.PointTo(this._playModeStore.Center, "Look at the ball", true);
        yield return (object) this._routineHandle.RunAdditive(GameplaySequences.LocatePlayer(this._avatarsManager.Center, camTx));
        this._playState.skipIdentification = true;
      }
      yield return (object) this._hikeSequence.RunHikeSequence(this._avatarsManager.Center, this._gameBall);
      if (!this._hikeSequence.hikeSuccess)
      {
        this._avatarsManager.HideAllAvatars();
        this._playModeStore.Result = EPlayResult.kHikeMissed;
        UIDispatch.FrontScreen.DisplayView(EScreens.kPlayFailed);
      }
      else
      {
        foreach (FootballVR.Avatar allocatedAttacker in this._avatarsManager.AllocatedAttackers)
          ((AttackerBehaviorController) allocatedAttacker.behaviourController).attackEnabled = true;
        VRState.CollisionEnabled.SetValue((bool) ScriptableSingleton<CollisionSettings>.Instance.CollisionEnabled);
        yield return (object) new WaitForSeconds(0.4f);
        AppSounds.PlaySfx(ESfxTypes.kCatchSuccess);
        TimeSlot timeSlot = this._receiverOpenTimeSlot;
        if (this._avatarsManager.ReceiversData != null && this._avatarsManager.ReceiversData.Count > 0)
        {
          ReceiverData receiverData = this._avatarsManager.ReceiversData[0];
          if (receiverData != null)
            timeSlot = new TimeSlot(receiverData.startTime, receiverData.endTime);
        }
        List<Transform> targets = new List<Transform>(this._playModeStore.Receivers.Count);
        foreach (Transform receiver in this._playModeStore.Receivers)
          targets.Add(receiver.transform);
        this._receiversHighlighter.HighlightReceivers(targets, timeSlot.startTime, 1.2f);
        yield return (object) new WaitUntil((Func<bool>) (() => (double) this._playbackInfo.PlayTime > (double) timeSlot.endTime || this._ballThrown));
        this._receiversHighlighter.Stop();
        this._handsDataModel.ResetHandsState();
        if (!this._ballThrown)
        {
          this._gameBall.gameObject.SetActive(false);
          yield return (object) this._routineHandle.RunAdditive(this.HandleSacked());
        }
      }
    }

    private void HandleThrowResult(bool throwHit, float distance)
    {
      this._playModeStore.distance = distance * 3.28084f;
      this._routineHandle.RunAdditive(this.HandlePassResult(throwHit));
    }

    private void HandleTackle(Collider collider)
    {
      this.StopGameplay();
      VREvents.PlayerKnockdown.Trigger(PersistentSingleton<PlayerCamera>.Instance.Position - collider.transform.position);
      this._routineHandle.RunAdditive(this.HandleSacked());
    }

    private IEnumerator HandleSacked()
    {
      this._avatarsManager.HideAllAvatars();
      AppSounds.PlaySfx(ESfxTypes.kSacked);
      yield return (object) new WaitForSeconds(0.2f);
      AppSounds.PlaySfx(ESfxTypes.kWhistle);
      yield return (object) new WaitForSeconds(0.7f);
      AppSounds.PlayStinger(EStingerType.kStinger3);
      yield return (object) new WaitForSeconds(1f);
      this._playModeStore.Result = EPlayResult.kSacked;
      UIDispatch.FrontScreen.DisplayView(EScreens.kPlayFailed);
    }

    private IEnumerator HandlePassResult(bool success)
    {
      this._avatarsManager.HideAllAvatars(delay: success ? 2f : 1f);
      if (success)
      {
        yield return (object) this._routineHandle.RunAdditive(AppSounds.WinSequence());
        UIDispatch.FrontScreen.DisplayView(EScreens.kThrowSuccess);
      }
      else
      {
        yield return (object) this._routineHandle.RunAdditive(AppSounds.FailSequence());
        this._playModeStore.Result = EPlayResult.kIncompletePass;
        UIDispatch.FrontScreen.DisplayView(EScreens.kPlayFailed);
      }
    }

    private void HandleBallCaught()
    {
      if ((UnityEngine.Object) this._gameBall == (UnityEngine.Object) null || this._gameBall.hasHitTarget)
        return;
      this.StartCoroutine(this.HackFixTrailRoutine(this._gameBall.Graphics.Trail.TrailRenderer));
      this._gameBall.CompleteBallFlight();
      this._gameBall.applyPhysics = false;
      this._gameBall.Graphics.HideTrail(0.0f, 0.8f);
      this._gameBall.Pick(hideTrail: false);
    }

    private IEnumerator HackFixTrailRoutine(TrailRenderer trail)
    {
      int posCount = trail.positionCount;
      Vector3 endPos = trail.GetPosition(posCount - 1);
      yield return (object) new WaitForEndOfFrame();
      int newPosCount = trail.positionCount;
      for (int index = posCount; index <= newPosCount; ++index)
        trail.SetPosition(index - 1, endPos);
      newPosCount = trail.positionCount;
      yield return (object) new WaitForEndOfFrame();
      for (int index = posCount; index <= newPosCount; ++index)
        trail.SetPosition(index - 1, endPos);
    }

    private async void HandleThrowProcessed(ThrowData throwData)
    {
      GameplayScene gameplayScene = this;
      gameplayScene._ballThrown = true;
      if (!throwData.hasTarget)
        return;
      Vector3 yard = Utilities.GamePosToYard(throwData.targetPosition);
      gameplayScene._playModeStore.yardLine = yard.x;
      EventData throwEventData = PassGameFlow.GenerateThrowEventData(throwData, gameplayScene._playbackInfo);
      // ISSUE: reference to a compiler-generated method
      throwEventData.OnEventKeyMoment = new Action<object>(gameplayScene.\u003CHandleThrowProcessed\u003Eb__29_0);
      if (!throwData.closestTarget.ReceiveBall(throwEventData) || !DevControls.SlowMotionReceiveBall)
        return;
      await Task.Delay((int) ((double) throwData.flightTime * 0.34999999403953552 * 1000.0));
      Time.timeScale = 0.4f;
      await Task.Delay((int) ((double) throwData.flightTime * 1000.0));
      Time.timeScale = (float) GameSettings.TimeScale;
    }
  }
}
