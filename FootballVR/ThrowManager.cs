// Decompiled with JetBrains decompiler
// Type: FootballVR.ThrowManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Managers/ThrowManager", fileName = "ThrowManager")]
  public class ThrowManager : ScriptableObject
  {
    [SerializeField]
    private HandsDataModel _handsDataModel;
    private readonly List<IThrowTarget> _throwTargets = new List<IThrowTarget>();

    private ThrowSettings _throwSettings => ScriptableSingleton<ThrowSettings>.Instance;

    public ThrowSettings Settings => this._throwSettings;

    public HandsDataModel HandsDataModel => this._handsDataModel;

    public event Action<BallObject, Vector3> OnBallThrown;

    public event Action<ThrowData> OnThrowProcessed;

    public float AutoAimStrength { get; set; } = 0.7f;

    public float AutoAimRange { get; set; } = 3.65f;

    public IReadOnlyList<IThrowTarget> ThrowTargets => (IReadOnlyList<IThrowTarget>) this._throwTargets;

    public bool ForceAutoAim { get; set; }

    private void OnEnable() => this.Clear();

    public void Clear(bool clearTargets = true)
    {
      if (clearTargets)
        this._throwTargets.Clear();
      this._handsDataModel.ResetHandsState();
    }

    public void RegisterTarget(IThrowTarget target)
    {
      if (this._throwTargets.Contains(target))
        return;
      this._throwTargets.Add(target);
    }

    public void UnregisterTarget(IThrowTarget target) => this._throwTargets.Remove(target);

    private void ApplyThrowAutoAim(ThrowData aimThrowData, float autoAimStrength)
    {
      aimThrowData.flightTime = AutoAim.GetFlightTime(aimThrowData.startPosition, aimThrowData.throwVector, 0.8f);
      if (!aimThrowData.ValidFlightime())
        return;
      if (ScriptableSingleton<ThrowSettings>.Instance.AutoAimSettings.UseMinimalPassAssistSettings)
        AutoAim.GetClosestTargetWithoutAutoAim(aimThrowData, (IList<IThrowTarget>) this._throwTargets);
      else
        AutoAim.GetClosestTarget(aimThrowData, (IList<IThrowTarget>) this._throwTargets, this._throwSettings.AutoAimSettings);
      if (!aimThrowData.hasTarget)
        return;
      aimThrowData.throwVector = Vector3.Lerp(aimThrowData.throwVector, aimThrowData.autoAimedVector, autoAimStrength);
    }

    public ThrowData ProcessThrow(BallObject ball, Vector3 rawVector)
    {
      Vector3 velocity = this._throwSettings.ThrowConfig.ApplyConfig(rawVector);
      ThrowData aimThrowData = new ThrowData()
      {
        ball = ball,
        startPosition = ball.transform.position,
        throwVector = velocity,
        timeStep = 0.03f,
        maxDistance = this.AutoAimRange
      };
      if ((UnityEngine.Object) ball == (UnityEngine.Object) null || this._throwTargets.Count == 0)
        return aimThrowData;
      Action<BallObject, Vector3> onBallThrown = this.OnBallThrown;
      if (onBallThrown != null)
        onBallThrown(ball, velocity);
      if (this._throwSettings.AutoAimSettings.AutoAimEnabled || this.ForceAutoAim)
        this.ApplyThrowAutoAim(aimThrowData, this.ForceAutoAim ? 1f : this.AutoAimStrength);
      if (this._throwSettings.DebugMode)
      {
        float flightTime = AutoAim.GetFlightTime(aimThrowData.startPosition, velocity, 1.7f);
        Debug.Log((object) (string.Format("Initial velocity {0}, multiplied {1}, with AutoAim {2}\r\n", (object) rawVector.magnitude, (object) velocity.magnitude, (object) aimThrowData.throwVector.magnitude) + string.Format("FlightTime: Initial {0} AutoAim {1} TimeToGetToTarget {2}", (object) flightTime, (object) aimThrowData.flightTime, (object) aimThrowData.timeToGetToTarget)));
      }
      if (!aimThrowData.hasTarget)
      {
        float time = Mathf.Clamp(AutoAim.GetFlightTime(aimThrowData.startPosition, aimThrowData.throwVector, 0.8f), 0.1f, 10f);
        Vector3[] trajectory = AutoAim.GetTrajectory(aimThrowData.startPosition, aimThrowData.throwVector, aimThrowData.timeStep, time);
        aimThrowData.targetPosition = trajectory[trajectory.Length - 1];
      }
      Action<ThrowData> onThrowProcessed = this.OnThrowProcessed;
      if (onThrowProcessed != null)
        onThrowProcessed(aimThrowData);
      return aimThrowData;
    }

    public void SetCanThrowBall(bool canThrow)
    {
      BallThrowMechanic ballThrowMechanic1 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Left).BallThrowMechanic;
      BallThrowMechanic ballThrowMechanic2 = ScriptableSingleton<HandsDataModel>.Instance.GetHand(EHand.Right).BallThrowMechanic;
      ballThrowMechanic1.IsThrowAllowed = canThrow;
      int num = canThrow ? 1 : 0;
      ballThrowMechanic2.IsThrowAllowed = num != 0;
    }
  }
}
