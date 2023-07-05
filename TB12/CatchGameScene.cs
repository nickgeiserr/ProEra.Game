// Decompiled with JetBrains decompiler
// Type: TB12.CatchGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections.Generic;
using TB12.GameplayData;
using UnityEngine;
using UnityEngine.Serialization;

namespace TB12
{
  public class CatchGameScene : MonoBehaviour
  {
    [SerializeField]
    private BallObject _ballObjectPrefab;
    [FormerlySerializedAs("_ballThrowerPrefab")]
    [SerializeField]
    private BallThrower _playerAvatarPrefab;
    [SerializeField]
    private JugsMachine _jugsMachinePrefab;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private WorldPopupText _scoreText;
    [SerializeField]
    private WorldPopupText _comboText;
    [SerializeField]
    private bool _trailEnabled;
    private MonoBehaviorObjectPool<BallObject> _ballPool;
    private Transform _myTx;
    private Transform _camTx;

    public IReadOnlyList<IBallThrower> BallThrowers { get; private set; }

    private ManagedList<BallThrower> PlayerAvatars { get; set; }

    private ManagedList<JugsMachine> JugMachines { get; set; }

    public event Action<BallObject> OnBallThrown;

    private void Awake()
    {
      this._myTx = this.transform;
      this._camTx = PlayerCamera.Camera.transform;
      Transform transform1 = new GameObject("BallPool").transform;
      transform1.parent = this._myTx;
      this._ballPool = new MonoBehaviorObjectPool<BallObject>(this._ballObjectPrefab, transform1);
      Transform transform2 = new GameObject("BallThrowerPool").transform;
      transform2.parent = this._myTx;
      this.PlayerAvatars = new ManagedList<BallThrower>((IObjectPool<BallThrower>) new MonoBehaviorObjectPool<BallThrower>(this._playerAvatarPrefab, transform2));
      this.JugMachines = new ManagedList<JugsMachine>((IObjectPool<JugsMachine>) new MonoBehaviorObjectPool<JugsMachine>(this._jugsMachinePrefab, transform2));
      this._scoreText.gameObject.SetActive(false);
      this._comboText.gameObject.SetActive(false);
    }

    public void InitializeProfile(CatchChallenge challenge)
    {
      this.Stop();
      this._playbackInfo.Setup(0.0f, float.PositiveInfinity);
      this._playbackInfo.StartPlayback();
      int num1 = challenge.fromBehind ? challenge.emittersCount * 2 : challenge.emittersCount;
      bool flag = challenge.emitterType == "Machine";
      this.JugMachines.SetCount(flag ? num1 : 0);
      this.PlayerAvatars.SetCount(flag ? 0 : num1);
      this.BallThrowers = flag ? (IReadOnlyList<IBallThrower>) this.JugMachines : (IReadOnlyList<IBallThrower>) this.PlayerAvatars;
      foreach (IBallThrower ballThrower in (IEnumerable<IBallThrower>) this.BallThrowers)
        ballThrower.OnBallThrown += new Action<Transform, Vector3, float>(this.HandleBallThrown);
      Vector3 position1 = this._myTx.position;
      Vector3 vector3_1 = this._myTx.forward * challenge.distance;
      float num2 = (float) -((double) (challenge.emittersCount - 1) * (double) challenge.emittersAngle / 2.0);
      Vector3 position2 = PersistentSingleton<PlayerCamera>.Instance.transform.position;
      for (int index = 0; index < challenge.emittersCount; ++index)
      {
        Vector3 vector3_2 = Quaternion.Euler((num2 + (float) index * challenge.emittersAngle) * Vector3.up) * vector3_1;
        this.Setup(this.BallThrowers[index], position1 + vector3_2, position2);
        if (challenge.fromBehind)
          this.Setup(this.BallThrowers[challenge.emittersCount + index], position1 - vector3_2, position2);
      }
    }

    public void DisplayScore(Vector3 pos, float score)
    {
      this._scoreText.transform.position = pos;
      this._scoreText.transform.LookAt(2f * pos - this._camTx.position, Vector3.up);
      this._scoreText.Display(string.Format("+{0}", (object) score));
    }

    public void DisplayCombo(int combo) => this._comboText.Display(string.Format("x{0}", (object) combo));

    public void HideAllHighlights()
    {
      foreach (IBallThrower ballThrower in (IEnumerable<IBallThrower>) this.BallThrowers)
        ballThrower.SetHighlight(false);
    }

    public void ShowAllHighlights()
    {
      foreach (IBallThrower ballThrower in (IEnumerable<IBallThrower>) this.BallThrowers)
        ballThrower.SetHighlight(true);
    }

    private void Stop(bool exitingScene = false)
    {
      if (!exitingScene)
        this._ballPool.ReturnAllObjects();
      if (this.BallThrowers == null)
        return;
      foreach (IBallThrower ballThrower in (IEnumerable<IBallThrower>) this.BallThrowers)
        ballThrower.OnBallThrown -= new Action<Transform, Vector3, float>(this.HandleBallThrown);
    }

    private void Setup(IBallThrower thrower, Vector3 position, Vector3 targetPos)
    {
      thrower.position = position;
      thrower.Initialize(targetPos, true);
    }

    private void HandleBallThrown(Transform spawnTx, Vector3 destination, float flightTime)
    {
      AppSounds.Play3DSfx(ESfxTypes.kMiniFiringMachine, spawnTx);
      BallObject ballObject = this._ballPool.GetObject();
      ballObject.transform.SetPositionAndRotation(spawnTx.position, spawnTx.rotation);
      ballObject.ThrowToPosition(destination, flightTime, this._trailEnabled);
      Action<BallObject> onBallThrown = this.OnBallThrown;
      if (onBallThrown == null)
        return;
      onBallThrown(ballObject);
    }

    private void OnDestroy()
    {
      this.Stop(true);
      foreach (BallObject allocatedObject in this._ballPool.AllocatedObjects)
      {
        if (!((UnityEngine.Object) allocatedObject == (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) allocatedObject.gameObject);
      }
    }
  }
}
