// Decompiled with JetBrains decompiler
// Type: TestPlayerScenarioRunner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using UnityEngine;

public class TestPlayerScenarioRunner : MonoBehaviour
{
  public PlayerScenario scenario;
  public BehaviourController player;
  public TestScenarioDrawer drawer;
  public PlaybackInfo playbackInfo;
  public float startTime = -5f;

  private void Start() => this.RunScenario();

  private void RunScenario()
  {
    this.scenario.Initialize();
    this.drawer.scenario = this.scenario;
    this.player.Scenario = this.scenario;
    this.player.playbackInfo = (IPlaybackInfo) this.playbackInfo;
    this.playbackInfo.Setup(this.startTime, this.scenario.time + 5f);
    this.playbackInfo.StartPlayback();
  }
}
