// Decompiled with JetBrains decompiler
// Type: TB12.OpponentQB
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballVR.Multiplayer;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.GameplayData;
using UnityEngine;

namespace TB12
{
  public class OpponentQB : MonoBehaviour
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private GameplayStore _store;
    [SerializeField]
    private PlayerName _playerNamePrefab;
    [SerializeField]
    private FootballVR.Avatar _avatar;
    [SerializeField]
    private BallThrower _thrower;
    [SerializeField]
    private float _hitRange = 0.1f;
    private PlayerName _playerName;
    private ThrowLevel _level;
    private readonly RoutineHandle _qbRoutine = new RoutineHandle();
    private const float _throwDelay = 0.8f;
    private bool _qbShown;
    private QbData _qbData;
    private OpponentData _data;
    private int _maxHits;
    private IThrowTarget _currentTarget;
    private Transform _qbPosition;
    private float _maxDelay = 0.8f;
    private UniformStore _uniformStore;
    private bool _init;

    public bool Done { get; private set; }

    private void Initialize()
    {
      if (this._init)
        return;
      this._uniformStore = SaveManager.GetUniformStore();
      this._init = true;
      this.gameObject.SetActive(false);
      this._playerName = UnityEngine.Object.Instantiate<PlayerName>(this._playerNamePrefab, this._avatar.transform);
      this._playerName.Initialize(this._avatar.HeadTx);
    }

    private void OnEnable() => this.Done = false;

    private void OnDestroy()
    {
      this._uniformStore = (UniformStore) null;
      if (!((UnityEngine.Object) this._thrower != (UnityEngine.Object) null))
        return;
      this._thrower.OnBallThrown -= new Action<Transform, Vector3, float>(this.HandleBallThrown);
    }

    public void SetupQb(
      ThrowLevel level,
      OpponentData data,
      Transform qbPos,
      QbData qbData,
      int playerIndex,
      float maxDelay = 0.8f)
    {
      if (level == null || data == null || qbData == null)
        return;
      this.Initialize();
      this._data = data;
      this._level = level;
      this._qbPosition = qbPos;
      this._qbData = qbData;
      this._maxDelay = maxDelay;
      this._avatar.gameObject.SetActive(false);
      this._qbShown = true;
      this._avatar.Appear(delay: 0.0f);
      this.transform.SetParentAndReset(qbPos);
      this._thrower.Initialize(this._qbPosition.position + this._qbPosition.forward * 100f, false);
      this._thrower.OnBallThrown += new Action<Transform, Vector3, float>(this.HandleBallThrown);
      this._playerName.SetName(this._qbData.lastName);
      FootballWorld.UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(this._qbData.team, ETeamUniformFlags.Home);
      UniformCapture.Info info = new UniformCapture.Info()
      {
        BaseMap = uniformConfig.BasemapAlternative,
        PlayerIndex = playerIndex
      };
      info.TextsAtlas = (Texture[]) UniformCapture.UpdateMultiplayerUniform(playerIndex, this._qbData.team, this._qbData.number, this._qbData.name, ETeamUniformFlags.Home);
      this._avatar.Graphics.avatarGraphicsData.uniformCaptureInfo.Value = info;
      this._avatar.Graphics.SetLod(false);
      this._qbRoutine.Run(this.QbThrowSequence());
    }

    private IEnumerator QbThrowSequence()
    {
      OpponentQB opponentQb = this;
      opponentQb.Done = false;
      opponentQb._data.ResetData(opponentQb._level.totalThrows);
      opponentQb._maxHits = opponentQb._level.throwsToWin - 1;
      yield return (object) new WaitForSecondsRealtime(0.2f);
      yield return (object) null;
      yield return (object) null;
      while ((int) opponentQb._data.BallsThrown < opponentQb._level.totalThrows)
      {
        BallObject ball = PersistentSingleton<BallsContainerManager>.Instance.GetBall();
        ball.Pick();
        opponentQb._avatar.behaviourController.GrabBall(ball.transform);
        while ((int) opponentQb._store.BallsThrown <= (int) opponentQb._data.BallsThrown)
          yield return (object) null;
        WaitForSeconds delay = new WaitForSeconds(UnityEngine.Random.Range(0.3f, opponentQb._maxDelay));
        do
        {
          yield return (object) delay;
          opponentQb._currentTarget = opponentQb._throwManager.ThrowTargets.GetRandom<IThrowTarget>((Func<IThrowTarget, bool>) (x => x.TargetValidForAI && x.IsTargetValid(1.5f)));
        }
        while (opponentQb._currentTarget == null);
        float throwDelay = 0.8f + UnityEngine.Random.Range(0.0f, 0.3f);
        float flightTime = Mathf.Lerp(0.6f, 1.2f, (opponentQb._currentTarget.EvaluatePosition(0.0f) - opponentQb._avatar.transform.position).magnitude / 40f) + UnityEngine.Random.Range(0.0f, 0.2f);
        Vector3 position = opponentQb._currentTarget.EvaluatePosition(flightTime + throwDelay);
        opponentQb._thrower.ThrowToSpot(position, flightTime, throwDelay);
        // ISSUE: reference to a compiler-generated method
        yield return (object) new WaitUntil(new Func<bool>(opponentQb.\u003CQbThrowSequence\u003Eb__27_1));
        yield return (object) new WaitForSeconds(flightTime);
        delay = (WaitForSeconds) null;
      }
      opponentQb.Done = true;
    }

    private void HandleBallThrown(Transform spawnTx, Vector3 targetPos, float flightTime)
    {
      int num = this._level.totalThrows - (int) this._data.BallsThrown <= this._maxHits - (int) this._data.BallsHit ? 1 : 0;
      bool flag = (int) this._data.BallsHit < this._maxHits && UnityEngine.Random.Range(0, 10) < 4;
      if (num == 0)
      {
        if (flag)
          targetPos += UnityEngine.Random.onUnitSphere * this._hitRange;
        else
          targetPos = this.GetFailingThrowPos(spawnTx.position, targetPos, flightTime);
      }
      ++this._data.BallsThrown;
      BallObject component = spawnTx.GetComponent<BallObject>();
      component.transform.SetPositionAndRotation(spawnTx.position, spawnTx.rotation);
      component.Graphics.customBallColor = true;
      component.ThrowToPosition(targetPos, flightTime, true);
      component.Graphics.TrailColor = this._qbData.trailColor;
      component.userThrown = false;
    }

    private Vector3 GetFailingThrowPos(Vector3 startPos, Vector3 actualTargetPos, float flightTime)
    {
      if (this._currentTarget == null)
      {
        Debug.LogError((object) "Null target. Returning original throw pos..");
        return actualTargetPos;
      }
      PracticeTarget currentTarget = this._currentTarget as PracticeTarget;
      if ((UnityEngine.Object) currentTarget == (UnityEngine.Object) null)
        return actualTargetPos;
      Vector3 impulseToHitTarget1 = AutoAim.GetImpulseToHitTarget(actualTargetPos - startPos, flightTime);
      float flightTime1 = AutoAim.GetFlightTime(startPos, impulseToHitTarget1, 0.1f);
      ThrowData throwData = new ThrowData()
      {
        startPosition = startPos,
        timeStep = 0.02f,
        maxDistance = 2f,
        flightTime = flightTime1
      };
      float num1 = currentTarget.hitRange * 2.1f;
      int num2 = 7;
      int num3 = 0;
      Vector3 failingThrowPos = actualTargetPos;
      do
      {
        ++num3;
        if (num3 > num2)
        {
          Debug.LogWarning((object) "Failed to get a failing throw direction..");
          return failingThrowPos;
        }
        throwData.hasTarget = false;
        Vector2 vector2 = UnityEngine.Random.insideUnitCircle.normalized / currentTarget.transform.lossyScale.x;
        failingThrowPos = currentTarget.transform.TransformPoint((Vector3) (vector2 * num1));
        num1 += 0.25f;
        Vector3 impulseToHitTarget2 = AutoAim.GetImpulseToHitTarget(failingThrowPos - throwData.startPosition, flightTime);
        if ((double) impulseToHitTarget2.y < 0.0)
          impulseToHitTarget2.y = 0.0f;
        throwData.throwVector = impulseToHitTarget2;
      }
      while (AutoAim.HitsAnyTarget(throwData, (IEnumerable<IThrowTarget>) this._throwManager.ThrowTargets));
      return failingThrowPos;
    }

    public void CleanupScene()
    {
      this._qbRoutine.Stop();
      if (!this._qbShown)
        return;
      this._avatar.Disappear();
    }
  }
}
