// Decompiled with JetBrains decompiler
// Type: Save_GameSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[MessagePackObject(false)]
[Serializable]
public class Save_GameSettings : ISaveSync
{
  [IgnoreMember]
  public static string FileName = "SaveGameSettings";
  [IgnoreMember]
  private bool isDirty;
  [Key(0)]
  public Dictionary<string, string> metaData = new Dictionary<string, string>();
  [Key(1)]
  public float GameSFXVolume = 0.7f;
  [Key(2)]
  public float AnnouncerVolume = 0.7f;
  [Key(3)]
  public float GameOptionsMusicVolume = 0.3f;
  [Key(4)]
  public float MenuMusicVolume = 0.7f;
  [Key(5)]
  public bool SoundOn = true;
  [Key(6)]
  public bool MusicOn = true;
  [Key(7)]
  public bool AnnouncerOn = true;
  [Key(8)]
  public int GraphicsQuality = 2;
  [Key(9)]
  public int ResolutionWidth = 1920;
  [Key(10)]
  public int ResolutionHeight = 1080;
  [Key(11)]
  public bool ignoreControllers;
  [Key(12)]
  public bool runInBackground = true;
  [Key(13)]
  public float SensitivitySliderP1 = 0.5f;
  [Key(14)]
  public float SensitivitySliderP2 = 0.5f;
  [Key(15)]
  public float LeadReceiverValueP1 = 0.05f;
  [Key(16)]
  public float LeadReceiverValueP2 = 0.05f;
  [Key(17)]
  public float GlobalTimeScale = 0.75f;
  [Key(18)]
  public bool ButtonPassingP1 = true;
  [Key(19)]
  public bool ButtonPassingP2 = true;
  [Key(20)]
  public bool StickPushP1;
  [Key(21)]
  public bool StickPushP2;
  [Key(22)]
  public int AcceleratedClockValue = 17;
  [Key(23)]
  public int ButtonStyle = 1;
  [Key(24)]
  public int ClockSpeed = 8;
  [Key(25)]
  public int ControllerIconStyle = 1;
  [Key(26)]
  public int TimeBetweenPlays = 6;
  [Key(27)]
  public float CameraHeightZoom = 0.4f;
  [Key(28)]
  public float CameraDepthZoom = 0.4f;
  [Key(29)]
  public bool ShowSidelinePlayers = true;
  [Key(30)]
  public string GameLanguage = "en";
  [Key(31)]
  public int DefDifficulty = 1;
  [Key(32)]
  public int OffDifficulty = 1;
  [Key(33)]
  public int QuarterLength = 1;
  [Key(34)]
  public bool UseFatigue = true;
  [Key(35)]
  public bool UseInjuries = true;
  [Key(36)]
  public bool TwoMinuteWarningEnabled = true;
  [Key(37)]
  public bool TutorialEnabled = true;
  [Key(38)]
  public int InjuryFrequency = 2;
  [Key(39)]
  public float PenaltyFrequency = 10f;
  [Key(40)]
  public bool DelayOfGame = true;
  [Key(41)]
  public bool FalseStart = true;
  [Key(42)]
  public bool OffensiveHolding = true;
  [Key(43)]
  public bool OffensivePassInterference = true;
  [Key(44)]
  public bool DefensivePassInterference = true;
  [Key(45)]
  public bool Facemask = true;
  [Key(46)]
  public bool IllegalForwardPass = true;
  [Key(47)]
  public bool Offsides = true;
  [Key(48)]
  public bool Encroachment = true;
  [Key(49)]
  public bool KickoffOutOfBounds = true;
  [Key(50)]
  public bool IntentionalGrounding = true;
  [Key(51)]
  public bool _useBaseAssets = true;
  [Key(52)]
  public bool _useModAssets;
  [Key(53)]
  public bool _logGamesToFile;
  [Key(54)]
  public bool _exportGameStatsToFile;
  [Key(55)]
  public bool _isFullscreeMode = true;
  [Key(56)]
  public int _lockerInteractionHistory;
  [Key(57)]
  public bool _didCompleteTutorial;

  [IgnoreMember]
  public bool UseBaseAssets
  {
    get => this._useBaseAssets;
    set => this._useBaseAssets = value;
  }

  [IgnoreMember]
  public bool UseModAssets
  {
    get => this._useModAssets;
    set => this._useModAssets = value;
  }

  [IgnoreMember]
  public bool LogGamesToFile
  {
    get => this._logGamesToFile;
    set => this._logGamesToFile = value;
  }

  [IgnoreMember]
  public bool ExportGameStatsToFile
  {
    get => this._exportGameStatsToFile;
    set => this._exportGameStatsToFile = value;
  }

  [IgnoreMember]
  public bool IsFullscreenMode
  {
    get => this._isFullscreeMode;
    set => this._isFullscreeMode = value;
  }

  public void CopyData(Save_GameSettings value)
  {
    if (value == null)
      return;
    this.GameSFXVolume = value.GameSFXVolume;
    this.AnnouncerVolume = value.AnnouncerVolume;
    this.GameOptionsMusicVolume = value.GameOptionsMusicVolume;
    this.MenuMusicVolume = value.MenuMusicVolume;
    this.SoundOn = value.SoundOn;
    this.MusicOn = value.MusicOn;
    this.AnnouncerOn = value.AnnouncerOn;
    this.GraphicsQuality = value.GraphicsQuality;
    this.ResolutionWidth = value.ResolutionWidth;
    this.ResolutionHeight = value.ResolutionHeight;
    this.ignoreControllers = value.ignoreControllers;
    this.runInBackground = value.runInBackground;
    this.SensitivitySliderP1 = value.SensitivitySliderP1;
    this.SensitivitySliderP2 = value.SensitivitySliderP2;
    this.LeadReceiverValueP1 = value.LeadReceiverValueP1;
    this.LeadReceiverValueP2 = value.LeadReceiverValueP2;
    this.GlobalTimeScale = value.GlobalTimeScale;
    this.ButtonPassingP1 = value.ButtonPassingP1;
    this.ButtonPassingP2 = value.ButtonPassingP2;
    this.StickPushP1 = value.StickPushP1;
    this.StickPushP2 = value.StickPushP2;
    this.AcceleratedClockValue = value.AcceleratedClockValue;
    this.ButtonStyle = value.ButtonStyle;
    this.ClockSpeed = value.ClockSpeed;
    this.ControllerIconStyle = value.ControllerIconStyle;
    this.TimeBetweenPlays = value.TimeBetweenPlays;
    this.CameraHeightZoom = value.CameraHeightZoom;
    this.CameraDepthZoom = value.CameraDepthZoom;
    this.ShowSidelinePlayers = value.ShowSidelinePlayers;
    this.GameLanguage = value.GameLanguage;
    this.DefDifficulty = value.DefDifficulty;
    this.OffDifficulty = value.OffDifficulty;
    this.QuarterLength = value.QuarterLength;
    this.UseFatigue = value.UseFatigue;
    this.UseInjuries = value.UseInjuries;
    this.TwoMinuteWarningEnabled = value.TwoMinuteWarningEnabled;
    this.TutorialEnabled = value.TutorialEnabled;
    this.InjuryFrequency = value.InjuryFrequency;
    this.PenaltyFrequency = value.PenaltyFrequency;
    this.DelayOfGame = value.DelayOfGame;
    this.FalseStart = value.FalseStart;
    this.OffensiveHolding = value.OffensiveHolding;
    this.OffensivePassInterference = value.OffensivePassInterference;
    this.DefensivePassInterference = value.DefensivePassInterference;
    this.Facemask = value.Facemask;
    this.IllegalForwardPass = value.IllegalForwardPass;
    this.Offsides = value.Offsides;
    this.Encroachment = value.Encroachment;
    this.KickoffOutOfBounds = value.KickoffOutOfBounds;
    this.IntentionalGrounding = value.IntentionalGrounding;
    this._useBaseAssets = value._useBaseAssets;
    this._useModAssets = value._useModAssets;
    this._logGamesToFile = value._logGamesToFile;
    this._exportGameStatsToFile = value._exportGameStatsToFile;
    this._isFullscreeMode = value._isFullscreeMode;
    this._lockerInteractionHistory = value._lockerInteractionHistory;
    this._didCompleteTutorial = value._didCompleteTutorial;
    this.metaData = value.metaData;
  }

  private void SetDefaultValues()
  {
    this.UseBaseAssets = true;
    this.UseModAssets = false;
    this.LogGamesToFile = false;
    this.ExportGameStatsToFile = false;
    this.GameLanguage = "en";
    this._lockerInteractionHistory = 0;
    this.DefDifficulty = 1;
    this.OffDifficulty = 1;
    this.QuarterLength = 1;
    this.UseFatigue = true;
    this.SetDefaultAudioSettings();
    this.SetDefaultVideoSettings();
    this.SetDefaultGameSettings();
    this.SetDefaultPenaltySettings();
  }

  public void SetDefaultAudioSettings()
  {
    this.SoundOn = true;
    this.MusicOn = true;
    this.AnnouncerOn = true;
    this.GameSFXVolume = 0.7f;
    this.AnnouncerVolume = 0.7f;
    this.GameOptionsMusicVolume = 0.3f;
    this.MenuMusicVolume = 0.7f;
  }

  public void SetDefaultVideoSettings()
  {
    this.GraphicsQuality = 2;
    this.GraphicsQuality = 4;
    this.CameraHeightZoom = 0.4f;
    this.CameraDepthZoom = 0.4f;
    this.ShowSidelinePlayers = true;
    this.IsFullscreenMode = true;
    this.runInBackground = true;
    this.ignoreControllers = false;
    this.ResolutionHeight = 1080;
    this.ResolutionWidth = 1920;
  }

  public void SetDefaultGameSettings()
  {
    this.AcceleratedClockValue = 17;
    this.GlobalTimeScale = 0.75f;
    this.ButtonStyle = 1;
    this.ClockSpeed = 8;
    this.TimeBetweenPlays = 6;
    this.ControllerIconStyle = 1;
    this.UseInjuries = true;
    this.InjuryFrequency = 2;
    this.ButtonPassingP1 = true;
    this.ButtonPassingP2 = true;
    this.StickPushP1 = false;
    this.StickPushP2 = false;
    this.SensitivitySliderP1 = 0.5f;
    this.SensitivitySliderP2 = 0.5f;
    this.LeadReceiverValueP1 = 0.05f;
    this.LeadReceiverValueP2 = 0.05f;
    this.TwoMinuteWarningEnabled = true;
    this.TutorialEnabled = true;
  }

  public void SetDefaultPenaltySettings()
  {
    this.PenaltyFrequency = 10f;
    this.DelayOfGame = true;
    this.FalseStart = true;
    this.OffensiveHolding = true;
    this.OffensivePassInterference = true;
    this.DefensivePassInterference = true;
    this.Facemask = true;
    this.IllegalForwardPass = true;
    this.Offsides = true;
    this.Encroachment = true;
    this.KickoffOutOfBounds = true;
    this.IntentionalGrounding = true;
  }

  public void SetDefaultDataSettings()
  {
    this.UseBaseAssets = true;
    this.UseModAssets = false;
    this.LogGamesToFile = false;
    this.ExportGameStatsToFile = false;
  }

  public bool GetDirty() => this.isDirty;

  public void SetDirty(bool value)
  {
    this.isDirty = value;
    if (!this.isDirty)
      return;
    AppEvents.SaveGameSettings.Trigger();
  }

  public int[] GetVersion() => SaveSyncUtils.GetAppVersion(this.metaData);

  public void SetVersion(string value) => SaveSyncUtils.SetAppVersion(value, ref this.metaData);

  public async Task Load()
  {
    Task<Save_GameSettings> t = SaveIO.LoadAsync<Save_GameSettings>(Path.Combine(SaveIO.DefaultFolderPath, Save_GameSettings.FileName));
    Save_GameSettings saveGameSettings = await t;
    this.CopyData(t.Result);
    t = (Task<Save_GameSettings>) null;
  }

  public async Task Save()
  {
    Save_GameSettings objectTarget = this;
    SaveSyncUtils.SetSaveTimeCreated(ref objectTarget.metaData);
    SaveSyncUtils.SetSaveTimeModified(ref objectTarget.metaData);
    SaveSyncUtils.SetAppVersion(SaveIO.gameVersion, ref objectTarget.metaData);
    await SaveIO.SaveAsync<Save_GameSettings>(objectTarget, Path.Combine(SaveIO.DefaultFolderPath, Save_GameSettings.FileName));
  }

  public DateTime GetCreatedDate() => SaveSyncUtils.GetSaveTimeCreated(this.metaData);

  public DateTime GetLastModifiedDate() => SaveSyncUtils.GetSaveTimeModified(this.metaData);
}
