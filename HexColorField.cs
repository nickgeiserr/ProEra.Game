// Decompiled with JetBrains decompiler
// Type: HexColorField
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof (InputField))]
public class HexColorField : MonoBehaviour
{
  public ColorPicker hsvpicker;
  public bool displayAlpha;
  private InputField hexInputField;
  private const string hexRegex = "^#?(?:[0-9a-fA-F]{3,4}){1,2}$";

  private void Awake()
  {
    this.hexInputField = this.GetComponent<InputField>();
    this.hexInputField.onEndEdit.AddListener(new UnityAction<string>(this.UpdateColor));
    this.hsvpicker.onValueChanged.AddListener(new UnityAction<Color>(this.UpdateHex));
  }

  private void OnDestroy()
  {
    this.hexInputField.onValueChanged.RemoveListener(new UnityAction<string>(this.UpdateColor));
    this.hsvpicker.onValueChanged.RemoveListener(new UnityAction<Color>(this.UpdateHex));
  }

  private void UpdateHex(Color newColor) => this.hexInputField.text = this.ColorToHex((Color32) newColor);

  private void UpdateColor(string newHex)
  {
    Color32 color;
    if (HexColorField.HexToColor(newHex, out color))
      this.hsvpicker.CurrentColor = (Color) color;
    else
      Debug.Log((object) "hex value is in the wrong format, valid formats are: #RGB, #RGBA, #RRGGBB and #RRGGBBAA (# is optional)");
  }

  private string ColorToHex(Color32 color)
  {
    if (!this.displayAlpha)
      return string.Format("#{0:X2}{1:X2}{2:X2}", (object) color.r, (object) color.g, (object) color.b);
    return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", (object) color.r, (object) color.g, (object) color.b, (object) color.a);
  }

  public static bool HexToColor(string hex, out Color32 color)
  {
    if (Regex.IsMatch(hex, "^#?(?:[0-9a-fA-F]{3,4}){1,2}$"))
    {
      int num = hex.StartsWith("#") ? 1 : 0;
      if (hex.Length == num + 8)
        color = new Color32(byte.Parse(hex.Substring(num, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 2, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 4, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 6, 2), NumberStyles.AllowHexSpecifier));
      else if (hex.Length == num + 6)
        color = new Color32(byte.Parse(hex.Substring(num, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 2, 2), NumberStyles.AllowHexSpecifier), byte.Parse(hex.Substring(num + 4, 2), NumberStyles.AllowHexSpecifier), byte.MaxValue);
      else if (hex.Length == num + 4)
      {
        ref Color32 local = ref color;
        string str1 = hex[num].ToString();
        char ch = hex[num];
        string str2 = ch.ToString();
        int r = (int) byte.Parse(str1 + str2, NumberStyles.AllowHexSpecifier);
        ch = hex[num + 1];
        string str3 = ch.ToString();
        ch = hex[num + 1];
        string str4 = ch.ToString();
        int g = (int) byte.Parse(str3 + str4, NumberStyles.AllowHexSpecifier);
        ch = hex[num + 2];
        string str5 = ch.ToString();
        ch = hex[num + 2];
        string str6 = ch.ToString();
        int b = (int) byte.Parse(str5 + str6, NumberStyles.AllowHexSpecifier);
        ch = hex[num + 3];
        string str7 = ch.ToString();
        ch = hex[num + 3];
        string str8 = ch.ToString();
        int a = (int) byte.Parse(str7 + str8, NumberStyles.AllowHexSpecifier);
        Color32 color32 = new Color32((byte) r, (byte) g, (byte) b, (byte) a);
        local = color32;
      }
      else
      {
        ref Color32 local = ref color;
        string str9 = hex[num].ToString();
        char ch = hex[num];
        string str10 = ch.ToString();
        int r = (int) byte.Parse(str9 + str10, NumberStyles.AllowHexSpecifier);
        ch = hex[num + 1];
        string str11 = ch.ToString();
        ch = hex[num + 1];
        string str12 = ch.ToString();
        int g = (int) byte.Parse(str11 + str12, NumberStyles.AllowHexSpecifier);
        ch = hex[num + 2];
        string str13 = ch.ToString();
        ch = hex[num + 2];
        string str14 = ch.ToString();
        int b = (int) byte.Parse(str13 + str14, NumberStyles.AllowHexSpecifier);
        Color32 color32 = new Color32((byte) r, (byte) g, (byte) b, byte.MaxValue);
        local = color32;
      }
      return true;
    }
    color = new Color32();
    return false;
  }
}
