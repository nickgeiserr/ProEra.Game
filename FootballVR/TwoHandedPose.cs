// Decompiled with JetBrains decompiler
// Type: FootballVR.TwoHandedPose
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class TwoHandedPose : MonoBehaviour
  {
    [SerializeField]
    private Transform _leftHand;
    [SerializeField]
    private Transform _rightHand;

    public Transform LeftHand => this._leftHand;

    public Transform RightHand => this._rightHand;
  }
}
