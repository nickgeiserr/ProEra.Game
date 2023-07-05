// Decompiled with JetBrains decompiler
// Type: PrecisionPassingGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TB12;
using TB12.AppStates.MiniCamp;
using TB12.UI;
using TB12.UI.Screens;
using UnityEngine;

public class PrecisionPassingGameFlow : MonoBehaviour
{
  [SerializeField]
  private ThrowManager _throwManager;
  [SerializeField]
  private PrecisionPassingGameState _state;
  [SerializeField]
  private PrecisionPassingLevelDataStore _levelDataStore;
  [SerializeField]
  private AxisPlaysStore _axisPlaysStore;
  [SerializeField]
  private GameObject _playbookPrefab;
  [SerializeField]
  private BallsContainer _qbteePrefab;
  [SerializeField]
  private RouteRunningTarget _routeRunningTargetPrefab;
  private ManagedList<RouteRunningTarget> _targets;
  [SerializeField]
  private ThrowRing _throwRingPrefab;
  private ManagedList<ThrowRing> _rings;
  [SerializeField]
  private PlayImageItem _playImage;
  [SerializeField]
  private Vector3 _playImageLocalOffset;
  [SerializeField]
  private Quaternion _playImageLocalRotation;
  private List<ReceiverUI> _receivers = new List<ReceiverUI>();
  private ReceiverUI _currentReceiver;
  private ReceiverUI _possibleCurrentReceiver;
  private int _currentReceiverIndex;
  private float _currentReceiverDot;
  [SerializeField]
  private Transform _bucketTransform;
  [SerializeField]
  private float ringPercentTowardsQB = 0.5f;
  [SerializeField]
  private string[] _routeStrings;
  [SerializeField]
  private int _attemptsPerPlay = 6;
  private int _attemptNumber;
  [HideInInspector]
  public Vector3 passDestination;
  private bool _playIsActive;
  private bool _pickedUpBall;
  private const float _playerDistanceToLine = 3.5f;
  private Coroutine _waitForBallToSettle;
  private int _routeIndex;
  private float _swtichTimer;
  private float _maxSwitchTime;
  private PlayDataOff _playDataOff;
  [SerializeField]
  private string _playbookName = "DIME DROPPING";
  private FormationData _formationData;
  [SerializeField]
  private List<PlayData> _playData;
  [SerializeField]
  private bool _randomizePlays = true;
  [SerializeField]
  private int _numPlays = 12;
  private int _currentPlayIndex;
  private readonly RoutineHandle _flowRoutine = new RoutineHandle();
  private Vector3 _playerPosition;
  private PrecisionPassingGameFlow.AwardPoints[,] throwAwards;
  private bool _throwAwarded;
  [SerializeField]
  private PrecisionPassingGameFlow.AwardPoints[] _throwAwardPoints;
  private bool _testMode;
  [SerializeField]
  private Vector3 _bucketOffset = new Vector3(0.0f, 0.0f, 1f);
  private bool _isPaused;

  public PrecisionPassingGameFlow.AwardPoints[,] ThrowAwards => this.throwAwards;

  public int GetPointsForThrowAward(PrecisionPassingGameFlow.ThrowAward award)
  {
    foreach (PrecisionPassingGameFlow.AwardPoints throwAwardPoint in this._throwAwardPoints)
    {
      if (throwAwardPoint.award == award)
        return throwAwardPoint.point;
    }
    return -1;
  }

  private void Awake()
  {
    PersistentSingleton<BallsContainerManager>.Instance.SetBallsContainersPrefab(this._qbteePrefab);
    BallsContainerManager.IsEnabled.SetValue(true);
    BallsContainerManager.CanSpawnBall.SetValue(false);
    this._state.OnTrainingStarted += new System.Action(this.HandleTrainingStarted);
    this._state.OnExitTraining += new System.Action(this.HandleExitTraining);
    this._throwManager.HandsDataModel.OnBallPicked += new System.Action(this.HandlePickedUpBall);
    BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
    this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
    VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
    this._targets = new ManagedList<RouteRunningTarget>((IObjectPool<RouteRunningTarget>) new MonoBehaviorObjectPool<RouteRunningTarget>(this._routeRunningTargetPrefab, this.transform));
    this._rings = new ManagedList<ThrowRing>((IObjectPool<ThrowRing>) new MonoBehaviorObjectPool<ThrowRing>(this._throwRingPrefab, this.transform));
    this._playImage.transform.SetParent((ScriptableSingleton<HandsDataModel>.Instance.ActiveHand == ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left) ? ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right).hand : ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left).hand).TopWristGO.transform, false);
    this._playImage.transform.localPosition = this._playImageLocalOffset;
    this._playImage.transform.localRotation = this._playImageLocalRotation;
    UnityEngine.Object.Instantiate<GameObject>(this._playbookPrefab);
    FieldState.OffenseGoingNorth.Value = false;
    PlaybookState.OnOffense.SetValue(true);
    Plays.self.SetOffensivePlaybookP1(this._playbookName);
    this._formationData = Plays.self.shotgunPlays_NormalDimeDropping;
    this._playData = new List<PlayData>();
    List<PlayData> playDataList = new List<PlayData>((IEnumerable<PlayData>) this._formationData.GetPlays());
    for (int index1 = 0; index1 < Mathf.Min(this._numPlays, this._formationData.GetNumberOfPlaysInFormation()); ++index1)
    {
      int index2 = this._randomizePlays ? UnityEngine.Random.Range(0, playDataList.Count) : index1;
      this._playData.Add(playDataList[index2]);
      if (this._randomizePlays)
        playDataList.RemoveAt(index2);
    }
  }

  private void Update()
  {
    if (PauseScreen.isPaused != this._isPaused)
    {
      this._isPaused = PauseScreen.isPaused;
      foreach (HandData handData in this._throwManager.HandsDataModel.HandDatas)
        handData.EnableCatching(!this._isPaused);
    }
    this.GetClosestReceiver();
    if (!((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null))
      return;
    EligibilityManager.Instance.SetCurrentTarget(this._currentReceiver);
  }

  private void OnDestroy()
  {
    BallsContainerManager.IsEnabled.SetValue(false);
    this._state.OnTrainingStarted -= new System.Action(this.HandleTrainingStarted);
    this._state.OnExitTraining -= new System.Action(this.HandleExitTraining);
    this._throwManager.HandsDataModel.OnBallPicked -= new System.Action(this.HandlePickedUpBall);
    BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
    this._throwManager.OnThrowProcessed -= new Action<ThrowData>(this.HandleThrowProcessed);
    VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
    this._targets.SetCount(0);
    this._rings.SetCount(0);
  }

  private void HandlePickedUpBall() => this._pickedUpBall = true;

  private void HandleBallSpawned(BallObject obj)
  {
    if (this._pickedUpBall)
      return;
    this.StartPlay();
  }

  private void HandleTrainingStarted()
  {
    BallsContainerManager.CanSpawnBall.SetValue(true);
    MiniGameScoreState.ResetData();
    this._routeIndex = 0;
    this._currentPlayIndex = 0;
    this._throwManager.HandsDataModel.ResetHandsState();
    this.EndPlay();
    this.throwAwards = new PrecisionPassingGameFlow.AwardPoints[this._levelDataStore.RouteData.Length, this._numPlays];
    this.SetPlayDataAndPlayerPosition();
    this.SetupPlay();
    try
    {
      TackleDummyColorChanger.SetDummyColor(this._targets.Select<RouteRunningTarget, Renderer>((Func<RouteRunningTarget, Renderer>) (t => t.transform.Find("TackleDummy").GetComponent<Renderer>())).ToList<Renderer>());
    }
    catch
    {
      Debug.LogError((object) "Failed to Set TackleDummy Renderer Color");
    }
  }

  private void SetPlayDataAndPlayerPosition()
  {
    if (this._formationData != null)
    {
      PlaybookState.CurrentFormation.SetValue(this._formationData);
      this._playDataOff = (PlayDataOff) this._playData[this._currentPlayIndex];
    }
    else
      Console.Error.WriteLine("PrecisionPassingGameFlow._playDataOff is null");
    if (this._playDataOff != null)
      this._playerPosition = new Vector3(0.0f, 0.0f, (this._playDataOff.GetFormation().GetXLocations()[0] - 20f) * (float) global::Game.OffensiveFieldDirection);
    this._bucketTransform.position = this._playerPosition + this._bucketOffset;
    PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
    {
      this._bucketTransform
    }, 1);
    BallsContainerManager.CanSpawnBall.SetValue(true);
    PersistentSingleton<BallsContainerManager>.Instance.gameObject.SetActive(true);
  }

  private void SetupPlay()
  {
    VRState.LocomotionEnabled.SetValue(false);
    VREvents.BlinkMovePlayer.Trigger(1f, this._playerPosition, Quaternion.Euler(0.0f, 180f, 0.0f));
    if (this._playDataOff == null)
      return;
    if ((UnityEngine.Object) this._playImage != (UnityEngine.Object) null && (UnityEngine.Object) this._axisPlaysStore != (UnityEngine.Object) null)
    {
      this._playImage.gameObject.SetActive(true);
      this._axisPlaysStore.SetupPlayImage(this._playImage.spriteRenderer, (PlayData) this._playDataOff);
    }
    int count = this._playDataOff.GetFormation().GetReceiversInFormation() + this._playDataOff.GetFormation().GetTEsInFormation();
    this._targets.SetCount(count);
    this._rings.SetCount(count);
    this._receivers.Clear();
    this._receivers.AddRange(this._targets.Select<RouteRunningTarget, ReceiverUI>((Func<RouteRunningTarget, ReceiverUI>) (tar => tar.GetComponent<ReceiverUI>())));
    int i = 0;
    int index = 0;
    PrecisionPassingRouteData passingRouteData1 = new PrecisionPassingRouteData();
    PrecisionPassingRouteData passingRouteData2 = this._levelDataStore.RouteData.Find<PrecisionPassingRouteData>((Predicate<PrecisionPassingRouteData>) (routeData => routeData.PlayName == this._playData[this._currentPlayIndex].GetPlayName()));
    if (passingRouteData2 != null)
      passingRouteData1 = passingRouteData2;
    foreach (Position position1 in this._playDataOff.GetFormation().GetPositions())
    {
      switch (position1)
      {
        case Position.WR:
        case Position.SLT:
        case Position.TE:
          Vector2 vector2 = new Vector2(this._playDataOff.GetFormation().GetXLocations()[i], this._playDataOff.GetFormation().GetZLocations()[i]);
          PrecisionPassingRouteRunnerData passingRouteRunnerData = new PrecisionPassingRouteRunnerData();
          if (index < passingRouteData1.RouteRunnerData.Count)
            passingRouteRunnerData = passingRouteData1.RouteRunnerData[index];
          RouteRunningTarget target = this._targets[index];
          ThrowRing ring = this._rings[index];
          ring.SetScale(passingRouteRunnerData.Scale);
          ring.SetMedalColor(passingRouteRunnerData.MedalIndex);
          ring.SetRouteRunningTarget(target);
          target.SetThrowRingTarget(ring);
          target.YardLineAdjustment = 20f;
          target.SetUIVisibility(false);
          target.Score = (float) this.GetPointsForThrowAward(ring.Award);
          target.SetActiveForPoints(false);
          this._throwManager.RegisterTarget((IThrowTarget) target);
          target.gameObject.SetActive(true);
          Debug.Log((object) (passingRouteData1.PlayName + " - Position: " + position1.ToString() + "(" + i.ToString() + ") : " + passingRouteRunnerData.PrintDebug()));
          Vector3 euler = new Vector3(0.0f, 180f, 0.0f);
          target.transform.SetPositionAndRotation(new Vector3(-vector2.x, 0.0f, vector2.y - (float) (20 * global::Game.OffensiveFieldDirection)), Quaternion.Euler(euler));
          target.SetRouteData(this._playDataOff.GetRouteData(i));
          int num1 = passingRouteRunnerData.RouteSegmentIndexToSpawnRing - 1;
          float b1 = target.path[num1 * 3 + 1];
          float num2 = target.path[num1 * 3 + 2] * (float) global::Game.OffensiveFieldDirection;
          float a1 = num1 > 0 ? target.path[(num1 - 1) * 3 + 1] : b1;
          double a2 = num1 > 0 ? (double) target.path[(num1 - 1) * 3 + 2] * (double) global::Game.OffensiveFieldDirection : (double) num2;
          float num3 = Mathf.Lerp(a1, b1, passingRouteRunnerData.PercentToSpawnRingWithinRouteSegment);
          double b2 = (double) num2;
          double withinRouteSegment = (double) passingRouteRunnerData.PercentToSpawnRingWithinRouteSegment;
          float num4 = Mathf.Lerp((float) a2, (float) b2, (float) withinRouteSegment);
          float yoffset = passingRouteRunnerData.YOffset;
          float x = passingRouteRunnerData.XOffSet + num3;
          float z = passingRouteRunnerData.ZOffset + num4 - (float) (20 * global::Game.OffensiveFieldDirection);
          Vector3 position2 = new Vector3(x, yoffset, z);
          Quaternion rotation = Quaternion.LookRotation(new Vector3(x, 0.0f, z) - new Vector3(this._playerPosition.x, 0.0f, this._playerPosition.z));
          ring.transform.SetPositionAndRotation(position2, rotation);
          ++index;
          break;
      }
      ++i;
    }
    foreach (ReceiverUI receiver in this._receivers)
    {
      if ((UnityEngine.Object) receiver != (UnityEngine.Object) null)
        receiver.SetActiveUI(false);
    }
  }

  private void StartPlay()
  {
    VRState.LocomotionEnabled.SetValue(true);
    int num = 0;
    int index = 0;
    foreach (Position position in this._playDataOff.GetFormation().GetPositions())
    {
      switch (position)
      {
        case Position.WR:
        case Position.SLT:
        case Position.TE:
          this._targets[index].SetShouldRunRoute(true);
          ++index;
          break;
      }
      ++num;
    }
    this._flowRoutine.Run(this.TrainingFlow());
  }

  private IEnumerator TrainingFlow()
  {
    this._playIsActive = true;
    yield return (object) this.WaitForPlayCompleted(10f);
    if (!this.IncrementPlay())
    {
      this.EndPlay();
      this.SetPlayDataAndPlayerPosition();
      this.SetupPlay();
    }
  }

  private void HandleThrowProcessed(ThrowData throwData)
  {
    AppSounds.PlaySfx(ESfxTypes.kQBThrow);
    if (!(throwData.closestTarget is RouteRunningTarget))
      return;
    ((RouteRunningTarget) throwData.closestTarget).SetTarget(throwData.targetPosition, true);
  }

  private void HandleThrowResult(bool targetHit, float distance)
  {
    if (this._throwManager.HandsDataModel.HandDatas[0].hasObject || this._throwManager.HandsDataModel.HandDatas[1].hasObject || !this._playIsActive)
      return;
    this._pickedUpBall = false;
    if (this._waitForBallToSettle != null)
      this.StopCoroutine(this._waitForBallToSettle);
    this._waitForBallToSettle = this.StartCoroutine(this.WaitForBallToSettle());
  }

  public void UpdateThrowAwards(PrecisionPassingGameFlow.AwardPoints throwAward)
  {
    if (this._testMode)
      return;
    this._throwAwarded = true;
    this.throwAwards[this._routeIndex, this._currentPlayIndex] = throwAward;
  }

  public void UpdateThrowAwards(PrecisionPassingGameFlow.ThrowAward _award)
  {
    if (this._testMode)
      return;
    this._throwAwarded = true;
    PrecisionPassingGameFlow.AwardPoints awardPoints = new PrecisionPassingGameFlow.AwardPoints(_award, this.GetPointsForThrowAward(_award));
    this.throwAwards[this._routeIndex, this._currentPlayIndex] = awardPoints;
    MiniGameScoreState.MarkPlayResult(new MiniGameScoreState.MiniCampPassResult(this._currentPlayIndex, awardPoints.award));
  }

  private IEnumerator WaitForBallToSettle()
  {
    yield return (object) new WaitForSeconds(3f);
    if (this._throwManager.HandsDataModel.HandDatas[0].hasObject || this._throwManager.HandsDataModel.HandDatas[1].hasObject)
      this._waitForBallToSettle = (Coroutine) null;
    else if (!this._pickedUpBall)
    {
      this._playIsActive = false;
      this._waitForBallToSettle = (Coroutine) null;
    }
  }

  private IEnumerator WaitForPlayCompleted(float maxSeconds)
  {
    PrecisionPassingGameFlow precisionPassingGameFlow = this;
    while (precisionPassingGameFlow._playIsActive && (double) maxSeconds > 0.0 && !precisionPassingGameFlow.CheckRouteCompleted())
    {
      maxSeconds -= Time.deltaTime;
      yield return (object) null;
    }
    if (precisionPassingGameFlow._waitForBallToSettle != null)
    {
      precisionPassingGameFlow.StopCoroutine(precisionPassingGameFlow._waitForBallToSettle);
      precisionPassingGameFlow._waitForBallToSettle = (Coroutine) null;
    }
    precisionPassingGameFlow._playIsActive = false;
    if (!precisionPassingGameFlow._throwAwarded)
    {
      precisionPassingGameFlow.throwAwards[precisionPassingGameFlow._routeIndex, precisionPassingGameFlow._currentPlayIndex] = new PrecisionPassingGameFlow.AwardPoints(PrecisionPassingGameFlow.ThrowAward.Miss, precisionPassingGameFlow.GetPointsForThrowAward(PrecisionPassingGameFlow.ThrowAward.Miss));
      MiniGameScoreState.MarkPlayResult(new MiniGameScoreState.MiniCampPassResult(precisionPassingGameFlow._currentPlayIndex, PrecisionPassingGameFlow.ThrowAward.Miss));
    }
    precisionPassingGameFlow._throwAwarded = false;
    precisionPassingGameFlow._state.CalculateStarsEarned();
  }

  private bool CheckRouteCompleted() => false;

  private bool IncrementPlay()
  {
    if (!this._testMode && ++this._currentPlayIndex >= this._numPlays)
    {
      this.GameOver();
      return true;
    }
    foreach (ThrowRing ring in this._rings)
      ring.ResetState();
    return false;
  }

  private void EndPlay()
  {
    this._throwManager.HandsDataModel.ResetHandsState();
    this._pickedUpBall = false;
    PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
    {
      this._bucketTransform
    }, 1);
    BallsContainerManager.CanSpawnBall.SetValue(true);
    PersistentSingleton<BallsContainerManager>.Instance.gameObject.SetActive(true);
    MiniGameScoreState.AttemptsRemaining.SetValue(this._numPlays - this._currentPlayIndex);
    foreach (RouteRunningTarget target in this._targets)
    {
      this._throwManager.UnregisterTarget((IThrowTarget) target);
      target.SetShouldRunRoute(false);
      target.gameObject.SetActive(false);
    }
  }

  private bool CheckAllRingsHit()
  {
    foreach (RouteRunningTarget target in this._targets)
    {
      if (!target.CheckForSuccessfulPass())
        return false;
    }
    return true;
  }

  private void GameOver() => this._flowRoutine.Run(this.TrainingCompleteRoutine());

  private void HandleExitTraining()
  {
    this._playImage.transform.SetParent(this.transform);
    GameplayUI.HidePointer();
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
      if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null)
        this._currentReceiver.SetActiveUI(false);
      ReceiverUI currentReceiver = this._currentReceiver;
      this._currentReceiver = this._receivers[index1];
      this._currentReceiverIndex = index1;
      this._currentReceiverDot = num1;
      this._currentReceiver.SetActiveUI(ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.ShowReceiverHighlights);
      this.OnCurrentReceiverChanged(this._currentReceiver, currentReceiver);
    }
  }

  private void OnCurrentReceiverChanged(ReceiverUI currentReceiver, ReceiverUI lastReceiver)
  {
    if ((UnityEngine.Object) currentReceiver != (UnityEngine.Object) null)
    {
      RouteRunningTarget component = currentReceiver.gameObject.GetComponent<RouteRunningTarget>();
      if ((UnityEngine.Object) component.ThrowRingTarget != (UnityEngine.Object) null)
        component.ThrowRingTarget.SetTargeted(true);
    }
    if (!((UnityEngine.Object) lastReceiver != (UnityEngine.Object) null))
      return;
    RouteRunningTarget component1 = lastReceiver.gameObject.GetComponent<RouteRunningTarget>();
    if (!((UnityEngine.Object) component1.ThrowRingTarget != (UnityEngine.Object) null))
      return;
    component1.ThrowRingTarget.SetTargeted(false);
  }

  [Serializable]
  public enum ThrowAward
  {
    Miss,
    Gold,
    Silver,
    Bronze,
    Star,
  }

  [Serializable]
  public struct AwardPoints
  {
    public PrecisionPassingGameFlow.ThrowAward award;
    public int point;

    public AwardPoints(PrecisionPassingGameFlow.ThrowAward _award, int _point)
    {
      this.award = _award;
      this.point = _point;
    }
  }
}
