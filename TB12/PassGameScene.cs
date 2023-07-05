// Decompiled with JetBrains decompiler
// Type: TB12.PassGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using System;
using System.Collections.Generic;
using TB12.GameplayData;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public class PassGameScene : MonoBehaviour
  {
    [SerializeField]
    private PlayerScenario _playerScenarioPrefab;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private FootballVR.Avatar _receiverPrefab;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    private UniformStore _uniformStore;
    [SerializeField]
    private AnimationConfiguration _animConfig;
    private readonly Vector3 _ballsContainerOffset = new Vector3(-0.23f, 0.0f, -1f);
    private ManagedList<FootballVR.Avatar> _avatars;
    public List<Transform> Receivers = new List<Transform>();
    private BallObject _activeBall;

    public IReadOnlyList<FootballVR.Avatar> Avatars => (IReadOnlyList<FootballVR.Avatar>) this._avatars;

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      this._avatars = new ManagedList<FootballVR.Avatar>((IObjectPool<FootballVR.Avatar>) new MonoBehaviorObjectPool<FootballVR.Avatar>(this._receiverPrefab, this.transform));
      BallsContainerManager.IsEnabled.SetValue(true);
      BallsContainerManager.OnBallSpawned += new Action<BallObject>(this.HandleBallSpawned);
    }

    private void HandleBallSpawned(BallObject obj)
    {
      this._activeBall = obj;
      UIDispatch.FrontScreen.HideView(EScreens.kIntroduction);
    }

    private void OnDestroy()
    {
      this.CleanupScene();
      BallsContainerManager.IsEnabled.SetValue(false);
      BallsContainerManager.OnBallSpawned -= new Action<BallObject>(this.HandleBallSpawned);
      this._uniformStore = (UniformStore) null;
    }

    public void CleanupScene()
    {
      this.Receivers.Clear();
      this._throwManager.Clear();
      this._avatars.SetCount(0);
    }

    public void InitializeScene(List<ReceiverRoute> routes, int levelYardLine)
    {
      BallsContainerManager.CanSpawnBall.SetValue(true);
      PersistentSingleton<BallsContainerManager>.Instance.SetPosition(PersistentSingleton<GamePlayerController>.Instance.transform.position + this._ballsContainerOffset);
      this._avatars.SetCount(routes.Count);
      Vector2 vector2 = new Vector2(Utilities.YardsToMeters((float) (levelYardLine - 50)), 0.0f);
      this._uniformStore.SetNamesAndNumbersVisibility(false);
      float num = 0.0f;
      for (int index = 0; index < routes.Count; ++index)
      {
        ReceiverRoute route = routes[index];
        FootballVR.Avatar avatar = this._avatars[index];
        avatar.Graphics.avatarGraphicsData.baseMap.Value = this._uniformStore.GetRavensTexture();
        BehaviourController behaviourController = avatar.behaviourController;
        this._animConfig.ApplyConfiguration(behaviourController.gameObject);
        ReceiverRouteAction instance = ScriptableObject.CreateInstance<ReceiverRouteAction>();
        instance.points = route.points;
        instance.eligibility = route.receiverOpen;
        instance.speed = 5f;
        PlayerScenario scenario = UnityEngine.Object.Instantiate<PlayerScenario>(this._playerScenarioPrefab);
        scenario.startPosition = Utilities.YardsToGamePosVec2(route.startPoint + vector2);
        scenario.actionList.Add((ScriptableObject) instance);
        scenario.actionList.Add((ScriptableObject) ScriptableObject.CreateInstance<AssumeStanceAction>());
        behaviourController.InitializeScenario(scenario);
        behaviourController.playbackInfo = (IPlaybackInfo) this._playbackInfo;
        behaviourController.Reset();
        this._throwManager.RegisterTarget((IThrowTarget) new PlayerThrowTarget(behaviourController, this._playbackInfo));
        num = Mathf.Max(num, scenario.time);
      }
      this._playbackInfo.Setup(0.0f, num);
      this.Receivers.Clear();
      foreach (Component avatar in this._avatars)
        this.Receivers.Add(avatar.transform);
    }

    public void HideActiveBall()
    {
      if (!((UnityEngine.Object) this._activeBall != (UnityEngine.Object) null))
        return;
      this._activeBall.gameObject.SetActive(false);
    }
  }
}
