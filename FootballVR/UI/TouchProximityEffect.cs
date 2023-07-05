// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchProximityEffect
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace FootballVR.UI
{
  public class TouchProximityEffect : MonoBehaviour
  {
    [SerializeField]
    private Material[] _materials;
    [SerializeField]
    private string[] _shaderPropertyNames;
    private static int[] _shaderPropertyIds;
    private static float _delayedUntil = -1f;

    private void Awake()
    {
      TouchProximityEffect._shaderPropertyIds = new int[this._shaderPropertyNames.Length];
      for (int index = 0; index < this._shaderPropertyNames.Length; ++index)
        TouchProximityEffect._shaderPropertyIds[index] = Shader.PropertyToID(this._shaderPropertyNames[index]);
    }

    private void LateUpdate()
    {
      if ((double) Time.time < (double) TouchProximityEffect._delayedUntil)
        return;
      Vector4 zero = Vector4.zero;
      List<ITouchInput> touchInputs = TouchUI.TouchInputs;
      int num = Mathf.Min(this._shaderPropertyNames.Length, touchInputs.Count);
      for (int index = 0; index < num; ++index)
      {
        Vector3 touchPosition = touchInputs[index].touchPosition;
        zero.Set(touchPosition.x, touchPosition.y, touchPosition.z, 0.0f);
        foreach (Material material in this._materials)
          material.SetVector(TouchProximityEffect._shaderPropertyIds[index], zero);
      }
    }

    private void OnDestroy()
    {
      for (int index = 0; index < this._shaderPropertyNames.Length; ++index)
      {
        foreach (Material material in this._materials)
          material.SetVector(TouchProximityEffect._shaderPropertyIds[index], Vector4.zero);
      }
    }

    public static void Pause(float delay) => TouchProximityEffect._delayedUntil = Time.time + delay;
  }
}
