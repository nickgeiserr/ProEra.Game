// Decompiled with JetBrains decompiler
// Type: MiniCampRolloutFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12;
using TB12.AppStates;
using TB12.AppStates.MiniCamp;
using TB12.UI;
using TB12.UI.Screens;
using UnityEngine;

public class MiniCampRolloutFlow : MonoBehaviour
{
  [SerializeField]
  private Transform _playerStartTransform;
  [SerializeField]
  private ThrowManager _throwManager;
  [SerializeField]
  private MiniCampRolloutGameState _state;
  [SerializeField]
  private Transform _outOfPocketTransform_R;
  [SerializeField]
  private Transform _outOfPocketTransform_L;
  [SerializeField]
  private PocketPresenceIndicator _pocketPresenceIndicator;
  [SerializeField]
  private MiniCampRolloutLevelDataStore _levelDataStore;
  [SerializeField]
  private AxisPlaysStore _axisPlaysStore;
  [SerializeField]
  private GameObject _playbookPrefab;
  [SerializeField]
  private BallsContainer _qbteePrefab;
  [SerializeField]
  private Vector3 _bucketOffset = new Vector3(0.0f, 0.0f, 1f);
  [SerializeField]
  private List<JugsMachine> _frontJugMachines = new List<JugsMachine>();
  [SerializeField]
  private List<JugsMachine> _rightJugMachines = new List<JugsMachine>();
  [SerializeField]
  private List<JugsMachine> _leftJugMachines = new List<JugsMachine>();
  [SerializeField]
  private GameObject _rightJugMachinesGroup;
  [SerializeField]
  private GameObject _leftJugMachinesGroup;
  private readonly RoutineHandle _flowRoutine = new RoutineHandle();
  private readonly RoutineHandle _jugMachineRoutine = new RoutineHandle();
  private MonoBehaviorObjectPool<BallObject> _ballPool;
  private List<JugsMachine> _allJugMachines = new List<JugsMachine>();
  private LinksHandler _linksHandler = new LinksHandler();
  private bool _playActive;
  private bool _pickedUpBall;
  private int _routeIndex;
  private int _miniCampLevel;
  private bool _hitTargetWhileGreen;
  private bool _hitTargetWhileYellow;
  private int _currentPlayResultIndex;
  [SerializeField]
  private int _attemptsPerPlay = 6;
  private int _attemptNumber;
  [SerializeField]
  private RolloutRouteRunningTarget _routeRunningTargetPrefab;
  private ManagedList<RolloutRouteRunningTarget> _targets;
  private List<ReceiverUI> _receivers = new List<ReceiverUI>();
  private PlayDataOff _playDataOff;
  [SerializeField]
  private Transform _bucketTransform;
  [SerializeField]
  private PlayImageItem _playImage;
  public Transform ballLandingSpot;
  public GameObject ballLandingSpotGO;
  private ReceiverUI _currentReceiver;
  private int _currentReceiverIndex;
  private float _currentReceiverDot;
  private ReceiverUI _possibleCurrentReceiver;
  private float _swtichTimer;
  private float _maxSwitchTime;
  [SerializeField]
  private Vector3 _playImageLocalOffset;
  [SerializeField]
  private Quaternion _playImageLocalRotation;
  [SerializeField]
  private int _maxComboModifier = 4;
  [SerializeField]
  private float _attackFudgeDistance = 1f;
  [SerializeField]
  private float JugMachineThrowDistOffset = 10f;
  [SerializeField]
  private float jugMachineAttackDelay = 3f;
  [SerializeField]
  [Tooltip("How much time the yellow cue is up before firing a ball")]
  private float jugMachineCueDelay = 0.5f;
  [SerializeField]
  [Tooltip("The time until the ball machines start their firing once the play starts")]
  private float startOfPlayDelay = 1f;
  [SerializeField]
  private BallObject _ballObjectPrefab;
  public MiniGameScoreState.MiniCampPassResult[,] PlayerResults = new MiniGameScoreState.MiniCampPassResult[3, 4];
  private int currRow;
  private int currElement;
  private bool rollLeft;
  private bool hasBeenHit;
  private bool _isPaused;
  private BallObject _heldBall;

  private void Awake()
  {
    PersistentSingleton<BallsContainerManager>.Instance.SetBallsContainersPrefab(this._qbteePrefab);
    BallsContainerManager.IsEnabled.SetValue(true);
    BallsContainerManager.CanSpawnBall.SetValue(false);
    BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
    VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
    VREvents.PlayerCollision.OnTrigger += new Action<GameObject>(this.HandlePlayerHit);
    this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
    this._state.OnTrainingStarted += new System.Action(this.HandleTrainingStarted);
    this._state.OnExitTraining += new System.Action(this.StopGameplay);
    this._throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleThrow);
    this._pocketPresenceIndicator.OnPocketStatusChanged += new Action<bool>(this.HandlePocketStatusChanged);
    this._pocketPresenceIndicator.gameObject.SetActive(false);
    this._targets = new ManagedList<RolloutRouteRunningTarget>((IObjectPool<RolloutRouteRunningTarget>) new MonoBehaviorObjectPool<RolloutRouteRunningTarget>(this._routeRunningTargetPrefab, this.transform));
    this._playImage.transform.SetParent((ScriptableSingleton<HandsDataModel>.Instance.ActiveHand == ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left) ? ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right).hand : ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left).hand).TopWristGO.transform, false);
    this._playImage.transform.localPosition = this._playImageLocalOffset;
    this._playImage.transform.localRotation = this._playImageLocalRotation;
    UnityEngine.Object.Instantiate<GameObject>(this._playbookPrefab);
    FieldState.OffenseGoingNorth.Value = false;
    PlaybookState.OnOffense.SetValue(true);
    Plays.self.SetOffensivePlaybookP1("ALL");
    PracticeTarget.OnTargetHit += new Action<int, bool, bool, PracticeTarget>(this.PracticeTarget_OnTargetHit);
    Transform transform = new GameObject("BallPool").transform;
    transform.parent = this.transform;
    this._ballPool = new MonoBehaviorObjectPool<BallObject>(this._ballObjectPrefab, transform);
    this._allJugMachines.Clear();
    this._allJugMachines.AddRange((IEnumerable<JugsMachine>) this._frontJugMachines);
    this._allJugMachines.AddRange((IEnumerable<JugsMachine>) this._leftJugMachines);
    this._allJugMachines.AddRange((IEnumerable<JugsMachine>) this._rightJugMachines);
    this.HookupJugMachines(this._frontJugMachines);
    this.HookupJugMachines(this._leftJugMachines);
    this.HookupJugMachines(this._rightJugMachines);
  }

  private void HandlePlayerHit(GameObject tackleBall)
  {
    if (this.hasBeenHit)
      return;
    ScriptableSingleton<CollisionSettings>.Instance.CollisionEnabled.SetValue(false);
    this.hasBeenHit = true;
    this._flowRoutine.Stop();
    this.StartCoroutine(this.CheckGameContinue());
  }

  private void HandleTrainingStarted()
  {
    this._throwManager.HandsDataModel.ResetHandsState();
    this._playActive = false;
    this._pickedUpBall = false;
    this._routeIndex = 0;
    this._attemptNumber = 0;
    this._targets.SetCount(0);
    this._receivers.Clear();
    this._currentReceiver = (ReceiverUI) null;
    this._currentReceiverIndex = 0;
    this._currentReceiverDot = 0.0f;
    this._possibleCurrentReceiver = (ReceiverUI) null;
    this._swtichTimer = 0.0f;
    this._miniCampLevel = PersistentSingleton<SaveManager>.Instance.miniCamp.SelectedEntry.Level - 1;
    this.EndPlay();
    this.SetupPlay();
    this.TeleportPlayerToStartPoint(true);
    try
    {
      TackleDummyColorChanger.SetDummyColor(this._targets.Select<RolloutRouteRunningTarget, Renderer>((Func<RolloutRouteRunningTarget, Renderer>) (t => t.transform.Find("TackleDummy").GetComponent<Renderer>())).ToList<Renderer>());
    }
    catch
    {
      Debug.LogError((object) "Failed to Set TackleDummy Renderer Color");
    }
  }

  private void Update()
  {
    this.GetClosestReceiver();
    if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null)
      EligibilityManager.Instance.SetCurrentTarget(this._currentReceiver);
    if (PauseScreen.isPaused == this._isPaused)
      return;
    this._isPaused = PauseScreen.isPaused;
    foreach (HandData handData in this._throwManager.HandsDataModel.HandDatas)
      handData.EnableCatching(!this._isPaused);
  }

  private IEnumerator JugMachineAttack()
  {
    if (this.rollLeft)
    {
      this._leftJugMachinesGroup.SetActive(true);
      this._rightJugMachinesGroup.SetActive(false);
    }
    else
    {
      this._rightJugMachinesGroup.SetActive(true);
      this._leftJugMachinesGroup.SetActive(false);
    }
    yield return (object) new WaitForSeconds(this.startOfPlayDelay);
    while (this._playActive)
    {
      foreach (JugsMachine allJugMachine in this._allJugMachines)
      {
        allJugMachine.Initialize(PersistentSingleton<GamePlayerController>.Instance.position, true);
        allJugMachine.SetHighlight(true);
      }
      yield return (object) new WaitForSeconds(this.jugMachineCueDelay);
      foreach (JugsMachine allJugMachine in this._allJugMachines)
        this.InitiateAttack(allJugMachine);
      yield return (object) new WaitForSeconds(this.jugMachineAttackDelay);
    }
  }

  private void InitiateAttack(JugsMachine AttackingJug)
  {
    if (!AttackingJug.gameObject.activeInHierarchy)
      return;
    AttackingJug.SetHighlight(false);
    Vector3 targetPos = PersistentSingleton<GamePlayerController>.Instance.position + (PersistentSingleton<GamePlayerController>.Instance.position - AttackingJug.transform.position + new Vector3(UnityEngine.Random.Range(-this._attackFudgeDistance, this._attackFudgeDistance), 0.0f, UnityEngine.Random.Range(-this._attackFudgeDistance, this._attackFudgeDistance))).normalized * this.JugMachineThrowDistOffset;
    AttackingJug.ThrowToSpot(targetPos, 1f, 0.0f);
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
    float num3 = (bool) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing ? 0.965f : 0.95f;
    if (!((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) this._receivers[index1]) || (double) num1 < (double) num3 || (double) this._currentReceiverDot >= (double) num3)
      return;
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
      this._currentReceiver = this._receivers[index1];
      this._currentReceiverIndex = index1;
      this._currentReceiverDot = num1;
    }
  }

  private void HandlePocketStatusChanged(bool inPocket)
  {
    if (!inPocket)
      GameplayUI.PointTo(this._pocketPresenceIndicator.transform, "");
    else
      GameplayUI.HidePointer();
    if (!this._playActive)
      return;
    foreach (PracticeTarget target in this._targets)
      target.SetActiveForPoints(inPocket);
  }

  private void HandleThrow(BallObject ball, Vector3 throwLocation)
  {
  }

  private void StopGameplay()
  {
    this.UnHookJugMachines(this._frontJugMachines);
    this.UnHookJugMachines(this._rightJugMachines);
    this.UnHookJugMachines(this._leftJugMachines);
    this._playImage.transform.SetParent(this.transform);
    GameplayUI.HidePointer();
  }

  private void HookupJugMachines(List<JugsMachine> jugMachines)
  {
    foreach (JugsMachine jugMachine in jugMachines)
      jugMachine.OnBallThrown += new Action<Transform, Vector3, float>(this.HandleBallThrown);
  }

  private void UnHookJugMachines(List<JugsMachine> jugMachines)
  {
    foreach (JugsMachine jugMachine in jugMachines)
      jugMachine.OnBallThrown -= new Action<Transform, Vector3, float>(this.HandleBallThrown);
  }

  private void PracticeTarget_OnTargetHit(
    int hitSoundId,
    bool userThrown,
    bool nearMiss,
    PracticeTarget target)
  {
    if (!(target is RolloutRouteRunningTarget))
      return;
    float sweetSpotPercent = ((RolloutRouteRunningTarget) target).GetSweetSpotPercent();
    if ((double) sweetSpotPercent >= 0.800000011920929)
      this._hitTargetWhileGreen = target.IsActiveForPoints;
    else if ((double) sweetSpotPercent >= 0.60000002384185791)
      this._hitTargetWhileYellow = target.IsActiveForPoints;
    Debug.Log((object) ("Sweet Spot percent for " + target.name + " is " + sweetSpotPercent.ToString() + " at timestamp " + Time.time.ToString()));
  }

  private void HandleThrowProcessed(ThrowData throwData)
  {
    AppSounds.PlaySfx(ESfxTypes.kQBThrow);
    this.StopJugMachines();
    this.ballLandingSpot.position = new Vector3(throwData.targetPosition.x, this.ballLandingSpot.position.y, throwData.targetPosition.z);
    this.ballLandingSpotGO.SetActive(true);
    if (!(throwData.closestTarget is RouteRunningTarget))
      return;
    ((RouteRunningTarget) throwData.closestTarget).SetTarget(throwData.targetPosition, true);
  }

  private void HandleThrowResult(bool targetHit, float distance)
  {
    this.StopJugMachines();
    if (this._throwManager.HandsDataModel.HandDatas[0].hasObject || this._throwManager.HandsDataModel.HandDatas[1].hasObject)
      return;
    if (!targetHit)
    {
      MiniGameScoreState.ComboModifier = 1;
      Console.WriteLine("MiniGameScoreState ComboModifier set to: 1");
    }
    else if (this._maxComboModifier != -1)
    {
      MiniGameScoreState.ComboModifier = Mathf.Clamp(MiniGameScoreState.ComboModifier + 1, 1, this._maxComboModifier);
      Console.WriteLine("MiniGameScoreState ComboModifier: " + MiniGameScoreState.ComboModifier.ToString());
    }
    this._pickedUpBall = false;
  }

  private void HandleBallSpawned(BallObject obj)
  {
    this._heldBall = obj;
    this._pickedUpBall = true;
    this.StartPlay();
  }

  private void StartPlay()
  {
    this.hasBeenHit = false;
    ScriptableSingleton<CollisionSettings>.Instance.CollisionEnabled.SetValue(true);
    AppSounds.PlayVO(this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].ShouldRollLeft ? EVOTypes.kThrowCallLeft : EVOTypes.kThrowCallRight);
    VRState.LocomotionEnabled.SetValue(true);
    this.ballLandingSpotGO.gameObject.SetActive(false);
    this._flowRoutine.Run(this.TrainingFlow());
    this._pocketPresenceIndicator.gameObject.SetActive(true);
    this._pocketPresenceIndicator.Initialize();
    this._playActive = true;
    foreach (RouteRunningTarget target in this._targets)
      target.SetShouldRunRoute(true);
    this._jugMachineRoutine.Run(this.JugMachineAttack());
  }

  private void EndPlay()
  {
    if ((UnityEngine.Object) this._heldBall != (UnityEngine.Object) null)
    {
      this._throwManager.Clear(false);
      UnityEngine.Object.Destroy((UnityEngine.Object) this._heldBall.gameObject, 0.5f);
    }
    PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
    {
      this._bucketTransform
    }, 1);
    BallsContainerManager.CanSpawnBall.SetValue(true);
    PersistentSingleton<BallsContainerManager>.Instance.gameObject.SetActive(true);
    this._pocketPresenceIndicator.gameObject.SetActive(false);
    this._playActive = false;
    GameplayUI.HidePointer();
    MiniGameScoreState.AttemptsRemaining.SetValue(this._attemptsPerPlay - this._attemptNumber);
    foreach (RolloutRouteRunningTarget target in this._targets)
    {
      this._throwManager.UnregisterTarget((IThrowTarget) target);
      target.SetShouldRunRoute(false);
      target.gameObject.SetActive(false);
    }
  }

  private void SetupPlay()
  {
    this.rollLeft = this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].ShouldRollLeft;
    VRState.LocomotionEnabled.SetValue(false);
    if (this._miniCampLevel >= this._levelDataStore.RouteDataList.Count || this._routeIndex >= this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData.Count)
    {
      Console.Error.WriteLine("Out of bounds in RolloutLevelDataStore");
    }
    else
    {
      FormationData withSubFormation = PlayManager.GetFormationByNameWithSubFormation(this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].FormationName, this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].SubFormationName, Plays.self.playbook_DimeDropping);
      if (withSubFormation == null)
      {
        Debug.LogError((object) ("ERROR: Formation data is null at SetUpPlay() for play " + this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].PlayName));
        Console.Error.WriteLine("ERROR: Formation data is null at SetUpPlay() for play " + this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].PlayName);
      }
      else
      {
        PlaybookState.CurrentFormation.SetValue(withSubFormation);
        this._playDataOff = (PlayDataOff) PlayManager.GetFormationPlayByName(this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].PlayName, withSubFormation);
        if (this._playDataOff == null)
        {
          Debug.LogError((object) ("ERROR: PlayDataOff is null at SetUpPlay() for play " + this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].PlayName));
          Console.Error.WriteLine("ERROR: PlayDataOff is null at SetUpPlay() for play " + this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].PlayName);
        }
        else
        {
          this._targets.SetCount(this._playDataOff.GetFormation().GetReceiversInFormation() + this._playDataOff.GetFormation().GetTEsInFormation());
          if ((UnityEngine.Object) this._playImage != (UnityEngine.Object) null && (UnityEngine.Object) this._axisPlaysStore != (UnityEngine.Object) null)
          {
            this._playImage.gameObject.SetActive(true);
            this._axisPlaysStore.SetupPlayImage(this._playImage.spriteRenderer, (PlayData) this._playDataOff);
          }
          this._receivers.Clear();
          this._receivers.AddRange(this._targets.Select<RolloutRouteRunningTarget, ReceiverUI>((Func<RolloutRouteRunningTarget, ReceiverUI>) (tar => tar.GetComponent<ReceiverUI>())));
          int index1 = 0;
          int index2 = 0;
          Vector2 zero = Vector2.zero;
          foreach (Position position in this._playDataOff.GetFormation().GetPositions())
          {
            if (position == Position.QB)
            {
              Vector2 vector2 = new Vector2(this._playDataOff.GetFormation().GetXLocations()[index1], this._playDataOff.GetFormation().GetZLocations()[index1]);
              break;
            }
            ++index1;
          }
          int i = 0;
          foreach (Position position in this._playDataOff.GetFormation().GetPositions())
          {
            switch (position)
            {
              case Position.WR:
              case Position.SLT:
              case Position.TE:
                Vector2 vector2 = new Vector2(this._playDataOff.GetFormation().GetXLocations()[i], this._playDataOff.GetFormation().GetZLocations()[i]);
                RolloutRouteRunningTarget target = this._targets[index2];
                target.YardLineAdjustment = 20f;
                Vector3 euler = new Vector3(0.0f, 180f, 0.0f);
                target.transform.SetPositionAndRotation(new Vector3(-vector2.x, 0.0f, vector2.y - (float) (20 * global::Game.OffensiveFieldDirection)), Quaternion.Euler(euler));
                target.gameObject.SetActive(true);
                target.SetRouteData(this._playDataOff.GetRouteData(i));
                target.SetRolloutSideValue(this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].ShouldRollLeft ? 1 : -1);
                target.SetUIVisibility(false);
                this._throwManager.RegisterTarget((IThrowTarget) target);
                target.SetShouldRunRoute(false);
                ++index2;
                break;
            }
            ++i;
          }
          foreach (ReceiverUI receiver in this._receivers)
          {
            if ((UnityEngine.Object) receiver != (UnityEngine.Object) null)
              receiver.SetActiveUI(true);
          }
          this._pocketPresenceIndicator.transform.SetParent(this._levelDataStore.RouteDataList[this._miniCampLevel].RouteData[this._routeIndex].ShouldRollLeft ? this._outOfPocketTransform_L : this._outOfPocketTransform_R, false);
        }
      }
    }
  }

  private void HandleBallThrown(Transform spawnTx, Vector3 destination, float flightTime)
  {
    AppSounds.Play3DSfx(ESfxTypes.kMiniFiringMachine, spawnTx);
    BallObject ballObject = this._ballPool.GetObject();
    ballObject.transform.SetPositionAndRotation(spawnTx.position, spawnTx.rotation);
    ballObject.ThrowToPosition(destination, flightTime, true);
  }

  private IEnumerator TrainingFlow()
  {
    yield return (object) this.WaitForSecondsOrBallInactive(10f);
    if (!this._pickedUpBall)
      yield return (object) new WaitForSeconds(1f);
    yield return (object) this.CheckGameContinue();
  }

  private IEnumerator CheckGameContinue()
  {
    this.StopJugMachines();
    if (this.IncrementAttempts())
    {
      VRState.LocomotionEnabled.SetValue(false);
      this.TeleportPlayerToStartPoint();
      yield return (object) new WaitForSeconds(0.5f);
      this.EndPlay();
      this.SetupPlay();
    }
  }

  private void StopJugMachines()
  {
    this._jugMachineRoutine.Stop();
    foreach (JugsMachine allJugMachine in this._allJugMachines)
      allJugMachine.SetHighlight(false);
  }

  private void CleanupPlay()
  {
  }

  private void TeleportPlayerToStartPoint(bool instant = false)
  {
    Vector3 position = this._playerStartTransform.position;
    Quaternion rotation = this._playerStartTransform.rotation;
    if (instant)
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(position, rotation);
    else
      VREvents.BlinkMovePlayer.Trigger(1f, position, rotation);
    this._bucketTransform.position = position + this._bucketOffset;
    PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
    {
      this._bucketTransform
    }, 1);
    BallsContainerManager.CanSpawnBall.SetValue(true);
    PersistentSingleton<BallsContainerManager>.Instance.gameObject.SetActive(true);
  }

  private bool IncrementAttempts()
  {
    bool flag = true;
    MiniGameScoreState.MiniCampPassResult miniCampPlayResult = new MiniGameScoreState.MiniCampPassResult(this._currentPlayResultIndex, this.CalculateAttemptResult());
    this._state.CalculateStarsEarned();
    MiniGameScoreState.MarkPlayResult(miniCampPlayResult);
    ++this._currentPlayResultIndex;
    this.PlayerResults[this.currRow, this.currElement] = miniCampPlayResult;
    ++this.currElement;
    if (this.currElement >= 4)
    {
      this.currElement = 0;
      ++this.currRow;
    }
    if (++this._attemptNumber >= this._attemptsPerPlay)
      flag = this.IncrementPlay();
    return flag;
  }

  private MiniGameScoreState.EMiniCampPassResult CalculateAttemptResult()
  {
    MiniGameScoreState.EMiniCampPassResult attemptResult = !this._hitTargetWhileGreen ? (!this._hitTargetWhileYellow ? MiniGameScoreState.EMiniCampPassResult.Miss : MiniGameScoreState.EMiniCampPassResult.Yellow) : MiniGameScoreState.EMiniCampPassResult.Green;
    this._hitTargetWhileGreen = false;
    this._hitTargetWhileYellow = false;
    return attemptResult;
  }

  private bool IncrementPlay()
  {
    bool flag = true;
    this._attemptNumber = 0;
    if (++this._routeIndex >= this._levelDataStore.RouteDataList.Count - 1)
    {
      flag = false;
      this.GameOver();
    }
    return flag;
  }

  private void GameOver()
  {
    this.ResetState();
    BallsContainerManager.CanSpawnBall.SetValue(false);
    this._flowRoutine.Run(this.TrainingCompleteRoutine());
  }

  private void ResetState()
  {
  }

  private IEnumerator WaitForSecondsOrBallInactive(float Seconds)
  {
    while (this._pickedUpBall && (double) Seconds > 0.0)
    {
      Seconds -= Time.deltaTime;
      yield return (object) null;
    }
  }

  private IEnumerator TrainingCompleteRoutine()
  {
    ScriptableSingleton<CollisionSettings>.Instance.CollisionEnabled.SetValue(true);
    yield return (object) new WaitForSeconds(1.5f);
    AppSounds.PlayStinger(EStingerType.kStinger2);
    this._currentPlayResultIndex = 0;
    this.currElement = 0;
    this.currRow = 0;
    this._state.ShowEndOfMinicampScreen();
  }

  private void OnDestroy()
  {
    PersistentSingleton<BallsContainerManager>.Instance.gameObject.SetActive(true);
    BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
    PracticeTarget.OnTargetHit -= new Action<int, bool, bool, PracticeTarget>(this.PracticeTarget_OnTargetHit);
    VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
    VREvents.PlayerCollision.OnTrigger -= new Action<GameObject>(this.HandlePlayerHit);
    if ((UnityEngine.Object) this._throwManager != (UnityEngine.Object) null)
    {
      this._throwManager.OnThrowProcessed -= new Action<ThrowData>(this.HandleThrowProcessed);
      this._throwManager.OnBallThrown -= new Action<BallObject, Vector3>(this.HandleThrow);
    }
    if ((UnityEngine.Object) this._state != (UnityEngine.Object) null)
    {
      this._state.OnTrainingStarted -= new System.Action(this.HandleTrainingStarted);
      this._state.OnExitTraining -= new System.Action(this.StopGameplay);
    }
    if (!((UnityEngine.Object) this._pocketPresenceIndicator != (UnityEngine.Object) null))
      return;
    this._pocketPresenceIndicator.OnPocketStatusChanged -= new Action<bool>(this.HandlePocketStatusChanged);
  }
}
