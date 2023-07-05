// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.PracticeModeState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using ProEra.Game;
using System.Collections;
using System.Collections.Generic;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/PracticeModeState")]
  public class PracticeModeState : AxisGameState
  {
    [EditorSetting(ESettingType.Debug)]
    private static bool coachmode;
    [Header("Settings")]
    [SerializeField]
    private float _playbookDisplayDelay = 4f;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    public override bool clearFadeOnEntry => false;

    public override EAppState Id => EAppState.kPracticeMode;

    public EScreens PlaybookToDisplay => EScreens.kPracticeMode;

    public event System.Action FinishedEnable;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      this.OnEnterStateAsync();
    }

    private async System.Threading.Tasks.Task OnEnterStateAsync()
    {
      PracticeModeState practiceModeState = this;
      // ISSUE: reference to a compiler-generated method
      practiceModeState._linksHandler.AddLinks((IReadOnlyList<EventHandle>) new List<EventHandle>()
      {
        PlaybookState.ShowPlaybook.Link(new System.Action(practiceModeState.ShowPlaybookHandler)),
        PlaybookState.HidePlaybook.Link(new System.Action(practiceModeState.StopShowPlayBook)),
        AppEvents.LoadMainMenu.Link(new System.Action(practiceModeState.\u003COnEnterStateAsync\u003Eb__14_0))
      });
      WorldState.CrowdEnabled.SetValue(false);
      AppSounds.CrowdSound.SetValue(false);
      AppSounds.AnnouncerSound.SetValue(false);
      UIDispatch.FrontScreen.CloseScreen();
      AppSounds.StopSfx(ESfxTypes.kTunnel);
      AppSounds.AmbienceSound.ForceValue(false);
      await MatchManager.instance.playersManager.IsInitialized();
      PlaybookState.ShowPlaybook.Trigger();
      GamePlayerController.CameraFade.Clear(0.5f, 1f);
      System.Action finishedEnable = practiceModeState.FinishedEnable;
      if (finishedEnable == null)
        return;
      finishedEnable();
    }

    protected override void OnExitState()
    {
      this._linksHandler.Clear();
      AppSounds.MusicSound.SetValue(true);
      UIDispatch.FrontScreen.HideView(EScreens.kPracticeMode);
      base.OnExitState();
    }

    private void ShowPlaybookHandler()
    {
      this.RunShowPlaybookHandler(this._playbookDisplayDelay);
      VRState.LocomotionEnabled.SetValue(false);
      PEGameplayEventManager.RecordShowPlaybookEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
    }

    public Coroutine RunShowPlaybookHandler(float delay) => this._routineHandle.Run(this.ShowPlaybookHandler_Coroutine(delay));

    private IEnumerator ShowPlaybookHandler_Coroutine(float delay)
    {
      yield return (object) new WaitForSeconds(delay);
      UIDispatch.FrontScreen.DisplayView(this.PlaybookToDisplay);
    }

    private void StopShowPlayBook()
    {
      this._routineHandle.Stop();
      PEGameplayEventManager.RecordPlayCallMadeEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position);
      PlayDataOff savedOffPlay = MatchManager.instance.playManager.savedOffPlay;
      PEGameplayEventManager.RecordPlaySelectedEvent(MatchManager.instance.timeManager.GetGameClockTimer(), SingletonBehaviour<BallManager, MonoBehaviour>.instance.transform.position, MatchManager.instance.timeManager.GetQuarter(), global::Game.IsHomeTeamOnOffense, global::Game.OffenseGoingNorth, savedOffPlay, MatchManager.instance.playManager.savedDefPlay, MatchManager.down, ProEra.Game.MatchState.FirstDown.Value, ProEra.Game.MatchState.BallOn.Value);
    }

    protected override void UpdateGroupPresence() => GroupPresenceManager.Instance.UpdateGroupPresenceStatus_Offline(DestinationDefinitions.Destination.Core_PracticeMode);
  }
}
