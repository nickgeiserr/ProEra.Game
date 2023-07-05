// Decompiled with JetBrains decompiler
// Type: UDB.CodeProfiler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class CodeProfiler : SingletonBehaviour<CodeProfiler, MonoBehaviour>
  {
    private static Dictionary<string, ProfilerRecording> recordings = new Dictionary<string, ProfilerRecording>();
    private float startTime;
    private float nextOutputTime = 5f;
    private int numFrames;
    private string displayText;
    private Rect displayRect = new Rect(10f, 10f, 460f, 300f);

    private new void Awake()
    {
      this.startTime = Time.time;
      this.displayText = "\n\nTaking initial readings...";
    }

    private void Update()
    {
      ++this.numFrames;
      if ((double) Time.time <= (double) this.nextOutputTime)
        return;
      int totalWidth = 10;
      this.displayText = "\n\n";
      float num1 = (float) (((double) Time.time - (double) this.startTime) * 1000.0);
      float num2 = num1 / (float) this.numFrames;
      float num3 = (float) (1000.0 / ((double) num1 / (double) this.numFrames));
      this.displayText += "Avg frame time: ";
      this.displayText = this.displayText + num2.ToString("0.#") + "ms, ";
      this.displayText = this.displayText + num3.ToString("0.#") + " fps \n";
      this.displayText += "Total".PadRight(totalWidth);
      this.displayText += "MS/frame".PadRight(totalWidth);
      this.displayText += "Calls/fra".PadRight(totalWidth);
      this.displayText += "MS/call".PadRight(totalWidth);
      this.displayText += "Label";
      this.displayText += "\n";
      foreach (KeyValuePair<string, ProfilerRecording> recording in CodeProfiler.recordings)
      {
        ProfilerRecording profilerRecording = recording.Value;
        double num4 = (double) profilerRecording.seconds * 1000.0;
        float num5 = (float) (num4 * 100.0) / num1;
        float num6 = (float) num4 / (float) this.numFrames;
        float num7 = (float) num4 / (float) profilerRecording.count;
        float num8 = (float) profilerRecording.count / (float) this.numFrames;
        this.displayText += (num5.ToString("0.000") + "%").PadRight(totalWidth);
        this.displayText += (num6.ToString("0.000") + "ms").PadRight(totalWidth);
        this.displayText += num8.ToString("0.000").PadRight(totalWidth);
        this.displayText += (num7.ToString("0.0000") + "ms").PadRight(totalWidth);
        this.displayText += profilerRecording.id;
        this.displayText += "\n";
        profilerRecording.Reset();
      }
      if (DebugManager.StateForKey("Code Profiler Message"))
        Debug.Log((object) ("Code Profiler: " + this.displayText));
      this.numFrames = 0;
      this.startTime = Time.time;
      this.nextOutputTime = Time.time + 5f;
    }

    private void OnGUI()
    {
      GUI.Box(this.displayRect, "Code Profiler");
      GUI.Label(this.displayRect, this.displayText);
    }

    public static void Begin(string id)
    {
      if (!CodeProfiler.recordings.ContainsKey(id))
        CodeProfiler.recordings[id] = new ProfilerRecording(id);
      CodeProfiler.recordings[id].Start();
    }

    public static void End(string id) => CodeProfiler.recordings[id].Stop();
  }
}
