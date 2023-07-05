// Decompiled with JetBrains decompiler
// Type: MiniCampRunAndShootFlow
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
using TB12;
using TB12.AppStates.MiniCamp.RunAndShoot;
using TB12.UI;
using TB12.UI.Screens;
using UnityEngine;

public class MiniCampRunAndShootFlow : MonoBehaviour
{
  [SerializeField]
  private MiniCampRunAndShootGameState _state;
  [SerializeField]
  private MiniCampRunAndShootScene _scene;
  [SerializeField]
  private GameplayStore _store;
  [SerializeField]
  private ThrowManager _throwManager;
  [SerializeField]
  private BallsContainer _qbteePrefab;
  [SerializeField]
  private float _corridorLength = 11f;
  private bool _pickedUpBall;
  private bool _playbackFinished;
  private readonly RoutineHandle _routineHandle = new RoutineHandle();
  private LinksHandler _linksHandler = new LinksHandler();
  [SerializeField]
  private RunAndShootLevel[] _levels;
  private RunAndShootLevel _level;
  [SerializeField]
  private bool _debugLevels;
  [SerializeField]
  private int _debugLevel = 1;
  private int _currentLevel;
  private RunAndShootLayout _currentLevelLayout;
  [SerializeField]
  private Transform _playerStartTransform;
  private int _currentLayoutIndex;
  [SerializeField]
  private int _ballsPerBallContainer = 1;
  [SerializeField]
  private int _ballsPerLayout = 5;
  private int _ballsThrown;
  [SerializeField]
  private float _maxTime = 60f;
  private float _timeRemaining;
  private bool _gameStarted;
  [SerializeField]
  private MiniCampTourStore _miniCampTourStore;
  private List<BallObject> _spawnedBalls = new List<BallObject>();
  private Coroutine _corridorRoutine;
  private bool _isPaused;

  public float TimeRemaining => this._timeRemaining;

  private void Awake()
  {
    BallsContainerManager.IsEnabled.SetValue(true);
    BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
    PersistentSingleton<BallsContainerManager>.Instance.SetBallsContainersPrefab(this._qbteePrefab);
    this._throwManager.OnThrowProcessed += new Action<ThrowData>(this.HandleThrowProcessed);
    VREvents.ThrowResult.OnTrigger += new Action<bool, float>(this.HandleThrowResult);
    this._state.OnTrainingStarted += new System.Action(this.StartTraining);
    this._state.OnExitTraining += new System.Action(this.StopTraining);
    PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
  }

  private void HandleThrowResult(bool hitTarget, float distance) => this._state.CalculateStarsEarned();

  private void HandleThrowProcessed(ThrowData throwData)
  {
    Debug.Log((object) "MCRASF: HandleThrowProcessed");
    AppSounds.PlaySfx(ESfxTypes.kQBThrow);
    if ((bool) (UnityEngine.Object) throwData.ball && throwData.ball.Graphics.IsStatusProBall)
      MiniGameScoreState.ComboModifier = 2;
    else
      MiniGameScoreState.ComboModifier = 1;
  }

  private IEnumerator MoveToNextLayoutRoutine()
  {
    // ISSUE: reference to a compiler-generated field
    int num1 = this.\u003C\u003E1__state;
    MiniCampRunAndShootFlow campRunAndShootFlow1 = this;
    if (num1 != 0)
    {
      if (num1 != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      MiniCampRunAndShootFlow campRunAndShootFlow2 = campRunAndShootFlow1;
      MiniCampRunAndShootFlow campRunAndShootFlow3 = campRunAndShootFlow1;
      int num2 = campRunAndShootFlow1._currentLayoutIndex + 1;
      int num3 = num2;
      campRunAndShootFlow3._currentLayoutIndex = num3;
      int newLevelIndex = num2;
      campRunAndShootFlow2.SetCurrentLevelIndex(newLevelIndex);
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    campRunAndShootFlow1._spawnedBalls.Clear();
    campRunAndShootFlow1._ballsThrown = 0;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(1f);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  private void HandleBallSpawned(BallObject obj)
  {
    this._spawnedBalls.Add(obj);
    this._gameStarted = true;
    this._pickedUpBall = true;
    if (!((UnityEngine.Object) this._currentLevelLayout != (UnityEngine.Object) null))
      return;
    this._currentLevelLayout.SetLayoutActive(true);
  }

  private void OnDestroy()
  {
    BallsContainerManager.IsEnabled.SetValue(false);
    BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
    VREvents.ThrowResult.OnTrigger -= new Action<bool, float>(this.HandleThrowResult);
    this._state.OnTrainingStarted -= new System.Action(this.StartTraining);
    this._state.OnExitTraining -= new System.Action(this.StopTraining);
    this._linksHandler.Clear();
  }

  private void Update()
  {
    if (this._gameStarted)
    {
      if ((double) this._timeRemaining > 0.0)
      {
        this._timeRemaining -= Time.deltaTime;
        MiniGameScoreState.AttemptsRemaining.SetValue(Mathf.FloorToInt(this._timeRemaining));
        if (this._spawnedBalls.Count != this._ballsPerLayout || !this._spawnedBalls.TrueForAll((Predicate<BallObject>) (ball => !ball.inFlight && !ball.inHand && ball.userThrown)))
          return;
        this.StartCoroutine(this.MoveToNextLayoutRoutine());
      }
      else
        this.GameOver();
    }
    if (PauseScreen.isPaused == this._isPaused)
      return;
    this._isPaused = PauseScreen.isPaused;
    BallsContainerManager.CanSpawnBall.SetValue(!this._isPaused);
    foreach (HandData handData in this._throwManager.HandsDataModel.HandDatas)
      handData.EnableCatching(!this._isPaused);
  }

  private void StartTraining()
  {
    this.ResetState();
    this.SetCurrentLevelIndex(this._currentLayoutIndex);
    if (this._corridorRoutine != null)
      this.StopCoroutine(this._corridorRoutine);
    this._corridorRoutine = this.StartCoroutine(this.BoundedBoxRoutine());
  }

  private IEnumerator BoundedBoxRoutine()
  {
    MiniCampRunAndShootFlow campRunAndShootFlow1 = this;
    if (!(bool) (UnityEngine.Object) campRunAndShootFlow1._level || campRunAndShootFlow1._level.LevelLayouts.Length == 0)
    {
      Debug.LogError((object) "Broke loop due to null _level");
    }
    else
    {
      Vector3 miniWallPosition1 = campRunAndShootFlow1._level.LevelLayouts[0].GetMiniWallPosition();
      PersistentSingleton<GamePlayerController>.Instance.SetNewBoundingRect(new Rect(miniWallPosition1.x - Field.FIVE_YARDS, miniWallPosition1.z - Field.ONE_YARD, Field.TEN_YARDS, Field.TEN_YARDS + Field.TWO_YARDS));
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitWhile(new Func<bool>(campRunAndShootFlow1.\u003CBoundedBoxRoutine\u003Eb__38_0));
      while (campRunAndShootFlow1._gameStarted)
      {
        MiniCampRunAndShootFlow campRunAndShootFlow = campRunAndShootFlow1;
        if (!(bool) (UnityEngine.Object) campRunAndShootFlow1._level || campRunAndShootFlow1._level.LevelLayouts.Length <= campRunAndShootFlow1._currentLayoutIndex)
        {
          Debug.LogError((object) "Broke loop due to index out of bounds");
          break;
        }
        int localLevelIndex = campRunAndShootFlow1._currentLayoutIndex;
        RunAndShootLayout levelLayout = campRunAndShootFlow1._level.LevelLayouts[campRunAndShootFlow1._currentLayoutIndex - 1];
        if (!(bool) (UnityEngine.Object) campRunAndShootFlow1._currentLevelLayout || !(bool) (UnityEngine.Object) levelLayout)
        {
          Debug.LogError((object) "Broke loop due to null _currentLevelLayout or null priorMiniWall");
          break;
        }
        Vector3 miniWallPosition2 = campRunAndShootFlow1._currentLevelLayout.GetMiniWallPosition();
        PersistentSingleton<GamePlayerController>.Instance.SetNewBoundingRect(new Rect(levelLayout.GetMiniWallPosition().x + miniWallPosition2.x - Field.FIVE_YARDS, miniWallPosition2.z - Field.ONE_YARD, Field.TEN_YARDS, Field.TEN_YARDS + campRunAndShootFlow1._corridorLength));
        yield return (object) new WaitUntil((Func<bool>) (() => campRunAndShootFlow._currentLayoutIndex != localLevelIndex));
      }
      campRunAndShootFlow1._corridorRoutine = (Coroutine) null;
      yield return (object) null;
    }
  }

  private void SetCurrentLevelIndex(int newLevelIndex)
  {
    if ((UnityEngine.Object) this._currentLevelLayout != (UnityEngine.Object) null)
      this._currentLevelLayout.SetLayoutActive(false);
    this._currentLayoutIndex = newLevelIndex;
    if (this._currentLayoutIndex >= 0 && this._currentLayoutIndex < this._levels[this._currentLevel].LevelLayouts.Length)
    {
      this._currentLevelLayout = this._level.LevelLayouts[this._currentLayoutIndex];
      this._currentLevelLayout.SetLayoutBucketActive(this._ballsPerBallContainer);
    }
    else
      this.GameOver();
  }

  private void GameOver()
  {
    PersistentSingleton<BallsContainerManager>.Instance.Clear();
    this._gameStarted = false;
    this.StartCoroutine(this.GameOverRoutine());
    BallsContainerManager.CanSpawnBall.SetValue(false);
  }

  private IEnumerator GameOverRoutine()
  {
    yield return (object) new WaitForSeconds(1.5f);
    AppSounds.PlayStinger(EStingerType.kStinger2);
    this._state.ShowEndOfMinicampScreen();
  }

  private void StopTraining()
  {
    this._routineHandle.Stop();
    if ((UnityEngine.Object) this._level != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this._level);
    GameplayUI.Hide();
  }

  private void ResetState()
  {
    PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(this._playerStartTransform.position, this._playerStartTransform.rotation);
    VRState.LocomotionEnabled.SetValue(true);
    this._currentLevel = this._debugLevels ? this._debugLevel : PersistentSingleton<SaveManager>.Instance.miniCamp.SelectedEntry.Level - 1;
    if ((UnityEngine.Object) this._level != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this._level);
    this._level = UnityEngine.Object.Instantiate<RunAndShootLevel>(this._levels[this._currentLevel]);
    this._pickedUpBall = false;
    this._timeRemaining = this._maxTime;
    this._gameStarted = false;
    this._currentLayoutIndex = 0;
    this._ballsThrown = 0;
    this._spawnedBalls.Clear();
  }

  private void HandlePickedUpBall() => this._pickedUpBall = true;
}
