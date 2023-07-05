// Decompiled with JetBrains decompiler
// Type: FootballVR.OrientToUser
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR
{
  public class OrientToUser : MonoBehaviour
  {
    [SerializeField]
    private Transform _orientToPoint;
    [SerializeField]
    private bool _zeroHeight = true;

    public void Apply()
    {
      Transform transform = this._orientToPoint;
      if ((Object) transform == (Object) null)
        transform = Camera.main.transform;
      Vector3 position = transform.position;
      if (this._zeroHeight)
        position.SetY(0.0f);
      this.transform.LookAt(position, Vector3.up);
    }
  }
}
