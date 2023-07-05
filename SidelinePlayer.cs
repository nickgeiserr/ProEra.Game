// Decompiled with JetBrains decompiler
// Type: SidelinePlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;

public class SidelinePlayer : CachedMonoBehaviour
{
  public bool sitting;
  public Animator anim;
  [SerializeField]
  private Transform trans;
  public UniformManager uniManager;

  private void Start()
  {
    if (!this.sitting)
    {
      this.trans.rotation = (double) this.trans.position.x >= 0.0 ? Quaternion.Euler(0.0f, -60f - Random.Range(0.0f, 60f), 0.0f) : Quaternion.Euler(0.0f, 60f + Random.Range(0.0f, 60f), 0.0f);
      this.trans.position = new Vector3(this.trans.position.x + Random.Range(0.0f, 0.4f), 0.0f, this.trans.position.z + Random.Range(0.0f, 0.25f));
      this.anim.speed = 0.2f + Random.Range(0.0f, 1.25f);
    }
    else
      this.anim.SetBool("Sitting", true);
  }
}
