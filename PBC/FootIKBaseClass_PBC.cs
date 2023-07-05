// Decompiled with JetBrains decompiler
// Type: PBC.FootIKBaseClass_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class FootIKBaseClass_PBC : MonoBehaviour
  {
    public Transform iAmStandingOn;
    public Transform stickToTransform;
    public Vector3 raycastDown_Hat = Physics.gravity.normalized;
    public Vector3 nRaw_Hat;
    public Vector3 transformTarget;
    public bool grounded;
    public float elevation;

    public virtual void DoFootIK()
    {
    }
  }
}
