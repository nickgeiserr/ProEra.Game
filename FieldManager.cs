// Decompiled with JetBrains decompiler
// Type: FieldManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using MovementEffects;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class FieldManager : SingletonBehaviour<FieldManager, MonoBehaviour>
{
  [Header("Referees")]
  [SerializeField]
  private Transform fieldJudge_East;
  [SerializeField]
  private Transform lineJudge_East;
  [SerializeField]
  private Transform sideJudge_West;
  [SerializeField]
  private Transform downJudge_West;
  [Header("Chain Gang")]
  [SerializeField]
  private Transform chainGangMarkerLOS_East;
  [SerializeField]
  private Transform chainGangMarkerFirstDown_East;
  [SerializeField]
  private Transform chainGangMarkerLOS_West;
  [SerializeField]
  private Transform chainGangMarkerFirstDown_West;
  [SerializeField]
  private Transform chainGangDown_East;
  [SerializeField]
  private Transform chainGangDown_West;
  [Header("Field Objects")]
  public Transform lineOfScrimTrans;
  public Transform firstDownTrans;
  public GameObject tee;
  public GameObject lineOfScrimmage;
  public GameObject firstDownLine;
  [SerializeField]
  private Material downMarkerMaterial;
  [SerializeField]
  private Texture[] downMarkerTextures;
  [HideInInspector]
  public float tempBallPos;
  [HideInInspector]
  public float savedBallOn;
  [HideInInspector]
  public float yardsToGo;
  [HideInInspector]
  public float lastFGKickPos;
  private MatchManager matchManager;
  private BallManager ballManager;
  private CoroutineHandle setLOSCoroutine;
  private CoroutineHandle setFirstDownCoroutine;
  private CoroutineHandle setChainGangCoroutine;
  private CoroutineHandle setSidelineRefCoroutine;

  private void Start()
  {
    this.matchManager = MatchManager.instance;
    this.ballManager = this.matchManager.ballManager;
  }

  private void OnDestroy()
  {
    Debug.Log((object) "FieldManager -> OnDestroy");
    SingletonBehaviour<FieldManager, MonoBehaviour>.instance = (FieldManager) null;
    this.StopAllCoroutines();
    Timing.KillCoroutines();
  }

  public void SetBallOnTee()
  {
    this.tee.SetActive(true);
    this.tee.transform.position = new Vector3(-Field.FIFTEEN_INCHES * (float) global::Game.OffensiveFieldDirection, 0.0f, ProEra.Game.MatchState.BallOn.Value);
    this.tee.transform.eulerAngles = new Vector3(0.0f, 90f * (float) global::Game.OffensiveFieldDirection, 0.0f);
    Ball.State.BallState.SetValue(EBallState.OnTee);
    this.ballManager.FreezeAfterPlay();
  }

  public void SetFirstDownAndFirstDownLine()
  {
    ProEra.Game.MatchState.Down.Value = 1;
    ProEra.Game.MatchState.FirstDown.Value = ProEra.Game.MatchState.BallOn.Value + (float) ((double) Field.ONE_YARD * (double) global::Game.OffensiveFieldDirection * 10.0);
  }

  public void SetFirstDownLine()
  {
    if (this.setFirstDownCoroutine.IsRunning)
      return;
    this.setFirstDownCoroutine = Timing.RunCoroutine(this.SetFirstDownLineInTransition());
  }

  private IEnumerator<float> SetFirstDownLineInTransition()
  {
    if ((UnityEngine.Object) PersistentSingleton<GamePlayerController>.Instance != (UnityEngine.Object) null)
      yield return Timing.WaitUntilTrue((Func<bool>) (() => PersistentSingleton<GamePlayerController>.Instance.IsFaded));
    if ((double) ProEra.Game.MatchState.FirstDown.Value >= (double) Field.NORTH_GOAL_LINE || (double) ProEra.Game.MatchState.FirstDown.Value <= (double) Field.SOUTH_GOAL_LINE)
    {
      FieldState.FirstDownLineVisible.SetValue(false);
    }
    else
    {
      FieldState.FirstDownLineVisible.SetValue(true);
      this.firstDownTrans.position = new Vector3(0.0f, Field.THREE_INCHES, ProEra.Game.MatchState.FirstDown.Value + ((bool) FieldState.OffenseGoingNorth ? 0.15f : -0.15f));
    }
    this.firstDownLine.SetActive(FieldState.FirstDownLineVisible.Value);
  }

  public void SetLineOfScrimmageLine()
  {
    if (this.setLOSCoroutine.IsRunning)
      return;
    this.setLOSCoroutine = Timing.RunCoroutine(this.SetLineOfScrimmageLineInTransition());
  }

  public void SetLineOfScrimmageLine(float xOffsetOnBall = 0.0f)
  {
    this.lineOfScrimTrans.position = new Vector3(xOffsetOnBall, Field.ONE_INCH, ProEra.Game.MatchState.BallOn.Value);
    this.lineOfScrimmage.SetActive(true);
  }

  private IEnumerator<float> SetLineOfScrimmageLineInTransition()
  {
    if ((UnityEngine.Object) PersistentSingleton<GamePlayerController>.Instance != (UnityEngine.Object) null)
      yield return Timing.WaitUntilTrue((Func<bool>) (() => PersistentSingleton<GamePlayerController>.Instance.IsFaded));
    this.lineOfScrimTrans.position = new Vector3(0.0f, Field.ONE_INCH, ProEra.Game.MatchState.BallOn.Value);
    this.lineOfScrimmage.SetActive(true);
  }

  public void SetChainGangPositions()
  {
    if (this.setChainGangCoroutine.IsRunning)
      return;
    this.setChainGangCoroutine = Timing.RunCoroutine(this.SetChainGangPositionsInTransition());
  }

  private IEnumerator<float> SetChainGangPositionsInTransition()
  {
    if ((UnityEngine.Object) PersistentSingleton<GamePlayerController>.Instance != (UnityEngine.Object) null)
      yield return Timing.WaitUntilTrue((Func<bool>) (() => PersistentSingleton<GamePlayerController>.Instance.IsFaded));
    float oneFoot = Field.ONE_FOOT;
    float threeYards = Field.THREE_YARDS;
    float twoYards = Field.TWO_YARDS;
    if ((UnityEngine.Object) this.chainGangMarkerLOS_East != (UnityEngine.Object) null)
      this.chainGangMarkerLOS_East.transform.position = new Vector3(Field.OUT_OF_BOUNDS + threeYards, 0.0f, (float) ((double) MatchManager.firstDown - (double) oneFoot - (double) Field.ONE_YARD_FORWARD * 10.0));
    if ((UnityEngine.Object) this.chainGangMarkerLOS_West != (UnityEngine.Object) null)
      this.chainGangMarkerLOS_West.transform.position = new Vector3(-Field.OUT_OF_BOUNDS - threeYards, 0.0f, (float) ((double) MatchManager.firstDown + (double) oneFoot - (double) Field.ONE_YARD_FORWARD * 10.0));
    if ((UnityEngine.Object) this.chainGangMarkerFirstDown_East != (UnityEngine.Object) null)
      this.chainGangMarkerFirstDown_East.transform.position = new Vector3(Field.OUT_OF_BOUNDS + threeYards, 0.0f, MatchManager.firstDown - oneFoot);
    if ((UnityEngine.Object) this.chainGangMarkerFirstDown_West != (UnityEngine.Object) null)
      this.chainGangMarkerFirstDown_West.transform.position = new Vector3(-Field.OUT_OF_BOUNDS - threeYards, 0.0f, MatchManager.firstDown + oneFoot);
    if ((UnityEngine.Object) this.chainGangDown_East != (UnityEngine.Object) null)
      this.chainGangDown_East.transform.position = new Vector3(Field.OUT_OF_BOUNDS + twoYards, 0.0f, MatchManager.ballOn - oneFoot);
    if ((UnityEngine.Object) this.chainGangDown_West != (UnityEngine.Object) null)
      this.chainGangDown_West.transform.position = new Vector3(-Field.OUT_OF_BOUNDS - twoYards, 0.0f, MatchManager.ballOn + oneFoot);
  }

  public void SetRefereePositions()
  {
    if (this.setSidelineRefCoroutine.IsRunning)
      return;
    this.setSidelineRefCoroutine = Timing.RunCoroutine(this.SetRefereePositionsInTransition());
  }

  private IEnumerator<float> SetRefereePositionsInTransition()
  {
    if ((UnityEngine.Object) PersistentSingleton<GamePlayerController>.Instance != (UnityEngine.Object) null)
      yield return Timing.WaitUntilTrue((Func<bool>) (() => PersistentSingleton<GamePlayerController>.Instance.IsFaded));
    if ((UnityEngine.Object) this.lineJudge_East != (UnityEngine.Object) null)
      this.lineJudge_East.position = new Vector3(Field.OUT_OF_BOUNDS + Field.ONE_YARD, 0.0f, MatchManager.ballOn);
    if ((UnityEngine.Object) this.fieldJudge_East != (UnityEngine.Object) null)
      this.fieldJudge_East.position = new Vector3(Field.OUT_OF_BOUNDS + Field.ONE_YARD, 0.0f, MatchManager.ballOn + Field.ONE_YARD_FORWARD * 10f);
    if ((UnityEngine.Object) this.sideJudge_West != (UnityEngine.Object) null)
      this.sideJudge_West.position = new Vector3(-Field.OUT_OF_BOUNDS - Field.ONE_YARD, 0.0f, MatchManager.ballOn + Field.ONE_YARD_FORWARD * 10f);
    if ((UnityEngine.Object) this.downJudge_West != (UnityEngine.Object) null)
      this.downJudge_West.position = new Vector3(-Field.OUT_OF_BOUNDS - Field.ONE_YARD, 0.0f, MatchManager.ballOn);
  }

  public void ToggleSidelineOfficialsForSide(bool isHomeSide, bool isVisible)
  {
    if (!((UnityEngine.Object) this.lineJudge_East != (UnityEngine.Object) null) || !((UnityEngine.Object) this.fieldJudge_East != (UnityEngine.Object) null) || !((UnityEngine.Object) this.chainGangMarkerLOS_East != (UnityEngine.Object) null) || !((UnityEngine.Object) this.chainGangMarkerFirstDown_East != (UnityEngine.Object) null))
      return;
    if (isHomeSide)
    {
      this.lineJudge_East.gameObject.SetActive(isVisible);
      this.fieldJudge_East.gameObject.SetActive(isVisible);
      this.chainGangMarkerLOS_East.gameObject.SetActive(isVisible);
      this.chainGangMarkerFirstDown_East.gameObject.SetActive(isVisible);
    }
    else
    {
      this.sideJudge_West.gameObject.SetActive(isVisible);
      this.downJudge_West.gameObject.SetActive(isVisible);
      this.chainGangMarkerLOS_West.gameObject.SetActive(isVisible);
      this.chainGangMarkerFirstDown_West.gameObject.SetActive(isVisible);
    }
  }

  public void SetDownMarkerTexture()
  {
    int down = MatchManager.down;
    if (down < 1 || down > 4)
      return;
    this.downMarkerMaterial.mainTexture = this.downMarkerTextures[down - 1];
  }

  public void ToggleSidelinePlayers(bool show)
  {
  }
}
