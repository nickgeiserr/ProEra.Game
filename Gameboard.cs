// Decompiled with JetBrains decompiler
// Type: Gameboard
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Gameboard", menuName = "NGS/Gameboard")]
public class Gameboard : ScriptableSingleton<Gameboard>
{
  public PlaybackInfo playbackInfo;
  public bool allowEventGeneration = true;
  [ReadOnly]
  public PlayContext context;
  [ReadOnly]
  public GameObject football;
  [ReadOnly]
  public List<PlayerIdentity> homePlayers;
  [ReadOnly]
  public List<PlayerIdentity> awayPlayers;
  [SerializeField]
  public List<GameplayEvent> scriptedEvents;
  [ReadOnly]
  [SerializeField]
  public List<GameplayEvent> generatedEvents;
  [ReadOnly]
  public List<EventListenerList> eventListeners;
  public static Action<GameplayEvent> OnEventRaised;
  public static float EventHeadsUpTime = 0.5f;
  [HideInInspector]
  public List<HashedString> EligibleReceiversPositionGroups = new List<HashedString>()
  {
    new HashedString("QB"),
    new HashedString("WR"),
    new HashedString("TE"),
    new HashedString("RB"),
    new HashedString("FB"),
    new HashedString("P")
  };
  private int _lastGameAttentionCenterCalulationFrame = -1;
  [ReadOnly]
  [SerializeField]
  private Vector3 _gameAttentionCenter;

  [ContextMenu("Add Snap Hack")]
  public void AddSnapHACK() => this.AddScriptedEvent(new GameplayEvent()
  {
    name = StateMachine.BallSnapEventHash,
    time = 0.0f,
    raised = false
  });

  public void BallSnapHandler()
  {
    int num = this.allowEventGeneration ? 1 : 0;
  }

  private List<PlayerIdentity> GetOffensivePlayers()
  {
    int num = (UnityEngine.Object) this.homePlayers.Find((Predicate<PlayerIdentity>) (x => this.EligibleReceiversPositionGroups.Contains(x.positionGroup))) != (UnityEngine.Object) null ? 1 : 0;
    bool flag = (UnityEngine.Object) this.awayPlayers.Find((Predicate<PlayerIdentity>) (x => this.EligibleReceiversPositionGroups.Contains(x.positionGroup))) != (UnityEngine.Object) null;
    return num == 0 || flag ? this.awayPlayers : this.homePlayers;
  }

  public void ExtractPlayContext(DataSensitiveStructs_v5.PlayData playData)
  {
    this.context = new PlayContext(playData);
    for (int index = 0; index < this.scriptedEvents.Count; ++index)
      this.scriptedEvents[index].raised = false;
    for (int index = 0; index < this.generatedEvents.Count; ++index)
      this.generatedEvents[index].raised = false;
  }

  public void AddScriptedEvent(GameplayEvent gameplayEvent)
  {
    this.scriptedEvents.Add(gameplayEvent);
    this.OnScriptedEventAddedHandler(gameplayEvent);
  }

  public Vector3 GameAttentionCenter
  {
    get
    {
      if (Time.frameCount != this._lastGameAttentionCenterCalulationFrame)
      {
        this._gameAttentionCenter = this.CalculateGameAttentionCenter();
        this._lastGameAttentionCenterCalulationFrame = Time.frameCount;
      }
      return this._gameAttentionCenter;
    }
  }

  private Vector3 CalculateGameAttentionCenter()
  {
    Vector3 zero1 = Vector3.zero;
    float num1 = 0.5f;
    float num2 = 3f;
    Vector3 position = this.football.transform.position;
    List<Vector3> vector3List = new List<Vector3>();
    vector3List.Add(position);
    for (int index = 0; index < this.homePlayers.Count; ++index)
    {
      PlayerIdentity homePlayer = this.homePlayers[index];
      float num3 = Vector3.Distance(position, homePlayer.transform.position);
      if ((((double) num3 < (double) num1 ? 0 : ((double) num3 <= (double) num2 ? 1 : 0)) & (this.EligibleReceiversPositionGroups.Contains(homePlayer.positionGroup) ? 1 : 0)) != 0)
        vector3List.Add(homePlayer.transform.position);
    }
    for (int index = 0; index < this.awayPlayers.Count; ++index)
    {
      PlayerIdentity awayPlayer = this.awayPlayers[index];
      float num4 = Vector3.Distance(position, awayPlayer.transform.position);
      if ((((double) num4 < (double) num1 ? 0 : ((double) num4 <= (double) num2 ? 1 : 0)) & (this.EligibleReceiversPositionGroups.Contains(awayPlayer.positionGroup) ? 1 : 0)) != 0)
        vector3List.Add(awayPlayer.transform.position);
    }
    Vector3 zero2 = Vector3.zero;
    for (int index = 0; index < vector3List.Count; ++index)
      zero2 += vector3List[index];
    return zero2 / (float) vector3List.Count;
  }

  public void RaiseEvent(GameplayEvent gameplayEvent)
  {
    gameplayEvent.raised = true;
    Action<GameplayEvent> onEventRaised = Gameboard.OnEventRaised;
    if (onEventRaised != null)
      onEventRaised(gameplayEvent);
    EventListenerList eventListeners = this.FindEventListeners(gameplayEvent.name);
    if (eventListeners == null)
      return;
    for (int index = 0; index < eventListeners.listeners.Count; ++index)
      eventListeners.listeners[index]();
  }

  public EventListenerList FindEventListeners(HashedString eventName) => this.eventListeners.Find((Predicate<EventListenerList>) (x => x.eventName.Hash == eventName.Hash));

  public void Reset()
  {
    this.homePlayers = new List<PlayerIdentity>();
    this.awayPlayers = new List<PlayerIdentity>();
    this.scriptedEvents = new List<GameplayEvent>();
    this.generatedEvents = new List<GameplayEvent>();
    this.eventListeners = new List<EventListenerList>()
    {
      new EventListenerList()
      {
        eventName = StateMachine.LineSetEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.ManInMotionEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.ShiftEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.BallSnapEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.HandoffEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.PlayActionEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.PassFowardEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.PassArrivedEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.PassOutcomeCaughtEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.TouchdownEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.TackleEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.FirstContactEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.PenaltyFlagEventHash
      },
      new EventListenerList()
      {
        eventName = StateMachine.PenaltyDeclinedEventHash
      }
    };
    this.AddStandardHandlers();
  }

  public void AddStandardHandlers() => this.FindEventListeners(StateMachine.BallSnapEventHash)?.listeners.Add(new UnityAction(this.BallSnapHandler));

  public void OnScriptedEventAddedHandler(GameplayEvent gameplayEvent)
  {
    if (!new List<HashedString>()
    {
      StateMachine.BallSnapEventHash,
      StateMachine.HandoffEventHash,
      StateMachine.PassFowardEventHash,
      StateMachine.PassArrivedEventHash,
      StateMachine.TackleEventHash
    }.Contains(gameplayEvent.name))
      return;
    this.generatedEvents.Add(new GameplayEvent()
    {
      name = new HashedString("gen_pre_" + gameplayEvent.name.Str),
      time = gameplayEvent.time - Gameboard.EventHeadsUpTime
    });
  }

  public void Tick()
  {
    float playTime = this.playbackInfo.PlayTime;
    for (int index = 0; index < this.scriptedEvents.Count; ++index)
    {
      GameplayEvent scriptedEvent = this.scriptedEvents[index];
      if (!scriptedEvent.raised && (double) scriptedEvent.time <= (double) playTime)
        this.RaiseEvent(scriptedEvent);
    }
    for (int index = 0; index < this.generatedEvents.Count; ++index)
    {
      GameplayEvent generatedEvent = this.generatedEvents[index];
      if (!generatedEvent.raised && (double) generatedEvent.time <= (double) playTime)
        this.RaiseEvent(generatedEvent);
    }
  }
}
