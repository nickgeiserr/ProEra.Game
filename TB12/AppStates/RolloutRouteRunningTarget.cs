// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.RolloutRouteRunningTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12.AppStates.MiniCamp;
using UnityEngine;

namespace TB12.AppStates
{
  public class RolloutRouteRunningTarget : RouteRunningTarget
  {
    [SerializeField]
    private float _stadiumXBounds = 25f;
    [SerializeField]
    private float _sweetZonePercentOfXBoundsMin = 0.3f;
    [SerializeField]
    private float _sweetZonePercentOfXBoundsMax = 0.5f;
    [SerializeField]
    private ReceiverUI _receiverUI;
    private int _sideValue = 1;

    private void OnEnable() => this.OnHit += new System.Action(((RouteRunningTarget) this).RouteRunningTarget_OnHit);

    private void OnDisable() => this.OnHit -= new System.Action(((RouteRunningTarget) this).RouteRunningTarget_OnHit);

    protected override float GetHitScore() => this.Score * this.GetSweetSpotPercent();

    protected override void Update()
    {
      base.Update();
      this._receiverUI.UpdateEligibility(this.IsActiveForPoints ? this.GetSweetSpotPercent() : 0.0f);
    }

    protected override void RouteRunningTarget_OnHit()
    {
      base.RouteRunningTarget_OnHit();
      this.SuccessfulPass();
    }

    public float GetSweetSpotPercent() => Mathf.Clamp(this.Scale(this._stadiumXBounds * 2f * this._sweetZonePercentOfXBoundsMin * (float) this._sideValue, this._stadiumXBounds * 2f * this._sweetZonePercentOfXBoundsMax * (float) this._sideValue, 0.0f, 1f, this.transform.position.x + this._stadiumXBounds * (float) this._sideValue), 0.0f, 1f);

    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
      float num1 = OldMax - OldMin;
      float num2 = NewMax - NewMin;
      return (OldValue - OldMin) * num2 / num1 + NewMin;
    }

    public void SetRolloutSideValue(int sideValue) => this._sideValue = sideValue;
  }
}
