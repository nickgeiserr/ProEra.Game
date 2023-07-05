// Decompiled with JetBrains decompiler
// Type: AudioFadeScript
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class AudioFadeScript : MonoBehaviour
{
  private int fadeDir;
  public AudioSource aud;
  public float maxVol;

  private void Start() => this.FadeIn();

  private void Update()
  {
    if (this.fadeDir == 1)
    {
      this.aud.volume += 0.004f;
      if ((double) this.aud.volume <= (double) this.maxVol)
        return;
      this.fadeDir = 0;
    }
    else
    {
      if (this.fadeDir != -1)
        return;
      this.aud.volume -= 0.004f;
      if ((double) this.aud.volume >= 0.0)
        return;
      this.fadeDir = 0;
    }
  }

  public void FadeIn() => this.fadeDir = 1;

  public void FadeOut() => this.fadeDir = -1;
}
