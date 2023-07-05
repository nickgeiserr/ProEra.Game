// Decompiled with JetBrains decompiler
// Type: TB12.IntelAnimationStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/IntelAnimationStore", fileName = "IntelAnimationStore")]
  [AppStore]
  public class IntelAnimationStore : ScriptableObject
  {
    public VariableBool isPlaying;
    public VariableFloat progress;
    public VariableFloat length;
    public bool isRewinding;

    public void Reset()
    {
      this.isPlaying.Value = false;
      this.progress.Value = 0.0f;
      this.length.Value = 0.0f;
    }
  }
}
