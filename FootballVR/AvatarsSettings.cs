// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarsSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;
using UnityEngine.Events;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "AvatarsSettings", menuName = "TB12/Settings/AvatarsSettings", order = 1)]
  [SettingsConfig]
  public class AvatarsSettings : SingletonScriptableSettings<AvatarsSettings>
  {
    public Save_AvatarsSettings saveAvatarSettings;
    public bool CustomStateBehavior = true;
    public bool PlayModeCollision;
    public bool PlayersCollisionPhysics = true;
    public AvatarPlaybackSettings AvatarPlaybackSettings;

    protected override void OnEnable()
    {
      base.OnEnable();
      this.PlayersCollisionPhysics = true;
    }

    public void PrepareToLoad()
    {
      this.saveAvatarSettings = new Save_AvatarsSettings();
      this.saveAvatarSettings.OnLoaded += new UnityAction(this.SettingsLoaded);
    }

    private void SettingsLoaded()
    {
      this.CustomStateBehavior = this.saveAvatarSettings.CustomStateBehavior;
      this.PlayModeCollision = this.saveAvatarSettings.PlayModeCollision;
      this.PlayersCollisionPhysics = this.saveAvatarSettings.PlayersCollisionPhysics;
      this.AvatarPlaybackSettings = new AvatarPlaybackSettings();
      this.AvatarPlaybackSettings.CatchUpThreshold = this.saveAvatarSettings.CatchUpThreshold;
      this.AvatarPlaybackSettings.SoftCatchUpThreshold = this.saveAvatarSettings.SoftCatchUpThreshold;
      this.AvatarPlaybackSettings.SoftCatchupSpeed = this.saveAvatarSettings.SoftCatchupSpeed;
      this.AvatarPlaybackSettings.CatchupLerpFactor = this.saveAvatarSettings.CatchupLerpFactor;
      this.AvatarPlaybackSettings.CatchUpMaxSpeed = this.saveAvatarSettings.CatchUpMaxSpeed;
      this.saveAvatarSettings.OnLoaded -= new UnityAction(this.SettingsLoaded);
    }
  }
}
