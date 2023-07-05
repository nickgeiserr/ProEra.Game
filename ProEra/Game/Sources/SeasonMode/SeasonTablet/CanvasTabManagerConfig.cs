// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.SeasonMode.SeasonTablet.CanvasTabManagerConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace ProEra.Game.Sources.SeasonMode.SeasonTablet
{
  [CreateAssetMenu(menuName = "Canvas Tab Manager/Configuration")]
  public class CanvasTabManagerConfig : ScriptableObject
  {
    [field: SerializeField]
    public int CachedTabIndex { get; set; }

    [field: SerializeField]
    public CanvasTabManagerConfig.AccountTabContext AccountContext { get; set; }

    private void OnDisable() => this.AccountContext = CanvasTabManagerConfig.AccountTabContext.CreateAccount;

    public enum AccountTabContext
    {
      Login,
      CreateAccount,
    }
  }
}
