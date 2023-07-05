// Decompiled with JetBrains decompiler
// Type: UDB.CameraExtentions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace UDB
{
  public static class CameraExtentions
  {
    private static int CameraCompareDepth(Camera c1, Camera c2) => Mathf.RoundToInt(c1.depth - c2.depth);

    public static Camera[] GetAllCameraDepthSorted()
    {
      Camera[] allCameras = Camera.allCameras;
      Array.Sort<Camera>(allCameras, new Comparison<Camera>(CameraExtentions.CameraCompareDepth));
      return allCameras;
    }
  }
}
