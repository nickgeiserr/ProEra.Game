// Decompiled with JetBrains decompiler
// Type: FootballVR.TackleMechanic
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using System.Threading.Tasks;
using UnityEngine;

namespace FootballVR
{
  public class TackleMechanic
  {
    private readonly Transform _camTx;
    private readonly TackleSettings _settings;
    private readonly HandsDataModel _handsDataModel;
    private readonly RaycastHit[] _resultsCache;
    private readonly TackleMechanic.TackleResult _result = new TackleMechanic.TackleResult();
    private bool canTackle = true;

    public TackleMechanic(
      Transform camTx,
      TackleSettings settings,
      HandsDataModel handsDataModel,
      RaycastHit[] resultsCache)
    {
      this._camTx = camTx;
      this._settings = settings;
      this._handsDataModel = handsDataModel;
      this._resultsCache = resultsCache;
    }

    public bool HitsAnyPlayer(float normalizedSpeed, out TackleMechanic.TackleResult result)
    {
      result = this._result;
      if (!this.canTackle)
        return false;
      Vector3 position = this._camTx.position;
      Vector3 normalized = this._camTx.forward.SetY(0.0f).normalized;
      int num = Physics.RaycastNonAlloc((position - normalized * 0.5f).SetY(1f), normalized, this._resultsCache, 3f, (int) WorldConstants.Layers.PlayerCapsule);
      Debug.Log((object) string.Format("<b>{0}</b>", (object) num));
      for (int index = 0; index < num; ++index)
      {
        GameObject gameObject = this._resultsCache[index].collider.gameObject;
        Debug.Log((object) ("<b>" + gameObject.name + " is the Tackler!</b>"));
        Vector3 opponentVector = (gameObject.transform.position - position).SetY(0.0f);
        if ((double) Vector3.Dot(opponentVector.normalized, normalized) >= (double) this._settings.opponentMinDirDot)
        {
          Debug.Log((object) ("<b>Tackler " + gameObject.name + " is in front of me!</b>"));
          float magnitude = opponentVector.magnitude;
          if ((double) magnitude >= (double) this._settings.minDistance && ((double) magnitude <= (double) this._settings.maxDistance || (double) magnitude <= (double) this._settings.tackleMaxDistance))
          {
            Debug.Log((object) ("<b>Tackler " + gameObject.name + " is in tackle distance!</b>"));
            Debug.Log((object) ("<b>Tackler " + gameObject.name + " is NOT a networked player!</b>"));
            result.impactPosition = gameObject.transform.position;
            result.impactVector = normalized;
            result.knockDown = (double) normalizedSpeed > (double) this._settings.tackleThreshold && this.ArmsThrustForward(position, normalized, opponentVector);
            if ((double) magnitude <= (double) this._settings.maxDistance || result.knockDown)
            {
              result.highImpact = (double) normalizedSpeed > (double) this._settings.pushThreshold;
              if (result.highImpact || result.knockDown)
                this.ApplyCooldown();
              return true;
            }
          }
        }
      }
      return false;
    }

    public bool HitPlayerResult(
      GameObject gObj,
      float normalizedSpeed,
      out TackleMechanic.TackleResult result)
    {
      result = this._result;
      if (!this.canTackle)
        return false;
      Vector3 position = gObj.transform.position;
      Transform centerEyeAnchor = PersistentSingleton<GamePlayerController>.Instance.Rig.centerEyeAnchor;
      Vector3 opponentVector = (position - centerEyeAnchor.position).SetY(0.0f);
      double magnitude = (double) opponentVector.magnitude;
      result.impactPosition = position;
      result.impactVector = centerEyeAnchor.forward;
      result.knockDown = (double) normalizedSpeed > (double) this._settings.tackleThreshold && this.ArmsThrustForward(centerEyeAnchor.position, result.impactVector, opponentVector);
      result.highImpact = (double) normalizedSpeed > (double) this._settings.pushThreshold;
      if (result.highImpact || result.knockDown)
        this.ApplyCooldown();
      return true;
    }

    private async void ApplyCooldown()
    {
      this.canTackle = false;
      await Task.Delay(750);
      this.canTackle = true;
    }

    private bool ArmsThrustForward(Vector3 pos, Vector3 forwardVec, Vector3 opponentVector)
    {
      Vector3 midPoint;
      if (!this._handsDataModel.TryGetMidPoint(out midPoint))
        return false;
      Vector3 vector3 = midPoint - pos;
      return (double) Vector3.Dot(vector3.SetY(0.0f).normalized, forwardVec) >= (double) this._settings.handDirectionDotThreshold && (double) Vector3.Project(vector3, opponentVector).magnitude > (double) this._settings.minProjectedArmsVector;
    }

    public class TackleResult
    {
      public int opponentId { get; set; }

      public Vector3 impactPosition { get; set; }

      public Vector3 impactVector { get; set; }

      public bool highImpact { get; set; }

      public bool knockDown { get; set; }
    }
  }
}
