// Decompiled with JetBrains decompiler
// Type: FootballVR.MultiplayerBot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  public class MultiplayerBot : MonoBehaviour
  {
    [SerializeField]
    private HandsDataModel _dataModel;
    [SerializeField]
    private ThrowManager _throwManager;
    [EditorSetting(ESettingType.Utility)]
    private static bool autoCatch = true;
    [EditorSetting(ESettingType.Utility)]
    private static bool autoThrow = true;
    private float _initializeTime;
    private readonly RoutineHandle _autoThrowRoutine = new RoutineHandle();

    private void Awake() => this._initializeTime = Time.time + 1f;

    private void OnTriggerEnter(Collider other)
    {
      BallObjectNetworked component = other.GetComponent<BallObjectNetworked>();
      if ((Object) component == (Object) null || component.inHand || !component.inFlight || component.PhotonView.IsMine || !MultiplayerBot.autoCatch || (double) Time.time < (double) this._initializeTime)
        return;
      HandData hand = this._dataModel.GetHand(EHand.Right);
      if (hand == null)
        return;
      hand.CurrentObject = (BallObject) component;
      if (!MultiplayerBot.autoThrow)
        return;
      this._autoThrowRoutine.Run(this.AutoThrowRoutine());
    }

    private IEnumerator AutoThrowRoutine()
    {
      yield return (object) new WaitForSeconds(3.5f);
      HandData hand = this._dataModel.GetHand(EHand.Right);
      if (hand != null && !((Object) hand.CurrentObject == (Object) null))
      {
        List<IThrowTarget> throwTargetList = new List<IThrowTarget>();
        foreach (IThrowTarget throwTarget in (IEnumerable<IThrowTarget>) this._throwManager.ThrowTargets)
        {
          if (throwTarget is RealPlayerTarget)
            throwTargetList.Add(throwTarget);
        }
        int count = throwTargetList.Count;
        if (count != 0)
        {
          int index = count > 0 ? Random.Range(0, count) : 0;
          IThrowTarget throwTarget = throwTargetList[index];
          Vector3 position1 = throwTarget.EvaluatePosition(1.1f);
          BallObject currentObject = hand.CurrentObject;
          Vector3 position2 = currentObject.transform.position;
          Vector3 impulseToHitTarget = AutoAim.GetImpulseToHitTarget(position1 - position2, 1.1f);
          hand.CurrentObject = (BallObject) null;
          int targetId = -1;
          if (throwTarget is RealPlayerTarget realPlayerTarget)
            targetId = realPlayerTarget.playerId;
          currentObject.Throw(currentObject.transform.position, impulseToHitTarget, true, accuracy: 1f, targetId: targetId);
        }
      }
    }
  }
}
