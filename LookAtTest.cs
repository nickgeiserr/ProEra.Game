// Decompiled with JetBrains decompiler
// Type: LookAtTest
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;
using UDB;
using UnityEngine;

public class LookAtTest : MonoBehaviour
{
  [SerializeField]
  private Transform _camera;
  [SerializeField]
  private Transform _objectToLookAt;

  private void Start()
  {
    if (AppState.Mode.Value != EMode.kMultiplayer)
      return;
    this.enabled = false;
  }

  private void Update()
  {
    if (AppState.Mode.Value == EMode.kMultiplayer)
      return;
    if ((Object) this._objectToLookAt == (Object) null)
    {
      if (!SingletonBehaviour<BallManager, MonoBehaviour>.Exists())
        return;
      BallManager instance = SingletonBehaviour<BallManager, MonoBehaviour>.instance;
      if ((Object) instance == (Object) null)
        return;
      this._objectToLookAt = instance.transform;
    }
    if (!(bool) (Object) this._camera || !(bool) (Object) this._objectToLookAt)
      return;
    this._camera.forward = this._objectToLookAt.position - this._camera.position;
  }
}
