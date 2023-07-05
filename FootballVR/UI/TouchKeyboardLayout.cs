// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchKeyboardLayout
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.FootballVR.UI.KeyboardUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR.UI
{
  public class TouchKeyboardLayout : MonoBehaviour
  {
    [SerializeField]
    private TouchKeyButton _buttonPrefab;
    [SerializeField]
    private List<TouchKeyButton> _buttons = new List<TouchKeyButton>();
    [SerializeField]
    private float _rowDeltaHeight;
    [SerializeField]
    private float[] _xRotation;
    [SerializeField]
    private float[] _zOffsets;
    [SerializeField]
    private float[] _itemAngleOffset;
    [SerializeField]
    private float _radius = 1f;
    [SerializeField]
    private float _forwardCoefficient = 0.1f;
    [SerializeField]
    private float _forwardOffset = 0.1f;
    [SerializeField]
    private bool _editMode;
    [SerializeField]
    private bool _numpadOnly;
    private readonly string[][] keys = new string[4][]
    {
      new string[10]
      {
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "0"
      },
      new string[10]
      {
        "Q",
        "W",
        "E",
        "R",
        "T",
        "Y",
        "U",
        "I",
        "O",
        "P"
      },
      new string[9]{ "A", "S", "D", "F", "G", "H", "J", "K", "L" },
      new string[9]{ "@", "Z", "X", "C", "V", "B", "N", "M", "." }
    };
    private readonly string[][] numpadKeys = new string[4][]
    {
      new string[3]{ "1", "2", "3" },
      new string[3]{ "4", "5", "6" },
      new string[3]{ "7", "8", "9" },
      new string[3]{ "Clear", "0", "OK" }
    };

    private void OnValidate()
    {
      if (!this._editMode)
        return;
      this.BuildButtons();
    }

    [ContextMenu("Build")]
    public void BuildButtons()
    {
      string[][] strArray1 = this._numpadOnly ? this.numpadKeys : this.keys;
      float num1 = Mathf.Sqrt(Mathf.Abs(this._forwardCoefficient));
      float num2 = this._forwardCoefficient * this._radius;
      int num3 = 0;
      float num4 = (float) ((double) this._rowDeltaHeight * (double) (strArray1.Length - 1) / 2.0);
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1];
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
        {
          TouchKeyButton button = this._buttons[num3++];
          float num5 = this._itemAngleOffset[index1] * ((float) index2 - (float) (strArray2.Length - 1) / 2f);
          float num6 = Mathf.Sin((float) (Math.PI / 180.0 * (90.0 - (double) num5)));
          float num7 = Mathf.Cos((float) (Math.PI / 180.0 * (90.0 - (double) num5))) * (2f - num1);
          Vector3 vector3 = new Vector3()
          {
            x = num7 * this._radius,
            y = num4,
            z = this._forwardOffset - num6 * this._radius * this._forwardCoefficient + this._zOffsets[index1] + num2
          };
          Quaternion quaternion = Quaternion.Euler(new Vector3(this._xRotation[index1], -num5 * this._forwardCoefficient, 0.0f));
          button.transform.localPosition = vector3;
          button.transform.localRotation = quaternion;
          button.ButtonText = strArray2[index2];
        }
        num4 -= this._rowDeltaHeight;
      }
    }

    public void ReinitButtons()
    {
      foreach (TouchKeyButton button in this._buttons)
      {
        if ((UnityEngine.Object) button != (UnityEngine.Object) null && (UnityEngine.Object) button.gameObject != (UnityEngine.Object) null)
          Objects.SafeDestroy((UnityEngine.Object) button.gameObject);
      }
      this._buttons.Clear();
      int num = this._numpadOnly ? 12 : 36;
      for (int index = 0; index < num; ++index)
        this._buttons.Add(UnityEngine.Object.Instantiate<TouchKeyButton>(this._buttonPrefab, this.transform));
      this.BuildButtons();
    }
  }
}
