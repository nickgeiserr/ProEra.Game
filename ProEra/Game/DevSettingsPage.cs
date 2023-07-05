// Decompiled with JetBrains decompiler
// Type: ProEra.Game.DevSettingsPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Game.Achievements;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.IO;
using UnityEngine;

namespace ProEra.Game
{
  public class DevSettingsPage : TabletPage
  {
    [Space(10f)]
    [SerializeField]
    private TouchUI2DButton _btnDeleteAllSavedFiles;
    [SerializeField]
    private TouchUI2DButton _btnCriticalErrorsOnScreen;
    [SerializeField]
    private TouchUI2DButton _btnResetAchievements;
    [SerializeField]
    private TouchUI2DButton _btnToggleMiniCamp;
    [SerializeField]
    private TouchUI2DButton _btnToggleEyeTrackingDebugSphere;

    private AchievementData _achievementData => SaveManager.GetAchievementData();

    private void Awake()
    {
      this._pageType = TabletPage.Pages.DevSettings;
      this.CheckLogs();
    }

    private void OnEnable()
    {
      this._btnDeleteAllSavedFiles.onClick += new System.Action(this.HandleDeleteAllSavedFiles);
      this._btnCriticalErrorsOnScreen.onClick += new System.Action(this.HandleCriticalErrorsOnScreen);
      this._btnResetAchievements.onClick += new System.Action(this.HandleResetAchievements);
      this._btnToggleMiniCamp.onClick += new System.Action(this.HandleToggleMiniCamp);
      this._btnToggleEyeTrackingDebugSphere.onClick += new System.Action(this.HandleToggleEyeTrackingDebugSphere);
    }

    private void OnDisable()
    {
      this._btnDeleteAllSavedFiles.onClick -= new System.Action(this.HandleDeleteAllSavedFiles);
      this._btnCriticalErrorsOnScreen.onClick -= new System.Action(this.HandleCriticalErrorsOnScreen);
      this._btnResetAchievements.onClick -= new System.Action(this.HandleResetAchievements);
      this._btnToggleMiniCamp.onClick -= new System.Action(this.HandleToggleMiniCamp);
      this._btnToggleEyeTrackingDebugSphere.onClick -= new System.Action(this.HandleToggleEyeTrackingDebugSphere);
    }

    private void HandleDeleteAllSavedFiles() => this.DeleteAllSaves();

    private void HandleCriticalErrorsOnScreen()
    {
      if ((UnityEngine.Object) PersistentSingleton<LogsController>.Instance == (UnityEngine.Object) null)
        return;
      LogsSettings settings = PersistentSingleton<LogsController>.Instance.GetSettings();
      if (settings == null)
        return;
      settings.FirstCriticalErrorOnScreen = !settings.FirstCriticalErrorOnScreen;
      settings.Save();
      this._btnCriticalErrorsOnScreen.SetLabelText(settings.FirstCriticalErrorOnScreen ? "Critical errors on screen\nENABLED" : "Critical errors on screen\nDISABLED");
    }

    private void CheckLogs()
    {
      if ((UnityEngine.Object) PersistentSingleton<LogsController>.Instance == (UnityEngine.Object) null)
      {
        this._btnCriticalErrorsOnScreen.gameObject.SetActive(false);
      }
      else
      {
        LogsSettings settings = PersistentSingleton<LogsController>.Instance.GetSettings();
        if (settings == null)
          return;
        this._btnCriticalErrorsOnScreen.SetLabelText(settings.FirstCriticalErrorOnScreen ? "Critical errors on screen\nENABLED" : "Critical errors on screen\nDISABLED");
      }
    }

    private void DeleteAllSaves()
    {
      foreach (string directory in Directory.GetDirectories(Application.persistentDataPath))
        new DirectoryInfo(directory).Delete(true);
      foreach (string file in Directory.GetFiles(Application.persistentDataPath))
        new FileInfo(file).Delete();
      PlayerPrefs.DeleteAll();
    }

    private void HandleResetAchievements() => this._achievementData.Reset();

    private void HandleToggleMiniCamp()
    {
      Save_MiniCamp miniCamp = PersistentSingleton<SaveManager>.Instance.miniCamp;
      if (miniCamp.AreLevelsMaxed())
        miniCamp.MinimizeAllLevels();
      else
        miniCamp.MaximizeAllLevels();
      miniCamp.SetDirty(true);
    }

    private void HandleBackButton() => (this.MainPage as DevConsolePage).OpenPage(TabletPage.Pages.DevConsole);

    private void HandleToggleEyeTrackingDebugSphere()
    {
      if (!((UnityEngine.Object) DebugGazeSphere.instance != (UnityEngine.Object) null))
        return;
      DebugGazeSphere.instance.ToggleDebugSphereIsActive();
    }
  }
}
