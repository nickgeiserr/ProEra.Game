// Decompiled with JetBrains decompiler
// Type: TB12.MiniCampQBPresenceGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.AppStates;
using TB12.GameplayData;
using TB12.Sequences;
using TB12.UI;
using TB12.UI.Screens;
using UnityEngine;

namespace TB12
{
  public class MiniCampQBPresenceGameFlow : MonoBehaviour
  {
    [SerializeField]
    private Transform _playerStartTransform;
    [SerializeField]
    private MiniCampQBPresenceGameState _state;
    [SerializeField]
    private GameplayDataStore _gameplayData;
    [SerializeField]
    private GameLevelsStore _levelsStore;
    [SerializeField]
    private MiniCampQBPresenceLevelDataStore _miniCampQBPresenceLevelDataStore;
    [SerializeField]
    private MiniCampQBPresenceScene _scene;
    [SerializeField]
    private MiniCampTourStore _miniCampTourStore;
    [SerializeField]
    private PracticeTargetsManager _targetsManager;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    private UniformStore _uniformStore;
    [SerializeField]
    private AvatarGraphics _introAvatar;
    [SerializeField]
    private Transform _bucketTxR;
    [SerializeField]
    private Transform _bucketTxL;
    [SerializeField]
    private PocketPresenceIndicator _pocketPresenceIndicator;
    [SerializeField]
    private string ChallengeName;
    [SerializeField]
    private float _outOfPocketDuration = 3f;
    private VRColorAdjustments _colorAdjustments;
    [SerializeField]
    private float _timeToHitTarget = 5f;
    [SerializeField]
    private int _numAttacks = 4;
    [SerializeField]
    private float _timeBetweenAttacks = 1.3f;
    [SerializeField]
    private float _attackDelay = 0.5f;
    [SerializeField]
    private float _attackFudgeDistance = 1f;
    [SerializeField]
    private int _maxComboModifier = 4;
    [SerializeField]
    private List<Renderer> tackleDummyRenderers;
    [SerializeField]
    private float JugMachineThrowDistOffset = 10f;
    [SerializeField]
    private BallsContainer _qbteePrefab;
    [Header("Flipped Orientation Settings")]
    [SerializeField]
    private Vector3 _flippedPlayerStartPos;
    [SerializeField]
    private Vector3 _flippedPosition;
    [SerializeField]
    private Vector3 _flippedRotation;
    private const float _playerDistanceToLine = 2f;
    private readonly RoutineHandle _flowRoutine = new RoutineHandle();
    private readonly RoutineHandle _throwRoutine = new RoutineHandle();
    private PassChallenge _level;
    private bool _ftuePlayed;
    private bool _playbackFinished;
    private PracticeTarget _lastTarget;
    private ReceiverUI _currentReceiver;
    private List<ReceiverUI> _receivers = new List<ReceiverUI>();
    private ReceiverUI _possibleCurrentReceiver;
    private float _currentReceiverDot;
    private int _currentReceiverIndex;
    private float _swtichTimer;
    private float _maxSwitchTime;
    private LinksHandler _linksHandler = new LinksHandler();
    private bool _pickedUpBall;
    private bool _ballThrown;
    private bool _throwSuccess;
    private bool _attackPhaseInProgress;
    private bool _throwPhaseInProgress;
    private bool _gameStarted;
    private int _numAttacksRemaining;
    private float _normalBallRotationsPerSecond;
    [SerializeField]
    private float _maxTime = 60f;
    private float _timeRemaining;
    private bool _didStartCountdown;
    private bool _isPaused;
    private bool _defaultHandPhysicsSetting;
    private readonly RoutineHandle _tackleRoutine = new RoutineHandle();
    private readonly RoutineHandle _outOfPocketRoutine = new RoutineHandle();

    public float TimeRemaining => this._timeRemaining;

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      try
      {
        this._timeBetweenAttacks = this._miniCampQBPresenceLevelDataStore.levels[PersistentSingleton<SaveManager>.Instance.miniCamp.SelectedEntry.Level - 1]._timeBetweenAttacks;
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex.Message);
        this._timeBetweenAttacks = 10f;
      }
      this._defaultHandPhysicsSetting = ScriptableSingleton<ThrowSettings>.Instance.HandPhysicsSettings.handPhysics.Value;
      ScriptableSingleton<ThrowSettings>.Instance.HandPhysicsSettings.handPhysics.Value = false;
      this._normalBallRotationsPerSecond = ScriptableSingleton<ThrowSettings>.Instance.FlightSettings.BallRotationsPerSecond;
      ScriptableSingleton<ThrowSettings>.Instance.FlightSettings.BallRotationsPerSecond = 0.0f;
      this._throwManager.AutoAimRange = 30f;
      PersistentSingleton<BallsContainerManager>.Instance.SetBallsContainersPrefab(this._qbteePrefab);
      BallsContainerManager.IsEnabled.SetValue(true);
      BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
      this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
      this._state.OnTrainingStarted += new System.Action(this.HandleTrainingStarted);
      this._state.OnExitTraining += new System.Action(this.StopGameplay);
      this._throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleThrow);
      this._uniformStore.SetNamesAndNumbersVisibility(true);
      this._pocketPresenceIndicator.OnPocketStatusChanged += new Action<bool>(this.HandlePocketStatusChanged);
      this._colorAdjustments = PersistentSingleton<GamePlayerController>.Instance.ColorAdjustments;
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._playbackInfo.OnPlaybackFinished.Link(new System.Action(this.HandlePlaybackFinished)),
        VREvents.PlayerCollision.Link<GameObject>((Action<GameObject>) (collisionObject => this._tackleRoutine.Run(this.TackleRoutine(collisionObject))))
      });
    }

    private void Update()
    {
      if (this._gameStarted)
      {
        if ((double) this._timeRemaining > 0.0)
        {
          this._timeRemaining -= Time.deltaTime;
          if ((double) this._timeRemaining <= 5.0 && !this._didStartCountdown)
          {
            AppSounds.PlaySfx(ESfxTypes.kMiniCountdown);
            this._didStartCountdown = true;
          }
          MiniGameScoreState.TimeRemaining.SetValue(this._timeRemaining);
        }
        else
          this.GameOver();
        this.GetClosestReceiver();
        if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null)
          EligibilityManager.Instance.SetCurrentTarget(this._currentReceiver);
      }
      if (PauseScreen.isPaused == this._isPaused)
        return;
      this._isPaused = PauseScreen.isPaused;
      BallsContainerManager.CanSpawnBall.SetValue(!this._isPaused);
      foreach (HandData handData in this._throwManager.HandsDataModel.HandDatas)
        handData.EnableCatching(!this._isPaused);
    }

    private void SetPostEffects(bool state)
    {
      if ((UnityEngine.Object) this._colorAdjustments == (UnityEngine.Object) null)
        return;
      this._colorAdjustments.active = state;
      this._colorAdjustments.colorFilter = Color.white;
    }

    private void HandlePocketStatusChanged(bool InPocket)
    {
      if (!this._gameStarted)
        return;
      if (!InPocket)
      {
        GameplayUI.PointTo(this._pocketPresenceIndicator.transform, "");
        this._outOfPocketRoutine.Run(this.OutOfPocketRoutine());
        if (!this._throwPhaseInProgress)
          return;
        this._lastTarget.SetActiveForPoints(false);
      }
      else
      {
        GameplayUI.HidePointer();
        if (!this._throwPhaseInProgress)
          return;
        this._lastTarget.SetActiveForPoints(true);
      }
    }

    private IEnumerator TackleRoutine(GameObject collisionObject)
    {
      if (this._throwManager.HandsDataModel.HandDatas[0].hasObject || this._throwManager.HandsDataModel.HandDatas[1].hasObject)
      {
        MiniGameScoreState.AccumulateScore((int) (-75.0 * (double) AppState.Difficulty.ThrowPointsMultiplier));
        this._state.CalculateStarsEarned();
        MiniGameScoreState.ComboModifier = 1;
      }
      yield return (object) new WaitForSeconds(0.1f);
      this._colorAdjustments.colorFilter = Color.white;
      this.SetPostEffects(false);
      VRState.LocomotionEnabled.SetValue(true);
    }

    private IEnumerator OutOfPocketRoutine()
    {
      this.SetPostEffects(true);
      float startTime = Time.time;
      float endTime = startTime + this._outOfPocketDuration;
      while (!this._pocketPresenceIndicator.InPocket && (double) Time.time < (double) endTime)
      {
        this._colorAdjustments.colorFilter = Color.Lerp(Color.white, Color.red, Mathf.InverseLerp(startTime, endTime, Time.time));
        yield return (object) null;
      }
      this._colorAdjustments.colorFilter = Color.white;
      this.SetPostEffects(false);
      if ((double) Time.time >= (double) endTime)
      {
        AppSounds.PlayOC(EOCTypes.kResultsFail);
        this._state.DidGiveResultsFeedback = true;
        this.GameOver();
      }
    }

    private void HandlePlaybackFinished() => this._playbackFinished = true;

    private void OnDestroy()
    {
      this.SetPostEffects(false);
      this._outOfPocketRoutine.Stop();
      ScriptableSingleton<ThrowSettings>.Instance.FlightSettings.BallRotationsPerSecond = this._normalBallRotationsPerSecond;
      ScriptableSingleton<ThrowSettings>.Instance.HandPhysicsSettings.handPhysics.Value = this._defaultHandPhysicsSetting;
      this.StopGameplay();
      PersistentSingleton<BallsContainerManager>.Instance.ResetBallsContainerPrefabToDefault();
      BallsContainerManager.IsEnabled.SetValue(false);
      BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
      VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
      this._state.OnTrainingStarted -= new System.Action(this.HandleTrainingStarted);
      this._state.OnExitTraining -= new System.Action(this.StopGameplay);
      this._throwManager.OnThrowProcessed -= new Action<ThrowData>(this.HandleThrowProcessed);
      this._throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleThrow);
      PracticeTarget.OnTargetHit -= new Action<int, bool, bool, PracticeTarget>(this.HandleTargetHit);
      this._pocketPresenceIndicator.OnPocketStatusChanged -= new Action<bool>(this.HandlePocketStatusChanged);
      this._uniformStore = (UniformStore) null;
    }

    private IEnumerator WaitForSecondsOrThrowPhaseEnd(float Seconds)
    {
      while (this._throwPhaseInProgress && (double) Seconds > 0.0)
      {
        Seconds -= Time.deltaTime;
        yield return (object) null;
      }
    }

    private void HandleTrainingStarted()
    {
      this.StopGameplay();
      MiniGameScoreState.ResetData();
      this._gameplayData.Initialize();
      this._throwManager.HandsDataModel.ResetHandsState();
      if ((UnityEngine.Object) this._playerStartTransform == (UnityEngine.Object) null)
      {
        Console.Error.WriteLine("ERROR: Player Start Transform is null in MiniCampQBPresenceGameFlow.cs");
      }
      else
      {
        try
        {
          if (this._miniCampTourStore.miniCampsTemplate[1].GetEntryByLevel(PersistentSingleton<SaveManager>.Instance.miniCamp.SelectedEntry.Level).FlipOrientation)
          {
            this.transform.position = this._flippedPosition;
            this.transform.rotation = Quaternion.Euler(this._flippedRotation);
            this._playerStartTransform.SetPositionAndRotation(this._flippedPlayerStartPos, Quaternion.Euler(this._flippedRotation));
          }
        }
        catch (Exception ex)
        {
          Console.Error.WriteLine(ex.Message);
        }
        PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._playerStartTransform.position, this._playerStartTransform.rotation);
        this._scene.InitializeScene();
        this._throwManager.Clear();
        this._receivers.Clear();
        foreach (PracticeTarget target in this._scene.Targets)
        {
          this._throwManager.RegisterTarget((IThrowTarget) target);
          this._receivers.Add(target.GetComponent<ReceiverUI>());
          PracticeTarget.OnTargetHit += new Action<int, bool, bool, PracticeTarget>(this.HandleTargetHit);
        }
        try
        {
          TackleDummyColorChanger.SetDummyColor(this.tackleDummyRenderers);
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
      }
    }

    private void HandleBallSpawned(BallObject obj)
    {
      UIDispatch.HideAll();
      if (!this._gameStarted)
      {
        this._flowRoutine.Run(this.TrainingFlow());
        this._throwRoutine.Run(this.ThrowFlow());
      }
      else
        this.StartCoroutine(this.CueBallMachine());
      this._pickedUpBall = true;
    }

    private IEnumerator TrainingFlow()
    {
      this.ResetState();
      this.StartAttackPhase();
      this._gameStarted = true;
      while (this._gameStarted)
        yield return (object) this.CueBallMachine();
      yield return (object) null;
    }

    private IEnumerator CueBallMachine()
    {
      JugsMachine jug = this.CueAttack();
      yield return (object) new WaitForSeconds(this._attackDelay);
      this.InitiateAttack(jug);
      yield return (object) new WaitForSeconds(this._timeBetweenAttacks);
    }

    private IEnumerator ThrowFlow()
    {
      while (this._gameStarted)
      {
        this.StartThrowPhase();
        yield return (object) new WaitForSeconds(this._timeToHitTarget);
        this.EndThrowPhase();
      }
      yield return (object) null;
    }

    private void StartAttackPhase()
    {
      this._throwPhaseInProgress = false;
      this._attackPhaseInProgress = true;
      this._numAttacksRemaining = this._numAttacks;
    }

    private void StartThrowPhase()
    {
      this.ChangeTarget();
      this._throwPhaseInProgress = true;
    }

    private void EndThrowPhase()
    {
      if (!this._throwPhaseInProgress)
        return;
      this.InvalidateLastTarget();
      this._throwPhaseInProgress = false;
    }

    private JugsMachine CueAttack()
    {
      JugsMachine random = this._scene.JugsMachines.GetRandom<JugsMachine>();
      random.Initialize(PersistentSingleton<GamePlayerController>.Instance.position, true);
      random.SetHighlight(true);
      return random;
    }

    private void InitiateAttack(JugsMachine AttackingJug)
    {
      AttackingJug.SetHighlight(false);
      Vector3 targetPos = PersistentSingleton<GamePlayerController>.Instance.position + (PersistentSingleton<GamePlayerController>.Instance.position - AttackingJug.transform.position + new Vector3(UnityEngine.Random.Range(-this._attackFudgeDistance, this._attackFudgeDistance), 0.0f, UnityEngine.Random.Range(-this._attackFudgeDistance, this._attackFudgeDistance))).normalized * this.JugMachineThrowDistOffset;
      AttackingJug.ThrowToSpot(targetPos, 1f, 0.0f);
    }

    private void InvalidateLastTarget()
    {
      if (!((UnityEngine.Object) this._lastTarget != (UnityEngine.Object) null))
        return;
      this._lastTarget.SetActiveForPoints(false);
    }

    private void ChangeTarget()
    {
      PracticeTarget random = this._scene.Targets.GetRandom<PracticeTarget>();
      random.SetActiveForPoints(true);
      if ((UnityEngine.Object) this._lastTarget != (UnityEngine.Object) null)
        this._lastTarget.GetComponent<MiniCampHighlightDecal>().SetTargetHighlightActive(false);
      MiniCampHighlightDecal component = random.GetComponent<MiniCampHighlightDecal>();
      component.SetTargetHighlightActive(true);
      switch (component.Location)
      {
        case MiniCampHighlightDecal.ELocation.kLeft:
          AppSounds.PlayVO(EVOTypes.kThrowCallLeft);
          break;
        case MiniCampHighlightDecal.ELocation.kRight:
          AppSounds.PlayVO(EVOTypes.kThrowCallRight);
          break;
        case MiniCampHighlightDecal.ELocation.kMiddle:
          AppSounds.PlayVO(EVOTypes.kThrowCallMiddle);
          break;
      }
      this._lastTarget = random;
    }

    private void HandleThrowProcessed(ThrowData throwData)
    {
      AppSounds.PlaySfx(ESfxTypes.kQBThrow);
      this._ballThrown = true;
      this._throwSuccess = throwData.hasTarget;
      if (!throwData.hasTarget)
        return;
      MiniCampQBPresenceGameFlow.GenerateThrowEventData(throwData, this._playbackInfo);
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

    private void GameOver()
    {
      this.ResetState();
      BallsContainerManager.CanSpawnBall.SetValue(false);
      this._flowRoutine.Run(this.TrainingCompleteRoutine());
    }

    private void ResetState()
    {
      this._timeRemaining = this._maxTime;
      this._gameStarted = false;
      this._playbackFinished = false;
      this._pickedUpBall = false;
      this._attackPhaseInProgress = false;
      this._didStartCountdown = false;
      this.InvalidateLastTarget();
    }

    private void StopGameplay()
    {
      this.ResetState();
      this._flowRoutine.Stop();
      this._scene.CleanupScene();
      this._targetsManager.HideTargets();
      FinishSequence.Stop();
      this._playbackInfo.StopPlayback();
      if (UnityState.quitting)
        return;
      PersistentSingleton<GamePlayerController>.Instance.SetMovementLimits();
    }

    private void HandleThrow(BallObject ballObject, Vector3 throwVector) => MiniGameScoreState.DoThrow(throwVector.magnitude, false);

    private void HandleThrowResult(bool hitTarget, float distance)
    {
      this._scene.ResetBallsContainer();
      if (MiniGameScoreState.Locked)
        return;
      if (!hitTarget)
        MiniGameScoreState.ComboModifier = 1;
      else if (this._maxComboModifier != -1)
      {
        MiniGameScoreState.ComboModifier = Mathf.Clamp(MiniGameScoreState.ComboModifier + 1, 1, this._maxComboModifier);
        Debug.Log((object) MiniGameScoreState.ComboModifier);
      }
      this._pickedUpBall = false;
      this._state.CalculateStarsEarned();
    }

    private void HandleTargetHit(
      int hitSoundId,
      bool userThrown,
      bool nearMiss,
      PracticeTarget practiceTarget)
    {
      if (practiceTarget.IsActiveForPoints)
        return;
      MiniGameScoreState.ComboModifier = 1;
    }

    private IEnumerator TrainingCompleteRoutine()
    {
      yield return (object) new WaitForSeconds(1.5f);
      AppSounds.PlayStinger(EStingerType.kStinger2);
      this._state.ShowEndOfMinicampScreen();
    }

    private void GetClosestReceiver()
    {
      Transform transform = PlayerCamera.Camera.transform;
      Vector3 forward = transform.forward;
      Vector3 vector3_1 = (bool) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing ? transform.position.SetY(0.0f) : transform.position;
      float num1 = -1f;
      int index1 = -1;
      if (this._receivers.Count == 0)
        return;
      for (int index2 = 0; index2 < this._receivers.Count; ++index2)
      {
        ReceiverUI receiver = this._receivers[index2];
        Vector3 vector3_2 = (bool) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing ? receiver.transform.position + receiver.transform.forward : receiver.transform.position;
        float num2 = Vector3.Dot(forward, (vector3_2 - vector3_1).normalized);
        if ((double) num2 > (double) num1)
        {
          num1 = num2;
          index1 = index2;
        }
        if (index2 == this._currentReceiverIndex)
          this._currentReceiverDot = num2;
      }
      float num3 = 0.93f;
      float num4 = 0.8f;
      if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) this._receivers[index1] && (double) num1 >= (double) num3 && (double) this._currentReceiverDot < (double) num3)
      {
        if ((UnityEngine.Object) this._possibleCurrentReceiver != (UnityEngine.Object) this._receivers[index1])
        {
          this._swtichTimer = 0.0f;
          this._possibleCurrentReceiver = this._receivers[index1];
        }
        else
        {
          this._swtichTimer += Time.deltaTime;
          if ((double) this._swtichTimer < (double) this._maxSwitchTime)
            return;
          if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null)
            this._currentReceiver.SetActiveUI(false);
          this._currentReceiver = this._receivers[index1];
          this._currentReceiverIndex = index1;
          this._currentReceiverDot = num1;
          this._currentReceiver.SetActiveUI(true);
        }
      }
      else
      {
        if (!((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null) || (double) this._currentReceiverDot >= (double) num4)
          return;
        this._currentReceiver.SetActiveUI(false);
        this._currentReceiver = (ReceiverUI) null;
        this._currentReceiverIndex = -1;
        this._currentReceiverDot = -1f;
      }
    }
  }
}
