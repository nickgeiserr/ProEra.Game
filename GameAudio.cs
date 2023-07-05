// Decompiled with JetBrains decompiler
// Type: GameAudio
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Threading.Tasks;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
  private string rootFolder = "gameaudio/";
  public AudioPath ballCaught;
  public AudioPath ballHitGround;
  public AudioPath ballKicked;
  public AudioPath block;
  public AudioPath runWithBall;
  public AudioPath tackle;
  public AudioPath whistle;

  public void LoadAudioPaths()
  {
    this.ballCaught = new AudioPath(this.rootFolder + "BALL_CAUGHT", 3, true);
    this.ballHitGround = new AudioPath(this.rootFolder + "BALL_HITGROUND", 1, true);
    this.ballKicked = new AudioPath(this.rootFolder + "BALL_KICKED", 2, true);
    this.block = new AudioPath(this.rootFolder + "BLOCK", 9, true);
    this.runWithBall = new AudioPath(this.rootFolder + "RUN_WITH_BALL", 1, true);
    this.tackle = new AudioPath(this.rootFolder + "TACKLE", 10, true);
    this.whistle = new AudioPath(this.rootFolder + "WHISTLE", 1, true);
  }

  public async Task<AudioClip> GetBallCaughtSound()
  {
    AudioPath ballCaught = this.ballCaught;
    return AudioManager.self.GetAudioClip(ballCaught, Random.Range(0, ballCaught.count) + 1);
  }

  public async Task<AudioClip> GetBallHitGroundSound()
  {
    AudioPath ballHitGround = this.ballHitGround;
    return AudioManager.self.GetAudioClip(ballHitGround, Random.Range(0, ballHitGround.count) + 1);
  }

  public async Task<AudioClip> GetBallKickedSound()
  {
    AudioPath ballKicked = this.ballKicked;
    return AudioManager.self.GetAudioClip(ballKicked, Random.Range(0, ballKicked.count) + 1);
  }

  public async Task<AudioClip> GetBlockSound()
  {
    AudioPath block = this.block;
    return AudioManager.self.GetAudioClip(block, Random.Range(0, block.count) + 1);
  }

  public async Task<AudioClip> GetRunWithBallSound()
  {
    AudioPath runWithBall = this.runWithBall;
    return AudioManager.self.GetAudioClip(runWithBall, Random.Range(0, runWithBall.count) + 1);
  }

  public async Task<AudioClip> GetTackleSound()
  {
    AudioPath tackle = this.tackle;
    return AudioManager.self.GetAudioClip(tackle, Random.Range(0, tackle.count) + 1);
  }

  public async Task<AudioClip> GetTackleSound(int index)
  {
    AudioPath tackle = this.tackle;
    return AudioManager.self.GetAudioClip(tackle, index);
  }

  public async Task<AudioClip> GetWhistleSound()
  {
    AudioPath whistle = this.whistle;
    return AudioManager.self.GetAudioClip(whistle, Random.Range(0, whistle.count) + 1);
  }
}
