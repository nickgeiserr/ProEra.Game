// Decompiled with JetBrains decompiler
// Type: VRKeyboard.Utils.KeyboardManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VRKeyboard.Utils
{
  public class KeyboardManager : MonoBehaviour
  {
    [Header("User defined")]
    [Tooltip("If the character is uppercase at the initialization")]
    public bool isUppercase;
    public int maxInputLength;
    [Header("UI Elements")]
    public TMP_InputField input;
    [Header("Essentials")]
    public Transform characters;
    public TouchUI2DButton confirmButton;
    public TouchUI2DButton cancelButton;
    public Action<string> OnConfirmInputText;
    private Dictionary<GameObject, Text> keysDictionary = new Dictionary<GameObject, Text>();
    private bool capslockFlag;
    private bool shiftFlag;

    private string Input
    {
      get => this.input.text;
      set => this.input.text = value;
    }

    private void Awake()
    {
      for (int index = 0; index < this.characters.childCount; ++index)
      {
        GameObject gameObject = this.characters.GetChild(index).gameObject;
        Text _text = gameObject.GetComponentInChildren<Text>();
        this.keysDictionary.Add(gameObject, _text);
        Button component1 = gameObject.GetComponent<Button>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.onClick.AddListener((UnityAction) (() => this.GenerateInput(_text.text)));
        TouchUI2DButton component2 = gameObject.GetComponent<TouchUI2DButton>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.onClick += (Action) (() => this.GenerateInput(_text.text));
      }
      this.capslockFlag = this.isUppercase;
      this.CapsLock();
      this.cancelButton.onClick += new Action(this.Cancel);
      this.confirmButton.onClick += new Action(this.Confirm);
    }

    public void Backspace()
    {
      if (this.Input.Length <= 0)
        return;
      this.Input = this.Input.Remove(this.Input.Length - 1);
    }

    public void Clear() => this.Input = "";

    public void CapsLock()
    {
      if (this.capslockFlag)
      {
        foreach (KeyValuePair<GameObject, Text> keys in this.keysDictionary)
          keys.Value.text = this.ToUpperCase(keys.Value.text);
      }
      else
      {
        foreach (KeyValuePair<GameObject, Text> keys in this.keysDictionary)
          keys.Value.text = this.ToLowerCase(keys.Value.text);
      }
      this.capslockFlag = !this.capslockFlag;
    }

    private void Cancel()
    {
      this.Clear();
      this.gameObject.SetActive(false);
    }

    private void Confirm()
    {
      Action<string> confirmInputText = this.OnConfirmInputText;
      if (confirmInputText != null)
        confirmInputText(this.input.text);
      this.gameObject.SetActive(false);
    }

    private void Shift()
    {
      if (this.shiftFlag)
      {
        foreach (KeyValuePair<GameObject, Text> keys in this.keysDictionary)
          keys.Value.text = this.ToUpperCase(keys.Value.text);
      }
      else
      {
        foreach (KeyValuePair<GameObject, Text> keys in this.keysDictionary)
          keys.Value.text = this.ToLowerCase(keys.Value.text);
      }
    }

    public void ShiftOff()
    {
      if (!this.shiftFlag)
        return;
      this.shiftFlag = false;
      this.Shift();
    }

    public void ShiftToggle()
    {
      this.shiftFlag = !this.shiftFlag;
      this.Shift();
    }

    public void AddSpace() => this.Input += " ";

    public void GenerateInput(string s)
    {
      if (this.Input.Length > this.maxInputLength)
        return;
      this.Input += s;
    }

    private string ToLowerCase(string s) => s.ToLower();

    private string ToUpperCase(string s) => s.ToUpper();
  }
}
