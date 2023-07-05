// Decompiled with JetBrains decompiler
// Type: AxisFootballColors
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class AxisFootballColors : MonoBehaviour
{
  public static Color brightBlue = new Color(0.0392156877f, 0.4862745f, 0.7294118f);
  public static Color mediumBlue = new Color(0.03137255f, 0.164705887f, 0.247058824f);
  public static Color darkBlue = new Color(0.0431372561f, 0.0627451f, 0.08627451f);
  public static Color green = new Color(0.494117647f, 0.827451f, 0.129411772f);
  public static Color red = new Color(0.929411769f, 0.2627451f, 0.356862754f);
  public static Color yellow = new Color(0.945098042f, 0.768627465f, 0.05882353f);
  public static Color orange = new Color(1f, 0.5019608f, 0.0f);
  public static Color darkGreen = new Color(0.156862751f, 0.698039234f, 0.05882353f);
  public static Color lightGray = new Color(0.5568628f, 0.5568628f, 0.5764706f);
  public static Color mediumGray = new Color(0.392156869f, 0.392156869f, 0.392156869f);
  public static Color[] barFillColor = new Color[5]
  {
    new Color(0.9607843f, 0.160784319f, 0.121568628f),
    new Color(0.968627453f, 0.670588255f, 0.117647059f),
    new Color(0.972549f, 0.905882359f, 0.109803922f),
    new Color(0.7411765f, 0.847058833f, 0.09019608f),
    new Color(0.31764707f, 0.847058833f, 0.08627451f)
  };
  public static Color[] facilityConditions = new Color[6]
  {
    Color.white,
    AxisFootballColors.red,
    AxisFootballColors.orange,
    AxisFootballColors.yellow,
    AxisFootballColors.green,
    AxisFootballColors.darkGreen
  };

  public static Color GetMainUIColor() => AxisFootballColors.brightBlue;

  public static Color GetBarColor(int value)
  {
    value -= 50;
    value /= 10;
    value = Mathf.Clamp(value, 0, AxisFootballColors.barFillColor.Length - 1);
    return AxisFootballColors.barFillColor[value];
  }

  public static Color GetBarColor_0to100(int value)
  {
    value /= 20;
    value = Mathf.Clamp(value, 0, AxisFootballColors.barFillColor.Length - 1);
    return AxisFootballColors.barFillColor[value];
  }
}
