// Decompiled with JetBrains decompiler
// Type: PlayersManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using BehaviorDesigner.Runtime;
using ControllerSupport;
using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDB;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayersManager : MonoBehaviour
{
  public BodyTypeConfigCollection bodyTypeCollection;
  private UniformStore uniformStore;
  public Color[] skinTones = new Color[7]
  {
    Color.white,
    new Color(0.8392157f, 0.7607843f, 0.694117665f),
    new Color(0.6627451f, 0.572549045f, 0.4509804f),
    new Color(0.607843161f, 0.490196079f, 0.3529412f),
    new Color(0.2784314f, 0.239215687f, 0.164705887f),
    new Color(0.215686277f, 0.156862751f, 0.09411765f),
    new Color(0.149019614f, 0.0784313753f, 0.0235294122f)
  };
  [SerializeField]
  private AssetReference playerPrefabRef;
  [NonSerialized]
  public List<PlayerAI> curUserScriptRef = new List<PlayerAI>();
  [NonSerialized]
  public List<PlayerAI> curCompScriptRef = new List<PlayerAI>();
  private List<GameObject> curUserObjectRef = new List<GameObject>();
  private List<GameObject> curCompObjectRef = new List<GameObject>();
  public SidelineManager homeSideline;
  public SidelineManager awaySideline;
  public TeamData userTeamData;
  public TeamData compTeamData;
  public Transform blockZoneParent;
  public Transform blockZone;
  public Transform blockFormation;
  public Transform passCircleTrans;
  public GameObject passCircleGO;
  public Transform ballLandingSpot;
  public GameObject ballLandingSpotGO;
  public BlockZoneScript blockScript;
  [HideInInspector]
  public PlayerAI ballHolderScript;
  [HideInInspector]
  public PlayerAI cachedBallHolderAtPlayEnd;
  [HideInInspector]
  public PlayerAI tackler;
  [HideInInspector]
  public PlayerAI userPlayerScript;
  [HideInInspector]
  public PlayerAI userPlayerScriptP2;
  public PlayerAI passTargetScript;
  [HideInInspector]
  public GameObject userPlayer;
  [HideInInspector]
  public GameObject userPlayerP2;
  [HideInInspector]
  public GameObject ballHolder;
  [HideInInspector]
  public GameObject passTarget;
  [HideInInspector]
  public List<GameObject> curUserPlayers;
  [HideInInspector]
  public List<GameObject> curCompPlayers;
  [HideInInspector]
  public int savedDefPlayer;
  [HideInInspector]
  public int savedDefPlayerP2;
  [HideInInspector]
  public int userPlayerIndex;
  [HideInInspector]
  public int userPlayerIndexP2;
  [HideInInspector]
  public int compReceiver;
  [HideInInspector]
  public int recButtonIndex;
  [HideInInspector]
  public float passDistanceInYards;
  [HideInInspector]
  public Vector3 passDestination;
  [HideInInspector]
  public Vector3 passVelocity;
  [HideInInspector]
  public bool userSprint;
  [HideInInspector]
  public bool userSprintP2;
  [HideInInspector]
  public bool userStrafe;
  [HideInInspector]
  public bool userStrafeP2;
  [HideInInspector]
  public bool bulletPass;
  [HideInInspector]
  public bool throwStarted;
  [HideInInspector]
  public bool ballWasThrownOrKicked;
  [HideInInspector]
  public bool convergeOnBall;
  [HideInInspector]
  public bool mouseDown;
  [HideInInspector]
  public bool allowPlayerCycle;
  [HideInInspector]
  public bool forceQBScramble;
  [HideInInspector]
  public bool runningToLineOfScrimmage;
  [HideInInspector]
  public PlayerAI intendedReceiver;
  [HideInInspector]
  public Coroutine snapBallCoroutine;
  [HideInInspector]
  public bool hasNotifiedBlockersToTurn;
  [HideInInspector]
  public bool isThrowingBallAway;
  [HideInInspector]
  public bool hasStartedHandoffFailsafe;
  [HideInInspector]
  public List<KeyValuePair<int, float>> receiversLeftToRight;
  [HideInInspector]
  public List<KeyValuePair<int, float>> defendersInManCoverageLeftToRight;
  [HideInInspector]
  public float playersCloseDistance;
  [HideInInspector]
  public float playersNearbyDistance;
  private SidelineTeamAvatars playerSidelineTeam;
  private SidelineTeamAvatars opponentSidelineTeam;
  private int[] currentFormationIDsPlayer;
  private int[] currentFormationIDsOpponent;
  private RosterData cacheRosterDataPlayer;
  private RosterData cacheRosterDataOpponent;
  private bool mouseToThrow;
  private bool checkForAIQBScramble;
  private float closestDistanceToHandoffTarget;
  private float scrambleThesholdForAIQB;
  public float STICK_MODIFIER = 2.5f;
  public static float PLAYERS_CLOSE = 1f;
  public static float PLAYERS_NEARBY = 2.5f;
  [NonSerialized]
  public bool isInitialized;
  private bool _logged;
  private WaitForSeconds _snapBallDelay;
  public static Dictionary<Position, HashedString> AxisPositionToNGSPositionGroup = new Dictionary<Position, HashedString>()
  {
    {
      Position.TE,
      new HashedString("TE")
    },
    {
      Position.SLT,
      new HashedString("TE")
    },
    {
      Position.WR,
      new HashedString("WR")
    },
    {
      Position.KR,
      new HashedString("WR")
    },
    {
      Position.PR,
      new HashedString("WR")
    },
    {
      Position.QB,
      new HashedString("QB")
    },
    {
      Position.RB,
      new HashedString("RB")
    },
    {
      Position.FB,
      new HashedString("RB")
    },
    {
      Position.P,
      new HashedString("SPEC")
    },
    {
      Position.K,
      new HashedString("SPEC")
    },
    {
      Position.LB,
      new HashedString("LB")
    },
    {
      Position.OLB,
      new HashedString("LB")
    },
    {
      Position.MLB,
      new HashedString("LB")
    },
    {
      Position.ILB,
      new HashedString("LB")
    },
    {
      Position.DL,
      new HashedString("DL")
    },
    {
      Position.DT,
      new HashedString("DL")
    },
    {
      Position.DE,
      new HashedString("DL")
    },
    {
      Position.NT,
      new HashedString("DL")
    },
    {
      Position.DB,
      new HashedString("DB")
    },
    {
      Position.CB,
      new HashedString("DB")
    },
    {
      Position.FS,
      new HashedString("DB")
    },
    {
      Position.SS,
      new HashedString("DB")
    },
    {
      Position.GUN,
      new HashedString("DB")
    },
    {
      Position.BLK,
      new HashedString("DB")
    },
    {
      Position.OL,
      new HashedString("OL")
    },
    {
      Position.C,
      new HashedString("OL")
    },
    {
      Position.LT,
      new HashedString("OL")
    },
    {
      Position.RT,
      new HashedString("OL")
    },
    {
      Position.LG,
      new HashedString("OL")
    },
    {
      Position.RG,
      new HashedString("OL")
    }
  };

  private MatchManager matchManager => MatchManager.instance;

  private PlayManager playManager => MatchManager.instance.playManager;

  private BallManager ballManager => this.matchManager.ballManager;

  public bool CenterBlockEnabled { get; private set; }

  public float throwAdjustmentP1 { get; set; }

  public float throwAdjustmentP2 { get; set; }

  public List<PlayerAI> Defense => !global::Game.IsPlayerOneOnOffense ? this.curUserScriptRef : this.curCompScriptRef;

  public List<PlayerAI> Offense => !global::Game.IsPlayerOneOnOffense ? this.curCompScriptRef : this.curUserScriptRef;

  public async System.Threading.Tasks.Task CallAwake()
  {
    Debug.Log((object) "PlayersManager -> CallAwake()");
    this.uniformStore = SaveManager.GetUniformStore();
    this.savedDefPlayer = 0;
    this.savedDefPlayerP2 = 0;
    this.userSprintP2 = this.userSprint = false;
    this.allowPlayerCycle = true;
    this.curUserPlayers = new List<GameObject>();
    this.curCompPlayers = new List<GameObject>();
    this.curUserObjectRef = await this.GeneratePlayers(new Vector3(31f, 0.0f, 0.0f));
    this.curCompObjectRef = await this.GeneratePlayers(new Vector3(-31f, 0.0f, 0.0f));
    for (int index = 0; index < this.curUserObjectRef.Count; ++index)
    {
      GameObject gameObject1 = this.curUserObjectRef[index];
      GameObject gameObject2 = this.curCompObjectRef[index];
      this.curUserScriptRef.Add(gameObject1.GetComponent<PlayerAI>());
      this.curCompScriptRef.Add(gameObject2.GetComponent<PlayerAI>());
      this.curUserPlayers.Add(gameObject1);
      this.curCompPlayers.Add(gameObject2);
    }
    AssetCache.CreatePlayerAICache();
    AssetCache.AddPlayersToPlayerAICache(this.curUserScriptRef);
    AssetCache.AddPlayersToPlayerAICache(this.curCompScriptRef);
    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.5));
    this.isInitialized = true;
  }

  private void OnDestroy()
  {
    this.ForceStopAllNteractCallbacks();
    Debug.Log((object) "PlayersManager -> OnDestroy");
    this.StopAllCoroutines();
    foreach (GameObject target in this.curUserObjectRef)
    {
      if ((UnityEngine.Object) target != (UnityEngine.Object) null)
        AddressablesData.DestroyGameObject(target);
    }
    foreach (GameObject target in this.curCompObjectRef)
    {
      if ((UnityEngine.Object) target != (UnityEngine.Object) null)
        AddressablesData.DestroyGameObject(target);
    }
    AssetCache.Clear();
    this.uniformStore = (UniformStore) null;
    this.playerSidelineTeam = (SidelineTeamAvatars) null;
    this.opponentSidelineTeam = (SidelineTeamAvatars) null;
    this.cacheRosterDataPlayer = (RosterData) null;
    this.cacheRosterDataOpponent = (RosterData) null;
    this.currentFormationIDsPlayer = (int[]) null;
    this.currentFormationIDsOpponent = (int[]) null;
  }

  private async Task<List<GameObject>> GeneratePlayers(Vector3 posOffset)
  {
    List<GameObject> players = new List<GameObject>();
    for (int i = 0; i < 11; ++i)
    {
      GameObject target = await AddressablesData.instance.InstantiateAsync(this.playerPrefabRef, posOffset + new Vector3(0.0f, 0.0f, (float) (2 * i)), Quaternion.Euler(Vector3.zero), (Transform) null);
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
      players.Add(target);
    }
    List<GameObject> players1 = players;
    players = (List<GameObject>) null;
    return players1;
  }

  public void CallStart()
  {
    this.receiversLeftToRight = new List<KeyValuePair<int, float>>();
    this.defendersInManCoverageLeftToRight = new List<KeyValuePair<int, float>>();
    this.userTeamData = PersistentData.GetUserTeam();
    this.compTeamData = PersistentData.GetCompTeam();
    this.CreateTeamPlayers(1);
    this.CreateTeamPlayers(2);
    for (int index = 0; index < 11; ++index)
    {
      PlayerAI playerAi1 = this.curUserScriptRef[index];
      playerAi1.SetPlayerIndex(index);
      playerAi1.onOffense = true;
      playerAi1.SetTeamIndex(this.userTeamData.TeamIndex);
      PlayerAI playerAi2 = this.curCompScriptRef[index];
      playerAi2.SetPlayerIndex(index);
      playerAi2.onOffense = false;
      playerAi2.SetTeamIndex(this.compTeamData.TeamIndex);
    }
    this.SetPlayersForPregame();
    this.SetAfterPlayActionsForAllPlayers();
    this.playersCloseDistance = Field.TWO_YARDS;
    this.playersNearbyDistance = Field.FIVE_YARDS;
  }

  public async System.Threading.Tasks.Task IsInitialized()
  {
    PlayersManager playersManager = this;
    while (!playersManager.isInitialized && playersManager.enabled)
      await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.5));
  }

  private void FixedUpdate()
  {
    if (!this.checkForAIQBScramble || (double) this.matchManager.playTimer <= (double) this.scrambleThesholdForAIQB || PlayerAI.IsQBBetweenTackles())
      return;
    this.playManager.runnerHoleOffset = 0;
    this.forceQBScramble = true;
    global::Game.OffensiveQB.animatorCommunicator.SetLocomotionStyle(ELocomotionStyle.Regular);
    this.checkForAIQBScramble = false;
    if (!((UnityEngine.Object) this.ballHolderScript != (UnityEngine.Object) null))
      return;
    this.ballHolderScript.SetLookAtTarget((Transform) null, 0.0f);
  }

  public void SetCheckForAIQBScramble(bool checkForScramble)
  {
    this.checkForAIQBScramble = checkForScramble;
    this.scrambleThesholdForAIQB = UnityEngine.Random.Range(2.92f, 4.12f);
  }

  private void SubscribeToNotifcations()
  {
    NotificationCenter<Vector2>.AddListener("InputDown", new Callback<Vector2>(this.InputDown));
    NotificationCenter<Vector2>.AddListener("InputUp", new Callback<Vector2>(this.InputUp));
    NotificationCenter.AddListener("CycleDefensePlayerRight", new Callback(this.CycleDefensePlayerRight));
    NotificationCenter.AddListener("CycleDefensePlayerLeft", new Callback(this.CycleDefensePlayerLeft));
  }

  private void UnsubscribeToNotifcations()
  {
    NotificationCenter<Vector2>.RemoveListener("InputDown", new Callback<Vector2>(this.InputDown));
    NotificationCenter<Vector2>.RemoveListener("InputUp", new Callback<Vector2>(this.InputUp));
    NotificationCenter.RemoveListener("CycleDefensePlayerRight", new Callback(this.CycleDefensePlayerRight));
    NotificationCenter.RemoveListener("CycleDefensePlayerLeft", new Callback(this.CycleDefensePlayerLeft));
  }

  public void InputDown(Vector2 inputPosition)
  {
  }

  public void InputUp(Vector2 inputPosition)
  {
    if (!this.throwStarted)
      return;
    if (ControllerManagerGame.usingController)
    {
      if (!global::Game.IsPlayerOneOnOffense || !global::Game.P1IsAimedPassing)
        return;
      if (!this.mouseToThrow && !UserManager.instance.RightStickButtonIsPressed(Player.One))
      {
        this.bulletPass = false;
      }
      else
      {
        if (!this.mouseToThrow)
          return;
        this.bulletPass = false;
      }
    }
    else
    {
      if (!global::Game.P1IsAimedPassing)
        return;
      this.bulletPass = false;
    }
  }

  public void CycleDefensePlayerRight()
  {
    if (PlayState.IsKickoff || (bool) PlayState.PlayOver || !this.allowPlayerCycle || !global::Game.IsPlayerOneOnDefense)
      return;
    if (this.userPlayerIndex == 10)
      this.userPlayerIndex = 0;
    else if (this.userPlayerIndex == 7)
      this.userPlayerIndex = 9;
    else if (this.userPlayerIndex == 9)
      this.userPlayerIndex = 8;
    else if (this.userPlayerIndex == 8)
      this.userPlayerIndex = 10;
    else
      ++this.userPlayerIndex;
    this.SetUserPlayer(this.userPlayerIndex);
  }

  public void CycleDefensePlayerLeft()
  {
    if (PlayState.IsKickoff || (bool) PlayState.PlayOver || !this.allowPlayerCycle || !global::Game.IsPlayerOneOnDefense)
      return;
    if (this.userPlayerIndex == 0)
      this.userPlayerIndex = 10;
    else if (this.userPlayerIndex == 9)
      this.userPlayerIndex = 7;
    else if (this.userPlayerIndex == 8)
      this.userPlayerIndex = 9;
    else if (this.userPlayerIndex == 10)
      this.userPlayerIndex = 8;
    else
      --this.userPlayerIndex;
    this.SetUserPlayer(this.userPlayerIndex);
  }

  public void CheckForUserInput(bool userOnOffense, float playTimer)
  {
  }

  public void CreateTeamPlayers(int teamId, int[] formationPlayerIds = null)
  {
    bool flag1 = formationPlayerIds == null;
    int[] numArray = formationPlayerIds;
    if (numArray == null)
      numArray = new int[11]
      {
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10
      };
    formationPlayerIds = numArray;
    bool isUserTeam = teamId == 1;
    RosterData mainRoster = (isUserTeam ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam()).TeamDepthChart.MainRoster;
    for (int playerIndex = 0; playerIndex < mainRoster.numberOfPlayersOnRoster; ++playerIndex)
    {
      mainRoster.GetPlayer(playerIndex).OnUserTeam = isUserTeam;
      mainRoster.GetPlayer(playerIndex).Fatigue = 100;
    }
    List<PlayerAI> playerAiList = isUserTeam ? this.curUserScriptRef : this.curCompScriptRef;
    List<int> intList = new List<int>();
    List<string> stringList = new List<string>();
    for (int index = 0; index < 11; ++index)
    {
      int formationPlayerId = formationPlayerIds[index];
      mainRoster.GetPlayer(formationPlayerId).TransferAttributes(playerAiList[index], (UniformSet) null, isUserTeam ? UniformAssetType.USER : UniformAssetType.COMP);
    }
    for (int playerIndex = 0; playerIndex < 53; ++playerIndex)
    {
      PlayerData player = mainRoster.GetPlayer(playerIndex);
      intList.Add(player.Number);
      stringList.Add(player.LastName);
    }
    int uniformId = this.GetUniformID(isUserTeam);
    int uniformFlag = this.GetUniformFlag(isUserTeam);
    FootballWorld.UniformConfig uniformConfig = this.GetUniformConfig(isUserTeam);
    UniformCapture.Info info1 = new UniformCapture.Info()
    {
      BaseMap = uniformConfig.BasemapAlternative
    };
    List<int> numbers = intList;
    List<string> names = stringList;
    int uniformFlags = uniformFlag;
    Texture2D[] textsTexture = UniformCapture.GetTextsTexture(uniformId, numbers, names, uniformFlags);
    bool flag2 = SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide == 1;
    for (int index1 = 0; index1 < 11; ++index1)
    {
      AvatarGraphics component = playerAiList[index1].GetComponent<AvatarGraphics>();
      UniformCapture.Info info2 = info1.ShallowCopy();
      int formationPlayerId = formationPlayerIds[index1];
      PlayerData player = mainRoster.GetPlayer(formationPlayerId);
      info2.PlayerIndex = player.IndexOnTeam;
      info2.TextsAtlas = (Texture[]) textsTexture;
      component.avatarGraphicsData.uniformCaptureInfo.Value = info2;
      BodyTypeConfig bodyTypeConfig = this.bodyTypeCollection.GetBodyTypeConfig(PlayersManager.AxisPositionToNGSPositionGroup[player.PlayerPosition]);
      CharacterParameters characterParameters = component.ConfigAvatar(player.AvatarID, player.PlayerPosition);
      if (characterParameters != null)
      {
        int bodyType = (int) characterParameters.bodyType;
        playerAiList[index1].animatorCommunicator.BodyType = bodyType;
      }
      for (int index2 = 0; index2 < component.Renderers.Count; ++index2)
        bodyTypeConfig.ApplyConfig(component.Renderers[index2] as SkinnedMeshRenderer);
      component.transform.localScale = Vector3.one * (float) playerAiList[index1].height * 0.0254f / 1.97f;
      int num = uniformFlag == 1 ? -1 : 1;
      if (flag2 & flag1)
      {
        Transform trans = playerAiList[index1].trans;
        playerAiList[index1].animatorCommunicator.Reset(new Vector3((float) (30 * num), trans.position.y, trans.position.z), Quaternion.Euler(new Vector3(0.0f, (float) (90 * -num), 0.0f)), false);
      }
    }
    if (isUserTeam)
    {
      this.cacheRosterDataPlayer = mainRoster;
      this.currentFormationIDsPlayer = formationPlayerIds;
    }
    else
    {
      this.cacheRosterDataOpponent = mainRoster;
      this.currentFormationIDsOpponent = formationPlayerIds;
    }
    this.RefreshSidelineTeamAvatars(isUserTeam, uniformConfig, textsTexture);
  }

  private int GetUniformID(bool isUserTeam) => SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide != 1 ? (!isUserTeam ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntAwayTeam : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntHomeTeam) : (!isUserTeam ? SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntHomeTeam : SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntAwayTeam);

  private int GetUniformFlag(bool isUserTeam) => SingletonBehaviour<PersistentData, MonoBehaviour>.instance.IntPlayerSide != 1 ? (!isUserTeam ? 1 : 2) : (!isUserTeam ? 2 : 1);

  private FootballWorld.UniformConfig GetUniformConfig(bool isUserTeam) => this.uniformStore.GetUniformConfigByInt(this.GetUniformID(isUserTeam), this.GetUniformFlag(isUserTeam));

  private void RefreshSidelineTeamAvatars(
    bool isUserTeam,
    FootballWorld.UniformConfig uniformConfig = null,
    Texture2D[] textsAtlas = null)
  {
    if (uniformConfig == null || textsAtlas == null)
    {
      int uniformId = this.GetUniformID(isUserTeam);
      int uniformFlag = this.GetUniformFlag(isUserTeam);
      uniformConfig = this.GetUniformConfig(isUserTeam);
      int uniformFlags = uniformFlag;
      textsAtlas = UniformCapture.GetTextsTexture(uniformId, (ETeamUniformFlags) uniformFlags);
    }
    if (isUserTeam)
    {
      if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.Value != ETeamUniformFlags.Home)
      {
        if (!((UnityEngine.Object) this.opponentSidelineTeam != (UnityEngine.Object) null))
          return;
        this.opponentSidelineTeam.Refresh(this.cacheRosterDataPlayer, this.currentFormationIDsPlayer, uniformConfig.BasemapAlternative, textsAtlas);
      }
      else
      {
        if (!((UnityEngine.Object) this.playerSidelineTeam != (UnityEngine.Object) null))
          return;
        this.playerSidelineTeam.Refresh(this.cacheRosterDataPlayer, this.currentFormationIDsPlayer, uniformConfig.BasemapAlternative, textsAtlas);
      }
    }
    else if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.PlayerSide.Value == ETeamUniformFlags.Home)
    {
      if (!((UnityEngine.Object) this.opponentSidelineTeam != (UnityEngine.Object) null))
        return;
      this.opponentSidelineTeam.Refresh(this.cacheRosterDataOpponent, this.currentFormationIDsOpponent, uniformConfig.BasemapAlternative, textsAtlas);
    }
    else
    {
      if (!((UnityEngine.Object) this.playerSidelineTeam != (UnityEngine.Object) null))
        return;
      this.playerSidelineTeam.Refresh(this.cacheRosterDataOpponent, this.currentFormationIDsOpponent, uniformConfig.BasemapAlternative, textsAtlas);
    }
  }

  public void RegisterSidelineTeam(SidelineTeamAvatars target, bool isPlayer)
  {
    if ((UnityEngine.Object) this.playManager == (UnityEngine.Object) null)
      return;
    if (isPlayer)
      this.playerSidelineTeam = target;
    else
      this.opponentSidelineTeam = target;
    if (!((UnityEngine.Object) this.playerSidelineTeam != (UnityEngine.Object) null) || !((UnityEngine.Object) this.opponentSidelineTeam != (UnityEngine.Object) null))
      return;
    this.RefreshSidelineTeamAvatars(true);
    this.RefreshSidelineTeamAvatars(false);
  }

  public void MarkPlayersToSwap(int teamId, int[] formationPlayerIds)
  {
    int num = teamId == 1 ? 1 : 0;
    TeamData teamData = num != 0 ? PersistentData.GetUserTeam() : PersistentData.GetCompTeam();
    List<PlayerAI> playerAiList = num != 0 ? this.curUserScriptRef : this.curCompScriptRef;
    for (int index = 0; index < 11; ++index)
    {
      int formationPlayerId = formationPlayerIds[index];
      PlayerData player = teamData.GetPlayer(formationPlayerId);
      if (playerAiList[index].indexOnTeam != player.IndexOnTeam)
        playerAiList[index].isBeingSubbed = true;
    }
  }

  private void SetPlayersForPregame()
  {
    float num1 = Field.MIDFIELD - Field.TEN_YARDS;
    float num2 = (float) (((double) Field.MIDFIELD + (double) Field.TEN_YARDS - (double) num1) / 11.0);
    for (int index = 0; index < this.curUserScriptRef.Count; ++index)
    {
      this.curUserScriptRef[index].trans.position = new Vector3(-Field.OUT_OF_BOUNDS + UnityEngine.Random.value, 0.0f, num1 + num2 * (float) index + UnityEngine.Random.value);
      this.curUserScriptRef[index].trans.eulerAngles = new Vector3(0.0f, UnityEngine.Random.Range(80f, 100f), 0.0f);
    }
    for (int index = 0; index < this.curCompScriptRef.Count; ++index)
    {
      this.curCompScriptRef[index].trans.position = new Vector3(Field.OUT_OF_BOUNDS + UnityEngine.Random.value, 0.0f, num1 + num2 * (float) index + UnityEngine.Random.value);
      this.curCompScriptRef[index].trans.eulerAngles = new Vector3(0.0f, UnityEngine.Random.Range(-80f, -100f), 0.0f);
    }
  }

  public static List<PlayerAI> GetPlayersInRange(
    Vector3 referencePoint,
    float range,
    List<PlayerAI> players)
  {
    List<PlayerAI> playersInRange = new List<PlayerAI>();
    for (int index = 0; index < players.Count; ++index)
    {
      if ((double) Vector3.Distance(referencePoint, players[index].trans.position) < (double) range)
        playersInRange.Add(players[index]);
    }
    return playersInRange;
  }

  public void DisallowPlayerCycle() => this.allowPlayerCycle = false;

  public void AllowPlayerCycle() => this.allowPlayerCycle = true;

  public void AllowPlayerCycleWithOneFrameDelay() => this.StartCoroutine(this.AllowPlayerCycleDelay());

  private IEnumerator AllowPlayerCycleDelay()
  {
    yield return (object) null;
    this.AllowPlayerCycle();
  }

  public void SetAfterPlayActionsForAllPlayers()
  {
    for (int index = 0; index < 11; ++index)
    {
      this.curCompScriptRef[index].SetAfterPlayActions();
      this.curUserScriptRef[index].SetAfterPlayActions();
    }
  }

  public void ClearCelebrationsForAllPlayers()
  {
    for (int index = 0; index < 11; ++index)
    {
      this.curCompScriptRef[index].ResetCelebration();
      this.curUserScriptRef[index].ResetCelebration();
    }
  }

  public void ForceStopAllNteractCallbacks()
  {
    for (int index = 0; index < this.curCompScriptRef.Count; ++index)
    {
      this.curCompScriptRef[index].ForceStopAllNteractCallbacks();
      this.curUserScriptRef[index].ForceStopAllNteractCallbacks();
    }
  }

  public void PutAllPlayersOnSideline()
  {
    this.ClearCelebrationsForAllPlayers();
    for (int index = 0; index < 11; ++index)
    {
      (Vector3, Vector3) sidelineTransform1 = this.curUserScriptRef[index].GetSidelineTransform();
      (Vector3, Vector3) sidelineTransform2 = this.curCompScriptRef[index].GetSidelineTransform();
      this.curUserScriptRef[index].animatorCommunicator.Reset(sidelineTransform1.Item1, Quaternion.Euler(sidelineTransform1.Item2), false);
      this.curCompScriptRef[index].animatorCommunicator.Reset(sidelineTransform2.Item1, Quaternion.Euler(sidelineTransform2.Item2), false);
      this.curUserScriptRef[index].animatorCommunicator.atPlayPosition = false;
      this.curCompScriptRef[index].animatorCommunicator.atPlayPosition = false;
    }
  }

  public void PutCompPlayersOnSideline()
  {
    for (int index = 0; index < 11; ++index)
    {
      (Vector3, Vector3) sidelineTransform = this.curCompScriptRef[index].GetSidelineTransform();
      this.curCompScriptRef[index].animatorCommunicator.Reset(sidelineTransform.Item1, Quaternion.Euler(sidelineTransform.Item2), false);
      this.curCompScriptRef[index].animatorCommunicator.atPlayPosition = false;
      this.curCompScriptRef[index].HidePlayerAvatar();
    }
  }

  public void PutAllPlayersInHuddle()
  {
    PEGameplayEventManager.RecordMovePlayersToHuddleEvent();
    this.ClearCelebrationsForAllPlayers();
    for (int index = 0; index < 11; ++index)
    {
      this.curUserScriptRef[index].animatorCommunicator.Reset(this.curUserScriptRef[index].GetHuddlePosition(), this.curUserScriptRef[index].GetHuddleRotation(), false);
      this.curCompScriptRef[index].animatorCommunicator.Reset(this.curCompScriptRef[index].GetHuddlePosition(), this.curCompScriptRef[index].GetHuddleRotation(), false);
      this.curUserScriptRef[index].animatorCommunicator.atPlayPosition = false;
      this.curCompScriptRef[index].animatorCommunicator.atPlayPosition = false;
      this.curUserScriptRef[index].animatorCommunicator.atHuddlePosition = true;
      this.curCompScriptRef[index].animatorCommunicator.atHuddlePosition = true;
      if (global::Game.CurrentPlayHasUserQBOnField && index == 5)
        MatchManager.instance.UpdateUserPlayerPosition();
    }
    if (!global::Game.IsNotSpectateMode)
      return;
    GamePlayerController.CameraFade.Clear();
  }

  public void PutPlayersInCelebrationHuddle(Vector3 celebrationLocation)
  {
    this.ClearCelebrationsForAllPlayers();
    this.PutCompPlayersOnSideline();
    for (int index = 0; index < 11; ++index)
    {
      BehaviorManager.instance.DisableBehavior(this.curUserScriptRef[index].behaviorTree);
      this.curUserScriptRef[index].animatorCommunicator.Reset(this.curUserScriptRef[index].GetChampionHuddlePosition(celebrationLocation), this.curUserScriptRef[index].GetChampionHuddleRotation(celebrationLocation), true);
      this.curUserScriptRef[index].animatorCommunicator.atPlayPosition = false;
      this.curUserScriptRef[index].animatorCommunicator.atHuddlePosition = true;
      this.curUserScriptRef[index].celebrations.SetCelebrationFromCategory(CelebrationCategory.SuperbowlCelebration);
    }
    if (!global::Game.IsNotSpectateMode)
      return;
    GamePlayerController.CameraFade.Clear();
  }

  public void PutAllPlayersInPlayPosition(bool movePlayer = true)
  {
    for (int index = 0; index < 11; ++index)
    {
      this.curUserScriptRef[index].PIPutPlayerInPlayPosition(movePlayer);
      this.curUserScriptRef[index].hasShiftedStartingPosition = true;
      this.curCompScriptRef[index].PIPutPlayerInPlayPosition(movePlayer);
      this.curCompScriptRef[index].hasShiftedStartingPosition = true;
    }
    if (!global::Game.CurrentPlayHasUserQBOnField)
      this.AISnapBall();
    if (!global::Game.IsNotSpectateMode)
      return;
    GamePlayerController.CameraFade.Clear();
  }

  public void BreakHuddle()
  {
    PlayState.PlayOver.Value = false;
    PlayState.HuddleBreak.Value = true;
    PEGameplayEventManager.RecordBreakHuddleEvent();
    this.StartCoroutine(this.WaitToRemoveHuddlePos());
  }

  private IEnumerator WaitToRemoveHuddlePos()
  {
    yield return (object) new WaitForSeconds(0.2f);
    List<PlayerAI> playerAiList;
    if (global::Game.IsPlayerOneOnOffense)
    {
      playerAiList = this.curUserScriptRef;
      List<PlayerAI> curCompScriptRef = this.curCompScriptRef;
    }
    else
    {
      playerAiList = this.curCompScriptRef;
      List<PlayerAI> curUserScriptRef = this.curUserScriptRef;
    }
    for (int index = 0; index < playerAiList.Count; ++index)
    {
      this.curUserScriptRef[index].animatorCommunicator.atHuddlePosition = false;
      this.curCompScriptRef[index].animatorCommunicator.atHuddlePosition = false;
    }
  }

  public void ForcePlayersToHuddleAfterPlay()
  {
    PEGameplayEventManager.RecordMovePlayersToHuddleEvent();
    if (global::Game.IsNotKickoff)
    {
      for (int index = 0; index < 11; ++index)
      {
        this.curUserScriptRef[index].PutPlayerInHuddlePosition();
        this.curCompScriptRef[index].PutPlayerInHuddlePosition();
      }
    }
    if (!global::Game.IsNotSpectateMode)
      return;
    GamePlayerController.CameraFade.Clear();
  }

  public void FinishPlayForAllPlayers()
  {
    for (int index = 0; index < 11; ++index)
    {
      this.curCompScriptRef[index].FinishPlay();
      this.curUserScriptRef[index].FinishPlay();
    }
  }

  public void HideAllPlayers()
  {
    for (int index = 0; index < 11; ++index)
    {
      this.curCompScriptRef[index].gameObject.SetActive(false);
      this.curUserScriptRef[index].gameObject.SetActive(false);
    }
  }

  public void ShowAllPlayers()
  {
    for (int index = 0; index < 11; ++index)
    {
      this.curCompScriptRef[index].gameObject.SetActive(true);
      this.curUserScriptRef[index].gameObject.SetActive(true);
    }
  }

  public void SetUserPlayer(int index)
  {
    if (index == 5 || index == -1)
    {
      if ((UnityEngine.Object) this.userPlayer != (UnityEngine.Object) null && (UnityEngine.Object) this.userPlayerScript != (UnityEngine.Object) null && this.userPlayer.activeInHierarchy && global::Game.IsPlayInactive && !(bool) PlayState.PlayOver && this.userPlayerScript.animatorCommunicator.atPlayPosition)
        this.userPlayerScript.animatorCommunicator.SetGoal(this.userPlayerScript.animatorCommunicator.CurrentGoal.position, Field.DEFENSE_TOWARDS_LOS_QUATERNION);
      this.userPlayerIndex = index;
      if (index != -1)
      {
        if (index < 0)
          return;
        this.userPlayer = this.curUserPlayers[index];
        this.userPlayerScript = this.curUserScriptRef[index];
      }
      else
        this.userPlayer = (GameObject) null;
    }
    else
      Debug.LogWarning((object) string.Format("Cant set user player to {0} in ProEra -- Only QB is allowed", (object) index));
  }

  public void SetUserPlayerP2(int index)
  {
  }

  public int GetPlayerIndex(GameObject go)
  {
    for (int index = 0; index < 11; ++index)
    {
      if ((UnityEngine.Object) this.curUserPlayers[index] == (UnityEngine.Object) go)
        return index;
    }
    for (int index = 0; index < 11; ++index)
    {
      if ((UnityEngine.Object) this.curCompPlayers[index] == (UnityEngine.Object) go)
        return index;
    }
    return -1;
  }

  public PlayerAI GetPlayerAI(Transform tran)
  {
    for (int index = 0; index < 11; ++index)
    {
      if ((UnityEngine.Object) this.curUserScriptRef[index].trans.root == (UnityEngine.Object) tran)
        return this.curUserScriptRef[index];
    }
    for (int index = 0; index < 11; ++index)
    {
      if ((UnityEngine.Object) this.curCompScriptRef[index].trans.root == (UnityEngine.Object) tran)
        return this.curCompScriptRef[index];
    }
    return (PlayerAI) null;
  }

  public PlayerAI GetCurrentQB() => !global::Game.IsPlayerOneOnOffense ? this.curCompScriptRef[5] : this.curUserScriptRef[5];

  public PlayerAI GetCurrentKicker() => !global::Game.IsPlayerOneOnOffense ? this.curCompScriptRef[6] : this.curUserScriptRef[6];

  public PlayerAI GetCurrentCenter() => !global::Game.IsPlayerOneOnOffense ? this.curCompScriptRef[2] : this.curUserScriptRef[2];

  public PlayerAI GetCurrentKickOrPuntReturner() => !global::Game.IsPlayerOneOnDefense ? this.curCompScriptRef[9] : this.curUserScriptRef[9];

  public PlayerAI GetCurrentSnapTarget() => !PlayState.IsPunt ? MatchManager.instance.playersManager.GetCurrentQB() : MatchManager.instance.playersManager.GetCurrentKicker();

  public void SetBlockZone()
  {
    if ((UnityEngine.Object) this.ballHolder != (UnityEngine.Object) null)
    {
      this.blockZoneParent.position = this.ballHolderScript.trans.position;
      this.blockZone.rotation = this.ballHolderScript.trans.rotation;
      float y = (float) ((double) this.ballHolderScript.trans.position.x / (double) Field.OUT_OF_BOUNDS * -20.0);
      if (!global::Game.IsTurnover && global::Game.OffensiveFieldDirection == -1)
        y = (float) ((double) this.ballHolderScript.trans.position.x / (double) Field.OUT_OF_BOUNDS * 20.0) + 180f;
      if (!this._logged)
      {
        this._logged = true;
        Debug.Log((object) "SetBlockZone: not flipping block zone direction");
      }
      this.blockFormation.rotation = Quaternion.Euler(new Vector3(0.0f, y, 0.0f));
    }
    else
      this.blockZoneParent.position = new Vector3(-1000f, 0.0f, 0.0f);
  }

  public void SetBallHolder(GameObject go, bool onUserTeam) => this.SetBallHolder(this.GetPlayerIndex(go), onUserTeam);

  public void SetBallHolder(int index, bool onUserTeam)
  {
    if ((UnityEngine.Object) this.ballHolderScript != (UnityEngine.Object) null)
    {
      this.ballHolderScript.BallPossession.inBallPossession = false;
      this.ballHolderScript.animatorCommunicator.hasBall = false;
    }
    if (index != -1)
    {
      Debug.Log((object) ("SetBallHolder: onUserTeam[" + onUserTeam.ToString() + "]"));
      if (this.ballManager.toss && index != this.playManager.handOffTargetIndex && (onUserTeam && global::Game.IsPlayerOneOnDefense || !onUserTeam && global::Game.IsPlayerOneOnOffense))
        return;
      if (PlayState.IsKickoff)
        MatchManager.instance.timeManager.SetRunGameClock(true);
      if (onUserTeam && global::Game.IsPlayerOneOnDefense || !onUserTeam && global::Game.IsPlayerOneOnOffense)
        this.SetTurnover();
      if (this.ballManager.toss)
        this.ballManager.toss = false;
      Debug.Log((object) ("SetBallHolder: index[" + index.ToString() + "] Game.IsPlayActive[" + global::Game.IsPlayActive.ToString() + "]"));
      if (global::Game.IsPlayActive)
      {
        bool flag1 = global::Game.BS_IsInAirPass || global::Game.BS_IsInAirDeflected || this.matchManager.IsSimulating && global::Game.IsPass;
        Ball.State.BallState.SetValue(EBallState.PlayersHands);
        if (global::Game.IsRun && (UnityEngine.Object) this.ballHolderScript == (UnityEngine.Object) global::Game.OffensiveQB)
          this.StartCoroutine(this.EnableQBColliderAfterHandoff());
        if (onUserTeam)
        {
          this.ballHolder = this.curUserPlayers[index];
          this.ballHolderScript = this.curUserScriptRef[index];
          if (flag1 && global::Game.IsPlayerOneOnOffense)
          {
            ++this.playManager.userTeamCurrentPlayStats.players[this.curUserScriptRef[5].indexOnTeam].QBCompletions;
            ++this.playManager.userTeamCurrentPlayStats.players[this.ballHolderScript.indexOnTeam].Receptions;
            this.matchManager.caughtPassOrKickAtPos = this.ballManager.trans.position.z;
            this.ballHolderScript.RunPlayerCaughtPass();
          }
        }
        else
        {
          this.ballHolder = this.curCompPlayers[index];
          this.ballHolderScript = this.curCompScriptRef[index];
          if (flag1 && global::Game.IsPlayerOneOnDefense)
          {
            ++this.playManager.compTeamCurrentPlayStats.players[this.curCompScriptRef[5].indexOnTeam].QBCompletions;
            ++this.playManager.compTeamCurrentPlayStats.players[this.ballHolderScript.indexOnTeam].Receptions;
            this.matchManager.caughtPassOrKickAtPos = this.ballManager.trans.position.z;
            this.ballHolderScript.RunPlayerCaughtPass();
          }
        }
        if (global::Game.IsKickoff || global::Game.IsPunt)
          PEGameplayEventManager.RecordKickReturnEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this.ballHolderScript);
        this.ResetAllPlayerIKTargets();
        FatigueManager.CatchBall(this.ballHolderScript);
        if (!PlayState.IsRun || index != 5)
          this.ballHolderScript.blockType = BlockType.None;
        this.ballHolderScript.lookForBlockTarget = false;
        this.ballHolderScript.animatorCommunicator.hasBall = true;
        this.ballHolderScript.BallPossession.inBallPossession = true;
        this.ballLandingSpotGO.SetActive(false);
        this.ballHolderScript.ResetIK();
        bool flag2 = false;
        if (this.ballHolderScript.IsQB())
          flag2 = true;
        if (this.ballHolderScript.IsKicker() && global::Game.IsPunt)
          flag2 = true;
        if (!flag2)
        {
          this.convergeOnBall = true;
          if (global::Game.IsPlayerOneOnOffense)
          {
            for (int index1 = 0; index1 < 11; ++index1)
              this.curCompScriptRef[index1].SetHandoffDetectionReactionDelay();
            if ((bool) ProEra.Game.MatchState.Turnover)
            {
              this.EnableAllBlockers(this.curCompScriptRef);
              this.DisableAllBlockers(this.curUserScriptRef);
            }
            else if (PlayState.IsPass)
              this.EnableAllBlockers(this.curUserScriptRef);
          }
          else
          {
            for (int index2 = 0; index2 < 11; ++index2)
              this.curUserScriptRef[index2].SetHandoffDetectionReactionDelay();
            if ((bool) ProEra.Game.MatchState.Turnover)
            {
              this.EnableAllBlockers(this.curUserScriptRef);
              this.DisableAllBlockers(this.curCompScriptRef);
            }
            else if (PlayState.IsPass)
              this.EnableAllBlockers(this.curCompScriptRef);
          }
        }
        if (global::Game.IsPlayerOneOnOffense & onUserTeam && (!this.ballHolderScript.IsQB() || PlayState.IsPass && !global::Game.IsPlayAction || global::Game.BS_IsOnGround) && index == 5)
          this.SetUserPlayer(index);
        if (global::Game.IsPlayerOneOnDefense & onUserTeam && global::Game.UserDoesNotControlPlayers)
          this.SetUserPlayer(index);
        string str = this.ballHolderScript.onUserTeam ? PersistentData.GetUserTeam().GetAbbreviation() : PersistentData.GetCompTeam().GetAbbreviation();
        this.playManager.AddToCurrentPlayLog(this.ballHolderScript.firstName + " " + this.ballHolderScript.lastName + " (" + this.ballHolderScript.playerPosition.ToString() + ", " + str + ") gained control of the ball at the " + Field.GetYardLineStringByFieldLocation(this.ballManager.trans.position.z) + " yard line");
      }
      else
      {
        this.ballHolderScript = (PlayerAI) null;
        this.ballHolder = (GameObject) null;
        Ball.State.BallState.SetValue(EBallState.InCentersHandsBeforeSnap);
        (onUserTeam ? this.curUserScriptRef[index] : this.curCompScriptRef[index]).BallPossession.inBallPossession = true;
      }
      this.ballManager.rigidbd.interpolation = RigidbodyInterpolation.None;
    }
    else
    {
      this.ballHolderScript = (PlayerAI) null;
      this.ballHolder = (GameObject) null;
    }
  }

  public void BallHolderReleaseBall() => this.SetBallHolder(-1, true);

  public PlayerAI GetBallHolder() => this.ballHolderScript;

  private void ResetAllPlayerIKTargets()
  {
    for (int index = 0; index < this.curUserScriptRef.Count; ++index)
    {
      this.curUserScriptRef[index].SetLookAtTarget((Transform) null, 0.0f);
      this.curCompScriptRef[index].SetLookAtTarget((Transform) null, 0.0f);
    }
  }

  private IEnumerator EnableQBColliderAfterHandoff()
  {
    yield return (object) new WaitForSeconds(1f);
    global::Game.OffensiveQB.EnableColliders();
  }

  public void ThrowBallTo(int i)
  {
    this.mouseDown = true;
    this.recButtonIndex = i;
  }

  public void EnableAllBlockers(List<PlayerAI> playersScripts)
  {
    for (int index = 0; index < 11; ++index)
    {
      playersScripts[index].lookForBlockTarget = true;
      playersScripts[index].blockType = BlockType.MoveToBallHolder;
    }
  }

  public void DisableAllBlockers(List<PlayerAI> playersScripts)
  {
    for (int index = 0; index < 11; ++index)
    {
      playersScripts[index].lookForBlockTarget = false;
      playersScripts[index].blockType = BlockType.None;
    }
  }

  public void SetTurnover()
  {
    ProEra.Game.MatchState.Turnover.Value = true;
    this.matchManager.caughtPassOrKickAtPos = this.ballManager.trans.position.z;
    SingletonBehaviour<MatchStatsManager, MonoBehaviour>.instance.ResetDriveStats();
  }

  public void ManageSelectingClosestPlayer(int player)
  {
    if (global::Game.UserDoesNotControlPlayers)
      return;
    switch (player)
    {
      case 1:
        if (!global::Game.IsPlayerOneOnDefense && !(bool) ProEra.Game.MatchState.Turnover && !global::Game.BS_IsOnGround)
          break;
        this.SetClosestPlayer(1);
        break;
      case 2:
        if (!global::Game.Is2PMatch)
          break;
        if (ProEra.Game.MatchState.IsPlayerTwoOnDefense || (bool) ProEra.Game.MatchState.Turnover || global::Game.BS_IsOnGround)
        {
          this.SetClosestPlayer2(2);
          break;
        }
        if (!ProEra.Game.MatchState.IsPlayerOneOnOffense)
          break;
        this.SetClosestPlayer2(2);
        break;
    }
  }

  public void SetClosestPlayer(int player) => this.SetUserPlayer(this.FindClosestUserPlayer(player));

  public void SetClosestPlayer2(int player) => this.SetUserPlayerP2(this.FindClosestUserPlayer(player));

  public void BeginQBThrow()
  {
    this.SetPassDestination();
    global::Game.OffensiveQB.throwAbility.ThrowBall(this.passDestination, global::Game.OffensiveQB.LeftHanded);
    this.throwStarted = true;
  }

  private void SetPassDestination()
  {
    if (this.passCircleGO.activeInHierarchy)
    {
      this.passCircleTrans.eulerAngles = new Vector3(0.0f, UnityEngine.Random.Range(0.0f, 360f), 0.0f);
      this.passDestination = this.passCircleTrans.position + this.passCircleTrans.forward * (UnityEngine.Random.Range(0.0f, this.passCircleTrans.localScale.x) / 2f);
      this.passCircleGO.SetActive(false);
    }
    else if ((UnityEngine.Object) this.intendedReceiver != (UnityEngine.Object) null)
    {
      if (global::Game.UserDoesNotControlPlayers || global::Game.IsNot2PMatch && global::Game.IsPlayerOneOnDefense)
        this.bulletPass = true;
      this.passDestination = PlayerAI.GetPassLocation(this.intendedReceiver, this.bulletPass);
    }
    else
    {
      if (!this.isThrowingBallAway)
        return;
      this.passDestination = new Vector3(!Field.IsOnLeftSideOfField(global::Game.OffensiveQB.trans.position.x) ? Field.RIGHT_OUT_OF_BOUNDS + UnityEngine.Random.Range(2f, 4f) * (float) global::Game.OffensiveFieldDirection : Field.LEFT_OUT_OF_BOUNDS - UnityEngine.Random.Range(2f, 4f) * (float) global::Game.OffensiveFieldDirection, 0.0f, MatchManager.ballOn + UnityEngine.Random.Range(Field.ONE_YARD * 10f, Field.ONE_YARD * 15f) * (float) global::Game.OffensiveFieldDirection);
    }
  }

  public void ExecuteThrow(bool vrThrow = false)
  {
    if (this.ballWasThrownOrKicked)
    {
      Debug.LogWarning((object) "Ball Was Already Thrown or Kicked when ExcuteThrow called");
    }
    else
    {
      this.checkForAIQBScramble = false;
      this.ballWasThrownOrKicked = true;
      Ball.State.BallState.SetValue(EBallState.InAirPass);
      this.throwStarted = false;
      if (!vrThrow)
      {
        float passD = Vector3.Distance(this.ballManager.trans.position, this.passDestination);
        PlayerAI currentQb = this.GetCurrentQB();
        float num1 = 50f;
        float num2 = 0.75f;
        float num3 = (float) (currentQb.throwingPower - 60) * num2;
        float num4 = num1 + num3;
        if ((double) num4 < 40.0)
          num4 = 40f;
        float num5 = num4 * Field.ONE_YARD;
        if ((double) passD > (double) num5)
        {
          Vector3 vector3 = this.passDestination - this.ballManager.trans.position;
          vector3.Normalize();
          vector3 *= num5;
          vector3 += currentQb.trans.position;
          this.passDestination = vector3;
        }
        this.ballManager.Throw(this.passDestination, passD, this.bulletPass, currentQb.throwingPower);
        PEGameplayEventManager.RecordBallThrownEvent(MatchManager.instance.timeManager.GetGameClockTimer(), this.ballManager.transform.position, currentQb, this.intendedReceiver, false);
      }
      if (vrThrow)
      {
        this.ballManager.VRThrow(this.passDestination, this.passVelocity);
        PlayerAI receiver = (PlayerAI) null;
        ReceiverUI currentTarget = EligibilityManager.Instance.GetCurrentTarget();
        if ((UnityEngine.Object) currentTarget != (UnityEngine.Object) null)
          receiver = currentTarget.GetComponent<PlayerAI>();
        PEGameplayEventManager.RecordBallThrownEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this.GetCurrentQB(), receiver, true);
      }
      this.passDistanceInYards = (float) Field.ConvertDistanceToYards(this.passDestination.z - this.ballManager.trans.position.z);
      this.ballLandingSpot.position = new Vector3(this.passDestination.x, this.ballLandingSpot.position.y, this.passDestination.z);
      this.ballLandingSpotGO.SetActive(true);
      if ((UnityEngine.Object) this.ballHolderScript != (UnityEngine.Object) null)
        this.ballHolderScript.animatorCommunicator.SetGoal(this.ballHolderScript.animatorCommunicator.CurrentGoal.position, Quaternion.LookRotation(this.passDestination - this.ballHolderScript.trans.position));
      Vector3 vector3_1 = new Vector3(this.passDestination.x, 0.0f, this.passDestination.z);
      if (global::Game.IsPlayerOneOnOffense)
      {
        ++this.playManager.userTeamCurrentPlayStats.players[this.curUserScriptRef[5].indexOnTeam].QBAttempts;
        this.curUserScriptRef[5].animatorCommunicator.hasBall = false;
        if ((UnityEngine.Object) this.intendedReceiver == (UnityEngine.Object) null)
        {
          int userReceiver = this.FindUserReceiver(this.curUserScriptRef);
          this.passTarget = this.curUserPlayers[userReceiver];
          this.passTargetScript = this.curUserScriptRef[userReceiver];
        }
        else
        {
          this.passTarget = this.intendedReceiver.mainGO;
          this.passTargetScript = this.intendedReceiver;
        }
        ++this.playManager.userTeamCurrentPlayStats.players[this.passTargetScript.indexOnTeam].Targets;
        this.curUserScriptRef[5].blockType = BlockType.MoveToBallHolder;
        this.curUserScriptRef[5].lookForBlockTarget = true;
      }
      else
      {
        ++this.playManager.compTeamCurrentPlayStats.players[this.curCompScriptRef[5].indexOnTeam].QBAttempts;
        AnimatorCommunicator animatorCommunicator = this.curCompScriptRef[5].animatorCommunicator;
        this.curCompScriptRef[5].animatorCommunicator.hasBall = false;
        if (global::Game.Is2PMatch)
        {
          if ((UnityEngine.Object) this.intendedReceiver == (UnityEngine.Object) null)
          {
            this.compReceiver = this.FindUserReceiver(this.curCompScriptRef);
            this.passTarget = this.curCompPlayers[this.compReceiver];
            this.passTargetScript = this.curCompScriptRef[this.compReceiver];
          }
          else
          {
            this.passTarget = this.intendedReceiver.mainGO;
            this.passTargetScript = this.intendedReceiver;
          }
        }
        else
        {
          this.passTarget = this.curCompPlayers[this.compReceiver];
          this.passTargetScript = this.curCompScriptRef[this.compReceiver];
        }
        ++this.playManager.compTeamCurrentPlayStats.players[this.passTargetScript.indexOnTeam].Targets;
        this.curCompScriptRef[5].blockType = BlockType.MoveToBallHolder;
        this.curCompScriptRef[5].lookForBlockTarget = true;
      }
      this.BallHolderReleaseBall();
    }
  }

  public void HandOffBall(bool toss)
  {
    PEGameplayEventManager.RecordBallHandoffEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this.playManager.handOffTarget);
    if (global::Game.IsPlayerOneOnOffense)
      ++this.playManager.userTeamCurrentPlayStats.players[this.playManager.handOffTarget.indexOnTeam].RushAttempts;
    else
      ++this.playManager.compTeamCurrentPlayStats.players[this.playManager.handOffTarget.indexOnTeam].RushAttempts;
    this.BallHolderReleaseBall();
    this.ballWasThrownOrKicked = true;
    if (toss)
    {
      this.ballManager.Toss(this.playManager.handOffTarget);
      Ball.State.BallState.SetValue(EBallState.InAirToss);
    }
    else
      this.SetBallHolder(this.playManager.handOffTargetIndex, global::Game.IsPlayerOneOnOffense);
  }

  public void TriggerFakeHandoff() => PEGameplayEventManager.RecordBallHandoffEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this.playManager.handOffTarget, true);

  public void TriggerAbortedHandoff() => PEGameplayEventManager.RecordHandoffAbortedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, this.ballHolderScript, this.playManager.handOffTarget);

  public int FindUserReceiver(List<PlayerAI> players)
  {
    int userReceiver = -1;
    float num1 = 10000f;
    for (int index = 6; index < 11; ++index)
    {
      float num2 = Vector3.Distance(players[index].trans.position, this.passDestination);
      if ((double) num2 < (double) num1)
      {
        userReceiver = index;
        num1 = num2;
      }
    }
    if (userReceiver == -1)
      Debug.Log((object) "Unable to find a suitable receiver in FindUserReceiver. Something went wrong.");
    return userReceiver;
  }

  public void CheckForBlitzers()
  {
    int num = 0;
    if (global::Game.IsPlayerOneOnOffense)
    {
      for (int index = 0; index < 11; ++index)
      {
        if (this.curCompScriptRef[index].defType == EPlayAssignmentId.Blitz)
          ++num;
      }
    }
    else
    {
      for (int index = 0; index < 11; ++index)
      {
        if (this.curUserScriptRef[index].defType == EPlayAssignmentId.Blitz)
          ++num;
      }
    }
  }

  public void ExecuteKick()
  {
    if (this.ballWasThrownOrKicked)
    {
      Debug.LogWarning((object) "Ball Was Already Thrown or Kicked when ExcuteThrow called");
    }
    else
    {
      Ball.State.BallState.SetValue(EBallState.Kick);
      this.ballWasThrownOrKicked = true;
      List<PlayerAI> playerAiList;
      if (global::Game.IsPlayerOneOnOffense)
      {
        playerAiList = this.curUserScriptRef;
        this.SetUserPlayer(-1);
      }
      else
      {
        playerAiList = this.curCompScriptRef;
        if (global::Game.Is2PMatch)
          this.SetUserPlayerP2(-1);
      }
      PEGameplayEventManager.RecordBallKickedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, playerAiList[6], playerAiList[2], playerAiList[5]);
      float y = 0.0f;
      float b1 = Field.ONE_YARD * 25f;
      float b2 = Field.ONE_YARD * 25f;
      float b3 = Field.ONE_YARD * 42f;
      int num1 = global::Game.UserDoesNotControlPlayers ? 1 : (!global::Game.IsPlayerOneOnDefense ? 0 : (global::Game.IsNot2PMatch ? 1 : 0));
      float num2 = 0.0f;
      float kickAngle = 0.0f;
      float num3 = 0.0f;
      if (global::Game.IsFG)
        SingletonBehaviour<FieldManager, MonoBehaviour>.instance.lastFGKickPos = SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position.z;
      float x;
      float num4;
      if (num1 == 0)
      {
        float num5 = (float) (((double) playerAiList[6].GetKickingPower() + (double) num2 * 2.0) / 3.0);
        float num6 = 29f;
        if (PlayState.IsKickoff)
        {
          if ((double) kickAngle < 45.0)
            num6 -= (float) ((45.0 - (double) kickAngle) * 0.73000001907348633);
          if (global::Game.IsOnsidesKick)
          {
            x = (float) (1.8200000524520874 + (double) num5 / 11.0) * (float) global::Game.OffensiveFieldDirection;
            float num7 = (float) (5.4600000381469727 + (double) num5 / 22.0);
            num4 = ProEra.Game.MatchState.BallOn.Value + num7 * (float) global::Game.OffensiveFieldDirection;
          }
          else
          {
            x = this.matchManager.ballHashPosition + num3 * num6 * (float) global::Game.OffensiveFieldDirection;
            if ((double) kickAngle < 45.0)
              num5 -= (float) ((45.0 - (double) kickAngle) * 2.0);
            float num8 = Mathf.Max(num5 / 1.65f, b3);
            num4 = ProEra.Game.MatchState.BallOn.Value + num8 * (float) global::Game.OffensiveFieldDirection;
            if ((double) kickAngle < 45.0)
              num4 -= (float) ((45.0 - (double) kickAngle) * 0.550000011920929) * (float) global::Game.OffensiveFieldDirection;
          }
        }
        else if (global::Game.IsFG)
        {
          float num9 = 10f;
          x = this.matchManager.ballHashPosition + num3 * num9 * (float) global::Game.OffensiveFieldDirection;
          float num10 = Mathf.Max(num5 / 2.06f, b2);
          num4 = ProEra.Game.MatchState.BallOn.Value + num10 * (float) global::Game.OffensiveFieldDirection;
          if ((double) kickAngle < 45.0)
            num4 -= (float) ((45.0 - (double) kickAngle) * 0.550000011920929) * (float) global::Game.OffensiveFieldDirection;
        }
        else
        {
          if ((double) kickAngle < 30.0)
            kickAngle = 30f;
          x = this.matchManager.ballHashPosition + num3 * num6 * (float) global::Game.OffensiveFieldDirection;
          float num11 = Mathf.Max(num5 / 2.06f, b1);
          num4 = ProEra.Game.MatchState.BallOn.Value + num11 * (float) global::Game.OffensiveFieldDirection;
          if ((double) kickAngle < 45.0)
            num4 -= (float) ((45.0 - (double) kickAngle) * 0.550000011920929) * (float) global::Game.OffensiveFieldDirection;
        }
        if (!global::Game.IsOnsidesKick && global::Game.UserControlsPlayers && (global::Game.Is2PMatch || global::Game.IsPlayerOneOnOffense))
        {
          float num12 = (float) (-1.0 * (double) Mathf.Pow(!global::Game.OffenseGoingNorth ? (Field.FIELD_LENGTH - ProEra.Game.MatchState.BallOn.Value) / Field.FIELD_LENGTH : ProEra.Game.MatchState.BallOn.Value / Field.FIELD_LENGTH, 3f) + 1.0);
          float num13 = kickAngle / 50f;
          float num14 = this.matchManager.windSpeed.x * num12 * num13;
          float num15 = this.matchManager.windSpeed.z * num12 * num13;
          x += num14;
          num4 += num15;
        }
      }
      else
      {
        kickAngle = PlayState.IsPunt ? 50f : 50f;
        float kickingPower = (float) playerAiList[6].GetKickingPower();
        playerAiList[5].animatorCommunicator.hasBall = false;
        FieldGoalConfig fieldGoalConfig = global::Game.FieldGoalConfig;
        int num16 = UnityEngine.Random.Range(fieldGoalConfig.kickMeterValueMin, fieldGoalConfig.kickMeterValueMax + 1);
        float kickPower = (float) (((double) kickingPower + (double) num16) / 2.0);
        if (global::Game.IsFG)
        {
          int kickingAccuracy = playerAiList[6].kickingAccuracy;
          Vector3 kickLandingPos = FieldGoalUtil.GetKickLandingPos(kickPower, (float) kickingAccuracy, fieldGoalConfig);
          x = kickLandingPos.x;
          y = Field.FG_CROSSBAR_HEIGHT;
          num4 = kickLandingPos.z;
          if (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.fieldGoalsAlwaysMiss)
            x += 20f;
        }
        else if (PlayState.IsKickoff)
        {
          if (global::Game.IsOnsidesKick)
          {
            KickoffConfig kickoffConfig = global::Game.KickoffConfig;
            x = kickoffConfig.GetOnsideKickLandingSpotX() * (float) global::Game.OffensiveFieldDirection;
            num4 = (float) ProEra.Game.MatchState.BallOn + kickoffConfig.GetOnsideKickLandingSpotZ() * (float) global::Game.OffensiveFieldDirection;
          }
          else
          {
            x = this.GetKickDirBasedOnKickoffType();
            float num17 = Mathf.Max(kickPower / 1.65f, b3);
            num4 = ProEra.Game.MatchState.BallOn.Value + num17 * (float) global::Game.OffensiveFieldDirection;
          }
        }
        else
        {
          x = UnityEngine.Random.Range(-Field.OUT_OF_BOUNDS + Field.FIVE_YARDS, Field.OUT_OF_BOUNDS - Field.FIVE_YARDS);
          float num18 = Mathf.Max(kickPower / 2.06f, b1);
          num4 = ProEra.Game.MatchState.BallOn.Value + num18 * (float) global::Game.OffensiveFieldDirection;
          if (Field.FurtherDownfield(ProEra.Game.MatchState.BallOn.Value, Field.OWN_FORTY_FIVE_YARD_LINE))
          {
            int kickingAccuracy = playerAiList[6].kickingAccuracy;
            float maxInclusive1 = Field.SIX_YARDS + Mathf.Clamp((float) ((double) Field.ONE_YARD * 20.0 * ((75.0 - (double) kickingAccuracy) / 100.0)), -Field.SIX_YARDS, Field.ONE_YARD * 15f);
            x = (double) this.matchManager.ballHashPosition <= 0.0 ? -Field.OUT_OF_BOUNDS - UnityEngine.Random.Range(-maxInclusive1, maxInclusive1) : Field.OUT_OF_BOUNDS + UnityEngine.Random.Range(-maxInclusive1, maxInclusive1);
            if (Field.FurtherDownfield(num4, Field.OFFENSIVE_GOAL_LINE))
              num4 = Field.OFFENSIVE_GOAL_LINE - Field.ONE_YARD_FORWARD * 3f;
            float num19 = (float) (100 - kickingAccuracy) / 5f * Field.ONE_UNIT_PER_YARD;
            float maxInclusive2 = Field.ONE_YARD * 5f + num19;
            num4 += UnityEngine.Random.Range(-maxInclusive2, maxInclusive2);
          }
        }
      }
      Vector3 target = new Vector3(x, y, num4);
      this.BallHolderReleaseBall();
      this.ballManager.Kick(target, kickAngle);
      this.passDestination = target;
      if (!PlayState.IsPuntOrKickoff)
        return;
      this.ballLandingSpot.position = new Vector3(this.passDestination.x, this.ballLandingSpot.position.y, this.passDestination.z);
      this.ballLandingSpotGO.SetActive(true);
      if (this.matchManager.onsideKick)
      {
        if (global::Game.IsPlayerOneOnOffense)
        {
          for (int index = 0; index < 11; ++index)
            this.curUserScriptRef[index].animatorCommunicator.SetGoal(this.passDestination);
        }
        else
        {
          for (int index = 0; index < 11; ++index)
            this.curCompScriptRef[index].animatorCommunicator.SetGoal(this.passDestination);
        }
      }
      else if (global::Game.IsPlayerOneOnOffense)
      {
        for (int index = 0; index < 11; ++index)
          this.curUserScriptRef[index].animatorCommunicator.SetGoal(new Vector3(this.curUserScriptRef[index].trans.position.x, 0.0f, this.passDestination.z));
      }
      else
      {
        for (int index = 0; index < 11; ++index)
          this.curCompScriptRef[index].animatorCommunicator.SetGoal(new Vector3(this.curCompScriptRef[index].trans.position.x, 0.0f, this.passDestination.z));
      }
    }
  }

  private float GetKickDirBasedOnKickoffType()
  {
    if (this.playManager.savedOffPlay == Plays.self.spc_kickoffLeft)
      return UnityEngine.Random.Range((float) (-(double) Field.ONE_YARD * 16.0), 0.0f);
    return this.playManager.savedOffPlay == Plays.self.spc_kickoffRight ? UnityEngine.Random.Range(0.0f, Field.ONE_YARD * 16f) : UnityEngine.Random.Range(-Field.EIGHT_YARDS, Field.EIGHT_YARDS);
  }

  public void AISnapBall()
  {
    this.StopAISnapBallCoroutine();
    this.snapBallCoroutine = this.StartCoroutine(this.AISnapBallCoroutine(4f));
  }

  public void StopAISnapBallCoroutine()
  {
    if (this.snapBallCoroutine == null)
      return;
    this.StopCoroutine(this.snapBallCoroutine);
  }

  private int FindClosestUserPlayer(int player)
  {
    int closestUserPlayer = -1;
    float num1 = 10000f;
    Vector3 b = !((UnityEngine.Object) this.ballHolder == (UnityEngine.Object) null) ? this.ballHolderScript.trans.position : (!global::Game.BS_IsInAirPass ? this.ballManager.trans.position : this.passDestination);
    for (int index = 0; index < 11; ++index)
    {
      if (player == 1)
      {
        if (!this.curUserScriptRef[index].isEngagedInBlock)
        {
          float num2 = Vector3.Distance(this.curUserScriptRef[index].trans.position, b);
          if ((double) num2 < (double) num1)
          {
            closestUserPlayer = index;
            num1 = num2;
          }
        }
      }
      else if (!this.curCompScriptRef[index].isEngagedInBlock)
      {
        float num3 = Vector3.Distance(this.curCompScriptRef[index].trans.position, b);
        if ((double) num3 < (double) num1)
        {
          closestUserPlayer = index;
          num1 = num3;
        }
      }
    }
    return closestUserPlayer;
  }

  private IEnumerator AISnapBallCoroutine(float timeToSnap)
  {
    PlayersManager playersManager = this;
    timeToSnap += UnityEngine.Random.Range(0.0f, 1.5f);
    playersManager._snapBallDelay = new WaitForSeconds(timeToSnap);
    yield return (object) playersManager._snapBallDelay;
    if (GameTimeoutState.NoTimeoutCalled())
    {
      if (playersManager.matchManager.IsSnapAllowed() && !global::Game.HasScreenOverlay && !playersManager.matchManager.penaltyManager.isPenaltyOnPlay)
      {
        playersManager.savedDefPlayer = playersManager.userPlayerIndex;
        if (playersManager.matchManager.penaltyManager.IsPenaltyBeforePlay(PersistentData.GetOffensiveTeamIndex()))
          playersManager.matchManager.DisallowSnap();
        else if ((!global::Game.IsPlayerOneOnDefense ? ProEra.Game.MatchState.Stats.IsCompLoosing() && MatchManager.instance.timeManager.GetDisplayMinutes() < 2 && MatchManager.instance.timeManager.IsGameClockRunning() && MatchManager.instance.timeManager.IsFourthQuarter() : ProEra.Game.MatchState.Stats.IsUserLoosing() && MatchManager.instance.timeManager.GetDisplayMinutes() < 2 && MatchManager.instance.timeManager.IsGameClockRunning() && MatchManager.instance.timeManager.IsFourthQuarter()) && SingletonBehaviour<PenaltyManager, MonoBehaviour>.instance.OptDelayOfGame && MatchManager.instance.timeManager.GetPlayClockTime() > 10)
          playersManager.snapBallCoroutine = playersManager.StartCoroutine(playersManager.AISnapBallCoroutine(3f));
        else
          playersManager.matchManager.SnapBall((Vector2) Input.mousePosition);
      }
      else
        playersManager.snapBallCoroutine = playersManager.StartCoroutine(playersManager.AISnapBallCoroutine(2f));
    }
  }

  public void SetVolume(float vol)
  {
    for (int index = 0; index < this.curUserScriptRef.Count; ++index)
      this.curUserScriptRef[index].SetVolume(vol);
    for (int index = 0; index < this.curCompScriptRef.Count; ++index)
      this.curCompScriptRef[index].SetVolume(vol);
  }

  public void WriteGameStatsToCsv()
  {
    string str1 = DateTime.Now.ToString("yyyyMMddHHmmss");
    string str2 = PersistentData.GameMode.ToString();
    string str3 = PersistentData.gameType.ToString();
    CsvExport csvExport1 = new CsvExport();
    for (int playerIndex = 0; playerIndex < this.userTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      PlayerData player = this.userTeamData.GetPlayer(playerIndex);
      csvExport1.AddRow(player.GetCsvRowForPlayerPositionAndName().Concat<CsvCell>((IEnumerable<CsvCell>) player.CurrentGameStats.GetCsvRow()).ToList<CsvCell>());
    }
    csvExport1.WriteToFile("Logs", PersistentData.GetUserTeam().GetAbbreviation() + "_" + str3 + "_" + str2 + "_" + str1 + "_PlayerStats.csv");
    CsvExport csvExport2 = new CsvExport();
    for (int playerIndex = 0; playerIndex < this.compTeamData.GetNumberOfPlayersOnRoster(); ++playerIndex)
    {
      PlayerData player = this.compTeamData.GetPlayer(playerIndex);
      csvExport2.AddRow(player.GetCsvRowForPlayerPositionAndName().Concat<CsvCell>((IEnumerable<CsvCell>) player.CurrentGameStats.GetCsvRow()).ToList<CsvCell>());
    }
    csvExport2.WriteToFile("Logs", PersistentData.GetCompTeam().GetAbbreviation() + "_" + str3 + "_" + str2 + "_" + str1 + "_PlayerStats.csv");
  }

  public void ActivateFumble()
  {
    MatchManager instance1 = MatchManager.instance;
    BallManager instance2 = SingletonBehaviour<BallManager, MonoBehaviour>.instance;
    if (!instance1.fumbleOccured)
    {
      instance1.fumbleOccured = true;
      MatchManager.instance.playManager.fumbleOccurredTimer = MatchManager.instance.playTimer;
    }
    this.passCircleTrans.gameObject.SetActive(false);
    instance1.fumbleTimer = 0.0f;
    instance2.FumbleBall();
    this.BallHolderReleaseBall();
    ProEra.Game.MatchState.Turnover.Value = false;
  }

  public void DisableCenterBlockingForPlayInit()
  {
    if (!global::Game.IsRun && !global::Game.IsPlayAction || this.playManager.savedOffPlay.GetHandoffType() == HandoffType.None)
      return;
    PlayerAI currentCenter = this.GetCurrentCenter();
    currentCenter.blockTarget = (PlayerAI) null;
    currentCenter.lookForBlockTarget = false;
    this.CenterBlockEnabled = false;
  }

  public void EnableCenterBlockingForPlayInit()
  {
    if (!global::Game.IsRun && !global::Game.IsPlayAction || this.playManager.savedOffPlay.GetHandoffType() == HandoffType.None)
      return;
    PlayerAI currentCenter = this.GetCurrentCenter();
    currentCenter.blockTarget = currentCenter.initialBlockTarget;
    currentCenter.lookForBlockTarget = true;
    this.CenterBlockEnabled = true;
  }
}
