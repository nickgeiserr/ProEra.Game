// Decompiled with JetBrains decompiler
// Type: TB12.ReceiverGameState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DSE;
using FootballVR;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using TB12.AppStates;
using TB12.RuntimeSystem;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/States/ReceiverGameState", fileName = "ReceiverGameState")]
  public class ReceiverGameState : GameState
  {
    [SerializeField]
    private LocalPlayLoader _localPlayLoader;
    [SerializeField]
    private PlayRuntimeData _playData;
    [SerializeField]
    private HandsDataModel _model;
    private readonly RoutineHandle _loadPlayRoutine = new RoutineHandle();

    public override EAppState Id => EAppState.kReceiverGame;

    public override bool clearFadeOnEntry => false;

    public event System.Action OnGameplayStarted;

    public event System.Action OnExit;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      this._playData.Initialize();
      this._model.playerRole = EPlayerRole.kWideReceiver;
      AppEvents.Retry.OnTrigger += new System.Action(this.RetryHandler);
      AppEvents.Continue.OnTrigger += new System.Action(this.RetryHandler);
      WorldState.CrowdEnabled.SetValue(true);
      VRState.HelmetEnabled.SetValue(true);
      this._loadPlayRoutine.Run(this.LoadPlayRoutine(false));
    }

    protected override void OnExitState()
    {
      UIDispatch.HideAll();
      this._playData.Deinitialize();
      this._model.playerRole = EPlayerRole.kQuarterBack;
      AppEvents.Retry.OnTrigger -= new System.Action(this.RetryHandler);
      AppEvents.Continue.OnTrigger -= new System.Action(this.RetryHandler);
      WorldState.CrowdEnabled.SetValue(false);
      VRState.HelmetEnabled.SetValue(false);
      System.Action onExit = this.OnExit;
      if (onExit == null)
        return;
      onExit();
    }

    private void RetryHandler() => this._loadPlayRoutine.Run(this.LoadPlayRoutine());

    private IEnumerator LoadPlayRoutine(bool fade = true)
    {
      ReceiverGameState receiverGameState = this;
      UIDispatch.HideAll();
      if (fade)
        yield return (object) GamePlayerController.CameraFade.Fade();
      receiverGameState._localPlayLoader.LoadReceiverPlay();
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(receiverGameState.\u003CLoadPlayRoutine\u003Eb__17_0));
      System.Action onGameplayStarted = receiverGameState.OnGameplayStarted;
      if (onGameplayStarted != null)
        onGameplayStarted();
      GamePlayerController.CameraFade.Clear();
    }
  }
}
