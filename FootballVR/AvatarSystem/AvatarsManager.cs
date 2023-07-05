// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarSystem.AvatarsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DataSensitiveStructs_v5;
using DDL;
using DSE;
using FootballWorld;
using Framework;
using System;
using System.Collections.Generic;
using TB12;
using TB12.RuntimeSystem;
using UnityEngine;

namespace FootballVR.AvatarSystem
{
  public class AvatarsManager : MonoBehaviour
  {
    private UniformStore _uniformStore;
    [SerializeField]
    private FootballVR.Avatar _avatarPrefab;
    [SerializeField]
    private FootballVR.Avatar _tacklerPrefab;
    [SerializeField]
    private FootballVR.Avatar _qbPrefab;
    [SerializeField]
    private Transform _gazeTargetsGroup;
    [SerializeField]
    private int _avatarsCount;
    [SerializeField]
    private PlayRuntimeData _playData;
    [SerializeField]
    private PlaybackInfo _playbackInfo;
    [SerializeField]
    private bool _hidePlayerAvatar = true;
    [SerializeField]
    private bool _vrTraining;
    [SerializeField]
    private bool _revealAvatarsOnPlayLoad = true;
    private MonoBehaviorObjectPool<FootballVR.Avatar> _avatarObjectPool;
    private MonoBehaviorObjectPool<FootballVR.Avatar> _tacklersPool;
    public DataSensitiveStructs_v5.EPlayerRole UserAssumedRole = DataSensitiveStructs_v5.EPlayerRole.QuarterBack;
    private int _userAssumedPlayerId;
    private int _qbId;
    private readonly RoutineHandle _transitionRoutine = new RoutineHandle();

    public event System.Action OnAvatarsInitialized;

    public event Action<List<FootballVR.Avatar>> OnAvatarsCleared;

    public List<FootballVR.Avatar> AllocatedAvatarObjects => this._avatarObjectPool.AllocatedObjects;

    public List<FootballVR.Avatar> AllocatedAttackers => this._tacklersPool.AllocatedObjects;

    public List<FootballVR.Avatar> Receivers { get; } = new List<FootballVR.Avatar>();

    public FootballVR.Avatar Center { get; private set; }

    public FootballVR.Avatar Qb { get; private set; }

    public List<ReceiverData> ReceiversData { get; private set; }

    public List<FootballVR.Avatar> GetAllActiveAvatars
    {
      get
      {
        List<FootballVR.Avatar> allActiveAvatars = new List<FootballVR.Avatar>(22);
        foreach (FootballVR.Avatar allocatedAvatarObject in this.AllocatedAvatarObjects)
          allActiveAvatars.Add(allocatedAvatarObject);
        foreach (FootballVR.Avatar allocatedAttacker in this.AllocatedAttackers)
          allActiveAvatars.Add(allocatedAttacker);
        if ((UnityEngine.Object) this.Qb != (UnityEngine.Object) null && this.UserAssumedRole != DataSensitiveStructs_v5.EPlayerRole.QuarterBack)
          allActiveAvatars.Add(this.Qb);
        return allActiveAvatars;
      }
    }

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      Transform transform = this.transform;
      this._avatarObjectPool = new MonoBehaviorObjectPool<FootballVR.Avatar>(this._avatarPrefab, transform, this._avatarsCount);
      this._tacklersPool = new MonoBehaviorObjectPool<FootballVR.Avatar>(this._tacklerPrefab, transform);
      this.Qb = UnityEngine.Object.Instantiate<FootballVR.Avatar>(this._qbPrefab, transform);
      this.Qb.gameObject.SetActive(false);
      this._playData.OnPlayLoaded += new Action<ExplodedPlayData>(this.HandlePlayLoaded);
      this._playData.OnPlayClosed += new System.Action(this.DeinitAllObjects);
      this._transitionRoutine.Stop();
      BaseStateMachineBehaviour.gazeTargetsParent = this._gazeTargetsGroup;
    }

    private void OnDisable() => this._transitionRoutine.Stop();

    private void OnDestroy()
    {
      this._transitionRoutine.Stop();
      this._playData.OnPlayLoaded -= new Action<ExplodedPlayData>(this.HandlePlayLoaded);
      this._playData.OnPlayClosed -= new System.Action(this.DeinitAllObjects);
      this._uniformStore = (UniformStore) null;
    }

    private void HandlePlayLoaded(ExplodedPlayData play)
    {
      this._transitionRoutine.Stop();
      this.Receivers.Clear();
      this.Center = (FootballVR.Avatar) null;
      DataSensitiveStructs_v5.PlayData playData = play.PlayData;
      this.Receivers.Clear();
      this.Qb.gameObject.SetActive(false);
      this.InitTeam(ETeamLetters.TeamA, playData.teamA, play.TeamAUniformConfig, play.TeamATexture, playData);
      this.InitTeam(ETeamLetters.TeamB, playData.teamB, play.TeamBUniformConfig, play.TeamBTexture, playData, true);
      if (!this._vrTraining)
      {
        this.ReceiversData = LocalPlayLoader.LoadedPlayMeta.receiverDatas;
        if (this.UserAssumedRole != DataSensitiveStructs_v5.EPlayerRole.WideReceiver && this.ReceiversData != null && this.ReceiversData.Count > 0)
        {
          this.Receivers.Clear();
          for (int index = 0; index < this.ReceiversData.Count; ++index)
          {
            FootballVR.Avatar objectWithDataKey = this.GetAvatarObjectWithDataKey(this.ReceiversData[index].playerId);
            if (!((UnityEngine.Object) objectWithDataKey == (UnityEngine.Object) null))
              this.Receivers.Add(objectWithDataKey);
          }
        }
      }
      System.Action avatarsInitialized = this.OnAvatarsInitialized;
      if (avatarsInitialized != null)
        avatarsInitialized();
      if (!this._revealAvatarsOnPlayLoad)
        return;
      this.ShowAllAvatars();
    }

    private void InitTeam(
      ETeamLetters teamType,
      DataSensitiveStructs_v5.TeamData teamData,
      FootballWorld.UniformConfig config,
      Texture2D basemap,
      DataSensitiveStructs_v5.PlayData playData,
      bool capture = false)
    {
      UniformCapture.Info info = new UniformCapture.Info()
      {
        BaseMap = basemap
      };
      Texture2D[] textsTexture = UniformCapture.GetTextsTexture((int) teamType, config.GetUniformFlags());
      this._userAssumedPlayerId = this._hidePlayerAvatar ? this.GetUserPlayerId(playData) : -1;
      this._uniformStore.SetNamesAndNumbersVisibility(true);
      int count = teamData.playerIds.Count;
      for (int index = 0; index < count; ++index)
      {
        int playerId = teamData.playerIds[index];
        if (playerId != 0)
        {
          info.PlayerIndex = index;
          info.TextsAtlas = (Texture[]) textsTexture;
          DataSensitiveStructs_v5.PlayerData playerData = SerializedDataManager.RetrievePlayerData(playerId);
          if (playerData.id != this._userAssumedPlayerId)
          {
            FootballVR.Avatar avatar;
            if (this._vrTraining)
              avatar = this._avatarObjectPool.GetObject();
            else if (playerData.role != DataSensitiveStructs_v5.EPlayerRole.QuarterBack)
            {
              TacklerData tacklerData = AvatarsManager.GetTacklerData((IReadOnlyList<TacklerData>) LocalPlayLoader.LoadedPlayMeta.tacklerDatas, playerData.id);
              int num = !ScriptableSingleton<AvatarsSettings>.Instance.PlayModeCollision ? 0 : (tacklerData != null ? 1 : 0);
              avatar = num != 0 ? this._tacklersPool.GetObject() : this._avatarObjectPool.GetObject();
              if (num != 0)
                ((AttackerBehaviorController) avatar.behaviourController).startTime = tacklerData.startTime;
            }
            else
            {
              avatar = this.Qb;
              this.Qb.GetComponent<BallThrower>().Initialize(this.transform.position + this.transform.forward, false);
            }
            this.InitializeAvatarObject(avatar, playerData, info.ShallowCopy());
            if (capture)
            {
              switch (playerData.role)
              {
                case DataSensitiveStructs_v5.EPlayerRole.WideReceiver:
                  this.Receivers.Add(avatar);
                  continue;
                case DataSensitiveStructs_v5.EPlayerRole.Center:
                  this.Center = avatar;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    private int GetUserPlayerId(DataSensitiveStructs_v5.PlayData playData)
    {
      switch (this.UserAssumedRole)
      {
        case DataSensitiveStructs_v5.EPlayerRole.WideReceiver:
          List<ReceiverData> receiverDatas = LocalPlayLoader.LoadedPlayMeta.receiverDatas;
          if (receiverDatas != null)
          {
            using (List<ReceiverData>.Enumerator enumerator = receiverDatas.GetEnumerator())
            {
              if (enumerator.MoveNext())
                return enumerator.Current.playerId;
              break;
            }
          }
          else
            break;
        case DataSensitiveStructs_v5.EPlayerRole.QuarterBack:
          using (List<DataSensitiveStructs_v5.PlayerData>.Enumerator enumerator = playData.players.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              DataSensitiveStructs_v5.PlayerData current = enumerator.Current;
              if (current.role == DataSensitiveStructs_v5.EPlayerRole.QuarterBack)
                return current.id;
            }
            break;
          }
      }
      Debug.LogError((object) string.Format("Failed to find user replaced player for role {0}", (object) this.UserAssumedRole));
      return -1;
    }

    private static TacklerData GetTacklerData(
      IReadOnlyList<TacklerData> tacklerDatas,
      int playerDataId)
    {
      if (tacklerDatas == null)
        return (TacklerData) null;
      for (int index = 0; index < tacklerDatas.Count; ++index)
      {
        if (tacklerDatas[index].attackerId == playerDataId)
          return tacklerDatas[index];
      }
      return (TacklerData) null;
    }

    private void InitializeAvatarObject(
      FootballVR.Avatar avatar,
      DataSensitiveStructs_v5.PlayerData playerData,
      UniformCapture.Info info)
    {
      avatar.DataKey = playerData.id;
      avatar.Outline = EOutlineMode.kDisabld;
      PlayerIdentity identity = avatar.Identity;
      identity.playerId = playerData.id;
      identity.teamId = (int) this._playData.GetPlayersTeamID(playerData.id);
      identity.role = playerData.role;
      identity.playerName = playerData.playerName;
      identity.uniformNumber = playerData.uniformNumber;
      identity.height = playerData.height;
      identity.weight = playerData.weight;
      identity.procedural = !this._vrTraining || SerializedDataManager.GetPlaySource() == EPlaySource.kNgs;
      System.Action onIdentitySet = identity.OnIdentitySet;
      if (onIdentitySet != null)
        onIdentitySet();
      PlayerRuntimeData playerRuntimeData = this._playData.RetrievePlayerData(playerData.id);
      avatar.behaviourController.Scenario = playerRuntimeData.GenerateScenario();
      avatar.behaviourController.playbackInfo = (IPlaybackInfo) this._playbackInfo;
      avatar.behaviourController.Reset();
      avatar.Graphics.avatarGraphicsData.uniformCaptureInfo.Value = info;
      avatar.gameObject.SetActive(playerData.visible);
    }

    public Vector3 GetUserPlayerPosition() => this._playData.RetrievePlayerData(this._userAssumedPlayerId).Evaluate3DPositionYards(0.0f);

    public float GetUserPlayerOrientation() => this._playData.RetrievePlayerData(this._userAssumedPlayerId).EvaluateOrientation(0.0f);

    public DataSensitiveStructs_v5.PlayerData GetUserAssumedPlayerData() => SerializedDataManager.RetrievePlayerData(this._userAssumedPlayerId);

    private FootballVR.Avatar GetAvatarObjectWithDataKey(int dataKey) => this._avatarObjectPool.AllocatedObjects.Find((Predicate<FootballVR.Avatar>) (x => x.DataKey == dataKey));

    private void DeinitAllObjects()
    {
      if (this._avatarObjectPool != null)
      {
        List<FootballVR.Avatar> allocatedObjects = this._avatarObjectPool.AllocatedObjects;
        int count = allocatedObjects.Count;
        for (int index = 0; index < count; ++index)
          allocatedObjects[index].Deinit();
        Action<List<FootballVR.Avatar>> onAvatarsCleared = this.OnAvatarsCleared;
        if (onAvatarsCleared != null)
          onAvatarsCleared(allocatedObjects);
        this._avatarObjectPool.ReturnAllObjects();
      }
      if (this._tacklersPool == null)
        return;
      List<FootballVR.Avatar> allocatedObjects1 = this._tacklersPool.AllocatedObjects;
      int count1 = allocatedObjects1.Count;
      for (int index = 0; index < count1; ++index)
        allocatedObjects1[index].Deinit();
      this._tacklersPool.ReturnAllObjects();
    }

    public void HideAllAvatars(float duration = 2f, float delay = 2f) => this._transitionRoutine.Run(AvatarTransition.Apply((IList<AvatarGraphics>) this.GetAllAvatarGraphics(), false, duration, delay, true));

    public void ShowAllAvatars(float duration = 2f, float delay = 0.5f) => this._transitionRoutine.Run(AvatarTransition.Apply((IList<AvatarGraphics>) this.GetAllAvatarGraphics(), true, duration, delay, true));

    private List<AvatarGraphics> GetAllAvatarGraphics()
    {
      List<AvatarGraphics> allAvatarGraphics = new List<AvatarGraphics>(22);
      foreach (FootballVR.Avatar allocatedAvatarObject in this.AllocatedAvatarObjects)
        allAvatarGraphics.Add(allocatedAvatarObject.Graphics);
      foreach (FootballVR.Avatar allocatedAttacker in this.AllocatedAttackers)
        allAvatarGraphics.Add(allocatedAttacker.Graphics);
      if ((UnityEngine.Object) this.Qb != (UnityEngine.Object) null && this.UserAssumedRole != DataSensitiveStructs_v5.EPlayerRole.QuarterBack)
        allAvatarGraphics.Add(this.Qb.Graphics);
      return allAvatarGraphics;
    }
  }
}
