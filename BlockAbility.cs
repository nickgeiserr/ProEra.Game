// Decompiled with JetBrains decompiler
// Type: BlockAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UDB;
using UnityEngine;

public class BlockAbility : MonoBehaviour
{
  private static Dictionary<BlockAbility, BlockAbility> _defenderToBlockerDict = new Dictionary<BlockAbility, BlockAbility>();
  private static Dictionary<BlockAbility, BlockAbility> _blockerToDefenderDict = new Dictionary<BlockAbility, BlockAbility>();
  [SerializeField]
  private NteractAgent _nteractAgent;
  [SerializeField]
  private int _blockingLibraryId;
  [SerializeField]
  private float _maxInitiationDistance = 2.5f;
  [SerializeField]
  private BlockIntroLoopSequence _blockIntroLoopSequence;
  [SerializeField]
  private DefenderBlockExits _blockExits;
  private int _instanceID;
  private Coroutine _blockingRoutine;
  private bool _exitBlock;
  private bool _exitImmediately;
  private static float _capsuleRadius = 0.2f;
  private static float _offGroundOffset = 0.2f;
  private static float _capsuleHeight = 0.5f;
  private static Vector3 _capsuleSphereCenter1 = new Vector3(0.0f, BlockAbility._capsuleRadius + BlockAbility._offGroundOffset, 0.0f);
  private static Vector3 _capsuleSphereCenter2 = new Vector3(0.0f, BlockAbility._capsuleHeight, 0.0f);
  private bool _defenderWinLoop;
  private static float _defenderWinLoopChance = 0.05f;

  private void Awake()
  {
    this._instanceID = this.GetInstanceID();
    this._blockIntroLoopSequence.Initiate();
  }

  private void OnDisable()
  {
    if (BlockAbility._defenderToBlockerDict.ContainsKey(this))
      BlockAbility._defenderToBlockerDict.Remove(this);
    if (!BlockAbility._blockerToDefenderDict.ContainsKey(this))
      return;
    BlockAbility._blockerToDefenderDict.Remove(this);
  }

  public void ExitBlock()
  {
    BlockAbility blockAbility;
    if (BlockAbility._defenderToBlockerDict.TryGetValue(this, out blockAbility))
      blockAbility._exitBlock = true;
    this._exitBlock = true;
  }

  public void LookForBlockOpportunities(PlayerAI blocker, PlayerAI defender, int typeTagId = -1)
  {
    NteractAgent nteractAgent1 = blocker.nteractAgent;
    NteractAgent nteractAgent2 = defender.nteractAgent;
    if ((double) Vector3.Distance(this.transform.position, nteractAgent2.transform.position) > (double) this._maxInitiationDistance)
      return;
    bool flag = false;
    Vector3 position = blocker.trans.position;
    Vector3 normalized = (defender.trans.position - position).normalized;
    Vector3 point1 = position + BlockAbility._capsuleSphereCenter1;
    UnityEngine.RaycastHit hitInfo;
    if (Physics.CapsuleCast(point1, point1 + BlockAbility._capsuleSphereCenter2, BlockAbility._capsuleRadius, normalized, out hitInfo, this._maxInitiationDistance, LayerMask.GetMask("PlayerCapsule")))
      flag = (UnityEngine.Object) hitInfo.transform == (UnityEngine.Object) defender.trans;
    if (!flag)
      return;
    int instanceId = this._instanceID;
    NteractManager.AddRequest(instanceId, this._blockingLibraryId, nteractAgent2, typeTagId);
    NteractManager.AddRequest(instanceId, this._blockingLibraryId, nteractAgent1, typeTagId);
    this._defenderWinLoop = blocker.blocking < defender.blockBreaking && (double) UnityEngine.Random.value < (double) BlockAbility._defenderWinLoopChance;
  }

  public void StartBlockProgressTracking(BlockAbility blocker, BlockAbility defender)
  {
    if (this._blockingRoutine != null)
    {
      this.StopCoroutine(this._blockingRoutine);
      this._blockingRoutine = (Coroutine) null;
    }
    this._blockingRoutine = this.StartCoroutine(this.TrackBlockProgress(blocker, defender));
  }

  private IEnumerator TrackBlockProgress(BlockAbility blocker, BlockAbility defender)
  {
    blocker._exitBlock = false;
    defender._exitBlock = false;
    BlockAbility._defenderToBlockerDict.Add(defender, blocker);
    BlockAbility._blockerToDefenderDict.Add(blocker, defender);
    yield return (object) new WaitUntil((Func<bool>) (() => blocker._nteractAgent.IsInsideInteraction));
    InteractionController loop = this._blockIntroLoopSequence.GetRandomLoopForIntroParticipant(defender._nteractAgent.Participant, this._defenderWinLoop);
    yield return (object) new WaitUntil((Func<bool>) (() => (double) defender._nteractAgent.PlaybackTimeLeft <= (double) defender._nteractAgent.Blender.BlendTime));
    SlottingResult slotAssignment1 = new SlottingResult();
    slotAssignment1.Add(defender._nteractAgent, loop.participants[0].slot.index);
    slotAssignment1.Add(blocker._nteractAgent, loop.participants[1].slot.index);
    InteractionController.StartPlaybackAndAlignment(loop, 0, slotAssignment1, (InteractionGroup) null);
    yield return (object) new WaitUntil((Func<bool>) (() => this._exitBlock));
    float exitTime = Time.time + (Game.BS_IsInAirPass ? UnityEngine.Random.value : 0.0f);
    yield return (object) new WaitUntil((Func<bool>) (() => (double) Time.time > (double) exitTime));
    InteractionController controller = !Game.IsPlayActive ? this._blockExits.PickPassiveExitController() : this._blockExits.PickActiveExitController(defender.transform, SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform);
    SlottingResult slotAssignment2 = new SlottingResult();
    slotAssignment2.Add(defender._nteractAgent, controller.participants[0].slot.index);
    slotAssignment2.Add(blocker._nteractAgent, controller.participants[1].slot.index);
    InteractionController.StartPlaybackAndAlignment(controller, 0, slotAssignment2, (InteractionGroup) null);
    BlockAbility._defenderToBlockerDict.Remove(defender);
    BlockAbility._blockerToDefenderDict.Remove(blocker);
    this._blockingRoutine = (Coroutine) null;
  }

  public enum BlockTypeInteractionTag
  {
    None = -1, // 0xFFFFFFFF
    Pass = 0,
    Run = 1,
    Chip = 2,
  }
}
