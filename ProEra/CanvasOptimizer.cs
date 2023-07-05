// Decompiled with JetBrains decompiler
// Type: ProEra.CanvasOptimizer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProEra
{
  public class CanvasOptimizer
  {
    public static void OptimizeCanvas(GameObject go)
    {
      CanvasOptimizer.Optimize<Image>(go);
      CanvasOptimizer.Optimize<TextMeshProUGUI>(go);
    }

    private static void Optimize<T>(GameObject go)
    {
      T[] componentsInChildren = go.GetComponentsInChildren<T>();
      GameObject gameObject = new GameObject(typeof (T)?.ToString() + "_parent", new System.Type[1]
      {
        typeof (RectTransform)
      });
      gameObject.transform.SetParent(go.transform);
      for (int index = 0; index < componentsInChildren.Length; ++index)
        ((object) componentsInChildren[index] as MonoBehaviour).GetComponent<RectTransform>().SetParent((Transform) gameObject.GetComponent<RectTransform>());
    }
  }
}
