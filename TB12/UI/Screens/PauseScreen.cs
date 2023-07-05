// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.PauseScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework.Data;
using Framework.Networked;
using Framework.UI;
using ProEra.Game.UI.Screens.Shared.Settings;
using UnityEngine;

namespace TB12.UI.Screens
{
  public class PauseScreen : UIPanel
  {
    [SerializeField]
    private PauseScreenV2 _singleplayerMenu;
    [SerializeField]
    private PauseScreenV2 _multiplayerMenu;
    private UIPanel _currentMenu;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public static bool isPaused { get; private set; }

    protected override void OnInitialize()
    {
      Transform transform = this.transform;
      transform.SetParent(UIAnchoring.PauseMenuCanvas.transform);
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
      this._singleplayerMenu.Initialize();
      this._multiplayerMenu.Initialize();
    }

    protected override void DidDisappear()
    {
      Time.timeScale = (float) GameSettings.TimeScale;
      UIDispatch.SetScreensVisible(true);
    }

    protected override void WillAppear()
    {
      if (!(bool) NetworkState.InRoom)
        Time.timeScale = 0.0f;
      UIDispatch.SetScreensVisible(false);
      this._currentMenu = (bool) NetworkState.InRoom ? (UIPanel) this._multiplayerMenu : (UIPanel) this._singleplayerMenu;
      this._currentMenu.Show();
    }

    protected override void WillDisappear()
    {
      Time.timeScale = (float) GameSettings.TimeScale;
      this._currentMenu.Hide();
    }

    protected override void OnDeinitialize() => this._linksHandler.Clear();

    public void HandlePauseState(bool state)
    {
      if (!VRState.PausePermission)
        return;
      if (state)
      {
        this.Show();
      }
      else
      {
        this._singleplayerMenu.CloseAllPopups();
        this._multiplayerMenu.CloseAllPopups();
        this.Hide();
      }
      PauseScreen.isPaused = state;
    }
  }
}
