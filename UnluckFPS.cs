// Decompiled with JetBrains decompiler
// Type: UnluckFPS
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class UnluckFPS : MonoBehaviour
{
  public TextMesh _textMesh;
  public float updateInterval = 0.5f;
  private float accum;
  private int frames;
  private float timeleft;

  public void Start()
  {
    this.timeleft = this.updateInterval;
    this._textMesh = this.transform.GetComponent<TextMesh>();
  }

  public void Update()
  {
    this.timeleft -= Time.deltaTime;
    this.accum += Time.timeScale / Time.deltaTime;
    ++this.frames;
    if ((double) this.timeleft > 0.0)
      return;
    this._textMesh.text = "FPS " + (this.accum / (float) this.frames).ToString("f2");
    this.timeleft = this.updateInterval;
    this.accum = 0.0f;
    this.frames = 0;
  }
}
