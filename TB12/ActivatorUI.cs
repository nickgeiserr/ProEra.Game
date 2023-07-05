// Decompiled with JetBrains decompiler
// Type: TB12.ActivatorUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using UnityEngine;

namespace TB12
{
  public class ActivatorUI : MonoBehaviour
  {
    private void Awake() => UIBaseDispatch.RedirectRequests((Enum) EScreens.kIntroduction, (Enum) EScreens.kHikeIntro);

    private void OnDestroy() => UIBaseDispatch.ClearRedirect((Enum) EScreens.kIntroduction);
  }
}
