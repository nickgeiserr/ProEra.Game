// Decompiled with JetBrains decompiler
// Type: FootballVR.ArmSwingController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR
{
  public class ArmSwingController
  {
    private Vector3 prevPos;
    public float charge;
    private readonly Transform tx;
    private readonly ArmSwingSettings _settings;
    private Vector3 swingDirection = Vector3.zero;

    public float recharge { get; private set; }

    public float acceleration { get; private set; }

    public ArmSwingController(Transform handTx, ArmSwingSettings settings)
    {
      this.tx = handTx;
      this.charge = settings.maxCharge / 2f;
      this.prevPos = this.tx.localPosition;
      this._settings = settings;
    }

    public void Prepare()
    {
      this.prevPos = this.tx.localPosition;
      this.swingDirection = Vector3.zero;
    }

    public void Update(Vector3 horizForw, bool moveForward)
    {
      if (this.prevPos == Vector3.zero)
      {
        this.recharge = 0.0f;
        this.acceleration = 0.0f;
        this.prevPos = this.tx.localPosition;
        this.swingDirection = Vector3.zero;
      }
      else
      {
        this.swingDirection = Vector3.Lerp(this.swingDirection, this.tx.localPosition - this.prevPos, this._settings.swingLerpCoef);
        float x1 = -Vector3.Dot(this.swingDirection, horizForw);
        float x2 = -Vector3.Dot(this.swingDirection, Vector3.up);
        float b;
        float num;
        if (moveForward)
        {
          b = (float) ((double) this._settings.verticalCoef * (double) x2.TakePositive() + (double) this._settings.forwardCoef * (double) x1.TakePositive());
          num = (float) ((double) this._settings.verticalCoef * (double) x2.TakeNegative() + (double) this._settings.forwardCoef * (double) x1.TakeNegative());
        }
        else
        {
          b = (float) ((double) this._settings.backwardsVerticalCoef * (double) x2.TakeNegative() * 0.5 + (double) this._settings.forwardCoef * (double) x1.TakeNegative());
          num = (float) ((double) this._settings.backwardsVerticalCoef * (double) x2.TakePositive() + (double) this._settings.forwardCoef * (double) x1.TakePositive());
        }
        this.acceleration = Mathf.Clamp(Mathf.Min(this.charge, b) * this._settings.accelProfile.acceleration, -this._settings.maxAcceleration, this._settings.maxAcceleration);
        this.recharge = (float) ((double) num * (double) this._settings.accelProfile.acceleration * -(double) this._settings.rechargeCoefficient);
        this.prevPos = this.tx.localPosition;
      }
    }
  }
}
