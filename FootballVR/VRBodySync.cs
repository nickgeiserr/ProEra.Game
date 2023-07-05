// Decompiled with JetBrains decompiler
// Type: FootballVR.VRBodySync
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR
{
  public class VRBodySync : MonoBehaviour
  {
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _heightOffset;

    private void Update()
    {
      Vector3 position = this._target.position;
      position.y += this._heightOffset;
      Vector3 eulerAngles = this.transform.rotation.eulerAngles with
      {
        y = this._target.rotation.eulerAngles.y
      };
      this.transform.SetPositionAndRotation(position, Quaternion.Euler(eulerAngles));
    }
  }
}
