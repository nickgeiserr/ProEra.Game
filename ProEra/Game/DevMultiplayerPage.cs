// Decompiled with JetBrains decompiler
// Type: ProEra.Game.DevMultiplayerPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System;
using UnityEngine;

namespace ProEra.Game
{
  public class DevMultiplayerPage : TabletPage
  {
    [Space(10f)]
    [SerializeField]
    private TouchUI2DButton _btnTestButton;

    private void Awake() => this._pageType = TabletPage.Pages.DevMultiplayer;

    private void OnEnable() => this._btnTestButton.onClick += new Action(this.HandleDoSomething);

    private void OnDisable() => this._btnTestButton.onClick -= new Action(this.HandleDoSomething);

    private void HandleDoSomething()
    {
    }

    private void HandleBackButton() => (this.MainPage as DevConsolePage).OpenPage(TabletPage.Pages.DevConsole);
  }
}
