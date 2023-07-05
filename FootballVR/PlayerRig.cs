// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerRig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class PlayerRig : MonoBehaviour
  {
    [SerializeField]
    private Transform _headTx;
    [SerializeField]
    private Transform _rightControllerTx;
    [SerializeField]
    private Transform _leftControllerTx;
    [SerializeField]
    private Transform _rightHandTx;
    [SerializeField]
    private Transform _leftHandTx;

    public Transform centerEyeAnchor => this._headTx;

    public Transform rightControllerAnchor => this._rightControllerTx;

    public Transform leftControllerAnchor => this._leftControllerTx;

    public Transform rightHandAnchor => this._rightHandTx;

    public Transform leftHandAnchor => this._leftHandTx;
  }
}
