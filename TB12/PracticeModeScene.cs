// Decompiled with JetBrains decompiler
// Type: TB12.PracticeModeScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using TB12.AppStates;
using UnityEngine;

namespace TB12
{
  public class PracticeModeScene : AxisGameScene
  {
    [SerializeField]
    private PracticeModeState _state;
    [SerializeField]
    private BallObject _ballPrefab;
    [SerializeField]
    private PracticeModeFlow _practiceModeFlow;
    [SerializeField]
    private Transform[] _speakers;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private ReceiverModeSettings _settings => ScriptableSingleton<ReceiverModeSettings>.Instance;

    public BallObject GameBall { get; private set; }

    private void Awake()
    {
      this.GameBall = UnityEngine.Object.Instantiate<BallObject>(this._ballPrefab);
      ScriptableSingleton<Gameboard>.Instance.football = this.GameBall.gameObject;
      this.GameBall.gameObject.SetActive(false);
      this._state.FinishedEnable += new System.Action(this.StartAudio);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<bool>(AppSounds.UpdateInstrumental, new Action<bool>(this.OnInstrumentalChanged))
      });
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) this.GameBall != (UnityEngine.Object) null && (UnityEngine.Object) this.GameBall.gameObject != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.GameBall.gameObject);
      this._linksHandler.Clear();
      this._state.FinishedEnable -= new System.Action(this.StartAudio);
      AppSounds.StopMusic.Trigger();
    }

    public void Cleanup() => this.GameBall.gameObject.SetActive(false);

    private void StartAudio()
    {
      AppSounds.StopMusic.Trigger();
      AppSounds.PlayMusicPlaylist((bool) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic ? EMusicTypes.kPracticeInst : EMusicTypes.kPracticeLyrics);
    }

    private void OnInstrumentalChanged(bool inst)
    {
      AppSounds.StopMusic.Trigger();
      AppSounds.PlayMusicPlaylist(inst ? EMusicTypes.kPracticeInst : EMusicTypes.kPracticeLyrics);
    }
  }
}
