// Decompiled with JetBrains decompiler
// Type: Presentation
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class Presentation : MonoBehaviour
{
  private bool left = true;
  private bool rotated;
  private bool move;
  public float speed = 0.35f;
  public int maxX = 16;

  private void Start()
  {
  }

  private void Update()
  {
    if ((double) this.transform.position.x < (double) this.maxX && this.left)
      this.transform.Translate(Vector3.left * (Time.deltaTime * this.speed));
    else if (!this.rotated)
    {
      this.left = false;
      this.transform.Rotate(0.0f, 180f, 0.0f, Space.World);
      this.rotated = true;
    }
    else if ((double) this.transform.position.x > 0.0)
    {
      this.transform.Translate(Vector3.left * (Time.deltaTime * this.speed));
    }
    else
    {
      if (this.move)
        return;
      this.transform.Rotate(0.0f, -90f, 0.0f, Space.World);
      this.move = true;
    }
  }
}
