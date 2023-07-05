// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Water.Displace
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UnityStandardAssets.Water
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (WaterBase))]
  public class Displace : MonoBehaviour
  {
    public void Awake()
    {
      if (this.enabled)
        this.OnEnable();
      else
        this.OnDisable();
    }

    public void OnEnable()
    {
      Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
      Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
    }

    public void OnDisable()
    {
      Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
      Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
    }
  }
}
