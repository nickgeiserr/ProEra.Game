// Decompiled with JetBrains decompiler
// Type: TB12.BallThrower
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TB12
{
  public class BallThrower : MonoBehaviour, IBallThrower
  {
    [SerializeField]
    private AnimationConfiguration _animConfig;
    [SerializeField]
    private FootballVR.Avatar _avatar;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    private UniformStore _uniformStore;
    private bool _autoSpawnBall = true;
    private BehaviourController _behaviourController;
    private Vector3 _targetPos;
    private float _flightTime;

    public event Action<Transform, Vector3, float> OnBallThrown;

    public Vector3 position
    {
      get => this.transform.position;
      set => this.transform.position = value;
    }

    public bool isReady { get; private set; } = true;

    private void OnDisable() => this.OnBallThrown = (Action<Transform, Vector3, float>) null;

    private void OnDestroy() => this._uniformStore = (UniformStore) null;

    public void Initialize(Vector3 targetPos, bool autoSpawnBall)
    {
      this._uniformStore = SaveManager.GetUniformStore();
      this._autoSpawnBall = autoSpawnBall;
      this.InitPlayer(targetPos);
      this._animConfig.ApplyConfiguration(this._avatar.gameObject);
    }

    public void SetHighlight(bool state)
    {
    }

    public void ThrowToSpot(Vector3 targetPos, float flightTime, float throwDelay)
    {
      Vector3 eulerAngles = Quaternion.LookRotation((targetPos - this.transform.position).SetY(0.0f), Vector3.up).eulerAngles;
      this._behaviourController.AssumeOrientation(eulerAngles.y);
      this._targetPos = targetPos;
      this._flightTime = flightTime;
      EventData eventData = new EventData()
      {
        time = this._playbackInfo.PlayTime + throwDelay,
        orientation = eulerAngles.y,
        OnEventKeyMoment = new Action<object>(this.BallThrowDoneHandler)
      };
      this._behaviourController.SetGazeTarget((Transform) null);
      this._behaviourController.ThrowBall(eventData);
      this.isReady = false;
    }

    private void BallThrowDoneHandler(object data) => this.StartCoroutine(this.BallThrowRoutine((Transform) data));

    private IEnumerator BallThrowRoutine(Transform ballTransform)
    {
      Action<Transform, Vector3, float> onBallThrown = this.OnBallThrown;
      if (onBallThrown != null)
        onBallThrown(ballTransform, this._targetPos, this._flightTime);
      yield return (object) new WaitForSeconds(0.4f);
      if (this._autoSpawnBall)
        this.SpawnBall();
      this._behaviourController.SetGazeTarget(PlayerCamera.Camera.transform);
      this.isReady = true;
    }

    private void InitPlayer(Vector3 targetPos)
    {
      this._avatar.Graphics.avatarGraphicsData.baseMap.Value = this._uniformStore.GetRavensTexture();
      PlayerScenario instance = ScriptableObject.CreateInstance<PlayerScenario>();
      instance.startPosition = new Vector2(this.position.x, this.position.z);
      instance.Initialize();
      Vector3 eulerAngles = Quaternion.LookRotation((targetPos - this.transform.position).SetY(0.0f), Vector3.up).eulerAngles;
      this._behaviourController = this._avatar.behaviourController;
      this._behaviourController.playbackInfo = (IPlaybackInfo) this._playbackInfo;
      this._behaviourController.Scenario = instance;
      this._behaviourController.AssumeOrientation(eulerAngles.y);
      this._behaviourController.Reset();
      this._behaviourController.SetQuarterback(true);
      if (this._autoSpawnBall)
        this.SpawnBall();
      this._behaviourController.SetGazeTarget(PlayerCamera.Camera.transform);
      this._animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
      this.isReady = true;
    }

    [ContextMenu("SpawnBall")]
    private void SpawnBall()
    {
      BallObject ball = PersistentSingleton<BallsContainerManager>.Instance.GetBall();
      ball.Pick();
      this._avatar.behaviourController.GrabBall(ball.transform);
    }

    [SpecialName]
    Transform IBallThrower.get_transform() => this.transform;
  }
}
