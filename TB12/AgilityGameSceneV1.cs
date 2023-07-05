// Decompiled with JetBrains decompiler
// Type: TB12.AgilityGameSceneV1
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using System.Collections.Generic;
using TB12.GameplayData;
using UnityEngine;

namespace TB12
{
  public class AgilityGameSceneV1 : MonoBehaviour
  {
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private BallObject _ballPrefab;
    [SerializeField]
    private FootballVR.Avatar _centerPlayer;
    [SerializeField]
    private PlayerScenario _centerScenario;
    [SerializeField]
    private PlayerScenario _scenarioPrefab;
    [SerializeField]
    private FootballVR.Avatar _avatarPrefab;
    [SerializeField]
    private AnimationConfiguration _animConfig;
    [SerializeField]
    private TimedTarget _target;
    [SerializeField]
    private WorldPopupText _scoreText;
    [SerializeField]
    private WorldPopupText _comboText;
    private MonoBehaviorObjectPool<FootballVR.Avatar> _avatars;
    private MonoBehaviorObjectPool<BallObject> _ballPool;
    private Transform _cameraTx;
    private UniformStore _uniformStore;
    private float prevAngle = -180f;

    public MonoBehaviorObjectPool<FootballVR.Avatar> Avatars => this._avatars;

    public FootballVR.Avatar CenterPlayer => this._centerPlayer;

    public TimedTarget Target => this._target;

    public event System.Action OnAttackerMissed;

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      this._avatars = new MonoBehaviorObjectPool<FootballVR.Avatar>(this._avatarPrefab, this.transform);
      this._scoreText.gameObject.SetActive(false);
      this._comboText.gameObject.SetActive(false);
      this._cameraTx = PlayerCamera.Camera.transform;
      this._ballPool = new MonoBehaviorObjectPool<BallObject>(this._ballPrefab, this.transform, 2);
    }

    private void OnDestroy()
    {
      this._uniformStore = (UniformStore) null;
      if (UnityState.quitting)
        return;
      this.CleanupScene();
      this._ballPool.ClearAll();
    }

    public void CleanupScene()
    {
      this._avatars.ReturnAllObjects();
      this._ballPool.ReturnAllObjects();
      this._centerPlayer.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this._target != (UnityEngine.Object) null) || !((UnityEngine.Object) this._target.gameObject != (UnityEngine.Object) null))
        return;
      this._target.gameObject.SetActive(false);
    }

    public void InitializeAttacker(
      Vector3 position,
      float spawnTime,
      float attackerSpeed,
      float lockThreshold)
    {
      FootballVR.Avatar avatar = this._avatars.GetObject();
      avatar.Graphics.avatarGraphicsData.baseMap.Value = this._uniformStore.GetDolphinsTexture();
      AttackerBehaviorController behaviourController = (AttackerBehaviorController) avatar.behaviourController;
      this._animConfig.ApplyConfiguration(behaviourController.gameObject);
      behaviourController.startTime = spawnTime;
      AssumeStanceAction instance = ScriptableObject.CreateInstance<AssumeStanceAction>();
      instance.stance = 1;
      instance.overrideTime = true;
      instance.time = spawnTime;
      PlayerScenario scenario = UnityEngine.Object.Instantiate<PlayerScenario>(this._scenarioPrefab);
      scenario.startPosition = new Vector2(position.x, position.z);
      scenario.actionList.Add((ScriptableObject) instance);
      behaviourController.InitializeScenario(scenario);
      behaviourController.playbackInfo = (IPlaybackInfo) this._playbackInfo;
      behaviourController.Reset();
      behaviourController.attackerSpeed = attackerSpeed;
      behaviourController.lockThreshold = lockThreshold;
      behaviourController.OnMiss += new System.Action(this.HandleAttackerMissed);
      behaviourController.attackEnabled = true;
      avatar.gameObject.SetActive(true);
      avatar.Appear(1.5f, 0.1f);
    }

    private void HandleAttackerMissed()
    {
      System.Action onAttackerMissed = this.OnAttackerMissed;
      if (onAttackerMissed == null)
        return;
      onAttackerMissed();
    }

    public void ResetScene()
    {
      this._centerPlayer.gameObject.SetActive(false);
      PlayerScenario scenario = UnityEngine.Object.Instantiate<PlayerScenario>(this._centerScenario);
      this._centerPlayer.behaviourController.playbackInfo = (IPlaybackInfo) this._playbackInfo;
      this._centerPlayer.behaviourController.InitializeScenario(scenario);
      this._centerPlayer.behaviourController.Reset();
      this._centerPlayer.gameObject.SetActive(true);
      this._centerPlayer.Appear(1.5f, 0.0f);
      this._uniformStore.SetNamesAndNumbersVisibility(false);
      this.Target.Hide();
      this.Target.gameObject.SetActive(true);
    }

    public void DisplayScore(Vector3 offset, float score)
    {
      Vector3 position = this._cameraTx.position;
      Vector3 vector3 = position + this._cameraTx.TransformVector(offset);
      this._scoreText.transform.position = vector3;
      this._scoreText.transform.LookAt(2f * vector3 - position, Vector3.up);
      this._scoreText.Display(string.Format("+{0}", (object) score));
    }

    public void DisplayCombo(int combo) => this._comboText.Display(string.Format("x{0}", (object) combo));

    public BallObject GetBall() => this._ballPool.GetObject();

    public void ReturnBall(BallObject ballObject) => this._ballPool.ReturnObject(ballObject);

    public List<AttackerData> GenerateAttackerDatas(
      AgilityChallengeV1 agilityChallenge,
      int attackerCount,
      bool frenzyMode,
      out float maxCollisionTime)
    {
      List<AttackerData> attackerDatas = new List<AttackerData>(attackerCount);
      Vector3 vector3_1 = this._cameraTx.position.SetY(0.0f);
      Vector3 forward = this.transform.forward;
      maxCollisionTime = 0.0f;
      float num1 = 2f + this._playbackInfo.PlayTime;
      float num2 = frenzyMode ? agilityChallenge.frenzyTimeBetweenAttacks : agilityChallenge.timeBetweenAttacks;
      for (int index = 0; index < attackerCount; ++index)
      {
        float num3 = UnityEngine.Random.Range(agilityChallenge.minDistance, agilityChallenge.maxDistance);
        float randomAngle = this.GetRandomAngle(agilityChallenge.maxIncomingAngle);
        Vector3 vector3_2 = vector3_1 + Quaternion.Euler(randomAngle * Vector3.up) * forward * num3;
        float num4 = (float) ((double) num3 / (double) agilityChallenge.attackerSpeed + 0.40000000596046448);
        float b = num4;
        if ((double) b < (double) num1)
          b = num1;
        maxCollisionTime = Mathf.Max(maxCollisionTime, b);
        attackerDatas.Add(new AttackerData()
        {
          position = vector3_2,
          spawnTime = b - num4
        });
        num1 = b + num2;
        if (frenzyMode)
          num2 = Mathf.Clamp(num2 - agilityChallenge.frenzyTimeDecrease, 0.3f, 10f);
      }
      float num5 = this._playbackInfo.PlayTime + 0.3f;
      float a = num5;
      for (int index = 0; index < attackerDatas.Count; ++index)
        a = Mathf.Min(a, attackerDatas[index].spawnTime);
      float num6 = num5 - a;
      if ((double) num6 > 0.0)
      {
        for (int index = 0; index < attackerDatas.Count; ++index)
          attackerDatas[index].spawnTime += num6;
        maxCollisionTime += num6;
      }
      return attackerDatas;
    }

    private float GetRandomAngle(float maxAngle)
    {
      int num = 0;
      float randomAngle;
      for (randomAngle = UnityEngine.Random.Range(-maxAngle, maxAngle); (double) Mathf.Abs(randomAngle - this.prevAngle) < 10.0 && num < 20; ++num)
        randomAngle = UnityEngine.Random.Range(-maxAngle, maxAngle);
      this.prevAngle = randomAngle;
      return randomAngle;
    }
  }
}
