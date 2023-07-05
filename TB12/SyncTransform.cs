// Decompiled with JetBrains decompiler
// Type: TB12.SyncTransform
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace TB12
{
  public class SyncTransform : MonoBehaviour
  {
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private bool _trackPlayer;
    [SerializeField]
    private bool _syncPosition;
    [SerializeField]
    private bool _overrideHeight;
    [SerializeField]
    private float _customHeight;
    [SerializeField]
    private bool _syncRotation;
    [SerializeField]
    private bool _syncOnlyY;

    private void Awake()
    {
      if (this._trackPlayer)
        this._target = PersistentSingleton<PlayerCamera>.Instance.transform;
      if (!((Object) this._target == (Object) null))
        return;
      this.enabled = false;
    }

    private void Update()
    {
      if (this._syncPosition)
      {
        Vector3 position = this._target.position;
        if (this._overrideHeight)
          position.y = this._customHeight;
        this.transform.position = position;
      }
      if (!this._syncRotation)
        return;
      if (!this._syncOnlyY)
        this.transform.rotation = this._target.rotation;
      else
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles with
        {
          y = this._target.rotation.eulerAngles.y
        });
    }
  }
}
