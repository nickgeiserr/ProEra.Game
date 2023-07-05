// Decompiled with JetBrains decompiler
// Type: TB12.UISettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using Vars;

namespace TB12
{
  [CreateAssetMenu(fileName = "UISettings", menuName = "TB12/Settings/UISettings", order = 1)]
  [SettingsConfig]
  public class UISettings : SingletonScriptableSettings<UISettings>
  {
    public VariableFloat LeaderboardDistance = new VariableFloat(10f);
    public const float canvasScale = 0.003f;
  }
}
