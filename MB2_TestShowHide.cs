// Decompiled with JetBrains decompiler
// Type: MB2_TestShowHide
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB2_TestShowHide : MonoBehaviour
{
  public MB3_MeshBaker mb;
  public GameObject[] objs;

  private void Update()
  {
    if (Time.frameCount == 100)
    {
      this.mb.ShowHide((GameObject[]) null, this.objs);
      this.mb.ApplyShowHide();
      Debug.Log((object) "should have disappeared");
    }
    if (Time.frameCount != 200)
      return;
    this.mb.ShowHide(this.objs, (GameObject[]) null);
    this.mb.ApplyShowHide();
    Debug.Log((object) "should show");
  }
}
