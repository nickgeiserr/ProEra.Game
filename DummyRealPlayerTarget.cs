// Decompiled with JetBrains decompiler
// Type: DummyRealPlayerTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using UnityEngine;

public class DummyRealPlayerTarget : MonoBehaviour
{
  [SerializeField]
  private ThrowManager _throwManager;
  private RealPlayerTarget _target;
  private Vector3 _velocity = new Vector3(6f, 0.0f, 0.0f);

  private void Start()
  {
    this._target = new RealPlayerTarget(this.transform, -100, this.GetComponent<MotionTracker>());
    this._throwManager.RegisterTarget((IThrowTarget) this._target);
  }

  private void Update()
  {
  }
}
