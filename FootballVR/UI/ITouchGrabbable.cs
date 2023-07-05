// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.ITouchGrabbable
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR.UI
{
  public interface ITouchGrabbable
  {
    void OnTouchDragStart();

    void OnTouchDrag(Vector3 delta, ITouchInput touchInput, bool usingLaserGrab = false);

    void OnTouchDragEnd(Vector3 delta, ITouchInput touchInput, bool usingLaserGrab = false);
  }
}
