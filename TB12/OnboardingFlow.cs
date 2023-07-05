// Decompiled with JetBrains decompiler
// Type: TB12.OnboardingFlow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework.Data;
using ProEra.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TB12.UI;
using UDB;
using UnityEngine;

namespace TB12
{
  public class OnboardingFlow : AxisGameFlow
  {
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private int _currentTutorialStep;
    private bool _caughtBall = true;
    private bool _resetPlay;
    private int _playerTPCount;
    [Header("Onboarding Steps")]
    public List<OnboardingFlow.OnboardingStep> OnboardingSteps;

    protected override void Awake()
    {
      Debug.Log((object) "OnboardingFlow: Awake");
      base.Awake();
      this._ballHikeSequence.OnHikeComplete += new System.Action(this.TurnOnLocomotion);
      MatchManager.instance.OnAllowUserHurryUp -= new Action<bool>(((AxisGameFlow) this).SetHurryUpVisibility);
      MatchManager.instance.OnAllowUserTimeout -= new Action<bool>(((AxisGameFlow) this).SetTimeoutVisibility);
      PEGameplayEventManager.OnEventOccurred += new Action<PEGameplayEvent>(this.HandleGameEvent);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        PlaybookState.ShowPlaybook.Link(new System.Action(((AxisGameFlow) this).HandlePlayCallStarted))
      });
      MatchManager.instance.OnQBPositionChange += new System.Action(this.HandleQBPositionChange);
      ProEra.Game.MatchState.Down.ClearValueEvents();
      this._currentTutorialStep = 0;
      this._showTutorialScreen = false;
      AppSounds.PlayerChatterSound.ForceValue(false);
      this._showTutorialScreen = true;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this._ballHikeSequence.OnHikeComplete -= new System.Action(this.TurnOnLocomotion);
      this._linksHandler.Clear();
      PEGameplayEventManager.OnEventOccurred -= new Action<PEGameplayEvent>(this.HandleGameEvent);
      MatchManager.instance.OnQBPositionChange -= new System.Action(this.HandleQBPositionChange);
    }

    private void DoTutorialStep()
    {
      Debug.Log((object) ("DoTutorialStep: " + this._currentTutorialStep.ToString()));
      OnboardingFlow.OnboardingStep onboardingStep = this.OnboardingSteps[this._currentTutorialStep];
      MatchManager.instance.DisallowSnap();
      if (onboardingStep.PlayAudio)
        AppSounds.PlayTutorial(this._currentTutorialStep, new System.Action(this.FinishedTutorialAudio));
      else
        this.FinishedTutorialAudio();
      int index = -1;
      switch (onboardingStep.PlayFormation)
      {
        case OnboardingFlow.PlayFormation.ShotgunNormal:
          PlaybookState.CurrentFormation.SetValue(Plays.self.shotgunPlays_Normal);
          switch (onboardingStep.PlayType)
          {
            case OnboardingFlow.PlayType.TEDrag:
              index = 1;
              break;
            case OnboardingFlow.PlayType.TEOut:
              index = 7;
              break;
            case OnboardingFlow.PlayType.X:
              index = 5;
              break;
          }
          break;
        case OnboardingFlow.PlayFormation.SingleBackBig:
          PlaybookState.CurrentFormation.SetValue(Plays.self.singleBackPlays_Big);
          switch (onboardingStep.PlayType)
          {
            case OnboardingFlow.PlayType.TEDrag:
              index = 0;
              break;
            case OnboardingFlow.PlayType.TEOut:
              index = 13;
              break;
            case OnboardingFlow.PlayType.X:
              index = 17;
              break;
            case OnboardingFlow.PlayType.HBDiveWeak:
              index = 20;
              break;
            case OnboardingFlow.PlayType.CornerStrike:
              index = 1;
              break;
          }
          break;
        case OnboardingFlow.PlayFormation.SingleBackSlot:
          PlaybookState.CurrentFormation.SetValue(Plays.self.singleBackPlays_Slot);
          if (onboardingStep.PlayType == OnboardingFlow.PlayType.SlotUnder)
          {
            index = 13;
            break;
          }
          break;
      }
      if (index != -1)
        MatchManager.instance.playManager.SetOnboardingPlay(index);
      switch (onboardingStep.TutorialScreen)
      {
        case OnboardingFlow.TutorialScreen.SelectPlay:
          UIDispatch.FrontScreen.DisplayView(EScreens.kPlaySelectionFTUE);
          break;
        case OnboardingFlow.TutorialScreen.ThrowBall:
          UIDispatch.FrontScreen.DisplayView(EScreens.kBasicFTUE);
          break;
        case OnboardingFlow.TutorialScreen.Ending:
          UIDispatch.FrontScreen.DisplayView(EScreens.kOnboardingEnding);
          break;
      }
    }

    private void FinishedTutorialAudio()
    {
      if (!MatchManager.Exists())
        return;
      switch (this.OnboardingSteps[this._currentTutorialStep].OnAudioComplete)
      {
        case OnboardingFlow.OnAudioComplete.PlayNext:
          this.PlayNextTutorial();
          break;
        case OnboardingFlow.OnAudioComplete.ShowPlayUI:
          this.ShowPlayConfirmation();
          break;
        case OnboardingFlow.OnAudioComplete.AllowSnap:
          MatchManager.instance.playManager.canUserCallAudible = false;
          this.SetWristPlayCallVisibility(true);
          this.SetPlayConfirmButtonVisibility(false);
          MatchManager.instance.AllowSnap();
          break;
        case OnboardingFlow.OnAudioComplete.Exit:
          UIDispatch.FrontScreen.DisplayView(EScreens.kOnboardingEnding);
          break;
      }
    }

    private void PlayNextTutorial()
    {
      ++this._currentTutorialStep;
      this.DoTutorialStep();
    }

    private void ShowPlayConfirmation() => this.SetWristPlayCallVisibility(true);

    private void HandlePlaySelected()
    {
      if (this._caughtBall)
      {
        Debug.Log((object) nameof (HandlePlaySelected));
        AudioController.StopCategory("Tutorial");
        UIDispatch.FrontScreen.HideView(EScreens.kPlaySelectionFTUE);
        this._caughtBall = false;
        this.ResetBallPosition();
        SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
        this.StartCoroutine(this.WaitForFeedback());
      }
      MatchManager.instance.playManager.canUserCallAudible = true;
    }

    private IEnumerator WaitForFeedback()
    {
      yield return (object) new WaitForSeconds(1f);
      ++this._currentTutorialStep;
      this.DoTutorialStep();
    }

    private void HandleBallCaught(bool interception)
    {
      if (interception)
        return;
      AudioController.StopCategory("Tutorial");
      AppSounds.PlayTutorialFeedback(ETutorialType.kPlaySuccess);
      this._caughtBall = true;
    }

    private void HandlePlayOver()
    {
      this.StartCoroutine(this.WaitToReset());
      if (this._caughtBall)
      {
        ++this._currentTutorialStep;
        this.DoTutorialStep();
      }
      else
      {
        AppSounds.PlayTutorialFeedback(ETutorialType.kPlayFail);
        this._resetPlay = true;
        MatchManager.instance.playManager.ShouldOffenseHurryUp = true;
      }
    }

    private IEnumerator WaitToReset()
    {
      MatchManager.instance.playManager.quickCleanUp = true;
      MatchManager.instance.playManager.autoSelectPlayForAI = false;
      yield return (object) new WaitForSeconds(3f);
      ProEra.Game.MatchState.Down.Value = 1;
      ProEra.Game.MatchState.Turnover.Value = false;
      ScoreClockState.SetYardLine.Trigger();
      MatchManager.instance.timeManager.SetRunPlayClock(false);
      this.ResetBallPosition();
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
      if (this._resetPlay)
        this._resetPlay = false;
    }

    private void ResetBallPosition()
    {
      float locationByYardline = Field.GetFieldLocationByYardline(75, true);
      Vector3 p = new Vector3(0.0f, Ball.BALL_ON_GROUND_HEIGHT, locationByYardline);
      MatchManager.instance.SetBallOn(locationByYardline);
      MatchManager.instance.SetBallHashPosition(p.x);
      SingletonBehaviour<BallManager, MonoBehaviour>.instance.SetVelocity(Vector3.zero);
      SingletonBehaviour<BallManager, MonoBehaviour>.instance.SetAngularVelocity(Vector3.zero);
      SingletonBehaviour<BallManager, MonoBehaviour>.instance.SetParent((Transform) null);
      SingletonBehaviour<BallManager, MonoBehaviour>.instance.SetPosition(p);
    }

    private new void HandleGameEvent(PEGameplayEvent e)
    {
      switch (e)
      {
        case PEPlaySelectedEvent _:
          this.HandlePlaySelected();
          break;
        case PEBallCaughtEvent peBallCaughtEvent:
          this.HandleBallCaught(peBallCaughtEvent.Interception);
          break;
        case PEPlayOverEvent _:
          this.HandlePlayOver();
          break;
        case PEBallHikedEvent _:
          this.HandleBallHiked();
          break;
        case PEBallHandoffEvent _:
          this.HandleBallCaught(false);
          break;
      }
    }

    private void HandleBallHiked()
    {
      AudioController.StopCategory("Tutorial");
      UIDispatch.FrontScreen.HideView(EScreens.kBasicFTUE);
    }

    private void TurnOnLocomotion() => VRState.LocomotionEnabled.SetValue(true);

    private void HandleQBPositionChange() => this.ResetBallPosition();

    public void Restart()
    {
      this._currentTutorialStep = 0;
      this.DoTutorialStep();
    }

    public void StartTutorial() => this.StartCoroutine(this.WaitToStartTutorial());

    private IEnumerator WaitToStartTutorial()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      OnboardingFlow onboardingFlow = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        onboardingFlow.Restart();
        onboardingFlow.MovePlayerCamera();
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) null;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public enum TutorialActivation
    {
      None,
      SelectPlay,
      CompletedPass,
    }

    public enum TutorialScreen
    {
      None,
      SelectPlay,
      ThrowBall,
      Ending,
    }

    public enum OnAudioComplete
    {
      None,
      PlayNext,
      ShowPlayUI,
      AllowSnap,
      Exit,
    }

    public enum PlayFormation
    {
      None,
      ShotgunNormal,
      SingleBackBig,
      SingleBackSlot,
    }

    public enum PlayType
    {
      None,
      TEDrag,
      TEOut,
      X,
      HBDiveWeak,
      SlotUnder,
      CornerStrike,
    }

    [Serializable]
    public class OnboardingStep
    {
      public bool PlayAudio;
      public OnboardingFlow.TutorialActivation TutorialActivation;
      public OnboardingFlow.TutorialScreen TutorialScreen;
      public OnboardingFlow.OnAudioComplete OnAudioComplete;
      public OnboardingFlow.PlayFormation PlayFormation;
      public OnboardingFlow.PlayType PlayType;
    }
  }
}
