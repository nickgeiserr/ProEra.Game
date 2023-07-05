// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.CircularLinkExtensions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.UI;
using System;
using UnityEngine;

namespace FootballVR.UI
{
  public static class CircularLinkExtensions
  {
    public static UIHandle Link(this CircularLayout layout, Action<int> action)
    {
      if ((UnityEngine.Object) layout != (UnityEngine.Object) null)
        return (UIHandle) new CircularLayoutActionLink(layout, action);
      Debug.LogError((object) "Null button, link initialization failed.");
      return (UIHandle) null;
    }
  }
}
