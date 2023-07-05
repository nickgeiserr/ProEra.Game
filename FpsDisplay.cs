// Decompiled with JetBrains decompiler
// Type: FpsDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using System;
using TMPro;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
  [SerializeField]
  private Canvas _canvas;
  [SerializeField]
  private TextMeshProUGUI _text;
  [Tooltip("How many frames should we consider into our average calculation?")]
  [SerializeField]
  private int frameRange = 60;
  private int averageFps;
  private int[] fpsBuffer;
  private int fpsBufferIndex;
  private static readonly string[] StringsFrom00To99 = new string[100]
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

  protected void Awake()
  {
    this.SetState((bool) ScriptableSingleton<GameGraphicsSettings>.Instance.ShowFpsCounter);
    ScriptableSingleton<GameGraphicsSettings>.Instance.ShowFpsCounter.OnValueChanged += new Action<bool>(this.SetState);
    this._canvas.worldCamera = Camera.main;
  }

  private void OnDestroy() => ScriptableSingleton<GameGraphicsSettings>.Instance.ShowFpsCounter.OnValueChanged -= new Action<bool>(this.SetState);

  private void Update()
  {
    if (this.fpsBuffer == null || this.fpsBuffer.Length != this.frameRange || (UnityEngine.Object) this._text == (UnityEngine.Object) null)
      this.InitBuffer();
    this.UpdateFrameBuffer();
    this.CalculateFps();
    this.UpdateTextDisplay(this.averageFps);
  }

  private void InitBuffer()
  {
    if ((UnityEngine.Object) this._text == (UnityEngine.Object) null)
      this._text = this.GetComponent<TextMeshProUGUI>();
    if (this.frameRange <= 0)
      this.frameRange = 1;
    this.fpsBuffer = new int[this.frameRange];
    this.fpsBufferIndex = 0;
  }

  private void UpdateTextDisplay(int fps)
  {
    string str = FpsDisplay.StringsFrom00To99[Mathf.Clamp(fps, 0, 99)];
    if (!((UnityEngine.Object) this._text != (UnityEngine.Object) null))
      return;
    this._text.text = str;
  }

  private void UpdateFrameBuffer()
  {
    this.fpsBuffer[this.fpsBufferIndex++] = (int) (1.0 / (double) Time.unscaledDeltaTime);
    if (this.fpsBufferIndex < this.frameRange)
      return;
    this.fpsBufferIndex = 0;
  }

  private void CalculateFps()
  {
    int num1 = 0;
    for (int index = 0; index < this.frameRange; ++index)
    {
      int num2 = this.fpsBuffer[index];
      num1 += num2;
    }
    this.averageFps = num1 / this.frameRange;
  }

  private void SetState(bool state) => this.gameObject.SetActive(state);
}
