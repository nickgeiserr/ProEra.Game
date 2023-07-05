// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UniformConfig
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using UnityEngine;

namespace ProEra.Game
{
  [MessagePackObject(false)]
  [Serializable]
  public class UniformConfig
  {
    [Key(0)]
    public bool IsCustomBaseUniform;
    [Key(1)]
    public string UniformSetName;
    [Key(2)]
    public string HelmetName;
    [Key(3)]
    public string JerseyName;
    [Key(4)]
    public string PantName;
    [Key(5)]
    public string NumberFontIndex;
    [Key(6)]
    public string NumberFillColor;
    [Key(7)]
    public string NumberOutline1Color;
    [Key(8)]
    public string NumberOutline2Color;
    [Key(9)]
    public string LetterFontIndex;
    [Key(10)]
    public string LetterFillColor;
    [Key(11)]
    public string LetterOutlineColor;
    [Key(12)]
    public string VisorColor;
    [Key(13)]
    public string ArmSleeveColor;
    [Key(14)]
    public string ArmBandColor;
    [Key(15)]
    public string HasSleeveNumber;
    [Key(16)]
    public string HasShoulderNumber;
    [Key(17)]
    public string HelmetType;

    public bool HelmetIsChrome() => this.HelmetType[0] == '1' || this.HelmetType[0] == 'Y';

    public bool HelmetIsMatte() => this.HelmetType[0] == '2';

    public bool JerseyHasNumberOutline1() => this.NumberOutline1Color[0] != 'N';

    public bool JerseyHasNumberOutline2() => this.NumberOutline2Color[0] != 'N';

    public bool JerseyHasShoulderNumbers() => this.HasShoulderNumber[0] != 'N';

    public bool JerseyHasSleeveNumbers() => this.HasSleeveNumber[0] != 'N';

    public bool JerseyHasLetterOutline() => this.LetterOutlineColor[0] != 'N';

    public FontType GetLetterFontType() => (FontType) int.Parse(this.LetterFontIndex);

    public FontType GetNumberFontType() => (FontType) int.Parse(this.NumberFontIndex);

    public Color GetNumberFillColor() => AssetManager.ConvertToColor(this.NumberFillColor);

    public Color GetNumberOutlineColor1() => AssetManager.ConvertToColor(this.NumberOutline1Color);

    public Color GetNumberOutlineColor2() => AssetManager.ConvertToColor(this.NumberOutline2Color);

    public Color GetLetterOutlineColor() => AssetManager.ConvertToColor(this.LetterOutlineColor);

    public Color GetLetterFillColor() => AssetManager.ConvertToColor(this.LetterFillColor);

    public Color GetArmSleevesColor() => AssetManager.ConvertToColor(this.ArmSleeveColor);

    public Color GetArmBandColor() => AssetManager.ConvertToColor(this.ArmBandColor);

    public Color GetHelmetVisorColor() => AssetManager.ConvertToColor(this.VisorColor);

    public void SetHelmetType(bool isChrome, bool isMatte)
    {
      if (isChrome)
        this.HelmetType = "1";
      else if (isMatte)
        this.HelmetType = "2";
      else
        this.HelmetType = "N";
    }

    public void SetJerseyHasShoulderNumbers(bool hasShoulderNumbers)
    {
      if (!hasShoulderNumbers)
        this.HasShoulderNumber = "N";
      else
        this.HasShoulderNumber = "Y";
    }

    public void SetJerseyHasSleeveNumbers(bool hasSleeveNumbers)
    {
      if (!hasSleeveNumbers)
        this.HasSleeveNumber = "N";
      else
        this.HasSleeveNumber = "Y";
    }

    public void SetLetterFontType(FontType type) => this.LetterFontIndex = ((int) type).ToString();

    public void SetNumberFontType(FontType type) => this.NumberFontIndex = ((int) type).ToString();

    public void SetNumberFillColor(Color c) => this.NumberFillColor = AssetManager.ConvertColorToRGB(c);

    public void SetNumberOutlineColor1(bool hasInnerOutline, Color c)
    {
      if (!hasInnerOutline)
        this.NumberOutline1Color = "N";
      else
        this.NumberOutline1Color = AssetManager.ConvertColorToRGB(c);
    }

    public void SetNumberOutlineColor2(bool hasOuterOutline, Color c)
    {
      if (!hasOuterOutline)
        this.NumberOutline2Color = "N";
      else
        this.NumberOutline2Color = AssetManager.ConvertColorToRGB(c);
    }

    public void SetLetterOutlineColor(bool hasOutline, Color c)
    {
      if (!hasOutline)
        this.LetterOutlineColor = "N";
      else
        this.LetterOutlineColor = AssetManager.ConvertColorToRGB(c);
    }

    public void SetLetterFillColor(Color c) => this.LetterFillColor = AssetManager.ConvertColorToRGB(c);

    public void SetArmSleevesColor(Color c) => this.ArmSleeveColor = AssetManager.ConvertColorToRGB(c);

    public void SetArmBandColor(Color c) => this.ArmBandColor = AssetManager.ConvertColorToRGB(c);

    public void SetHelmetVisorColor(Color c) => this.VisorColor = AssetManager.ConvertColorToRGB(c);
  }
}
