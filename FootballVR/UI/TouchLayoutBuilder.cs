// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchLayoutBuilder
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR.UI
{
  public class TouchLayoutBuilder : MonoBehaviour
  {
    [SerializeField]
    private TouchLayoutButton _buttonPrefab;
    [SerializeField]
    private List<TouchLayoutButton> _cachedButtons;
    [SerializeField]
    private float _rowDeltaHeight;
    [SerializeField]
    private float[] _xRotation;
    [SerializeField]
    private float[] _zOffsets;
    [SerializeField]
    private float _itemAngleOffset = 20f;
    [SerializeField]
    private float _radius = 1f;
    [SerializeField]
    private float _forwardCoefficient = 0.1f;
    [SerializeField]
    private float _forwardOffset = 0.1f;
    [SerializeField]
    private bool _editMode;
    [SerializeField]
    [Range(1f, 10f)]
    private int _elementsInRow = 1;
    [SerializeField]
    public string[] keys = new string[8]
    {
      "US",
      "US-WEST\r\n 130ms",
      "EU",
      "RU",
      "AU",
      "FR",
      "China",
      "Tokio"
    };
    [SerializeField]
    [Header("Used if buttonPrefab is toggle")]
    private TouchToggleGroup _toggleGroup;
    private ManagedList<TouchLayoutButton> _buttons;

    public IReadOnlyCollection<TouchLayoutButton> Elements => (IReadOnlyCollection<TouchLayoutButton>) this._buttons.Values;

    public event Action<string> OnButtonPressed;

    private void OnValidate()
    {
      this.Initialize();
      if (!this._editMode)
        return;
      this.BuildButtons((IList<string>) this.keys);
    }

    public void Awake() => this.Initialize();

    private void Initialize()
    {
      if (this._buttons != null)
        return;
      this._buttons = new ManagedList<TouchLayoutButton>((IObjectPool<TouchLayoutButton>) new MonoBehaviorObjectPool<TouchLayoutButton>(this._buttonPrefab, this.transform), this._cachedButtons);
    }

    public void BuildButtons(IList<string> buttonTexts, IList<string> ids = null)
    {
      this.Initialize();
      if (buttonTexts == null)
      {
        Debug.LogError((object) "buttonTexts should not be null.");
      }
      else
      {
        int count = buttonTexts.Count;
        foreach (TouchLayoutButton button in this._buttons)
        {
          if ((UnityEngine.Object) button != (UnityEngine.Object) null)
            button.OnButtonPress -= this.OnButtonPressed;
        }
        this._buttons.SetCount(count);
        foreach (TouchLayoutButton button in this._buttons)
          button.OnButtonPress += this.OnButtonPressed;
        if (this._buttonPrefab.Button is TouchToggle)
        {
          foreach (TouchLayoutButton button in this._buttons)
            ((TouchToggle) button.Button).SetToggleGroup(this._toggleGroup);
        }
        bool flag = ids != null;
        float num1 = Mathf.Sqrt(Mathf.Abs(this._forwardCoefficient));
        float num2 = this._forwardCoefficient * this._radius;
        int num3 = 0;
        float num4 = (float) ((double) this._rowDeltaHeight * (double) ((buttonTexts.Count - 1) / this._elementsInRow) / 2.0);
        int index1 = 0;
        int num5 = 0;
        for (int index2 = 0; index2 < count; ++index2)
        {
          int num6 = Mathf.Min(count - this._elementsInRow * index1, this._elementsInRow);
          TouchLayoutButton button = this._buttons[num3++];
          float num7 = this._itemAngleOffset * ((float) num5 - (float) (num6 - 1) / 2f);
          float num8 = Mathf.Sin((float) (Math.PI / 180.0 * (90.0 - (double) num7)));
          float num9 = Mathf.Cos((float) (Math.PI / 180.0 * (90.0 - (double) num7))) * (2f - num1);
          Vector3 vector3 = new Vector3()
          {
            x = num9 * this._radius,
            y = num4,
            z = this._forwardOffset - num8 * this._radius * this._forwardCoefficient + this.GetValue<float>((IReadOnlyList<float>) this._zOffsets, index1) + num2
          };
          Quaternion quaternion = Quaternion.Euler(new Vector3(this.GetValue<float>((IReadOnlyList<float>) this._xRotation, index1), -num7 * this._forwardCoefficient, 0.0f));
          button.transform.localPosition = vector3;
          button.transform.localRotation = quaternion;
          string buttonText = buttonTexts[index2];
          if ((UnityEngine.Object) button.ButtonText != (UnityEngine.Object) null)
          {
            button.ButtonText.text = buttonText;
            if (button.ButtonText.movesToParent)
              button.ButtonText.containerTx.SetPositionAndRotation(button.transform.position, button.transform.rotation);
          }
          button.id = flag ? ids[index2] : buttonText;
          ++num5;
          if (num5 % this._elementsInRow == 0)
          {
            num5 = 0;
            ++index1;
            num4 -= this._rowDeltaHeight;
          }
        }
      }
    }

    private T GetValue<T>(IReadOnlyList<T> array, int index)
    {
      int count = array.Count;
      if (index < count)
        return array[index];
      return count != 0 ? array[count - 1] : default (T);
    }

    public void ReinitButtons(int count)
    {
      this._buttons = new ManagedList<TouchLayoutButton>((IObjectPool<TouchLayoutButton>) new MonoBehaviorObjectPool<TouchLayoutButton>(this._buttonPrefab, this.transform), this._cachedButtons);
      this._buttons.SetCount(0, true);
      this._buttons.SetCount(count);
      if (this._buttonPrefab.Button is TouchToggle)
      {
        foreach (TouchLayoutButton button in this._buttons)
          ((TouchToggle) button.Button).SetToggleGroup(this._toggleGroup);
      }
      this.BuildButtons((IList<string>) this.keys);
    }
  }
}
