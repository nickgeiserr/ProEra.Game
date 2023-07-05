// Decompiled with JetBrains decompiler
// Type: FootballVR.WallCollisionMechanic
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using UnityEngine;

namespace FootballVR
{
  public class WallCollisionMechanic
  {
    private readonly Transform _camTx;
    private readonly WallCollisionSettings _settings;
    private readonly RaycastHit[] _raycastResults;

    public Vector3 impactVector { get; private set; }

    public bool knocked { get; private set; }

    public bool applyFall { get; private set; }

    public WallCollisionMechanic(
      Transform camTx,
      WallCollisionSettings settings,
      RaycastHit[] rayHitsBuffer)
    {
      this._camTx = camTx;
      this._settings = settings;
      this._raycastResults = rayHitsBuffer;
    }

    public bool HitsAnyWalls(Vector3 moveVector)
    {
      float magnitude = moveVector.magnitude;
      if (Physics.RaycastNonAlloc(this._camTx.position.SetY(0.1f), moveVector.normalized, this._raycastResults, magnitude * this._settings.movementRaycastDistanceMultiplier + this._settings.MinOffset, (int) WorldConstants.Layers.Environment, QueryTriggerInteraction.Ignore) == 0)
        return false;
      float num = magnitude / Time.deltaTime;
      this.knocked = (double) num > (double) this._settings.KnockSpeedThreshold;
      if (this.knocked)
      {
        this.applyFall = (double) num > (double) this._settings.FallSpeedThreshold;
        this.impactVector = -moveVector.SetY(0.0f) / Time.deltaTime * (this.applyFall ? this._settings.FallPushback : this._settings.Pushback);
      }
      return true;
    }
  }
}
