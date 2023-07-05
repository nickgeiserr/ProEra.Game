// Decompiled with JetBrains decompiler
// Type: UDB.DollyZoom
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  public class DollyZoom : MonoBehaviour
  {
    public Transform Target;
    public Camera Camera;
    private float initHeightAtDist;
    private bool dzEnabled;

    private void Start()
    {
    }

    private void Update()
    {
      if (!this.dzEnabled)
        return;
      this.Camera.fieldOfView = this.FOVForHeightAndDistance(this.initHeightAtDist, Vector3.Distance(this.transform.position, this.Target.position));
    }

    private float FrustumHeightAtDistance(float distance) => 2f * distance * Mathf.Tan((float) ((double) this.Camera.fieldOfView * 0.5 * (System.Math.PI / 180.0)));

    private float FOVForHeightAndDistance(float height, float distance) => (float) (2.0 * (double) Mathf.Atan(height * 0.5f / distance) * 57.295780181884766);

    public void StartDZ()
    {
      this.initHeightAtDist = this.FrustumHeightAtDistance(Vector3.Distance(this.transform.position, this.Target.position));
      this.dzEnabled = true;
    }

    private void StopDZ() => this.dzEnabled = false;
  }
}
