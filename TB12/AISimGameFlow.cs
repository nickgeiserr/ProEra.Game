// Decompiled with JetBrains decompiler
// Type: TB12.AISimGameFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using ProEra.Game;
using TB12.AppStates;
using UDB;
using UnityEngine;

namespace TB12
{
  public class AISimGameFlow : MonoBehaviour
  {
    [SerializeField]
    private AISimGameState _state;
    [SerializeField]
    private AISimGameScene _scene;
    [SerializeField]
    private PlayersManager _playersManager;
    [SerializeField]
    private Transform _sideLinePosition;
    private bool _isOnOffense;
    private PlayerAI _qbAI;
    private Transform _currentFollowTx;
    private bool _doRotationReset;
    private bool _canFumble;
    private bool _didAnnouncePlayEnd;
    private bool _didStartPlay;
    private bool _playActive;
    private bool _wasIntercepted;
    private bool _gameStarted;
    private bool _isOnSideline;
    private float _crowdStareTimer;
    private float _isReturnToPosition;
    private HandData _snapHand;
    private Material _crowdMaterial;
    private float _crowdExtraReactionLevel;
    public float DropbackDuration = 0.6f;
    public float DropbackTimeScale = 0.25f;
    private float _ballHeldTime;
    [SerializeField]
    private AISimGameFlow.QBFollowTypes _followType;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private EPlayEndType _lastPlayEndType;

    protected virtual void Awake()
    {
      BallManager instance = SingletonBehaviour<BallManager, MonoBehaviour>.instance;
      if ((Object) instance == (Object) null || (Object) instance.GetComponent<BallObject>() == (Object) null)
        return;
      this._isOnOffense = global::Game.IsPlayerOneOnOffense;
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      AppSounds.StopSideline();
      MainGameTablet.SelfDestroy();
      if (UnityState.quitting)
        return;
      MatchManager.Exists();
    }

    public void MovePlayerCamera()
    {
    }

    private void Update()
    {
    }

    private enum QBFollowTypes
    {
      OnCall,
      FollowBody,
      FollowHead,
    }
  }
}
