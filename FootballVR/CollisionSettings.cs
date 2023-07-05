// Decompiled with JetBrains decompiler
// Type: FootballVR.CollisionSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using Vars;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "CollisionSettings", menuName = "TB12/Settings/AgilitySettings", order = 1)]
  [SettingsConfig]
  public class CollisionSettings : SingletonScriptableSettings<CollisionSettings>
  {
    public VariableBool CollisionEnabled = new VariableBool(true);
    public bool WallCollisionEnabled = true;
    public Vector3 ScorePosition = new Vector3(0.0f, -0.2f, 0.8f);
    public TackleSettings tackleSettings;
    public CollidersSettings collidersSettings;
    public AttackerSettings attackerSettings;
    public CollisionEffectSettings collisionEffect;
    public FallEffect fallEffect;
    public WallCollisionSettings wallCollisionSettings;
    public const float firstCollisionMinTime = 2f;
    public const float minAngleDifference = 10f;
    public float GetUpFadeDuration = 0.3f;
    public float GetUpButtonUp = 0.3f;
    public float GetUpButtonForw = 0.5f;
  }
}
