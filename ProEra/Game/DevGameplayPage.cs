// Decompiled with JetBrains decompiler
// Type: ProEra.Game.DevGameplayPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using UDB;
using UnityEngine;

namespace ProEra.Game
{
  public class DevGameplayPage : TabletPage
  {
    [Space(10f)]
    [SerializeField]
    private TouchUI2DButton _btnAddScore;
    [SerializeField]
    private TouchUI2DButton _btnSubScore;
    [SerializeField]
    private TouchUI2DButton _btnAddToPlayClock;
    [SerializeField]
    private TouchUI2DButton _btnAddToGameClock;
    [SerializeField]
    private TouchUI2DButton _btnSkipQuarter;
    [SerializeField]
    private TouchUI2DButton _btnToggleDefenseAi;
    [SerializeField]
    private TouchUI2DButton _btnSimulateSeasonNormal;
    [SerializeField]
    private TouchUI2DButton _btnSimulateSeasonWin;
    [SerializeField]
    private TouchUI2DButton _btnIgnoreSidelineBoundary;
    [SerializeField]
    private TouchUI2DButton _btnFieldGoalDebug;

    private void Awake() => this._pageType = TabletPage.Pages.DevGameplay;

    private void OnEnable()
    {
      this._btnAddScore.onClick += new System.Action(this.HandleAddScore);
      this._btnSubScore.onClick += new System.Action(this.HandleSubScore);
      this._btnAddToPlayClock.onClick += new System.Action(this.HandleAddToPlayClock);
      this._btnAddToGameClock.onClick += new System.Action(this.HandleAddToGameClock);
      this._btnSkipQuarter.onClick += new System.Action(this.HandleSkipQuarter);
      this._btnToggleDefenseAi.onClick += new System.Action(this.HandleToggleDefenseAi);
      this._btnSimulateSeasonNormal.onClick += new System.Action(this.HandleSimulateSeasonNormal);
      this._btnSimulateSeasonWin.onClick += new System.Action(this.HandleSimulateSeasonWin);
      this._btnIgnoreSidelineBoundary.onClick += new System.Action(this.HandleIgnoreSidelineBoundary);
      this._btnFieldGoalDebug.onClick += new System.Action(this.HandleFieldGoalDebug);
      this.UpdateIgnoreSidelineBoundariesLabel();
      this.UpdateFieldGoalDebugLabel();
    }

    private void OnDisable()
    {
      this._btnAddScore.onClick -= new System.Action(this.HandleAddScore);
      this._btnSubScore.onClick -= new System.Action(this.HandleSubScore);
      this._btnAddToPlayClock.onClick -= new System.Action(this.HandleAddToPlayClock);
      this._btnAddToGameClock.onClick -= new System.Action(this.HandleAddToGameClock);
      this._btnSkipQuarter.onClick -= new System.Action(this.HandleSkipQuarter);
      this._btnToggleDefenseAi.onClick -= new System.Action(this.HandleToggleDefenseAi);
      this._btnSimulateSeasonNormal.onClick -= new System.Action(this.HandleSimulateSeasonNormal);
      this._btnSimulateSeasonWin.onClick -= new System.Action(this.HandleSimulateSeasonWin);
      this._btnIgnoreSidelineBoundary.onClick -= new System.Action(this.HandleIgnoreSidelineBoundary);
      this._btnFieldGoalDebug.onClick -= new System.Action(this.HandleFieldGoalDebug);
    }

    private void HandleAddScore()
    {
      if (!((UnityEngine.Object) MatchManager.instance != (UnityEngine.Object) null))
        return;
      MatchManager.instance.AddScore(1);
    }

    private void HandleSubScore()
    {
      if (!((UnityEngine.Object) MatchManager.instance != (UnityEngine.Object) null))
        return;
      MatchManager.instance.AddScore(-1);
    }

    private void HandleAddToPlayClock()
    {
      if (!((UnityEngine.Object) MatchManager.instance.timeManager != (UnityEngine.Object) null))
        return;
      MatchManager.instance.timeManager.AddToPlayClock(1.5f);
    }

    private void HandleAddToGameClock()
    {
      if (!((UnityEngine.Object) MatchManager.instance.timeManager != (UnityEngine.Object) null))
        return;
      MatchManager.instance.timeManager.AddToGameClock(1.5f);
    }

    private void HandleBackButton() => (this.MainPage as DevConsolePage).OpenPage(TabletPage.Pages.DevConsole);

    private void HandleSkipQuarter()
    {
      if (!((UnityEngine.Object) MatchManager.instance.timeManager != (UnityEngine.Object) null))
        return;
      MatchManager.instance.timeManager.DebugEndQuarter();
    }

    private void HandleToggleDefenseAi()
    {
      PersistentData instance = SingletonBehaviour<PersistentData, MonoBehaviour>.instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      instance.defenseWillStandStill = !instance.defenseWillStandStill;
      this._btnToggleDefenseAi.SetLabelText(instance.defenseWillStandStill ? "Disable Defense AI" : "Enable Defense AI");
    }

    private void HandleSimulateSeasonNormal() => SeasonModeManager.DebugSimulateSeason();

    private void HandleSimulateSeasonWin() => SeasonModeManager.DebugSimulateSeason(true);

    private void HandleIgnoreSidelineBoundary()
    {
      if ((UnityEngine.Object) SingletonBehaviour<PersistentData, MonoBehaviour>.instance == (UnityEngine.Object) null)
        return;
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.ignoreSidelineBoundary = !SingletonBehaviour<PersistentData, MonoBehaviour>.instance.ignoreSidelineBoundary;
      this.UpdateIgnoreSidelineBoundariesLabel();
    }

    private void UpdateIgnoreSidelineBoundariesLabel() => this._btnIgnoreSidelineBoundary.SetLabelText(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.ignoreSidelineBoundary ? "Respect Sideline Boundary" : "Ignore Sideline Boundary");

    private void HandleFieldGoalDebug()
    {
      if (!SingletonBehaviour<PersistentData, MonoBehaviour>.instance.alwaysKickFieldGoals)
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.alwaysKickFieldGoals = true;
      else if (!SingletonBehaviour<PersistentData, MonoBehaviour>.instance.fieldGoalsAlwaysMiss)
      {
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.fieldGoalsAlwaysMiss = true;
      }
      else
      {
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.alwaysKickFieldGoals = false;
        SingletonBehaviour<PersistentData, MonoBehaviour>.instance.fieldGoalsAlwaysMiss = false;
      }
      this.UpdateFieldGoalDebugLabel();
    }

    private void UpdateFieldGoalDebugLabel() => this._btnFieldGoalDebug.SetLabelText(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.alwaysKickFieldGoals ? (SingletonBehaviour<PersistentData, MonoBehaviour>.instance.fieldGoalsAlwaysMiss ? "Always Kick Missed FGs" : "Always Kick Field Goals") : "Normal Field Goals");
  }
}
