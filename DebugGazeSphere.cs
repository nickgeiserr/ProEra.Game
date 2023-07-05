// Decompiled with JetBrains decompiler
// Type: DebugGazeSphere
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class DebugGazeSphere : MonoBehaviour
{
  public static DebugGazeSphere instance;
  [SerializeField]
  private GameObject debugSphere;
  private bool debugSphereIsActive;

  private void Awake()
  {
    if ((Object) DebugGazeSphere.instance == (Object) null)
      DebugGazeSphere.instance = this;
    else
      Object.Destroy((Object) this);
  }

  public void ToggleDebugSphereIsActive()
  {
    if (!((Object) this.debugSphere != (Object) null))
      return;
    this.debugSphereIsActive = !this.debugSphereIsActive;
    this.debugSphere.SetActive(this.debugSphereIsActive);
  }
}
