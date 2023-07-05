// Decompiled with JetBrains decompiler
// Type: TB12.OnboardingScene
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
  public class OnboardingScene : AxisGameScene
  {
    [SerializeField]
    private OnboardingState _state;
    [SerializeField]
    private BallObject _ballPrefab;
    [SerializeField]
    private OnboardingFlow _practiceModeFlow;
    [SerializeField]
    private Transform[] _speakers;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      this._state.FinishedEnable += new Action(this.StartAudio);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        EventHandle.Link<bool>(AppSounds.UpdateInstrumental, new Action<bool>(this.OnInstrumentalChanged))
      });
    }

    private void OnDestroy()
    {
      this._state.FinishedEnable -= new Action(this.StartAudio);
      this._linksHandler.Clear();
      AppSounds.StopSfx(ESfxTypes.kOnboardingLoop);
    }

    private void StartAudio()
    {
      Debug.Log((object) "OnboardingScene: StartAudio");
      AppSounds.StopMusic.Trigger();
      AppSounds.PlaySfx(ESfxTypes.kOnboardingLoop);
      AppSounds.PlayMusicPlaylist((bool) ScriptableSingleton<SettingsStore>.Instance.InstrumentalMusic ? EMusicTypes.kPracticeInst : EMusicTypes.kPracticeLyrics);
    }

    private void OnInstrumentalChanged(bool inst)
    {
      AppSounds.StopMusic.Trigger();
      AppSounds.PlayMusicPlaylist(EMusicTypes.kPracticeInst);
    }
  }
}
