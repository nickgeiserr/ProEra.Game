// Decompiled with JetBrains decompiler
// Type: APracticeModeScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12;
using TB12.UI;
using TMPro;
using UDB;
using UnityEngine;
using Vars;

public class APracticeModeScreen : UIView
{
  [Header("Ball Placement Settings")]
  [SerializeField]
  private TouchButton _centerOfFieldButton;
  [SerializeField]
  private TouchButton _incrementButton;
  [SerializeField]
  private TouchButton _decrementButton;
  [SerializeField]
  private TouchButton _oneYardIncrementButton;
  [SerializeField]
  private TouchButton _fiveYardIncrementButton;
  [SerializeField]
  private TouchButton _tenYardIncrementButton;
  [SerializeField]
  private TouchButton _centerOfScrimmageButton;
  [SerializeField]
  private TouchButton _leftHashButton;
  [SerializeField]
  private TouchButton _rightHashButton;
  [SerializeField]
  private float _actionDelay = 0.1f;
  [SerializeField]
  private BallPlacementManager _ballPlacementManager;
  [Header("Playbook Settings")]
  [SerializeField]
  private CircularLayout _baseFormSelection;
  [SerializeField]
  private CircularLayout _formationSelection;
  [SerializeField]
  private CircularLayout _playSelection;
  [SerializeField]
  private PracticePlaysStore _store;
  [SerializeField]
  private TouchButton _offensivePlaysButton;
  [SerializeField]
  private TouchButton _defensivePlaysButton;
  [SerializeField]
  private TouchButton _defenseTeamToggleButton;
  [SerializeField]
  private bool _defenseIsOn = true;
  [SerializeField]
  private TouchButton _okButton;
  [SerializeField]
  private PlayImageItem _playGraphic;
  [SerializeField]
  private TextMeshProUGUI _playName;
  [SerializeField]
  private TextMeshProUGUI _concept;
  [SerializeField]
  private TextMeshProUGUI _personnel;
  [SerializeField]
  private TextMeshProUGUI _playType;
  private ButtonText _defenseTeamToggleButtonText;
  private bool _cachedOffense;
  private int _cachedOffensivePlayTypeIndex;
  private int _cachedOffensiveFormationIndex;
  private int _cachedOffensivePlaySelectionIndex;
  private bool _cachedDefense;
  private int _cachedDefensivePlayTypeIndex;
  private int _cachedDefensiveFormationIndex;
  private int _cachedDefensivePlaySelectionIndex;

  public override Enum ViewId { get; } = (Enum) EScreens.kPracticeMode;

  public PlayDataOff CurrentOffensePlay { get; private set; }

  public PlayDataDef CurrentDefensePlay { get; private set; }

  public EPlaybookType CurrentPlaybookType { get; private set; }

  protected override void OnInitialize()
  {
    this.linksHandler.SetLinks(new List<EventHandle>()
    {
      (EventHandle) UIHandle.Link((IButton) this._centerOfFieldButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.CenterOfField))),
      (EventHandle) UIHandle.Link((IButton) this._incrementButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.Increment))),
      (EventHandle) UIHandle.Link((IButton) this._decrementButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.Decrement))),
      (EventHandle) UIHandle.Link((IButton) this._oneYardIncrementButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.OneYardIncrement))),
      (EventHandle) UIHandle.Link((IButton) this._fiveYardIncrementButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.FiveYardIncrement))),
      (EventHandle) UIHandle.Link((IButton) this._tenYardIncrementButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.TenYardIncrement))),
      (EventHandle) UIHandle.Link((IButton) this._centerOfScrimmageButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.CenterOfHash))),
      (EventHandle) UIHandle.Link((IButton) this._leftHashButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.LeftHash))),
      (EventHandle) UIHandle.Link((IButton) this._rightHashButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.RightHash))),
      (EventHandle) UIHandle.Link((IButton) this._offensivePlaysButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.OffensivePlays))),
      (EventHandle) UIHandle.Link((IButton) this._defensivePlaysButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.DefensivePlays))),
      (EventHandle) UIHandle.Link((IButton) this._defenseTeamToggleButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.ToggleDefense))),
      (EventHandle) UIHandle.Link((IButton) this._okButton, (System.Action) (() => this.HandleResult(APracticeModeScreen.EPracticeModeScreenAction.Ok))),
      PlaybookState.FormationBase.Link<BaseFormation>(new Action<BaseFormation>(this.HandleBaseFormationChanged)),
      PlaybookState.CurrentFormation.Link<FormationData>(new Action<FormationData>(this.HandleFormationChanged)),
      PlaybookState.CurrentPlay.Link<PlayData>(new Action<PlayData>(this.HandlePlayChanged)),
      PlaybookState.ShowPlaybook.Link((System.Action) (() => this.Show())),
      PlaybookState.HidePlaybook.Link((System.Action) (() => this.Hide()))
    });
    PlaybookState.OnOffense.Value = global::Game.IsPlayerOneOnOffense;
  }

  private async void HandleResult(
    APracticeModeScreen.EPracticeModeScreenAction action)
  {
    await System.Threading.Tasks.Task.Delay((int) ((double) this._actionDelay * 1000.0));
    switch (action)
    {
      case APracticeModeScreen.EPracticeModeScreenAction.CenterOfField:
        this._ballPlacementManager.PlaceBallAtCenterOfField();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.Increment:
        this._ballPlacementManager.IncrementBallPosition();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.Decrement:
        this._ballPlacementManager.DecrementBallPosition();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.OneYardIncrement:
        this._ballPlacementManager.SetIncrementAmount(1);
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.FiveYardIncrement:
        this._ballPlacementManager.SetIncrementAmount(5);
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.TenYardIncrement:
        this._ballPlacementManager.SetIncrementAmount(10);
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.CenterOfHash:
        this._ballPlacementManager.PlaceBallAtCenterOfHash();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.LeftHash:
        this._ballPlacementManager.PlaceBallAtLeftHash();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.RightHash:
        this._ballPlacementManager.PlaceBallAtRightHash();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.OffensivePlays:
        this.SetPlaybookTypeHandler(EPlaybookType.Offense);
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.DefensivePlays:
        this.SetPlaybookTypeHandler(EPlaybookType.Defense);
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.ToggleDefense:
        this.ToggleDefense();
        break;
      case APracticeModeScreen.EPracticeModeScreenAction.Ok:
        this.HandleOk();
        return;
    }
    this._ballPlacementManager.SetBallLocation();
  }

  private void SetPlaybookTypeHandler(EPlaybookType newPlaybookType)
  {
    PlaybookState.SetPlaybookType.Trigger(newPlaybookType);
    if (this.CurrentPlaybookType != newPlaybookType)
    {
      this.SavePlaybookCache();
      if (newPlaybookType == EPlaybookType.Offense)
      {
        this.CurrentDefensePlay = (PlayDataDef) (PlayData) PlaybookState.CurrentPlay;
        PlaybookState.OnOffense.Value = true;
        this._offensivePlaysButton.SetInteractible(false);
        if ((UnityEngine.Object) this._defensivePlaysButton != (UnityEngine.Object) null)
          this._defensivePlaysButton.SetInteractible(true);
      }
      else
      {
        this.CurrentOffensePlay = (PlayDataOff) (PlayData) PlaybookState.CurrentPlay;
        PlaybookState.OnOffense.Value = false;
        this._defensivePlaysButton.SetInteractible(false);
        if ((UnityEngine.Object) this._offensivePlaysButton != (UnityEngine.Object) null)
          this._offensivePlaysButton.SetInteractible(true);
      }
      this.CurrentPlaybookType = newPlaybookType;
    }
    this._store.LoadPlayBook(newPlaybookType);
    this._baseFormSelection.InvalidateCache();
    this.LoadPlaybookCache();
  }

  private void ToggleDefense()
  {
    if (this._defenseTeamToggleButtonText == null)
      this._defenseTeamToggleButton.TryGetComponent<ButtonText>(out this._defenseTeamToggleButtonText);
    if (this._defenseIsOn)
    {
      this._defenseIsOn = false;
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.defenseWillStandStill = true;
      this._defenseTeamToggleButtonText.text = "Defense Is Off";
    }
    else
    {
      this._defenseIsOn = true;
      SingletonBehaviour<PersistentData, MonoBehaviour>.instance.defenseWillStandStill = false;
      this._defenseTeamToggleButtonText.text = "Defense Is On";
    }
  }

  protected override void WillAppear()
  {
    ProEra.Game.MatchState.Down.Value = 1;
    ProEra.Game.MatchState.Turnover.Value = false;
    if (!MatchManager.Exists())
      return;
    MatchManager.instance.playManager.autoSelectPlayForAI = false;
    MatchManager.instance.playManager.quickCleanUp = true;
    ScoreClockState.SetYardLine.Trigger();
    MatchManager.instance.timeManager.SetRunPlayClock(false);
    if ((UnityEngine.Object) this._store != (UnityEngine.Object) null)
    {
      this._store.HideSpecialTeams = true;
      if (PersistentData.GameMode == GameMode.Coach)
        this._store.HideSpecialTeams = false;
      this._store.Initialize(this.CurrentPlaybookType);
    }
    if ((UnityEngine.Object) this._ballPlacementManager != (UnityEngine.Object) null)
      this._ballPlacementManager.Initialize();
    this.LoadPlaybookCache();
    MatchManager.instance.playersManager.PutAllPlayersInHuddle();
  }

  protected override void DidAppear()
  {
    this._baseFormSelection.OnCurrentIndexChanged += new Action<int>(this.HandleSelectedBaseFormationChanged);
    this._formationSelection.OnCurrentIndexChanged += new Action<int>(this.HandleSelectedFormationChanged);
    this._playSelection.OnCurrentIndexChanged += new Action<int>(this.HandleSelectedPlayChanged);
    PlaybookState.IsShown.SetValue(true);
  }

  protected override void WillDisappear()
  {
    this.SavePlaybookCache();
    this._baseFormSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleSelectedBaseFormationChanged);
    this._formationSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleSelectedFormationChanged);
    this._playSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleSelectedPlayChanged);
    PlaybookState.IsShown.SetValue(false);
  }

  private void HandleSelectedBaseFormationChanged(int baseIndex) => PlaybookState.FormationBase.SetValue(this._store.GetBaseFormation(baseIndex));

  private void HandleSelectedFormationChanged(int formationIndex)
  {
    FormationData formationData = this._store.GetFormationData((BaseFormation) (Variable<BaseFormation>) PlaybookState.FormationBase, formationIndex);
    PlaybookState.CurrentFormation.SetValue(formationData);
  }

  private void HandleSelectedPlayChanged(int playIndex)
  {
    FormationData formationData = PlaybookState.CurrentFormation.Value;
    PlayData play = formationData?.GetPlay(playIndex);
    if (play == null)
      Debug.LogError((object) string.Format("Null formation or play not found {0}, {1}", (object) (formationData != null), (object) playIndex));
    else
      PlaybookState.CurrentPlay.SetValue(play);
  }

  private void HandleBaseFormationChanged(BaseFormation data)
  {
    this._formationSelection.InvalidateCache();
    this.HandleSelectedFormationChanged(0);
  }

  private void HandleFormationChanged(FormationData formationData)
  {
    this._playSelection.InvalidateCache();
    this.HandleSelectedPlayChanged(0);
  }

  private void HandlePlayChanged(PlayData playData)
  {
    if (this.CurrentPlaybookType == EPlaybookType.Defense)
    {
      playData = (PlayData) (playData as PlayDataDef);
      Plays.self.GetCurDefPlaybookP1();
    }
    else
    {
      playData = (PlayData) (playData as PlayDataOff);
      Plays.self.GetCurOffPlaybookP1();
    }
    FormationData formationData = PlaybookState.CurrentFormation.Value;
    if ((UnityEngine.Object) this._playGraphic.spriteRenderer != (UnityEngine.Object) null)
      this._store.SetupPlayImage(this._playGraphic.spriteRenderer, playData);
    this._playName.text = playData.GetPlayName();
    this._concept.text = playData.GetPlayConceptString();
    this._playType.text = playData is PlayDataOff ? ((PlayDataOff) playData).GetPlayType().ToString().ToUpper() : "";
    this._personnel.text = formationData.GetFormationPositions().GetPersonnel();
  }

  private void SavePlaybookCache()
  {
    Debug.Log((object) ("Saving the " + this.CurrentPlaybookType.ToString() + " Cache"));
    if (this.CurrentPlaybookType == EPlaybookType.Offense)
    {
      this._cachedOffensivePlayTypeIndex = this._baseFormSelection.CurrentIndex;
      this._cachedOffensiveFormationIndex = this._formationSelection.CurrentIndex;
      this._cachedOffensivePlaySelectionIndex = this._playSelection.CurrentIndex;
      this._cachedOffense = true;
    }
    else
    {
      this._cachedDefensivePlayTypeIndex = this._baseFormSelection.CurrentIndex;
      this._cachedDefensiveFormationIndex = this._formationSelection.CurrentIndex;
      this._cachedDefensivePlaySelectionIndex = this._playSelection.CurrentIndex;
      this._cachedDefense = true;
    }
  }

  private void LoadPlaybookCache()
  {
    Debug.Log((object) ("Loading the " + this.CurrentPlaybookType.ToString() + " Cache"));
    if (this.CurrentPlaybookType == EPlaybookType.Offense)
    {
      if (!this._cachedOffense)
        return;
      this._cachedOffense = false;
      this._baseFormSelection.CurrentIndex = this._cachedOffensivePlayTypeIndex;
      this.HandleSelectedBaseFormationChanged(this._cachedOffensivePlayTypeIndex);
      this._formationSelection.CurrentIndex = this._cachedOffensiveFormationIndex;
      this.HandleSelectedFormationChanged(this._cachedOffensiveFormationIndex);
      this._playSelection.CurrentIndex = this._cachedOffensivePlaySelectionIndex;
      this.HandleSelectedPlayChanged(this._cachedOffensivePlaySelectionIndex);
    }
    else
    {
      if (!this._cachedDefense)
        return;
      this._cachedDefense = false;
      this._baseFormSelection.CurrentIndex = this._cachedDefensivePlayTypeIndex;
      this.HandleSelectedBaseFormationChanged(this._cachedDefensivePlayTypeIndex);
      this._formationSelection.CurrentIndex = this._cachedDefensiveFormationIndex;
      this.HandleSelectedFormationChanged(this._cachedDefensiveFormationIndex);
      this._playSelection.CurrentIndex = this._cachedDefensivePlaySelectionIndex;
      this.HandleSelectedPlayChanged(this._cachedDefensivePlaySelectionIndex);
    }
  }

  private void HandleOk() => this.StartCoroutine(this.WaitToHandleOk());

  private IEnumerator WaitToHandleOk()
  {
    yield return (object) GamePlayerController.CameraFade.Fade();
    FormationData formationData = PlaybookState.CurrentFormation.Value;
    int currentIndex = this._playSelection.CurrentIndex;
    if (formationData?.GetPlay(currentIndex) == null)
    {
      Debug.LogError((object) string.Format("Null formation or play not found {0}, {1}", (object) (formationData != null), (object) currentIndex));
    }
    else
    {
      Debug.Log((object) ("Current Selected Play is: " + PlaybookState.CurrentPlay.Value.GetFormation().ToString()));
      if (this.CurrentPlaybookType == EPlaybookType.Offense)
        this.CurrentOffensePlay = PlaybookState.CurrentPlay.Value as PlayDataOff;
      else
        this.CurrentDefensePlay = PlaybookState.CurrentPlay.Value as PlayDataDef;
      if (this.CurrentOffensePlay == null)
        this.CurrentOffensePlay = MatchManager.instance.playManager.GetOffensivePlayForAI();
      if (this.CurrentDefensePlay == null)
        this.CurrentDefensePlay = MatchManager.instance.playManager.GetDefensivePlayForAI(this.CurrentOffensePlay);
      this.SavePlaybookCache();
      this.SetPlaybookTypeHandler(EPlaybookType.Offense);
      MonoBehaviour.print((object) "<b>Practice Mode Log:</b>");
      MonoBehaviour.print((object) ("Current Playbook Active is " + this.CurrentPlaybookType.ToString()));
      MonoBehaviour.print((object) ("Current Offense Play is : " + this.CurrentOffensePlay.GetPlayName()));
      MonoBehaviour.print((object) ("Current Defense Play is : " + this.CurrentDefensePlay.GetPlayName()));
      MonoBehaviour.print((object) ("Current Down is: " + ProEra.Game.MatchState.Down.Value.ToString()));
      MonoBehaviour.print((object) string.Format("Ball is on the {0} Yard Line", (object) this._ballPlacementManager.BallYardLine));
      MonoBehaviour.print((object) string.Format("Ball is on the {0} Hash Position", (object) this._ballPlacementManager.BallHashPosition));
      ProEra.Game.MatchState.Turnover.Value = false;
      bool offenseGoingNorth = (bool) FieldState.OffenseGoingNorth;
      MatchManager.instance.playManager.LoadPlay(this.CurrentOffensePlay, this.CurrentDefensePlay, this._ballPlacementManager.BallYardLine, this._ballPlacementManager.BallHashPosition, offenseGoingNorth, false);
      GameSettings.OffenseGoingNorth.SetValue(offenseGoingNorth);
      PlaybookState.HidePlaybook.Trigger();
      UIDispatch.FrontScreen.HideView(EScreens.kPracticeMode);
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      MatchManager.instance.playersManager.PutAllPlayersInPlayPosition();
    }
  }

  protected override void OnSetVisible(bool visible)
  {
    foreach (Collider componentsInChild in this.gameObject.GetComponentsInChildren<BoxCollider>())
      componentsInChild.enabled = visible;
    this._playSelection.gameObject.SetActive(visible);
    this._playSelection.GetComponent<Collider>().enabled = visible;
  }

  private enum EPracticeModeScreenAction
  {
    CenterOfField,
    Increment,
    Decrement,
    OneYardIncrement,
    FiveYardIncrement,
    TenYardIncrement,
    CenterOfHash,
    LeftHash,
    RightHash,
    OffensivePlays,
    DefensivePlays,
    ToggleDefense,
    Ok,
  }
}
