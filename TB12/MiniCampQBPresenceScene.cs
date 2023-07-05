// Decompiled with JetBrains decompiler
// Type: TB12.MiniCampQBPresenceScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class MiniCampQBPresenceScene : MonoBehaviour
  {
    [SerializeField]
    private GameObject _throwTargetsPrefab;
    [SerializeField]
    private BallObject _ballObjectPrefab;
    [SerializeField]
    private GameObject _jugMachineContainerObject;
    [SerializeField]
    private Transform _bucketTransform;
    private List<PracticeTarget> _targets;
    private List<JugsMachine> _jugs;
    private readonly Vector3 _ballsContainerOffset = new Vector3(-0.23f, 0.0f, -1f);
    private BallObject _activeBall;
    private MonoBehaviorObjectPool<BallObject> _ballPool;

    public Vector3 BallsContainerOffset => this._ballsContainerOffset;

    public List<PracticeTarget> Targets => this._targets;

    public List<JugsMachine> JugsMachines => this._jugs;

    public bool Done => true;

    private void Awake()
    {
      this._targets = new List<PracticeTarget>();
      this._jugs = new List<JugsMachine>();
      Transform transform = new GameObject("BallPool").transform;
      transform.parent = this.transform;
      this._ballPool = new MonoBehaviorObjectPool<BallObject>(this._ballObjectPrefab, transform);
    }

    private void HandleBallSpawned(BallObject obj) => this._activeBall = obj;

    public void HideActiveBall()
    {
      if (!((UnityEngine.Object) this._activeBall != (UnityEngine.Object) null))
        return;
      this._activeBall.gameObject.SetActive(false);
    }

    public void CleanupScene()
    {
      foreach (JugsMachine jug in this._jugs)
        jug.OnBallThrown -= new Action<Transform, Vector3, float>(this.HandleBallThrown);
      this._jugs.Clear();
    }

    public void InitializeScene()
    {
      PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
      {
        this._bucketTransform
      }, 1);
      BallsContainerManager.CanSpawnBall.SetValue(true);
      this._targets.Clear();
      foreach (PracticeTarget componentsInChild in this._throwTargetsPrefab.GetComponentsInChildren<PracticeTarget>())
        this._targets.Add(componentsInChild);
      foreach (JugsMachine componentsInChild in this._jugMachineContainerObject.GetComponentsInChildren<JugsMachine>())
      {
        this._jugs.Add(componentsInChild);
        componentsInChild.Initialize(PersistentSingleton<GamePlayerController>.Instance.position, false);
        componentsInChild.OnBallThrown += new Action<Transform, Vector3, float>(this.HandleBallThrown);
      }
    }

    public void ResetBallsContainer() => PersistentSingleton<BallsContainerManager>.Instance.InitializeBallContainers(new Transform[1]
    {
      this._bucketTransform
    }, 1);

    private void HandleBallThrown(Transform spawnTx, Vector3 destination, float flightTime)
    {
      AppSounds.Play3DSfx(ESfxTypes.kMiniFiringMachine, spawnTx);
      BallObject ballObject = this._ballPool.GetObject();
      ballObject.transform.SetPositionAndRotation(spawnTx.position, spawnTx.rotation);
      ballObject.ThrowToPosition(destination, flightTime, true);
    }
  }
}
