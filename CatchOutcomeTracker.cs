// Decompiled with JetBrains decompiler
// Type: CatchOutcomeTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchOutcomeTracker
{
  private CatchOutcomeTracker.ECatchOutcome _catchOutcome;
  private RigHumanoidMapping _rigHumanoidMapping;
  private Coroutine _catchOutcomeRoutine;
  private const float _extraThresholdHeight = 0.05f;

  public CatchOutcomeTracker.ECatchOutcome CatchOutcome => this._catchOutcome;

  public void ResetCatchOutcome()
  {
    this.StopCatchOutcomeTracking();
    this._catchOutcome = CatchOutcomeTracker.ECatchOutcome.NotDetermined;
  }

  public void StartCatchOutcomeTracking(
    RigHumanoidMapping rigMapping,
    List<InboundsCheckMeta.BoneData> inboundsCheckBoneMeta,
    Rect fieldRect)
  {
    this._rigHumanoidMapping = rigMapping;
    this.StopCatchOutcomeTracking();
    this._catchOutcomeRoutine = this._rigHumanoidMapping.StartCoroutine(this.CatchOutcomeRoutine(inboundsCheckBoneMeta, fieldRect));
  }

  private void StopCatchOutcomeTracking()
  {
    if (this._catchOutcomeRoutine == null)
      return;
    this._rigHumanoidMapping.StopCoroutine(this._catchOutcomeRoutine);
  }

  private IEnumerator CatchOutcomeRoutine(
    List<InboundsCheckMeta.BoneData> inboundsCheckBoneMeta,
    Rect fieldRect)
  {
    this._catchOutcome = CatchOutcomeTracker.ECatchOutcome.NotDetermined;
    List<CatchOutcomeTracker.InboundsCheckJoint> _inboundsCheckJoints = new List<CatchOutcomeTracker.InboundsCheckJoint>();
    for (int index = 0; index < inboundsCheckBoneMeta.Count; ++index)
      _inboundsCheckJoints.Add(new CatchOutcomeTracker.InboundsCheckJoint()
      {
        joint = this._rigHumanoidMapping.GetHumanBone(inboundsCheckBoneMeta[index].bone),
        groundDistThreshold = (float) ((double) inboundsCheckBoneMeta[index].absoluteLocation.y * (double) this._rigHumanoidMapping.transform.lossyScale.x + 0.05000000074505806)
      });
    while (this._catchOutcome == CatchOutcomeTracker.ECatchOutcome.NotDetermined)
    {
      for (int index = 0; index < _inboundsCheckJoints.Count; ++index)
      {
        if (!_inboundsCheckJoints[index].landed)
        {
          Vector3 position = _inboundsCheckJoints[index].joint.position;
          _inboundsCheckJoints[index].landed = (double) position.y <= (double) _inboundsCheckJoints[index].groundDistThreshold;
          if (_inboundsCheckJoints[index].landed)
          {
            Vector2 point = new Vector2(position.x, position.z);
            _inboundsCheckJoints[index].inBounds = fieldRect.Contains(point);
            if (!_inboundsCheckJoints[index].inBounds)
            {
              this._catchOutcome = CatchOutcomeTracker.ECatchOutcome.CaughtOutOfBounds;
              this._catchOutcomeRoutine = (Coroutine) null;
              break;
            }
          }
        }
      }
      if (this._catchOutcome == CatchOutcomeTracker.ECatchOutcome.NotDetermined)
      {
        bool flag = true;
        for (int index = 0; index < _inboundsCheckJoints.Count; ++index)
        {
          if (!_inboundsCheckJoints[index].landed || !_inboundsCheckJoints[index].inBounds)
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          this._catchOutcome = CatchOutcomeTracker.ECatchOutcome.CaughtInBounds;
          this._catchOutcomeRoutine = (Coroutine) null;
        }
      }
      yield return (object) null;
    }
    yield return (object) null;
  }

  private class InboundsCheckJoint
  {
    public Transform joint;
    public float groundDistThreshold;
    [ReadOnly]
    public bool landed;
    [ReadOnly]
    public bool inBounds;
  }

  public enum ECatchOutcome
  {
    NotDetermined,
    CaughtOutOfBounds,
    CaughtInBounds,
  }
}
