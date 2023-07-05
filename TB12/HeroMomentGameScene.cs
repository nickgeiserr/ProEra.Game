// Decompiled with JetBrains decompiler
// Type: TB12.HeroMomentGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using System;
using TB12.AppStates;
using UnityEngine;

namespace TB12
{
  public class HeroMomentGameScene : AxisGameScene
  {
    [SerializeField]
    private HeroMomentGameState _state;

    private void Awake() => this._state.FinishedEnable += new Action(this.StartAudio);

    private void OnDestroy() => this._state.FinishedEnable -= new Action(this.StartAudio);

    private void StartAudio()
    {
      AppSounds.CrowdSound.SetValue(true);
      WorldState.CrowdEnabled.SetValue(true);
      AppSounds.AnnouncerSound.SetValue(true);
      AppSounds.PlayerChatterSound.ForceValue(true);
    }
  }
}
