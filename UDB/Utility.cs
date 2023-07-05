// Decompiled with JetBrains decompiler
// Type: UDB.Utility
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Text;
using UnityEngine;

namespace UDB
{
  public class Utility
  {
    public static bool isWinEditor => Application.platform == RuntimePlatform.WindowsEditor;

    public static bool isOSXEditorget => Application.platform == RuntimePlatform.OSXEditor;

    public static string version => Application.version;

    public static string unityVersion => Application.unityVersion;

    public static bool genuine => Application.genuine;

    private static int CameraCompareDepth(Camera c1, Camera c2) => Mathf.RoundToInt(c1.depth - c2.depth);

    public static Camera[] GetAllCameraDepthSorted()
    {
      Camera[] allCameras = Camera.allCameras;
      Array.Sort<Camera>(allCameras, new Comparison<Camera>(Utility.CameraCompareDepth));
      return allCameras;
    }

    public static string BuildString(params string[] strings)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < strings.Length; ++index)
        stringBuilder.Append(strings[index]);
      return stringBuilder.ToString();
    }
  }
}
