// Decompiled with JetBrains decompiler
// Type: AudioSegment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class AudioSegment
{
  private AudioClip audioClip;
  private float delayAfter;
  private bool clearQueueAfterPlay;

  public AudioSegment(AudioClip ac, float delay)
  {
    this.audioClip = ac;
    this.delayAfter = delay;
    this.clearQueueAfterPlay = false;
  }

  public AudioSegment(float delay)
  {
    this.audioClip = (AudioClip) null;
    this.delayAfter = delay;
    this.clearQueueAfterPlay = false;
  }

  public AudioSegment(AudioClip ac)
  {
    this.audioClip = ac;
    this.delayAfter = 0.0f;
    this.clearQueueAfterPlay = false;
  }

  public AudioSegment(float delay, bool clearQueueAfter)
  {
    this.audioClip = (AudioClip) null;
    this.delayAfter = delay;
    this.clearQueueAfterPlay = clearQueueAfter;
  }

  public AudioSegment(AudioClip ac, float delay, bool clearQueueAfter)
  {
    this.audioClip = ac;
    this.delayAfter = delay;
    this.clearQueueAfterPlay = clearQueueAfter;
  }

  public AudioClip GetAudioClip() => this.audioClip;

  public float GetDelayAfter() => this.delayAfter;

  public bool ClearQueueAfterPlay() => this.clearQueueAfterPlay;
}
