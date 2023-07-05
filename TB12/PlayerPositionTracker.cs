// Decompiled with JetBrains decompiler
// Type: TB12.PlayerPositionTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using UnityEngine;
using Vars;

namespace TB12
{
  public class PlayerPositionTracker : MonoBehaviour
  {
    [SerializeField]
    private Transform _rootTx;
    private Transform _tx;
    private Vector3 _previousPosition;
    private readonly Vector3Cache _deltaCache = new Vector3Cache(10);

    public static VariableBool TrackHeadPosition { get; } = new VariableBool();

    public static Vector3 AverageMovement { get; private set; } = Vector3.zero;

    private void Awake()
    {
      this._tx = this.transform;
      this._deltaCache.FillValues(Vector3.zero);
      PlayerPositionTracker.TrackHeadPosition.OnValueChanged += new Action<bool>(this.HandlePositionTracking);
      this.HandlePositionTracking((bool) PlayerPositionTracker.TrackHeadPosition);
    }

    private void HandlePositionTracking(bool trackEnabled)
    {
      if (!trackEnabled)
        return;
      this._deltaCache.Clear();
      this._previousPosition = this._tx.localPosition;
      PlayerPositionTracker.AverageMovement = Vector3.zero;
    }

    private void Update()
    {
      if (!PlayerPositionTracker.TrackHeadPosition.Value)
        return;
      Vector3 localPosition = this._tx.localPosition;
      this._deltaCache.PushValue(localPosition - this._previousPosition);
      this._previousPosition = localPosition;
      PlayerPositionTracker.AverageMovement = this._rootTx.TransformDirection(this._deltaCache.AverageValue());
    }

    private void OnDrawGizmos()
    {
      if (!((UnityEngine.Object) this._tx != (UnityEngine.Object) null))
        return;
      Gizmos.DrawLine(this._tx.position, this._tx.position + PlayerPositionTracker.AverageMovement);
    }
  }
}
