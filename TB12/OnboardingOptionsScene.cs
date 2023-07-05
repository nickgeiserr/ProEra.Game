// Decompiled with JetBrains decompiler
// Type: TB12.OnboardingOptionsScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class OnboardingOptionsScene : MonoBehaviour
  {
    [SerializeField]
    private List<GameObject> psvrOnlyUIArrows;

    private void Awake()
    {
      foreach (Object psvrOnlyUiArrow in this.psvrOnlyUIArrows)
        Object.Destroy(psvrOnlyUiArrow);
    }
  }
}
