// Decompiled with JetBrains decompiler
// Type: TB12.AppStates.PlayState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DSE;
using FootballVR;
using FootballWorld;
using Framework;
using System;
using System.Collections;
using TB12.RuntimeSystem;
using TB12.UI;
using UnityEngine;

namespace TB12.AppStates
{
  [CreateAssetMenu(menuName = "TB12/States/PlayState")]
  public class PlayState : GameState
  {
    [SerializeField]
    private LocalPlayLoader _localPlayLoader;
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PlayModeStore _playModeStore;
    [SerializeField]
    private PlayRuntimeData _playData;
    [SerializeField]
    private HandsDataModel _handsDataModel;
    private readonly RoutineHandle _loadPlayRoutine = new RoutineHandle();
    private readonly RoutineHandle _ballFlightRoutine = new RoutineHandle();

    public bool skipIdentification { get; set; }

    public override bool clearFadeOnEntry => false;

    public override EAppState Id => EAppState.kPlay;

    public event System.Action OnGameplayStopped;

    public event System.Action OnGameplayStarted;

    protected override void OnEnterState()
    {
      base.OnEnterState();
      PlayRuntimeData.OverrideUniforms = true;
      this._playModeStore.ResetYardLine();
      this._playData.Initialize();
      WorldState.CrowdEnabled.SetValue(true);
      this._throwManager.ForceAutoAim = true;
      this._throwManager.OnBallThrown += new Action<BallObject, Vector3>(this.HandleBallThrown);
      AppEvents.Retry.OnTrigger += new System.Action(this.RetryHandler);
      AppEvents.Continue.OnTrigger += new System.Action(this.NextHandler);
      this._localPlayLoader.CurrentPlayIndex = 0;
      this._loadPlayRoutine.Run(this.LoadPlayRoutine(false));
      this._handsDataModel.playerRole = EPlayerRole.kQuarterBack;
    }

    public override void WillExit()
    {
      System.Action onGameplayStopped = this.OnGameplayStopped;
      if (onGameplayStopped == null)
        return;
      onGameplayStopped();
    }

    protected override void OnExitState()
    {
      UIDispatch.HideAll();
      this._throwManager.Clear();
      this._throwManager.ForceAutoAim = false;
      this._playData.Deinitialize();
      this.skipIdentification = false;
      WorldState.CrowdEnabled.SetValue(false);
      VRState.LocomotionEnabled.SetValue(false);
      AppEvents.Retry.OnTrigger -= new System.Action(this.RetryHandler);
      AppEvents.Continue.OnTrigger -= new System.Action(this.NextHandler);
      this._ballFlightRoutine.Stop();
    }

    private IEnumerator LoadPlayRoutine(bool fade = true)
    {
      PlayState playState = this;
      UIDispatch.HideAll();
      if (fade)
        yield return (object) GamePlayerController.CameraFade.Fade();
      if (playState._playModeStore.Touchdown)
      {
        playState._playModeStore.ResetYardLine();
        playState._localPlayLoader.CurrentPlayIndex = 0;
      }
      playState._localPlayLoader.LoadCurrentPlay(playState._playModeStore.yardLine);
      // ISSUE: reference to a compiler-generated method
      yield return (object) new WaitUntil(new Func<bool>(playState.\u003CLoadPlayRoutine\u003Eb__24_0));
      System.Action onGameplayStarted = playState.OnGameplayStarted;
      if (onGameplayStarted != null)
        onGameplayStarted();
      yield return (object) GamePlayerController.CameraFade.Clear();
    }

    private void RetryHandler()
    {
      System.Action onGameplayStopped = this.OnGameplayStopped;
      if (onGameplayStopped != null)
        onGameplayStopped();
      this._loadPlayRoutine.Run(this.LoadPlayRoutine());
    }

    private void NextHandler()
    {
      this.skipIdentification = false;
      ++this._localPlayLoader.CurrentPlayIndex;
      this._loadPlayRoutine.Run(this.LoadPlayRoutine());
    }

    private void HandleBallThrown(BallObject ball, Vector3 throwVector)
    {
      this._playModeStore.speed = throwVector.magnitude * 2.23694f;
      this._playModeStore.distance = 0.0f;
    }

    public override void ClearState()
    {
      this._playData.Deinitialize();
      this._throwManager.Clear();
      this._playData.Deinitialize();
      this.skipIdentification = false;
    }
  }
}
