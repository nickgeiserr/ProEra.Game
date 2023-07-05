// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlayCallView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.Data;
using Framework.UI;
using ProEra.Game;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vars;

namespace TB12.UI
{
  public class PlayCallView : UIView
  {
    [SerializeField]
    protected AxisPlaysStore _store;
    [SerializeField]
    private TouchButton _okButton;
    [SerializeField]
    protected CircularLayout _playTypeSelection;
    [SerializeField]
    protected CircularLayout _formationSelection;
    [SerializeField]
    protected CircularLayout _playSelection;
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
    protected bool _audible;
    private bool _laserWasEnabled;
    private bool _catched;
    private int _catchedPlayTypeIndex;
    private int _catchedFormationIndex;
    private int _catchedPlaySelectionIndex;

    public override Enum ViewId { get; } = (Enum) EScreens.kSelectPlay;

    protected override void OnInitialize()
    {
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._okButton.Link(new System.Action(this.HandleOk)),
        PlaybookState.FormationBase.Link<BaseFormation>(new Action<BaseFormation>(this.HandleBaseFormationChanged)),
        PlaybookState.CurrentFormation.Link<FormationData>(new Action<FormationData>(this.HandleFormationChanged)),
        PlaybookState.CurrentPlay.Link<PlayData>(new Action<PlayData>(this.HandlePlayChanged)),
        PlaybookState.SetPlaybookType.Link<EPlaybookType>(new Action<EPlaybookType>(this.SetPlaybookTypeHandler)),
        PlaybookState.ShowPlaybook.Link((System.Action) (() => this.Show())),
        PlaybookState.HidePlaybook.Link((System.Action) (() => this.Hide()))
      });
      PlaybookState.OnOffense.Value = global::Game.IsPlayerOneOnOffense;
    }

    private void SetPlaybookTypeHandler(EPlaybookType obj)
    {
    }

    protected override void WillAppear()
    {
      ScoreClockState.SetYardLine.Trigger();
      this._store.Initialize();
    }

    protected override void DidAppear()
    {
      if (this._catched)
      {
        this._catched = false;
        this._playTypeSelection.CurrentIndex = this._catchedPlayTypeIndex;
        this.HandleSelectedBaseFormationChanged(this._catchedPlayTypeIndex);
        this._formationSelection.CurrentIndex = this._catchedFormationIndex;
        this.HandleSelectedFormationChanged(this._catchedFormationIndex);
        this._playSelection.CurrentIndex = this._catchedPlaySelectionIndex;
        this.HandleSelectedPlayChanged(this._catchedPlaySelectionIndex);
      }
      this._playTypeSelection.OnCurrentIndexChanged += new Action<int>(this.HandleSelectedBaseFormationChanged);
      this._formationSelection.OnCurrentIndexChanged += new Action<int>(this.HandleSelectedFormationChanged);
      this._playSelection.OnCurrentIndexChanged += new Action<int>(this.HandleSelectedPlayChanged);
      PlaybookState.IsShown.SetValue(true);
    }

    protected override void WillDisappear()
    {
      this._catchedPlayTypeIndex = this._playTypeSelection.CurrentIndex;
      this._catchedFormationIndex = this._formationSelection.CurrentIndex;
      this._catchedPlaySelectionIndex = this._playSelection.CurrentIndex;
      this._catched = true;
      this._playTypeSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleSelectedBaseFormationChanged);
      this._formationSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleSelectedFormationChanged);
      this._playSelection.OnCurrentIndexChanged -= new Action<int>(this.HandleSelectedPlayChanged);
      PlaybookState.IsShown.SetValue(false);
    }

    protected void HandleSelectedBaseFormationChanged(int baseIndex) => PlaybookState.FormationBase.SetValue(this._store.GetBaseFormation(baseIndex));

    protected void HandleSelectedFormationChanged(int formationIndex)
    {
      FormationData formationData = this._store.GetFormationData((BaseFormation) (Variable<BaseFormation>) PlaybookState.FormationBase, formationIndex);
      PlaybookState.CurrentFormation.SetValue(formationData);
    }

    protected virtual void HandleSelectedPlayChanged(int playIndex)
    {
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      PlayData play = formationData?.GetPlay(playIndex);
      if (play == null)
        Debug.LogError((object) string.Format("Null formation or play not found {0}, {1}", (object) (formationData != null), (object) playIndex));
      else
        PlaybookState.CurrentPlay.SetValue(play);
    }

    protected void HandleBaseFormationChanged(BaseFormation data)
    {
      this._formationSelection.InvalidateCache();
      this.HandleSelectedFormationChanged(0);
    }

    protected void HandleFormationChanged(FormationData formationData)
    {
      this._playSelection.InvalidateCache();
      this.HandleSelectedPlayChanged(0);
    }

    protected void HandlePlayChanged(PlayData playData)
    {
      PlayDataOff playDataOff = (PlayDataOff) playData;
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      if ((UnityEngine.Object) this._playGraphic.spriteRenderer != (UnityEngine.Object) null)
        this._store.SetupPlayImage(this._playGraphic.spriteRenderer, playData);
      this._playName.text = playDataOff.GetPlayName();
      this._concept.text = playDataOff.GetPlayConceptString();
      this._playType.text = playDataOff.GetPlayType().ToString().ToUpper();
      this._personnel.text = formationData.GetFormationPositions().GetPersonnel();
    }

    protected void HandleOk()
    {
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      int currentIndex = this._playSelection.CurrentIndex;
      PlayData play = formationData?.GetPlay(currentIndex);
      if (play == null)
      {
        Debug.LogError((object) string.Format("Null formation or play not found {0}, {1}", (object) (formationData != null), (object) currentIndex));
      }
      else
      {
        MatchManager.instance.playManager.SetPlay(play, false, this._audible, (bool) PlaybookState.OnOffense);
        UIDispatch.FrontScreen.HideView(EScreens.kSelectPlay);
        MatchManager.instance.timeManager.SetRunPlayClock(true);
      }
    }

    protected override void OnSetVisible(bool visible)
    {
      foreach (Collider componentsInChild in this.gameObject.GetComponentsInChildren<BoxCollider>())
        componentsInChild.enabled = visible;
    }
  }
}
