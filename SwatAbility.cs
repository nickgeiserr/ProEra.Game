// Decompiled with JetBrains decompiler
// Type: SwatAbility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;
using UnityEngine;

public class SwatAbility : MonoBehaviour
{
  [SerializeField]
  private AnimationEventAgent _eventAgent;
  [SerializeField]
  private float _maxAcceptableCost = 1f;
  [SerializeField]
  private AnimationEventScenario _swats;
  [SerializeField]
  private float _swatRadius = 3.5f;
  private Transform _trans;
  private const float _swatWindupTime = 0.5f;
  private const float _maxSwatHeight = 2.85f;
  private const float _minSwatHeight = 0.6f;

  public float ScaledMaxSwatHeight => 2.85f * this._trans.lossyScale.x;

  public float ScaledMinSwatHeight => 0.6f * this._trans.lossyScale.x;

  public float ScaledSwatRadius => this._swatRadius * this._trans.lossyScale.x;

  private void Awake() => this._trans = this.transform;

  public void LookForSwatOpportunities(
    PlayersManager playersManager,
    BallManager ballManager,
    PlayerAI playerAI)
  {
    if ((Object) playersManager.ballHolderScript == (Object) playerAI || this._eventAgent.IsInsideEvent || playerAI.nteractAgent.IsInsideInteraction || AppState.GameMode == EGameMode.kHeroMoment || CatchAbility.OffenseGoingForBall)
      return;
    Vector3 position1 = BallPhysicsSimulation.EvaluatePosition(Time.time + 0.5f);
    if ((double) position1.y >= (double) this.ScaledMaxSwatHeight || (double) position1.y <= (double) this.ScaledMinSwatHeight)
      return;
    float scaledSwatRadius = this.ScaledSwatRadius;
    int num = (double) Vector3.Distance(this._trans.position, position1) < (double) scaledSwatRadius ? 1 : 0;
    Vector3 position2 = ballManager.trans.position;
    Vector3 vector3 = position1 - position2;
    vector3.Scale(new Vector3(1f, 0.0f, 1f));
    Vector3 normalized = vector3.normalized;
    Quaternion rotation = Quaternion.Euler(0.0f, Vector3.SignedAngle(Vector3.forward, new Vector3(normalized.x, 0.0f, normalized.z).normalized, Vector3.up), 0.0f);
    AffineTransform affineTransform = new AffineTransform(position1, rotation);
    if (num == 0)
      return;
    float cost;
    AnimationEventController animationEventController = this._swats.SelectController(affineTransform, this._trans, out cost);
    if ((double) cost > (double) this._maxAcceptableCost)
      return;
    this._eventAgent.EnterEvent(affineTransform, animationEventController);
  }
}
