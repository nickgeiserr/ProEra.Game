// Decompiled with JetBrains decompiler
// Type: Save_VRSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Events;

[MessagePackObject(false)]
[Serializable]
public class Save_VRSettings : ISaveSync
{
  [IgnoreMember]
  public static string FileName = nameof (Save_VRSettings);
  [IgnoreMember]
  private bool isDirty;
  [IgnoreMember]
  public UnityAction OnLoaded;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public bool LoadedGameStartupScene;
  [Key(2)]
  public bool BypassStartup;
  [Key(3)]
  public bool ControllerAnnotations = true;
  [Key(4)]
  public float PlayerBodyScale = 0.93f;
  [Key(5)]
  public float UIDistance = 0.8f;
  [Key(6)]
  public bool UpdateUIHeight = true;
  [Key(7)]
  public bool UseLeftHand;
  [Key(8)]
  public bool UseVrLaser = true;
  [Key(9)]
  public bool GripButtonThrow;
  [Key(10)]
  public bool SeatedMode;
  [Key(11)]
  public bool HelmetActive = true;
  [Key(12)]
  public bool OneHandedMode;
  [Key(13)]
  public int HuddlePlayClock = 8;
  [Key(14)]
  public int NoHuddlePlayClockOffset = 8;
  [Key(15)]
  public int QuarterLength = 5;
  [Key(16)]
  public float PositionalAudio = 0.6f;
  [Key(17)]
  public bool AlphaThrowing;
  [Key(18)]
  public float AudioSpeakThreshold = 0.01f;
  [Key(19)]
  public bool AllowDynamicTargets = true;
  [Key(20)]
  public float VCVolume = 1f;
  [Key(21)]
  public float MicVolume = 1f;
  [Key(22)]
  public bool ImmersiveTackleEnabled;
  [Key(23)]
  public bool AutoDropbackEnabled = true;
  [Key(24)]
  public bool VignetteEnabled = true;
  [Key(25)]
  public float VignetteFalloffDegrees = 20f;
  [Key(26)]
  public float VignetteAspectRatio = 1f;
  [Key(27)]
  public float VignetteLerpFactor = 0.2f;
  [Key(28)]
  public float VignetteMinFov = 55f;
  [Key(29)]
  public float VignetteMaxFov = 105f;
  [Key(30)]
  public float gravity = 5f;
  [Key(31)]
  public float timeFactor = 4f;
  [Key(32)]
  public bool useSpline;
  [Key(33)]
  public float splineCoef = 1.2f;
  [Key(34)]
  public float splineHeightCoef = 0.3f;
  [Key(35)]
  public float laserPower = 20f;

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value) => this.isDirty = value;

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_VRSettings> deserializedData = SaveIO.LoadAsync<Save_VRSettings>(Path.Combine(SaveIO.DefaultFolderPath, Save_VRSettings.FileName));
    Save_VRSettings saveVrSettings = await deserializedData;
    if (deserializedData.Result != null)
      this.CopyData(deserializedData.Result);
    UnityAction onLoaded = this.OnLoaded;
    if (onLoaded == null)
    {
      deserializedData = (Task<Save_VRSettings>) null;
    }
    else
    {
      onLoaded();
      deserializedData = (Task<Save_VRSettings>) null;
    }
  }

  private void CopyData(Save_VRSettings loadedData)
  {
    this.metaData = loadedData.metaData;
    this.LoadedGameStartupScene = loadedData.LoadedGameStartupScene;
    this.BypassStartup = loadedData.BypassStartup;
    this.ControllerAnnotations = loadedData.ControllerAnnotations;
    this.PlayerBodyScale = loadedData.PlayerBodyScale;
    this.UIDistance = loadedData.UIDistance;
    this.UpdateUIHeight = loadedData.UpdateUIHeight;
    this.UseLeftHand = loadedData.UseLeftHand;
    this.UseVrLaser = loadedData.UseVrLaser;
    this.GripButtonThrow = loadedData.GripButtonThrow;
    this.SeatedMode = loadedData.SeatedMode;
    this.HelmetActive = loadedData.HelmetActive;
    this.OneHandedMode = loadedData.OneHandedMode;
    this.HuddlePlayClock = loadedData.HuddlePlayClock;
    this.NoHuddlePlayClockOffset = loadedData.NoHuddlePlayClockOffset;
    this.QuarterLength = loadedData.QuarterLength;
    this.PositionalAudio = loadedData.PositionalAudio;
    this.AlphaThrowing = loadedData.AlphaThrowing;
    this.AudioSpeakThreshold = loadedData.AudioSpeakThreshold;
    this.AllowDynamicTargets = loadedData.AllowDynamicTargets;
    this.VCVolume = loadedData.VCVolume;
    this.MicVolume = loadedData.MicVolume;
    this.ImmersiveTackleEnabled = loadedData.ImmersiveTackleEnabled;
    this.AutoDropbackEnabled = loadedData.AutoDropbackEnabled;
    this.VignetteEnabled = loadedData.VignetteEnabled;
    this.VignetteFalloffDegrees = loadedData.VignetteFalloffDegrees;
    this.VignetteAspectRatio = loadedData.VignetteAspectRatio;
    this.VignetteLerpFactor = loadedData.VignetteLerpFactor;
    this.VignetteMinFov = loadedData.VignetteMinFov;
    this.VignetteMaxFov = loadedData.VignetteMaxFov;
    this.gravity = loadedData.gravity;
    this.timeFactor = loadedData.timeFactor;
    this.useSpline = loadedData.useSpline;
    this.splineCoef = loadedData.splineCoef;
    this.splineHeightCoef = loadedData.splineHeightCoef;
    this.laserPower = loadedData.laserPower;
  }

  public async Task Save()
  {
    Save_VRSettings objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_VRSettings>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_VRSettings.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
