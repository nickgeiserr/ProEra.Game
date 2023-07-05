// Decompiled with JetBrains decompiler
// Type: FootballVR.VRState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Vars;

namespace FootballVR
{
  [RuntimeState]
  public static class VRState
  {
    public static readonly VariableBool LaserEnabled = new VariableBool();
    public static readonly VariableBool ControllerGraphics = new VariableBool(false);
    public static readonly VariableBool HandsVisible = new VariableBool();
    public static readonly VariableBool HelmetEnabled = new VariableBool();
    public static readonly VariableBool CenterUI = new VariableBool();
    public static readonly VariableBool PauseMenu = new VariableBool();
    public static readonly VariableBool ErrorOccurred = new VariableBool();
    public static bool PausePermission = false;
    public static readonly ManagedBool BigSizeMode = new ManagedBool();
    public static float BigSizeScale = 5f;
    public static bool ForceSyncThrows;
    public static readonly VariableBool LocomotionEnabled = new VariableBool();
    public static bool PlayerOneDropbackActive;
    public static readonly VariableBool CollisionEnabled = new VariableBool();
    public static readonly VariableBool AutoHandoffEnabled = new VariableBool(false);
    public static readonly VariableBool EyeTrackingEnabled = new VariableBool(true);
    public static readonly VariableBool TriggerEffectsEnabled = new VariableBool(true);
    public static readonly VariableBool HeadHapticsEnabled = new VariableBool(true);
    public static readonly VariableBool Muted = new VariableBool(false);
    public static VariableBool InterationWithUI = new VariableBool(true);
    [EditorSetting(ESettingType.Debug)]
    public static bool debug = false;
  }
}
