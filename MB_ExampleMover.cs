// Decompiled with JetBrains decompiler
// Type: MB_ExampleMover
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB_ExampleMover : MonoBehaviour
{
  public int axis;

  private void Update()
  {
    Vector3 vector3 = new Vector3(5f, 5f, 5f);
    vector3[this.axis] *= Mathf.Sin(Time.time);
    this.transform.position = vector3;
  }
}
