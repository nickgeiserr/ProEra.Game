// Decompiled with JetBrains decompiler
// Type: TackleBallObject
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using UnityEngine;

public class TackleBallObject : BallObject
{
  private bool _tackledOnce;

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.layer != LayerMask.NameToLayer("UserAvatar") || this._tackledOnce)
      return;
    this._tackledOnce = true;
    this.Collider.enabled = false;
    VREvents.PlayerCollision.Trigger(this.gameObject);
    VRState.LocomotionEnabled.SetValue(false);
    PersistentSingleton<GamePlayerController>.Instance.HapticsController.SendHapticPulseFromBothNodes(1f, 1f);
  }

  public override void Throw(
    Vector3 startPosition,
    Vector3 throwDirection,
    bool throwActivated,
    bool trailEnabled = true,
    bool hideTrail = true,
    float accuracy = 0.5f,
    int targetId = -1)
  {
    base.Throw(startPosition, throwDirection, throwActivated, trailEnabled, hideTrail, accuracy, targetId);
    this.userThrown = false;
  }
}
