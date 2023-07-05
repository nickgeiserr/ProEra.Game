// Decompiled with JetBrains decompiler
// Type: net.krej.FPSCounter.FramerateCounter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using net.krej.AutoQualityChooser;
using UnityEngine;
using UnityEngine.Events;

namespace net.krej.FPSCounter
{
  public class FramerateCounter : net.krej.Singleton.Singleton<FramerateCounter>
  {
    public float currentFrameRate;
    public bool showFpsInGame;
    public UnityEvent onFramerateCalculated = new UnityEvent();
    private float updateRate = 1f;
    private float accum;
    private int frames;
    private float timeleft;
    private const int FPS_BOX_WIDTH = 128;
    private const int FPS_BOX_HEIGHT = 32;

    private void Update()
    {
      this.timeleft -= Time.deltaTime;
      this.accum += Time.timeScale / Time.deltaTime;
      ++this.frames;
      if ((double) this.timeleft > 0.0)
        return;
      this.StartNewInterval();
    }

    private void StartNewInterval()
    {
      this.currentFrameRate = this.accum / (float) this.frames;
      this.ResetTimeLeft();
      this.accum = 0.0f;
      this.frames = 0;
      this.onFramerateCalculated.Invoke();
    }

    public void ResetTimeLeft() => this.timeleft = 1f / this.updateRate;

    private void OnGUI()
    {
      if (!this.showFpsInGame)
        return;
      this.ShowFpsInCorner();
    }

    private void ShowFpsInCorner()
    {
      GUIStyle style = new GUIStyle(GUI.skin.box)
      {
        fontSize = 10,
        alignment = TextAnchor.MiddleCenter,
        richText = true
      };
      GUI.Box(new Rect(0.0f, 0.0f, 128f, 32f), string.Format("<color=white><size=12><B>Auto Quality Chooser</B></size>\n{1}FPS ({0})</color>", (object) QualityChanger.GetCurrentQualityName(), (object) this.currentFrameRate.ToString("0")), style);
    }
  }
}
