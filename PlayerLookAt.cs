// Decompiled with JetBrains decompiler
// Type: PlayerLookAt
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
  [SerializeField]
  private bool _normalizeY;

  private void Update()
  {
    Vector3 position = PersistentSingleton<GamePlayerController>.Instance.position;
    if (this._normalizeY)
      position.y = this.transform.position.y;
    this.transform.rotation = Quaternion.LookRotation(this.transform.position - position);
  }
}
