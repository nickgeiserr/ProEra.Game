// Decompiled with JetBrains decompiler
// Type: ShaderTime
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Threading.Tasks;
using UnityEngine;

public class ShaderTime : MonoBehaviour
{
  private int _shaderID_ShaderTime;

  private void Awake()
  {
    this._shaderID_ShaderTime = Shader.PropertyToID("_ShaderTime");
    Object.DontDestroyOnLoad((Object) this);
  }

  private void OnEnable() => this.Loop();

  private async Task Loop()
  {
    ShaderTime shaderTime = this;
    while (shaderTime.enabled)
    {
      Shader.SetGlobalFloat(shaderTime._shaderID_ShaderTime, Time.realtimeSinceStartup);
      await Task.Delay(1);
    }
  }
}
