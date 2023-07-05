// Decompiled with JetBrains decompiler
// Type: EndlessRotation
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;

public class EndlessRotation : CachedMonoBehaviour
{
  public float rotationSpeed;
  [SerializeField]
  private Transform trans;
  private float y;

  private void Update()
  {
    this.y = this.trans.eulerAngles.y - this.rotationSpeed * Time.deltaTime;
    this.trans.eulerAngles = new Vector3(0.0f, this.y, 0.0f);
  }
}
