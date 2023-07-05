// Decompiled with JetBrains decompiler
// Type: UDB.FPS
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace UDB
{
  public class FPS : SingletonBehaviour<FPS, MonoBehaviour>
  {
    public bool developmentBuildsOnly;
    public int frameRange = 60;
    public Text highestFPSLabel;
    public Text averageFPSLabel;
    public Text lowestFPSLabel;
    private int[] fpsBuffer;
    private int fpsBufferIndex;
    [SerializeField]
    private FPS.FPSColor[] coloring;
    private static string[] stringsFrom00To99 = new string[100]
    {
      "00",
      "01",
      "02",
      "03",
      "04",
      "05",
      "06",
      "07",
      "08",
      "09",
      "10",
      "11",
      "12",
      "13",
      "14",
      "15",
      "16",
      "17",
      "18",
      "19",
      "20",
      "21",
      "22",
      "23",
      "24",
      "25",
      "26",
      "27",
      "28",
      "29",
      "30",
      "31",
      "32",
      "33",
      "34",
      "35",
      "36",
      "37",
      "38",
      "39",
      "40",
      "41",
      "42",
      "43",
      "44",
      "45",
      "46",
      "47",
      "48",
      "49",
      "50",
      "51",
      "52",
      "53",
      "54",
      "55",
      "56",
      "57",
      "58",
      "59",
      "60",
      "61",
      "62",
      "63",
      "64",
      "65",
      "66",
      "67",
      "68",
      "69",
      "70",
      "71",
      "72",
      "73",
      "74",
      "75",
      "76",
      "77",
      "78",
      "79",
      "80",
      "81",
      "82",
      "83",
      "84",
      "85",
      "86",
      "87",
      "88",
      "89",
      "90",
      "91",
      "92",
      "93",
      "94",
      "95",
      "96",
      "97",
      "98",
      "99"
    };

    public int averageFPS { get; private set; }

    public int highestFPS { get; private set; }

    public int lowestFPS { get; private set; }

    private new void Awake()
    {
      if (!this.developmentBuildsOnly)
        return;
      this.gameObject.SetActive(Debug.isDebugBuild);
    }

    private void Update()
    {
      if (this.fpsBuffer == null || this.fpsBuffer.Length != this.frameRange)
        this.InitializeBuffer();
      this.UpdateBuffer();
      this.CalculateFPS();
      this.Display(this.highestFPSLabel, this.highestFPS);
      this.Display(this.averageFPSLabel, this.averageFPS);
      this.Display(this.lowestFPSLabel, this.lowestFPS);
    }

    private void Display(Text label, int fps)
    {
      label.text = FPS.stringsFrom00To99[Mathf.Clamp(fps, 0, 99)];
      for (int index = 0; index < this.coloring.Length; ++index)
      {
        if (fps >= this.coloring[index].minimumFPS)
        {
          label.color = this.coloring[index].color;
          break;
        }
      }
    }

    private void InitializeBuffer()
    {
      if (this.frameRange <= 0)
        this.frameRange = 1;
      this.fpsBuffer = new int[this.frameRange];
      this.fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
      this.fpsBuffer[this.fpsBufferIndex++] = (int) (1.0 / (double) Time.unscaledDeltaTime);
      if (this.fpsBufferIndex < this.frameRange)
        return;
      this.fpsBufferIndex = 0;
    }

    private void CalculateFPS()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = int.MaxValue;
      for (int index = 0; index < this.frameRange; ++index)
      {
        int num4 = this.fpsBuffer[index];
        num1 += num4;
        if (num4 > num2)
          num2 = num4;
        if (num4 < num3)
          num3 = num4;
      }
      this.averageFPS = (int) ((double) num1 / (double) this.frameRange);
      this.highestFPS = num2;
      this.lowestFPS = num3;
    }

    [Serializable]
    private struct FPSColor
    {
      public Color color;
      public int minimumFPS;
    }
  }
}
