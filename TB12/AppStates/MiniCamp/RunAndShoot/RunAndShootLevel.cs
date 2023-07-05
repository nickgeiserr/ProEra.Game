// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.MiniCamp.RunAndShoot.RunAndShootLevel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12.AppStates.MiniCamp.RunAndShoot
{
  public class RunAndShootLevel : MonoBehaviour
  {
    [SerializeField]
    private RunAndShootLayout[] _levelLayouts;

    public RunAndShootLayout[] LevelLayouts => this._levelLayouts;

    private void Awake()
    {
      foreach (RunAndShootLayout levelLayout in this.LevelLayouts)
        levelLayout.SetLayoutActive(false);
    }
  }
}
