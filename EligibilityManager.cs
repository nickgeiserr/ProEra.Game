// Decompiled with JetBrains decompiler
// Type: EligibilityManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EligibilityManager : MonoBehaviour
{
  public static EligibilityManager Instance;
  private int MAX_ELIGIBILITY_COVERAGE = 100;
  private float MIN_DISTANCE = 0.5f;
  private float MAX_DISTANCE = 5f;
  private bool _updateEligibility;
  private List<ReceiverUI> _receivers;
  private ReceiverUI _currentReceiver;
  private ReceiverUI _possibleCurrentReceiver;
  private float _currentReceiverDot;
  private int _currentReceiverIndex;
  private float _swtichTimer;
  private float _maxSwitchTime;
  private readonly LinksHandler _linksHandler = new LinksHandler();

  private void Awake()
  {
    EligibilityManager.Instance = this;
    this._receivers = new List<ReceiverUI>();
    this._linksHandler.SetLinks(new List<EventHandle>()
    {
      Ball.State.BallState.Link<EBallState>(new Action<EBallState>(this.HandleBallState)),
      PlaybookState.HidePlaybook.Link(new System.Action(this.HidePlaybookHandler)),
      PlayState.PlayOver.Link<bool>(new Action<bool>(this.OnPlayComplete))
    });
  }

  public static bool IsActive => EligibilityManager.Instance._updateEligibility;

  private void OnDestroy()
  {
    Debug.Log((object) "EligibilityManager -> OnDestroy");
    this.StopAllCoroutines();
    this._receivers.Clear();
    this._linksHandler.Clear();
    EligibilityManager.Instance = (EligibilityManager) null;
  }

  private void Update()
  {
    if (!global::Game.IsPlayerOneOnOffense || !this._updateEligibility || Globals.ReplayMode.Value)
      return;
    this.GetClosestReceiver();
    if (!((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null))
      return;
    this._currentReceiver.UpdateEligibility(Mathf.Clamp(this.GetReceiverEligibility(this._currentReceiver.transform), 0.0f, 100f) / 100f);
  }

  public void HidePlaybookHandler() => this.UpdateEligibleReceivers();

  public void UpdateEligibleReceivers()
  {
    PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
    if (savedOffPlay == null || savedOffPlay.GetPlayType() != PlayType.Pass || (bool) PlayState.PlayOver)
      return;
    this._receivers.Clear();
    List<PlayerAI> curUserScriptRef = MatchManager.instance.playersManager.curUserScriptRef;
    for (int index = 0; index < 11; ++index)
    {
      if (curUserScriptRef[index].blockType == BlockType.None && curUserScriptRef[index].indexInFormation > 5)
        this._receivers.Add(curUserScriptRef[index].GetComponent<ReceiverUI>());
    }
    this._currentReceiver = (ReceiverUI) null;
    this._currentReceiverIndex = -1;
    this._currentReceiverDot = -1f;
    this._updateEligibility = true;
  }

  public void OnPlayComplete(bool isOver)
  {
    if (!isOver)
      return;
    this.TurnOffReceiverUI();
  }

  private void HandleBallState(EBallState state)
  {
    if (state != EBallState.InAirPass)
      return;
    this.TurnOffReceiverUI();
  }

  public void TurnOffReceiverUI()
  {
    this._updateEligibility = false;
    for (int index = 0; index < this._receivers.Count; ++index)
      this._receivers[index].SetActiveUI(false);
  }

  public float GetReceiverEligibility(Transform receiver) => 0.0f + this.CalculateEligibilityCoverage(receiver);

  private float CalculateEligibilityCoverage(Transform receiver)
  {
    float eligibilityCoverage = (float) this.MAX_ELIGIBILITY_COVERAGE;
    for (int index = 0; index < 11; ++index)
    {
      PlayerAI defensivePlayer = global::Game.DefensivePlayers[index];
      if (!defensivePlayer.isEngagedInBlock)
      {
        float num = Vector3.Distance(receiver.position, defensivePlayer.trans.position);
        if ((double) num < (double) this.MAX_DISTANCE)
          eligibilityCoverage -= (float) (1.0 - ((double) num - (double) this.MIN_DISTANCE) / ((double) this.MAX_DISTANCE - (double) this.MIN_DISTANCE)) * (float) this.MAX_ELIGIBILITY_COVERAGE;
      }
    }
    return eligibilityCoverage;
  }

  private void GetClosestReceiver()
  {
    Transform transform = PlayerCamera.Camera.transform;
    Vector3 forward = transform.forward;
    Vector3 vector3_1 = (bool) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing ? transform.position.SetY(0.0f) : transform.position;
    float num1 = -1f;
    int index1 = -1;
    if (this._receivers.Count == 0)
      return;
    for (int index2 = 0; index2 < this._receivers.Count; ++index2)
    {
      ReceiverUI receiver = this._receivers[index2];
      Vector3 vector3_2 = (bool) ScriptableSingleton<VRSettings>.Instance.AlphaThrowing ? receiver.transform.position + receiver.transform.forward : receiver.transform.position;
      float num2 = Vector3.Dot(forward, (vector3_2 - vector3_1).normalized);
      if ((double) num2 > (double) num1)
      {
        num1 = num2;
        index1 = index2;
      }
      if (index2 == this._currentReceiverIndex)
        this._currentReceiverDot = num2;
    }
    float num3 = 0.93f;
    float num4 = 0.8f;
    if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) this._receivers[index1] && (double) num1 >= (double) num3 && (double) this._currentReceiverDot < (double) num3)
    {
      if ((UnityEngine.Object) this._possibleCurrentReceiver != (UnityEngine.Object) this._receivers[index1])
      {
        this._swtichTimer = 0.0f;
        this._possibleCurrentReceiver = this._receivers[index1];
      }
      else
      {
        this._swtichTimer += Time.deltaTime;
        if ((double) this._swtichTimer < (double) this._maxSwitchTime)
          return;
        if ((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null)
          this._currentReceiver.SetActiveUI(false);
        this._currentReceiver = this._receivers[index1];
        this._currentReceiverIndex = index1;
        this._currentReceiverDot = num1;
        this._currentReceiver.SetActiveUI(ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.ShowReceiverHighlights);
      }
    }
    else
    {
      if (!((UnityEngine.Object) this._currentReceiver != (UnityEngine.Object) null) || (double) this._currentReceiverDot >= (double) num4)
        return;
      this._currentReceiver.SetActiveUI(false);
      this._currentReceiver = (ReceiverUI) null;
      this._currentReceiverIndex = -1;
      this._currentReceiverDot = -1f;
    }
  }

  public ReceiverUI GetCurrentTarget() => this._currentReceiver;

  public void SetCurrentTarget(PlayerAI player)
  {
    if (!Globals.ReplayMode.Value)
      return;
    this._currentReceiver = player.GetComponent<ReceiverUI>();
  }

  public void SetCurrentTarget(ReceiverUI receiverUI) => this._currentReceiver = receiverUI;
}
