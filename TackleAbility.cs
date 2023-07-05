// Decompiled with JetBrains decompiler
// Type: TackleAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;
using UnityEngine;

public class TackleAbility : MonoBehaviour
{
  public NteractAgent nteractAgent;
  public AnimationEventAgent eventAgent;
  [Tooltip("The maximum distance at which the AI defender will attempt a tackle on another AI ball carrier")]
  public float maxTackleDistance = 3f;
  [Tooltip("The maximum distance at which the AI defender will attempt a tackle on a user")]
  public float maxUserTackleDistance = 1.25f;
  [SerializeField]
  private int _tackleInteractionId;
  [SerializeField]
  private int _brokenTackleInteractionId = 2;
  private int _instanceID;
  private Transform _trans;
  private static float _capsuleRadius = 0.2f;
  private static float _offGroundOffset = 0.2f;
  private static float _capsuleHeight = 0.5f;
  private static Vector3 _capsuleSphereCenter1 = new Vector3(0.0f, TackleAbility._capsuleRadius + TackleAbility._offGroundOffset, 0.0f);
  private static Vector3 _capsuleSphereCenter2 = new Vector3(0.0f, TackleAbility._capsuleHeight, 0.0f);
  private static PlayerAI _lastTackledBallCarrier;
  private static float _lastTackleTime;
  private const float _tackleBrakeBaselineCoefficient = 1f;
  private const float _diceRollFrequency = 0.5f;
  private float _lastTackleDiceRollTime;
  private bool _isTackleSuccessfull;

  private void Awake()
  {
    this._instanceID = this.GetInstanceID();
    this._trans = this.transform;
  }

  public bool LookForTackleOpportunities(PlayerAI playerAI)
  {
    PlayerAI holderPursuitTarget = playerAI.BallHolderPursuitTarget;
    if (AppState.GameMode == EGameMode.kHeroMoment && !(bool) ProEra.Game.MatchState.Turnover || playerAI.isEngagedInBlock || (Object) holderPursuitTarget == (Object) null || (Object) holderPursuitTarget == (Object) playerAI)
      return false;
    NteractAgent nteractAgent = holderPursuitTarget.nteractAgent;
    if (this.nteractAgent.IsInsideInteraction && !this.nteractAgent.CanBeInterupted || nteractAgent.IsInsideInteraction && !nteractAgent.CanBeInterupted || this.eventAgent.IsInsideEvent && !this.eventAgent.CanBeInterupted || holderPursuitTarget.eventAgent.IsInsideEvent && !holderPursuitTarget.eventAgent.CanBeInterupted)
      return false;
    Vector3 position1 = this._trans.position;
    Vector3 position2 = holderPursuitTarget.trans.position;
    Vector3 normalized = (position2 - position1).normalized;
    float num1 = Vector3.Dot(this._trans.forward, normalized);
    double num2 = (double) Vector3.Distance(position1, position2);
    float maxDistance = !holderPursuitTarget.IsQB() || !global::Game.CurrentPlayHasUserQBOnField ? this.maxTackleDistance : this.maxUserTackleDistance;
    double num3 = (double) maxDistance;
    if ((num2 >= num3 ? 0 : ((double) num1 > 0.0 ? 1 : 0)) != 0)
    {
      bool flag1 = false;
      Vector3 point1 = position1 + TackleAbility._capsuleSphereCenter1;
      UnityEngine.RaycastHit hitInfo;
      if (Physics.CapsuleCast(point1, point1 + TackleAbility._capsuleSphereCenter2, TackleAbility._capsuleRadius, normalized, out hitInfo, maxDistance, (int) playerAI.TackleLayerMask))
      {
        PlayerAI playerAi = AssetCache.GetPlayerAI(hitInfo.transform);
        flag1 = (Object) playerAi != (Object) null && (Object) playerAi == (Object) holderPursuitTarget;
      }
      if (flag1)
      {
        bool flag2 = (double) Vector3.Dot(nteractAgent.transform.forward.normalized, Mathf.Approximately(Mathf.Sign(global::Game.IsTurnover ? -1f : 1f) * (float) global::Game.OffensiveFieldDirection, 1f) ? Vector3.forward : Vector3.back) < -0.40000000596046448;
        TackleAbility.TackleInteractionTag tagIndex = !holderPursuitTarget.IsQB() || !Field.IsBehindLineOfScrimmage(holderPursuitTarget.trans.position.z) ? (flag2 ? TackleAbility.TackleInteractionTag.Standing : TackleAbility.TackleInteractionTag.Running) : TackleAbility.TackleInteractionTag.Standing;
        int instanceId = this._instanceID;
        if (tagIndex == TackleAbility.TackleInteractionTag.Running)
        {
          if ((double) Time.time > (double) this._lastTackleDiceRollTime + 0.5)
          {
            float recoveryCoefficient = holderPursuitTarget.BrokenTackleRecoveryCoefficient;
            int tackleBreaking = holderPursuitTarget.tackleBreaking;
            float num4 = MathUtils.MapRange((float) tackleBreaking, 0.0f, (float) (tackleBreaking + playerAI.tackling), 0.0f, 1f) * 1f * recoveryCoefficient;
            Debug.Log((object) ("Break tackle check -- unclamped chance = " + num4.ToString()));
            float num5 = Mathf.Clamp(num4, 0.0f, 0.1f);
            float num6 = Random.value;
            this._isTackleSuccessfull = (double) num6 >= (double) num5;
            Debug.Log((object) ("                   -- chance = " + num5.ToString() + "rand = " + num6.ToString()));
            if (this._isTackleSuccessfull)
              Debug.Log((object) " Tackle NOT Broken");
            else
              Debug.Log((object) " Tackle Broken");
            this._lastTackleDiceRollTime = Time.time;
          }
          if (this._isTackleSuccessfull)
          {
            NteractManager.AddRequest(instanceId, this._tackleInteractionId, this.nteractAgent, (int) tagIndex);
            NteractManager.AddRequest(instanceId, this._tackleInteractionId, nteractAgent, (int) tagIndex);
          }
          else
          {
            NteractManager.AddRequest(instanceId, this._brokenTackleInteractionId, this.nteractAgent, 0);
            NteractManager.AddRequest(instanceId, this._brokenTackleInteractionId, nteractAgent, 0);
          }
        }
        else
        {
          NteractManager.AddRequest(instanceId, this._tackleInteractionId, this.nteractAgent, (int) tagIndex);
          NteractManager.AddRequest(instanceId, this._tackleInteractionId, nteractAgent, (int) tagIndex);
        }
        return true;
      }
    }
    return false;
  }

  public enum TackleInteractionTag
  {
    None = -1, // 0xFFFFFFFF
    Running = 0,
    Standing = 1,
    Sack = 2,
  }
}
