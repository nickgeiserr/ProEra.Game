// Decompiled with JetBrains decompiler
// Type: TB12.Utils.RenderStateTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

namespace TB12.Utils
{
  public class RenderStateTracker : MonoBehaviour
  {
    public VariableBool IsRendering { get; } = new VariableBool(true);

    private void OnBecameInvisible() => this.IsRendering.SetValue(false);

    private void OnBecameVisible() => this.IsRendering.SetValue(true);
  }
}
