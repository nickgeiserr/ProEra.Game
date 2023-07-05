// Decompiled with JetBrains decompiler
// Type: FootballVR.BallsContainerManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using FootballWorld;
using Framework;
using Framework.Data;
using Photon.Pun;
using System;
using System.Collections.Generic;
using TB12;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class BallsContainerManager : PersistentSingleton<BallsContainerManager>
  {
    [SerializeField]
    private TeamBallMatStore _teamBallMatStore;
    [SerializeField]
    private PlayerProfile _playerProfile;
    [SerializeField]
    private BallObject _ballObjectPrefab;
    [SerializeField]
    private BallObject _ballObjectNetworkedPrefab;
    [SerializeField]
    private BallObject _ballObjectStausProPrefab;
    [SerializeField]
    private Material _defaultMiniCampBallMaterial;
    [SerializeField]
    private BallsContainer _ballsContainersPrefab;
    [SerializeField]
    private BallsContainerNetworked _ballsContainerNetworkedPrefab;
    [SerializeField]
    private bool _isNetworked;
    [SerializeField]
    private bool _hasStatusProBall;
    public static readonly VariableBool IsEnabled = new VariableBool(true);
    public static readonly VariableBool CanSpawnBall = new VariableBool();
    public static readonly AppEvent ClearBalls = new AppEvent();
    private MonoBehaviorObjectPool<BallObject> _ballPool;
    private MonoBehaviorObjectPool<BallObject> _statusProBallPool;
    private List<BallsContainer> _ballsContainersPool;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private BallsContainer _defaultBallsContainersPrefab;

    public static event Action<BallObject> OnBallSpawned;

    public bool HasStatusProBall
    {
      get => this._hasStatusProBall;
      set => this._hasStatusProBall = value;
    }

    public bool IsNetworked
    {
      get => this._isNetworked;
      set => this._isNetworked = value;
    }

    protected override void Awake()
    {
      base.Awake();
      this._defaultBallsContainersPrefab = this._ballsContainersPrefab;
      this._ballsContainersPool = new List<BallsContainer>();
      this._ballPool = new MonoBehaviorObjectPool<BallObject>(this._ballObjectPrefab, this.transform, 2);
      this._statusProBallPool = new MonoBehaviorObjectPool<BallObject>(this._ballObjectStausProPrefab, this.transform, 2);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        BallsContainerManager.CanSpawnBall.Link<bool>((Action<bool>) (value =>
        {
          for (int index = 0; index < this._ballsContainersPool.Count; ++index)
            this._ballsContainersPool[index].SetColliderActive(value);
        })),
        BallsContainerManager.IsEnabled.Link<bool>((Action<bool>) (state =>
        {
          this.gameObject.SetActive(state);
          if (state)
            return;
          this._ballPool.ClearAll();
        })),
        BallsContainerManager.ClearBalls.Link(new System.Action(this.Clear))
      });
    }

    public BallObject GetBall(bool isStatusProBall = false)
    {
      BallObject component;
      if (this._isNetworked)
        component = (BallObject) PhotonNetwork.Instantiate(this._ballObjectNetworkedPrefab.gameObject.name, Vector3.zero, Quaternion.identity).GetComponent<BallObjectNetworked>();
      else if (AppState.IsInMiniCamp())
      {
        component = this._statusProBallPool.GetObject();
        if (isStatusProBall)
          component.Graphics.BallMaterialID.SetValue(32);
        else
          component.Graphics.BallMaterial.SetValue(this._defaultMiniCampBallMaterial);
      }
      else
        component = this._ballPool.GetObject();
      AudioController.Play("sfx_mini_pickup_ball", this.transform);
      component.ResetState();
      return component;
    }

    public BallObject SpawnBall(Vector3 pos, bool isStatusProBall = false)
    {
      if (!(bool) BallsContainerManager.CanSpawnBall)
        return (BallObject) null;
      BallObject ball = this.GetBall(isStatusProBall);
      ball.transform.position = pos;
      Action<BallObject> onBallSpawned = BallsContainerManager.OnBallSpawned;
      if (onBallSpawned != null)
        onBallSpawned(ball);
      return ball;
    }

    public override void OnDestroy()
    {
      base.OnDestroy();
      this._linksHandler.Clear();
      this._ballPool?.ClearAll();
    }

    public void Clear()
    {
      for (int index = 0; index < this._ballsContainersPool.Count; ++index)
      {
        if (this._ballsContainersPool[index] is BallsContainerNetworked && PhotonNetwork.IsMasterClient)
          PhotonNetwork.Destroy(this._ballsContainersPool[index].gameObject);
        else
          UnityEngine.Object.Destroy((UnityEngine.Object) this._ballsContainersPool[index].gameObject);
      }
      this._ballsContainersPool.Clear();
      this._ballPool?.ClearAll();
    }

    private void CreateBallContainers(int count, BallContainerType type = BallContainerType.Normal, float ballDelayTime = 5f)
    {
      for (int index = 0; index < count; ++index)
      {
        BallsContainer ballsContainer = (BallsContainer) null;
        switch (type)
        {
          case BallContainerType.Normal:
            ballsContainer = UnityEngine.Object.Instantiate<BallsContainer>(this._ballsContainersPrefab, this.transform);
            break;
          case BallContainerType.Delayed:
            BallsContainerNetworked component;
            if (PhotonNetwork.Instantiate(this._ballsContainerNetworkedPrefab.name, this.transform.position, this.transform.rotation).TryGetComponent<BallsContainerNetworked>(out component))
            {
              component.SetDelayTime(ballDelayTime);
              ballsContainer = (BallsContainer) component;
              break;
            }
            break;
        }
        if (this._hasStatusProBall && (bool) (UnityEngine.Object) ballsContainer && index == count - 1)
          ballsContainer.SetIsStatusProBallContainer(true);
        this._ballsContainersPool.Add(ballsContainer);
      }
    }

    public void ResetPosition()
    {
      for (int index = 0; index < this._ballsContainersPool.Count; ++index)
        this._ballsContainersPool[index].ResetPosition();
    }

    public void SetPosition(Vector3 pos)
    {
      for (int index = 0; index < this._ballsContainersPool.Count; ++index)
      {
        this._ballsContainersPool[index].transform.position = pos;
        this._ballsContainersPool[index].gameObject.SetActive(false);
        if (index == 0)
        {
          this._ballsContainersPool[index].gameObject.SetActive(true);
          this._ballsContainersPool[index].SetBallCount(-1);
        }
      }
    }

    public void SetPositionAndRotation(Transform trans)
    {
      for (int index = 0; index < this._ballsContainersPool.Count; ++index)
      {
        this._ballsContainersPool[index].transform.position = trans.position;
        this._ballsContainersPool[index].transform.rotation = trans.rotation;
        this._ballsContainersPool[index].gameObject.SetActive(false);
        if (index == 0)
        {
          this._ballsContainersPool[index].gameObject.SetActive(true);
          this._ballsContainersPool[index].SetBallCount(-1);
        }
      }
    }

    public void InitializeBallContainers(
      Transform[] trans,
      int ballCount,
      BallContainerType type = BallContainerType.Normal,
      float delayOnBallBucket = 5f)
    {
      int count = trans.Length - this._ballsContainersPool.Count;
      if (count > 0)
        this.CreateBallContainers(count, type, delayOnBallBucket);
      for (int index = 0; index < this._ballsContainersPool.Count; ++index)
      {
        if (index < trans.Length)
        {
          this._ballsContainersPool[index].transform.position = trans[index].position;
          this._ballsContainersPool[index].transform.rotation = trans[index].rotation;
          this._ballsContainersPool[index].transform.localScale = trans[index].localScale;
          this._ballsContainersPool[index].gameObject.SetActive(true);
          this._ballsContainersPool[index].SetBallCount(ballCount);
        }
        else
          this._ballsContainersPool[index].gameObject.SetActive(false);
      }
    }

    public void SetBallsContainersPrefab(BallsContainer ballsContainer) => this._ballsContainersPrefab = ballsContainer;

    public void ResetBallsContainerPrefabToDefault() => this._ballsContainersPrefab = this._defaultBallsContainersPrefab;
  }
}
