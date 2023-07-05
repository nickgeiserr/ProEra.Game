// Decompiled with JetBrains decompiler
// Type: BenchmarkSummaryText
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using net.krej.AutoQualityChooser;
using net.krej.FPSCounter;
using UnityEngine;
using UnityEngine.UI;

public class BenchmarkSummaryText : MonoBehaviour
{
  public ParticleSystem particles;
  private Text text;
  private string currentLine;
  private string oldLines;
  private int maxParticles;

  private void Start() => this.text = this.GetComponent<Text>();

  private void Update()
  {
    this.maxParticles = Mathf.Max(this.maxParticles, this.particles.particleCount);
    this.currentLine = string.Format("Quality: {0} - {1} particles, {2}FPS \n", (object) QualityChanger.GetCurrentQualityName(), (object) this.maxParticles, (object) net.krej.Singleton.Singleton<FramerateCounter>.Instance.currentFrameRate.ToString("0.0"));
    this.text.text = this.oldLines + this.currentLine;
  }

  public void GoToNextLine() => this.oldLines += this.currentLine;
}
